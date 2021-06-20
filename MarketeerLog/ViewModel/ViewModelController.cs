using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using MarketeerLog.ViewModel.EventArguments;
namespace MarketeerLog.ViewModel
{
    public class ViewModelController
    {
        private static ViewModelController _instance;

        public event EventHandler<ViewModelRegisteredEventArgs> ViewModelRegistered;

        public event EventHandler<ViewModelRegisteredEventArgs> ViewModelUnregistered;
        public static ViewModelController Instance
        {
            get => _instance;
        }

        private List<IViewModel> _viewModels = new List<IViewModel>();

        public T GetViewModel<T>() where T : IViewModel
        {
            foreach(IViewModel viewmodel in _viewModels)
            {
                if(viewmodel.GetType().IsEquivalentTo(typeof(T)))
                {
                    return (T)viewmodel;
                }
            }
            return default(T);
        }

        public void RegisterViewModel<T>(T viewModel) where T : IViewModel
        {
            _viewModels.Add(viewModel);

            ViewModelRegistered?.Invoke(this, new ViewModelRegisteredEventArgs(viewModel));

        }

        public void UnRegisterviewModel<T>(T viewModel) where T : IViewModel
        {
            _viewModels.Remove(viewModel); //TODO MAKE SURE THAT SHIT IN THERE LMAO

            ViewModelUnregistered?.Invoke(this, new ViewModelRegisteredEventArgs(viewModel));
        }

        public ViewModelController()
        {
            _instance = this;
        }

        
        
    }
}
