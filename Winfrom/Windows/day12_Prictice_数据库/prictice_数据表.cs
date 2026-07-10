using day12_Prictice_数据库;
using day12_Prictice_数据库.工具类;

using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;

namespace day12
{
    public partial class prictice_数据表 : Form
    {
        public prictice_数据表()
        {
            InitializeComponent();
            InitDataGrivew();
            InitControls();
            InitDataGrivewTable("select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别 ,StuBirthday from Students");
        }



        /// <summary>
        /// 获取数据,参数注解
        /// </summary>
        private void InitDataGrivewTable(string sql, params SqlParameter[] sqlParameters)
        {


            //直接判断是否为空 ,是因为现在只有姓名一个 查询语句
            if (string.IsNullOrEmpty(sql))
            {
                return;
            }



            DataSet dataSet = DbConnectionHelper.GetReader(DbConnectionHelper.GetConnection(), sql, sqlParameters);
            if (dataSet == null || dataSet.Tables[0].Rows.Count <= 0)
            {
                return;
            }
            dataGridView1.DataSource = dataSet.Tables[0];

            InitColumns();
        }
        /// <summary>
        /// 列渲染 重命名列明
        /// </summary>
        private void InitColumns()
        {
            dataGridView1.Columns["Id"].HeaderText = "编号";
            dataGridView1.Columns["StuName"].HeaderText = "学生姓名";
            dataGridView1.Columns["StuAge"].HeaderText = "学生年龄";

            dataGridView1.Columns["StuBirthday"].HeaderText = "出生日期";
        }

        /// <summary>
        /// 查询 数据渲染
        /// </summary>
        [Obsolete("使用参数注解的方式实现查询")]
        private void SelectTable()
        {
            using (SqlConnection conn = DbConnectionHelper.GetConnection())
            {
                conn.Open();

                using (SqlCommand cmd = new SqlCommand("select Id,StuName,StuAge,  CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别,StuBirthday from Students", conn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;

                    dataGridView1.Columns["Id"].HeaderText = "编号";

                    dataGridView1.Columns["StuName"].HeaderText = "学生姓名";
                    dataGridView1.Columns["StuAge"].HeaderText = "学生年龄";
                    //dataGridView1.Columns["StuSex"].HeaderText = "学生性别";
                    //dataGridView1.Columns["StuSex"].CellTemplate=
                    dataGridView1.Columns["StuBirthday"].HeaderText = "出生日期";
                }

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            InitDataGrivewTable("select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别 ,StuBirthday from Students");


        }
        #region 初始化表格
        private void InitDataGrivew()
        {
            //隐藏行头
            dataGridView1.RowHeadersVisible = false;
            //禁止多选
            dataGridView1.MultiSelect = false;
            //禁止用户编辑
            dataGridView1.ReadOnly = true;
            //设置列宽
            dataGridView1.AllowUserToOrderColumns = false;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
        private void InitControls()
        {
            cbbPageSize.Items.Add(5);
            cbbPageSize.Items.Add(10);
            cbbPageSize.Items.Add(15);
            cbbPageSize.Text = cbbPageSize.Items[0].ToString();
        }
        #endregion

        private void button2_Click(object sender, EventArgs e)
        {
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@Id",SqlDbType.Int),

            };
            DataGridViewRow dataGridViewRow = GetDataGridViewRow();
            if (dataGridViewRow == null)
            {
                textBox1.Text = "操作无效";
                return;
            }
            sqlParameters[0].Value = dataGridViewRow.Cells["Id"].Value;

            string str = $"DELETE Students WHERE @Id=Id";
            int num = DbConnectionHelper.GetCommand(DbConnectionHelper.GetConnection(), str, sqlParameters);
            if (num <= 0)
            {
                textBox1.Text = "操作失败";
                return;
            }
            textBox1.Text = num.ToString();
            InitDataGrivewTable("select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别 ,StuBirthday from Students");
        }
        /// <summary>
        /// 获取表格的索引
        /// </summary>
        /// <returns></returns>
        private int SeletGridId()
        {


            if (dataGridView1.CurrentCell == null) return -1;
            //获取当前行 选中
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row == null) return -1;
            var value = row.Cells["Id"].Value;
            //判断是否为空 C# null 与 数据库 null
            if (value == null || value == DBNull.Value) return -1;
            return int.TryParse(value.ToString(), out int result) ? result : -1;
        }
        /// <summary>
        /// 获取表格的行
        /// </summary>
        /// <returns></returns>
        private DataGridViewRow GetDataGridViewRow()
        {

            if (dataGridView1.CurrentCell == null) return null;
            //获取当前行 选中
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row == null) return null;
            return row;
        }

        private void prictice_数据表_Load(object sender, EventArgs e)
        {
            textBox1.ReadOnly = true;
            //SelectTable();
            //初始化总行数
            InitTotalPage();


        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {

            Add_modify add_Modify = new Add_modify(SeletGridId());
            if (add_Modify.ShowDialog() == DialogResult.OK)
            {
                //InitDataGrivewTable("select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别 ,StuBirthday from Students");
            }
        }
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {

            Add_modify add_Modify = new Add_modify(SeletGridId(), "添加", true);
            if (add_Modify.ShowDialog() == DialogResult.OK)
            {
                //InitDataGrivewTable("select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS 学生性别 ,StuBirthday from Students");
            }
        }

        private void prictice_数据表_FormClosing(object sender, FormClosingEventArgs e)
        {

        }
        /// <summary>
        /// 姓名查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            string str = "select Id,StuName,StuAge,CASE WHEN StuSex = 1 THEN '男' ELSE '女' END AS " +
                "学生性别 ,StuBirthday from Students where 1=1 ";

            SqlParameter[] sqlParameters = new SqlParameter[]
              {
                new SqlParameter("@StuName",SqlDbType.VarChar),
                new SqlParameter("@StuAge",SqlDbType.Int)
              };

            //输入姓名 
            if (!string.IsNullOrWhiteSpace(textBox2.Text))
            {
                sqlParameters[0].Value = textBox2.Text;
                str += " and @StuName=StuName";

            }
            try
            {

                //输入年龄
                if (!string.IsNullOrEmpty(textBox3.Text))
                {
                    sqlParameters[1].Value = Convert.ToInt32(textBox3.Text);
                    str += " and @StuAge=StuAge";
                }
            }
            catch (Exception ex)
            {
                textBox1.ReadOnly = false;
                textBox1.ForeColor = System.Drawing.Color.Red;
                textBox1.Text = "请输入正确的年龄";
                MessageBox.Show(ex.Message);
                textBox1.ReadOnly = true;
                //textBox1.ForeColor = System.Drawing.Color.Black;
            }

            //添加参数
            InitDataGrivewTable(str, sqlParameters);
            InitColumns();
        }





        #region 分页查询

        #region 声明分页变量
        /// <summary>
        /// 当前页
        /// </summary>
        private int CurrentPage = 1;
        /// <summary>
        /// 每页显示的条数
        /// </summary>
        private double PageSize = 5;
        /// <summary>
        /// 总页数
        /// </summary>
        private int TotalPage = 0;
        #endregion
        #region 分页显示
        public void SelectTable1()
        {
            //根据id 显示是由局限的, 明天使用 row_number() y 与 setoff

            SqlParameter[] sqlParameters = new SqlParameter[]
          {
                new SqlParameter("@currentPage",SqlDbType.Int),
                new SqlParameter("@pageSize",SqlDbType.Int)
          };
            sqlParameters[0].Value = CurrentPage;
            sqlParameters[1].Value = PageSize;


            string sql = "select * from( select row_number() over (order by Id) as RowID,* from Students ) as t  where RowID between (@currentPage-1)* @pageSize + 1 and @currentPage* @pageSize; ";

            //设置标签显示 当前页与总页数
            lblCurrenPageAndTotalPage.Text = $"{CurrentPage}/{TotalPage}";
            InitDataGrivewTable(sql, sqlParameters);


            //DataSet dataSet = DbConnectionHelper.GetReader(DbConnectionHelper.GetConnection(), sql, sqlParameters);
            //dataGridView1.DataSource = dataSet.Tables[0];
            //MessageBox.Show(CurrentPage.ToString());


        }
        #endregion

        #region 初始化显示总行数
        private void InitTotalPage()
        {

            string sql = "select count(*) from Students;";
            try
            {
                double Total = (int)DbConnectionHelper.GetExecuteScalar(DbConnectionHelper.GetConnection(), sql, null);
                TotalPage = (int)Math.Ceiling(Total / PageSize);
                textBox4.Text = "共" + Total.ToString() + "条\n\r";
                textBox4.Text += "共" + TotalPage.ToString() + "页\n\r ";
            }
            catch (Exception)
            {

                textBox4.Text = DbConnectionHelper.GetExecuteScalar(DbConnectionHelper.GetConnection(), sql, null).ToString();

            }
        }
        #endregion



        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            SelectTable1();
        }
        /// <summary>
        /// 首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;

            //每页显示的条数由 combox决定       
            SelectTable1();
            //动态加载控件
            LoadButton(TotalPage);
        }

        private void LoadButton(int totalPage)
        {
            panel2.BorderStyle = BorderStyle.FixedSingle;
            panel2.AutoScroll = true;
            
            int btnWidth = 70;
            int btnHeight = 50;
            int pointX = -80;
            int pointY = 0;
            for (int i = 0; i < 10; i++)
            {
                Button button = new Button();
                button.Text = (i + 1).ToString();
                button.Size = new System.Drawing.Size(btnWidth, btnHeight);

                button.Location = new System.Drawing.Point( pointX+=80, pointY);
                button.Click += new System.EventHandler(this.button_Click);
                if (panel2.Width<=pointX+ btnWidth)
                {
                    pointY += 70;
                    pointX = -80;
                } 
                panel2.Controls.Add(button);
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
            //CurrentPage = Convert.ToInt32(button.Text.Trim());
            SelectTable1();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage = CurrentPage - 1;
            }

            SelectTable1();
        }

        private void cbbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {

            int temp = (int)cbbPageSize.SelectedItem;
            PageSize = (double)temp;
        }
        /// <summary>
        /// 下一页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPage)
            {
                CurrentPage += 1;
            }
            SelectTable1();
        }
        /// <summary>
        /// 尾页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLast_Click(object sender, EventArgs e)
        {
            if (CurrentPage < TotalPage)
            {

                CurrentPage = TotalPage;
            }
            SelectTable1();
        }
        /// <summary>
        /// 跳转的页码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGo_Click(object sender, EventArgs e)
        {
            try
            {

                if (CurrentPage <= TotalPage && Convert.ToInt32(txtCurrentpage.Text) <= TotalPage)
                {
                    CurrentPage = Convert.ToInt32(txtCurrentpage.Text);
                    SelectTable1();
                }
            }
            catch (Exception ex)
            {

                textBox4.Text += ex.Message;
            }
        }
    }
}
