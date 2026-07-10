using SqlSugar;
using System;
using System.Reflection.Emit;
using System.Windows.Forms;
using TemperatureControl.WinForms.Herper;
namespace TemperatureControl.WinForms.Froms
{
    public partial class UserType : frmBase
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
        public UserType()
        {
            InitializeComponent();
        }

        public void UserType_Load(object sender, EventArgs e)
        {
            label1.Text = "用户类型";
            label2.Text = "用户姓名";
           
            BindData();
        }
        private void BindData()
        {
            var v = sqlSugar.Queryable<Models.UserType>().ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);
            //var v = sqlSugar.Queryable<VUserType>().ToList();
            //MessageBox.Show(totalNumber.ToString());
            //MessageBox.Show(userTotalPage.ToString());
            dataGridView1.DataSource = v;
            lblCurrenPageAndTotalPage.Text = $"{CurrentPage}/{userTotalPage}";
        }
        private void UserType_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void UserType_Resize(object sender, EventArgs e)
        {
            base.frmBase_Load(sender, e);
        }
        #region 分页
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
        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            UserTypeAdd add = new UserTypeAdd();
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
            int temp = (int)dataGridView1.CurrentRow.Cells["UserTypeId"].Value;
            UserTypeAdd add = new UserTypeAdd("编辑", temp);
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
            int temp = (int)dataGridView1.CurrentRow.Cells["UserTypeId"].Value;
            if (MessageBox.Show("确定删除吗？", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                
                if (sqlSugar.Deleteable<Models.UserType>().Where(it => it.UserTypeId == temp).ExecuteCommand() > 0)
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
            var v=Expressionable.Create<Models.UserType>();
            if (!string.IsNullOrEmpty(txtProductName.Text))
            {
                v.And(it => it.UserTypeId==int.Parse( txtProductName.Text.Trim()));
            }else if (!string.IsNullOrEmpty(txtProductNo.Text))
            {
                v.And(it => it.UserTypeName.Contains(txtProductNo.Text.Trim()));
            }
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sqlSugar.Queryable<Models.UserType>().Where(v.ToExpression()).ToPageList(CurrentPage, PageSize, ref totalNumber, ref userTotalPage);
        }
    }
}
