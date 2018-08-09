using System.Threading.Tasks;

using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotifyMe.ViewModels;
using NotifyMe.Models;
using NotifyMe.Views.Popups;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeAlertsPage : ContentPage
	{
        private TimeAlertsViewModel vm;

		public TimeAlertsPage ()
		{
			InitializeComponent ();
            vm = BindingContext as TimeAlertsViewModel;
        }

        private async Task ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var alert = e.Item as DisplayAlert;
            await PopupNavigation.Instance.PushAsync(new AlertDetailsPopup(alert.Id));
        }

        public void UpdateAlert(Alert alert, bool disable)
        {
            vm.UpdateAlert(alert, disable);
            vm.Refresh.Execute(null);
        }

        public void DeleteAlert(Alert alert)
        {
            vm.DeleteAlert(alert);
            vm.Refresh.Execute(null);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var vm = BindingContext as TimeAlertsViewModel;
            vm.ShowDisabled = e.Value;
            vm.Refresh.Execute(null);
        }
    }
}