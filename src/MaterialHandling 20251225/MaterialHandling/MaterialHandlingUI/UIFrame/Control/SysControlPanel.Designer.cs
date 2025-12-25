
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    partial class SysControlPanel
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
            this.btnRotateLeft = new System.Windows.Forms.Button();
            this.btnSwitchMode = new System.Windows.Forms.Button();
            this.btnRotateRight = new System.Windows.Forms.Button();
            this.btnForward = new System.Windows.Forms.Button();
            this.btnBackward = new System.Windows.Forms.Button();
            this.textBox_speed = new System.Windows.Forms.TextBox();
            this.textBox_angle = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button_Stop_speed = new System.Windows.Forms.Button();
            this.button_Stop_angle = new System.Windows.Forms.Button();
            this.comboBox_Step = new System.Windows.Forms.ComboBox();
            this.cb_alpha_speed = new System.Windows.Forms.ComboBox();
            this.lb_alpha_speed = new System.Windows.Forms.Label();
            this.bt_alpha_speed = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRotateLeft
            // 
            this.btnRotateLeft.Font = new System.Drawing.Font("宋体", 44F);
            this.btnRotateLeft.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.btnRotateLeft.Location = new System.Drawing.Point(166, 231);
            this.btnRotateLeft.Name = "btnRotateLeft";
            this.btnRotateLeft.Size = new System.Drawing.Size(70, 70);
            this.btnRotateLeft.TabIndex = 9;
            this.btnRotateLeft.Text = "▶";
            this.btnRotateLeft.UseVisualStyleBackColor = true;
            this.btnRotateLeft.Click += new System.EventHandler(this.btnRotateLeft_Click);
            // 
            // btnSwitchMode
            // 
            this.btnSwitchMode.Font = new System.Drawing.Font("宋体", 20F);
            this.btnSwitchMode.Location = new System.Drawing.Point(16, 46);
            this.btnSwitchMode.Name = "btnSwitchMode";
            this.btnSwitchMode.Size = new System.Drawing.Size(88, 41);
            this.btnSwitchMode.TabIndex = 6;
            this.btnSwitchMode.Text = "自动";
            this.btnSwitchMode.UseVisualStyleBackColor = true;
            this.btnSwitchMode.Visible = false;
            this.btnSwitchMode.Click += new System.EventHandler(this.btnSwitchMode_Click);
            // 
            // btnRotateRight
            // 
            this.btnRotateRight.Font = new System.Drawing.Font("宋体", 44F);
            this.btnRotateRight.Location = new System.Drawing.Point(14, 231);
            this.btnRotateRight.Name = "btnRotateRight";
            this.btnRotateRight.Size = new System.Drawing.Size(70, 70);
            this.btnRotateRight.TabIndex = 10;
            this.btnRotateRight.Text = "◀";
            this.btnRotateRight.UseVisualStyleBackColor = true;
            this.btnRotateRight.Click += new System.EventHandler(this.btnRotateRight_Click);
            // 
            // btnForward
            // 
            this.btnForward.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnForward.Font = new System.Drawing.Font("宋体", 34F);
            this.btnForward.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnForward.Location = new System.Drawing.Point(90, 191);
            this.btnForward.Name = "btnForward";
            this.btnForward.Size = new System.Drawing.Size(70, 70);
            this.btnForward.TabIndex = 7;
            this.btnForward.Text = "▲";
            this.btnForward.UseVisualStyleBackColor = true;
            this.btnForward.Click += new System.EventHandler(this.btnForward_Click);
            // 
            // btnBackward
            // 
            this.btnBackward.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btnBackward.Font = new System.Drawing.Font("宋体", 40F);
            this.btnBackward.ImageAlign = System.Drawing.ContentAlignment.BottomLeft;
            this.btnBackward.Location = new System.Drawing.Point(90, 267);
            this.btnBackward.Name = "btnBackward";
            this.btnBackward.Size = new System.Drawing.Size(70, 70);
            this.btnBackward.TabIndex = 11;
            this.btnBackward.Text = "▼";
            this.btnBackward.UseVisualStyleBackColor = true;
            this.btnBackward.Click += new System.EventHandler(this.btnBackward_Click);
            // 
            // textBox_speed
            // 
            this.textBox_speed.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox_speed.Location = new System.Drawing.Point(110, 94);
            this.textBox_speed.Name = "textBox_speed";
            this.textBox_speed.Size = new System.Drawing.Size(64, 35);
            this.textBox_speed.TabIndex = 12;
            this.textBox_speed.Text = "0";
            this.textBox_speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox_angle
            // 
            this.textBox_angle.Font = new System.Drawing.Font("宋体", 18F);
            this.textBox_angle.Location = new System.Drawing.Point(110, 138);
            this.textBox_angle.Name = "textBox_angle";
            this.textBox_angle.Size = new System.Drawing.Size(64, 35);
            this.textBox_angle.TabIndex = 13;
            this.textBox_angle.Text = "0";
            this.textBox_angle.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 20F);
            this.label1.Location = new System.Drawing.Point(11, 100);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 27);
            this.label1.TabIndex = 14;
            this.label1.Text = "线速度";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 20F);
            this.label2.Location = new System.Drawing.Point(11, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 27);
            this.label2.TabIndex = 15;
            this.label2.Text = "角速度";
            // 
            // button_Stop_speed
            // 
            this.button_Stop_speed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Stop_speed.Font = new System.Drawing.Font("宋体", 20F);
            this.button_Stop_speed.Location = new System.Drawing.Point(180, 94);
            this.button_Stop_speed.Name = "button_Stop_speed";
            this.button_Stop_speed.Size = new System.Drawing.Size(42, 38);
            this.button_Stop_speed.TabIndex = 16;
            this.button_Stop_speed.Text = "■";
            this.button_Stop_speed.UseVisualStyleBackColor = true;
            this.button_Stop_speed.Click += new System.EventHandler(this.button_Stop_speed_Click);
            // 
            // button_Stop_angle
            // 
            this.button_Stop_angle.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.button_Stop_angle.Font = new System.Drawing.Font("宋体", 20F);
            this.button_Stop_angle.Location = new System.Drawing.Point(180, 137);
            this.button_Stop_angle.Name = "button_Stop_angle";
            this.button_Stop_angle.Size = new System.Drawing.Size(42, 40);
            this.button_Stop_angle.TabIndex = 17;
            this.button_Stop_angle.Text = "■";
            this.button_Stop_angle.UseVisualStyleBackColor = true;
            this.button_Stop_angle.Click += new System.EventHandler(this.button_Stop_angle_Click);
            // 
            // comboBox_Step
            // 
            this.comboBox_Step.Font = new System.Drawing.Font("宋体", 20F);
            this.comboBox_Step.FormattingEnabled = true;
            this.comboBox_Step.Items.AddRange(new object[] {
            "0.01",
            "0.1",
            "1"});
            this.comboBox_Step.Location = new System.Drawing.Point(110, 49);
            this.comboBox_Step.Name = "comboBox_Step";
            this.comboBox_Step.Size = new System.Drawing.Size(112, 35);
            this.comboBox_Step.TabIndex = 18;
            this.comboBox_Step.Text = "0.1";
            // 
            // cb_alpha_speed
            // 
            this.cb_alpha_speed.Font = new System.Drawing.Font("宋体", 20F);
            this.cb_alpha_speed.FormattingEnabled = true;
            this.cb_alpha_speed.Items.AddRange(new object[] {
            "0.1",
            "0.2",
            "0.5"});
            this.cb_alpha_speed.Location = new System.Drawing.Point(83, 343);
            this.cb_alpha_speed.Name = "cb_alpha_speed";
            this.cb_alpha_speed.Size = new System.Drawing.Size(112, 35);
            this.cb_alpha_speed.TabIndex = 19;
            this.cb_alpha_speed.Text = "0.01";
            // 
            // lb_alpha_speed
            // 
            this.lb_alpha_speed.AutoSize = true;
            this.lb_alpha_speed.Font = new System.Drawing.Font("宋体", 20F);
            this.lb_alpha_speed.Location = new System.Drawing.Point(11, 346);
            this.lb_alpha_speed.Name = "lb_alpha_speed";
            this.lb_alpha_speed.Size = new System.Drawing.Size(66, 27);
            this.lb_alpha_speed.TabIndex = 20;
            this.lb_alpha_speed.Text = "速度";
            // 
            // bt_alpha_speed
            // 
            this.bt_alpha_speed.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.bt_alpha_speed.Font = new System.Drawing.Font("宋体", 20F);
            this.bt_alpha_speed.Location = new System.Drawing.Point(201, 339);
            this.bt_alpha_speed.Name = "bt_alpha_speed";
            this.bt_alpha_speed.Size = new System.Drawing.Size(42, 40);
            this.bt_alpha_speed.TabIndex = 21;
            this.bt_alpha_speed.Text = "■";
            this.bt_alpha_speed.UseVisualStyleBackColor = true;
            this.bt_alpha_speed.Click += new System.EventHandler(this.bt_alpha_speed_Click);
            // 
            // SysControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.bt_alpha_speed);
            this.Controls.Add(this.lb_alpha_speed);
            this.Controls.Add(this.cb_alpha_speed);
            this.Controls.Add(this.comboBox_Step);
            this.Controls.Add(this.button_Stop_angle);
            this.Controls.Add(this.button_Stop_speed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_angle);
            this.Controls.Add(this.textBox_speed);
            this.Controls.Add(this.btnBackward);
            this.Controls.Add(this.btnRotateRight);
            this.Controls.Add(this.btnRotateLeft);
            this.Controls.Add(this.btnForward);
            this.Controls.Add(this.btnSwitchMode);
            this.Name = "SysControlPanel";
            this.Size = new System.Drawing.Size(250, 382);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRotateRight;
        private System.Windows.Forms.Button btnRotateLeft;
        private System.Windows.Forms.Button btnForward;
        private System.Windows.Forms.Button btnSwitchMode;
        private System.Windows.Forms.Button btnBackward;
        private System.Windows.Forms.TextBox textBox_speed;
        private System.Windows.Forms.TextBox textBox_angle;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button_Stop_speed;
        private System.Windows.Forms.Button button_Stop_angle;
        private System.Windows.Forms.ComboBox comboBox_Step;
        private System.Windows.Forms.ComboBox cb_alpha_speed;
        private System.Windows.Forms.Label lb_alpha_speed;
        private System.Windows.Forms.Button bt_alpha_speed;
    }
}