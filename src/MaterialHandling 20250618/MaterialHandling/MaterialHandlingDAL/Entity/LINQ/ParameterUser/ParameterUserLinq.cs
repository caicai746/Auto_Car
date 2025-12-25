using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterUser
{
    public class ParameterUserLinq
    {
        private static DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();

        public static String GetRoleGuidByUsername(String username)
        {
            var roleGuid = from PU in DBCon.Parameter_User
                           where PU.Role_Name == username
                           select PU.RoleID;
            return roleGuid.ToList().First();
        }
    }
}
