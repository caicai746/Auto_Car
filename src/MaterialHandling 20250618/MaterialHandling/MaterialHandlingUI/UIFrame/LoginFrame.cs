using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.LogHelper;
using System.Configuration;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterUser;
using System.IO;
using System.Data.SqlClient;
using System.Net;
using MaterialHandlingUI;

namespace MaterialHandling.MaterialHandlingUI.UIFrame
{
    public partial class LoginFrame : Form
    {
        public const int max_TryLoginAmount = 5;      //最大尝试登录次数为5
        public static String username = "";           //用户名         
        public static String authority = "";          //权限

        public LoginFrame() {
            InitializeComponent();
        }

        //检测是否可以成功登录，用户名和密码是否在数据库中且匹配
        private bool CheckLogin( String username, String password ) {
            bool flagLogin = false;
            string pass = GetMD5.GetMD5WithString(password);
            String sqlString = "select * from Parameter_User where Role_Name={0} and Password = {1}"; //去User表中查询
            int result = ParameterUserDB.Query<Parameter_User>(sqlString, username, pass).Count();
            if( result != 0 )
                flagLogin = true;
           
            return flagLogin;
        }

        //检测用户名是否在数据库中
        private bool CheckUsername( String username ) {
            bool flagUsername = false;
            String sqlString = "select * from Parameter_User where Role_Name={0}";   //去User表中查询
            int result = ParameterUserDB.Query<Parameter_User>(sqlString, username).Count();
            if( result != 0 )
                flagUsername = true;
            return flagUsername;
        }

        //获取成功登录用户的权限
        private String GetUserAuthority( String username ) {
            String roleGuid = ParameterUserLinq.GetRoleGuidByUsername(username);
            String authority = "";
            String sqlString = "select * from Parameter_Role where RoleID={0}";
            authority = ParameterRoleDB.QueryOneData(sqlString, roleGuid).Authority.ToString();   //获取权限
            return authority;
        }

        //登陆成功更新Parameter_User表里面的内容
        private void LoginSuccessUpdateUser( String username ) {
            DateTime tryLoginTime = DateTime.Now;
            DateTime lastLoginTime = DateTime.Now;
            int tryLoginAmount = 0;                     //登录成功后将尝试登录次数设置为0
            String sqlString = "update Parameter_User set Last_Login_Time={0},Try_Login_Time={1},TryLoginCount={2} where Role_Name={3}";
            ParameterUserDB.Update(sqlString, lastLoginTime, tryLoginTime, tryLoginAmount, username);
        }

        //登陆失败更新Parameter_User表里面的内容
        private void LoginFailureUpdateUser( String username, int tryLoginAmount ) {
            DateTime tryLoginTime = DateTime.Now;
            String sqlString = "update Parameter_User set Try_Login_Time={0},TryLoginCount={1} where Role_Name={2}";
            ParameterUserDB.Update(sqlString, tryLoginTime, tryLoginAmount, username);
        }

        private void btOK_Click( object sender, EventArgs e ) {
            bool flagEmpty = false;                    //输入信息是否为空的标志位
            bool flagLogin = false;                    //是否成功登录的标志位
            bool flagUsername = false;                 //用户名是否正确的标志位
            bool flagUserValid = false;                //用户状态是否有效的标志位

            if(Program.useDatabase)
            {
                //检测输入是否为空
                if (tbUserName.Text == "" && tbPassword.Text == "")
                {
                    flagEmpty = true;
                    MessageBox.Show("用户名和密码输入为空，请重新输入！");
                }
                else if (tbUserName.Text == "" && tbPassword.Text != "")
                {
                    flagEmpty = true;
                    MessageBox.Show("用户名输入为空，请重新输入！");
                }
                else if (tbUserName.Text != "" && tbPassword.Text == "")
                {
                    flagEmpty = true;
                    MessageBox.Show("密码输入为空，请重新输入！");
                }




                if (!flagEmpty)
                {
                    try
                    {
                        flagLogin = CheckLogin(tbUserName.Text, tbPassword.Text);  //检测用户名和密码是否合法
                        flagUsername = CheckUsername(tbUserName.Text);             //检测用户名是否合法
                        if (flagLogin)       //成功登录
                        {
                            String sqlString = "select * from Parameter_User where Role_Name={0}";
                            flagUserValid = ParameterUserDB.QueryOneData(sqlString, tbUserName.Text).Flag_Isvalid;   //获取用户当前状态

                            if (flagUserValid)     //状态为有效，允许登录
                            {
                                PrintFileVersionInfo("Log\\UserToLogRollingFileAppender.txt");
                                this.Visible = false;
                                //LogToTest.UserLogin("用户：" + username + "权限：" + authority + " 成功登录！");

                                //更新Parameter_User表里面的内容
                                LoginSuccessUpdateUser(tbUserName.Text);

                                username = tbUserName.Text; //获取用户名
                                authority = GetUserAuthority(tbUserName.Text);   //获取用户权限
                                LogHelp.Info(new LogContent("登录系统", " ", username));
                                MainFrame mainFrame = new MainFrame();
                                mainFrame.Show();
                            }
                            else
                            {
                                MessageBox.Show("该用户当前状态无效，无法登录");
                            }
                        }
                        else if (flagUsername)       //用户名正确，密码错误
                        {
                            String sqlString = "select * from Parameter_User where Role_Name={0}";
                            flagUserValid = ParameterUserDB.QueryOneData(sqlString, tbUserName.Text).Flag_Isvalid;   //获取用户当前状态
                            if (flagUserValid)
                            {
                                int tryLoginAmount = ParameterUserDB.QueryOneData(sqlString, tbUserName.Text).TryLoginCount;//获取尝试登录次数
                                if (tryLoginAmount == max_TryLoginAmount)   //尝试登录次数到达最大值后，将该用户状态设置为无效
                                {
                                    String sqlStringFlag = "update Parameter_User set TryLoginCount={0},Flag_Isvalid={1} where Role_Name={2}";
                                    ParameterUserDB.Update(sqlStringFlag, max_TryLoginAmount, false, tbUserName.Text);
                                    MessageBox.Show("该用户当前状态无效，无法登录");
                                }
                                else
                                {
                                    //更新Parameter_User表里面的内容                             
                                    if (tryLoginAmount == max_TryLoginAmount)
                                    {
                                        MessageBox.Show("该用户登录次数已达上限，状态变为无效状态，请联系管理员恢复有效状态！");
                                    }
                                    else
                                    {
                                        MessageBox.Show("密码输入错误，请重新输入！");
                                        tbPassword.Text = "";
                                    }
                                }
                            }
                        }
                        else if (!flagUsername)      //用户名不存在
                        {
                            MessageBox.Show("用户名输入错误，请重新输入！");
                            tbUserName.Text = "";
                            tbPassword.Text = "";
                        }

                    }
                    catch (Exception rex)
                    {
                        MessageBox.Show(rex.StackTrace);
                        MessageBox.Show("用户登录" + rex.Message);
                        LogToTest.SQLError(rex.Message.ToString());
                        MessageBox.Show("数据库连接错误！");
                    }
                }
            }
            else
            {
                this.Visible = false;
                MainFrame mainFrame = new MainFrame();
                mainFrame.Show();
            }


        }
        //判断文本是否有数据
        public static void PrintFileVersionInfo(string path)
        {
            FileInfo fileInfo = null;
            try
            {
                fileInfo = new FileInfo(path);
            }
            catch (Exception e)
            {
                LogToTest.SQLError(e.Message);
                // 其他处理异常的代码
            }
            if (fileInfo != null && fileInfo.Exists)
            {
                int fileLen = Convert.ToInt16(Math.Ceiling(fileInfo.Length / 1024.0));
                //判断文件有无内容，有内容执行存储过程
                if (fileLen > 0)
                {
                    List<String[]> ls = ReadTxt(path);

                    string d1 = ls[0][0].ToString();
                    DateTime d = DateTime.Parse(d1);
                    string d2 = ls[0][2].ToString();
                    string d3 = ls[0][3].ToString();
                    string d4 = ls[0][4].ToString();
                    string d5 = ls[0][5].ToString();
                    string d6 = ls[0][6].ToString();
                    LogDetail1.Insert(d, d3, d2, d4, d5, d6);
                    //string sql = "INSERT INTO LogDetail(LogDate,LogLevel,LogThread,Logger,LogMessage,UsName) VALUES(@logDate,@logLevel,@logThread,@logger,@logMessage,@usName)";

                    //string connStr = "server=10.162.34.217;uid=sa;pwd=1234;database=BatteryCheckerDB";
                    //using (SqlConnection conn = new SqlConnection(connStr))
                    //{
                    //    conn.Open();
                    //    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    //    {
                    //        cmd.Parameters.AddWithValue("@logDate", d);
                    //        cmd.Parameters.AddWithValue("@logLevel", d3);
                    //        cmd.Parameters.AddWithValue("@logThread", d2);
                    //        cmd.Parameters.AddWithValue("@logger", d4);
                    //        cmd.Parameters.AddWithValue("@logMessage", d5);
                    //        cmd.Parameters.AddWithValue("@usName", d6);
                    //        cmd.ExecuteNonQuery();

                    //    }
                    //    conn.Close();
                    //}
                    FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite, FileShare.ReadWrite);
                     fs.Close(); 

                }
            }
        }
        //读取日志文件
        public static List<String[]> ReadTxt(string filePathName)
        {
       
            List<String[]> ls = new List<String[]>();
            using (FileStream fileStream = new FileStream(filePathName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            {
                StreamReader fileReader = new StreamReader(fileStream, Encoding.GetEncoding("gb2312"));
                
                string strLine = "";
                while (strLine != null)
                {
                    strLine = fileReader.ReadLine();
                    if (strLine != null && strLine.Length > 0)
                    {
                        ls.Add(strLine.Split(','));
                        //Debug.WriteLine(strLine);
                    }
                }
            
                fileReader.Close();
                fileStream.Close();
            }

            return ls;
        }
        private void btCancel_Click( object sender, EventArgs e ) {
            Application.Exit();
        }

        private void btClear_Click( object sender, EventArgs e ) {
            tbUserName.Text = "";
            tbPassword.Text = "";
        }

        private void tbUserName_TextChanged(object sender, EventArgs e)
        {

        }
        

    }
}
