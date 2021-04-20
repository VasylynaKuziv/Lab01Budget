using Prism.Mvvm;
using Lab01.Entities;
using System;
using Prism.Commands;
using Lab01Budget.Entities.Storage;
using Budgets.GUI.WPF.Service;
using Budgets.GUI.WPF.Authentication;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

        public string Incomes
        {
            get
            {if (_wallet.GetLastMonthIncome() == 0)
                    return 0.ToString();
                return _wallet.GetLastMonthIncome().ToString("#.##");
            }
        }
        public string Expenses
        {
            get
            {
                if (_wallet.GetLastMonthExpenses() == 0)
                    return 0.ToString();
                return _wallet.GetLastMonthExpenses().ToString("#.##");
            }
        }

        public string CurrentBalance
        {
            get
            {

                RaisePropertyChanged(nameof(DisplayName));
                UpdateWalletCommand.RaiseCanExecuteChanged();
                _wallet.CurrentBalance = _wallet.GetCurrentBalance();
                return _wallet.CurrentBalance.ToString("#.##");
            }

        }

        public string Name
        {
            get
            {
                return _wallet.Name;
            }
            set
            {
                _wallet.Name = value;
                RaisePropertyChanged(nameof(DisplayName));
                UpdateWalletCommand.RaiseCanExecuteChanged();
            }
        }

      
        public DelegateCommand UpdateWalletCommand { get; }

        public string Description
        {
            get
            {
                return _wallet.Description;
            }
            set
            {
                _wallet.Description = value;
                RaisePropertyChanged(nameof(DisplayName));
                UpdateWalletCommand.RaiseCanExecuteChanged();
            }
        }

        public string Currency
        {
            get
            {
                return _wallet.Currency;
            }
        }

       
        
        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name}";
            }
        }

        public Wallet Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public Guid Guid
        {
            get
            {
                return _wallet.Guid;
            }

        }

        public WalletDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
            UpdateWalletCommand = new DelegateCommand(UpdateWallet, checkInput);
        }

        private bool checkInput()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Description);
        }


        private async void UpdateWallet()
        {
            var service = new WalletService();
            try
            {
               DBWallet wallet = new DBWallet(_wallet.Guid, _wallet.Name, _wallet.InitBalance, _wallet.Description,
                    _wallet.Currency);
                await service.SaveUpdateWallet(AuthenticationService.CurrentUser, wallet);
            }
            catch (Exception)
            {
                return;
            }
        }
    }
}
