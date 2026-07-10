namespace TemperatureControl.WinForms.Froms
{
    partial class InboundAdd
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
            this.dtpIncomeTime = new System.Windows.Forms.DateTimePicker();
            this.cbProduct = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.rtbRemark = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.cbStoreArea = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Size = new System.Drawing.Size(1152, 100);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.dtpIncomeTime);
            this.panel2.Controls.Add(this.cbProduct);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.rtbRemark);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.cbStoreArea);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtAmount);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Location = new System.Drawing.Point(199, 174);
            // 
            // dtpIncomeTime
            // 
            this.dtpIncomeTime.CustomFormat = "yyyy-MM-dd HH:mm";
            this.dtpIncomeTime.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.dtpIncomeTime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpIncomeTime.Location = new System.Drawing.Point(301, 254);
            this.dtpIncomeTime.Margin = new System.Windows.Forms.Padding(4);
            this.dtpIncomeTime.Name = "dtpIncomeTime";
            this.dtpIncomeTime.Size = new System.Drawing.Size(311, 43);
            this.dtpIncomeTime.TabIndex = 22;
            // 
            // cbProduct
            // 
            this.cbProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbProduct.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbProduct.FormattingEnabled = true;
            this.cbProduct.Location = new System.Drawing.Point(301, 115);
            this.cbProduct.Margin = new System.Windows.Forms.Padding(4);
            this.cbProduct.Name = "cbProduct";
            this.cbProduct.Size = new System.Drawing.Size(221, 43);
            this.cbProduct.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(140, 121);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 35);
            this.label5.TabIndex = 16;
            this.label5.Text = "入库产品：";
            // 
            // rtbRemark
            // 
            this.rtbRemark.Location = new System.Drawing.Point(301, 329);
            this.rtbRemark.Margin = new System.Windows.Forms.Padding(4);
            this.rtbRemark.Name = "rtbRemark";
            this.rtbRemark.Size = new System.Drawing.Size(311, 127);
            this.rtbRemark.TabIndex = 24;
            this.rtbRemark.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(193, 329);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 35);
            this.label4.TabIndex = 23;
            this.label4.Text = "备注：";
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(1)))), ((int)(((byte)(88)))), ((int)(((byte)(219)))));
            this.btnAdd.FlatAppearance.BorderSize = 0;
            this.btnAdd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAdd.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.btnAdd.ForeColor = System.Drawing.Color.White;
            this.btnAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btnAdd.Location = new System.Drawing.Point(301, 486);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 67);
            this.btnAdd.TabIndex = 25;
            this.btnAdd.Text = "添加";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // cbStoreArea
            // 
            this.cbStoreArea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbStoreArea.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.cbStoreArea.FormattingEnabled = true;
            this.cbStoreArea.Location = new System.Drawing.Point(301, 46);
            this.cbStoreArea.Margin = new System.Windows.Forms.Padding(4);
            this.cbStoreArea.Name = "cbStoreArea";
            this.cbStoreArea.Size = new System.Drawing.Size(221, 43);
            this.cbStoreArea.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(140, 51);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(150, 35);
            this.label3.TabIndex = 13;
            this.label3.Text = "入库区域：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(140, 259);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(150, 35);
            this.label2.TabIndex = 21;
            this.label2.Text = "入库日期：";
            // 
            // txtAmount
            // 
            this.txtAmount.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtAmount.Location = new System.Drawing.Point(301, 185);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(4);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(311, 43);
            this.txtAmount.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(140, 190);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 35);
            this.label6.TabIndex = 19;
            this.label6.Text = "产品数量：";
            // 
            // InboundAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1152, 943);
            this.Name = "InboundAdd";
            this.Text = "InboundAdd";
            this.Load += new System.EventHandler(this.InboundAdd_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DateTimePicker dtpIncomeTime;
        private System.Windows.Forms.ComboBox cbProduct;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RichTextBox rtbRemark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.ComboBox cbStoreArea;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label6;
    }
}