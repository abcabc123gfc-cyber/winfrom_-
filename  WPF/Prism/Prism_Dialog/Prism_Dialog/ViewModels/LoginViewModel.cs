using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using Prism_Dialog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Dialog.ViewModels
{
    //1.把用户控件变成一个对话框,只需要实现IDialogAware接口即可
    internal class LoginViewModel : BindableBase, IDialogAware
    {
        //对话框的标题
        public string Title => "登录";
        //事件  对话框关闭的时候触发的事件 (确认 取消)
        public event Action<IDialogResult> RequestClose;

        //是否可以关闭该对话框  返回true的时候 对话框可以关闭 false 不能关闭
        public bool CanCloseDialog()
        {
            return true;
        }

        //对话框关闭之后执行的函数
        public void OnDialogClosed()
        {

        }

        //对话框打开之后执行的函数
        public void OnDialogOpened(IDialogParameters parameters)
        {

        }


        private User users = new User() { Account = "admin", Password = "12345" };

        public User Users
        {
            get { return users; }
            set
            {
                SetProperty(ref users, value);
            }
        }

        public DelegateCommand LoginCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    if (Users.Account == "admin" && Users.Password == "12345")
                    {
                        //4.
                        RequestClose.Invoke(new DialogResult(ButtonResult.OK));
                    }

                });
            }
        }
    } 
}
