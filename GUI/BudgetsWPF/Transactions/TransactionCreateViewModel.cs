using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Service;
using Lab01.Entities;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace Budgets.GUI.WPF.Transactions
{
    class TransactionCreateViewModel : INotifyPropertyChanged, INavigatable<WalletsNavigatableTypes>
    {
        private Transaction _transaction = new(WalletService.CurrentWallet.Currency);
        private Action _backToWallet;
        public List<string> allCurrency => Wallet.allCurrency.Keys.ToList();

        public string Currency
        {
            get
            {
                return _transaction.Currency;
            }
            set
            {
                _transaction.Currency = value;
                OnPropertyChanged();
                AddTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public Category CurrentCategory
        {
            get
            {
                return _transaction.Category;
            }
            set
            {
                _transaction.Category = value;
                OnPropertyChanged();
                AddTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public List<Category> Categories
        {
            get
            {
                return AuthenticationService.CurrentUser.Categories;
            }
        }

        public DelegateCommand GoBackCommand { get; }
        public DelegateCommand AddTransactionCommand { get; }
        public TransactionCreateViewModel(Action gotoWallets)
        {
            _backToWallet = gotoWallets;
            GoBackCommand = new DelegateCommand(_backToWallet);
            AddTransactionCommand = new DelegateCommand(CreateTransaction, IsValid);
        }

        public decimal Sum
        {
            get
            {
                return _transaction.Sum;
            }
            set
            {
                _transaction.Sum = value;
                OnPropertyChanged();
                AddTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get
            {
                return _transaction.Description;
            }
            set
            {
                _transaction.Description = value;
                OnPropertyChanged();
                AddTransactionCommand.RaiseCanExecuteChanged();
            }
        }

        public DateTime DateTime
        {
            get
            {
                return _transaction.Date;
            }
            set
            {
                _transaction.Date = value;
                OnPropertyChanged();
            }
        }


        private bool IsValid()
        {
            return !String.IsNullOrWhiteSpace(Description) && CurrentCategory != null && !String.IsNullOrWhiteSpace(Currency);
        }

        private async void CreateTransaction()
        {
            var service = new TransactionService();
            try
            {
                WalletService.CurrentWallet.AddTransaction(_transaction);
                await service.SaveUpdateTransaction(WalletService.CurrentWallet, _transaction);
            }
            catch (Exception ex)
            {
                return;
            }
            _backToWallet.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public WalletsNavigatableTypes Type
        {
            get
            {
                return WalletsNavigatableTypes.TransactionCreation;
            }
        }
        public void ClearSensitiveData()
        {
            _transaction = new Transaction(WalletService.CurrentWallet.Currency);
        }

        public void Update()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}