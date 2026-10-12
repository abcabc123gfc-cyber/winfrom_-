using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace _01_基础使用
{
    internal class MainWindowViewModel:ObservableObject
    {
       

        private string name = "吴亦凡";

        public string Name
        {
            get { return name; }
            //让属性支持通知
            //写法1:
            //         set
            //         {
            //      name  = value; 
            //   OnPropertyChanged(); 
            //}

            //写法2:
            //set
            //{
            //	name = value;
            //	SetProperty(ref name, value);
            //}
            //写法3:
            set => SetProperty(ref name, value);
        }
        private int age = 18;

        public int Age
        {
            get { return age; }
            set { age = value; OnPropertyChanged(); }
        }
        //命令的写法:
        //写法1:建议写法
        public RelayCommand FirstCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    MessageBox.Show($"无参命令{Name}--{Age}");
                });
            }
        }
        //写法2:
        public bool Sex { get; }
        //public RelayCommand FirstCommand { get; }






        public RelayCommand<object> SecondCommand
        {
            get
            {
                return new RelayCommand<object>(obj =>
                {
                    MessageBox.Show($"有参命令{obj}");
                });
            }
        }
        public IAsyncRelayCommand FirstAsyncCommand
        {
            get
            {
                return new AsyncRelayCommand(async () =>
                {
                    await Task.Delay(1000);
                    MessageBox.Show($"异步无参命令");
                });
            }
        }

        public IAsyncRelayCommand<object> SecondAsyncCommand
        {
            get
            {
                return new AsyncRelayCommand<object>(async obj =>
                {
                    await Task.Delay(1000);
                    MessageBox.Show($"异步无参命令{obj}");
                });
            }
        }
    }
}
