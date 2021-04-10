using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Budgets.GUI.WPF
{
    public class AuthenticationService
    {
        public User Authenticate(AuthenticationUser authUser)
        {
            if (String.IsNullOrWhiteSpace(authUser.Login) || String.IsNullOrWhiteSpace(authUser.Password))
                throw new ArgumentException("Login or Password is Empty");
            //Todo Call method for user login and password validation and retrieve user from storage
            return new User(Guid.NewGuid(), "UserName", "UserSurname", "user_email@gmail.com", "UserLogin");
        }
    }
}
