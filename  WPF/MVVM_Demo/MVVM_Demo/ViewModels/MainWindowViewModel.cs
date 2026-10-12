
using MVVM_Demo.Common;
using System.Collections.ObjectModel;
using System.Windows;
using 手写MVVM.Models;


namespace MVVM_Demo.ViewModels
{
    //视图模型
    //注意命名: View的名字+ViewModel
    //理论上每个View都要有一个对应的ViewModel 视图
    //作用是: "桥梁" 连接了View 和 Model的桥梁,
    //主要职责是: 把Model上的数据, 传递到View上, 把 View上收集到的数据,传递给Model
    //一段连接 View 另一端连接到Model
    internal class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            //初始化
            //list不能被监听
            //ObservableCollection 能够被监听到的集合
            students = new ObservableCollection<Student>(MockDataHelper.InitData);

        }
        private ObservableCollection<Student> students;

        //students 属性 改变影响View
        public ObservableCollection<Student> Students
        {
            get
            {
                return students;
            }
            set
            {
                students = value;
                //调用父类方法 通知View
                OnPropertyChanged();
            }
            /*
             * OnPropertyChanged() 方法 主要的职责就通知变化,Model 数据中的数据发生变化, 它负责通知ViewModel
             *由于View 又和View捆绑(Binding语法) 到一起,索引View也会跟着变化, 双数据绑定的第一个方法就实现了
             *
             *通过Command接口 Command属性 ComanParmter Binding 语法
             *问题: 在视图上更新数据 有哪些操作? 添加 删除 编辑
             *
             *在WPF中 以添加为例, 在视图上完成书数据收集后,会调用一个命令如SaveCommand
             *命令在ViewModel中定义,执行 一段业务逻辑
             *视图为什么可以调用ViewModel中的命令? Binding 语法绑定的命令, 命令必须实现IComand接口
             *一个命令以属性的形式存在在ViewModel中,但是要求属性的类型为RelayCommand 但是此类必须实现ICommand接口
             */


        }
        public RelayCommand DeleteCommand
        {
            get
            {
                return new RelayCommand(
                    (p) =>
                    {
                        MessageBox.Show(p.ToString());
                    });
            }
        }
        public RelayCommand ModifyCommand
        {
            get
            {
                return new RelayCommand(
                    (p) =>
                    {
                        MessageBox.Show(p.ToString());
                    });
            }
        }




    }
}
