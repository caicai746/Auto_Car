using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using System.Diagnostics;
using System.IO;

namespace MaterialHandling.MaterialHandlingDAL.Entity.QueryForm
{
    public class PagingForQueryTable
    {
        private DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
        private QueryParameter queryPara;           //查询参数        
        public string sql;
        /// <summary>
        /// 获取查询结果记录总数
        /// </summary>
        /// <param name="queryParameter"></param>
        /// <returns></returns>
        public int GetQueryCount(QueryParameter queryParameter)
        {
            
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            this.queryPara = queryParameter;
            var queryList = (from PB in DBCon.Production_Batch
                             join PT in DBCon.Parameter_Type
                             on PB.typeID equals PT.typeid
                            // join PW in DBCon.Production_Invoice
                            // on PB.Invoice_No equals PW.Invoice_No
                             //join PB in DBCon.Production_Batch
                             //on PI.GUID.ToString() equals PB.InvoiceID
                             where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&     
                                  (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                  (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                  (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                  (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                  (String.IsNullOrEmpty(queryPara.typeNo) || PT.ModelID == queryPara.typeNo) &&
                                  (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)&&
                                  (String.IsNullOrEmpty(queryPara.materialNO) || PB.Material_Number== queryPara.materialNO)
                             select new
                            {
                                开始时间 = PB.Start_Time,
                              //  充电完成时间=PB.Finish_Charging_Time,
                                发货单号 = PB.Invoice_No,
                                订单号 = PB.Contract_No,
                                流水号 = PB.Serial_Number,
                                物料号=PB.Material_Number,
                                批号 = PB.Batch_No,
                                型号 = PT.ModelID,
                                数量 = PB.Battery_Amount,
                                BatchID = PB.GUID.ToString(),
                                
                            });
            //if (queryPara.flag)
            //{
            //    queryList = queryList.Where(x => x.开始时间 > queryPara.startTime && x.开始时间 < queryPara.endTime);
            //}

            //if (!String.IsNullOrEmpty(queryPara.batchNo))
            //{
            //    queryList = queryList.Where(x => x.批号 == queryPara.batchNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.contractNo))
            //{
            //    queryList = queryList.Where(x => x.订单号 == queryPara.contractNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.invoiceNo))
            //{
            //    queryList = queryList.Where(x => x.发货单号 == queryPara.invoiceNo);
            //}

            //if (queryPara.serialNo != -1)
            //{
            //    queryList = queryList.Where(x => x.流水号 == queryPara.serialNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.typeNo))
            //{
            //    queryList = queryList.Where(x => x.型号 == queryPara.typeNo);
            //}
            //sw.Stop();
            //Console.WriteLine(sw.Elapsed.ToString());
            return queryList.Count();
        }

        /// <summary>
        /// 分页查询结果
        /// </summary>
        /// <param name="pageSize">每页显示数量</param>
        /// <param name="pageNum">页数</param>
        /// <returns></returns>
        public void GetPagingQuery(int pageSize, int pageNum, DataGridView dataGridView)
        {

            Stopwatch sw = new Stopwatch();
            sw.Start();
            var queryList = (from PB in DBCon.Production_Batch
                             join PT in DBCon.Parameter_Type
                             on PB.typeID equals PT.typeid
                           //  join PW in DBCon.Production_Invoice
                          //   on PB.Invoice_No equals PW.Invoice_No
                                 //join PB in DBCon.Production_Batch
                                 //on PI.GUID.ToString() equals PB.InvoiceID
                             where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                  (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                  (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                  (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                  (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                  (String.IsNullOrEmpty(queryPara.typeNo) || PT.ModelID == queryPara.typeNo) &&
                                  (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)&&
                                  (String.IsNullOrEmpty(queryPara.materialNO) || PB.Material_Number == queryPara.materialNO)


                             select new
                            {
                                开始时间 = PB.Start_Time,
                                 充电时间 = PB.Finish_Charging_Time,
                                 发货单号 = PB.Invoice_No,
                                订单号 = PB.Contract_No,
                                流水号 = PB.Serial_Number,
                                物料号=PB.Material_Number == null? "NULL":PB.Material_Number,
                                批号 = PB.Batch_No,
                                型号 = PT.ModelID,
                                检测机 = PB.Station_Address,
                                数量 = PB.Battery_Amount,
                                实际检测数量 = PB.Actual_Detection_Amount,
                                算法 = PB.Algorithm,
                                电压最大值 = PB.Voltage_Max,
                                电压最小值 = PB.Voltage_Min,
                                电压平均值 = PB.Voltage_Avg,
                                电压偏差值 = PB.Voltage_Dev,
                                电压上限值 = PB.Voltage_Up,
                                电压下限值 = PB.Voltage_Lower,
                                内阻最大值 = PB.Resistance_Max,
                                内阻最小值 = PB.Resistance_Min,
                                内阻平均值 = PB.Resistance_Avg,
                                内阻偏差值 = PB.Resistance_Dev,
                                内阻上限值 = PB.Resistance_Up,
                                内阻下限值 = PB.Resistance_Lower,
                                有效记录数量 = PB.Valid_Number,
                                正常电池数量 = PB.Normal_Number,
                                超电压下限电池数量 = PB.V_Lower_Number,
                                超电压上限电池数量 = PB.V_UP_Number,
                                超内阻下限电池数量 = PB.R_Lower_Number,
                                超内阻上限电池数量 = PB.R_UP_Number,
                                typeid = PT.typeid.ToString(),
                                BatchID = PB.GUID.ToString(),
                                //InvoiceID = PI.GUID.ToString()
                            });
            //if (queryPara.flag)
            //{
            //    queryList = queryList.Where(x => x.开始时间 > queryPara.startTime && x.开始时间 < queryPara.endTime);
            //}

            //if (!String.IsNullOrEmpty(queryPara.batchNo))
            //{
            //    queryList = queryList.Where(x => x.批号 == queryPara.batchNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.contractNo))
            //{
            //    queryList = queryList.Where(x => x.订单号 == queryPara.contractNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.invoiceNo))
            //{
            //    queryList = queryList.Where(x => x.发货单号 == queryPara.invoiceNo);
            //}

            //if (queryPara.serialNo != -1)
            //{
            //    queryList = queryList.Where(x => x.流水号 == queryPara.serialNo);
            //}

            //if (!String.IsNullOrEmpty(queryPara.typeNo))
            //{
            //    queryList = queryList.Where(x => x.型号 == queryPara.typeNo);
            //}
            queryList = queryList.OrderByDescending(x => x.开始时间).ThenByDescending(x => x.发货单号).Skip(pageSize * (pageNum - 1)).Take(pageSize);
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            //WriteMwssage("sql查询时间" + ts);
            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            int i = 0;
            int j = (pageNum - 1) * pageSize;
            foreach (var a in queryList)
            {
                int index = dataGridView.Rows.Add();
                
                dataGridView.Rows[i].Cells[0].Value = (j + 1).ToString("000");
                dataGridView.Rows[i].Cells[1].Value = a.开始时间;
                dataGridView.Rows[i].Cells[2].Value = a.充电时间;
                dataGridView.Rows[i].Cells[3].Value = a.发货单号;
                dataGridView.Rows[i].Cells[4].Value = a.订单号;
                dataGridView.Rows[i].Cells[5].Value = a.流水号;
                
                dataGridView.Rows[i].Cells[6].Value = a.批号;
                dataGridView.Rows[i].Cells[7].Value = a.型号;
                dataGridView.Rows[i].Cells[8].Value = a.物料号;
                dataGridView.Rows[i].Cells[9].Value = a.检测机+"#";
                dataGridView.Rows[i].Cells[10].Value = a.数量;
                dataGridView.Rows[i].Cells[11].Value = a.有效记录数量;
                dataGridView.Rows[i].Cells[12].Value = a.实际检测数量;
                dataGridView.Rows[i].Cells[13].Value = a.算法;
                dataGridView.Rows[i].Cells[14].Value ="明细";
                dataGridView.Rows[i].Cells[15].Value = a.超电压下限电池数量;
                dataGridView.Rows[i].Cells[16].Value = a.超电压上限电池数量;
                dataGridView.Rows[i].Cells[17].Value = a.超内阻下限电池数量;
                dataGridView.Rows[i].Cells[18].Value = a.超内阻上限电池数量;
                dataGridView.Rows[i].Cells[19].Value = Convert.ToDouble(a.电压最大值).ToString("F3");
                dataGridView.Rows[i].Cells[20].Value = Convert.ToDouble(a.电压最小值).ToString("F3");
                dataGridView.Rows[i].Cells[21].Value = Convert.ToDouble(a.电压平均值).ToString("F3");
                dataGridView.Rows[i].Cells[22].Value = Convert.ToDouble(a.电压偏差值).ToString("F3");
                dataGridView.Rows[i].Cells[23].Value = Convert.ToDouble(a.电压上限值).ToString("F3");
                dataGridView.Rows[i].Cells[24].Value = Convert.ToDouble(a.电压下限值).ToString("F3");
                dataGridView.Rows[i].Cells[25].Value = Convert.ToDouble(a.内阻最大值).ToString("F3");
                dataGridView.Rows[i].Cells[26].Value = Convert.ToDouble(a.内阻最小值).ToString("F3");
                dataGridView.Rows[i].Cells[27].Value = Convert.ToDouble(a.内阻平均值).ToString("F3");
                dataGridView.Rows[i].Cells[28].Value = Convert.ToDouble(a.内阻偏差值).ToString("F3");
                dataGridView.Rows[i].Cells[29].Value = Convert.ToDouble(a.内阻上限值).ToString("F3");
                dataGridView.Rows[i].Cells[30].Value = Convert.ToDouble(a.内阻下限值).ToString("F3");

                dataGridView.Rows[i].Cells[31].Value = a.BatchID;
                //dataGridView.Rows[i].Cells[31].Value = a.InvoiceID;
                i++;
                j++;
            }

            sw1.Stop();
            TimeSpan ts1 = sw1.Elapsed;
            //WriteMwssage("datagrid填充时间:" + ts1);

        }
        public int GetLogQueryCount(QueryParameter queryParameter)
        {
            System.Diagnostics.Stopwatch sw = new System.Diagnostics.Stopwatch();
            sw.Start();
            this.queryPara = queryParameter;
            var list_Log = (from s in DBCon.LogDetail
                            select new
                            {
                                序号 = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            });
            if (queryPara.flag)
            {
                list_Log = list_Log.Where(x => x.登录时间 > queryPara.startTime && x.登录时间 < queryPara.endTime);
            }
            if (!String.IsNullOrEmpty(queryPara.contractNo))
            {
                list_Log = list_Log.Where(x => x.用户名 == queryPara.username);
            }
            sw.Stop();
            Console.WriteLine(sw.Elapsed.ToString());
            return list_Log.Count();

        }
        public void GetLogPaggQuery(int pageSize, int pageNum, DataGridView dataGridView)
        {
            var list_Log = (from s in DBCon.LogDetail

                            select new
                            {
                                LogId = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            });
        
            if (queryPara.flag)
            {
                list_Log = list_Log.Where(x => x.登录时间 > queryPara.startTime && x.登录时间 < queryPara.endTime);
            }
            if (!String.IsNullOrEmpty(queryPara.username))
            {
                list_Log = list_Log.Where(x => x.用户名 == queryPara.username);
            }
            list_Log = list_Log.OrderByDescending(x => x.登录时间).Skip(pageSize * (pageNum - 1)).Take(pageSize);
            int i = 0;
            int j = (pageNum - 1) * pageSize;
            foreach (var a in list_Log)
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = (j + 1).ToString("000");
                dataGridView.Rows[i].Cells[1].Value = a.登录时间;
                dataGridView.Rows[i].Cells[2].Value = a.用户名;
                dataGridView.Rows[i].Cells[3].Value = a.操作;
                dataGridView.Rows[i].Cells[4].Value = a.内容;
                dataGridView.Rows[i].Cells[5].Value = a.LogId;
                i++;
                j++;
            }
        }
        public void WriteMwssage(String msg)
        {
            using (FileStream fs = new FileStream("Log//QueryTime.txt", FileMode.OpenOrCreate, FileAccess.Write))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.BaseStream.Seek(0, SeekOrigin.End);
                    sw.WriteLine("{0}\n", msg, DateTime.Now);
                    sw.Flush();
                }
            }
        }
        //public void GetQuery(int pageSnuv, int pagetol, List<DetailAndBatchInfo> list)
        //    {

        //        var queryList = from PI in DBCon.Production_Invoice
        //                        join PB in DBCon.Production_Batch
        //                        on PI.GUID.ToString() equals PB.InvoiceID
        //                        join PT in DBCon.Parameter_Type
        //                        on PB.TypeID equals PT.GUID.ToString()

        //                        select new
        //                        {
        //                            开始时间 = PI.Test_Time,
        //                            发货单号 = PI.Invoice_No,
        //                            订单号 = PI.Contract_No,
        //                            流水号 = PI.Serial_Number,
        //                            批号 = PB.Batch_No,
        //                            型号 = PT.ModelID,
        //                            //检测机 = PC.Station_Address,
        //                            //检测机 = ProductionCheckDB.QueryOneData("Select * from Production_Check Where BatchID={0}",PB.GUID).Station_Address,
        //                            数量 = PB.Battery_Amount,
        //                            实际检测数量 = PB.Actual_Detection_Amount,
        //                            算法 = PB.Algorithm,
        //                            电压最大值 = PB.Voltage_Max,
        //                            电压最小值 = PB.Voltage_Min,
        //                            电压平均值 = PB.Voltage_Avg,
        //                            电压偏差值 = PB.Voltage_Dev,
        //                            电压上限值 = PB.Voltage_Up,
        //                            电压下限值 = PB.Voltage_Lower,
        //                            内阻最大值 = PB.Resistance_Max,
        //                            内阻最小值 = PB.Resistance_Min,
        //                            内阻平均值 = PB.Resistance_Avg,
        //                            内阻偏差值 = PB.Resistance_Dev,
        //                            内阻上限值 = PB.Resistance_Up,
        //                            内阻下限值 = PB.Resistance_Lower,
        //                            有效记录数量 = PB.Valid_Number,
        //                            正常电池数量 = PB.Normal_Number,
        //                            超电压下限电池数量 = PB.V_Lower_Number,
        //                            超电压上限电池数量 = PB.V_UP_Number,
        //                            超内阻下限电池数量 = PB.R_Lower_Number,
        //                            超内阻上限电池数量 = PB.R_UP_Number,

        //                            BatchID = PB.GUID.ToString(),
        //                            InvoiceID = PI.GUID.ToString()
        //                        };
        //    }



    }

    public class QueryParameter
    {
        public bool flag;           //时间控件是否启用
        public bool flagcharge;      //充电时间空间是否启用
        public DateTime startTime;  //开始时间
        public DateTime endTime;    //结束时间
        public DateTime Charging_startTime;   //充电开始时间
        public DateTime Charging_endTime;     //充电结束时间
        public string batchNo;      //批号
        public string contractNo;   //订单号
        public string invoiceNo;    //发货单号
        public int serialNo;        //流水号
        public string typeNo;       //电池型号
        public int statinaddress;  //检测机
        public string username;
        public string LogId;//用户名
        public string materialNO;//物料号
        public int flag1;//查询1 or 查询2
    }
}
