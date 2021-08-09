using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace MarketeerLog.ViewModel
{
    public interface IWindowService
    {
        void ShowDialog<T>(IViewModel viewmodel) where T : Window, new();

        void ShowWindow<T>(IViewModel viewmodel) where T : Window, new();
    }
}
