using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{

    public class AddLocationAlertViewModel : INotifyPropertyChanged
    {
        public AddLocationAlertViewModel(IUserService userService, 
                                         ILocationService locationService,
                                         IAlertService alertService)
        {
            _userService = userService;
            _locationService = locationService;
            _alertService = alertService;
            _currentUser = _userService.GetCurrentUser();

            Locations  = _locationService.GetLocationsByUser(_currentUser.UserName);

            Title = string.Empty;
            Description = string.Empty;
        }

        private IUserService _userService;

        private ILocationService _locationService;

        private IAlertService _alertService;

        private User _currentUser;

        public List<Location> Locations { get; set; }

        public Location Location { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public ICommand AddAlert
        {
            get
            {
                return new Command(async () => {
                    if (Title == string.Empty || Description == string.Empty || Location == null)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Information incomplete.", "Ok");
                    }
                    else
                    {
                        var alert = new Alert
                        {
                            Title = Title,
                            Description = Description,
                            LocationID = Location.Id,
                            LocationName = Location.Name,
                            User = _currentUser.UserName,
                            Type = false, // false means Location Alert
                            IsActive = true,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            var id = _alertService.AddAlert(alert);
                            if (id != -1)
                            {
                                // Need to find a way to send Location alerts
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
