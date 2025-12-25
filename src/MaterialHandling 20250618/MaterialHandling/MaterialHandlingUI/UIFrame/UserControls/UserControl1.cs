using System;
using System.Drawing;
using System.Windows.Forms;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.UserControls
{
    public partial class UserControl1 : UserControl
    {
        // 小车参数
        private float xA, yA; // 起点
        private float xB, yB; // 终点
        private float angle;  // 目标旋转角度
        private float speed;  // 平移速度
        private float angularSpeed; // 旋转角速度

        // 当前状态
        private float currentX = 100; // 当前位置
        private float currentY = 100; // 当前位置
        private float currentAngle;       // 当前角度
        private enum State { MovingToStartX, RotatingToTarget, Moving, RotatingBack, Idle }
        private State currentState = State.Idle; // 当前状态

        // 边界
        private RectangleF boundary; // 边界矩形

        // 计时器
        private Timer timer;

        // 小车图片
        private Image carImage;

        // 固定车长
        private float carLength = 90f; // 假设车长为 90 像素

        // 自定义原点
        private float originX = 0; // 自定义原点的 X 坐标
        private float originY = 0; // 自定义原点的 Y 坐标
        private float initialCarY; // 记录小车的初始 Y 坐标
        // 安全距离--距离边界多远算是安全距离
        private float safeDistance = 5f; // 假设安全距离为 5 像素

        private float initialPanelHeight; // 记录 Panel 的初始高度

        public UserControl1()
        {
            InitializeComponent();
            InitializeSimulation();
            InitializeTimer();
            LoadCarImage(); // 加载小车图片
        }

        // 加载小车图片
        private void LoadCarImage()
        {
            try
            {
                // 从文件加载图片（确保图片路径正确）
                carImage = Image.FromFile("C:\\project\\MaterialHandling 20240108 demo\\res\\img\\car.png"); // 替换为你的图片路径
            }
            catch (Exception ex)
            {
                MessageBox.Show("无法加载小车图片: " + ex.Message);
            }
        }

        private void InitializeSimulation()
        {


            // 记录小车的初始 Y 坐标
            initialCarY = currentY;

            // 设置起点（相对于自定义原点）
            xA = currentX - originX;
            yA = currentY - originY;

            // 计算终点（相对于自定义原点）
            float distanceFromRight = 20; // 小车终点距离矩形框右边的距离
            float distanceFromTop = 50;   // 小车终点距离矩形框顶部的距离
            CalculateDestination(distanceFromRight, distanceFromTop, out xB, out yB);

            // 计算旋转角度
            float deltaX = xB - xA;
            float deltaY = yB - yA;
            angle = (float)(Math.Atan2(deltaY, deltaX) * (180 / Math.PI));

            // 设置速度和角速度
            speed = 0.5f; // 平移速度（像素/秒）
            angularSpeed = 10.0f; // 旋转角速度（度/秒）

            // 初始化当前位置和角度
            currentX = xA;
            currentY = yA;
            currentAngle = 0;

            // 设置边界（Panel的尺寸）
            boundary = new RectangleF(0, 0, panel_carriage.Width, panel_carriage.Height);
        }

        // 初始化计时器
        private void InitializeTimer()
        {
            timer = new Timer();
            timer.Interval = 100; // 每100毫秒更新一次
            timer.Tick += Timer_Tick;
        }

        // 计时器事件：更新小车状态
        private void Timer_Tick(object sender, EventArgs e)
        {
            textBox1.Text = currentX.ToString();
            textBox2.Text = currentY.ToString();
            switch (currentState)
            {
                case State.MovingToStartX:
                    MoveToStartX();
                    break;
                case State.RotatingToTarget:
                    RotateToTarget();
                    break;
                case State.Moving:
                    MoveToDestination();
                    break;
                case State.RotatingBack:
                    RotateBackToInitial();
                    break;
                case State.Idle:
                    // 什么都不做
                    break;
            }

            panel_carriage.Invalidate(); // 触发重绘
        }
        private void MoveToStartX()
        {
            float targetX = 100; // 目标 X 坐标
            float deltaX = targetX - currentX;

            // 如果小车已经到达目标位置
            if (Math.Abs(deltaX) < 1)
            {
                currentX = targetX; // 精确设置为目标位置
                currentState = State.RotatingToTarget; // 切换到旋转状态
                return;
            }

            // 计算移动步长
            float step = speed * (timer.Interval / 1000.0f) * 10f;
            if (Math.Abs(deltaX) < step)
            {
                currentX = targetX; // 如果步长超过剩余距离，直接设置为目标位置
            }
            else
            {
                currentX += Math.Sign(deltaX) * step; // 否则按步长移动
            }
        }
        // 旋转到目标角度
        private void RotateToTarget()
        {
            currentAngle -= angularSpeed * (timer.Interval / 1000.0f);
            if (currentAngle <= angle)
            {
                currentAngle = angle;
                currentState = State.Moving; // 进入平移状态
                //实际控制小车旋转====
            }
            else
            {
                //实际控制小车停止旋转====
            }

            // 检查小车边缘是否碰到边界
            if (IsCarCollidingWithBoundary(currentX, currentY, currentAngle))
            {
                currentState = State.RotatingBack; // 如果碰到边界，停止旋转
            }
        }

        // 平移到终点
        private void MoveToDestination()
        {
            float deltaX = (xB - xA) * (timer.Interval / 1000.0f) * speed;
            float deltaY = (yB - yA) * (timer.Interval / 1000.0f) * speed;
            float newX = currentX + deltaX;
            float newY = currentY + deltaY;

            // 检查新位置是否超出边界
            if (IsCarCollidingWithBoundary(newX, newY, currentAngle))
            {
                // 如果碰到边界，停止移动
                currentState = State.RotatingBack;
            }
            else
            {
                currentX = newX;
                currentY = newY;
            }

            // 检查是否到达终点
            if (Math.Abs(currentX - xB) < 1 && Math.Abs(currentY - yB) < 1)
            {
                currentX = xB;
                currentY = yB;
                currentState = State.RotatingBack; // 进入旋转回初始状态
            }
        }

        // 旋转回初始角度
        private void RotateBackToInitial()
        {
            currentAngle += angularSpeed * (timer.Interval / 1000.0f);
            if (currentAngle >= 0)
            {
                currentAngle = 0;
                currentState = State.Idle; // 进入空闲状态
                timer.Stop(); // 停止计时器
            }

            // 检查小车边缘是否碰到边界
            if (IsCarCollidingWithBoundary(currentX, currentY, currentAngle))
            {
                currentState = State.Idle; // 如果碰到边界，停止旋转
            }
        }

        private bool IsCarCollidingWithBoundary(float x, float y, float angle)
        {
            // 计算缩放后的尺寸
            float scale = carLength / carImage.Width;
            float carWidth = carImage.Width * scale;
            float carHeight = carImage.Height * scale;

            // 计算小车的四个角（局部坐标系，以小车中心为原点）
            PointF[] corners = new PointF[]
            {
                new PointF(-carWidth / 2, -carHeight / 2), // 左上角
                new PointF(carWidth / 2, -carHeight / 2),  // 右上角
                new PointF(carWidth / 2, carHeight / 2),   // 右下角
                new PointF(-carWidth / 2, carHeight / 2)   // 左下角
            };

            // 旋转小车的四个角
            float radians = (float)(angle * Math.PI / 180);
            for (int i = 0; i < corners.Length; i++)
            {
                float rotatedX = (float)(corners[i].X * Math.Cos(radians) - corners[i].Y * Math.Sin(radians));
                float rotatedY = (float)(corners[i].X * Math.Sin(radians) + corners[i].Y * Math.Cos(radians));
                corners[i] = new PointF(x + rotatedX + originX, y + rotatedY + originY); // 转换到全局坐标系
            }

            // 检查旋转后的四个角是否在边界内
            foreach (var corner in corners)
            {
                if (corner.X < (boundary.Left + safeDistance) || corner.X > (boundary.Right - safeDistance) ||
                    corner.Y < (boundary.Top + safeDistance) || corner.Y > (boundary.Bottom - safeDistance))
                {
                    return true; // 如果有一个角超出边界，返回 true
                }
            }

            return false; // 所有角都在边界内
        }

        private void DrawCar(Graphics g, float x, float y, float angle)
        {
            if (carImage == null) return; // 如果图片未加载，直接返回

            // 计算缩放比例
            float scale = carLength / carImage.Width;

            // 计算缩放后的宽度和高度
            int scaledWidth = (int)(carImage.Width * scale);
            int scaledHeight = (int)(carImage.Height * scale);

            // 旋转轴偏移量（向下偏移）
            float rotationOffsetY = scaledHeight * 0.25f; // 旋转轴向下偏移25%的高度

            // 保存当前绘图状态
            var state = g.Save();

            // 平移和旋转
            g.TranslateTransform(x + originX, y + originY); // 平移到小车中心（考虑自定义原点）
            g.TranslateTransform(0, rotationOffsetY); // 将旋转轴向下偏移
            g.RotateTransform(angle); // 旋转
            g.TranslateTransform(0, -rotationOffsetY); // 将旋转轴移回

            // 绘制小车图片（等比例缩放）
            g.DrawImage(carImage, -scaledWidth / 2, -scaledHeight / 2, scaledWidth, scaledHeight);

            // 恢复绘图状态
            g.Restore(state);
        }

        // 绘制边界
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // 获取 Panel2 的宽度和高度
            int panelWidth = panel2.Width;
            int panelHeight = panel2.Height;

            // 定义边界矩形区域
            Rectangle borderRect = new Rectangle(0, 0, panelWidth, panelHeight);

            // 使用红色画笔绘制边界
            using (Pen borderPen = new Pen(Color.Red, 2)) // 红色，线宽为 2
            {
                e.Graphics.DrawRectangle(borderPen, borderRect);
            }
        }
        private void panel_carriage_Paint(object sender, PaintEventArgs e)
        {
            // 绘制矩形框
            Draw_Box(e.Graphics, e);

            // 绘制自定义原点
            DrawOrigin(e.Graphics, originX, originY);

            // 绘制小车
            DrawCar(e.Graphics, currentX, currentY, currentAngle);
        }
        private void Draw_Box(object sender, PaintEventArgs e)
        {
            // 获取 Panel 的宽度和高度
            int panelWidth = panel_carriage.Width;
            int panelHeight = panel_carriage.Height;

            // 计算框的高度（Panel 高度的 3/5）
            int boxHeight = (int)(panelHeight);


            // 计算框的左上角坐标（居中显示）
            int boxX = 0; // 框的左上角 X 坐标
            int boxY = 0; // 框的左上角 Y 坐标

            // 定义框的矩形区域
            Rectangle boxRect1 = new Rectangle(boxX, boxY, panelWidth, boxHeight);

            // 使用蓝色画笔绘制框
            using (Pen boxPen = new Pen(Color.Gray, 2)) // 蓝色，线宽为 2
            {
                e.Graphics.DrawRectangle(boxPen, boxRect1);
            }



        }

        private void button_carriage_W_Click(object sender, EventArgs e)
        {
            // 获取输入的宽度值
            int newWidth;
            if (int.TryParse(textBox_carriage_W.Text, out newWidth))
            {
                // 设置新的宽度
                panel_carriage.Width = newWidth;

                // 触发 SizeChanged 事件
                panel_carriage_SizeChanged(panel_carriage, EventArgs.Empty);

                // 检查小车是否超出新的边界
                if (IsCarCollidingWithBoundary(currentX, currentY, currentAngle))
                {
                    // 如果超出边界，调整小车位置或停止移动
                    currentState = State.Idle;
                }

                // 触发 Panel 的重绘
                panel_carriage.Invalidate();
            }
            else
            {
                MessageBox.Show("请输入有效的整数宽度值！");
            }
        }

        private void button_carriage_L_Click(object sender, EventArgs e)
        {
            // 获取输入的高度值
            int newHeight;
            if (int.TryParse(textBox_carriage_L.Text, out newHeight))
            {

                // 记录 Panel 的初始高度
                initialPanelHeight = panel_carriage.Height;

                // 记录 Panel 的原始底部位置
                int originalBottom = panel_carriage.Top + panel_carriage.Height;

                // 设置新的高度
                panel_carriage.Height = newHeight;

                // 调整 Panel 的 Top 属性，使其底部位置不变
                panel_carriage.Top = originalBottom - panel_carriage.Height;

                // 触发 SizeChanged 事件
                panel_carriage_SizeChanged(panel_carriage, EventArgs.Empty);

                // 检查小车是否超出新的边界
                if (IsCarCollidingWithBoundary(currentX, currentY, currentAngle))
                {
                    // 如果超出边界，调整小车位置或停止移动
                    currentState = State.Idle;
                }

                // 触发 Panel 的重绘
                panel_carriage.Invalidate();
            }
            else
            {
                MessageBox.Show("请输入有效的整数高度值！");
            }
        }

        private void panel_carriage_SizeChanged(object sender, EventArgs e)
        {
            // 获取新的 Panel 高度
            float newHeight = panel_carriage.Height;

            // 计算 Panel 高度的变化量
            float heightChange = newHeight - initialPanelHeight;

            // 调整小车的 Y 坐标
            initialCarY = currentY;
            currentY = initialCarY + heightChange;

            // 更新边界
            boundary = new RectangleF(0, 0, panel_carriage.Width, panel_carriage.Height);

            // 触发重绘
            panel_carriage.Invalidate();
        }


        // 绘制自定义原点
        private void DrawOrigin(Graphics g, float originX, float originY)
        {
            // 使用红色画笔
            Pen originPen = new Pen(Color.Red, 2);

            // 绘制一个小十字标记
            /*
            float crossSize = 10; // 十字的大小
            g.DrawLine(originPen, originX - crossSize, originY, originX + crossSize, originY); // 水平线
            g.DrawLine(originPen, originX, originY - crossSize, originX, originY + crossSize); // 垂直线
            */
            // 绘制一个圆点标记
            float dotRadius = 5; // 圆点的半径
            g.FillEllipse(Brushes.Yellow, originX - dotRadius, originY - dotRadius, dotRadius * 2, dotRadius * 2);
        }

        // 启动按钮点击事件
        private void btnStart_Click(object sender, EventArgs e)
        {
            InitializeSimulation(); // 重置参数
            currentState = State.MovingToStartX; // 先移动到 X 轴为 100 的位置
            timer.Start(); // 启动计时器
        }

        // 计算终点坐标
        // 输入：小车终点距离矩形框右边的距离 (distanceFromRight)，小车终点距离矩形框顶部的距离 (distanceFromTop)
        // 输出：小车终点坐标 (xB, yB)
        private void CalculateDestination(float distanceFromRight, float distanceFromTop, out float xB, out float yB)
        {
            // 矩形框的右上角坐标
            float right = boundary.Right;
            float top = boundary.Top;

            // 计算终点坐标（相对于自定义原点）
            xB = right - distanceFromRight - originX;
            yB = top + distanceFromTop - originY;
        }
    }
}