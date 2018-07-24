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

        private async void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LocationsViewModel;
            var location = e.Item as Location;
            await PopupNavigation.PushAsync(new MapPopup(location));
        }

        public void DeleteLocation(Location location)
        {
            var vm = BindingContext as LocationsViewModel;
            vm.DeleteLocation(location);
        }
    }
}