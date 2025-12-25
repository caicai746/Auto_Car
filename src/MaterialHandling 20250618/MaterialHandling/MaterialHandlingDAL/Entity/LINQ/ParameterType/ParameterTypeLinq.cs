using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterType
{
    public class ParameterTypeLinq
    {
        private static DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();

        public static String GetModelIdByTypeId(String typeId)
        {
            var modelId = from PT in DBCon.Parameter_Type
                           where PT.typeid.ToString() == typeId
                           select PT.ModelID;
            return modelId.ToList().First();
        }

        public static int GetTypeIdByModelId(String modelId)
        {
            var typeId = from PT in DBCon.Parameter_Type
                          where PT.ModelID == modelId
                          select PT.typeid;
            return typeId.ToList().First();
        }

        public static String GetDeviationVolByModelId( String modelId ) {
            var typeId = from PT in DBCon.Parameter_Type
                         where PT.ModelID == modelId
                         select PT.U_Phase_Impairment;
            return typeId.ToList().First().ToString("F3");
        }

        public static String GetUpLimitByModelId(String modelId) {
            var typeId = from PT in DBCon.Parameter_Type
                         where PT.ModelID == modelId
                         select PT.Voltage_Min;
            return typeId.ToList().First().ToString("F3");
        }
    }
}
