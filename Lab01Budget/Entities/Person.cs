using System;
using System.Collections.Generic;
namespace Lab01.Entities
{
    public class Person
    {
        private int _id;
        private string _name;
        private string _surname;
        private string _email;
        private List<Category> _categories;  
        private List<Wallet> _wallets;

        private static int IdCounter;

        public int Id
        {
            get { return _id; }
            private set { _id = value; }
        }

        public string Surname
        {
            get
            {
                return _surname;
            }
            set
            {
                _surname = value;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        
        public List<Category> Categories
        {
            get
            {
                return _categories;
            }
        }

        public List<Wallet> Wallets
        {
            get
            {
                return _wallets;
            }
        }
        

        public Person(string name, string surname, string email)
        {
            _id = IdCounter;
            _name = name;
            _surname = surname;
            _email = email;

            _categories = new List<Category>();
            _wallets = new List<Wallet>();

            IdCounter += 1;
        }

        public Person(string name, string surname, string email, List<Category> categories, List<Wallet> wallets)
        {
            _id = IdCounter;
            _name = name;
            _surname = surname;
            _email = email;

            _categories = categories;
            _wallets = wallets;

            IdCounter += 1;
        }

        public void addCategory(Category category)
        {
            _categories.Add(category);
        }

        public bool Validate()
        {
            var result = true;

            if (Id <= 0)
                result = false;
            if (String.IsNullOrWhiteSpace(Surname))
                result = false;
            if (String.IsNullOrWhiteSpace(Name))
                result = false;
            if (String.IsNullOrWhiteSpace(Email))
                result = false;

            return result;
        }


        public void addSharedWallet(Wallet wallet)
        {
            _wallets.Add(wallet);
        }
    }
}
