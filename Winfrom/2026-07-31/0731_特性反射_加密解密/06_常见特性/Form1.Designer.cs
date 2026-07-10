namespace _06_常见特性
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
            this.ucList1 = new _06_常见特性.UCList();
            this.SuspendLayout();
            // 
            // ucList1
            // 
            this.ucList1.Location = new System.Drawing.Point(203, 63);
            this.ucList1.Name = "ucList1";
            this.ucList1.Size = new System.Drawing.Size(1403, 729);
            this.ucList1.TabIndex = 0;
            this.ucList1.wuyifan = "123";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1927, 1209);
            this.Controls.Add(this.ucList1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private UCList ucList1;
    }
}

