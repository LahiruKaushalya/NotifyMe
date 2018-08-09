using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;
using static NotifyMe.Helpers.Enums;
using CommonServiceLocator;

namespace NotifyMe.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AlertDetailsPopup
    {
        private Alert _alert;
        private LocationAlertsPage _locationAlertsPage;
        private TimeAlertsPage _timeAlertsPage;

        public AlertDetailsPopup (int id)
		{
			InitializeComponent ();
            var alertService = ServiceLocator.Current.GetInstance<IAlertService>();
            _alert = alertService.GetAlertById(id); ;
            CreateContent();
		}

        private void CreateContent()
        {
            Label1.Text = "Title";
            DataLabel1.Text = _alert.Title;

            Label2.Text = "Discription";
            DataLabel2.Text = _alert.Description;

            Label4.Text = "Created On";
            DataLabel4.Text = _alert.CreatedOn.ToString();

            if (_alert.State == AlertState.Disabled)
            {
                AlertStateIcon.Source = "round_alarm_off_black_18.png";
            }
            else if (_alert.State == AlertState.Sent)
            {
                AlertStateIcon.Source = "round_check_circle_black_18.png";
            }
            else if(_alert.State == AlertState.Active ||
                    _alert.State == AlertState.Pending)
            {
                AlertStateIcon.Source = "round_check_circle_outline_black_18.png";
            }

            if (_alert.Type == AlertType.Time)//Time alert
            {
                _timeAlertsPage = new TimeAlertsPage();
                Label3.Text = "Date and Time";
                DataLabel3.Text = (_alert.Date + _alert.Time).ToString();

                var _validator = CommonServiceLocator.ServiceLocator.Current.GetInstance<IValidator>();

                var isDateTimeStillValid = _validator.ValidateDateTime(_alert.Date, _alert.Time);

                if (!isDateTimeStillValid)
                {
                    DeleteBtn.IsVisible = true;
                }
                else
                {
                    ReactivateBtn.IsVisible = (_alert.State == AlertState.Disabled);
                }
            }
            else if(_alert.Type == (int)AlertType.Location)//Location alert
            {
                _locationAlertsPage = new LocationAlertsPage();
                Label3.Text = "Location";
                DataLabel3.Text = _alert.LocationName;
                
                ReactivateBtn.IsVisible = _alert.State == AlertState.Disabled || _alert.State == AlertState.Sent;
            }
        }

        private async Task DisableAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Disable Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                if (_alert.Type == AlertType.Time)
                {
                    _timeAlertsPage.UpdateAlert(_alert, true);
                }
                else if(_alert.Type == AlertType.Location)
                {
                    _locationAlertsPage.UpdateAlert(_alert, true);
                }
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }

        private async Task ReactivateAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Reactivate Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                if (_alert.Type == AlertType.Time)
                {
                    _timeAlertsPage.UpdateAlert(_alert, false);
                }
                else if(_alert.Type == (int)AlertType.Location)
                {
                    _locationAlertsPage.UpdateAlert(_alert, false);
                }
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }

        private async Task DeleteAlert()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Delete Alert", "Are you sure?", "Yes", "No");

            if (responce)
            {
                if (_alert.Type == AlertType.Time)
                {
                    _timeAlertsPage.DeleteAlert(_alert);
                }
                else if (_alert.Type == (int)AlertType.Location)
                {
                    _locationAlertsPage.DeleteAlert(_alert);
                }
                await PopupNavigation.Instance.PopAsync();
            }
            else { return; }
        }
    }
}