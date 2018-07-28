using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

using Xamarin.Forms;

using NotifyMe.ServiceInterfaces;
using NotifyMe.Models.DbModels;
using NotifyMe.Models;
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
                        int id = 1;//Remember to cahange................................
                        try
                        {
                            var currentUser = _userService.GetCurrentUser();
                            var alert = new Alert()
                            {
                                Title = Title,
                                Description = Description,
                                User = currentUser.UserName,
                                Type = true, //True means Time alert
                                DateTime = Date + Time,
                                IsActive = true,
                                CreatedOn = DateTime.Now
                            };
                            // Add alert to database
                            //id =  _alertService.AddAlert(alert);

                            if (id != -1)
                            {
                                var notification = new Notification
                                {
                                    Id = id,
                                    Title = Title,
                                    Body = Description,
                                    Date = Date,
                                    Time = Time
                                };
                                //Platform specfic notification handle
                                var result = DependencyService.Get<INotificationSender>().ScheduleNotification(notification);
                                if (result != null)
                                {
                                    await Application.Current.MainPage.DisplayAlert("Success", "Alert added successfully.", "Ok");
                                }
                                else
                                {
                                    await RollbackDB(id);
                                }
                            }
                            else
                            {
                                await RollbackDB(id);
                            }
                        }
                        catch (Exception)
                        {
                            await RollbackDB(id);
                        }
                    }
                });
            }

        }

        private async Task RollbackDB(int id)
        {
            if (id != -1)
            {
                _alertService.DeleteAlertById(id);
            }
            await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong. Please try again", "Ok");
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
