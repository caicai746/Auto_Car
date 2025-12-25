using Newtonsoft.Json;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.UserControls
{

    public partial class CamToMaterial : UserControl
    {
        // 定义物料尺寸
        private const int MaterialWidth = 60;
        private const int MaterialHeight = 25;

        private List<MaterialPoint> _materialPoints = new List<MaterialPoint>();
        public CamToMaterial()
        {
            InitializeComponent();
        }

        public void setListPoint(List<MaterialPoint> list1)
        {
            _materialPoints = list1;
        }

        //private void CamToMaterial_Paint(object sender, PaintEventArgs e)
        //{
        //    Graphics g = e.Graphics;
        //    // 设置高质量绘图
        //    g.SmoothingMode = SmoothingMode.AntiAlias;

        //    // 获取Panel的中心点作为坐标原点 (0,0)
        //    float centerX = this.Width / 2.0f;
        //    float centerY = this.Height / 2.0f;

        //    // 1. 绘制坐标轴 (可选)
        //    g.DrawLine(Pens.DarkOrange, centerX, 0, centerX, this.Height); // Z轴
        //    g.DrawLine(Pens.DarkOrange, 0, centerY, this.Width, centerY);  // X轴

        //    // 2. 遍历并绘制每一个物料
        //    using (SolidBrush fillBrush = new SolidBrush(Color.CornflowerBlue))
        //    using (Pen borderPen = new Pen(Color.DarkBlue, 1.5f))
        //    {
        //        foreach (var point in _materialPoints)
        //        {
        //            // **核心：坐标转换**
        //            float screenX = (float)point.X + centerX;
        //            float screenY = (float)-point.Z + centerY;

        //            // 计算椭圆的左上角坐标，使其中心位于(screenX, screenY)
        //            float rectX = screenX - MaterialWidth / 2.0f;
        //            float rectY = screenY - MaterialHeight / 2.0f;

        //            // 绘制并填充椭圆
        //            g.FillEllipse(fillBrush, rectX, rectY, MaterialWidth, MaterialHeight);
        //            g.DrawEllipse(borderPen, rectX, rectY, MaterialWidth, MaterialHeight);
        //        }
        //    }
        //}
        private void CamToMaterial_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            // 设置高质量绘图
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // 获取Panel的中心点作为坐标原点 (0,0)
            float centerX = this.Width / 2.0f;
            float centerY = this.Height / 2.0f;

            // 1. 绘制坐标轴 (可选)
            g.DrawLine(Pens.DarkOrange, centerX, 0, centerX, this.Height); // Z轴
            g.DrawLine(Pens.DarkOrange, 0, centerY, this.Width, centerY);  // X轴

            // 2. 遍历并绘制每一个物料和序号
            using (SolidBrush fillBrush = new SolidBrush(Color.CornflowerBlue))
            using (Pen borderPen = new Pen(Color.DarkBlue, 1.5f))
            // 新增：为文字创建画刷和字体
            using (SolidBrush textBrush = new SolidBrush(Color.Black))
            using (Font textFont = new Font("Arial", 10, FontStyle.Bold))
            // 新增：创建StringFormat用于文本居中
            using (StringFormat sf = new StringFormat())
            {
                // 设置文本对齐为水平居中和垂直居中
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;

                // 使用 for 循环来获取索引 i
                for (int i = 0; i < _materialPoints.Count; i++)
                {
                    var point = _materialPoints[i];

                    Console.WriteLine("== [x = {0}, z = {1}] ==", point.X, point.Z);

                    // 核心：坐标转换
                    float screenX = (float)point.X + centerX;
                    float screenY = (float)-point.Z + centerY;

                    // 计算椭圆的左上角坐标，使其中心位于(screenX, screenY)
                    float rectX = screenX - MaterialWidth / 2.0f;
                    float rectY = screenY - MaterialHeight / 2.0f;

                    // 绘制并填充椭圆
                    //g.FillEllipse(fillBrush, rectX, rectY, MaterialWidth, MaterialHeight);

                    g.DrawEllipse(borderPen, rectX, rectY, MaterialWidth, MaterialHeight);

                    // **新增：在椭圆中心绘制数字序号**
                    string text = (i + 1).ToString();
                    // 使用 screenX 和 screenY 作为绘制中心，配合 StringFormat 自动居中
                    g.DrawString(text, textFont, textBrush, screenX, screenY, sf);
                }
            }
        }
    }
    public class MaterialPoint
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        // ... 其他相关信息
    }

    public class ApiResponse
    {
        [JsonProperty("data")]
        public List<MaterialPoint> Data { get; set; }
    }
}
