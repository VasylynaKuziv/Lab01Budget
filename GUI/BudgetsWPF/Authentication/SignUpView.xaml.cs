
using System.Windows;
using System.Windows.Controls;
using Budgets.Services;

using System;

namespace Budgets.GUI.WPF.Authentication
{
    public partial class SignUpView : UserControl
    {
        public SignUpView()
        {
            InitializeComponent();
        }

        private void TbPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignUpViewModel)DataContext).Password = TbPassword.Password; 
        }
    }
}
