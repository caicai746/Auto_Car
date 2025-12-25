using System;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingUI.UIFrame.Control;
using MaterialHandling.MaterialHandlingUI.UIFrame.Management;
using MaterialHandling.MaterialHandlingDAL.LogHelper;
using System.IO;
using MaterialHandling.MaterialHandlingUI.UIFrame.Query;
using MaterialHandling.MaterialHandlingPLC;
using S7.Net;
using MaterialHandling.MaterialHandlingUI.UIFrame.ROS;
using MaterialHandlingUI;
namespace MaterialHandling.MaterialHandlingUI.UIFrame
{
    public partial class MainFrame : Form
    {
        // 1. 系统初始化
        public static ControlFrame cf;        //主控制界面    
        public static QueryFrame qf; //查询界面
        public static ManagementFrame mf; //参数管理界面


        //PLC相关模块 20250304 lpd **********
        private static readonly object _lockObject = new object();
        public Timer PlcRWTimer = new Timer(); //plc数据读取定时器
        public static PLCSignal CSignal = new PLCSignal();
        public static S7PLC plc = new S7PLC(CpuType.S71500, "192.168.158.112", 0, 1, 50); //1500sp 插槽号为1，PLC300插槽号为2
        public bool plc_isconnected = false; //plc连接状态

        //CAN相关模块 20250427 lpd
        public static CAN.CanBus canBus = new CAN.CanBus();
        //ROS
        ROSClient rOSClient;
        public MainFrame()
        {
            // Complete member initialization
            InitializeComponent();

            cf = new ControlFrame();
            qf = new QueryFrame();
            mf = new ManagementFrame(tsslBlank);


            //设置系统时间
            this.tsslTime.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            this.timerClock.Interval = 1000;
            this.timerClock.Start();

            //设置全屏
            this.FormBorderStyle = FormBorderStyle.None;
            // this.WindowState = FormWindowState.Maximized;
            if(Program.useDatabase)
            {
                //设置左下角的用户信息
                this.UserInfo.Text = "用户名：" + LoginFrame.username + "  角色：" + LoginFrame.authority;

                //根据用户的权限让其跳转到相应的界面************模块化
                switch (LoginFrame.authority)
                {
                    case "管理员":
                        this.FormBorderStyle = FormBorderStyle.FixedSingle;
                        cf.MdiParent = this;
                        cf.Show();
                        cf.Visible = true;
                        qf.Visible = false;
                        mf.Visible = false;
                        AuthorityJudgeFunction(true, true, true);
                        break;
                    case "检查员":
                        cf.MdiParent = this;
                        cf.Show();
                        cf.Visible = true;
                        qf.Visible = false;
                        mf.Visible = false;
                        AuthorityJudgeFunction(true, true, false);
                        break;
                    case "质检员":
                        qf.MdiParent = this;
                        qf.Show();
                        cf.Visible = false;
                        qf.Visible = true;
                        mf.Visible = false;
                        AuthorityJudgeFunction(false, true, true);
                        mf.QueryManLogin();
                        break;
                    case "查询员":
                        qf.MdiParent = this;
                        qf.Show();
                        cf.Visible = false;
                        qf.Visible = true;
                        mf.Visible = false;
                        AuthorityJudgeFunction(false, true, false);
                        break;
                    case "超级质检员":
                        qf.MdiParent = this;
                        qf.Show();
                        cf.Visible = false;
                        qf.Visible = true;
                        mf.Visible = false;
                        AuthorityJudgeFunction(false, true, true);
                        mf.QueryManLogin();
                        break;
                }
            }
            else
            {
                this.FormBorderStyle = FormBorderStyle.FixedSingle;
                cf.MdiParent = this;
                cf.Show();
                cf.Visible = true;
                qf.Visible = false;
                mf.Visible = false;
            }
            
        }

        // 2. 主界面按钮切换、状态栏
        //根据用户的权限来判断用户可以使用哪些功能
        private void AuthorityJudgeFunction( bool flagButtonCheck, bool flagButtonQuery,bool flagButtonManage) {
            tsbCheck.Enabled = flagButtonCheck;
            stbQuery.Enabled = flagButtonQuery;
            tsbParameter.Enabled = flagButtonManage;
        }


        // 状态栏时间更新
        private void timerClock_Tick(object sender, EventArgs e)
        {
            this.tsslTime.Text = "系统当前时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        // 退出按钮，点击后将退出系统
        private void tsbQuit_Click(object sender, EventArgs e)
        {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定退出系统", "退出系统", messButton);
            if( dr == DialogResult.OK )//如果点击“确定”按钮
            {
                canBus?.Uninitialize();
                cf?.sysControlPanel?.StopVCUCommand();
                rOSClient?.OnClosing();
                if(Program.useDatabase) LogHelp.Info(new LogContent("退出系统", " ", LoginFrame.username));
                Application.Exit();
            }
        }
        // 控制按钮切换
        private void tsbCheck_Click(object sender, EventArgs e)
        {
            
            cf.MdiParent = this;
            cf.Show();
            cf.Visible = true;
            qf.Visible = false;
            mf.Visible = false;
            
        }

        // 查询按钮切换
        private void stbQuery_Click(object sender, EventArgs e)
        {
            qf.MdiParent = this;
            qf.Show();
            cf.Visible = false;
            qf.Visible = true;
            mf.Visible = false;
        }

        // 参数管理按钮切换
        private void tsbParameter_Click(object sender, EventArgs e)
        {
            if (Program.useDatabase) mf.FillDateToView();
            mf.MdiParent = this;
            mf.Show();
            cf.Visible = false;
            qf.Visible = false;
            mf.Visible = true;
        }
        // 注销按钮
        private void toolStripButton_logout_Click( object sender, EventArgs e ) {
            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定注销", "注销", messButton);
            if( dr == DialogResult.OK )//如果点击“确定”按钮
            {
                LogHelp.Info(new LogContent("用户注销", " ", LoginFrame.username));
                this.Close();//关闭本窗口
                new LoginFrame().Show();//重新回到登陆界面
            }
        }     
        //修改密码按钮
        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            UpdatePassWord UPW = new UpdatePassWord();
            UPW.ShowDialog();
        }

        // 显示软件的版本号
        private void tslIntroduction_Click(object sender, EventArgs e)
        {
            string versionNumber = "V0.1";  //版本号
            string screenResolution = "1920*1080";  //屏幕分辨率
            string Datatime = "2025.05.08";
            MessageBox.Show("   版本号: " + versionNumber + "\n\n" + "屏幕分辨率: " + screenResolution + "\n\n" + " 时间：" + Datatime);
        }

        // 3. PLC读写定时器，读写数据保存
        private void PlcRWTimer_Tick(object sender, EventArgs e)//读写计时器
        {
            if(plc.PLC_NetState == true)//PLC连接状态在参数界面更新
            {
                mf.tb_plc_conneted.Text = "已连接";
            }
            else
            {
                try
                {
                    this.PLCInfo.Text = "PLC：初始化成功";
                    plc.Start_PLC();
                    PlcRWTimer.Start();
                }
                catch
                {
                    this.PLCInfo.Text = "PLC：初始化失败";
                }
                mf.tb_plc_conneted.Text = "未连接";
            }
            mf.tb_plc_read.Text = plc.count1.ToString();//PLC读次数的参数界面更新
            mf.tb_plc_write.Text = plc.count2.ToString();//PLC写次数的参数界面更新
            //rtb_Textshow.Clear();
            lock (_lockObject)
            {
                try
                {
                    plc.Receive_Control_Signal(CSignal); //接收控制信号
                }
                catch
                {

                }

                mf.rtb_Textshow.AppendText("bool1:" + plc.Output_DB.bool1.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool2:" + plc.Output_DB.bool2.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool3:" + plc.Output_DB.bool3.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool4:" + plc.Output_DB.bool4.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool5:" + plc.Output_DB.bool5.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool6:" + plc.Output_DB.bool6.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool7:" + plc.Output_DB.bool7.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool8:" + plc.Output_DB.bool8.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool9:" + plc.Output_DB.bool9.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool10:" + plc.Output_DB.bool10.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool11:" + plc.Output_DB.bool11.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool12:" + plc.Output_DB.bool12.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool13:" + plc.Output_DB.bool13.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool14:" + plc.Output_DB.bool14.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool15:" + plc.Output_DB.bool15.ToString() + "\n");
                mf.rtb_Textshow.AppendText("bool16:" + plc.Output_DB.bool16.ToString() + "\n");

                mf.rtb_Textshow.AppendText("int16_1:" + plc.Output_DB.Int1.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_2:" + plc.Output_DB.Int2.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_3:" + plc.Output_DB.Int3.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_4:" + plc.Output_DB.Int4.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_5:" + plc.Output_DB.Int5.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_6:" + plc.Output_DB.Int6.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_7:" + plc.Output_DB.Int7.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_8:" + plc.Output_DB.Int8.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_9:" + plc.Output_DB.Int9.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int16_10:" + plc.Output_DB.Int10.ToString() + "\n");

                mf.rtb_Textshow.AppendText("int32_1:" + plc.Output_DB.Dint1.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_2:" + plc.Output_DB.Dint2.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_3:" + plc.Output_DB.Dint3.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_4:" + plc.Output_DB.Dint4.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_5:" + plc.Output_DB.Dint5.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_6:" + plc.Output_DB.Dint6.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_7:" + plc.Output_DB.Dint7.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_8:" + plc.Output_DB.Dint8.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_9:" + plc.Output_DB.Dint9.ToString() + "\n");
                mf.rtb_Textshow.AppendText("int32_10:" + plc.Output_DB.Dint10.ToString() + "\n");
                //rtb_Textshow.Clear();
            }
        }

        // 4. 系统启动按钮控制
        private void button_SysStart_Click(object sender, EventArgs e)
        {

            InitializeButtonStart();
            if (mf != null) mf.EnableButton();//启动后，启用管理界面的按钮
            //4.1  PLC相关模块 20250304 lpd
            try
            {
                PlcRWTimer.Interval = 2 * 1000; //2s
                PlcRWTimer.Tick += PlcRWTimer_Tick;
                plc.Start_PLC();
                PlcRWTimer.Start();
                if (plc.PLC_NetState)
                {
                    this.PLCInfo.Text = "初始化成功";
                }
                else
                {
                    this.PLCInfo.Text = "初始化失败";
                }
            }
            catch
            {
                this.PLCInfo.Text = "初始化异常";
            }
            //4.2  CAN相关模块

            try
            {
                bool canConnect = canBus.Initialize();//返回Can是否连接成功的标志位
                canBus.StartListening();
                if (canConnect)
                {
                    this.CANInfo.Text = "初始化成功";
                }
                else
                {
                    this.CANInfo.Text = "初始化失败";
                }
            }
            catch
            {
                this.CANInfo.Text = "初始化异常";
            }


            //4.3  ROS相关模块
            try
            {
                //mf.ROS_Panel_Load();
                rOSClient = mf.rOSClient;
                this.ROSInfo.Text = "初始化成功";
            }
            catch
            {
                this.ROSInfo.Text = "初始化异常";
            }
            //4.4  SVR相关模块
            try
            {

            }
            catch
            {
                
            }
            
        }
        private void InitializeButtonStart()
        {
            this.button_Start.Enabled = false;
        }
        private void MainFrame_Load(object sender, EventArgs e)
        {

        }

        private void tslCheck_Click(object sender, EventArgs e)
        {

        }
    }
}
