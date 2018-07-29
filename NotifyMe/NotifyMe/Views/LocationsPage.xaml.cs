using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotifyMe.Models.DbModels;
using NotifyMe.ViewModels;
using NotifyMe.Views.Popups;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;

namespace NotifyMe.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationsPage : ContentPage
	{
		public LocationsPage ()
		{
			InitializeComponent ();
		}

        private async Task ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LocationsViewModel;
            var location = e.Item as Location;
            await PopupNavigation.Instance.PushAsync(new MapPopup(location));
        }

        public void UpdateLocation(Location location, bool delete)
        {
            var vm = BindingContext as LocationsViewModel;
            vm.UpdateLocation(location, delete);
            vm.Refresh.Execute(null);
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var vm = BindingContext as LocationsViewModel;
            vm.ShowDeleted = e.Value;
            vm.Refresh.Execute(null);
        }
    }
}