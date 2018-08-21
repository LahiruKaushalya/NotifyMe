using Xamarin.Forms;
using Xamarin.Forms.Maps;
using NotifyMe.Interfaces;
using NotifyMe.Views.Popups;
using Rg.Plugins.Popup.Services;

namespace NotifyMe.CustomRenderers
{
    public class CustomMap : Map
    {
        private Position _tapPosition;

        private bool _isConnected;
        
        public void OnTap(Position position)
        {
            _tapPosition = position;
        }

        public void IsNetworkConnected(bool isConnected)
        {
            _isConnected = isConnected;
            GetAddressAsync();
        }

        private async void GetAddressAsync()
        {
            if (_isConnected)
            {
                var pos = new Plugin.Geolocator.Abstractions.Position(_tapPosition.Latitude, _tapPosition.Longitude);
                await PopupNavigation.Instance.PushAsync(new LocationAddressesPopup(pos));
            }
            else
            {
                DependencyService.Get<IToastService>().ShortMessage("No internet connection");
            }
        }
    }
}
