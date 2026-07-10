using System;
using System.Collections;
using System.Collections.Generic;

namespace day10
{
    /// <summary>
    /// 各种集合的对比与选型建议
    /// 帮你搞清楚：什么时候用什么集合
    /// </summary>
    internal class 集合对比与选型
    {
        public void Demo()
        {
            Console.WriteLine("========== C# 集合大对比 ==========\n");

            // ========================================
            // 一、Hashtable vs Dictionary（最常问的面试题）
            // ========================================
            Console.WriteLine("【一、Hashtable vs Dictionary】");
            Console.WriteLine("┌─────────────┬──────────────────┬──────────────────────┐");
            Console.WriteLine("│   对比项     │    Hashtable     │   Dictionary<TKey,T> │");
            Console.WriteLine("├─────────────┼──────────────────┼──────────────────────┤");
            Console.WriteLine("│  类型安全    │      ❌ 不是      │       ✅ 是（泛型）   │");
            Console.WriteLine("│  装箱拆箱    │   ✅ 有，性能差   │      ❌ 无，性能好    │");
            Console.WriteLine("│  线程安全    │  部分支持（老的） │     ❌ 不支持         │");
            Console.WriteLine("│  推荐使用    │   ❌ 不推荐       │     ✅ 推荐           │");
            Console.WriteLine("│  出现版本    │  .NET 1.0（老）  │   .NET 2.0+（新）    │");
            Console.WriteLine("└─────────────┴──────────────────┴──────────────────────┘");
            Console.WriteLine();

            // 演示：Hashtable 的装箱拆箱问题
            Console.WriteLine("Hashtable 的问题演示：");
            Hashtable ht = new Hashtable();
            ht.Add("num", 123);  // int 装箱成 object，有性能损耗
            int num = (int)ht["num"];  // 还要拆箱，麻烦又慢
            Console.WriteLine($"  Hashtable 取值要拆箱：{num}");

            Console.WriteLine("\nDictionary 的优势：");
            Dictionary<string, int> dict = new Dictionary<string, int>();
            dict.Add("num", 123);  // 无装箱，直接存int
            int num2 = dict["num"];  // 直接拿int，不用转
            Console.WriteLine($"  Dictionary 直接拿类型：{num2}");
            Console.WriteLine();


            // ========================================
            // 二、常用集合适用场景总结
            // ========================================
            Console.WriteLine("【二、常用集合适用场景】");
            Console.WriteLine();

            Console.WriteLine("1️⃣ List<T> —— 最常用的列表");
            Console.WriteLine("   ✅ 优点：按索引访问快，添加删除方便");
            Console.WriteLine("   ❌ 缺点：查找元素要遍历，慢");
            Console.WriteLine("   🎯 适用：有序列表、需要按索引访问的数据");
            Console.WriteLine();

            Console.WriteLine("2️⃣ Dictionary<TKey,TValue> —— 键值对查找");
            Console.WriteLine("   ✅ 优点：按键查找超快（O(1)）");
            Console.WriteLine("   ❌ 缺点：占用内存比List大，无序");
            Console.WriteLine("   🎯 适用：需要快速查找、缓存、映射关系");
            Console.WriteLine();

            Console.WriteLine("3️⃣ HashSet<T> —— 去重集合");
            Console.WriteLine("   ✅ 优点：自动去重，判断是否存在超快");
            Console.WriteLine("   ❌ 缺点：不能重复，无序");
            Console.WriteLine("   🎯 适用：去重、标签、交集并集运算");
            Console.WriteLine();

            Console.WriteLine("4️⃣ Queue<T> —— 先进先出队列");
            Console.WriteLine("   ✅ 优点：先进先出，入队出队快");
            Console.WriteLine("   🎯 适用：任务排队、消息队列、打印队列");
            Console.WriteLine();

            Console.WriteLine("5️⃣ Stack<T> —— 后进先出栈");
            Console.WriteLine("   ✅ 优点：后进先出，入栈出栈快");
            Console.WriteLine("   🎯 适用：撤销操作、表达式求值、浏览器后退");
            Console.WriteLine();

            Console.WriteLine("6️⃣ SortedList / SortedDictionary —— 自动排序");
            Console.WriteLine("   ✅ 优点：键自动排序");
            Console.WriteLine("   ❌ 缺点：插入删除比Dictionary慢");
            Console.WriteLine("   🎯 适用：需要有序键值对的场景");
            Console.WriteLine();


            // ========================================
            // 三、HashSet 演示（很实用的一个集合）
            // ========================================
            Console.WriteLine("\n【三、HashSet 演示（去重神器）】");

            HashSet<string> tags = new HashSet<string>();
            tags.Add("C#");
            tags.Add("WPF");
            tags.Add("C#");  // 重复添加，不会报错，但也不会加进去
            tags.Add(".NET");

            Console.WriteLine($"HashSet 元素个数（自动去重）：{tags.Count}");
            Console.WriteLine($"是否包含'WPF'：{tags.Contains("WPF")}");

            // 集合运算（超好用）
            HashSet<string> tags2 = new HashSet<string> { "C#", "Java", "Python" };

            // 交集（两个集合都有的）
            tags.IntersectWith(tags2);
            Console.WriteLine($"\n交集（两个都有的）：{string.Join("、", tags)}");

            // 并集（两个合起来去重）
            HashSet<string> unionSet = new HashSet<string> { "C#", "WPF" };
            unionSet.UnionWith(tags2);
            Console.WriteLine($"并集（合起来去重）：{string.Join("、", unionSet)}");

            // 差集（A有B没有的）
            HashSet<string> exceptSet = new HashSet<string> { "C#", "WPF", ".NET" };
            exceptSet.ExceptWith(tags2);
            Console.WriteLine($"差集（A有B没有的）：{string.Join("、", exceptSet)}");
            Console.WriteLine();


            // ========================================
            // 四、选型速查表
            // ========================================
            Console.WriteLine("【四、选型速查表】");
            Console.WriteLine();
            Console.WriteLine("  需要按索引访问？        → List<T>");
            Console.WriteLine("  需要快速按键查找？      → Dictionary<TKey,TValue>");
            Console.WriteLine("  需要去重？              → HashSet<T>");
            Console.WriteLine("  需要先进先出？          → Queue<T>");
            Console.WriteLine("  需要后进先出？          → Stack<T>");
            Console.WriteLine("  需要自动排序的键值对？  → SortedDictionary<TKey,TValue>");
            Console.WriteLine("  多线程环境？            → Concurrent 系列集合");
            Console.WriteLine();

            Console.WriteLine("💡 一句话总结：");
            Console.WriteLine("   90% 的场景用 List 和 Dictionary 就够了");
            Console.WriteLine("   去重用 HashSet，排队用 Queue，栈操作 Stack");
            Console.WriteLine("   老的 ArrayList、Hashtable 就别用了，都有泛型替代");
        }
    }
}
