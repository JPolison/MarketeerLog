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
using System.Windows.Forms;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using MarketeerLog.Model.Content;
using System.Windows.Data;
using MarketeerLog.View;
using System.Globalization;
using System.Windows;

namespace MarketeerLog.ViewModel
{


    //IM NOT DOCUMENTING ANY OF THIS THE CODE SPEAKS FOR ITSELF.
    //PS IT MAKES SENSE IN MY MIND
    [Serializable]

    public class MainViewModel : ViewModelBase
    {
        #region commands

        private readonly DelegateCommand _editEntryCommand;


        public ICommand EditEntryCommand => _editEntryCommand;

        private readonly DelegateCommand _addEntryCommand;

        public ICommand AddEntryCommand => _addEntryCommand;

        private ItemListing _selectedListing;


        private readonly DelegateCommand _removeEntryCommand;

        public ICommand RemoveEntryCommand => _removeEntryCommand;

        private readonly DelegateCommand _saveContent;

        public ICommand SaveContent => _saveContent;

        private readonly DelegateCommand _openContent;

        public ICommand OpenContent => _openContent;

        private readonly DelegateCommand _openFilterSettings;

        public ICommand OpenFilterSettings => _openFilterSettings;

        private readonly DelegateCommand _compareFilter;

        public ICommand CompareFilter => _compareFilter;
        #endregion


        #region ModelBindings
        public ItemListing SelectedListing
        {
            get => _selectedListing;
            set
            {
                
                _selectedListing = value;
                _editEntryCommand.InvokeCanExecuteChanged();
                OnPropertyChanged("SelectedListing");
                

            }
        }

        private ObservableCollection<ItemListing> _listedItems;

        public ObservableCollection<ItemListing> ListedItems
        {
            get => _listedItems;
            set
            {
                _listedItems = value;
                OnPropertyChanged("ListedItems");
                ListView.Refresh();

            }
        }
     
        private CollectionViewSource viewSource { get; } = new CollectionViewSource();

        public ICollectionView ListView => viewSource.View;

        #endregion ModelBindings

        #region UIVariables

        private bool _isStringInput = true;

        public bool IsStringInput
        {
            get => _isStringInput;
            set
            {
                _isStringInput = value;
                switch(value)
                {
                    case true:
                        StringInputVisibility = Visibility.Visible;
                        break;
                    case false:
                        StringInputVisibility = Visibility.Hidden;
                        break;
                }
                OnPropertyChanged("IsStringInput");
            }
        }

        
        private Visibility _stringInputVisibility = Visibility.Visible;

        public Visibility StringInputVisibility
        {
            get => _stringInputVisibility;
            set
            {
                _stringInputVisibility = value;
                OnPropertyChanged("StringInputVisibility");
            }
        }


        private bool _isComparableValue = false;
        private bool IsComparableValue
        {
            get => _isComparableValue;
            set
            {
                _isComparableValue = value;
                switch (value)
                {
                    case true:
                        CompareFilterVisibility = Visibility.Visible;
                        break;
                    case false:
                        CompareFilterVisibility = Visibility.Hidden;
                        break;
                }
            }
        }

        private Visibility _compareFilterVisibility = Visibility.Hidden;

        public Visibility CompareFilterVisibility
        {
            get => _compareFilterVisibility;
            set
            {
                _compareFilterVisibility = value;
                OnPropertyChanged("CompareFilterVisibility");
            }
        }

        private string _filterName = "Filtering by Name";

        public string FilterName
        {
            get => _filterName;
            set
            {
                _filterName = value;
                OnPropertyChanged("FilterName");
            }
        }

        private bool _isDateTimeInput;

        public bool IsDateTimeInput 
        {
            get => _isDateTimeInput;
            set
            {
                _isDateTimeInput = value;
                switch (value)
                {
                    case true:
                        DatetimeInputVisibility = Visibility.Visible;
                        break;
                    case false:
                        DatetimeInputVisibility = Visibility.Hidden;
                        break;
                }
                OnPropertyChanged("IsDateTimeInput");
            }
        }

        private Visibility _datetimeInputVisibility = Visibility.Visible;

        public Visibility DatetimeInputVisibility
        {
            get => _datetimeInputVisibility;
            set
            {
                _datetimeInputVisibility = value;
                OnPropertyChanged("DatetimeInputVisibility");
            }
        }


        private DateTime _filterDatetime;

        public DateTime FilterDateTime
        {
            get => _filterDatetime;
            set
            {
                _filterDatetime = value;
                ListView.Refresh();
                OnPropertyChanged("FilterDateTime");
            }
        }

        private string _filterString;

        public string FilterString
        {
            get => _filterString;
            set
            {
                _filterString = value;
                ListView.Refresh();
                OnPropertyChanged("FilterString");
            }
        }



        public enum ComparatorMode { GreaterThanEqual = 1, GreaterThan = 2, Equal = 3, LessThanEqual = 4, LessThan = 5};

        private ComparatorMode _comparefilterMode = ComparatorMode.GreaterThanEqual;

        public ComparatorMode CompareFilterMode
        {
            get => _comparefilterMode;
            set
            {
                _comparefilterMode = value;
                switch(value)
                {
                    case ComparatorMode.GreaterThanEqual:
                        if (IsStringInput)
                        {
                            CompareFilterContent = "Greater Than or Equal";
                        }
                        else
                        {
                            CompareFilterContent = "On or After";
                        }
                        break;
                    case ComparatorMode.GreaterThan:
                        if (IsStringInput)
                        {
                            CompareFilterContent = "Greater Than";
                        }
                        else
                        {
                            CompareFilterContent = "After";
                        }
                        break;
                    case ComparatorMode.Equal:
                        if (IsStringInput)
                        {
                            CompareFilterContent = "Equals";
                        }
                        else
                        {
                            CompareFilterContent = "On";
                        }
                        break;
                    case ComparatorMode.LessThanEqual:
                        if (IsStringInput)
                        {
                            CompareFilterContent = "Less Than or Equal";
                        }
                        else
                        {
                            CompareFilterContent = "On or Before";
                        }
                        break;
                    case ComparatorMode.LessThan:
                        if (IsStringInput)
                        {
                            CompareFilterContent = "Less Than";
                        }
                        else
                        {
                            CompareFilterContent = "Before";
                        }
                        break;
                }
            }
        }

        
        //Text of the Comparator Button
        private string _compareFilterContent = "Greater Than or Equal";
        public string CompareFilterContent
        {
            get => _compareFilterContent;
            set
            {
                _compareFilterContent = value;
                OnPropertyChanged("CompareFilterContent");
            }
        }
        #endregion

        public FilterSettings filterSettings = new FilterSettings();

        public MainViewModel()
        {
            RegisterVM();
            IsDateTimeInput = false;
            IsStringInput = true;
     
            _listedItems = new ObservableCollection<ItemListing>();
            viewSource.Source = ListedItems;
            viewSource.Filter += FilterList;
            _editEntryCommand = new DelegateCommand(_editList, _canEditList);
            _addEntryCommand = new DelegateCommand(_addList, _canAddList);
            _removeEntryCommand = new DelegateCommand(_removeFromList, _canRemoveFromList);
            _saveContent = new DelegateCommand(_saveToFile, _canSaveToFile);
            _openContent = new DelegateCommand(_openFile, _canOpenFile);
            _openFilterSettings = new DelegateCommand(_openFilter, _canOpenFilter);
            _compareFilter = new DelegateCommand(_switchComparator, _canSwitchComparator);
            ViewModelController.Instance.ViewModelRegistered += HandleRegister;
            ViewModelController.Instance.ViewModelUnregistered += HandleUnregister;

            FilterDateTime = DateTime.Today;

        }

        #region CommandHandlers
        private void _editList(object command)
        {
            DataEntryViewModel vm = new DataEntryViewModel(EntryMode.Edit);
            vm.SetItem(_selectedListing);
            WindowProvider.Instance.ShowDialog<View.DataEntryWindow>(vm);
        }

        private bool _canEditList(object command)
        {
            if(_selectedListing != null)
            {
                return true;

            }
            return false;
        
        }

        private void _addList(object command)
        {
            WindowProvider.Instance.ShowDialog<View.DataEntryWindow>(new DataEntryViewModel(EntryMode.Add));
            SelectedListing = null;
            
            
        }

        private bool _canAddList(object command)
        {
            return true;
        }

        private void _removeFromList(object command)
        {
            ListedItems.Remove(SelectedListing);
        }

        private bool _canRemoveFromList(object command)
        {
            return true;
        }

        private void _saveToFile(object command)  
        {
            ContentManager.Save(new ContentState(ListedItems));
        }
        private bool _canSaveToFile(object command)
        {
            return true;
        }
        private void _openFile(object command)
        {
            ContentState state = new ContentState();
            if(ContentManager.Open(ref state))
            {
                ListedItems.Clear();
                foreach(ItemListing x in state.ListedItems)
                {
                    ListedItems.Add(x);
                }
            }
        }
        private bool _canOpenFile(object command)
        {
            return true;
        }

        private void _openFilter(object command)
        {
            WindowProvider.Instance.ShowDialog<FilterWindow>(new FilterViewModel());
        }

        private bool _canOpenFilter(object command)
        {
            return true;
        }

        private void _listedItemAdded(object sender, ListingAddedEventArgs args)
        {
            _listedItems.Add(args.item);
            ListView.Refresh();

        }

        private void _OnFilterChanged(object sender, FilterChangedEventArgs args)
        {
            filterSettings = args.filtersettings;
            switch(filterSettings.Mode)
            {
                case Filtermode.ListedDate:
                    ChangeVisibility(false, true, true);
                    FilterName = "Filtering by Listed Date";
                    break;
                case Filtermode.SellDate:
                    ChangeVisibility(false, true, true);
                    FilterName = "Filtering by Sell Date";
                    break;
                case Filtermode.Name:
                    ChangeVisibility(true, false, false);
                    FilterName = "Filtering by Name";
                    break;
                case Filtermode.Profit:
                    ChangeVisibility(true, false, true);
                    FilterName = "Filtering by Profit";
                    break;
                case Filtermode.ListedPrice:
                    ChangeVisibility(true, false, true);
                    FilterName = "Filtering by Listed Price";
                    break;
                case Filtermode.SellPrice:
                    ChangeVisibility(true, false, true);
                    FilterName = "Filtering by Purchase Price";
                    break;

            }
            CompareFilterMode = CompareFilterMode;
            ListView.Refresh();
            Debug.Write("FILTER MODE" + filterSettings.Mode.ToString());
        }

        private void _switchComparator(object commandparam)
        {
            if((int)CompareFilterMode < 5)
            {
               
                CompareFilterMode = (ComparatorMode)(int)CompareFilterMode + 1;
            }
            else
            {
                CompareFilterMode = (ComparatorMode)1;
            }
            ListView.Refresh();
            Debug.WriteLine("ComparatorMode" + CompareFilterMode.ToString() + " : " + ((int)CompareFilterMode));
        }

        private bool _canSwitchComparator(object commmandparam)
        {
            return true;
        }
        #endregion CommandHandlers

        #region ViewModelHandling
        private void HandleRegister(object sender, ViewModelRegisteredEventArgs args)
        {
          
            if (args.viewmodel.GetType().IsEquivalentTo(typeof(DataEntryViewModel)))
            {
                DataEntryViewModel vm = (DataEntryViewModel)args.viewmodel;
                vm.ItemAddedEvent += _listedItemAdded;
              
            }
            if (args.viewmodel.GetType().IsEquivalentTo(typeof(FilterViewModel)))
            {
                FilterViewModel vm = (FilterViewModel)args.viewmodel;
                vm.FilterChanged += _OnFilterChanged;
            }
        }

        private void HandleUnregister(object send, ViewModelRegisteredEventArgs args)
        {
            if(args.viewmodel.GetType().IsEquivalentTo(typeof(DataEntryViewModel)))
            {
                DataEntryViewModel vm = (DataEntryViewModel)args.viewmodel;
                vm.ItemAddedEvent -= _listedItemAdded;
                Debug.WriteLine("UNREGISTERED DATAENTRY VIEWMODEL");
            }
            if (args.viewmodel.GetType().IsEquivalentTo(typeof(FilterViewModel)))
            {
                FilterViewModel vm = (FilterViewModel)args.viewmodel;
                vm.FilterChanged -= _OnFilterChanged;
                Debug.WriteLine("UNREGISTERED FILTER VIEWMODEL");
            }
        }
        #endregion
        private void ChangeVisibility(bool IsStringInput, bool IsDateTimeInput, bool IsComparableValue)
        {
            this.IsStringInput = IsStringInput;
            this.IsDateTimeInput = IsDateTimeInput;
            this.IsComparableValue = IsComparableValue;
        }

        private bool Operation(float a, float b)
        {
            switch(CompareFilterMode)
            {
                case ComparatorMode.GreaterThanEqual:
                    return a >= b;
                case ComparatorMode.GreaterThan:
                    return a > b;
                case ComparatorMode.Equal:
                    return a == b;
                case ComparatorMode.LessThanEqual:
                    return a <= b;
                case ComparatorMode.LessThan:
                    return a < b;
         

            }
            return false;
        }
        
        private bool Operation(DateTime? a, DateTime? b)
        {
            switch(CompareFilterMode)
            {
                case ComparatorMode.GreaterThanEqual:
                    return a >= b;
                case ComparatorMode.GreaterThan:
                    return a > b;
                case ComparatorMode.Equal:
                    return a == b;
                case ComparatorMode.LessThanEqual:
                    return a <= b;
                case ComparatorMode.LessThan:
                    return a < b;
         

            }
            return false;
        }
        private void FilterList(object sender, FilterEventArgs e)
        {
            ItemListing i = e.Item as ItemListing;
            if(i != null)
            {
                switch (filterSettings.Mode)
                {
                    case Filtermode.Name:
                        if (string.IsNullOrEmpty(FilterString) || i.Name.StartsWith(FilterString, true, new CultureInfo("en-us")))
                        {
                            e.Accepted = true;
                        }
                        else
                        {
                            e.Accepted = false;
                        }
                        break;
                    case Filtermode.ListedDate:
                        try
                        {
                            e.Accepted = Operation(i.ListDate.Date, FilterDateTime.Date);                   
                        } catch (Exception ex)
                        {
                            e.Accepted = false;
                            Debug.Write("Exception!" + ex.ToString());

                        }
                        break;
                    case Filtermode.SellDate:
                        try
                        {
                            e.Accepted =  Operation(i.SellDate?.Date, FilterDateTime.Date);
                        } catch (Exception ex)
                        {
                            e.Accepted = false;
                            Debug.Write("Exception!" + ex.ToString());
                        }
                        break;
                    case Filtermode.ListedPrice:
                        try
                        {
                            e.Accepted = string.IsNullOrEmpty(FilterString) ? true : Operation(i.ListedPrice, float.Parse(FilterString));
                        }
                        catch (Exception ex)
                        {
                            e.Accepted = false;
                            Debug.Write("Exception!" + ex.ToString());
                        }
                        break;
                    case Filtermode.Profit:
                        try
                        {
                            e.Accepted = string.IsNullOrEmpty(FilterString) ? true : Operation(i.Profit, float.Parse(FilterString));
                        }
                        catch (Exception ex)
                        {
                            e.Accepted = false;
                            Debug.Write("Exception!" + ex.ToString());
                        }
                        break;
                    case Filtermode.SellPrice:
                        try
                        {
                            e.Accepted = string.IsNullOrEmpty(FilterString) ? true : Operation(i.PurchasePrice, float.Parse(FilterString));
                        }
                        catch (Exception ex)
                        {
                            e.Accepted = false;
                            Debug.Write("Exception!" + ex.ToString());
                        }
                        break;
                }         
            }
        }
    }
}
