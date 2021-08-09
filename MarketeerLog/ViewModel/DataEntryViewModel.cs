using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MarketeerLog.ViewModel.EventArguments;
using MarketeerLog.Model;
using System.Windows.Input;
using System.Windows;

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

        private Visibility _closeVisibility;

        public Visibility CloseVisibility
        {
            get => _closeVisibility;
            set
            {
                _closeVisibility = value;
                OnPropertyChanged("CloseVisibility");
            }
        }


        private readonly DelegateCommand _addCommand;

        public ICommand AddCommand => _addCommand;

        private readonly DelegateCommand _closeCommand;

        public ICommand CloseCommand => _closeCommand;



        public DataEntryViewModel(EntryMode mode)
        {
            _item = new ItemListing();
            RegisterVM();
            SwitchMode(mode);
            _addCommand = new DelegateCommand(_addToList, _canAddToList);
            _closeCommand = new DelegateCommand(_closeWindow, _canCloseWindow);
         
            
        }
        
        public void SwitchMode(EntryMode mode)
        {
            Mode = mode;
            switch(Mode)
            {
                case EntryMode.Add:
                    ButtonName = "Add";
                    CloseVisibility = Visibility.Visible;
                    break;
                case EntryMode.Edit:
                    ButtonName = "Edit";
                    CloseVisibility = Visibility.Hidden;
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

        
        private void _addToList(object commandParam)
        {
            if(Mode == EntryMode.Add)
            {
                ListingAddedEventArgs arg = new ListingAddedEventArgs();
                arg.item = _item;
                if(arg.item.Sold)
                {
                    arg.item.SellDate = null;
                }
                ItemAddedEvent?.Invoke(this, arg);
            }
            UnregisterVM();
        }

    

        private bool _canAddToList(object commandParam)
        {
            return true;
        }

        private void _closeWindow(object commandParam)
        {
            UnregisterVM();
        }

        private bool _canCloseWindow(object commandParam)
        {
            return true;
        }
    }
}
