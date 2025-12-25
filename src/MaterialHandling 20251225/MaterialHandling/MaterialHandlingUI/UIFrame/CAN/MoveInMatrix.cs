using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MaterialHandling.MaterialHandlingMAP;
using MaterialHandling.MaterialHandlingUI.UIFrame.Control;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.CAN
{
    public class MoveInMatrix
    {
        Point topLeft;
        Point topRight;
        Point bottomLeft;
        Point bottomRight;
        MapVehiclePanel mapVehiclePanel;
        public VCUMotionCalculate vCUMotionCalculate;
        public static Point endPosition;
        private TaskCompletionSource<bool> motionCompletionSource;
        public static double MatrixRatio = 0.3;
        public int distanceToRight = 50;
        public int distanceToTop = 50;
        public MoveInMatrix()
        {
            mapVehiclePanel = MainFrame.cf.mapVehiclePanel;
            topLeft = MatrixStrategy.GetTopLeft(); // 返回左上角坐标
            topRight = MatrixStrategy.GetTopRight(); // 返回右上角坐标
            bottomLeft = MatrixStrategy.GetBottomLeft(); // 返回左下角坐标
            bottomRight = MatrixStrategy.GetBottomRight(); // 返回右下角坐标
            vCUMotionCalculate = new VCUMotionCalculate();
        }
        public bool IsMoveToTopLeft = false;//是否移动到左偏上位置的标志位
        public async Task MovementLogicAsync()
        {
            // 1. 获取当前位姿
            double currentX = mapVehiclePanel.carPosition.X;
            double currentY = mapVehiclePanel.carPosition.Y;
            double currentTheta = mapVehiclePanel.carTheta;

            // 1.1 第一段运动：移动到矩形中间偏左，Y 保持当前 Y 坐标
            double rectangleWidth = topRight.X - topLeft.X;
            double rectangleHeight = bottomLeft.Y - topLeft.Y;
            double boundedX = Math.Max(topLeft.X, Math.Min(topLeft.X + MatrixRatio * rectangleWidth, topRight.X));
            double boundedY = currentY; // 直接使用当前 Y 坐标
            // boundedY = Math.Max(topLeft.Y, Math.Min(topLeft.Y + 0.3 * rectangleHeight, bottomRight.Y)); //移动到左偏上位置
            if (IsMoveToTopLeft) boundedY = Math.Min(topLeft.Y + MatrixRatio * rectangleHeight,topRight.Y); //移动到左偏上位置
            endPosition = new Point((int)boundedX, (int)boundedY);//用于绘制的终点

            // 创建完成信号
            motionCompletionSource = new TaskCompletionSource<bool>();

            // 订阅完成事件
            vCUMotionCalculate.MotionCompleted += OnFirstMotionCompleted;

            // 开始第一段运动
            vCUMotionCalculate.Calculate(boundedX, boundedY, 0);

            // 等待第一段运动完成
            await motionCompletionSource.Task;

            // 取消订阅
            vCUMotionCalculate.MotionCompleted -= OnFirstMotionCompleted;

            // 1.2 第二段运动：移动到右上角
            boundedX = Math.Max(topLeft.X, Math.Min(topRight.X - distanceToRight, topRight.X)); // 右上角 X - distanceToRight
            boundedY = Math.Max(bottomLeft.Y, Math.Min(topRight.Y - distanceToTop, topLeft.Y)); // 右上角 Y - distanceToTop
            endPosition = new Point((int)boundedX, (int)boundedY);//用于绘制的终点

            // 创建新的完成信号
            motionCompletionSource = new TaskCompletionSource<bool>();
            vCUMotionCalculate.MotionCompleted += OnSecondMotionCompleted;

            // 开始第二段运动
            vCUMotionCalculate.Calculate(boundedX, boundedY, 0);

            // 等待第二段运动完成
            await motionCompletionSource.Task;

            // 取消订阅
            vCUMotionCalculate.MotionCompleted -= OnSecondMotionCompleted;
        }


        private void OnFirstMotionCompleted(object sender, EventArgs e)
        {
            motionCompletionSource?.SetResult(true);
        }

        private void OnSecondMotionCompleted(object sender, EventArgs e)
        {
            motionCompletionSource?.SetResult(true);
        }
    }
}
