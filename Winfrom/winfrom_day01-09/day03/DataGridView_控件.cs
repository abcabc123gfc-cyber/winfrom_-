using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day03
{
    public partial class DataGridView_控件 : Form
    {
        public DataGridView_控件()
        {
            InitializeComponent();
            Datetable();
            //创建文本列
          DataGridViewTextBoxColumn del=new DataGridViewTextBoxColumn();

            del.Name = "操作";
            dataGridView1.Columns.Add(del);

            //按钮列
            DataGridViewButtonColumn dataGridViewButtonColumn = new DataGridViewButtonColumn();
            dataGridViewButtonColumn.Name = "按钮";
            dataGridView1.Columns.Add(dataGridViewButtonColumn);
            //任何单元格点击触发
            dataGridView1.CellClick += SelectTable;
        }

        private void SelectTable(object sender, DataGridViewCellEventArgs e)
        {
            //获取列索引
            //e.ColumnIndex;  
            //获取行索引
            //e.RowIndex;
        }

        //public DataTable
        public void Datetable()
        {
            DataTable dataTable = new DataTable();
            dataGridView1.AutoSize=true;
            //添加列
            dataTable.Columns.Add("ID ");
            dataTable.Columns.Add("Name");
            dataTable.Columns.Add("State");
            dataTable.Columns.Add("Phone");
            //添加行
            dataTable.Rows.Add("1", "张三", "北京", "123456");
            dataTable.Rows.Add("2", "王五", "上海", "123456");
            dataTable.Rows.Add("3", "赵六", "广州", "123456");
            dataTable.Rows.Add("4", "王五", "上海", "123456");
            dataGridView1.DataSource = dataTable;



        }
    }
}
