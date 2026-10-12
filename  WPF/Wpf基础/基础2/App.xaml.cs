using System;
using System.Windows;

namespace 基础2
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //窗体的三种启动方式 s = new 窗体的三种启动方式();
            //s.Show();
        }

        //报错: 程序定义了多个入口点
        //Main方法只能有一个,不能有多个,否则就会报错
        //解决办法:在App.xaml文件上,右键==>属性===>生成操作===>选择"无",不让他产生Main方法,开发者才能自定义Main方法,然后在Main方法中,设置需要显示的窗体

        //报错:调用线程必须为 STA，因为许多 UI 组件都需要。

        //解决办法: 在Main方法上面添加一个特性STAThread,理解为Winform中,分线程不允许操作控件,控件运行需要一个安全的线程环境,这个特性的作用,就是告诉编译器,这个方法会在单线程环境下运行,以确保UI组件正常工作
        [STAThread]
        public static void Main(string[] args)
        {
            //方式1
            //窗体的三种启动方式 s = new 窗体的三种启动方式();
            //s.Show();

            //方式2
            //Application app=new Application();
            //app.Run(new 窗体的三种启动方式());

            //方式3
            //窗体的三种启动方式 a = new 窗体的三种启动方式();
            //Application app = new Application();
            //app.MainWindow = a;
            //a.Show();
            //app.Run();

            //方式4
            //Application application = new Application();
            //application.StartupUri = new Uri("窗体的三种启动方式.xaml", UriKind.Relative);
            //application.Run();
        }
    }
}
