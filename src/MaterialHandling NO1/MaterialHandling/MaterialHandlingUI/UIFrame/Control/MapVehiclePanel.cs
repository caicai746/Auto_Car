using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialHandling.MaterialHandlingMAP;
using MaterialHandling.MaterialHandlingUI.UIFrame.CAN;
namespace MaterialHandling.MaterialHandlingUI.UIFrame.Control
{
    public partial class MapVehiclePanel : UserControl
    {
        // 地图
        private Bitmap _mapImage;
        public PointF originPoint;
        public MapZoomController _zoomController { get; private set; }  
        private int[,] _currentMapData;                                  // 保存当前地图数据

        // 门
        private Point _doorPoint = new Point(900,160);                   // 开门位置
        private Size _doorSize;                                          // 开门大小

        // 小车
        private PointF lidarPosition;                                    // 雷达坐标
        public PointF carPosition { get; private set; }
        public float carTheta { get; private set; }                      // 当前角度
        private Image _carImage;                                         // 小车图片
        private Size _carSize;                                           // 小车尺寸

        // 鼠标手动框选
        private Point _selectionStartPoint;                              // 鼠标按下时的起始点
        private Rectangle _currentSelectionRectangle;                    // 当前拖拽形成的矩形
        private bool _isSelecting = false;                               // 标记是否正在进行拖拽选择
        private Rectangle _finalSelectionRectangle;                      // (可选) 存储最终确定的矩形，如果需要在释放后继续显示
        private bool _selectionMade = false;                             // (可选) 标记是否已完成一次选择

        // 重绘参数
        private bool isRedrawMatrixRect = false;
        public bool isRedrawC1_targetPoint = false;

        // C1 计算的目标点
        public Point C1_targetPoint { get; set; }


        private float safeDistance = 5f; // 假设安全距离为 5 像素
        public MapVehiclePanel()
        {
            InitializeComponent();
            
            // 加载小车图片
            _carImage = Image.FromFile("car.png");
            // 地图相关
            originPoint = new PointF(1500 - 900, 340);
            InitializeComponents();

            this.Load += MapVehiclePanel_Load; // 将初始化移到 Load 事件  
        }
        private void MapVehiclePanel_Load(object sender, EventArgs e)
        {
            try
            {
                LoadMapAndCar("map_data.txt");  // 修改初始化方法
    
            }
            catch (Exception ex)
            {
                MessageBox.Show($"VehicleControlFrame初始化失败: {ex.Message}");
            }
        }
        // -- 地图相关

        private void InitializeComponents()
        {
            // 事件绑定
            pictureBoxMap.Paint += PictureBoxMap_Paint;
            //pictureBoxMap.MouseWheel += PictureBoxMap_MouseWheel;
            pictureBoxMap.MouseDown += PictureBoxMap_MouseDown;
            pictureBoxMap.MouseMove += PictureBoxMap_MouseMove;
            pictureBoxMap.MouseUp += PictureBoxMap_MouseUp;

            //
            pictureBoxMap.Size = new Size(pictureBoxMap.ClientSize.Width, pictureBoxMap.ClientSize.Height);
            // 初始化缩放控制器
            _zoomController = new MapZoomController(
                //new Size(1500, 500),
                new Size(pictureBoxMap.ClientSize.Width, pictureBoxMap.ClientSize.Height),
                pictureBoxMap.ClientSize
            );

            _zoomController.ViewChanged += (s, e) => pictureBoxMap.Invalidate();

            // 设置用户坐标系原点为如 
            //PointF orignPoint = new PointF(rosClient.xP, );
            //_zoomController.UserOrigin = originPoint;
            SetUserOrigin(originPoint);
        }


        // 加载 地图 和 小车
        private void LoadMapAndCar(string fileName)
        {
            try
            {
                // 加载地图
                //var mapData = MapUtil.LoadMapData("test_map.txt");
                //var mapData = MapUtil.LoadMapData("map//testInit.txt");
                _currentMapData = MapUtil.LoadMapData("map//" + fileName);
                //_currentMapData = MapUtil.ProcessMap(_currentMapData, _doorPoint, _doorSize);
                _mapImage = MapUtil.GenerateMapBitmap(_currentMapData);

                pictureBoxMap.Invalidate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载地图初始化失败：{ex.Message}");
            }
        }

        public void Invalidate_pictureBoxMap()
        {
            pictureBoxMap.Invalidate();
        }
        // 设置坐标原点x
        public void SetUserOrigin(PointF origin)
        {
            _zoomController.UserOrigin = origin;
            //SetCarInitialPosition(0); // 重置小车到新原点的 (0,0)
            pictureBoxMap.Invalidate();
        }
        private void PictureBoxMap_Paint(object sender, PaintEventArgs e)
        {

            //Console.WriteLine("pictureBoxMap_Paint");
            e.Graphics.Clear(pictureBoxMap.BackColor);
            // 绘制地图
            if (_mapImage != null)
            {
                {
                    e.Graphics.Clear(pictureBoxMap.BackColor);
                    _zoomController.DrawMap(e.Graphics, _mapImage);
                }
            }

            // 绘制小车
            if (_carImage != null)
            {
                // 调用 drawCar 方法绘制汽车
                if(this.cb_car_visiable.Checked)
                {
                    _zoomController.DrawCar(e.Graphics, _carImage, lidarPosition, carTheta);
                }
            }
            if(isRedrawMatrixRect)
            {
                _zoomController.DrawMatrixRect(e.Graphics, MatrixStrategy.curRect);
                //isRedrawMatrixRect = false;
            }
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias; // 使绘制更平滑

            // 如果正在拖拽选择，并且矩形有效，则绘制预览框
            if (_isSelecting && _currentSelectionRectangle.Width > 0 && _currentSelectionRectangle.Height > 0)
            {
                // 使用虚线画笔来表示正在选择的“橡皮筋”效果
                using (Pen selectionPen = new Pen(Color.Red, 1)) // 红色，1像素宽
                {
                    selectionPen.DashStyle = DashStyle.Dash; // 设置为虚线
                    g.DrawRectangle(selectionPen, _currentSelectionRectangle);
                }
                //(可选)绘制一个半透明的填充，让选区更明显
                using (SolidBrush selectionFill = new SolidBrush(Color.FromArgb(50, Color.LightBlue))) // 50/255 透明度的浅蓝色
                {
                    g.FillRectangle(selectionFill, _currentSelectionRectangle);
                }
            }

            // 绘制目标点（调试用）
            using (SolidBrush brush = new SolidBrush(Color.Black)) // 50/255 透明度的浅蓝色
            {
                var scP = _zoomController.MapToScreen(MoveInMatrix.endPosition);
                var rect = new Rectangle(scP.X, scP.Y, 5, 5);
                g.FillRectangle( brush, rect);
            }

            if(isRedrawC1_targetPoint)
            {
                using (SolidBrush brush = new SolidBrush(Color.Black)) 
                {
                    var scP = _zoomController.MapToScreen(C1_targetPoint);
                    var rect = new Rectangle(scP.X, scP.Y, 5, 5);
                    g.FillRectangle(brush, rect);
                }
                // 
                //isRedrawC1_targetPoint = false;
            }
            

            //// (可选) 如果希望在鼠标释放后仍然显示最终选择的矩形
            //else if (_selectionMade && _finalSelectionRectangle.Width > 0 && _finalSelectionRectangle.Height > 0)
            //{
            //    using (Pen finalizedPen = new Pen(Color.Blue, 2)) // 蓝色，2像素宽的实线
            //    {
            //        g.DrawRectangle(finalizedPen, _finalSelectionRectangle);
            //    }
            //}
        }


        private void PictureBoxMap_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //_zoomController.HandleMouseDown(e.Location);
                _isSelecting = true;
                _selectionMade = false; // 重置选择完成标记
                _selectionStartPoint = e.Location; // 记录起始点 (相对于 pictureBoxMap 的坐标)

                // 初始化矩形为一个点，或者一个空矩形
                // 当鼠标移动时，这个矩形会被更新
                _currentSelectionRectangle = new Rectangle(e.Location, Size.Empty);

                // (可选) 如果您希望在按下鼠标时立即开始绘制，可以调用 Invalidate
                // 但通常 MouseMove 中的 Invalidate 已经足够
                // pictureBoxMap.Invalidate();
            }
        }

        private void PictureBoxMap_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                //_zoomController.HandleMouseMove(e.Location);

            }
            // 只有在正在选择时（即鼠标左键已按下且未释放）才更新矩形
            if (_isSelecting)
            {
                Point currentPoint = e.Location;

                // 根据起始点和当前点计算矩形的左上角和尺寸
                // Math.Min 和 Math.Abs 用于处理从任意方向拖拽的情况
                int x = Math.Min(_selectionStartPoint.X, currentPoint.X);
                int y = Math.Min(_selectionStartPoint.Y, currentPoint.Y);
                int width = Math.Abs(_selectionStartPoint.X - currentPoint.X);
                int height = Math.Abs(_selectionStartPoint.Y - currentPoint.Y);

                _currentSelectionRectangle = new Rectangle(x, y, width, height);

                // 请求 pictureBoxMap 重绘以显示更新后的选择框
                pictureBoxMap.Invalidate();
            }
        }
        private void PictureBoxMap_MouseUp(object sender, MouseEventArgs e)
        {
            _zoomController.HandleMouseUp();
            // 仅当释放鼠标左键且之前正在选择时结束选择
            if (e.Button == MouseButtons.Left && _isSelecting)
            {
                _isSelecting = false; // 标记选择操作结束

                // 此时 _currentSelectionRectangle 中存储的就是最终选择的矩形
                // 确保矩形有实际大小 (用户可能只是点击而没有拖拽)
                if (_currentSelectionRectangle.Width > 0 && _currentSelectionRectangle.Height > 0)
                {
                    _finalSelectionRectangle = _currentSelectionRectangle; // 保存最终的矩形
                    _selectionMade = true;

                    // --- 在这里处理您获取到的矩形 ---
                    Console.WriteLine($"最终选择矩形: 左上角({_finalSelectionRectangle.Left}, {_finalSelectionRectangle.Top}), " +
                                      $"右下角({_finalSelectionRectangle.Right}, {_finalSelectionRectangle.Bottom}), " +
                                      $"宽度={_finalSelectionRectangle.Width}, 高度={_finalSelectionRectangle.Height}");

                    // 例如，您可以触发一个事件，将选择的矩形传递出去
                    // OnRectangleSelected?.Invoke(this, _finalSelectionRectangle);
                    // --- 处理结束 ---
                }
                else
                {
                    // 用户只是点击，没有拖拽出有效矩形
                    _selectionMade = false; // 没有有效选择
                    Console.WriteLine("选择的矩形无效 (宽度或高度为0)。");
                }

                // 请求 pictureBoxMap 重绘
                // 如果您希望选择框在释放鼠标后消失，并且 Paint 方法只在 _isSelecting 为 true 时绘制，
                // 这次 Invalidate 会清除它。
                // 如果您希望最终选择的矩形保留，Paint 方法需要相应逻辑。

                MatrixStrategy.curRect.x = _finalSelectionRectangle.X;
                MatrixStrategy.curRect.y = _finalSelectionRectangle.Y;
                MatrixStrategy.curRect.Width = _finalSelectionRectangle.Width;
                MatrixStrategy.curRect.Height = _finalSelectionRectangle.Height;
                isRedrawMatrixRect = true;
                pictureBoxMap.Invalidate();
            }

        }

        /*
        private void comboBox_map_SelectedIndexChanged(object sender, EventArgs e)
        {
            string file = string.Format("matrix_output{0}.txt", this.comboBox_map.SelectedItem.ToString());
            LoadMapAndCar(file);
            pictureBoxMap.Invalidate();
        }
        */

        private void button1_Click(object sender, EventArgs e)
        {
            LoadMapAndCar("map_data.txt"); 
        }


        private void pictureBoxMap_Click(object sender, EventArgs e)
        {

        }

        public void UpdateCarPosition(PointF newPosition)
        {
            if (lidarPosition != newPosition)
            {
                lidarPosition = newPosition;
                if (pictureBoxMap != null && pictureBoxMap.IsHandleCreated)
                {
                    pictureBoxMap.Invalidate();
                }
            }
        }
        public void UpdateCarPosture(PointF newPosition, float newAngle)
        {

            var primePoint = _zoomController.TransLidaToCarLeft(newPosition, newAngle);
            int xPrime = primePoint.X;
            int yPrime = primePoint.Y;

            //int xPrime = (int)(newPosition.X - _zoomController.calibrationValue * Math.Cos(newAngle));
            //int yPrime = (int)(newPosition.Y - _zoomController.calibrationValue * Math.Sin(newAngle));


            carPosition = new PointF(xPrime, yPrime);

            textBox1.Text = String.Format("转换前 ： [{0}, {1}]", newPosition.X, newPosition.Y);
            textBox_old_x.Text = newPosition.X.ToString("F3");
            textBox_old_y.Text = newPosition.Y.ToString("F3");
            textBox_old_theta.Text = newAngle.ToString("F2");
            textBox2.Text = String.Format("转换后 ： [{0}, {1}], {2}°", xPrime, yPrime, MapZoomController.RadiansToDegrees(newAngle));
            textBox_new_x.Text = xPrime.ToString("F3");
            textBox_new_y.Text = yPrime.ToString("F3");
            textBox_new_theta.Text = MapZoomController.RadiansToDegrees(newAngle).ToString("F2");

            if (lidarPosition != newPosition || newAngle != carTheta)
            {
                lidarPosition = newPosition;
                carTheta = newAngle;

                if (pictureBoxMap != null && pictureBoxMap.IsHandleCreated)
                {
                    pictureBoxMap.Invalidate();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
     
            double v = Convert.ToDouble(tb_calibrationValue.Text);
            _zoomController.calibrationValue = v;
        }

        private void btn_init_rect_Click(object sender, EventArgs e)
        {
            var p = _zoomController.MapToScreen(new Point((int)carPosition.X, (int)carPosition.Y));
            MatrixStrategy.curRect.y = (int)(p.Y - 90);
            MatrixStrategy.curRect.x = (int)(p.X - 100);
            MatrixStrategy.curRect.Height = 180;
            MatrixStrategy.curRect.Width = 200;
            isRedrawMatrixRect = true;

            MapUtil.setMapRectValue(_currentMapData, MatrixStrategy.curRect, AreaType.Movable);
            _mapImage = MapUtil.GenerateMapBitmap(_currentMapData);

            pictureBoxMap.Invalidate();
        }

        private void comboBox_map_SelectedValueChanged(object sender, EventArgs e)
        {
            string file = string.Format(".//MAP55//map_data{0}.txt", this.comboBox_map.SelectedItem.ToString());
            LoadMapAndCar(file);
            isRedrawMatrixRect = true;
            pictureBoxMap.Invalidate();
        }

        private void btn_next_rect_Click(object sender, EventArgs e)
        {
            RectangleInfo newRect = MatrixStrategy.FindNextPreferredRect(_currentMapData, MatrixStrategy.curRect, 180, 150);
            if(newRect.Height != 0 && newRect.Width != 0)
            {
                MatrixStrategy.curRect = newRect;
                isRedrawMatrixRect = true;
                //_mapImage = MapUtil.GenerateMapBitmap(_currentMapData);
                pictureBoxMap.Invalidate();
            }
        }

        private void cb_car_visiable_CheckedChanged(object sender, EventArgs e)
        {
            pictureBoxMap.Invalidate();
        }
        MoveInMatrix moveInMatrix;
        VCUMotionCalculate vCUMotionCalculate;
        public double destinationX, destinationY, destinationTheta;
        MessagePanel messagePanel;
        private async void bt_StartMove_Click(object sender, EventArgs e)
        {
            try
            {
                // 禁用按钮，防止重复点击
                bt_StartMove.Enabled = false;
                if (ck_CameraDistance.Checked)
                {
                    messagePanel = MainFrame.cf.messagePanel;
                    destinationX = messagePanel.targetx;
                    destinationY = messagePanel.targety;
                    destinationTheta = 0;
                    vCUMotionCalculate = new VCUMotionCalculate();
                    vCUMotionCalculate.Calculate(destinationX, destinationY, destinationTheta);//调用计算运动控制逻辑
                }
                else
                {
                    //实例化在矩形框中移动
                    moveInMatrix = new MoveInMatrix();
                    if (cb_IsMoveToTopLeft.Checked) moveInMatrix.IsMoveToTopLeft = true;//是否移动到左偏上位置的标志位
                                                                                        //获取相对右上角的距离
                    moveInMatrix.distanceToRight = int.Parse(tb_DistanceToRight.Text);
                    moveInMatrix.distanceToTop = int.Parse(tb_DistanceToTop.Text);
                    //矩形框中移动策略
                    await moveInMatrix.MovementLogicAsync();
                    // 运动完成后的处理
                    MessageBox.Show("运动完成！");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"运动过程中发生错误：{ex.Message}");
            }
            finally
            {
                // 重新启用按钮
                bt_StartMove.Enabled = true;
            }
        }

        private void bt_StopMotion_Click(object sender, EventArgs e)
        {
            bt_StartMove.Enabled = true;
            if (ck_CameraDistance.Checked)
            {
                vCUMotionCalculate?.StopMotion();
            }
            else
            {
                moveInMatrix.vCUMotionCalculate.InterruptMotion();
            }
            
           
        }
    }
}

