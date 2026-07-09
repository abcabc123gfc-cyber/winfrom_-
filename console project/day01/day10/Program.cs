using System;

namespace day10
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("========== C# 集合学习 Day10 ==========\n");
            Console.WriteLine("请选择要运行的示例：");
            Console.WriteLine("1. Dictionary 基础用法");
            Console.WriteLine("2. Dictionary 进阶用法");
            Console.WriteLine("3. 堆栈和队列");
            Console.WriteLine("4. Hashtable");
            Console.WriteLine("5. SortedList 排序列表");
            Console.WriteLine("6. 装箱与拆箱");
            Console.WriteLine("7. 集合对比与选型");
            Console.WriteLine("8. 进阶集合（Concurrent、SortedDictionary等）");
            Console.WriteLine("0. 全部运行一遍");
            Console.WriteLine();
            Console.Write("请输入编号：");

            string input = Console.ReadLine();
            Console.WriteLine();

            switch (input)
            {
                case "1":
                    字典_Dictionary dict = new 字典_Dictionary();
                    dict.DictionaryDemo();
                    break;
                case "2":
                    Dictionary进阶 dictAdv = new Dictionary进阶();
                    dictAdv.Demo();
                    break;
                case "3":
                    _Queue_Stack stackQueue = new _Queue_Stack();
                    stackQueue.Queue();
                    Console.WriteLine("\n----- 堆栈 -----");
                    stackQueue.StackDemo();
                    break;
                case "4":
                    hashTable hash = new hashTable();
                    hash.HashTableDemo();
                    break;
                case "5":
                    排序列表SortedList sorted = new 排序列表SortedList();
                    sorted.SortedListDemo();
                    break;
                case "6":
                    装箱与拆箱 boxing = new 装箱与拆箱();
                    boxing.BoxingDemo();
                    Console.WriteLine("\n（装箱拆箱示例已运行完成）");
                    break;
                case "7":
                    集合对比与选型 compare = new 集合对比与选型();
                    compare.Demo();
                    break;
                case "8":
                    进阶集合 advanced = new 进阶集合();
                    advanced.Demo();
                    break;
                case "0":
                    RunAll();
                    break;
                default:
                    Console.WriteLine("输入有误，按任意键退出...");
                    break;
            }

            Console.WriteLine("\n按任意键退出...");
            Console.ReadKey();
        }

        /// <summary>
        /// 运行所有示例
        /// </summary>
        static void RunAll()
        {
            Console.WriteLine("===== 1. Dictionary 基础 =====");
            字典_Dictionary dict = new 字典_Dictionary();
            dict.DictionaryDemo();

            Console.WriteLine("\n\n===== 2. Dictionary 进阶 =====");
            Dictionary进阶 dictAdv = new Dictionary进阶();
            dictAdv.Demo();

            Console.WriteLine("\n\n===== 3. 堆栈和队列 =====");
            _Queue_Stack stackQueue = new _Queue_Stack();
            stackQueue.Queue();
            Console.WriteLine("\n----- 堆栈 -----");
            stackQueue.StackDemo();

            Console.WriteLine("\n\n===== 4. Hashtable =====");
            hashTable hash = new hashTable();
            hash.HashTableDemo();

            Console.WriteLine("\n\n===== 5. SortedList =====");
            排序列表SortedList sorted = new 排序列表SortedList();
            sorted.SortedListDemo();

            Console.WriteLine("\n\n===== 6. 装箱与拆箱 =====");
            装箱与拆箱 boxing = new 装箱与拆箱();
            boxing.BoxingDemo();

            Console.WriteLine("\n\n===== 7. 集合对比与选型 =====");
            集合对比与选型 compare = new 集合对比与选型();
            compare.Demo();

            Console.WriteLine("\n\n===== 8. 进阶集合 =====");
            进阶集合 advanced = new 进阶集合();
            advanced.Demo();
        }
    }
}
