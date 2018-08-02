using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyMe.Views.Popups
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeAlertDetailsPopup
	{
        private Alert _alert;

		public TimeAlertDetailsPopup (Alert alert)
		{
			InitializeComponent ();

            var _validatorService = CommonServiceLocator.ServiceLocator.Current.GetInstance<IValidatorService>();

            var isDayStillValid = _validatorService.ValidateDate(alert.Date);
            var isTimeStillValid = _validatorService.ValidateTime(alert.Time);

            if (!isDayStillValid || !isTimeStillValid)
            {
                ReactivateBtn.IsVisible = false;
                DisableBtn.IsVisible = false;
            }
            else
            {
                ReactivateBtn.IsVisible = alert.IsDisabled;
            }
            _alert = alert;

            AlertTitle.Text = alert.Title;
            Body.Text = alert.Description;
            DateTime.Text = alert.DisplayDateTime.ToString();
            CreatedOn.Text = alert.CreatedOn.ToString();
        }

        private async Task DisableAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Disable Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                new TimeAlertsPage().UpdateAlert(_alert, true);
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }

        private async Task ReactivateAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Reactivate Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                new TimeAlertsPage().UpdateAlert(_alert, false);
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }

        private async Task DeleteAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Delete Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                new TimeAlertsPage().DeleteAlert(_alert);
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }
    }
}