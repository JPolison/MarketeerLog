using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog.ViewModel.EventArguments
{
    public class ViewModelRegisteredEventArgs : EventArgs
    {
        public ViewModelRegisteredEventArgs(IViewModel viewmodel)
        {
            this.viewmodel = viewmodel;
        }
        public IViewModel viewmodel;
    }
}
