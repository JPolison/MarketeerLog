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

        private List<Window> _windows = new List<Window>();

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
            win.Owner = GetWindow<MainWindow>();
            win.DataContext = viewmodel;
            win.ShowDialog();
            
        }

        public void ShowWindow<T>(IViewModel viewModel) where T : Window, new()
        {
            Window win = new T();
            _windows.Add(win);
            win.DataContext = viewModel;
            win.Show();
        }

        public T GetWindow<T>() where T : Window
        {
            foreach (Window window in _windows)
            {
                if (window.GetType().IsEquivalentTo(typeof(T)))
                {
                    return (T)window;
                }
            }
            return default(T);
        }
    }
}
