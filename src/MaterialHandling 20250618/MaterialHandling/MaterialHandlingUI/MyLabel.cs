#region 圆形标签类
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

public class CircleLabel : Label // 继承标签类
{
    private int _borderWidth = 2; // 圆形边框的宽度
    private Color _borderColor = Color.Black; // 圆形边框的颜色

    public CircleLabel()
    {
        // 设置默认属性
        this.BackColor = Color.FromArgb(255, 246, 246, 4); // 背景颜色
        this.TextAlign = ContentAlignment.MiddleCenter; // 文本居中
        this.AutoSize = false; // 禁用自动调整大小
        this.Font = new Font("宋体", 18, FontStyle.Bold); // 字体
    }

    /// <summary>
    /// 获取或设置圆形边框的宽度。
    /// </summary>
    public int BorderWidth
    {
        get { return _borderWidth; }
        set
        {
            _borderWidth = value;
            this.Invalidate(); // 触发重绘
        }
    }

    /// <summary>
    /// 获取或设置圆形边框的颜色。
    /// </summary>
    public Color BorderColor
    {
        get { return _borderColor; }
        set
        {
            _borderColor = value;
            this.Invalidate(); // 触发重绘
        }
    }

    protected override void OnPaint(PaintEventArgs e) // 重写绘制方法
    {
        base.OnPaint(e); // 调用基类的绘制方法

        // 启用抗锯齿
        e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

        // 绘制圆形背景
        using (Brush brush = new SolidBrush(this.BackColor))
        {
            e.Graphics.FillEllipse(brush, 2, 2, this.Width - 6, this.Height - 6);
        }

        // 绘制圆形边框
        using (Pen pen = new Pen(_borderColor, _borderWidth))
        {
            e.Graphics.DrawEllipse(pen, 2, 2, this.Width - 6, this.Height - 6);
        }

        // 绘制文本
        using (Brush textBrush = new SolidBrush(this.ForeColor))
        {
            StringFormat format = new StringFormat
            {
                Alignment = StringAlignment.Center, // 水平居中
                LineAlignment = StringAlignment.Center // 垂直居中
            };

            e.Graphics.DrawString(this.Text, this.Font, textBrush, new RectangleF(2, 2, this.Width - 6, this.Height - 6), format);
        }

        // 设置控件的区域为圆形
        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddEllipse(2, 2, this.Width - 6, this.Height - 6);
            this.Region = new Region(path);
        }
    }

    protected override void OnResize(EventArgs e)
    {
        base.OnResize(e);
        // 强制控件为正方形，保持圆形形状
        this.Width = this.Height;
    }
}
#endregion