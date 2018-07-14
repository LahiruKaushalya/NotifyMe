using System;
using System.Windows.Input;
using Xamarin.Forms;

using NotifyMe.ServiceInterfaces;
using NotifyMe.Views;

namespace NotifyMe.ViewModels
{
    public class LoginViewModel
    {
        private IUserService _userService;

        public LoginViewModel(IUserService userService)
        {
            _userService = userService;
        }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(() => {
                    if (Email != null && Password != null)
                    {
                        try
                        {
                            var user = _userService.GetUserByEmail(Email);

                            if (user != null)
                            {
                                var password = user.Password;
                                if (password == Password)
                                {
                                    //Login success
                                }
                                else
                                {

                                }
                            }
                            else //invalid Email
                            {

                            }
                        }
                        catch (Exception e)
                        {
                            //DB Error
                        }
                    }
                    else //Incomplete inputs
                    {
                        
                    }
                });
            }
        }

        public ICommand Signup
        {
            get
            {
                return new Command(() => {
                    Application.Current.MainPage.Navigation.PushAsync(new SignupPage());
                });
            }
        }
    }
}
