
namespace MaterialHandling.MaterialHandlingUI.UIFrame.UserControls
{
    partial class VCUMotionCalculateParameters
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
            this.tb_ANGLE_THRESHOLD = new System.Windows.Forms.TextBox();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.btn_THRESHOLD = new System.Windows.Forms.Button();
            this.tb_DISTANCE_THRESHOLD = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_alpha_speed = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tb_MatrixRatio = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // tb_ANGLE_THRESHOLD
            // 
            this.tb_ANGLE_THRESHOLD.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_ANGLE_THRESHOLD.Location = new System.Drawing.Point(84, 47);
            this.tb_ANGLE_THRESHOLD.Name = "tb_ANGLE_THRESHOLD";
            this.tb_ANGLE_THRESHOLD.Size = new System.Drawing.Size(42, 26);
            this.tb_ANGLE_THRESHOLD.TabIndex = 38;
            this.tb_ANGLE_THRESHOLD.Text = "0.05";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Font = new System.Drawing.Font("宋体", 10F);
            this.label17.Location = new System.Drawing.Point(15, 53);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(63, 14);
            this.label17.TabIndex = 39;
            this.label17.Text = "角度容差";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label18.Location = new System.Drawing.Point(14, 14);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(129, 19);
            this.label18.TabIndex = 40;
            this.label18.Text = "车辆移动参数";
            // 
            // btn_THRESHOLD
            // 
            this.btn_THRESHOLD.Font = new System.Drawing.Font("宋体", 9F);
            this.btn_THRESHOLD.Location = new System.Drawing.Point(264, 117);
            this.btn_THRESHOLD.Name = "btn_THRESHOLD";
            this.btn_THRESHOLD.Size = new System.Drawing.Size(49, 26);
            this.btn_THRESHOLD.TabIndex = 41;
            this.btn_THRESHOLD.Text = "设 置";
            this.btn_THRESHOLD.UseVisualStyleBackColor = true;
            this.btn_THRESHOLD.Click += new System.EventHandler(this.btn_THRESHOLD_Click);
            // 
            // tb_DISTANCE_THRESHOLD
            // 
            this.tb_DISTANCE_THRESHOLD.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_DISTANCE_THRESHOLD.Location = new System.Drawing.Point(84, 79);
            this.tb_DISTANCE_THRESHOLD.Name = "tb_DISTANCE_THRESHOLD";
            this.tb_DISTANCE_THRESHOLD.Size = new System.Drawing.Size(42, 26);
            this.tb_DISTANCE_THRESHOLD.TabIndex = 42;
            this.tb_DISTANCE_THRESHOLD.Text = "3";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("宋体", 10F);
            this.label1.Location = new System.Drawing.Point(15, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 14);
            this.label1.TabIndex = 43;
            this.label1.Text = "距离容差";
            // 
            // tb_alpha_speed
            // 
            this.tb_alpha_speed.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_alpha_speed.Location = new System.Drawing.Point(84, 111);
            this.tb_alpha_speed.Name = "tb_alpha_speed";
            this.tb_alpha_speed.Size = new System.Drawing.Size(42, 26);
            this.tb_alpha_speed.TabIndex = 44;
            this.tb_alpha_speed.Text = "0.4";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("宋体", 10F);
            this.label2.Location = new System.Drawing.Point(15, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 14);
            this.label2.TabIndex = 45;
            this.label2.Text = "转速系数";
            // 
            // tb_MatrixRatio
            // 
            this.tb_MatrixRatio.Font = new System.Drawing.Font("宋体", 12F);
            this.tb_MatrixRatio.Location = new System.Drawing.Point(271, 47);
            this.tb_MatrixRatio.Name = "tb_MatrixRatio";
            this.tb_MatrixRatio.Size = new System.Drawing.Size(42, 26);
            this.tb_MatrixRatio.TabIndex = 46;
            this.tb_MatrixRatio.Text = "0.3";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("宋体", 10F);
            this.label3.Location = new System.Drawing.Point(174, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 14);
            this.label3.TabIndex = 47;
            this.label3.Text = "框内移动比例";
            // 
            // VCUMotionCalculateParameters
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.tb_MatrixRatio);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tb_alpha_speed);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_DISTANCE_THRESHOLD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btn_THRESHOLD);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.tb_ANGLE_THRESHOLD);
            this.Controls.Add(this.label17);
            this.Name = "VCUMotionCalculateParameters";
            this.Size = new System.Drawing.Size(434, 811);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.TextBox tb_ANGLE_THRESHOLD;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        public System.Windows.Forms.Button btn_THRESHOLD;
        public System.Windows.Forms.TextBox tb_DISTANCE_THRESHOLD;
        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.TextBox tb_alpha_speed;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.TextBox tb_MatrixRatio;
        private System.Windows.Forms.Label label3;
    }
}