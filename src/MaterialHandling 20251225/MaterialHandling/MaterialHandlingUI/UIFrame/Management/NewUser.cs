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
using MaterialHandling.MaterialHandlingDAL.Entity.ComboBoxQueryAndInsert;
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.User;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterRole;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class NewUser : Form
    {
        private DataGridView dataGridView_UserManage;

        public NewUser()
        {
            InitializeComponent();
        }

        public NewUser(DataGridView dataGridView_UserManage)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dataGridView_UserManage = dataGridView_UserManage;
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            this.comboBox_userName.Text = "";
            this.passWord.Text = "";
            this.checkPassword.Text = "";
            this.comboBox_authority.SelectedIndex = -1;
            this.true_Name.Text = "";
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

        //确定按钮之前的验证函数是否为空
        private bool UserCheckEmpty()
        {
            String userMessage = "";
            bool flagUserEmpty = true;
            if (comboBox_userName.Text == "")
            {
                userMessage += "用户名 ";
                flagUserEmpty = false;
            }
            if (passWord.Text == "")
            {
                userMessage += "密码 ";
                flagUserEmpty = false;
            }
            if (checkPassword.Text == "")
            {
                userMessage += "确认密码 ";
                flagUserEmpty = false;
            }
            if (comboBox_authority.Text == "")
            {
                userMessage += "权限 ";
                flagUserEmpty = false;
            }
            if (true_Name.Text == "")
            {
                userMessage += "真实姓名 ";
                flagUserEmpty = false;
            }
            if (!flagUserEmpty)
            {
                userMessage += "填写值为空，请重新填写";
                MessageBox.Show(userMessage);
            }
            return flagUserEmpty;
        }

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            if (UserCheckEmpty() && UserCheckPassword())
            {
                String new_Username, new_Password, new_TrueName, new_Authority = "";
                DateTime new_Datetime;
                new_Username = comboBox_userName.Text;
                new_Password = GetMD5.GetMD5WithString(passWord.Text);
                new_TrueName = true_Name.Text;

                //查询数据表中的用户名
                String sqlString = "select * from Parameter_User where Role_Name={0}";
                var user = ParameterUserDB.QueryOneData(sqlString, new_Username);

                if (user == null) //如果用户名在数据表中不存在，则插入该数据
                {
                    //获取用户权限
                    int idx = comboBox_authority.SelectedIndex;
                    switch (idx)
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

                    var roleGuid = ParameterRoleLinq.GetRoleGuid(new_Authority);
                    new_Datetime = DateTime.Now;

                    //插入到Parameter_Role表中
                    //ParameterRoleDB.Insert(Guid.NewGuid(), new_Username, new_Authority);

                    //插入到Parameter_User表中
                    ParameterUserDB.Insert(new_Username, new_Password, true, new_TrueName, new_Datetime, new_Datetime, 0, roleGuid.ToString());

                    //重新刷新dategridview的内容
                    QueryUserToView queryUserToView = new QueryUserToView(dataGridView_UserManage);
                    queryUserToView.FillDataToView();

                    //关闭窗口
                    this.Close();
                }
                else
                {
                    MessageBox.Show("该用户名已被注册，请重新输入");
                    comboBox_userName.Text = "";
                }
            }
        }

        public string ChangeType(bool flag)
        {
            return flag ? "有效" : "无效";
        }

        private void userName_DropDown(object sender, EventArgs e)
        {
            //ComboBoxUsername comboBoxUsername = new ComboBoxUsername(comboBox_userName);
            //comboBoxUsername.ComboBoxUsernameInsert();
        }

        private void comboBox_userName_TextUpdate(object sender, EventArgs e)
        {
            var temp_User = ParameterUserDB.Query<String>("select Role_Name from Parameter_User");
            MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery.VagueQuery.ResultByVagueQuery(comboBox_userName, temp_User.ToList<string>());
        }
    }
}
