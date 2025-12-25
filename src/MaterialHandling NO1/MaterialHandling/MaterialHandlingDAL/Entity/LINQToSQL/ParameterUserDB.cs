using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.LogHelper;
using MaterialHandlingUI;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL
{
    public class ParameterUserDB
    {
        public static void Insert( string roleName , string passWord ,
                            bool flagIsvalid , string trueName ,
                            DateTime lastLoginTime , DateTime tryLoginTime ,
                            int tryLoginCount , string roleID ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {

                    DataToDBEnts.Parameter_User parameterUser = new DataToDBEnts.Parameter_User();

                   
                    parameterUser.Role_Name = roleName;
                    parameterUser.Password = passWord;
                    parameterUser.Flag_Isvalid = flagIsvalid;
                    parameterUser.True_Name = trueName;
                    parameterUser.Last_Login_Time = lastLoginTime;
                    parameterUser.Try_Login_Time = tryLoginTime;
                    parameterUser.TryLoginCount = tryLoginCount;
                    parameterUser.RoleID = roleID;

                    DBContext.Parameter_User.InsertOnSubmit(parameterUser);
                    DBContext.SubmitChanges();
                }
            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
            }
        }

        public static void Delete( string sqlString ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    //DBContext.ExecuteCommand(sqlString);
                    var list = DBContext.Parameter_User.Where(s => s.Role_Name == sqlString);

                    DBContext.Parameter_User.DeleteAllOnSubmit(list);
                    DBContext.SubmitChanges();
                }
            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
            }
        }

        public static void Delete( string sqlString , params object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    DBContext.ExecuteCommand(sqlString , parameters);
                }
            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
            }
        }

        public static Parameter_User QueryOneData( string sqlString , params Object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    var c = DBContext.ExecuteQuery<Parameter_User>(sqlString , parameters);
                    return c.FirstOrDefault();
                }
            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
                return null;
            }
        }

        public static IEnumerable<TResult> Query<TResult>( string sqlString , params Object[] parameters ) {
            try {
                DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext();
                var c = DBContext.ExecuteQuery<TResult>(sqlString , parameters);
                return c;

            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
                return null;
            }
        }

        public static int Update( string sqlString , params Object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    var c = DBContext.ExecuteCommand(sqlString , parameters);
                    return c;
                }
            } catch( Exception e ) {
                LogToTest.SQLError(e.Message.ToString());
                return 0;
            }
        }
       
    }
}
