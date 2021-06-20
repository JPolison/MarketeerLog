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
using MarketeerLog.View;
using MarketeerLog.ViewModel;
namespace MarketeerLog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            
            InitializeComponent();
            new ViewModelController();
            //MainViewModel VM = new MainViewModel();
            DataContext = new MainViewModel();

        }

        private void AddEntry_Click(object sender, RoutedEventArgs e)
        {
        }
        private void EditEntry_Click(object sender, RoutedEventArgs e)
        {
          
        }
    }
}
