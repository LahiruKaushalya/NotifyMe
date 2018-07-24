using System.Collections.Generic;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IUserService
    {
        User GetCurrentUser();

        void SetCurrentUser(User user);

        User GetUserByUserName(string userName);

        List<User> GetAllUsers();

        bool AddUser(User user);
        
        void UpdateUser(User user);

        User GetCurrentUserFromDb();

    }
}
