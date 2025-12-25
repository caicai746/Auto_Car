
namespace MaterialHandling.MaterialHandlingUI.UIFrame.UserControls
{
    partial class UserControl1
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
            this.panel_carriage = new System.Windows.Forms.Panel();
            this.btnStart = new System.Windows.Forms.Button();
            this.textBox_carriage_L = new System.Windows.Forms.TextBox();
            this.label_l = new System.Windows.Forms.Label();
            this.button_carriage_L = new System.Windows.Forms.Button();
            this.button_carriage_W = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_carriage_W = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // panel_carriage
            // 
            this.panel_carriage.Location = new System.Drawing.Point(12, 314);
            this.panel_carriage.Name = "panel_carriage";
            this.panel_carriage.Size = new System.Drawing.Size(300, 200);
            this.panel_carriage.TabIndex = 0;
            this.panel_carriage.Paint += new System.Windows.Forms.PaintEventHandler(this.panel_carriage_Paint);
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("黑体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(451, 67);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(75, 23);
            this.btnStart.TabIndex = 1;
            this.btnStart.Text = "开始运行";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // textBox_carriage_L
            // 
            this.textBox_carriage_L.Location = new System.Drawing.Point(377, 13);
            this.textBox_carriage_L.Name = "textBox_carriage_L";
            this.textBox_carriage_L.Size = new System.Drawing.Size(100, 21);
            this.textBox_carriage_L.TabIndex = 2;
            // 
            // label_l
            // 
            this.label_l.Location = new System.Drawing.Point(318, 12);
            this.label_l.Name = "label_l";
            this.label_l.Size = new System.Drawing.Size(53, 21);
            this.label_l.TabIndex = 3;
            this.label_l.Text = "车厢长度";
            this.label_l.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button_carriage_L
            // 
            this.button_carriage_L.Location = new System.Drawing.Point(483, 13);
            this.button_carriage_L.Name = "button_carriage_L";
            this.button_carriage_L.Size = new System.Drawing.Size(39, 20);
            this.button_carriage_L.TabIndex = 4;
            this.button_carriage_L.Text = "Set";
            this.button_carriage_L.UseVisualStyleBackColor = true;
            this.button_carriage_L.Click += new System.EventHandler(this.button_carriage_L_Click);
            // 
            // button_carriage_W
            // 
            this.button_carriage_W.Location = new System.Drawing.Point(483, 40);
            this.button_carriage_W.Name = "button_carriage_W";
            this.button_carriage_W.Size = new System.Drawing.Size(39, 20);
            this.button_carriage_W.TabIndex = 7;
            this.button_carriage_W.Text = "Set";
            this.button_carriage_W.UseVisualStyleBackColor = true;
            this.button_carriage_W.Click += new System.EventHandler(this.button_carriage_W_Click);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(318, 39);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "车厢宽度";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_carriage_W
            // 
            this.textBox_carriage_W.Location = new System.Drawing.Point(377, 40);
            this.textBox_carriage_W.Name = "textBox_carriage_W";
            this.textBox_carriage_W.Size = new System.Drawing.Size(100, 21);
            this.textBox_carriage_W.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(12, 13);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(300, 299);
            this.panel2.TabIndex = 1;
            this.panel2.Visible = false;
            this.panel2.Paint += new System.Windows.Forms.PaintEventHandler(this.panel2_Paint);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(318, 193);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 21);
            this.label2.TabIndex = 9;
            this.label2.Text = "x";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(377, 194);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 21);
            this.textBox1.TabIndex = 8;
            this.textBox1.Text = "x";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(318, 220);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 21);
            this.label3.TabIndex = 11;
            this.label3.Text = "y";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(377, 221);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 10;
            this.textBox2.Text = "y";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(538, 526);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button_carriage_W);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_carriage_W);
            this.Controls.Add(this.button_carriage_L);
            this.Controls.Add(this.label_l);
            this.Controls.Add(this.textBox_carriage_L);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.panel_carriage);
            this.Name = "Form1";
            this.Text = "小车移动";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_carriage;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox textBox_carriage_L;
        private System.Windows.Forms.Label label_l;
        private System.Windows.Forms.Button button_carriage_L;
        private System.Windows.Forms.Button button_carriage_W;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_carriage_W;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox2;
    }
}

