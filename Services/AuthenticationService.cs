using Budgets.Models.Users;
using Budgets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Budgets.GUI.WPF.Authentication
{
    public class AuthenticationService
    {
        private static List<DBUser> Users = new List<DBUser>() { new DBUser("1", "1", "1", "1", "1") };
        public User Authenticate(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            authUser.Password = PasswordEncoder.Encrypt(authUser.Password);
            var dbUser = Users.FirstOrDefault(user => user.Login == authUser.Login && user.Password == authUser.Password);
            if (dbUser == null)
                throw new Exception("Wrong Login or Password");
            return new User(dbUser.Guid, dbUser.FirstName, dbUser.LastName, dbUser.Email, dbUser.Login);
        }
        bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        public bool RegisterUser(RegistrationUser regUser)
        {
            if (!IsValidEmail(regUser.Email))
                throw  new Exception("Invalid Email");
            if (regUser.Login.Length < 6)
                throw new Exception("Login length can't be less 6 characters.");
            if (regUser.Password.Length < 6)
                throw new Exception("Password length can't be less 6 characters.");
            var dbUser = Users.FirstOrDefault(user => user.Login == regUser.Login);

            if (dbUser != null)
                throw new Exception("User already exists");
            if (String.IsNullOrWhiteSpace(regUser.Login) || String.IsNullOrWhiteSpace(regUser.Password) || String.IsNullOrWhiteSpace(regUser.LastName))
                throw new ArgumentException("Something is Empty. Fill everething in, please/");
            dbUser = new DBUser(regUser.FirstName + "First", regUser.LastName, regUser.Email,
                regUser.Login, PasswordEncoder.Encrypt(regUser.Password));
            Users.Add(dbUser);
            return true;
        }
    }
}
