namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    partial class EditUser
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
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.true_Name = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBox_authority = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.passWord = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.userName = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_NowStatus = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lastLoginTime = new System.Windows.Forms.DateTimePicker();
            this.tryLoginTime = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.checkPassword = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Clear.Location = new System.Drawing.Point(40, 344);
            this.btn_Clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(125, 48);
            this.btn_Clear.TabIndex = 10;
            this.btn_Clear.Text = "清空";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Submit.Location = new System.Drawing.Point(248, 344);
            this.btn_Submit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(125, 48);
            this.btn_Submit.TabIndex = 11;
            this.btn_Submit.Text = "确定";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // true_Name
            // 
            this.true_Name.Font = new System.Drawing.Font("宋体", 12F);
            this.true_Name.Location = new System.Drawing.Point(165, 205);
            this.true_Name.Margin = new System.Windows.Forms.Padding(4);
            this.true_Name.Name = "true_Name";
            this.true_Name.Size = new System.Drawing.Size(193, 26);
            this.true_Name.TabIndex = 3;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F);
            this.label22.Location = new System.Drawing.Point(69, 208);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 16);
            this.label22.TabIndex = 68;
            this.label22.Text = "真实姓名：";
            // 
            // comboBox_authority
            // 
            this.comboBox_authority.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBox_authority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_authority.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBox_authority.FormattingEnabled = true;
            this.comboBox_authority.Items.AddRange(new object[] {
            "管理员",
            "检查员",
            "质检员",
            "查询员",
            "超级质检员"});
            this.comboBox_authority.Location = new System.Drawing.Point(165, 250);
            this.comboBox_authority.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_authority.Name = "comboBox_authority";
            this.comboBox_authority.Size = new System.Drawing.Size(193, 24);
            this.comboBox_authority.TabIndex = 9;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 12F);
            this.label21.Location = new System.Drawing.Point(76, 250);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 16);
            this.label21.TabIndex = 66;
            this.label21.Text = "权限：";
            // 
            // passWord
            // 
            this.passWord.Font = new System.Drawing.Font("宋体", 12F);
            this.passWord.Location = new System.Drawing.Point(168, 77);
            this.passWord.Margin = new System.Windows.Forms.Padding(4);
            this.passWord.Name = "passWord";
            this.passWord.PasswordChar = '*';
            this.passWord.Size = new System.Drawing.Size(193, 26);
            this.passWord.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F);
            this.label19.Location = new System.Drawing.Point(76, 80);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 16);
            this.label19.TabIndex = 62;
            this.label19.Text = "密码：";
            // 
            // userName
            // 
            this.userName.Enabled = false;
            this.userName.Font = new System.Drawing.Font("宋体", 12F);
            this.userName.Location = new System.Drawing.Point(168, 31);
            this.userName.Margin = new System.Windows.Forms.Padding(4);
            this.userName.Name = "userName";
            this.userName.Size = new System.Drawing.Size(193, 26);
            this.userName.TabIndex = 0;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F);
            this.label20.Location = new System.Drawing.Point(76, 34);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 16);
            this.label20.TabIndex = 60;
            this.label20.Text = "用户名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 12F);
            this.label1.Location = new System.Drawing.Point(69, 163);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 16);
            this.label1.TabIndex = 72;
            this.label1.Text = "当前状态：";
            // 
            // comboBox_NowStatus
            // 
            this.comboBox_NowStatus.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBox_NowStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_NowStatus.Font = new System.Drawing.Font("宋体", 12F);
            this.comboBox_NowStatus.FormattingEnabled = true;
            this.comboBox_NowStatus.Items.AddRange(new object[] {
            "有效",
            "无效"});
            this.comboBox_NowStatus.Location = new System.Drawing.Point(165, 160);
            this.comboBox_NowStatus.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_NowStatus.Name = "comboBox_NowStatus";
            this.comboBox_NowStatus.Size = new System.Drawing.Size(193, 24);
            this.comboBox_NowStatus.TabIndex = 2;
            this.comboBox_NowStatus.SelectedIndexChanged += new System.EventHandler(this.comboBox_NowStatus_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 12F);
            this.label2.Location = new System.Drawing.Point(13, 324);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 74;
            this.label2.Text = "最近成功登陆时间：";
            this.label2.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 12F);
            this.label3.Location = new System.Drawing.Point(37, 324);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(120, 16);
            this.label3.TabIndex = 75;
            this.label3.Text = "尝试登录时间：";
            this.label3.Visible = false;
            // 
            // lastLoginTime
            // 
            this.lastLoginTime.Font = new System.Drawing.Font("宋体", 12F);
            this.lastLoginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.lastLoginTime.Location = new System.Drawing.Point(180, 314);
            this.lastLoginTime.Name = "lastLoginTime";
            this.lastLoginTime.Size = new System.Drawing.Size(193, 26);
            this.lastLoginTime.TabIndex = 4;
            this.lastLoginTime.Visible = false;
            // 
            // tryLoginTime
            // 
            this.tryLoginTime.Font = new System.Drawing.Font("宋体", 12F);
            this.tryLoginTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.tryLoginTime.Location = new System.Drawing.Point(164, 317);
            this.tryLoginTime.Name = "tryLoginTime";
            this.tryLoginTime.Size = new System.Drawing.Size(193, 26);
            this.tryLoginTime.TabIndex = 6;
            this.tryLoginTime.Visible = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7F);
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(151, 77);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 10);
            this.label4.TabIndex = 76;
            this.label4.Text = "*";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("宋体", 7F);
            this.label5.ForeColor = System.Drawing.Color.DarkRed;
            this.label5.Location = new System.Drawing.Point(151, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(10, 10);
            this.label5.TabIndex = 77;
            this.label5.Text = "*";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("宋体", 7F);
            this.label6.ForeColor = System.Drawing.Color.DarkRed;
            this.label6.Location = new System.Drawing.Point(148, 250);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(10, 10);
            this.label6.TabIndex = 78;
            this.label6.Text = "*";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("宋体", 12F);
            this.label7.Location = new System.Drawing.Point(76, 120);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(88, 16);
            this.label7.TabIndex = 79;
            this.label7.Text = "确认密码：";
            // 
            // checkPassword
            // 
            this.checkPassword.Font = new System.Drawing.Font("宋体", 12F);
            this.checkPassword.Location = new System.Drawing.Point(168, 120);
            this.checkPassword.Margin = new System.Windows.Forms.Padding(4);
            this.checkPassword.Name = "checkPassword";
            this.checkPassword.PasswordChar = '*';
            this.checkPassword.Size = new System.Drawing.Size(193, 26);
            this.checkPassword.TabIndex = 80;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("宋体", 7F);
            this.label8.ForeColor = System.Drawing.Color.DarkRed;
            this.label8.Location = new System.Drawing.Point(151, 163);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(10, 10);
            this.label8.TabIndex = 81;
            this.label8.Text = "*";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("宋体", 7F);
            this.label9.ForeColor = System.Drawing.Color.DarkRed;
            this.label9.Location = new System.Drawing.Point(151, 120);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(10, 10);
            this.label9.TabIndex = 82;
            this.label9.Text = "*";
            // 
            // EditUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(462, 481);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.checkPassword);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.true_Name);
            this.Controls.Add(this.tryLoginTime);
            this.Controls.Add(this.lastLoginTime);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_NowStatus);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_Clear);
            this.Controls.Add(this.btn_Submit);
            this.Controls.Add(this.label22);
            this.Controls.Add(this.comboBox_authority);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.passWord);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.userName);
            this.Controls.Add(this.label20);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EditUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "编辑用户";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.TextBox true_Name;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboBox_authority;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox userName;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_NowStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker lastLoginTime;
        private System.Windows.Forms.DateTimePicker tryLoginTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox checkPassword;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
    }
}