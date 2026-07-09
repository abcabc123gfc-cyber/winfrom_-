using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace day11
{
    internal class 添加项目引用
    {
        public void ProjectDemo()
        {
            // 添加项目引用 注意: 两个项目之间不可以相互引用 会报错
            //例如: 项目A 有方法1, 项目B 有方法2
            //项目A 调用项目B的方法2 可以把项目B引用到项目A中

            //注意: 项目A引用项目B,项目B引用项目A 会报错

            //解决办法: 创建一个中间项目C,把项目A和项目B都引用到C中,然后把C引用到项目A中

           
        }
    }
}
