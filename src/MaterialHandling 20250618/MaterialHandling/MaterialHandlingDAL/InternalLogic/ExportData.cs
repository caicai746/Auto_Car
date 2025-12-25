using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using CrystalDecisions.CrystalReports.Engine;
using Microsoft.Office.Interop.Excel;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    public class ExportData
    {
        /// <summary>
        /// 1、在DataGridView中不能有空数据。2.fileName为导出文件保存路径。3.n为从后往前去掉n行。
        /// </summary>
        /// <param name="dataGridView1"></param>
        /// <param name="fileName"></param>
        /// <returns></returns>
        

        public bool DataGridViewToExcelFast(DataGridView dataGridView1, int n)
        {
            int ExcelColumns = 0;
            SaveFileDialog kk = new SaveFileDialog();
            kk.Title = "保存为EXcel文件";
            kk.Filter = "Excel文件(*.xls,xlsx)|*.xlsx;*.xls";
            kk.FilterIndex = 1;
            if (kk.ShowDialog() == DialogResult.OK)
            {
                string FileName = kk.FileName + "";
                if (File.Exists(FileName))
                    File.Delete(FileName);
                //&&
                //try
                //{
                //    dataGridView1.Columns.Remove("pur_code");
                //    dataGridView1.Columns.Remove("cons_code");
                //}
                //catch { }
                //&&
                if (dataGridView1.Rows.Count > 65536)
                {
                    if (MessageBox.Show("数据行大于Excel2003所容许的最大行数65535行，继续导出可能会引发错误，是否继续？", "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1) == DialogResult.No)
                    {
                        return false;
                    }
                }
                System.Reflection.Missing miss = System.Reflection.Missing.Value;
                //创建EXCEL对象appExcel,Workbook对象,Worksheet对象,Range对象
                Microsoft.Office.Interop.Excel.Application appExcel = null;
                appExcel = new Microsoft.Office.Interop.Excel.Application();
                Microsoft.Office.Interop.Excel.Workbook workbookData;
                Microsoft.Office.Interop.Excel.Worksheet worksheetData;
                Microsoft.Office.Interop.Excel.Range rangedata;
                //设置对象不可见
                appExcel.Visible = false;
                /* 在调用Excel应用程序，或创建Excel工作簿之前，记着加上下面的两行代码
                * 这是因为Excel有一个Bug，如果你的操作系统的环境不是英文的，而Excel就会在执行下面的代码时，报异常。
                */
                System.Globalization.CultureInfo CurrentCI = System.Threading.Thread.CurrentThread.CurrentCulture;
                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("en-US");
                workbookData = appExcel.Workbooks.Add(miss);
                worksheetData = (Microsoft.Office.Interop.Excel.Worksheet)workbookData.Worksheets.Add(miss, miss, miss, miss);
                //给工作表赋名称
                worksheetData.Name = "MySheet";

                // 保存到WorkSheet的表头，你应该看到，是一个Cell一个Cell的存储，这样效率特别低，解决的办法是，使用Rang，一块一块地存储到Excel
                ExcelColumns = 0;
                for (int i = 0; i < dataGridView1.ColumnCount; i++)
                {
                    if (!dataGridView1.Columns[i].Visible)
                    {
                        continue;
                    }
                    worksheetData.Cells[1, ++ExcelColumns] = dataGridView1.Columns[i].HeaderText.ToString();
                }
                //先给Range对象一个范围为A2开始，Range对象可以给一个CELL的范围，也可以给例如A1到H10这样的范围
                //因为第一行已经写了表头，所以所有数据都应该从A2开始
                worksheetData.get_Range("A1", Missing.Value).ColumnWidth = 8;
                worksheetData.get_Range("B1", Missing.Value).ColumnWidth = 20;
                worksheetData.get_Range("C1", Missing.Value).ColumnWidth = 18;
                worksheetData.get_Range("D1", Missing.Value).ColumnWidth = 12;
                worksheetData.get_Range("E1", Missing.Value).ColumnWidth = 16;
                worksheetData.get_Range("F1", Missing.Value).ColumnWidth = 12;
                worksheetData.get_Range("G1", Missing.Value).ColumnWidth = 16;
                worksheetData.get_Range("H1", Missing.Value).ColumnWidth = 18;
                worksheetData.get_Range("I1", Missing.Value).ColumnWidth = 20;
                worksheetData.get_Range("J1", Missing.Value).ColumnWidth = 20;
                worksheetData.get_Range("K1", Missing.Value).ColumnWidth = 8;
                worksheetData.get_Range("L1", Missing.Value).ColumnWidth = 24;
                worksheetData.get_Range("M1", Missing.Value).ColumnWidth = 12;
                worksheetData.get_Range("N1", Missing.Value).ColumnWidth = 24;
                worksheetData.get_Range("O1", Missing.Value).ColumnWidth = 24;
                worksheetData.get_Range("P1", Missing.Value).ColumnWidth = 8;
                //worksheetData.get_Range("A1:L1", Missing.Value).EntireColumn.AutoFit();  //自动调整列宽
                Microsoft.Office.Interop.Excel.Range range = worksheetData.get_Range("A1:P1");
                range.Font.Bold = true;
                range.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                range.VerticalAlignment = XlHAlign.xlHAlignCenter;
                //range.Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
                range.Borders.LineStyle = 1;
                rangedata = worksheetData.get_Range("A2", miss);
                Microsoft.Office.Interop.Excel.Range xlRang = null;
                //iRowCount为实际行数，最大行
                int iRowCount = dataGridView1.RowCount;
                int iParstedRow = 0, iCurrSize = 0;
                //iEachSize为每次写行的数值，可以自己设置，每次写1000行和每次写2000行大家可以自己测试下效率
                int iEachSize = 1000;
                //iColumnAccount为实际列数，最大列数
                int iColumnAccount = dataGridView1.ColumnCount;
                //在内存中声明一个iEachSize×iColumnAccount的数组，iEachSize是每次最大存储的行数，iColumnAccount就是存储的实际列数
                object[,] objVal = new object[iEachSize, iColumnAccount];
                try
                {
                    iCurrSize = iEachSize;
                    //计算Excel列头
                    string En = NumToEn(iColumnAccount);
                    while (iParstedRow < iRowCount)
                    {
                        if ((iRowCount - iParstedRow) < iEachSize)
                            iCurrSize = iRowCount - iParstedRow;
                        //用FOR循环给数组赋值
                        for (int i = 0; i < iCurrSize; i++)
                        {
                            try
                            {
                                for (int j = 0; j < iColumnAccount; j++)
                                {
                                    if (!dataGridView1.Columns[j].Visible)
                                    {
                                        continue;
                                    }
                                    objVal[i, j] = dataGridView1[j, i + iParstedRow].Value == null ? "" : dataGridView1[j, i + iParstedRow].Value.ToString();
                                }
                                System.Windows.Forms.Application.DoEvents();
                            }
                            catch (Exception e)
                            {
                                MessageBox.Show(e.ToString());
                            }
                        }
                        /*
                        * 建议使用设置断点研究下哈
                        * 例如A1到H10的意思是从A到H，第一行到第十行
                        * 下句很关键，要保证获取Sheet中对应的Range范围
                        * 下句实际上是得到这样的一个代码语句xlRang = worksheetData.get_Range("A2","H100");
                        * 注意看实现的过程
                        * 'A' + iColumnAccount - 1这儿是获取你的最后列，A的数字码为65，大家可以仔细看下是不是得到最后列的字母
                        * iParstedRow + iCurrSize + 1获取最后行
                        * 若WHILE第一次循环的话这应该是A2,最后列字母+最后行数字
                        * iParstedRow + 2要注意，每次循环这个值不一样，他取决于你每次循环RANGE取了多大，循环了几次，也就是iEachSize设置值的大小哦
                        */
                        //xlRang = worksheetData.get_Range("A" + ((int)(iParstedRow + 2)).ToString(), ((char)('A' + iColumnAccount - 1)).ToString() + ((int)(iParstedRow + iCurrSize + 1)).ToString());

                        xlRang = worksheetData.get_Range("A" + ((int)(iParstedRow + 2)).ToString(), En + ((int)(iParstedRow + iCurrSize + 1)).ToString());
                        // 调用Range的Value2属性，把内存中的值赋给Excel
                        xlRang.Value2 = objVal;
                        xlRang.Borders.LineStyle = 1;
                        //xlRang.Borders.Weight = 2;
                        xlRang.HorizontalAlignment = XlHAlign.xlHAlignCenter;
                        xlRang.VerticalAlignment = XlHAlign.xlHAlignCenter;
                        iParstedRow = iParstedRow + iCurrSize;
                    }
                    //保存工作表

                    worksheetData.SaveAs(FileName, miss, miss, miss, miss, miss, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, miss, miss, miss);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlRang);
                    xlRang = null;
                    //调用方法关闭EXCEL进程，大家可以试下不用的话如果程序不关闭在进程里一直会有EXCEL.EXE这个进程并锁定你的EXCEL表格
                    this.KillSpecialExcel(appExcel);
                    MessageBox.Show("数据已经成功导出", "导出完成", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    // 别忘了在结束程序之前恢复你的环境！                   
                    System.Threading.Thread.CurrentThread.CurrentCulture = CurrentCI;
                    return true;
                }
                catch (Exception ex)
                {
                    this.KillSpecialExcel(appExcel);
                    MessageBox.Show(ex.Message);
                    return false;
                }
            }
            return false;
        }

        public string NumToEn(int Num)
        {
            string sEn = "", sNum = "";
            Dictionary<string, string> d = new Dictionary<string, string>();
            #region AddValue
            d.Add("0", "");
            d.Add("1", "A");
            d.Add("2", "B");
            d.Add("3", "C");
            d.Add("4", "D");
            d.Add("5", "E");
            d.Add("6", "F");
            d.Add("7", "G");
            d.Add("8", "H");
            d.Add("9", "I");
            d.Add("10", "J");
            d.Add("11", "K");
            d.Add("12", "L");
            d.Add("13", "M");
            d.Add("14", "N");
            d.Add("15", "O");
            d.Add("16", "P");
            d.Add("17", "Q");
            d.Add("18", "R");
            d.Add("19", "S");
            d.Add("20", "T");
            d.Add("21", "U");
            d.Add("22", "V");
            d.Add("23", "W");
            d.Add("24", "X");
            d.Add("25", "Y");
            d.Add("26", "Z");
            #endregion
            string sRemainder = decimal.Round(Num / 26, 0).ToString();
            Num = Num - int.Parse(sRemainder) * 26;
            sNum = sRemainder + "," + Num.ToString();

            string[] ssNum = sNum.Split(',');
            foreach (string num in ssNum)
            {
                sEn += d[num];
            }
            return sEn;
        }

        #region 结束EXCEL.EXE进程的方法
        /// <summary>
        /// 结束EXCEL.EXE进程的方法
        /// </summary>
        /// <param name="m_objExcel">EXCEL对象</param>
        [DllImport("user32.dll", SetLastError = true)]
        static extern int GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);

        public void KillSpecialExcel(Microsoft.Office.Interop.Excel.Application m_objExcel)
        {
            try
            {
                if (m_objExcel != null)
                {
                    int lpdwProcessId;
                    GetWindowThreadProcessId(new IntPtr(m_objExcel.Hwnd), out lpdwProcessId); System.Diagnostics.Process.GetProcessById(lpdwProcessId).Kill();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #endregion
    }
}
