using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog.Model.Content
{
    [Serializable]
    public class ContentState
    {
        public ObservableCollection<ItemListing> ListedItems { get; set; }

        public ContentState(ObservableCollection<ItemListing> ListedItems)
        {
            this.ListedItems = ListedItems;
        }

        public ContentState() 
        {
            ListedItems = new ObservableCollection<ItemListing>();
        }

    }
}
