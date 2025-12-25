using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaterialHandling.MaterialHandlingDAL.Entity.QueryForm;

namespace MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery
{
    public class QueryComboList
    {
        private DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
        private QueryParameter queryPara;           //查询参数 

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="queryParameter"></param>
        public QueryComboList(QueryParameter queryParameter)
        {
            this.queryPara = queryParameter;
        }

        /// <summary>
        /// 获取批号的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetBatchList()
        {
            var queryList = from PB in DBCon.Production_Batch
                                //join PI in DBCon.Production_Invoice
                                //on PI.GUID.ToString() equals PB.InvoiceID
                            where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                     //(String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                     (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                     (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                     (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                     (String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                                     (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new
                            {
                                开始时间 = PB.Start_Time,
                                批号 = PB.Batch_No,
                                订单号 = PB.Contract_No,
                                发货单号 = PB.Invoice_No,
                                流水号 = PB.Serial_Number,
                                型号 = PB.ModelID,
                                检测机 = PB.Station_Address
                            };
            //if (queryPara.flag)
            //{
            //    queryList = queryList.Where(x => x.开始时间 > queryPara.startTime && x.开始时间 < queryPara.endTime);
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
            //if(queryPara.statinaddress != -1)
            //{
            //    queryList = queryList.Where(x => x.检测机 == queryPara.statinaddress);
            //}
            List<string> batchList = new List<string>();
            foreach (var a in queryList)
            {
                batchList.Add(a.批号);
            }
            return batchList;
        }

        /// <summary>
        /// 获取订单号的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetContractList()
        {
            var queryList = from PB in DBCon.Production_Batch
                                //join PI in DBCon.Production_Invoice
                                //on PI.GUID.ToString() equals PB.InvoiceID
                            where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                   (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                   //(String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                   (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                   (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                   (String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                                   (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new
                            {
                                开始时间 = PB.Start_Time,
                                批号 = PB.Batch_No,
                                订单号 = PB.Contract_No,
                                发货单号 = PB.Invoice_No,
                                流水号 = PB.Serial_Number,
                                型号 = PB.ModelID,
                                检测机 = PB.Station_Address
                            };
            //if (queryPara.flag)
            //{
            //    queryList = queryList.Where(x => x.开始时间 > queryPara.startTime && x.开始时间 < queryPara.endTime);
            //}
            //if (!String.IsNullOrEmpty(queryPara.batchNo))
            //{
            //    queryList = queryList.Where(x => x.批号 == queryPara.batchNo);
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
            //if (queryPara.statinaddress != -1)
            //{
            //    queryList = queryList.Where(x => x.检测机 == queryPara.statinaddress);
            //}
            List<string> contractList = new List<string>();
            foreach (var a in queryList)
            {
                contractList.Add(a.订单号);
            }
            return contractList;
        }

        /// <summary>
        /// 获取发货单号的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetInoviceList()
        {
            var queryList = from PB in DBCon.Production_Batch
                                //join PI in DBCon.Production_Invoice
                                //on PI.GUID.ToString() equals PB.InvoiceID
                            where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                     (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                     (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                     //(String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                     (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                     (String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                                     (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new
                            {
                                开始时间 = PB.Start_Time,
                                批号 = PB.Batch_No,
                                订单号 = PB.Contract_No,
                                发货单号 = PB.Invoice_No,
                                流水号 = PB.Serial_Number,
                                型号 = PB.ModelID,
                                检测机 = PB.Station_Address
                            };
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
            //if (queryPara.serialNo != -1)
            //{
            //    queryList = queryList.Where(x => x.流水号 == queryPara.serialNo);
            //}
            //if (!String.IsNullOrEmpty(queryPara.typeNo))
            //{
            //    queryList = queryList.Where(x => x.型号 == queryPara.typeNo);
            //}
            //if (queryPara.statinaddress != -1)
            //{
            //    queryList = queryList.Where(x => x.检测机 == queryPara.statinaddress);
            //}
            List<string> invoiceList = new List<string>();
            foreach (var a in queryList)
            {
                invoiceList.Add(a.发货单号);
            }
            return invoiceList;
        }

        /// <summary>
        /// 获取流水号的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetSerialList()
        {
            var queryList = from PB in DBCon.Production_Batch
                                //join PI in DBCon.Production_Invoice
                                //on PI.GUID.ToString() equals PB.InvoiceID
                            where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                   (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                   (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                   (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                   //(queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                   (String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                                   (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new
                            {
                                开始时间 = PB.Start_Time,
                                批号 = PB.Batch_No,
                                订单号 = PB.Contract_No,
                                发货单号 = PB.Invoice_No,
                                流水号 = PB.Serial_Number,
                                型号 = PB.ModelID,
                                检测机 = PB.Station_Address
                            };
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
            //if (!String.IsNullOrEmpty(queryPara.typeNo))
            //{
            //    queryList = queryList.Where(x => x.型号 == queryPara.typeNo);
            //}
            //if (queryPara.statinaddress != -1)
            //{
            //    queryList = queryList.Where(x => x.检测机 == queryPara.statinaddress);
            //}
            List<string> serialList = new List<string>();
            foreach (var a in queryList)
            {
                serialList.Add(a.流水号.ToString());
            }
            return serialList;
        }

        /// <summary>
        /// 获取型号的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetTypeList()
        {
            var queryList = from PT in DBCon.Parameter_Type
                            join PB in DBCon.Production_Batch
                            on PT.typeid equals PB.typeID
                            where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                                   (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                                   (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                                   (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                                   (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                                   //(String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                                   (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new
                            {
                                
                                型号 = PT.ModelID,
                           
                            };
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
            List<string> typeList = new List<string>();
            foreach (var a in queryList)
            {
                typeList.Add(a.型号);
            }
            return typeList;
        }
        
        /// <summary>
        /// 获得检测机的下拉列表
        /// </summary>
        /// <returns></returns>
        public List<string> GetStationList()
        { 
            var querylist = from PB in DBCon.Production_Batch
                            //where (queryPara.flag == false || PB.Start_Time > queryPara.startTime && PB.Start_Time < queryPara.endTime) &&
                            //       (String.IsNullOrEmpty(queryPara.batchNo) || PB.Batch_No == queryPara.batchNo) &&
                            //       (String.IsNullOrEmpty(queryPara.contractNo) || PB.Contract_No == queryPara.contractNo) &&
                            //       (String.IsNullOrEmpty(queryPara.invoiceNo) || PB.Invoice_No == queryPara.invoiceNo) &&
                            //       (queryPara.serialNo == -1 || PB.Serial_Number == queryPara.serialNo) &&
                            //       (String.IsNullOrEmpty(queryPara.typeNo) || PB.ModelID == queryPara.typeNo) &&
                            //       (queryPara.statinaddress == -1 || PB.Station_Address == queryPara.statinaddress)
                            select new {
                                开始时间 = PB.Start_Time,
                                批号 = PB.Batch_No,
                                订单号 = PB.Contract_No,
                                发货单号 = PB.Invoice_No,
                                流水号 = PB.Serial_Number,
                                型号 = PB.ModelID,
                                检测机 = PB.Station_Address

            };
            List<string> stationList = new List<string>();
            foreach (var a in querylist)
            {
                stationList.Add(a.检测机.ToString());
            }
            return stationList;
        }
        public List<string> GetLogUserList()
        {
            var queryList= (from s in DBCon.LogDetail
                            select new
                            {
                                序号 = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            });
            List<string> List_Log = new List<string>();
            foreach(var a in queryList)
            {
                List_Log.Add(a.用户名);
            }
            return List_Log;
        }


    }
}
