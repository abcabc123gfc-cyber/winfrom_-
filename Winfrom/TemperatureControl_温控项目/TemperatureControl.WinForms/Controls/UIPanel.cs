using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace TemperatureControl.WinForms.Controls
{
    internal class UIPanel : Panel
    {
        private int _radius = 10;
        private int _borderWidth = 1;
        private Color _borderColor = Color.Gray;
        private Color _fillColor = Color.White;

        public UIPanel()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw,
                true);

            BackColor = Color.White;
        }

        [Category("圆角外观"), Description("圆角半径，0 表示直角")]
        public int Radius
        {
            get => _radius;
            set
            {
                _radius = Math.Max(0, value);
                UpdateRegion();
                Invalidate();
            }
        }

        [Category("圆角外观"), Description("边框宽度，0 表示无边框")]
        public int BorderWidth
        {
            get => _borderWidth;
            set
            {
                _borderWidth = Math.Max(0, value);
                Invalidate();
            }
        }

        [Category("圆角外观"), Description("边框颜色")]
        public Color BorderColor
        {
            get => _borderColor;
            set { _borderColor = value; Invalidate(); }
        }

        [Category("圆角外观"), Description("填充颜色")]
        public Color FillColor
        {
            get => _fillColor;
            set { _fillColor = value; Invalidate(); }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateRegion();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateRegion();
        }

        /// <summary>
        /// 把控件外形裁剪成圆角矩形 —— 这一步是"不留空白"的关键
        /// </summary>
        private void UpdateRegion()
        {
            if (Width <= 0 || Height <= 0) return;

            int r = Math.Min(_radius, Math.Min(Width, Height) / 2);
            using (var path = GetRoundedPath(new RectangleF(0, 0, Width, Height), r))
            {
                var old = Region;
                Region = new Region(path);
                old?.Dispose();
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            
            var g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int w = Width, h = Height;
            int r = Math.Min(_radius, Math.Min(w, h) / 2);

            // 1) 填充整个圆角矩形
            using (var path = GetRoundedPath(new RectangleF(0, 0, w, h), r))
            using (var brush = new SolidBrush(_fillColor))
            {
                g.FillPath(brush, path);
            }

            // 2) 画边框：把路径向内缩 BorderWidth/2，画笔居中描边后正好贴在内侧
            if (_borderWidth > 0 && _borderColor.A > 0)
            {
                float half = _borderWidth / 2f;
                var borderRect = new RectangleF(
                    half, half,
                    w - _borderWidth, h - _borderWidth);
                float borderR = Math.Max(0, r - half);

                using (var path = GetRoundedPath(borderRect, borderR))
                using (var pen = new Pen(_borderColor, _borderWidth))
                {
                    pen.Alignment = PenAlignment.Center;
                    g.DrawPath(pen, path);
                }
            }
        }

        private static GraphicsPath GetRoundedPath(RectangleF rect, float radius)
        {
            var path = new GraphicsPath();

            if (radius <= 0.1f)
            {
                path.AddRectangle(rect);
                return path;
            }

            float d = radius * 2f;
            if (d > rect.Width) d = rect.Width;
            if (d > rect.Height) d = rect.Height;

            path.AddArc(rect.X, rect.Y, d, d, 180, 90);
            path.AddArc(rect.Right - d, rect.Y, d, d, 270, 90);
            path.AddArc(rect.Right - d, rect.Bottom - d, d, d, 0, 90);
            path.AddArc(rect.X, rect.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }
    }
}
