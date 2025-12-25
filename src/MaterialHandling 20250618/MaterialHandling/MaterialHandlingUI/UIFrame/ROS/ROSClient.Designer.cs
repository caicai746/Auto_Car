
namespace MaterialHandling.MaterialHandlingUI.UIFrame.ROS
{
    partial class ROSClient
    {
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
            this.rtb_ROS_Log = new System.Windows.Forms.RichTextBox();
            this.btn_ROS_Connect = new System.Windows.Forms.Button();
            this.tb_Server_IP = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.btn_ROS_Disconnect = new System.Windows.Forms.Button();
            this.label16 = new System.Windows.Forms.Label();
            this.btn_StartLidar = new System.Windows.Forms.Button();
            this.label15 = new System.Windows.Forms.Label();
            this.btn_buildmap = new System.Windows.Forms.Button();
            this.tb_Server_MapPort = new System.Windows.Forms.TextBox();
            this.btn_SaveMap = new System.Windows.Forms.Button();
            this.tb_Server_PosePort = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.tb_xN = new System.Windows.Forms.TextBox();
            this.tb_yN = new System.Windows.Forms.TextBox();
            this.tb_yP = new System.Windows.Forms.TextBox();
            this.tb_xP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.bt_Savemap = new System.Windows.Forms.Button();
            this.tb_prebuildTime = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.tb_buildTime = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.bt_Clear_rtb_ROS_Log = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_ROS_Log
            // 
            this.rtb_ROS_Log.Font = new System.Drawing.Font("宋体", 9F);
            this.rtb_ROS_Log.Location = new System.Drawing.Point(15, 288);
            this.rtb_ROS_Log.Name = "rtb_ROS_Log";
            this.rtb_ROS_Log.Size = new System.Drawing.Size(420, 540);
            this.rtb_ROS_Log.TabIndex = 43;
            this.rtb_ROS_Log.Text = "";
            // 
            // btn_ROS_Connect
            // 
            this.btn_ROS_Connect.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_ROS_Connect.Location = new System.Drawing.Point(28, 130);
            this.btn_ROS_Connect.Name = "btn_ROS_Connect";
            this.btn_ROS_Connect.Size = new System.Drawing.Size(65, 26);
            this.btn_ROS_Connect.TabIndex = 39;
            this.btn_ROS_Connect.Text = "连 接";
            this.btn_ROS_Connect.UseVisualStyleBackColor = true;
            this.btn_ROS_Connect.Click += new System.EventHandler(this.btn_ROS_Connect_Click);
            // 
            // tb_Server_IP
            // 
            this.tb_Server_IP.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_Server_IP.Location = new System.Drawing.Point(85, 28);
            this.tb_Server_IP.Name = "tb_Server_IP";
            this.tb_Server_IP.Size = new System.Drawing.Size(147, 26);
            this.tb_Server_IP.TabIndex = 36;
            this.tb_Server_IP.Text = "192.168.158.15";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("宋体", 10F);
            this.label12.Location = new System.Drawing.Point(15, 266);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(98, 14);
            this.label12.TabIndex = 44;
            this.label12.Text = "ROS传入参数：";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F);
            this.label17.Location = new System.Drawing.Point(16, 34);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 37;
            this.label17.Text = "服务端ip";
            // 
            // btn_ROS_Disconnect
            // 
            this.btn_ROS_Disconnect.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_ROS_Disconnect.Location = new System.Drawing.Point(99, 130);
            this.btn_ROS_Disconnect.Name = "btn_ROS_Disconnect";
            this.btn_ROS_Disconnect.Size = new System.Drawing.Size(64, 26);
            this.btn_ROS_Disconnect.TabIndex = 45;
            this.btn_ROS_Disconnect.Text = "断 开";
            this.btn_ROS_Disconnect.UseVisualStyleBackColor = true;
            this.btn_ROS_Disconnect.Click += new System.EventHandler(this.btn_ROS_Disconnect_Click);
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Font = new System.Drawing.Font("宋体", 10F);
            this.label16.Location = new System.Drawing.Point(16, 66);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(63, 14);
            this.label16.TabIndex = 40;
            this.label16.Text = "位姿端口";
            // 
            // btn_StartLidar
            // 
            this.btn_StartLidar.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_StartLidar.Location = new System.Drawing.Point(28, 160);
            this.btn_StartLidar.Name = "btn_StartLidar";
            this.btn_StartLidar.Size = new System.Drawing.Size(65, 26);
            this.btn_StartLidar.TabIndex = 46;
            this.btn_StartLidar.Text = "开启雷达";
            this.btn_StartLidar.UseVisualStyleBackColor = true;
            this.btn_StartLidar.Click += new System.EventHandler(this.btn_StartLidar_Click);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("宋体", 10F);
            this.label15.Location = new System.Drawing.Point(16, 98);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(63, 14);
            this.label15.TabIndex = 42;
            this.label15.Text = "地图端口";
            // 
            // btn_buildmap
            // 
            this.btn_buildmap.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_buildmap.Location = new System.Drawing.Point(28, 217);
            this.btn_buildmap.Name = "btn_buildmap";
            this.btn_buildmap.Size = new System.Drawing.Size(65, 26);
            this.btn_buildmap.TabIndex = 47;
            this.btn_buildmap.Text = "建图";
            this.btn_buildmap.UseVisualStyleBackColor = true;
            this.btn_buildmap.Click += new System.EventHandler(this.btn_buildmap_Click);
            // 
            // tb_Server_MapPort
            // 
            this.tb_Server_MapPort.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_Server_MapPort.Location = new System.Drawing.Point(85, 92);
            this.tb_Server_MapPort.Name = "tb_Server_MapPort";
            this.tb_Server_MapPort.Size = new System.Drawing.Size(147, 26);
            this.tb_Server_MapPort.TabIndex = 41;
            this.tb_Server_MapPort.Text = "5001";
            // 
            // btn_SaveMap
            // 
            this.btn_SaveMap.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_SaveMap.Location = new System.Drawing.Point(28, 188);
            this.btn_SaveMap.Name = "btn_SaveMap";
            this.btn_SaveMap.Size = new System.Drawing.Size(65, 26);
            this.btn_SaveMap.TabIndex = 48;
            this.btn_SaveMap.Text = "预建图";
            this.btn_SaveMap.UseVisualStyleBackColor = true;
            this.btn_SaveMap.Click += new System.EventHandler(this.btn_SaveMap_Click);
            // 
            // tb_Server_PosePort
            // 
            this.tb_Server_PosePort.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_Server_PosePort.Location = new System.Drawing.Point(85, 60);
            this.tb_Server_PosePort.Name = "tb_Server_PosePort";
            this.tb_Server_PosePort.Size = new System.Drawing.Size(147, 26);
            this.tb_Server_PosePort.TabIndex = 38;
            this.tb_Server_PosePort.Text = "5000";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(9, 9);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(82, 19);
            this.label18.TabIndex = 35;
            this.label18.Text = "ROS系统";
            // 
            // tb_xN
            // 
            this.tb_xN.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_xN.Location = new System.Drawing.Point(303, 192);
            this.tb_xN.Name = "tb_xN";
            this.tb_xN.Size = new System.Drawing.Size(29, 23);
            this.tb_xN.TabIndex = 49;
            this.tb_xN.Text = "600";
            this.tb_xN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_yN
            // 
            this.tb_yN.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_yN.Location = new System.Drawing.Point(303, 220);
            this.tb_yN.Name = "tb_yN";
            this.tb_yN.Size = new System.Drawing.Size(29, 23);
            this.tb_yN.TabIndex = 50;
            this.tb_yN.Text = "160";
            this.tb_yN.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_yP
            // 
            this.tb_yP.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_yP.Location = new System.Drawing.Point(267, 220);
            this.tb_yP.Name = "tb_yP";
            this.tb_yP.Size = new System.Drawing.Size(29, 23);
            this.tb_yP.TabIndex = 52;
            this.tb_yP.Text = "340";
            this.tb_yP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_xP
            // 
            this.tb_xP.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_xP.Location = new System.Drawing.Point(267, 192);
            this.tb_xP.Name = "tb_xP";
            this.tb_xP.Size = new System.Drawing.Size(29, 23);
            this.tb_xP.TabIndex = 51;
            this.tb_xP.Text = "900";
            this.tb_xP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(250, 196);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(14, 14);
            this.label1.TabIndex = 53;
            this.label1.Text = "X";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(250, 224);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(14, 14);
            this.label2.TabIndex = 54;
            this.label2.Text = "Y";
            // 
            // bt_Savemap
            // 
            this.bt_Savemap.Font = new System.Drawing.Font("宋体", 9F);
            this.bt_Savemap.Location = new System.Drawing.Point(169, 130);
            this.bt_Savemap.Name = "bt_Savemap";
            this.bt_Savemap.Size = new System.Drawing.Size(65, 26);
            this.bt_Savemap.TabIndex = 56;
            this.bt_Savemap.Text = "保存地图";
            this.bt_Savemap.UseVisualStyleBackColor = true;
            this.bt_Savemap.Click += new System.EventHandler(this.bt_Savemap_Click);
            // 
            // tb_prebuildTime
            // 
            this.tb_prebuildTime.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_prebuildTime.Location = new System.Drawing.Point(177, 192);
            this.tb_prebuildTime.Name = "tb_prebuildTime";
            this.tb_prebuildTime.Size = new System.Drawing.Size(29, 23);
            this.tb_prebuildTime.TabIndex = 57;
            this.tb_prebuildTime.Text = "30";
            this.tb_prebuildTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(100, 194);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(77, 14);
            this.label3.TabIndex = 58;
            this.label3.Text = "预建图时间";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 10F);
            this.label4.Location = new System.Drawing.Point(100, 223);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 14);
            this.label4.TabIndex = 59;
            this.label4.Text = "建图时间";
            // 
            // tb_buildTime
            // 
            this.tb_buildTime.Font = new System.Drawing.Font("宋体", 10F);
            this.tb_buildTime.Location = new System.Drawing.Point(177, 221);
            this.tb_buildTime.Name = "tb_buildTime";
            this.tb_buildTime.Size = new System.Drawing.Size(29, 23);
            this.tb_buildTime.TabIndex = 60;
            this.tb_buildTime.Text = "10";
            this.tb_buildTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 10F);
            this.label5.Location = new System.Drawing.Point(213, 194);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 14);
            this.label5.TabIndex = 61;
            this.label5.Text = "秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 10F);
            this.label6.Location = new System.Drawing.Point(213, 224);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(21, 14);
            this.label6.TabIndex = 62;
            this.label6.Text = "秒";
            // 
            // bt_Clear_rtb_ROS_Log
            // 
            this.bt_Clear_rtb_ROS_Log.Font = new System.Drawing.Font("宋体", 9F);
            this.bt_Clear_rtb_ROS_Log.Location = new System.Drawing.Point(370, 256);
            this.bt_Clear_rtb_ROS_Log.Name = "bt_Clear_rtb_ROS_Log";
            this.bt_Clear_rtb_ROS_Log.Size = new System.Drawing.Size(65, 26);
            this.bt_Clear_rtb_ROS_Log.TabIndex = 63;
            this.bt_Clear_rtb_ROS_Log.Text = "清空文本";
            this.bt_Clear_rtb_ROS_Log.UseVisualStyleBackColor = true;
            this.bt_Clear_rtb_ROS_Log.Click += new System.EventHandler(this.bt_Clear_rtb_ROS_Log_Click);
            // 
            // ROSClient
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.bt_Clear_rtb_ROS_Log);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.tb_buildTime);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_prebuildTime);
            this.Controls.Add(this.bt_Savemap);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_yP);
            this.Controls.Add(this.tb_xP);
            this.Controls.Add(this.tb_yN);
            this.Controls.Add(this.tb_xN);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tb_Server_IP);
            this.Controls.Add(this.tb_Server_PosePort);
            this.Controls.Add(this.rtb_ROS_Log);
            this.Controls.Add(this.btn_SaveMap);
            this.Controls.Add(this.btn_ROS_Connect);
            this.Controls.Add(this.tb_Server_MapPort);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.btn_buildmap);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.btn_ROS_Disconnect);
            this.Controls.Add(this.btn_StartLidar);
            this.Controls.Add(this.label16);
            this.Name = "ROSClient";
            this.Size = new System.Drawing.Size(450, 850);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.RichTextBox rtb_ROS_Log;
        public System.Windows.Forms.TextBox tb_Server_IP;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label17;
        public System.Windows.Forms.Button btn_ROS_Disconnect;
        private System.Windows.Forms.Label label16;
        public System.Windows.Forms.Button btn_StartLidar;
        private System.Windows.Forms.Label label15;
        public System.Windows.Forms.Button btn_buildmap;
        private System.Windows.Forms.TextBox tb_Server_MapPort;
        public System.Windows.Forms.Button btn_SaveMap;
        private System.Windows.Forms.TextBox tb_Server_PosePort;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.Button btn_ROS_Connect;
        private System.Windows.Forms.TextBox tb_xN;
        private System.Windows.Forms.TextBox tb_yN;
        private System.Windows.Forms.TextBox tb_yP;
        private System.Windows.Forms.TextBox tb_xP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button bt_Savemap;
        private System.Windows.Forms.TextBox tb_prebuildTime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tb_buildTime;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button bt_Clear_rtb_ROS_Log;
    }
}