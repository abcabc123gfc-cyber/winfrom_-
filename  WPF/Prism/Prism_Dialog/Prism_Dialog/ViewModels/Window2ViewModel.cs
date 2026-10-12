using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism_Dialog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Prism_Dialog.ViewModels
{
    internal class Window2ViewModel:BindableBase
    {

        private readonly IEventAggregator eventAggregator;

        public Window2ViewModel(IEventAggregator eventAggregator)
        {
            this.eventAggregator = eventAggregator;

        }

        private string msg;

        public string Msg
        {
            get { return msg; }
            set { SetProperty(ref msg, value); }
        }
        //订阅
        public DelegateCommand ReciCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                    eventAggregator.GetEvent<StringMessage>().Subscribe(mes =>
                    {
                        Msg = mes;
                    });
                });       
            }
        }
        //取消订阅
        public DelegateCommand CancelCommand
        {
            get
            {
                return new DelegateCommand(() =>
                {
                   
                    eventAggregator.GetEvent<StringMessage>().Unsubscribe(mes => Msg = mes);        
                });
            }
        }
    }
}
