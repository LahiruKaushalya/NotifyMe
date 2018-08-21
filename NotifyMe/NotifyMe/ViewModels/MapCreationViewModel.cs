using System;
using System.Windows.Input;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using NotifyMe.Interfaces;
using NotifyMe.CustomRenderers;
using NotifyMe.Models.DbModels;

namespace NotifyMe.ViewModels
{

    public class MapCreationViewModel : INotifyPropertyChanged
    {
        private bool _isLoading;
        
        public CustomMap Map { get; private set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public MapCreationViewModel()
        {
            var defPos = new Position(7.8731, 80.7718);// Set default focus to Sri Lanka
            var defRad = Distance.FromKilometers(400);

            Map = new CustomMap()
            {
                MapType = MapType.Street, 
                IsShowingUser = true
            };
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(defPos, defRad));
        }
        
        public async Task GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var location = await locator.GetPositionAsync(timeout: TimeSpan.FromMilliseconds(5000)); //Get current position from GPS
            
            var position = new Position(location.Latitude, location.Longitude);
            var radius = Distance.FromKilometers(1.5);
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, radius));

        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
