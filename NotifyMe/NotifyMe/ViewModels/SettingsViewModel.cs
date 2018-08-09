using NotifyMe.Interfaces;

namespace NotifyMe.ViewModels
{

    public class SettingsViewModel
    {
        private IUserService _userService;

        public SettingsViewModel(IUserService userService)
        {
            _userService = userService;
        }
    }
}
