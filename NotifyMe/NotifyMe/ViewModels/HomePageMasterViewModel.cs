using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;

using NotifyMe.Models;
using NotifyMe.Views;
using NotifyMe.ServiceInterfaces;

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
                    new HomePageMenuItem { Id = 0, Title = "Page 1", Icon = "ic_action_person.png", TargetType = typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 1, Title = "Page 2", Icon = "ic_action_person.png", TargetType = typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 2, Title = "Page 3", Icon = "ic_action_person.png", TargetType = typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 3, Title = "Page 4", Icon = "ic_action_person.png", TargetType = typeof(HomePageDetail) },
                    new HomePageMenuItem { Id = 4, Title = "Logout", Icon = "ic_action_previous_item.png", TargetType = typeof(LoginPage) },
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
