using System;
using System.Collections.Generic;
//using Budgets.GUI.WPF.Models.Wallets;
using DataStorage;

namespace Lab01.Entities
{
    public class Wallet : IStorable
    {
        public Guid Guid { get; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal InitBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public List<Category> Categories { get; }

        private List<Transaction> Transactions;

        private List<Category> AllCategories;

        private string currency;

        public string Currency
        {
            get => currency;

            set
            {
                InitBalance = InitBalance * allCurrency[currency] / allCurrency[value];
                currency = value;
                CurrentBalance = CurrentBalance * allCurrency[currency] / allCurrency[value];
            }
        }

        public static Dictionary<string, int> allCurrency = new Dictionary<string, int>
        {
            {"USD", 28},
            {"EUR", 33},
            {"UAH", 1}
      
        };

        public Wallet(string name, string description, decimal initBalance, List<Category> personCategories, string currency)
        {
            Guid = Guid.NewGuid();
            Name = name;
            Description = description;
      
            AllCategories = personCategories;
            Categories = new List<Category>();
            
            InitBalance = initBalance;
            CurrentBalance = initBalance;

            this.currency = currency;
            Transactions = new List<Transaction>();
        }

        public Wallet(Guid guid, string name, string description, decimal initBalance, List<Category> personCategories, string currency)
        {
            Guid = guid;
            Name = name;
            Description = description;

            AllCategories = personCategories;
            Categories = new List<Category>();

            InitBalance = initBalance;
            CurrentBalance = initBalance;

            this.currency = currency;
            Transactions = new List<Transaction>();
        }

        public static Wallet CreateWallet(User person, string name, string mainCurrency, decimal initBalance, string description)
        {
            Wallet wallet = new Wallet(name, description, initBalance, person.Categories, mainCurrency);
            person.Wallets.Add(wallet);
            return wallet;
        }

        public static Wallet CreateWallet(Guid guid, User person, string name, string mainCurrency, decimal initBalance, string description)
        {
            Wallet wallet = new Wallet(guid, name, description, initBalance, person.Categories, mainCurrency);
            person.Wallets.Add(wallet);
            return wallet;
        }

        //work with Transactions

        //test
        public List<Transaction> GetLastTenTransaction()
        {
            List<Transaction> result;
            Transactions.Reverse();
            if (Transactions.Count < 10)
                result = Transactions.GetRange(0, Transactions.Count);
            else
                result = Transactions.GetRange(0, 10);
            Transactions.Reverse();
            return result;
        }

        public List<Transaction> GetSomeTransactions(int start, int end)
        {
            if (start < 0 || end > Transactions.Count || start > end)
                return new List<Transaction>();
            var result = Transactions.GetRange(start, (end - start));
            return result;
        }

        public List<Transaction> GetAllTransactions(User user)
        {
            return Transactions;
        }

        //test
        public void AddTransaction(Transaction transaction)
        {
            if (Transactions.Contains(transaction) || Categories.Contains(transaction.Category))
            {
                throw new Exception("Unable to add transaction to wallet");
            }

            Transactions.Add(transaction);
            CurrentBalance += transaction.Sum*allCurrency[transaction.Currency]/ allCurrency[Currency];
        }

        //test
        public void DeleteTransaction(Transaction transaction)
        {
            if (!Transactions.Contains(transaction))
            {
                throw new Exception("Unable to delete transaction from wallet");
            }
            Transactions.Remove(transaction);

            CurrentBalance -= transaction.Sum*allCurrency[transaction.Currency]/allCurrency[Currency];
        }

        public bool validateDate(DateTime dt)
        {
            DateTime lastMonth = DateTime.Today.AddMonths(-1);
            return (dt > DateTime.Today.AddMonths(-1) && dt <= DateTime.Today);
        }

        public decimal GetLastMonthIncome()
        {
            decimal income = 0;
            //if (Transactions == null)
            //{
            //    return 0;
            //}
            foreach (Transaction tr in Transactions)
            {
                int rate = allCurrency[tr.Currency];
                decimal sum = tr.Sum;
                if (validateDate(tr.Date) && sum > 0)
                {
                    income += rate * sum;
                }
            }
            return income;
        }


        public decimal GetLastMonthExpenses()
        {
            decimal expenses = 0;
            //if (Transactions == null)
            //{
            //    return 0;
            //}
            foreach (Transaction tr in Transactions)
            {
                int rate = allCurrency[tr.Currency];
                decimal sum = tr.Sum;
                if (validateDate(tr.Date) && sum < 0)
                {
                    expenses += rate * sum;
                }
            }
            return expenses;
        }

        /*
        public override string ToString()
        {
            return $"Name: {Name}\nDescription: {Description}";
        }
        */
    }
}
