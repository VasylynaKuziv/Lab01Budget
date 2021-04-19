using Budgets.Models.Users;
using DataStorage;
using Lab01.Entities;
using System.Threading.Tasks;

namespace Budgets.GUI.WPF.Service
{
    public class UserService
    {
        private FileDataStorage<DBUser> _storage = new FileDataStorage<DBUser>();

        public async Task<bool> SaveChangesCategories(User user)
        {
            DBUser userDb = await _storage.GetAsync(user.Guid);
            userDb.Categories = user.Categories;
            await _storage.AddOrUpdateAsync(userDb);
            return true;
        }
        
    }
}

