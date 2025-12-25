using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
//MaterialHandling.MaterialHandlingUI.UIFrame.entity
namespace WindowsFormsApp1.UserControls
{
    public partial class MotorControl : UserControl
    {
        // 定义对外提供的事件
        public event EventHandler btn_ForwardClick;
        public event EventHandler btn_BackwardClick;
        public event EventHandler btn_ActivateClick;




        public MotorControl()
        {
            InitializeComponent();

            // 初始化按钮背景颜色
            btn_Forward.BackColor = Color.Gainsboro;
            btn_Backward.BackColor = Color.Gainsboro;

            // 绑定按钮点击事件
            btn_Forward.Click += Btn_Forward_Click;
            btn_Backward.Click += Btn_Backward_Click;
            btn_Activate.Click += Btn_Activate_Click;

        }

        // MotorCode 属性
        [Category("Motor Settings")]
        [Description("电机编码")]
        public string MotorCode
        {
            get { return circleLbl_MotorCode.Text; }
            set { circleLbl_MotorCode.Text = value; }
        }

        // MotorName 属性
        [Category("Motor Settings")]
        [Description("电机名称")]
        public string MotorName
        {
            get { return lbl_MotorName.Text; }
            set { lbl_MotorName.Text = value; }
        }

        // MotorActivate 属性
        [Category("Motor Settings")]
        [Description("启动按钮")]
        public bool BtnActivateVisable
        {
            get { return btn_Activate.Visible; }
            set
            {
                btn_Activate.Visible = value;
                if (false == value)
                {
                    // 获取启动按钮宽度
                    int buttonWidth = btn_Activate.Width;

                    // 调整整个控件宽度
                    this.Width -= buttonWidth;
                }
                else
                {
                    // 恢复宽度和位置
                    int buttonWidth = btn_Activate.Width;
                    this.Width += buttonWidth;
                }
            }
        }


        // BtnForwardText 属性
        [Category("Motor_BtnForward")]
        [Description("text")]
        public string BtnForwardText
        {
            get { return btn_Forward.Text; }
            set { btn_Forward.Text = value; }
        }

        // BtnBackwardText 属性
        [Category("Motor_BtnBackward")]
        [Description("text")]
        public string BtnBackwardText
        {
            get { return btn_Backward.Text; }
            set {
                btn_Backward.Text = value;
            }
        }


        /// <summary>
        /// 获取或设置“前进”按钮的背景颜色。
        /// </summary>
        [Category("Motor_BtnForward")]
        [Description("BackColor")]
        public Color BtnForwardBackColor
        {
            get { return btn_Forward.BackColor; }
            set { btn_Forward.BackColor = value; }
        }

        /// <summary>
        /// 获取或设置“后退”按钮的背景颜色。
        /// </summary>
        [Category("Motor_BtnBackward")]
        [Description("BaclColor")]
        public Color BtnBackwardBackColor
        {
            get { return btn_Backward.BackColor; }
            set { btn_Backward.BackColor = value; }
        }

        /// <summary>
        /// 获取或设置“启动”按钮的背景颜色。
        /// </summary>
        [Category("Motor_BtnActivate")]
        [Description("BackColor")]
        public Color BtnActivateColor
        {
            get { return btn_Activate.BackColor; }
            set { btn_Activate.BackColor = value; }
        }

        // 正转按钮点击事件
        private void Btn_Forward_Click(object sender, EventArgs e)
        {
            btn_ForwardClick?.Invoke(this, e);
        }

        // 反转按钮点击事件
        private void Btn_Backward_Click(object sender, EventArgs e)
        {
               // 触发对外提供的事件
            btn_BackwardClick?.Invoke(this, e);
        }

        private void Btn_Activate_Click(object sender, EventArgs e)
        {
            //触发对外提供的事件
           btn_ActivateClick?.Invoke(this, e);
        }

        /// <summary>
        /// 设置手动控制模式
        /// </summary>
        public void SetManualMode()
        {
            try
            {
                // 设置按钮和文本框可见
                btn_Forward.Visible = true;
                btn_Backward.Visible = true;
                //btn_SpeedSetting.Visible = true;
                //txt_SpeedSetting.Visible = true;
                //lbl_SpeedUnit.Visible = true;
                this.Height = 77;
            }
            catch (Exception ex)
            {
                // 处理异常，例如记录日志或显示错误信息
                MessageBox.Show($"设置手动模式时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 设置自动控制模式
        /// </summary>
        public void SetAutoMode()
        {
            try
            {
                // 设置按钮和文本框不可见
                btn_Forward.Visible = false;
                btn_Backward.Visible = false;
                //btn_SpeedSetting.Visible = false;
                //txt_SpeedSetting.Visible = false;
                //lbl_SpeedUnit.Visible = false;
                this.Height = 50;
            }
            catch (Exception ex)
            {
                // 处理异常，例如记录日志或显示错误信息
                MessageBox.Show($"设置自动模式时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void MotorControl_Load(object sender, EventArgs e)
        {

        }

        private void btn_Forward_Click_1(object sender, EventArgs e)
        {

        }

        private void btn_Activate_Click(object sender, EventArgs e)
        {

        }
    }

    // 自定义 TypeConverter，用于在属性栏中提供下拉选项
    public class CurrentStateConverter : TypeConverter
    {
        // 定义可选的值
        private static readonly string[] ValidValues = { "←", "→", "⚪" };

        // 重写 GetStandardValuesSupported，表示支持标准值
        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        // 重写 GetStandardValues，返回可选的值
        public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new StandardValuesCollection(ValidValues);
        }

        // 重写 CanConvertFrom，允许从字符串转换
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
            {
                return true;
            }
            return base.CanConvertFrom(context, sourceType);
        }

        // 重写 ConvertFrom，将字符串转换为属性值
        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value is string stringValue)
            {
                return stringValue;
            }
            return base.ConvertFrom(context, culture, value);
        }

    }
}