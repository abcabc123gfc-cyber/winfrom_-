using _02_Project.DBContext;
using _02_Project.Service;
using _02_Project.Service.lmp;
using _02_Project.View;
using _02_Project.ViewModel;
using System.IO;
//安装Microsoft.Extensions.DependencyInjection程序集
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;
using System.Windows;
namespace _02_Project
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // 捕获 UI 线程未处理异常，写入日志文件方便排查
            this.DispatcherUnhandledException += (s, args) =>
            {
                try
                {
                    File.AppendAllText("app_error.log",
                        $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {args.Exception}\r\n\r\n");
                }
                catch { }
                args.Handled = true;
            };

            // 1. 调用独立的方法来构建容器（保持代码清爽）
            ServiceProvider = ConfigureServices();

            // 2. 从容器中解析登录窗口（触发依赖注入）
            var loginWindow = ServiceProvider.GetRequiredService<Login>();

            // 3. 显示窗口
            loginWindow.Show();
        }

        private IServiceProvider ConfigureServices()
        {
            var services = new ServiceCollection();

            // ===== 窗口（瞬态：每次导航都拿到全新实例，避免 Close 后无法再 Show） =====
            services.AddTransient<Login>();
            services.AddTransient<Register>();
            services.AddSingleton<Main>();

            // ===== ViewModel =====
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddSingleton<MainViewModel>();

            // ===== 通用服务（对话框抽象 -> 实现） =====
            services.AddSingleton<Service.IDialogService, Service.lmp.DialogService>();

            // ===== 业务服务（接口 -> 实现） =====
            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IWindowService, WindowService>();
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IBasicSettingService, BasicSettingService>();
            services.AddTransient<IReportSettingService, ReportSettingService>();
            services.AddTransient<ISalaryReportService, SalaryReportService>();

            // ===== 用户控件页面（ViewModel 同时注册，页面构造注入 VM，依赖注入容器统一管理） =====
            services.AddTransient<ViewModel.UserControls.UserManage.UcUserManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcWorkshopManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcProductionLineManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcEmployeeManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcWorkGroupManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcWorkStationManageViewModel>();
            services.AddTransient<ViewModel.UserControls.BasicSetting.UcProcessManageViewModel>();
            services.AddTransient<ViewModel.UserControls.Report.UcWageSettingManageViewModel>();
            services.AddTransient<ViewModel.UserControls.Report.UcCraftRouteManageViewModel>();
            services.AddTransient<ViewModel.UserControls.Report.UcWorkReportManageViewModel>();
            services.AddTransient<ViewModel.UserControls.Salary.UcSalaryReportViewModel>();
            services.AddTransient<ViewModel.UserControls.Salary.UcPerformanceReportViewModel>();

            services.AddTransient<View.UserControls.UserManage.UcUserManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcWorkshopManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcProductionLineManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcEmployeeManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcWorkGroupManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcWorkStationManage>();
            services.AddTransient<View.UserControls.BasicSetting.UcProcessManage>();
            services.AddTransient<View.UserControls.Report.UcWageSettingManage>();
            services.AddTransient<View.UserControls.Report.UcCraftRouteManage>();
            services.AddTransient<View.UserControls.Report.UcWorkReportManage>();
            services.AddTransient<View.UserControls.Salary.UcSalaryReport>();
            services.AddTransient<View.UserControls.Salary.UcPerformanceReport>();

            // ===== EF Core Factory =====
            services.AddDbContextFactory<SQLContext>(options =>
                options.UseSqlServer(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString));

            return services.BuildServiceProvider();
        }

    }
}
