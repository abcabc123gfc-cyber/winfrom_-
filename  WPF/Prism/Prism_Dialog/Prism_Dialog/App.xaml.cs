using Prism.Ioc;
using Prism.Services.Dialogs;
using Prism_Dialog.Views;
using System.Windows;

namespace Prism_Dialog
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
        protected override void InitializeShell(Window shell)
        {
            //base.InitializeShell(shell);
            //3.使用对话框
            //对话框服务 已经被Prism 中的Ioc容器自动管理了
            IDialogService dialogService = Container.Resolve<IDialogService>();
            dialogService.ShowDialog("Login", dialogResult =>
            {
                if (dialogResult.Result != ButtonResult.OK)
                {
                    App.Current.Shutdown();
                }
            });
        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterDialog<Login>();
            containerRegistry.RegisterDialog<Window1>();
            containerRegistry.RegisterDialog<Window1>();
        }
    }
}
