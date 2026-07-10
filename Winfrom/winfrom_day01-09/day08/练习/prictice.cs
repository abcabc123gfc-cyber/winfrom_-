using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day08.练习
{
    public partial class prictice : Form
    {
        public prictice()
        {
            InitializeComponent();
            Init();
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
        private void Init()
        {
            //不显示初始列
            dataGridView1.AutoGenerateColumns = false;
            

            DataGridViewTextBoxColumn column = new DataGridViewTextBoxColumn();
            column.Name = "Num";
            column.HeaderText = "数字";
            DataGridViewTextBoxColumn dataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            dataGridViewTextBoxColumn.Name = "Result";
            dataGridViewTextBoxColumn.HeaderText = "结果";
            dataGridView1.Columns.Add(column);
            dataGridView1.Columns.Add(dataGridViewTextBoxColumn);
        }

        /// <summary>
        /// 开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                long nums = await Func(i);
                dataGridView1.Rows.Add(i, nums);
            }

        }
        private async Task<long> Func(int a)
        {

            // 创建一个任务


            long b = await System.Threading.Tasks.Task.Run(() =>
            {
                return Fobo(a);
            });
            return b;
        }
        private long Fobo(long n)
        {
            if (n <= 1) return n;
            return Fobo(n - 1) + Fobo(n - 2);
        }

        private void prictice_Load(object sender, EventArgs e)
        {

        }
    }
}
