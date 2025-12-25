using System;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingUI.UIFrame.CAN;
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    /// 功能说明：

    ///
    public partial class SysControlPanel : UserControl
    {
        // 1. 初始化
        // 添加对VehicleControlFrame的引用
        //private MapVehiclePanel vehicleControlFrame;
        private MapVehiclePanel mapVehiclePanel;
        // 添加与VCUFrom一致的变量
        public CanBus canBus = null;
        public System.Threading.Timer VCUcommendTimer = null;
        public int VCUsleeptime = 100;

        // 添加速度和角速度变量
        public double speed = 0.0;
        public double angle = 0.0;
        public double STEP = 0.01; // 每次点击改变的步长
        private static readonly object _lockobject = new object(); //小车移动控制命令发送进程锁
       
        // 控制模式
        private enum ControlMode { Auto, Manual }
        private ControlMode currentControlMode = ControlMode.Manual;
        Timer VCU_CAN_Send_Timer = new Timer(); // 小车移动控制命令发送 定时器
        
        // 启动定时器
        public SysControlPanel()
        {
            InitializeComponent();
            this.Load += SysPanelFrame_Load;
            VCU_CAN_Send_Timer.Start();
            VCU_CAN_Send_Timer.Tick += Timer_Tick;
        }
        // 释放系统控制Panel资源
        ~SysControlPanel()
        {
            StopVCUCommand();
        }


        // 2. 小车移动控制命令 定时器
        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTextBox();
            STEP = float.Parse(comboBox_Step.Text);
            
            if (Math.Abs(speed) < 0.005) speed = 0;
            if (Math.Abs(angle) < 0.005) angle = 0;
        }
        // 3. 设置Can总线的方法
        public void SetCanBus(CanBus canbus)
        {
            this.canBus = canbus;
        }

        private void SysPanelFrame_Load(object sender, EventArgs e)
        {
            SetCanBus(MainFrame.canBus);
            // 禁用所有手动控制按钮
            //btnForward.Enabled = false;
            //btnBackward.Enabled = false;
            //btnRotateLeft.Enabled = false;
            //btnRotateRight.Enabled = false;
        }

        /*public void SetVehicleControlFrame(MapVehiclePanel vcf)
        {
            vehicleControlFrame = vcf;
        }*/
        public void SetVehicleControlFrame(MapVehiclePanel vcf)
        {
            mapVehiclePanel = vcf;
        }

        // 4. 手动/自动模式切换按钮点击事件
        private void btnSwitchMode_Click(object sender, EventArgs e)
        {
            if (mapVehiclePanel == null)
            {
                MessageBox.Show("未找到小车控制界面，请确保已正确初始化。");
                return;
            }

            if (currentControlMode == ControlMode.Auto)
            {
                // 切换到手动模式
                currentControlMode = ControlMode.Manual;
                btnSwitchMode.Text = "手动";
                SetCanBus(MainFrame.canBus);
                // 停止当前自动运行
                //vehicleControlFrame.StopAutoMode();

                // 启用所有手动控制按钮
                btnForward.Enabled = true;
                btnBackward.Enabled = true;
                btnRotateLeft.Enabled = true;
                btnRotateRight.Enabled = true;

                speed = 0.0;
                angle = 0.0;
            }
            else
            {
                // 切换到自动模式
                currentControlMode = ControlMode.Auto;
                btnSwitchMode.Text = "自动";
                
                // 子窗口自动运行
                //vehicleControlFrame.ResetAutoMode();

                // 禁用所有手动控制按钮
                btnForward.Enabled = false;
                btnBackward.Enabled = false;
                btnRotateLeft.Enabled = false;
                btnRotateRight.Enabled = false;

                // 确保停止手动控制命令
                StopVCUCommand();
            }
        }

        // 5. 实现小车手动控制移动的四个按钮点击事件
        private void btnForward_Click(object sender, EventArgs e)
        {
            speed += STEP;
            if (speed > 1.0) speed = 1.0; // 限制最大值

            // 如果命令已在发送中，只更新速度值
            if (VCUcommendTimer != null)
            {
                return;
            }
            else
            {
                StartSendingCommands();
            }
        }

        private void btnBackward_Click(object sender, EventArgs e)
        {
            speed -= STEP;
            if (speed < -1.0) speed = -1.0; // 限制最小值

            // 如果命令已在发送中，只更新速度值
            if (VCUcommendTimer != null)
            {
                return;
            }
            else
            {
                StartSendingCommands();
            }
        }

        private void btnRotateLeft_Click(object sender, EventArgs e)
        {

            angle -= STEP;
            if (angle < -1.0) angle = -1.0; // 限制最小值
            // 如果命令已在发送中，只更新角速度值
            if (VCUcommendTimer != null)
            {
                return;
            }
            else
            {
                StartSendingCommands();
            }
        }

        private void btnRotateRight_Click(object sender, EventArgs e)
        {

            angle += STEP;
            if (angle > 1.0) angle = 1.0; // 限制最大值
            // 如果命令已在发送中，只更新角速度值
            if (VCUcommendTimer != null)
            {
                return;
            }
            else
            {
                StartSendingCommands();
            }
        }

        // 6. CAN总线控制
        // 6.1 VCU CAN总线 启动命令发送
        public void StartSendingCommands()
        {
            try
            {
                VCUcommendTimer = new System.Threading.Timer(ExecCommand, null, 0, VCUsleeptime);
            }
            catch
            {
                Console.WriteLine("CAN发送消息失败");
            }
        }

        // 6.2 VCU CAN总线 移动控制命令发送
        private void ExecCommand(object state)
        {
            lock(_lockobject)
            {
                try
                {
                    /* 防止资源占用
                    // 检查范围
                    if (speed < -1.0 || speed > 1.0)
                    {
                        MessageBox.Show("速度超出范围！允许范围：-1.0 ~ 1.0 m/s");
                        return;
                    }

                    if (angle < -1.0 || angle > 1.0)
                    {
                        MessageBox.Show("角速度超出范围！允许范围：-1.0 ~ 1.0 rad/s");
                        return;
                    }*/

                    // 放大并转为 short（速度放大 1000 倍，角速度放大 1000 倍）
                    short scaledSpeed = (short)(speed * 1000);
                    short scaledAngle = (short)(angle * 1000);

                    // 发送指令
                    VCUCommand cmd = new VCUCommand(0x200, scaledSpeed, scaledAngle);
                    if(canBus != null) canBus.SendMessage(cmd);
                }
                catch (FormatException)
                {
                    MessageBox.Show("请输入有效的数字！");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"发生错误：{ex.Message}");
                }
            }
            
        }

        // 6.3 CAN总线 停止命令发送
        public void StopVCUCommand()
        {
            if (VCUcommendTimer != null)
            {
                VCUcommendTimer.Change(-1, -1);
                VCUcommendTimer = null;
                // 重置速度和角速度
                speed = 0.0;   angle = 0.0;
            }
        }
      
        // 7.1 线速度急停按钮
        private void button_Stop_speed_Click(object sender, EventArgs e)
        {
                // 重置速度和角速度
            speed = 0.0;
        }

        // 7.2 角速度急停按钮
        private void button_Stop_angle_Click(object sender, EventArgs e)
        {
            angle = 0.0;
        }

        // 7.3 更新速度和角速度
        private void UpdateTextBox()
        {
            textBox_angle.Text = angle.ToString("F2");
            textBox_speed.Text = speed.ToString("F2");
        }

        private void bt_alpha_speed_Click(object sender, EventArgs e)
        {
            VCUMotionCalculate.alpha_speed = double.Parse(cb_alpha_speed.Text);
        }
    }
}
