using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLCForm
{
    public class PLCSignal //控制信号类
    {
        public PLCSignal() {

            this.bool1 = true;
            this.bool2 = true;
            this.bool3 = true;
            this.bool4 = false;
            this.bool5 = true;
            this.bool6 = true;
            this.bool7 = true;
            this.bool8 = false;
            this.bool9 = true;
            this.bool10 = true;
            this.bool11 = true;
            this.bool12 = true;
            this.bool13 = false;
            this.bool14 = true;
            this.bool15 = true;
            this.bool16 = true;


            this.Int1 = 1;
            this.Int2 = 2;
            this.Int3 = 3;
            this.Int4 = 4;
            this.Int5 = 5;
            this.Int6 = 6;
            this.Int7 = 7;
            this.Int8 = 8;
            this.Int9 = 9;
            this.Int10 = 10;

            this.Dint1 = 10;
            this.Dint2 = 20;
            this.Dint3 = 30;
            this.Dint4 = 40;
            this.Dint5 = 50;
            this.Dint6 = 60;
            this.Dint7 = 70;
            this.Dint8 = 80;
            this.Dint9 = 90;
            this.Dint10 = 100;
        }

        public int DB_number; //需要输入的数据块号

        //bool值数据区
        public bool bool1;
        public bool bool2;
        public bool bool3;
        public bool bool4;
        public bool bool5;
        public bool bool6;
        public bool bool7;
        public bool bool8;
        public bool bool9;
        public bool bool10;
        public bool bool11;
        public bool bool12;
        public bool bool13;
        public bool bool14;
        public bool bool15;
        public bool bool16;


        //int16值数据区
        public short Int1;
        public short Int2;
        public short Int3;
        public short Int4;
        public short Int5;
        public short Int6;
        public short Int7;
        public short Int8;
        public short Int9;
        public short Int10;

        //int32值数据区
        public int Dint1;
        public int Dint2;
        public int Dint3;
        public int Dint4;
        public int Dint5;
        public int Dint6;
        public int Dint7;
        public int Dint8;
        public int Dint9;
        public int Dint10;

    }
}
