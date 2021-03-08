using System;
using System.Collections.Generic;
using Xunit;
using Lab01.Entities;

namespace Lab01.Tests
{
    public class PersonTest
    { 

        [Fact]
        public void ValidateAddCategory()
        {
            Person person = new Person("Peter", "Parker", "peter.parker@gmail.com");
            int beforeAdd = person.Categories.Count;
            Category cat = new Category("food", "blue", "food.png");
            person.addCategory(cat);
            int afterAdd = person.Categories.Count;
            Assert.True(afterAdd-beforeAdd==1);
        }

        [Fact]
        public void ValidateAddSharedWallet()
        {
            Person person = new Person("Peter", "Parker", "peter.parker@gmail.com");
            int beforeAdd = person.Wallets.Count;
            var wallet = new Wallet("salary card", Currency.uan, 0, 1, new List<Category>() { new Category("food", "red", "icon.png") });
            Assert.DoesNotContain(person.Id, wallet.OtherUsers);
            person.addSharedWallet(wallet);
            Assert.Contains(person.Id, wallet.OtherUsers);
            int afterAdd = person.Wallets.Count;
            Assert.True(afterAdd - beforeAdd == 1);
        }
        
        [Fact]
        public void ValidateAddCategoryInWallet()
        {
            var wallet = new Wallet("salary card", Currency.uan, 0, 0, new List<Category>() { new Category("food", "red", "icon.png") });
            Person person = new Person("Peter", "Parker", "peter.parker@gmail.com", new List<Category>(), new List<Wallet>() { wallet });
            int beforeAdd = wallet.Categories.Count;

            Category cat = new Category("fuel", "blue", "fuel.png");
            person.addCategoryInWallet(wallet.Id, cat);
            int afterAdd = wallet.Categories.Count;
            Assert.True(afterAdd - beforeAdd == 1);
        }
        
        [Fact]
        public void ValidateRenameCategoryInWallet()
        {
            var wallet = new Wallet("salary card", Currency.uan, 0, 0, new List<Category>() { new Category("food", "red", "icon.png") });
            Person person = new Person("Peter", "Parker", "peter.parker@gmail.com", new List<Category>(), new List<Wallet>() { wallet });
            string oldName = wallet.Categories[0].Name;

            person.renameCategoryInWallet(wallet.Id, wallet.Categories[0].Id, "cafe");
            string newName = wallet.Categories[0].Name;
            Assert.NotEqual(oldName, newName);
        }

        [Fact]
        public void ValidateDeleteCategoryInWallet()
        {
            var wallet = new Wallet("salary card", Currency.uan, 0, 0, new List<Category>() { new Category("food", "red", "icon.png") });
            Person person = new Person("Peter", "Parker", "peter.parker@gmail.com", new List<Category>(), new List<Wallet>() { wallet });
            int beforeDelete = wallet.Categories.Count;

            person.deleteCategoryInWallet(wallet.Id, wallet.Categories[0].Id);
            int afterDelete = wallet.Categories.Count;

            Assert.True(beforeDelete - 1 == afterDelete);
        }

        [Fact]
        public void ValidateValid()
        {
            Person person = new Person("", " ", "peter.parker@gmail.com");
            Assert.False(person.Validate());
        }
    }
}
