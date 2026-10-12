using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Dialog.Models
{
    public class User : BindableBase
    {
        private string account;

        public string Account
        {
            get { return account; }
            set
            {
                SetProperty(ref account, value);
            }
        }


        private string password;

        public string Password
        {
            get { return password; }
            set
            {
                SetProperty(ref password, value);
            }
        }
    }
}
