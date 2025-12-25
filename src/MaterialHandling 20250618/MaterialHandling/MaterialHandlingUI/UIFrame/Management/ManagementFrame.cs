using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using MaterialHandlingUI;
using MaterialHandling.MaterialHandlingDAL.InternalLogic;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.BatteryType;
using MaterialHandling.MaterialHandlingDAL.Entity.ParameterManage.User;
using MaterialHandling.MaterialHandlingDAL.Entity.QueryForm;
using MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery;
using MaterialHandling.MaterialHandlingDAL.Entity.ModifyAndDeleteForDB;
using MaterialHandling.MaterialHandlingUI.UIFrame.ROS;
using MaterialHandling.MaterialHandlingUI.UIFrame.UserControls;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Management
{
    public partial class ManagementFrame : Form
    {
        public List<string> MaterialNum;
        ToolStripStatusLabel tsslBlank;
        PageInfo commonPageInfo;
        Boolean commonTimeflag = false;//综合查询的时间控件默认不启用
        PagingForQueryTable pagingQueryTable;
        QueryParameter logParameter = new QueryParameter();
        QueryComboList queryComboList;
        int commonEveryPageShowCount = 25;//综合查询查询每页默认显示的数据数量
        public ROSClient rOSClient;
        public ManagementFrame(ToolStripStatusLabel tsslBlank)
        {
            InitializeComponent();
            IsUseDatabase();
            ROS_Panel_Load();
            Load_Panel_VCUParameters();
            InitializeButton();
            SettingDateTimePickerCustom(dateTimePicker_CommonTime1);
            SettingDateTimePickerCustom(dateTimePicker_CommonTime2);
            dateTimePicker_CommonTime1.Value = dateTimePicker_CommonTime2.Value.AddMonths(-1);
            this.tsslBlank = tsslBlank;
            if (Program.useDatabase) FillDateToView();

        }

        private void IsUseDatabase()
        {
            if(! Program.useDatabase)
            {
                btn_NewType.Enabled = false;
                btn_DeleteType.Enabled = false;
                btn_EditType.Enabled = false;
                btn_QueryType.Enabled = false;
                btn_NewUser.Enabled = false;
                btn_DeleteUser.Enabled = false;
                btn_EditUser.Enabled = false;
                btn_QueryUser.Enabled = false;
                btn_Deletelog.Enabled = false;
                btn_Querylog.Enabled = false;
            }
        }
        VCUMotionCalculateParameters vCUMotionCalculateParameters;
        private void Load_Panel_VCUParameters()
        {
            vCUMotionCalculateParameters = new VCUMotionCalculateParameters();// 系统参数面板
            panel_VCUParameters.Controls.Add(vCUMotionCalculateParameters);
            vCUMotionCalculateParameters.Show();
        }

        private void ManagementFrame_Load(object sender, EventArgs e)
        {
            this.Dock = DockStyle.Fill;
            this.DoubleBuffered = true;
        }
        private void InitializeButton()
        {
            //ROS相关按钮
            rOSClient.btn_buildmap.Enabled = false;
            rOSClient.btn_SaveMap.Enabled = false;
            
            rOSClient.btn_ROS_Connect.Enabled = false;
            rOSClient.btn_StartLidar.Enabled = false;
            rOSClient.btn_ROS_Disconnect.Enabled = false;
        }
        public void EnableButton()
        {
            //ROS相关按钮
            //rOSClient.btn_buildmap.Enabled = true;
            //rOSClient.btn_SaveMap.Enabled = true;
            
            rOSClient.btn_ROS_Connect.Enabled = true;
            //rOSClient.btn_StartLidar.Enabled = true;
            //rOSClient.btn_ROS_Disconnect.Enabled = true;
        }
        public void ROS_Panel_Load()
        {
            rOSClient = new ROSClient();
            panel_ROS.Controls.Add(rOSClient);
            rOSClient.Show();
        }
        private void SettingDateTimePickerCustom(DateTimePicker dateTimePicker)
        {
            dateTimePicker.CustomFormat = "yyyy/MM/dd HH:mm:ss";
        }
        //查询员登录，只显示电池型号管理tab
        public void QueryManLogin()
        {
            tabControl1.TabPages.RemoveAt(3);
            tabControl1.TabPages.RemoveAt(2);
            tabControl1.TabPages.RemoveAt(1);
        }
        private void QueryResult()
        {

            commonPageInfo = new PageInfo(); //统计每页的信息
            pagingQueryTable = new PagingForQueryTable();//用于查询数据
            commonPageInfo.totalCount = pagingQueryTable.GetLogQueryCount(this.logParameter);//得到查询的数量，查询1次
            commonPageInfo.currentPageNumber = 1;//当前页面序号
            commonPageInfo.everyPageShowCount = commonEveryPageShowCount;//每个页面展示的数据个数
            commonPageInfo.GetTotalPageNumber();//页面总数
            textbox_Totalnum.Text = commonPageInfo.totalCount.ToString();
            label_CommonTotalPage.Text = "/" + commonPageInfo.totalPageNumber;
            commonPageInfo.CommonEveryLogPageShow(pagingQueryTable, commonPageInfo.currentPageNumber, DataGridView_common, textBox_CommonCurrentPage);//此处查询第二次
        }
        public void FillDateToView()
        {
            if(Program.useDatabase)
            {
                //对参数设置(电池类型)进行数据填充
                QueryTypeToView queryTypeToView = new QueryTypeToView(dataGridView_ParameterSetting);
                queryTypeToView.FillDataToView();

                //对用户设置进行数据填充
                QueryUserToView queryUserToView = new QueryUserToView(dataGridView_UserManage);
                queryUserToView.FillDataToView();
            }
            
        }



        private void btn_NewUser_Click(object sender, EventArgs e)
        {
            NewUser newUser = new NewUser(dataGridView_UserManage);
            newUser.ShowDialog();
        }
        /// <summary>
        /// 新建类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_NewType_Click(object sender, EventArgs e)
        {
            NewType newType = new NewType(dataGridView_ParameterSetting);
            newType.ShowDialog();
        }


        //删除类型的监听器
        private void btn_DeleteType_Click(object sender, EventArgs e)
        {
            //当选中某一项时，自动让该行也被选中
            dataGridView_ParameterSetting.Rows[dataGridView_ParameterSetting.CurrentCellAddress.Y].Selected = true;

            MessageBoxButtons messButton = MessageBoxButtons.OKCancel;
            DialogResult dr = MessageBox.Show("是否确定删除", "删除记录", messButton);
            if (dr == DialogResult.OK)//点击确定按钮
            {
                //LogHelp.Info(new LogContent("删除数据", "dataGridView_ParameterSetting.SelectedRows", LoginFrame.username));
                //去遍历datagridview每一行
                foreach (DataGridViewRow r in dataGridView_ParameterSetting.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        //从数据库中删除记录
                        String deleteBatteryType = r.Cells[0].Value + "";//获取选中行的型号数据
                        ParameterTypeDB.Delete("Delete from Parameter_Type where ModelID = {0}", deleteBatteryType);

                        //从界面上删除记录
                        dataGridView_ParameterSetting.Rows.Remove(r);
                    }
                }
            }

        }

        //给编辑类型按钮加监听器
        private void btn_EditType_Click(object sender, EventArgs e)
        {
            int idx = 0;//选中行的次数
            DataToDBCont.DataToDBDataContext DBCon = new DataToDBCont.DataToDBDataContext();    //创建LINQ连接
            //当选中某一项时，自动让该行也被选中
            dataGridView_ParameterSetting.Rows[dataGridView_ParameterSetting.CurrentCellAddress.Y].Selected = true;
            String[] needEditData = new String[10];//电池类型表共有9个字段数据需要被修改
            //去遍历datagridview每一行
            foreach (DataGridViewRow r in dataGridView_ParameterSetting.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    idx++;//选中行次数加1
                    for (int jdx = 0; jdx < 9; jdx++)
                    {
                        needEditData[jdx] = r.Cells[jdx].Value + "";
                    }
                    var typeid = (from PT in DBCon.Parameter_Type
                                  where PT.ModelID == needEditData[0]
                                  select PT).FirstOrDefault().typeid;
                    needEditData[9] = typeid.ToString();
                }
            }
            switch (idx)
            {
                case 0:
                    MessageBox.Show("请选择要编辑的某一行记录");
                    break;
                case 1:
                    EditType editType = new EditType(needEditData, dataGridView_ParameterSetting);
                    editType.ShowDialog();
                    break;
                default:
                    MessageBox.Show("每次可编辑的记录数为1，请重新选择");
                    break;
            }
        }
        public void CompareTime(ref DateTime t1, ref DateTime t2)
        {
            DateTime temp;
            if (t1 > t2)
            {
                temp = t1;
                t1 = t2;
                t2 = temp;
            }
        }
        //查询参数填充
        private void CreateQueryParameter()
        {
            logParameter.username = comboBox_Combineuser.Text;
            DateTime time1 = dateTimePicker_CommonTime1.Value;
            DateTime time2 = dateTimePicker_CommonTime2.Value;
            CompareTime(ref time1, ref time2);
            logParameter.flag = commonTimeflag;

            logParameter.startTime = time1;
            logParameter.endTime = time2;


        }
        //选择时间查询
        private void checkBox_CommonTime_MouseClick(object sender, MouseEventArgs e)
        {
            if (checkBox_CommonTime.Checked)
            {
                commonTimeflag = true;
                DatetimepickerEnable(true);
            }
            else
            {
                commonTimeflag = false;
                DatetimepickerEnable(false);
            }

        }
        private void DatetimepickerEnable(bool flag)
        {
            dateTimePicker_CommonTime1.Enabled = flag;
            dateTimePicker_CommonTime2.Enabled = flag;
        }

        private void page_ButtonUse(Boolean flag)
        {
            button_firstpage.Enabled = flag;
            button_lastpage.Enabled = flag;
            button_CommonPageSubmit.Enabled = flag;
            button_nextpage.Enabled = flag;
            button_finapage.Enabled = flag;
        }
        //删除日志按钮添加监听器
        private void btn_Deletelog_Click(object sender, EventArgs e)
        {
            //当选中某一项时，自动让该行也被选中
            DialogResult dr = MessageBox.Show("是否确定删除记录", "删除记录", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button2);
            if (dr == DialogResult.Yes)//如果点击“确定”按钮
            {
                GetDataAboutChoose("delete");
            }
        }
        private void GetDataAboutChoose(String ste)
        {
            try
            {
                //
                DataGridView_common.Rows[DataGridView_common.CurrentCellAddress.Y].Selected = true;
                foreach (DataGridViewRow r in DataGridView_common.SelectedRows)
                {
                    if (!r.IsNewRow)
                    {
                        logParameter.LogId = r.Cells[5].Value.ToString();
                        //获取界面上选中行的所有数据
                        logParameter.startTime = (DateTime)r.Cells[1].Value;
                        logParameter.username = r.Cells[2].Value.ToString();
                    }
                }
                if (ste == "delete")
                {
                    ModifyAndDelete.DeleteLogQueryTable(logParameter);
                    //重新刷新页面
                    CreateQueryParameter();//填充参数
                    page_ButtonUse(true);//让分页按钮可用
                    QueryResult();
                }

            }
            catch
            {

            }
        }
        //给删除用户按钮添加监听器
        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            //当选中某一项时，自动让该行也被选中
            dataGridView_UserManage.Rows[dataGridView_UserManage.CurrentCellAddress.Y].Selected = true;

            //去遍历datagridview每一行
            foreach (DataGridViewRow r in dataGridView_UserManage.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    //从数据库中删除记录
                    String deleteUserName = r.Cells[0].Value + "";//获取选中行的型号数据
                    ParameterUserDB.Delete("Delete from Parameter_User where Role_Name = {0}", deleteUserName);
                    //ParameterRoleDB.Delete("Delete from Parameter_Role where Role_Name = {0}", deleteUserName);

                    //从界面上删除记录
                    dataGridView_UserManage.Rows.Remove(r);
                }
            }
        }

        //给编辑用户按钮添加监听器
        private void btn_EditUser_Click(object sender, EventArgs e)
        {
            int idx = 0;//选中行的次数

            //当选中某一项时，自动让该行也被选中
            dataGridView_UserManage.Rows[dataGridView_UserManage.CurrentCellAddress.Y].Selected = true;

            String[] needEditData = new String[7];//用户表和角色表共有8个字段数据需要被修改
            DateTime[] needEditTime = new DateTime[2];//要修改的时间数据
            //去遍历datagridview每一行
            foreach (DataGridViewRow r in dataGridView_UserManage.SelectedRows)
            {
                if (!r.IsNewRow)
                {
                    idx++;//选中行次数加1
                    for (int jdx = 0; jdx < 7; jdx++)
                    {
                        needEditData[jdx] = r.Cells[jdx].Value + "";
                    }
                    needEditData[1] = "";
                    needEditTime[0] = (DateTime)r.Cells[4].Value;
                    needEditTime[1] = (DateTime)r.Cells[5].Value;
                }
            }
            switch (idx)
            {
                case 0:
                    MessageBox.Show("请选择要编辑的某一行记录");
                    break;
                case 1:
                    EditUser editUser = new EditUser(needEditData, needEditTime, dataGridView_UserManage);
                    editUser.ShowDialog();
                    break;
                default:
                    MessageBox.Show("每次可编辑的记录数为1，请重新选择");
                    break;
            }
        }

        //日志用户的模糊查询
        private void comboBox_Combineuser_TextUpdate(object sender, EventArgs e)
        {
            CreateQueryParameter();//填充参数
            queryComboList = new QueryComboList(logParameter);

            List<string> temp_Invoice = queryComboList.GetLogUserList();
            MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery.VagueQuery.ResultByVagueQuery(comboBox_Combineuser, temp_Invoice);
        }
        private void comboBox_QueryUser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox_QueryType_DropDown(object sender, EventArgs e)
        {
            //ComboBoxType comboBoxType = new ComboBoxType(comboBox_QueryType);
            //comboBoxType.ComboBoxTypeInsert();
        }

        private void comboBox_QueryUser_DropDown(object sender, EventArgs e)
        {
            //ComboBoxUsername comboBoxUsername = new ComboBoxUsername(comboBox_QueryUser);
            //comboBoxUsername.ComboBoxUsernameInsert();
        }

        //查询类型按钮加监听器
        private void btn_QueryType_Click(object sender, EventArgs e)
        {
            String queryType = comboBox_QueryType.Text.Trim();
            QueryTypeToView queryTypeToView = new QueryTypeToView(dataGridView_ParameterSetting);
            if (queryType == "")
            {
                //如果查询类型为空，则显示全部的类型
                queryTypeToView.FillDataToView();
            }
            else
            {
                //如果不为空，则对参数设置(电池类型)进行数据填充,有具体的型号
                queryTypeToView.QueryDataToView(queryType);
            }
        }

        //查询日志按钮加监听器
        private void button10_Click(object sender, EventArgs e)
        {
            CreateQueryParameter();//填充参数
            page_ButtonUse(true);//让分页按钮可用

            QueryResult();
        }

        private void comboBox_Combineuser_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        //查询用户按钮加监听器
        private void button8_Click(object sender, EventArgs e)
        {
            String username = comboBox_QueryUser.Text;
            QueryUserToView queryUserToView = new QueryUserToView(dataGridView_UserManage);
            if (username == "")
            {
                //如果查询用户为空，则显示全部的类型
                queryUserToView.FillDataToView();
            }
            else
            {
                //如果不为空，则对用户管理进行数据填充,有具体的用户名
                queryUserToView.QueryDataToView(username);
            }
        }

        private void comboBox_QueryType_TextUpdate(object sender, EventArgs e)
        {
            var temp_Type = ParameterTypeDB.Query<String>("select ModelID from Parameter_Type");
            MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery.VagueQuery.ResultByVagueQuery(comboBox_QueryType, temp_Type.ToList<string>());
        }

        private void comboBox_QueryUser_TextUpdate(object sender, EventArgs e)
        {
            var temp_User = ParameterUserDB.Query<String>("select Role_Name from Parameter_User");
            MaterialHandling.MaterialHandlingDAL.Entity.VagueQuery.VagueQuery.ResultByVagueQuery(comboBox_QueryUser, temp_User.ToList<string>());
        }

        //给串口号设置按钮加监听器
       
        //用于分页按钮重新显示数据
        private void PageInfoEveryShow(int currentPageNumber)
        {
            commonPageInfo.currentPageNumber = currentPageNumber;
            commonPageInfo.CommonEveryLogPageShow(pagingQueryTable, commonPageInfo.currentPageNumber, DataGridView_common, textBox_CommonCurrentPage);
        }
        //
        //首页按钮的监听器
        private void button_firstpage_Click(object sender, EventArgs e)
        {
            PageInfoEveryShow(1);//设置当前页数为1
        }
        //上一页按钮的监听器
        private void button_lastpage_Click(object sender, EventArgs e)
        {
            if (commonPageInfo.currentPageNumber != 1)
            {
                commonPageInfo.currentPageNumber--;
                PageInfoEveryShow(commonPageInfo.currentPageNumber);
            }
        }
        //确定按钮的监听器
        private void button_CommonPageSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                commonPageInfo.currentPageNumber = int.Parse(textBox_CommonCurrentPage.Text);
                PageInfoEveryShow(commonPageInfo.currentPageNumber);
            }
            catch { }
        }

        //下一页按钮的监听器
        private void button_nextpage_Click(object sender, EventArgs e)
        {
            if (commonPageInfo.currentPageNumber != commonPageInfo.totalPageNumber)
            {
                commonPageInfo.currentPageNumber++;
                PageInfoEveryShow(commonPageInfo.currentPageNumber);
            }
        }

        //尾页按钮的监听器
        private void button_finapage_Click(object sender, EventArgs e)
        {
            commonPageInfo.currentPageNumber = commonPageInfo.totalPageNumber;
            PageInfoEveryShow(commonPageInfo.currentPageNumber);
        }

        private void button_PLCip_Click(object sender, EventArgs e)
        {
            MainFrame.plc.PLCip = tb_PLCip.Text;//更改plc的ip地址
            MainFrame.plc.read_plc.IP = tb_PLCip.Text;
            MainFrame.plc.write_plc.IP = tb_PLCip.Text;
            //更改plc的频率
            int sleeptime;
            if (int.TryParse(tb_plc_sleeptime.Text, out sleeptime))
            {
                MainFrame.plc.sleeptime = sleeptime;
                MainFrame.plc.write_timer.Change(0, sleeptime);
                MainFrame.plc.read_timer.Change(0, sleeptime + 5);
            }
            else
            {
                MessageBox.Show("请输入整数！");
            }
        }

        private void rtb_Textshow_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
