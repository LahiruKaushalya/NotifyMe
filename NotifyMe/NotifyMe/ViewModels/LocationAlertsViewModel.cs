using System.Windows.Input;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Collections.Generic;

using Xamarin.Forms;
using NotifyMe.Models;

using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.ViewModels
{
    public class LocationAlertsViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private IAlertService _alertService;
        private ILocationService _locationService;
        private IConverter _converter;

        public ObservableCollection<DisplayAlert> LocationAlerts { get; set; }

        public LocationAlertsViewModel(IUserService userService, 
                                       IAlertService alertService,
                                       ILocationService locationService,
                                       IConverter converter)
        {
            _userService = userService;
            _alertService = alertService;
            _locationService = locationService;
            _converter = converter;
            
            _userName = _userService.GetCurrentUser().UserName;
            var displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetActiveUserLocationAlerts(_userName));
            LocationAlerts = displayAlerts;
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

                    ObservableCollection<DisplayAlert> displayAlerts;
                    if (ShowDisabled)
                    {
                        displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetAllUserLocationAlerts(_userName));
                    }
                    else
                    {
                        displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetActiveUserLocationAlerts(_userName));
                    }
                    foreach (DisplayAlert displayAlert in displayAlerts)
                    {
                        LocationAlerts.Add(displayAlert);
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
                alert.State = AlertState.Pending;
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
