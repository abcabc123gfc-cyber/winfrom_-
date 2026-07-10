namespace TemperatureControl.WinForms.Froms
{
    partial class ProductAdd
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
            this.txtMaxTemperature = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.rtbRemark = new System.Windows.Forms.RichTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnAdd = new System.Windows.Forms.Button();
            this.txtMinTemperature = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtProductName = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Size = new System.Drawing.Size(1221, 100);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.txtMaxTemperature);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.rtbRemark);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Controls.Add(this.txtMinTemperature);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.txtProductName);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Location = new System.Drawing.Point(234, 131);
            // 
            // txtMaxTemperature
            // 
            this.txtMaxTemperature.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtMaxTemperature.Location = new System.Drawing.Point(330, 213);
            this.txtMaxTemperature.Margin = new System.Windows.Forms.Padding(4);
            this.txtMaxTemperature.Name = "txtMaxTemperature";
            this.txtMaxTemperature.Size = new System.Drawing.Size(311, 43);
            this.txtMaxTemperature.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(131, 218);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(177, 35);
            this.label3.TabIndex = 13;
            this.label3.Text = "温度最大值：";
            // 
            // rtbRemark
            // 
            this.rtbRemark.Location = new System.Drawing.Point(330, 280);
            this.rtbRemark.Margin = new System.Windows.Forms.Padding(4);
            this.rtbRemark.Name = "rtbRemark";
            this.rtbRemark.Size = new System.Drawing.Size(311, 127);
            this.rtbRemark.TabIndex = 16;
            this.rtbRemark.Text = "";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(211, 280);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(96, 35);
            this.label4.TabIndex = 15;
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
            this.btnAdd.Location = new System.Drawing.Point(330, 437);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(5);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(160, 67);
            this.btnAdd.TabIndex = 17;
            this.btnAdd.Text = "添加";
            this.btnAdd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.btnAdd.UseVisualStyleBackColor = false;
            // 
            // txtMinTemperature
            // 
            this.txtMinTemperature.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtMinTemperature.Location = new System.Drawing.Point(330, 152);
            this.txtMinTemperature.Margin = new System.Windows.Forms.Padding(4);
            this.txtMinTemperature.Name = "txtMinTemperature";
            this.txtMinTemperature.Size = new System.Drawing.Size(311, 43);
            this.txtMinTemperature.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(131, 157);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(177, 35);
            this.label2.TabIndex = 11;
            this.label2.Text = "温度最小值：";
            // 
            // txtProductName
            // 
            this.txtProductName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.txtProductName.Location = new System.Drawing.Point(330, 90);
            this.txtProductName.Margin = new System.Windows.Forms.Padding(4);
            this.txtProductName.Name = "txtProductName";
            this.txtProductName.Size = new System.Drawing.Size(311, 43);
            this.txtProductName.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(158, 96);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(150, 35);
            this.label5.TabIndex = 9;
            this.label5.Text = "产品名称：";
            // 
            // ProductAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1221, 857);
            this.Name = "ProductAdd";
            this.Text = "ProductAdd";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaxTemperature;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RichTextBox rtbRemark;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.TextBox txtMinTemperature;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtProductName;
        private System.Windows.Forms.Label label5;
    }
}