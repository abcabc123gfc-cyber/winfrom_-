namespace day09.完全自定义控件
{
    partial class 完全自定义控件
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.myLabel1 = new day09.完全自定义控件.MyLabel();
            this.SuspendLayout();
            // 
            // myLabel1
            // 
            this.myLabel1.Location = new System.Drawing.Point(369, 133);
            this.myLabel1.MyFont = new System.Drawing.Font("宋体", 12F);
            this.myLabel1.MyText = "文字";
            this.myLabel1.Name = "myLabel1";
            this.myLabel1.Size = new System.Drawing.Size(134, 94);
            this.myLabel1.TabIndex = 0;
            this.myLabel1.Text = "myLabel1";
            // 
            // 完全自定义控件
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myLabel1);
            this.Name = "完全自定义控件";
            this.Text = "完全自定义控件";
            this.ResumeLayout(false);

        }

        #endregion

        private MyLabel myLabel1;
    }
}