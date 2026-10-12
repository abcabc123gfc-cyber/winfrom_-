using _02_Project.ViewModel.UserControls.Salary;
using System.Windows;
using System.Windows.Controls;

namespace _02_Project.View.UserControls.Salary
{
    /// <summary>
    /// 薪资统计页（MVVM 模式）：DataContext 为 UcSalaryReportViewModel（依赖注入），
    /// 无业务逻辑残留，仅保留构造注入。
    /// </summary>
    public partial class UcSalaryReport : UserControl
    {
        public UcSalaryReport(UcSalaryReportViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
