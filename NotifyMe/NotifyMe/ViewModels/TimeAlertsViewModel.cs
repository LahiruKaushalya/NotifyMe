using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace NotifyMe.ViewModels
{
    public class TimeAlertsViewModel
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

        public void AddOrHideAlert(Alert alert)
        {

        }
        
    }
}
