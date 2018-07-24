using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Xamarin.Forms;
using Plugin.LocalNotifications;

using NotifyMe.ServiceInterfaces;
using NotifyMe.Models.DbModels;


namespace NotifyMe.ViewModels
{
    public class AddTimeAlertViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private IAlertService _alertService;

        public AddTimeAlertViewModel(IUserService userService , IAlertService alertService)
        {
            _userService = userService;
            _alertService = alertService;
            Date = DateTime.Now.Date;
            Title = string.Empty;
            Description = string.Empty;
        }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public ICommand AddAlert
        {
            get
            {
                return new Command(async () => {
                    if (Title.Equals(string.Empty) || Description.Equals(string.Empty))
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Information that need create alert is incomplete.", "Ok");
                    }
                    else
                    {
                        try
                        {
                            var currentUser = _userService.GetCurrentUser();
                            var alert = new Alert()
                            {
                                Title = Title,
                                Description = Description,
                                User = currentUser.UserName,
                                Type = false, //false means Time alert
                                DateTime = Date + Time
                            };
                            var id =  _alertService.AddAlert(alert);
                            if (id != -1)
                            {
                                CrossLocalNotifications.Current.Show(Title, Description, id, Date + Time);
                                await Application.Current.MainPage.DisplayAlert("Success", "Alert added successfully.", "Ok");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong. Please try again", "Ok");
                            }
                        }
                        catch (Exception)
                        {
                            await Application.Current.MainPage.DisplayAlert("Oops", "Can't connect to database.", "Ok");
                        }
                        
                    }
                });
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
