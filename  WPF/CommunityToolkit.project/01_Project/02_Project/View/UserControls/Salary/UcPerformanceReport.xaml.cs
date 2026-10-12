using _02_Project.ViewModel.UserControls.Salary;
using System.Windows;
using System.Windows.Controls;

namespace _02_Project.View.UserControls.Salary
{
    /// <summary>
    /// 业绩统计页（MVVM 模式）：DataContext 为 UcPerformanceReportViewModel（依赖注入），
    /// 无业务逻辑残留，仅保留构造注入。
    /// </summary>
    public partial class UcPerformanceReport : UserControl
    {
        public UcPerformanceReport(UcPerformanceReportViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
