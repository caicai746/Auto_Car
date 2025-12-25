using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery
{
    public class VagueQuery
    {
        public static void ResultByVagueQuery(ComboBox comboBox, List<string> temp_Type)
        {
            comboBox.Items.Clear();
            List<string> listNew = new List<string>();
            comboBox.DroppedDown = true;
            string text = comboBox.Text;
            //temp_Type.Add(text);
            foreach (var item in temp_Type.Distinct().OrderBy(a => a))
            {
                if (item.Contains(text))
                {
                    listNew.Add(item);
                }
            }
            if (listNew.Count != 0)
            {
                comboBox.Items.AddRange(listNew.Distinct().OrderBy(a => a).ToArray());
            }
            else
            {
                comboBox.Items.Add(text);
            }
            comboBox.Text = text;
            comboBox.SelectionStart = comboBox.Text.Length;
            Cursor.Current = Cursors.Default;
        }

        public static void ResultByVagueQueryForSerial(ComboBox comboBox, List<int> temp_Type)
        {
            comboBox.Items.Clear();
            List<int> listNew = new List<int>();
            comboBox.DroppedDown = true;
            string text = comboBox.Text;
            foreach (var item in temp_Type.Distinct().OrderByDescending(x => x))
            {
                string itemString = item.ToString();
                if (itemString.Contains(text))
                {
                    listNew.Add(item);
                }
            }
            if (listNew.Count != 0)
            {
                comboBox.Items.AddRange(listNew.ConvertAll<string>(x => x.ToString()).ToArray());
            }
            else
            {
                comboBox.Items.Add(text);
            }
            comboBox.Text = text;
            comboBox.SelectionStart = comboBox.Text.Length;
            Cursor.Current = Cursors.Default;
        }

    }
}
