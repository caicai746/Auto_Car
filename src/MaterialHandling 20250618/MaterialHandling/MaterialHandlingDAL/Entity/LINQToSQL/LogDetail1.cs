using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.LogHelper;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL
{
    public class LogDetail1
    {
        //插入一条新的记录
        public static void Insert(DateTime logdate, string logLevel, string logThread, string logger,  string logMessage ,string usname)

        {
            try
            {
                using (DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext())
                {

                    DataToDBEnts.LogDetail logDetail = new DataToDBEnts.LogDetail();

                    logDetail.LogDate = logdate;
                    logDetail.LogLevel = logLevel;
                    logDetail.LogThread = logThread;
                    logDetail.Logger = logger;
                    logDetail.LogMessage = logMessage;
                    logDetail.UsName = usname;

                    DBContext.LogDetail.InsertOnSubmit(logDetail);
                    DBContext.SubmitChanges();
                }
            }
            catch (Exception e)
            {
                LogToTest.SQLError(e.Message.ToString());
            }
        }
    }
}
