using Prism.Ioc;
using Prism.Modularity;
using prism_Model.Views;
using System.Linq.Expressions;
using System.Windows;

namespace prism_Model
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
        //在主项目中使用模块的步骤:
        //1.先定义Module 使用Prism Module模板 注意:每个模块都有独立的视图 视图模型 配置文件
        //2.把模块引入到主项目  在"依赖项" 右键 添加引用即可
        //3.在主项目注册
        protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
        {
            //base.ConfigureModuleCatalog(moduleCatalog);
            moduleCatalog.AddModule<ModuleA.ModuleAModule>();
            moduleCatalog.AddModule<ModuleB.ModuleBModule>();
        }

        protected override IModuleCatalog CreateModuleCatalog()
        {

            //注册方式2:Directory配置模块目录
            //string path = Path.Combine(Environment.CurrentDirectory, "/Modules");
            //return new DirectoryModuleCatalog() { ModulePath = path };

            //注册方式3: 使用 配置文件 app.config
            return new ConfigurationModuleCatalog();
        }
    }
}
