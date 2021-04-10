using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Wallets;
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
    /// Interaction logic for MainView.xaml
    /// </summary>
    public partial class MainView : UserControl
    {
        public MainView()
        {
            InitializeComponent();
            Content = new SignInView(GotoSignUp, GotoWalletsView);
        }

        public void GotoSignUp()
        {
            Content = new SignUpView(GotoSignIn);
        }

        public void GotoSignIn()
        {
            Content = new SignInView(GotoSignUp, GotoWalletsView);
        }

        public void GotoWalletsView()
        {
            Content = new WalletsView();
        }

    }
}
