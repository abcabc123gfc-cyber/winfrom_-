using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TemperatureControl.WinForms.Controls
{
    /// <summary>
    /// 温度显示控件，根据温度值改变颜色
    /// </summary>
    public class TemperatureGauge : Control
    {
        #region 私有字段
        private double _temperature = 25;
        private double _minValue = 0;
        private double _maxValue = 100;
        private string _unit = "℃";
        private bool _showUnit = true;
        private Color _borderColor = Color.FromArgb(80, 80, 80);
        private int _borderWidth = 2;
        private int _cornerRadius = 10;
        #endregion

        #region 公共属性

        /// <summary>
        /// 当前温度值
        /// </summary>
        public double Temperature
        {
            get => _temperature;
            set
            {
                // 限制在范围内
                if (value < _minValue) value = _minValue;
                if (value > _maxValue) value = _maxValue;
                _temperature = value;
                Invalidate(); // 重绘
            }
        }

        /// <summary>
        /// 最小值
        /// </summary>
        public double MinValue
        {
            get => _minValue;
            set
            {
                if (value >= _maxValue) throw new ArgumentException("最小值必须小于最大值");
                _minValue = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 最大值
        /// </summary>
        public double MaxValue
        {
            get => _maxValue;
            set
            {
                if (value <= _minValue) throw new ArgumentException("最大值必须大于最小值");
                _maxValue = value;
                Invalidate();
            }
        }

        /// <summary>
        /// 温度单位
        /// </summary>
        public string Unit
        {
            get => _unit;
            set { _unit = value; Invalidate(); }
        }

        /// <summary>
        /// 是否显示单位
        /// </summary>
        public bool ShowUnit
        {
            get => _showUnit;
            set { _showUnit = value; Invalidate(); }
        }

        /// <summary>
        /// 边框颜色
        /// </summary>
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        /// <summary>
        /// 边框宽度
        /// </summary>
        public int BorderWidth
        {
            get => _borderWidth;
            set { _borderWidth = value; Invalidate(); }
        }

        /// <summary>
        /// 圆角半径
        /// </summary>
        public int CornerRadius
        {
            get => _cornerRadius;
            set { _cornerRadius = value; Invalidate(); }
        }

        #endregion

        public TemperatureGauge()
        {
            // 开启双缓冲，减少闪烁
            SetStyle(ControlStyles.AllPaintingInWmPaint |
                     ControlStyles.UserPaint |
                     ControlStyles.OptimizedDoubleBuffer |
                     ControlStyles.ResizeRedraw, true);

            Size = new Size(120, 120);
            Font = new Font("微软雅黑", 20, FontStyle.Bold);
        }

        #region 颜色计算

        /// <summary>
        /// 根据温度比例（0~1）计算颜色
        /// 蓝 → 青 → 绿 → 黄 → 橙 → 红
        /// </summary>
        private Color GetTemperatureColor(double ratio)
        {
            // 颜色分段：0(蓝) - 0.25(青) - 0.5(绿) - 0.75(黄) - 1(红)
            Color[] colors =
            {
                Color.FromArgb(52, 152, 219),   // 蓝
                Color.FromArgb(26, 188, 156),   // 青
                Color.FromArgb(46, 204, 113),   // 绿
                Color.FromArgb(241, 196, 15),   // 黄
                Color.FromArgb(230, 126, 34),   // 橙
                Color.FromArgb(231, 76, 60)     // 红
            };

            double segment = 1.0 / (colors.Length - 1);
            int index = (int)(ratio / segment);
            if (index >= colors.Length - 1) index = colors.Length - 2;

            double localRatio = (ratio - index * segment) / segment;

            Color c1 = colors[index];
            Color c2 = colors[index + 1];

            // 线性插值
            int r = (int)(c1.R + (c2.R - c1.R) * localRatio);
            int g = (int)(c1.G + (c2.G - c1.G) * localRatio);
            int b = (int)(c1.B + (c2.B - c1.B) * localRatio);

            return Color.FromArgb(
                Math.Max(0, Math.Min(255, r)),
                Math.Max(0, Math.Min(255, g)),
                Math.Max(0, Math.Min(255, b)));
        }

        #endregion

        #region 绘制

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;

            // 1. 计算当前温度的比例
            double ratio = (_temperature - _minValue) / (_maxValue - _minValue);
            ratio = Math.Max(0, Math.Min(1, ratio));

            // 2. 获取对应颜色
            Color mainColor = GetTemperatureColor(ratio);

            // 3. 绘制圆角矩形背景（渐变）
            Rectangle rect = new Rectangle(_borderWidth, _borderWidth,
                                           Width - _borderWidth * 2,
                                           Height - _borderWidth * 2);

            using (GraphicsPath path = GetRoundRectPath(rect, _cornerRadius))
            {
                // 渐变填充：顶部亮，底部暗
                using (LinearGradientBrush brush = new LinearGradientBrush(
                    rect,
                    Color.FromArgb(255, mainColor.R, mainColor.G, mainColor.B),
                    Color.FromArgb(255,
                        (int)(mainColor.R * 0.7),
                        (int)(mainColor.G * 0.7),
                        (int)(mainColor.B * 0.7)),
                    LinearGradientMode.Vertical))
                {
                    g.FillPath(brush, path);
                }

                // 边框
                if (_borderWidth > 0)
                {
                    using (Pen pen = new Pen(_borderColor, _borderWidth))
                    {
                        g.DrawPath(pen, path);
                    }
                }
            }

            // 4. 绘制温度文字
            string text = _temperature.ToString("0.#");
            if (_showUnit) text += _unit;

            using (StringFormat sf = new StringFormat
            {
                Alignment = StringAlignment.Center,
                LineAlignment = StringAlignment.Center
            })
            using (Brush textBrush = new SolidBrush(Color.White))
            {
                g.DrawString(text, Font, textBrush, ClientRectangle, sf);
            }
        }

        /// <summary>
        /// 生成圆角矩形路径
        /// </summary>
        private GraphicsPath GetRoundRectPath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            if (radius <= 0)
            {
                path.AddRectangle(rect);
                return path;
            }

            int diameter = radius * 2;
            path.AddArc(rect.X, rect.Y, diameter, diameter, 180, 90);
            path.AddArc(rect.Right - diameter, rect.Y, diameter, diameter, 270, 90);
            path.AddArc(rect.Right - diameter, rect.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(rect.X, rect.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            return path;
        }

        #endregion

        #region 属性变化时重绘

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Invalidate();
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            Invalidate();
        }

        #endregion
    }
}
