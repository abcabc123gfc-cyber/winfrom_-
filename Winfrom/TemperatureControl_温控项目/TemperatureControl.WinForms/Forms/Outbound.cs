using SqlSugar;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;

namespace TemperatureControl.WinForms.Froms
{

    public partial class Outbound : frmBase
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
        public Outbound()
        {
            InitializeComponent();
            #region 事件绑定 分页
            btnFirst.Click += btnFirst_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;
            btnLast.Click += btnLast_Click;
            btnGo.Click += btnGo_Click;
            this.Resize += Partition_Resize;
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
            var v = sqlSugar.Queryable<Models.VOutboundRecord>().ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);

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
        #region crud
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            OutboundAdd add = new OutboundAdd();
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
            int temp = (int)dataGridView1.CurrentRow.Cells["OutboundId"].Value;
            OutboundAdd  add = new OutboundAdd("编辑", temp);
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
            int temp = (int)dataGridView1.CurrentRow.Cells["OutboundId"].Value;
            if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                if (sqlSugar.Deleteable<Models.OutboundRecord>().Where(it => it.OutboundId == temp).ExecuteCommand() > 0)
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
            var v = Expressionable.Create<Models.VOutboundRecord>();
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                v.And(it => it.OutboundId == int.Parse(txtProductName.Text.Trim()));
            }
            else if (!string.IsNullOrEmpty(txtProductNo.Text))
            {
                v.And(it => it.StoreAreaName.Contains(txtProductNo.Text.Trim()));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sqlSugar.Queryable<Models.VOutboundRecord>().Where(v.ToExpression()).ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);
        }
        #endregion
        private void Partition_Resize(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
        }
        public void Partition_Load(object sender, EventArgs e)
        {

            label2.Text = "区域:";

            BindData();
        }
    }
}
