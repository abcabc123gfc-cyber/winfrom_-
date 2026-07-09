using System;
using System.Collections.Generic;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
namespace _01_core_WPF
{
    class HorizontalPanel :Panel
    {
        protected  override Size MeasureOverride(Size availableSize)
        {
         return base.MeasureOverride(availableSize);   
        }
        protected override Size ArrangeOverride(Size finalSize)
        {
            return base.ArrangeOverride(finalSize);
        }

    }
}
