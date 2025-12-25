using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.LogDetail
{
    public class QueryLogToView
    {
        private DataGridView dataGridView_LogDetail;
         public QueryLogToView(DataGridView dataGridView_LogDetail)
        {
            this.dataGridView_LogDetail = dataGridView_LogDetail;
        }

         public void FillLogToView()
         {
             //查询Parameter_User表和Parameter_Role表内的所有数据，赋值到容器list_User中
             DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
            var list_Log = (from s in DBCon.LogDetail

                            select new
                            {
                                序号 = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            }) ; 
             //将list_Log容器中的数据赋给dataGridView_LogDetail的数据源DataSource
             dataGridView_LogDetail.DataSource = list_Log;
         }
        public void FillLogToView1(DateTime DateTimePickerValue1,DateTime DateTimePickerValue2)
        {
            //查询Parameter_User表和Parameter_Role表内的所有数据，赋值到容器list_User中
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
            var list_Log = (from s in DBCon.LogDetail
                            where s.LogDate>= DateTimePickerValue1 && s.LogDate<= DateTimePickerValue2
                            select new
                            {
                                序号 = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            });
            //将list_Log容器中的数据赋给dataGridView_LogDetail的数据源DataSource
            dataGridView_LogDetail.DataSource = list_Log;
        }
        public void QuerydataToView1(DateTime DateTimePickerValue1, DateTime DateTimePickerValue2,String usname)
         {
             //查询Parameter_User表和Parameter_Role表内的所有数据，赋值到容器list_User中
             DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
             var list_Log = (from s in DBCon.LogDetail
                              
                             where s.UsName == usname && s.LogDate >= DateTimePickerValue1 && s.LogDate <= DateTimePickerValue2
                             select new
                             {
                                 序号 = s.LogID,
                                 登录时间 = s.LogDate,
                                 用户名 = s.UsName,
                                 操作 = s.ActionType,
                                 内容 = s.LogMessage

                             });
             //将list_User容器中的数据赋给dataGridView_UserManage的数据源DataSource
             dataGridView_LogDetail.DataSource = list_Log;
         }
        public void QuerydataToView(String usname)
        {
            //查询Parameter_User表和Parameter_Role表内的所有数据，赋值到容器list_User中
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();
            var list_Log = (from s in DBCon.LogDetail

                            where s.UsName == usname
                            select new
                            {
                                序号 = s.LogID,
                                登录时间 = s.LogDate,
                                用户名 = s.UsName,
                                操作 = s.ActionType,
                                内容 = s.LogMessage

                            });
            //将list_User容器中的数据赋给dataGridView_UserManage的数据源DataSource
            dataGridView_LogDetail.DataSource = list_Log;
        }
        public string ChangeType(bool flag)
         {
             return flag ? "有效" : "无效";
         }
    }
}
