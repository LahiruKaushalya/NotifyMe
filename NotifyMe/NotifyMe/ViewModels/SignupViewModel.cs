using System;
using System.Windows.Input;
using Xamarin.Forms;

using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;

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
                    if (Password != ConfirmPassword)
                    {
                        // DisplayAlert("Alert", "You have been alerted", "OK"); 
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
