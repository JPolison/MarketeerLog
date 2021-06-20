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
    public enum EntryMode { Edit, Add };

    public class DataEntryViewModel : ViewModelBase
    {

      
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

        private EntryMode _mode;

        public EntryMode Mode
        {
            get => _mode;
            set => _mode = value;
        }

        private string _buttonName;

        public string ButtonName
        {
            get => _buttonName;
            set
            {
                _buttonName = value;
                OnPropertyChanged("ButtonName");
            }
        }
   

        private readonly DelegateCommand _addCommand;

        public ICommand AddCommand => _addCommand;

        public DataEntryViewModel(EntryMode mode)
        {
            _item = new ItemListing();
            RegisterVM();
            SwitchMode(mode);
            _addCommand = new DelegateCommand(AddToList, CanAddToList);
         
            
        }
        
        public void SwitchMode(EntryMode mode)
        {
            Mode = mode;
            switch(Mode)
            {
                case EntryMode.Add:
                    ButtonName = "Add";
                    break;
                case EntryMode.Edit:
                    ButtonName = "Close";
                    break;
            }
        }

        public void SetItem(ItemListing item)
        {
            if(Mode == EntryMode.Edit)
            {
                _item = item;
            }
        }

        
        private void AddToList(object commandParam)
        {
            if(Mode == EntryMode.Add)
            {
                ListingAddedEventArgs arg = new ListingAddedEventArgs();
                arg.item = _item;
                ItemAddedEvent?.Invoke(this, arg);
            }
            UnregisterVM();
        }

    

        private bool CanAddToList(object commandParam)
        {
            return true;
        }
    }
}
