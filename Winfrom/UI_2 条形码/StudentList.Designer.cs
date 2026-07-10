//namespace UI_2
//{
//    partial class StudentList
//    {
//        /// <summary>
//        /// Required designer variable.
//        /// </summary>
//        private System.ComponentModel.IContainer components = null;

//        /// <summary>
//        /// Clean up any resources being used.
//        /// </summary>
//        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
//        protected override void Dispose(bool disposing)
//        {
//            if (disposing && (components != null))
//            {
//                components.Dispose();
//            }
//            base.Dispose(disposing);
//        }

//        #region Windows Form Designer generated code

//        /// <summary>
//        /// Required method for Designer support - do not modify
//        /// the contents of this method with the code editor.
//        /// </summary>
//        private void InitializeComponent()
//        {
//            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("切换用户视图");
//            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("切换客户视图");
//            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("切换客户地址视图");
//            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("个人信息");
//            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("数据备份");
//            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("客户日志");
//            System.Windows.Forms.TreeNode treeNode7 = new System.Windows.Forms.TreeNode("用户日志");
//            System.Windows.Forms.TreeNode treeNode8 = new System.Windows.Forms.TreeNode("日志", new System.Windows.Forms.TreeNode[] {
//            treeNode6,
//            treeNode7});
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
//            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StudentList));
//            this.uiNavMenu1 = new Sunny.UI.UINavMenu();
//            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
//            this.dataGridView1 = new Sunny.UI.UIDataGridView();
//            this.groupBox1 = new Sunny.UI.UICheckBoxGroup();
//            this.button4 = new Sunny.UI.UIButton();
//            this.button3 = new Sunny.UI.UIButton();
//            this.button2 = new Sunny.UI.UIButton();
//            this.button1 = new Sunny.UI.UIButton();
//            this.textBox1 = new Sunny.UI.UITextBox();
//            this.textBox3 = new Sunny.UI.UITextBox();
//            this.textBox2 = new Sunny.UI.UITextBox();
//            this.label2 = new Sunny.UI.UILabel();
//            this.label1 = new Sunny.UI.UILabel();
//            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
//            this.groupBox1.SuspendLayout();
//            this.SuspendLayout();
//            // 
//            // uiNavMenu1
//            // 
//            this.uiNavMenu1.BorderStyle = System.Windows.Forms.BorderStyle.None;
//            this.uiNavMenu1.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
//            this.uiNavMenu1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.uiNavMenu1.FullRowSelect = true;
//            this.uiNavMenu1.HotTracking = true;
//            this.uiNavMenu1.ItemHeight = 50;
//            this.uiNavMenu1.Location = new System.Drawing.Point(-1, -2);
//            this.uiNavMenu1.Name = "uiNavMenu1";
//            treeNode1.Name = "节点0";
//            treeNode1.Text = "切换用户视图";
//            treeNode2.Name = "节点1";
//            treeNode2.Text = "切换客户视图";
//            treeNode3.Name = "节点2";
//            treeNode3.Text = "切换客户地址视图";
//            treeNode4.Name = "节点3";
//            treeNode4.Text = "个人信息";
//            treeNode5.Name = "节点4";
//            treeNode5.Text = "数据备份";
//            treeNode6.Name = "节点6";
//            treeNode6.Text = "客户日志";
//            treeNode7.Name = "节点7";
//            treeNode7.Text = "用户日志";
//            treeNode8.Name = "节点5";
//            treeNode8.Text = "日志";
//            this.uiNavMenu1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
//            treeNode1,
//            treeNode2,
//            treeNode3,
//            treeNode4,
//            treeNode5,
//            treeNode8});
//            this.uiNavMenu1.ShowLines = false;
//            this.uiNavMenu1.ShowPlusMinus = false;
//            this.uiNavMenu1.ShowRootLines = false;
//            this.uiNavMenu1.Size = new System.Drawing.Size(256, 928);
//            this.uiNavMenu1.TabIndex = 0;
//            this.uiNavMenu1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            // 
//            // dataGridView1
//            // 
//            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
//            this.dataGridView1.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
//            this.dataGridView1.BackgroundColor = System.Drawing.Color.White;
//            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
//            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
//            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
//            dataGridViewCellStyle2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
//            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
//            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
//            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
//            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
//            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
//            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle3.BackColor = System.Drawing.SystemColors.Window;
//            dataGridViewCellStyle3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            dataGridViewCellStyle3.ForeColor = System.Drawing.SystemColors.ControlText;
//            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
//            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
//            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
//            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle3;
//            this.dataGridView1.EnableHeadersVisualStyles = false;
//            this.dataGridView1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.dataGridView1.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
//            this.dataGridView1.Location = new System.Drawing.Point(264, 343);
//            this.dataGridView1.Name = "dataGridView1";
//            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
//            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
//            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
//            dataGridViewCellStyle4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
//            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(160)))), ((int)(((byte)(255)))));
//            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.White;
//            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
//            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle4;
//            this.dataGridView1.RowHeadersWidth = 82;
//            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
//            dataGridViewCellStyle5.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle5;
//            this.dataGridView1.RowTemplate.Height = 37;
//            this.dataGridView1.SelectedIndex = -1;
//            this.dataGridView1.Size = new System.Drawing.Size(1010, 518);
//            this.dataGridView1.StripeOddColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(243)))), ((int)(((byte)(255)))));
//            this.dataGridView1.TabIndex = 2;
//            // 
//            // groupBox1
//            // 
//            this.groupBox1.Controls.Add(this.button4);
//            this.groupBox1.Controls.Add(this.button3);
//            this.groupBox1.Controls.Add(this.button2);
//            this.groupBox1.Controls.Add(this.button1);
//            this.groupBox1.Controls.Add(this.textBox1);
//            this.groupBox1.Controls.Add(this.textBox3);
//            this.groupBox1.Controls.Add(this.textBox2);
//            this.groupBox1.Controls.Add(this.label2);
//            this.groupBox1.Controls.Add(this.label1);
//            this.groupBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.groupBox1.Location = new System.Drawing.Point(261, -2);
//            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//            this.groupBox1.MinimumSize = new System.Drawing.Size(1, 1);
//            this.groupBox1.Name = "groupBox1";
//            this.groupBox1.Padding = new System.Windows.Forms.Padding(0, 32, 0, 0);
//            this.groupBox1.SelectedIndexes = ((System.Collections.Generic.List<int>)(resources.GetObject("groupBox1.SelectedIndexes")));
//            this.groupBox1.Size = new System.Drawing.Size(1013, 312);
//            this.groupBox1.TabIndex = 3;
//            this.groupBox1.Text = "搜索条件";
//            this.groupBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
//            // 
//            // button4
//            // 
//            this.button4.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.button4.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.button4.Location = new System.Drawing.Point(763, 136);
//            this.button4.MinimumSize = new System.Drawing.Size(1, 1);
//            this.button4.Name = "button4";
//            this.button4.Size = new System.Drawing.Size(176, 60);
//            this.button4.TabIndex = 6;
//            this.button4.Text = "立即刷新";
//            this.button4.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            // 
//            // button3
//            // 
//            this.button3.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.button3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.button3.Location = new System.Drawing.Point(238, 225);
//            this.button3.MinimumSize = new System.Drawing.Size(1, 1);
//            this.button3.Name = "button3";
//            this.button3.Size = new System.Drawing.Size(176, 60);
//            this.button3.TabIndex = 6;
//            this.button3.Text = "添加用户";
//            this.button3.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            // 
//            // button2
//            // 
//            this.button2.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.button2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.button2.Location = new System.Drawing.Point(555, 136);
//            this.button2.MinimumSize = new System.Drawing.Size(1, 1);
//            this.button2.Name = "button2";
//            this.button2.Size = new System.Drawing.Size(176, 60);
//            this.button2.TabIndex = 6;
//            this.button2.Text = "添加用户";
//            this.button2.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            // 
//            // button1
//            // 
//            this.button1.Cursor = System.Windows.Forms.Cursors.Hand;
//            this.button1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.button1.Location = new System.Drawing.Point(555, 37);
//            this.button1.MinimumSize = new System.Drawing.Size(1, 1);
//            this.button1.Name = "button1";
//            this.button1.Size = new System.Drawing.Size(176, 60);
//            this.button1.TabIndex = 6;
//            this.button1.Text = "查询用户";
//            this.button1.TipsFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            // 
//            // textBox1
//            // 
//            this.textBox1.Cursor = System.Windows.Forms.Cursors.IBeam;
//            this.textBox1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.textBox1.Location = new System.Drawing.Point(26, 225);
//            this.textBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//            this.textBox1.MinimumSize = new System.Drawing.Size(1, 16);
//            this.textBox1.Name = "textBox1";
//            this.textBox1.Padding = new System.Windows.Forms.Padding(5);
//            this.textBox1.ShowText = false;
//            this.textBox1.Size = new System.Drawing.Size(181, 42);
//            this.textBox1.TabIndex = 4;
//            this.textBox1.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
//            this.textBox1.Watermark = "";
//            // 
//            // textBox3
//            // 
//            this.textBox3.Cursor = System.Windows.Forms.Cursors.IBeam;
//            this.textBox3.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.textBox3.Location = new System.Drawing.Point(221, 128);
//            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//            this.textBox3.MinimumSize = new System.Drawing.Size(1, 16);
//            this.textBox3.Name = "textBox3";
//            this.textBox3.Padding = new System.Windows.Forms.Padding(5);
//            this.textBox3.ShowText = false;
//            this.textBox3.Size = new System.Drawing.Size(292, 60);
//            this.textBox3.TabIndex = 4;
//            this.textBox3.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
//            this.textBox3.Watermark = "";
//            // 
//            // textBox2
//            // 
//            this.textBox2.Cursor = System.Windows.Forms.Cursors.IBeam;
//            this.textBox2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.textBox2.Location = new System.Drawing.Point(221, 37);
//            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
//            this.textBox2.MinimumSize = new System.Drawing.Size(1, 16);
//            this.textBox2.Name = "textBox2";
//            this.textBox2.Padding = new System.Windows.Forms.Padding(5);
//            this.textBox2.ShowText = false;
//            this.textBox2.Size = new System.Drawing.Size(292, 60);
//            this.textBox2.TabIndex = 5;
//            this.textBox2.TextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
//            this.textBox2.Watermark = "";
//            // 
//            // label2
//            // 
//            this.label2.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
//            this.label2.Location = new System.Drawing.Point(20, 136);
//            this.label2.Name = "label2";
//            this.label2.Size = new System.Drawing.Size(154, 41);
//            this.label2.TabIndex = 2;
//            this.label2.Text = "ID";
//            // 
//            // label1
//            // 
//            this.label1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
//            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(48)))), ((int)(((byte)(48)))), ((int)(((byte)(48)))));
//            this.label1.Location = new System.Drawing.Point(20, 45);
//            this.label1.Name = "label1";
//            this.label1.Size = new System.Drawing.Size(154, 41);
//            this.label1.TabIndex = 3;
//            this.label1.Text = "账号";
//            // 
//            // StudentList
//            // 
//            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
//            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
//            this.ClientSize = new System.Drawing.Size(1412, 979);
//            this.Controls.Add(this.groupBox1);
//            this.Controls.Add(this.dataGridView1);
//            this.Controls.Add(this.uiNavMenu1);
//            this.Name = "StudentList";
//            this.Text = "StudentList";
//            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
//            this.groupBox1.ResumeLayout(false);
//            this.ResumeLayout(false);

//        }

//        #endregion

//        private Sunny.UI.UINavMenu uiNavMenu1;
//        private System.ComponentModel.BackgroundWorker backgroundWorker1;
//        private Sunny.UI.UIDataGridView dataGridView1;
//        private Sunny.UI.UICheckBoxGroup groupBox1;
//        private Sunny.UI.UITextBox textBox3;
//        private Sunny.UI.UITextBox textBox2;
//        private Sunny.UI.UILabel label2;
//        private Sunny.UI.UILabel label1;
//        private Sunny.UI.UIButton button4;
//        private Sunny.UI.UIButton button2;
//        private Sunny.UI.UIButton button1;
//        private Sunny.UI.UIButton button3;
//        private Sunny.UI.UITextBox textBox1;
//    }
//}