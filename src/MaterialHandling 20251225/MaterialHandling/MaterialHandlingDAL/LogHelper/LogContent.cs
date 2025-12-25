using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace MaterialHandling.MaterialHandlingDAL.LogHelper
{
    public class LogContent
    {
        //private string p1;
        ////private global::MaterialHandling.MaterialHandlingDAL.Entity.TransportDataClass.QueryData querydata;
        //private string p2;

        /// <summary>
        /// 日志内容
        /// </summary>
        public string LogMessage { get; set; }
        public string UsName { get; set; }
         public string ActionType { get; set; }
        public LogContent(string actionType,string logMessage,string userName)
        {
            ActionType = actionType;
            LogMessage = logMessage;
            UsName = userName;
        }

        //public LogContent(string p1, global::MaterialHandling.MaterialHandlingDAL.Entity.TransportDataClass.QueryData querydata, string p2)
        //{
        //    // TODO: Complete member initialization
        //    this.p1 = p1;
        //    this.querydata = querydata;
        //    this.p2 = p2;
        //}
    }
}
