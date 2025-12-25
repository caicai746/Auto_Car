using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class UpdatePassWord : Form
    {
        public UpdatePassWord()
        {
            InitializeComponent();
            this.userName.Text = LoginFrame.username;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //确定按钮之前的验证函数
        private bool UserCheck()
        {
            String userMessage = "";
            bool flagUser = true;
            if (passWord.Text == "")
            {
                userMessage += "密码 ";
                flagUser = false;
            }
            if (checkPassword.Text == "")
            {
                userMessage += "确认密码 ";
                flagUser = false;
            }
            if (!flagUser)
            {
                //userMessage += "填写值为空，请重新填写";
                //MessageBox.Show(userMessage);
                //this.Close();
                flagUser = true;

            }
            return flagUser;
        }

        //确定按钮之前的两次密码是否输入一致
        private bool UserCheckPassword()
        {
            bool flagUserPassword = true;
            if (passWord.Text != checkPassword.Text)
            {
                MessageBox.Show("两次密码输入不一致，请重新输入");
                flagUserPassword = false;
                passWord.Text = "";
                checkPassword.Text = "";
            }
            return flagUserPassword;
        }
        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (UserCheck() && UserCheckPassword())
            {
                try
                {

                    String new_Username, new_Password = "";


                    new_Username = LoginFrame.username;
                    if (passWord.Text == "")
                    {
                        MessageBox.Show("密码不能为空！");
                    }
                    else
                    {
                        new_Password = GetMD5.GetMD5WithString(passWord.Text);

                        //向Parameter_Role表中修改该纪录
                        //String sqlStringRole = "update Parameter_Role set Authority={0} where Role_Name={1}";
                        //ParameterRoleDB.Update(sqlStringRole, new_Authority, new_Username);
                        if (new_Password == "")
                        {
                            MessageBox.Show("密码不能为空！");
                            //String sqlstring1 = "update Parameter_User set Flag_Isvalid={0},True_Name={1},Last_Login_Time={2},Try_Login_Time={3},TryLoginCount={4},RoleID={5} where Role_Name={6}";
                            //ParameterUserDB.Update(sqlstring1, flagIsvalid, new_Truename, new_lastLoginTime, new_tryLoginTime, new_TryLoginAmount, roleGuid.ToString(), new_Username);

                        }
                        else
                        {
                            //向Parameter_User表中修改该纪录
                            String sqlStringUser = "update Parameter_User set Password={0} where Role_Name={1}";
                            ParameterUserDB.Update(sqlStringUser, new_Password, new_Username);
                        }

                    }
                    MessageBox.Show("修改成功!");

                }

                catch
                {
                    MessageBox.Show("修改出错！");
                }
                //关闭窗口
                this.Close();

            }
        }
    }
}
