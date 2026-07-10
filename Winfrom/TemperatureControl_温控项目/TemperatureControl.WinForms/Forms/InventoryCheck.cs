using Models;
using SqlSugar;
using System;
using System.Linq;
using System.Windows.Forms;
using TemperatureControl.WinForms.Froms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Forms
{
    public partial class InventoryCheck : frmBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        #region 分页变量

        /// <summary>
        /// 分页数
        /// </summary>
        int userTotalPage = 0;
        /// <summary>
        /// 总条数
        /// </summary>
        int totalNumber = 0;

        string UserWhere = null;
        #endregion
        public InventoryCheck()
        {
            InitializeComponent();
            #region 事件绑定 分页

            this.Resize += Partition_Resize;
            this.Load += Partition_Load;
            #endregion
            #region 事件绑定 crud
            btnAdd.Click += btnAdd_Click;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            btnSearch.Click += btnSearch_Click;
            #endregion
            userControl11.PageRequested += UserControl1_PageRequested;
        }


        #region crud
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            
        }

        private void BindData()
        {
            UserControl1_PageRequested(null, EventArgs.Empty);

        }

        private void UserControl1_PageRequested(object sender, EventArgs e)
        {

            var v = sqlSugar.Queryable<Models.VInventory>().ToPageList(userControl11.CurrentPage, userControl11.PageSize, ref totalNumber, ref userTotalPage).ToList();
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = v;
            userControl11.totalNumber = totalNumber;
            userControl11.userTotalPage = userTotalPage;
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count < 0) return;
            int temp = (int)dataGridView1.CurrentRow.Cells["InventoryId"].Value;
           
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count < 0) return;
            int temp = (int)dataGridView1.CurrentRow.Cells["InventoryId"].Value;
            if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                if (sqlSugar.Deleteable<Models.VInventory>().Where(it => it.InventoryId == temp).ExecuteCommand() > 0)
                {
                    MessageBox.Show("删除成功");
                    BindData();
                }
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            var v = Expressionable.Create<Models.VInventory>();
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                v.And(it => it.InventoryId == int.Parse(txtProductName.Text.Trim()));
            }
            else if (!string.IsNullOrEmpty(txtProductNo.Text))
            {
                v.And(it => it.ProductId == int.Parse(txtProductNo.Text.Trim()));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sqlSugar.Queryable<Models.VInventory>().Where(v.ToExpression()).ToPageList(userControl11.CurrentPage, userControl11.PageSize, ref totalNumber, ref userTotalPage);
        }
        #endregion
        private void Partition_Resize(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);

            userControl11.Width = panel2.Width;
            userControl11.Height = panel2.Height;
            userControl11.Location = new System.Drawing.Point(panel2.Bounds.X, panel2.Bounds.Y);


        }
        public void Partition_Load(object sender, EventArgs e)
        {
            label1.Text = "出库编号:";
            label2.Text = "产品编号:";

            BindData();
        }

        private void InventoryCheck_Load(object sender, EventArgs e)
        {
            btnAdd.Visible = false;
            button1.Visible = false;
            button1.Visible = false;
        }

        private void InventoryCheck_Shown(object sender, EventArgs e)
        {

        }
    }
}
