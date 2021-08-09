using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog.Model
{
    public enum Filtermode { Name, ListedPrice, SellPrice, ListedDate, SellDate, Profit}; 
    public class FilterSettings : NotifiedModel
    {
        private Filtermode _mode;

        public Filtermode Mode
        {
            get => _mode;
            set
            {
                if (_mode == value) return;

                _mode = value;
                OnPropertyChanged("IsName");
                OnPropertyChanged("IsListedDate");
                OnPropertyChanged("IsListedPrice");
                OnPropertyChanged("IsSellPrice");
                OnPropertyChanged("IsProfit");
                OnPropertyChanged("IsSellDate");

            }
        }

        public bool IsName
        {
            get 
            { 
                return Mode == Filtermode.Name; 
            }
            set  
            { 
                Mode = value ? Filtermode.Name : Mode; 
            }
        }
        public bool IsListedPrice
        {
            get
            {
                return Mode == Filtermode.ListedPrice;
            }
            set
            {
                Mode = value ? Filtermode.ListedPrice : Mode;
            }
        }
        public bool IsSellPrice
        {
            get
            {
                return Mode == Filtermode.SellPrice;
            }
            set
            {
                Mode = value ? Filtermode.SellPrice : Mode;
            }
        }
        public bool IsListedDate
        {
            get
            {
                return Mode == Filtermode.ListedDate;
            }
            set
            {
                Mode = value ? Filtermode.ListedDate : Mode;
            }
        }
        public bool IsSellDate
        {
            get
            {
                return Mode == Filtermode.SellDate;
            }
            set
            {
                Mode = value ? Filtermode.SellDate : Mode;
            }
        }

        public bool IsProfit
        {
            get
            {
                return Mode == Filtermode.Profit;
            }
            set
            {
                Mode = value ? Filtermode.Profit : Mode;
            }
        }

        public FilterSettings()
        {
            Mode = Filtermode.Name;
        }

    }
}
