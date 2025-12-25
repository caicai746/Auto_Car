using System;
using System.Linq;
using S7.Net;
using static PLCForm.PLCSignal;
using System.Threading;
using System.Net.NetworkInformation;
using System.Diagnostics;

namespace PLCForm
{
    public class S7PLC
    {

        public Timer read_timer;  //读定时器

        public Timer write_timer;  //写定时器

        public S7.Net.CpuType PLCcpu ; //cpu型号

        public string PLCip; //PLC运行的IP地址.

        public int sleeptime; //PLC频率 ms.

        public short PLCrack; //机架号

        public short PLCslot; //插槽号

        public Ping PLCping = new Ping(); //ping对象，用于检查PLC网络连接

        public bool PLC_NetState = false; //PLC网络连接状态.

        private static readonly object _lockObject = new object(); //锁对象

        //读写连接分离
        public Plc read_plc; //读PLC连接对象
        public Plc write_plc; //写PLC连接对象

        public long count1 = 0; //读计数.
        public long count2 = 0; //写计数.

        // new Plc(CpuType.S71500, PLC的IP, PLC的机架, PLC的插槽);

        public struct Data_Cache //数据缓存区
        {
            public int DB_number; //输入的数据块号
            public int bool_size;   //所有bool值占用的字节大小 
            public int short_size;  //所有int16值占用的字节大小 
            public int int_size;    //所有int32值占用的字节大小 

            //bool值数据区
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


            //int16值数据区
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

            //int32值数据区
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

        //v1.0 此PLC读写数据区是分离的
        public Data_Cache Output_DB; //输出缓存 用于读取 此用于DB1

        //创建读字节数组
        public byte[] Readbytes = new byte[62];
        // 创建写字节数组
        public byte[] Writebytes = new byte[62];// 2字节bool + 20字节short + 40字节int
        //各种类型变量的数组，用于按字节写入PLC
        public bool[] BoolData = new bool[16]; //16个bool型变量
        public short[] IntData = new short[10];//10个int16型变量
        public int[] DintData = new int[10]; //10个int32型变量
        public S7PLC(S7.Net.CpuType cpu, string TestServerIp, short rack,short slot,int time)
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

        public bool PLC_isconnected() //返回PLC连接状态
        {
            return read_plc.IsConnected && write_plc.IsConnected;
        }

        public bool Start_PLC() //PLC开启
        {
            //1）检查PLC网络连接
            PingReply PlcReply = PLCping.Send(PLCip);
            while ((PlcReply.Status != IPStatus.Success))
            {
                PLC_NetState = false;
                PlcReply = PLCping.Send(PLCip);
            }
            PLC_NetState = true;

            try
            {
                //2）开启读连接
                read_plc.Open();
                //开启写连接
                write_plc.Open();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"PLC开启失败: {ex.Message}"); 
            }

            write_timer = new Timer(Write_TimerTick, null, 0, sleeptime);
            read_timer = new Timer(Read_TimerTick, null, 0, sleeptime+5);
            return read_plc.IsConnected && write_plc.IsConnected;
        }

        public void Stop_PLC()  //PLC关闭
        {   
            read_timer.Change(-1, -1);
            write_timer.Change(-1, -1);
        }


        private void Read_TimerTick(object state)   //读定时器回调函数
        {
            /*
            方法：public byte[] ReadBytes(DataType dataType, int db, int startByteAdr, int count)
            入参：
                1）DataType数据类型，可选择从DB块或者Memory中读取；
                2）DB1：DataBlock=1,Memory=0；
                3）startByteAdr：起始地址，即DB块的起始偏移量；
                4）count:读取大小，该大小由读取的DB块的最后一个数据的偏移量和大小决定，
                  这里最后一个字节Dint偏移量为58，数据类型为Dint，2个字节，因此此次读取为58+2=62个字节。
            出参：Byte[],这里Byte[]的大小必然和count的大小是相同的，
            */
            lock (_lockObject)
            {   
                try
                {
                    //3）读取PLC数据
                    Readbytes = read_plc.ReadBytes(DataType.DataBlock, 1, 0, 62); //读取一个数据块，共62字节
                    count1++;
                    Console.WriteLine("read:" + count1);

                    //4）读Bool型
                    Output_DB.bool1 = Readbytes[0].SelectBit(0);
                    Output_DB.bool2 = Readbytes[0].SelectBit(1);
                    Output_DB.bool3 = Readbytes[0].SelectBit(2);
                    Output_DB.bool4 = Readbytes[0].SelectBit(3);
                    Output_DB.bool5 = Readbytes[0].SelectBit(4);
                    Output_DB.bool6 = Readbytes[0].SelectBit(5);
                    Output_DB.bool7 = Readbytes[0].SelectBit(6);
                    Output_DB.bool8 = Readbytes[0].SelectBit(7);
                    Output_DB.bool9 = Readbytes[1].SelectBit(0);
                    Output_DB.bool10 = Readbytes[1].SelectBit(1);
                    Output_DB.bool11 = Readbytes[1].SelectBit(2);
                    Output_DB.bool12 = Readbytes[1].SelectBit(3);
                    Output_DB.bool13 = Readbytes[1].SelectBit(4);
                    Output_DB.bool14 = Readbytes[1].SelectBit(5);
                    Output_DB.bool15 = Readbytes[1].SelectBit(6);
                    Output_DB.bool16 = Readbytes[1].SelectBit(7);

                    //5）读Int型
                    Output_DB.Int2 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(2).Take(2).ToArray());
                    Output_DB.Int2 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(4).Take(2).ToArray());
                    Output_DB.Int3 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(6).Take(2).ToArray());
                    Output_DB.Int4 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(8).Take(2).ToArray());
                    Output_DB.Int5 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(10).Take(2).ToArray());
                    Output_DB.Int6 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(12).Take(2).ToArray());
                    Output_DB.Int7 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(14).Take(2).ToArray());
                    Output_DB.Int8 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(16).Take(2).ToArray());
                    Output_DB.Int9 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(18).Take(2).ToArray());
                    Output_DB.Int10 = S7.Net.Types.Int.FromByteArray(Readbytes.Skip(20).Take(2).ToArray());

                    //6）读Dint型
                    Output_DB.Dint1 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(22).Take(4).ToArray());
                    Output_DB.Dint2 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(26).Take(4).ToArray());
                    Output_DB.Dint3 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(30).Take(4).ToArray());
                    Output_DB.Dint4 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(34).Take(4).ToArray());
                    Output_DB.Dint5 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(38).Take(4).ToArray());
                    Output_DB.Dint6 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(42).Take(4).ToArray());
                    Output_DB.Dint7 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(46).Take(4).ToArray());
                    Output_DB.Dint8 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(50).Take(4).ToArray());
                    Output_DB.Dint9 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(54).Take(4).ToArray());
                    Output_DB.Dint10 = S7.Net.Types.DInt.FromByteArray(Readbytes.Skip(58).Take(4).ToArray());
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"读取失败: {ex.Message}");
                    if (read_plc.IsConnected)
                        read_plc.Close();
                    Thread.Sleep(1000);//休眠一秒

                    PingReply PlcReply = PLCping.Send(PLCip);
                    while ((PlcReply.Status != IPStatus.Success))   //PLC断线
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
                        Debug.WriteLine($"PLC开启失败: {ex.Message}");
                    }
                }
            }
        }

        private void Write_TimerTick(object state) //写定时器回调函数
        {
            lock (_lockObject)
            {
                //准备写入数据
                // 1）将bool数据写入字节数组
                byte boolByte1 = 0, boolByte2 = 0;
                for (int i = 0; i < 16; i++) //16个bool变量
                {
                    if (BoolData[i])
                    {
                        if (i < 8)
                            boolByte1 |= (byte)(1 << i);
                        else
                            boolByte2 |= (byte)(1 << (i - 8));
                    }
                }
                Writebytes[0] = boolByte1; 
                Writebytes[1] = boolByte2;

                // 2）将int数据写入字节数组
                for (int i = 0; i < 10; i++) //10个int16型变量
                {
                    byte[] shortBytes = BitConverter.GetBytes(IntData[i]);
                    if (BitConverter.IsLittleEndian)// Windows系统是小端字节序，PLC是大端字节序，需要反转字节序
                    {
                        Array.Reverse(shortBytes);
                    }
                    Array.Copy(shortBytes, 0, Writebytes, 2 + i * 2, 2); //从第2字节开始
                }

                // 3）将Dint数据写入字节数组
                for (int i = 0; i < 10; i++) //10个int32型变量
                {
                    byte[] intBytes = BitConverter.GetBytes(DintData[i]);
                    if (BitConverter.IsLittleEndian)
                    {
                        Array.Reverse(intBytes);
                    }
                    Array.Copy(intBytes, 0, Writebytes, 22 + i * 4, 4);  //从第22字节开始
                }

                //执行写操作
                try
                {   
                    
                    write_plc.WriteBytes(DataType.DataBlock, 2, 0, Writebytes); //写入PLC
                    count2++;
                    Console.WriteLine("write:" + count2);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine($"写入失败:{ex.Message}");
                    if (write_plc.IsConnected)
                        write_plc.Close();
                    Thread.Sleep(1000);//休眠一秒

                    PingReply PlcReply = PLCping.Send(PLCip);
                    while ((PlcReply.Status != IPStatus.Success))   //PLC断线
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
                        Debug.WriteLine($"PLC开启失败: {ex.Message}");
                    }
                    
                }

            }
        }

        public void Receive_Control_Signal(PLCSignal input) //接收控制信号 参数为控制信号类
        {
            BoolData[0] = input.bool1;
            BoolData[1] = input.bool2;
            BoolData[2] = input.bool3;
            BoolData[3] = input.bool4;
            BoolData[4] = input.bool5;
            BoolData[5] = input.bool6;
            BoolData[6] = input.bool7;
            BoolData[7] = input.bool8;
            BoolData[8] = input.bool9;
            BoolData[9] = input.bool10;
            BoolData[10] = input.bool11;
            BoolData[11] = input.bool12;
            BoolData[12] = input.bool13;
            BoolData[13] = input.bool14;
            BoolData[14] = input.bool15;
            BoolData[15] = input.bool16;

            IntData[0] = input.Int1;
            IntData[1] = input.Int2;
            IntData[2] = input.Int3;
            IntData[3] = input.Int4;
            IntData[4] = input.Int5;
            IntData[5] = input.Int6;
            IntData[6] = input.Int7;
            IntData[7] = input.Int8;
            IntData[8] = input.Int9;
            IntData[9] = input.Int10;

            DintData[0] = input.Dint1;
            DintData[1] = input.Dint2;
            DintData[2] = input.Dint3;
            DintData[3] = input.Dint4;
            DintData[4] = input.Dint5;
            DintData[5] = input.Dint6;
            DintData[6] = input.Dint7;
            DintData[7] = input.Dint8;
            DintData[8] = input.Dint9;
            DintData[9] = input.Dint10;
        }
    }
}
