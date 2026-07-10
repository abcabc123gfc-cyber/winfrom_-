using System;
using System.Collections.Generic;
using System.Linq;

namespace day10
{
    /// <summary>
    /// Dictionary 进阶用法补充
    /// 包含：安全取值、遍历方式、常用操作、性能注意事项
    /// </summary>
    internal class Dictionary进阶
    {
        public void Demo()
        {
            Console.WriteLine("========== Dictionary 进阶用法 ==========\n");

            Dictionary<string, int> scoreDict = new Dictionary<string, int>()
            {
                { "张三", 95 },
                { "李四", 88 },
                { "王五", 76 }
            };

            // ========================================
            // 1. 安全取值：TryGetValue（推荐！）
            // ========================================
            Console.WriteLine("【1. 安全取值 TryGetValue】");

            // 不推荐：直接用 [键] 取值，键不存在会抛 KeyNotFoundException
            // int score = scoreDict["赵六"];  // 这行会报错！

            // 推荐：用 TryGetValue，不会抛异常，返回bool表示是否找到
            if (scoreDict.TryGetValue("张三", out int zhangsanScore))
            {
                Console.WriteLine($"找到张三，分数：{zhangsanScore}");
            }
            else
            {
                Console.WriteLine("张三不存在");
            }

            if (scoreDict.TryGetValue("赵六", out int zhaoliuScore))
            {
                Console.WriteLine($"找到赵六，分数：{zhaoliuScore}");
            }
            else
            {
                Console.WriteLine("赵六不存在（TryGetValue 不会报错，返回false）");
            }
            Console.WriteLine();


            // ========================================
            // 2. 三种遍历方式
            // ========================================
            Console.WriteLine("【2. 遍历方式】");

            // 方式一：遍历键值对（最常用）
            Console.WriteLine("遍历键值对：");
            foreach (KeyValuePair<string, int> item in scoreDict)
            {
                Console.WriteLine($"  {item.Key}：{item.Value}分");
            }

            // 方式二：只遍历键
            Console.WriteLine("\n只遍历键：");
            foreach (string key in scoreDict.Keys)
            {
                Console.WriteLine($"  键：{key}");
            }

            // 方式三：只遍历值
            Console.WriteLine("\n只遍历值：");
            foreach (int value in scoreDict.Values)
            {
                Console.WriteLine($"  值：{value}");
            }
            Console.WriteLine();


            // ========================================
            // 3. 添加或更新：两种写法
            // ========================================
            Console.WriteLine("【3. 添加或更新】");

            // 方式一：Add 方法（键已存在会报错）
            try
            {
                scoreDict.Add("赵六", 82);
                Console.WriteLine("添加赵六成功");
            }
            catch (ArgumentException)
            {
                Console.WriteLine("添加失败：赵六已存在");
            }

            // 方式二：索引器写法（键存在就更新，不存在就添加，不会报错）
            scoreDict["张三"] = 98;  // 更新已有的
            scoreDict["钱七"] = 90;  // 添加新的
            Console.WriteLine("用索引器更新张三为98分，添加钱七为90分");
            Console.WriteLine();


            // ========================================
            // 4. 删除元素
            // ========================================
            Console.WriteLine("【4. 删除元素】");

            // Remove 返回bool表示是否删除成功
            bool removed = scoreDict.Remove("王五");
            Console.WriteLine($"删除王五：{(removed ? "成功" : "失败（不存在）")}");

            // 清空所有元素
            // scoreDict.Clear();
            Console.WriteLine();


            // ========================================
            // 5. 字典的长度和判断
            // ========================================
            Console.WriteLine("【5. 长度和判断】");
            Console.WriteLine($"字典元素个数：{scoreDict.Count}");
            Console.WriteLine($"是否包含键'张三'：{scoreDict.ContainsKey("张三")}");
            Console.WriteLine($"是否包含值98：{scoreDict.ContainsValue(98)}");
            Console.WriteLine();


            // ========================================
            // 6. 结合 LINQ 操作（需要引用 System.Linq）
            // ========================================
            Console.WriteLine("【6. LINQ 操作】");

            // 找出分数大于80的人
            var highScore = scoreDict.Where(s => s.Value > 80)
                                     .Select(s => s.Key)
                                     .ToList();
            Console.WriteLine("分数大于80的人：" + string.Join("、", highScore));

            // 按分数排序
            var sorted = scoreDict.OrderByDescending(s => s.Value).ToList();
            Console.WriteLine("\n按分数从高到低排序：");
            foreach (var item in sorted)
            {
                Console.WriteLine($"  {item.Key}：{item.Value}分");
            }
            Console.WriteLine();


            // ========================================
            // 7. 性能注意事项
            // ========================================
            Console.WriteLine("【7. 性能注意事项】");
            Console.WriteLine("① Dictionary 查找是 O(1)，比 List 遍历快很多");
            Console.WriteLine("② 键的类型最好用 string、int 等基础类型，自定义类型要重写 GetHashCode 和 Equals");
            Console.WriteLine("③ 预估数据量大时，可以在构造函数里指定容量，减少扩容开销：");
            Console.WriteLine("   Dictionary<string, int> dict = new Dictionary<string, int>(10000);");
            Console.WriteLine("④ 遍历字典时不要删除元素，会报错，要删的话先转成列表再遍历");
        }
    }
}
