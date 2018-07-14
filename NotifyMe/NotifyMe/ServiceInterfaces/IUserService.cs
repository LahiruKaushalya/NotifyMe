using System.Collections.Generic;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IUserService
    {
        User GetUserByEmail(string Email);

        List<User> GetAllUsers();

        bool AddUser(User user);

    }
}
