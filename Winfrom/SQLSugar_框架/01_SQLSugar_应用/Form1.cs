using SqlSugar;
using SQLSugar.Model;
using System;
using System.Windows.Forms;

namespace SQLSugar_应用
{
    public partial class Form1 : Form
    {
        SqlSugarClient Db;
        int page = 1;//当前页
        int pageSize = 5;//每页显示的条数
        int totalPage = 0;//总页数
        int totalCount = 0;//总条数
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            // 创建数据库对象(用法和EF Dappper一样通过new保证线程安全)
            Db = new SqlSugarClient(new ConnectionConfig()
            {
                //数据库连接字符串
                ConnectionString = "server=.;database=db_first;uid=root;pwd=root;",
                //数据库连接类型
                DbType = SqlSugar.DbType.SqlServer,
                //是否自动关闭连接,设置为true之后,会自动关闭,不需要手动Close
                IsAutoCloseConnection = true
            }
           //db => {

           //    db.Aop.OnLogExecuting = (sql, pars) =>
           //    {

           //        //获取原生SQL推荐 5.1.4.63  性能OK
           //        Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));

           //        //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
           //        //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer,sql,pars))


           //    };

           //    //注意多租户 有几个设置几个
           //    //db.GetConnection(i).Aop

           //}
           );

        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataGridView()
        {
            //MessageBox.Show("开始查询");
            //显示所有数据, 并显示 没有筛选
            try
            {
                //dataGridView1.DataSource = Db.Queryable<StudentInfo>().ToList();
                //筛选条件

                //动态OR查询 

                //查询表达式
                var exp = Expressionable.Create<StudentInfo>();

                //一定会拼接
                //exp.And()
                //exp.Or()
                //多个条件的时候  OR或者||    And 并且  &&
                // exp.OrIF(布尔值或者布尔表达式, 筛选条件);
                // exp.AndIF(布尔值或者布尔表达式, 筛选条件);


                //布尔表达式或者布尔值为 true 时, 会触发拼接条件
                exp.OrIF(!string.IsNullOrWhiteSpace(textBox1.Text), s => s.StuName.Contains(textBox1.Text.Trim()));

                exp.AndIF(!string.IsNullOrWhiteSpace(textBox2.Text), s => s.StuAge > int.Parse(textBox2.Text));

                //if (!string.IsNullOrWhiteSpace(textBox1.Text))
                //{
                //    exp.And(s => s.Name.Contains(textBox1.Text.Trim()));
                //}

                //动态查询
                //dataGridView1.DataSource=Db.Queryable<StudentInfo>().Where(exp.ToExpression()).ToList();

                //分页
                dataGridView1.DataSource = Db.Queryable<StudentInfo>().Where(exp.ToExpression()).ToPageList(page, pageSize, ref totalCount, ref totalPage);

            }
            catch (Exception)
            {

                throw;
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            BindDataGridView();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            page = 1;
            BindDataGridView();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            page--;
            BindDataGridView();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            page++;
            BindDataGridView();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            page = totalPage;
            BindDataGridView();
        }
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0) return;
            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;

            //.ExecuteCommand()：真正把 DELETE 语句发到数据库执行，并返回删掉了几行。
            if (Db.Deleteable<StudentInfo>().Where(s => s.Id == id).ExecuteCommand() > 0)
            {
                listBox1.Items.Add("删除了ID为" + id + "的数据");
                BindDataGridView();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int id = Db.Insertable(new StudentInfo()
            {
                StuName = "小王",
                StuAge = 18,
                StuSex = 1,
                StuBirthday = DateTime.Now,
                Status = 1
            }).ExecuteCommand();
            if (id > 0)
            {
                listBox1.Items.Add("插入了姓名为" + "小王" + "的数据");
                BindDataGridView();
            }
        }
        /// <summary>
        /// 修改数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count <= 0) return;
            int id = (int)dataGridView1.CurrentRow.Cells["Id"].Value;
            int temp = Db.Updateable(new StudentInfo()
            {
                Id = id,
                StuName = "小王",
                StuAge = 18,
                StuSex = 1,
                StuBirthday = DateTime.Now,
                Status = 1
            }).ExecuteCommand();
            if (temp > 0)
            {
                listBox1.Items.Add("修改了ID为" + id + "的数据");
                BindDataGridView();
            }

            #region 修改数据 示例
            //Where() 筛选  返回值是一个集合  返回所有满足条件的数据
            // Student stu=  Db.Queryable<Student>().Where(s => s.Id == id).ToList()[0];
            //MessageBox.Show(stu.Name);

            //Single()  返回单条数据 没有则返回null  如果结果条数大于1 会抛异常
            //Student stu = Db.Queryable<Student>().Single(s => s.Id == id);

            //stu.Name = "123";
            //stu.Age = 66;
            //int row = Db.Updateable(stu).ExecuteCommand();
            //if (row > 0)
            //{
            //    BindDataGridView();

            //}
            #endregion
        }
    }
}
