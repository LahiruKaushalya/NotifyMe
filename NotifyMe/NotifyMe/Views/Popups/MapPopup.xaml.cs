using System.Threading.Tasks;
using Rg.Plugins.Popup.Services;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

using NotifyMe.Models.DbModels;


namespace NotifyMe.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MapPopup 
	{
		public MapPopup (Location location)
		{
			InitializeComponent ();

            _location = location;
            RestoreBtn.IsVisible = location.IsDeleted;

            var position = new Position(location.Latitude, location.Longitude);
            Latitude.Text = location.Latitude.ToString();
            Longitude.Text = location.Longitude.ToString();

            MiniMap.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.2)));

            var pin = new Pin
            {
                Label = location.Name,
                Position = position
            };
            MiniMap.Pins.Add(pin);
        }

        private Location _location;
        
        private async Task DeleteLocation()
        {
            var responce = await Application.Current.MainPage.DisplayAlert("Delete Location", "Are you sure?", "Yes", "No");
            
            if (responce)
            {
                new LocationsPage().UpdateLocation(_location, true);
                await PopupNavigation.PopAsync();
            }
            else { return; }
        }

        private async Task RestoreLocation()
        { 
            var responce = await Application.Current.MainPage.DisplayAlert("Restore Location", "Are you sure?", "Yes", "No");
            
            if (responce)
            {
                new LocationsPage().UpdateLocation(_location, false);
                await PopupNavigation.PopAsync();
            }
            else { return; }
        }

    }
}