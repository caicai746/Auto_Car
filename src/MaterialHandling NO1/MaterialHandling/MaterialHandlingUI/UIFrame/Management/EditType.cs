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
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.BatteryType;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class EditType : Form
    {
        private string[] needEditData;
        private DataGridView dataGridView_ParameterSetting;
        public int typeid;

        public EditType()
        {
            InitializeComponent();
        }

        public EditType(string[] needEditData, DataGridView dataGridView_ParameterSetting)
        {
            // TODO: Complete member initialization
            InitializeComponent();
            this.needEditData = needEditData;
            this.dataGridView_ParameterSetting = dataGridView_ParameterSetting;

            this.comboBox_Type_Number.Text = needEditData[0];
            this.battery_Name.Text = needEditData[1];
            this.voltage_Standard_Value.Text = needEditData[2];
            this.vMin.Text = needEditData[3];
            this.uSub.Text = needEditData[4]; 
            this.vMax.Text = ParameterTypeDB.QueryOneData("select * from Parameter_Type where ModelID={0}", needEditData[0]).Voltage_Max.ToString();
            this.resistance_Standard_Value.Text = needEditData[5];
            this.rMin.Text = needEditData[6];
            this.rSub.Text = needEditData[7];
            this.rMax.Text = ParameterTypeDB.QueryOneData("select * from Parameter_Type where ModelID={0}", needEditData[0]).Resistance_Max.ToString();
            this.coefficient.Text = needEditData[8];
            this.typeid = int.Parse(needEditData[9]);
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
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

        //确定按钮之前的验证函数
        private bool TypeCheck()
        {
            String typeMessage = "";
            bool flagType = true;
            if (comboBox_Type_Number.Text == "")
            {
                typeMessage += "电池型号 ";
                flagType = false;
            }
            if (battery_Name.Text == "")
            {
                battery_Name.Text = "NULL";
            }
            if (voltage_Standard_Value.Text == "")
            {
                typeMessage += "电压标准值 ";
                flagType = false;
            }

            if (vMax.Text == "")
            {
                vMax.Text = "-1";
            }

            if (vMin.Text == "")
            {
                typeMessage += "电压下限值 ";
                flagType = false;
            }

            if (uSub.Text == "")
            {
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
            if (!flagType)
            {
                typeMessage += "填写值为空，请重新填写";
                MessageBox.Show(typeMessage);
            }
            try
            {
                float.Parse(voltage_Standard_Value.Text);
            }
            catch
            {
                MessageBox.Show("电压标准值输入非法，请重新输入！");
                voltage_Standard_Value.Text = "";
                return false;
            }

            try
            {
                float.Parse(vMax.Text);
            }
            catch
            {
                MessageBox.Show("电压上限值输入非法，请重新输入！");
                vMax.Text = "";
                return false;
            }

            try
            {
                float.Parse(vMin.Text);
            }
            catch
            {
                MessageBox.Show("电压下限值输入非法，请重新输入！");
                vMin.Text = "";
                return false;
            }

            try
            {
                float.Parse(uSub.Text);
            }
            catch
            {
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

        private void btn_Submit_Click(object sender, EventArgs e)
        {
            try
            {
                if (TypeCheck())
                {
                    float new_Voltage_Standard_Value, new_Vmax, new_Vmin, new_Usub,
                          new_Resistance_Standard_Value, new_Rmax, new_Rmin, new_Rsub, new_coefficient;
                    
                    new_Voltage_Standard_Value = float.Parse(voltage_Standard_Value.Text);
                    try
                    {
                        new_Vmax = float.Parse(vMax.Text);
                    }
                    catch
                    {
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
                    //向数据库中修改该纪录
                    String sqlString = "update Parameter_Type set ModelID={0}, TypeName={1},Voltage_Standard_Value={2},Voltage_Max={3},Voltage_Min={4},U_Phase_Impairment={5}, Resistance_Standard_Value ={6},Resistance_Max={7},Resistance_Min={8}, R_Phase_Impairment={9}, coefficient={10} where typeid={11}";
                    ParameterTypeDB.Update(sqlString, comboBox_Type_Number.Text, battery_Name.Text, new_Voltage_Standard_Value, new_Vmax, new_Vmin, new_Usub, new_Resistance_Standard_Value, new_Rmax, new_Rmin, new_Rsub, new_coefficient, typeid);
                    //对参数设置(电池类型)进行数据更新
                    QueryTypeToView queryTypeToView = new QueryTypeToView(dataGridView_ParameterSetting);
                    queryTypeToView.FillDataToView();

                    //关闭窗口
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("数据输入不合法，请重新输入");
            }

        }

        private void voltage_Standard_Value_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_Type_Number_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
