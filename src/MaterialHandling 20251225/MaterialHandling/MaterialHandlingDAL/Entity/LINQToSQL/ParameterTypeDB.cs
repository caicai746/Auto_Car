using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.LogHelper;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL
{
    public class ParameterTypeDB
    {
        public static void Insert(  string modelID , string typeName ,
                            float voltageStandardValue , float voltageMax ,
                            float voltageMin , float uPhaseImpairment ,
                            float resistanceStandardValue , float resistanceMax , 
                            float resistanceMin , float rPhaseImpairment,float coefficient) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {

                    DataToDBEnts.Parameter_Type parameterType = new DataToDBEnts.Parameter_Type();

                    
                    parameterType.ModelID = modelID;
                    parameterType.TypeName = typeName;
                    parameterType.Voltage_Standard_Value = voltageStandardValue;
                    parameterType.Voltage_Max = voltageMax;
                    parameterType.Voltage_Min = voltageMin;
                    parameterType.U_Phase_Impairment = uPhaseImpairment;
                    parameterType.Resistance_Standard_Value = resistanceStandardValue;
                    parameterType.Resistance_Max = resistanceMax;
                    parameterType.Resistance_Min = resistanceMin;
                    parameterType.R_Phase_Impairment = rPhaseImpairment;
                    parameterType.coefficient = coefficient;
                    //parameterType.Test_Amount = testAmount;

                    DBContext.Parameter_Type.InsertOnSubmit(parameterType);
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
                    var list = DBContext.Parameter_Type.Where(s => s.TypeName == sqlString);

                    DBContext.Parameter_Type.DeleteAllOnSubmit(list);
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

        public static Parameter_Type QueryOneData(  string sqlString , params Object[] parameters ) {
            try {
                using( DataToDBCont.DataToDBDataContext DBContext = new DataToDBCont.DataToDBDataContext() ) {
                    var c = DBContext.ExecuteQuery<Parameter_Type>(sqlString , parameters);
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
