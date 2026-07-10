namespace day08
{
    partial class _01_Task
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
            this.button6 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button8 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(90, 282);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(919, 84);
            this.button6.TabIndex = 12;
            this.button6.Text = "factory .ContinueWhenAny()/factory.ContinueWhenAll()";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(692, 171);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(522, 69);
            this.button5.TabIndex = 11;
            this.button5.Text = "Task .WhenAny()/Task .WhenAll()";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(90, 171);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(544, 69);
            this.button4.TabIndex = 10;
            this.button4.Text = "Task .WaitAny()/Task .WaitAll()/";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(708, 39);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(235, 82);
            this.button3.TabIndex = 9;
            this.button3.Text = "task.Wait()";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(401, 40);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(256, 81);
            this.button2.TabIndex = 8;
            this.button2.Text = "取消任务";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(90, 39);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(216, 81);
            this.button1.TabIndex = 7;
            this.button1.Text = "启动任务";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Form1_Load);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(301, 716);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(82, 24);
            this.label1.TabIndex = 13;
            this.label1.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(460, 716);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(82, 24);
            this.label2.TabIndex = 13;
            this.label2.Text = "label1";
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(523, 431);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(382, 125);
            this.button8.TabIndex = 14;
            this.button8.Text = "Task<T> 获取任务的返回值2";
            this.button8.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(90, 431);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(382, 125);
            this.button7.TabIndex = 15;
            this.button7.Text = "Task<T> 获取任务的返回值1";
            this.button7.UseVisualStyleBackColor = true;
            // 
            // _01_Task
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1287, 869);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Name = "_01_Task";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button7;
    }
}

