using Budgets.GUI.WPF;
using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Service;
using Budgets.GUI.WPF.Categories;
using Budgets.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Lab01.Entities;
using Budgets.Models.Users;

namespace Lab02Tests
{
    public class TransactionTest
    {
        [Fact]
        public void ValidateCreateTransactionClass()
        {
            var tran = new Transaction(-100,"USD",new Category("food", "cool"),DateTime.Today,"products");
            Assert.Equal(-100, tran.Sum);
            Assert.Equal("USD", tran.Currency);
            Assert.Equal(DateTime.Today, tran.Date);
        }

        [Fact]
        public void ValidateCreateTransaction()
        {
            AuthenticationService service = new AuthenticationService();
            AuthenticationUser aUser = new();
            aUser.Login = "testLogin";
            aUser.Password = "testPassword";
            var createdUser = service.Authenticate(aUser).Result;
            var _wallet = Wallet.CreateWallet(createdUser, "Test2", "UAH", 100, "Test2");
            Assert.NotEmpty(createdUser.Wallets);
            var tran = new Transaction(1000, "UAH", new Category("food", "cool"), DateTime.Today, "products");
            _wallet.AddTransaction(tran);
            Assert.Equal(1100, _wallet.CurrentBalance);

            var tran2 = new Transaction(-500, "UAH", new Category("food", "cool"), DateTime.Today, "products");
            _wallet.AddTransaction(tran2);
            Assert.Equal(600, _wallet.CurrentBalance);
            Assert.Equal(1000, _wallet.GetLastMonthIncome());
            Assert.Equal(-500, _wallet.GetLastMonthExpenses());

            _wallet.DeleteTransaction(tran2);
            Assert.Equal(1100, _wallet.CurrentBalance);

        }
        }
}
