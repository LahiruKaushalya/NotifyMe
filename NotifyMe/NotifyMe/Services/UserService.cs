using System.Linq;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;

using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;

namespace NotifyMe.Services
{
    public class UserService : IUserService
    {
        private SQLiteConnection _dbContext;

        private User _currentUser;
        
        public UserService()
        {
            _dbContext = DependencyService.Get<ISqliteConnection>().GetConnection();
            _dbContext.CreateTable<User>();
        }

        public bool AddUser(User user)
        {
            var rowId =  _dbContext.Insert(user);
            return rowId != -1;
        }

        public List<User> GetAllUsers()
        {
            var users = (from user in _dbContext.Table<User>() select user).ToList();
            return users;
        }

        public User GetCurrentUser()
        {
            return _currentUser;
        }

        public User GetCurrentUserFromDb()
        {
            var user = _dbContext.Table<User>().FirstOrDefault(u => u.LoginState == true);
            return user;
        }

        public User GetUserByUserName(string userName)
        {
            var user = _dbContext.Table<User>().FirstOrDefault(u => u.UserName == userName);
            return user;
        }

        public void SetCurrentUser(User user)
        {
            _currentUser = user;
        }
        
        public void UpdateUser(User user)
        {
            _dbContext.InsertOrReplace(user);
        }
    }
}
