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
        public void AddTransaction()
        {
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { new Category("food", "red", "icon.png") });
            wallet.addTransaction(new Transaction(200,Currency.euro,))
            Assert.False(wallet.Validate());
        }

    }
}
