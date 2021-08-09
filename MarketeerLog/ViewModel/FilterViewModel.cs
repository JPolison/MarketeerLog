using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MarketeerLog.Model;
using MarketeerLog.ViewModel.EventArguments;
namespace MarketeerLog.ViewModel
{
    public class FilterViewModel : ViewModelBase
    {
        public event EventHandler<FilterChangedEventArgs> FilterChanged;

        private readonly DelegateCommand _filterCommand;

        public ICommand FilterCommand => _filterCommand;

        private FilterSettings _settings = new FilterSettings();

        public FilterSettings Settings
        {
            get => _settings;
            set
            {
                _settings = value;
                OnPropertyChanged("Settings");
            }
        }
        
        public FilterViewModel()
        {
            RegisterVM();
            _filterCommand = new DelegateCommand(_applyFilter, _canFilter);
        }

        private void _applyFilter(object parameter)
        {
            FilterChanged?.Invoke(this, new FilterChangedEventArgs(Settings));
            UnregisterVM();
        }
        private bool _canFilter(object parmater)
        {
            return true;
        }
    }
}
