using System;


using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MarketeerLog.Model
{
    public class ItemListing : NotifiedModel
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        private DateTime _listDate;
        public DateTime ListDate
        {
            get => _listDate;
            set
            {
                _listDate = value;
                OnPropertyChanged("ListDate");
            }
        }

        private DateTime _sellDate;
        public DateTime SellDate
        {
            get => _sellDate;
            set 
            {
                _sellDate = value;
                OnPropertyChanged("SellDate");
            }
        }
        private float _purchasePrice;
        public float PurchasePrice 
        {
            get => _purchasePrice;
            set 
            {
                _purchasePrice = value;
                calculateProfit();
                OnPropertyChanged("PurchasePrice");
            }
        }

        private float _listedPrice;
        public float ListedPrice 
        {
            get => _listedPrice;
            set
            {
                _listedPrice = value;
                calculateProfit();
                OnPropertyChanged("ListedPrice");
            }
        }

        private float _profit;
        public float Profit
        {
            get => _profit;
            private set
            {
                _profit = value;
                OnPropertyChanged("Profit");
            }
        }

        private bool _sold;
        public bool Sold
        {
            get => _sold;
            set
            {
                _sold = value;
                OnPropertyChanged("Sold");

            }
        }

        public ItemListing(string Name, DateTime ListDate, DateTime SellDate, float PurchasePrice, float ListedPrice)
        {
            _name = Name;
            _listDate = ListDate;
            _sellDate = SellDate;
            _purchasePrice = PurchasePrice;
            _listedPrice = ListedPrice;
            calculateProfit();
        }
        public ItemListing() 
        {
            _listedPrice = 0;
            _purchasePrice = 0;
        }

        private void calculateProfit()
        {
   
            Profit = ListedPrice - PurchasePrice;
        }
    }
}
