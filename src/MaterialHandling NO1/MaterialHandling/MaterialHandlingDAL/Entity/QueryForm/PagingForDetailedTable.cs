using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace MaterialHandling.MaterialHandlingDAL.Entity.QueryForm
{
    public class PagingForDetailedTable
    {
        private DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
        private string DetailedPara;           //查询参数
        private float deviation;                //实际电压偏差值
        private float Rdeviation;               //实际内阻偏差值
        private List<float> normalVol = new List<float>();      //正常电池电压
        private List<float> normalRes = new List<float>();      //正常电池内阻
        private List<float> allVol = new List<float>();         //所有电池电压
        private List<float> allRes = new List<float>();       //所有电池内阻
        private List<DateTime> TestTimeOfNormalVol = new List<DateTime>();     //正常电池的检测时间
        private List<string> allVolFlag = new List<string>();                //所有电池的电压flag
        private List<string> allResFlag = new List<string>();                //所有电池的内阻flag

        /// <summary>
        /// 获取明细结果记录总数
        /// </summary>
        /// <param name="invoiceForGUID"></param>
        /// <returns></returns>
        public int GetDetailedCount(string batchID)
        {
            this.DetailedPara = batchID;
            var detailedList = from PC in DBCon.Production_Check.Where(x => x.BatchID == DetailedPara)

                               join PB in DBCon.Production_Batch
                               on PC.BatchID equals PB.GUID.ToString()

                               select new
                               {
                                   电池编码 = PC.Battery_Serial_No,
                                   检测时间 = PC.Test_Time,
                                   站点地址 = PB.Station_Address,
                                   温度 = PC.Temperature,
                                   标志 = PC.Flag,
                                   电压 = PC.Open_Voltage,
                                   标准电压 = PC.Voltage_Standard_Value,
                                   内阻 = PC.Open_Resistance,
                                   标准内阻 = PC.Resistance_Standard_Value



                               };
            return detailedList.Count();
        }

        public void GetPagingDetailed(int pageSize, int pageNum, DataGridView dataGridView)
        {
            var detailedList = from PC in DBCon.Production_Check.Where(x => x.BatchID == DetailedPara)

                               join PB in DBCon.Production_Batch
                               on PC.BatchID equals PB.GUID.ToString()

                               select new
                               {
                                   算法 = PB.Algorithm,
                                   电池编码 = PC.Battery_Serial_No,
                                   检测时间 = PC.Test_Time,
                                   站点地址 = PB.Station_Address,
                                   温度 = PC.Temperature,
                                   电压 = PC.Open_Voltage,
                                   标准电压 = PC.Voltage_Standard_Value,
                                   电压标志 = PC.Flag,
                                   内阻 = PC.Open_Resistance,
                                   标准内阻 = PC.Resistance_Standard_Value,
                                   内阻标志 = PC.Flag,
                                   每条记录的ID = PC.GUID,
                                   每只电池对应匹配的信息=PC.Battery_P1
                               };
            detailedList = detailedList.OrderBy(x => x.检测时间).ThenByDescending(x => x.站点地址).Skip(pageSize * (pageNum - 1)).Take(pageSize);

            int i = 0;
            int j = (pageNum - 1) * pageSize;
            foreach (var a in detailedList)
            {
                int index = dataGridView.Rows.Add();
                dataGridView.Rows[i].Cells[0].Value = (j + 1).ToString("000");
                dataGridView.Rows[i].Cells[1].Value = a.电池编码;
                dataGridView.Rows[i].Cells[2].Value = a.检测时间;
                dataGridView.Rows[i].Cells[3].Value = a.温度;
                dataGridView.Rows[i].Cells[4].Value = a.电压.ToString("F3");
                dataGridView.Rows[i].Cells[5].Value = a.标准电压.ToString("F3");
                dataGridView.Rows[i].Cells[6].Value = a.电压标志;
                dataGridView.Rows[i].Cells[7].Value = Convert.ToDouble(a.内阻).ToString("F3");
                dataGridView.Rows[i].Cells[8].Value = Convert.ToDouble(a.标准内阻).ToString("F3");
                dataGridView.Rows[i].Cells[9].Value = a.内阻标志;
                dataGridView.Rows[i].Cells[10].Value = a.每只电池对应匹配的信息;
                dataGridView.Rows[i].Cells[11].Value = a.算法;                             
                dataGridView.Rows[i].Cells[12].Value = a.每条记录的ID;





                //根据标志将数据显示红色，即标志为2,3,4的数据显示红色，即为无效（具体显示）;0,1为有效
                switch (a.电压标志)
                {
                    case 0:
                        dataGridView.Rows[i].Cells[5].Value = "合格";
                        break;
                    case 1:
                        dataGridView.Rows[i].Cells[5].Value = "合格";
                        break;
                    case 2:
                        if (a.算法 == 1)
                        {
                            dataGridView.Rows[i].Cells[5].Value = "超上限";
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[5].Value = "不合格";
                        }

                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 3:
                        dataGridView.Rows[i].Cells[5].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 4:
                        dataGridView.Rows[i].Cells[5].Value = "超默认下限";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 5:
                        dataGridView.Rows[i].Cells[5].Value = "合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 6:
                        dataGridView.Rows[i].Cells[5].Value = "合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 7:
                        dataGridView.Rows[i].Cells[5].Value = "超上限";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 8:
                        if (a.算法 == 1 || a.算法 == 3)
                        {
                            dataGridView.Rows[i].Cells[5].Value = "合格";
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[5].Value = "超下限";
                        }
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 9:
                        dataGridView.Rows[i].Cells[5].Value = "超下限";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;



                }

                switch (a.内阻标志)
                {
                    case 0:
                        dataGridView.Rows[i].Cells[8].Value = "合格";
                        break;
                    case 1:
                        dataGridView.Rows[i].Cells[8].Value = "合格";
                        break;
                    case 2:
                        dataGridView.Rows[i].Cells[8].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 3:
                        dataGridView.Rows[i].Cells[8].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 4:
                        dataGridView.Rows[i].Cells[8].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 5:
                        if (a.算法 == 1)
                        {
                            dataGridView.Rows[i].Cells[8].Value = "不合格";
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[8].Value = "超默认下限";
                        }
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 6:
                        if (a.算法 == 1 || a.算法 == 3)
                        {
                            dataGridView.Rows[i].Cells[8].Value = "超上限";
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[8].Value = "超默认上限";
                        }
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 7:
                        dataGridView.Rows[i].Cells[8].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 8:
                        if (a.算法 == 1 || a.算法 == 3)
                        {
                            dataGridView.Rows[i].Cells[8].Value = "超下限";
                        }
                        else
                        {
                            dataGridView.Rows[i].Cells[8].Value = "不合格";
                        }
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;
                    case 9:
                        dataGridView.Rows[i].Cells[8].Value = "不合格";
                        for (int idxColumn = 0; idxColumn < dataGridView.ColumnCount; idxColumn++)
                        {
                            dataGridView.Rows[i].Cells[idxColumn].Style.ForeColor = Color.Red;
                        }
                        break;

                }

                i++;
                j++;
            }



        }

        public void GetDataToChart()
        {
            var detailedList = (from PC in DBCon.Production_Check.Where(x => x.BatchID == DetailedPara)

                                join PB in DBCon.Production_Batch
                                on PC.BatchID equals PB.GUID.ToString()

                                select new
                                {
                                    检测时间 = PC.Test_Time,
                                    站点地址 = PB.Station_Address,
                                    电压 = PC.Open_Voltage,
                                    内阻 = (float)PC.Open_Resistance,
                                    标准电压 = PC.Voltage_Standard_Value,
                                    温度 = PC.Temperature,
                                    标志 = PC.Flag,
                                    算法 = PB.Algorithm
                                }).OrderBy(x => x.检测时间).ThenByDescending(x => x.站点地址);

            //计算偏差值
            //float deviation = 0f;
            float max = 0f;
            float min = 10000f;//表示一个很大的数
            float Rmax = 0f;
            float Rmin = 0f;
            foreach (var a in detailedList)
            {
                allVol.Add(a.电压);   //装载所有电压
                allRes.Add(a.内阻);   //装载所有内阻
                switch (a.标志)
                {
                    case 0:
                    case 1:
                        normalVol.Add(a.电压);    //装载正常电压
                        normalRes.Add(a.内阻);    //装载正常内阻
                        TestTimeOfNormalVol.Add(a.检测时间);    //装载正常电压的检测时间
                        allVolFlag.Add("合格");   //合格电压的flag
                        allResFlag.Add("合格");   //合格内阻的flag

                        //获得正常电池中的电压最大最小值
                        if (a.电压 >= max)
                        {
                            max = a.电压;
                        }
                        if (a.电压 <= min)
                        {
                            min = a.电压;
                        }

                        //获得正常电池中的内阻最大最小值
                        if (a.内阻 >= Rmax)
                        {
                            Rmax = a.内阻;
                        }
                        if (a.内阻 <= Rmin)
                        {
                            Rmin = a.内阻;
                        }



                        break;

                    case 2:
                        if (a.算法 == 1)
                        {
                            allVolFlag.Add("超上限");
                            allResFlag.Add("不合格");
                        }
                        else
                        {
                            allVolFlag.Add("不合格");
                            allResFlag.Add("不合格");
                        }


                        break;
                    case 3:
                        allVolFlag.Add("不合格");
                        allResFlag.Add("不合格");
                        break;

                    case 4:
                        allVolFlag.Add("超默认下限");
                        allResFlag.Add("不合格");
                        break;
                    case 5:
                        allVolFlag.Add("合格");
                        if (a.算法 == 1)
                        {
                            allResFlag.Add("不合格");
                        }
                        else
                        {
                            allResFlag.Add("超默认下限");
                        }
                        break;

                    case 6:
                        allVolFlag.Add("合格");
                        if (a.算法 == 1 || a.算法 == 3)
                        {
                            allResFlag.Add("超上限");
                        }
                        else
                        {
                            allResFlag.Add("超默认上限");
                        }
                        break;

                    case 7:
                        allVolFlag.Add("超上限");
                        allResFlag.Add("不合格");


                        break;

                    case 8:
                        if (a.算法 == 1 || a.算法 == 3)
                        {
                            allVolFlag.Add("合格");
                            allResFlag.Add("超下限");
                        }
                        else
                        {
                            allVolFlag.Add("超下限");
                            allResFlag.Add("不合格");
                        }
                        break;
                    case 9:

                        allVolFlag.Add("超下限");
                        allResFlag.Add("不合格");

                        break;



                    default:
                        break;
                }
            }
            deviation = max - min;      //正常电压的实际偏差
            Rdeviation = Rmax - Rmin;   //正常内阻的实际偏差
        }

        /// <summary>
        /// 获得实际电压偏差值
        /// </summary>
        /// <returns></returns>
        public float GetDeviation()
        {
            return deviation;
        }

        /// <summary>
        /// 获得实际内阻偏差值
        /// </summary>
        /// <returns></returns>
        public float GetRDeviation()
        {
            return Rdeviation;
        }

        /// <summary>
        /// 获得正常电压的集合
        /// </summary>
        /// <returns></returns>
        public List<float> GetNormalVol()
        {
            return normalVol;
        }

        /// <summary>
        /// 获得正常内阻的集合
        /// </summary>
        /// <returns></returns>
        public List<float> GetNormalRes()
        {
            return normalRes;
        }

        /// <summary>
        /// 获得所有电池的集合
        /// </summary>
        /// <returns></returns>
        public List<float> GetAllVol()
        {
            return allVol;
        }

        public List<float> GetAllRes()
        {
            return allRes;
        }

        /// <summary>
        /// 获得正常电压的检测时间的集合
        /// </summary>
        /// <returns></returns>
        public List<DateTime> GetTestTimeOfNormalVol()
        {
            return TestTimeOfNormalVol;
        }

        /// <summary>
        /// 获得所有电池的电压flag
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllVolFlag()
        {
            return allVolFlag;
        }

        /// <summary>
        /// 获得所有电池的内阻flag
        /// </summary>
        /// <returns></returns>
        public List<string> GetAllResFlag()
        {
            return allResFlag;
        }

    }
}