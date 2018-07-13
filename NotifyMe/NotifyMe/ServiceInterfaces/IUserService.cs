using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models;

namespace NotifyMe.ServiceInterfaces
{
    public interface IUserService
    {
        User GetUserByEmailAsync(string Email);

        IEnumerable<User> GetAllUsersAsync();

        bool AddUserAsync(User user);

    }
}
