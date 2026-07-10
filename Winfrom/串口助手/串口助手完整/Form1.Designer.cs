namespace 串口助手完整
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
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.cbTimeSend = new System.Windows.Forms.CheckBox();
            this.cbNewLine = new System.Windows.Forms.CheckBox();
            this.cbHexSend = new System.Windows.Forms.CheckBox();
            this.rtbSendInfo = new System.Windows.Forms.RichTextBox();
            this.cbWhiteOrBlack = new System.Windows.Forms.CheckBox();
            this.cbHexDislpay = new System.Windows.Forms.CheckBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnOpenOrClose = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.cbbPartiy = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cbbDataBits = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbbStopBits = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbBaudRate = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cbbProt = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rtbDislpayInfo = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(297, 761);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(100, 35);
            this.txtInterval.TabIndex = 31;
            this.txtInterval.Text = "1000";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(417, 765);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(58, 24);
            this.label7.TabIndex = 30;
            this.label7.Text = "毫秒";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(221, 765);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(58, 24);
            this.label6.TabIndex = 29;
            this.label6.Text = "周期";
            // 
            // cbTimeSend
            // 
            this.cbTimeSend.AutoSize = true;
            this.cbTimeSend.Location = new System.Drawing.Point(40, 763);
            this.cbTimeSend.Name = "cbTimeSend";
            this.cbTimeSend.Size = new System.Drawing.Size(138, 28);
            this.cbTimeSend.TabIndex = 28;
            this.cbTimeSend.Text = "定时发送";
            this.cbTimeSend.UseVisualStyleBackColor = true;
            // 
            // cbNewLine
            // 
            this.cbNewLine.AutoSize = true;
            this.cbNewLine.Location = new System.Drawing.Point(813, 763);
            this.cbNewLine.Name = "cbNewLine";
            this.cbNewLine.Size = new System.Drawing.Size(138, 28);
            this.cbNewLine.TabIndex = 27;
            this.cbNewLine.Text = "发送新行";
            this.cbNewLine.UseVisualStyleBackColor = true;
            // 
            // cbHexSend
            // 
            this.cbHexSend.AutoSize = true;
            this.cbHexSend.Location = new System.Drawing.Point(571, 763);
            this.cbHexSend.Name = "cbHexSend";
            this.cbHexSend.Size = new System.Drawing.Size(186, 28);
            this.cbHexSend.TabIndex = 26;
            this.cbHexSend.Text = "以16进制发送";
            this.cbHexSend.UseVisualStyleBackColor = true;
            // 
            // rtbSendInfo
            // 
            this.rtbSendInfo.Location = new System.Drawing.Point(28, 541);
            this.rtbSendInfo.Name = "rtbSendInfo";
            this.rtbSendInfo.Size = new System.Drawing.Size(1194, 184);
            this.rtbSendInfo.TabIndex = 25;
            this.rtbSendInfo.Text = "";
            // 
            // cbWhiteOrBlack
            // 
            this.cbWhiteOrBlack.AutoSize = true;
            this.cbWhiteOrBlack.Location = new System.Drawing.Point(1072, 475);
            this.cbWhiteOrBlack.Name = "cbWhiteOrBlack";
            this.cbWhiteOrBlack.Size = new System.Drawing.Size(138, 28);
            this.cbWhiteOrBlack.TabIndex = 24;
            this.cbWhiteOrBlack.Text = "白底黑字";
            this.cbWhiteOrBlack.UseVisualStyleBackColor = true;
            // 
            // cbHexDislpay
            // 
            this.cbHexDislpay.AutoSize = true;
            this.cbHexDislpay.Location = new System.Drawing.Point(788, 475);
            this.cbHexDislpay.Name = "cbHexDislpay";
            this.cbHexDislpay.Size = new System.Drawing.Size(186, 28);
            this.cbHexDislpay.TabIndex = 23;
            this.cbHexDislpay.Text = "以16进制显示";
            this.cbHexDislpay.UseVisualStyleBackColor = true;
            this.cbHexDislpay.CheckedChanged += new System.EventHandler(this.cbHexDislpay_CheckedChanged);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(1011, 743);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(184, 69);
            this.btnSend.TabIndex = 21;
            this.btnSend.Text = "发送";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // btnOpenOrClose
            // 
            this.btnOpenOrClose.Location = new System.Drawing.Point(982, 362);
            this.btnOpenOrClose.Name = "btnOpenOrClose";
            this.btnOpenOrClose.Size = new System.Drawing.Size(240, 60);
            this.btnOpenOrClose.TabIndex = 20;
            this.btnOpenOrClose.Text = "打开串口";
            this.btnOpenOrClose.UseVisualStyleBackColor = true;
            this.btnOpenOrClose.Click += new System.EventHandler(this.btnOpenOrClose_Click);
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(788, 362);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(136, 60);
            this.btnClear.TabIndex = 22;
            this.btnClear.Text = "清除";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // cbbPartiy
            // 
            this.cbbPartiy.FormattingEnabled = true;
            this.cbbPartiy.Location = new System.Drawing.Point(926, 282);
            this.cbbPartiy.Name = "cbbPartiy";
            this.cbbPartiy.Size = new System.Drawing.Size(296, 32);
            this.cbbPartiy.TabIndex = 19;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(784, 286);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(118, 24);
            this.label5.TabIndex = 13;
            this.label5.Text = "奇偶校验:";
            // 
            // cbbDataBits
            // 
            this.cbbDataBits.FormattingEnabled = true;
            this.cbbDataBits.Location = new System.Drawing.Point(926, 216);
            this.cbbDataBits.Name = "cbbDataBits";
            this.cbbDataBits.Size = new System.Drawing.Size(296, 32);
            this.cbbDataBits.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(784, 220);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(94, 24);
            this.label4.TabIndex = 12;
            this.label4.Text = "数据位:";
            // 
            // cbbStopBits
            // 
            this.cbbStopBits.FormattingEnabled = true;
            this.cbbStopBits.Location = new System.Drawing.Point(926, 155);
            this.cbbStopBits.Name = "cbbStopBits";
            this.cbbStopBits.Size = new System.Drawing.Size(296, 32);
            this.cbbStopBits.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(784, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(94, 24);
            this.label3.TabIndex = 11;
            this.label3.Text = "停止位:";
            // 
            // cbbBaudRate
            // 
            this.cbbBaudRate.FormattingEnabled = true;
            this.cbbBaudRate.Location = new System.Drawing.Point(926, 88);
            this.cbbBaudRate.Name = "cbbBaudRate";
            this.cbbBaudRate.Size = new System.Drawing.Size(296, 32);
            this.cbbBaudRate.TabIndex = 16;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(784, 92);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 24);
            this.label2.TabIndex = 10;
            this.label2.Text = "波特率:";
            // 
            // cbbProt
            // 
            this.cbbProt.FormattingEnabled = true;
            this.cbbProt.Location = new System.Drawing.Point(926, 33);
            this.cbbProt.Name = "cbbProt";
            this.cbbProt.Size = new System.Drawing.Size(296, 32);
            this.cbbProt.TabIndex = 15;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(784, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 24);
            this.label1.TabIndex = 14;
            this.label1.Text = "串口选择:";
            // 
            // rtbDislpayInfo
            // 
            this.rtbDislpayInfo.BackColor = System.Drawing.Color.Black;
            this.rtbDislpayInfo.ForeColor = System.Drawing.SystemColors.Window;
            this.rtbDislpayInfo.Location = new System.Drawing.Point(28, 12);
            this.rtbDislpayInfo.Name = "rtbDislpayInfo";
            this.rtbDislpayInfo.Size = new System.Drawing.Size(729, 491);
            this.rtbDislpayInfo.TabIndex = 9;
            this.rtbDislpayInfo.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 924);
            this.Controls.Add(this.txtInterval);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cbTimeSend);
            this.Controls.Add(this.cbNewLine);
            this.Controls.Add(this.cbHexSend);
            this.Controls.Add(this.rtbSendInfo);
            this.Controls.Add(this.cbWhiteOrBlack);
            this.Controls.Add(this.cbHexDislpay);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnOpenOrClose);
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.cbbPartiy);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cbbDataBits);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.cbbStopBits);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.cbbBaudRate);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbbProt);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rtbDislpayInfo);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.CheckBox cbTimeSend;
        private System.Windows.Forms.CheckBox cbNewLine;
        private System.Windows.Forms.CheckBox cbHexSend;
        private System.Windows.Forms.RichTextBox rtbSendInfo;
        private System.Windows.Forms.CheckBox cbWhiteOrBlack;
        private System.Windows.Forms.CheckBox cbHexDislpay;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnOpenOrClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ComboBox cbbPartiy;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cbbDataBits;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbbStopBits;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbBaudRate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cbbProt;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RichTextBox rtbDislpayInfo;
    }
}

