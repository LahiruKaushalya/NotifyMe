using System;
using System.ComponentModel;
using System.Windows.Input;

using Xamarin.Forms;
using Rg.Plugins.Popup.Services;

using NotifyMe.Views.Popups;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;

        private User _currentUser;

        public string Name { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }

        public AccountViewModel(IUserService userService)
        {
            _userService = userService;
            _currentUser = _userService.GetCurrentUser();
            Name = _currentUser.Name;
            Telephone = _currentUser.Telephone;
            Address = _currentUser.Address;
        }

        

        public ICommand UpdateAccountPic
        {
            get
            {
                return new Command(()=> { });
            }
        }

        public ICommand ChangePassword 
        {
            get
            {
                return new Command(()=> {
                    PopupNavigation.Instance.PushAsync(new PasswordPopup());
                });
            }
        }

        public ICommand UpdateAccount
        {
            get
            {
                return new Command(async () => {
                    if (Name.Equals(""))
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Name should be there.", "Ok");
                    }
                    else if (_currentUser.Name == Name &&  _currentUser.Address == Address && _currentUser.Telephone == Telephone)
                    {
                        return;
                    }
                    else
                    {
                        try
                        {
                            var user = _userService.GetUserByUserName(_currentUser.UserName);
                            if (user == null || user.Name == _currentUser.Name)
                            {
                                _currentUser.Name = Name;
                                _currentUser.Telephone = Telephone;
                                _currentUser.Address = Address;

                                _userService.UpdateUser(_currentUser);
                                _userService.SetCurrentUser(_currentUser);
                                await Application.Current.MainPage.DisplayAlert("Success", "Account successfully updated.", "Ok");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", "Email not available.", "Ok");
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

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
