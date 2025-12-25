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
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.User;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterRole;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class EditUser : Form
    {
        private string[] needEditData;
        private DataGridView dataGridView_UserManage;
        private DateTime[] needEditTime;

        public EditUser()
        {
            InitializeComponent();
        }

        public EditUser(string[] needEditData, DateTime[] needEditTime, DataGridView dataGridView_UserManage)
        {
            // TODO: Complete member initialization
            InitializeComponent();

            SettingDateTimePickerCustom(lastLoginTime);
            SettingDateTimePickerCustom(tryLoginTime);

            this.needEditData = needEditData;
            this.needEditTime = needEditTime;
            this.dataGridView_UserManage = dataGridView_UserManage;

            this.userName.Text = needEditData[0];
            this.passWord.Text = needEditData[1];
            //获取当前用户状态
            int ndx = 0;
            switch (needEditData[2])
            {
                case "有效":
                    ndx = 0;
                    break;
                case "无效":
                    ndx = 1;
                    break;
            }
            this.comboBox_NowStatus.SelectedIndex = ndx;

            this.true_Name.Text = needEditData[3];
            this.lastLoginTime.Value = needEditTime[0];
            this.tryLoginTime.Value = needEditTime[1];

            //获取尝试登录次数
            

            //获取用户权限
            int jdx = 0;
            switch (needEditData[6])
            {
                case "管理员":
                    jdx = 0;
                    break;
                case "检查员":
                    jdx = 1;
                    break;
                case "质检员":
                    jdx = 2;
                    break;
                case "查询员":
                    jdx = 3;
                    break;
                case "超级质检员":
                    jdx = 3;
                    break;
            }
            this.comboBox_authority.SelectedIndex = jdx;
        }

        private void SettingDateTimePickerCustom(DateTimePicker dateTimePicker)
        {
            dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            this.passWord.Text = "";
            this.comboBox_NowStatus.SelectedIndex = -1;
            this.true_Name.Text = "";
            this.lastLoginTime.Value = DateTime.Now;
            this.tryLoginTime.Value = DateTime.Now;
            this.comboBox_authority.SelectedIndex = -1;
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
            if (comboBox_NowStatus.Text == "")
            {
                userMessage += "当前状态 ";
                flagUser = false;
            }
            if (true_Name.Text == "")
            {
                userMessage += "真实姓名 ";
                flagUser = false;
            }
            if (comboBox_authority.Text == "")
            {
                userMessage += "权限 ";
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
                String new_Username, new_Password, new_Truename, new_Authority = "";
                DateTime new_lastLoginTime, new_tryLoginTime;
                int new_TryLoginAmount = 0;
                bool flagIsvalid = true;

                new_Username = userName.Text;
                if(passWord.Text == "")
                {
                    MessageBox.Show("密码不能为空！");
                }
                else
                {
                    new_Password = GetMD5.GetMD5WithString(passWord.Text);
                    //获取界面上用户状态
                    switch (this.comboBox_NowStatus.SelectedIndex)
                    {
                        case 0:
                            flagIsvalid = true;
                            break;
                        case 1:
                            flagIsvalid = false;
                            break;
                    }
                    new_Truename = true_Name.Text;
                    new_lastLoginTime = lastLoginTime.Value;
                    new_tryLoginTime = tryLoginTime.Value;
                    switch (this.comboBox_authority.SelectedIndex)
                    {
                        case 0:
                            new_Authority = "管理员";
                            break;
                        case 1:
                            new_Authority = "检查员";
                            break;
                        case 2:
                            new_Authority = "质检员";
                            break;
                        case 3:
                            new_Authority = "查询员";
                            break;
                        case 4:
                            new_Authority = "超级质检员";
                            break;
                    }
                    string roleGuid = ParameterRoleLinq.GetRoleGuid(new_Authority);

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
                        String sqlStringUser = "update Parameter_User set Password={0},Flag_Isvalid={1},True_Name={2},Last_Login_Time={3},Try_Login_Time={4},TryLoginCount={5},RoleID={6} where Role_Name={7}";
                        ParameterUserDB.Update(sqlStringUser, new_Password, flagIsvalid, new_Truename, new_lastLoginTime, new_tryLoginTime, new_TryLoginAmount, roleGuid.ToString(), new_Username);
                    }
                    //重新刷新dategridview的内容
                    QueryUserToView queryUserToView = new QueryUserToView(dataGridView_UserManage);
                    queryUserToView.FillDataToView();

                    //关闭窗口
                    this.Close();

                }



            }
        }
        //当前状态设置为有效时，尝试登录次数设置为0
        private void comboBox_NowStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
