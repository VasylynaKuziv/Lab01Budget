using Budgets.GUI.WPF.Navigation;
using System.Collections.ObjectModel;
using Budgets.GUI.WPF.Service;
using Budgets.GUI.WPF.Authentication;
using Prism.Commands;
using Lab01Budget.Entities.Storage;
using System.ComponentModel;
using System;
using System.Runtime.CompilerServices;
using Budgets.GUI.WPF.Transactions;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletsViewModel : INotifyPropertyChanged, INavigatable<WalletsNavigatableTypes>
    {
       
        private WalletDetailsViewModel _currentWallet;
        private Action _toCreateWallet;

        private Action _createCategory;
        
        private TransactionDetailViewModel _currentTransaction;
        private Action _toCreateTransaction;

        private Action _update;

        public WalletDetailsViewModel CurrentWallet
        {
            get
            {
                return _currentWallet;
            }
            set
            {
                _currentTransaction = null;
                _currentWallet = value;
                WalletService.CurrentWallet = _currentWallet?.Wallet;

                Transactions = new ObservableCollection<TransactionDetailViewModel>();

                if (_currentWallet != null)
                {
                    foreach (var transaction in _currentWallet.Wallet.GetAllTransactions(AuthenticationService.CurrentUser))
                    {
                        Transactions.Add(new TransactionDetailViewModel(transaction, _update));
                    }
                }


                OnPropertyChanged(nameof(CurrentWallet));
                OnPropertyChanged(nameof(CurrentTransaction));
                OnPropertyChanged(nameof(Transactions));
                GoToTransactionCreation.RaiseCanExecuteChanged();
                RemoveWalletCommand.RaiseCanExecuteChanged();

            }
        }


        public WalletsViewModel(Action gotoWalletCreation, Action gotoTransactionCreation, Action categoryCreation, Action update)
        {
            Wallets = new ObservableCollection<WalletDetailsViewModel>();
        
            foreach (var wallet in AuthenticationService.CurrentUser.Wallets)
            {
                Wallets.Add(new WalletDetailsViewModel(wallet));
            }

            _toCreateWallet = gotoWalletCreation;
            _toCreateTransaction = gotoTransactionCreation;
            _createCategory = categoryCreation;
            _update = update;
            GoToWalletCreation = new DelegateCommand(_toCreateWallet);
            GoToTransactionCreation = new DelegateCommand(_toCreateTransaction, IsSelectedWallet);
            GoToCategoryCreation = new DelegateCommand(_createCategory);
            RemoveWalletCommand = new DelegateCommand(RemoveWallet, IsSelectedWallet);
            RemoveTransactionCommand = new DelegateCommand(RemoveTransaction, IsSelectedTransaction);
        }

        public async void LoadWallets()
        {
            var service = new WalletService();
            try
            {
                await service.LoadUserWallets(AuthenticationService.CurrentUser);
            }
            catch (Exception ex)
            {
                return;
            }
        }
        public async void RemoveWallet()
        {

            WalletService service = new WalletService();
            DBWallet wallet = new DBWallet(_currentWallet.Wallet.Guid, _currentWallet.Name, _currentWallet.Wallet.CurrentBalance, _currentWallet.Description,
                    _currentWallet.Currency);
            await service.DeleteWallet(wallet);

            TransactionService tservice = new TransactionService();
            foreach (var transaction in _currentWallet.Wallet.GetAllTransactions(AuthenticationService.CurrentUser))
            {
                await tservice.DeleteTransaction(transaction);
            }

            AuthenticationService.CurrentUser.Wallets.RemoveAll(x => x.Guid == wallet.Guid);
            Update();
        }

        private bool IsSelectedWallet()
        {
            return WalletService.CurrentWallet != null;
        }


        public WalletsNavigatableTypes Type
        {
            get
            {
                return WalletsNavigatableTypes.Wallet;
            }
        }


        //work with Transaction
        public TransactionDetailViewModel CurrentTransaction
        {
            get
            {
                return _currentTransaction;
            }
            set
            {
                _currentWallet = null;
                _currentTransaction = value;
                OnPropertyChanged(nameof(CurrentWallet));
                OnPropertyChanged(nameof(CurrentTransaction));
                RemoveTransactionCommand.RaiseCanExecuteChanged();

            }
        }

        private bool IsSelectedTransaction()
        {
            return CurrentTransaction != null;
        }


        public async void RemoveTransaction()
        {
            TransactionService service = new TransactionService();
            WalletService.CurrentWallet.DeleteTransaction(CurrentTransaction.Transaction);
            await service.DeleteTransaction(CurrentTransaction.Transaction);
            Transactions.Remove(CurrentTransaction);
            OnPropertyChanged(nameof(Transactions));
            Update();
        }


        public ObservableCollection<WalletDetailsViewModel> Wallets { get; set; }
        public ObservableCollection<TransactionDetailViewModel> Transactions { get; set; }

        public DelegateCommand GoToWalletCreation { get; }
        public DelegateCommand RemoveWalletCommand { get; }
        public DelegateCommand GoToCategoryCreation { get; }
        public DelegateCommand GoToTransactionCreation { get; }
        public DelegateCommand RemoveTransactionCommand { get; }




        public void ClearSensitiveData()
        {

        }

        public void Update()
        {
            Wallets = new ObservableCollection<WalletDetailsViewModel>();
            foreach (var wallet in AuthenticationService.CurrentUser.Wallets)
            {
                Wallets.Add(new WalletDetailsViewModel(wallet));
            }
            OnPropertyChanged(nameof(Wallets));

            if (WalletService.CurrentWallet != null)
            {
                Transactions = new ObservableCollection<TransactionDetailViewModel>();
                foreach (var transaction in WalletService.CurrentWallet.GetAllTransactions(AuthenticationService.CurrentUser))
                    Transactions.Add(new TransactionDetailViewModel(transaction, _update));
                OnPropertyChanged(nameof(Transactions));
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}