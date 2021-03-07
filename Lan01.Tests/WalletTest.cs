using System;
using Xunit;
using Lab01.Entities;
using System.Collections.Generic;

namespace Lab01.Tests
{
    public class WalletTest
    {
        [Fact]
        public void ValidateValid()
        {
            var wallet = new Wallet("salary card", Currency.uan, 0, 1, new List<Category>() { new Category("food", "red", "icon.png") });
            Assert.True(wallet.Validate());
        }
        [Fact]
        public void ValidateEmptyCategories()
        {
            var wallet = new Wallet("salary card", Currency.uan, 0, 1, new List<Category>());
            Assert.False(wallet.Validate());
        }
        [Fact]
        public void ValidateEmptyName()
        {
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { new Category("food", "red", "icon.png") });
            Assert.False(wallet.Validate());
        }

        [Fact]
        public void sendTransaction()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            wallet.sendTransaction(100, Currency.dollar, cat.Id, 2, DateTime.Today);
            Assert.Equal(1, wallet.Transactions.Count);
        }
        [Fact]
        public void receiveTransaction()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            wallet.receiveTransaction(100, Currency.dollar, cat.Id, 2, DateTime.Today);
            Assert.Equal(1, wallet.Transactions.Count);
        }
        [Fact]
        public void removeTransaction()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            wallet.sendTransaction(100, Currency.dollar, cat.Id, 2, DateTime.Today);
            wallet.deleteTransaction(2);
            Assert.Equal(0, wallet.Transactions.Count);
        }

    }
}
