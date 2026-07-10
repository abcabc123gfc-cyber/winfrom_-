using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IDAL
{
    public  interface ILog
    {
        void LogAll();
        bool LogOperrate(Log log);
        bool LogTrade(Log log);
        List<Log> OutputLogOperate();
        List<Log> OutputLogTrade();
     
    }
}
