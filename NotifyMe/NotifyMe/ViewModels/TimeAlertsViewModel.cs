using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using System.Collections.Generic;

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
            TimeAlerts = new ObservableCollection<Alert>(_alertService.GetUserTimeAlerts(userName));
        }

        private string _userName;

        private bool _isRefreshing;

        private bool _showDeleted;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;
                OnPropertyChanged(nameof(ShowDeleted));
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

                    if (ShowDeleted)
                    {
                        list = _alertService.GetAllUserTimeAlerts(_userName);
                    }
                    else
                    {
                        list = _alertService.GetUserTimeAlerts(_userName);
                    }

                    foreach (Alert alert in list)
                    {
                        TimeAlerts.Add(alert);
                    }
                    IsRefreshing = false;
                });
            }
        }

        public void UpdateAlert(Alert alert, bool delete)
        {
            if (delete)
            {
                _alertService.DeleteAlertSoft(alert);
                DependencyService.Get<IToastService>().ShortMessage("Alert deleted");
            }
            else
            {
                _alertService.RestoreAlert(alert);
                DependencyService.Get<IToastService>().ShortMessage("Alert restored");
            }
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
