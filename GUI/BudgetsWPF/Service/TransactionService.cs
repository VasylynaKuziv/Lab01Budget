using DataStorage;
using Lab01.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using Budgets.GUI.WPF.Authentication;

namespace Budgets.GUI.WPF.Service
{
    public class TransactionService
    {
        private FileDataStorage<Transaction> _storage = new FileDataStorage<Transaction>();

        public async Task<Wallet> LoadTransactions(Wallet wallet)
        {
            List<Transaction> transactions = await _storage.GetAllAsyncForObject(wallet);
            foreach (var transaction in transactions)
            {
                wallet.AddTransaction(transaction);
            }
            return wallet;
        }

        public async Task<bool> SaveUpdateTransaction(Wallet wallet, Transaction transaction)
        {
            await _storage.AddOrUpdateAsyncForObject(transaction, wallet);
            return true;
        }

        public async Task<bool> DeleteTransaction(Transaction transaction)
        {
            await _storage.RemoveObj(transaction);
            return true;
        }
    }
}
