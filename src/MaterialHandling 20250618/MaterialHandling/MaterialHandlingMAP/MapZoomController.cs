using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MaterialHandling.MaterialHandlingMAP
{
    public class MapZoomController
    {
        private const int carWidth = 100;
        private const int carHeight = 85;
        //private const double beta = 0.7; // 弧度制
        //private const double L = 65.6;
        private PointF _offset = PointF.Empty;
        private PointF _lastMousePos;
        private bool _isPanning;

        // 地图原始尺寸和显示尺寸
        private Size _mapSize;
        private Size _displaySize;

        // 雷达与小车中心校准值
        public double calibrationValue { get; set; } = 46.5;

        // 自定义坐标系原点（默认为地图左上角）
        private PointF _userOrigin = PointF.Empty;

        public double OffsetWidth = 47.8, OffsetHeight = 39.8;

        // 允许外部设置坐标系原点（例如设置为位置 (750,200)为坐标原点）
        public PointF UserOrigin
        {
            get => _userOrigin;
            set
            {
                // 如果设置的坐标超出地图范围，则自动修正为地图中心
                if (value.X < 0 || value.X >= _mapSize.Width ||
                    value.Y < 0 || value.Y >= _mapSize.Height)
                {
                    _userOrigin = new PointF(
                        _mapSize.Width / 2f,
                        _mapSize.Height / 2f
                    );
                }
                else
                {
                    _userOrigin = value;
                }

                CenterMap(); // 无论是否修正，都触发居中逻辑
            }
        }

        public event EventHandler ViewChanged;

        public MapZoomController(Size mapSize, Size displaySize)
        {
            _mapSize = mapSize;
            _displaySize = displaySize;
            CenterMap();
        }

        // 坐标转换逻辑
        public Point MapToScreen(Point mapPoint)
        {
            Point ret = new Point(
                (int)(_offset.X + mapPoint.X + _userOrigin.X),
                (int)(_offset.Y + _userOrigin.Y - mapPoint.Y) // Y轴向上为正
            );
            return ret;
        }

        public Point ScreenToMap(Point screenPoint)
        {
            Point ret = new Point(
                (int)((screenPoint.X - _offset.X) - _userOrigin.X),
                (int)(_userOrigin.Y - (screenPoint.Y - _offset.Y))
            );
            return ret;
        }

        // 处理鼠标事件
        public void HandleMouseDown(Point location)
        {
            _lastMousePos = location;
            _isPanning = true;
        }

        // 处理鼠标移动
        public void HandleMouseMove(Point location)
        {
            if (!_isPanning) return;

            var delta = new PointF(
                location.X - _lastMousePos.X,
                location.Y - _lastMousePos.Y
            );

            _offset.X += delta.X;
            _offset.Y += delta.Y;
            _lastMousePos = location;

            ConstrainOffset();
            ViewChanged?.Invoke(this, EventArgs.Empty);
        }

        // 处理鼠标释放
        public void HandleMouseUp()
        {
            _isPanning = false;
        }



        // 居中逻辑
        private void CenterMap()
        {
            _offset.X = (_displaySize.Width - _mapSize.Width) / 2 - _userOrigin.X;
            _offset.Y = (_displaySize.Height - _mapSize.Height) / 2 + _userOrigin.Y;
            ConstrainOffset();
        }

        // 约束偏移量
        private void ConstrainOffset()
        {
            // 计算允许的偏移范围（未考虑缩放因子_zoom）
            float maxX = Math.Max(0, _mapSize.Width - _displaySize.Width);
            float maxY = Math.Max(0, _mapSize.Height - _displaySize.Height);

            // 使用Math.Max和Math.Min来模拟Math.Clamp的功能
            _offset.X = Math.Max(-maxX, Math.Min(_offset.X, maxX));
            _offset.Y = Math.Max(-maxY, Math.Min(_offset.Y, maxY));
        }

        // 绘制地图（无缩放版本）
        public void DrawMap(Graphics g, Image mapImage)
        {
            g.InterpolationMode = InterpolationMode.NearestNeighbor;
            g.PixelOffsetMode = PixelOffsetMode.Half;

            var destRect = new Rectangle(
                (int)_offset.X,
                (int)_offset.Y,
                _mapSize.Width,
                _mapSize.Height
            );

            g.DrawImage(mapImage, destRect);

            // 新增红色分割线绘制（在原点Y轴位置）
            DrawOriginYAxisLine(g);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="carPicture">小车图片</param>
        /// <param name="location">雷达坐标</param>
        /// <param name="angleDegrees">弧度值角度</param>
        //public void DrawCar(Graphics g, Image carPicture, PointF location, float angleDegrees) // location 是地图坐标系中的“右中”点, angleDegrees 是旋转角度
        //{
        //    // 如果图片为空，则不执行任何操作
        //    if (carPicture == null)
        //    {
        //        return;
        //    }
        //    float theta = RadiansToDegrees(angleDegrees);
        //    PointF screenAnchorPoint = MapToScreen(location);
        //    // 旋转中心
        //    int xPrime = (int)(screenAnchorPoint.X - 48 * Math.Cos(angleDegrees));
        //    int yPrime = (int)(screenAnchorPoint.Y - 48 * Math.Sin(angleDegrees));

        //    Console.WriteLine("xprime : " + xPrime + ", yPrime : " + yPrime);
        //    // 目标绘制尺寸
        //    int targetWidth = 100;
        //    int targetHeight = 85;

        //    GraphicsState originalState = g.Save();

        //    try
        //    {
        //        g.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        g.SmoothingMode = SmoothingMode.AntiAlias;

        //        // 将坐标系原点移动到汽车的锚点（旋转中心）
        //        g.TranslateTransform(xPrime, yPrime);
        //        //g.TranslateTransform(screenAnchorPoint.X, screenAnchorPoint.Y);

        //        // 旋转坐标系
        //        g.RotateTransform(-1 * theta);

        //        // 4. 定义汽车图片的绘制矩形。
        //        //    由于坐标系已经移动并旋转，我们现在相对于新的原点（即 screenAnchorPoint）进行绘制。
        //        //    汽车的“右中”点应该位于这个新原点 (0,0)。
        //        //    所以，图片的左上角相对于这个新原点是 (-targetWidth, -targetHeight / 2)。
        //        /*
        //        Rectangle carImageRect = new Rectangle(
        //            -targetWidth,                       // X: 从旋转中心向左移动一个车宽
        //            -(int)(targetHeight / 2.0f),        // Y: 从旋转中心向上移动半个车高
        //            targetWidth,
        //            targetHeight
        //        );   
        //        */
        //        Rectangle carImageRect = new Rectangle(
        //            -(targetWidth / 2),                  // X: 从旋转中心（图片几何中心）向左移动半个车宽
        //            -(targetHeight / 2),                // Y: 从旋转中心（图片几何中心）向上移动半个车高
        //            targetWidth,
        //            targetHeight
        //        );

        //        // 绘制经过变换（旋转）的汽车图片
        //        g.DrawImage(carPicture, carImageRect);

        //        float dotRadius = 3f; // 红点的半径
        //        float dotDiameter = dotRadius * 2;
        //        // 计算红点绘制的左上角坐标，使其中心位于 screenAnchorPoint
        //        //float dotX = screenAnchorPoint.X - dotRadius;
        //        //float dotY = screenAnchorPoint.Y - dotRadius;
        //        float dotX = (targetWidth / 2) - dotRadius;
        //        float dotY = 0 - dotRadius;

        //        using (SolidBrush redBrush = new SolidBrush(Color.Red))
        //        {
        //            g.FillEllipse(redBrush, dotX, dotY, dotDiameter, dotDiameter);
        //        }
        //    }
        //    finally
        //    { 
        //        g.Restore(originalState);
        //    }




        //    DrawOriginYAxisLine(g);
        //}u1

        public Point TransLidaToCarMiddle(PointF radarLocationMap, float angleRadians)
        {
            int xPrime = (int)(radarLocationMap.X - calibrationValue * Math.Cos(angleRadians));
            int yPrime = (int)(radarLocationMap.Y - calibrationValue * Math.Sin(angleRadians));
            return new Point(xPrime, yPrime);
        }
        public Point TransLidaToCarLeft(PointF radarLocationMap, float angleRadians)
        {
            
            int xPrime = (int)(radarLocationMap.X - GetL(OffsetHeight,OffsetWidth) * Math.Cos(angleRadians + GetLidaToCarTheta(OffsetHeight, OffsetWidth)));
            int yPrime = (int)(radarLocationMap.Y - GetL(OffsetHeight, OffsetWidth) * Math.Sin(angleRadians + GetLidaToCarTheta(OffsetHeight, OffsetWidth)));
            return new Point(xPrime, yPrime);
        }
        public double GetLidaToCarTheta(double a,double b)
        {
            double angle = Math.Atan2(a, b);
            return angle;
        }
        public double GetL(double a, double b)
        {
            double c = Math.Sqrt(a * a + b * b);
            return c;
        }
        public void DrawCar(Graphics g, Image carPicture, PointF radarLocationMap, float angleRadians)
        {
            // 如果图片为空，则不执行任何操作
            if (carPicture == null)
            {
                return;
            }

            var primePoint = TransLidaToCarLeft(radarLocationMap, angleRadians);
            int xPrime = primePoint.X;
            int yPrime = primePoint.Y;


            PointF screenCarAnchorPoint = MapToScreen(new Point(xPrime, yPrime));
            // 将传入的弧度角转换为角度，供 RotateTransform 使用
            float angleForTransformDegrees = RadiansToDegrees(angleRadians);

            // 转换 雷达锚点（右中）的地图坐标 为 屏幕坐标
            PointF screenRadarAnchorPoint = MapToScreen(new Point((int)radarLocationMap.X, (int)radarLocationMap.Y));

            // 目标绘制尺寸 (小车缩放后的大小)
            int targetWidth = 100;
            int targetHeight = 85;

            Console.WriteLine($"GeometricCenter: ({xPrime}, {yPrime})");


            // 保存当前的 Graphics 状态
            GraphicsState originalState = g.Save();

            try
            {
                // 1. 设置图像质量
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // 2. 将坐标系原点移动到汽车的几何中心 (geometricCenterX, geometricCenterY)
                g.TranslateTransform(screenCarAnchorPoint.X, screenCarAnchorPoint.Y);
                

                // 3. 旋转坐标系
                // GDI+ 中，正角度表示顺时针。若希望 angleRadians (其正值表示逆时针) 能正确应用，则用负的角度值。
                g.RotateTransform(-angleForTransformDegrees);

                // 4. 定义汽车图片的绘制矩形。
                //    由于坐标系已经移动到图片的几何中心并已旋转，
                //    图片的左上角相对于这个新原点（图片的几何中心）是 (-targetWidth / 2, -targetHeight / 2)。
                Rectangle carImageRect = new Rectangle(
                    -(targetWidth / 2),                // X: 从旋转中心（图片几何中心）向左移动半个车宽
                    -(targetHeight / 2),               // Y: 从旋转中心（图片几何中心）向上移动半个车高
                    targetWidth,
                    targetHeight
                );
                // 绘制 机器臂start
                int clawWidth = targetWidth;
                int clawHeight = 110;
                int clawX = -(targetWidth / 2);
                //int clawY = -(targetHeight / 2) + targetHeight;
                int clawY = -(targetHeight / 2) - clawHeight;
                Rectangle carClawRect = new Rectangle(
                     clawX,
                     clawY,
                     clawWidth,
                     clawHeight
                );
                var b = new SolidBrush(Color.BlueViolet);
                g.FillRectangle(b, carClawRect);

                // 绘制 机器臂start


                // 5. 绘制经过变换（旋转）的汽车图片
                g.DrawImage(carPicture, carImageRect);
                

                // --- 在变换后的坐标系中绘制红点标识 (位于车辆的右中侧) ---
                // 这个红点会随着车辆一起旋转。
                // 其位置是相对于车辆几何中心 (当前坐标系原点0,0) 的 (targetWidth/2, 0)
                float dotRadius = 3f; // 红点的半径
                float dotDiameter = dotRadius * 2;

                float localDotX = (targetWidth / 2.0f) - dotRadius; // 相对于几何中心的X (红点中心在右中)
                float localDotY = 0 - dotRadius;                     // 相对于几何中心的Y (红点中心在右中)

                using (SolidBrush redBrush = new SolidBrush(Color.Red), blueBrush = new SolidBrush(Color.Blue))
                {
                    g.FillEllipse(redBrush, localDotX, localDotY, dotDiameter, dotDiameter);

                    g.FillEllipse(blueBrush, 0 - 3, 0 - 3, dotDiameter, dotDiameter);

                }
                // --- 红点标识绘制结束 ---
            }
            finally
            {
                // 6. 恢复 Graphics 到调用此方法之前的状态
                g.Restore(originalState);
            }
            DrawOriginYAxisLine(g);
        }
       
        public void DrawMatrixRect(Graphics g, RectangleInfo matrixRect)
        {
            // 创建一个画笔，用于绘制矩形边框
            using (Pen pen = new Pen(Color.Red, 3)) // 红色边框，宽度为3
            {
                // 绘制矩形边框
                g.DrawRectangle(pen, matrixRect.x, matrixRect.y, matrixRect.Width, matrixRect.Height);
            }
        }

        /// <summary>
        /// 弧度值 转 角度值
        /// </summary>
        /// <param name="radians"></param>
        /// <returns></returns>
        public static float RadiansToDegrees(float radians)
        {
            return (float)(radians * (180.0 / Math.PI));
        }

        /// <summary>
        /// 将角度转换为弧度的函数
        /// </summary>
        /// <param name="degrees"></param>
        /// <returns></returns>
        public static float DegreesToRadians(float degrees)
        {
            return degrees * ((float)Math.PI / 180.0f);
        }
        private void DrawOriginYAxisLine(Graphics g)
        {
            // 计算线段的屏幕坐标
            float lineY = _offset.Y + _userOrigin.Y;

            // 确定线段横跨整个显示区域
            using (var redPen = new Pen(Color.White, 1))
            {
                g.DrawLine(redPen,
                    _offset.X,          // 从控件最左侧开始
                    lineY,
                    _offset.X + _displaySize.Width, // 到控件最右侧结束
                    lineY
                );
            }

            Point lf = new Point(0, 0);
            // 绘制黑色坐标原点（用户自定义原点）
            var originScreenPos = MapToScreen(lf); // 用户坐标系 (0,0) 对应原点
            //var originScreenPos = MapToScreen(PointF.Empty); // 用户坐标系 (0,0) 对应原点
            using (var blackBrush = new SolidBrush(Color.Green))
            {
                g.FillEllipse(blackBrush,
                    originScreenPos.X - 4, // 中心点向左偏移2像素
                    originScreenPos.Y - 4, // 中心点向上偏移2像素
                    8,  // 直径4像素
                    8
                );
            }
        }
    }
}