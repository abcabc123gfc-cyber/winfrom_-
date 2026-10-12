using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism_Dialog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prism_Dialog.ViewModels
{
    internal class Window1ViewModel:BindableBase
    {
        private readonly IEventAggregator _eventAggregator;
        public Window1ViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;
        }

        public DelegateCommand PubMessCommand
        {
            get => new(() => _eventAggregator.GetEvent<StringMessage>().Publish("吴亦凡"));
        }
    }
}
