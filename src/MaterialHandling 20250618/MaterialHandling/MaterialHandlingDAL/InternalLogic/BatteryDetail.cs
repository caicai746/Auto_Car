using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MaterialHandling.MaterialHandlingDAL.InternalLogic
{
    class BatteryDetail
    {
        public string batchID { get; set; }
        public DateTime 检测时间 { get; set; }
        public int 站点地址 { get; set; }
        public float 电压 { get; set; }
        public float 内阻 { get; set; }
        public float 标准电压 { get; set; }
        public float 温度 { get; set; }
        public int 标志 { get; set; }
        public int 算法 { get; set; }
        public int 检测机 { get; set; }
    }
}
