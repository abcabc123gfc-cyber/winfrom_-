using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using 示例;
using 示例.BLL;

namespace UI
{
    public partial class StudentList : Form
    {
        public static List<User> userList = UserDAL.listUser;
        UserBLL userBLL = new UserBLL();
        CustomerBLL customerBLL = new CustomerBLL();
        public static StudentList Instance { get; set; }
        public static Customer customer1 = null;
        public static Address address1 = null;
        LogBLL logBLL = new LogBLL();
        public StudentList(string str)
        {

            InitializeComponent();
            textBox1.Text = str;
            InitDataGridView();

            if (str != "游客登录")
            {
                DateTest(userList);
            }
            else
            {
                button1.Enabled =

                    false;
                button2.Enabled = false;
                button4.Enabled = false;
                menuStrip1.Enabled = false;
            }
            Instance = this;
        }
        #region 数据渲染
        private void DateTest(List<User> list, bool isShow = true)
        {
            //dataGridView1.Rows.Add("值1", "值2", "值3");

            dataGridView1.Rows.Clear();
            foreach (var item in list)
            {
                if (item.Id != UserDAL.userId || isShow == false)
                {

                    dataGridView1.Rows.Add(item.Id, item.Name, item.Account, item.Password, item.Grade == "管理员" ? true : false);
                }
            }

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
            //dataGridView1.Columns.Add("operation_0", "操作1");
            //dataGridView1.Columns.Add("operation_1", "操作2");
            //dataGridView1.Columns.Add("operation_2", "操作3");
            //设置列宽
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
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
                if (textBox2.Text.Trim() != "")
                {
                    List<User> list = userBLL.GetUser(textBox2.Text);
                    if (list != null && list.Count > 0)
                    {

                        DateTest(list);
                    }
                    textBox2.Text = "";
                }
                else if (textBox3.Text.Trim() != "")
                {//使用id查询
                    User u = userBLL.GetUser(Convert.ToInt32(textBox3.Text.Trim()));
                    if (u != null)
                    {
                        DateTest(new List<User>() { u });
                    }


                    textBox3.Text = "";
                }
            }
            else if (button1.Text == "查询客户")
            {
                //使用名字查询
                if (textBox2.Text.Trim() != "")
                {
                    List<Customer> list = customerBLL.GetName(textBox2.Text);
                    if (list.Count > 0 && list != null)
                    {
                        DateTestCustomer(list);
                    }
                    textBox2.Text = "";
                }
                else if (textBox3.Text.Trim() != "")
                {//使用id查询
                    List<Customer> listCu = new List<Customer>()
                  {
                      customerBLL.Getdal(Convert.ToInt32(textBox3.Text))
                  };
                    DateTestCustomer(listCu);
                    textBox3.Text = "";
                }
            }
            else if (button1.Text == "查询地址")
            {

                if (textBox3.Text.Trim() != "")
                {//使用id查询
                    List<Address> listCu = new List<Address>()
                  {
                      customerBLL.GetAddress(Convert.ToInt32(textBox3.Text))
                  };
                    DateTestCustomerAddress(listCu);
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
            Form target = Application.OpenForms["Login1"];
            if (target != null)
            {

                Login1 form = target as Login1;
                form.Show();

                //userBLL.logout();
                UserDAL userDAL = new UserDAL();
                userDAL.LogOut();
                this.Close();
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
                        User user = userList.Find(x => x.Id == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Form_Load("编辑用户", "保存", user);
                    }
                    else if (button1.Text == "查询客户")
                    {
                        customer1 = customerBLL.Getdal(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Form_Load("编辑客户", "保存");

                    }
                    else if (button1.Text == "查询地址")
                    {
                        address1 = customerBLL.GetAddress(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Addressoperation addressoperation = new Addressoperation("修改地址");
                        addressoperation.Show();
                        this.Hide();
                    }
                }
                #endregion
                #region 删除
                else if (e.ColumnIndex == dataGridView1.Columns["操作2"].Index && e.ColumnIndex >= 0)
                {
                    var DialogResult = MessageBox.Show("确认删除吗?", "删除", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Warning);
                    if (DialogResult == DialogResult.Yes)
                    {
                        if (button1.Text == "查询用户" && userBLL.deleteUser(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString())))
                        {

                            dataGridView1.Rows.RemoveAt(e.RowIndex);
                        }
                        else if (button1.Text == "查询客户")
                        {

                            customerBLL.Deletedal(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));

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

                        User user = userList.Find(x => x.Id == Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Form_Load("用户详情", "", user);
                    }
                    else if (button1.Text == "查询客户")
                    {
                        customer1 = customerBLL.Getdal(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Form_Load("客户详情", "");
                    }
                    else if (button1.Text == "查询地址")
                    {
                        address1 = customerBLL.GetAddress(Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()));
                        Addressoperation addressoperation = new Addressoperation("地址详情");
                        addressoperation.Show();
                        this.Hide();
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
            selectStudent.Show();
        }

        private void StudentList_FormClosing(object sender, FormClosingEventArgs e)
        {
            Form target = Application.OpenForms["Login1"];
            if (target != null)
            {

                Login1 form = target as Login1;
                form.Show();


            }
        }
        #endregion


        /// <summary>
        /// 刷新表格
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (button1.Text == "查询用户")
            {
                DateTest(userList);
            }
            else if (button1.Text == "查询客户")
            {
                // 加载数据
                DateTestCustomer(customerBLL.GetAlldal());
            }
            else if (button1.Text == "查询地址")
            {

                InitAddressDate();
            }
            //else
            //{
            //    button4.Click += toolStripMenuItem1_Click;
            //}
        }


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
            DateTest(userList);
            //控件更改
            button1.Text = "查询用户";
            button2.Text = "添加用户";
            label2.Show();
            textBox2.Show();
            listBox1.Hide();
            button2.Show();
            button4.Show();

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
            #region 表格数据加载
            //清除表格
            ClearDataGridView();
            // 初始化表格
            InitDataGridViewCustomer();
            // 加载数据
            DateTestCustomer(customerBLL.GetAlldal());
            #endregion
            //控件更改
            InitControlCustomer();

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

        }

        private void InitControlCustomer()
        {
            label2.Show();
            textBox2.Show();
            listBox1.Hide();
            button1.Text = "查询客户";
            button2.Text = "添加客户";
            button2.Show();
            button4.Show();
        }
        #endregion
        #endregion
        #region 数据备份
        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            userBLL.BlackDate();
        }
        #endregion

        #region 个人信息
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //label2.Text = "当前登录用户为:   " + userBLL.Personalinfo().Name;
            //label2.Show();
            //textBox2.Hide();
            //textBox3.Hide();
            //label3.Hide();
            button1.Text = "查询用户";
            button2.Text = "添加用户";
            listBox1.Hide();
            //刷新按钮
            button2.Hide();
            button4.Hide();
            List<User> u = new List<User>()
                {
                    userBLL.Personalinfo()
                };
            ClearDataGridView();
            // 初始化表格
            InitDataGridView();
            InitColumnsOperation();
            // 加载数据
            DateTest(u, false);


        }

        #endregion

        private void StudentList_Load(object sender, EventArgs e)
        {

        }

        #region 客户地址视图
        private void 查看客户地址视图ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InitCustomerAddress();
            InitDataGridViewCustomerAddress();
            InitAddressDate();
            //省市级 下拉列表框初始化
            InitComBox();

        }
        #region 初始化控件
        /// <summary>
        /// 初始化控件文本
        /// </summary>
        /// 
        private void InitCustomerAddress()
        {
            label2.Hide();
            textBox2.Hide();
            button1.Text = "查询地址";
            button2.Text = "添加地址";
            button2.Show();
            button4.Show();
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
        /// <summary>
        /// 数据渲染
        /// </summary>
        /// <param name="list"></param>
        private void DateTestCustomerAddress(List<Address> list)
        {
            dataGridView1.Rows.Clear();
            foreach (var item in list)
            {
                dataGridView1.Rows.Add(item.Id, item.Add);
            }
        }
        /// <summary>
        /// 初始化数据
        /// </summary>
        private void InitAddressDate()
        {//加载数据
            List<Address> u = customerBLL.ShowAddress();
            if (u.Count == 0 && u == null)
            {
                MessageBox.Show("没有客户地址");
                return;
            }
            DateTestCustomerAddress(u);

        }

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

        private void 用户日志ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            listBox1.Show();
            listBox1.Items.Clear();
            List<Log> log = logBLL.LogOperrate();
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
            List<Log> log = logBLL.LogTrade();
            if (log == null)
            {
                return;
            }

            foreach (var item in log)
            {

                listBox1.Items.Add(item.LogOperate());

            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

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
    }
}
