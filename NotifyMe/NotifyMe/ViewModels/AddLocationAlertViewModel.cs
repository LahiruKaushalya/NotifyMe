using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using NotifyMe.Models;
using Xamarin.Forms.Maps;

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
                return new Command(() => {
                    if (Title == string.Empty || Description == string.Empty || Location == null)
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Information incomplete");
                    }
                    else
                    {
                        int id = -1;
                        var alert = new Alert
                        {
                            Title = Title,
                            Description = Description,
                            LocationID = Location.Id,
                            LocationName = Location.Name,
                            User = _currentUser.UserName,
                            Type = false, // false means Location Alert
                            IsActive = true,
                            IsDeleted = false,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            id = _alertService.AddAlert(alert);
                            if (id != -1)
                            {
                                var locationNotification = new LocationNotification
                                {
                                    Id = id,
                                    Title = Title,
                                    Body = Description,
                                    Position = new Position(Location.Latitude, Location.Longitude),
                                    Radius = 10
                                };

                                // Send Native Location alerts
                                var result = DependencyService.Get<INotificationService>().ScheduleLocationNotification(locationNotification);

                                if (result != null)
                                {
                                    DependencyService.Get<IToastService>().ShortMessage("Alert added successfully");
                                }
                                else
                                {
                                    RollbackDB(id);
                                }
                            }
                            else
                            {
                                RollbackDB(id);
                            }
                        }
                        catch (Exception)
                        {
                            RollbackDB(id);
                        }
                    }
                });
            }
        }

        private void RollbackDB(int id)
        {
            if (id != -1)
            {
                _alertService.DeleteAlertById(id);
            }
            DependencyService.Get<IToastService>().LongMessage("Something went wrong. Please try again");
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
