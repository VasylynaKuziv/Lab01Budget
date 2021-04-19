using DataStorage;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lab01.Entities;
using Lab01Budget.Entities.Storage;
using Budgets.Models.Users;

namespace Budgets.GUI.WPF.Service
{
    public class WalletService
    {
        private FileDataStorage<DBWallet> _storage = new FileDataStorage<DBWallet>();
        public static Wallet CurrentWallet;

        public async Task<User> LoadUserWallets(User user)
        {
            var transactService = new TransactionService();
            List<DBWallet> wallets = await _storage.GetAllAsyncForObject(user);
            foreach (var wallet in wallets)
            {
                Wallet wallet_cr = Wallet.CreateWallet(wallet.Guid, user, wallet.Name, wallet.Currency, wallet.CurrBalance, wallet.Description);
                await transactService.LoadTransactions(wallet_cr);

            }
            return user;
        }

        public async Task<bool> SaveUpdateWallet(User user, DBWallet wallet)
        {
            await _storage.AddOrUpdateAsyncForObject(wallet, user);
            return true;
        }
       
        public async Task<bool> DeleteWallet(DBWallet wallet)
        {
            //var transactService = new TransactionService();
             await _storage.RemoveObj(wallet);
              return true;
        }
    }
}

