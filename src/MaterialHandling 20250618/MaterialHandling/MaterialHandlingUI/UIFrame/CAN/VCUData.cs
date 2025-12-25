using System;


namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class VCUData
    {
        // 数据下发指令 详情请查看VCU文档
        //1.运动控制指令
        public double VCU_W_LinearVelocity { get; set; } //线速度
        public double VCU_W_AngularVelocity { get; set; } //角速度
        public double VCU_W_FrontWheelAngle { get; set; } //前轮速度

        //2.辅助控制指令
        public int VCU_W_MotionMode { get; set; } //运动模式（仅限四轮四转）
        public int VCU_W_FunctionControl { get; set; } //功能控制
        public int VCU_W_SwitchControl { get; set; } //开关控制

        //数据上报指令（V2.3） 详情请查看VCU文档
        //1.底盘通用数据
        //1.1 电池信息上报指令，ID：0x201，周期：1000ms
        public double VCU_R_BusVoltage { get; set; } //总线电压
        public double VCU_R_BusCurrent { get; set; } //总线电流
        public int VCU_R_BatteryMaxTemperature { get; set; } //电池最高温度
        public int VCU_R_BatteryMinTemperature { get; set; } //电池最低温度
        public int VCU_R_SOC { get; set; } //剩余电量
        public int VCU_R_SOH { get; set; } //性能状态

        //1.2 底盘状态位信息上报指令，ID：0x202，周期：100ms
        public int VCU_R_FunctionStatus { get; set; } //功能状态*
        public bool VCU_R_EmergencyStop { get; set; } // Bit0: 急停
        public bool VCU_R_ParkingEngaged { get; set; } // Bit1: 驻车
        public bool VCU_R_ObstacleAvoidanceEnabled { get; set; } // Bit2: 避障
        public bool VCU_R_FollowModeActive { get; set; } // Bit3: 跟随
        public bool VCU_R_BrakeApplied { get; set; } // Bit4: 刹车

        public int VCU_R_WorkMode { get; set; } //工作模式*
        public int VCU_R_LightStatus { get; set; } //灯光状态*
        public int VCU_R_DeviceStatus { get; set; } //设备状态
        public int VCU_R_CollisionStatus { get; set; } //碰撞状态*
        public int VCU_R_AvoidanceStatus { get; set; } //避障状态*
        public int VCU_R_BatteryStatus { get; set; } //电池状态

        //电池状态详细解析位
        public bool Battery_DischargeMOS { get; set; }       // 低字节 Bit0
        public bool Battery_ChargeMOS { get; set; }          // 低字节 Bit1
        public bool Battery_VoltageDiffProtect { get; set; } // 低字节 Bit2

        public bool Battery_LowTempProtect { get; set; }     // 高字节 Bit0
        public bool Battery_OverTempProtect { get; set; }    // 高字节 Bit1
        public bool Battery_Discharging { get; set; }        // 高字节 Bit2
        public bool Battery_Charging { get; set; }           // 高字节 Bit3
        public bool Battery_DischargeOvercurrent { get; set; } // 高字节 Bit4
        public bool Battery_ChargeOvercurrent { get; set; }  // 高字节 Bit5
        public bool Battery_UnderVoltage { get; set; }       // 高字节 Bit6
        public bool Battery_OverVoltage { get; set; }        // 高字节 Bit7

        //1.3 底盘里程信息上报指令，ID：0x208，周期：1000ms
        public int VCU_R_Mileage { get; set; }

        //2. 底盘运动数据
        //2.1 电机转速上报指令，ID：0x203，周期：20ms
        public double VCU_R_MotorSpeed1 { get; set; } //行走电机转速1
        public double VCU_R_MotorSpeed2 { get; set; } //行走电机转速2
        public double VCU_R_MotorSpeed3 { get; set; } //行走电机转速3
        public double VCU_R_MotorSpeed4 { get; set; } //行走电机转速4

        //2.2 电机电流上报指令，ID：0x204，周期：100ms
        public double VCU_R_MotorCurrent1 { get; set; } //行走电机电流1
        public double VCU_R_MotorCurrent2 { get; set; } //行走电机电流2
        public double VCU_R_MotorCurrent3 { get; set; } //行走电机电流3
        public double VCU_R_MotorCurrent4 { get; set; } //行走电机电流4

        //2.3 转向角度上报指令，ID：0x205，周期：20ms
        public double VCU_R_SteeringAngle1 { get; set; } //转向角度1
        public double VCU_R_SteeringAngle2 { get; set; } //转向角度2
        public double VCU_R_SteeringAngle3 { get; set; } //转向角度3
        public double VCU_R_SteeringAngle4 { get; set; } //转向角度4

        //2.4 转向电机电流上报指令，ID：0x206，周期：100ms
        public double VCU_R_SteeringMotorCurrent1 { get; set; } //转向电机电流1
        public double VCU_R_SteeringMotorCurrent2 { get; set; } //转向电机电流2
        public double VCU_R_SteeringMotorCurrent3 { get; set; } //转向电机电流3
        public double VCU_R_SteeringMotorCurrent4 { get; set; } //转向电机电流4

        //2.5 运动信息上报指令，ID：0x207，周期：20ms
        public double VCU_R_LinearSpeed { get; set; } //线速度
        public double VCU_R_AngularSpeed { get; set; } //角速度

        //2.6 车轮里程信息上报指令，ID：0x20A，周期：20ms
        public double VCU_R_WheelMileage1 { get; set; } //车轮里程1
        public double VCU_R_WheelMileage2 { get; set; } //车轮里程2

        //2.7 车轮里程信息上报指令，ID：0x20B，周期：20ms
        public double VCU_R_WheelMileage3 { get; set; } //车轮里程3
        public double VCU_R_WheelMileage4 { get; set; } //车轮里程4

        public string fullBinary;//电池状态 传输过来的二进制码
        public void ParseData(CanMessage message) //数据赋值
        {
            if (message == null) return;

            byte[] Data = message.CAN_Data;

            switch (message.CAN_Id)
            {
                case 0x201:
                    // 电池信息
                    VCU_R_BusVoltage = BitConverter.ToUInt16(Data, 0) * 0.01;
                    VCU_R_BusCurrent = BitConverter.ToInt16(Data, 2) * 0.01;
                    VCU_R_BatteryMaxTemperature = Data[4];
                    VCU_R_BatteryMinTemperature = Data[5];
                    VCU_R_SOC = Data[6];
                    VCU_R_SOH = Data[7];
                    break;
                case 0x202:
                    // 底盘状态位信息
                    VCU_R_FunctionStatus = Data[0];
                    
                    // 解析功能状态位
                    VCU_R_EmergencyStop = (Data[0] & 0x01) != 0; // Bit0 - 急停
                    VCU_R_ParkingEngaged = (Data[0] & 0x02) != 0; // Bit1 - 驻车
                    VCU_R_ObstacleAvoidanceEnabled = (Data[0] & 0x04) != 0; // Bit2 - 避障
                    VCU_R_FollowModeActive = (Data[0] & 0x08) != 0; // Bit3 - 跟随
                    VCU_R_BrakeApplied = (Data[0] & 0x10) != 0; // Bit4 - 刹车
                    

                    VCU_R_WorkMode = Data[1];
                    VCU_R_LightStatus = Data[2];
                    VCU_R_DeviceStatus = Data[3];
                    VCU_R_CollisionStatus = Data[4];
                    VCU_R_AvoidanceStatus = Data[5];
                    VCU_R_BatteryStatus = BitConverter.ToUInt16(Data, 6);

                    // 解析电池状态
                    ushort batteryStatus = BitConverter.ToUInt16(Data, 6);
                    byte lowByte = (byte)(batteryStatus & 0xFF);  // 低字节（Data[6]）
                    byte highByte = (byte)(batteryStatus >> 8);   // 高字节（Data[7]）
                                                                  // 针对每个字节的二进制格式化
                    string lowBinary = Convert.ToString(lowByte, 2).PadLeft(8, '0');  // 低字节二进制
                    string highBinary = Convert.ToString(highByte, 2).PadLeft(8, '0'); // 高字节二进制

                    // 组合后的16位二进制（高字节在前）
                    fullBinary = $"{highBinary} {lowBinary}"; // 输出示例: "10100011 00000101"

                    // 解析低字节（Data[6]）
                    Battery_DischargeMOS = (lowByte & (1 << 0)) != 0;
                    Battery_ChargeMOS = (lowByte & (1 << 1)) != 0;
                    Battery_VoltageDiffProtect = (lowByte & (1 << 2)) != 0;

                    // 解析高字节（Data[7]）
                    Battery_LowTempProtect = (highByte & (1 << 0)) != 0;
                    Battery_OverTempProtect = (highByte & (1 << 1)) != 0;
                    Battery_Discharging = (highByte & (1 << 2)) != 0;
                    Battery_Charging = (highByte & (1 << 3)) != 0;
                    Battery_DischargeOvercurrent = (highByte & (1 << 4)) != 0;
                    Battery_ChargeOvercurrent = (highByte & (1 << 5)) != 0;
                    Battery_UnderVoltage = (highByte & (1 << 6)) != 0;
                    Battery_OverVoltage = (highByte & (1 << 7)) != 0;
                    break;
                case 0x203:
                    // 电机转速
                    VCU_R_MotorSpeed1 = BitConverter.ToInt16(Data, 0);
                    VCU_R_MotorSpeed2 = BitConverter.ToInt16(Data, 2);
                    VCU_R_MotorSpeed3 = BitConverter.ToInt16(Data, 4);
                    VCU_R_MotorSpeed4 = BitConverter.ToInt16(Data, 6);
                    break;
                case 0x204:
                    // 电机电流
                    VCU_R_MotorCurrent1 = BitConverter.ToInt16(Data, 0) * 0.01;
                    VCU_R_MotorCurrent2 = BitConverter.ToInt16(Data, 2) * 0.01;
                    VCU_R_MotorCurrent3 = BitConverter.ToInt16(Data, 4) * 0.01;
                    VCU_R_MotorCurrent4 = BitConverter.ToInt16(Data, 6) * 0.01;
                    break;
                case 0x205:
                    // 转向角度
                    VCU_R_SteeringAngle1 = BitConverter.ToInt16(Data, 0) * 0.1;
                    VCU_R_SteeringAngle2 = BitConverter.ToInt16(Data, 2) * 0.1;
                    VCU_R_SteeringAngle3 = BitConverter.ToInt16(Data, 4) * 0.1;
                    VCU_R_SteeringAngle4 = BitConverter.ToInt16(Data, 6) * 0.1;
                    break;
                case 0x206:
                    // 转向电机电流
                    VCU_R_SteeringMotorCurrent1 = BitConverter.ToInt16(Data, 0) * 0.01;
                    VCU_R_SteeringMotorCurrent2 = BitConverter.ToInt16(Data, 2) * 0.01;
                    VCU_R_SteeringMotorCurrent3 = BitConverter.ToInt16(Data, 4) * 0.01;
                    VCU_R_SteeringMotorCurrent4 = BitConverter.ToInt16(Data, 6) * 0.01;
                    break;
                case 0x207:
                    // 运动信息
                    VCU_R_LinearSpeed = BitConverter.ToInt16(Data, 0) * 0.001;
                    VCU_R_AngularSpeed = BitConverter.ToInt16(Data, 2) * 0.001;
                    break;
                case 0x208:
                    // 底盘里程信息
                    VCU_R_Mileage = BitConverter.ToInt32(Data, 0);
                    break;
                case 0x209:
                    // 驱动器故障信息
                    // 这里可以解析故障码
                    break;
                case 0x20A:
                    // 车轮里程信息
                    VCU_R_WheelMileage1 = BitConverter.ToInt32(Data, 0) * 0.0001;
                    VCU_R_WheelMileage2 = BitConverter.ToInt32(Data, 4) * 0.0001;
                    break;
                case 0x20B:
                    // 车轮里程信息
                    VCU_R_WheelMileage3 = BitConverter.ToInt32(Data, 0) * 0.0001;
                    VCU_R_WheelMileage4 = BitConverter.ToInt32(Data, 4) * 0.0001;
                    break;
            }
        }
    }
}
