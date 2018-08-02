using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using System.Collections.Generic;
using NotifyMe.Models;

namespace NotifyMe.ViewModels
{
    public class TimeAlertsViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private IAlertService _alertService;

        public ObservableCollection<Alert> TimeAlerts { get; set; }

        public TimeAlertsViewModel(IUserService userService, IAlertService alertService)
        {
            _userService = userService;
            _alertService = alertService;

            var userName = _userService.GetCurrentUser().UserName;
            _userName = _userService.GetCurrentUser().UserName;
            TimeAlerts = new ObservableCollection<Alert>(_alertService.GetActiveUserTimeAlerts(userName));
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

                    List<Alert> list;

                    if (ShowDisabled)
                    {
                        list = _alertService.GetAllUserTimeAlerts(_userName);
                    }
                    else
                    {
                        list = _alertService.GetActiveUserTimeAlerts(_userName);
                    }

                    foreach (Alert alert in list)
                    {
                        TimeAlerts.Add(alert);
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
