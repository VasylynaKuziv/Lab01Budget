using System;
using System.Collections.Generic;///using Budgets.GUI.WPF.Models.Users;

using DataStorage;

namespace Lab01.Entities
{
    public class User : IStorable
    {
        public Guid Guid { get; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public List<Category> Categories { get; }
        public List<Wallet> Wallets { get; }
        public List<Wallet> SharedWallets { get; }

        public User(Guid guid, string name, string surname, string email, List<Category> categories)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Wallets = new List<Wallet>();
            SharedWallets = new List<Wallet>();
            //add categories to Category???
            if (!categories.Contains(Category.DefaultCategory)) categories.Add(Category.DefaultCategory);
            Categories = categories;
        }

        public User(Guid guid, string name, string surname, string email)
        {
            Guid = guid;
            Name = name;
            Surname = surname;
            Email = email;
            Wallets = new List<Wallet>();
            SharedWallets = new List<Wallet>();

            Categories = new List<Category>() { Category.DefaultCategory };
        }

        public User(string name, string surname, string email)
        {
            Guid = new Guid();
            Name = name;
            Surname = surname;
            Email = email;
            Wallets = new List<Wallet>();
            SharedWallets = new List<Wallet>();

            Categories = new List<Category>();
        }

        public Wallet CreateWallet(string name, string description, decimal initBalance, string currency)
        {
            Wallet wallet = Wallet.CreateWallet(this, name, currency, initBalance, description);
            return wallet;
        }

        public Wallet CreateWallet(string name, string description, decimal initBalance, string currency, Guid guid)
        {
            Wallet wallet = Wallet.CreateWallet(guid, this, name, currency, initBalance, description);
            return wallet;
        }

        public void addSharedWallet(User user, Wallet shareWallet)
        {
            if (user == null || shareWallet is null || user == this || user.SharedWallets.Contains(shareWallet) || !Wallets.Contains(shareWallet))
            {
                throw new Exception("Unable to share wallet");
            }

            user.SharedWallets.Add(shareWallet);
        }
        //work with Wallets
        public decimal GetCurrentBalance(Wallet wallet)
        {
            return wallet.CurrentBalance;
        }

        public decimal GetLastMonthIncome(Wallet wallet)
        {
            return wallet.GetLastMonthIncome();
        }
        public decimal GetLastMonthExpenses(Wallet wallet)
        {
            return wallet.GetLastMonthExpenses();
        }


        //work with Transactions
        public void AddTransaction(Wallet wallet, Transaction transaction)
        {
            wallet.AddTransaction(transaction);
        }

        public void DeleteTransaction(Wallet wallet, Transaction transaction)
        {
            wallet.DeleteTransaction(transaction);
        }


        /*
        public void addCategory(Category category)
        {
            Categories.Add(category);
        }
        public void addCategoryInWallet(Guid walletId, Category category)
        {
            foreach (Wallet wallet in Wallets)
            {
                if (wallet.Guid == walletId && wallet.OwnerId == Id)
                {
                    wallet.addCategory(category);
                }
            }
        }
        public void renameCategoryInWallet(Guid walletId, int categoryId, string newName)
        {

            foreach (Wallet wallet in Wallets)
            {
                if (wallet.Guid == walletId && wallet.OwnerId == Id)
                {
                    wallet.renameCategory(categoryId, newName);
                }
            }
        }
        public void deleteCategoryInWallet(Guid walletId, int categoryId)
        {
            foreach (Wallet wallet in Wallets)
            {
                if (wallet.Guid == walletId && wallet.OwnerId == Id)
                {
                    wallet.removeCategory(categoryId);
                }
            }
        }
        public bool Validate()
        {
            var result = true;
            if (String.IsNullOrWhiteSpace(Surname))
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (String.IsNullOrWhiteSpace(Email))
                result = false;
            return result;
        }
        */
    }
}
