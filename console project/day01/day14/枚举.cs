using System;

namespace day14
{
    internal class 枚举
    {
        //枚举类型定义在类中
        //格式: enum 枚举类型名称{枚举成员1,枚举成员2,枚举成员3}

        //枚举类型的名字使用大驼峰
        //枚举的成员可以使用索引去访问,默认是从0开始
        //可以在成员命名时定义索引 , 格式: 枚举成员名称 = 索引

        //通过 枚举类型名.枚举成员名 访问枚举成员
        //通过 枚举类型名.GetName(索引) 访问枚举成员
        enum Color
        {
            Red,
            Green,
            Blue
        }

        //扩展
        //给枚举增加一个特性
        [Flags]
        enum Week
        {
            Monday = 1,
            Tuesday = 2,
            Wednesday = 4,
            Thursday = 8,
            Friday = 16,
            Saturday = 32,
            Sunday = 64
        }
    }
}
