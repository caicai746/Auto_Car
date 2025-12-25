using System.Drawing;

namespace WindowsFormsApp1.UserControls
{
    partial class MotorControl
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl_MotorName = new System.Windows.Forms.Label();
            this.txt_Speed = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.btn_Forward = new System.Windows.Forms.Button();
            this.txt_SpeedSetting = new System.Windows.Forms.TextBox();
            this.btn_SpeedSetting = new System.Windows.Forms.Button();
            this.lbl_SpeedUnit = new System.Windows.Forms.Label();
            this.btn_Backward = new System.Windows.Forms.Button();
            this.picbox_MotorCurrentState = new System.Windows.Forms.PictureBox();
            this.circleLbl_MotorCode = new CircleLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picbox_MotorCurrentState)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(4)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.picbox_MotorCurrentState);
            this.panel1.Controls.Add(this.lbl_MotorName);
            this.panel1.Location = new System.Drawing.Point(35, 9);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(247, 39);
            this.panel1.TabIndex = 1;
            // 
            // lbl_MotorName
            // 
            this.lbl_MotorName.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_MotorName.Location = new System.Drawing.Point(8, 0);
            this.lbl_MotorName.Name = "lbl_MotorName";
            this.lbl_MotorName.Size = new System.Drawing.Size(120, 39);
            this.lbl_MotorName.TabIndex = 0;
            this.lbl_MotorName.Text = "皮带电机";
            this.lbl_MotorName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txt_Speed
            // 
            this.txt_Speed.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txt_Speed.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_Speed.ForeColor = System.Drawing.Color.White;
            this.txt_Speed.Location = new System.Drawing.Point(252, 16);
            this.txt_Speed.Name = "txt_Speed";
            this.txt_Speed.ReadOnly = true;
            this.txt_Speed.Size = new System.Drawing.Size(54, 26);
            this.txt_Speed.TabIndex = 3;
            this.txt_Speed.Text = "5.5";
            this.txt_Speed.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // btn_Forward
            // 
            this.btn_Forward.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Forward.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Forward.Location = new System.Drawing.Point(48, 54);
            this.btn_Forward.Name = "btn_Forward";
            this.btn_Forward.Size = new System.Drawing.Size(54, 31);
            this.btn_Forward.TabIndex = 5;
            this.btn_Forward.Text = "正转";
            this.btn_Forward.UseVisualStyleBackColor = false;
            // 
            // txt_SpeedSetting
            // 
            this.txt_SpeedSetting.BackColor = System.Drawing.SystemColors.HotTrack;
            this.txt_SpeedSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_SpeedSetting.ForeColor = System.Drawing.Color.White;
            this.txt_SpeedSetting.Location = new System.Drawing.Point(252, 58);
            this.txt_SpeedSetting.Name = "txt_SpeedSetting";
            this.txt_SpeedSetting.Size = new System.Drawing.Size(54, 26);
            this.txt_SpeedSetting.TabIndex = 7;
            this.txt_SpeedSetting.Text = "5.5";
            this.txt_SpeedSetting.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btn_SpeedSetting
            // 
            this.btn_SpeedSetting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btn_SpeedSetting.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.btn_SpeedSetting.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_SpeedSetting.Location = new System.Drawing.Point(170, 54);
            this.btn_SpeedSetting.Name = "btn_SpeedSetting";
            this.btn_SpeedSetting.Size = new System.Drawing.Size(76, 31);
            this.btn_SpeedSetting.TabIndex = 8;
            this.btn_SpeedSetting.Text = "设置速度";
            this.btn_SpeedSetting.UseVisualStyleBackColor = false;
            // 
            // lbl_SpeedUnit
            // 
            this.lbl_SpeedUnit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_SpeedUnit.Location = new System.Drawing.Point(308, 59);
            this.lbl_SpeedUnit.Name = "lbl_SpeedUnit";
            this.lbl_SpeedUnit.Size = new System.Drawing.Size(39, 23);
            this.lbl_SpeedUnit.TabIndex = 9;
            this.lbl_SpeedUnit.Text = "m/s";
            this.lbl_SpeedUnit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btn_Backward
            // 
            this.btn_Backward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Backward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Backward.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Backward.Location = new System.Drawing.Point(108, 54);
            this.btn_Backward.Name = "btn_Backward";
            this.btn_Backward.Size = new System.Drawing.Size(56, 31);
            this.btn_Backward.TabIndex = 6;
            this.btn_Backward.Text = "反转";
            this.btn_Backward.UseVisualStyleBackColor = false;
            // 
            // picbox_MotorCurrentState
            // 
            this.picbox_MotorCurrentState.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.picbox_MotorCurrentState.Location = new System.Drawing.Point(134, 5);
            this.picbox_MotorCurrentState.Name = "picbox_MotorCurrentState";
            this.picbox_MotorCurrentState.Size = new System.Drawing.Size(57, 29);
            this.picbox_MotorCurrentState.TabIndex = 1;
            this.picbox_MotorCurrentState.TabStop = false;
            // 
            // circleLbl_MotorCode
            // 
            this.circleLbl_MotorCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(246)))), ((int)(((byte)(246)))), ((int)(((byte)(4)))));
            this.circleLbl_MotorCode.BorderColor = System.Drawing.Color.Black;
            this.circleLbl_MotorCode.BorderWidth = 2;
            this.circleLbl_MotorCode.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Bold);
            this.circleLbl_MotorCode.Location = new System.Drawing.Point(1, 1);
            this.circleLbl_MotorCode.Name = "circleLbl_MotorCode";
            this.circleLbl_MotorCode.Size = new System.Drawing.Size(56, 56);
            this.circleLbl_MotorCode.TabIndex = 2;
            this.circleLbl_MotorCode.Text = "M1";
            this.circleLbl_MotorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MotorControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.lbl_SpeedUnit);
            this.Controls.Add(this.btn_SpeedSetting);
            this.Controls.Add(this.txt_SpeedSetting);
            this.Controls.Add(this.btn_Backward);
            this.Controls.Add(this.btn_Forward);
            this.Controls.Add(this.txt_Speed);
            this.Controls.Add(this.circleLbl_MotorCode);
            this.Controls.Add(this.panel1);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(357, 94);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picbox_MotorCurrentState)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_MotorName;
        private CircleLabel circleLbl_MotorCode;
        private System.Windows.Forms.PictureBox picbox_MotorCurrentState;
        private System.Windows.Forms.TextBox txt_Speed;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_Forward;
        private System.Windows.Forms.Button btn_Backward;
        private System.Windows.Forms.TextBox txt_SpeedSetting;
        private System.Windows.Forms.Button btn_SpeedSetting;
        private System.Windows.Forms.Label lbl_SpeedUnit;
    }
}
