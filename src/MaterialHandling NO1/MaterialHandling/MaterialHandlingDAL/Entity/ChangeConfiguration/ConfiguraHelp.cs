using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ChangeConfiguration
{
    public static class ConfiguraHelp
    {
        public static String GetConnectiongStringsConfig(string connectionName)
        {
            //依据链接字符串名字connnectionName返回链接字符串
            //string file = System.Windows.Forms.Application.ExecutablePath;
            string file = @"MaterialHandling.dll";
            System.Configuration.Configuration config = ConfigurationManager.OpenExeConfiguration(file);
             string connectionString =
               config.ConnectionStrings.ConnectionStrings[connectionName].ConnectionString.ToString();
            return connectionString;
        }
        /// <summary>
        /// 更新链接字符串
        /// </summary>
        /// <param name="newName">链接字符串名称</param>
        /// <param name="newConString">链接字符串内容</param>
        /// <param name="newProviderName">数据提供程序名称</param>
        //
        public static String GetConnectiongStringsConfig_2()
        {
            //依据链接字符串名字connnectionName返回链接字符串
            //string file = System.Windows.Forms.Application.ExecutablePath;
            string res = Properties.Settings.Default.BatteryCheckerDB2ConnectionString;
            return res;
        }
        public static void UpdateConnectionStringConfig(string newName, string newConString, string newProviderName)
        {
            //指定config文件进行读取
            //string file = "C:\\Users\\MYW\\Desktop\\MaterialHandling.MaterialHandlingDAL(1600x900)\\MaterialHandling.MaterialHandlingDAL\\app";
            string file = @"MaterialHandling.MaterialHandlingDAL.dll";
            Configuration config = ConfigurationManager.OpenExeConfiguration(file);

            bool exist = false; //记录该链接字符串是否存在
            if (config.ConnectionStrings.ConnectionStrings[newName] != null)
            {
                exist = true;
            }
            //如果链接字符串存在，首先删除它
            if (exist)
            {
                config.ConnectionStrings.ConnectionStrings.Remove(newName);
            }
            //新建一个链接字符串实例
            ConnectionStringSettings mySettings =
                new ConnectionStringSettings(newName, newConString, newProviderName);
            //将新的链接字符串添加到配置文件中
            config.ConnectionStrings.ConnectionStrings.Add(mySettings);
            //保存对配置文件的修改
            config.Save(ConfigurationSaveMode.Modified);
            //强制重新载入配置文件的ConnectionStrings配置节
            ConfigurationManager.RefreshSection("ConnectionStrings");

        }

    }
}
