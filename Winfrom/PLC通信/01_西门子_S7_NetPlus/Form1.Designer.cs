namespace _01_西门子_S7_NetPlus
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            textBox1 = new TextBox();
            textBox2 = new TextBox();
            textBox3 = new TextBox();
            textBox4 = new TextBox();
            button1 = new Button();
            listBox1 = new ListBox();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            button5 = new Button();
            button6 = new Button();
            button7 = new Button();
            button8 = new Button();
            button9 = new Button();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 40);
            label1.Name = "label1";
            label1.Size = new Size(102, 31);
            label1.TabIndex = 0;
            label1.Text = "plc 地址";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 113);
            label2.Name = "label2";
            label2.Size = new Size(102, 31);
            label2.TabIndex = 0;
            label2.Text = "plc 机架";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(497, 43);
            label3.Name = "label3";
            label3.Size = new Size(102, 31);
            label3.TabIndex = 0;
            label3.Text = "plc 端口";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(497, 127);
            label4.Name = "label4";
            label4.Size = new Size(102, 31);
            label4.TabIndex = 0;
            label4.Text = "plc 插槽";
            // 
            // textBox1
            // 
            textBox1.Location = new Point(153, 40);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(300, 38);
            textBox1.TabIndex = 1;
            textBox1.Text = "192.168.8.10";
            // 
            // textBox2
            // 
            textBox2.Location = new Point(153, 110);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(300, 38);
            textBox2.TabIndex = 1;
            textBox2.Text = "0";
            // 
            // textBox3
            // 
            textBox3.Location = new Point(631, 36);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(300, 38);
            textBox3.TabIndex = 1;
            textBox3.Text = "102";
            // 
            // textBox4
            // 
            textBox4.Location = new Point(631, 120);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(300, 38);
            textBox4.TabIndex = 1;
            textBox4.Text = "1";
            // 
            // button1
            // 
            button1.Location = new Point(96, 185);
            button1.Name = "button1";
            button1.Size = new Size(202, 100);
            button1.TabIndex = 2;
            button1.Text = "连接";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // listBox1
            // 
            listBox1.FormattingEnabled = true;
            listBox1.Location = new Point(661, 243);
            listBox1.Name = "listBox1";
            listBox1.Size = new Size(391, 407);
            listBox1.TabIndex = 3;
            // 
            // button2
            // 
            button2.Location = new Point(34, 368);
            button2.Name = "button2";
            button2.Size = new Size(137, 76);
            button2.TabIndex = 4;
            button2.Text = "灯亮_1";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(221, 368);
            button3.Name = "button3";
            button3.Size = new Size(137, 76);
            button3.TabIndex = 4;
            button3.Text = "灯灭_1";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(34, 474);
            button4.Name = "button4";
            button4.Size = new Size(137, 76);
            button4.TabIndex = 4;
            button4.Text = "灯亮_2";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // button5
            // 
            button5.Location = new Point(221, 474);
            button5.Name = "button5";
            button5.Size = new Size(137, 76);
            button5.TabIndex = 4;
            button5.Text = "灯灭_2";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // button6
            // 
            button6.Location = new Point(34, 574);
            button6.Name = "button6";
            button6.Size = new Size(137, 76);
            button6.TabIndex = 4;
            button6.Text = "灯亮_3";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button7
            // 
            button7.Location = new Point(221, 574);
            button7.Name = "button7";
            button7.Size = new Size(137, 76);
            button7.TabIndex = 4;
            button7.Text = "灯灭_3";
            button7.UseVisualStyleBackColor = true;
            button7.Click += button7_Click;
            // 
            // button8
            // 
            button8.Location = new Point(450, 368);
            button8.Name = "button8";
            button8.Size = new Size(149, 80);
            button8.TabIndex = 5;
            button8.Text = "读_按钮状态_1";
            button8.UseVisualStyleBackColor = true;
            button8.Click += button8_Click;
            // 
            // button9
            // 
            button9.Location = new Point(450, 486);
            button9.Name = "button9";
            button9.Size = new Size(149, 80);
            button9.TabIndex = 5;
            button9.Text = "读_按钮状态";
            button9.UseVisualStyleBackColor = true;
            button9.Click += button9_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(14F, 31F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1115, 807);
            Controls.Add(button9);
            Controls.Add(button8);
            Controls.Add(button7);
            Controls.Add(button5);
            Controls.Add(button3);
            Controls.Add(button6);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(listBox1);
            Controls.Add(button1);
            Controls.Add(textBox2);
            Controls.Add(textBox4);
            Controls.Add(textBox3);
            Controls.Add(textBox1);
            Controls.Add(label4);
            Controls.Add(label2);
            Controls.Add(label3);
            Controls.Add(label1);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private TextBox textBox1;
        private TextBox textBox2;
        private TextBox textBox3;
        private TextBox textBox4;
        private Button button1;
        private ListBox listBox1;
        private Button button2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private Button button8;
        private Button button9;
    }
}
