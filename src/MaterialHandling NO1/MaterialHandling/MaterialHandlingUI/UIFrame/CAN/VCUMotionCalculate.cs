using MaterialHandling.MaterialHandlingUI.UIFrame.ROS;
using MaterialHandling.MaterialHandlingUI.UIFrame.Control;
using System;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class VCUMotionCalculate
    {
        // 1.定义变量
        ROSClient rOSClient;
        SysControlPanel sysControlPanel;
        MapVehiclePanel mapVehiclePanel;
        ParameterVCUPanel parameterVCUPanel;
        // 1.1 添加定时器相关字段
        private System.Timers.Timer motionTimer;
        private double targetX, targetY, targetTheta;
        public static double DISTANCE_THRESHOLD = 3.0; // 距离容差
        public static double ANGLE_THRESHOLD = 0.05; // 角度容差（弧度）
        public static double alpha_speed = 0.4;//控制旋转速度的系数
        // 1.2 过冲检测相关变量
        private double previousX, previousY, previousTheta;
        private double previousDist = double.MaxValue;
        private double previousAngleDiff = double.MaxValue;
        private bool isOvershootDetected = false;
        private const double OVERSHOOT_THRESHOLD = 0.05; // 过冲检测阈值

        // 1.3 定义运动阶段
        private enum MotionStage
        {
            InitialRotation,  // 阶段1：初始旋转，对准目标方向
            Translation,      // 阶段2：直线平移到目标位置附近
            FinalRotation,    // 阶段3：最终旋转，调整到目标角度
            Completed         // 完成所有运动
        }
        // 1.4 添加运动完成事件
        public event EventHandler MotionCompleted;

        private MotionStage currentStage = MotionStage.InitialRotation;
        private double targetDirection = 0.0;
        private bool moveForward = true;

        // 2.初始化
        public VCUMotionCalculate()
        {
            rOSClient = MainFrame.mf.rOSClient;
            sysControlPanel = MainFrame.cf.sysControlPanel;
            mapVehiclePanel = MainFrame.cf.mapVehiclePanel;
            parameterVCUPanel = MainFrame.cf.parameterVCUPanel;
            // 初始化定时器但不启动
            motionTimer = new System.Timers.Timer(100); // 100ms检查一次
            motionTimer.Elapsed += OnMotionTimerElapsed;
            motionTimer.AutoReset = true;
        }
        // 3.计算模块
        // 3.1 角度规范化函数，将角度转换到[-π, π]范围
        private double NormalizeAngle(double angle)
        {
            while (angle > Math.PI) angle -= 2 * Math.PI;
            while (angle < -Math.PI) angle += 2 * Math.PI;
            return angle;
        }

        // 3.2 计算两个角度之间的最小差值
        private double AngleDifference(double angle1, double angle2)
        {
            double diff = NormalizeAngle(angle1 - angle2);
            return diff;
        }
        // 3.3 用于最终旋转阶段，避免在 ±π 处震荡

        private void UpdateUI(Action updateAction)
        {
            if (parameterVCUPanel.rtb_Mov.InvokeRequired)
            {
                parameterVCUPanel.rtb_Mov.Invoke(new MethodInvoker(() => updateAction()));
            }
            else
            {
                updateAction();
            }
        }

        private double FinalRotationAngleDifference(double targetAngle, double currentAngle)
        {
            // 标准化两个角度
            double normTarget = NormalizeAngle(targetAngle);
            double normCurrent = NormalizeAngle(currentAngle);

            // 计算差值
            double diff = normTarget - normCurrent;

            // 如果差值超过π，选择另一个方向（较短路径）锐角
            if (diff > Math.PI) diff -= 2 * Math.PI;
            else if (diff < -Math.PI) diff += 2 * Math.PI;

            // 检测特殊情况：如果在 ±π 边界附近且目标接近0或±π
            if ((Math.Abs(Math.Abs(normCurrent) - Math.PI) < 0.1) &&
                (Math.Abs(normTarget) < 0.1 || Math.Abs(Math.Abs(normTarget) - Math.PI) < 0.1))
            {
                // 强制朝一个固定方向旋转
                return 0.1; // 返回一个小正值，使其始终顺时针旋转
            }

            return diff;
        }
        // 3.4 计算旋转角度，确保是锐角，并确定移动方向
        private double CalculateRotationAngle(double currentX, double currentY, double currentTheta, double targetX, double targetY)
        {
            // 计算目标角度
            double dx = targetX - currentX;
            double dy = targetY - currentY;
            double targetDirection = Math.Atan2(dy, dx);

            // 标准化角度为 [-π, π]
            double normCurrentTheta = NormalizeAngle(currentTheta);
            double normTargetDirection = NormalizeAngle(targetDirection);

            // 计算最小的角度差
            double angleDiff = normTargetDirection - normCurrentTheta;
            if (angleDiff > Math.PI) angleDiff -= 2 * Math.PI;
            else if (angleDiff < -Math.PI) angleDiff += 2 * Math.PI;

            // 是否要前进
            double acuteAngle;
            if (Math.Abs(angleDiff) > Math.PI / 2)
            {
                // 当前旋转角度需要作后退操作
                moveForward = false;
                // 调整角度到更小的角度（补角
                acuteAngle = Math.Abs(angleDiff) > Math.PI ? angleDiff : -Math.Sign(angleDiff) * (Math.PI - Math.Abs(angleDiff));
            }
            else
            {
                // 当前旋转角度需要作前进操作
                moveForward = true;
                acuteAngle = angleDiff;
            }

            // 调试信息
            Console.WriteLine($"角度差={angleDiff:F4}, 锐角={acuteAngle:F4}, 正向移动={moveForward}");
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"角度差={angleDiff:F4}, 锐角={acuteAngle:F4}, 正向移动={moveForward}{Environment.NewLine}"));

            return acuteAngle;
        }

        // 检测过冲情况
        private bool DetectOvershoot(double currentX, double currentY, double currentTheta)
        {
            switch (currentStage)
            {
                case MotionStage.InitialRotation:
                case MotionStage.FinalRotation:
                    // 检测角度过冲
                    double currAngleDiff;
                    if (currentStage == MotionStage.InitialRotation)
                    {
                        // 在初始旋转阶段，检查与目标方向的角度差
                        currAngleDiff = Math.Abs(AngleDifference(targetDirection, currentTheta));
                    }
                    else
                    {
                        // 在最终旋转阶段，检查与目标角度的角度差
                        currAngleDiff = Math.Abs(AngleDifference(targetTheta, currentTheta));
                    }

                    // 如果角度差开始增大而不是减小，说明发生了过冲
                    if (currAngleDiff > previousAngleDiff && previousAngleDiff != double.MaxValue)
                    {
                        Console.WriteLine($"角度过冲检测: 当前差={currAngleDiff:F4}, 上一次差={previousAngleDiff:F4}");
                        UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"角度过冲检测: 当前差={currAngleDiff:F4}, 上一次差={previousAngleDiff:F4}{Environment.NewLine}"));
                        previousAngleDiff = currAngleDiff;
                        return true;
                    }
                    previousAngleDiff = currAngleDiff;
                    break;

                case MotionStage.Translation:
                    // 计算与目标的距离
                    double dx = targetX - currentX;
                    double dy = targetY - currentY;
                    double currentDist = Math.Sqrt(dx * dx + dy * dy);

                    // 如果距离开始增大而不是减小，说明发生了过冲
                    if (currentDist > previousDist && previousDist != double.MaxValue)
                    {
                        Console.WriteLine($"距离过冲检测: 当前距离={currentDist:F4}, 上一次距离={previousDist:F4}");
                        UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"距离过冲检测: 当前距离={currentDist:F4}, 上一次距离={previousDist:F4}{Environment.NewLine}"));
                        previousDist = currentDist;
                        return true;
                    }
                    previousDist = currentDist;
                    break;
            }

            return false;
        }
        public async Task CalculateAsync(string destinationX, string destinationY, string destinationTheta)
        {
            // 确保ROS连接状态正确
            EnsureROSConnection();
            if (destinationX == "") return;
            if (destinationY == "") return;
            if (destinationTheta == "") return;
            // 保存目标位姿为实例变量，供定时器回调使用
            targetX = double.Parse(destinationX);
            targetY = double.Parse(destinationY);
            targetTheta = double.Parse(destinationTheta);

            // 重置运动阶段
            currentStage = MotionStage.InitialRotation;

            // 重置过冲检测相关变量
            previousDist = double.MaxValue;
            previousAngleDiff = double.MaxValue;
            isOvershootDetected = false;

            // 获取当前位置计算初始目标方向
            double currentX = double.Parse(mapVehiclePanel.carPosition.X.ToString());
            double currentY = double.Parse(mapVehiclePanel.carPosition.Y.ToString());
            double currentTheta = double.Parse(mapVehiclePanel.carTheta.ToString());

            // 保存初始位置
            previousX = currentX;
            previousY = currentY;
            previousTheta = currentTheta;

            // 计算初始旋转角度并决定前进/后退
            double rotAngle = CalculateRotationAngle(currentX, currentY, currentTheta, targetX, targetY);
            double dx = targetX - currentX;
            double dy = targetY - currentY;
            targetDirection = Math.Atan2(dy, dx);

            Console.WriteLine($"运动规划: 当前位置=({currentX},{currentY}), 目标=({targetX},{targetY})");
            Console.WriteLine($"初始方向={currentTheta}, 目标方向={targetDirection}, 最终方向={targetTheta}, 移动方向={(moveForward ? "前进" : "后退")}");
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"运动规划: 当前位置 = ({ currentX},{ currentY}), 目标 = ({ targetX},{ targetY}){Environment.NewLine}"));
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"初始方向 ={ currentTheta}, 目标方向 ={ targetDirection}, 最终方向 ={ targetTheta}, 移动方向 ={ (moveForward ? "前进" : "后退")}{Environment.NewLine}"));

            // 立即执行一次运动逻辑
            UpdateMotion();

            // 启动定时器持续监控和更新运动
            if (!motionTimer.Enabled)
            {
                motionTimer.Start();
                Console.WriteLine("运动定时器已启动，开始三阶段运动控制");
            }
        }
        // 4. 操控逻辑
        // 4.1 计算 - 运动逻辑（运动逻辑初始化）
        public void Calculate(double destinationX, double destinationY, double destinationTheta)
        {
            // 确保ROS连接状态正确
            EnsureROSConnection();

            // 保存目标位姿  ( 实例变量，供定时器回调使用
            targetX = destinationX;
            targetY = destinationY;
            targetTheta = destinationTheta;

            // 重置运动阶段
            currentStage = MotionStage.InitialRotation;

            // 重置过冲检测相关变量
            previousDist = double.MaxValue;//距离过冲值
            previousAngleDiff = double.MaxValue;//角度过冲值
            isOvershootDetected = false;//是否检测到过冲的标志位

            // 获取当前位置计算初始目标方向
            double currentX = mapVehiclePanel.carPosition.X;
            double currentY = mapVehiclePanel.carPosition.Y;
            double currentTheta = mapVehiclePanel.carTheta;

            // 保存初始位置
            previousX = currentX;
            previousY = currentY;
            previousTheta = currentTheta;

            // 计算初始旋转角度并决定前进/后退
            double rotAngle = CalculateRotationAngle(currentX, currentY, currentTheta, targetX, targetY);
            double dx = targetX - currentX;
            double dy = targetY - currentY;
            targetDirection = Math.Atan2(dy, dx);

            Console.WriteLine($"运动规划: 当前位置=({currentX},{currentY}), 目标=({targetX},{targetY})");
            Console.WriteLine($"初始方向={currentTheta}, 目标方向={targetDirection}, 最终方向={targetTheta}, 移动方向={(moveForward ? "前进" : "后退")}");
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"运动规划: 当前位置 = ({ currentX},{ currentY}), 目标 = ({ targetX},{ targetY}){Environment.NewLine}"));
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"初始方向 ={ currentTheta}, 目标方向 ={ targetDirection}, 最终方向 ={ targetTheta}, 移动方向 ={ (moveForward ? "前进" : "后退")}{Environment.NewLine}"));

            // 立即执行一次运动逻辑
            UpdateMotion();

            // 启动定时器持续监控和更新运动
            if (!motionTimer.Enabled)
            {
                motionTimer.Start();
                Console.WriteLine("运动定时器已启动，开始三阶段运动控制");
            }
        }
        // 4.2 定时器内的更新运动
        private void OnMotionTimerElapsed(object sender, ElapsedEventArgs e)
        {
            try
            {
                UpdateMotion();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"运动更新异常: {ex.Message}");
                StopMotion();
            }
        }
        // 4.3 更新运动 - 具体的运动逻辑
        private void UpdateMotion()
        {
            // 获取当前位姿
            double currentX = mapVehiclePanel.carPosition.X;
            double currentY = mapVehiclePanel.carPosition.Y;
            double currentTheta = mapVehiclePanel.carTheta;

            // 检测是否发生过冲
            bool isOvershoot = DetectOvershoot(currentX, currentY, currentTheta);
            
            // 处理过冲情况

            if (isOvershoot && !isOvershootDetected)
            {
                isOvershootDetected = true;
                // 发送反向指令以校正过冲
                HandleOvershoot(currentX, currentY, currentTheta);
                return;
            }
            else if (!isOvershoot)
            {
                // 重置过冲标志
                isOvershootDetected = false;
            }

            // 保存当前位置为上一位置，用于下次检测
            previousX = currentX;
            previousY = currentY;
            previousTheta = currentTheta;

            // 根据当前运动阶段执行不同逻辑
            switch (currentStage)
            {
                case MotionStage.InitialRotation:
                    // 阶段1：初始旋转，对准目标方向
                    double rotAngle = CalculateRotationAngle(currentX, currentY, currentTheta, targetX, targetY);

                    if (Math.Abs(rotAngle) > ANGLE_THRESHOLD)
                    {
                        // 根据锐角计算旋转速度，使用P控制
                        double rotationSpeed = Math.Sign(rotAngle) * Math.Min(0.1, Math.Abs(rotAngle) * alpha_speed);

                        sysControlPanel.angle = rotationSpeed;
                        sysControlPanel.speed = 0; // 纯旋转阶段，不移动
                        

                        Console.WriteLine($"阶段1-旋转: 当前角度={currentTheta:F4}, 目标方向={targetDirection:F4}, " +
                                         $"旋转角度={rotAngle:F4}, 角速度={rotationSpeed:F4}");
                        UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"阶段1-旋转: 当前角度={currentTheta:F4}, 目标方向={targetDirection:F4}, " +
                                         $"旋转角度={rotAngle:F4}, 角速度={rotationSpeed:F4}{Environment.NewLine}"));
                    }
                    else
                    {
                        // 旋转完成，进入平移阶段
                        sysControlPanel.angle = 0;
                        sysControlPanel.speed = 0;
                        

                        // 重置过冲检测变量
                        previousDist = double.MaxValue;
                        previousAngleDiff = double.MaxValue;

                        currentStage = MotionStage.Translation;
                        Console.WriteLine("初始旋转完成，进入平移阶段");
                        UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"初始旋转完成，进入平移阶段{Environment.NewLine}"));
                    }
                    break;

                case MotionStage.Translation:
                    // 阶段2：直线平移到目标位置附近
                    double dx = targetX - currentX;
                    double dy = targetY - currentY;
                    double distance = Math.Sqrt(dx * dx + dy * dy);

                    // 重新计算旋转角度确保方向正确
                    double directionAngle = CalculateRotationAngle(currentX, currentY, currentTheta, targetX, targetY);

                    if (distance > DISTANCE_THRESHOLD)
                    {
                        // 首先检查是否仍然朝向目标方向，并进行角度调整
                        if (Math.Abs(directionAngle) > ANGLE_THRESHOLD * 2)
                        {
                            // 方向偏离太大，需要重新调整角度
                            double adjustSpeed = Math.Sign(directionAngle) * Math.Min(0.1, Math.Abs(directionAngle) * alpha_speed);
                            sysControlPanel.speed = 0;
                            sysControlPanel.angle = adjustSpeed;
                            

                            Console.WriteLine($"平移中方向调整: 当前角度={currentTheta:F4}, " +
                                             $"旋转角度={directionAngle:F4}, 是否前进={moveForward}");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"平移中方向调整: 当前角度={currentTheta:F4}, " +
                                             $"旋转角度={directionAngle:F4}, 是否前进={moveForward}{Environment.NewLine}"));
                        }
                        else
                        {
                            // 方向正确，执行平移
                            // 使用P控制计算速度，距离越近速度越小
                            double baseSpeed = Math.Min(0.1, Math.Max(0.01, distance * alpha_speed));
                            double moveSpeed = moveForward ? baseSpeed : -baseSpeed; // 根据前进/后退标志设置速度

                            sysControlPanel.speed = moveSpeed;
                            sysControlPanel.angle = 0;
                            

                            Console.WriteLine($"阶段2-平移: 当前位置=({currentX:F2},{currentY:F2}), " +
                                             $"目标=({targetX:F2},{targetY:F2}), 距离={distance:F2}, 速度={moveSpeed:F4}, 前进={moveForward}");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"阶段2-平移: 当前位置=({currentX:F2},{currentY:F2}), " +
                                             $"目标=({targetX:F2},{targetY:F2}), 距离={distance:F2}, 速度={moveSpeed:F4}, 前进={moveForward}{Environment.NewLine}"));
                        }
                    }
                    else
                    {
                        // 平移完成，进入最终旋转阶段
                        sysControlPanel.speed = 0;
                        sysControlPanel.angle = 0;
                        

                        // 重置过冲检测变量
                        previousDist = double.MaxValue;
                        previousAngleDiff = double.MaxValue;

                        currentStage = MotionStage.FinalRotation;
                        Console.WriteLine("平移阶段完成，进入最终旋转阶段");
                        UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"平移阶段完成，进入最终旋转阶段{Environment.NewLine}"));
                    }
                    break;

                case MotionStage.FinalRotation:
                    // 阶段3：最终旋转到目标朝向

                    // 检测是否处于±π边界情况
                    bool isNearPiBoundary = (Math.Abs(Math.Abs(currentTheta) - Math.PI) < 0.2) &&
                                           (Math.Abs(targetTheta) < 0.2);

                    // 处理特殊情况
                    if (isNearPiBoundary)
                    {
                        // 在边界情况下，强制使用固定方向旋转
                        double forcedRotSpeed = 0.3 * alpha_speed; // 始终使用正向旋转

                        // 检查是否已接近目标
                        if (Math.Abs(currentTheta) < 0.1 || Math.Abs(Math.Abs(currentTheta) - 2 * Math.PI) < 0.1)
                        {
                            // 已接近0，完成旋转
                            sysControlPanel.angle = 0;
                            sysControlPanel.speed = 0;
                            
                            currentStage = MotionStage.Completed;
                            Console.WriteLine("边界情况处理完成，到达目标位姿");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"边界情况处理完成，到达目标位姿{ Environment.NewLine}"));
                        }
                        else
                        {
                            // 继续朝固定方向旋转
                            sysControlPanel.angle = forcedRotSpeed;
                            sysControlPanel.speed = 0;
                            
                            Console.WriteLine($"±π边界处理: 当前角度={currentTheta:F4}, 目标角度={targetTheta:F4}, " +
                                             $"使用固定正向旋转={forcedRotSpeed}");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"±π边界处理: 当前角度={currentTheta:F4}, 目标角度={targetTheta:F4}, " +
                                             $"使用固定正向旋转={forcedRotSpeed}{Environment.NewLine}"));
                        }
                    }
                    else
                    {
                        // 常规情况，计算最短路径
                        double finalAngleError = AngleDifference(targetTheta, currentTheta);

                        if (Math.Abs(finalAngleError) > ANGLE_THRESHOLD)
                        {
                            // 计算旋转速度
                            double finalRotSpeed = Math.Sign(finalAngleError) * alpha_speed;

                            sysControlPanel.angle = finalRotSpeed;
                            sysControlPanel.speed = 0;
                            

                            Console.WriteLine($"阶段3-最终旋转: 当前角度={currentTheta:F4}, 目标角度={targetTheta:F4}, " +
                                             $"误差={finalAngleError:F4}, 角速度={finalRotSpeed:F4}");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"阶段3-最终旋转: 当前角度={currentTheta:F4}, 目标角度={targetTheta:F4}, " +
                                             $"误差={finalAngleError:F4}, 角速度={finalRotSpeed:F4}{Environment.NewLine}"));
                        }
                        else
                        {
                            // 最终旋转完成
                            sysControlPanel.angle = 0;
                            sysControlPanel.speed = 0;
                            
                            currentStage = MotionStage.Completed;
                            Console.WriteLine("最终旋转完成，到达目标位姿");
                            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"最终旋转完成，到达目标位姿{Environment.NewLine}"));
                        }
                    }
                    break;



                case MotionStage.Completed:
                    // 已完成所有运动，停止
                    StopMotion();
                    MotionCompleted?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        // 4.4 处理过冲情况
        private void HandleOvershoot(double currentX, double currentY, double currentTheta)
        {
            Console.WriteLine("检测到过冲，应用反向校正...");
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"检测到过冲，应用反向校正...{Environment.NewLine}"));
            switch (currentStage)
            {
                case MotionStage.InitialRotation:
                case MotionStage.FinalRotation:
                    // 旋转过冲，给反向旋转速度
                    double currentAngleSpeed = sysControlPanel.angle;
                    double correctionSpeed = -currentAngleSpeed * 0.8; // 使用80%的反向速度纠正

                    sysControlPanel.angle = correctionSpeed;
                    sysControlPanel.speed = 0;
                    

                    Console.WriteLine($"角度过冲校正: 应用反向角速度={correctionSpeed:F4}");
                    UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"角度过冲校正: 应用反向角速度 ={ correctionSpeed: F4}{Environment.NewLine}"));
                    break;

                case MotionStage.Translation:
                    // 平移过冲，给反向速度
                    double currentMoveSpeed = sysControlPanel.speed;
                    double correctionMoveSpeed = -currentMoveSpeed * 0.7; // 使用70%的反向速度纠正

                    sysControlPanel.speed = correctionMoveSpeed;
                    sysControlPanel.angle = 0;
                    

                    Console.WriteLine($"距离过冲校正: 应用反向速度={correctionMoveSpeed:F4}");
                    UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"距离过冲校正: 应用反向速度={correctionMoveSpeed:F4}{Environment.NewLine}"));
                    break;
            }

            // 短暂应用校正后，下一次更新会恢复正常控制
        }
        // 4.5 停止运动
        public void StopMotion()
        {
            // 停止定时器
            motionTimer.Stop();
            // 最终旋转完成
            sysControlPanel.angle = 0;
            sysControlPanel.speed = 0;
            // 停止车辆
            //sysControlPanel.StopVCUCommand();
            Console.WriteLine("运动控制已结束");
            UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"运动控制已结束{Environment.NewLine}"));
            //this.Dispose();
        }


        // 4.6 确保ROS连接 
        private void EnsureROSConnection()
        {
            // 检查连接状态
            if (!rOSClient.poseIsRunning)
            {
                Console.WriteLine("ROS位姿连接未启动，尝试重新连接...");
                rOSClient.ROSConnect(); // 重新建立连接
                // ROS状态栏输出连接未启动


                // 给一点时间让连接建立
                Task.Delay(1000).Wait();

                if (!rOSClient.poseIsRunning)
                {
                    //throw new InvalidOperationException("无法建立ROS位姿连接，请检查网络状态和ROS服务器");
                    Console.WriteLine("无法建立ROS位姿连接，请检查网络状态和ROS服务器");
                }
            }

            Console.WriteLine($"ROS连接状态: poseIsRunning={rOSClient.poseIsRunning}");
        }

        // 4.7 清理资源的方法
        public void Dispose()
        {
            if (motionTimer != null)
            {
                motionTimer.Stop();
                motionTimer.Elapsed -= OnMotionTimerElapsed;
                motionTimer.Dispose();
                motionTimer = null;
            }
        }
        // 4.8 中断小车当前的运动
        /// <summary>
        /// 中断小车当前的运动
        /// </summary>
        /// <returns>返回是否成功中断运动</returns>
        public bool InterruptMotion()
        {
            try
            {
                // 停止定时器
                if (motionTimer != null && motionTimer.Enabled)
                {
                    motionTimer.Stop();
                    Console.WriteLine("运动定时器已停止");
                }

                // 立即停止小车
                sysControlPanel.angle = 0;
                sysControlPanel.speed = 0;

                // 发送停止命令
                if (sysControlPanel.VCUcommendTimer != null)
                {
                    sysControlPanel.StopVCUCommand();
                }

                // 重置运动阶段
                currentStage = MotionStage.Completed;

                // 记录中断位置
                double interruptX = mapVehiclePanel.carPosition.X;
                double interruptY = mapVehiclePanel.carPosition.Y;
                double interruptTheta = mapVehiclePanel.carTheta;

                Console.WriteLine($"运动已中断，当前位置=({interruptX:F2},{interruptY:F2}), 当前角度={interruptTheta:F4}");
                UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"运动已中断，当前位置=({interruptX:F2},{interruptY:F2}), 当前角度={interruptTheta:F4}{Environment.NewLine}"));

                // 触发运动完成事件，但传递中断状态
                MotionInterrupted?.Invoke(this, new MotionEventArgs { IsInterrupted = true });

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"中断运动时发生错误: {ex.Message}");
                UpdateUI(() => parameterVCUPanel.rtb_Mov.AppendText($"中断运动时发生错误: {ex.Message}{Environment.NewLine}"));
                return false;
            }
        }

        // 添加运动中断事件和事件参数类
        public event EventHandler<MotionEventArgs> MotionInterrupted;

        public class MotionEventArgs : EventArgs
        {
            public bool IsInterrupted { get; set; }
        }

    }
}
