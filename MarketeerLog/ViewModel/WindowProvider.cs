using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketeerLog.ViewModel
{
    public class WindowProvider : IWindowService
    {
        private static WindowProvider _instance;
        public static WindowProvider Instance
        {
            get => _instance;
            set => _instance = value;
        }

        public WindowProvider()
        {
            _instance = this;
        }

        public void ShowDialog<T>(IViewModel viewmodel) where T : Window, new()
        {
            Window win = new T();
            win.DataContext = viewmodel;
            win.ShowDialog();
            
        }
    }
}
