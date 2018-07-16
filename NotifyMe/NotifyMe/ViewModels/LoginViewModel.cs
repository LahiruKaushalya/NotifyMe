﻿using System;
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
                return new Command(async() => {
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
                                    _userService.SetCurrentUser(user);
                                    await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
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

        public ICommand Signup
        {
            get
            {
                return new Command(async() => {
                   await Application.Current.MainPage.Navigation.PushAsync(new SignupPage());
                });
            }
        }
    }

    public interface IPageDialogService
    {
    }
}
