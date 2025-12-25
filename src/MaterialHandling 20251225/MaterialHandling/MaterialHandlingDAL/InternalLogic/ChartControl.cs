using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    public class ChartControl
    {
        /// <summary>
        /// 对Chart控件初始化
        /// </summary>
        /// <param name="thisChart"></param>
        /// <param name="upperLimit"></param>
        /// <param name="downLimit"></param>
        public static void InitChart(Chart thisChart, float upperLimit, float downLimit)
        {
            //定义图表区域
            thisChart.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("ValueShowOfVol");
            thisChart.ChartAreas.Add(chartArea1);

            //定义储存和显示点的容器
            thisChart.Series.Clear();

            Series series1 = new Series("电压");
            Series series2 = new Series("上限值");
            Series series3 = new Series("下限值");
            Series series4 = new Series("平均值");
           

            series1.ChartArea = "ValueShowOfVol";
            series2.ChartArea = "ValueShowOfVol";
            series3.ChartArea = "ValueShowOfVol";
            series4.ChartArea = "ValueShowOfVol";
        

            thisChart.Series.Add(series1);
            thisChart.Series.Add(series2);
            thisChart.Series.Add(series3);
            thisChart.Series.Add(series4);
         


            //设置图表显示样式
            try
            {
                thisChart.ChartAreas[0].AxisY.Minimum = downLimit;
                thisChart.ChartAreas[0].AxisY.Maximum = upperLimit;
            }
            catch
            {
                MessageBox.Show("上下限设置有误！");
            }

            thisChart.ChartAreas[0].AxisX.Interval = 1;
            thisChart.ChartAreas[0].AxisY.Interval = 0.1;
            //thisChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //thisChart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //是否显示栅格
            thisChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            thisChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            //设置标题
            thisChart.Titles.Clear();
            thisChart.Titles.Add("");
            thisChart.Titles[0].ForeColor = Color.RoyalBlue;
            thisChart.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            thisChart.Titles[0].Text = string.Format("");

            //设置图表显示样式
            thisChart.Series[0].Color = Color.Black;
            thisChart.Series[1].Color = Color.Blue;
            thisChart.Series[2].Color = Color.Green;
            thisChart.Series[3].Color = Color.Red;
            

            thisChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            thisChart.Series[0].ChartType = SeriesChartType.Line;
            thisChart.Series[1].ChartType = SeriesChartType.Spline;
            thisChart.Series[2].ChartType = SeriesChartType.Spline;
            thisChart.Series[3].ChartType = SeriesChartType.Spline;
            

            //设置Y轴数值保留三位小数
            thisChart.ChartAreas[0].AxisY.LabelStyle.Format = "N3";

            //设置滚动条
            //thisChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;  //是否启用X轴上的滚动条
            //thisChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;//指滚动条位于图表区内还是图表区外            
            //thisChart.ChartAreas[0].AxisX.ScaleView.Size = 20;//视野范围内共有多少个数据点

            //thisChart.Series[0].IsValueShownAsLabel = true;     //是否显示数据点的值

            thisChart.Series[1].Enabled = false;
            thisChart.Series[2].Enabled = false;

            thisChart.Series[0].Points.Clear();
            thisChart.Series[1].Points.Clear();
            thisChart.Series[2].Points.Clear();
            thisChart.Series[3].Points.Clear();
        }

        public static void RInitChart(Chart thisChart, float RupperLimit, float RdownLimit)
        {
            //定义图表区域
            thisChart.ChartAreas.Clear();
            ChartArea chartArea1 = new ChartArea("ValueShowOfRes");
            thisChart.ChartAreas.Add(chartArea1);

            //定义储存和显示点的容器
            thisChart.Series.Clear();

            Series series1 = new Series("内阻");
            Series series2 = new Series("上限值");
            Series series3 = new Series("下限值");
            Series series4 = new Series("平均值");


            series1.ChartArea = "ValueShowOfRes";
            series2.ChartArea = "ValueShowOfRes";
            series3.ChartArea = "ValueShowOfRes";
            series4.ChartArea = "ValueShowOfRes";


            thisChart.Series.Add(series1);
            thisChart.Series.Add(series2);
            thisChart.Series.Add(series3);
            thisChart.Series.Add(series4);



            //设置图表显示样式
            try
            {
                thisChart.ChartAreas[0].AxisY.Minimum = RdownLimit;
                thisChart.ChartAreas[0].AxisY.Maximum = RupperLimit;
            }
            catch
            {
                MessageBox.Show("上下限设置有误！");
            }

            thisChart.ChartAreas[0].AxisX.Interval = 1;
            thisChart.ChartAreas[0].AxisY.Interval = 0.1;
            //thisChart.ChartAreas[0].AxisX.MajorGrid.LineColor = System.Drawing.Color.Silver;
            //thisChart.ChartAreas[0].AxisY.MajorGrid.LineColor = System.Drawing.Color.Silver;

            //是否显示栅格
            thisChart.ChartAreas[0].AxisX.MajorGrid.Enabled = false;
            thisChart.ChartAreas[0].AxisY.MajorGrid.Enabled = false;

            //设置标题
            thisChart.Titles.Clear();
            thisChart.Titles.Add("");
            thisChart.Titles[0].ForeColor = Color.RoyalBlue;
            thisChart.Titles[0].Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            thisChart.Titles[0].Text = string.Format("");

            //设置图表显示样式
            thisChart.Series[0].Color = Color.Black;
            thisChart.Series[1].Color = Color.Blue;
            thisChart.Series[2].Color = Color.Green;
            thisChart.Series[3].Color = Color.Red;


            thisChart.Series[0].MarkerStyle = MarkerStyle.Circle;
            thisChart.Series[0].ChartType = SeriesChartType.Line;
            thisChart.Series[1].ChartType = SeriesChartType.Spline;
            thisChart.Series[2].ChartType = SeriesChartType.Spline;
            thisChart.Series[3].ChartType = SeriesChartType.Spline;


            //设置Y轴数值保留三位小数
            thisChart.ChartAreas[0].AxisY.LabelStyle.Format = "F3";

            //设置滚动条
            //thisChart.ChartAreas[0].AxisX.ScrollBar.Enabled = true;  //是否启用X轴上的滚动条
            //thisChart.ChartAreas[0].AxisX.ScrollBar.IsPositionedInside = true;//指滚动条位于图表区内还是图表区外            
            //thisChart.ChartAreas[0].AxisX.ScaleView.Size = 20;//视野范围内共有多少个数据点

            //thisChart.Series[0].IsValueShownAsLabel = true;     //是否显示数据点的值

            thisChart.Series[1].Enabled = false;
            thisChart.Series[2].Enabled = false;

            thisChart.Series[0].Points.Clear();
            thisChart.Series[1].Points.Clear();
            thisChart.Series[2].Points.Clear();
            thisChart.Series[3].Points.Clear();
        }

        /// <summary>
        /// 清空chart，再次使用chart必须初始化
        /// </summary>
        /// <param name="thisChart"></param>
        public static void Clear(Chart thisChart)
        {
            thisChart.Series[0].Points.Clear();
            thisChart.Series[1].Points.Clear();
            thisChart.Series[2].Points.Clear();
            thisChart.Series[3].Points.Clear();

            MAXvoltage = 0;
            MINvoltage = 1000;
            AvgOFYAxis = 0;
            MAXresistance = 0;
            MINresistance = 0;
        }

        private static double MAXvoltage = 0;            //电压最大值
        private static double MINvoltage = 1000;         //电压最小值
        private static double MAXresistance = 0;         //内阻最大值
        private static double MINresistance = 1000;      //内阻最小值 
        private static double AvgOFYAxis = 0;            //Y轴平均值
        /// <summary>
        /// 调用此方法向chart中添加数据
        /// </summary>
        /// <param name="thisChart"></param>
        /// <param name="voltage"></param>
        /// <param name="upper"></param>
        /// <param name="lower"></param>
        /// <param name="avgVol"></param>
        /// <param name="maxNum"></param>
        /// <param name="minNum"></param>
        /// <param name="dataToForms"></param>
        /// <param name="deviationVol"></param>
        public static void AddNewPonit(Chart thisChart, float voltage, float upper, float lower, float avgVol, float maxVol, float minVol, float resistance, float Rupper, float Rlower, float RavgVol, float maxRes, float minRes, DataToForms dataToForms, string deviationVol)
        {
            //thisChart.ChartAreas[0].AxisX.ScaleView.Scroll(ScrollType.Last); //设置滚动条的位置

            if (MAXvoltage < voltage)
            {
                MAXvoltage = voltage;
            }
            if (MINvoltage > voltage)
            {
                MINvoltage = voltage;
            }

            if (MAXresistance < resistance)
            {
                MAXresistance = resistance;
            }
            if (MINresistance > resistance)
            {
                MINresistance = resistance;
            }



            //上下限是否存在
            if (upper == 0 && lower == 0)
            {
                thisChart.Series[1].Enabled = false;
                thisChart.Series[2].Enabled = false;
                thisChart.Series[0].Points.AddY(voltage);
                
                
                thisChart.Series[3].Points.AddY(avgVol);

                AvgOFYAxis = avgVol == 0 ? voltage : avgVol;

                //UpperOfYAxis = avgVol + 0.5*float.Parse(deviationVol)/1000;
                //LowerOfYAxis = avgVol - 0.5*float.Parse(deviationVol)/1000;
                //UpperOfYAxis = AvgOFYAxis + 0.1 * float.Parse(deviationVol);
                //LowerOfYAxis = AvgOFYAxis - 0.1 * float.Parse(deviationVol);

            }
            else
            {
                thisChart.Series[1].Enabled = true;
                thisChart.Series[2].Enabled = true;

                //if( upper * 1.005 > UpperOfYAxis ) {
                //    UpperOfYAxis = upper * 1.005;
                //}
                //if( lower * 0.995 < LowerOfYAxis ) {
                //    LowerOfYAxis = lower * 0.995;
                //}   
                if (MAXvoltage < upper)
                {
                    MAXvoltage = upper;
                }
                if (MINvoltage > lower)
                {
                    MINvoltage = lower;
                }

                if (MAXresistance < Rupper)
                {
                    MAXresistance = Rupper;
                }
                if (MINresistance > Rlower)
                {
                    MINresistance = Rlower;
                }

                //添加数据点
                thisChart.Series[0].Points.AddY(voltage);
                thisChart.Series[1].Points.AddY(upper);
                thisChart.Series[2].Points.AddY(lower);
                thisChart.Series[3].Points.AddY(avgVol);

                //UpperOfYAxis = dataToForms.maxVol;
                //LowerOfYAxis = dataToForms.minVol;
                //if( UpperOfYAxis < dataToForms.maxVol ) {
                //    UpperOfYAxis = dataToForms.maxVol;
                //}

                //if( LowerOfYAxis > dataToForms.minVol) {
                //    LowerOfYAxis = dataToForms.minVol;
                //}

                //if( UpperOfYAxis < voltage ) {
                //    UpperOfYAxis = voltage;
                //}
                //if( LowerOfYAxis > voltage ) {
                //    LowerOfYAxis = voltage;
                //}

                //if( UpperOfYAxis < upper ) {
                //    UpperOfYAxis = upper;
                //}
                //if( LowerOfYAxis > lower ) {
                //    LowerOfYAxis = lower;
                //}
            }

            //调整Y轴            
            //thisChart.ChartAreas[0].AxisY.Maximum = UpperOfYAxis;
            //thisChart.ChartAreas[0].AxisY.Minimum = LowerOfYAxis;
            //thisChart.ChartAreas[0].AxisY.Interval = (UpperOfYAxis - LowerOfYAxis) / 5;
            if (MAXvoltage > MINvoltage)
            {
                thisChart.ChartAreas[0].AxisY.Maximum = MAXvoltage;
                thisChart.ChartAreas[0].AxisY.Minimum = MINvoltage;
                thisChart.ChartAreas[0].AxisY.Interval = (MAXvoltage - MINvoltage) / 5;
            }
            else
            {
                thisChart.ChartAreas[0].AxisY.Maximum = AvgOFYAxis + 0.1 * float.Parse(deviationVol);
                thisChart.ChartAreas[0].AxisY.Minimum = AvgOFYAxis - 0.1 * float.Parse(deviationVol);
                thisChart.ChartAreas[0].AxisY.Interval = ((AvgOFYAxis + 0.1 * float.Parse(deviationVol)) - (AvgOFYAxis - 0.1 * float.Parse(deviationVol))) / 5;
            }
            //调整X轴
            int currentNum = thisChart.Series[0].Points.Count;
            thisChart.ChartAreas[0].AxisX.Interval = currentNum / 10 + 1;
        }

        /// <summary>
        /// 填充20组上下限的值
        /// </summary>
        /// <param name="thisChart"></param>
        /// <param name="upper"></param>
        /// <param name="lower"></param>
        public static void FillTwenty(Chart thisChart, float upper, float lower)
        {
            for (int i = 0; i < 20; i++)
            {
                thisChart.Series[1].Points.AddY(upper);
                thisChart.Series[2].Points.AddY(lower);
            }
        }

        /// <summary>
        /// 只添加电压值和平均值，不添加上下限值
        /// </summary>
        /// <param name="thisChart"></param>
        /// <param name="voltage"></param>
        /// <param name="avg"></param>
        public static void AddNewPonitOnlyPoint(Chart thisChart, float voltage, float avg,float deviationVol)
        {
            if (voltage > MAXvoltage)
            {
                MAXvoltage = voltage;
            }
            if (voltage < MINvoltage)
            {
                MINvoltage = voltage;
            }

            MAXvoltage = thisChart.ChartAreas[0].AxisY.Maximum;
            MINvoltage = thisChart.ChartAreas[0].AxisY.Minimum;

            thisChart.Series[0].Points.AddY(voltage);
            thisChart.Series[3].Points.AddY(avg);

            if (MAXvoltage > MINvoltage)
            {
                thisChart.ChartAreas[0].AxisY.Maximum = MAXvoltage;
                thisChart.ChartAreas[0].AxisY.Minimum = MINvoltage;
                thisChart.ChartAreas[0].AxisY.Interval = (MAXvoltage - MINvoltage) / 5;
            }
            else
            {
                thisChart.ChartAreas[0].AxisY.Maximum = avg + 0.1 * deviationVol;
                thisChart.ChartAreas[0].AxisY.Minimum = avg - 0.1 * deviationVol;
                thisChart.ChartAreas[0].AxisY.Interval = ((avg + 0.1 * deviationVol) - (avg - 0.1 * deviationVol)) / 5;
            }

            int currentNum = thisChart.Series[0].Points.Count;
            thisChart.ChartAreas[0].AxisX.Interval = currentNum / 10 + 1;
        }
        /// <summary>
        /// 只添加内阻值和平均值，不添加上下限值
        /// </summary>
        /// <param name="thisChart"></param>
        /// <param name="voltage"></param>
        /// <param name="avg"></param>
        public static void RAddNewPonitOnlyPoint(Chart thisChart, float resistance, float Ravg,float deviationRes)
        {
            if (resistance > MAXresistance)
            {
                MAXresistance = resistance;
            }
            if (resistance < MINresistance)
            {
                MINresistance = resistance;
            }

            MAXresistance = thisChart.ChartAreas[0].AxisY.Maximum;
            MINresistance = thisChart.ChartAreas[0].AxisY.Minimum;

            thisChart.Series[0].Points.AddY(resistance);
            thisChart.Series[3].Points.AddY(Ravg);

            if (MAXresistance > MINresistance)
            {
                thisChart.ChartAreas[0].AxisY.Maximum = MAXresistance;
                thisChart.ChartAreas[0].AxisY.Minimum = MINresistance;
                thisChart.ChartAreas[0].AxisY.Interval = (MAXresistance - MINresistance) / 5;
            }
            else
            {
                thisChart.ChartAreas[0].AxisY.Maximum = Ravg + 0.1 * deviationRes;
                thisChart.ChartAreas[0].AxisY.Minimum = Ravg - 0.1 * deviationRes;
                thisChart.ChartAreas[0].AxisY.Interval = ((Ravg + 0.1 * deviationRes) - (Ravg - 0.1 * deviationRes)) / 5;
            }

            int currentNum = thisChart.Series[0].Points.Count;
            thisChart.ChartAreas[0].AxisX.Interval = currentNum / 10 + 1;
        }
    }
}
