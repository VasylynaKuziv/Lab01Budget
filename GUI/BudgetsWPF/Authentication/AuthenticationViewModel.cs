using Budgets.GUI.WPF.Navigation;
using System;


namespace Budgets.GUI.WPF.Authentication
{
    public class AuthViewModel : NavigationBase<AuthNavigatableTypes>, INavigatable<MainNavigatableTypes>
    {
        private Action _signInSuccess;

        public AuthViewModel(Action signInSuccess)
        {
            _signInSuccess = signInSuccess;
            Navigate(AuthNavigatableTypes.SignIn);
        }

        protected override INavigatable<AuthNavigatableTypes> CreateViewModel(AuthNavigatableTypes type)
        {
            if (type == AuthNavigatableTypes.SignIn)
            {
                return new SignInViewModel(() => Navigate(AuthNavigatableTypes.SignUp), _signInSuccess);
            }
            else
            {
                return new SignUpViewModel(() => Navigate(AuthNavigatableTypes.SignIn));
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