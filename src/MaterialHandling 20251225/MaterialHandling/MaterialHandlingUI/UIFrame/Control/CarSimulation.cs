using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    class CarSimulation
    {
        public static float xA, yA; // 起点
        public static float xB, yB; // 终点
        public static float angle;  // 目标旋转角度
        public static float speed;  // 平移速度
        public static float angularSpeed; // 旋转角速度

        // 当前状态
        public static float currentX = 100; // 当前位置
        public static float currentY = 100; // 当前位置
        public static float currentAngle;       // 当前角度
        public static enum State { MovingToStartX, RotatingToTarget, Moving, RotatingBack, Idle }
        public static State currentState = State.Idle; // 当前状态

        // 边界
        public RectangleF boundary; // 边界矩形

        // 计时器
        public Timer timer;

        // 小车图片
        public Image carImage;

        // 固定车长
        public static float carLength = 90f; // 假设车长为 90 像素

        // 自定义原点
        public static float originX = 0; // 自定义原点的 X 坐标
        public static float originY = 0; // 自定义原点的 Y 坐标
        public static float initialCarY; // 记录小车的初始 Y 坐标
        // 安全距离--距离边界多远算是安全距离
        public static float safeDistance = 5f; // 假设安全距离为 5 像素

        public static float initialPanelHeight; // 记录 Panel 的初始高度
    }
}
