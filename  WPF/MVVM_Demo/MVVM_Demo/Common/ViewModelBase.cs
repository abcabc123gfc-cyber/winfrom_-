using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace MVVM_Demo.Common
{
    //Notify 通知 Property 属性 Changed 已变更
    //INotifyPropertyChanged :两个功能
    //1.把Model中的数据的变化 通知到View
    //2.把View中收集的数据, 更新到Model
    internal class ViewModelBase : INotifyPropertyChanged
    {
        //事件
        public event PropertyChangedEventHandler PropertyChanged;

        //事件调用方法
        //1.像方法一样 添加() 执行运算符
        //2.Invoke()调用

        //所以在实现了INotifyPropertyChanged 接口之后,对接口中 PropertyChanged 事件做了一层封装,把事件封装到一个方法中,外部只需要调用 外部只需要调用OnPropertyChanged() 方法 ,相当于触发了这个事件

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
