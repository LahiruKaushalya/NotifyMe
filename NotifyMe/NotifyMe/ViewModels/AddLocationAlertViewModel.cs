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
                        DependencyService.Get<IToastService>().ShortMessage("Information incomplete");
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
                                DependencyService.Get<IToastService>().ShortMessage("Alert added successfully");
                            }
                            else
                            {
                                DependencyService.Get<IToastService>().LongMessage("Something went wrong. Please try again");
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
