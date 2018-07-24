using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;

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

            var userEmail = _userService.GetCurrentUser().UserName;
            
            TimeAlerts = new ObservableCollection<Alert>(_alertService.GetAllUserTimeAlerts(userEmail));
        }

        private bool _isRefreshing;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public ICommand Refresh
        {
            get
            {
                return new Command(() => {
                    IsRefreshing = true;

                    IsRefreshing = false;
                });
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
