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

        public string UserName { get; set; }

        public string Password { get; set; }

        public ICommand Login
        {
            get
            {
                return new Command(async() => {
                    if (UserName != null && Password != null)
                    {
                        try
                        {
                            var user = _userService.GetUserByUserName(UserName);
                            if (user != null)
                            {
                                var password = user.Password;
                                if (password == Password)//Login success
                                {
                                    user.LoginState = true;
                                    _userService.SetCurrentUser(user);
                                    _userService.UpdateUser(user);

                                    DependencyService.Get<IToastService>().ShortMessage("Login successfull");
                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                                    await PopupNavigation.Instance.PopAsync(true);
                                }
                                else
                                {
                                    DependencyService.Get<IToastService>().ShortMessage("Invalid password");
                                }
                            }
                            else //invalid password
                            {
                                DependencyService.Get<IToastService>().ShortMessage("Invalid password");
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            DependencyService.Get<IToastService>().LongMessage("Cannot connect to database");
                        }
                    }
                    else //Incomplete inputs
                    {
                        DependencyService.Get<IToastService>().LongMessage("Information  incomplete.");
                    }
                });
            }
        }
    }
}
