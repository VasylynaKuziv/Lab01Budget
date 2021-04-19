using DataStorage;
using Lab01.Entities;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Budgets.Models.Users
{
    public class DBUser : IStorable
    {
        public string Login { get; }
        public string Password { get; set; }
        public Guid Guid { get; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<Category> Categories { get; set; }

        public DBUser(string firstName, string lastName, string email, string login, string password)
        {
            Guid = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
        }

        [JsonConstructor]
        public DBUser(Guid guid, string firstName, string lastName, string email, string login, string password, List<Category> categories)
        {
            Guid = guid;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Login = login;
            Password = password;
            Categories = categories;
        }

    }
}
