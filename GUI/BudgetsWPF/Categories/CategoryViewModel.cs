using Budgets.GUI.WPF.Authentication;
using Budgets.GUI.WPF.Navigation;
using Budgets.GUI.WPF.Service;
using Lab01.Entities;
using Prism.Commands;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budgets.GUI.WPF.Categories
{
   public class CategoryViewModel : INotifyPropertyChanged, INavigatable<WalletsNavigatableTypes>
    {
        private Category _category = new Category();
        private Category _currentCategory;
        private Action _returnBack;

        public DelegateCommand GoBack { get; }

        public ObservableCollection<Category> Categories { get; set; }

        public Category CurrentCategory
        {
            get
            {
                return _currentCategory;
            }
            set
            {
                _currentCategory = value;
                OnPropertyChanged();
                RemoveCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public string DisplayName
        {
            get
            {
                return _category.Name;
            }
        }

        public string Name
        {
            get
            {
                return _category.Name;
            }
            set
            {
                _category.Name = value;
                OnPropertyChanged();
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }

        public string Description
        {
            get
            {
                return _category.Description;
            }
            set
            {
                _category.Description = value;
                OnPropertyChanged();
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }
      /*  public Guid Guid
        {
            get
            {
                return _category.Guid;
            }
            set
            {
                _category.Guid = value;
                OnPropertyChanged();
                AddCategoryCommand.RaiseCanExecuteChanged();
            }
        }*/
        public async void CreateCategory()
        {
            var service = new UserService();
            try
            {
                var cat = new Category(_category.Name, _category.Description);
                AuthenticationService.CurrentUser.Categories.Add(cat);
                await service.SaveChangesCategories(AuthenticationService.CurrentUser);
            }
            catch (Exception ex)
            {
                return;
            }
            _returnBack.Invoke();
        }

        private async void RemoveCategory()
        {
            var service = new UserService();
            var tservice = new TransactionService();
            try
            {
                AuthenticationService.CurrentUser.Categories.Remove(_currentCategory);
                await service.SaveChangesCategories(AuthenticationService.CurrentUser);

                foreach (var wallet in AuthenticationService.CurrentUser.Wallets)
                {
                    foreach (var transaction in wallet.GetAllTransactions(AuthenticationService.CurrentUser))
                    {
                        if (Equals(transaction.Category, _currentCategory))
                        {
                            transaction.Category = Category.DefaultCategory;
                            await tservice.SaveUpdateTransaction(wallet, transaction);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return;
            }
            _returnBack.Invoke();
        }
        
        private bool correctInput()
        {
            return !String.IsNullOrWhiteSpace(Name) && !String.IsNullOrWhiteSpace(Description);
        }
        

        public WalletsNavigatableTypes Type
        {
            get
            {
                return WalletsNavigatableTypes.CategoryCreation;
            }
        }

        public DelegateCommand AddCategoryCommand { get; }

        public DelegateCommand RemoveCategoryCommand { get; }
        //for test
        public CategoryViewModel()
        {

            Categories = new ObservableCollection<Category>();

            foreach (var category in AuthenticationService.CurrentUser.Categories)
            {
                Categories.Add(category);
            }

            GoBack = new DelegateCommand(_returnBack);
            AddCategoryCommand = new DelegateCommand(CreateCategory, correctInput);
            RemoveCategoryCommand = new DelegateCommand(RemoveCategory, IsValidRemove);
        }
        public CategoryViewModel(Action goBack)
        {

            Categories = new ObservableCollection<Category>();

            foreach (var category in AuthenticationService.CurrentUser.Categories)
            {
                Categories.Add(category);
            }

            _returnBack = goBack;
            GoBack = new DelegateCommand(_returnBack);
            AddCategoryCommand = new DelegateCommand(CreateCategory, correctInput);
            RemoveCategoryCommand = new DelegateCommand(RemoveCategory, IsValidRemove);
        }
        

        private bool IsValidRemove()
        {
            return _currentCategory != null && !Equals(_currentCategory, Category.DefaultCategory);
        }


        public void ClearSensitiveData()
        {
        }

        public void Update()
        {
            Categories = new ObservableCollection<Category>();

            foreach (var category in AuthenticationService.CurrentUser.Categories)
            {
                Categories.Add(category);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}