using System;
using System.Windows.Input;
using Xamarin.Forms;

using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using NotifyMe.Views;

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

        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get ; set ; }
        
        public ICommand Signup
        {
            get
            {
                return new Command(async() => {
                    if (Name == null || Email == null || Password == null || ConfirmPassword == null)
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
                            Email = Email,
                            Password = Password,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            bool isok = _userService.AddUser(user);
                            if (isok)
                            {
                                await Application.Current.MainPage.Navigation.PushAsync(new HomePage());
                            }
                            else
                            {
                                //Adding failed
                                await Application.Current.MainPage.DisplayAlert("Oops", "Signup failed. Try again later", "Ok");
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

        //public event PropertyChangedEventHandler PropertyChanged;

        //private void OnPropertyChanged(string propertyName)
        //{
        //    PropertyChanged?.Invoke(this , new PropertyChangedEventArgs(propertyName));
        //}
    }
}
