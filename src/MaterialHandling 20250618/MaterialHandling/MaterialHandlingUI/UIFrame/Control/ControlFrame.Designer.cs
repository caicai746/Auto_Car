
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    partial class ControlFrame
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
            if (disposing)
            {
                //_mapImage?.Dispose();
                //pictureBoxMap?.Dispose();
            }
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.Panel_Motor = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.Panel_MAP = new System.Windows.Forms.Panel();
            this.panel_CamToMaterial = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel_CAM1 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel_CAM2 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel_Sys = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.panel_Par = new System.Windows.Forms.Panel();
            this.panel_Message = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.Panel_Motor.SuspendLayout();
            this.Panel_MAP.SuspendLayout();
            this.panel_CAM1.SuspendLayout();
            this.panel_CAM2.SuspendLayout();
            this.panel_Sys.SuspendLayout();
            this.panel_Par.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1920, 43691);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 26F, System.Drawing.FontStyle.Bold);
            this.label1.Location = new System.Drawing.Point(834, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(159, 35);
            this.label1.TabIndex = 1;
            this.label1.Text = "操作界面";
            // 
            // Panel_Motor
            // 
            this.Panel_Motor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_Motor.Controls.Add(this.label4);
            this.Panel_Motor.Location = new System.Drawing.Point(5, 46);
            this.Panel_Motor.Name = "Panel_Motor";
            this.Panel_Motor.Size = new System.Drawing.Size(411, 888);
            this.Panel_Motor.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label4.Location = new System.Drawing.Point(3, 4);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(98, 21);
            this.label4.TabIndex = 1;
            this.label4.Text = "抓取装置";
            // 
            // Panel_MAP
            // 
            this.Panel_MAP.BackColor = System.Drawing.SystemColors.Control;
            this.Panel_MAP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Panel_MAP.Controls.Add(this.panel_CamToMaterial);
            this.Panel_MAP.Controls.Add(this.label2);
            this.Panel_MAP.Location = new System.Drawing.Point(422, 434);
            this.Panel_MAP.Name = "Panel_MAP";
            this.Panel_MAP.Size = new System.Drawing.Size(1500, 500);
            this.Panel_MAP.TabIndex = 5;
            // 
            // panel_CamToMaterial
            // 
            this.panel_CamToMaterial.Location = new System.Drawing.Point(1071, 5);
            this.panel_CamToMaterial.Margin = new System.Windows.Forms.Padding(2);
            this.panel_CamToMaterial.Name = "panel_CamToMaterial";
            this.panel_CamToMaterial.Size = new System.Drawing.Size(406, 267);
            this.panel_CamToMaterial.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "地图";
            // 
            // panel_CAM1
            // 
            this.panel_CAM1.BackColor = System.Drawing.SystemColors.Control;
            this.panel_CAM1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CAM1.Controls.Add(this.label5);
            this.panel_CAM1.Location = new System.Drawing.Point(1130, 161);
            this.panel_CAM1.Name = "panel_CAM1";
            this.panel_CAM1.Size = new System.Drawing.Size(382, 267);
            this.panel_CAM1.TabIndex = 6;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label5.Location = new System.Drawing.Point(3, 4);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 21);
            this.label5.TabIndex = 1;
            this.label5.Text = "摄像头1";
            // 
            // panel_CAM2
            // 
            this.panel_CAM2.BackColor = System.Drawing.SystemColors.Control;
            this.panel_CAM2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_CAM2.Controls.Add(this.label6);
            this.panel_CAM2.Location = new System.Drawing.Point(1518, 161);
            this.panel_CAM2.Name = "panel_CAM2";
            this.panel_CAM2.Size = new System.Drawing.Size(382, 267);
            this.panel_CAM2.TabIndex = 7;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label6.Location = new System.Drawing.Point(3, 5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(88, 21);
            this.label6.TabIndex = 1;
            this.label6.Text = "摄像头2";
            // 
            // panel_Sys
            // 
            this.panel_Sys.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Sys.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Sys.Controls.Add(this.label3);
            this.panel_Sys.Location = new System.Drawing.Point(422, 46);
            this.panel_Sys.Name = "panel_Sys";
            this.panel_Sys.Size = new System.Drawing.Size(250, 382);
            this.panel_Sys.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "控制";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label7.Location = new System.Drawing.Point(3, 5);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "系统";
            // 
            // panel_Par
            // 
            this.panel_Par.BackColor = System.Drawing.SystemColors.Control;
            this.panel_Par.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_Par.Controls.Add(this.label7);
            this.panel_Par.Location = new System.Drawing.Point(678, 46);
            this.panel_Par.Name = "panel_Par";
            this.panel_Par.Size = new System.Drawing.Size(446, 382);
            this.panel_Par.TabIndex = 8;
            // 
            // panel_Message
            // 
            this.panel_Message.Location = new System.Drawing.Point(1130, 46);
            this.panel_Message.Margin = new System.Windows.Forms.Padding(0);
            this.panel_Message.Name = "panel_Message";
            this.panel_Message.Size = new System.Drawing.Size(770, 112);
            this.panel_Message.TabIndex = 9;
            // 
            // ControlFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1920, 960);
            this.Controls.Add(this.panel_Message);
            this.Controls.Add(this.panel_Par);
            this.Controls.Add(this.panel_Sys);
            this.Controls.Add(this.panel_CAM2);
            this.Controls.Add(this.panel_CAM1);
            this.Controls.Add(this.Panel_MAP);
            this.Controls.Add(this.Panel_Motor);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ControlFrame";
            this.ShowInTaskbar = false;
            this.Text = "ControlFrame";
            this.Load += new System.EventHandler(this.ControlFrame_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.Panel_Motor.ResumeLayout(false);
            this.Panel_Motor.PerformLayout();
            this.Panel_MAP.ResumeLayout(false);
            this.Panel_MAP.PerformLayout();
            this.panel_CAM1.ResumeLayout(false);
            this.panel_CAM1.PerformLayout();
            this.panel_CAM2.ResumeLayout(false);
            this.panel_CAM2.PerformLayout();
            this.panel_Sys.ResumeLayout(false);
            this.panel_Sys.PerformLayout();
            this.panel_Par.ResumeLayout(false);
            this.panel_Par.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel Panel_Motor;
        private System.Windows.Forms.Panel Panel_MAP;
        private System.Windows.Forms.Panel panel_CAM1;
        private System.Windows.Forms.Panel panel_CAM2;
        private System.Windows.Forms.Panel panel_Sys;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Panel panel_Par;
        private System.Windows.Forms.Panel panel_Message;
        private System.Windows.Forms.Panel panel_CamToMaterial;
    }
}