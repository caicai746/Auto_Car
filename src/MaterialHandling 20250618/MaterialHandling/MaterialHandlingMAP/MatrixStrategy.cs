using MaterialHandling.MaterialHandlingUI.UIFrame;
using System;
using System.Drawing;

namespace MaterialHandling.MaterialHandlingMAP
{
    // 假设 AreaType 枚举已在 MaterialHandling.MaterialHandlingMAP 命名空间中定义:
    // public enum AreaType
    // {
    //     Movable = 0, // 可移动区域
    //     Obstacle = 1 // 障碍物
    // }

    public struct RectangleInfo
    {
        // x, y 为相对屏幕坐标
        public int x { get; set; }
        public int y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

        public override string ToString()
        {
            return $"左上角坐标: ({y}, {x}), 宽度: {Width}, 高度: {Height}";
        }
    }

    public class MatrixStrategy
    {
        //private MapZoomController _zoomController;
        public static RectangleInfo curRect = new RectangleInfo();
        /// <summary>
        /// 检查在指定的行带内，从某个起始列开始，有多少列是完全由可移动区域组成的。
        /// </summary>
        /// <param name="mapMatrix">地图数据 (整数形式，其中值对应 AreaType 枚举)</param>
        /// <param name="startRow">行带的起始行</param>
        /// <param name="targetHeight">行带的高度</param>
        /// <param name="startCol">检查的起始列</param>
        /// <param name="mapRows">地图总行数</param>
        /// <param name="mapCols">地图总列数</param>
        /// <returns>从startCol开始的有效宽度</returns>
        private static int GetSolidWidthStartingFromColumn(
            int[,] mapMatrix,
            int startRow,
            int targetHeight,
            int startCol,
            int mapRows,
            int mapCols)
        {
            if (startRow < 0 || startRow + targetHeight > mapRows || startCol < 0 || startCol >= mapCols)
            {
                return 0; // 起始位置或区域超出地图边界
            }

            int achievableWidth = 0;
            for (int c = startCol; c < mapCols; c++)
            {
                bool columnIsValid = true;
                for (int rOffset = 0; rOffset < targetHeight; rOffset++)
                {
                    int currentRow = startRow + rOffset;
                    // 再次检查行边界，虽然理论上startRow + targetHeight已检查，但多一层保护
                    if (currentRow >= mapRows || mapMatrix[currentRow, c] == (int)AreaType.Obstacle) // 使用枚举值进行比较
                    {
                        columnIsValid = false;
                        break;
                    }
                }

                if (columnIsValid)
                {
                    achievableWidth++;
                }
                else
                {
                    break; //遇到障碍或列部分无效，宽度确定
                }
            }
            return achievableWidth;
        }


        /// <summary>
        /// 根据特定策略寻找下一个优选的矩形区域。
        /// 策略：固定矩形高度，尝试从当前矩形向上平移，并检查宽度是否满足安全值。
        /// </summary>
        /// <param name="mapMatrix">当前地图的二维数组 (值对应 AreaType 枚举, 例如 0表示AreaType.Movable, 1表示AreaType.Obstacle)。</param>
        /// <param name="prevRectInfo">上一个选择的矩形信息。</param>
        /// <param name="targetRectHeight">新矩形期望的固定高度。</param>
        /// <param name="presetSafeWidth">新矩形预设的最小安全宽度。</param>
        /// <param name="initialUpwardAttemptValue">初始向上尝试平移的行数（正值表示向上）。默认为40。</param>
        /// <param name="minUpwardAttemptValue">向上尝试平移的最小行数（可以是0或负值，负值表示向下）。默认为0。</param>
        /// <returns>找到的新的合适矩形信息。如果未找到，返回的RectangleInfo的Width和Height为0。</returns>
        public static RectangleInfo FindNextPreferredRect(
            int[,] mapMatrix,
            RectangleInfo prevRectInfo,
            int targetRectHeight,
            int presetSafeWidth,
            int initialUpwardAttemptValue = 40,
            int minUpwardAttemptValue = 0)
        {
            if (mapMatrix == null || mapMatrix.GetLength(0) == 0 || mapMatrix.GetLength(1) == 0)
            {
                Console.WriteLine("错误: 地图数据无效。");
                return new RectangleInfo { Width = 0, Height = 0, y = -1, x = -1 };
            }
            if (targetRectHeight <= 0 || presetSafeWidth <= 0)
            {
                Console.WriteLine("错误: 目标高度和安全宽度必须为正。");
                return new RectangleInfo { Width = 0, Height = 0, y = -1, x = -1 };
            }

            int mapRows = mapMatrix.GetLength(0);
            int mapCols = mapMatrix.GetLength(1);

            RectangleInfo bestFoundRect = new RectangleInfo { Width = 0, Height = 0, y = -1, x = -1 };

            // 迭代尝试不同的向上平移量 (deltaY)
            // deltaY 为正表示新矩形的顶边比旧矩形的顶边更靠上
            for (int deltaY = initialUpwardAttemptValue; deltaY >= minUpwardAttemptValue; deltaY--)
            {
                int newTopRow = prevRectInfo.y - deltaY;

                // 1. 检查新矩形的垂直边界是否有效
                if (newTopRow < 0 || (newTopRow + targetRectHeight) > mapRows)
                {
                    continue; // 新的垂直范围超出地图边界，尝试下一个deltaY
                }

                // 2. 确定新矩形的左列 (X 坐标) 和计算宽度
                // 策略：以旧矩形的左边界 prevRectInfo.LeftCol 为起点尝试
                // 未来可以优化：在旧矩形横向范围内(prevRectInfo.LeftCol 到 prevRectInfo.LeftCol + prevRectInfo.Width)
                // 寻找一个最佳的起始列，使得 GetSolidWidthStartingFromColumn 最大化。
                // 为简化，我们先固定使用 prevRectInfo.LeftCol。

                int potentialLeftCol = prevRectInfo.x;
                //int potentialLeftCol = 0;
                if (potentialLeftCol < 0 || potentialLeftCol >= mapCols) // 确保旧矩形左列本身在界内
                {
                    // 旧矩形信息可能无效或已在地图边缘导致计算起始列出界
                    // 这种情况下，此deltaY的尝试可能无意义，或者需要更复杂的左列定位逻辑
                    continue;
                }

                int achievableWidth = GetSolidWidthStartingFromColumn(
                                            mapMatrix,
                                            newTopRow,
                                            targetRectHeight,
                                            potentialLeftCol,
                                            mapRows,
                                            mapCols);

                // 3. 判断宽度是否满足预设安全宽度
                if (achievableWidth >= presetSafeWidth)
                {
                    // 找到了一个符合条件的矩形
                    // 根据你的需求，可以选择第一个找到的（即最靠上的），或者继续迭代寻找是否有“更好”的
                    // （例如，如果希望在满足条件下宽度也尽量大，这里可能需要比较）
                    // 当前实现是找到第一个满足条件的、最靠上的矩形就返回。
                    bestFoundRect.y = newTopRow;
                    bestFoundRect.x = potentialLeftCol;
                    bestFoundRect.Width = achievableWidth; // 使用实际获得的安全宽度
                    bestFoundRect.Height = targetRectHeight;
                    return bestFoundRect; // 直接返回第一个找到的满足条件的
                }
            }

            // 如果循环结束仍未找到满足条件的矩形
            Console.WriteLine($"未能根据策略找到合适的矩形 (目标高度: {targetRectHeight}, 安全宽度: {presetSafeWidth})。");
            return bestFoundRect; // 返回 Width=0, Height=0 的 RectangleInfo
        }

        // 返回左上角坐标
        public static Point GetTopLeft()
        {
            return MainFrame.cf.mapVehiclePanel._zoomController.ScreenToMap(new Point(curRect.x, curRect.y));
        }

        // 返回右上角坐标
        public static Point GetTopRight()
        {
            return MainFrame.cf.mapVehiclePanel._zoomController.ScreenToMap(new Point(curRect.x + curRect.Width, curRect.y));
        }

        // 返回左下角坐标
        public static Point GetBottomLeft()
        {
            return MainFrame.cf.mapVehiclePanel._zoomController.ScreenToMap(new Point(curRect.x, curRect.y + curRect.Height));
        }

        // 返回右下角坐标
        public static Point GetBottomRight()
        {
            return MainFrame.cf.mapVehiclePanel._zoomController.ScreenToMap(new Point(curRect.x + curRect.Width, curRect.y + curRect.Height));
        }

    }
}
