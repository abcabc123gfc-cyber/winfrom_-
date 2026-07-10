using SqlSugar;
using SQLSugar.Model;
using System;
using System.Windows.Forms;

namespace SQLSugar
{
    public partial class Form1 : Form
    {
        SqlSugarClient Db;
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
                ConnectionString = "server=.,;database=db_first;uid=root;pwd=root;",
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
            BindDataGridView();
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindDataGridView()
        {
            //显示所有数据, 并显示 没有筛选
            //dataGridView1.DataSource = Db.Queryable<StudentInfo>().ToList();
        }

        private void button6_Click(object sender, EventArgs e)
        {

        }
    }
}
