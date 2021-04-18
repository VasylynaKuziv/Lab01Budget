using Prism.Mvvm;
using Lab01.Entities;
using System;
using Prism.Commands;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletDetailsViewModel : BindableBase
    {
        private Wallet _wallet;

        public int Id
        {
            get
            {
                return _wallet.Id;
            }
        }
        public string Incomes
        {
            get
            {
                return _wallet.getLastMonthIncome().ToString("#.##");
            }
        }
        public string Expenses
        {
            get
            {
                return _wallet.GetLastMonthExpenses().ToString("#.##");
            }
        }

        public Wallet Wallet
        {
            get
            {
                return _wallet;
            }
        }

        public string NameErr { get; set; }
        public string Name
        {
            get
            {
                return _wallet.Name;
            }
            set
            {
                _wallet.Name = value;
                //test
                RaisePropertyChanged(nameof(DisplayName));
                UpdateWalletCommand.RaiseCanExecuteChanged();
            }
        }

      
        public DelegateCommand UpdateWalletCommand { get; }


        public string DescriptionErr { get; set; }
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
                return _wallet.MainCurrency;
            }
        }

        
        public string CurrBalance
        {
            get
            {
                return _wallet.getCurrentBalance().ToString("#.##");
            }

        }
        
        public string DisplayName
        {
            get
            {
                return $"{_wallet.Name} ({_wallet.getCurrentBalance():#.##} )";
            }
        }
        
        public WalletDetailsViewModel(Wallet wallet)
        {
            _wallet = wallet;
            UpdateWalletCommand = new DelegateCommand(UpdateWallet, IsValid);
        }

        private bool IsValid()
        {
            bool valid = true;
            if (String.IsNullOrWhiteSpace(Name))
            {
                NameErr = "Name can't be empty";
                RaisePropertyChanged(nameof(NameErr));
                valid = false;
            }
            else if (Name.Length > 20)
            {
                NameErr = "Name can't be more than 20 symbols";
                RaisePropertyChanged(nameof(NameErr));
                valid = false;
            }
            else
            {
                NameErr = "";
                RaisePropertyChanged(nameof(NameErr));
            }

            if (String.IsNullOrWhiteSpace(Description))
            {
                DescriptionErr = "Description can't be empty";
                RaisePropertyChanged(nameof(DescriptionErr));
                valid = false;
            }
            else
            {
                DescriptionErr = "";
                RaisePropertyChanged(nameof(DescriptionErr));
            }

            return valid;
        }


        private async void UpdateWallet()
        {
            //var service = new WalletService();
            try
            {
               // WalletDb wallet = new WalletDb(_wallet.Guid, _wallet.Name, _wallet.CurrBalance, _wallet.Description,
               //     _wallet.Currency);
               // await service.SaveUpdateWallet(AuthenticationService.CurrentUser, wallet);
            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Update wallet failed: {ex.Message}");
                return;
            }
            //MessageBox.Show($"Wallet updated");
        }
    }
}
