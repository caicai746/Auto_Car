using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;

namespace MaterialHandling.MaterialHandlingDAL.Entity.QueryForm

{
    public class PagingQuery
    {
        public int invoiceSerialNumber;
        public string invoiceComboBoxText;

        public DateTime typeTime1;
        public DateTime typeTime2;
        public string typeComboBoxText;

        public DateTime batchTime1;
        public DateTime batchTime2;
        public string batchComboBoxText;

        public string commonComboBoxInvoice;
        public int commonComboBoxSerial;
        public string commonComboBoxType;
        public string commonComboBoxBatch;

        public List<float> dataShowChart = new List<float>();

        DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();

        private void GetInvoiceParameter( int serialNumber, string comboBoxText ) {
            this.invoiceSerialNumber = serialNumber;
            this.invoiceComboBoxText = comboBoxText;
        }

        private void GetTypeParameter( DateTime time1, DateTime time2, string type ) {
            this.typeTime1 = time1;
            this.typeTime2 = time2;
            this.typeComboBoxText = type;
        }

        public void GetBatchParameter( DateTime time1, DateTime time2, string comboBoxText ) {
            this.batchTime1 = time1;
            this.batchTime2 = time2;
            this.batchComboBoxText = comboBoxText;
        }

        public void GetCommonParameter( string comboBoxInvoice, int comboBoxSerial, string comboBoxType, string comboBoxBatch ) {
            this.commonComboBoxInvoice = comboBoxInvoice;
            this.commonComboBoxSerial = comboBoxSerial;
            this.commonComboBoxType = comboBoxType;
            this.commonComboBoxBatch = comboBoxBatch;
        }

        public void SetDataGridView( IQueryable list, DataGridView dataGridView ) {

            try {
                dataGridView.DataSource = list;
            } catch {

            }
        }

        Stopwatch sw = new Stopwatch();
        TimeSpan ts;
        public void StartTiming() {
            sw.Restart();
        }

        public void EndTiming( string whichQuery, string queryMode ) {
            sw.Stop();
            ts = sw.Elapsed;
            Console.WriteLine("----{0}--{1}--花费时间{2}ms", whichQuery, queryMode, ts.TotalMilliseconds);
        }
        public void EndTiming() {
            sw.Stop();
            ts = sw.Elapsed;
            Console.WriteLine("花费时间{0}ms", ts.TotalMilliseconds);
        }

        //发货单号查询总数
        public int GetInvoicePageCount( int serialNumber, string comboBoxText ) {
            StartTiming();

            GetInvoiceParameter(serialNumber, comboBoxText);
            var listStart = (from PB in DBCon.Production_Batch
                             //join PB in DBCon.Production_Batch
                             //on PI.Invoice_No equals PB.InvoiceID
                             join PC in DBCon.Production_Check
                             on PB.Batch_No equals PC.BatchID
                             //where (String.IsNullOrEmpty(invoiceComboBoxText) || PI.Invoice_No == invoiceComboBoxText) &&
                             //(invoiceSerialNumber == -1 || PI.Serial_Number == invoiceSerialNumber)
                             where PC.Flag != 3
                             select new {
                                 发货单号 = PB.Invoice_No,
                                 订单号 = PB.Contract_No,
                                 流水号 = PB.Serial_Number,
                                 电池批号 = PB.Batch_No,
                                 检测数量 = PB.Battery_Amount,
                                 电池型号 = PB.ModelID,
                                 检测编号 = PC.Number,
                                 开路电压 = PC.Open_Voltage,
                                 检测时间 = PC.Test_Time,
                                 检测人 = PB.UserID
                             });
            if( !String.IsNullOrEmpty(invoiceComboBoxText) ) {
                listStart = listStart.Where(x => x.发货单号 == invoiceComboBoxText);
            }
            if( invoiceSerialNumber != -1 ) {
                listStart = listStart.Where(x => x.流水号 == invoiceSerialNumber);
            }
            int listStartCount = listStart.Count();

            EndTiming("发货单号", "总数查询");
            return listStartCount;
        }
        //发货单号查询分页
        public void GetInvoicePageResult( int pageSize, int pageNum, DataGridView dataGridView ) {
            //StartTiming();

            //var listSelect = (from PI in DBCon.Production_Invoice
            //                  join PB in DBCon.Production_Batch
            //                  on PI.Invoice_No equals PB.InvoiceID
            //                  join PC in DBCon.Production_Check
            //                  on PB.Batch_No equals PC.BatchID
            //                  //where (String.IsNullOrEmpty(invoiceComboBoxText) || PI.Invoice_No == invoiceComboBoxText) &&
            //                  //(invoiceSerialNumber == -1 || PI.Serial_Number == invoiceSerialNumber)
            //                  where PC.Flag != 3
            //                  select new {
            //                      发货单号 = PI.Invoice_No,
            //                      订单号 = PI.Contract_No,
            //                      流水号 = PI.Serial_Number,
            //                      电池批号 = PB.Batch_No,
            //                      电池型号 = PB.TypeID,
            //                      检测数量 = PB.Battery_Amount,
            //                      检测编号 = PC.Number,
            //                      开路电压 = PC.Open_Voltage,
            //                      检测时间 = PC.Test_Time,
            //                      检测人 = PI.UserID
            //                  });
            //if( !String.IsNullOrEmpty(invoiceComboBoxText) ) {
            //    listSelect = listSelect.Where(x => x.发货单号 == invoiceComboBoxText);
            //}
            //if( invoiceSerialNumber != -1 ) {
            //    listSelect = listSelect.Where(x => x.流水号 == invoiceSerialNumber);
            //}
            //listSelect = listSelect.OrderByDescending(x => x.检测时间).Skip(pageSize * (pageNum - 1)).Take(pageSize);

            //SetDataGridView(listSelect, dataGridView);
            //EndTiming("发货单号", "单页查询");
            var listQuery = from PB in DBCon.Production_Batch
                            //join PI in DBCon.Production_Invoice
                            //on PB.InvoiceID equals PI.Invoice_No
                            select new {
                                开始时间 = PB.Start_Time,
                                发货单号 = PB.Invoice_No,
                                订单号 = PB.Contract_No,
                                流水号 = PB.Serial_Number,
                                批号 = PB.Batch_No,
                                型号 = PB.ModelID,
                                数量 = PB.Battery_Amount
                            };
            SetDataGridView(listQuery, dataGridView);
        }

        //电池型号查询总数
        public int GetTypePageCount( DateTime time1, DateTime time2, string type ) {
            GetTypeParameter(time1, time2, type);
            var listStart = (from PB in DBCon.Production_Batch
                             //join PB in DBCon.Production_Batch
                             //on PI.Invoice_No equals PB.InvoiceID
                             join PC in DBCon.Production_Check
                             on PB.Batch_No equals PC.BatchID
                             //where (typeTime1.ToString() != typeTime2.ToString() &&
                             //(PI.Test_Time >= typeTime1 && PI.Test_Time <= typeTime2)) &&
                             //(String.IsNullOrEmpty(typeComboBoxText) || PB.TypeID == typeComboBoxText)
                             where PC.Flag != 3
                             select new {
                                 发货单号 = PB.Invoice_No,
                                 订单号 = PB.Contract_No,
                                 流水号 = PB.Serial_Number,
                                 电池批号 = PB.Batch_No,
                                 电池型号 = PB.ModelID,
                                 检测数量 = PB.Battery_Amount,
                                 检测编号 = PC.Number,
                                 开路电压 = PC.Open_Voltage,
                                 检测时间 = PC.Test_Time,
                                 //检测人 = PI.UserID
                             });

            if( typeTime1.ToString() != typeTime2.ToString() ) {
                listStart = listStart.Where(x => x.检测时间 >= typeTime1 && x.检测时间 <= typeTime2);
            }
            if( !String.IsNullOrEmpty(typeComboBoxText) ) {
                listStart = listStart.Where(x => x.电池型号 == typeComboBoxText);
            }

            return listStart.Count();
        }

        //电池查询分页
        public void GetTypePageResult( int pageSize, int pageNum, DataGridView dataGridView ) {
            var listSelect = (from PB in DBCon.Production_Batch
                              //join PB in DBCon.Production_Batch
                              //on PI.Invoice_No equals PB.InvoiceID
                              join PC in DBCon.Production_Check
                              on PB.Batch_No equals PC.BatchID
                              //where (typeTime1.ToString() != typeTime2.ToString() &&
                              //(PI.Test_Time >= typeTime1 && PI.Test_Time <= typeTime2)) &&
                              //(String.IsNullOrEmpty(typeComboBoxText) || PB.TypeID == typeComboBoxText)
                              where PC.Flag != 3
                              select new {
                                  发货单号 = PB.Invoice_No,
                                  订单号 = PB.Contract_No,
                                  流水号 = PB.Serial_Number,
                                  电池批号 = PB.Batch_No,
                                  电池型号 = PB.ModelID,
                                  检测数量 = PB.Battery_Amount,
                                  检测编号 = PC.Number,
                                  开路电压 = PC.Open_Voltage,
                                  检测时间 = PC.Test_Time,
                                  //检测人 = PI.UserID
                              });

            if( typeTime1.ToString() != typeTime2.ToString() ) {
                listSelect = listSelect.Where(x => x.检测时间 >= typeTime1 && x.检测时间 <= typeTime2);
            }
            if( !String.IsNullOrEmpty(typeComboBoxText) ) {
                listSelect = listSelect.Where(x => x.电池型号 == typeComboBoxText);
            }
            listSelect = listSelect.OrderByDescending(x => x.检测时间).Skip(pageSize * (pageNum - 1)).Take(pageSize);
            SetDataGridView(listSelect, dataGridView);
        }

        //电池批次查询总数
        public int GetBatchPageCount( DateTime time1, DateTime time2, string comboBoxText ) {
            GetBatchParameter(time1, time2, comboBoxText);
            var listStart = (from PB in DBCon.Production_Batch
                             //join PI in DBCon.Production_Invoice
                             //on PI.Invoice_No equals PB.InvoiceID
                             join PC in DBCon.Production_Check
                             on PB.Batch_No equals PC.BatchID
                             //where (batchTime1.ToString() != batchTime2.ToString() &&
                             //(PI.Test_Time >= batchTime1 && PI.Test_Time <= batchTime2)) &&
                             //(String.IsNullOrEmpty(batchComboBoxText) || PB.Batch_No == batchComboBoxText)
                             where PC.Flag != 3
                             select new {
                                 发货单号 = PB.Invoice_No,
                                 订单号 = PB.Contract_No,
                                 流水号 = PB.Serial_Number,
                                 电池批号 = PB.Batch_No,
                                 电池型号 = PB.ModelID,
                                 检测数量 = PB.Battery_Amount,
                                 检测编号 = PC.Number,
                                 开路电压 = PC.Open_Voltage,
                                 检测时间 = PC.Test_Time,
                                 //检测人 = PI.UserID
                             });
            if( batchTime1.ToString() != batchTime2.ToString() ) {
                listStart = listStart.Where(x => x.检测时间 >= batchTime1 && x.检测时间 <= batchTime2);
            }
            if( !String.IsNullOrEmpty(batchComboBoxText) ) {
                listStart = listStart.Where(x => x.电池批号 == batchComboBoxText);
            }
            return listStart.Count();
        }

        //电池批次查询分页
        public void GetBatchPageResult( int pageSize, int pageNum, DataGridView dataGridView ) {
            var listSelect = (from PB in DBCon.Production_Batch
                              //join PI in DBCon.Production_Invoice
                              //on PI.Invoice_No equals PB.InvoiceID
                              join PC in DBCon.Production_Check
                              on PB.Batch_No equals PC.BatchID
                              //where (batchTime1.ToString() != batchTime2.ToString() &&
                              //(PI.Test_Time >= batchTime1 && PI.Test_Time <= batchTime2)) &&
                              //(String.IsNullOrEmpty(batchComboBoxText) || PB.Batch_No == batchComboBoxText)
                              where PC.Flag != 3
                              select new {
                                  发货单号 = PB.Invoice_No,
                                  订单号 = PB.Contract_No,
                                  流水号 = PB.Serial_Number,
                                  电池批号 = PB.Batch_No,
                                  电池型号 = PB.ModelID,
                                  检测数量 = PB.Battery_Amount,
                                  检测编号 = PC.Number,
                                  开路电压 = PC.Open_Voltage,
                                  检测时间 = PC.Test_Time,
                                  //检测人 = PI.UserID
                              });
            if( batchTime1.ToString() != batchTime2.ToString() ) {
                listSelect = listSelect.Where(x => x.检测时间 >= batchTime1 && x.检测时间 <= batchTime2);
            }
            if( !String.IsNullOrEmpty(batchComboBoxText) ) {
                listSelect = listSelect.Where(x => x.电池批号 == batchComboBoxText);
            }
            listSelect = listSelect.OrderByDescending(x => x.检测时间).Skip(pageSize * (pageNum - 1)).Take(pageSize);
            SetDataGridView(listSelect, dataGridView);
        }

        //综合查询总数
        public int GetCommonPageCount( string comboBoxInvoice, int comboBoxSerial, string comboBoxType, string comboBoxBatch ) {
            GetCommonParameter(comboBoxInvoice, comboBoxSerial, comboBoxType, comboBoxBatch);
            var listStart = (
                              from PB in DBCon.Production_Batch
                              //join PI in DBCon.Production_Invoice
                              //on PI.Invoice_No equals PB.InvoiceID
                              join PC in DBCon.Production_Check
                              on PB.Batch_No equals PC.BatchID
                              //where (String.IsNullOrEmpty(commonComboBoxInvoice) || PI.Invoice_No == commonComboBoxInvoice) &&
                              //(commonComboBoxSerial != -1 || PI.Serial_Number == commonComboBoxSerial) &&
                              //(String.IsNullOrEmpty(commonComboBoxType) || PB.TypeID == commonComboBoxType) &&
                              //(String.IsNullOrEmpty(commonComboBoxBatch) || PB.Batch_No == commonComboBoxBatch)
                              where PC.Flag != 3
                              select new {
                                  发货单号 = PB.Invoice_No,
                                  订单号 = PB.Contract_No,
                                  //流水号 = PC.Serial_Number,
                                  电池批号 = PC.BatchID,
                                  电池型号 = PB.ModelID,
                                  检测数量 = PB.Battery_Amount,
                                  检测编号 = PC.Number,
                                  开路电压 = PC.Open_Voltage,
                                  检测时间 = PC.Test_Time,
                                  //检测人 = PI.UserID
                              });
            int flag = 0;
            if( !String.IsNullOrEmpty(commonComboBoxInvoice) ) {
                listStart = listStart.Where(x => x.发货单号 == commonComboBoxInvoice);
                flag++;
            }
            //if( commonComboBoxSerial != -1 ) {
            //    listStart = listStart.Where(x => x.流水号 == commonComboBoxSerial);
            //    flag++;
            //}
            if( !String.IsNullOrEmpty(commonComboBoxType) ) {
                listStart = listStart.Where(x => x.电池型号 == commonComboBoxType);
                flag++;
            }
            if( !String.IsNullOrEmpty(commonComboBoxBatch) ) {
                listStart = listStart.Where(x => x.电池批号 == commonComboBoxBatch);
                flag++;
            }

            //判断信息是否填写完整
            if( flag == 4 ) {
                foreach( var temp in listStart.OrderBy(x => x.检测时间) ) {
                    try {
                        dataShowChart.Add(temp.开路电压);
                    } catch { }
                }
            } else {
                dataShowChart = null;
            }

            return listStart.Distinct().Count();
        }

        //综合查询分页
        public void GetCommonPageResult( int pageSize, int pageNum, DataGridView dataGridView ) {
            //StreamWriter sw = new StreamWriter("C:\\Users\\hello\\Desktop\\log123.txt", true);
            //DBCon.Log = sw;
            var listSelect = (
                             from PB in DBCon.Production_Batch
                             //join PI in DBCon.Production_Invoice
                             //on PI.Invoice_No equals PB.InvoiceID
                             join PC in DBCon.Production_Check
                             on PB.Batch_No equals PC.BatchID
                             //where (String.IsNullOrEmpty(commonComboBoxInvoice) || PI.Invoice_No == commonComboBoxInvoice) &&
                             //(commonComboBoxSerial != -1 || PI.Serial_Number == commonComboBoxSerial) &&
                             //(String.IsNullOrEmpty(commonComboBoxType) || PB.TypeID == commonComboBoxType) &&
                             //(String.IsNullOrEmpty(commonComboBoxBatch) || PB.Batch_No == commonComboBoxBatch)
                             where PC.Flag != 3
                             select new {
                                 发货单号 = PB.Invoice_No,
                                 订单号 = PB.Contract_No,
                                 //流水号 = PC.Serial_Number,
                                 电池批号 = PC.BatchID,
                                 电池型号 = PB.ModelID,
                                 检测数量 = PB.Battery_Amount,
                                 检测编号 = PC.Number,
                                 开路电压 = PC.Open_Voltage,
                                 检测时间 = PC.Test_Time,
                                 //检测人 = PI.UserID
                             });
            if( !String.IsNullOrEmpty(commonComboBoxInvoice) ) {
                listSelect = listSelect.Where(x => x.发货单号 == commonComboBoxInvoice);
            }
            //if( commonComboBoxSerial != -1 ) {
            //    listSelect = listSelect.Where(x => x.流水号 == commonComboBoxSerial);
            //}
            if( !String.IsNullOrEmpty(commonComboBoxType) ) {
                listSelect = listSelect.Where(x => x.电池型号 == commonComboBoxType);
            }
            if( !String.IsNullOrEmpty(commonComboBoxBatch) ) {
                listSelect = listSelect.Where(x => x.电池批号 == commonComboBoxBatch);
            }
            listSelect = listSelect.Distinct().OrderByDescending(x => x.检测时间).Skip(pageSize * (pageNum - 1)).Take(pageSize);
            SetDataGridView(listSelect, dataGridView);
            //sw.Close();
        }

        public List<float> GetDataToChart() {
            return dataShowChart;
        }
    }
}
