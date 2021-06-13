using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketeerLog.Model;
namespace MarketeerLog.ViewModel.EventArguments
{
    public class ListingAddedEventArgs : EventArgs
    {
        public ItemListing item;
    }
}
