namespace day09.自定义用户控件
{
    partial class 自定义控件
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
            this.userControl_自定义1 = new day09.自定义用户控件.UserControl_自定义();
            this.SuspendLayout();
            // 
            // userControl_自定义1
            // 
            this.userControl_自定义1.ID = null;
            this.userControl_自定义1.Location = new System.Drawing.Point(94, 86);
            this.userControl_自定义1.Name = "userControl_自定义1";
            this.userControl_自定义1.Size = new System.Drawing.Size(800, 450);
            this.userControl_自定义1.TabIndex = 0;
            this.userControl_自定义1.MyEvent += new System.EventHandler(this.userControl_自定义1_MyEvent);
            // 
            // 自定义控件
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1055, 683);
            this.Controls.Add(this.userControl_自定义1);
            this.Name = "自定义控件";
            this.Text = "自定义控件";
            this.ResumeLayout(false);

        }

        #endregion

        private UserControl_自定义 userControl_自定义1;
    }
}