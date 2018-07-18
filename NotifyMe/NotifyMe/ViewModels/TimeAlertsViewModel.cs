using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{
    public class TimeAlertsViewModel
    {
        private IUserService _userService;

        public TimeAlertsViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
