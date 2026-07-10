namespace TemperatureControl.WinForms.Forms
{
    partial class InventoryCheck
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
            this.userControl11 = new TemperatureControl.WinForms.Controls.UserControl1();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.userControl11);
            this.panel2.Controls.SetChildIndex(this.lblCurrenPageAndTotalPage, 0);
            this.panel2.Controls.SetChildIndex(this.label7, 0);
            this.panel2.Controls.SetChildIndex(this.btnFirst, 0);
            this.panel2.Controls.SetChildIndex(this.btnGo, 0);
            this.panel2.Controls.SetChildIndex(this.btnPrev, 0);
            this.panel2.Controls.SetChildIndex(this.txtCurrentpage, 0);
            this.panel2.Controls.SetChildIndex(this.btnNext, 0);
            this.panel2.Controls.SetChildIndex(this.btnLast, 0);
            this.panel2.Controls.SetChildIndex(this.cbbPageSize, 0);
            this.panel2.Controls.SetChildIndex(this.userControl11, 0);
            // 
            // btnAdd
            // 
            this.btnAdd.FlatAppearance.BorderSize = 0;
            // 
            // btnSearch
            // 
            this.btnSearch.FlatAppearance.BorderSize = 0;
            // 
            // button2
            // 
            this.button2.FlatAppearance.BorderSize = 0;
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            // 
            // userControl11
            // 
            this.userControl11.CurrentPage = 1;
            this.userControl11.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.userControl11.Location = new System.Drawing.Point(0, -9);
            this.userControl11.Name = "userControl11";
            this.userControl11.PageSize = 10;
            this.userControl11.Size = new System.Drawing.Size(1515, 105);
            this.userControl11.TabIndex = 15;
            this.userControl11.totalNumber = 0;
            this.userControl11.userTotalPage = 0;
            // 
            // InventoryCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1759, 1150);
            this.Name = "InventoryCheck";
            this.Text = "InventoryCheck";
            this.Load += new System.EventHandler(this.InventoryCheck_Load);
            this.Shown += new System.EventHandler(this.InventoryCheck_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.UserControl1 userControl11;
    }
}