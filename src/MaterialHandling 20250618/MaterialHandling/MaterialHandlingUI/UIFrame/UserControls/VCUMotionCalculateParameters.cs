using MaterialHandling.MaterialHandlingUI.UIFrame.CAN;
using System;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.UserControls
{
    public partial class VCUMotionCalculateParameters : UserControl
    {
        public VCUMotionCalculateParameters()
        {
            InitializeComponent();
        }

        private void btn_THRESHOLD_Click(object sender, EventArgs e)
        {
            try
            {
                VCUMotionCalculate.ANGLE_THRESHOLD = double.Parse(tb_ANGLE_THRESHOLD.Text);
                VCUMotionCalculate.DISTANCE_THRESHOLD = double.Parse(tb_DISTANCE_THRESHOLD.Text);
                VCUMotionCalculate.alpha_speed = double.Parse(tb_alpha_speed.Text);
                MoveInMatrix.MatrixRatio = double.Parse(tb_MatrixRatio.Text);
            }
            catch
            {
                MessageBox.Show("请输入合法值！ #车辆移动参数窗口");
            }
            
        }
    }
}
