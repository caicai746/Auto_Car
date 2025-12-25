using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.User
{
    public class QueryUserToView
    {
        private DataGridView dataGridView_UserManage;
       

        public QueryUserToView(DataGridView dataGridView_UserManage)
        {
            this.dataGridView_UserManage = dataGridView_UserManage;
        }
       
        public void FillDataToView()
        {
            //查询Parameter_User表和Parameter_Role表内的所有数据，赋值到容器list_User中
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
            var list_User = (from s in DBCon.Parameter_User
                             join c in DBCon.Parameter_Role
                             on s.RoleID equals c.RoleID.ToString()
                             select new
                             {
                                 用户名 = s.Role_Name,
                                 密码 = "*",
                                 当前状态 = ChangeType(s.Flag_Isvalid),
                                 真实姓名 = s.True_Name,
                                 最近成功登陆时间 = s.Last_Login_Time,
                                 尝试登录时间 = s.Try_Login_Time,
                                 权限 = c.Authority
                             });
            //将list_User容器中的数据赋给dataGridView_UserManage的数据源DataSource
            dataGridView_UserManage.DataSource = list_User;
        }
       
        public void QueryDataToView(String username)
        {
            //根据具体用户名，查询Parameter_User表和Parameter_Role表内的数据，赋值到容器list_User中
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
            var list_User = (from s in DBCon.Parameter_User
                             join c in DBCon.Parameter_Role
                             on s.RoleID equals c.RoleID.ToString()
                             where s.Role_Name == username
                             select new
                             {
                                 用户名 = s.Role_Name,
                                 密码 = "*",
                                 当前状态 = ChangeType(s.Flag_Isvalid),
                                 真实姓名 = s.True_Name,
                                 最近成功登陆时间 = s.Last_Login_Time,
                                 尝试登录时间 = s.Try_Login_Time,
                                 权限 = c.Authority
                             });
            //将list_User容器中的数据赋给dataGridView_UserManage的数据源DataSource
            dataGridView_UserManage.DataSource = list_User;
        }
        //public string QueryDataToView1(String username)
        //{
        //    //根据具体用户名，查询Parameter_User表和Parameter_Role表内的数据，赋值到容器list_User中
        //    DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
        //    var list_User = (from s in DBCon.Parameter_User
        //                     join c in DBCon.Parameter_Role
        //                     on s.RoleID equals c.GUID.ToString()
        //                     where s.Role_Name == username
        //                     select new
        //                     {
        //                         用户名 = s.Role_Name,
        //                         密码 = s.Password,
        //                         当前状态 = ChangeType(s.Flag_Isvalid),
        //                         真实姓名 = s.True_Name,
        //                         最近成功登陆时间 = s.Last_Login_Time,
        //                         尝试登录时间 = s.Try_Login_Time,
        //                         权限 = c.Authority
        //                     });
        //    //将list_User容器中的数据赋给dataGridView_UserManage的数据源DataSource
        //    return ;
        //}
        //bool类型和String类型的转化
        public string ChangeType(bool flag)
        {
            return flag ? "有效" : "无效";
        }
    }
}
