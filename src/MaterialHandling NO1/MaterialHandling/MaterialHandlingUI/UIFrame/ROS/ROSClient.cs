using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using ZstdNet;
using System.Net.NetworkInformation;
using MaterialHandling.MaterialHandlingUI.UIFrame.Control;
using System.Drawing;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.ROS
{
    public partial class ROSClient : UserControl
    {
        private MapVehiclePanel mapVehiclePanel;
        //实时位姿连接
        private Socket poseSocket;
        private Thread poseThread = null;

        //地图连接
        private Socket mapSocket;
        private Thread mapThread = null;

        //连接状态
        public volatile bool poseIsRunning = false;
        private volatile bool mapIsRunning = false;

        private volatile bool lidarIsRunning = false;

        private System.Timers.Timer heartbeatTimer; //应用层心跳检测
        private Int64 heartbeatTimes = 0; //未响应的心跳次数

        private string server_IP;
        private string server_PosePort;
        private string server_MapPort;

        // lidar 坐标 角度
        public string poseY     = "";
        public string poseTheta = "";
        public string poseX     = "";
        public double lidarX;
        public double lidarY;
        
        // 地图裁剪参数
        public int xN { get; private set; }
        public int yN { get; private set; }
        public int yP { get; private set; }
        public int xP { get; private set; }

        
        SysControlPanel sysControlPanel;
        public ROSClient() //构造函数
        {
            InitializeComponent();
            InitializeROS_Par();
            heartbeatTimer = new System.Timers.Timer(5000);
            heartbeatTimer.Elapsed += (sender, e) => ROSSendHeartbeat();
            heartbeatTimer.AutoReset = true;

            mapVehiclePanel = MainFrame.cf.mapVehiclePanel;
            // 更新裁剪参数
            assignCropParam();

            //btn_buildmap.Enabled = false;
            //btn_SaveMap.Enabled = false;
        }
        double targetX;
        double targetY;
        double targetTheta;
        double currentX;
        double currentY;
        double currentTheta;
        double dx;
        double dy;
        double dtheta;
        ~ROSClient()
        {
        }
        private void InitializeROS_Par()
        {
            this.server_IP = tb_Server_IP.Text;
            this.server_PosePort = tb_Server_PosePort.Text;
            this.server_MapPort = tb_Server_MapPort.Text;
            /*this.destinationX = "1";
            this.destinationY = "0";
            this.destinationTheta = "0";
            

            // 计算位姿差值
            dx = targetX - currentX;
            dy = targetY - currentY;
            dtheta = targetTheta - currentTheta;*/
            
        }
        public void ROSConnect() //开启TCP连接按键
        {
            try
            {
                ROSPing();
                ROSInitPoseConnection();
                ROSInitMapConnection();

                // 启用控件
                btn_StartLidar.Enabled = true;
                btn_ROS_Disconnect.Enabled = true;
                // heartbeatTimer.Start();
            }
            catch (Exception ex)
            {
                ROSShowLog($"ROS连接失败: {ex.Message}");
            }
        }

        private void ROSInitPoseConnection() //初始化位姿连接
        {
            try
            {
                if (poseThread != null && poseThread.IsAlive)
                {
                    poseThread.Join(2000); // 等待旧线程退出
                }
                //建立关于位姿的TCP连接
                poseSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(server_IP), Convert.ToInt32(server_PosePort));
                poseSocket.Connect(remoteEP);
                poseSocket.NoDelay = true; // 禁用Nagle算法，提高实时性
                poseIsRunning = true;
                poseThread = new Thread(ROSReceivePose);
                poseThread.IsBackground = true;
                poseThread.Start();
                ROSShowLog($"ROS位姿连接建立成功: {remoteEP}");
            }
            catch (Exception ex)
            {
                ROSShowLog($"ROS位姿连接建立失败: {ex}");
            }

        }

        private void ROSInitMapConnection() //初始化地图连接
        {
            try
            {
                if (mapThread != null && mapThread.IsAlive)
                {
                    mapThread.Join(2000); // 等待旧线程退出
                }
                //建立关于地图的TCP连接
                mapSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint remoteEP = new IPEndPoint(IPAddress.Parse(server_IP), Convert.ToInt32(server_MapPort));
                mapSocket.Connect(remoteEP);
                mapIsRunning = true;
                mapThread = new Thread(ROSReceiveMap);
                mapThread.IsBackground = true;
                mapThread.Start();
                heartbeatTimes = 0;
                ROSShowLog($"ROS:地图连接建立成功: {remoteEP}");
            }
            catch (Exception ex)
            {
                ROSShowLog($"ROS:地图连接建立失败: {ex}");
            }
        }



        private void ROSPing()  //检查ROS端网络连接
        {
            Ping ROSping = new Ping();
            PingReply rosReply = ROSping.Send(server_IP);
            while (rosReply.Status != IPStatus.Success)
            {
                ROSShowLog("ROS：网络连接出错，尝试重连");
                Thread.Sleep(1000);
                rosReply = ROSping.Send(server_IP);
            }
        }

        private void ROSReceivePose() //接收位姿连接响应
        {
                byte[] buffer = new byte[1024];
                while (poseIsRunning)
                {
                    try
                    {
                        int bytesRead = poseSocket.Receive(buffer);
                        if (bytesRead == 0) break;

                        string json = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        BeginInvoke(new Action<string>(ROSHandlePoseResponse), json);
                    }
                    catch (SocketException ex) when (ex.SocketErrorCode == SocketError.Interrupted)
                    {
                        break;
                    }
                    catch (Exception ex)
                    {
                        ROSShowLog($"ROS:位姿接收异常: {ex.Message}");
                        poseIsRunning = false;
                    }
                }
                ROSShowLog("ROS:位姿接收线程已退出");
            
        }

        private void ROSReceiveMap()
        {
                var stream = new NetworkStream(mapSocket); // 或者直接用 mapSocket.Receive
                var reader = new BinaryReader(stream, Encoding.UTF8);

                try
                {
                    while (mapIsRunning)
                    {
                        //1. 先读取 4 字节长度前缀
                        byte[] lenBytes = reader.ReadBytes(4);
                        if (lenBytes.Length < 4) break;  // 说明连接断了

                        int dataLen = IPAddress.NetworkToHostOrder(BitConverter.ToInt32(lenBytes, 0));// 网络字节序转主机字节序

                        //2. 再读取指定长度的 JSON 数据
                        byte[] jsonBytes = reader.ReadBytes(dataLen);
                        if (jsonBytes.Length < dataLen) break;

                        string json = Encoding.UTF8.GetString(jsonBytes);

                        // 3. 交给处理函数 
                        BeginInvoke(new Action<string>(ROSHandleMapResponse), json);
                    }
                }
                catch (Exception ex)
                {
                    ROSShowLog($"ROS地图数据接收异常: {ex.Message}");
                }
                finally
                {
                    reader.Close();
                    stream.Close();
                    mapIsRunning = false;
                }
            
        } //接收地图连接响应
        double Clamp(double value, double min, double max) =>
            value < min ? min : (value > max ? max : value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="json"></param>
        private void ROSHandlePoseResponse(string json) //位姿连接响应处理方法
        {
            try
            {
                dynamic response = JsonConvert.DeserializeObject(json);
                string logMsg = $"ROS:位姿响应: {json}";
                sysControlPanel = MainFrame.cf.sysControlPanel;
                // 根据响应类型处理
                if (response.Type == "pose")
                {
                    logMsg = $"位姿:[{response.Pose.x},{response.Pose.y}，{response.Pose.theta}],Timestamp:{response.Timestamp}";
                    poseX = Convert.ToString(response.Pose.x);
                    poseY = Convert.ToString(response.Pose.y);
                    poseTheta = Convert.ToString(response.Pose.theta);


                    //============================================================================================//
                    double x = (Convert.ToDouble(poseX) * 100);
                    double y = (Convert.ToDouble(poseY) * 100);
                    lidarX = x;
                    lidarY = y;
                    double theta = Convert.ToDouble(poseTheta); // 弧度theta

                    // 计算新的坐标  b = 48
                    int xPrime = (int)(x - 48 * Math.Cos(theta));
                    int yPrime = (int)(y - 48 * Math.Sin(theta));

                    /*
                    v1.UpdateCarPosture(
                            new Point(xPrime, yPrime),
                            RadiansToDegrees((float)theta) // 转换角度theta
                        );
                    */

                    mapVehiclePanel.UpdateCarPosture(
                            new PointF((float)x, (float)y),
                            //RadiansToDegrees((float)theta) // 转换角度theta
                            (float)theta    // 转换角度theta
                        );
                    //============================================================================================//
                }

                //ROSShowLog(logMsg);
            }
            catch
            {
                ROSShowLog($"ROS:原始响应: {json}");
            }
        }
        
        // 将弧度转换为角度的函数
        static float RadiansToDegrees(float radians)
        {
            return (float)(radians * (180.0 / Math.PI));
        }

        //start lidar  //save map  //generate map  //heartbeat  //rebuild

        private void ROSHandleMapResponse(string json)
        {
            try
            {
                dynamic response = JsonConvert.DeserializeObject(json);
                string logMsg = $"ROS：地图信号响应: {json}";
                if (response.Type == "build")
                {
                    ROSMapBuild(response);
                    logMsg = "ROS地图数据已成功接收";
                }
                else if(response.Type == "prebuild")
                {
                    ROSMapBuild(response);
                    logMsg = "ROS预地图数据已成功接收";

                    // 预建图成功， 启用 建图控件
                    btn_buildmap.Enabled = true;
                }
                else if (response.Type == "heartbeat")
                {
                    logMsg = "ROS心跳应答: 连接正常";
                    heartbeatTimes--;
                }
                else if (response.Type == "save map") //弃用
                { 
                    logMsg = response.Msg.ToString();
                    //int times = Convert.ToInt32(response.Timestamp); //获取时间戳
                    //int status = Convert.ToBoolean(response.Status); //获取响应状态
                }
                else if (response.Type == "error")
                {
                    logMsg = ROSErrorDeal(response);
                }
                else if (response.Type == "start lidar")
                {
                    if (response.Status == true)
                    {
                        lidarIsRunning = true;
                        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
                        timer.Interval = 10000; //雷达开启需要两分钟后再建图
                        timer.Tick += (s, args) =>
                        {
                            //btn_buildmap.Enabled = lidarIsRunning;
                            timer.Stop();
                            timer.Dispose();
                        };
                        timer.Start();

                        // 雷达开启成功，启用预建图控件
                        btn_SaveMap.Enabled = true; 
                    }
                    logMsg = response.Msg.ToString();
                }
                ROSShowLog(logMsg);
            }
            catch
            {
                ROSShowLog($"ROS原始响应: {json}");
            }

        } //地图连接响应处理方法

        private string ROSErrorDeal(dynamic response)//处理ROS端出现的错误
        {
            string errorLog = response.Msg.toString();
            //可能需要的错误处理
            return errorLog;
        }

        private void ROSMapBuild(dynamic response)
        {
            // 检查Base64字符串有效性
            string base64Data = response.Grid_data_bin.ToString();
            if (string.IsNullOrEmpty(base64Data) || base64Data.Length % 4 != 0)
            {
                ROSShowLog("ROS:无效的Base64数据");
                return;
            }

            try
            {
                byte[] compressed = Convert.FromBase64String(base64Data);
                byte[] binary = new Decompressor().Unwrap(compressed);

                //转为二维数组
                int height = 500, width = 1500;
                var grid = new int[height, width];
                for (int i = 0; i < height * width; i++)
                {
                    int byteIndex = i / 8;
                    int bitIndex = i % 8;  // 从高位到低位：7 - (i % 8)
                    int bit = (binary[byteIndex] >> (7 - bitIndex)) & 1;
                    grid[i / width, i % width] = bit;
                }
                //保存至本地文件
                //SaveGridToFile(grid, ".//map//map_data.txt");
                // 获取应用程序的执行路径
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                // 外部配置文件路径
                string configFilePath = Path.Combine(appPath, "map//map_data.txt");
                SaveGridToFile(grid, configFilePath);
                //SaveGridToFile(grid, "D://Pro//MaterialHandling//MaterialHandling pro//MaterialHandling 20250602//MaterialHandling//bin//Debug//map//map_data.txt");
                //btn_SaveMap.Enabled = false;
            }
            catch (FormatException)
            {
                ROSShowLog("ROS:Base64数据格式错误");
            }
            catch( Exception e)
            {
                Console.WriteLine(e.Message);
            }
        } //地图构建方法

        private void SaveGridToFile(int[,] grid, string filePath) //保存二值化数据至txt文件（可选）
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    int height = grid.GetLength(0); 
                    int width = grid.GetLength(1);

                    for (int y = 0; y < height; y++)
                    {
                        StringBuilder line = new StringBuilder();
                        for (int x = 0; x < width; x++)
                        {
                            line.Append(grid[y, x].ToString());
                        }
                        writer.WriteLine(line.ToString());
                    }
                }
                ROSShowLog($"地图数据已保存至: {Path.GetFullPath(filePath)}");
            }
            catch (Exception ex)
            {
                ROSShowLog($"保存文件失败: {ex.Message}");
            }
        }

        private void ROSSendHeartbeat() //应用层心跳检测
        {
            if (mapSocket?.Connected != true) return;
            var cmd = new { Cmd = "heartbeat", Seq = 10086 };
            SendJsonCommand(cmd);
            heartbeatTimes++;
            if (heartbeatTimes > 3)
                ROSShowLog("ROS报警:ROS端心跳响应丢失");
        }

        private void SendJsonCommand(object command) //JSON指令发送
        {
            try
            {
                string json = JsonConvert.SerializeObject(command);
                byte[] data = Encoding.UTF8.GetBytes(json);
                mapSocket.Send(data);
                ROSShowLog($"发送指令: {json}");
            }
            catch (Exception ex)
            {
                ROSShowLog($"发送失败: {ex.Message}");
            }
        }

        private void ROSShowLog(string message) //日志打印
        {

            if (rtb_ROS_Log.InvokeRequired)
            {
                rtb_ROS_Log.Invoke(new Action<string>(ROSShowLog), message);
            }
            else
            {
                rtb_ROS_Log.AppendText($"[{DateTime.Now:HH:mm:ss}] {message}\n");
                rtb_ROS_Log.ScrollToCaret();
            }
        }

        public void ROSDisconnect() //断开连接按键
        {

            // 停止位姿连接
            poseIsRunning = false;
            if (poseSocket?.Connected == true)
            {
                poseSocket.Shutdown(SocketShutdown.Both);
                poseSocket.Close();
            }
            poseThread?.Join(2000); // 等待线程退出，最多1秒

            // 停止地图连接
            mapIsRunning = false;
            if (mapSocket?.Connected == true)
            {
                mapSocket.Shutdown(SocketShutdown.Both);
                mapSocket.Close();
            }
            mapThread?.Join(2000); // 等待线程退出，最多1秒

            //heartbeatTimer.Stop();
            ROSShowLog("ROS所有连接已断开");

            // 断开连接后，停用控件
            btn_ROS_Disconnect.Enabled = false;
            btn_buildmap.Enabled = false;
            btn_SaveMap.Enabled = false;
            btn_StartLidar.Enabled = false;

        }


        // 
        private void assignCropParam()
        {
            yP = Convert.ToInt32(tb_yP.Text);
            xP = Convert.ToInt32(tb_xP.Text);

            mapVehiclePanel.SetUserOrigin(new PointF(1500 - xP, yP));
            mapVehiclePanel.originPoint = new PointF(1500 - xP, yP);

        }

        public void OnClosing() //安全退出释放线程资源
        {
            ROSDisconnect(); // 触发断开逻辑
            //poseThread?.Join(1000);
            //mapThread?.Join(1000);
        }

        public void btn_Prebuild_Click() //通知开发板保存地图
        {
            if (mapSocket?.Connected != true) return;
            if (!lidarIsRunning)
            {
                ROSShowLog("ROS:雷达未开启");
                return;
            }
            string time = tb_prebuildTime.Text;
            //if (Convert.ToInt32(time) < 60)
            //{
            //    time = "60";
            //    tb_buildTime.Text = "60";
            //}

            var cmd = new
            {
                Cmd = "prebuild",
                Param = new
                {
                    xP = tb_xP.Text, //X轴+向裁剪距离
                    xN = tb_xN.Text, //X轴-向裁剪距离
                    yP = tb_yP.Text, //U轴+向裁剪距离
                    yN = tb_yN.Text,  //Y轴-向裁剪距离
                    time = time
                }
            };
            assignCropParam();
            SendJsonCommand(cmd);
        }

        public void btn_StartLidar_Click()
        {
            if (mapSocket?.Connected != true) return;
            var cmd = new
            {
                Cmd = "start lidar"
            };
            SendJsonCommand(cmd);
        }

        public void btn_Rebuild_Click()
        {
            if (mapSocket?.Connected != true) return;
            var cmd = new
            {
                Cmd = "close cartorgrahper"
            };
            SendJsonCommand(cmd);

        }

        public void ROSBuildMap() //发送命令：建图
        {
            if (mapSocket?.Connected != true) return;
            if (!lidarIsRunning)
            {
                ROSShowLog("ROS:雷达未开启");
                return;
            }
            string time = tb_buildTime.Text;
            //if (Convert.ToInt32(time) < 60)
            //{
            //    time = "60";
            //    tb_buildTime.Text = "60";
            //}
            var cmd = new
            {
                Cmd = "build",
                //Cmd = "generate_map",
                Param = new
                {
                    xP = tb_xP.Text, //X轴+向裁剪距离
                    xN = tb_xN.Text, //X轴-向裁剪距离
                    yP = tb_yP.Text, //U轴+向裁剪距离
                    yN = tb_yN.Text,  //Y轴-向裁剪距离
                    time = time
                }
            };
            assignCropParam();
            SendJsonCommand(cmd);
        }

        private void btn_ROS_Connect_Click(object sender, EventArgs e)
        {
            ROSConnect();
        }

        private void btn_ROS_Disconnect_Click(object sender, EventArgs e)
        {
            ROSDisconnect();
        }

        private void btn_StartLidar_Click(object sender, EventArgs e)
        {
            btn_StartLidar_Click();
        }

        private void btn_buildmap_Click(object sender, EventArgs e)
        {
            ROSBuildMap();

            // TODO: 让小车旋转180°
            ROSShowLog("小车旋转180°...");

        }

        public string GetTimesStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }

        private void bt_Savemap_Click(object sender, EventArgs e) //弃用
        {
            if (mapSocket?.Connected != true) return;
            if (!lidarIsRunning)
            {
                ROSShowLog("ROS:雷达未开启");
                return;
            }
            string time = tb_prebuildTime.Text;
            //if (Convert.ToInt32(time) < 60)
            //{
            //    time = "60";
            //    tb_buildTime.Text = "60";
            //}
            var cmd = new
            {
                Cmd = "save map",
                Param = new
                {
                    xP = tb_xP.Text, //X轴+向裁剪距离
                    xN = tb_xN.Text, //X轴-向裁剪距离
                    yP = tb_yP.Text, //U轴+向裁剪距离
                    yN = tb_yN.Text,  //Y轴-向裁剪距离
                    time = time
                }
            };
            assignCropParam();
            SendJsonCommand(cmd);
        }

        private void bt_Clear_rtb_ROS_Log_Click(object sender, EventArgs e)
        {
            rtb_ROS_Log.Clear();
        }

        private void btn_SaveMap_Click(object sender, EventArgs e)
        {
            btn_Prebuild_Click();

            // TODO: 让小车旋转360°
            ROSShowLog("小车旋转360°...");
        }

        private void btn_Rebuild_Click(object sender, EventArgs e)
        {
            btn_Rebuild_Click();
        }
    }
}
