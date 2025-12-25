using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using System.Runtime.InteropServices;
using Excel = Microsoft.Office.Interop.Excel;
using System.Collections;
using MaterialHandling.MaterialHandlingDAL.LogHelper;
using System.IO;
using System.Diagnostics;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    class PrintExcel
    {
        private Excel.Application xlsApp = null;  //生成的Excel实例对象
        private Excel._Workbook xlsBook = null;
        private Excel.Sheets xlsSheets = null;
        private Excel._Worksheet xlsSheet = null;
        private Excel.Range xlsRange = null;
        object xlsObjOpt = System.Reflection.Missing.Value;

        private string filePath;                                    //文件路径和文件名
        private GetDataToExcel dt;                                  //保存的报表相关参数
        private int listIndexes;                                    //记录当前list的索引号
        private int currentRow;                                     //记录当前行数
        private int totalPages;                                     //总页数
        private int currentPage;                                    //当前页数        

        /// <summary>
        /// 调用此方法生成用户V+R报表文件
        /// </summary>
        /// <param name="getData"></param>
        public void SaveToExcel(GetDataToExcel getData)
        {
            //初始化相关参数
            if (!Init(getData, false))
            {
                MessageBox.Show("打印报表失败！");

                return;
            }

            //通过总页数生成整体报表模板
            MakeLayout();

            //生成报表内容
            //int totalpages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            for (int i = 1; i <= totalPages; i++)
            {
                MakeHeader();
                MakeTable();
                MakeFooter();
            }







            //保存文件
            SaveAndExit();
        }

        /// <summary>
        /// 调用此方法生成员工V+R报表文件
        /// </summary>
        /// <param name="getData"></param>
        public void SaveToExcelForAll(GetDataToExcel getData)
        {
            //初始化相关参数
            if (!Init(getData, true))
            {
                MessageBox.Show("打印报表失败！");
              
                return;
            }

            //通过总页数生成整体报表模板
            MakeLayoutToAll();

            //生成报表内容
            //int totalpages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            for (int i = 1; i <= totalPages; i++)
            {
                MakeHeaderToAll();
                MakeTableForAll();
                MakeFooter();
            }





            //保存文件
            SaveAndExit();
        }

        /// <summary>
        /// 调用此方法生成主查询页面导出数据的报表文件
        /// </summary>
        /// <param name="list"></param>
        public void SaveToExcelForMainQuery(List<DetailAndBatchInfo> list)
        {
            //初始化相关参数
            if (!MInit(list))
            {
                MessageBox.Show("打印报表失败！");
                
                return;
            }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            //数据报表布局
            MainMakelayout();
            sw.Stop();
            TimeSpan ts = sw.Elapsed;
            

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            //生成报表内容
            MakeTableForMain(list);
            sw1.Stop();
            TimeSpan ts1 = sw1.Elapsed;
            
            //保存文件
            SaveAndExit();


        }

        /// <summary>
        /// 新导出方法，接受DataGrid
        /// </summary>
        /// <param name="m_DataView"></param>
        //public void DataTOExcel(DataGridView m_DataView)
        //{
        //    SaveFileDialog kk = new SaveFileDialog();
        //    kk.Title = "保存为EXcel文件";
        //    kk.Filter = "EXECL文件(*.xls) |*.xls |所有文件(*.*) |*.*";
        //    kk.FilterIndex = 1;
        //    if (kk.ShowDialog() == DialogResult.OK)
        //    {
        //        string FileName = kk.FileName + "";
        //        if (File.Exists(FileName))
        //            File.Delete(FileName);
        //        FileStream objFileStream;
        //        StreamWriter objStreamWriter;
        //        string strLine = "";
        //        objFileStream = new FileStream(FileName, FileMode.OpenOrCreate, FileAccess.Write);
        //        objStreamWriter = new StreamWriter(objFileStream, System.Text.Encoding.Unicode);
        //        for (int i = 0; i < m_DataView.Columns.Count; i++)
        //        {
        //            if (m_DataView.Columns[i].Visible == true)
        //            {
        //                strLine = strLine + m_DataView.Columns[i].HeaderText.ToString() + Convert.ToChar(9);
        //            }
        //        }
        //        objStreamWriter.WriteLine(strLine);
        //        strLine = "";

        //        for (int i = 0; i < m_DataView.Rows.Count; i++)
        //        {
        //            if (m_DataView.Columns[0].Visible == true)
        //            {
        //                if (m_DataView.Rows[i].Cells[0].Value == null)
        //                    strLine = strLine + " " + Convert.ToChar(9);
        //                else
        //                    strLine = strLine + m_DataView.Rows[i].Cells[0].Value.ToString() + Convert.ToChar(9);
        //            }
        //            for (int j = 1; j < m_DataView.Columns.Count; j++)
        //            {
        //                if (m_DataView.Columns[j].Visible == true)
        //                {
        //                    if (m_DataView.Rows[i].Cells[j].Value == null)
        //                        strLine = strLine + " " + Convert.ToChar(9);
        //                    else
        //                    {
        //                        string rowstr = "";
        //                        rowstr = m_DataView.Rows[i].Cells[j].Value.ToString();
        //                        if (rowstr.IndexOf("\r\n") > 0)
        //                            rowstr = rowstr.Replace("\r\n", " ");
        //                        if (rowstr.IndexOf("\t") > 0)
        //                            rowstr = rowstr.Replace("\t", " ");
        //                        strLine = strLine + rowstr + Convert.ToChar(9);
        //                    }
        //                }
        //            }
        //            objStreamWriter.WriteLine(strLine);
        //            strLine = "";
        //        }
        //        objStreamWriter.Close();
        //        objFileStream.Close();
        //        MessageBox.Show(this, "保存EXCEL成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }
         
        //}


        /// <summary>
        /// 调用此方法生成员工V报表文件
        /// </summary>
        /// <param name="getdata"></param>
        public void SaveToExcelForAllOnlyV(GetDataToExcel getData)
        {
            //初始化相关参数
            if (!VInit(getData, true))
            {
                MessageBox.Show("打印报表失败！");
             
                return;
            }


            //通过总页数生成整体报表模板
            VMakeLayoutToAll();

            //生成报表内容
            //int totalpages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            for (int i = 1; i <= totalPages; i++)
            {
                VMakeHeaderToAll();
                VMakeTableForAll();
                VMakeFooter();
            }







            //保存文件
            SaveAndExit();
        }

        /// <summary>
        /// 调用此方法生成用户V报表文件
        /// </summary>
        /// <param name="getData"></param>
        public void SaveToExcelOnlyV(GetDataToExcel getData)
        {
            //初始化相关参数
            if (!VInit(getData, false))
            {
                MessageBox.Show("打印报表失败！");
          
                return;
            }


            //通过总页数生成整体报表模板
            VMakeLayout();

            //生成报表内容
            //int totalpages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            for (int i = 1; i <= totalPages; i++)
            {
                VMakeHeader();
                VMakeTable();
                VMakeFooter();
            }







            //保存文件
            SaveAndExit();
        }


        /// <summary>
        /// 初始化(获得Excel实例对象，文件路径和名称，报表相关参数，当前行数)（打印内阻和电压）
        /// </summary>
        /// <param name="getDataToExcel"></param>
        private bool Init(GetDataToExcel getDataToExcel, bool isPrintAll)
        {
            this.dt = getDataToExcel;
            if (isPrintAll)
            {
                this.filePath = GetFilePath(dt.excelHeadData.test_Date, dt.excelHeadData.contract, dt.excelHeadData.serialNumber, true);
            }
            else
            {
                this.filePath = GetFilePath(dt.excelHeadData.test_Date, dt.excelHeadData.contract, dt.excelHeadData.serialNumber, false);
            }
            if (String.IsNullOrEmpty(this.filePath))
            {
                MessageBox.Show("选择文件路径无效。");
                return false;
            }

            if (!getExcelObj(isPrintAll))
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }

            this.currentRow = 1;
            this.listIndexes = 0;


            if (isPrintAll)
            {
                this.totalPages = (int)Math.Ceiling(dt.voltageAndStatus.Count / 60.0);
            }
            else
            {
                this.totalPages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            }




            this.currentPage = 1;
            return true;
        }
        /// <summary>
        /// 初始化(获得Excel实例对象，文件路径和名称，报表相关参数，当前行数)（仅打印电压）
        /// </summary>
        /// <param name="getDataToExcel"></param>
        /// <param name="isPrintAll"></param>
        /// <returns></returns>
        private bool VInit(GetDataToExcel getDataToExcel, bool isPrintAll)
        {
            this.dt = getDataToExcel;
            if (isPrintAll)
            {
                this.filePath = VGetFilePath(dt.excelHeadData.test_Date, dt.excelHeadData.contract, dt.excelHeadData.serialNumber, true);
            }
            else
            {
                this.filePath = VGetFilePath(dt.excelHeadData.test_Date, dt.excelHeadData.contract, dt.excelHeadData.serialNumber, false);
            }
            if (String.IsNullOrEmpty(this.filePath))
            {
                MessageBox.Show("选择文件路径无效。");
                return false;
            }

            if (!VgetExcelObj(isPrintAll))
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }

            this.currentRow = 1;
            this.listIndexes = 0;


            if (isPrintAll)
            {
                this.totalPages = (int)Math.Ceiling(dt.voltageAndStatus.Count / 60.0);
            }
            else
            {
                this.totalPages = (int)Math.Ceiling(dt.voltageAndTime.Count / 60.0);
            }



            this.currentPage = 1;
            return true;
        }
        /// <summary>
        /// 初始化(获得Excel实例对象，文件路径和名称，报表相关参数，当前行数)（仅主查询页面打印使用）
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool MInit(List<DetailAndBatchInfo> list)
        {
            this.filePath = MGetFilePath();
            if (String.IsNullOrEmpty(this.filePath))
            {
                MessageBox.Show("选择文件路径无效。");
                return false;
            }
            if (!MgetExcelObj())
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }

            this.currentRow = 1;
            this.listIndexes = 0;

            totalPages = 1;

            this.currentPage = 1;
            return true;
        }

        /// <summary>
        /// 用户打印V+R选择文件路径
        /// </summary>
        /// <returns>文件路径</returns>
        private string GetFilePath(DateTime testTime, string contract, string serialNumber, bool isPrintAll)
        {
            string path = null;
            SaveFileDialog frm = new SaveFileDialog();  //设置文件类型 
            frm.Filter = "Excel文件(*.xls,xlsx)|*.xlsx;*.xls";
            //frm.InitialDirectory = Application.StartupPath + "\\report";  //设置初始目录
            frm.RestoreDirectory = true;    //保存对话框是否记忆上次打开的目录
            if (isPrintAll)
            {

                frm.FileName = testTime.ToString("yyyy-MM-dd") + " " + contract + " " + serialNumber + " V+Rall.xls";



            }
            else
            {

                frm.FileName = testTime.ToString("yyyy-MM-dd") + " " + contract + " " + serialNumber + " V+R.xls";


            }
            //frm.FileOk += new CancelEventHandler(frm_FileOk); //确定按钮的点击事件
            if (frm.ShowDialog() == DialogResult.OK)
            {
                path = frm.FileName;
            }
            return path;
        }

        /// <summary>
        /// 用户打印V选择文件路径
        /// </summary>
        /// <returns>文件路径</returns>
        private string VGetFilePath(DateTime testTime, string contract, string serialNumber, bool isPrintAll)
        {
            string path = null;
            SaveFileDialog frm = new SaveFileDialog();  //设置文件类型 
            frm.Filter = "Excel文件(*.xls,xlsx)|*.xlsx;*.xls";
            //frm.InitialDirectory = Application.StartupPath + "\\report";  //设置初始目录
            frm.RestoreDirectory = true;    //保存对话框是否记忆上次打开的目录
            if (isPrintAll)
            {

                frm.FileName = testTime.ToString("yyyy-MM-dd") + " " + contract + " " + serialNumber + " Vall.xls";



            }
            else
            {


                frm.FileName = testTime.ToString("yyyy-MM-dd") + " " + contract + " " + serialNumber + " V.xls";

            }
            //frm.FileOk += new CancelEventHandler(frm_FileOk); //确定按钮的点击事件
            if (frm.ShowDialog() == DialogResult.OK)
            {
                path = frm.FileName;
            }
            return path;
        }


        /// <summary>
        /// 主查询页面打印选择路径
        /// </summary>
        /// <returns></returns>
        private string MGetFilePath()
        {
            string path = null;
            SaveFileDialog frm = new SaveFileDialog();  //设置文件类型
            frm.Filter = "Excel文件(*.xls,xlsx)|*.xlsx;*.xls";
            frm.RestoreDirectory = true; //保存对话框是否记忆上次打开的目录

            frm.FileName = "所有批次电池数据.xls";

            if (frm.ShowDialog() == DialogResult.OK)
            {
                path = frm.FileName;
            }
            return path;


        }

        /// <summary>
        /// 初始化相关实例化对象（电压和内阻）
        /// </summary>
        /// <returns></returns>
        private bool getExcelObj(bool isPrintAll)
        {
            try
            {
                xlsApp = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }
            try
            {
                if (isPrintAll)
                {

                    xlsBook = xlsApp.Workbooks.Open(Application.StartupPath + "\\ReportFormatFile\\ReportFormatToAll.xls", xlsObjOpt, xlsObjOpt,
                                                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                                                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);


                }
                else
                {

                    xlsBook = xlsApp.Workbooks.Open(Application.StartupPath + "\\ReportFormatFile\\ReportFormat.xls", xlsObjOpt, xlsObjOpt,
                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);



                }
            }
            catch
            {
                MessageBox.Show("找不到默认模板文件。");
                return false;
            }
            try
            {
                xlsSheets = (Excel.Sheets)xlsBook.Worksheets;
                xlsSheet = (Excel._Worksheet)(xlsSheets.get_Item(1));
                xlsRange = xlsSheet.get_Range("A1:O51", xlsObjOpt);
                //设置Excel分页卡标题
                xlsSheet.Name = dt.excelHeadData.contract;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 初始化相关实例化对象（仅打印电压）
        /// </summary>
        /// <returns></returns>
        private bool VgetExcelObj(bool isPrintAll)
        {
            try
            {
                xlsApp = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }
            try
            {
                if (isPrintAll)
                {

                    xlsBook = xlsApp.Workbooks.Open(Application.StartupPath + "\\ReportFormatFile\\ReportFormatToAllOnlyV.xls", xlsObjOpt, xlsObjOpt,
                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                    xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);



                }
                else
                {

                    xlsBook = xlsApp.Workbooks.Open(Application.StartupPath + "\\ReportFormatFile\\ReportFormatOnlyV.xls", xlsObjOpt, xlsObjOpt,
                                xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                                xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);



                }
            }
            catch
            {
                MessageBox.Show("找不到默认模板文件。");
                return false;
            }
            try
            {
                xlsSheets = (Excel.Sheets)xlsBook.Worksheets;
                xlsSheet = (Excel._Worksheet)(xlsSheets.get_Item(1));
                xlsRange = xlsSheet.get_Range("A1:O51", xlsObjOpt);
                //设置Excel分页卡标题
                xlsSheet.Name = dt.excelHeadData.contract;
            }
            catch
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// 初始化相关实例化对象（进主页面打印专用）
        /// </summary>
        /// <returns></returns>
        private bool MgetExcelObj()
        {
            try
            {
                xlsApp = new Excel.Application();
            }
            catch
            {
                MessageBox.Show("无法创建Excel对象，可能是没有安装相应的Excel软件。");
                return false;
            }

            try
            {
                xlsBook = xlsApp.Workbooks.Open(Application.StartupPath + "\\ReportFormatFile\\ReportFormatForMainQuery.xls", xlsObjOpt, xlsObjOpt,
                        xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                        xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);
            }
            catch
            {
                MessageBox.Show("找不到默认模板文件。");
                return false;
            }
            try
            {
                xlsSheets = (Excel.Sheets)xlsBook.Worksheets;
                xlsSheet = (Excel._Worksheet)(xlsSheets.get_Item(1));
                xlsRange = xlsSheet.get_Range("A1:L1000", xlsObjOpt);
            }
            catch
            {
                return false;
            }

            return true;
        }
        /// <summary>
        /// 用户V+R数据报表布局
        /// </summary>
        private void MakeLayout()
        {
            const int heightOfPage = 51;
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            int bottomRow = 51;
            for (int i = 1; i < totalPages; i++)
            {
                topRow += heightOfPage;
                bottomRow += heightOfPage;
                tempRange = xlsSheet.get_Range("A" + topRow.ToString() + ":L" + bottomRow.ToString(), xlsObjOpt);
                tempRangeFont = xlsSheet.get_Range("A" + (topRow + 3).ToString() + ":L" + bottomRow.ToString(), xlsObjOpt);
                //tempRangeFont.Font.Name = "Calibri";
                tempRangeFont.Font.Name = "宋体";
                xlsRange.Copy(tempRange);
            }

            xlsSheet.PageSetup.PrintArea = "A1:L" + bottomRow.ToString();
        }
        /// <summary>
        /// 用户V+R无表格尾部数据报表布局
        /// </summary>
        private void MakeLayoutNoFoot()
        {
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            xlsSheet.PageSetup.PrintArea = "A1:L" + 1000.ToString();
        }

        /// <summary>
        /// 员工V+R数据报表布局
        /// </summary>
        private void MakeLayoutToAll()
        {
            const int heightOfPage = 51;
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            int bottomRow = 51;
            for (int i = 1; i < totalPages; i++)
            {
                topRow += heightOfPage;
                bottomRow += heightOfPage;
                tempRange = xlsSheet.get_Range("A" + topRow.ToString() + ":O" + bottomRow.ToString(), xlsObjOpt);
                tempRangeFont = xlsSheet.get_Range("A" + (topRow + 3).ToString() + ":O" + bottomRow.ToString(), xlsObjOpt);
                //tempRangeFont.Font.Name = "Calibri";
                tempRangeFont.Font.Name = "宋体";
                xlsRange.Copy(tempRange);
            }

            xlsSheet.PageSetup.PrintArea = "A1:O" + bottomRow.ToString();
        }

        /// <summary>
        /// 员工V+R无表格尾部数据报表布局
        /// </summary>
        private void MakeLayoutToAllNoFoot()
        {
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            xlsSheet.PageSetup.PrintArea = "A1:O" + 1000.ToString();

        }
        /// <summary>
        /// 员工V数据报表布局
        /// </summary>
        private void VMakeLayoutToAll()
        {
            const int heightOfPage = 51;
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            int bottomRow = 51;
            for (int i = 1; i < totalPages; i++)
            {
                topRow += heightOfPage;
                bottomRow += heightOfPage;
                tempRange = xlsSheet.get_Range("A" + topRow.ToString() + ":I" + bottomRow.ToString(), xlsObjOpt);
                tempRangeFont = xlsSheet.get_Range("A" + (topRow + 3).ToString() + ":I" + bottomRow.ToString(), xlsObjOpt);
                //tempRangeFont.Font.Name = "Calibri";
                tempRangeFont.Font.Name = "宋体";
                xlsRange.Copy(tempRange);
            }

            xlsSheet.PageSetup.PrintArea = "A1:I" + bottomRow.ToString();
        }

        /// <summary>
        /// 员工V数据无表格尾部数据报表布局
        /// </summary>
        private void VMakeLayoutToAllNoFoot()
        {
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            xlsSheet.PageSetup.PrintArea = "A1:I" + 1000.ToString();
        }

        /// <summary>
        /// 用户V数据报表布局
        /// </summary>
        private void VMakeLayout()
        {
            const int heightOfPage = 51;
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            int bottomRow = 51;
            for (int i = 1; i < totalPages; i++)
            {
                topRow += heightOfPage;
                bottomRow += heightOfPage;
                tempRange = xlsSheet.get_Range("A" + topRow.ToString() + ":I" + bottomRow.ToString(), xlsObjOpt);
                tempRangeFont = xlsSheet.get_Range("A" + (topRow + 3).ToString() + ":I" + bottomRow.ToString(), xlsObjOpt);
                //tempRangeFont.Font.Name = "Calibri";
                tempRangeFont.Font.Name = "宋体";
                xlsRange.Copy(tempRange);
            }

            xlsSheet.PageSetup.PrintArea = "A1:I" + bottomRow.ToString();
        }

        /// <summary>
        /// 用户V数据无表格尾部数据报表布局
        /// </summary>
        public void VMakelayoutNoFoot()
        {
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            xlsSheet.PageSetup.PrintArea = "A1:I" + 1000.ToString();
        }

        /// <summary>
        /// 主页面打印数据报表布局
        /// </summary>
        public void MainMakelayout()
        {
            Excel.Range tempRange = null;
            Excel.Range tempRangeFont = null;
            int topRow = 1;
            xlsSheet.PageSetup.PrintArea = "A1:L" + 1000.ToString();

        }


        /// <summary>
        /// 生成V+R用户表头部分
        /// </summary>
        /// <returns></returns>
        private void MakeHeader()
        {
            //表头部分的设计            
            currentRow += 3;

            //流水号和页数
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.serialNumber;
            xlsSheet.Cells[10][currentRow] = xlsSheet.Cells[10][currentRow].Text + totalPages.ToString() + "--" + currentPage.ToString();
            currentRow++;
            //发货单号，环境温度         
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.invoice;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + dt.excelHeadData.temperature + "℃";
            currentRow += 2;
            //订单号，充电完成日期
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.contract;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + dt.excelHeadData.finish_Charging_Date.ToString().Split(' ')[0];
            currentRow += 2;
            //检测日期，测试开路电压标准
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.test_Date.ToString();
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + "≥" + dt.excelHeadData.voltage_Requirement + "V";
            currentRow += 2;
            //开路电压检测记录
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text;
            currentRow += 2;
        }

        /// <summary>*
        /// 生成V+R员工表头部分
        /// </summary>
        /// <returns></returns>
        private void MakeHeaderToAll()
        {
            //表头部分的设计            
            currentRow += 3;

            //流水号和页数
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.serialNumber;
            xlsSheet.Cells[12][currentRow] = xlsSheet.Cells[12][currentRow].Text + totalPages.ToString() + "--" + currentPage.ToString();
            currentRow++;
            //发货单号，环境温度         
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.invoice;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + dt.excelHeadData.temperature + "℃";
            currentRow += 2;
            //订单号，充电完成日期
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.contract;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + dt.excelHeadData.finish_Charging_Date.ToString().Split(' ')[0];
            currentRow += 2;
            //检测日期，测试开路电压标准
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.test_Date.ToString();
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + "≥" + dt.excelHeadData.voltage_Requirement + "V";
            currentRow += 2;
            //开路电压检测记录
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text;
            currentRow += 2;
        }

        /// <summary>
        /// 
        /// 
        /// 生成用户V+R表格部分
        /// </summary>
        private void MakeTable()
        {
            const int N = 20;   //每一页纸显示N行数据

            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[5][currentRow] = xlsSheet.Cells[5][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[9][currentRow] = xlsSheet.Cells[9][currentRow].Text + dt.voltageAndTime.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndTime.Count;
            //int numbersOfRows = (int)Math.Ceiling(totalData / 5.0);   //数据表所占的行数
            //填充编号和电压数据
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    xlsApp.Cells[1 + i * 4][currentRow + j] = j + 1 + i * N + (currentPage - 1) * 60;
                    if (listIndexes < totalData)
                    {
                        xlsSheet.Cells[4 + i * 4][currentRow + j] = dt.voltageAndTime[listIndexes].test_Time.ToString().Split(' ')[1];
                        xlsSheet.Cells[3 + i * 4][currentRow + j] = "'" + dt.voltageAndTime[listIndexes].resistance.ToString("F3");
                        xlsSheet.Cells[2 + i * 4][currentRow + j] = "'" + dt.voltageAndTime[listIndexes++].voltage.ToString("F3");
                    }
                }
            }

            currentRow += N;
        }
        /// <summary>
        /// 生成不需要表格尾部的用户V+R表格部分
        /// </summary>
        public void MakeTableNoFoot()
        {
            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[5][currentRow] = xlsSheet.Cells[5][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[9][currentRow] = xlsSheet.Cells[9][currentRow].Text + dt.voltageAndTime.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndTime.Count;

            //填充编号和电压数据
            try
            {
                for (int i = 1; i <= totalData; i++)
                {
                    xlsApp.Cells[1][currentRow] = i;
                    if (listIndexes < totalData)
                    {
                        xlsSheet.Cells[4][currentRow] = dt.voltageAndTime[listIndexes].test_Time.ToString().Split(' ')[1];
                        xlsSheet.Cells[3][currentRow] = "'" + dt.voltageAndTime[listIndexes].resistance.ToString("F3");
                        xlsSheet.Cells[2][currentRow] = "'" + dt.voltageAndTime[listIndexes++].voltage.ToString("F3");
                    }
                    currentRow++;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }


        /// <summary>
        /// 生成员工V+R表格部分
        /// </summary>
        private void MakeTableForAll()
        {
            const int N = 20;   //每一页纸显示N行数据

            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[11][currentRow] = xlsSheet.Cells[11][currentRow].Text + dt.voltageAndStatus.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndStatus.Count;
            //int numbersOfRows = (int)Math.Ceiling(totalData / 5.0);   //数据表所占的行数
            //填充编号和电压数据
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    try
                    {
                        xlsApp.Cells[1 + i * 5][currentRow + j] = j + 1 + i * N + (currentPage - 1) * 60;
                        if (listIndexes < totalData)
                        {
                            xlsSheet.Cells[5 + i * 5][currentRow + j] = dt.resistanceAndStatus[listIndexes].status;
                            xlsSheet.Cells[4 + i * 5][currentRow + j] = "'" + dt.resistanceAndStatus[listIndexes].resistance.ToString("F3");
                            xlsSheet.Cells[3 + i * 5][currentRow + j] = dt.voltageAndStatus[listIndexes].status;
                            xlsSheet.Cells[2 + i * 5][currentRow + j] = "'" + dt.voltageAndStatus[listIndexes++].voltage.ToString("F3");
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }
                }
            }

            currentRow += N;
        }
        /// <summary>
        /// 生成不需要表格尾部的员工V+R表格部分
        /// </summary>
        public void MakeTableForAllNoFoot()
        {
            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[11][currentRow] = xlsSheet.Cells[11][currentRow].Text + dt.voltageAndStatus.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndStatus.Count;
            //填充编号和电压数据
            try
            {
                for (int i = 1; i <= totalData; i++)
                {
                    xlsApp.Cells[1][currentRow] = i;
                    if (listIndexes < totalData)
                    {
                        xlsSheet.Cells[5][currentRow] = dt.resistanceAndStatus[listIndexes].status;
                        xlsSheet.Cells[4][currentRow] = "'" + dt.resistanceAndStatus[listIndexes].resistance.ToString("F3");
                        xlsSheet.Cells[3][currentRow] = dt.voltageAndStatus[listIndexes].status;
                        xlsSheet.Cells[2][currentRow] = "'" + dt.voltageAndStatus[listIndexes++].voltage.ToString("F3");
                    }
                    currentRow++;

                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }
        }

        /// <summary>
        /// 主页面打印表格的数据部分
        /// </summary>
        /// <param name="list"></param>
        public void MakeTableForMain(List<DetailAndBatchInfo> list)
        {
            currentRow += 2;
            //开始填充数据

            try
            {
                for (int i = 0; i < list.Count; i++)
                {
                    xlsSheet.Cells[1][currentRow] = i + 1;
                    xlsSheet.Cells[2][currentRow] = list[i].voltage.ToString("F3");
                    xlsSheet.Cells[3][currentRow] = list[i].Vstate;
                    xlsSheet.Cells[4][currentRow] = list[i].resistance.ToString("F3");
                    xlsSheet.Cells[5][currentRow] = list[i].Rstate;
                    xlsSheet.Cells[6][currentRow] = list[i].startTime;
                    xlsSheet.Cells[7][currentRow] = list[i].invoice;
                    xlsSheet.Cells[8][currentRow] = list[i].contract;
                    xlsSheet.Cells[9][currentRow] = list[i].serialNumber;
                    xlsSheet.Cells[10][currentRow] = list[i].batch;
                    xlsSheet.Cells[11][currentRow] = list[i].type;
                    xlsSheet.Cells[12][currentRow] = list[i].plc;

                    currentRow++;
                }
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e.ToString());
            }


        }

        /// <summary>
        /// 生成V+R表尾部分
        /// </summary>
        /// <returns></returns>
        private void MakeFooter()
        {

            //电池电压一致性
            xlsSheet.Cells[1][currentRow] = "电池电压一致性:" + "  压差≤" + dt.excelHeadData.uniformity_Of_Sells + "\n" + "uniformity of sells";
            currentRow += 3;

            //出厂核查
            currentRow += 4;

            //结论
            currentRow += 5;

            //表格编号
            currentRow += 2;

            //操作员，检测员
            xlsApp.Cells[1][currentRow] = xlsApp.Cells[1][currentRow].Text + " " + dt.excelHeadData.operatorUser + "#机";// +dt.excelHeadData.operatorUser;
            //xlsApp.Cells[5][currentRow] = "检验Checker:";// +dt.excelHeadData.checker;
            currentRow++;

            //插入分页线
            try
            {
                xlsSheet.Rows[currentRow].PageBreak = 1;
            }
            catch
            {
                //MessageBox.Show("这个地方出错");
            }

            //当前页数加一
            currentPage++;
        }

        /// <summary>*
        /// 生成员工V表头部分
        /// </summary>
        /// <returns></returns>
        private void VMakeHeaderToAll()
        {
            //表头部分的设计            
            currentRow += 3;

            //流水号和页数
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.serialNumber;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + totalPages.ToString() + "--" + currentPage.ToString();
            currentRow++;
            //发货单号，环境温度         
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.invoice;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.temperature + "℃";
            currentRow += 2;
            //订单号，充电完成日期
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.contract;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.finish_Charging_Date.ToString().Split(' ')[0];
            currentRow += 2;
            //检测日期，测试开路电压标准
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.test_Date.ToString();
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + "≥" + dt.excelHeadData.voltage_Requirement + "V";
            currentRow += 2;
            //开路电压检测记录
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text;
            currentRow += 2;
        }

        /// <summary>
        /// 生成员工V表格部分
        /// </summary>
        private void VMakeTableForAll()
        {
            const int N = 20;   //每一页纸显示N行数据

            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[4][currentRow] = xlsSheet.Cells[4][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[7][currentRow] = xlsSheet.Cells[7][currentRow].Text + dt.voltageAndStatus.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndStatus.Count;
            //int numbersOfRows = (int)Math.Ceiling(totalData / 5.0);   //数据表所占的行数
            //填充编号和电压数据
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    try
                    {
                        xlsApp.Cells[1 + i * 3][currentRow + j] = j + 1 + i * N + (currentPage - 1) * 60;
                        if (listIndexes < totalData)
                        {
                            xlsSheet.Cells[3 + i * 3][currentRow + j] = dt.voltageAndStatus[listIndexes].status;
                            xlsSheet.Cells[2 + i * 3][currentRow + j] = "'" + dt.voltageAndStatus[listIndexes++].voltage.ToString("F3");
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.ToString());
                    }
                }
            }

            currentRow += N;
        }

        /// <summary>
        /// 生成无表格尾部员工V表格部分
        /// </summary>
        private void VMakeTableForAllNoFoot()
        {
            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[4][currentRow] = xlsSheet.Cells[4][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[7][currentRow] = xlsSheet.Cells[7][currentRow].Text + dt.voltageAndStatus.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndStatus.Count;

            for (int i = 1; i <= totalData; i++)
            {
                try
                {
                    xlsApp.Cells[1][currentRow] = i;
                    if (listIndexes < totalData)
                    {
                        xlsSheet.Cells[3][currentRow] = dt.voltageAndStatus[listIndexes].status;
                        xlsSheet.Cells[2][currentRow] = "'" + dt.voltageAndStatus[listIndexes++].voltage.ToString("F3");
                    }
                }
                catch (Exception e)
                {
                    System.Console.WriteLine(e.ToString());
                }
                currentRow++;
            }
        }

        /// <summary>
        /// 生成V表尾部分
        /// </summary>
        /// <returns></returns>
        private void VMakeFooter()
        {

            //电池电压一致性
            xlsSheet.Cells[1][currentRow] = "电池电压一致性:" + "  压差≤" + dt.excelHeadData.uniformity_Of_Sells + "\n" + "uniformity of sells";
            currentRow += 3;

            //出厂核查
            currentRow += 4;

            //结论
            currentRow += 5;

            //表格编号
            currentRow += 2;

            //操作员，检测员
            xlsApp.Cells[1][currentRow] = xlsApp.Cells[1][currentRow].Text + " " + dt.excelHeadData.operatorUser + "#机";// +dt.excelHeadData.operatorUser;
            //xlsApp.Cells[5][currentRow] = "检验Checker:";// +dt.excelHeadData.checker;
            currentRow++;

            //插入分页线
            try
            {
                xlsSheet.Rows[currentRow].PageBreak = 1;
            }
            catch
            {
                //MessageBox.Show("这个地方出错");
            }

            //当前页数加一
            currentPage++;
        }

        /// <summary>
        /// 生成V用户表头部分
        /// </summary>
        /// <returns></returns>
        private void VMakeHeader()
        {
            //表头部分的设计            
            currentRow += 3;

            //流水号和页数
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.serialNumber;
            xlsSheet.Cells[8][currentRow] = xlsSheet.Cells[8][currentRow].Text + totalPages.ToString() + "--" + currentPage.ToString();
            currentRow++;
            //发货单号，环境温度         
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.invoice;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.temperature + "℃";
            currentRow += 2;
            //订单号，充电完成日期
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.contract;
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + dt.excelHeadData.finish_Charging_Date.ToString().Split(' ')[0];
            currentRow += 2;
            //检测日期，测试开路电压标准
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.test_Date.ToString();
            xlsSheet.Cells[6][currentRow] = xlsSheet.Cells[6][currentRow].Text + "≥" + dt.excelHeadData.voltage_Requirement + "V";
            currentRow += 2;
            //开路电压检测记录
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text;
            currentRow += 2;
        }



        /// <summary>
        /// 生成用户V表格部分
        /// </summary>
        private void VMakeTable()
        {
            const int N = 20;   //每一页纸显示N行数据

            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[4][currentRow] = xlsSheet.Cells[4][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[7][currentRow] = xlsSheet.Cells[7][currentRow].Text + dt.voltageAndTime.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndTime.Count;
            //int numbersOfRows = (int)Math.Ceiling(totalData / 5.0);   //数据表所占的行数
            //填充编号和电压数据
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    xlsApp.Cells[1 + i * 3][currentRow + j] = j + 1 + i * N + (currentPage - 1) * 60;
                    if (listIndexes < totalData)
                    {
                        xlsSheet.Cells[3 + i * 3][currentRow + j] = dt.voltageAndTime[listIndexes].test_Time.ToString().Split(' ')[1];
                        xlsSheet.Cells[2 + i * 3][currentRow + j] = "'" + dt.voltageAndTime[listIndexes++].voltage.ToString("F3");
                    }
                }
            }

            currentRow += N;
        }

        /// <summary>
        /// 生成不需要表格尾部用户V表格部分
        /// </summary>
        private void VMakeTableNoFoot()
        {
            //电池型号，批号，数量
            xlsSheet.Cells[1][currentRow] = xlsSheet.Cells[1][currentRow].Text + dt.excelHeadData.type;
            xlsSheet.Cells[4][currentRow] = xlsSheet.Cells[4][currentRow].Text + dt.excelHeadData.code;
            xlsSheet.Cells[7][currentRow] = xlsSheet.Cells[7][currentRow].Text + dt.voltageAndTime.Count;
            currentRow += 2;

            //数据表设计部分
            currentRow += 2;

            //需要打印的数据表的排版设计
            int totalData = dt.voltageAndTime.Count;

            for (int i = 1; i <= totalData; i++)
            {
                xlsApp.Cells[1][currentRow] = i;
                if (listIndexes < totalData)
                {
                    xlsSheet.Cells[3][currentRow] = dt.voltageAndTime[listIndexes].test_Time.ToString().Split(' ')[1];
                    xlsSheet.Cells[2][currentRow] = "'" + dt.voltageAndTime[listIndexes++].voltage.ToString("F3");
                }
                currentRow++;
            }
        }



        /// <summary>
        /// 保存Excel文件并关闭当前Excel进程
        /// </summary>
        /// <param name="xlsApp"></param>
        /// <param name="filePath"></param>
        private void SaveAndExit()
        {
            //将制作的Excel保存到指定位置
            try
            {
                //xlsApp.ActiveWorkbook.SaveAs(Application.StartupPath + "\\report\\" + addr);
                //xlsApp.ActiveWorkbook.SaveAs(filePath);
                xlsBook.SaveAs(filePath, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt,
                Excel.XlSaveAsAccessMode.xlNoChange, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt, xlsObjOpt);

                xlsApp.DisplayAlerts = false;
                xlsApp.Visible = true;

                //显示打印界面                
                //xlsSheet.PrintPreview(true);

                try
                {
                    xlsBook.Close(xlsObjOpt, xlsObjOpt, xlsObjOpt);
                    xlsApp.Workbooks.Close();
                    xlsApp.Quit();
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheets);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsSheet);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsRange);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsBook);
                    System.Runtime.InteropServices.Marshal.ReleaseComObject(xlsApp);

                    xlsBook = null;
                    xlsApp = null;
                    GC.Collect();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                KillSpecialExcel(xlsApp);   //关闭当前Excel进程

                MessageBox.Show("数据导出成功！");

            }
            catch
            {
                MessageBox.Show("数据导出失败！");
            }
            finally
            {
                System.Diagnostics.Process.Start("explorer", "/n, " + filePath);    //打开新生成的Excel报表文件
            }
            //调用析构函数
            Destructor();
        }

        //关闭当前Excel进程
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern int GetWindowThreadProcessId(IntPtr hwnd, out int ID);
        private void KillSpecialExcel(Microsoft.Office.Interop.Excel.Application xlsApp)
        {
            try
            {
                if (xlsApp != null)
                {
                    int ipdwProcessID;
                    GetWindowThreadProcessId(new IntPtr(xlsApp.Hwnd), out ipdwProcessID);
                    System.Diagnostics.Process.GetProcessById(ipdwProcessID).Kill();
                }
            }
            catch (Exception ex)
            {
                Console.Write("Delete Excel Process Error:" + ex.Message);

            }
        }

        /// <summary>
        /// 析构函数
        /// </summary>
        private void Destructor()
        {
            xlsApp = null;
            xlsBook = null;
            xlsSheets = null;
            xlsSheet = null;
            xlsRange = null;
            xlsObjOpt = System.Reflection.Missing.Value;

            this.dt = null;
            this.filePath = null;
            this.listIndexes = 0;
            this.currentRow = 1;
            this.totalPages = 0;
            this.currentPage = 1;
        }
    }
}
