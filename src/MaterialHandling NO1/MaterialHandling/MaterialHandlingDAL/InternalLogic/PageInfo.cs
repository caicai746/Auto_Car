using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaterialHandling.MaterialHandlingDAL.Entity.QueryForm;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    class PageInfo
    {
        public int totalCount;//数据总数
        public int totalPageNumber;//总页数
        public int currentPageNumber;//当前页数
        public int everyPageShowCount;//每一页显示的数据数量

        public PageInfo() {
        }

        public void GetTotalPageNumber() {//判断页数
            if (totalCount % everyPageShowCount == 0) {
                totalPageNumber = totalCount / everyPageShowCount;
            } else {
                totalPageNumber = totalCount / everyPageShowCount + 1;
            }
        }

        public void CommonEveryPageShow( PagingForQueryTable pagingQueryTable, int currentPageNumber, DataGridView dataGridView, TextBox textbox_PageNumber ) {
            dataGridView.Rows.Clear();
            pagingQueryTable.GetPagingQuery(everyPageShowCount, currentPageNumber,dataGridView);
            textbox_PageNumber.Text = currentPageNumber + "";
        }
        public void CommonEveryLogPageShow(PagingForQueryTable pagingQueryTable, int currentPageNumber, DataGridView dataGridView, TextBox textbox_PageNumber)
        {
            dataGridView.Rows.Clear();
            pagingQueryTable.GetLogPaggQuery(everyPageShowCount, currentPageNumber, dataGridView);
            textbox_PageNumber.Text = currentPageNumber + "";
        }
        public void DetailEveryPageShow( PagingForDetailedTable pagingForDetailTable, int currentPageNumber, DataGridView dataGridView, TextBox textBox_PageNumber) {
            dataGridView.Rows.Clear();//清空行
            pagingForDetailTable.GetPagingDetailed(everyPageShowCount, currentPageNumber, dataGridView);
            textBox_PageNumber.Text = currentPageNumber + "";
        }
    }
}
