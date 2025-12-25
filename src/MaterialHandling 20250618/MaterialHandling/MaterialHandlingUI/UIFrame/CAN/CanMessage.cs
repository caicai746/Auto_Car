using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class CanMessage
    {
        public uint CAN_Id { get; set; } //消息ID
        public byte[] CAN_Data { get; set; } //消息数据
        public long CAN_Count { get; set; } //消息计数
        public DateTime CAN_Timestamp { get; set; } //消息时间戳
        public CanMessage(uint id, byte[] data, long count, DateTime timestamp) //构造函数
        {
            CAN_Id = id;
            CAN_Data = data;
            CAN_Count = count;
            CAN_Timestamp = timestamp;
        }

        public override string ToString() //消息类重载字符串转换方便显示
        {
            return $"ID: {CAN_Id:X3}, CAN_Data: {BitConverter.ToString(CAN_Data).Replace("-", " ")}, CAN_Count: {CAN_Count}, CAN_Timestamp: {CAN_Timestamp:yyyy-MM-dd HH:mm:ss.fff}";
        }
    }
}
