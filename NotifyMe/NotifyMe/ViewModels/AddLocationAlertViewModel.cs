using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{

    public class AddLocationAlertViewModel
    {
        private IUserService _userService;

        public AddLocationAlertViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
