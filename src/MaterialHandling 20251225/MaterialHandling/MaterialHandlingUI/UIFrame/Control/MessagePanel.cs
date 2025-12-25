using MaterialHandling.MaterialHandlingUI.UIFrame.UserControls;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{

    public partial class MessagePanel : UserControl
    {
        private readonly HttpClient _httpClient;
        
        private string messageIp1;
        private string messageIp2;
        private string port1;
        private string port2;

        public double targetx, targety, targetz;


        public MessagePanel(string ip1, string port1, string ip2, string port2)
        {
            this.messageIp1 = ip1;
            this.port1 = port1;
            this.messageIp2 = ip2;
            this.port2 = port2;

            // 初始化 HttpClient
            _httpClient = new HttpClient();

            InitializeComponent();

            // 初始化定时器
            //requestTimer = new Timer();
            //requestTimer.Interval = 1000; // 1秒间隔
            //requestTimer.Tick += async (sender, e) => await btn_requestMessage_Click(sender, e);
            //btn_timerControl.Click += Btn_timerControl_Click; // 定时器按钮事件

            btn_requestC1Message.Click += async (sender, e) => await btn_C1_RequestMessage_Click(sender, e);
            //btn_requestC1Message.Click += async (sender, e) => await requestMultiMaterialPoint(sender, e);

            btn_requestC2Message.Click += async (sender, e) => await btn_C2_RequestMessage_Click(sender, e);
        }

        private async Task btn_C1_RequestMessage_Click(object sender, EventArgs e)
        {
            try
            { 
                //string mapId = this.comboBox_mapId.SelectedItem.ToString();
                //string operation = this.comboBox_operation.SelectedItem.ToString();

                if (string.IsNullOrEmpty(messageIp1) || string.IsNullOrEmpty(port1))
                {
                    MessageBox.Show("IP地址或端口不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string url = $"http://{messageIp1}:{port1}/coordinate";

                // 发送HTTP请求
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // 解析JSON响应
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                // 如果成功，解析data字段中的3D信息
                if (jsonResponse.status == "1" && jsonResponse.message == "success")
                {
                    string dataStr = jsonResponse.data.ToString();
                    // 将字符串形式的字典转换为对象
                    dynamic data = JsonConvert.DeserializeObject(dataStr.Replace("'", "\""));
                    
                    double pos_x = data.X * 100;
                    double pos_y = data.Z * 100;
                    double pos_z = data.Y * 100;
                    this.tb_C1X.Text = string.Format("{0:F2}",pos_x);
                    this.tb_C1Y.Text = string.Format("{0:F2}",pos_y);
                    this.tb_C1Z.Text = string.Format("{0:F2}",pos_z);
                                                            
                    // 将数据转换为格式化的JSON字符串显示
                    this.textBox_C1RawMessage.Text = JsonConvert.SerializeObject(data, Formatting.Indented);

                    
                    double offsetx = Convert.ToDouble(this.tb_offsetC1X.Text);
                    double offsety = Convert.ToDouble(this.tb_offsetC1Y.Text);
                    double offsetz = Convert.ToDouble(this.tb_offsetC1Z.Text);

                    targetx = MainFrame.mf.rOSClient.lidarX - offsetx + pos_x;


                    targety = MainFrame.mf.rOSClient.lidarY - offsety + pos_y;
                    targetz = pos_z - offsetz;

                    this.tb_targetC1X.Text = string.Format("{0:F2}", targetx);
                    this.tb_targetC1Y.Text = string.Format("{0:F2}", targety);
                    this.tb_targetC1Z.Text = string.Format("{0:F2}", targetz);

                    try
                    {
                        int c1_target_x = (int)Convert.ToDouble(this.tb_targetC1X.Text);
                        int c1_target_y = (int)Convert.ToDouble(this.tb_targetC1Y.Text);
                        int c1_target_z = (int)Convert.ToDouble(this.tb_targetC1Z.Text);
                                          
                        MainFrame.cf.mapVehiclePanel.C1_targetPoint = new System.Drawing.Point(c1_target_x, c1_target_y);
                        // 绘制目标点
                        MainFrame.cf.mapVehiclePanel.isRedrawC1_targetPoint = true;
                        MainFrame.cf.mapVehiclePanel.Invalidate_pictureBoxMap();

                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("MessagePanel: " + ex.Message);
                    }

                }
                else
                {
                    this.textBox_C1RawMessage.Text = $"请求失败: {jsonResponse.message}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                // 处理HTTP请求异常
                MessageBox.Show($"HTTP请求错误: {httpEx.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox_C1RawMessage.Text = $"HTTP请求错误: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // 处理一般异常
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox_C1RawMessage.Text = $"错误: {ex.Message}";
            }
        }

        private async Task btn_C2_RequestMessage_Click(object sender, EventArgs e)
        {
            try
            {
                //string mapId = this.comboBox_mapId.SelectedItem.ToString();
                //string operation = this.comboBox_operation.SelectedItem.ToString();

                if (string.IsNullOrEmpty(messageIp1) || string.IsNullOrEmpty(port1))
                {
                    MessageBox.Show("IP地址或端口不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string url = $"http://{messageIp2}:{port2}/coordinates";

                // 发送HTTP请求
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                // 解析JSON响应
                dynamic jsonResponse = JsonConvert.DeserializeObject(responseBody);

                // 如果成功，解析data字段中的3D信息
                if (jsonResponse.status == "0" && jsonResponse.message == "success")
                {
                    string dataStr = jsonResponse.data.ToString();
                    // 将字符串形式的字典转换为对象
                    dynamic data = JsonConvert.DeserializeObject(dataStr.Replace("'", "\""));
                    double pos_x = data.X * 100;
                    double pos_y = data.Z * 100;
                    double pos_z = data.Y * 100;
                    this.tb_C2X.Text = string.Format("{0:F2}", pos_x);
                    this.tb_C2Y.Text = string.Format("{0:F2}", pos_y);
                    this.tb_C2Z.Text = string.Format("{0:F2}", pos_z);

                    // 将数据转换为格式化的JSON字符串显示
                    this.textBox_C1RawMessage.Text = JsonConvert.SerializeObject(data, Formatting.Indented);


                    double offsetx = Convert.ToDouble(this.tb_offsetC2X.Text);
                    double offsety = Convert.ToDouble(this.tb_offsetC2Y.Text);
                    double offsetz = Convert.ToDouble(this.tb_offsetC2Z.Text);

                    // 计算目标 坐标点
                    targetx = pos_x - offsetx;
                    targety = pos_y - offsety;
                    targetz = pos_z - offsetz;

                    this.tb_targetC2X.Text = string.Format("{0:F2}", targetx);
                    this.tb_targetC2Y.Text = string.Format("{0:F2}", targety);
                    this.tb_targetC2Z.Text = string.Format("{0:F2}", targetz);

                    try
                    {
                        int c2_target_x = Convert.ToInt32(this.tb_targetC2X.Text);
                        int c2_target_y = Convert.ToInt32(this.tb_targetC2Y.Text);
                        int c2_target_z = Convert.ToInt32(this.tb_targetC2Z.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Message Panel ：" + ex.Message);
                    }


                }
                else
                {
                    this.textBox_C2RawMessage.Text = $"请求失败: {jsonResponse.message}";
                }
            }
            catch (HttpRequestException httpEx)
            {
                // 处理HTTP请求异常
                MessageBox.Show($"HTTP请求错误: {httpEx.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox_C2RawMessage.Text = $"HTTP请求错误: {httpEx.Message}";
            }
            catch (Exception ex)
            {
                // 处理一般异常
                MessageBox.Show($"发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.textBox_C2RawMessage.Text = $"错误: {ex.Message}";
            }
        }

        private void btn_requestMultiMaterialPoint_Click(object sender, EventArgs e)
        {
            requestMultiMaterialPoint(sender, e);
            MainFrame.cf.camToMaterial.Invalidate();
        }

        private void MessagePanel_Load(object sender, EventArgs e)
        {

        }

        // 更新后的方法
        // 你的方法（已更新为两步解析法）
        // 你的方法（已增加 Y/Z 调换逻辑）
        // 你的方法（已增加单位换算逻辑）
        private async Task requestMultiMaterialPoint(object sender, EventArgs e)
        {
            try
            {
                string url = $"http://{messageIp1}:{port1}/coordinates";
                HttpResponseMessage response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                string responseBody = await response.Content.ReadAsStringAsync();

                Console.WriteLine("##### " + responseBody + " ####");
                List<MaterialPoint> materialPoints = null;

                // 第 1 步和第 2 步：解析 JSON
                var rawResponse = JsonConvert.DeserializeObject<RawApiResponse>(responseBody);
                if (rawResponse != null && rawResponse.Status == "1" && !string.IsNullOrEmpty(rawResponse.Data))
                {
                    materialPoints = JsonConvert.DeserializeObject<List<MaterialPoint>>(rawResponse.Data);

                    // 第 3 步：数据转换 (Y/Z 对调 和 m -> cm 单位换算)
                    foreach (var point in materialPoints)
                    {
                        // 先读取从JSON解析出来的原始值 (单位: m)
                        double originalX = point.X;
                        double originalY = point.Y;
                        double originalZ = point.Z;

                        // 一步到位完成所有转换和赋值
                        // 1. X 坐标转换单位
                        point.X = originalX * 100;
                        // 2. 将原始 Z 值转换单位后，赋给 Y (实现对调和单位换算)
                        point.Y = -originalZ * 100;
                        // 3. 将原始 Y 值转换单位后，赋给 Z (实现对调和单位换算)
                        point.Z = originalY * 100;
                    }
                }

                if (materialPoints != null)
                {
                    MainFrame.cf.camToMaterial.setListPoint(materialPoints);
                    MainFrame.cf.camToMaterial.Invalidate();
                    Console.WriteLine($"成功解析并转换 {materialPoints.Count} 个点。");
                    foreach (var p in materialPoints)
                    {
                        Console.WriteLine(p.ToString());
                    }
                }
                else
                {
                    Console.WriteLine($"请求失败或返回的 data 字段为空。Message: {rawResponse?.Message}");
                }
            }
            catch (Exception ex) // 使用更通用的 Exception 来捕获所有可能的错误
            {
                Console.WriteLine($"处理请求时发生错误: {ex.Message}");
            }
        }
    }
}
