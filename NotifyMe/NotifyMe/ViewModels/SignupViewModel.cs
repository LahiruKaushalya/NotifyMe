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
                        await Application.Current.MainPage.DisplayAlert("Alert", "Information required to signup is incomplete.", "Ok");
                    }
                    else if (Password != ConfirmPassword)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Passwords are mismatch.", "Ok");
                    }
                    else
                    {
                        var user = new User(){
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
                                await Application.Current.MainPage.DisplayAlert("Oops", "Email not avilable. Try again another", "Ok");
                            }
                            else
                            {
                                bool isok = _userService.AddUser(user);
                                if (isok)
                                {
                                    _userService.SetCurrentUser(user);
                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                }
                                else
                                {
                                    //Adding failed
                                    await Application.Current.MainPage.DisplayAlert("Oops", "Signup failed. Try again later", "Ok");
                                }
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            await Application.Current.MainPage.DisplayAlert("Oops", "Can't connect to database.", "Ok");
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
