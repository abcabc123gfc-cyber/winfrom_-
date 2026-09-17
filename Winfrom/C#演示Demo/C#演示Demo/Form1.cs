using System;
using System.Collections;
using System.Windows.Forms;

namespace C_演示Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        public void UserPhone()
        {
            Phoneuser phoneuser = new Phoneuser(new Interface_实现());
        }

    }
    #region 例子: 使用接口 实现运行时多态
    //接口动态绑定
    class Test
    {
        int[] ints = { 1, 2, 3, 4, 5 };
        ArrayList arrayList = new ArrayList()
        {
            1, 2, 3, 4, 5
        };
        //对于int 与 ArrayList 都遵循寻 IEnumerable 接口规范: 可迭代对象,
        //那么无需对具体的类型进行定义
        public double Sum(IEnumerable collection)
        {
            double sum = 0;
            foreach (var item in collection)
            {
                sum += Convert.ToDouble(item);
            }
            return sum;
        }
        public double Avg(IEnumerable collection)
        {
            double sum = 0;
            double count = 0;
            foreach (double item in collection)
            {
                sum += item;
                count++;
            }
            return sum / count;
        }
        #endregion

        #region 接口耦合
        // 汽车类运行在 引擎类中, 当引擎类出现问题,机车类的工作就会受阻
        // 引擎类与机车类之间存在耦合, 
        //那么代码的可测试性, 可扩展性, 可维护性, 可复用性, 都会下降
        class Engine
        {
            //Engine 引擎, 发动机
            public int RPM { get; set; }
            public void Work(int gas)
            {
                this.RPM = 1000  *gas;
            }
        }
        class Car
        {
            /// <summary>
            /// 转速
            /// </summary>
            public int Speed { get; set; }
            public Engine _engine;
            public Car()
            {
                this._engine = new Engine();
            }
            public void Run(int gas)
            {
                _engine.Work(gas);
                this.Speed = _engine.RPM / 100;
            }
        }
        #endregion

    }

}
