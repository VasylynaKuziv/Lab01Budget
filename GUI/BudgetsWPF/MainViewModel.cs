using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Wallets;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgets.GUI.WPF
{
    public class MainViewModel : BindableBase
    {
        private List<IMainNavigatable> _viewModels = new List<IMainNavigatable>();

        public IMainNavigatable CurrentViewModel
        {
            get;
            private set;
        }
        public MainViewModel()
        {
            Navigate(MainNavigatableTypes.Authentication);
        }

        public void Navigate(MainNavigatableTypes type)
        {
            if (CurrentViewModel != null && CurrentViewModel.Type == type)
                return;
            IMainNavigatable viewModel = _viewModels.FirstOrDefault(authNavigatable => authNavigatable.Type == type);

            if (viewModel == null)
            {
                viewModel = CreateViewModel(type);
                _viewModels.Add(viewModel);
            }
            viewModel.ClearSensitiveData();
            CurrentViewModel = viewModel;
            RaisePropertyChanged(nameof(CurrentViewModel));
        }

        private IMainNavigatable CreateViewModel(MainNavigatableTypes type)
        {
            if (type == MainNavigatableTypes.Authentication)
            {
                return new AuthViewModel(() => Navigate(MainNavigatableTypes.Wallets));
            }
            else
            {
                return new WalletsViewModel();
            }
        }
    }
}
