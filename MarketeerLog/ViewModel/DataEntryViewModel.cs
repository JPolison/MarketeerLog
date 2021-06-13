using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketeerLog.ViewModel.EventArguments;
using MarketeerLog.Model;
using System.Windows.Input;

namespace MarketeerLog.ViewModel
{
    public class DataEntryViewModel : ViewModelBase
    {

       // private EventHandler<ListingAddedEventArgs> _itemAddedEvent;
        public event EventHandler<ListingAddedEventArgs> ItemAddedEvent;

        private ItemListing _item;

        public ItemListing Item
        {
            get => _item;
            set
            {
                _item = value;
                OnPropertyChanged();
            }
        }

        private readonly DelegateCommand _addCommand;

        public ICommand AddCommand => _addCommand;

        public DataEntryViewModel()
        {
            RegisterVM();
            _addCommand = new DelegateCommand(AddToList);
            
        }

        private void AddToList(object commandParam)
        {
            _item = new ItemListing("test2", DateTime.Now, DateTime.Now, 22, 33);
            ListingAddedEventArgs arg = new ListingAddedEventArgs();
            arg.item = _item;
            ItemAddedEvent?.Invoke(this, arg);
           
        }
    }
}
