using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace day10
{
    /// <summary>
    /// 进阶集合补充
    /// 包含：线程安全集合、SortedDictionary、LinkedList 等
    /// </summary>
    internal class 进阶集合
    {
        public void Demo()
        {
            Console.WriteLine("========== 进阶集合 ==========\n");

            // ========================================
            // 一、线程安全集合（Concurrent 系列）
            // ========================================
            Console.WriteLine("【一、线程安全集合 Concurrent 系列】");
            Console.WriteLine("多线程环境下，普通集合（List、Dictionary）不是线程安全的，");
            Console.WriteLine("多个线程同时读写会报错、数据错乱。这时候要用 Concurrent 系列。\n");

            // 1. ConcurrentDictionary —— 线程安全的字典
            Console.WriteLine("1. ConcurrentDictionary（线程安全字典）");
            ConcurrentDictionary<string, int> concurrentDict = new ConcurrentDictionary<string, int>();

            // 添加或更新（线程安全的）
            concurrentDict.TryAdd("张三", 95);
            concurrentDict.AddOrUpdate("李四", 88, (key, oldValue) => oldValue + 5);

            // 安全取值
            if (concurrentDict.TryGetValue("张三", out int score))
            {
                Console.WriteLine($"   张三的分数：{score}");
            }

            // 安全删除
            if (concurrentDict.TryRemove("张三", out int removed))
            {
                Console.WriteLine($"   删除了张三，分数是：{removed}");
            }
            Console.WriteLine();

            // 2. ConcurrentQueue —— 线程安全队列
            Console.WriteLine("2. ConcurrentQueue（线程安全队列）");
            ConcurrentQueue<int> concurrentQueue = new ConcurrentQueue<int>();
            concurrentQueue.Enqueue(1);
            concurrentQueue.Enqueue(2);
            concurrentQueue.Enqueue(3);

            if (concurrentQueue.TryDequeue(out int result))
            {
                Console.WriteLine($"   出队：{result}");
            }

            if (concurrentQueue.TryPeek(out int peek))
            {
                Console.WriteLine($"   看下队首：{peek}");
            }
            Console.WriteLine();

            // 3. ConcurrentStack —— 线程安全栈
            Console.WriteLine("3. ConcurrentStack（线程安全栈）");
            ConcurrentStack<int> concurrentStack = new ConcurrentStack<int>();
            concurrentStack.Push(1);
            concurrentStack.Push(2);

            if (concurrentStack.TryPop(out int popResult))
            {
                Console.WriteLine($"   出栈：{popResult}");
            }
            Console.WriteLine();

            Console.WriteLine("💡 什么时候用 Concurrent 集合？");
            Console.WriteLine("   → 多线程、任务并行、生产者消费者模式");
            Console.WriteLine("   → 单线程场景用普通集合就行，Concurrent 有性能开销");
            Console.WriteLine();


            // ========================================
            // 二、SortedDictionary —— 自动排序的字典
            // ========================================
            Console.WriteLine("【二、SortedDictionary（自动排序的字典）】");
            Console.WriteLine("和 Dictionary 用法一样，只是键会自动排序\n");

            SortedDictionary<int, string> sortedDict = new SortedDictionary<int, string>();
            sortedDict.Add(3, "丙");
            sortedDict.Add(1, "甲");
            sortedDict.Add(2, "乙");

            Console.WriteLine("按键自动排序后的结果：");
            foreach (var item in sortedDict)
            {
                Console.WriteLine($"   {item.Key}：{item.Value}");
            }

            Console.WriteLine("\n💡 SortedList vs SortedDictionary：");
            Console.WriteLine("   → SortedList 占用内存少，索引访问快，但插入删除慢");
            Console.WriteLine("   → SortedDictionary 插入删除快，但内存占用大");
            Console.WriteLine("   → 数据量小、不常改选 SortedList，数据量大、经常改选 SortedDictionary");
            Console.WriteLine();


            // ========================================
            // 三、LinkedList —— 双向链表
            // ========================================
            Console.WriteLine("【三、LinkedList（双向链表）】");
            Console.WriteLine("插入删除快，但查找慢（只能从头/尾遍历）\n");

            LinkedList<string> linkedList = new LinkedList<string>();
            linkedList.AddLast("A");
            linkedList.AddLast("C");
            linkedList.AddLast("D");

            // 在中间插入（找到节点后插入很快）
            LinkedListNode<string> nodeC = linkedList.Find("C");
            if (nodeC != null)
            {
                linkedList.AddBefore(nodeC, "B");
            }

            Console.WriteLine("链表内容：");
            foreach (string item in linkedList)
            {
                Console.Write(item + " → ");
            }
            Console.WriteLine("null");

            Console.WriteLine("\n💡 什么时候用 LinkedList？");
            Console.WriteLine("   → 需要频繁在中间插入删除的场景");
            Console.WriteLine("   → 大部分场景 List 够用了，LinkedList 用得不多");
            Console.WriteLine();


            // ========================================
            // 四、ObservableCollection —— WPF 绑定专用
            // ========================================
            Console.WriteLine("【四、ObservableCollection（WPF 绑定专用）】");
            Console.WriteLine("WPF 里绑定列表时，如果用 List，添加删除元素界面不会自动更新");
            Console.WriteLine("用 ObservableCollection 就会自动通知界面更新");
            Console.WriteLine("\n用法和 List 几乎一样：");
            Console.WriteLine("   ObservableCollection<string> list = new ObservableCollection<string>();");
            Console.WriteLine("   list.Add(\"xxx\");  // 界面自动更新");
            Console.WriteLine();
            Console.WriteLine("💡 这个在你 WPF 项目里会经常用到～");
        }
    }
}
