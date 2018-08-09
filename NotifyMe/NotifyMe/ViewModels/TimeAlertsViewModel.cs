using System.ComponentModel;
using System.Windows.Input;
using System.Runtime.CompilerServices;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using Xamarin.Forms;
using NotifyMe.Models;
using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.ViewModels
{
    public class TimeAlertsViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private IAlertService _alertService;
        private IConverter _converter;

        public ObservableCollection<DisplayAlert> TimeAlerts { get; set; }

        public TimeAlertsViewModel(IUserService userService, 
                                   IAlertService alertService,
                                   IConverter converter)
        {
            _userService = userService;
            _alertService = alertService;
            _converter = converter;

            _userName = _userService.GetCurrentUser().UserName;
            var displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetAllUserTimeAlerts(_userName));
            
            TimeAlerts = new ObservableCollection<DisplayAlert>(displayAlerts);
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
                    TimeAlerts.Clear();

                    ObservableCollection<DisplayAlert> displayAlerts;
                    if (ShowDisabled)
                    {
                        displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetAllUserTimeAlerts(_userName));
                    }
                    else
                    {
                        displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetActiveUserTimeAlerts(_userName));
                    }
                    foreach (DisplayAlert displayAlert in displayAlerts)
                    {
                        TimeAlerts.Add(displayAlert);
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
                DependencyService.Get<INotificationService>().RemoveTimeNotification(alert);
                DependencyService.Get<IToastService>().ShortMessage("Alert disabled");
            }
            else
            {
                alert.State = AlertState.Pending;
                _alertService.ActivateAlert(alert);
                var notification = new TimeNotification
                {
                    Id = alert.Id,
                    Body = alert.Description,
                    Title = alert.Title,
                    Date = alert.Date,
                    Time = alert.Time
                };
                DependencyService.Get<INotificationService>().ScheduleTimeNotification(notification);
                DependencyService.Get<IToastService>().ShortMessage("Alert reactivated");
            }
        }

        public void DeleteAlert(Alert alert)
        {
            _alertService.DeleteAlert(alert);
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
