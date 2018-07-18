using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{

    public class AddLocationViewModel
    {
        private IUserService _userService;

        public AddLocationViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
