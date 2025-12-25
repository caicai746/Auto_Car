using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.MaterialHandlingMotor
{
    public class VehicleBattery
    {
        public float BusVoltage { get; set; } // 总线电压 (V)
        public float BusCurrent { get; set; } // 总线电流 (A)
        public int MaxTemperature { get; set; } // 电池最高温度 (℃)
        public int SOC { get; set; } // 剩余电量 (%)
        public int SOH { get; set; } // 性能状态 (%)
        public int BatteryStatus { get; set; } // 电池状态信息

        public void UpdateBatteryInfo(float busVoltage, float busCurrent, int maxTemperature, int soc, int soh, int batteryStatus)
        {
            BusVoltage = busVoltage;
            BusCurrent = busCurrent;
            MaxTemperature = maxTemperature;
            SOC = soc;
            SOH = soh;
            BatteryStatus = batteryStatus;
        }

        public void DisplayBatteryInfo()
        {
            Console.WriteLine($"Bus Voltage: {BusVoltage} V");
            Console.WriteLine($"Bus Current: {BusCurrent} A");
            Console.WriteLine($"Max Temperature: {MaxTemperature} °C");
            Console.WriteLine($"SOC: {SOC} %");
            Console.WriteLine($"SOH: {SOH} %");
            Console.WriteLine($"Battery Status: {BatteryStatus}");
        }
    }

    public class Vehicle
    {
        public int MotorSpeed1 { get; set; } // 电机1的当前转速 (rpm)
        public int MotorSpeed2 { get; set; } // 电机2的当前转速 (rpm)
        public float MotorCurrent1 { get; set; } // 电机1的当前电流 (A)
        public float MotorCurrent2 { get; set; } // 电机2的当前电流 (A)
        public int Mileage { get; set; } // 小车里程数 (m)
        public float LinearVelocity { get; set; } // 当前线速度 (m/s)
        public float AngularVelocity { get; set; } // 当前角速度 (rad/s)
        public VehicleBattery Battery { get; set; } // 电池对象

        public Vehicle()
        {
            Battery = new VehicleBattery();
        }

        public void UpdateVehicleInfo(int motorSpeed1, int motorSpeed2, float motorCurrent1, float motorCurrent2, int mileage, float linearVelocity, float angularVelocity)
        {
            MotorSpeed1 = motorSpeed1;
            MotorSpeed2 = motorSpeed2;
            MotorCurrent1 = motorCurrent1;
            MotorCurrent2 = motorCurrent2;
            Mileage = mileage;
            LinearVelocity = linearVelocity;
            AngularVelocity = angularVelocity;
        }

        public void DisplayVehicleInfo()
        {
            Console.WriteLine($"Motor Speed 1: {MotorSpeed1} rpm");
            Console.WriteLine($"Motor Speed 2: {MotorSpeed2} rpm");
            Console.WriteLine($"Motor Current 1: {MotorCurrent1} A");
            Console.WriteLine($"Motor Current 2: {MotorCurrent2} A");
            Console.WriteLine($"Mileage: {Mileage} m");
            Console.WriteLine($"Linear Velocity: {LinearVelocity} m/s");
            Console.WriteLine($"Angular Velocity: {AngularVelocity} rad/s");
            Battery.DisplayBatteryInfo();
        }
    }
}



