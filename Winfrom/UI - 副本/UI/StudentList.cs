
using model;
using Model;

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using 示例;



namespace UI
{
    public partial class StudentList : Form
    {
        #region 分页初始变量
        int userCurrentPage = 1;
        int userPageSize = 10;
        int userTotalPage = 0;
        int userId = -1;
        string UserName = "";
        string UserWhere = null;

        int customerCurrentPage = 1;
        int customerPageSize = 10;
        int customerTotalPage = 0;
        int customerId = -1;
        string customerName = "";
        string customerWhere = null;


        int customerAddressCurrentPage = 1;
        int customerAddressPageSize = 10;
        int customerAddressTotalPage = 0;
        int customerAddressId = -1;
        string customerAddressName = "";
        string customerAddressWhere = null;

        #endregion

        #region 初始化实例
        // 在类的顶部定义（窗体类内部）
        private static readonly SolidBrush BrushEditBack = new SolidBrush(Color.FromArgb(227, 242, 253)); // #E3F2FD
        private static readonly SolidBrush BrushDelBack = new SolidBrush(Color.FromArgb(255, 235, 238)); // #FFEBEE

        UserBLL userBLL = new UserBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        public static StudentList Instance { get; set; }
        public static Customer customer1 = null;
        public static Address address1 = null;
        LogBLL logBLL = new LogBLL();
        #endregion

        #region 登录初始化
        public StudentList(string str)
        {

            InitializeComponent();
            textBox1.Text = str;
            InitDataGridView();

            if (str != "游客登录")
            {

                UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
            }
            else
            {
                button1.Enabled = false;
                button2.Enabled = false;

                menuStrip1.Enabled = false;
            }
            Instance = this;
        }
        #endregion
        #region 数据渲染
        /// <summary>
        /// 测试数据
        /// </summary>
        /// <param name="list"></param>
        /// <param name="isShow"> 不显示个人信息</param>
        private void UserDateTest(List<User> list, bool isShow = true)
        {
            //dataGridView1.Rows.Add("值1", "值2", "值3");

            dataGridView1.Rows.Clear();
            if (isShow == true)
            {
                //item.Id != UserDAL.userId ||
                if (list == null) return;
                foreach (User item in list)
                {


                    dataGridView1.Rows.Add(item.Id, item.Name, item.Account, item.Password, item.Grade == "管理员" ? true : false);
                }
            }
            else
            {
                if (list == null) return;
                foreach (var item in list)
                {
                    dataGridView1.Rows.Add(item.Id, item.Name, item.Account, item.Password, item.Grade == "管理员" ? true : false);
                }
            }
            //显示当前页数
            lblCurrenPageAndTotalPage.Text = userCurrentPage + "/" + userTotalPage;
            //添加按钮
            LoadButton(userTotalPage);
        }

        #endregion


        #region 初始化控件及数据源
        public void InitDataGridView()
        {

            //隐藏行头
            dataGridView1.RowHeadersVisible = false;
            //禁止多选
            dataGridView1.MultiSelect = false;
            //禁止用户编辑
            dataGridView1.ReadOnly = true;
            // 禁止用户调整行高
            dataGridView1.AllowUserToResizeRows = false;
            //禁止用户调整列宽
            dataGridView1.AllowUserToOrderColumns = false;
            //自动填满表格
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            //添加默认列
            dataGridView1.Columns.Add("ID", "编号");
            dataGridView1.Columns.Add("Name", "姓名");
            dataGridView1.Columns.Add("Age", "账号");
            dataGridView1.Columns.Add("Sex", "密码");

            //添加 复选框列
            DataGridViewCheckBoxColumn IsGraduate = new DataGridViewCheckBoxColumn();
            IsGraduate.Name = "IsGraduate";
            IsGraduate.HeaderText = "是否管理员";
            dataGridView1.Columns.Add(IsGraduate);
            listBox1.Hide();
            InitColumnsOperation();



            //是否显示按钮在列中
            //operation_1.UseColumnTextForButtonValue = true;
        }

        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //当前正在绘制的那个单元格的列索引
            //e.ColumnIndex

            if (e.ColumnIndex >= 0 && e.RowIndex >= 0)
                if (dataGridView1.Columns[e.ColumnIndex].Name == "OperateColumn")
                {
                    //获取鼠标在当前单元格中的坐标
                    Point curPosition = e.Location;
                    //创建绘图对象
                    Graphics g = dataGridView1.CreateGraphics();
                    using (Font font = new Font("宋体", 10, FontStyle.Bold))
                    {
                        //获取固定文字的宽高,单位像素
                        SizeF size = g.MeasureString("删除", font);
                        SizeF size1 = g.MeasureString("详情", font);

                        // 5. 定义左右内边距（让文字不贴边，视觉上更舒适）
                        float padding = 10;
                        float totalWidth = size.Width + size1.Width;
                        //实际宽度
                        float availableWidth = this.dataGridView1.Columns[e.ColumnIndex].Width - padding * 2;

                        //计算比例
                        float ratioDel = size.Width / totalWidth;
                        float ratioEdit = size1.Width / totalWidth;



                        // 8. 计算第一个文字（“删除”）区域的起始 X 坐标（从左边界加上左内边距）
                        float leftX = padding;

                        // 9. 计算每个文字区域的实际宽度（可用宽度 * 各自比例）
                        float widthDel = availableWidth * ratioDel;
                        float widthEdit = availableWidth * ratioEdit;
                        //会执矩形
                        RectangleF rectDel = new RectangleF(leftX, 0, widthDel, this.dataGridView1.Rows[e.RowIndex].Height);
                        RectangleF rectEdit = new RectangleF(leftX + widthDel, 0, widthEdit, this.dataGridView1.Rows[e.RowIndex].Height);
                        //删除
                        if (rectDel.Contains(curPosition))
                        {
                            if (button1.Text == "查询用户")
                            {

                                var DialogResult = MessageBox.Show("确认删除吗?", "删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                                if (DialogResult != DialogResult.Yes) return;

                                userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                                if (button1.Text == "查询用户" && userBLL.deleteUser(userId))
                                {
                                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                                }
                            }
                        }

                        if (rectEdit.Contains(curPosition))
                        {
                            if (button1.Text == "查询用户")
                            {
                                userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                                string where = " and Id = @Id";


                                List<User> list1 = userBLL.GetAllUser(where, 1, out userTotalPage, userPageSize, userId, "", "Id desc");
                                User user = list1[0];

                                Form_Load("用户详情", "", user);
                            }
                        }

                    }
                }

        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            // 只处理数据行（RowIndex >= 0），跳过表头（RowIndex == -1）和行头（ColumnIndex == -1）
            if (e.RowIndex >= 0 && dataGridView1.Columns[e.ColumnIndex].Name == "OperateColumn")
                //判断要详细会执的列
                if (dataGridView1.Columns[e.ColumnIndex].Name == "OperateColumn")
                {
                    //参数1: 会执的矩形边界,参数2:true : 选中时高亮
                    e.PaintBackground(e.CellBounds, true);
                    //格式化文字
                    StringFormat sf = StringFormat.GenericDefault.Clone() as StringFormat;//设置重绘入单元格的字体样式
                                                                                          //水平居中
                    sf.Alignment = StringAlignment.Center;
                    //垂直居中
                    sf.LineAlignment = StringAlignment.Center;

                    //释放资源
                    using (Font font = new Font("宋体", 10, FontStyle.Bold))
                    {


                        //会执区域,返回宽高
                        //计算文字的大小, 返回像素单位
                        SizeF size = e.Graphics.MeasureString("删除", font);
                        SizeF size1 = e.Graphics.MeasureString("详情", font);
                        //计算文字的总宽度
                        float totalWidth = size.Width + size1.Width;

                        // 5. 定义左右内边距（让文字不贴边，视觉上更舒适）
                        float padding = 10;

                        // 6. 计算单元格内可供文字使用的实际宽度（总宽度减去左右留白）
                        float availableWidth = e.CellBounds.Width - padding * 2;

                        // 7. 根据每个文本的自然宽度计算其应占的比例
                        //    例如：如果“删除”宽 30px，“编辑”宽 40px，总宽 70px，
                        //    则 ratioDel ≈ 0.428，ratioEdit ≈ 0.572
                        float ratioDel = size.Width / totalWidth;
                        float ratioEdit = size1.Width / totalWidth;

                        // 8. 计算第一个文字（“删除”）区域的起始 X 坐标（从左边界加上左内边距）
                        float leftX = e.CellBounds.Left + padding;

                        // 9. 计算每个文字区域的实际宽度（可用宽度 * 各自比例）
                        float widthDel = availableWidth * ratioDel;
                        float widthEdit = availableWidth * ratioEdit;







                        ////会执两个文本宽度比率
                        //float ratio = size.Width / (size1.Width + size.Width);
                        //float ratio1 = size1.Width / (size1.Width + size.Width);

                        //矩形区域 : 单元格的一半区域
                        //RectangleF rect = new RectangleF(e.CellBounds.Left, e.CellBounds.Top, e.CellBounds.Width * ratio, e.CellBounds.Height);

                        //RectangleF rectDel = new RectangleF(
                        //       rect.Right,
                        //       e.CellBounds.Top, e.CellBounds.Width * ratio1, e.CellBounds.Height);

                        //     - rectEdit：紧接着 rectDel 的右侧，宽度为剩余比例
                        RectangleF rectDel = new RectangleF(leftX, e.CellBounds.Top, widthDel, e.CellBounds.Height);
                        RectangleF rectEdit = new RectangleF(leftX + widthDel, e.CellBounds.Top, widthEdit, e.CellBounds.Height);

                        //添加背景

                        //e.Graphics.FillRectangle(Brushes.Aqua, rectEdit);
                        //e.Graphics.FillRectangle(Brushes.Beige, rectDel);

                        // 先画背景色（这样就可以看到“按钮”的底色）
                        e.Graphics.FillRectangle(BrushEditBack, rectEdit);  // 编辑区域变淡蓝
                        e.Graphics.FillRectangle(BrushDelBack, rectDel);    // 删除区域变淡粉

                        // 然后在上面画文字（黑色和红色）
                        e.Graphics.DrawString("详情", font, Brushes.Black, rectEdit, sf);
                        e.Graphics.DrawString("删除", font, Brushes.Red, rectDel, sf);
                        //绘制文字
                        //e.Graphics.DrawString("删除", font, BrushDelBack, rectDel, sf);
                        //e.Graphics.DrawString("详情", font, BrushEditBack, rectEdit, sf);
                        // 13. 关键步骤：将事件标记为“已处理”
                        //     告诉 DataGridView 不再执行默认的单元格绘制逻辑（否则默认绘制的文本会覆盖我们的自定义内容）
                        e.Handled = true;
                    }
                }

        }
        /// <summary>
        /// 初始化操作列
        /// </summary>
        private void InitColumnsOperation()
        {

            //添加 操作列//编辑
            DataGridViewButtonColumn operation_0 = new DataGridViewButtonColumn();
            operation_0.Name = "操作1";
            operation_0.Text = "编辑";
            //决定了按钮上显示的文字来源。
            operation_0.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(operation_0);

            //添加 操作列 删除
            DataGridViewButtonColumn operation_1 = new DataGridViewButtonColumn();
            operation_1.Name = "操作2";
            operation_1.Text = "删除";
            operation_1.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(operation_1);

            //添加 操作列 详情
            DataGridViewButtonColumn operation_2 = new DataGridViewButtonColumn();
            operation_2.Name = "操作3";
            operation_2.Text = "详情";
            operation_2.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(operation_2);

            DataGridViewLinkColumn column1 = new DataGridViewLinkColumn();
            column1.Name = "OperateColumn";
            column1.HeaderText = "操作4";
            //column1.Text = "超链接";
            //column1.UseColumnTextForLinkValue = true;
            dataGridView1.Columns.Add(column1);

        }

        #endregion
        #region 查询
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "查询用户")
            {
                //使用名字查询
                if (!string.IsNullOrEmpty(textBox2.Text.Trim()))
                {
                    UserWhere = $" and Name like @Name";
                    UserName = $"%{textBox2.Text}%";
                    userCurrentPage = 1;

                    userTotalPage = 0;
                    UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName, "Id desc"));

                    textBox2.Text = "";
                }
                else if (!string.IsNullOrEmpty(textBox3.Text.Trim()))
                {//使用id查询

                    try
                    {

                        User u = userBLL.GetUser(Convert.ToInt32(textBox3.Text.Trim()));
                        if (u != null)
                        {
                            UserDateTest(new List<User>() { u });
                        }
                    }
                    catch (Exception ex)
                    {

                        textBox4.Text = ex.Message;
                    }


                    textBox3.Text = "";
                }
            }
            else if (button1.Text == "查询客户")
            {
                //使用名字查询
                if (!string.IsNullOrEmpty(textBox2.Text))
                {
                    customerWhere = $" and Name like @Name";
                    customerName = $"%{textBox2.Text.Trim()}%";
                    customerId = -1;

                    customerTotalPage = 0;

                    DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));

                    textBox2.Text = "";
                }
                else if (!string.IsNullOrEmpty(textBox3.Text))
                {//使用id查询
                    try
                    {

                        customerWhere = $" and  @Id=Id";

                        customerId = Convert.ToInt32(textBox3.Text.Trim());

                        customerTotalPage = 0;
                        DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, ""));
                        textBox3.Text = "";
                    }
                    catch (Exception ex)
                    {

                        textBox4.Text = ex.Message;
                    }
                }
            }
            else if (button1.Text == "查询地址")
            {
                try
                {

                    if (!string.IsNullOrEmpty(textBox3.Text))
                    {
                        //使用id查询
                        customerAddressWhere = $" and  @Id=Id";
                        customerAddressId = Convert.ToInt32(textBox3.Text.Trim());
                        customerAddressName = "";
                        customerAddressCurrentPage = 1;

                        DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));

                    }
                    else if (!string.IsNullOrEmpty(textBox2.Text))
                    {
                        customerAddressWhere = $" and Address like @Address";
                        customerAddressName = $"%{textBox2.Text.Trim()}%";
                        customerAddressId = -1;


                        DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));

                    }
                }
                catch (Exception ex)
                {

                    textBox4.Text = ex.Message;
                }


            }


        }
        #endregion
        #region 退出登录

        /// <summary>
        /// 登出
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            FormClose();
            this.Close();

        }
        private void FormClose()
        {
            Form target = Application.OpenForms["Login1"];
            if (target != null && userBLL.logout())
            {
                Login1 form = target as Login1;
                form.Show();
            }
        }
        #endregion
        #region 添加
        private void button2_Click(object sender, EventArgs e)
        {
            if (button2.Text == "添加用户")
            {
                Form_Load("添加用户", "添加");
            }
            else if (button2.Text == "添加客户")
            {
                Form_Load("添加客户", "添加");
            }
            else if (button2.Text == "添加客户")
            {
                Form_Load("添加客户", "添加");
            }
            else if (button2.Text == "添加地址")
            {
                Addressoperation addressoperation = new Addressoperation("添加");
                addressoperation.Show();
                this.Hide();

            }


        }
        #endregion
        #region 表格行点击事件 编辑 删除 详情
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //string str= dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString();
            //int i =  Convert.ToInt32( str);
            try
            {
                #region 编辑
                if (e.ColumnIndex == dataGridView1.Columns["操作1"].Index && e.ColumnIndex >= 0)
                {
                    if (button1.Text == "查询用户")
                    {

                        userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";


                        List<User> list1 = userBLL.GetAllUser(where, 1, out userTotalPage, userPageSize, userId, "", "Id desc");
                        User user = list1[0];
                        //MessageBox.Show(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value).ToString());
                        Form_Load("编辑用户", "保存", user);
                    }
                    else if (button1.Text == "查询客户")
                    {
                        customerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";
                        customer1 = customerBLL.GetAlldal(where, 1, out customerTotalPage, customerPageSize, customerId, "")[0];
                        Form_Load("编辑客户", "保存");

                    }
                    else if (button1.Text == "查询地址")
                    {
                        customerAddressId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";
                        address1 = customerBLL.ShowAddress(where, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, "")[0];
                        Addressoperation addressoperation = new Addressoperation("修改地址");
                        this.Hide();
                        if (addressoperation.DialogResult == DialogResult.OK)
                        {
                            DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, 1, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                        }

                    }
                }
                #endregion
                #region 删除
                else if (e.ColumnIndex == dataGridView1.Columns["操作2"].Index && e.ColumnIndex >= 0)
                {
                    var DialogResult = MessageBox.Show("确认删除吗?", "删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (DialogResult == DialogResult.Yes)
                    {
                        userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());

                        if (button1.Text == "查询用户" && userBLL.deleteUser(userId))
                        {
                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else if (button1.Text == "查询客户")
                        {
                            customerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                            customerBLL.Deletedal(customerId);

                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else if (button1.Text == "查询地址")
                        {
                            //MessageBox.Show(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                            if (customerBLL.DeleteAddress(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                            {

                                dataGridView1.Rows.RemoveAt(e.RowIndex);
                            }
                        }

                    }
                }
                #endregion
                #region 详情
                else if (e.ColumnIndex == dataGridView1.Columns["操作3"].Index && e.ColumnIndex >= 0)
                {
                    if (button1.Text == "查询用户")
                    {

                        userId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";


                        List<User> list1 = userBLL.GetAllUser(where, 1, out userTotalPage, userPageSize, userId, "", "Id desc");
                        User user = list1[0];

                        Form_Load("用户详情", "", user);
                    }
                    else if (button1.Text == "查询客户")
                    {
                        customerId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";
                        customer1 = customerBLL.GetAlldal(where, 1, out customerTotalPage, customerPageSize, customerId, "")[0];

                        Form_Load("客户详情", "");
                    }
                    else if (button1.Text == "查询地址")
                    {

                        customerAddressId = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString());
                        string where = " and Id = @Id";
                        address1 = customerBLL.ShowAddress(where, 1, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, "")[0];
                        this.Hide();
                        Addressoperation addressoperation = new Addressoperation("地址详情");
                        if (addressoperation.ShowDialog() == DialogResult.OK)
                        {
                            DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                        }

                    }
                }
                #endregion
            }
            catch (Exception)
            {

                MessageBox.Show("请选择行");
            }

        }
        #endregion

        #region 加载其他窗体
        /// <summary>
        /// 加载其他窗体
        /// </summary>
        /// <param name="btnText"></param>
        /// <param name="str"></param>
        /// <param name="user"></param>
        private void Form_Load(string str, string btnText, params User[] user)
        {
            //当登录类为主线程时,当前页面可关闭, 现在为线程, 不可关闭 隐藏
            this.Hide();
            SelectStudent selectStudent = new SelectStudent(str, btnText, user);
            if (selectStudent.ShowDialog() == DialogResult.OK)
            {


                if (button1.Text == "查询用户")
                {

                    UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));

                }
                else if (button1.Text == "查询客户")
                {
                    DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
                }

            }

        }

        private void StudentList_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormClose();
        }
        #endregion




        #region 表格初始化
        private void ClearDataGridView()
        {
            //清除表格
            dataGridView1.Rows.Clear();
            dataGridView1.Columns.Clear();

        }
        #endregion

        #region 切换用户视图
        private void 切换用户视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ClearDataGridView();
            // 初始化表格
            InitDataGridView();
            // 加载数据
            UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName, "Id desc"));

            //控件更改
            button1.Text = "查询用户";
            button2.Text = "添加用户";
            label2.Text = "姓名";
            label2.Show();
            textBox2.Show();
            listBox1.Hide();
            button2.Show();


        }
        #endregion
        #region 切换客户视图
        /// <summary>
        /// 切换客户视图
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void 切换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            #region 表格数据加载与初始化
            //清除表格
            ClearDataGridView();
            // 初始化表格
            InitDataGridViewCustomer();
            // 加载数据
            DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
            //控件更改
            InitControlCustomer();
            #endregion

        }


        #region 客户表列名
        public void InitDataGridViewCustomer()
        {
            //隐藏行头
            dataGridView1.RowHeadersVisible = false;
            //禁止多选
            dataGridView1.MultiSelect = false;
            //禁止用户编辑
            dataGridView1.ReadOnly = true;
            //添加默认列
            dataGridView1.Columns.Add("ID", "编号");
            dataGridView1.Columns.Add("Name", "姓名");
            dataGridView1.Columns.Add("Phone", "手机");
            dataGridView1.Columns.Add("Balance", "余额");

            InitColumnsOperation();


            //是否显示按钮在列中
            //operation_1.UseColumnTextForButtonValue = true;
        }
        #endregion
        #region 客户数据渲染
        private void DateTestCustomer(List<Customer> list, bool isShow = true)
        {
            //dataGridView1.Rows.Add("值1", "值2", "值3");

            dataGridView1.Rows.Clear();
            foreach (var item in list)
            {
                dataGridView1.Rows.Add(item.Id, item.Name, item.Phone, item.Balance);
            }
            //显示当前页数
            lblCurrenPageAndTotalPage.Text = customerCurrentPage + "/" + customerTotalPage;
            //添加按钮
            LoadButton(customerTotalPage);
        }

        private void InitControlCustomer()
        {
            label2.Show();
            textBox2.Show();
            listBox1.Hide();
            label2.Text = "姓名";
            button1.Text = "查询客户";
            button2.Text = "添加客户";
            button2.Show();

        }
        #endregion
        #endregion

        #region 客户地址视图
        private void 查看客户地址视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {

            InitCustomerAddress();
            InitDataGridViewCustomerAddress();

            //省市级 下拉列表框初始化
            InitComBox();

            //数据渲染
            DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));

        }
        #region 初始化控件
        /// <summary>
        /// 初始化控件文本
        /// </summary>
        /// 
        private void InitCustomerAddress()
        {

            label2.Text = "地址名称";
            button1.Text = "查询地址";
            button2.Text = "添加地址";
            button2.Show();

            listBox1.Hide();

        }

        /// <summary>
        /// 初始化表格
        /// </summary>
        private void InitDataGridViewCustomerAddress()
        {
            ClearDataGridView();
            //隐藏行头
            dataGridView1.RowHeadersVisible = false;
            //禁止多选
            dataGridView1.MultiSelect = false;
            //禁止用户编辑
            dataGridView1.ReadOnly = true;
            //添加默认列
            dataGridView1.Columns.Add("ID", "编号");
            dataGridView1.Columns.Add("Address", "地址");
            InitColumnsOperation();
        }
        #region 地址数据渲染
        /// <summary>
        /// 数据渲染
        /// </summary>
        /// <param name="list"></param>
        private void DateTestCustomerAddress(List<Address> list)
        {
            dataGridView1.Rows.Clear();
            if (list == null)
            {
                MessageBox.Show("没有客户地址");
                return;
            }
            foreach (var item in list)
            {
                dataGridView1.Rows.Add(item.Id, item.Add);
            }
            //显示当前页数
            lblCurrenPageAndTotalPage.Text = customerAddressCurrentPage + "/" + customerAddressTotalPage;
            //添加按钮
            LoadButton(customerAddressTotalPage);

        }
        #endregion


        #region 省 市 县
        public void InitComBox()
        {
            comboBox1.Text = "选择省";
            comboBox2.Text = "选择市";
            comboBox3.Text = "选择县";
            foreach (var item in provinces)
            {
                comboBox1.Items.Add(item);
            }
        }

        List<string> provinces = new List<string>
        {
            "广东省", "江苏省", "浙江省","湖北省","福建省",
        };

        Dictionary<string, List<string>> keys = new Dictionary<string, List<string>>();
        /// <summary>
        /// 获取省份与市级城市字典数据
        /// </summary>
        public Dictionary<string, List<string>> GetProvinceCityData()
        {
            return new Dictionary<string, List<string>>
                {
                    { "广东省", new List<string> { "广州市", "深圳市", "珠海市", "汕头市", "佛山市", "东莞市", "中山市", "惠州市" } },
                    { "江苏省", new List<string> { "南京市", "苏州市", "无锡市", "常州市", "南通市", "徐州市", "扬州市", "盐城市" } },
                    { "浙江省", new List<string> { "杭州市", "宁波市", "温州市", "嘉兴市", "绍兴市", "金华市", "台州市", "温州市" } },
                    {  "四川省", new List<string> { "成都市", "绵阳市", "德阳市", "宜宾市", "南充市", "乐山市", "泸州市", "自贡市" } },
                    { "湖北省", new List<string> { "武汉市", "宜昌市", "襄阳市", "荆州市", "黄石市", "十堰市", "荆门市", "孝感市" } },
                    { "福建省", new List<string> { "福州市", "厦门市", "泉州市", "漳州市", "莆田市", "龙岩市", "三明市", "南平市" } }
             };

        }

        Dictionary<string, List<string>> cityCountyDict = new Dictionary<string, List<string>>
{
    // 福建省
    { "福州市", new List<string> { "闽侯县", "连江县", "罗源县", "闽清县", "永泰县", "平潭县" } },
    { "泉州市", new List<string> { "惠安县", "安溪县", "永春县", "德化县" } },
    { "漳州市", new List<string> { "漳浦县", "诏安县", "长泰县", "东山县", "南靖县", "平和县", "华安县" } },

    // 四川省
    { "成都市", new List<string> { "金堂县", "大邑县", "蒲江县" } },
    { "绵阳市", new List<string> { "三台县", "盐亭县", "梓潼县", "平武县", "北川羌族自治县" } },
    { "宜宾市", new List<string> { "江安县", "长宁县", "高县", "珙县", "筠连县", "兴文县", "屏山县" } },

    // 湖北省
    { "宜昌市", new List<string> { "远安县", "兴山县", "秭归县", "长阳土家族自治县", "五峰土家族自治县" } },
    { "襄阳市", new List<string> { "南漳县", "谷城县", "保康县" } },
    { "黄冈市", new List<string> { "团风县", "红安县", "罗田县", "英山县", "浠水县", "蕲春县", "黄梅县" } },

    // 浙江省
    { "杭州市", new List<string> { "桐庐县", "淳安县" } },
    { "宁波市", new List<string> { "象山县", "宁海县" } },
    { "温州市", new List<string> { "永嘉县", "平阳县", "苍南县", "文成县", "泰顺县" } },

    // 江苏省
    { "徐州市", new List<string> { "丰县", "沛县", "睢宁县" } },
    { "连云港市", new List<string> { "东海县", "灌云县", "灌南县" } },
    { "盐城市", new List<string> { "响水县", "滨海县", "阜宁县", "射阳县", "建湖县" } },

    // 广东省
    { "韶关市", new List<string> { "始兴县", "仁化县", "翁源县", "乳源瑶族自治县", "新丰县" } },
    { "惠州市", new List<string> { "博罗县", "惠东县", "龙门县" } },
    { "梅州市", new List<string> { "大埔县", "丰顺县", "五华县", "平远县", "蕉岭县" } }
};
        #endregion
        #endregion

        #endregion
        #region 数据备份
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (userBLL.BlackDate())
            {
                textBox4.Text = "备份成功";
            }
            else
            {
                textBox4.Text = "备份失败";
            }
        }
        #endregion

        #region 个人信息
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            label2.Text = "姓名";
            button1.Text = "查询用户";
            button2.Text = "添加用户";
            listBox1.Hide();
            //刷新按钮
            button2.Hide();

            List<User> u = new List<User>()
                {
                    userBLL.Personalinfo()
                };
            ClearDataGridView();
            // 初始化表格
            InitDataGridView();
            InitColumnsOperation();
            // 加载数据
            UserDateTest(u, false);


        }

        #endregion

        private void StudentList_Load(object sender, EventArgs e)
        {
            InitControl();



        }
        #region 全局控件初始化
        private void InitControl()
        {
            toolStripStatusLabel2.Text = LoggedInUser.user.Name;
            cbbPageSize.Items.Add(5);
            cbbPageSize.Items.Add(10);
            cbbPageSize.Items.Add(15);
            cbbPageSize.Text = cbbPageSize.Items[1].ToString();
            cbbPageSize.DropDownStyle = ComboBoxStyle.DropDownList;
            textBox4.Text = userTotalPage.ToString() + "页";
        }
        #endregion


        #region 日志查询
        private void 用户日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Show();
            listBox1.Items.Clear();
            List<Logs> log = logBLL.LogOperrate();
            if (log == null)
            {
                return;
            }

            foreach (var item in log)
            {
                listBox1.Items.Add(item);

            }

        }

        private void 客户日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Show();
            listBox1.Items.Clear();
            List<Logs> log = logBLL.LogTrade();
            if (log == null)
            {
                MessageBox.Show("没有客户日志");
                return;
            }

            foreach (var item in log)
            {
                listBox1.Items.Add(item.LogOperate());
            }
        }
        #endregion
        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }
        #region 地址 省 市 县
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox1.Text;
            Dictionary<string, List<String>> Shi = GetProvinceCityData();
            foreach (var item in Shi.Keys)
            {
                if (item == str)
                {
                    foreach (var item2 in Shi[item])
                    {

                        comboBox2.Items.Add(item2);
                    }
                }
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = comboBox2.Text;
            //cityCountyDict
            foreach (var item in cityCountyDict.Keys)
            {
                if (str == item)
                {
                    foreach (var item1 in cityCountyDict[item])
                    {
                        comboBox3.Items.Add(item1);
                    }
                }
            }
        }
        #endregion
        #region 计时器_更改底部状态栏时间
        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabel3.Text = DateTime.Now.ToString();
        }
        #endregion
        #region 更改查询条数 combox

        private void cbbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (button1.Text == "查询用户")
            {
                userPageSize = Convert.ToInt32(cbbPageSize.Text);

            }
            else if (button1.Text == "查询客户")
            {
                customerPageSize = Convert.ToInt32(cbbPageSize.Text);
            }
            else if (button1.Text == "查询地址")
            {
                customerAddressPageSize = Convert.ToInt32(cbbPageSize.Text);
            }
            else if (button1.Text == "查询日志")
            {

            }
        }
        #endregion
        #region 分页查询

        #region 分页 首页
        /// <summary>
        /// 分页首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            //用户首页
            if (button1.Text == "查询用户")
            {
                UserWhere = "";
                userId = -1;
                UserName = "";
                userCurrentPage = 1;
                UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
                //加载控件 页码
            }
            else if (button1.Text == "查询客户")
            {
                customerWhere = "";
                customerId = -1;
                customerName = "";
                customerCurrentPage = 1;
                DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
            }
            else if (button1.Text == "查询地址")
            {
                customerAddressWhere = "";
                customerAddressId = -1;
                customerAddressName = "";
                customerAddressCurrentPage = 1;

                DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));

            }
            else if (button1.Text == "查询日志")
            {

            }
        }
        #endregion

        #region 分页 上一页
        private void btnPrev_Click(object sender, EventArgs e)
        {
            //更新 lblCurrenPageAndTotalPage

            if (button1.Text == "查询用户")
            {
                if (userCurrentPage > 1 && userCurrentPage <= userTotalPage)
                {
                    userCurrentPage -= 1;
                    UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));

                }

            }
            else if (button1.Text == "查询客户")
            {
                if (customerCurrentPage > 1 && customerCurrentPage <= customerTotalPage)
                {
                    customerCurrentPage -= 1;
                    DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                }
            }
            else if (button1.Text == "查询地址")
            {
                if (customerAddressCurrentPage > 1)
                {
                    customerAddressCurrentPage -= 1;
                    DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                }

            }
            else if (button1.Text == "查询日志")
            {

            }
        }


        #endregion


        #region 分页 下一页
        private void btnNext_Click(object sender, EventArgs e)
        {
            //更新 lblCurrenPageAndTotalPage
            if (button1.Text == "查询用户")
            {
                if (userCurrentPage < userTotalPage)
                {
                    userCurrentPage += 1;
                    UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
                }
                //加载控件 页码
            }
            else if (button1.Text == "查询客户")
            {
                if (customerCurrentPage < customerTotalPage)
                {
                    customerCurrentPage += 1;
                    DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
                }

            }
            else if (button1.Text == "查询地址")
            {
                if (customerAddressCurrentPage < customerAddressTotalPage)
                {
                    customerAddressCurrentPage += 1;
                    DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                }

            }
            else if (button1.Text == "查询日志")
            {

            }
        }
        #endregion
        #region 分页 尾页
        private void btnLast_Click(object sender, EventArgs e)
        {
            //更新 lblCurrenPageAndTotalPage
            if (button1.Text == "查询用户")
            {
                if (userCurrentPage < userTotalPage)
                {
                    userCurrentPage = userTotalPage;
                    UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
                }
                //加载控件 页码
            }
            else if (button1.Text == "查询客户")
            {
                if (customerCurrentPage < customerTotalPage)
                {
                    customerCurrentPage = customerTotalPage;
                    DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
                }
            }
            else if (button1.Text == "查询地址")
            {
                if (customerAddressCurrentPage < customerAddressTotalPage)
                {
                    customerAddressCurrentPage = customerAddressTotalPage;
                    DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                }

            }
            else if (button1.Text == "查询日志")
            {


            }
        }


        #endregion


        #region 分页 跳转
        private void btnGo_Click(object sender, EventArgs e)
        {
            //更新 lblCurrenPageAndTotalPage
            try
            {
                int temp = Convert.ToInt32(txtCurrentpage.Text);

                if (button1.Text == "查询用户")
                {
                    if (temp > 0 && temp <= userTotalPage)
                    {
                        userCurrentPage = temp;
                        UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
                    }
                    //加载控件 页码
                }
                else if (button1.Text == "查询客户")
                {

                    if (temp > 0 && temp <= customerTotalPage)
                    {
                        customerCurrentPage = temp;
                        DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));
                    }
                }
                else if (button1.Text == "查询地址")
                {
                    if (temp > 0 && temp <= customerAddressTotalPage)
                    {
                        customerAddressCurrentPage = temp;
                        DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));
                    }
                }
                else if (button1.Text == "查询日志")
                {

                }
            }
            catch (Exception ex)
            {

                textBox4.Text = ex.Message;
            }

        }
        #endregion
        #region 分页 添加按钮

        private void LoadButton(int totalPage)
        {
            groupBox2.Controls.Clear();
            int btnWidth = 70;
            int btnHeight = 50;
            int pointX = -80;
            int pointY = 40;
            for (int i = 0; i < totalPage; i++)
            {
                Button button = new Button();
                button.Text = (i + 1).ToString();
                button.Size = new System.Drawing.Size(btnWidth, btnHeight);

                button.Location = new System.Drawing.Point(pointX += 80, pointY);
                button.Click += new System.EventHandler(this.button_Click);
                if (groupBox2.Width <= pointX + btnWidth)
                {
                    pointY += 70;
                    pointX = -80;
                }
                groupBox2.Controls.Add(button);
            }
        }
        /// <summary>
        /// 动态生成的按钮绑定事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <exception cref="NotImplementedException"></exception>
        private void button_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            if (button1.Text == "查询用户")
            {

                userCurrentPage = Convert.ToInt32(button.Text.Trim());
                UserDateTest(userBLL.GetAllUser(UserWhere, userCurrentPage, out userTotalPage, userPageSize, userId, UserName));
            }
            else if (button1.Text == "查询客户")
            {
                customerCurrentPage = Convert.ToInt32(button.Text.Trim());
                DateTestCustomer(customerBLL.GetAlldal(customerWhere, customerCurrentPage, out customerTotalPage, customerPageSize, customerId, customerName));

            }
            else if (button1.Text == "查询地址")
            {
                customerAddressCurrentPage = Convert.ToInt32(button.Text.Trim());
                DateTestCustomerAddress(customerBLL.ShowAddress(customerAddressWhere, customerAddressCurrentPage, out customerAddressTotalPage, customerAddressPageSize, customerAddressId, customerAddressName));

            }
            else if (button1.Text == "查询日志")
            {


            }
        }
        #endregion

        #endregion

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        #region 生成Excel
        private void button4_Click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("是否全量覆盖?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result != DialogResult.OK) return;
            if (userBLL.OutputExcel())
            {
                MessageBox.Show("生成成功");
            }
        }


        #endregion


    }
}
