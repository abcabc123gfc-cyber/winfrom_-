using _02_Project.ViewModel.UserControls.BasicSetting;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace _02_Project.View.UserControls.BasicSetting
{
    /// <summary>
    /// 车间管理页（MVVM 模式）：DataContext 为 UcWorkshopManageViewModel（依赖注入构造传入），
    /// 所有业务逻辑均位于 ViewModel；本文件仅保留纯 UI 交互事件（数字输入过滤）。
    /// </summary>
    public partial class UcWorkshopManage : UserControl
    {
        public UcWorkshopManage(UcWorkshopManageViewModel viewModel)
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
