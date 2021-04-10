﻿
using System;

namespace Budgets.Models.Users
{
    public class DBUser
    {
        public string Login { get; }
        public string Password { get; }
        public Guid Guid { get; }
        public string FirstName { get; }

        public string LastName { get; }

        public string Email { get; }
        public DBUser(string firstName, string lastName, string email, string login, string password)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
        }
    }
}