namespace day09.扩展控件
{
    partial class 扩展控件
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
            this.myTextBox1 = new day09.扩展控件.MyTextBox();
            this.SuspendLayout();
            // 
            // myTextBox1
            // 
            this.myTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.myTextBox1.Location = new System.Drawing.Point(171, 180);
            this.myTextBox1.MyBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.myTextBox1.Name = "myTextBox1";
            this.myTextBox1.Size = new System.Drawing.Size(382, 35);
            this.myTextBox1.TabIndex = 1;
            this.myTextBox1.Text = "这是扩展的TextBox";
            // 
            // 扩展控件
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.myTextBox1);
            this.Name = "扩展控件";
            this.Text = "扩展控件";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MyTextBox myTextBox1;
    }
}