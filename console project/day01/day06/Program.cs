using System;

namespace day06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //测试string类库
            //Practice practice = new Practice();
            //practice.CopyTo(0, new char[3], 0, "123");
            数组 array = new 数组();
            //array.ArrayDemo();
            //array.Sotr_RandomtDemo();
            多维数组_交错数组 multiArray = new 多维数组_交错数组();
            //multiArray.Arraymore();
            //multiArray .JiaoCuoArray();

            //卡牌
            Practice practice = new Practice();
            //practice.OutCard();

            //practice.ArrarReverse(new int[] {1,2,3,4});
            //practice.GetRandom();
            //practice.ArrayDisarrange(new int[] {1,2,3,4});
            //practice.BubbleSort(new int[] { 1, 3, 2, 4 });
            //数组方法进阶 arrayMethod = new 数组方法进阶();
            //arrayMethod.ArrayMethod();

            //lambda表达式 lambda = new lambda表达式();
            //lambda.TestLambda();

            Peraon peraon = new Peraon("张三", 18, '男');
            Peraon[] peraons = { peraon, new Peraon("小明", 8, '男'), new Peraon("小红", 7, '女') };
            //List<string> strings = new List<string>();
            //foreach (var item in peraons)
            //{
            //    strings.Add(item.Sex.ToString());
            //}
            //string[] man = new string[strings.Count];
            //man = peraon.GetMan(strings);
            //foreach (var item in man)
            //{
            //    Console.WriteLine(item);
            //}
            //peraon.GetPerson(peraons);
            //Console.WriteLine(   peraon.IsAdult(peraons));
            //Console.WriteLine(peraon.GetAgeAvg(peraons));
            //Console.WriteLine(peraon.GetMinor(peraons));

            //声明一个数组并初始化
            int[] arr = { 1, 2, 3, 4, 5 };
            //使用自定义扩展方法,查找满足条件的元素,返回值
            arr.FindLast(x => x > 3);
            //使用自定义扩展方法,查找满足条件的元素,返回索引
            Console.WriteLine(arr.FindIndex(x => x > 3));
            //使用自定义扩展方法,查找满足条件的元素,返回索引
            Console.WriteLine(arr.FindLastIndex1(x => x > 3));
            // 使用自定义扩展方法,查找所有满足条件的元素,返回数组
            int[] temp = arr.FindAll(x => x > 3);
            //使用自定义扩展方法,判断所有元素是否满足条件,返回true/false
            Console.WriteLine(arr.TrueForAll(x => x > 3));
            //使用自定义扩展方法,判断元素是否满足条件:一个及以上元素,返回true/false
            Console.WriteLine(arr.Exists(x => x > 3));

        }
    }
}

