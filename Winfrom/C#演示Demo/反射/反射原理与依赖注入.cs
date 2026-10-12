using System;
using System.Net.Mail;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace 反射
{
    internal class 反射原理与依赖注入
    {
        //以 .NET Core 为例 
        //反射式在内存中动态 获取对象信息, 拿到对象与 它绑定的类型信息的描述
        //用这些描述去创建 新的对象, 会对程序的性能有一定的影响
        //在程序当中不要盲目 或者过度的使用反射 以免影响程序的性能
        // ---------- 接口 ----------

        public static void Main(string[] args)
        {
            #region  直接使用反射
            //ITruck truck = new HeavyTank();
            ////--- 使用反射---
            ////获取 对象在运行时的类型 如方法 属性 成员变量
            //var t= truck.GetType();
            ////Activator: 激活器 Instance 实例
            //object o =Activator.CreateInstance(t);
            ////调用方法
            //MethodInfo firMi = t.GetMethod("Fire");
            //MethodInfo run = t.GetMethod("Run");
            ////执行方法 
            ////参数1 调用对象 参数2 方法参数
            //firMi.Invoke(o, null);
            //run.Invoke(o, null);
            #endregion
            #region 依赖注入
            #region 1.1 使用封装好的反射
            //封装好的反射最重要的功能就是 依赖注入 dependency injection di (DID)

            //依赖反转 dependence inversion principle (DIP)

            //依赖反转 是一个软件设计原则 概念
            //依赖注入: 在概念的基础上 结合接口 结合反射机制 所形成的应用
            //依赖注入与 依赖反转不同
            //依赖注入: 创建对象时, 把对象所依赖的类或者对象, 创建好, 注入到对象当中
            #endregion
            #region 1.2 依赖注入
            //依赖注入需要借助 依赖注入框架 
            //neGet 包: Microsoft.Extensions.DependencyInjection
            //引入命名空间 using Microsoft.Extensions.DependencyInjection;

            //依赖注入有一个重要的东西 : Container 容器
            //容器: service provider   存放对象实例的 仓库

            //把各种各样的类型与接口 放在容器里边
            //注册类型时 可以设置 每次创建新的对象实例 还创建一个单例模式, 每次返回同一个对象实例
            //可以 从 容器 中添加对象实例 

            #endregion
            #region 1.3 依赖注入的使用
            //ServiceCollection 容器
            // ---------- 1. 创建服务容器 ----------
            // ServiceCollection 是微软官方轻量级 IOC 容器，用来"登记"接口与实现的映射关系
            // 此时容器里还是空的，只是准备了一个"注册表"
            var serviceCollection = new ServiceCollection();
            //参数1 接口类型 参数2 那个类实现了接口
            //typeof(): 动态获取 ITruck 类型描述
            // ---------- 2. 注册服务映射 ----------
            // AddScoped(serviceType, implementationType)
            //   参数1 serviceType      ：要注册的"接口类型"（使用时按它来解析）
            //   参数2 implementationType：真正实现该接口的"具体类"
            // typeof(ITruck)    ：在运行时动态获取 ITruck 的 Type 描述（不是实例）
            // typeof(HeavyTank) ：同理，获取 HeavyTank 的 Type 描述
            // AddScoped 的生命周期：
            //   每次"新建一个作用域(Scope)"时创建一个实例，
            //   同一个作用域内多次解析返回同一个对象；
            //   作用域结束（Dispose）时实例被释放。
            //   其他可选生命周期：
            //     AddSingleton ：整个应用只创建一个实例（全局共享）
            //     AddTransient ：每次解析都 new 一个新实例
            serviceCollection.AddScoped(typeof(ITruck), typeof(HeavyTank));
  
            // ---------- 3. 构建服务提供者 ----------
            // BuildServiceProvider() 把上面的"注册表"编译成一个可用的容器（ServiceProvider）
            // 之后通过 sp 就能按类型取出对应的实例
            // 注意：BuildServiceProvider 之后一般不要再往 serviceCollection 里注册新服务
            var sp = serviceCollection.BuildServiceProvider();

            // ---------- 4. 从容器解析并使用 ----------
            // 按接口类型解析：容器会根据注册关系自动 new 一个 HeavyTank 返回
            ITruck truck=sp.GetService<ITruck>();
            // 调用接口方法，实际执行的是 HeavyTank 里的实现
            truck.Fire();       
            truck.Run();
            //程序后期 更换实现时 , 只需要修改注册关系 不使用依赖注入如:
            //ITruck truck = new HeavyTank(); 需要更改多处 new HeavyTank();
            #endregion
            #region 1.4 依赖注入对象的使用
            Console.WriteLine(  "-------");
            DependencyInjection();

            #endregion
            #endregion
            #region 使用反射追求更松的耦合
            //更松的耦合应用在 插件式编程
            //插件式编程: 插件式编程就是把一个功能模块封装成插件, 然后在主程序中动态加载插件
            //不与主体程序 一起编译, 但是与主题程序一起工作的组件 往往是由第三方来提供的
            //好处:
            //以主体程序为中心生成一个生态圈, 有人不断更新主体程序, 有人不断用插件添加新功能,并且从中获利
            //如: office , Vision Studio 

            //一般情况下 主体程序的发布 包含程序开发接口 API 这样的程序开发包SDK
            //API Application Programming Interface 
            //SDK Software Development Kit
            //使用 SDK中的 API 开发插件 会比较容易得开发出插件,也能比较高效的与主程序对接

            //API 中不一定都是接口 , 也可以肯是 一组 方法 类 接口\

            //为什么使用 SDK 中的 API?
            //使用反射很容易 把耦合降到很低, 写程序会非常自由
            //为了避免不必要的错误 如 方法的大小写等, 会使用 SDK 中的 API 来约束 开发者的开发,减去工作量
            #endregion
        }
        public static void DependencyInjection()
        { 
            //使用注册的类型,创建实例,注入 构造器中
            var sc=new ServiceCollection();
            sc.AddScoped(typeof(ITruck), typeof(HeavyTank));
            sc.AddScoped(typeof(IVehicle), typeof(Car));
            sc.AddScoped<Dirver>();
            var sp=sc.BuildServiceProvider();
            //-- 调用 --
            //会找 实现 IVehicle 的实例, 实现对应的实例,传递给 Dirver
            var dirver =sp.GetService<Dirver>();
            dirver.Drive();
        }

        class Dirver
        {
            public IVehicle _truck;

            public Dirver(IVehicle powerSupply)
            {
                this._truck = powerSupply;
            }

            public void Drive()
            {
                _truck.Run();

            }
        }
        public interface IVehicle
        {
            void Run();
        }

        public interface ITruck : IVehicle, IWeapon
        {


        }

        public interface IWeapon
        {
            void Fire();
        }

        // ---------- 车辆类 ----------

        internal class Car : IVehicle
        {
            public void Run()
            {
                Console.WriteLine("Run");

            }
        }

        public class Truck : IVehicle
        {
            public void Run()
            {
                Console.WriteLine("Run");
            }
        }

        // ---------- 坦克类 ----------

        internal class HeavyTank : ITruck
        {
            public void Fire()
            {
                Console.WriteLine("开火");
            }

            public void Run()
            {
                Console.WriteLine("MediumTank ... Run");
            }
        }

        internal class LightTank : ITruck
        {
            public void Fire()
            {
                Console.WriteLine("开火");
            }

            public void Run()
            {
                Console.WriteLine("LightTank... Run");
            }
        }

        internal class MediumTank : ITruck
        {
            public void Fire()
            {
                Console.WriteLine("开火");
            }

            public void Run()
            {
                Console.WriteLine("MediumTank ... Run");
            }
        }
    }
}
