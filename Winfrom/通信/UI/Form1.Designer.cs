namespace UI
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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnConn = new System.Windows.Forms.Button();
            this.cbbParity = new System.Windows.Forms.ComboBox();
            this.cbbStopBit = new System.Windows.Forms.ComboBox();
            this.cbbDataBit = new System.Windows.Forms.ComboBox();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.cbbPortName = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnRead = new System.Windows.Forms.Button();
            this.cbRealTimeRead = new System.Windows.Forms.CheckBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.uiLineChart1 = new Sunny.UI.UILineChart();
            this.uiPieChart1 = new Sunny.UI.UIPieChart();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.uiDoughnutChart1 = new Sunny.UI.UIDoughnutChart();
            this.uiBarChart1 = new Sunny.UI.UIBarChart();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnConn);
            this.groupBox1.Controls.Add(this.cbbParity);
            this.groupBox1.Controls.Add(this.cbbStopBit);
            this.groupBox1.Controls.Add(this.cbbDataBit);
            this.groupBox1.Controls.Add(this.cbbBaudRate);
            this.groupBox1.Controls.Add(this.cbbPortName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(22, 13);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(473, 381);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接和关闭";
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(169, 309);
            this.btnConn.Margin = new System.Windows.Forms.Padding(4);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(133, 53);
            this.btnConn.TabIndex = 2;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // cbbParity
            // 
            this.cbbParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbParity.FormattingEnabled = true;
            this.cbbParity.Items.AddRange(new object[] {
            "None",
            "Odd",
            "Even",
            "Mark",
            "Space"});
            this.cbbParity.Location = new System.Drawing.Point(225, 248);
            this.cbbParity.Margin = new System.Windows.Forms.Padding(4);
            this.cbbParity.Name = "cbbParity";
            this.cbbParity.Size = new System.Drawing.Size(160, 32);
            this.cbbParity.TabIndex = 1;
            // 
            // cbbStopBit
            // 
            this.cbbStopBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbStopBit.FormattingEnabled = true;
            this.cbbStopBit.Items.AddRange(new object[] {
            "None",
            "One",
            "Two",
            "OnePointFive"});
            this.cbbStopBit.Location = new System.Drawing.Point(225, 197);
            this.cbbStopBit.Margin = new System.Windows.Forms.Padding(4);
            this.cbbStopBit.Name = "cbbStopBit";
            this.cbbStopBit.Size = new System.Drawing.Size(160, 32);
            this.cbbStopBit.TabIndex = 1;
            // 
            // cbbDataBit
            // 
            this.cbbDataBit.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbDataBit.FormattingEnabled = true;
            this.cbbDataBit.Items.AddRange(new object[] {
            "8",
            "7"});
            this.cbbDataBit.Location = new System.Drawing.Point(225, 147);
            this.cbbDataBit.Margin = new System.Windows.Forms.Padding(4);
            this.cbbDataBit.Name = "cbbDataBit";
            this.cbbDataBit.Size = new System.Drawing.Size(160, 32);
            this.cbbDataBit.TabIndex = 1;
            // 
            // cbbBaudRate
            // 
            this.cbbBaudRate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbBaudRate.FormattingEnabled = true;
            this.cbbBaudRate.Items.AddRange(new object[] {
            "4800",
            "9600",
            "19200"});
            this.cbbBaudRate.Location = new System.Drawing.Point(225, 96);
            this.cbbBaudRate.Margin = new System.Windows.Forms.Padding(4);
            this.cbbBaudRate.Name = "cbbBaudRate";
            this.cbbBaudRate.Size = new System.Drawing.Size(160, 32);
            this.cbbBaudRate.TabIndex = 1;
            // 
            // cbbPortName
            // 
            this.cbbPortName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbbPortName.FormattingEnabled = true;
            this.cbbPortName.Location = new System.Drawing.Point(225, 45);
            this.cbbPortName.Margin = new System.Windows.Forms.Padding(4);
            this.cbbPortName.Name = "cbbPortName";
            this.cbbPortName.Size = new System.Drawing.Size(160, 32);
            this.cbbPortName.TabIndex = 1;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(111, 253);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 24);
            this.label5.TabIndex = 0;
            this.label5.Text = "校验位：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 203);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(106, 24);
            this.label4.TabIndex = 0;
            this.label4.Text = "停止位：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(111, 152);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "数据位：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(111, 101);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(106, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "波特率：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(87, 51);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口名称：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.btnRead);
            this.groupBox2.Controls.Add(this.cbRealTimeRead);
            this.groupBox2.Location = new System.Drawing.Point(32, 419);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(473, 329);
            this.groupBox2.TabIndex = 19;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读取数据";
            this.groupBox2.Enter += new System.EventHandler(this.groupBox2_Enter);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(190, 223);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(117, 53);
            this.button3.TabIndex = 4;
            this.button3.Text = "读取饼状图";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(30, 223);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 53);
            this.button2.TabIndex = 4;
            this.button2.Text = "读取饼状图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(190, 150);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(117, 53);
            this.button1.TabIndex = 4;
            this.button1.Text = "读取柱状图";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(30, 150);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(133, 53);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "读取线表";
            this.btnRead.UseVisualStyleBackColor = true;
            this.btnRead.Click += new System.EventHandler(this.btnRead_Click);
            // 
            // cbRealTimeRead
            // 
            this.cbRealTimeRead.AutoSize = true;
            this.cbRealTimeRead.Location = new System.Drawing.Point(169, 103);
            this.cbRealTimeRead.Margin = new System.Windows.Forms.Padding(4);
            this.cbRealTimeRead.Name = "cbRealTimeRead";
            this.cbRealTimeRead.Size = new System.Drawing.Size(138, 28);
            this.cbRealTimeRead.TabIndex = 3;
            this.cbRealTimeRead.Text = "实时读取";
            this.cbRealTimeRead.UseVisualStyleBackColor = true;
            this.cbRealTimeRead.CheckedChanged += new System.EventHandler(this.cbRealTimeRead_CheckedChanged);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // uiLineChart1
            // 
            this.uiLineChart1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLineChart1.LegendFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLineChart1.Location = new System.Drawing.Point(531, 29);
            this.uiLineChart1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiLineChart1.MouseDownType = Sunny.UI.UILineChartMouseDownType.Zoom;
            this.uiLineChart1.Name = "uiLineChart1";
            this.uiLineChart1.Size = new System.Drawing.Size(571, 521);
            this.uiLineChart1.SubFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiLineChart1.TabIndex = 21;
            this.uiLineChart1.Text = "uiLineChart1";
            // 
            // uiPieChart1
            // 
            this.uiPieChart1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPieChart1.LegendFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPieChart1.Location = new System.Drawing.Point(1149, 29);
            this.uiPieChart1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiPieChart1.Name = "uiPieChart1";
            this.uiPieChart1.Size = new System.Drawing.Size(520, 296);
            this.uiPieChart1.SubFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiPieChart1.TabIndex = 24;
            this.uiPieChart1.Text = "uiPieChart1";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1188, 371);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(451, 179);
            this.richTextBox1.TabIndex = 4;
            this.richTextBox1.Text = "";
            // 
            // uiDoughnutChart1
            // 
            this.uiDoughnutChart1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDoughnutChart1.LegendFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDoughnutChart1.Location = new System.Drawing.Point(1188, 569);
            this.uiDoughnutChart1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiDoughnutChart1.Name = "uiDoughnutChart1";
            this.uiDoughnutChart1.Size = new System.Drawing.Size(585, 409);
            this.uiDoughnutChart1.SubFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiDoughnutChart1.TabIndex = 25;
            this.uiDoughnutChart1.Text = "uiDoughnutChart1";
            // 
            // uiBarChart1
            // 
            this.uiBarChart1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiBarChart1.LegendFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiBarChart1.Location = new System.Drawing.Point(531, 569);
            this.uiBarChart1.MinimumSize = new System.Drawing.Size(1, 1);
            this.uiBarChart1.Name = "uiBarChart1";
            this.uiBarChart1.Size = new System.Drawing.Size(580, 373);
            this.uiBarChart1.SubFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uiBarChart1.TabIndex = 26;
            this.uiBarChart1.Text = "uiBarChart1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1832, 1025);
            this.Controls.Add(this.uiBarChart1);
            this.Controls.Add(this.uiDoughnutChart1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.uiPieChart1);
            this.Controls.Add(this.uiLineChart1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.ComboBox cbbParity;
        private System.Windows.Forms.ComboBox cbbStopBit;
        private System.Windows.Forms.ComboBox cbbDataBit;
        private System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.ComboBox cbbPortName;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.CheckBox cbRealTimeRead;
        private System.IO.Ports.SerialPort serialPort1;
        private Sunny.UI.UILineChart uiLineChart1;
        private Sunny.UI.UIPieChart uiPieChart1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button button1;
        private Sunny.UI.UIDoughnutChart uiDoughnutChart1;
        private Sunny.UI.UIBarChart uiBarChart1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
    }
}

