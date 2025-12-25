using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace MaterialHandling.MaterialHandlingMAP
{
    public enum AreaType
    {
        Obstacle = 0, // 障碍物
        Movable = 1   // 可移动区域

    }

    public static class MapUtil
    {
        // 地图编号
        public static string MapId { get; private set; }

        // 地图宽度
        public static int Width { get; private set; }

        // 地图高度
        public static int Height { get; private set; }

        // 地图最大灰度值
        public static int MaxGrayValue { get; private set; }
        /// <summary>
        /// 从文件加载地图数据
        /// </summary>

        public static int[,] LoadMapData1(string filePath)
        {
            // 检查文件是否存在
            if (!File.Exists(filePath))
                throw new FileNotFoundException("地图文件不存在", filePath);

            // 读取文件的所有行
            string[] lines = File.ReadAllLines(filePath);

            /*
            // 确保文件至少有 3 行（编号、宽高、最大灰度值）
            if (lines.Length < 3)
                throw new InvalidDataException("文件至少需要包含 3 行数据");

            // 读取并保存第一行：地图编号
            MapId = lines[0].Trim();

            // 读取并解析第二行：宽度和高度
            string[] dimensions = lines[1].Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            if (dimensions.Length != 2)
                throw new InvalidDataException("第二行必须包含宽度和高度");
            

            Width = int.Parse(dimensions[0]); // 地图宽度
            Height = int.Parse(dimensions[1]); // 地图高度
            */
            Width =  1500;
            Height = 500;

            // 确保文件的行数正确（地图数据行）
            //if (lines.Length != Height)
            //    throw new InvalidDataException($"地图数据行数不正确，应为 {Height} 行，实际为 {lines.Length} 行");

            // 初始化地图数据数组
            int[,] map = new int[Height, Width];

            // 从第四行开始读取地图数据
            for (int y = 0; y < Height; y++)
            {
                // 移除空格，并将数据拼接成连续字符串
                string line = lines[y].Replace(" ", "");

                // 验证每行的长度是否等于地图宽度
                if (line.Length != Width)
                    throw new InvalidDataException($"第 {y} 行长度不符，应为 {Width} 字符，实际为 {line.Length}");

                // 遍历每行的每个字符
                for (int x = 0; x < Width; x++)
                {
                    char c = line[x];
                    if (c == '0') // 0 表示可通行区域
                    {
                        map[y, x] = 0;
                    }
                    else if (c == '1') // 1 表示障碍物
                    {
                        map[y, x] = 1;
                    }
                    else // 其他字符视为无效
                    {
                        throw new InvalidDataException(
                            $"第 {y} 行第 {x} 列包含无效字符 '{c}'");
                    }
                }
            }

            // 返回加载的地图数据
            return map;
        }

        public static int[,] LoadMapData(string filePath)
        {
            // 检查文件是否存在 (
            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException("地图文件不存在", filePath);
            }

            // 读取文件的所有行 
            string[] lines = File.ReadAllLines(filePath);

            // 检查文件是否为空 
            if (lines.Length == 0)
            {
                throw new InvalidDataException("地图文件为空");
            }

            // 确定地图高度 
            int height = lines.Length;
            int width = 0; // 初始化宽度

            // 处理第一行以确定宽度并进行基本验证
            // 移除潜在的空格 
            string firstLineProcessed = lines[0].Replace(" ", "");
            if (string.IsNullOrEmpty(firstLineProcessed))
            {
                throw new InvalidDataException("地图文件的第一行为空或只包含空格"); 
            }
            width = firstLineProcessed.Length; // 将处理后的第一行长度作为宽度 

            // 初始化地图数据数组
            int[,] map = new int[height, width];

            // 遍历所有行来填充地图数据并验证 
            for (int y = 0; y < height; y++)
            {
                // 移除当前行的空格 
                string currentLineProcessed = lines[y].Replace(" ", "");

                // 验证当前行的处理后长度是否与计算出的宽度一致 
                if (currentLineProcessed.Length != width)
                {
                    // 行长度不一致，抛出错误 
                    throw new InvalidDataException(
                        $"地图数据行的长度不一致。预期宽度为 {width}，但第 {y + 1} 行（处理后）的实际宽度为 {currentLineProcessed.Length}。" +
                        $"(Map data rows have inconsistent lengths. Expected width {width}, but line {y + 1} (processed) has actual width {currentLineProcessed.Length}.)");
                }

                // 遍历行中的每个字符 
                for (int x = 0; x < width; x++)
                {
                    char c = currentLineProcessed[x];
                    if (c == '0') // 0 表示可通行区域 
                    {
                        map[y, x] = (int)AreaType.Obstacle;
                    }
                    else if (c == '1') // 1 表示障碍物 
                    {
                        map[y, x] = (int)AreaType.Movable;
                    }
                    else // 其他字符视为无效 
                    {
                        // 发现无效字符，抛出错误 
                        throw new InvalidDataException(
                            $"第 {y + 1} 行第 {x + 1} 列包含无效字符 '{c}'。只允许 '0' 或 '1'。" +
                            $"(Invalid character '{c}' found at row {y + 1}, column {x + 1}. Only '0' or '1' are allowed.)");
                    }
                }
            }

            // 返回加载并验证后的地图数据 
            return map;
        }
        public static int[,] ProcessMap(int[,] mapData, Point doorPosition, Size doorSize)
        {
            // GetLength(0) 返回行数 (Height)
            // GetLength(1) 返回列数 (Width)
            int mapHeight = mapData.GetLength(0);
            int mapWidth = mapData.GetLength(1);

            // --- 定义门的边界 ---
            int doorMinX = doorPosition.X;
            int doorMaxX = doorPosition.X + doorSize.Width;
            int doorMinY = doorPosition.Y;
            int doorMaxY = doorPosition.Y + doorSize.Height;

            for (int i = 300; i < mapHeight; ++i)
            {
                for (int j = 0; j < mapWidth; ++j)
                {
                    // --- 检查当前坐标 (j, i) 是否在门区域内 ---
                    bool withinXBounds = (j >= doorMinX && j < doorMaxX);
                    bool withinYBounds = (i >= doorMinY && i < doorMaxY);

                    // 如果坐标同时在水平和垂直边界内
                    if (withinXBounds && withinYBounds)
                    {
                        // ...将地图值设为 0 (在门内)。
                        mapData[i, j] = (int)AreaType.Movable;
                    }
                    else
                    {
                        // ...否则，将地图值设为 1 (在门外)。
                        mapData[i, j] = (int)AreaType.Obstacle;
                    }
                }
            }

            // 返回修改后的地图数据。
            return mapData;
        }


        /// <summary>
        /// 动态生成地图位图（修改尺寸硬编码问题）
        /// </summary>
        public static Bitmap GenerateMapBitmap(int[,] mapData)
        {
            // 动态获取尺寸
            Height = mapData.GetLength(0);
            Width = mapData.GetLength(1);

            Bitmap bitmap = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
            Rectangle rect = new Rectangle(0, 0, Width, Height);
            BitmapData bmpData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);

            try
            {
                int bytes = Math.Abs(bmpData.Stride) * Height;
                byte[] buffer = new byte[bytes];

                // 预定义颜色值（BGRA格式）
                byte[] white = { 255, 255, 255, 255 };      // 白色
                byte[] grey = { 128, 128, 128, 255 };       // 灰色
                unsafe
                {
                    byte* ptr = (byte*)bmpData.Scan0;
                    for (int y = 0; y < Height; y++)
                    {
                        for (int x = 0; x < Width; x++)
                        {
                            int offset = y * bmpData.Stride + x * 4;
                            //byte[] color = mapData[y, x] == 1 ? grey : white;
                            byte[] color = mapData[y, x] == (int)AreaType.Movable ? white : grey;

                            ptr[offset] = color[0];     // Blue
                            ptr[offset + 1] = color[1]; // Green
                            ptr[offset + 2] = color[2]; // Red
                            ptr[offset + 3] = color[3]; // Alpha
                        }
                    }
                }
            }
            finally
            {
                bitmap.UnlockBits(bmpData);
            }

            return bitmap;
        }

        public static string GenerateInitialMap(
           string mapName,
           int mapWidth,
           int mapHeight,
           PointF doorPosition,   // 门的左上角坐标
           int doorWidth,
           int doorHeight)
        {
            // 参数校验
            if (mapWidth <= 0 || mapHeight <= 0)
                throw new ArgumentException("地图尺寸必须大于0");
            if (doorWidth < 0 || doorHeight < 0)
                throw new ArgumentException("门的尺寸不能为负数");

            // 创建全1地图
            int[][] map = new int[mapHeight][];
            for (int y = 0; y < mapHeight; y++)
            {
                map[y] = new int[mapWidth];
                for (int x = 0; x < mapWidth; x++)
                {
                    map[y][x] = 1;
                }
            }

            // 转换门坐标为整数（向下取整）
            int doorX = (int)Math.Floor(doorPosition.X);
            int doorY = (int)Math.Floor(doorPosition.Y);

            // 计算门区域边界（用Math.Max/Math.Min替代Clamp）
            int yStart = Math.Max(doorY, 0);
            yStart = Math.Min(yStart, mapHeight);       // 不超过最大高度
            int yEnd = Math.Max(doorY + doorHeight, 0);
            yEnd = Math.Min(yEnd, mapHeight);           // 不超过最大高度

            int xStart = Math.Max(doorX, 0);
            xStart = Math.Min(xStart, mapWidth);        // 不超过最大宽度
            int xEnd = Math.Max(doorX + doorWidth, 0);
            xEnd = Math.Min(xEnd, mapWidth);            // 不超过最大宽度

            // 标记门区域
            for (int y = yStart; y < yEnd; y++)
            {
                for (int x = xStart; x < xEnd; x++)
                {
                    map[y][x] = 0;
                }
            }

            // 保存文件
            //SaveMapToFile(mapName, map);
            SaveMapToFile(mapName, map, mapWidth, mapHeight);
            return mapName;
        }
        /// <summary>
        /// 保存地图文件
        /// </summary>
        private static void SaveMapToFile(string mapName, int[][] map, int width, int height)
        {
            string directoryPath = Path.Combine(Directory.GetCurrentDirectory(), "map");
            Directory.CreateDirectory(directoryPath);

            using (StreamWriter writer = new StreamWriter(Path.Combine("map", $"{mapName}.txt")))
            {
                /*
                writer.WriteLine(mapName);
                writer.WriteLine($"{width} {height}"); // 使用动态尺寸
                writer.WriteLine("255");
                */
                foreach (int[] row in map)
                {
                    StringBuilder sb = new StringBuilder(row.Length * 2);
                    foreach (int cell in row)
                    {
                        sb.Append(cell).Append(' ');
                    }
                    if (sb.Length > 0) sb.Length--;
                    writer.WriteLine(sb);
                }
            }
        }
        public static void setMapRectValue(int [,]mapData, RectangleInfo rect, AreaType value)
        {
            int x = rect.x;
            int y = rect.y;
            int width = rect.Width;
            int height = rect.Height;

            int iMax = Math.Min(500, y + height);
            int jMax = Math.Min(1500, x + width);

            for(int i=y; i < iMax; ++i)
            {
                for(int j=x; j < jMax; ++j)
                {
                    mapData[i, j] = (int)value;
                }
            }
        }
    }
}