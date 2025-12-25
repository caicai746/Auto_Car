using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQToSQL;
using DataToDBEnts;
using MaterialHandling.MaterialHandlingDAL.Entity.LINQ.ParameterType;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    public class GetDataToExcel
    {
        public ExcelHeadData excelHeadData = new ExcelHeadData();
        public List<ExcelVoltageData> voltageAndTime = new List<ExcelVoltageData>();
        public List<ExcelVoltageAndStatus> voltageAndStatus = new List<ExcelVoltageAndStatus>();
        public List<ExcelResistanceAndStatus> resistanceAndStatus = new List<ExcelResistanceAndStatus>();

        public GetDataToExcel( string batchGUID, String invoice, String contract, String serialNumber, String batch, String type, String deviationVol,
                                List<float> normalVol,List<float> allVol,List<DateTime> TestTimeOfNormalVol,List<string> allVolFlag,
                                List<float> normalRes,List<float> allRes,List<string> allResFlag)
        {
            //填充excel表的表头数据
            excelHeadData.invoice = invoice;
            excelHeadData.contract = contract;
            excelHeadData.serialNumber = serialNumber;
            excelHeadData.type = type;
            excelHeadData.customer = "admin";
            excelHeadData.code = batch;
            //excelHeadData.temperature = ProductionCheckDB.QueryOneData("select * from Production_Check where BatchID={0}",new Guid(batchGUID)).Temperature.ToString();//invoice??
            //excelHeadData.finish_Charging_Date = ProductionBatchDB.QueryOneData("select * from Production_Batch where GUID={0}", new Guid(batchGUID)).Finish_Charging_Time;
            excelHeadData.voltage_Requirement = ParameterTypeLinq.GetUpLimitByModelId(type);

            //excelHeadData.test_Date = ProductionBatchDB.QueryOneData("select * from Production_Batch where GUID={0}", new Guid(batchGUID)).Start_Time;
            excelHeadData.uniformity_Of_Sells = deviationVol+"mV";

            excelHeadData.appearance = "好看";
            excelHeadData.accessories = "充电器";
            excelHeadData.document = "详细";
            excelHeadData.packing = "精致";

            excelHeadData.conclusion = "电池合格";
            //string stationAddress = ProductionBatchDB.QueryOneData("select * from Production_Check where BatchID={0}", batchGUID).Station_Address.ToString();    //站地址
            //excelHeadData.operatorUser = stationAddress;
            //excelHeadData.operatorUser = ProductionInvoiceDB.QueryOneData("select * from Production_Invoice where GUID={0}", new Guid(invoiceGUID)).UserID;
            excelHeadData.checker = "User";

            //填充excel表的附表：有效电压数据,遍历normalVol 和 TestTimeOfNormalVol
            for( int idx = 0; idx < normalVol.Count; idx++ ) {
                ExcelVoltageData excelVoltageData = new ExcelVoltageData();
                excelVoltageData.voltage = normalVol[idx];
                excelVoltageData.resistance = normalRes[idx];
                excelVoltageData.test_Time = TestTimeOfNormalVol[idx];
                voltageAndTime.Add(excelVoltageData);
            }

            //填充excel表的附表：所有电压数据、内阻数据和状态，遍历allVol和allVolFlag,allRes,allResFlag
            for( int jdx = 0; jdx < allVol.Count; jdx++ ) {
                ExcelVoltageAndStatus excelVoltageAndStatus = new ExcelVoltageAndStatus();
                ExcelResistanceAndStatus excelResistanceAndStatus = new ExcelResistanceAndStatus();
                excelVoltageAndStatus.voltage = allVol[jdx];
                excelVoltageAndStatus.status = allVolFlag[jdx];
                excelResistanceAndStatus.resistance = allRes[jdx];
                excelResistanceAndStatus.status = allResFlag[jdx];
                voltageAndStatus.Add(excelVoltageAndStatus);
                resistanceAndStatus.Add(excelResistanceAndStatus);
            }
        }

        //定义excel表头部分数据类
        public class ExcelHeadData
        {
            public String invoice;//发货单号
            public String contract;//订单号
            public String serialNumber;//流水号
            public String type;//电池型号
            public String customer;//用户
            public String code;//电池批号
            public String temperature;//环境温度
            public DateTime? finish_Charging_Date;//充电完成日期
            public String voltage_Requirement;//测试开路电压标准

            public DateTime test_Date;//检测日期
            public String uniformity_Of_Sells;//电池电压一致性

            public String appearance;//外观
            public String accessories;//配件
            public String document;//资料
            public String packing;//包装

            public String conclusion;//结论
            public String operatorUser;//操作员
            public String checker;//检验员
        }

        //定义excel表中附表：电压和内阻数据类（给用户的，电压、内阻数据和时间）
        public class ExcelVoltageData
        {
            public float voltage;//电压
            public float resistance;//内阻
            public DateTime test_Time;//测试时间
        }

        //定义excel中附表：电压数据类（给员工的，电压数据和标志）
        public class ExcelVoltageAndStatus
        {
            public float voltage;//电压
            public String status;//标志
        }
        //定义内阻数据类
        public class ExcelResistanceAndStatus
        {
            public float resistance;//内阻
            public String status;//内阻标志
        }
    }
}
