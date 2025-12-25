using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using System.Collections;

namespace MaterialHandling.MaterialHandlingDAL.Entity.QueryForm
{
    public class ListToDataGridView
    {
        //public static void ListToShowInvoice(int serialNumber, string comboBoxText, DataGridView dataGridView)
        //{
        //    DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
        //    var listStart = (
        //                     from PB in DBCon.Production_Batch

        //                     join PC in DBCon.Production_Check
        //                     on PB.Batch_No equals PC.BatchID
        //                     where (String.IsNullOrEmpty(comboBoxText) || PB.Invoice_No == comboBoxText) &&
        //                     (serialNumber == -1 || PB.Serial_Number == serialNumber)
        //                     select new
        //                     {
        //                         发货单号 = PB.Invoice_No,
        //                         订单号 = PB.Contract_No,
        //                         流水号 = PB.Serial_Number,
        //                         电池批号 = PB.Batch_No,
        //                         检测数量 = PB.Battery_Amount,
        //                         电池型号 = PB.ModelID,
        //                         检测编号 = PC.Number,
        //                         开路电压 = PC.Open_Voltage,
        //                         检测时间 = PC.Test_Time,
        //                         检测人 = PB.UserID
        //                     }).Distinct().OrderBy(x => x.检测时间);
        //    //PagingQuery PQ = new PagingQuery();
        //    //int countNum = PQ.GetInvoicePageCount(serialNumber, comboBoxText);
        //    //var listStart = PQ.GetPageResult(10, 20);
        //    dataGridView.DataSource = listStart;
        //    //if( !String.IsNullOrEmpty(comboBoxText) ) {
        //    //    listStart = listStart.Where(x => x.发货单号 == comboBoxText);
        //    //}
        //    //if( serialNumber != -1 ) {
        //    //    listStart = listStart.Where(x => x.流水号 == serialNumber);
        //    //}
        //}

        //public static void ListToShowType(DateTime time1, DateTime time2, string type, DataGridView dataGridView)
        //{
        //    DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
        //    if (time1.ToString() == time2.ToString())
        //    {
        //        var list_User = (from PI in DBCon.Production_Invoice
        //                         join PB in DBCon.Production_Batch
        //                         on PI.Invoice_No equals PB.InvoiceID
        //                         join PC in DBCon.Production_Check
        //                         on PB.Batch_No equals PC.BatchID
        //                         select new
        //                         {
        //                             发货单号 = PI.Invoice_No,
        //                             订单号 = PI.Contract_No,
        //                             流水号 = PI.Serial_Number,
        //                             电池批号 = PB.Batch_No,
        //                             电池型号 = PB.ModelID,
        //                             检测数量 = PB.Battery_Amount,
        //                             检测编号 = PC.Number,
        //                             开路电压 = PC.Open_Voltage,
        //                             检测时间 = PC.Test_Time,
        //                             检测人 = PI.UserID
        //                         });
        //        if (!String.IsNullOrEmpty(type))
        //        {
        //            list_User = list_User.Where(x => x.电池型号 == type);
        //        }
        //        dataGridView.DataSource = list_User.Distinct().OrderBy(x => x.检测时间);
        //    }
        //    else
        //    {
        //        var list_User = (from PI in DBCon.Production_Invoice
        //                         join PB in DBCon.Production_Batch
        //                         on PI.Invoice_No equals PB.InvoiceID
        //                         join PC in DBCon.Production_Check
        //                         on PB.Batch_No equals PC.BatchID
        //                         where PI.Test_Time >= time1 &&
        //                         PI.Test_Time <= time2
        //                         select new
        //                         {
        //                             发货单号 = PI.Invoice_No,
        //                             流水号 = PI.Serial_Number,
        //                             订单号 = PI.Contract_No,
        //                             电池批号 = PB.Batch_No,
        //                             电池型号 = PB.ModelID,
        //                             检测数量 = PB.Battery_Amount,
        //                             检测编号 = PC.Number,
        //                             开路电压 = PC.Open_Voltage,
        //                             检测时间 = PC.Test_Time,
        //                             检测人 = PI.UserID
        //                         });
        //        if (!String.IsNullOrEmpty(type))
        //        {
        //            list_User = list_User.Where(x => x.电池型号 == type);
        //        }
        //        dataGridView.DataSource = list_User.Distinct().OrderBy(x => x.检测时间);
        //    }
        //}

        //public static void ListToShowBatch(DateTime time1, DateTime time2, string comboBoxText, DataGridView dataGridView)
        //{
        //    DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
        //    if (time1.ToString() == time2.ToString())
        //    {
        //        var list_User = (from PI in DBCon.Production_Invoice
        //                         join PB in DBCon.Production_Batch
        //                         on PI.Invoice_No equals PB.InvoiceID
        //                         join PC in DBCon.Production_Check
        //                         on PB.Batch_No equals PC.BatchID
        //                         select new
        //                         {
        //                             发货单号 = PI.Invoice_No,
        //                             订单号 = PI.Contract_No,
        //                             流水号 = PI.Serial_Number,
        //                             电池批号 = PB.Batch_No,
        //                             电池型号 = PB.ModelID,
        //                             检测数量 = PB.Battery_Amount,
        //                             检测编号 = PC.Number,
        //                             开路电压 = PC.Open_Voltage,
        //                             检测时间 = PC.Test_Time,
        //                             检测人 = PI.UserID
        //                         });
        //        if (!String.IsNullOrEmpty(comboBoxText))
        //        {
        //            list_User = list_User.Where(x => x.电池批号 == comboBoxText);
        //        }
        //        dataGridView.DataSource = list_User.Distinct().OrderBy(x => x.检测时间);
        //    }
        //    else
        //    {
        //        var list_User = (from PI in DBCon.Production_Invoice
        //                         join PB in DBCon.Production_Batch
        //                         on PI.Invoice_No equals PB.InvoiceID
        //                         join PC in DBCon.Production_Check
        //                         on PB.Batch_No equals PC.BatchID
        //                         where PI.Test_Time >= time1 &&
        //                         PI.Test_Time <= time2
        //                         select new
        //                         {
        //                             发货单号 = PI.Invoice_No,
        //                             订单号 = PI.Contract_No,
        //                             流水号 = PI.Serial_Number,
        //                             电池批号 = PB.Batch_No,
        //                             电池型号 = PB.ModelID,
        //                             检测数量 = PB.Battery_Amount,
        //                             检测编号 = PC.Number,
        //                             开路电压 = PC.Open_Voltage,
        //                             检测时间 = PC.Test_Time,
        //                             检测人 = PI.UserID
        //                         });
        //        if (!String.IsNullOrEmpty(comboBoxText))
        //        {
        //            list_User = list_User.Where(x => x.电池批号 == comboBoxText);
        //        }
        //        dataGridView.DataSource = list_User.Distinct().OrderBy(x => x.检测时间);
        //    }
        //}

        //public static List<float> ListToShowCommon(Chart thisChart, string comboBoxInvoice, int comboBoxSerial, string comboBoxType, string comboBoxBatch, DataGridView dataGridView)
        //{
        //    DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
        //    var list_User = (from PI in DBCon.Production_Invoice
        //                     join PB in DBCon.Production_Batch
        //                     on PI.Invoice_No equals PB.InvoiceID
        //                     join PC in DBCon.Production_Check
        //                     on PB.Batch_No equals PC.BatchID
        //                     select new
        //                     {
        //                         发货单号 = PI.Invoice_No,
        //                         订单号 = PI.Contract_No,
        //                         流水号 = PI.Serial_Number,
        //                         电池批号 = PB.Batch_No,
        //                         电池型号 = PB.ModelID,
        //                         检测数量 = PB.Battery_Amount,
        //                         检测编号 = PC.Number,
        //                         开路电压 = PC.Open_Voltage,
        //                         检测时间 = PC.Test_Time,
        //                         检测人 = PI.UserID
        //                     });
        //    if (!String.IsNullOrEmpty(comboBoxInvoice))
        //    {
        //        list_User = list_User.Where(x => x.发货单号 == comboBoxInvoice);
        //    }
        //    if (comboBoxSerial != -1)
        //    {
        //        list_User = list_User.Where(x => x.流水号 == comboBoxSerial);
        //    }
        //    if (!String.IsNullOrEmpty(comboBoxType))
        //    {
        //        list_User = list_User.Where(x => x.电池型号 == comboBoxType);
        //    }
        //    if (!String.IsNullOrEmpty(comboBoxBatch))
        //    {
        //        list_User = list_User.Where(x => x.电池批号 == comboBoxBatch);
        //    }
        //    dataGridView.DataSource = list_User.Distinct().OrderBy(x => x.检测时间);

        //    if (comboBoxInvoice != "" && comboBoxSerial != -1 && comboBoxType != "" && comboBoxBatch != "")
        //    {
        //        List<float> voltageResult = new List<float>();
        //        foreach (var temp in list_User.Distinct().OrderBy(x => x.检测时间))
        //        {
        //            voltageResult.Add(temp.开路电压);
        //        }
        //        return voltageResult;
        //    }
        //    else
        //    {
        //        return null;
        //    }
        //}
    }
}
