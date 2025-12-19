using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

namespace CSharpApp
{
    class Program
    {
        // 定义回调函数类型
        public delegate void RobotStatusCallbackDelegate(IntPtr pRobotStatus);

        // 定义常量
        public const int HRC_AXIS_NUM_MAX = 7;      // 当前最大机器人内轴最大支持数目
        public const int HRC_EAXIS_NUM_MAX = 7;     // 用户最大定义外部轴数目
        public const int BUFFER_LEN_256 = 256;      // 256字节缓存区长度
        public const int BUFFER_LEN_32 = 32;        // 32字节缓存区长度（用于连接信息）
        public const int BUFFER_LEN_64 = 64;        // 64字节缓存区长度（用户信息）

        // SDK回调类型和成功代码
        public const int MP_CALLBACK_FUN_ROBOT_STATUS = 1;
        public const long MP_SUCCESS = 0;

        // 新增：工作模式枚举（对应C++的E_WORKING_MODE）
        public enum E_WORKING_MODE : int
        {
            MP_WORKING_MODE_MANUAL_LS = 0,  // 手动低速模式
            MP_WORKING_MODE_MANUAL_HS = 1,  // 手动高速模式
            MP_WORKING_MODE_AUTO = 2        // 自动模式
        }

        // 电机状态枚举
        public enum E_MOTOR_STATE : int
        {
            MP_MOTOR_STATE_DISABLE = 0,
            MP_MOTOR_STATE_ENABLE = 1,
            MP_MOTOR_STATE_ERROR = 2
        }

        // 程序状态枚举
        public enum E_PROGRAM_STATE : int
        {
            MP_PROGRAM_STATE_NOT_LOAD = 0,
            MP_PROGRAM_STATE_LOADED = 1
        }

        // 运动状态枚举
        public enum E_MOV_STATUS : int
        {
            MP_MOV_STATUS_ALREADY_STOP = 0,
            MP_MOV_STATUS_IN_ACTION = 1,
            MP_MOV_IN_STOP_PROCESS = 2
        }

        // 负载辨识状态枚举
        public enum E_LOAD_IDENTIFY_STATUS : int
        {
            E_LOAD_IDENTIFY_IDLE = 0,
            E_LOAD_IDENTIFY_RUNNING = 1
        }

        // 用户类型枚举
        public enum E_USER_TYPE : int
        {
            MP_USER_ADMIN = 0,        // 管理员
            MP_USER_PROGRAMMER = 1,   // 示教员
            MP_USER_OPERATOR = 2      // 操作员
        }

        // 用户信息结构体
        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct ST_USER_INFO
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = BUFFER_LEN_64)]
            public byte[] szUserName;                // 用户名

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = BUFFER_LEN_64)]
            public byte[] szUserPwd;                 // 密码

            public E_USER_TYPE eUserType;            // 用户类型

            public override string ToString()
            {
                string userName = GetStringFromBytes(szUserName);
                string userPwd = "******"; // 不显示密码
                return $"用户名: {userName}, 用户类型: {GetUserTypeString(eUserType)}";
            }
        }

        // HRC_EULER 结构体定义
        [StructLayout(LayoutKind.Sequential, Pack = 8)]
        public struct HRC_EULER
        {
            public double x;
            public double y;
            public double z;
            public double roll;
            public double pitch;
            public double yaw;

            public override string ToString()
            {
                return $"位置: ({x:F2}, {y:F2}, {z:F2}) mm, 姿态: (roll:{roll:F2}, pitch:{pitch:F2}, yaw:{yaw:F2}) deg";
            }
        }

        // ST_ROBOT_STATUS 结构体定义
        [StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Ansi)]
        public struct ST_ROBOT_STATUS
        {
            public E_MOTOR_STATE eServoState;
            public E_PROGRAM_STATE eProgramState;
            public E_MOV_STATUS eMovStatus;
            public int emergency_stat;
            public int pendent_connect_button;
            public int controller_stat;
            public int jnt_num;
            public int exjnt_num;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HRC_AXIS_NUM_MAX)]
            public double[] jnt;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HRC_EAXIS_NUM_MAX)]
            public double[] exJnt;

            public HRC_EULER pose;
            public HRC_EULER base_pose;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = HRC_AXIS_NUM_MAX)]
            public long[] encode_pulse;

            public int safe_gate_stat;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = BUFFER_LEN_256)]
            public byte[] cur_project_name;

            public int nMotionModeState;
            public int nIOSimulationState;
            public E_LOAD_IDENTIFY_STATUS eLoadIdentifyStatus;
            public double dCurMotionExecutionPercent;
            public ulong uUpdateTime;
        }

        // 声明 MPSDK 函数（新增 MP_SetWorkMode）
        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr MP_CreateHandle();

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_DestroyHandle(IntPtr handle);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_SetUserCallback(
            IntPtr handle,
            int callbackType,
            RobotStatusCallbackDelegate callback,
            IntPtr pRobotStatus);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern long MP_ConnectCtrlWithIP(IntPtr handle, string connectInfo);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_RequestControlAccess(IntPtr handle);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern long MP_SwitchUser(IntPtr handle, ref ST_USER_INFO pUserInfo);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_SetMotorOn(IntPtr handle);

        // 新增：设置工作模式函数声明
        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_SetWorkMode(IntPtr handle, E_WORKING_MODE workMode);
        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_EliminateAllAlarm(IntPtr handle);
        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_SetMotorOff(IntPtr handle);

        [DllImport("MPSDK.dll", CallingConvention = CallingConvention.StdCall)]
        public static extern long MP_DisConnectCTRL(IntPtr handle);
        

        // 机器人状态回调函数
        private static void OnRobotStatusChanged(IntPtr pRobotStatus)
        {
            try
            {
                ST_ROBOT_STATUS robotStatus = Marshal.PtrToStructure<ST_ROBOT_STATUS>(pRobotStatus);
                PrintRobotStatus(robotStatus);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[回调错误] {DateTime.Now:HH:mm:ss.fff}: {ex.Message}");
            }
        }

        // 打印机器人状态信息
        private static void PrintRobotStatus(ST_ROBOT_STATUS status)
        {
            // 减少输出频率，避免控制台过于频繁更新
            if (DateTime.Now.Second % 2 != 0)
                return;

            Console.WriteLine($"\n[状态更新] {DateTime.Now:HH:mm:ss.fff}");
            Console.WriteLine("=========================================");

            // 基本状态
            Console.WriteLine($"电机状态: {GetMotorStateString(status.eServoState)}");
            Console.WriteLine($"程序状态: {GetProgramStateString(status.eProgramState)}");
            Console.WriteLine($"运动状态: {GetMovStatusString(status.eMovStatus)}");
            Console.WriteLine($"急停状态: {status.emergency_stat}");
            Console.WriteLine($"示教器连接: {status.pendent_connect_button}");
            Console.WriteLine($"控制器状态: {status.controller_stat}");
            Console.WriteLine($"安全门状态: {status.safe_gate_stat}");

            // 关节信息
            Console.WriteLine($"\n控制轴数目: {status.jnt_num}");
            if (status.jnt != null && status.jnt_num > 0)
            {
                Console.Write("关节角度: ");
                for (int i = 0; i < Math.Min(status.jnt_num, HRC_AXIS_NUM_MAX); i++)
                {
                    Console.Write($"J{i + 1}:{status.jnt[i]:F2}° ");
                }
                Console.WriteLine();
            }

            // 扩展关节信息
            Console.WriteLine($"\n扩展轴数目: {status.exjnt_num}");
            if (status.exJnt != null && status.exjnt_num > 0)
            {
                Console.Write("扩展关节角度: ");
                for (int i = 0; i < Math.Min(status.exjnt_num, HRC_EAXIS_NUM_MAX); i++)
                {
                    Console.Write($"EX{i + 1}:{status.exJnt[i]:F2}° ");
                }
                Console.WriteLine();
            }

            // 笛卡尔位姿
            Console.WriteLine($"\n笛卡尔位姿:");
            Console.WriteLine($"  - 当前位置: {status.pose}");
            Console.WriteLine($"  - 基坐标系: {status.base_pose}");

            // 编码器脉冲值
            if (status.encode_pulse != null && status.jnt_num > 0)
            {
                Console.Write("编码器脉冲: ");
                for (int i = 0; i < Math.Min(status.jnt_num, 3); i++)
                {
                    Console.Write($"{status.encode_pulse[i]} ");
                }
                if (status.jnt_num > 3) Console.Write("...");
                Console.WriteLine();
            }

            // 工程名
            string projectName = GetStringFromBytes(status.cur_project_name);
            if (!string.IsNullOrEmpty(projectName))
            {
                Console.WriteLine($"当前工程: {projectName}");
            }

            // 其他状态
            Console.WriteLine($"运动模式状态: {(status.nMotionModeState == 0 ? "不在运动SDK模式" : "在运动SDK模式")}");
            Console.WriteLine($"DI仿真状态: {(status.nIOSimulationState == 0 ? "关闭" : "开启")}");
            Console.WriteLine($"负载辨识状态: {GetLoadIdentifyStatusString(status.eLoadIdentifyStatus)}");

            // 动作执行百分比
            string percentStr;
            if (status.dCurMotionExecutionPercent == -1.0)
                percentStr = "未知";
            else
                percentStr = $"{status.dCurMotionExecutionPercent:F2}%";
            Console.WriteLine($"动作执行百分比: {percentStr}");

            // 更新时间
            if (status.uUpdateTime > 0)
            {
                try
                {
                    DateTime updateTime = DateTimeOffset.FromUnixTimeMilliseconds((long)status.uUpdateTime).DateTime;
                    Console.WriteLine($"更新时间: {updateTime:yyyy-MM-dd HH:mm:ss.fff}");
                }
                catch
                {
                    Console.WriteLine($"更新时间戳: {status.uUpdateTime}");
                }
            }

            Console.WriteLine("=========================================\n");
        }

        // 切换用户函数
        private static long SwitchUser(IntPtr handle, string userName, string userPwd, E_USER_TYPE userType)
        {
            try
            {
                Console.WriteLine($"\n正在切换用户: {userName} ({GetUserTypeString(userType)})");

                // 创建用户信息结构体
                ST_USER_INFO userInfo = new ST_USER_INFO
                {
                    szUserName = new byte[BUFFER_LEN_64],
                    szUserPwd = new byte[BUFFER_LEN_64],
                    eUserType = userType
                };

                // 复制用户名到字节数组
                byte[] userNameBytes = Encoding.ASCII.GetBytes(userName);
                int userNameLength = Math.Min(userNameBytes.Length, BUFFER_LEN_64 - 1);
                Array.Copy(userNameBytes, 0, userInfo.szUserName, 0, userNameLength);
                userInfo.szUserName[userNameLength] = 0; // 添加终止符

                // 复制密码到字节数组
                byte[] userPwdBytes = Encoding.ASCII.GetBytes(userPwd);
                int userPwdLength = Math.Min(userPwdBytes.Length, BUFFER_LEN_64 - 1);
                Array.Copy(userPwdBytes, 0, userInfo.szUserPwd, 0, userPwdLength);
                userInfo.szUserPwd[userPwdLength] = 0; // 添加终止符

                // 调用SDK切换用户
                long llRet = MP_SwitchUser(handle, ref userInfo);

                if (llRet != MP_SUCCESS)
                {
                    Console.WriteLine($"切换用户失败! 错误码: 0x{llRet:X}");
                }
                else
                {
                    Console.WriteLine("切换用户成功!");
                }

                return llRet;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"切换用户时发生异常: {ex.Message}");
                return -1;
            }
        }

        // 辅助函数：获取状态字符串
        private static string GetMotorStateString(E_MOTOR_STATE state)
        {
            switch (state)
            {
                case E_MOTOR_STATE.MP_MOTOR_STATE_DISABLE: return "电机非使能";
                case E_MOTOR_STATE.MP_MOTOR_STATE_ENABLE: return "电机使能";
                case E_MOTOR_STATE.MP_MOTOR_STATE_ERROR: return "电机错误";
                default: return $"未知({(int)state})";
            }
        }

        private static string GetProgramStateString(E_PROGRAM_STATE state)
        {
            switch (state)
            {
                case E_PROGRAM_STATE.MP_PROGRAM_STATE_NOT_LOAD: return "未加载";
                case E_PROGRAM_STATE.MP_PROGRAM_STATE_LOADED: return "已加载";
                default: return $"未知({(int)state})";
            }
        }

        private static string GetMovStatusString(E_MOV_STATUS state)
        {
            switch (state)
            {
                case E_MOV_STATUS.MP_MOV_STATUS_ALREADY_STOP: return "已停止";
                case E_MOV_STATUS.MP_MOV_STATUS_IN_ACTION: return "运动中";
                case E_MOV_STATUS.MP_MOV_IN_STOP_PROCESS: return "停止过程中";
                default: return $"未知({(int)state})";
            }
        }

        private static string GetLoadIdentifyStatusString(E_LOAD_IDENTIFY_STATUS state)
        {
            switch (state)
            {
                case E_LOAD_IDENTIFY_STATUS.E_LOAD_IDENTIFY_IDLE: return "空闲";
                case E_LOAD_IDENTIFY_STATUS.E_LOAD_IDENTIFY_RUNNING: return "执行中";
                default: return $"未知({(int)state})";
            }
        }

        private static string GetUserTypeString(E_USER_TYPE userType)
        {
            switch (userType)
            {
                case E_USER_TYPE.MP_USER_ADMIN: return "管理员";
                case E_USER_TYPE.MP_USER_PROGRAMMER: return "示教员";
                case E_USER_TYPE.MP_USER_OPERATOR: return "操作员";
                default: return $"未知({(int)userType})";
            }
        }

        // 辅助函数：从字节数组获取字符串
        private static string GetStringFromBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
                return string.Empty;

            int length = 0;
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] == 0)
                {
                    length = i;
                    break;
                }
            }

            if (length == 0) length = bytes.Length;

            return Encoding.ASCII.GetString(bytes, 0, length);
        }

        // 辅助函数：分配并初始化机器人状态结构体
        private static IntPtr AllocateRobotStatus()
        {
            try
            {
                ST_ROBOT_STATUS status = new ST_ROBOT_STATUS
                {
                    jnt = new double[HRC_AXIS_NUM_MAX],
                    exJnt = new double[HRC_EAXIS_NUM_MAX],
                    encode_pulse = new long[HRC_AXIS_NUM_MAX],
                    cur_project_name = new byte[BUFFER_LEN_256]
                };

                int size = Marshal.SizeOf(status);
                Console.WriteLine($"ST_ROBOT_STATUS 结构体大小: {size} 字节");

                IntPtr ptr = Marshal.AllocHGlobal(size);

                // 初始化数组为零
                for (int i = 0; i < HRC_AXIS_NUM_MAX; i++)
                {
                    status.jnt[i] = 0.0;
                    status.encode_pulse[i] = 0;
                }

                for (int i = 0; i < HRC_EAXIS_NUM_MAX; i++)
                {
                    status.exJnt[i] = 0.0;
                }

                for (int i = 0; i < BUFFER_LEN_256; i++)
                {
                    status.cur_project_name[i] = 0;
                }

                Marshal.StructureToPtr(status, ptr, false);

                return ptr;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"分配机器人状态内存失败: {ex.Message}");
                return IntPtr.Zero;
            }
        }

        // 辅助函数：释放机器人状态结构体
        private static void FreeRobotStatus(IntPtr ptr)
        {
            if (ptr != IntPtr.Zero)
            {
                try
                {
                    Marshal.FreeHGlobal(ptr);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"释放机器人状态内存失败: {ex.Message}");
                }
            }
        }

        static void Main(string[] args)
        {
            Console.Title = "机器人状态监控系统";
            Console.WriteLine("C# 调用 MPSDK.dll 示例 - 机器人状态监控");
            Console.WriteLine("=======================================");
            Console.WriteLine($"应用程序架构: {(Environment.Is64BitProcess ? "64位" : "32位")}");
            Console.WriteLine($"当前目录: {AppDomain.CurrentDomain.BaseDirectory}");

            IntPtr pHandle = IntPtr.Zero;
            IntPtr pRobotStatus = IntPtr.Zero;

            try
            {
                // 1. 创建句柄
                Console.WriteLine("\n正在创建 MPSDK 句柄...");
                pHandle = MP_CreateHandle();

                if (pHandle == IntPtr.Zero)
                {
                    Console.WriteLine("错误: 创建句柄失败!");
                    Console.WriteLine("可能原因:");
                    Console.WriteLine("1. MPSDK.dll 未找到或加载失败");
                    Console.WriteLine("2. 平台架构不匹配 (x86/x64)");
                    Console.WriteLine("3. 缺少依赖的 DLL 文件");
                    Console.WriteLine("4. 硬件连接问题");
                    return;
                }

                Console.WriteLine($"成功创建句柄: 0x{pHandle.ToString("X")}");

                // 2. 分配机器人状态结构体内存
                Console.WriteLine("\n分配机器人状态内存...");
                pRobotStatus = AllocateRobotStatus();

                if (pRobotStatus == IntPtr.Zero)
                {
                    Console.WriteLine("错误: 分配机器人状态内存失败!");
                    MP_DestroyHandle(pHandle);
                    return;
                }

                Console.WriteLine($"分配的内存地址: 0x{pRobotStatus.ToString("X")}");

                // 3. 设置回调函数
                Console.WriteLine($"\n立即设置机器人状态回调 (类型: {MP_CALLBACK_FUN_ROBOT_STATUS})...");
                long callbackResult = MP_SetUserCallback(
                    pHandle,
                    MP_CALLBACK_FUN_ROBOT_STATUS,
                    OnRobotStatusChanged,
                    pRobotStatus);

                if (callbackResult != MP_SUCCESS)
                {
                    Console.WriteLine($"警告: 设置回调失败! 错误码: 0x{callbackResult:X}");
                    Console.WriteLine("回调设置失败，但继续尝试连接控制器...");
                }
                else
                {
                    Console.WriteLine("回调设置成功!");
                }

                // 4. 等待初始化
                Console.WriteLine("初始化中...");
                Thread.Sleep(1000);

                // 5. 连接控制器
                Console.WriteLine("\n连接控制器...");
                string connectInfo = "192.168.2.64:9000";
                long connectResult = MP_ConnectCtrlWithIP(pHandle, connectInfo);

                if (connectResult != MP_SUCCESS)
                {
                    Console.WriteLine($"连接控制器失败! 错误码: 0x{connectResult:X}");
                    Console.WriteLine("请检查:");
                    Console.WriteLine("1. 控制器IP地址和端口是否正确");
                    Console.WriteLine("2. 网络连接是否正常");
                    Console.WriteLine("3. 控制器是否已上电并处于可连接状态");

                    if (callbackResult != MP_SUCCESS)
                    {
                        Console.WriteLine("\n尝试重新设置回调函数...");
                        callbackResult = MP_SetUserCallback(
                            pHandle,
                            MP_CALLBACK_FUN_ROBOT_STATUS,
                            OnRobotStatusChanged,
                            pRobotStatus);

                        if (callbackResult == MP_SUCCESS)
                        {
                            Console.WriteLine("回调设置成功!");
                        }
                    }

                    Console.WriteLine("\n连接失败，但回调可能已设置。按任意键继续或按'Q'退出...");
                    ConsoleKeyInfo key = Console.ReadKey();
                    if (key.KeyChar == 'q' || key.KeyChar == 'Q')
                    {
                        FreeRobotStatus(pRobotStatus);
                        MP_DestroyHandle(pHandle);
                        return;
                    }
                }
                else
                {
                    Console.WriteLine($"连接控制器成功: {connectInfo}");
                }

                // 6. 请求控制权限
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n请求控制权限...");
                    long requestResult = MP_RequestControlAccess(pHandle);
                    if (requestResult != MP_SUCCESS)
                    {
                        Console.WriteLine($"请求控制权限失败! 错误码: 0x{requestResult:X}");
                    }
                    else
                    {
                        Console.WriteLine("请求控制权限成功!");
                    }
                }

                // 7. 切换用户
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n正在切换到示教员用户...");

                    // 使用你提供的用户名和密码
                    string userName = "programmer";
                    string userPwd = "WKD@2025";
                    E_USER_TYPE userType = E_USER_TYPE.MP_USER_PROGRAMMER;

                    long switchResult = SwitchUser(pHandle, userName, userPwd, userType);

                    if (switchResult != MP_SUCCESS)
                    {
                        Console.WriteLine("警告: 用户切换失败，部分功能可能受限");
                        Console.WriteLine("是否继续? (Y/N)");
                        var key = Console.ReadKey();
                        if (key.KeyChar == 'n' || key.KeyChar == 'N')
                        {
                            FreeRobotStatus(pRobotStatus);
                            MP_DestroyHandle(pHandle);
                            return;
                        }
                    }
                }
                //消除警告
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n消除警告...");
                    long requestResult = MP_EliminateAllAlarm(pHandle);
                    if (requestResult != MP_SUCCESS)
                    {
                        Console.WriteLine($"消除警告失败! 错误码: 0x{requestResult:X}");
                    }
                    else
                    {
                        Console.WriteLine("消除警告成功!");
                    }
                }
                // 8. 设置工作模式
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n正在设置工作模式为手动低速...");
                    long llRet = MP_SetWorkMode(pHandle, E_WORKING_MODE.MP_WORKING_MODE_MANUAL_LS);
                    if (MP_SUCCESS != llRet)
                    {
                        Console.WriteLine($"设置工作模式失败! 错误码: 0x{llRet:X}");
                        FreeRobotStatus(pRobotStatus);
                        MP_DestroyHandle(pHandle);
                        return; // 替换C++的break
                    }
                    Console.WriteLine("设置工作模式成功! 当前模式：手动低速");
                }

                //9.电机上电
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n正在启用电机...");
                    long llRet = MP_SetMotorOn(pHandle);
                    if (MP_SUCCESS != llRet)
                    {
                        Console.WriteLine($"上电失败 错误码： [0x{llRet:X}]");
                        FreeRobotStatus(pRobotStatus);
                        MP_DestroyHandle(pHandle);
                        return;
                    }
                    Console.WriteLine("上电成功!");
                }
                /*
                 在这里写工作代码......
                 */
                //电机下电
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n正在关闭电机...");
                    long llRet = MP_SetMotorOff(pHandle);
                    if (MP_SUCCESS != llRet)
                    {
                        Console.WriteLine($"下电失败 错误码： [0x{llRet:X}]");
                        FreeRobotStatus(pRobotStatus);
                        MP_DestroyHandle(pHandle);
                        return;
                    }
                    Console.WriteLine("下电成功!");
                }
                //断开连接
                if (connectResult == MP_SUCCESS)
                {
                    Console.WriteLine("\n正在断开控制器...");
                    long llRet = MP_DisConnectCTRL(pHandle);
                    if (MP_SUCCESS != llRet)
                    {
                        Console.WriteLine($"断开控制器失败 错误码： [0x{llRet:X}]");
                        FreeRobotStatus(pRobotStatus);
                        MP_DestroyHandle(pHandle);
                        return;
                    }
                    Console.WriteLine("断开控制器成功!");
                }
                //销毁句柄
                //if (connectResult == MP_SUCCESS)
                //{
                //    Console.WriteLine("\n正在销毁句柄...");
                //    long llRet = MP_DestroyHandle(pHandle);
                //    if(MP_SUCCESS != llRet)
                //    {
                //        Console.WriteLine($"销毁句柄失败 错误码： [0x{llRet:X}]");
                //        FreeRobotStatus(pRobotStatus);
                        
                //        return;
                //    }
                //    Console.WriteLine("销毁句柄成功!");
                //}
                Console.WriteLine("\n=======================================");
                Console.WriteLine("机器人状态监控系统已启动");
                Console.WriteLine("=======================================");
                Console.WriteLine("操作说明:");
                Console.WriteLine("- 回调函数已在创建句柄后立即设置");
                Console.WriteLine("- 系统将自动接收并显示机器人状态更新");
                Console.WriteLine("- 按 'Q' 键停止监控并退出");
                Console.WriteLine("- 按 'S' 键显示当前状态统计");
                Console.WriteLine("- 按 'C' 键清除控制台");
                Console.WriteLine("- 按 'R' 键重新设置回调函数");
                Console.WriteLine("- 按 'U' 键切换用户");
                Console.WriteLine("=======================================\n");

                // 10. 主循环
                int callbackCount = 0;
                DateTime startTime = DateTime.Now;
                DateTime lastDisplayTime = DateTime.Now;

                while (true)
                {
                    if (Console.KeyAvailable)
                    {
                        ConsoleKeyInfo key = Console.ReadKey(true);

                        switch (key.KeyChar)
                        {
                            case 'q':
                            case 'Q':
                                Console.WriteLine("\n接收到退出命令...");
                                return;

                            case 's':
                            case 'S':
                                TimeSpan runningTime = DateTime.Now - startTime;
                                Console.WriteLine($"\n[状态统计]");
                                Console.WriteLine($"运行时间: {runningTime:mm\\:ss}");
                                Console.WriteLine($"回调次数: {callbackCount}");
                                Console.WriteLine($"平均频率: {callbackCount / Math.Max(runningTime.TotalSeconds, 1):F2} Hz");
                                break;

                            case 'c':
                            case 'C':
                                Console.Clear();
                                Console.WriteLine("控制台已清除");
                                Console.WriteLine($"机器人状态监控已启动 - {DateTime.Now:HH:mm:ss}");
                                break;

                            case 'r':
                            case 'R':
                                Console.WriteLine("\n重新设置回调函数...");
                                long reCallbackResult = MP_SetUserCallback(
                                    pHandle,
                                    MP_CALLBACK_FUN_ROBOT_STATUS,
                                    OnRobotStatusChanged,
                                    pRobotStatus);

                                if (reCallbackResult != MP_SUCCESS)
                                {
                                    Console.WriteLine($"重新设置回调失败! 错误码: 0x{reCallbackResult:X}");
                                }
                                else
                                {
                                    Console.WriteLine("回调重新设置成功!");
                                }
                                break;

                            case 'u':
                            case 'U':
                                Console.WriteLine("\n切换用户菜单:");
                                Console.WriteLine("1. 管理员 (MP_USER_ADMIN)");
                                Console.WriteLine("2. 示教员 (MP_USER_PROGRAMMER)");
                                Console.WriteLine("3. 操作员 (MP_USER_OPERATOR)");
                                Console.Write("请选择用户类型 (1-3): ");

                                var userKey = Console.ReadKey();
                                Console.WriteLine();

                                string inputUserName, inputUserPwd;
                                E_USER_TYPE selectedType;

                                switch (userKey.KeyChar)
                                {
                                    case '1':
                                        selectedType = E_USER_TYPE.MP_USER_ADMIN;
                                        Console.Write("请输入管理员用户名: ");
                                        inputUserName = Console.ReadLine();
                                        Console.Write("请输入密码: ");
                                        inputUserPwd = Console.ReadLine();
                                        SwitchUser(pHandle, inputUserName, inputUserPwd, selectedType);
                                        break;

                                    case '2':
                                        selectedType = E_USER_TYPE.MP_USER_PROGRAMMER;
                                        Console.Write("请输入示教员用户名: ");
                                        inputUserName = Console.ReadLine();
                                        Console.Write("请输入密码: ");
                                        inputUserPwd = Console.ReadLine();
                                        SwitchUser(pHandle, inputUserName, inputUserPwd, selectedType);
                                        break;

                                    case '3':
                                        selectedType = E_USER_TYPE.MP_USER_OPERATOR;
                                        Console.Write("请输入操作员用户名: ");
                                        inputUserName = Console.ReadLine();
                                        Console.Write("请输入密码: ");
                                        inputUserPwd = Console.ReadLine();
                                        SwitchUser(pHandle, inputUserName, inputUserPwd, selectedType);
                                        break;

                                    default:
                                        Console.WriteLine("无效的选择!");
                                        break;
                                }
                                break;
                        }
                    }

                    // 显示连接状态
                    if ((DateTime.Now - lastDisplayTime).TotalSeconds >= 10)
                    {
                        string statusMessage = $"[系统状态] {DateTime.Now:HH:mm:ss} - ";
                        if (connectResult == MP_SUCCESS)
                        {
                            statusMessage += "已连接控制器，等待状态更新...";
                        }
                        else
                        {
                            statusMessage += "未连接控制器，回调可能已设置...";
                        }
                        Console.WriteLine(statusMessage);
                        lastDisplayTime = DateTime.Now;
                    }

                    Thread.Sleep(100);
                }
            }
            catch (DllNotFoundException ex)
            {
                Console.WriteLine($"\n错误: 找不到 MPSDK.dll");
                Console.WriteLine($"详细信息: {ex.Message}");
                Console.WriteLine("\n解决方案:");
                Console.WriteLine("1. 确保 MPSDK.dll 在以下目录之一:");
                Console.WriteLine($"   - {AppDomain.CurrentDomain.BaseDirectory}");
                Console.WriteLine($"   - {Environment.CurrentDirectory}");
                Console.WriteLine("2. 将 MPSDK.dll 添加到系统 PATH 环境变量");
            }
            catch (EntryPointNotFoundException ex)
            {
                Console.WriteLine($"\n错误: 找不到指定的函数");
                Console.WriteLine($"详细信息: {ex.Message}");
                Console.WriteLine("\n可能原因:");
                Console.WriteLine("1. 函数名拼写错误");
                Console.WriteLine("2. 调用约定不匹配");
                Console.WriteLine("3. DLL 导出函数名有变化");
            }
            catch (BadImageFormatException ex)
            {
                Console.WriteLine($"\n错误: DLL 格式不匹配");
                Console.WriteLine($"详细信息: {ex.Message}");
                Console.WriteLine("\n平台架构检查:");
                Console.WriteLine($"- 应用程序: {(Environment.Is64BitProcess ? "64位" : "32位")}");
                Console.WriteLine("\n解决方案:");
                Console.WriteLine("1. 如果 MPSDK.dll 是 32 位: 将项目设置为 x86");
                Console.WriteLine("2. 如果 MPSDK.dll 是 64 位: 将项目设置为 x64");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n发生未预期的错误:");
                Console.WriteLine($"类型: {ex.GetType().Name}");
                Console.WriteLine($"消息: {ex.Message}");
                Console.WriteLine($"堆栈: {ex.StackTrace}");
            }
            finally
            {
                // 清理资源
                Console.WriteLine("\n=======================================");
                Console.WriteLine("正在清理资源...");

                if (pRobotStatus != IntPtr.Zero)
                {
                    Console.Write("释放机器人状态内存... ");
                    FreeRobotStatus(pRobotStatus);
                    Console.WriteLine("完成");
                }

                if (pHandle != IntPtr.Zero)
                {
                    Console.Write("销毁 MPSDK 句柄... ");
                    try
                    {
                        MP_DestroyHandle(pHandle);
                        Console.WriteLine("完成");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"失败: {ex.Message}");
                    }
                }

                Console.WriteLine("资源清理完成");
                Console.WriteLine("=======================================");
            }

            Console.WriteLine("\n按任意键退出程序...");
            Console.ReadKey();
        }
    }
}