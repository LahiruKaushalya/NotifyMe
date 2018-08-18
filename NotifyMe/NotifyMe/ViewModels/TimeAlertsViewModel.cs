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
            var displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetAllTimeAlerts(_userName));
            TimeAlerts = displayAlerts;

            Options = new List<string>()
            {
                "All",
                "Active",
                "Sent",
                "Disabled"
            };
        }

        private string _userName;

        private bool _isRefreshing;

        private string _selectedOption;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public string SelectedOption
        {
            get { return _selectedOption; }
            set
            {
                _selectedOption = value;
                OnPropertyChanged(nameof(SelectedOption));
            }
        }

        public List<string> Options { get; set; }

        public ICommand Refresh
        {
            get
            {
                return new Command(() => {
                    IsRefreshing = true;
                    TimeAlerts.Clear();

                    ObservableCollection<DisplayAlert> _displayAlerts;

                    switch (SelectedOption)
                    {
                        case "All":
                            _displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetAllTimeAlerts(_userName));
                            break;

                        case "Active":
                            _displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetActiveTimeAlerts(_userName));
                            break;

                        case "Sent":
                            _displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetSentTimeAlerts(_userName));
                            break;

                        case "Disabled":
                            _displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetDisabledTimeAlerts(_userName));
                            break;

                        default:
                            _displayAlerts = _converter.AlertToDisplayAlert(_alertService.GetActiveTimeAlerts(_userName));
                            break;
                    }
                    
                    foreach (DisplayAlert displayAlert in _displayAlerts)
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
                Refresh.Execute(null);
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
                Refresh.Execute(null);
            }
        }

        public void DeleteAlert(Alert alert)
        {
            _alertService.DeleteAlert(alert);
            DependencyService.Get<IToastService>().ShortMessage("Alert deleted");
            Refresh.Execute(null);
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
