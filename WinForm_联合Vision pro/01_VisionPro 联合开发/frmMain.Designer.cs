namespace _01_VisionPro_联合开发
{
    partial class frmMain
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
            this.cogRecordsDisplay1 = new Cognex.VisionPro.CogRecordsDisplay();
            this.cogRecordsDisplay2 = new Cognex.VisionPro.CogRecordsDisplay();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.labelLV = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.labelOK = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.labelZ = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.系统ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.编辑作业ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.参数设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.其他设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.相机1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.作业1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.通信设置ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // cogRecordsDisplay1
            // 
            this.cogRecordsDisplay1.Location = new System.Drawing.Point(12, 89);
            this.cogRecordsDisplay1.Name = "cogRecordsDisplay1";
            this.cogRecordsDisplay1.SelectedRecordKey = null;
            this.cogRecordsDisplay1.ShowRecordsDropDown = true;
            this.cogRecordsDisplay1.Size = new System.Drawing.Size(830, 1176);
            this.cogRecordsDisplay1.Subject = null;
            this.cogRecordsDisplay1.TabIndex = 0;
            // 
            // cogRecordsDisplay2
            // 
            this.cogRecordsDisplay2.Location = new System.Drawing.Point(876, 89);
            this.cogRecordsDisplay2.Name = "cogRecordsDisplay2";
            this.cogRecordsDisplay2.SelectedRecordKey = null;
            this.cogRecordsDisplay2.ShowRecordsDropDown = true;
            this.cogRecordsDisplay2.Size = new System.Drawing.Size(830, 1176);
            this.cogRecordsDisplay2.Subject = null;
            this.cogRecordsDisplay2.TabIndex = 0;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 24;
            this.listBox1.Location = new System.Drawing.Point(1781, 325);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(426, 940);
            this.listBox1.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.labelLV);
            this.panel1.Controls.Add(this.label6);
            this.panel1.Controls.Add(this.labelOK);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.labelZ);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Location = new System.Drawing.Point(1744, 36);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(447, 269);
            this.panel1.TabIndex = 2;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(114, 206);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 32, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 58);
            this.button1.TabIndex = 13;
            this.button1.Text = "清空";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // labelLV
            // 
            this.labelLV.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelLV.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelLV.Location = new System.Drawing.Point(213, 103);
            this.labelLV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelLV.Name = "labelLV";
            this.labelLV.Size = new System.Drawing.Size(122, 32);
            this.labelLV.TabIndex = 12;
            this.labelLV.Text = "0";
            this.labelLV.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(111, 108);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(70, 24);
            this.label6.TabIndex = 11;
            this.label6.Text = "良率:";
            // 
            // labelOK
            // 
            this.labelOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelOK.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelOK.Location = new System.Drawing.Point(213, 55);
            this.labelOK.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelOK.Name = "labelOK";
            this.labelOK.Size = new System.Drawing.Size(122, 32);
            this.labelOK.TabIndex = 10;
            this.labelOK.Text = "0";
            this.labelOK.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(111, 60);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(70, 24);
            this.label4.TabIndex = 9;
            this.label4.Text = "良品:";
            // 
            // labelZ
            // 
            this.labelZ.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.labelZ.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.labelZ.Location = new System.Drawing.Point(213, 5);
            this.labelZ.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.labelZ.Name = "labelZ";
            this.labelZ.Size = new System.Drawing.Size(122, 32);
            this.labelZ.TabIndex = 8;
            this.labelZ.Text = "0";
            this.labelZ.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(111, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 24);
            this.label1.TabIndex = 7;
            this.label1.Text = "总数:";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2, 2, 0, 2);
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.系统ToolStripMenuItem,
            this.相机设置ToolStripMenuItem,
            this.编辑作业ToolStripMenuItem,
            this.参数设置ToolStripMenuItem,
            this.其他设置ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(2433, 39);
            this.menuStrip1.TabIndex = 3;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 系统ToolStripMenuItem
            // 
            this.系统ToolStripMenuItem.Name = "系统ToolStripMenuItem";
            this.系统ToolStripMenuItem.Size = new System.Drawing.Size(82, 38);
            this.系统ToolStripMenuItem.Text = "系统";
            // 
            // 相机设置ToolStripMenuItem
            // 
            this.相机设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.相机1ToolStripMenuItem});
            this.相机设置ToolStripMenuItem.Name = "相机设置ToolStripMenuItem";
            this.相机设置ToolStripMenuItem.Size = new System.Drawing.Size(130, 38);
            this.相机设置ToolStripMenuItem.Text = "相机设置";
            // 
            // 编辑作业ToolStripMenuItem
            // 
            this.编辑作业ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.作业1ToolStripMenuItem});
            this.编辑作业ToolStripMenuItem.Name = "编辑作业ToolStripMenuItem";
            this.编辑作业ToolStripMenuItem.Size = new System.Drawing.Size(130, 38);
            this.编辑作业ToolStripMenuItem.Text = "编辑作业";
            // 
            // 参数设置ToolStripMenuItem
            // 
            this.参数设置ToolStripMenuItem.Name = "参数设置ToolStripMenuItem";
            this.参数设置ToolStripMenuItem.Size = new System.Drawing.Size(130, 38);
            this.参数设置ToolStripMenuItem.Text = "参数设置";
            // 
            // 其他设置ToolStripMenuItem
            // 
            this.其他设置ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.通信设置ToolStripMenuItem});
            this.其他设置ToolStripMenuItem.Name = "其他设置ToolStripMenuItem";
            this.其他设置ToolStripMenuItem.Size = new System.Drawing.Size(130, 35);
            this.其他设置ToolStripMenuItem.Text = "其他设置";
            // 
            // 相机1ToolStripMenuItem
            // 
            this.相机1ToolStripMenuItem.Name = "相机1ToolStripMenuItem";
            this.相机1ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.相机1ToolStripMenuItem.Text = "相机1";
            // 
            // 作业1ToolStripMenuItem
            // 
            this.作业1ToolStripMenuItem.Name = "作业1ToolStripMenuItem";
            this.作业1ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.作业1ToolStripMenuItem.Text = "作业1";
            // 
            // 通信设置ToolStripMenuItem
            // 
            this.通信设置ToolStripMenuItem.Name = "通信设置ToolStripMenuItem";
            this.通信设置ToolStripMenuItem.Size = new System.Drawing.Size(359, 44);
            this.通信设置ToolStripMenuItem.Text = "通信设置";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(2433, 1347);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.cogRecordsDisplay2);
            this.Controls.Add(this.cogRecordsDisplay1);
            this.Controls.Add(this.menuStrip1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Cognex.VisionPro.CogRecordsDisplay cogRecordsDisplay1;
        private Cognex.VisionPro.CogRecordsDisplay cogRecordsDisplay2;
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label labelLV;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label labelZ;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 系统ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 编辑作业ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 参数设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 其他设置ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 相机1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 作业1ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 通信设置ToolStripMenuItem;
    }
}

