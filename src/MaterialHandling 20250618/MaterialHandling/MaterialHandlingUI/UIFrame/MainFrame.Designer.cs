using System.Windows.Forms;
using System;

namespace MaterialHandling.MaterialHandlingUI.UIFrame
{
    partial class MainFrame
    {

        #region Singleton Model
        private static MainFrame uniqueInstance;
        private static readonly object locker = new object();

        /// <summary>
        /// 定义公有方法提供一个全局访问点
        /// </summary>
        /// <returns></returns>
        public static MainFrame GetInstance()
        {
            // lock语句运行完之后（即线程运行完之后）会对该对象"解锁"
            lock (locker)
            {
                if (uniqueInstance == null)
                {
                    uniqueInstance = new MainFrame();
                }
            }
            return uniqueInstance;
        }
        #endregion

        #region Cancel Double Click
        /// <summary>
        /// 取消窗体的双击事件
        /// </summary>
        /// <returns></returns>
        protected override void WndProc(ref Message m)
        
        {
            if (m.Msg == 0x112)
            {
                switch ((int)m.WParam)
                {
                    //禁止双击标题栏关闭窗体
                    //case 0xF063:
                    //case 0xF093:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                    ////禁止拖拽标题栏还原窗体
                    //case 0xF012:
                    //case 0xF010:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                    ////禁止双击标题栏
                    //case 0xf122:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                    //禁止关闭按钮
                    case 0xF060:
                        m.WParam = IntPtr.Zero;
                        break;
                    ////禁止最大化按钮
                    //case 0xf020:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                    ////禁止最小化按钮
                    //case 0xf030:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                    ////禁止还原按钮
                    //case 0xf120:
                    //    m.WParam = IntPtr.Zero;
                    //    break;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrame));
            this.tsALL = new System.Windows.Forms.ToolStrip();
            this.tsbCheck = new System.Windows.Forms.ToolStripButton();
            this.tslCheck = new System.Windows.Forms.ToolStripLabel();
            this.stbQuery = new System.Windows.Forms.ToolStripButton();
            this.tslQuery = new System.Windows.Forms.ToolStripLabel();
            this.tsbParameter = new System.Windows.Forms.ToolStripButton();
            this.tslParameter = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton_logout = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.tsbQuit = new System.Windows.Forms.ToolStripButton();
            this.tslQuit = new System.Windows.Forms.ToolStripLabel();
            this.tslIntroduction = new System.Windows.Forms.ToolStripLabel();
            this.ssInfo = new System.Windows.Forms.StatusStrip();
            this.UserInfo = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslBlank = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslTime = new System.Windows.Forms.ToolStripStatusLabel();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.button_Start = new System.Windows.Forms.Button();
            this.PLCInfo = new System.Windows.Forms.Label();
            this.CANInfo = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.ROSInfo = new System.Windows.Forms.Label();
            this.SVRInfo = new System.Windows.Forms.Label();
            this.tsALL.SuspendLayout();
            this.ssInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // tsALL
            // 
            this.tsALL.BackColor = System.Drawing.SystemColors.Window;
            this.tsALL.ImageScalingSize = new System.Drawing.Size(48, 48);
            this.tsALL.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsbCheck,
            this.tslCheck,
            this.stbQuery,
            this.tslQuery,
            this.tsbParameter,
            this.tslParameter,
            this.toolStripButton_logout,
            this.toolStripLabel1,
            this.toolStripButton1,
            this.toolStripLabel2,
            this.tsbQuit,
            this.tslQuit,
            this.tslIntroduction});
            this.tsALL.Location = new System.Drawing.Point(0, 0);
            this.tsALL.Name = "tsALL";
            this.tsALL.Size = new System.Drawing.Size(1600, 55);
            this.tsALL.TabIndex = 1;
            this.tsALL.Text = "toolStrip1";
            // 
            // tsbCheck
            // 
            this.tsbCheck.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbCheck.Image = ((System.Drawing.Image)(resources.GetObject("tsbCheck.Image")));
            this.tsbCheck.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbCheck.Name = "tsbCheck";
            this.tsbCheck.Size = new System.Drawing.Size(52, 52);
            this.tsbCheck.Click += new System.EventHandler(this.tsbCheck_Click);
            // 
            // tslCheck
            // 
            this.tslCheck.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tslCheck.Name = "tslCheck";
            this.tslCheck.Size = new System.Drawing.Size(74, 52);
            this.tslCheck.Text = "操作界面";
            // 
            // stbQuery
            // 
            this.stbQuery.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.stbQuery.Image = ((System.Drawing.Image)(resources.GetObject("stbQuery.Image")));
            this.stbQuery.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.stbQuery.Name = "stbQuery";
            this.stbQuery.Size = new System.Drawing.Size(52, 52);
            this.stbQuery.Click += new System.EventHandler(this.stbQuery_Click);
            // 
            // tslQuery
            // 
            this.tslQuery.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tslQuery.Name = "tslQuery";
            this.tslQuery.Size = new System.Drawing.Size(74, 52);
            this.tslQuery.Text = "数据查询";
            // 
            // tsbParameter
            // 
            this.tsbParameter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbParameter.Image = ((System.Drawing.Image)(resources.GetObject("tsbParameter.Image")));
            this.tsbParameter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbParameter.Name = "tsbParameter";
            this.tsbParameter.Size = new System.Drawing.Size(52, 52);
            this.tsbParameter.Text = "toolStripButton1";
            this.tsbParameter.Click += new System.EventHandler(this.tsbParameter_Click);
            // 
            // tslParameter
            // 
            this.tslParameter.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tslParameter.Name = "tslParameter";
            this.tslParameter.Size = new System.Drawing.Size(74, 52);
            this.tslParameter.Text = "参数管理";
            // 
            // toolStripButton_logout
            // 
            this.toolStripButton_logout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton_logout.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton_logout.Image")));
            this.toolStripButton_logout.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton_logout.Name = "toolStripButton_logout";
            this.toolStripButton_logout.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton_logout.Click += new System.EventHandler(this.toolStripButton_logout_Click);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(74, 52);
            this.toolStripLabel1.Text = "注销系统";
            // 
            // toolStripButton1
            // 
            this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
            this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton1.Name = "toolStripButton1";
            this.toolStripButton1.Size = new System.Drawing.Size(52, 52);
            this.toolStripButton1.Text = "toolStripButton1";
            this.toolStripButton1.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(74, 52);
            this.toolStripLabel2.Text = "修改密码";
            // 
            // tsbQuit
            // 
            this.tsbQuit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tsbQuit.Image = ((System.Drawing.Image)(resources.GetObject("tsbQuit.Image")));
            this.tsbQuit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tsbQuit.Name = "tsbQuit";
            this.tsbQuit.Size = new System.Drawing.Size(52, 52);
            this.tsbQuit.Click += new System.EventHandler(this.tsbQuit_Click);
            // 
            // tslQuit
            // 
            this.tslQuit.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.tslQuit.Name = "tslQuit";
            this.tslQuit.Size = new System.Drawing.Size(74, 52);
            this.tslQuit.Text = "退出系统";
            // 
            // tslIntroduction
            // 
            this.tslIntroduction.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.tslIntroduction.Font = new System.Drawing.Font("微软雅黑", 16F, System.Drawing.FontStyle.Bold);
            this.tslIntroduction.ForeColor = System.Drawing.Color.SeaGreen;
            this.tslIntroduction.Name = "tslIntroduction";
            this.tslIntroduction.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tslIntroduction.Size = new System.Drawing.Size(261, 52);
            this.tslIntroduction.Text = "智能机器人装卸系统V0.1";
            this.tslIntroduction.Click += new System.EventHandler(this.tslIntroduction_Click);
            // 
            // ssInfo
            // 
            this.ssInfo.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.ssInfo.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.UserInfo,
            this.tsslBlank,
            this.tsslTime});
            this.ssInfo.Location = new System.Drawing.Point(0, 878);
            this.ssInfo.Name = "ssInfo";
            this.ssInfo.Padding = new System.Windows.Forms.Padding(1, 0, 19, 0);
            this.ssInfo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ssInfo.Size = new System.Drawing.Size(1600, 22);
            this.ssInfo.TabIndex = 3;
            this.ssInfo.Text = "statusStrip1";
            // 
            // UserInfo
            // 
            this.UserInfo.Name = "UserInfo";
            this.UserInfo.Size = new System.Drawing.Size(172, 17);
            this.UserInfo.Text = "用户名：admin  角色：管理员";
            this.UserInfo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslBlank
            // 
            this.tsslBlank.BorderSides = ((System.Windows.Forms.ToolStripStatusLabelBorderSides)((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left | System.Windows.Forms.ToolStripStatusLabelBorderSides.Right)));
            this.tsslBlank.Name = "tsslBlank";
            this.tsslBlank.Size = new System.Drawing.Size(1408, 17);
            this.tsslBlank.Spring = true;
            this.tsslBlank.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tsslTime
            // 
            this.tsslTime.Name = "tsslTime";
            this.tsslTime.Size = new System.Drawing.Size(0, 17);
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1290, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(48, 48);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 5;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1190, 2);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(90, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 7;
            this.pictureBox2.TabStop = false;
            // 
            // button_Start
            // 
            this.button_Start.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold);
            this.button_Start.Location = new System.Drawing.Point(770, 12);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(67, 28);
            this.button_Start.TabIndex = 11;
            this.button_Start.Text = "启 动";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_SysStart_Click);
            // 
            // PLCInfo
            // 
            this.PLCInfo.AutoSize = true;
            this.PLCInfo.BackColor = System.Drawing.Color.White;
            this.PLCInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.PLCInfo.Font = new System.Drawing.Font("宋体", 10F);
            this.PLCInfo.Location = new System.Drawing.Point(895, 8);
            this.PLCInfo.Name = "PLCInfo";
            this.PLCInfo.Size = new System.Drawing.Size(93, 16);
            this.PLCInfo.TabIndex = 13;
            this.PLCInfo.Text = "未连接AAAAAA";
            // 
            // CANInfo
            // 
            this.CANInfo.AutoSize = true;
            this.CANInfo.BackColor = System.Drawing.Color.White;
            this.CANInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.CANInfo.Font = new System.Drawing.Font("宋体", 10F);
            this.CANInfo.Location = new System.Drawing.Point(895, 32);
            this.CANInfo.Name = "CANInfo";
            this.CANInfo.Size = new System.Drawing.Size(93, 16);
            this.CANInfo.TabIndex = 14;
            this.CANInfo.Text = "未连接AAAAAA";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(861, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(28, 14);
            this.label1.TabIndex = 16;
            this.label1.Text = "PLC";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(861, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 14);
            this.label2.TabIndex = 16;
            this.label2.Text = "VCU";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(1019, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(28, 14);
            this.label3.TabIndex = 16;
            this.label3.Text = "ROS";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(1019, 33);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(28, 14);
            this.label4.TabIndex = 16;
            this.label4.Text = "SVR";
            // 
            // ROSInfo
            // 
            this.ROSInfo.AutoSize = true;
            this.ROSInfo.BackColor = System.Drawing.Color.White;
            this.ROSInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.ROSInfo.Font = new System.Drawing.Font("宋体", 10F);
            this.ROSInfo.Location = new System.Drawing.Point(1053, 8);
            this.ROSInfo.Name = "ROSInfo";
            this.ROSInfo.Size = new System.Drawing.Size(93, 16);
            this.ROSInfo.TabIndex = 18;
            this.ROSInfo.Text = "未连接AAAAAA";
            // 
            // SVRInfo
            // 
            this.SVRInfo.AutoSize = true;
            this.SVRInfo.BackColor = System.Drawing.Color.White;
            this.SVRInfo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.SVRInfo.Font = new System.Drawing.Font("宋体", 10F);
            this.SVRInfo.Location = new System.Drawing.Point(1053, 32);
            this.SVRInfo.Name = "SVRInfo";
            this.SVRInfo.Size = new System.Drawing.Size(93, 16);
            this.SVRInfo.TabIndex = 19;
            this.SVRInfo.Text = "未连接AAAAAA";
            // 
            // MainFrame
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1600, 900);
            this.Controls.Add(this.SVRInfo);
            this.Controls.Add(this.ROSInfo);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.CANInfo);
            this.Controls.Add(this.PLCInfo);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.ssInfo);
            this.Controls.Add(this.tsALL);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "MainFrame";
            this.Text = "西高电气装卸机器人控制系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainFrame_Load);
            this.tsALL.ResumeLayout(false);
            this.tsALL.PerformLayout();
            this.ssInfo.ResumeLayout(false);
            this.ssInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsALL;
        private System.Windows.Forms.ToolStripButton tsbCheck;
        private System.Windows.Forms.ToolStripButton stbQuery;
        private System.Windows.Forms.ToolStripButton tsbQuit;
        private System.Windows.Forms.ToolStripLabel tslCheck;
        private System.Windows.Forms.ToolStripLabel tslQuery;
        private System.Windows.Forms.ToolStripLabel tslQuit;
        private System.Windows.Forms.ToolStripLabel MinMax;
        private System.Windows.Forms.ToolStripLabel Normal;
        private StatusStrip ssInfo;
        private ToolStripStatusLabel UserInfo;
        private ToolStripStatusLabel tsslBlank;
        private ToolStripStatusLabel tsslTime;
        private Timer timerClock;
        private ToolStripButton tsbParameter;
        private ToolStripLabel tslParameter;
        private ToolStripLabel tslIntroduction;
        private ToolStripButton toolStripButton_logout;
        private ToolStripLabel toolStripLabel1;
        private ToolStripButton toolStripButton1;
        private ToolStripLabel toolStripLabel2;
        private PictureBox pictureBox1;
        private PictureBox pictureBox2;
        private Button button_Start;
        private Label PLCInfo;
        private Label CANInfo;
        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label ROSInfo;
        private Label SVRInfo;
    }
}