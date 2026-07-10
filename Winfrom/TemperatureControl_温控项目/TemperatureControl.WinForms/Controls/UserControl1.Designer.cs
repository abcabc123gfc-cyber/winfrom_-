namespace TemperatureControl.WinForms.Controls
{
    partial class UserControl1
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel6 = new System.Windows.Forms.Panel();
            this.cbbPageSize = new System.Windows.Forms.ComboBox();
            this.btnLast = new System.Windows.Forms.Button();
            this.btnNext = new System.Windows.Forms.Button();
            this.txtCurrentpage = new System.Windows.Forms.TextBox();
            this.btnPrev = new System.Windows.Forms.Button();
            this.btnGo = new System.Windows.Forms.Button();
            this.btnFirst = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblCurrenPageAndTotalPage = new System.Windows.Forms.Label();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel6.Controls.Add(this.cbbPageSize);
            this.panel6.Controls.Add(this.btnLast);
            this.panel6.Controls.Add(this.btnNext);
            this.panel6.Controls.Add(this.txtCurrentpage);
            this.panel6.Controls.Add(this.btnPrev);
            this.panel6.Controls.Add(this.btnGo);
            this.panel6.Controls.Add(this.btnFirst);
            this.panel6.Controls.Add(this.label7);
            this.panel6.Controls.Add(this.label8);
            this.panel6.Controls.Add(this.lblCurrenPageAndTotalPage);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 51);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(1581, 102);
            this.panel6.TabIndex = 12;
            // 
            // cbbPageSize
            // 
            this.cbbPageSize.FormattingEnabled = true;
            this.cbbPageSize.Items.AddRange(new object[] {
            "5",
            "10",
            "15"});
            this.cbbPageSize.Location = new System.Drawing.Point(884, 40);
            this.cbbPageSize.Name = "cbbPageSize";
            this.cbbPageSize.Size = new System.Drawing.Size(182, 32);
            this.cbbPageSize.TabIndex = 14;
            // 
            // btnLast
            // 
            this.btnLast.Location = new System.Drawing.Point(607, 29);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(125, 52);
            this.btnLast.TabIndex = 9;
            this.btnLast.Text = "尾页";
            this.btnLast.UseVisualStyleBackColor = true;
            // 
            // btnNext
            // 
            this.btnNext.Location = new System.Drawing.Point(423, 30);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(125, 52);
            this.btnNext.TabIndex = 10;
            this.btnNext.Text = "下一页";
            this.btnNext.UseVisualStyleBackColor = true;
            // 
            // txtCurrentpage
            // 
            this.txtCurrentpage.Location = new System.Drawing.Point(1193, 37);
            this.txtCurrentpage.Name = "txtCurrentpage";
            this.txtCurrentpage.Size = new System.Drawing.Size(103, 35);
            this.txtCurrentpage.TabIndex = 8;
            // 
            // btnPrev
            // 
            this.btnPrev.Location = new System.Drawing.Point(176, 30);
            this.btnPrev.Name = "btnPrev";
            this.btnPrev.Size = new System.Drawing.Size(125, 52);
            this.btnPrev.TabIndex = 11;
            this.btnPrev.Text = "前一页";
            this.btnPrev.UseVisualStyleBackColor = true;
            // 
            // btnGo
            // 
            this.btnGo.Location = new System.Drawing.Point(1340, 26);
            this.btnGo.Name = "btnGo";
            this.btnGo.Size = new System.Drawing.Size(125, 52);
            this.btnGo.TabIndex = 12;
            this.btnGo.Text = "跳转";
            this.btnGo.UseVisualStyleBackColor = true;
            // 
            // btnFirst
            // 
            this.btnFirst.Location = new System.Drawing.Point(13, 30);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(125, 52);
            this.btnFirst.TabIndex = 13;
            this.btnFirst.Text = "首页";
            this.btnFirst.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1105, 42);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 24);
            this.label7.TabIndex = 5;
            this.label7.Text = "页码:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(777, 42);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(70, 24);
            this.label8.TabIndex = 6;
            this.label8.Text = "条数:";
            // 
            // lblCurrenPageAndTotalPage
            // 
            this.lblCurrenPageAndTotalPage.AutoSize = true;
            this.lblCurrenPageAndTotalPage.Location = new System.Drawing.Point(354, 44);
            this.lblCurrenPageAndTotalPage.Name = "lblCurrenPageAndTotalPage";
            this.lblCurrenPageAndTotalPage.Size = new System.Drawing.Size(46, 24);
            this.lblCurrenPageAndTotalPage.TabIndex = 7;
            this.lblCurrenPageAndTotalPage.Text = "0/0";
            // 
            // UserControl1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel6);
            this.Name = "UserControl1";
            this.Size = new System.Drawing.Size(1581, 153);
            this.Load += new System.EventHandler(this.UserControl1_Load);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Panel panel6;
        protected internal System.Windows.Forms.ComboBox cbbPageSize;
        protected internal System.Windows.Forms.Button btnLast;
        protected internal System.Windows.Forms.Button btnNext;
        protected internal System.Windows.Forms.TextBox txtCurrentpage;
        protected internal System.Windows.Forms.Button btnPrev;
        protected internal System.Windows.Forms.Button btnGo;
        protected internal System.Windows.Forms.Button btnFirst;
        protected internal System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        protected internal System.Windows.Forms.Label lblCurrenPageAndTotalPage;
    }
}
