using System;
using Xunit;
using Lab01.Entities;

namespace Lab01.Tests
{
    public class CategoryTest
    {
        [Fact]
        public void ValidateValid()
        {
            var category = new Category("food", "all about food, cafes, restaurants etc.", "red", "food_icon.png");

            var result = category.Validate();

            Assert.True(result);
        }

    }
}
