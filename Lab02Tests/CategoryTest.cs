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

namespace Lab02Tests
{
    public class CategoryTest
    {
        [Fact]
        public void ValidateCreateCategory()
        {
            var cat = new Category("Category1", "Description for Cat1");
            Assert.Equal("Category1", cat.Name);
            Assert.Equal("Description for Cat1", cat.Description);
        }

    }
}
