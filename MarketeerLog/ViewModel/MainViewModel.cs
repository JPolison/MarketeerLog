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
namespace MarketeerLog.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<ItemListing> _listedItems;
        
        
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
            _listedItems = new ObservableCollection<ItemListing>();
            ViewModelController.Instance.ViewModelRegistered += HandleRegister;
            //_listedItems.CollectionChanged += _listedItemCollectionChanged;
            ItemListing p = new ItemListing("Test", DateTime.Now, DateTime.Now, 10, 20);
            
          
            _listedItems.Add(p);     
        }

       

        public void _listedItemAdded(object sender, ListingAddedEventArgs args)
        {
            _listedItems.Add(args.item);
        }

        public void HandleRegister(object sender, ViewModelRegisteredEventArgs args)
        {
            if (args.viewmodel.GetType().IsEquivalentTo(typeof(DataEntryViewModel)))
            {
                DataEntryViewModel vm = (DataEntryViewModel)args.viewmodel;
                vm.ItemAddedEvent += _listedItemAdded;
            }
        }



        /*
        private void _listedItemCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if(e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach(INotifyPropertyChanged item in e.NewItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged += _itemListingPropertyChanged;

                }
            }
            if(e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (INotifyPropertyChanged item in e.OldItems.OfType<INotifyPropertyChanged>())
                {
                    item.PropertyChanged -= _itemListingPropertyChanged;

                }
            }
        }

        private void _itemListingPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

        } */

    }

 
}
