using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MarketeerLog.Model;
using System.ComponentModel;
using System.Windows.Input;

using MarketeerLog.ViewModel.EventArguments;
using System.Diagnostics;

namespace MarketeerLog.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ItemListing> _listedItems;

        private readonly DelegateCommand _editEntryCommand;

        public ICommand EditEntryCommand => _editEntryCommand;

        private readonly DelegateCommand _addEntryCommand;

        public ICommand AddEntryCommand => _addEntryCommand;

        private ItemListing _selectedListing;

        public ItemListing SelectedListing
        {
            get => _selectedListing;
            set
            {
                _selectedListing = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        
        public ObservableCollection<ItemListing> ListedItems
        {
            get => _listedItems;
            set
            {
                _listedItems = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            RegisterVM();
            new WindowProvider();
            _listedItems = new ObservableCollection<ItemListing>();
            _selectedListing = new ItemListing();
            _editEntryCommand = new DelegateCommand(_editList, _canEditList);
            _addEntryCommand = new DelegateCommand(_addList, _canAddList);
            ViewModelController.Instance.ViewModelRegistered += HandleRegister;
            ViewModelController.Instance.ViewModelUnregistered += HandleUnregister;
            ItemListing p = new ItemListing("Test", DateTime.Now, DateTime.Now, 10, 20);
            
          
            _listedItems.Add(p);     
        }

       
        private void _editList(object command)
        {
            DataEntryViewModel vm = new DataEntryViewModel(EntryMode.Edit);
            vm.SetItem(_selectedListing);
            WindowProvider.Instance.ShowDialog<View.DataEntryWindow>(vm);
        }

        private bool _canEditList(object command)
        {
            if(SelectedListing != null)
            {
                return true;
            }
            return false;
        
        }

        private void _addList(object command)
        {
            WindowProvider.Instance.ShowDialog<View.DataEntryWindow>(new DataEntryViewModel(EntryMode.Add));
        }

        private bool _canAddList(object command)
        {
            return true;
        }


        private void _listedItemAdded(object sender, ListingAddedEventArgs args)
        {
            _listedItems.Add(args.item);
        }

        private void HandleRegister(object sender, ViewModelRegisteredEventArgs args)
        {
            if (args.viewmodel.GetType().IsEquivalentTo(typeof(DataEntryViewModel)))
            {
                DataEntryViewModel vm = (DataEntryViewModel)args.viewmodel;
                vm.ItemAddedEvent += _listedItemAdded;
              
            }
        }

        private void HandleUnregister(object send, ViewModelRegisteredEventArgs args)
        {
            if(args.viewmodel.GetType().IsEquivalentTo(typeof(DataEntryViewModel)))
            {
                DataEntryViewModel vm = (DataEntryViewModel)args.viewmodel;
                vm.ItemAddedEvent -= _listedItemAdded;
                Debug.WriteLine("UNREGISTERED EVENT");
            }
        }
    }
}
