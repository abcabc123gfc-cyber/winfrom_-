namespace modbus
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
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.txtWDZ = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnConn = new System.Windows.Forms.Button();
            this.cbbParity = new System.Windows.Forms.ComboBox();
            this.cbbStopBit = new System.Windows.Forms.ComboBox();
            this.cbbDataBit = new System.Windows.Forms.ComboBox();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.txtPH = new System.Windows.Forms.TextBox();
            this.cbbPortName = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDDL = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.txtHSL = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.serialPort1 = new System.IO.Ports.SerialPort(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.btnWrite = new System.Windows.Forms.Button();
            this.txtData = new System.Windows.Forms.TextBox();
            this.txtStartAddress = new System.Windows.Forms.TextBox();
            this.txtSlaveID = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.btnRead = new System.Windows.Forms.Button();
            this.cbRealTimeRead = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // txtWDZ
            // 
            this.txtWDZ.Location = new System.Drawing.Point(187, 95);
            this.txtWDZ.Margin = new System.Windows.Forms.Padding(4);
            this.txtWDZ.Name = "txtWDZ";
            this.txtWDZ.Size = new System.Drawing.Size(219, 35);
            this.txtWDZ.TabIndex = 4;
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            // txtPH
            // 
            this.txtPH.Location = new System.Drawing.Point(187, 196);
            this.txtPH.Margin = new System.Windows.Forms.Padding(4);
            this.txtPH.Name = "txtPH";
            this.txtPH.Size = new System.Drawing.Size(219, 35);
            this.txtPH.TabIndex = 4;
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
            // txtDDL
            // 
            this.txtDDL.Location = new System.Drawing.Point(187, 145);
            this.txtDDL.Margin = new System.Windows.Forms.Padding(4);
            this.txtDDL.Name = "txtDDL";
            this.txtDDL.Size = new System.Drawing.Size(219, 35);
            this.txtDDL.TabIndex = 4;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.txtPH);
            this.groupBox3.Controls.Add(this.txtDDL);
            this.groupBox3.Controls.Add(this.txtWDZ);
            this.groupBox3.Controls.Add(this.txtHSL);
            this.groupBox3.Controls.Add(this.label10);
            this.groupBox3.Controls.Add(this.label9);
            this.groupBox3.Controls.Add(this.label8);
            this.groupBox3.Controls.Add(this.label7);
            this.groupBox3.Location = new System.Drawing.Point(526, 27);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(499, 381);
            this.groupBox3.TabIndex = 18;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "展示数据";
            // 
            // txtHSL
            // 
            this.txtHSL.Location = new System.Drawing.Point(187, 44);
            this.txtHSL.Margin = new System.Windows.Forms.Padding(4);
            this.txtHSL.Name = "txtHSL";
            this.txtHSL.Size = new System.Drawing.Size(219, 35);
            this.txtHSL.TabIndex = 4;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(72, 203);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(82, 24);
            this.label10.TabIndex = 3;
            this.label10.Text = "PH值：";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(72, 152);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 24);
            this.label9.TabIndex = 3;
            this.label9.Text = "电导率：";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(72, 104);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(106, 24);
            this.label8.TabIndex = 3;
            this.label8.Text = "温度值：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(72, 51);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(106, 24);
            this.label7.TabIndex = 3;
            this.label7.Text = "含水率：";
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
            this.groupBox1.Location = new System.Drawing.Point(24, 27);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox1.Size = new System.Drawing.Size(473, 381);
            this.groupBox1.TabIndex = 16;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "连接和关闭";
            this.groupBox1.Enter += new System.EventHandler(this.groupBox1_Enter);
            // 
            // serialPort1
            // 
            this.serialPort1.DataReceived += new System.IO.Ports.SerialDataReceivedEventHandler(this.serialPort1_DataReceived);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 272);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(160, 57);
            this.button1.TabIndex = 14;
            this.button1.Text = "修改波特率";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnWrite
            // 
            this.btnWrite.Location = new System.Drawing.Point(187, 221);
            this.btnWrite.Margin = new System.Windows.Forms.Padding(4);
            this.btnWrite.Name = "btnWrite";
            this.btnWrite.Size = new System.Drawing.Size(133, 53);
            this.btnWrite.TabIndex = 13;
            this.btnWrite.Text = "写入";
            this.btnWrite.UseVisualStyleBackColor = true;
            this.btnWrite.Click += new System.EventHandler(this.btnWrite_Click);
            // 
            // txtData
            // 
            this.txtData.Location = new System.Drawing.Point(241, 163);
            this.txtData.Margin = new System.Windows.Forms.Padding(4);
            this.txtData.Name = "txtData";
            this.txtData.Size = new System.Drawing.Size(160, 35);
            this.txtData.TabIndex = 9;
            this.txtData.Text = "123";
            // 
            // txtStartAddress
            // 
            this.txtStartAddress.Location = new System.Drawing.Point(241, 103);
            this.txtStartAddress.Margin = new System.Windows.Forms.Padding(4);
            this.txtStartAddress.Name = "txtStartAddress";
            this.txtStartAddress.Size = new System.Drawing.Size(160, 35);
            this.txtStartAddress.TabIndex = 11;
            this.txtStartAddress.Text = "4";
            // 
            // txtSlaveID
            // 
            this.txtSlaveID.Location = new System.Drawing.Point(241, 51);
            this.txtSlaveID.Margin = new System.Windows.Forms.Padding(4);
            this.txtSlaveID.Name = "txtSlaveID";
            this.txtSlaveID.Size = new System.Drawing.Size(160, 35);
            this.txtSlaveID.TabIndex = 12;
            this.txtSlaveID.Text = "1";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(127, 169);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(106, 24);
            this.label14.TabIndex = 5;
            this.label14.Text = "数据值：";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(103, 109);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(130, 24);
            this.label12.TabIndex = 7;
            this.label12.Text = "起始地址：";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(103, 57);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(130, 24);
            this.label11.TabIndex = 8;
            this.label11.Text = "从站地址：";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.button1);
            this.groupBox4.Controls.Add(this.btnWrite);
            this.groupBox4.Controls.Add(this.txtData);
            this.groupBox4.Controls.Add(this.txtStartAddress);
            this.groupBox4.Controls.Add(this.txtSlaveID);
            this.groupBox4.Controls.Add(this.label14);
            this.groupBox4.Controls.Add(this.label12);
            this.groupBox4.Controls.Add(this.label11);
            this.groupBox4.Location = new System.Drawing.Point(526, 427);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox4.Size = new System.Drawing.Size(504, 329);
            this.groupBox4.TabIndex = 19;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "写入数据";
            // 
            // btnRead
            // 
            this.btnRead.Location = new System.Drawing.Point(169, 155);
            this.btnRead.Margin = new System.Windows.Forms.Padding(4);
            this.btnRead.Name = "btnRead";
            this.btnRead.Size = new System.Drawing.Size(133, 53);
            this.btnRead.TabIndex = 2;
            this.btnRead.Text = "读取";
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
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnRead);
            this.groupBox2.Controls.Add(this.cbRealTimeRead);
            this.groupBox2.Location = new System.Drawing.Point(24, 427);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox2.Size = new System.Drawing.Size(473, 329);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "读取数据";
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1073, 478);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(275, 223);
            this.richTextBox1.TabIndex = 20;
            this.richTextBox1.Text = "";
            // 
            // chart1
            // 
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(1048, 47);
            this.chart1.Name = "chart1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(598, 361);
            this.chart1.TabIndex = 21;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1899, 958);
            this.Controls.Add(this.chart1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox2);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtWDZ;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.ComboBox cbbParity;
        private System.Windows.Forms.ComboBox cbbStopBit;
        private System.Windows.Forms.ComboBox cbbDataBit;
        private System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.TextBox txtPH;
        private System.Windows.Forms.ComboBox cbbPortName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDDL;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox txtHSL;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.IO.Ports.SerialPort serialPort1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnWrite;
        private System.Windows.Forms.TextBox txtData;
        private System.Windows.Forms.TextBox txtStartAddress;
        private System.Windows.Forms.TextBox txtSlaveID;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button btnRead;
        private System.Windows.Forms.CheckBox cbRealTimeRead;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}

