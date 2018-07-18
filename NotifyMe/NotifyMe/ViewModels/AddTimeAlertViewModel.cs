using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{

    public class AddTimeAlertViewModel
    {
        private IUserService _userService;

        public AddTimeAlertViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
