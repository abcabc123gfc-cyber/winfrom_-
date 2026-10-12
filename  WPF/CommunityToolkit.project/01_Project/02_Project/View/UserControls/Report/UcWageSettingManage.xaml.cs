using _02_Project.ViewModel.UserControls.Report;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02_Project.View.UserControls.Report
{
    /// <summary>
    /// 工价管理页（MVVM 模式）：DataContext 为 UcWageSettingManageViewModel（依赖注入），
    /// 业务逻辑全部位于 ViewModel；本文件仅保留纯 UI 交互事件（输入过滤）。
    /// </summary>
    public partial class UcWageSettingManage : UserControl
    {
        public UcWageSettingManage(UcWageSettingManageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>查询框只允许输入数字（纯 UI 约束）</summary>
        private void QueryIdBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }

        /// <summary>单价框允许数字与小数点（纯 UI 约束；数值校验在 ViewModel 保存时）</summary>
        private void TxtUnitPrice_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9.]$");
        }
    }
}
