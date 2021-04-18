﻿using Budgets.GUI.WPF;
using Budgets.GUI.WPF.Authentication;
using Budgets.Models.Users;
using Budgets.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Lab02Tests
{
    public class AuthRegTest
    {
        [Fact]
        public void ValidateRegAuth()
        {

            AuthenticationService service = new AuthenticationService();
            SignInViewModel model = new SignInViewModel();
            model.Login = "login";
            model.Password = "password";

            RegistrationUser registrationUser = new RegistrationUser();
            registrationUser.Email = "test@gmail.com";
            registrationUser.FirstName = "test";
            registrationUser.LastName = "test";
            registrationUser.Login = "testLogin";
            registrationUser.Password = "testPassword";
            Assert.True(service.RegisterUser(registrationUser));

            AuthenticationUser aUser = new();
            aUser.Login = "testLogin";
            aUser.Password = "testPassword";
            var createdUser = service.Authenticate(aUser);
            Assert.Equal( "test", createdUser.LastName);
            Assert.Equal("test", createdUser.FirstName);
            Assert.Equal("testLogin", createdUser.Login);
            Assert.Equal("test@gmail.com", createdUser.Email);
        }

    }
}