using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.LogHelper;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL
{
    public class ParameterRoleDB
    {
        //插入一条新的记录
        public static void Insert(  string roleName ,
                            string authority ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {

                    DataToDBEnts.Parameter_Role parameterRole = new DataToDBEnts.Parameter_Role();

                    
                    parameterRole.Role_Name = roleName;
                    parameterRole.Authority = authority;

                    DBContext.Parameter_Role.InsertOnSubmit(parameterRole);
                    DBContext.SubmitChanges();
                }
            } catch( Exception e ) {
               // LogToTest.SQLError(e.Message.ToString());
            }
        }

        //删除记录
        public static void Delete( string sqlString ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    //DBContext.ExecuteCommand(sqlString);
                    var list = DBContext.Parameter_Role.Where(s => s.Role_Name == sqlString);

                    DBContext.Parameter_Role.DeleteAllOnSubmit(list);
                    DBContext.SubmitChanges();
                }
            } catch( Exception e ) {
                //LogToTest.SQLError(e.Message.ToString());
            }
        }

        //删除记录
        public static void Delete( string sqlString , params object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    DBContext.ExecuteCommand(sqlString , parameters);
                }
            } catch( Exception e ) {
               // LogToTest.SQLError(e.Message.ToString());
            }
        }

        //查询一条记录
        public static Parameter_Role QueryOneData( string sqlString , params Object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    var c = DBContext.ExecuteQuery<Parameter_Role>(sqlString , parameters);
                    return c.FirstOrDefault();
                }
            } catch( Exception e ) {
               // LogToTest.SQLError(e.Message.ToString());
                return null;
            }
        }

        //查询多条记录
        public static IEnumerable<Parameter_Role> Query( string sqlString , params Object[] parameters ) {
            try {
                DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext();
                var c = DBContext.ExecuteQuery<Parameter_Role>(sqlString , parameters);
                return c;
            } catch( Exception e ) {
              //  LogToTest.SQLError(e.Message.ToString());
                return null;
            }
        }

        //更新数据
        public static int Update( string sqlString , params Object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    var c = DBContext.ExecuteCommand(sqlString , parameters);
                    return c;
                }
            } catch( Exception e ) {
               // LogToTest.SQLError(e.Message.ToString());
                return 0;
            }
        }
    }
}
