using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterRole
{
    public class ParameterRoleLinq
    {
        private static DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();

        public static String GetRoleGuid(String authority)
        {
            var roleGuid = from PR in DBCon.Parameter_Role
                           where PR.Authority == authority
                            select PR.RoleID;
            return roleGuid.FirstOrDefault().ToString();
        }
    }
}
