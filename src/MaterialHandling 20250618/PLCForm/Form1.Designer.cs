namespace PLCForm
{
    partial class Form1
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.rtb_Textshow = new System.Windows.Forms.RichTextBox();
            this.btn_ClearTextshow = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.tb_PLCstate = new System.Windows.Forms.TextBox();
            this.btn_PLCopen = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // rtb_Textshow
            // 
            this.rtb_Textshow.Location = new System.Drawing.Point(15, 139);
            this.rtb_Textshow.Name = "rtb_Textshow";
            this.rtb_Textshow.Size = new System.Drawing.Size(180, 280);
            this.rtb_Textshow.TabIndex = 1;
            this.rtb_Textshow.Text = "";
            // 
            // btn_ClearTextshow
            // 
            this.btn_ClearTextshow.Location = new System.Drawing.Point(213, 139);
            this.btn_ClearTextshow.Name = "btn_ClearTextshow";
            this.btn_ClearTextshow.Size = new System.Drawing.Size(75, 23);
            this.btn_ClearTextshow.TabIndex = 7;
            this.btn_ClearTextshow.Text = "清空";
            this.btn_ClearTextshow.UseVisualStyleBackColor = true;
            this.btn_ClearTextshow.Click += new System.EventHandler(this.btn_ClearTextshow_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(83, 12);
            this.label5.TabIndex = 14;
            this.label5.Text = "PLC连接状态：";
            // 
            // tb_PLCstate
            // 
            this.tb_PLCstate.Location = new System.Drawing.Point(101, 6);
            this.tb_PLCstate.Name = "tb_PLCstate";
            this.tb_PLCstate.Size = new System.Drawing.Size(36, 21);
            this.tb_PLCstate.TabIndex = 15;
            this.tb_PLCstate.Text = "false";
            // 
            // btn_PLCopen
            // 
            this.btn_PLCopen.Location = new System.Drawing.Point(213, 218);
            this.btn_PLCopen.Name = "btn_PLCopen";
            this.btn_PLCopen.Size = new System.Drawing.Size(75, 23);
            this.btn_PLCopen.TabIndex = 17;
            this.btn_PLCopen.Text = "开启PLC";
            this.btn_PLCopen.UseVisualStyleBackColor = true;
            this.btn_PLCopen.Click += new System.EventHandler(this.btn_PLCopen_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(298, 431);
            this.Controls.Add(this.btn_PLCopen);
            this.Controls.Add(this.tb_PLCstate);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btn_ClearTextshow);
            this.Controls.Add(this.rtb_Textshow);
            this.Name = "Form1";
            this.Text = "MyPLC";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox rtb_Textshow;
        private System.Windows.Forms.Button btn_ClearTextshow;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tb_PLCstate;
        private System.Windows.Forms.Button btn_PLCopen;
    }
}

