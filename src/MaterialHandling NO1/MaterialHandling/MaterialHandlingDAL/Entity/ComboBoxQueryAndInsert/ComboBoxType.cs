using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ComboBoxQueryAndInsert
{
    public class ComboBoxType
    {
        private ComboBox comboBox_Type;

        public ComboBoxType(ComboBox comboBox_Type)
        {
            this.comboBox_Type = comboBox_Type;
        }

        public void ComboBoxTypeInsert()
        {
            //comboBox_Type.Items.Clear();
            var temp_Type = ParameterTypeDB.Query<String>("select ModelID from Parameter_Type");

            foreach (String Type in temp_Type.Distinct().OrderBy(a => a))
            {
                comboBox_Type.Items.Add(Type);
            }
        }
    }
}
