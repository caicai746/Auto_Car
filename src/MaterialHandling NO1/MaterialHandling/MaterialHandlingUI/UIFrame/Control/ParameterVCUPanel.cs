using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Linq;
using MaterialHandling.MaterialHandlingUI.UIFrame.CAN;
using MaterialHandling.MaterialHandlingUI.UIFrame.ROS;
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    public partial class ParameterVCUPanel : UserControl
    {
        VCUData vCUData;
        Timer timer_VCU_Panel = new Timer();
        //ROSClient rOSClient;
        MapVehiclePanel mapVehiclePanel;
        VCUMotionCalculate vCUMotionCalculate;
        public void AssignPose()
        {
            tb_pose_x.Text = mapVehiclePanel.carPosition.X.ToString();
            tb_pose_y.Text = mapVehiclePanel.carPosition.Y.ToString();
            tb_pose_theta.Text = mapVehiclePanel.carTheta.ToString();
        }
        public ParameterVCUPanel()
        {
            InitializeComponent();
            Load_Parameters();
        }
        public void Load_Parameters()
        {
            mapVehiclePanel = MainFrame.cf.mapVehiclePanel;
            //rOSClient = MainFrame.mf.rOSClient;
            vCUData = CanBus.vcu_data;
            
            timer_VCU_Panel.Start();
            timer_VCU_Panel.Tick += Timer_VCU_Panel_Tick;
            destinationX = 0;
            destinationY = 0;
            destinationTheta = 0;
        }
        private void Timer_VCU_Panel_Tick(object sender, EventArgs e)//界面显示的定时器
        {
            AssignPose();
            lb_Linear_Velocity.Text = vCUData.VCU_R_LinearSpeed.ToString("F2") + "m/s";//线速度
            lb_Angular_Velocity.Text = vCUData.VCU_R_AngularSpeed.ToString("F2") + "r/s";//角速度

            lb_SOC.Text = vCUData.VCU_R_SOC.ToString("F1") + "%";//剩余电量
            lb_SOH.Text = vCUData.VCU_R_SOH.ToString("F1") + "%";//性能状态

            lb_Motor_Speed1.Text = vCUData.VCU_R_MotorSpeed1.ToString("F1");//行走电机转速1
            lb_Motor_Speed2.Text = vCUData.VCU_R_MotorSpeed2.ToString("F1");//行走电机转速2

            lb_Mileage1.Text = vCUData.VCU_R_WheelMileage1.ToString("F1") + "m";//车轮里程1
            lb_Mileage2.Text = vCUData.VCU_R_WheelMileage2.ToString("F1") + "m";//车轮里程2

            lb_Max_Temperature.Text = vCUData.VCU_R_BatteryMaxTemperature.ToString("F1") + "℃";//电池最高温度
            lb_Min_Temperature.Text = vCUData.VCU_R_BatteryMinTemperature.ToString("F1") + "℃";//电池最低温度

            lb_Bus_Voltage.Text = vCUData.VCU_R_BusVoltage.ToString("F1") + "V";//总线电压

            lb_Bus_Current.Text = vCUData.VCU_R_BusCurrent.ToString("F1") + "A";//总线电流

            lb_VCU_R_MotorCurrent1.Text = vCUData.VCU_R_MotorCurrent1.ToString("F1") + "A";//行走电机电流1
            lb_VCU_R_MotorCurrent2.Text = vCUData.VCU_R_MotorCurrent2.ToString("F1") + "A";//行走电机电流2

            lb_VCU_R_SteeringAngle1.Text = vCUData.VCU_R_SteeringAngle1.ToString("F2") + "r";//转向角度1
            lb_VCU_R_SteeringAngle2.Text = vCUData.VCU_R_SteeringAngle2.ToString("F2") + "r";//转向角度1//转向角度2

            lb_VCU_R_SteeringMotorCurrent1.Text = vCUData.VCU_R_SteeringMotorCurrent1.ToString("F2") + "A";//转向电机电流1
            lb_VCU_R_SteeringMotorCurrent2.Text = vCUData.VCU_R_SteeringMotorCurrent2.ToString("F2") + "A";//转向电机电流2

            //lb_VCU_R_FunctionStatus.Text = vCUData.VCU_R_FunctionStatus.ToString("F2");//功能状态
            // 更新功能状态栏显示
            string activeStates = "无";
            if (vCUData.VCU_R_EmergencyStop) activeStates="急停";
            if (vCUData.VCU_R_ParkingEngaged) activeStates = "驻车";
            if (vCUData.VCU_R_ObstacleAvoidanceEnabled)  activeStates = "避障";
            if (vCUData.VCU_R_FollowModeActive)  activeStates = "跟随";
            if (vCUData.VCU_R_BrakeApplied) activeStates = "刹车";
            lb_VCU_R_FunctionStatus.Text = activeStates;//功能状态   
            lb_VCU_R_DeviceStatus.Text = vCUData.VCU_R_DeviceStatus.ToString("F2"); //设备状态
            //=====================================================================================================
            lb_VCU_R_WorkMode.Text = vCUData.VCU_R_WorkMode.ToString("F2"); //工作模式
            // 更新工作模式状态栏显示  解析工作模式位
            activeStates = "无";
            if (vCUData.VCU_R_WorkMode == 1) activeStates = "CAN协议";
            else if (vCUData.VCU_R_WorkMode == 2) activeStates = "SBUS";
            else if (vCUData.VCU_R_WorkMode == 3) activeStates = "VRF";
            else if (vCUData.VCU_R_WorkMode == 4) activeStates = "PWM-RTK";
            else if (vCUData.VCU_R_WorkMode == 255) activeStates = "定制";
            lb_VCU_R_WorkMode.Text = activeStates; //工作模式
            //=====================================================================================================

            //lb_Battery_Status.Text = vCUData.VCU_R_BatteryStatus.ToString("F1");//电池状态
            string batteryStates = "";  // 默认状态

            List<string> activeBatteryStates = new List<string>();

            /*// 低压侧状态解析（低字节 Data[6]）
            if (vCUData.Battery_DischargeMOS) activeBatteryStates.Add("放电MOS开启");
            if (vCUData.Battery_ChargeMOS) activeBatteryStates.Add("充电MOS开启");
            if (vCUData.Battery_VoltageDiffProtect) activeBatteryStates.Add("压差保护");
            */
            // 高压侧状态解析（高字节 Data[7]）
            if (vCUData.Battery_LowTempProtect) activeBatteryStates.Add("低温保护");
            if (vCUData.Battery_OverTempProtect) activeBatteryStates.Add("过温保护");
            if (vCUData.Battery_Discharging) activeBatteryStates.Add("放电中");
            if (vCUData.Battery_Charging) activeBatteryStates.Add("充电中");
            if (vCUData.Battery_DischargeOvercurrent) activeBatteryStates.Add("放电过流");
            if (vCUData.Battery_ChargeOvercurrent) activeBatteryStates.Add("充电过流");
            if (vCUData.Battery_UnderVoltage) activeBatteryStates.Add("欠压保护");
            if (vCUData.Battery_OverVoltage) activeBatteryStates.Add("过压保护");

            // 组合状态显示
            if (activeBatteryStates.Count > 0)
            {
                batteryStates = string.Join("\n", activeBatteryStates);
                // 如果超过3个状态显示省略号
                if (activeBatteryStates.Count > 3)
                {
                    batteryStates = string.Join(", ", activeBatteryStates.Take(3)) + "...";
                }
            }

            // 显示到界面控件
            lb_Battery_Status.Text = batteryStates;
            //lb_Battery_Status.Text = vCUData.fullBinary;

            // 可选：根据紧急状态改变颜色
            if (vCUData.Battery_OverVoltage || vCUData.Battery_UnderVoltage)
            {
                lb_Battery_Status.ForeColor = Color.Red;
            }
            else
            {
                lb_Battery_Status.ForeColor = SystemColors.ControlText;
            }
            //=====================================================================================================

        }
        public double destinationX, destinationY, destinationTheta;

        private void bt_StopCalculate_Click(object sender, EventArgs e)
        {
            vCUMotionCalculate.StopMotion();
        }

        private void button_setDestination_Click(object sender, EventArgs e)
        {
            
            destinationX = double.Parse(tb_destinationX.Text);
            destinationY = double.Parse(tb_destinationY.Text);
            destinationTheta = double.Parse(tb_destinationTheta.Text);
            vCUMotionCalculate = new VCUMotionCalculate();
            vCUMotionCalculate.Calculate(destinationX, destinationY, destinationTheta);//调用计算运动控制逻辑
            
        }
    }
}
