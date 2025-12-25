using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaterialHandling.MaterialHandlingDAL.Entity.QueryForm;
using MaterialHandling.MaterialHandlingDAL.Entity.TransportDataClass;
using DataToDBEnts;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ModifyAndDeleteForDB
{
    public class ModifyAndDelete
    {      
        /// <summary>
        /// 删除一条查询结果
        /// </summary>
        /// <param name="batchGuid"></param>
        public static void DeleteQueryTable( QueryData querydata ) {
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
            var check = from CK in DBCon.Production_Check
                        where CK.BatchID == querydata.batchGuid
                        select CK;
            if(check != null)
            {
                DBCon.Production_Check.DeleteAllOnSubmit(check);
            }
            DBCon.SubmitChanges();
        }

        /// <summary>
        /// 更新一条查询结果
        /// </summary>
        /// <param name="newQueryData"></param>
        public static void ModifyQueryTable( QueryData newQueryData ) {
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
            
            int typeID = (from type in DBCon.Parameter_Type
                          where type.ModelID == newQueryData.type
                          select type.typeid).FirstOrDefault();

            //var invoiceTable = (from invoice in DBCon.Production_Invoice
            //                   where invoice.GUID.ToString() == newQueryData.invoiceGuid
            //                   select invoice).SingleOrDefault();
            //if(invoiceTable!=null) {
            //    invoiceTable.Test_Time = newQueryData.startTime;
            //    invoiceTable.Invoice_No = newQueryData.invoice;
            //    invoiceTable.Contract_No = newQueryData.contract;
            //    invoiceTable.Serial_Number = int.Parse(newQueryData.serialNumber);
            //}
            
            var batchTable = (from batch in DBCon.Production_Batch
                              where batch.GUID.ToString() == newQueryData.batchGuid
                              select batch).SingleOrDefault();
            if(batchTable!=null) {
                batchTable.typeID = typeID;
                batchTable.Batch_No = newQueryData.batch;
                batchTable.Battery_Amount = int.Parse(newQueryData.amount);
                batchTable.Serial_Number = int.Parse(newQueryData.serialNumber);
                batchTable.Start_Time = newQueryData.startTime;
                batchTable.Invoice_No = newQueryData.invoice;
                batchTable.Contract_No = newQueryData.contract;
                batchTable.ModelID = newQueryData.type;
                batchTable.Material_Number = newQueryData.material;
                batchTable.Finish_Charging_Time = newQueryData.Finish_Charging_Time;
            }

            //var checkTable = from check in DBCon.Production_Check
            //                 where check.BatchID == newQueryData.batchGuid
            //                 select check;
            //if(checkTable!=null) {
            //    foreach(var a in checkTable) {
            //        a.Serial_Number = int.Parse(newQueryData.serialNumber);
                    
            //    }
            //}

            DBCon.SubmitChanges();
        }

        /// <summary>
        /// 删除一批明细
        /// </summary>
        /// <param name="batchGuid"></param>
        public static void DeleteDetailedTable( string batchGuid ) {
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
            var checkTable = from check in DBCon.Production_Check
                             where check.BatchID == batchGuid
                             select check;

            if( checkTable == null ) {
                return;
            }

            DBCon.Production_Check.DeleteAllOnSubmit(checkTable);
            DBCon.SubmitChanges();
        }
        //删除一条日志信息
        public static void DeleteLogQueryTable(QueryParameter logParameter)
        {
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
            var logTable = from Log in DBCon.LogDetail
                               where Log.LogID.ToString() == logParameter.LogId
                               select Log;
            if (logTable != null)
            {
                DBCon.LogDetail.DeleteAllOnSubmit(logTable);
            }
            DBCon.SubmitChanges();
        }
    

    }
}
