using Budgets.GUI.WPF.Service;
using Budgets.Models.Users;
using Budgets.Services;
using DataStorage;
using Lab01.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Budgets.GUI.WPF.Authentication
{
    public class AuthenticationService
    {
        private readonly FileDataStorage<DBUser> _storage = new FileDataStorage<DBUser>();
        public static User CurrentUser;

        public async Task<User> Authenticate(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            var users = await _storage.GetAllAsync();
            DBUser dbUser = null;
            try
            {
                authUser.Password = PasswordEncoder.Encrypt(authUser.Password);
                dbUser = users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);
                if (dbUser == null)
                    throw new Exception("Wrong Login or Password");
            }
            catch (Exception)
            {
                throw new Exception("Wrong Login or Password");
            }

            dbUser.Categories ??= new List<Category>();
            var service = new WalletService();
            CurrentUser = new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Categories);
            CurrentUser = await service.LoadUserWallets(CurrentUser);
            return CurrentUser;
        }

        public async Task<bool> RegisterUser(RegistrationUser regUser)
        {
            var users = await _storage.GetAllAsync();
            var dbUser = users.FirstOrDefault(user => user.Login == regUser.Login);
            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Login) ||
                String.IsNullOrWhiteSpace(regUser.FirstName) ||
                String.IsNullOrWhiteSpace(regUser.LastName) ||
                String.IsNullOrWhiteSpace(regUser.Password) ||
                String.IsNullOrWhiteSpace(regUser.Email))
                throw new ArgumentException("Something is Empty. Fill everething in, please/");
            dbUser = new DBUser(regUser.FirstName, regUser.LastName, regUser.Email,
                regUser.Login, PasswordEncoder.Encrypt(regUser.Password));
            await _storage.AddOrUpdateAsync(dbUser);
            return true;
        }
    }
}
