using SqlSugar;
using System;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{
    public partial class Product : frmBase
    {
        SqlSugarClient sqlSugar = SQLHerper.Connection();
        #region 分页变量
        int CurrentPage = 1;
        int PageSize = 10;
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
        public Product()
        {
            InitializeComponent();
            #region 事件绑定 分页
            btnFirst.Click += btnFirst_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;
            btnLast.Click += btnLast_Click;
            btnGo.Click += btnGo_Click;
            this.Load += Partition_Load;
            #endregion
            #region 事件绑定 crud
            btnAdd.Click += btnAdd_Click;
            button1.Click += button1_Click;
            button2.Click += button2_Click;
            btnSearch.Click += btnSearch_Click;

            #endregion
        }
        #region 分页
        private void BindData()
        {
            var v = sqlSugar.Queryable<Models.VProduct>().ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);

            dataGridView1.DataSource = v;
            lblCurrenPageAndTotalPage.Text = $"{CurrentPage}/{userTotalPage}";
        }
        private void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            PageSize = Convert.ToInt32(cbbPageSize.SelectedItem);

            BindData();
        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                PageSize = Convert.ToInt32(cbbPageSize.SelectedItem);
                BindData();
            }

        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < userTotalPage)
            {
                CurrentPage++;
                PageSize = Convert.ToInt32(cbbPageSize.SelectedItem);
                BindData();
            }
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            if (CurrentPage < userTotalPage)
            {
                CurrentPage = userTotalPage;
                PageSize = Convert.ToInt32(cbbPageSize.SelectedItem);
                BindData();
            }
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            CurrentPage = int.TryParse(txtCurrentpage.Text, out int page) ? page : 1;
            if (CurrentPage < userTotalPage)
            {
                PageSize = Convert.ToInt32(cbbPageSize.SelectedItem);
                txtCurrentpage.Text = CurrentPage.ToString();
                BindData();

            }
            else
            {
                txtCurrentpage.Text = string.Empty;
            }
        }
        #endregion
        public void Partition_Load(object sender, EventArgs e)
        {


            BindData();
        }
        #region crud
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            ProductAdd add = new ProductAdd();
            if (add.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }
        /// <summary>
        /// 编辑
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count < 0) return;
            int temp = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;
            ProductAdd add = new ProductAdd("编辑", temp);
            if (add.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count < 0) return;
            int temp = (int)dataGridView1.CurrentRow.Cells["ProductId"].Value;
            if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                if (sqlSugar.Deleteable<Models.Product>().Where(it => it.ProductId == temp).ExecuteCommand() > 0)
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
            var v = Expressionable.Create<Models.Product>();
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                v.And(it => it.ProductName.Contains(txtProductName.Text.Trim()));
            }
            else if (!string.IsNullOrEmpty(txtProductNo.Text))
            {
                v.And(it => it.ProductId == int.Parse(txtProductNo.Text.Trim()));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sqlSugar.Queryable<Models.Product>().Where(v.ToExpression()).ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);
        }
        #endregion

        private void Product_Resize(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
        }
    }
}
