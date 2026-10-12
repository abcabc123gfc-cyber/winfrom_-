using _02_Project.ViewModel.UserControls.BasicSetting;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02_Project.View.UserControls.BasicSetting
{
    /// <summary>
    /// 生产线管理页（MVVM 模式）：DataContext 为 UcProductionLineManageViewModel（依赖注入），
    /// 业务逻辑全部位于 ViewModel；本文件仅保留纯 UI 交互事件。
    /// </summary>
    public partial class UcProductionLineManage : UserControl
    {
        public UcProductionLineManage(UcProductionLineManageViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }

        /// <summary>查询框只允许输入数字（纯 UI 约束）</summary>
        private void QueryIdBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !Regex.IsMatch(e.Text, "^[0-9]+$");
        }
    }
}
