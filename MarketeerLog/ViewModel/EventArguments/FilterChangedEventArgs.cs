using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketeerLog.Model;
namespace MarketeerLog.ViewModel.EventArguments
{
    public class FilterChangedEventArgs : EventArgs
    {
        public FilterSettings filtersettings;

        public FilterChangedEventArgs(FilterSettings filtersettings)
        {
            this.filtersettings = filtersettings;
        }
    }
}
