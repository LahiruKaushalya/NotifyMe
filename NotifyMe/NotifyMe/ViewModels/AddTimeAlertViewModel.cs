using System.Windows.Input;
using Xamarin.Forms;

using NotifyMe.ServiceInterfaces;
using System;
using Plugin.LocalNotifications;

namespace NotifyMe.ViewModels
{

    public class AddTimeAlertViewModel
    {
        private IUserService _userService;

        public AddTimeAlertViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public ICommand AddAlert
        {
            get
            {
                return new Command(() => {
                    CrossLocalNotifications.Current.Show("title", "body", 101, DateTime.Now.AddSeconds(5));
                });
            }
        }
    }
}
