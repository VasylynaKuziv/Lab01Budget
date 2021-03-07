
using System;
using System.Collections.Generic;

namespace Lab01.Entities
{
    public class Wallet 
    {
        private static int InstanceCount = 0;

        private int _id;
        private string _name;
        private string? _description;
        private Currency _mainCurrency;
        private double _initBalance;
        private int _ownerId;
        private HashSet<int> _otherUsers;
        private List<Transaction> _transactions;
        private List<Category> _categories;


        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public string Name 
        { 
            get {return _name; }
            private set{ _name = value; }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public Currency MainCurrency
        {
            get { return _mainCurrency; }
            set { _mainCurrency = value; }
        }
        public double InitBalance
        {
            get { return _initBalance; }
            set { _initBalance = value; }
        }
        public int OwnerId
        {
            get { return _ownerId; }
            set { _ownerId = value; }
        }
        public HashSet<int> OtherUsers
        {
            get { return _otherUsers; }
        }
        public List<Transaction> Transactions
        {
            get { return _transactions; }
        }
        public List<Category> Categories
        {
            get { return _categories; }
        }
        public Wallet(string name, Currency mainCurrency, double initBalance, int ownerId, List<Category> categories, string description = "")
        {
            _id = InstanceCount;
            _name = name;
            _mainCurrency = mainCurrency;
            _ownerId = ownerId;
            _categories = categories;
            _description = description;
            _otherUsers = new HashSet<int>();
            _initBalance = initBalance;
            _transactions = new List<Transaction>();

            InstanceCount += 1;
        }
        

        public bool Validate()
        {
            var result = true;

            if (Id < 0)
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (Categories.Count == 0 || Categories == null)
                result = false;
            if (OwnerId <= 0)
                result = false;

            return result;
        }

        public void addTransaction(Transaction newTransaction)
        {
            newTransaction.SenderId = Id;
            //if (newTransaction.Category)
            _transactions.Add(newTransaction);
            
        }
        public void deleteTransaction(Transaction newTransaction)
        {
            _transactions.Remove(newTransaction);
        }
        public void addCategory(Category category)
        {
            _categories.Add(category);
        }
        public void removeCategory(Category category)
        {
            _categories.Remove(category);
        }
        public void shareWallet(int newUser)
        {
            if (newUser>=0 )
                _otherUsers.Add(newUser);
        }

        public List<Transaction> getLastTenTransaction()
        {
            _transactions.Reverse();
            var res = _transactions.GetRange(0, 10);
            _transactions.Reverse();
            return res;
        }
        public List<Transaction> getSomeTransactions(int start, int end)
        {
            return _transactions.GetRange(start, (end - start));
        }
        public Transaction changeTransactionSum(Transaction tr, double newSum)
        {
            var index = _transactions.IndexOf(tr);
            var modified = tr;
            modified.Sum = newSum;
            _transactions.Remove(tr);
            _transactions.Insert(index, modified);
            return modified;
        }
        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}\nDescription: {Description}";
        }
    }
}
