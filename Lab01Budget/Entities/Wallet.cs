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
        private string _mainCurrency;
        private decimal _initBalance;
        private int _ownerId;
        private HashSet<int> _otherUsers;
        private List<Transaction> _transactions;
        private List<Category> _categories;

        public static Dictionary<string, int> allCurrency = new Dictionary<string, int>
        {
            {"UAH", 1},
            {"USD", 28}
        };

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }
        public string Name 
        { 
            get {return _name; }
             set{ _name = value; }
        }
        public string? Description
        {
            get { return _description; }
            set { _description = value; }
        }
        public string MainCurrency
        {
            get { return _mainCurrency; }
        }
        public decimal InitBalance
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
        public Wallet(string name, string mainCurrency, decimal initBalance, int ownerId, List<Category> categories, string description = "")
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
        
        public Wallet(string name, decimal initBalance)
        {
            _id = InstanceCount;
            _name = name;
            _initBalance = initBalance;
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
        private Category hasCategory(int catId) 
        {
            foreach (var cat in _categories)
                if (catId == cat.Id)
                    return cat;
            return null;
        }
        public void sendTransaction(decimal sum, string currency, int categoryId, int receiverId, DateTime date, string description = "")
        {
            var category = hasCategory(categoryId);
            if (category !=null && getCurrentBalance() >= (sum * allCurrency[currency]))
            {
                _transactions.Add(new Transaction(sum, currency, category,date, Id, receiverId,description));
            }
        }
        public void receiveTransaction(decimal sum, string currency, int categoryId, int senderId, DateTime date, string description = "")
        {
            var category = hasCategory(categoryId);
            if (category != null)
            {
                _transactions.Add(new Transaction(sum, currency, category, date, senderId, Id, description));
            }
        }
        public void deleteTransaction(int trId)
        {
            foreach (var tr in _transactions.ToArray())
                if (trId == tr.Id)
                    _transactions.Remove(tr);
        }
        public void addCategory(Category category)
        {
            _categories.Add(category);
        }
        public void renameCategory(int catId, string newName)
        {
            foreach (var cat in _categories.ToArray())
                if (catId == cat.Id)
                    cat.Name = newName;
        }
        public void removeCategory(int catId)
        {
            foreach (var cat in _categories.ToArray())
                if (catId == cat.Id)
                    _categories.Remove(cat);
        }
        public void shareWallet(int newUser)
        {
            if (newUser>=0 && OwnerId!=newUser)
                _otherUsers.Add(newUser);
        }

        public List<Transaction> getLastTenTransaction()
        {
            List<Transaction> result;
            _transactions.Reverse();
            if (_transactions.Count < 10)
                result = _transactions.GetRange(0, _transactions.Count);
            else 
                result = _transactions.GetRange(0, 10);
            _transactions.Reverse();
            return result;
        }
        public List<Transaction> getSomeTransactions(int start, int end)
        {
            if (start < 0 || end > _transactions.Count||start>end)
                return new List<Transaction>();
            var result = _transactions.GetRange(start, (end - start));
            return result;
        }
        public void changeTransactionSum(int trId, decimal newSum)
        {
            foreach (var tr in _transactions)
                if (tr.Id == trId)
                    tr.Sum = newSum;
        }
        public void changeTransactionCurrency(int trId, string value)
        {
            foreach (var tr in _transactions)
                if (tr.Id == trId)
                    tr.Currency = value;
        }

        public void changeTransactionDescription(int trId, string value)
        {
            foreach (var tr in _transactions)
                if (tr.Id == trId)
                    tr.Description = value;
        }

        public override string ToString()
        {
            return $"Id: {Id}, Name: {Name}\nDescription: {Description}";
        }

        //added by Iryna
        public decimal getCurrentBalance()
        {
            //remove
            if (MainCurrency == null)
            {
                return InitBalance;
            }
            decimal balance = InitBalance * allCurrency[MainCurrency];
            if (Transactions == null)
            {
                return balance;
            }
            foreach (Transaction tr in Transactions) {
                int rate = allCurrency[tr.Currency];
                decimal sum = tr.Sum;
                if(tr.ReceiverId== Id)
                {
                    balance += rate * sum;
                }
                else
                {
                    balance -= rate * sum;
                }
            }

            return balance;
        }


        public bool validateDate(DateTime dt)
        {
            DateTime lastMonth = DateTime.Today.AddMonths(-1);
            return (dt > DateTime.Today.AddMonths(-1) && dt <= DateTime.Today);
        }


        public decimal getLastMonthIncome()
        {
            decimal income = 0;
            if (Transactions == null)
            {
                return 0;
            }
            foreach (Transaction tr in Transactions)
            {
                int rate = allCurrency[tr.Currency];
                decimal sum = tr.Sum;

                if (tr.ReceiverId == Id && validateDate(tr.Date))
                {
                    income += rate * sum;
                }
                
            }

            return income;
        }


        public decimal GetLastMonthExpenses()
        {

            decimal expenses = 0;
            if (Transactions == null)
            {
                return 0;
            }
            foreach (Transaction tr in Transactions)
            {
                int rate = allCurrency[tr.Currency];
                decimal sum = tr.Sum;

                if (tr.SenderId == Id && validateDate(tr.Date))
                {
                    expenses += rate * sum;
                }

            }

            return expenses;

        }
    }
}
