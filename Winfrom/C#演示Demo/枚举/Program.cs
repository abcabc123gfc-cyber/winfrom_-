using System;

namespace 枚举
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
            person.Level = Level.User;
            person.Skill = Skill.C | Skill.Java;
            Console.WriteLine(person.Skill);
            //判断具体的技能
            Console.WriteLine((person.Skill & Skill.Java) > 0);
            //比特位用法 场景很多 
            //如: 读写文件: 控制文件的打开方式
        }

    }
    class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        //对于权限定义, 使用枚举能保证权限的合法值
        public Level Level { get; set; }
        public Skill Skill { get; set; }
    }
    enum Level
    {
        //默认从 0 开始 
        //可以自定义从 值 : 如 1
        Admin = 1,
        User,
        Guest
    }

    //技能可以多选 指定  1  2 4 8  二进制可以错开
    // 01    10   100   1000
    //按位取 或 就可以让这些技能叠加在一起 如:  1 | 2 | 4 | 8
    enum Skill
    {
        C = 1,
        Java = 2,
        Python = 4,
        PHP = 8,
    }
}
