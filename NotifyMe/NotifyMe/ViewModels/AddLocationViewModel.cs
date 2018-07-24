using System;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

using Plugin.Geolocator;
using Xamarin.Forms.Maps;
using NotifyMe.ServiceInterfaces;
using Plugin.Geolocator.Abstractions;
using System.Windows.Input;
using Xamarin.Forms;
using NotifyMe.CustomRenderers;
using NotifyMe.Models.DbModels;

namespace NotifyMe.ViewModels
{

    public class AddLocationViewModel : INotifyPropertyChanged
    {

        private IUserService _userService;
        private ILocationService _locationService;

        private bool _isLoading;

        public CustomMap Map { get; private set; }

        public string LocationName { get; set; }

        public bool IsLoading
        {
            get { return _isLoading; }
            set {
                _isLoading = value;
                OnPropertyChanged();
            }
        }


        public AddLocationViewModel(IUserService userService, ILocationService locationService)
        {
            _userService = userService;
            _locationService = locationService;

            var defPos = new Xamarin.Forms.Maps.Position(7.8731, 80.7718);// Set default focus to Sri Lanka
            var defRad = Distance.FromKilometers(400);

            Map = new CustomMap()
            {
                MapType = MapType.Street, 
                IsShowingUser = true
            };
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(defPos, defRad));

            LocationName = string.Empty;
        }
        
        public async Task GetCurrentLocation()
        {
            var locator = CrossGeolocator.Current;
            locator.DesiredAccuracy = 20;

            var location = await locator.GetPositionAsync(timeout: TimeSpan.FromMilliseconds(5000)); //Get current position from GPS
            
            var position = new Xamarin.Forms.Maps.Position(location.Latitude, location.Longitude);
            var radius = Distance.FromKilometers(1.5);
            Map.TapPosition = position;
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, radius));

        }

        public ICommand AddLocation
        {
            get
            {
                return new Command(async () => {
                    if (LocationName == string.Empty)
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Add location name.", "Ok");
                    }
                    else
                    {
                        var location = new Location
                        {
                            Name = LocationName,
                            User = _userService.GetCurrentUser().UserName,
                            Latitude = Map.TapPosition.Latitude,
                            Longitude = Map.TapPosition.Longitude,
                            IsDeleted = false,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            var id = _locationService.AddLocation(location);
                            if (id != -1)
                            {
                                await Application.Current.MainPage.DisplayAlert("Success", "Location added successfully.", "Ok");
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Oops", "Something went wrong. Please try again", "Ok");
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            await Application.Current.MainPage.DisplayAlert("Oops", "Can't connect to database.", "Ok");
                        }
                    }
                });
            }
        }

        #region INotifyPropertyChanged Implementation
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            if (PropertyChanged == null)
                return;

            PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
