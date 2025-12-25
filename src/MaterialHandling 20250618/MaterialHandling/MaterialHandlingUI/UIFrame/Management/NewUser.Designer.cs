namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    partial class NewUser
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.comboBox_userName = new System.Windows.Forms.ComboBox();
            this.btn_Clear = new System.Windows.Forms.Button();
            this.btn_Submit = new System.Windows.Forms.Button();
            this.true_Name = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.comboBox_authority = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.checkPassword = new System.Windows.Forms.TextBox();
            this.label18 = new System.Windows.Forms.Label();
            this.passWord = new System.Windows.Forms.TextBox();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.comboBox_userName);
            this.panel1.Controls.Add(this.btn_Clear);
            this.panel1.Controls.Add(this.btn_Submit);
            this.panel1.Controls.Add(this.true_Name);
            this.panel1.Controls.Add(this.label22);
            this.panel1.Controls.Add(this.comboBox_authority);
            this.panel1.Controls.Add(this.label21);
            this.panel1.Controls.Add(this.checkPassword);
            this.panel1.Controls.Add(this.label18);
            this.panel1.Controls.Add(this.passWord);
            this.panel1.Controls.Add(this.label19);
            this.panel1.Controls.Add(this.label20);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(361, 439);
            this.panel1.TabIndex = 0;
            // 
            // comboBox_userName
            // 
            this.comboBox_userName.FormattingEnabled = true;
            this.comboBox_userName.Location = new System.Drawing.Point(135, 35);
            this.comboBox_userName.Name = "comboBox_userName";
            this.comboBox_userName.Size = new System.Drawing.Size(177, 24);
            this.comboBox_userName.TabIndex = 0;
            this.comboBox_userName.DropDown += new System.EventHandler(this.userName_DropDown);
            this.comboBox_userName.TextUpdate += new System.EventHandler(this.comboBox_userName_TextUpdate);
            // 
            // btn_Clear
            // 
            this.btn_Clear.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Clear.Location = new System.Drawing.Point(13, 366);
            this.btn_Clear.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Clear.Name = "btn_Clear";
            this.btn_Clear.Size = new System.Drawing.Size(125, 48);
            this.btn_Clear.TabIndex = 5;
            this.btn_Clear.Text = "清空";
            this.btn_Clear.UseVisualStyleBackColor = true;
            this.btn_Clear.Click += new System.EventHandler(this.btn_Clear_Click);
            // 
            // btn_Submit
            // 
            this.btn_Submit.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btn_Submit.Location = new System.Drawing.Point(216, 366);
            this.btn_Submit.Margin = new System.Windows.Forms.Padding(4);
            this.btn_Submit.Name = "btn_Submit";
            this.btn_Submit.Size = new System.Drawing.Size(125, 48);
            this.btn_Submit.TabIndex = 6;
            this.btn_Submit.Text = "确定";
            this.btn_Submit.UseVisualStyleBackColor = true;
            this.btn_Submit.Click += new System.EventHandler(this.btn_Submit_Click);
            // 
            // true_Name
            // 
            this.true_Name.Location = new System.Drawing.Point(135, 295);
            this.true_Name.Margin = new System.Windows.Forms.Padding(4);
            this.true_Name.Name = "true_Name";
            this.true_Name.Size = new System.Drawing.Size(177, 26);
            this.true_Name.TabIndex = 4;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("宋体", 12F);
            this.label22.Location = new System.Drawing.Point(43, 298);
            this.label22.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(88, 16);
            this.label22.TabIndex = 56;
            this.label22.Text = "真实姓名：";
            // 
            // comboBox_authority
            // 
            this.comboBox_authority.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.comboBox_authority.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_authority.FormattingEnabled = true;
            this.comboBox_authority.Items.AddRange(new object[] {
            "管理员",
            "检查员",
            "质检员",
            "查询员",
            "超级质检员"});
            this.comboBox_authority.Location = new System.Drawing.Point(135, 232);
            this.comboBox_authority.Margin = new System.Windows.Forms.Padding(4);
            this.comboBox_authority.Name = "comboBox_authority";
            this.comboBox_authority.Size = new System.Drawing.Size(177, 24);
            this.comboBox_authority.TabIndex = 3;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("宋体", 12F);
            this.label21.Location = new System.Drawing.Point(43, 235);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(56, 16);
            this.label21.TabIndex = 54;
            this.label21.Text = "权限：";
            // 
            // checkPassword
            // 
            this.checkPassword.Location = new System.Drawing.Point(135, 165);
            this.checkPassword.Margin = new System.Windows.Forms.Padding(4);
            this.checkPassword.Name = "checkPassword";
            this.checkPassword.PasswordChar = '*';
            this.checkPassword.Size = new System.Drawing.Size(177, 26);
            this.checkPassword.TabIndex = 2;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 12F);
            this.label18.Location = new System.Drawing.Point(43, 168);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(88, 16);
            this.label18.TabIndex = 50;
            this.label18.Text = "确认密码：";
            // 
            // passWord
            // 
            this.passWord.Location = new System.Drawing.Point(135, 96);
            this.passWord.Margin = new System.Windows.Forms.Padding(4);
            this.passWord.Name = "passWord";
            this.passWord.PasswordChar = '*';
            this.passWord.Size = new System.Drawing.Size(177, 26);
            this.passWord.TabIndex = 1;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("宋体", 12F);
            this.label19.Location = new System.Drawing.Point(43, 99);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(56, 16);
            this.label19.TabIndex = 48;
            this.label19.Text = "密码：";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("宋体", 12F);
            this.label20.Location = new System.Drawing.Point(43, 38);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(72, 16);
            this.label20.TabIndex = 46;
            this.label20.Text = "用户名：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 7F);
            this.label1.ForeColor = System.Drawing.Color.DarkRed;
            this.label1.Location = new System.Drawing.Point(119, 35);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 10);
            this.label1.TabIndex = 57;
            this.label1.Text = "*";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 7F);
            this.label2.ForeColor = System.Drawing.Color.DarkRed;
            this.label2.Location = new System.Drawing.Point(119, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(10, 10);
            this.label2.TabIndex = 58;
            this.label2.Text = "*";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 7F);
            this.label3.ForeColor = System.Drawing.Color.DarkRed;
            this.label3.Location = new System.Drawing.Point(119, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(10, 10);
            this.label3.TabIndex = 59;
            this.label3.Text = "*";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("宋体", 7F);
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(119, 232);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(10, 10);
            this.label4.TabIndex = 60;
            this.label4.Text = "*";
            // 
            // NewUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(361, 439);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("宋体", 12F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NewUser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "新建用户";
            this.TopMost = true;
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox true_Name;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.ComboBox comboBox_authority;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.TextBox checkPassword;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox passWord;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btn_Clear;
        private System.Windows.Forms.Button btn_Submit;
        private System.Windows.Forms.ComboBox comboBox_userName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}