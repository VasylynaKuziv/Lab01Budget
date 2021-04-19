using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Wallets;

namespace Budgets.GUI.WPF
{
    public class MainViewModel : NavigationBase<MainNavigatableTypes>
    {
        public MainViewModel()
        {
            Navigate(MainNavigatableTypes.Authentication);
        }

        protected override INavigatable<MainNavigatableTypes> CreateViewModel(MainNavigatableTypes type)
        {
            if (type == MainNavigatableTypes.Authentication)
            {
                return new AuthViewModel(() => Navigate(MainNavigatableTypes.Wallets));
            }
            else
            {
                return new WalletMainViewModel();
            }
        }
    }
}