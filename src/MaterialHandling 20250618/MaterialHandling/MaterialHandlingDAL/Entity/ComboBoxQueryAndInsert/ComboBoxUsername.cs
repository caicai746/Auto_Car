using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ComboBoxQueryAndInsert
{
    public class ComboBoxUsername
    {
        private ComboBox comboBox_Username;

        public ComboBoxUsername(ComboBox comboBox_Username)
        {
            this.comboBox_Username = comboBox_Username;
        }

        public void ComboBoxUsernameInsert()
        {
            comboBox_Username.Items.Clear();
            var temp_Username = ParameterUserDB.Query<String>("select Role_Name from Parameter_User");

            foreach (String Username in temp_Username.Distinct())
            {
                comboBox_Username.Items.Add(Username);
            }
        }
    }
}
