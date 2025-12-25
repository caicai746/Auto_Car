using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterType;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;

namespace MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.BatteryType
{
    public class QueryTypeToView
    {
        private DataGridView dataGridView_ParameterSetting;

        public QueryTypeToView(DataGridView dataGridView_ParameterSetting)
        {
            this.dataGridView_ParameterSetting = dataGridView_ParameterSetting;    
        }

        public void FillDataToView()
        {
            //查询Parameter_Type表内的所有数据，赋值到容器list_Parameter中
            var list_Parameter = (from s in new DataToDBCont.DataToDBDataContext().Parameter_Type
                                  select new
                                  {
                                      型号 = s.ModelID,
                                      类型名称 = s.TypeName,
                                      电压标准值 = s.Voltage_Standard_Value,
                                      电压下限值 = s.Voltage_Min,
                                      电压偏差值 = s.U_Phase_Impairment,
                                      内阻标准值 = s.Resistance_Standard_Value,
                                      内阻下限值 = s.Resistance_Min,
                                      内阻偏差值 = s.R_Phase_Impairment,
                                      内阻系数 = s.coefficient
                                  } ).OrderBy(a=>a.型号);
            //将list_Parameter容器中的数据赋给dataGridView_ParameterSetting的数据源DataSource

            
            dataGridView_ParameterSetting.DataSource = list_Parameter;
        }

        public void QueryDataToView(String queryType)
        {
            //根据具体的型号，查询Parameter_Type表内的数据，赋值到容器list_Parameter中
            var list_Parameter = (from s in new DataToDBCont.DataToDBDataContext().Parameter_Type
                                  where s.ModelID == queryType
                                  select new
                                  {
                                      型号 = s.ModelID,
                                      类型名称 = s.TypeName,
                                      电压标准值 = s.Voltage_Standard_Value,
                                      电压下限值 = s.Voltage_Min,
                                      电压偏差值 = s.U_Phase_Impairment,
                                      内阻标准值 = s.Resistance_Standard_Value,
                                      内阻下限值 = s.Resistance_Min,
                                      内阻偏差值 = s.R_Phase_Impairment,
                                      内阻系数 = s.coefficient
                                  });
            //将list_Parameter容器中的数据赋给dataGridView_ParameterSetting的数据源DataSource
            dataGridView_ParameterSetting.DataSource = list_Parameter;
        }
    }
}
