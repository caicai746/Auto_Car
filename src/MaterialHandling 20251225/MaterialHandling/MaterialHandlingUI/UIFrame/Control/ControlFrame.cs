using System;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingUI.UIFrame.UserControls;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    

    public partial class ControlFrame : Form
    {
        // 1. 窗口初始化
        public ControlFrame()
        {
            InitializeComponent();
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;
        }
        
        
        public SysControlPanel sysControlPanel;       // 系统控制 子窗口
        public MapVehiclePanel mapVehiclePanel = new MapVehiclePanel();
        public ParameterVCUPanel parameterVCUPanel;
        public MessagePanel messagePanel;
        public CamToMaterial camToMaterial;
        // 2. 控制主界面加载 初始化
        private void ControlFrame_Load(object sender, EventArgs e)
        {
            // 填充
            this.Dock = DockStyle.Fill;

            // 加载车辆控制框架
            Panel_MAP_Load();
            // 加载电机控制框架
            MotorPanel_Load();
            // 加载系统参数面板（包含移动控制按钮）
            SysParameterFrame_Load();
            ParameterVCUPanel_Load();
            // 加载摄像头
            Camera1Frame_Load();
            Camera2Frame_Load();
            MessageFrame_Load();
            // 摄像头传入物料参数 可视化界面
            CamToMaterial_Load();
        }

        private void CamToMaterial_Load()
        {
            camToMaterial = new CamToMaterial();
            panel_CamToMaterial.Controls.Add(camToMaterial);
            camToMaterial.Show();
        }

        private void ParameterVCUPanel_Load()
        {
            parameterVCUPanel = new ParameterVCUPanel();// 系统参数面板
            panel_Par.Controls.Add(parameterVCUPanel);
            parameterVCUPanel.Show();
        }

        // 3.电机控制 子界面加载 初始化
        private void MotorPanel_Load()
        {
            MotorPanel motorPanel = new MotorPanel();
            Panel_Motor.Controls.Add(motorPanel);
            motorPanel.Show();
        }
        // 4.系统控制 子界面加载 初始化
        private void SysParameterFrame_Load()
        {
            // 创建系统参数面板
            sysControlPanel = new SysControlPanel();
            panel_Sys.Controls.Add(sysControlPanel);
            sysControlPanel.Show();

            // 向系统参数面板传递车辆控制框架引用
            sysControlPanel.SetVehicleControlFrame(mapVehiclePanel);
        }
        // 5.摄像头1 子界面加载 初始化
        private void Camera1Frame_Load()
        {
            CameraFrame CameraFrame1 = new CameraFrame("192.168.158.111", 8100);
            //CameraFrame CameraFrame1 = new CameraFrame("10.124.4.138", 8000);
            panel_CAM1.Controls.Add(CameraFrame1);
            CameraFrame1.Show();
        }
        // 6.摄像头2 子界面加载 初始化
        private void Camera2Frame_Load()
        {
            CameraFrame CameraFrame2 = new CameraFrame("192.168.158.111", 8100);
            //CameraFrame CameraFrame2 = new CameraFrame("10.124.4.138", 8100);
            panel_CAM2.Controls.Add(CameraFrame2);
            CameraFrame2.Show();
        }
        // 7.地图 子界面加载 初始化
        private void Panel_MAP_Load()
        {
            Panel_MAP.Controls.Add(mapVehiclePanel);
            mapVehiclePanel.Show();
        }

        // 视觉信息Message 子界面
        private void MessageFrame_Load()
        {
            messagePanel = new MessagePanel("192.168.158.111", "9100", "192.168.158.111", "9000");
            panel_Message.Controls.Add(messagePanel);
            messagePanel.Show();
        }

        private void Panel_Motor_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
