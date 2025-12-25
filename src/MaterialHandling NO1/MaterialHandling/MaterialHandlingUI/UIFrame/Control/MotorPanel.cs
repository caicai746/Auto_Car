using MaterialHandling.MaterialHandlingMotor;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    public partial class MotorPanel : UserControl
    { 

        private int testNum = 0; // 测试用
        private bool isAutoMode = false;             // 默认系统为手动模式

        // 电机相关
        private Motor M1_ReclaimerPlatform;         // 取料平台
        private Motor M2_ReclaimerDrum;             // 取料滚筒
        private Motor M3_ReclaimerScissor;          // 取料剪叉
        private Motor M4_Reclaimer;                 // 取料
        private Motor M5_Framework;                 // 框架
        private Motor M6_ReclaimerFinger;           // 取料手指

        public MotorPanel()
        {
            InitializeComponent();
            InitializeMotors();     // 初始化电机
            BindControlsToMotors(); // 绑定控件与电机对象
            //this.Dock = DockStyle.Fill;
            // 初始化模式切换按钮文本
            btn_ModeChange.Text = isAutoMode ? "自动" : "手动";
        }

        // 初始化电机对象
        private void InitializeMotors()
        {
            // 初始化皮带电机
            M1_ReclaimerPlatform = new BeltMotor("Motor_M1_ReclaimerPlatform", 1);
            M1_ReclaimerPlatform.Type = "Belt";

            // 初始化伸缩电机
            M2_ReclaimerDrum = new StretchMotor("Motor_M2_ReclaimerDrum", 2);
            M2_ReclaimerDrum.Type = "Stretch";

            // 初始化取料升降电机
            M3_ReclaimerScissor = new LifterMotor("Motor_M3_ReclaimerScissor", 3);
            M3_ReclaimerScissor.Type = "Lifter";

            // 初始化升降电机
            M4_Reclaimer = new LifterMotor("Motor_M4_Reclaimer", 4);
            M4_Reclaimer.Type = "Lifter";

                              
            // 初始化滚筒电机
            M5_Framework = new BeltMotor("Motor_M5_Framework", 5);
            M5_Framework.Type = "Belt";

            // 初始化滚筒电机
            M6_ReclaimerFinger = new BeltMotor("Motor_M6_ReclaimerFinger", 6);
            M6_ReclaimerFinger.Type = "Belt";
        }

        // 绑定控件与电机对象
        private void BindControlsToMotors()
        {
            /*
            BindMotorToControl(M1_ReclaimerBelt);
            BindMotorToControl(M2_ReclaimerStretch);
            BindMotorToControl(M3_ReclaimerLifter);
            BindMotorToControl(M4_FrameLifter);
            BindMotorToControl(M5_Roller);
            */
            M1_ReclaimerPlatform.BindMotorToControl(this);
            M2_ReclaimerDrum.BindMotorToControl(this);
            M3_ReclaimerScissor.BindMotorToControl(this);
            M4_Reclaimer.BindMotorToControl(this);
            M5_Framework.BindMotorToControl(this);
            M6_ReclaimerFinger.BindMotorToControl(this);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //M1_ReclaimerBelt.CurrentState = MotorState.Forward;
            MotorState[] arr = new MotorState[4] { MotorState.Forward, MotorState.Backward, MotorState.Stop, MotorState.Fault };
            int idx = testNum % 4;
            testNum += 1;
            M1_ReclaimerPlatform.CurrentState = arr[idx];
            //M1_ReclaimerBelt.Speed = 10;
            M1_ReclaimerPlatform.Speed = idx;



        }
        private void btn_ModeChange_Click(object sender, EventArgs e)
        {
            try
            {
                // 根据当前模式切换
                if (isAutoMode)
                {
                    // 切换到手动模式
                    Motor_M5_Framework.SetManualMode();
                    Motor_M1_ReclaimerPlatform.SetManualMode();
                    Motor_M2_ReclaimerDrum.SetManualMode();
                    Motor_M4_Reclaimer.SetManualMode();
                    Motor_M6_ReclaimerFinger.SetManualMode();

                    // 更新按钮文本
                    btn_ModeChange.Text = "手动";

                    // 更新系统状态
                    isAutoMode = false;
                }
                else
                {
                    // 切换到自动模式
                    Motor_M5_Framework.SetAutoMode();
                    Motor_M1_ReclaimerPlatform.SetAutoMode();
                    Motor_M2_ReclaimerDrum.SetAutoMode();
                    Motor_M4_Reclaimer.SetAutoMode();
                    Motor_M6_ReclaimerFinger.SetAutoMode();

                    // 更新按钮文本
                    btn_ModeChange.Text = "自动";

                    // 更新系统状态
                    isAutoMode = true;
                }
            }
            catch (Exception ex)
            {
                // 处理异常，例如记录日志或显示错误信息
                MessageBox.Show($"切换模式时发生错误: {ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Motor_M1_ReclaimerBelt_Load(object sender, EventArgs e)
        {

        }

        private void M1_ReclaimerPlatform_Load(object sender, EventArgs e)
        {
            // Bool16 读写示例
            //MainFrame.CSignal.bool16 = false;
            //bool flag = MainFrame.plc.Output_DB.bool16;

            bool M1_Run = MainFrame.plc.Output_DB.bool9;
            bool M1_Up = MainFrame.plc.Output_DB.bool10;
            bool M1_Down = MainFrame.plc.Output_DB.bool11;


        }

        //MessageBox.Show("up...");
        //MainFrame.CSignal.bool10 = false;
        //MainFrame.CSignal.bool11 = true;
        /// <summary>
        /// M1 堆取料机平台上升 (Forward) 按钮点击事件。
        /// </summary>
        private void M1_ReclaimerPlatform_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M1_ReclaimerPlatform.Forward == true)
            {
                M1_ReclaimerPlatform.Forward = false;
                // 写PLC变量
                MainFrame.CSignal.bool10 = false;
                Motor_M1_ReclaimerPlatform.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M1_ReclaimerPlatform.Forward = true;
                MainFrame.CSignal.bool10 = true;
                Motor_M1_ReclaimerPlatform.BtnForwardBackColor = Color.Lime;

                M1_ReclaimerPlatform.Backward = false;
                MainFrame.CSignal.bool11 = false;
                Motor_M1_ReclaimerPlatform.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M1 堆取料机平台下降 (Backward) 按钮点击事件。
        /// </summary>
        private void M1_ReclaimerPlatform_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M1_ReclaimerPlatform.Backward == true)
            {
                M1_ReclaimerPlatform.Backward = false;
                MainFrame.CSignal.bool11 = false;
                Motor_M1_ReclaimerPlatform.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M1_ReclaimerPlatform.Backward = true;
                MainFrame.CSignal.bool11 = true;
                Motor_M1_ReclaimerPlatform.BtnBackwardBackColor = Color.Lime;

                M1_ReclaimerPlatform.Forward = false;
                MainFrame.CSignal.bool10 = false;
                Motor_M1_ReclaimerPlatform.BtnForwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M2 堆取料机滚筒前进按钮点击事件。
        /// </summary>
        private void M2_ReclaimerDrum_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M2_ReclaimerDrum.Forward == true)
            {
                M2_ReclaimerDrum.Forward = false;
                // 写入PLC变量
                MainFrame.CSignal.bool13 = false;
                Motor_M2_ReclaimerDrum.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M2_ReclaimerDrum.Forward = true;
                MainFrame.CSignal.bool13 = true;
                Motor_M2_ReclaimerDrum.BtnForwardBackColor = Color.Lime;

                M2_ReclaimerDrum.Backward = false;
                MainFrame.CSignal.bool14 = false;
                Motor_M2_ReclaimerDrum.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M2 堆取料机滚筒后退按钮点击事件。
        /// </summary>
        private void M2_ReclaimerDrum_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M2_ReclaimerDrum.Backward == true)
            {
                M2_ReclaimerDrum.Backward = false;
                MainFrame.CSignal.bool14 = false;
                Motor_M2_ReclaimerDrum.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M2_ReclaimerDrum.Backward = true;
                MainFrame.CSignal.bool14 = true;
                Motor_M2_ReclaimerDrum.BtnBackwardBackColor = Color.Lime;

                M2_ReclaimerDrum.Forward = false;
                MainFrame.CSignal.bool13 = false;
                Motor_M2_ReclaimerDrum.BtnForwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M3 堆取料机剪式装置前进按钮点击事件。
        /// </summary>
        private void M3_ReclaimerScissor_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M3_ReclaimerScissor.Forward == true)
            {
                M3_ReclaimerScissor.Forward = false;
                MainFrame.CSignal.bool15 = false;
                Motor_M3_ReclaimerScissor.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M3_ReclaimerScissor.Forward = true;
                MainFrame.CSignal.bool15 = true;
                Motor_M3_ReclaimerScissor.BtnForwardBackColor = Color.Lime;

                M3_ReclaimerScissor.Backward = false;
                MainFrame.CSignal.bool16 = false;
                Motor_M3_ReclaimerScissor.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M3 堆取料机剪式装置后退按钮点击事件。
        /// </summary>
        private void M3_ReclaimerScissor_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M3_ReclaimerScissor.Backward == true)
            {
                M3_ReclaimerScissor.Backward = false;
                MainFrame.CSignal.bool16 = false;
                Motor_M3_ReclaimerScissor.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M3_ReclaimerScissor.Backward = true;
                MainFrame.CSignal.bool16 = true;
                Motor_M3_ReclaimerScissor.BtnBackwardBackColor = Color.Lime;

                M3_ReclaimerScissor.Forward = false;
                MainFrame.CSignal.bool15 = false;
                Motor_M3_ReclaimerScissor.BtnForwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M4 堆取料机前进 (左移) 按钮点击事件。
        /// </summary>
        private void M4_Reclaimer_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M4_Reclaimer.Forward == true)
            {
                M4_Reclaimer.Forward = false;
                MainFrame.CSignal.bool17 = false;
                Motor_M4_Reclaimer.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M4_Reclaimer.Forward = true;
                MainFrame.CSignal.bool17 = true;
                Motor_M4_Reclaimer.BtnForwardBackColor = Color.Lime;

                M4_Reclaimer.Backward = false;
                MainFrame.CSignal.bool18 = false;
                Motor_M4_Reclaimer.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M4 堆取料机后退 (右移) 按钮点击事件。
        /// </summary>
        private void M4_Reclaimer_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M4_Reclaimer.Backward == true)
            {
                M4_Reclaimer.Backward = false;
                MainFrame.CSignal.bool18 = false;
                Motor_M4_Reclaimer.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M4_Reclaimer.Backward = true;
                MainFrame.CSignal.bool18 = true;
                Motor_M4_Reclaimer.BtnBackwardBackColor = Color.Lime;

                M4_Reclaimer.Forward = false;
                MainFrame.CSignal.bool17 = false;
                Motor_M4_Reclaimer.BtnForwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M5 框架前进 (左移) 按钮点击事件。
        /// </summary>
        private void M5_Framework_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M5_Framework.Forward == true)
            {
                M5_Framework.Forward = false;
                MainFrame.CSignal.bool19 = false;
                Motor_M5_Framework.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M5_Framework.Forward = true;
                MainFrame.CSignal.bool19 = true;
                Motor_M5_Framework.BtnForwardBackColor = Color.Lime;

                M5_Framework.Backward = false;
                MainFrame.CSignal.bool20 = false;
                Motor_M5_Framework.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M5 框架后退 (右移) 按钮点击事件。
        /// </summary>
        private void M5_Framework_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M5_Framework.Backward == true)
            {
                M5_Framework.Backward = false;
                MainFrame.CSignal.bool20 = false;
                Motor_M5_Framework.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M5_Framework.Backward = true;
                MainFrame.CSignal.bool20 = true;
                Motor_M5_Framework.BtnBackwardBackColor = Color.Lime;

                M5_Framework.Forward = false;
                MainFrame.CSignal.bool19 = false;
                Motor_M5_Framework.BtnForwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M6 堆取料机伸指前进 (伸出) 按钮点击事件。
        /// </summary>
        private void M6_ReclaimerFinger_btn_ForwardClick(object sender, EventArgs e)
        {
            if (M6_ReclaimerFinger.Forward == true)
            {
                M6_ReclaimerFinger.Forward = false;
                MainFrame.CSignal.bool22 = false;
                Motor_M6_ReclaimerFinger.BtnForwardBackColor = Color.DarkGray;
            }
            else
            {
                M6_ReclaimerFinger.Forward = true;
                MainFrame.CSignal.bool22 = true;
                Motor_M6_ReclaimerFinger.BtnForwardBackColor = Color.Lime;

                M6_ReclaimerFinger.Backward = false;
                MainFrame.CSignal.bool23 = false;
                Motor_M6_ReclaimerFinger.BtnBackwardBackColor = Color.DarkGray;
            }
        }

        /// <summary>
        /// M6 堆取料机伸指后退 (缩回) 按钮点击事件。
        /// </summary>
        private void M6_ReclaimerFinger_btn_BackwardClick(object sender, EventArgs e)
        {
            if (M6_ReclaimerFinger.Backward == true)
            {
                M6_ReclaimerFinger.Backward = false;
                MainFrame.CSignal.bool23 = false;
                Motor_M6_ReclaimerFinger.BtnBackwardBackColor = Color.DarkGray;
            }
            else
            {
                M6_ReclaimerFinger.Backward = true;
                MainFrame.CSignal.bool23 = true;
                Motor_M6_ReclaimerFinger.BtnBackwardBackColor = Color.Lime;

                M6_ReclaimerFinger.Forward = false;
                MainFrame.CSignal.bool22 = false;
                Motor_M6_ReclaimerFinger.BtnForwardBackColor = Color.DarkGray;
            }
        }
      



        // 从 plc 读取 各个电机状态
        private void read_plc(object sender, EventArgs e)
        {
            // M1: ReclaimerPlatform (Up/Down)
            //M1_ReclaimerPlatform.Forward = MainFrame.plc.Output_DB.bool10;
            //Motor_M1_ReclaimerPlatform.BtnForwardBackColor = M1_ReclaimerPlatform.Forward ? Color.Lime : Color.DarkGray;

            //M1_ReclaimerPlatform.Backward = MainFrame.plc.Output_DB.bool11;
            //Motor_M1_ReclaimerPlatform.BtnBackwardBackColor = M1_ReclaimerPlatform.Backward ? Color.Lime : Color.DarkGray;

            //M1_ReclaimerPlatform.Run = MainFrame.plc.Output_DB.bool9;
            //Motor_M1_ReclaimerPlatform.BtnActivateColor = M1_ReclaimerPlatform.Run ? Color.Lime : Color.DarkGray;

            //// M2: ReclaimerDrum
            //M2_ReclaimerDrum.Forward = MainFrame.plc.Output_DB.bool13;
            //Motor_M2_ReclaimerDrum.BtnForwardBackColor = M2_ReclaimerDrum.Forward ? Color.Lime : Color.DarkGray;

            //M2_ReclaimerDrum.Backward = MainFrame.plc.Output_DB.bool14;
            //Motor_M2_ReclaimerDrum.BtnBackwardBackColor = M2_ReclaimerDrum.Backward ? Color.Lime : Color.DarkGray;

            //// M3: ReclaimerScissor
            //M3_ReclaimerScissor.Forward = MainFrame.plc.Output_DB.bool15;
            //Motor_M3_ReclaimerScissor.BtnForwardBackColor = M3_ReclaimerScissor.Forward ? Color.Lime : Color.DarkGray;

            //M3_ReclaimerScissor.Backward = MainFrame.plc.Output_DB.bool16;
            //Motor_M3_ReclaimerScissor.BtnBackwardBackColor = M3_ReclaimerScissor.Backward ? Color.Lime : Color.DarkGray;

            //// M4: Reclaimer
            //M4_Reclaimer.Forward = MainFrame.plc.Output_DB.bool17;
            //Motor_M4_Reclaimer.BtnForwardBackColor = M4_Reclaimer.Forward ? Color.Lime : Color.DarkGray;

            //M4_Reclaimer.Backward = MainFrame.plc.Output_DB.bool18;
            //Motor_M4_Reclaimer.BtnBackwardBackColor = M4_Reclaimer.Backward ? Color.Lime : Color.DarkGray;

            //// M5: Framework
            //M5_Framework.Forward = MainFrame.plc.Output_DB.bool19;
            //Motor_M5_Framework.BtnForwardBackColor = M5_Framework.Forward ? Color.Lime : Color.DarkGray;

            //M5_Framework.Backward = MainFrame.plc.Output_DB.bool20;
            //Motor_M5_Framework.BtnBackwardBackColor = M5_Framework.Backward ? Color.Lime : Color.DarkGray;

            //// M6: ReclaimerFinger
            //M6_ReclaimerFinger.Forward = MainFrame.plc.Output_DB.bool22;
            //Motor_M6_ReclaimerFinger.BtnForwardBackColor = M6_ReclaimerFinger.Forward ? Color.Lime : Color.DarkGray;

            //M6_ReclaimerFinger.Backward = MainFrame.plc.Output_DB.bool23;
            //Motor_M6_ReclaimerFinger.BtnBackwardBackColor = M6_ReclaimerFinger.Backward ? Color.Lime : Color.DarkGray;

            //M6_ReclaimerFinger.Run = MainFrame.plc.Output_DB.bool21;
            //Motor_M6_ReclaimerFinger.BtnActivateColor = M6_ReclaimerFinger.Run ? Color.Lime : Color.DarkGray;

        }

        private void Motor_M1_ReclaimerPlatform_btn_ActivateClick(object sender, EventArgs e)
        {
            if(M1_ReclaimerPlatform.Run == true)
            {
                M1_ReclaimerPlatform.Run = false;
                MainFrame.CSignal.bool9 = false;
                Motor_M1_ReclaimerPlatform.BtnActivateColor = Color.DarkGray;
                // ...
            }
            else
            {
                M1_ReclaimerPlatform.Run = true;
                MainFrame.CSignal.bool9 = true;
                Motor_M1_ReclaimerPlatform.BtnActivateColor = Color.Lime;
                // ...
            }
        }

        private void Motor_M6_ReclaimerFinger_btn_ActivateClick(object sender, EventArgs e)
        {
            if (M6_ReclaimerFinger.Run == true)
            {
                M6_ReclaimerFinger.Run = false;
                MainFrame.CSignal.bool21 = false;
                Motor_M6_ReclaimerFinger.BtnActivateColor = Color.DarkGray;
                // ...
            }
            else
            {
                M6_ReclaimerFinger.Run = true;
                MainFrame.CSignal.bool21 = true;
                Motor_M6_ReclaimerFinger.BtnActivateColor = Color.Lime;
                // ...
            }
        }
    }
}
