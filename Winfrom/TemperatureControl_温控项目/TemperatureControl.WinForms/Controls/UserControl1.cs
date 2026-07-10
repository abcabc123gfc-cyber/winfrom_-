using System;
using System.Windows.Forms;

namespace TemperatureControl.WinForms.Controls
{
    public partial class UserControl1 : UserControl
    {
        #region 分页变量
        public int CurrentPage { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        /// <summary>
        /// 分页数
        /// </summary>
        public int userTotalPage { get; set; } = 0;
        /// <summary>
        /// 总条数
        /// </summary>
        public int totalNumber { get; set; } = 0;


        #endregion
        public event EventHandler PageRequested;
        public UserControl1()
        {
            InitializeComponent();
            #region 事件绑定 分页
            btnFirst.Click += btnFirst_Click;
            btnPrev.Click += btnPrev_Click;
            btnNext.Click += btnNext_Click;
            btnLast.Click += btnLast_Click;
            btnGo.Click += btnGo_Click;

            #endregion
        }
        #region 分页
        private void BindData()
        {
            if (PageRequested != null)
            {
                PageRequested(this, new EventArgs());
            }
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
           
        }

        private void UserControl1_Load(object sender, EventArgs e)
        {
            cbbPageSize.SelectedIndex = 0;
           BindData();
        }
    }
}
