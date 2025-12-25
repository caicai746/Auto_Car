using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    /// <summary>
    /// 一个class类，用于保存一条电压记录（时间、电压、标记）
    /// </summary>
    public class DBNum
    {
        public DateTime detecctionTime;     //检测时间
        public float voltage;               //电压值
        public int flag;                    //标记
        public int number;                  //编号
        public float resistance;           //内阻值
        public int stationadress;           //站地址
        public string Batterycode;             //电池编码
        public string Checkid;              //check表主键
    }
    public class DataToForms
    {
        public List<DBNum> informationOfVol;    //电池电压数据容器

        public float maxVol;                    //电压最大值
        public float minVol;                    //电压最小值
        public float avgVol;                    //电压平均值
        public float deviationVol;              //当前电压偏差
        public float upVol;                     //当前电压上限
        public float lowerVol;                  //当前电压下限
        public float maxRes;                    //内阻最大值
        public float minRes;                    //内阻最小值
        public float avgRes;                    //内阻平均值
        public float deviationRes;              //当前内阻偏差
        public float upRes;                     //当前内阻上限
        public float lowerRes;                  //当前内阻下限
        public float Rstandarddeviation;         //当前内阻标准差
        public int currentNumber;               //当前有效记录的数量


        public int normalNumber;                //正常电池数量
        public int lowerNumber;                 //超电压下限电池数量
        public int upperNumber;                 //超电压上限电池数量
        public int RlowerNumber;                //超内阻下限电池数量
        public int RupperNumber;                //超内阻上限电池数量
        public bool isOver;                     //判断程序是否结束

        public int statusFlag;                  //当前记录处理情况。0-标记为0，1-标记为1，2-标记为2，3-标记为3，4-不满足下限丢弃
        public int stationaddress;               //当前电压的站地址
        public bool isGetStandard;              //判断是否得到电压上下限
        public bool isGetRStandard;             //判断是否得到内阻上下限

        public List<int> unqualifiedList;  //储存电压值不满足下限的电池编号
        public List<int> flag3List;        //储存电压值标记为3的电池编号
        public List<int> flag5List;        //储存标记为5的电池编号
        public List<int> flag7List;        //储存标记为7的电池编号
        public List<int> flagList;         //储存标记3,5,7的电池编号
        public List<DBNum> flag2List;        //储存算法二中标记为2的电池编号
        public bool DeviationOverLimit;             //算法三标记为3的偏差超限的
    }
}
