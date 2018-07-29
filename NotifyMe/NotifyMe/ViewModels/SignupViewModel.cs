using System;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using NotifyMe.Views;
using NotifyMe.Views.Popups;

namespace NotifyMe.ViewModels
{
    public class SignupViewModel 
    {
        private IUserService _userService;

        public SignupViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Name { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get ; set ; }
        
        public ICommand Signup
        {
            get
            {
                return new Command(async() => {
                    if (Name == null || UserName == null || Password == null || ConfirmPassword == null)
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Information incomplete");
                    }
                    else if (Password != ConfirmPassword)
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Passwords mismatch");
                    }
                    else
                    {
                        var user = new User()
                        {
                            Name = Name,
                            UserName = UserName,
                            Password = Password,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            var isUser = _userService.GetUserByUserName(UserName);

                            if (isUser != null)
                            {
                                DependencyService.Get<IToastService>().ShortMessage("Username not available");
                            }
                            else
                            {
                                bool isok = _userService.AddUser(user);
                                if (isok)
                                {
                                    _userService.SetCurrentUser(user);
                                    DependencyService.Get<IToastService>().LongMessage("Welcome to NotifyMe");
                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                }
                                else
                                {
                                    //Adding failed
                                    DependencyService.Get<IToastService>().LongMessage("Signup failed");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            DependencyService.Get<IToastService>().LongMessage("Cannot connect to database");
                        }
                    }
                });
            }
        }

        public ICommand Login
        {
            get
            {
                return new Command(async() => {
                    await PopupNavigation.PushAsync(new LoginPopup());
                });
            }
        }
    }
}
