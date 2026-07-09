using System;
using System.Collections.Generic;

namespace day15
{
    internal class 索引器
    {
        public void Test()
        {
            ClassRoom classRoom = new ClassRoom();
            classRoom.MyAdd(new Student() { Name = "小名", Sex = '男' });

            //需求通过索引获取班级中学生的姓名
            //需要使用"索引器"
            //格式: public 返回值类型 rhis[索引器的数据类型] {}
            //索引器: 一种可以让我们使用索引访问对象的一种方式

            Console.Clear();

            Student student = new Student(new string[] { "小蘑菇", "小明", "小张" });
            Console.WriteLine(student[1]);
        }
    }
    class Student
    {
        public string[] names;
        public string Name { get; set; }

        public char Sex { get; set; }

        public Student()
        {
            names = new string[10];
        }
        public Student(string[] strs)
        {
            names = strs;
        }

        public string this[int index]
        {
            get =>names[index];

            set
            {
                if (index >= names.Length)
                {
                    string[] NewNames = new string[names.Length + index];
                    NewNames[index] = value;

                    names.CopyTo(NewNames, 0);
                    names = NewNames; 
                }
                names[index] = value;
            }
        }
    }

    class ClassRoom
    {

        private List<Student> students = new List<Student>();

        public Student this[int index]
        {
            get
            {
                return students[index];
            }
            set
            {
                students[index] = value;
            }

        }

        public Student this[string index]
        {
            get
            {
                foreach (Student s in students)
                {
                    if (s.Name == index)
                    {
                        return s;
                    }
                }
                return null;
            }
        }

        public string ClasssID { get; set; }

        public void MyAdd(Student s)
        {
            students.Add(s);
        }
    }
}
