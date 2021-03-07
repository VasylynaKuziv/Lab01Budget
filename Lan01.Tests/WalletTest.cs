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
            var wallet = new Wallet(" ", Currency.dollar, 200, 1, new List<Category>() { cat });
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
            var wallet = new Wallet(" ", Currency.dollar, 200, 1, new List<Category>() { cat });
            wallet.receiveTransaction(100, Currency.dollar, cat.Id, 2, DateTime.Today);
            wallet.deleteTransaction(wallet.Transactions[0].Id);
            Assert.Equal(0, wallet.Transactions.Count);
        }
        [Fact]
        public void getLast10()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            for( int i =0; i<10; i++)
                wallet.receiveTransaction(100*i, Currency.dollar, cat.Id, 2, DateTime.Today);
            var last10 = wallet.getLastTenTransaction();
            Assert.Equal(10, last10.Count);
        }
        [Fact]
        public void getLast10FromLess()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            for (int i = 0; i < 5; i++)
                wallet.receiveTransaction(100 * i, Currency.dollar, cat.Id, 2, DateTime.Today);
            var last10 = wallet.getLastTenTransaction();
            Assert.Equal(5, last10.Count);
        }

        [Fact]
        public void getLast10FromMany()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            for (int i = 0; i < 20; i++)
                wallet.receiveTransaction(i, Currency.dollar, cat.Id, 2, DateTime.Today);
            var last10 = wallet.getLastTenTransaction();
            Assert.Equal(19, last10[0].Sum);
        }
        [Fact]
        public void getSomeCount()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            for (int i = 0; i < 20; i++)
                wallet.receiveTransaction(i, Currency.dollar, cat.Id, 2, DateTime.Today);
            var res = wallet.getSomeTransactions(10,15);
            Assert.Equal(10, res[0].Sum);
            Assert.Equal(5, res.Count);
        }
        [Fact]
        public void getSomeButZero()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            for (int i = 1; i <= 20; i++)
                wallet.receiveTransaction(i, Currency.dollar, cat.Id, 2, DateTime.Today);
            var res = wallet.getSomeTransactions(15, 22);
            Assert.Equal(0, res.Count);
        }
        [Fact]
        public void changeTransaction()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            wallet.receiveTransaction(10, Currency.dollar, cat.Id, 2, DateTime.Today);
            var index = wallet.Transactions[0].Id;
            wallet.changeTransactionCurrency(index, Currency.euro);
            wallet.changeTransactionSum(index, 10000);
            wallet.changeTransactionDescription(index, "New description");
            Assert.Equal(Currency.euro, wallet.Transactions[0].Currency);
            Assert.Equal(10000, wallet.Transactions[0].Sum);
            Assert.Equal("New description", wallet.Transactions[0].Description);
        }
        [Fact]
        public void categoryManipulationTest()
        {
            var cat = new Category("food", "red", "icon.png");
            var wallet = new Wallet(" ", Currency.uan, 0, 1, new List<Category>() { cat });
            wallet.addCategory(new Category("home", "blue", "photo.png", "home transactions"));
            Assert.Equal(2, wallet.Categories.Count);

            wallet.renameCategory(wallet.Categories[1].Id, "pobut");
            Assert.Equal("pobut", wallet.Categories[1].Name);

            wallet.removeCategory(wallet.Categories[0].Id);
            Assert.Equal(1, wallet.Categories.Count);
        }
    }
}
