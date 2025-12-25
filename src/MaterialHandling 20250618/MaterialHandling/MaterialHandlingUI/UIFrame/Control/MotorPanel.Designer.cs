
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    partial class MotorPanel
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_ModeChange = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.Motor_M3_ReclaimerScissor = new WindowsFormsApp1.UserControls.MotorControl();
            this.Motor_M5_Framework = new WindowsFormsApp1.UserControls.MotorControl();
            this.Motor_M1_ReclaimerPlatform = new WindowsFormsApp1.UserControls.MotorControl();
            this.Motor_M4_Reclaimer = new WindowsFormsApp1.UserControls.MotorControl();
            this.Motor_M2_ReclaimerDrum = new WindowsFormsApp1.UserControls.MotorControl();
            this.Motor_M6_ReclaimerFinger = new WindowsFormsApp1.UserControls.MotorControl();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_ModeChange
            // 
            this.btn_ModeChange.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.btn_ModeChange.Location = new System.Drawing.Point(216, 828);
            this.btn_ModeChange.Name = "btn_ModeChange";
            this.btn_ModeChange.Size = new System.Drawing.Size(82, 23);
            this.btn_ModeChange.TabIndex = 17;
            this.btn_ModeChange.Text = "手动/自动";
            this.btn_ModeChange.UseVisualStyleBackColor = false;
            this.btn_ModeChange.Visible = false;
            this.btn_ModeChange.Click += new System.EventHandler(this.btn_ModeChange_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MaterialHandling.Properties.Resources.model1;
            this.pictureBox1.Location = new System.Drawing.Point(-87, -37);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(547, 1005);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(304, 828);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 18;
            this.button1.Text = "读PLC";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.read_plc);
            // 
            // Motor_M3_ReclaimerScissor
            // 
            this.Motor_M3_ReclaimerScissor.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M3_ReclaimerScissor.BtnActivateColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Motor_M3_ReclaimerScissor.BtnActivateVisable = false;
            this.Motor_M3_ReclaimerScissor.BtnBackwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M3_ReclaimerScissor.BtnBackwardText = "后退";
            this.Motor_M3_ReclaimerScissor.BtnForwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M3_ReclaimerScissor.BtnForwardText = "前进";
            this.Motor_M3_ReclaimerScissor.Location = new System.Drawing.Point(58, 706);
            this.Motor_M3_ReclaimerScissor.MotorCode = "M3";
            this.Motor_M3_ReclaimerScissor.MotorName = "取料剪叉";
            this.Motor_M3_ReclaimerScissor.Name = "Motor_M3_ReclaimerScissor";
            this.Motor_M3_ReclaimerScissor.Size = new System.Drawing.Size(233, 52);
            this.Motor_M3_ReclaimerScissor.TabIndex = 19;
            this.Motor_M3_ReclaimerScissor.btn_ForwardClick += new System.EventHandler(this.M3_ReclaimerScissor_btn_ForwardClick);
            this.Motor_M3_ReclaimerScissor.btn_BackwardClick += new System.EventHandler(this.M3_ReclaimerScissor_btn_BackwardClick);
            // 
            // Motor_M5_Framework
            // 
            this.Motor_M5_Framework.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M5_Framework.BtnActivateColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Motor_M5_Framework.BtnActivateVisable = false;
            this.Motor_M5_Framework.BtnBackwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M5_Framework.BtnBackwardText = "下降";
            this.Motor_M5_Framework.BtnForwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M5_Framework.BtnForwardText = "上升";
            this.Motor_M5_Framework.Location = new System.Drawing.Point(57, 26);
            this.Motor_M5_Framework.MotorCode = "M5";
            this.Motor_M5_Framework.MotorName = "框架";
            this.Motor_M5_Framework.Name = "Motor_M5_Framework";
            this.Motor_M5_Framework.Size = new System.Drawing.Size(230, 52);
            this.Motor_M5_Framework.TabIndex = 16;
            this.Motor_M5_Framework.btn_ForwardClick += new System.EventHandler(this.M5_Framework_btn_ForwardClick);
            this.Motor_M5_Framework.btn_BackwardClick += new System.EventHandler(this.M5_Framework_btn_BackwardClick);
            this.Motor_M5_Framework.Load += new System.EventHandler(this.Motor_M1_ReclaimerBelt_Load);
            // 
            // Motor_M1_ReclaimerPlatform
            // 
            this.Motor_M1_ReclaimerPlatform.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M1_ReclaimerPlatform.BtnActivateColor = System.Drawing.Color.Lime;
            this.Motor_M1_ReclaimerPlatform.BtnActivateVisable = true;
            this.Motor_M1_ReclaimerPlatform.BtnBackwardBackColor = System.Drawing.Color.DarkGray;
            this.Motor_M1_ReclaimerPlatform.BtnBackwardText = "下降";
            this.Motor_M1_ReclaimerPlatform.BtnForwardBackColor = System.Drawing.Color.Lime;
            this.Motor_M1_ReclaimerPlatform.BtnForwardText = "上升";
            this.Motor_M1_ReclaimerPlatform.Location = new System.Drawing.Point(57, 84);
            this.Motor_M1_ReclaimerPlatform.MotorCode = "M1";
            this.Motor_M1_ReclaimerPlatform.MotorName = "取料平台";
            this.Motor_M1_ReclaimerPlatform.Name = "Motor_M1_ReclaimerPlatform";
            this.Motor_M1_ReclaimerPlatform.Size = new System.Drawing.Size(280, 52);
            this.Motor_M1_ReclaimerPlatform.TabIndex = 15;
            this.Motor_M1_ReclaimerPlatform.btn_ForwardClick += new System.EventHandler(this.M1_ReclaimerPlatform_btn_ForwardClick);
            this.Motor_M1_ReclaimerPlatform.btn_BackwardClick += new System.EventHandler(this.M1_ReclaimerPlatform_btn_BackwardClick);
            this.Motor_M1_ReclaimerPlatform.btn_ActivateClick += new System.EventHandler(this.Motor_M1_ReclaimerPlatform_btn_ActivateClick);
            this.Motor_M1_ReclaimerPlatform.Load += new System.EventHandler(this.M1_ReclaimerPlatform_Load);
            // 
            // Motor_M4_Reclaimer
            // 
            this.Motor_M4_Reclaimer.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M4_Reclaimer.BtnActivateColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Motor_M4_Reclaimer.BtnActivateVisable = false;
            this.Motor_M4_Reclaimer.BtnBackwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M4_Reclaimer.BtnBackwardText = "右移";
            this.Motor_M4_Reclaimer.BtnForwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M4_Reclaimer.BtnForwardText = "左移";
            this.Motor_M4_Reclaimer.Location = new System.Drawing.Point(58, 142);
            this.Motor_M4_Reclaimer.MotorCode = "M4";
            this.Motor_M4_Reclaimer.MotorName = "取料";
            this.Motor_M4_Reclaimer.Name = "Motor_M4_Reclaimer";
            this.Motor_M4_Reclaimer.Size = new System.Drawing.Size(229, 52);
            this.Motor_M4_Reclaimer.TabIndex = 14;
            this.Motor_M4_Reclaimer.btn_ForwardClick += new System.EventHandler(this.M4_Reclaimer_btn_ForwardClick);
            this.Motor_M4_Reclaimer.btn_BackwardClick += new System.EventHandler(this.M4_Reclaimer_btn_BackwardClick);
            // 
            // Motor_M2_ReclaimerDrum
            // 
            this.Motor_M2_ReclaimerDrum.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M2_ReclaimerDrum.BtnActivateColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.Motor_M2_ReclaimerDrum.BtnActivateVisable = false;
            this.Motor_M2_ReclaimerDrum.BtnBackwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M2_ReclaimerDrum.BtnBackwardText = "后退";
            this.Motor_M2_ReclaimerDrum.BtnForwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M2_ReclaimerDrum.BtnForwardText = "前进";
            this.Motor_M2_ReclaimerDrum.Location = new System.Drawing.Point(58, 764);
            this.Motor_M2_ReclaimerDrum.MotorCode = "M2";
            this.Motor_M2_ReclaimerDrum.MotorName = "取料滚筒";
            this.Motor_M2_ReclaimerDrum.Name = "Motor_M2_ReclaimerDrum";
            this.Motor_M2_ReclaimerDrum.Size = new System.Drawing.Size(233, 52);
            this.Motor_M2_ReclaimerDrum.TabIndex = 13;
            this.Motor_M2_ReclaimerDrum.btn_ForwardClick += new System.EventHandler(this.M2_ReclaimerDrum_btn_ForwardClick);
            this.Motor_M2_ReclaimerDrum.btn_BackwardClick += new System.EventHandler(this.M2_ReclaimerDrum_btn_BackwardClick);
            // 
            // Motor_M6_ReclaimerFinger
            // 
            this.Motor_M6_ReclaimerFinger.BackColor = System.Drawing.Color.Transparent;
            this.Motor_M6_ReclaimerFinger.BtnActivateColor = System.Drawing.Color.Lime;
            this.Motor_M6_ReclaimerFinger.BtnActivateVisable = true;
            this.Motor_M6_ReclaimerFinger.BtnBackwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M6_ReclaimerFinger.BtnBackwardText = "后退";
            this.Motor_M6_ReclaimerFinger.BtnForwardBackColor = System.Drawing.Color.Gainsboro;
            this.Motor_M6_ReclaimerFinger.BtnForwardText = "前进";
            this.Motor_M6_ReclaimerFinger.Location = new System.Drawing.Point(59, 200);
            this.Motor_M6_ReclaimerFinger.MotorCode = "M6";
            this.Motor_M6_ReclaimerFinger.MotorName = "取料手指";
            this.Motor_M6_ReclaimerFinger.Name = "Motor_M6_ReclaimerFinger";
            this.Motor_M6_ReclaimerFinger.Size = new System.Drawing.Size(278, 52);
            this.Motor_M6_ReclaimerFinger.TabIndex = 12;
            this.Motor_M6_ReclaimerFinger.btn_ForwardClick += new System.EventHandler(this.M6_ReclaimerFinger_btn_ForwardClick);
            this.Motor_M6_ReclaimerFinger.btn_BackwardClick += new System.EventHandler(this.M6_ReclaimerFinger_btn_BackwardClick);
            this.Motor_M6_ReclaimerFinger.btn_ActivateClick += new System.EventHandler(this.Motor_M6_ReclaimerFinger_btn_ActivateClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(34, 269);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 26);
            this.label1.TabIndex = 20;
            this.label1.Text = "M5";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(164, 394);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(42, 26);
            this.label2.TabIndex = 21;
            this.label2.Text = "M1";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(249, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(42, 26);
            this.label3.TabIndex = 22;
            this.label3.Text = "M4";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(337, 379);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(42, 26);
            this.label4.TabIndex = 23;
            this.label4.Text = "M6";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(190, 350);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(42, 26);
            this.label5.TabIndex = 24;
            this.label5.Text = "M2";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(238, 350);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(42, 26);
            this.label6.TabIndex = 25;
            this.label6.Text = "M3";
            // 
            // MotorPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Motor_M3_ReclaimerScissor);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_ModeChange);
            this.Controls.Add(this.Motor_M5_Framework);
            this.Controls.Add(this.Motor_M1_ReclaimerPlatform);
            this.Controls.Add(this.Motor_M4_Reclaimer);
            this.Controls.Add(this.Motor_M2_ReclaimerDrum);
            this.Controls.Add(this.Motor_M6_ReclaimerFinger);
            this.Controls.Add(this.pictureBox1);
            this.Name = "MotorPanel";
            this.Size = new System.Drawing.Size(410, 888);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M6_ReclaimerFinger;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M2_ReclaimerDrum;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M4_Reclaimer;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M1_ReclaimerPlatform;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M5_Framework;
        private System.Windows.Forms.Button btn_ModeChange;
        private System.Windows.Forms.Button button1;
        private WindowsFormsApp1.UserControls.MotorControl Motor_M3_ReclaimerScissor;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}
