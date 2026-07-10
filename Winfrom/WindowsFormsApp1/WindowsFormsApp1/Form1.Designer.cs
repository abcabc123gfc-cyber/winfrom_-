namespace WindowsFormsApp1
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.battery1 = new AntdUI.Battery();
            this.input1 = new AntdUI.Input();
            this.elementHost1 = new System.Windows.Forms.Integration.ElementHost();
            this.angularGauge1 = new LiveCharts.Wpf.AngularGauge();
            this.elementHost2 = new System.Windows.Forms.Integration.ElementHost();
            this.cartesianChart1 = new LiveCharts.Wpf.CartesianChart();
            this.elementHost3 = new System.Windows.Forms.Integration.ElementHost();
            this.defaultGeoMapTooltip1 = new LiveCharts.Wpf.DefaultGeoMapTooltip();
            this.elementHost4 = new System.Windows.Forms.Integration.ElementHost();
            this.pieChart1 = new LiveCharts.Wpf.PieChart();
            this.pieChart2 = new LiveCharts.WinForms.PieChart();
            this.SuspendLayout();
            // 
            // battery1
            // 
            this.battery1.Location = new System.Drawing.Point(774, 21);
            this.battery1.Name = "battery1";
            this.battery1.Size = new System.Drawing.Size(331, 174);
            this.battery1.TabIndex = 0;
            this.battery1.Text = "battery1";
            // 
            // input1
            // 
            this.input1.Location = new System.Drawing.Point(1211, 256);
            this.input1.Name = "input1";
            this.input1.Size = new System.Drawing.Size(105, 283);
            this.input1.TabIndex = 1;
            this.input1.Text = "input1";
            // 
            // elementHost1
            // 
            this.elementHost1.Location = new System.Drawing.Point(12, 12);
            this.elementHost1.Name = "elementHost1";
            this.elementHost1.Size = new System.Drawing.Size(791, 389);
            this.elementHost1.TabIndex = 2;
            this.elementHost1.Text = "elementHost1";
            this.elementHost1.Child = this.angularGauge1;
            // 
            // elementHost2
            // 
            this.elementHost2.Location = new System.Drawing.Point(31, 440);
            this.elementHost2.Name = "elementHost2";
            this.elementHost2.Size = new System.Drawing.Size(761, 308);
            this.elementHost2.TabIndex = 3;
            this.elementHost2.Text = "elementHost2";
            this.elementHost2.Child = this.cartesianChart1;
            // 
            // elementHost3
            // 
            this.elementHost3.Location = new System.Drawing.Point(867, 559);
            this.elementHost3.Name = "elementHost3";
            this.elementHost3.Size = new System.Drawing.Size(665, 116);
            this.elementHost3.TabIndex = 4;
            this.elementHost3.Text = "elementHost3";
            this.elementHost3.Child = this.defaultGeoMapTooltip1;
            // 
            // elementHost4
            // 
            this.elementHost4.Location = new System.Drawing.Point(1372, 256);
            this.elementHost4.Name = "elementHost4";
            this.elementHost4.Size = new System.Drawing.Size(412, 231);
            this.elementHost4.TabIndex = 5;
            this.elementHost4.Text = "elementHost4";
            this.elementHost4.Child = this.pieChart1;
            // 
            // pieChart2
            // 
            this.pieChart2.Location = new System.Drawing.Point(883, 720);
            this.pieChart2.Name = "pieChart2";
            this.pieChart2.Size = new System.Drawing.Size(451, 171);
            this.pieChart2.TabIndex = 6;
            this.pieChart2.Text = "pieChart2";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1892, 939);
            this.Controls.Add(this.pieChart2);
            this.Controls.Add(this.elementHost4);
            this.Controls.Add(this.elementHost3);
            this.Controls.Add(this.elementHost2);
            this.Controls.Add(this.elementHost1);
            this.Controls.Add(this.input1);
            this.Controls.Add(this.battery1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private AntdUI.Battery battery1;
        private AntdUI.Input input1;
        private System.Windows.Forms.Integration.ElementHost elementHost1;
        private LiveCharts.Wpf.AngularGauge angularGauge1;
        private System.Windows.Forms.Integration.ElementHost elementHost2;
        private LiveCharts.Wpf.CartesianChart cartesianChart1;
        private System.Windows.Forms.Integration.ElementHost elementHost3;
        private LiveCharts.Wpf.DefaultGeoMapTooltip defaultGeoMapTooltip1;
        private System.Windows.Forms.Integration.ElementHost elementHost4;
        private LiveCharts.Wpf.PieChart pieChart1;
        private LiveCharts.WinForms.PieChart pieChart2;
    }
}

