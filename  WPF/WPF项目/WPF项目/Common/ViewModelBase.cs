using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPF项目.Common
{
    //INotifyPropertyChanged :两个功能
    //1.把Model中数据的变化 通知View
    //2.把view中收集的数据,更新到Model
    public class ViewModelBase : INotifyPropertyChanged
    {
        //事件
        public event PropertyChangedEventHandler PropertyChanged;


        //事件如何调用?
        //1.像方法一样 添加() 执行运算符
        //2.Invoke()调用

        //所以在实现了INotifyPropertyChanged 接口之后,对接口中PropertyChanged事件,做了一层封装,把事件调用封装到了一个方法中,外部只需要调用OnPropertyChanged()函数,就相当于触发了这个事件



        //CallerMemberName 编译就会自动把OnPropertyChanged() 调用时需要的参数名称传递进去
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
