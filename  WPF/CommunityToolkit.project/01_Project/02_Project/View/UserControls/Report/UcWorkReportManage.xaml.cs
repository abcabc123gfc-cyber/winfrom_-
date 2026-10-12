using _02_Project.ViewModel.UserControls.Report;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02_Project.View.UserControls.Report
{
    /// <summary>
    /// 报工管理页（MVVM 模式）：DataContext 为 UcWorkReportManageViewModel（依赖注入），
    /// 业务逻辑（工价联动带单价、工资实时计算、子表单按工组过滤）全部位于 ViewModel；
    /// 本文件仅保留纯 UI 交互事件（输入过滤）。
    /// </summary>
    public partial class UcWorkReportManage : UserControl
    {
        public UcWorkReportManage(UcWorkReportManageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>查询框 / 数量框只允许输入数字（纯 UI 约束；数值校验在 ViewModel 保存时）</summary>
        private void QueryIdBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        private void TxtCount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
