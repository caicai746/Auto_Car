using System;
using System.Linq;
using System.Threading;
using System.Net.NetworkInformation;
using System.Diagnostics;
using S7.Net;
namespace MaterialHandling.MaterialHandlingPLC
{
    public class S7PLC
    {
        public Timer read_timer;  // 读定时器
        public Timer write_timer;  // 写定时器
        public S7.Net.CpuType PLCcpu; // cpu 型号
        public string PLCip; // PLC 运行的 IP 地址
        public int sleeptime; // PLC 频率 ms
        public short PLCrack; // 机架号
        public short PLCslot; // 插槽号
        public Ping PLCping = new Ping(); // ping 对象，用于检查 PLC 网络连接
        public bool PLC_NetState = false; // PLC 网络连接状态
        private static readonly object _lockObject = new object(); // 锁对象

        // 读写连接分离
        public Plc read_plc; // 读 PLC 连接对象
        public Plc write_plc; // 写 PLC 连接对象
        public long count1 = 0; // 读计数
        public long count2 = 0; // 写计数

        public struct Data_Cache // 数据缓存区
        {
            public int DB_number; // 输入的数据块号
            public int bool_size;   // 所有 bool 值占用的字节大小
            public int short_size;  // 所有 short 值占用的字节大小
            public int int_size;    // 所有 int 值占用的字节大小

            // bool 值数据区
            public bool bool1;
            public bool bool2;
            public bool bool3;
            public bool bool4;
            public bool bool5;
            public bool bool6;
            public bool bool7;
            public bool bool8;
            public bool bool9;
            public bool bool10;
            public bool bool11;
            public bool bool12;
            public bool bool13;
            public bool bool14;
            public bool bool15;
            public bool bool16;
            public bool bool17;
            public bool bool18;
            public bool bool19;
            public bool bool20;
            public bool bool21;
            public bool bool22;
            public bool bool23;
            public bool bool24;
            public bool bool25;
            public bool bool26;
            public bool bool27;
            public bool bool28;
            public bool bool29;
            public bool bool30;
            public bool bool31;
            public bool bool32;

            // short 值数据区
            public short Int1;
            public short Int2;
            public short Int3;
            public short Int4;
            public short Int5;
            public short Int6;
            public short Int7;
            public short Int8;
            public short Int9;
            public short Int10;

            // int 值数据区
            public int Dint1;
            public int Dint2;
            public int Dint3;
            public int Dint4;
            public int Dint5;
            public int Dint6;
            public int Dint7;
            public int Dint8;
            public int Dint9;
            public int Dint10;
        };

        public Data_Cache Output_DB; // 输出缓存 用于读取 此用于 DB200
        public byte[] Readbytes = new byte[4 + 20 + 40]; // 4字节bool + 20字节short + 40字节int = 64字节
        public byte[] Writebytes = new byte[4 + 20 + 40]; // 4字节bool + 20字节short + 40字节int = 64字节

        //用于写入
        public bool[] BoolData = new bool[32]; // 32个bool型变量
        public short[] IntData = new short[10]; // 10个short型变量
        public int[] DintData = new int[10]; // 10个int型变量

        public S7PLC(S7.Net.CpuType cpu, string TestServerIp, short rack, short slot, int time)
        {
            PLCcpu = cpu;
            PLCip = TestServerIp;
            PLCrack = rack;
            PLCslot = slot;
            sleeptime = time;

            //跟变量设置有关
            Output_DB.DB_number = 200;
            Output_DB.bool_size = 4; // 32个bool占用4字节
            Output_DB.short_size = 20; // 10个short占用20字节
            Output_DB.int_size = 40; // 10个int占用40字节

            // 创建读写连接对象
            read_plc = new Plc(cpu, TestServerIp, rack, slot);
            write_plc = new Plc(cpu, TestServerIp, rack, slot);
        }

        public bool PLC_isconnected() // 返回 PLC 连接状态
        {
            return read_plc.IsConnected && write_plc.IsConnected;
        }

        public bool Start_PLC() // PLC 开启
        {
            // 1）检查 PLC 网络连接
            PingReply PlcReply = PLCping.Send(PLCip);
            while ((PlcReply.Status != IPStatus.Success))
            {
                PLC_NetState = false;
                PlcReply = PLCping.Send(PLCip);
            }
            PLC_NetState = true;

            try
            {
                // 2）开启读连接
                read_plc.Open();
                // 开启写连接
                write_plc.Open();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PLC 开启失败: {ex.Message}");
            }

            write_timer = new Timer(Write_TimerTick, null, 0, sleeptime);
            read_timer = new Timer(Read_TimerTick, null, 0, sleeptime + 5);
            return read_plc.IsConnected && write_plc.IsConnected;
        }

        public void Stop_PLC() // PLC 关闭
        {
            read_timer.Change(-1, -1);
            write_timer.Change(-1, -1);
        }

        private void Read_TimerTick(object state) // 读定时器回调函数
        {
            lock (_lockObject)
            {
                try
                {
                    // 3）读取 PLC 数据
                    Readbytes = read_plc.ReadBytes(DataType.DataBlock, 200, 0, 64); // 读取一个数据块，共 4+20+40 = 64 字节
                    count1++;
                    Console.WriteLine("read:" + count1);

                    // 4）读 Bool 型
                    for (int i = 0; i < 32; i++)
                    {
                        int byteIndex = i / 8; // 字节索引
                        int bitIndex = i % 8;  // 位索引
                        switch (i)
                        {
                            case 0: Output_DB.bool1 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 1: Output_DB.bool2 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 2: Output_DB.bool3 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 3: Output_DB.bool4 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 4: Output_DB.bool5 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 5: Output_DB.bool6 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 6: Output_DB.bool7 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 7: Output_DB.bool8 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 8: Output_DB.bool9 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 9: Output_DB.bool10 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 10: Output_DB.bool11 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 11: Output_DB.bool12 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 12: Output_DB.bool13 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 13: Output_DB.bool14 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 14: Output_DB.bool15 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 15: Output_DB.bool16 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 16: Output_DB.bool17 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 17: Output_DB.bool18 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 18: Output_DB.bool19 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 19: Output_DB.bool20 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 20: Output_DB.bool21 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 21: Output_DB.bool22 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 22: Output_DB.bool23 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 23: Output_DB.bool24 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 24: Output_DB.bool25 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 25: Output_DB.bool26 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 26: Output_DB.bool27 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 27: Output_DB.bool28 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 28: Output_DB.bool29 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 29: Output_DB.bool30 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 30: Output_DB.bool31 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                            case 31: Output_DB.bool32 = Readbytes[byteIndex].SelectBit(bitIndex); break;
                        }
                    }

                    // 5）读 Int 型
                    Output_DB.Int1 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(4).Take(2).ToArray());
                    Output_DB.Int2 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(6).Take(2).ToArray());
                    Output_DB.Int3 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(8).Take(2).ToArray());
                    Output_DB.Int4 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(10).Take(2).ToArray());
                    Output_DB.Int5 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(12).Take(2).ToArray());
                    Output_DB.Int6 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(14).Take(2).ToArray());
                    Output_DB.Int7 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(16).Take(2).ToArray());
                    Output_DB.Int8 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(18).Take(2).ToArray());
                    Output_DB.Int9 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(20).Take(2).ToArray());
                    Output_DB.Int10 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(22).Take(2).ToArray());

                    // 6）读 Dint 型
                    Output_DB.Dint1 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(24).Take(4).ToArray());
                    Output_DB.Dint2 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(28).Take(4).ToArray());
                    Output_DB.Dint3 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(32).Take(4).ToArray());
                    Output_DB.Dint4 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(36).Take(4).ToArray());
                    Output_DB.Dint5 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(40).Take(4).ToArray());
                    Output_DB.Dint6 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(44).Take(4).ToArray());
                    Output_DB.Dint7 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(48).Take(4).ToArray());
                    Output_DB.Dint8 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(52).Take(4).ToArray());
                    Output_DB.Dint9 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(56).Take(4).ToArray());
                    Output_DB.Dint10 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(60).Take(4).ToArray());
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"读取失败: {ex.Message}");
                    if (read_plc.IsConnected)
                        read_plc.Close();
                    Thread.Sleep(1000); // 休眠一秒

                    PingReply PlcReply = PLCping.Send(PLCip);
                    while ((PlcReply.Status != IPStatus.Success)) // PLC 断线
                    {
                        PLC_NetState = false;
                        PlcReply = PLCping.Send(PLCip);
                    }
                    PLC_NetState = true;
                    try
                    {
                        read_plc.Open();
                    }
                    catch
                    {
                        Debug.WriteLine($"PLC 开启失败: {ex.Message}");
                    }
                }
            }
        }

        private void Write_TimerTick(object state) // 写定时器回调函数
        {
            lock (_lockObject)
            {
                // 准备写入数据
                // 1）将bool数据写入字节数组
                byte[] boolBytes = new byte[4];
                for (int i = 0; i < 32; i++)
                {
                    int byteIndex = i / 8;
                    int bitIndex = i % 8;
                    if (BoolData[i])
                    {
                        boolBytes[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }
                Array.Copy(boolBytes, 0, Writebytes, 0, 4);

                // 2）将short数据写入字节数组
                for (int i = 0; i < 10; i++)
                {
                    byte[] shortBytes = BitConverter.GetBytes(IntData[i]);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(shortBytes);
                    }
                    Array.Copy(shortBytes, 0, Writebytes, 4 + i * 2, 2);
                }

                // 3）将int数据写入字节数组
                for (int i = 0; i < 10; i++)
                {
                    byte[] intBytes = BitConverter.GetBytes(DintData[i]);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(intBytes);
                    }
                    Array.Copy(intBytes, 0, Writebytes, 4 + 20 + i * 4, 4);
                }

                // 执行写操作
                try
                {
                    write_plc.WriteBytes(DataType.DataBlock, 100, 0, Writebytes.Take(64).ToArray());
                    count2++;
                    Console.WriteLine("write:" + count2);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"写入失败: {ex.Message}");
                    if (write_plc.IsConnected)
                        write_plc.Close();
                    Thread.Sleep(1000); // 休眠一秒

                    PingReply PlcReply = PLCping.Send(PLCip);
                    while ((PlcReply.Status != IPStatus.Success)) // PLC 断线
                    {
                        PLC_NetState = false;
                        PlcReply = PLCping.Send(PLCip);
                    }
                    PLC_NetState = true;
                    try
                    {
                        write_plc.Open();
                    }
                    catch
                    {
                        Debug.WriteLine($"PLC 开启失败: {ex.Message}");
                    }
                }
            }
        }

        public void Receive_Control_Signal(PLCSignal input) // 接收控制信号
        {
            for (int i = 0; i < 32; i++)
            {
                BoolData[i] = (bool)input.GetType().GetField($"bool{i + 1}").GetValue(input);
            }

            for (int i = 0; i < 10; i++)
            {
                IntData[i] = (short)input.GetType().GetField($"Int{i + 1}").GetValue(input);
            }

            for (int i = 0; i < 10; i++)
            {
                DintData[i] = (int)input.GetType().GetField($"Dint{i + 1}").GetValue(input);
            }
        }
    }
}