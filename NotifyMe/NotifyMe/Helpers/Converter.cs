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
        public ObservableCollection<DisplayAlert> AlertToDisplayAlert(List<Alert> alerts)
        {
            var displayAlerts = new ObservableCollection<DisplayAlert>();

            foreach (Alert alert in alerts)
            {

                var isDisabled = false;
                var isSent = false;

                if (alert.State == AlertState.Disabled)
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
                    IsSent = isSent
                };
                displayAlerts.Add(displayAlert);
            }
            return displayAlerts;
        }
    }
}
