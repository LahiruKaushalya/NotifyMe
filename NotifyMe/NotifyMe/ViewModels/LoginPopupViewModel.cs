using System;
using System.Windows.Input;
using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

using NotifyMe.ServiceInterfaces;
using NotifyMe.Views;

namespace NotifyMe.ViewModels
{
    public class LoginPopupViewModel
    {
        private IUserService _userService;

        public LoginPopupViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(async() => {
                    if (Email != null && Password != null)
                    {
                        try
                        {
                            var user = _userService.GetUserByEmail(Email);
                            if (user != null)
                            {
                                var password = user.Password;
                                if (password == Password)//Login success
                                {
                                    user.LoginState = true;
                                    _userService.SetCurrentUser(user);
                                    _userService.UpdateUser(user);
                                    
                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                    await PopupNavigation.PopAsync(true);
                                }
                                else
                                {
                                    await Application.Current.MainPage.DisplayAlert("Alert", "Invalid password", "Ok");
                                }
                            }
                            else //invalid Email
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", "Invalid username", "Ok");
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            await Application.Current.MainPage.DisplayAlert("Oops", "Can't connect to database.", "Ok");
                        }
                    }
                    else //Incomplete inputs
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Information required to login is incomplete.", "Ok");
                    }
                });
            }
        }
    }
}
