using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Partial类
{
    using System;
    using System.Collections.Generic;

    public partial class Blog
    {
       public string Report()
        {
            return  "Blog: "  + " " + BlogId+ " " + BlogTitle + " " + BlogContent;
        }
    }
}
