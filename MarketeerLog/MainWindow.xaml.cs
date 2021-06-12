using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MarketeerLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public class Listing
    {
        public string name { get; set; }
        public float purchasePrice { get; set; }
        public float listingPrice { get; set; }
        public float income { get; set; }
      
    }
    public partial class MainWindow : Window
    {
        public ObservableCollection<Listing> listings = new ObservableCollection<Listing>();
        public MainWindow()
        {
            InitializeComponent();

            ListingDataGrid.ItemsSource = listings;
            LoadListings(30);
          
            

            
        }

        public void testStack(int n)
        {

        }

        private void LoadListings(int n)
        {
          

            for(int i = 0; i < n; i++)
            {
                Listing l = new Listing()
                {
                    name = "id" + i,
                    purchasePrice = n,
                    listingPrice = n + 1,
                };
                l.income = l.listingPrice - l.purchasePrice;


                listings.Add(l);
            }

           
            
        }

        private void ListingDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Edit(object sender, RoutedEventArgs e)
        {

        }
    }
}
