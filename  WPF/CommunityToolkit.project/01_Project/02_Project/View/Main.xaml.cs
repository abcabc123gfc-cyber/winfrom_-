using _02_Project.View.UserControls;
using _02_Project.View.UserControls.UserManage;
using _02_Project.View.UserControls.BasicSetting;
using _02_Project.View.UserControls.Report;
using _02_Project.View.UserControls.Salary;
using _02_Project.ViewModel;
using HandyControl.Data;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace _02_Project.View
{
    /// <summary>
    /// Main.xaml 交互逻辑：
    /// 左侧 SideMenu 导航 -> 切换右侧用户控件；
    /// 底部状态栏：当前登录人（MainViewModel 从数据库加载）+ 系统时间（DispatcherTimer 每秒刷新）。
    /// </summary>
    public partial class Main : Window
    {
        private readonly MainViewModel _mainViewModel;
        private readonly DispatcherTimer _clockTimer;
        private readonly Dictionary<string, UserControl> _pages = new();

        public Main(MainViewModel mainViewModel)
        {
            InitializeComponent();
            _mainViewModel = mainViewModel;
            DataContext = mainViewModel;

            // 系统时间：启动即刷新，之后每秒刷新
            _clockTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            _clockTimer.Tick += (_, _) => ClockText.Text = $"系统时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss dddd}";
            _clockTimer.Start();

            Loaded += Main_Loaded;
        }

        private async void Main_Loaded(object sender, RoutedEventArgs e)
        {
            // 加载当前登录人（从数据库读取）
            await _mainViewModel.LoadCurrentUserAsync();
            ClockText.Text = $"系统时间：{DateTime.Now:yyyy-MM-dd HH:mm:ss dddd}";

            // 默认展示“用户管理”，并高亮对应菜单项
            ShowPage("用户管理");
            if (NavMenu.Items.Count > 0 && NavMenu.Items[0] is HandyControl.Controls.SideMenuItem first)
                first.IsSelected = true;
        }

        /// <summary>SideMenu 选中项改变：e.Info 为被点击的 SideMenuItem（叶子项触发）</summary>
        private void NavMenu_SelectionChanged(object sender, FunctionEventArgs<object> e)
        {
            if (e.Info is HandyControl.Controls.SideMenuItem item && item.Header is string header)
                ShowPage(header);
        }

        /// <summary>根据菜单标题切换右侧用户控件（页面实例缓存复用）</summary>
        private void ShowPage(string key)
        {
            if (!_pages.TryGetValue(key, out var page))
            {
                page = key switch
                {
                    "用户管理" => App.ServiceProvider.GetRequiredService<UcUserManage>(),
                    "车间管理" => App.ServiceProvider.GetRequiredService<UcWorkshopManage>(),
                    "生产线管理" => App.ServiceProvider.GetRequiredService<UcProductionLineManage>(),
                    "员工管理" => App.ServiceProvider.GetRequiredService<UcEmployeeManage>(),
                    "工组管理" => App.ServiceProvider.GetRequiredService<UcWorkGroupManage>(),
                    "工位管理" => App.ServiceProvider.GetRequiredService<UcWorkStationManage>(),
                    "工序管理" => App.ServiceProvider.GetRequiredService<UcProcessManage>(),
                    "工价管理" => App.ServiceProvider.GetRequiredService<UcWageSettingManage>(),
                    "工艺路线管理" => App.ServiceProvider.GetRequiredService<UcCraftRouteManage>(),
                    "报工管理" => App.ServiceProvider.GetRequiredService<UcWorkReportManage>(),
                    "薪资统计" => App.ServiceProvider.GetRequiredService<UcSalaryReport>(),
                    "业绩统计" => App.ServiceProvider.GetRequiredService<UcPerformanceReport>(),
                    _ => new UcPlaceholder { PageTitle = key }
                };
                _pages[key] = page;
            }
            ContentHost.Content = page;
        }
    }
}
