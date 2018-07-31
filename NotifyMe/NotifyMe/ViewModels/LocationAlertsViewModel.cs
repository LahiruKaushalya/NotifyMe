using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using System.Collections.Generic;
using NotifyMe.Models;

namespace NotifyMe.ViewModels
{
    public class LocationAlertsViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private IAlertService _alertService;
        private ILocationService _locationService;

        public ObservableCollection<Alert> LocationAlerts { get; set; }

        public LocationAlertsViewModel(IUserService userService, 
                                       IAlertService alertService,
                                       ILocationService locationService)
        {
            _userService = userService;
            _alertService = alertService;
            _locationService = locationService;

            var userEmail = _userService.GetCurrentUser().UserName;
            _userName = _userService.GetCurrentUser().UserName;
            LocationAlerts = new ObservableCollection<Alert>(_alertService.GetActiveUserLocationAlerts(userEmail));
        }

        private string _userName;

        private bool _isRefreshing;

        private bool _showDisabled;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool ShowDisabled
        {
            get { return _showDisabled; }
            set
            {
                _showDisabled = value;
                OnPropertyChanged(nameof(ShowDisabled));
            }
        }

        public ICommand Refresh
        {
            get
            {
                return new Command(() => {
                    IsRefreshing = true;
                    LocationAlerts.Clear();

                    List<Alert> list;

                    if (ShowDisabled)
                    {
                        list = _alertService.GetAllUserLocationAlerts(_userName);
                    }
                    else
                    {
                        list = _alertService.GetActiveUserLocationAlerts(_userName);
                    }

                    foreach (Alert alert in list)
                    {
                        LocationAlerts.Add(alert);
                    }
                    IsRefreshing = false;
                });
            }
        }

        public void UpdateAlert(Alert alert, bool disable)
        {
            if (disable)
            {
                _alertService.DisableAlert(alert);
                DependencyService.Get<INotificationService>().RemoveLocationNotification(alert);
                DependencyService.Get<IToastService>().ShortMessage("Alert disabled");
            }
            else
            {
                _alertService.ActivateAlert(alert);
                var location = _locationService.GetLocationById(alert.LocationID);
                var notification = new LocationNotification
                {
                    Id = alert.Id,
                    Title = alert.Title,
                    Body = alert.Description,
                    Position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude),
                    Radius = 2
                };
                DependencyService.Get<INotificationService>().ScheduleLocationNotification(notification);
                DependencyService.Get<IToastService>().ShortMessage("Alert reactivated");
            }
        }

        public void DeleteAlert(Alert alert)
        {
            _alertService.DeleteAlert(alert);
            DependencyService.Get<INotificationService>().RemoveLocationNotification(alert);
            DependencyService.Get<IToastService>().ShortMessage("Alert deleted");
        }

        public void AddOrHideAlert(Alert alert)
        {

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
