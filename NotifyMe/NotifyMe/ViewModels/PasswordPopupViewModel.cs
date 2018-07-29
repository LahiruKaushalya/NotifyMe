using NotifyMe.ServiceInterfaces;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace NotifyMe.ViewModels
{
    public class PasswordPopupViewModel
    {
        private IUserService _userService;

        public string CurrentPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

        public PasswordPopupViewModel(IUserService userServise)
        {
            _userService = userServise;
        }

        public ICommand Update
        {
            get
            {
                return new Command(async ()=> {
                    if (CurrentPassword == null || CurrentPassword.Equals(""))
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Current password is required");
                    }
                    else if (NewPassword == null || NewPassword.Equals(""))
                    {
                        DependencyService.Get<IToastService>().ShortMessage("New password is required");
                    }
                    else if (ConfirmPassword == null || ConfirmPassword.Equals(""))
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Password confirmation is required");
                    }
                    else if (NewPassword != ConfirmPassword)
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Passwords mismatch");
                    }
                    else
                    {
                        try
                        {
                            var user = _userService.GetCurrentUser();
                            var currentPwd = _userService.GetUserByUserName(user.UserName).Password;

                            if (currentPwd == CurrentPassword)
                            {
                                user.Password = NewPassword;
                                _userService.UpdateUser(user);
                                DependencyService.Get<IToastService>().ShortMessage("Password successfully updated");
                                await PopupNavigation.PopAsync(true);
                            }
                            else
                            {
                                DependencyService.Get<IToastService>().ShortMessage("Current passwords is invalid");
                            }
                        }
                        catch (Exception)
                        {
                            DependencyService.Get<IToastService>().LongMessage("Cannot connect to database");
                        }
                    }
                });
            }
        }
    }
}
