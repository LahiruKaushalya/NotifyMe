using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace NotifyMe.ViewModels
{
    public class LocationAlertsViewModel
    {
        private IUserService _userService;
        private IAlertService _alertService;

        public ObservableCollection<Alert> LocationAlerts { get; set; }

        public LocationAlertsViewModel(IUserService userService, IAlertService alertService)
        {
            _userService = userService;
            _alertService = alertService;

            var userEmail = _userService.GetCurrentUser().UserName;

            LocationAlerts = new ObservableCollection<Alert>(_alertService.GetAllUserLocationAlerts(userEmail));
        }

        public void AddOrHideAlert(Alert alert)
        {

        }

    }
}
