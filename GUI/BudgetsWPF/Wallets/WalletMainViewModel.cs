using Budgets.GUI.WPF.Categories;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Transactions;

namespace Budgets.GUI.WPF.Wallets
{
    public class WalletMainViewModel : NavigationBase<WalletsNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {

        public WalletMainViewModel()
        {
            Navigate(WalletsNavigatableTypes.Wallet);
        }

        protected override INavigatable<WalletsNavigatableTypes> CreateViewModel(WalletsNavigatableTypes type)
        {
            if (type == WalletsNavigatableTypes.Wallet)
            {
                return new WalletsViewModel(() => Navigate(WalletsNavigatableTypes.WalletCreation), () => Navigate(WalletsNavigatableTypes.TransactionCreation), () => Navigate(WalletsNavigatableTypes.CategoryCreation), () => Navigate(WalletsNavigatableTypes.Wallet));
            }
            else if (type == WalletsNavigatableTypes.CategoryCreation)
            {
                return new CategoryViewModel(() => Navigate(WalletsNavigatableTypes.Wallet));
            }
            else if (type == WalletsNavigatableTypes.WalletCreation)
            {
                return new WalletCreateViewModel(() => Navigate(WalletsNavigatableTypes.Wallet));
            }
            else
            {
                return new TransactionCreateViewModel(() => Navigate(WalletsNavigatableTypes.Wallet));
            }
        }

        public MainNavigatableTypes Type
        {
            get
            {
                return MainNavigatableTypes.Authentication;
            }
        }

        public void ClearSensitiveData()
        {
            CurrentViewModel.ClearSensitiveData();
        }

        public void Update()
        {
            CurrentViewModel.Update();
        }
    }
}
