using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Peak.Can.Basic.BackwardCompatibility;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class CanBus
    {
        private readonly ushort _channel = PCANBasic.PCAN_USBBUS1; //实例化一个PCAN_USB对象
        private readonly TPCANBaudrate _baudrate = TPCANBaudrate.PCAN_BAUD_500K; //设置波特率为500kbps
        public long count = 0;  //消息计数
        public static VCUData vcu_data = new VCUData(); //CAN总线和VCU一一对应，为了方便起见设置该类的类成员，避免层层传参
        public bool Initialize()
        {
            // 初始化CAN通道
            TPCANStatus status = PCANBasic.Initialize(_channel, _baudrate);
            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                Console.WriteLine($"CAN总线初始化失败: {status}");
                return false;
            }

            Console.WriteLine("CAN总线初始化成功");

            // 配置接收过滤器
            status = PCANBasic.FilterMessages(_channel, 0, 0x7FF, TPCANMode.PCAN_MODE_STANDARD);
            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                Console.WriteLine($"配置CAN接收过滤器失败: {status}");
                return false;
            }

            Console.WriteLine("CAN接收过滤器配置成功");
            return true;
        }
        public void Uninitialize()
        {
            if (PCANBasic.GetStatus(_channel) == TPCANStatus.PCAN_ERROR_OK) //通道存在
            {
                TPCANStatus status = PCANBasic.Uninitialize(_channel);
                if (status != TPCANStatus.PCAN_ERROR_OK)
                {
                    Console.WriteLine($"CAN总线释放失败: {status}");
                    return;
                }
                Console.WriteLine("CAN总线释放成功");
            }
            else
            {
                Console.WriteLine("CAN总线未初始化");
            }
        }
         ~CanBus()
        {
            Uninitialize();
        }


        public void StartListening()
        {
            // 启动监听线程
            var thread = new System.Threading.Thread(Listen);
            thread.Start();
        }

        private void Listen()
        {
            while (true)
            {
                TPCANMsg msg = new TPCANMsg(); //创建一个PCAN消息对象
                TPCANTimestamp timestamp = new TPCANTimestamp(); //PCAN消息时间戳

                TPCANStatus status = PCANBasic.Read(_channel, out msg, out timestamp); //从PCAN channel中读取消息，并获取时间戳
                if (status == TPCANStatus.PCAN_ERROR_OK)
                {
                    HandleMessage(msg, timestamp);
                }
                else if (status == TPCANStatus.PCAN_ERROR_QRCVEMPTY)
                {
                    // 消息队列为空，继续等待
                    System.Threading.Thread.Sleep(100);
                }
                else
                {
                    Console.WriteLine($"CAN读取消息失败: {status}");
                    break;
                }
            }
        }

        private void HandleMessage(TPCANMsg msg, TPCANTimestamp timestamp) //处理消息和时间戳
        {
            uint id = msg.ID;
            byte[] data = new byte[msg.LEN];
            Array.Copy(msg.DATA, data, msg.LEN);

            // 计算时间戳（以微秒为单位）
            long totalMicroseconds = timestamp.micros + 1000 * timestamp.millis + 0x100000000 * 1000 * timestamp.millis_overflow;

            // 将时间戳转换为DateTime
            DateTime timestampUtc = DateTime.SpecifyKind(DateTime.UtcNow.AddTicks(totalMicroseconds / 10), DateTimeKind.Utc);

            // 更新消息计数
            CanMessage message = new CanMessage(id, data, count++, timestampUtc);

            // 存储到变量
            StoreMessage(message);

            // 打印到控制台
            //Console.WriteLine(message);
            PrintAllProperties(vcu_data);
        }

        private void StoreMessage(CanMessage message)// 根据需要存储消息到变量或数据库
        {
            vcu_data.ParseData(message);
        }

        public static void PrintAllProperties(object obj) //显示当前参数
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();

            //Console.WriteLine($"类 {type.Name} 的所有属性：");
            foreach (PropertyInfo property in properties)
            {
                object value = property.GetValue(obj);
                //Console.WriteLine($"{property.Name}: {value}");
            }
        }

        public void SendMessage(VCUCommand cmd) //消息发送
        {
            byte[] data = cmd.GetCommandData();
            uint id = cmd.commandID; 
            if (data == null)
            {
                throw new ArgumentNullException(nameof(data), "CAN数据不能为null");
            }

            if (data.Length > 8)
            {
                throw new ArgumentException("CAN数据长度不能超过8字节", nameof(data));
            }

            //创建需要发送的消息对象
            TPCANMsg msg = new TPCANMsg();
            msg.ID = id;
            msg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            msg.LEN = (byte)data.Length;

            // 确保msg.DATA被正确初始化
            msg.DATA = new byte[8];
            Array.Copy(data, msg.DATA, data.Length);

            TPCANStatus status = PCANBasic.Write(_channel, ref msg);
            if (status != TPCANStatus.PCAN_ERROR_OK)
            {
                Console.WriteLine($"CAN发送消息失败: {status}");
            }
            else
            {
                Console.WriteLine($"CAN消息发送成功: ID: {id:X3}, CAN_Data: {BitConverter.ToString(data).Replace("-", " ")}");
            }
        }
    }
}
