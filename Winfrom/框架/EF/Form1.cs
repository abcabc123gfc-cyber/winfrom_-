using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace EF
{
    public partial class Form1 : Form
    {
        db_IMEntities context = new db_IMEntities();
        //db_IMEntities.BuildConnectionString();
        public Form1()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            //Contains() 模糊查询
            RenderData(context.UserInfo.Where(m => m.Name.Contains("张三")).ToList());
        }

        #region 数据渲染
        private void RenderData<T>(List<T> values)
        {
            dataGridView1.DataSource = values;
        }
        #endregion

        private void Form1_Load(object sender, EventArgs e)
        {

            RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            //将数据添加到实体数据模型文件中,没有添加到数据库中
            context.UserInfo.Add(new UserInfo()
            {
                Name = "张三",
                Account = 123456,
                Password = 123456,
                Grade = 1,
                State = 0
            });
            //把实体数据模型文件中的更改,(添加修改删除)提交给数据库
            //返回影响的行数
            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("添加成功");
                //刷新
                RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
            }

        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button3_Click(object sender, EventArgs e)
        {
            //必须在表头中选中一行, 方法过时, 请使用 //dataGridView1.CurrentRow
            if (dataGridView1.SelectedRows.Count != 1)
            {
                MessageBox.Show(dataGridView1.SelectedRows.Count.ToString());
                return;
            }
            //dataGridView1.CurrentRow

            int id = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[0].Value);

            //获取数据库实例, 通过Id 查询
            UserInfo u = context.UserInfo.Single(m => m.Id == id);
            //对象上下文中,设置对象的状态
            // Added 表示实体是添加的实体
            // Modified 表示实体是修改的实体
            // Deleted 表示实体是删除的实体
            context.Entry(u).State = System.Data.Entity.EntityState.Modified;


            // 修改
            u.Name = "小王";
            //同步到数据库
            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("修改成功");
                //刷新
                RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
            }


        }

        /// <summary>
        /// 物理删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的行");
            }
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            UserInfo u = context.UserInfo.Single(m => m.Id == id);
            if (u != null)
            {
                //设置对象状态
                context.Entry(u).State = System.Data.Entity.EntityState.Deleted;

                //删除
                context.UserInfo.Remove(u);
                //同步到数据库
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("删除成功");
                    //刷新
                    RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
                }
            }

            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的行");
            }
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            UserInfo u = context.UserInfo.Single(m => m.Id == id);
            u.State = 1;
            //context.UserInfo.Attach(u);
            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("删除成功");
               
                RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow == null)
            {
                MessageBox.Show("请选择要删除的行");
            }
            int id = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            //构造一个只有Id 的对象
            UserInfo u = new UserInfo() { Id = id };
            context.UserInfo.Attach(u);
            context.UserInfo.Remove(u);
            if (context.SaveChanges() > 0)
            {
                MessageBox.Show("删除成功");
                //刷新
                RenderData(context.UserInfo.Where(m => m.State == 0).ToList());
            }
        }
    }
}



