using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebAPI_练习.Model;

namespace 测试_WebAPI.Model
{
    internal class RespondResult
    {
       public string Code { get; set;}
      public  string Message { get; set;}
       public List< UserInfo> Data { get; set;}
    }
}
