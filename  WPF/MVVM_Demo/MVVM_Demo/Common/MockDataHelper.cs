using System.Collections.Generic;
using 手写MVVM.Models;

namespace MVVM_Demo.Common
{
    internal class MockDataHelper
    {
        //模拟数据
        public static List<Student> InitData { get; set; } = new List<Student>(){

            new Student(){Id=1,Name="吴亦凡",Age=18},
            new Student(){Id=2,Name="罗志祥",Age=19},
            new Student(){Id=3,Name="李云迪",Age=20},
            new Student(){Id=3,Name="蔡徐坤",Age=20},
        };
    }
}
