using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using NotifyMe.Models;
using NotifyMe.Views;
using NotifyMe.Interfaces;

namespace NotifyMe.ViewModels
{
    public class HomePageMasterViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<HomePageMenuItem> MenuItems { get; set; }

        public string UserName { get; set;}

        private IUserService _userService;

        public HomePageMasterViewModel(IUserService userService)
        {
            _userService = userService;
            UserName = _userService.GetCurrentUser().Name;

            MenuItems = new ObservableCollection<HomePageMenuItem>(new[]
            {
                    new HomePageMenuItem
                    {
                        Id = 0,
                        Title = "Alerts",
                        Icon = "round_notifications_black_24.png",
                        TargetType = typeof(AlertsPage)
                    },

                    new HomePageMenuItem
                    {
                        Id = 1,
                        Title = "Add Alert",
                        Icon = "round_add_alert_black_24.png",
                        TargetType = typeof(AddAlertPage)
                    },

                    new HomePageMenuItem
                    {
                        Id = 2,
                        Title = "Locations",
                        Icon = "round_place_black_24.png",
                        TargetType = typeof(LocationsPage)
                    },

                    new HomePageMenuItem
                    {
                        Id = 3,
                        Title = "Add Lacation",
                        Icon = "round_add_location_black_24.png",
                        TargetType = typeof(AddLocationPage)
                    },

                    new HomePageMenuItem
                    {
                        Id = 4,
                        Title = "Account",
                        Icon = "round_account_circle_black_24.png",
                        TargetType = typeof(AccountPage)
                    },

                    new HomePageMenuItem
                    {
                        Id = 5,
                        Title = "Settings",
                        Icon = "round_settings_black_24.png",
                        TargetType = typeof(SettingsPage)
                    },

                    new HomePageMenuItem {
                        Id = 6,
                        Title = "Logout",
                        Icon = "round_exit_to_app_black_24.png",
                        TargetType = typeof(SignupPage)
                    },
            });
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
