using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class VCUCommand
    {
        public uint commandID = 0x200; //区分指令类型，0x200运动控制指令，0x1FF辅助控制指令
        public byte[] data = new byte[8];

        public byte[] linearVelocity = BitConverter.GetBytes((short)(0.0)); // 0.0 m/s
        public byte[] angularVelocity = BitConverter.GetBytes((short)(0.0)); // 0.0 rad/s
        public byte[] frontWheelAngle = BitConverter.GetBytes((short)(0.0)); // 0.0 deg (无效)

        public VCUCommand(uint cmdID, short lineSpeed, short angleSpeed)
        {
            commandID = cmdID;
            linearVelocity = BitConverter.GetBytes((short)(lineSpeed));
            angularVelocity = BitConverter.GetBytes((short)(angleSpeed));
            Array.Copy(linearVelocity, data, 2);
            Array.Copy(angularVelocity, 0, data, 2, 2);
            Array.Copy(frontWheelAngle, 0, data, 4, 2);
        }

        public byte[] GetCommandData()
        {
            return data;
        }
    }
}
