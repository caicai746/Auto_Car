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
            this.btn_Activate = new System.Windows.Forms.Button();
            this.btn_Forward = new System.Windows.Forms.Button();
            this.btn_Backward = new System.Windows.Forms.Button();
            this.lbl_MotorName = new System.Windows.Forms.Label();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.circleLbl_MotorCode = new CircleLabel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.btn_Activate);
            this.panel1.Controls.Add(this.btn_Forward);
            this.panel1.Controls.Add(this.btn_Backward);
            this.panel1.Controls.Add(this.lbl_MotorName);
            this.panel1.Location = new System.Drawing.Point(33, 7);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(244, 39);
            this.panel1.TabIndex = 1;
            // 
            // btn_Activate
            // 
            this.btn_Activate.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Activate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Activate.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Activate.Location = new System.Drawing.Point(193, 7);
            this.btn_Activate.Name = "btn_Activate";
            this.btn_Activate.Size = new System.Drawing.Size(47, 24);
            this.btn_Activate.TabIndex = 7;
            this.btn_Activate.Text = "启动";
            this.btn_Activate.UseVisualStyleBackColor = false;
            this.btn_Activate.Click += new System.EventHandler(this.btn_Activate_Click);
            // 
            // btn_Forward
            // 
            this.btn_Forward.BackColor = System.Drawing.Color.Gainsboro;
            this.btn_Forward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Forward.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Forward.Location = new System.Drawing.Point(94, 7);
            this.btn_Forward.Name = "btn_Forward";
            this.btn_Forward.Size = new System.Drawing.Size(47, 24);
            this.btn_Forward.TabIndex = 5;
            this.btn_Forward.Text = "正转";
            this.btn_Forward.UseVisualStyleBackColor = false;
            this.btn_Forward.Click += new System.EventHandler(this.btn_Forward_Click_1);
            // 
            // btn_Backward
            // 
            this.btn_Backward.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.btn_Backward.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_Backward.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Backward.Location = new System.Drawing.Point(144, 7);
            this.btn_Backward.Name = "btn_Backward";
            this.btn_Backward.Size = new System.Drawing.Size(47, 24);
            this.btn_Backward.TabIndex = 6;
            this.btn_Backward.Text = "反转";
            this.btn_Backward.UseVisualStyleBackColor = false;
            // 
            // lbl_MotorName
            // 
            this.lbl_MotorName.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbl_MotorName.Location = new System.Drawing.Point(-4, -1);
            this.lbl_MotorName.Name = "lbl_MotorName";
            this.lbl_MotorName.Size = new System.Drawing.Size(120, 39);
            this.lbl_MotorName.TabIndex = 0;
            this.lbl_MotorName.Text = "皮带电机";
            this.lbl_MotorName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // circleLbl_MotorCode
            // 
            this.circleLbl_MotorCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.circleLbl_MotorCode.BorderColor = System.Drawing.Color.Black;
            this.circleLbl_MotorCode.BorderWidth = 2;
            this.circleLbl_MotorCode.Font = new System.Drawing.Font("宋体", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.circleLbl_MotorCode.Location = new System.Drawing.Point(-1, -1);
            this.circleLbl_MotorCode.Name = "circleLbl_MotorCode";
            this.circleLbl_MotorCode.Size = new System.Drawing.Size(56, 56);
            this.circleLbl_MotorCode.TabIndex = 2;
            this.circleLbl_MotorCode.Text = "M1";
            this.circleLbl_MotorCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // MotorControl
            // 
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.circleLbl_MotorCode);
            this.Controls.Add(this.panel1);
            this.Name = "MotorControl";
            this.Size = new System.Drawing.Size(277, 52);
            this.Load += new System.EventHandler(this.MotorControl_Load);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl_MotorName;
        private CircleLabel circleLbl_MotorCode;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.Button btn_Forward;
        private System.Windows.Forms.Button btn_Backward;
        private System.Windows.Forms.Button btn_Activate;
    }
}
