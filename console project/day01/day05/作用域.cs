using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day05
{
    internal class 作用域
    {
        public void TestScope()
        {
            //权限修饰符
            {
                //访问修饰符: public private protected internal
                //public: 公有方法,可以在任何地方调用
                // private: 私有方法, 只能在本类中调用
                // protected: 保护方法, 只能在本类和子类中调用
                // internal: 内部方法, 只能在同一个程序集中调用
                // protected internal :可以在同一程序集内的任何地方访问，或者在任何地方的子类中访问
                // private protected: 私有保护方法, 只能在本类和子类中访问

            }
        }
    }
}
