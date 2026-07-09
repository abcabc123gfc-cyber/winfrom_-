using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day10
{
    internal class _Queue_Stack
    {
        //队列
        public void Queue()
        { 
            //队列(Queue) 类 表示一个先进先出的对象集合,当需要对项目进行 先进先出 访问时,可以使用队列.
            Queue<int> intqueue = new Queue<int>();
            Queue<List<int>> queue = new Queue<List<int>>();
            

            //添加元素
            intqueue.Enqueue(1);
            intqueue.Enqueue(2);
            intqueue.Enqueue(3);
            //删除
            //intqueue.Dequeue();

            //获取队列的元素,开始处元素
            Console.WriteLine( intqueue.Peek());

            //获取队列的元素个数
            Console.WriteLine(intqueue.Count);

            //清空
            //intqueue.Clear();

            //转换为对应类型的数组
            int[] ints = intqueue.ToArray();

            //添加元素
            queue.Enqueue(new List<int>() { 1, 2, 3 });
            

            Console.WriteLine("----------------");
            foreach(var item in intqueue)
            {
                Console.WriteLine(item);
            }
        }
        //堆栈
        public void StackDemo()
        {
            //堆栈(Stack) 类 表示一个后进先出的对象集合,当需要对项目进行 后进先出 访问时,可以使用堆栈.
            //向堆栈中添加项目时,称为"推送"项目(入栈),从堆栈中移除项目时,称为"弹出"项目(出栈)
            Stack<int> intstack = new Stack<int>();
            Stack<List<int>> stack  = new Stack<List<int>>();
        
            //添加元素
            intstack.Push(1);
            intstack.Push(3);
            intstack.Push(2);

            //删除元素,删除的是栈顶元素,按照顺序删除
            intstack.Pop();

            //获取栈顶元素
            Console.WriteLine(intstack.Peek());

            //清空堆栈
            intstack.Clear();
            //将堆栈转换成对应类型的数组
            int[] ints = intstack.ToArray();

            //判断堆栈是否为空
            
            //获取堆栈的元素个数
            Console.WriteLine(intstack.Count);
        }

        //
    }
}
