using System;
using System.Text;
using System.Threading;

namespace S7.Net
{
    public class PLCHelper : IDisposable
    {
        private static readonly object _lockObject = new object();
        private readonly Timer PlcRWTimer;//plc数据读取定时器
        private readonly PLCSignal CSignal = new PLCSignal();
        public S7PLC plc = new S7PLC(CpuType.S71500, "192.168.0.1", 0, 1, 50); //1500sp 插槽号为1，PLC300插槽号为2
        public bool plc_isconnected = false; //plc连接状态
        public bool IsConnected => plc?.IsConnected ?? false;
        public event Action<string> OnLogMessage; // 日志事件

        public PLCHelper()
        {
            plc = new S7PLC(CpuType.S71500, "192.168.0.1", 0, 1, 50);

            // 使用System.Timers.Timer替代Windows.Forms.Timer
            _plcTimer = new Timer(PlcTimerElapsed, null, Timeout.Infinite, 2000);
        }

        public void StartMonitoring()
        {
            try
            {
                plc.Start_PLC();
                _plcTimer.Change(0, 2000); // 启动定时器
                Log("plc监控已启动");
            }
            catch (Exception ex)
            {
                Log($"启动失败：{ex.Message}");
                throw;
            }
        }

        private void PlcTimerElapsed(object state)
        {
            lock (_lockObject)
            {
                try
                {
                    plc.Receive_Control_Signal(_cSignal);
                    ReadPlcData();
                }
                catch (Exception ex)
                {
                    Log($"读取异常：{ex.Message}");
                }
            }
        }

        private void ReadPlcData()
        {
            var output = plc.Output_DB;
            var sb = new StringBuilder();

            // 读取布尔值
            for (int i = 1; i <= 16; i++)
            {
                sb.AppendLine($"bool{i}: {GetPropertyValue(output, $"bool{i}")}");
            }

            // 读取16位整数
            for (int i = 1; i <= 10; i++)
            {
                sb.AppendLine($"int16_{i}: {GetPropertyValue(output, $"Int{i}")}");
            }

            // 读取32位整数
            for (int i = 1; i <= 10; i++)
            {
                sb.AppendLine($"int32_{i}: {GetPropertyValue(output, $"Dint{i}")}");
            }

            Log(sb.ToString());
        }

        private object GetPropertyValue(object src, string propName)
        {
            return src.GetType().GetProperty(propName)?.GetValue(src, null) ?? "N/A";
        }

        private void Log(string message)
        {
            OnLogMessage?.Invoke($"[{DateTime.Now:HH:mm:ss}] {message}");
        }

        public void Dispose()
        {
            _plcTimer?.Dispose();
            plc?.Dispose();
        }
    }

    // 以下是需要从原项目迁移的依赖类（保持原有实现）
    public class PLCSignal { /* 原PLCSignal实现 */ }
    public S7PLC(S7.Net.CpuType cpu, string TestServerIp, short rack, short slot, int time)
    {
        PLCcpu = cpu;
        PLCip = TestServerIp;
        PLCrack = rack;
        PLCslot = slot;
        sleeptime = time;

        Output_DB.DB_number = 1;
        Output_DB.bool_size = 2;
        Output_DB.short_size = 20;
        Output_DB.int_size = 40;

        //创建读写连接对象
        read_plc = new Plc(cpu, TestServerIp, rack, slot);
        write_plc = new Plc(cpu, TestServerIp, rack, slot);

    }

    public class OutputDB
    {
        // 根据实际情况定义所有属性
        public bool bool1 { get; set; }
        // ...其他bool属性
        public int Int1 { get; set; }
        // ...其他Int属性
        public int Dint1 { get; set; }
        // ...其他Dint属性
    }
}
