using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFLight.Service
{
    public interface Iservice<T> where T : class 
    {
        List<T> GetAll();   
    }
}
