using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Service;
using Lab01.Entities;
using Lab01Budget.Entities.Storage;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Budgets.GUI.WPF.Wallets
{
    class WalletCreateViewModel : INotifyPropertyChanged, INavigatable<WalletsNavigatableTypes>
    {

        private DBWallet _wallet = new DBWallet();
        private Action _backToWallets;
        public WalletCreateViewModel(Action gotoWallets)
        {
            _backToWallets = gotoWallets;
            GoBackCommand = new DelegateCommand(_backToWallets);
            AddWalletCommand = new DelegateCommand(CreateWallet, checkInput);
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
                OnPropertyChanged();
                AddWalletCommand.RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get
            {
                return _wallet.Description;
            }
            set
            {
                _wallet.Description = value;
                OnPropertyChanged();
                AddWalletCommand.RaiseCanExecuteChanged();
            }
        }

        public decimal Balance
        {
            get
            {
                return _wallet.CurrBalance;
            }
            set
            {
                _wallet.CurrBalance = value;
                OnPropertyChanged();
                AddWalletCommand.RaiseCanExecuteChanged();
            }
        }


        public List<string> allCurrency => Wallet.allCurrency.Keys.ToList();
        public string Currency
        {
            get
            {
                return _wallet.Currency;
            }
            set
            {
                _wallet.Currency = value;
                OnPropertyChanged();
                AddWalletCommand.RaiseCanExecuteChanged();
            }
        }

        public WalletsNavigatableTypes Type
        {
            get
            {
                return WalletsNavigatableTypes.WalletCreation;
            }
        }

        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand AddWalletCommand { get; }

        private bool checkInput()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Description) &&
                !String.IsNullOrWhiteSpace(Currency) && (Balance > 0);
        }

        private async void CreateWallet()
        {

            var service = new WalletService();
            try
            {
                AuthenticationService.CurrentUser.CreateWallet(_wallet.Name, _wallet.Description, _wallet.CurrBalance, _wallet.Currency, _wallet.Guid); 
                await service.SaveUpdateWallet(AuthenticationService.CurrentUser, _wallet);
            }
            catch (Exception ex)
            {
                return;
            }

            _backToWallets.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void ClearSensitiveData()
        {
            _wallet = new DBWallet();
        }

        public void Update() { }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
