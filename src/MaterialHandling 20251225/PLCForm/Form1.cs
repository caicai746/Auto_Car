using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static PLCForm.S7PLC;
using static PLCForm.PLCSignal;
using S7.Net;
using System.Net.NetworkInformation;

namespace PLCForm
{
    public partial class Form1 : Form
    {
        private static readonly object _lockObject = new object();
        public System.Windows.Forms.Timer PlcRWTimer = new System.Windows.Forms.Timer(); //plc数据读取定时器
        public PLCForm.PLCSignal CSignal = new PLCForm.PLCSignal();
        public S7PLC plc = new S7PLC(CpuType.S71500, "192.168.0.1", 0, 1, 50); //1500sp 插槽号为1，PLC300插槽号为2
        public bool plc_isconnected = false; //plc连接状态
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e) //页面加载
        {
            
            try

            {   

                PlcRWTimer.Interval = 2*1000; //2s
                PlcRWTimer.Tick += PlcRWTimer_Tick;

                rtb_Textshow.AppendText("初始化成功！\n");
            }
            catch {
                rtb_Textshow.AppendText("初始化失败！\n");
            }
        }

        private void PlcRWTimer_Tick(object sender, EventArgs e)
        {

            //rtb_Textshow.Clear();
            lock (_lockObject)
            {
                try
                {
                    plc.Receive_Control_Signal(CSignal); //接收控制信号
                }
                catch
                {

                }
                

                rtb_Textshow.AppendText("bool1:" + plc.Output_DB.bool1.ToString() + "\n");
                rtb_Textshow.AppendText("bool2:" + plc.Output_DB.bool2.ToString() + "\n");
                rtb_Textshow.AppendText("bool3:" + plc.Output_DB.bool3.ToString() + "\n");
                rtb_Textshow.AppendText("bool4:" + plc.Output_DB.bool4.ToString() + "\n");
                rtb_Textshow.AppendText("bool5:" + plc.Output_DB.bool5.ToString() + "\n");
                rtb_Textshow.AppendText("bool6:" + plc.Output_DB.bool6.ToString() + "\n");
                rtb_Textshow.AppendText("bool7:" + plc.Output_DB.bool7.ToString() + "\n");
                rtb_Textshow.AppendText("bool8:" + plc.Output_DB.bool8.ToString() + "\n");
                rtb_Textshow.AppendText("bool9:" + plc.Output_DB.bool9.ToString() + "\n");
                rtb_Textshow.AppendText("bool10:" + plc.Output_DB.bool10.ToString() + "\n");
                rtb_Textshow.AppendText("bool11:" + plc.Output_DB.bool11.ToString() + "\n");
                rtb_Textshow.AppendText("bool12:" + plc.Output_DB.bool12.ToString() + "\n");
                rtb_Textshow.AppendText("bool13:" + plc.Output_DB.bool13.ToString() + "\n");
                rtb_Textshow.AppendText("bool14:" + plc.Output_DB.bool14.ToString() + "\n");
                rtb_Textshow.AppendText("bool15:" + plc.Output_DB.bool15.ToString() + "\n");
                rtb_Textshow.AppendText("bool16:" + plc.Output_DB.bool16.ToString() + "\n");

                rtb_Textshow.AppendText("int16_1:" + plc.Output_DB.Int1.ToString() + "\n");
                rtb_Textshow.AppendText("int16_2:" + plc.Output_DB.Int2.ToString() + "\n");
                rtb_Textshow.AppendText("int16_3:" + plc.Output_DB.Int3.ToString() + "\n");
                rtb_Textshow.AppendText("int16_4:" + plc.Output_DB.Int4.ToString() + "\n");
                rtb_Textshow.AppendText("int16_5:" + plc.Output_DB.Int5.ToString() + "\n");
                rtb_Textshow.AppendText("int16_6:" + plc.Output_DB.Int6.ToString() + "\n");
                rtb_Textshow.AppendText("int16_7:" + plc.Output_DB.Int7.ToString() + "\n");
                rtb_Textshow.AppendText("int16_8:" + plc.Output_DB.Int8.ToString() + "\n");
                rtb_Textshow.AppendText("int16_9:" + plc.Output_DB.Int9.ToString() + "\n");
                rtb_Textshow.AppendText("int16_10:" + plc.Output_DB.Int10.ToString() + "\n");

                rtb_Textshow.AppendText("int32_1:" + plc.Output_DB.Dint1.ToString() + "\n");
                rtb_Textshow.AppendText("int32_2:" + plc.Output_DB.Dint2.ToString() + "\n");
                rtb_Textshow.AppendText("int32_3:" + plc.Output_DB.Dint3.ToString() + "\n");
                rtb_Textshow.AppendText("int32_4:" + plc.Output_DB.Dint4.ToString() + "\n");
                rtb_Textshow.AppendText("int32_5:" + plc.Output_DB.Dint5.ToString() + "\n");
                rtb_Textshow.AppendText("int32_6:" + plc.Output_DB.Dint6.ToString() + "\n");
                rtb_Textshow.AppendText("int32_7:" + plc.Output_DB.Dint7.ToString() + "\n");
                rtb_Textshow.AppendText("int32_8:" + plc.Output_DB.Dint8.ToString() + "\n");
                rtb_Textshow.AppendText("int32_9:" + plc.Output_DB.Dint9.ToString() + "\n");
                rtb_Textshow.AppendText("int32_10:" + plc.Output_DB.Dint10.ToString() + "\n");
                rtb_Textshow.Clear();
            }
        }



        private void btn_ClearTextshow_Click(object sender, EventArgs e)//清空文本框
        {
            rtb_Textshow.Clear();
        }

        private void btn_PLCopen_Click(object sender, EventArgs e)
        {
            plc.Start_PLC();
            PlcRWTimer.Start();
               
        }
    }
}
