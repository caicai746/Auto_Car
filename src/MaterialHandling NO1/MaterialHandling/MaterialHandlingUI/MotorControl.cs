using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1.UserControls
{
    public partial class MotorControl : UserControl
    {
        // 定义对外提供的事件
        public event EventHandler btn_ForwardClick;
        public event EventHandler btn_BackwardClick;
        public event EventHandler btn_SpeedSettingClick;
        public MotorControl()
        {
            InitializeComponent();

            // 初始化按钮背景颜色
            btn_Forward.BackColor = Color.Gainsboro;
            btn_Backward.BackColor = Color.Gainsboro;

            // 绑定按钮点击事件
            btn_Forward.Click += Btn_Forward_Click;
            btn_Backward.Click += Btn_Backward_Click;
            btn_SpeedSetting.Click += Btn_SpeedSetting_Click;
        
        }

        // MotorCode 属性
        public string MotorCode
        {
            get { return circleLbl_MotorCode.Text; }
            set { circleLbl_MotorCode.Text = value; }
        }

        // MotorName 属性
        public string MotorName
        {
            get { return lbl_MotorName.Text; }
            set { lbl_MotorName.Text = value; }
        }

        // MotorCurrentStateImage 属性
        public Image MotorCurrentStateImage
        {
            get { return picbox_MotorCurrentState.Image; }
            set { picbox_MotorCurrentState.Image = value; }
        }

        // SizeMode 属性
        public PictureBoxSizeMode ImageSizeMode
        {
            get { return picbox_MotorCurrentState.SizeMode; }
            set { picbox_MotorCurrentState.SizeMode = value; }
        }

        // Speed_State 属性（只读）
        public string Speed_State
        {
            get { return txt_Speed.Text; }
            private set { txt_Speed.Text = value; }
        }

        // Setting_Speed 属性
        public string Setting_Speed
        {
            get { return txt_SpeedSetting.Text; }
            set { txt_SpeedSetting.Text = value; }
        }

        // 正转按钮点击事件
        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            // 如果正转按钮已经是绿色，则恢复为 Gainsboro
            if (btn_Forward.BackColor == Color.Green)
            {
                btn_Forward.BackColor = Color.Gainsboro;
            }
            else
            {
                // 设置正转按钮为绿色
                btn_Forward.BackColor = Color.Green;
                // 确保反转按钮恢复为 Gainsboro
                btn_Backward.BackColor = Color.Gainsboro;
            }

            // 触发对外提供的事件
            btn_ForwardClick?.Invoke(this, e);
        }

        // 反转按钮点击事件
        private void Btn_Backward_Click(object sender, EventArgs e)
        {
            // 如果反转按钮已经是绿色，则恢复为 Gainsboro
            if (btn_Backward.BackColor == Color.Green)
            {
                btn_Backward.BackColor = Color.Gainsboro;
            }
            else
            {
                // 设置反转按钮为绿色
                btn_Backward.BackColor = Color.Green;
                // 确保正转按钮恢复为 Gainsboro
                btn_Forward.BackColor = Color.Gainsboro;
            }

            // 触发对外提供的事件
            btn_BackwardClick?.Invoke(this, e);
        }

        // 设置速度按钮点击事件
        private void Btn_SpeedSetting_Click(object sender, EventArgs e)
        {
            // 触发对外提供的事件
            btn_SpeedSettingClick?.Invoke(this, e);
        }
    }
}
