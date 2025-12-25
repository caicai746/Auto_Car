using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using System.Xml;
using System.Data.SqlClient;
using MaterialHandling.Properties;
using MaterialHandling;
using MaterialHandling.MaterialHandlingUI.UIFrame;
using DataToDBCont;


namespace MaterialHandlingUI
{
    static class Program
    {
        public static bool localTest = false;//是否进行本地测试的标志位，本地测试为True，远程连接为False
        public static bool useDatabase = false;//是否使用数据库的标志位，连接数据库为True，不连接为False
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            //hello world
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (useDatabase)
            {
                // 获取应用程序的执行路径
                string appPath = Path.GetDirectoryName(Application.ExecutablePath);

                // 外部配置文件路径
                string configFilePath = Path.Combine(appPath, "config.txt");

                // 读取配置文件
                var config = ReadConfigFile(configFilePath);

                // 解密数据库账号和密码
                var decryptionTable = GetDecryptionTable();

                string bcdbUnEncrypted = config["BCDBUN"];
                string bcdbPdEncrypted = config["BCDBPD"];
                string mesdbUnEncrypted = config["MESDBUN"];
                string mesdbPdEncrypted = config["MESDBPD"];

                string bcdbUnDecrypted = DecryptValue(decryptionTable, bcdbUnEncrypted);
                string bcdbPdDecrypted = DecryptValue(decryptionTable, bcdbPdEncrypted);
                string mesdbUnDecrypted = DecryptValue(decryptionTable, mesdbUnEncrypted);
                string mesdbPdDecrypted = DecryptValue(decryptionTable, mesdbPdEncrypted);

                // 更新配置文件中的值
                config["BCDBUN"] = bcdbUnDecrypted;
                config["BCDBPD"] = bcdbPdDecrypted;
                config["MESDBUN"] = mesdbUnDecrypted;
                config["MESDBPD"] = mesdbPdDecrypted;
                Settings.Default.MESIP = config["MESJK_IP"];
                Settings.Default.username = config["MESJK_US"];
                Settings.Default.password = config["MESJK_PD"];
                Settings.Default.tenant = config["MESJK_ORG"];


                // 数据库连接参数
                string bcdbIP = GetConfigValue(config, "BCDBIP", "192.168.0.0");
                string mesdbIP = GetConfigValue(config, "MESDBIP", "192.168.0.0");
                string batteryCheckerDB2DatabaseName = GetConfigValue(config, "BCDBDN", "BatteryCheckerDB2");
                string batteryCheckerDB2UserName = GetConfigValue(config, "BCDBUN", "sa");
                string batteryCheckerDB2Password = GetConfigValue(config, "BCDBPD", "1234");

                string mesDB2DatabaseName = GetConfigValue(config, "MESDBDN", "MESDB2");
                string mesDB2UserName = GetConfigValue(config, "MESDBUN", "sa");
                string mesDB2Password = GetConfigValue(config, "MESDBPD", "1234");

                // 配置文件路径
                string appConfigFilePath = AppDomain.CurrentDomain.SetupInformation.ConfigurationFile;

                // 修改配置文件
                ModifyConfigFile(appConfigFilePath, "MaterialHandlingUI.Properties.Settings.BatteryCheckerDBConnectionString", bcdbIP, batteryCheckerDB2DatabaseName, batteryCheckerDB2UserName, batteryCheckerDB2Password);
                ModifyConfigFile(appConfigFilePath, "MaterialHandlingUI.Properties.Settings.BatteryCheckerDB2ConnectionString", mesdbIP, mesDB2DatabaseName, mesDB2UserName, mesDB2Password);

                // 读取修改后的配置文件并连接数据库
                string batteryCheckerDB2ConnectionString = ReadConnectionStringFromConfig(appConfigFilePath, "MaterialHandlingUI.Properties.Settings.BatteryCheckerDBConnectionString");
                string mesDB2ConnectionString = ReadConnectionStringFromConfig(appConfigFilePath, "MaterialHandlingUI.Properties.Settings.BatteryCheckerDB2ConnectionString");

                // 确保连接字符串不为null
                if (batteryCheckerDB2ConnectionString != null && mesDB2ConnectionString != null)
                {
                    // 更新MaterialHandling.MaterialHandlingDAL项目中的Settings.settings文件
                    UpdateDALSettings(batteryCheckerDB2ConnectionString, mesDB2ConnectionString);

                    // 更新 DataContext 的 DatabaseName 属性
                    UpdateDataContextDatabaseName(batteryCheckerDB2DatabaseName);

                    // 配置 Log4Net
                    MaterialHandling.MaterialHandlingDAL.LogHelper.LogHelp.ConfigureLog4Net(batteryCheckerDB2ConnectionString);

                    // 连接数据库
                    ConnectToDatabase(batteryCheckerDB2ConnectionString);
                    ConnectToDatabase(mesDB2ConnectionString);
                }
                else
                {
                    Console.WriteLine("读取连接字符串失败，连接字符串为null。");
                }
            }
            else//不连接数据库的话，什么都不做
            {

            }
            

            // 运行应用程序
            Application.Run(LoginFrame.GetInstance());
        }

        public static Dictionary<string, string> ReadConfigFile(string filePath)
        {
            var config = new Dictionary<string, string>();

            if (File.Exists(filePath))
            {
                string[] lines = File.ReadAllLines(filePath);
                foreach (var line in lines)
                {
                    var parts = line.Split(new[] { '=' }, 2);
                    if (parts.Length == 2)
                    {
                        config[parts[0].Trim()] = parts[1].Trim();
                    }
                }
            }

            return config;
        }

        public static string GetConfigValue(Dictionary<string, string> config, string key, string defaultValue)
        {
            if (config.ContainsKey(key))
            {
                return config[key];
            }
            else
            {
                Console.WriteLine("警告: 配置文件中缺少键 '{0}'，使用默认值 '{1}'。", key, defaultValue);
                return defaultValue;
            }
        }

        static void ModifyConfigFile(string configFilePath, string connectionStringName, string ipAddress, string databaseName, string userName, string password)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configFilePath);

            // 假设配置文件中有一个connectionStrings节
            XmlNode connectionStringsNode = xmlDoc.SelectSingleNode("configuration/connectionStrings");
            if (connectionStringsNode != null)
            {
                XmlNode connectionStringNode = connectionStringsNode.SelectSingleNode(string.Format("add[@name='{0}']", connectionStringName));
                if (connectionStringNode != null)
                {
                    // 修改连接字符串
                    string newConnectionString = string.Format("Data Source={0};Initial Catalog={1};Persist Security Info=True;User ID={2};Password={3}", ipAddress, databaseName, userName, password);
                    connectionStringNode.Attributes["connectionString"].Value = newConnectionString;

                    // 保存修改后的配置文件
                    xmlDoc.Save(configFilePath);
                }
            }
        }

        public static string ReadConnectionStringFromConfig(string configFilePath, string connectionStringName)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(configFilePath);

            // 假设配置文件中有一个connectionStrings节
            XmlNode connectionStringsNode = xmlDoc.SelectSingleNode("configuration/connectionStrings");
            if (connectionStringsNode != null)
            {
                XmlNode connectionStringNode = connectionStringsNode.SelectSingleNode(string.Format("add[@name='{0}']", connectionStringName));
                if (connectionStringNode != null)
                {
                    return connectionStringNode.Attributes["connectionString"].Value;
                }
            }

            return null;
        }

        static void UpdateDALSettings(string batteryCheckerDB2ConnectionString, string mesDB2ConnectionString)
        {
            // 更新MaterialHandling.MaterialHandlingDAL项目中的Settings.settings文件
            if (Settings.Default != null)
            {
                Settings.Default["BatteryCheckerDB2ConnectionString"] = batteryCheckerDB2ConnectionString;
                Settings.Default["MESDB2ConnectionString"] = mesDB2ConnectionString;
                Settings.Default.Save();
            }
            else
            {
                Console.WriteLine("Settings.Default 为 null，无法更新设置。");
            }
        }

        static void UpdateDataContextDatabaseName(string databaseName)
        {
            // 获取 DataContext 的类型
            Type dataContextType = typeof(DataToDBDataContext);

            // 获取 DataContext 的 DatabaseName 属性
            var databaseNameProperty = dataContextType.GetProperty("DatabaseName", BindingFlags.Instance | BindingFlags.NonPublic);

            if (databaseNameProperty != null)
            {
                // 创建 DataContext 实例
                var dataContext = new DataToDBDataContext();

                // 设置 DatabaseName 属性
                databaseNameProperty.SetValue(dataContext, databaseName, null);
            }
            else
            {
                Console.WriteLine("无法找到 DataContext 的 DatabaseName 属性。");
            }
        }

        static void ConnectToDatabase(string connectionString)
        {
            if (connectionString != null)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    try
                    {
                        connection.Open();
                        Console.WriteLine("数据库连接成功！");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("数据库连接失败: " + ex.Message);
                    }
                }
            }
            else
            {
                Console.WriteLine("连接字符串为 null，无法连接数据库。");
            }
        }

        public static Dictionary<string, string> GetDecryptionTable()
        {
            return new Dictionary<string, string>
            {
                { "fdafd", "HBK" },                  // DB用户1          实际：HBK           测试系统：sa
                { "ruiew", "hoppecke" },            // DB用户2
                { "dhsaj", "hoppcheck" },           // DB用户3
                { "rewuouoi", "hoppecke" },             // DB密码1          实际：hoppecke     测试系统：1234
                { "cnxzvkyt", "hopp123456" },       // DB密码2
                { "ipotreit", "check1234" },        // DB密码3
                { "fjdsk", "MESUSER" },                  // 质控DB 用户1     实际：MESUSER         测试系统：sa
                { "vcbxz", "hoppecke" },            // 质控DB 用户2
                { "hgbnf", "hoppcheck" },           // 质控DB 用户3
                { "ereklrew", "hoppecke2021" },             // 质控DB 密码1     实际：hoppecke2021    测试系统：1234
                { "yewiqyei", "hopp123456" },       // 质控DB 密码2
                { "uytreiud", "check1234" }         // 质控DB 密码3
            };
        }

        public static string DecryptValue(Dictionary<string, string> decryptionTable, string encryptedValue)
        {
            if (decryptionTable.ContainsKey(encryptedValue))
            {
                return decryptionTable[encryptedValue];
            }

            return null;
        }
    }
}