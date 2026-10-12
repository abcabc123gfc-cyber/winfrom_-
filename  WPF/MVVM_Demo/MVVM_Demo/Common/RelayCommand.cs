using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MVVM_Demo.Common
{
    //ICmoamnd 接口中 有一个事件: CanExcuteChanged 两个方法: CanExecute() 和 Execute()
    //CanExcuteChanged: 执行变化的事件
    // _canExecute 是否允许变化 换句话说 是否允许 添加 编辑 删除 等操作 类似与" 保安"
    // //返回 true 表示允许执行 Execute() 方法 返回 false 表示不允许执行 Execute() 方法
    //Excute() 方法 执行变化的逻辑
    internal class RelayCommand : ICommand
    {
        //委托 要求输入一个参数 返回值是Bool
        private Predicate<object> _canExecute;
        //要求输入一个参数 无返回值 的委托
        private Action<object> _execute;

        //输入的参数很重要 主要负责把页面上的收集的信息传递到ViewModel
        public RelayCommand(Predicate<object> canExecute, Action<object> execute)
        {
            this._canExecute = canExecute;
            this._execute = execute;
        }
        public RelayCommand(Action<object> execute)
        {
            this._canExecute = (o) => true;
            this._execute = execute;
        }

        //让事件支持多播
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
