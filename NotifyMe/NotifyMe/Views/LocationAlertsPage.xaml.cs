using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;

using NotifyMe.ViewModels;
using NotifyMe.Models;
using NotifyMe.Models.DbModels;
using NotifyMe.Views.Popups;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationAlertsPage : ContentPage
	{
        private LocationAlertsViewModel vm;

		public LocationAlertsPage ()
		{
			InitializeComponent ();
            vm = BindingContext as LocationAlertsViewModel;
            Picker.SelectedIndex = 0;
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

        private void Picker_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            var picker = (Picker)sender;
            vm.SelectedOption = (string)picker.SelectedItem;
            vm.Refresh.Execute(null);
        }
    }
}