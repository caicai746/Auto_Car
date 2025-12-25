
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    partial class MapVehiclePanel
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
                _mapImage?.Dispose();

            }
            if (disposing && (components != null))
            {
                components.Dispose();
                pictureBoxMap?.Dispose();
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
            this.components = new System.ComponentModel.Container();
            this.pictureBoxMap = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.fontDialog1 = new System.Windows.Forms.FontDialog();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.tb_calibrationValue = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_init_rect = new System.Windows.Forms.Button();
            this.comboBox_map = new System.Windows.Forms.ComboBox();
            this.btn_next_rect = new System.Windows.Forms.Button();
            this.cb_car_visiable = new System.Windows.Forms.CheckBox();
            this.bt_StartMove = new System.Windows.Forms.Button();
            this.tb_DistanceToRight = new System.Windows.Forms.TextBox();
            this.tb_DistanceToTop = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_old_x = new System.Windows.Forms.TextBox();
            this.textBox_new_x = new System.Windows.Forms.TextBox();
            this.textBox_old_y = new System.Windows.Forms.TextBox();
            this.textBox_new_y = new System.Windows.Forms.TextBox();
            this.textBox_new_theta = new System.Windows.Forms.TextBox();
            this.textBox_old_theta = new System.Windows.Forms.TextBox();
            this.cb_IsMoveToTopLeft = new System.Windows.Forms.CheckBox();
            this.bt_StopMotion = new System.Windows.Forms.Button();
            this.ck_CameraDistance = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxMap
            // 
            this.pictureBoxMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBoxMap.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxMap.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBoxMap.Name = "pictureBoxMap";
            this.pictureBoxMap.Size = new System.Drawing.Size(1500, 500);
            this.pictureBoxMap.TabIndex = 50;
            this.pictureBoxMap.TabStop = false;
            this.pictureBoxMap.Click += new System.EventHandler(this.pictureBoxMap_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(1100, 45);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 132;
            this.button1.Text = "读取文件";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(681, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(199, 21);
            this.textBox1.TabIndex = 141;
            this.textBox1.Visible = false;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(681, 46);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(199, 21);
            this.textBox2.TabIndex = 143;
            this.textBox2.Visible = false;
            // 
            // tb_calibrationValue
            // 
            this.tb_calibrationValue.Location = new System.Drawing.Point(899, 17);
            this.tb_calibrationValue.Name = "tb_calibrationValue";
            this.tb_calibrationValue.Size = new System.Drawing.Size(84, 21);
            this.tb_calibrationValue.TabIndex = 144;
            this.tb_calibrationValue.Visible = false;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(988, 16);
            this.button2.Margin = new System.Windows.Forms.Padding(2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(84, 32);
            this.button2.TabIndex = 145;
            this.button2.Text = "更新校准值";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_init_rect
            // 
            this.btn_init_rect.Location = new System.Drawing.Point(907, 53);
            this.btn_init_rect.Name = "btn_init_rect";
            this.btn_init_rect.Size = new System.Drawing.Size(75, 23);
            this.btn_init_rect.TabIndex = 147;
            this.btn_init_rect.Text = "初始化矩形";
            this.btn_init_rect.UseVisualStyleBackColor = true;
            this.btn_init_rect.Visible = false;
            this.btn_init_rect.Click += new System.EventHandler(this.btn_init_rect_Click);
            // 
            // comboBox_map
            // 
            this.comboBox_map.FormattingEnabled = true;
            this.comboBox_map.Items.AddRange(new object[] {
            "0",
            "1",
            "2"});
            this.comboBox_map.Location = new System.Drawing.Point(1100, 19);
            this.comboBox_map.Name = "comboBox_map";
            this.comboBox_map.Size = new System.Drawing.Size(68, 20);
            this.comboBox_map.TabIndex = 148;
            this.comboBox_map.Visible = false;
            this.comboBox_map.SelectedValueChanged += new System.EventHandler(this.comboBox_map_SelectedValueChanged);
            // 
            // btn_next_rect
            // 
            this.btn_next_rect.Location = new System.Drawing.Point(988, 53);
            this.btn_next_rect.Name = "btn_next_rect";
            this.btn_next_rect.Size = new System.Drawing.Size(75, 23);
            this.btn_next_rect.TabIndex = 150;
            this.btn_next_rect.Text = "nextRect";
            this.btn_next_rect.UseVisualStyleBackColor = true;
            this.btn_next_rect.Visible = false;
            this.btn_next_rect.Click += new System.EventHandler(this.btn_next_rect_Click);
            // 
            // cb_car_visiable
            // 
            this.cb_car_visiable.AutoSize = true;
            this.cb_car_visiable.Checked = true;
            this.cb_car_visiable.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cb_car_visiable.Location = new System.Drawing.Point(-1, 484);
            this.cb_car_visiable.Name = "cb_car_visiable";
            this.cb_car_visiable.Size = new System.Drawing.Size(48, 16);
            this.cb_car_visiable.TabIndex = 151;
            this.cb_car_visiable.Text = "小车";
            this.cb_car_visiable.UseVisualStyleBackColor = true;
            this.cb_car_visiable.CheckedChanged += new System.EventHandler(this.cb_car_visiable_CheckedChanged);
            // 
            // bt_StartMove
            // 
            this.bt_StartMove.Location = new System.Drawing.Point(18, 106);
            this.bt_StartMove.Name = "bt_StartMove";
            this.bt_StartMove.Size = new System.Drawing.Size(63, 23);
            this.bt_StartMove.TabIndex = 152;
            this.bt_StartMove.Text = "开始移动";
            this.bt_StartMove.UseVisualStyleBackColor = true;
            this.bt_StartMove.Click += new System.EventHandler(this.bt_StartMove_Click);
            // 
            // tb_DistanceToRight
            // 
            this.tb_DistanceToRight.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tb_DistanceToRight.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_DistanceToRight.ForeColor = System.Drawing.Color.White;
            this.tb_DistanceToRight.Location = new System.Drawing.Point(98, 106);
            this.tb_DistanceToRight.Margin = new System.Windows.Forms.Padding(2);
            this.tb_DistanceToRight.Name = "tb_DistanceToRight";
            this.tb_DistanceToRight.Size = new System.Drawing.Size(41, 14);
            this.tb_DistanceToRight.TabIndex = 153;
            this.tb_DistanceToRight.Text = "70";
            // 
            // tb_DistanceToTop
            // 
            this.tb_DistanceToTop.BackColor = System.Drawing.SystemColors.HotTrack;
            this.tb_DistanceToTop.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tb_DistanceToTop.ForeColor = System.Drawing.Color.White;
            this.tb_DistanceToTop.Location = new System.Drawing.Point(148, 106);
            this.tb_DistanceToTop.Margin = new System.Windows.Forms.Padding(2);
            this.tb_DistanceToTop.Name = "tb_DistanceToTop";
            this.tb_DistanceToTop.Size = new System.Drawing.Size(41, 14);
            this.tb_DistanceToTop.TabIndex = 154;
            this.tb_DistanceToTop.Text = "70";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(36, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 14);
            this.label1.TabIndex = 155;
            this.label1.Text = "转换前";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(36, 56);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 14);
            this.label2.TabIndex = 156;
            this.label2.Text = "转换后";
            // 
            // textBox_old_x
            // 
            this.textBox_old_x.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_old_x.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_old_x.ForeColor = System.Drawing.Color.White;
            this.textBox_old_x.Location = new System.Drawing.Point(98, 29);
            this.textBox_old_x.Name = "textBox_old_x";
            this.textBox_old_x.Size = new System.Drawing.Size(41, 14);
            this.textBox_old_x.TabIndex = 157;
            // 
            // textBox_new_x
            // 
            this.textBox_new_x.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_new_x.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_new_x.ForeColor = System.Drawing.Color.White;
            this.textBox_new_x.Location = new System.Drawing.Point(98, 57);
            this.textBox_new_x.Name = "textBox_new_x";
            this.textBox_new_x.Size = new System.Drawing.Size(41, 14);
            this.textBox_new_x.TabIndex = 158;
            // 
            // textBox_old_y
            // 
            this.textBox_old_y.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_old_y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_old_y.ForeColor = System.Drawing.Color.White;
            this.textBox_old_y.Location = new System.Drawing.Point(148, 29);
            this.textBox_old_y.Name = "textBox_old_y";
            this.textBox_old_y.Size = new System.Drawing.Size(41, 14);
            this.textBox_old_y.TabIndex = 159;
            // 
            // textBox_new_y
            // 
            this.textBox_new_y.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_new_y.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_new_y.ForeColor = System.Drawing.Color.White;
            this.textBox_new_y.Location = new System.Drawing.Point(148, 57);
            this.textBox_new_y.Name = "textBox_new_y";
            this.textBox_new_y.Size = new System.Drawing.Size(41, 14);
            this.textBox_new_y.TabIndex = 160;
            // 
            // textBox_new_theta
            // 
            this.textBox_new_theta.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_new_theta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_new_theta.ForeColor = System.Drawing.Color.White;
            this.textBox_new_theta.Location = new System.Drawing.Point(199, 57);
            this.textBox_new_theta.Name = "textBox_new_theta";
            this.textBox_new_theta.Size = new System.Drawing.Size(41, 14);
            this.textBox_new_theta.TabIndex = 161;
            // 
            // textBox_old_theta
            // 
            this.textBox_old_theta.BackColor = System.Drawing.SystemColors.HotTrack;
            this.textBox_old_theta.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox_old_theta.ForeColor = System.Drawing.Color.White;
            this.textBox_old_theta.Location = new System.Drawing.Point(199, 29);
            this.textBox_old_theta.Name = "textBox_old_theta";
            this.textBox_old_theta.Size = new System.Drawing.Size(41, 14);
            this.textBox_old_theta.TabIndex = 162;
            // 
            // cb_IsMoveToTopLeft
            // 
            this.cb_IsMoveToTopLeft.AutoSize = true;
            this.cb_IsMoveToTopLeft.Location = new System.Drawing.Point(168, 83);
            this.cb_IsMoveToTopLeft.Name = "cb_IsMoveToTopLeft";
            this.cb_IsMoveToTopLeft.Size = new System.Drawing.Size(60, 16);
            this.cb_IsMoveToTopLeft.TabIndex = 163;
            this.cb_IsMoveToTopLeft.Text = "左偏上";
            this.cb_IsMoveToTopLeft.UseVisualStyleBackColor = true;
            // 
            // bt_StopMotion
            // 
            this.bt_StopMotion.Location = new System.Drawing.Point(18, 135);
            this.bt_StopMotion.Name = "bt_StopMotion";
            this.bt_StopMotion.Size = new System.Drawing.Size(63, 23);
            this.bt_StopMotion.TabIndex = 164;
            this.bt_StopMotion.Text = "停止移动";
            this.bt_StopMotion.UseVisualStyleBackColor = true;
            this.bt_StopMotion.Click += new System.EventHandler(this.bt_StopMotion_Click);
            // 
            // ck_CameraDistance
            // 
            this.ck_CameraDistance.AutoSize = true;
            this.ck_CameraDistance.Checked = true;
            this.ck_CameraDistance.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ck_CameraDistance.Location = new System.Drawing.Point(18, 83);
            this.ck_CameraDistance.Name = "ck_CameraDistance";
            this.ck_CameraDistance.Size = new System.Drawing.Size(144, 16);
            this.ck_CameraDistance.TabIndex = 165;
            this.ck_CameraDistance.Text = "是否从摄像头获取数据";
            this.ck_CameraDistance.UseVisualStyleBackColor = true;
            // 
            // MapVehiclePanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ck_CameraDistance);
            this.Controls.Add(this.bt_StopMotion);
            this.Controls.Add(this.cb_IsMoveToTopLeft);
            this.Controls.Add(this.textBox_old_theta);
            this.Controls.Add(this.textBox_new_theta);
            this.Controls.Add(this.textBox_new_y);
            this.Controls.Add(this.textBox_old_y);
            this.Controls.Add(this.textBox_new_x);
            this.Controls.Add(this.textBox_old_x);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_DistanceToTop);
            this.Controls.Add(this.tb_DistanceToRight);
            this.Controls.Add(this.bt_StartMove);
            this.Controls.Add(this.cb_car_visiable);
            this.Controls.Add(this.btn_next_rect);
            this.Controls.Add(this.comboBox_map);
            this.Controls.Add(this.btn_init_rect);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.tb_calibrationValue);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.pictureBoxMap);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "MapVehiclePanel";
            this.Size = new System.Drawing.Size(1500, 500);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxMap)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBoxMap;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FontDialog fontDialog1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox tb_calibrationValue;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_init_rect;
        private System.Windows.Forms.ComboBox comboBox_map;
        private System.Windows.Forms.Button btn_next_rect;
        private System.Windows.Forms.CheckBox cb_car_visiable;
        private System.Windows.Forms.Button bt_StartMove;
        private System.Windows.Forms.TextBox tb_DistanceToRight;
        private System.Windows.Forms.TextBox tb_DistanceToTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_old_x;
        private System.Windows.Forms.TextBox textBox_new_x;
        private System.Windows.Forms.TextBox textBox_old_y;
        private System.Windows.Forms.TextBox textBox_new_y;
        private System.Windows.Forms.TextBox textBox_new_theta;
        private System.Windows.Forms.TextBox textBox_old_theta;
        private System.Windows.Forms.CheckBox cb_IsMoveToTopLeft;
        private System.Windows.Forms.Button bt_StopMotion;
        private System.Windows.Forms.CheckBox ck_CameraDistance;
    }
}