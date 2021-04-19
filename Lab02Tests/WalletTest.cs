using Budgets.GUI.WPF;
using Budgets.GUI.WPF.Authentication;
using System.Collections.Generic;
using Xunit;
using Lab01.Entities;
using Budgets.Models.Users;

namespace Lab02Tests
{
    public class WalletTest
    {
        [Fact]
        public void ValidateCreateWalletClass()
        {
            var wallet = new Wallet("Test1", "Description for Test1",1000,new List<Category>(),"USD");
            Assert.Equal("Test1", wallet.Name);
            Assert.Equal("Description for Test1", wallet.Description);
            Assert.Equal(1000, wallet.InitBalance);
        }

        [Fact]
        public void ValidateCreateWallet()
        {

            AuthenticationService service = new AuthenticationService();
            AuthenticationUser aUser = new();
            aUser.Login = "testLogin";
            aUser.Password = "testPassword";
            var createdUser = service.Authenticate(aUser).Result;
            var _wallet = Wallet.CreateWallet(createdUser, "Test2", "UAN", 100, "Test2");
            Assert.NotEmpty(createdUser.Wallets);
        }
        }
}
