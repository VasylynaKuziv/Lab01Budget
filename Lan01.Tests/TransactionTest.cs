using System;
using Xunit;
using Lab01.Entities;

namespace Lab01.Tests
{
    public class TransactionTest
    {
        [Fact]
        public void ValidateValid()
        {
            var transaction = new Transaction(100, Currency.dollar, new Category("food", "yellow", "icon.png"), DateTime.Today, 1, 2);

            var result = transaction.Validate();

            Assert.True(result);
        }

        [Fact]
        public void ValidateSenderAndReceiver()
        {
            var transaction = new Transaction(100, Currency.dollar, new Category("food", "yellow", "icon.png"), DateTime.Today, 1, 1);

            var result = transaction.Validate();

            Assert.False(result);
        }

        [Fact]
        public void ValidateNegativeSum()
        {
            var transaction = new Transaction(-100, Currency.dollar, new Category("food", "yellow", "icon.png"), DateTime.Today, 1, 1);

            var result = transaction.Validate();

            Assert.False(result);
        }

    }
}
