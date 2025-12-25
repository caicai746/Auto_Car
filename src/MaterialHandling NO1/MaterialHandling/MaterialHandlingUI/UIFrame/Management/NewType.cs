using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using MaterialHandling.MaterialHandlingDAL.Entity.ComboBoxQueryAndInsert;
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.BatteryType;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class NewType : Form
    {
        private DataGridView dataGridView_ParameterSetting;

        public NewType() {
            InitializeComponent();
        }

        public NewType( DataGridView dataGridView_ParameterSetting ) {
            // TODO: Complete member initialization
            InitializeComponent();
            this.dataGridView_ParameterSetting = dataGridView_ParameterSetting;
        }

        private void btn_Clear_Click( object sender, EventArgs e ) {
            this.comboBox_Type_Number.Text = "";
            this.battery_Name.Text = "";
            this.voltage_Standard_Value.Text = "";
            this.vMax.Text = "";
            this.vMin.Text = "";
            this.uSub.Text = "";
            this.resistance_Standard_Value.Text = "";
            this.rMax.Text = "";
            this.rMin.Text = "";
            this.rSub.Text = "";
            this.coefficient.Text = "";
        }

        /// <summary>
        /// 确定按钮之前的验证函数
        /// </summary>
        /// <returns></returns>
        private bool TypeCheck() 
        {
            String typeMessage = "";
            bool flagType = true;
            if( comboBox_Type_Number.Text == "" ) {
                typeMessage += "电池型号 ";
                flagType = false;
            }
            if( battery_Name.Text == "" ) {
                battery_Name.Text = "NULL";
            }
            if( voltage_Standard_Value.Text == "" ) {
                typeMessage += "电压标准值 ";
                flagType = false;
            }
           
            if( vMax.Text == "" ) {
                vMax.Text = "-1";
            }
          
            if( vMin.Text == "" ) {
                typeMessage += "电压下限值 ";
                flagType = false;
            }
          
            if( uSub.Text == "" ) {
                typeMessage += "电压偏差值 ";
                flagType = false;
            }
           
            if (resistance_Standard_Value.Text == "")
            {
                typeMessage += "内阻标准值";
                flagType = false;
            }
            if (rMax.Text == "")
            {
                rMax.Text = "-1";
            }
            if (rMin.Text == "")
            {
                typeMessage += "内阻下限值 ";
                flagType = false;
            }
            if (rSub.Text == "")
            {
                typeMessage += "内阻偏差值 ";
                flagType = false;
            }
            if (coefficient.Text == "")
            {
                typeMessage += "内阻偏差值 ";
                flagType = false;
            }
            if( !flagType ) {
                typeMessage += "填写值为空，请重新填写";
                MessageBox.Show(typeMessage);
            }
            try {
                float.Parse(voltage_Standard_Value.Text);
            } catch {
                MessageBox.Show("电压标准值输入非法，请重新输入！");
                voltage_Standard_Value.Text = "";
                return false;
            }
           
            try {
                float.Parse(vMax.Text);
            } catch {
                MessageBox.Show("电压上限值输入非法，请重新输入！");
                vMax.Text = "";
                return false;
            }
          
            try {
                float.Parse(vMin.Text);
            } catch {
                MessageBox.Show("电压下限值输入非法，请重新输入！");
                vMin.Text = "";
                return false;
            }
           
            try {
                float.Parse(uSub.Text);
            } catch {
                MessageBox.Show("电压偏差值输入非法，请重新输入！");
                uSub.Text = "";
                return false;
            }
            try
            {
                float.Parse(resistance_Standard_Value.Text);
            }
            catch
            {
                MessageBox.Show("内阻标准值输入非法，请重新输入！");
                resistance_Standard_Value.Text = "";
                return false;
            }
            try
            {
                float.Parse(rMax.Text);
            }
            catch
            {
                MessageBox.Show("内阻上限值输入非法，请重新输入！");
                rMax.Text = "";
                return false;
            }
            try
            {
                float.Parse(rMin.Text);
            }
            catch
            {
                MessageBox.Show("内阻下限值输入非法，请重新输入！");
                rMin.Text = "";
                return false;
            }
            try
            {
                float.Parse(rSub.Text);
            }
            catch
            {
                MessageBox.Show("内阻偏差值输入非法，请重新输入！");
                rSub.Text = "";
                return false;
            }
            try
            {
                float.Parse(coefficient.Text);
            }
            catch
            {
                MessageBox.Show("内阻系数输入非法，请重新输入！");
                coefficient.Text = "";
                return false;
            }
            return flagType;
        }

        //给确定按钮加监听器
        private void btn_Submit_Click( object sender, EventArgs e ) {
            if( TypeCheck() ) {
                //去数据表中查询该型号是否已存在
                String new_TypeNumber = comboBox_Type_Number.Text.Trim();
                String sqlString = "select * from Parameter_Type where ModelID={0}";
                var type = ParameterTypeDB.QueryOneData(sqlString, new_TypeNumber);
                if( type == null ) //如不存在则插入数据库中
                {
                    float new_Voltage_Standard_Value, new_Vmax, new_Vmin, new_Usub,
                        new_Resistance_Standard_Value, new_Rmax, new_Rmin, new_Rsub, new_coefficient;
                    new_Voltage_Standard_Value = float.Parse(voltage_Standard_Value.Text);
                  
                    try {
                        new_Vmax = float.Parse(vMax.Text);
                    } catch {
                        new_Vmax = -1;
                    }

                    new_Vmin = float.Parse(vMin.Text);
                    new_Usub = float.Parse(uSub.Text);

                    new_Resistance_Standard_Value = float.Parse(resistance_Standard_Value.Text);
                    try
                    {
                        new_Rmax = float.Parse(rMax.Text);
                    }
                    catch
                    {
                        new_Rmax = -1;
                    }

                    new_Rmin = float.Parse(rMin.Text);
                    new_Rsub = float.Parse(rSub.Text);
                    new_coefficient = float.Parse(coefficient.Text);

                    //向数据库中添加新纪录                  
                    ParameterTypeDB.Insert(comboBox_Type_Number.Text, battery_Name.Text, new_Voltage_Standard_Value, new_Vmax, new_Vmin, new_Usub,new_Resistance_Standard_Value,new_Rmax,new_Rmin,new_Rsub,new_coefficient);

                    //对参数设置(电池类型)进行数据更新
                    QueryTypeToView queryTypeToView = new QueryTypeToView(dataGridView_ParameterSetting);
                    queryTypeToView.FillDataToView();

                    //关闭窗口
                    this.Close();
                } else //如存在则重新输入
                {
                    MessageBox.Show("该电池型号已存在，请重新输入");
                    comboBox_Type_Number.Text = "";
                }
            }
        }

        private void comboBox_Type_Number_DropDown( object sender, EventArgs e ) {
           
        }

        private void comboBox_Type_Number_TextUpdate( object sender, EventArgs e ) {
            var temp_Type = ParameterTypeDB.Query<String>("select ModelID from Parameter_Type");
            MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery.VagueQuery.ResultByVagueQuery(comboBox_Type_Number, temp_Type.ToList<string>());
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void comboBox_Type_Number_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void battery_Name_TextChanged(object sender, EventArgs e)
        {

        }

        private void voltage_Standard_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void label30_Click(object sender, EventArgs e)
        {

        }
    }
}
