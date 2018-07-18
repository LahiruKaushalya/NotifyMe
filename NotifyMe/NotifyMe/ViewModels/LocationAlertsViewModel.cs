using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{
    public class LocationAlertsViewModel
    {
        private IUserService _userService;

        public LocationAlertsViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
