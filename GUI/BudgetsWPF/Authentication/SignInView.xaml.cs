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

namespace Budgets.GUI.WPF.Authentication
{
    public partial class SignInView : UserControl
    {
        private SignInViewModel _viewModel;
        public SignInView(Action gotoSignUp, Action gotoWallets)
        {
            InitializeComponent();
            _viewModel = new SignInViewModel(gotoSignUp, gotoWallets);
            this.DataContext = _viewModel;
        }
        private void TbPassword_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            ((SignInViewModel)DataContext).Password = TbPassword.Password;
        }
    }
}
