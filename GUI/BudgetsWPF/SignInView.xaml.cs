using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Budgets.GUI.WPF
{
    /// <summary>
    /// Interaction logic for SignInView.xaml
    /// </summary>
    public partial class SignInView : UserControl
    {
        public SignInView()
        {
            InitializeComponent();
        }
        private void BClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void BSignIn_Click(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TbLogin.Text) || String.IsNullOrWhiteSpace(TbPassword.Text))
                MessageBox.Show("Login or password is empty.");
            else
            {
                var authUser = new AuthenticationUser()
                {
                    Login = TbLogin.Text,
                    Password = TbPassword.Text
                };
                var authService = new AuthenticationService();
                User user = null;
                try
                {
                    user = authService.Authenticate(authUser);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Sign In failed: {ex.Message}");
                    return;
                }
                MessageBox.Show($"Sign In was successful for user {user.FirstName} {user.LastName}");
                //TODO Navigate to Main View of Application
            }
        }
        private void Input_Changed(object sender, TextChangedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(TbLogin.Text) || String.IsNullOrWhiteSpace(TbPassword.Text))
                BSignIn.IsEnabled = false;
            else
                BSignIn.IsEnabled = true;
        }
    }
}
