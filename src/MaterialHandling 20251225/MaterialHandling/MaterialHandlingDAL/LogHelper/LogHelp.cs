using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Repository.Hierarchy;
using log4net.Appender;
using System.Configuration;



namespace MaterialHandling.MaterialHandlingDAL.LogHelper
{
    public class LogHelp
    {
        public static readonly ILog logger = LogManager.GetLogger("myLogger");

        public static void Fatal(LogContent content)
        {
            logger.Fatal(content);
        }

        public static void Error(LogContent content)
        {
            logger.Error(content);
        }

        public static void Warn(LogContent content)
        {
            logger.Warn(content);
        }

        public static void Info(LogContent content)
        {
            logger.Info(content);
        }

        public static void Debug(LogContent content)
        {
            logger.Debug(content);
        }

        public static void ConfigureLog4Net(string ConnectionString)
        {
            Hierarchy hierarchy = LogManager.GetRepository() as Hierarchy;

            if (hierarchy != null && hierarchy.Configured)
            {
                foreach (IAppender appender in hierarchy.GetAppenders())
                {
                    if (appender is AdoNetAppender)
                    {
                        var adoNetAppender = (AdoNetAppender)appender;
                        adoNetAppender.ConnectionString = ConnectionString;
                        adoNetAppender.ActivateOptions(); //Refresh AdoNetAppenders Settings
                    }
                }
            }
        }
    }
}
