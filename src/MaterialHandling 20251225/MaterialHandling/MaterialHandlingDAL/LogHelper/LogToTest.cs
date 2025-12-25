using System;                                                      
using System.Collections.Generic;                                  
using System.Linq;                                                
using System.Text;
using System.IO;
using log4net;
using System.Reflection;


namespace MaterialHandling.MaterialHandlingDAL.LogHelper
{
    public class LogToTest
    {
        public static readonly ILog logger = LogManager.GetLogger("log2file");    //这里的参数不能使用Type类型
        /// <summary>
        /// 数据库操作错误记录
        /// </summary>
        /// <param name="msg">需要记录的信息</param>
        public static void SQLError( string msg ) {
            logger.Error(msg, new Exception("发生了一个致命错误\r\n"));
        }

        /// <summary>
        /// 用户登录信息记录
        /// </summary>
        /// <param name="msg">用户登录信息</param>
        public static void UserLogin( string msg ) {
            logger.Info(msg);
        }
    }
}
