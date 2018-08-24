using System.Collections.Generic;
using System.Collections.ObjectModel;

using NotifyMe.Interfaces;
using NotifyMe.Models;
using NotifyMe.Models.DbModels;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.Helpers
{
    public class Converter : IConverter
    {
        private IValidator _validator;
        private IAlertService _alertService;

        public Converter(IAlertService alertService, IValidator validator)
        {
            _alertService = alertService;
            _validator = validator;
        }

        public ObservableCollection<DisplayAlert> AlertToDisplayAlert(List<Alert> alerts)
        {
            var displayAlerts = new ObservableCollection<DisplayAlert>();

            foreach (Alert alert in alerts)
            {

                var isDisabled = false;
                var isSent = false;
                var isFailed = false;

                if (alert.Type == AlertType.Time && alert.State == AlertState.Active)
                {
                    var isDateTimeStillValid = _validator.ValidateDateTime(alert.Date, alert.Time);

                    if (!isDateTimeStillValid)
                    {
                        isFailed = true;
                        alert.State = AlertState.Failed;
                        _alertService.UpdateAlert(alert);
                    }
                }
                if (alert.State == AlertState.Failed)
                {
                    isFailed = true;
                }
                else if (alert.State == AlertState.Disabled)
                {
                    isDisabled = true;
                }
                else if (alert.State == AlertState.Active)
                {
                    isDisabled = false;
                }
                else if (alert.State == AlertState.Pending)
                {
                    isSent = false;
                }
                else if (alert.State == AlertState.Sent)
                {
                    isSent = true;
                }

                var displayAlert = new DisplayAlert
                {
                    Id = alert.Id,
                    Title = alert.Title,
                    Description = alert.Description,
                    LocationName = alert.LocationName,
                    DisplayDateTime = (alert.Date + alert.Time).ToString(),
                    IsDisabled = isDisabled,
                    IsSent = isSent,
                    IsFailed = isFailed
                };
                displayAlerts.Add(displayAlert);
            }
            return displayAlerts;
        }
    }
}
