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
        bool LogOperrate(Logs log);
        bool LogTrade(Logs log);
        List<Logs> OutputLogOperate();
        List<Logs> OutputLogTrade();
     
    }
}
