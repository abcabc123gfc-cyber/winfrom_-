using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10_单例模式
{
    internal class Boss
    {
        public int HP = 100;
        //吧构造函数设置成私有的 不能在外部进行实例化,只能在内部进行实例化
        private Boss() { }

        static Boss Insetance = null;//实例
        static object locker = new object();//锁

        public static Boss GetBoss()
        {
            lock (locker)
            {

                if (Insetance == null)
                {
                    Insetance= new Boss();
                }
            }
            return Insetance;
        }

        public int Sub()
        {
            return HP-=10;
        }
    }
}
