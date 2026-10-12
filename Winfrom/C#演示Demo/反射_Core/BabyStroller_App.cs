using System.Runtime.Loader;
using 反射_Core_01_主程序_SDK;

namespace 反射
{
    internal class BabyStroller_App
    {
        //会从程序 debug 地方 下 Animals 文件夹下 把第三方的插件加载进来
        //调用 Voice 方法 接受参数允许叫声的次数
        public static void Main(string[] args)
        {
            //当前程序的文件夹
            //Console.WriteLine(Environment.CurrentDirectory);
            //当前程序所在目录 中程序本身文件
            //Console.WriteLine(AppDomain.CurrentDomain.BaseDirectory);
            var folder = Path.Combine(Environment.CurrentDirectory, "Animals");
            var files = Directory.GetFiles(folder);
            var animalsTypes = new List<Type>();
            //遍历文件 获取 .dll 文件
            foreach (var file in files)
            {
                //把指定路径 file 上的程序集文件（通常是.dll 或 .exe）加载到.NET 运行时的“默认程序集加载上下文”中，并返回加载后的 Assembly 对象。
                var assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(file);
                var types = assembly.GetTypes();
                //animalsTypes.AddRange(types);
                foreach (var type in types)
                {
                    //if (type.IsInterface)
                    //    continue;
                    //if (type.GetInterface("IAnimal") != null)
                    //{
                    //    var animal = Activator.CreateInstance(type);
                    //    var animalAsIAnimal = animal as IAnimal;
                    //    animalAsIAnimal.Voice(5);
                    //}
                    //if (type.GetInterface("IAnimal") != null)
                    //{

                    //}
                    //注意 获取接口的方法是复数 
                    //意图：遍历插件程序集里的每个 type，如果它「实现了 IAnimal」且「没有被标记[Unfinished]」，就收集起来，后续用 Activator.CreateInstance 创建实例
                    //GetCustomAttributes(false)
                    //false：只看当前类型自己身上直接写的特性，不查父类 / 基接口。
                    //true：还会把父类（以及某些场景下的接口，具体看运行时实现）上的特性合并进来。

                    //!type.IsAbstract // 排除抽象类  !type.IsInterface // 排除接口本身
                    //if (type.GetInterfaces().Contains(typeof(IAnimal)) && !type.IsDefined(typeof(UnfinishedAttribute), inherit: false)&&!type.IsAbstract && !type.IsInterface)
                    //{
                    //    //动态创建实例
                    //    animalsTypes.Add(type);
                    //}
                   //---或者使用
                   if (type.GetInterfaces().Contains(typeof(IAnimal)))
                    {
                        //x.GetType() == typeof(UnfinishedAttribute)   // 精确匹配,派生类不命中
                        //x is UnfinishedAttribute                      // 匹配自身 + 所有派生类
                        if (type.GetCustomAttributes(false).Any(x => x.GetType() == typeof(UnfinishedAttribute))) continue;
                        animalsTypes.Add(type);
                    }
                }
            }
            while (true)
            {
                int i = 0;
                foreach (var animalType in animalsTypes)
                {

                    Console.WriteLine($"{++i},{animalType.Name}");
                }
                Console.WriteLine("========= 类型名称打印完毕=========== ");
                Console.WriteLine("选择动物序号");
                int index = int.Parse(Console.ReadLine());
                if (index > animalsTypes.Count || index < 0)
                {
                    Console.WriteLine("选项错误");
                    continue;
                }
                Console.WriteLine("选择执行次数");
                int times = int.Parse(Console.ReadLine());
                var t = animalsTypes[index - 1];
                var m = t.GetMethod("Voice");
                var o = Activator.CreateInstance(t) as IAnimal;
                //可以直接实使用接口访问
                o.Voice(times);
                //m?.Invoke(o,new object[]{times});
            }
        }
    }
}
