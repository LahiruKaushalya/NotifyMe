using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{

    public class LocationsViewModel
    {
        private IUserService _userService;

        public LocationsViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
