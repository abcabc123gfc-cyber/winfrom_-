namespace _15_modbus_RTU_TCP_over
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
            this.txtData4 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtData3 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtData2 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtData1 = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.raRTUOU = new System.Windows.Forms.RadioButton();
            this.raRTUOT = new System.Windows.Forms.RadioButton();
            this.raUDP = new System.Windows.Forms.RadioButton();
            this.raTCP = new System.Windows.Forms.RadioButton();
            this.btnConn = new System.Windows.Forms.Button();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.txtIP = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtData4
            // 
            this.txtData4.Location = new System.Drawing.Point(147, 303);
            this.txtData4.Name = "txtData4";
            this.txtData4.Size = new System.Drawing.Size(337, 35);
            this.txtData4.TabIndex = 3;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(43, 303);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 24);
            this.label6.TabIndex = 2;
            this.label6.Text = "数据4";
            // 
            // txtData3
            // 
            this.txtData3.Location = new System.Drawing.Point(147, 230);
            this.txtData3.Name = "txtData3";
            this.txtData3.Size = new System.Drawing.Size(337, 35);
            this.txtData3.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(43, 230);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(70, 24);
            this.label5.TabIndex = 2;
            this.label5.Text = "数据3";
            // 
            // txtData2
            // 
            this.txtData2.Location = new System.Drawing.Point(147, 161);
            this.txtData2.Name = "txtData2";
            this.txtData2.Size = new System.Drawing.Size(337, 35);
            this.txtData2.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 161);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 24);
            this.label4.TabIndex = 2;
            this.label4.Text = "数据2";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 94);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 24);
            this.label3.TabIndex = 2;
            this.label3.Text = "数据1";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtData4);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txtData3);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.txtData2);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txtData1);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(24, 399);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1228, 382);
            this.groupBox2.TabIndex = 7;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "数据展示";
            // 
            // txtData1
            // 
            this.txtData1.Location = new System.Drawing.Point(147, 94);
            this.txtData1.Name = "txtData1";
            this.txtData1.Size = new System.Drawing.Size(337, 35);
            this.txtData1.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.raRTUOU);
            this.groupBox1.Controls.Add(this.raRTUOT);
            this.groupBox1.Controls.Add(this.raUDP);
            this.groupBox1.Controls.Add(this.raTCP);
            this.groupBox1.Controls.Add(this.btnConn);
            this.groupBox1.Controls.Add(this.txtPort);
            this.groupBox1.Controls.Add(this.txtIP);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(24, 24);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1228, 209);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器连接/断开";
            // 
            // raRTUOU
            // 
            this.raRTUOU.AutoSize = true;
            this.raRTUOU.Location = new System.Drawing.Point(1002, 57);
            this.raRTUOU.Name = "raRTUOU";
            this.raRTUOU.Size = new System.Drawing.Size(185, 28);
            this.raRTUOU.TabIndex = 3;
            this.raRTUOU.Text = "RTU Over UDP";
            this.raRTUOU.UseVisualStyleBackColor = true;
            // 
            // raRTUOT
            // 
            this.raRTUOT.AutoSize = true;
            this.raRTUOT.Location = new System.Drawing.Point(637, 57);
            this.raRTUOT.Name = "raRTUOT";
            this.raRTUOT.Size = new System.Drawing.Size(185, 28);
            this.raRTUOT.TabIndex = 3;
            this.raRTUOT.Text = "RTU Over TCP";
            this.raRTUOT.UseVisualStyleBackColor = true;
            // 
            // raUDP
            // 
            this.raUDP.AutoSize = true;
            this.raUDP.Location = new System.Drawing.Point(352, 57);
            this.raUDP.Name = "raUDP";
            this.raUDP.Size = new System.Drawing.Size(77, 28);
            this.raUDP.TabIndex = 3;
            this.raUDP.Text = "UDP";
            this.raUDP.UseVisualStyleBackColor = true;
            // 
            // raTCP
            // 
            this.raTCP.AutoSize = true;
            this.raTCP.Checked = true;
            this.raTCP.Location = new System.Drawing.Point(47, 57);
            this.raTCP.Name = "raTCP";
            this.raTCP.Size = new System.Drawing.Size(77, 28);
            this.raTCP.TabIndex = 3;
            this.raTCP.TabStop = true;
            this.raTCP.Text = "TCP";
            this.raTCP.UseVisualStyleBackColor = true;
            // 
            // btnConn
            // 
            this.btnConn.Location = new System.Drawing.Point(1045, 126);
            this.btnConn.Name = "btnConn";
            this.btnConn.Size = new System.Drawing.Size(142, 58);
            this.btnConn.TabIndex = 2;
            this.btnConn.Text = "连接";
            this.btnConn.UseVisualStyleBackColor = true;
            this.btnConn.Click += new System.EventHandler(this.btnConn_Click);
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(666, 138);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(300, 35);
            this.txtPort.TabIndex = 1;
            // 
            // txtIP
            // 
            this.txtIP.Location = new System.Drawing.Point(184, 138);
            this.txtIP.Name = "txtIP";
            this.txtIP.Size = new System.Drawing.Size(300, 35);
            this.txtIP.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(542, 143);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "端口:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(43, 143);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 0;
            this.label1.Text = "地址:";
            // 
            // btn
            // 
            this.btn.Location = new System.Drawing.Point(1069, 271);
            this.btn.Name = "btn";
            this.btn.Size = new System.Drawing.Size(142, 58);
            this.btn.TabIndex = 5;
            this.btn.Text = "写入";
            this.btn.UseVisualStyleBackColor = true;
            this.btn.Click += new System.EventHandler(this.btn_Click);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(43, 271);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(142, 58);
            this.btnStart.TabIndex = 6;
            this.btnStart.Text = "实时读取";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1345, 934);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn);
            this.Controls.Add(this.btnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtData4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtData3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtData2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtData1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton raRTUOU;
        private System.Windows.Forms.RadioButton raRTUOT;
        private System.Windows.Forms.RadioButton raUDP;
        private System.Windows.Forms.RadioButton raTCP;
        private System.Windows.Forms.Button btnConn;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.TextBox txtIP;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn;
        private System.Windows.Forms.Button btnStart;
    }
}

