using System;
using System.Windows.Input;
using Xamarin.Forms;
using Plugin.Geolocator.Abstractions;

using NotifyMe.Interfaces;
using NotifyMe.Models.DbModels;

namespace NotifyMe.ViewModels
{
    public class AddLocationViewModel
    {
        private IUserService _userService;
        private ILocationService _locationService;

        public Position TapPosition { get; set; }

        public string LocationName { get; set; }

        public AddLocationViewModel(IUserService userService, ILocationService locationService)
        {
            _userService = userService;
            _locationService = locationService;
        }

        public ICommand AddLocation {
            get
            {
                return new Command(() => {

                    if (LocationName == string.Empty)
                    {
                        DependencyService.Get<IToastService>().ShortMessage("Add location name");
                    }
                    else
                    {
                        var location = new Location
                        {
                            Name = LocationName,
                            User = _userService.GetCurrentUser().UserName,
                            Latitude = TapPosition.Latitude,
                            Longitude = TapPosition.Longitude,
                            IsDeleted = false,
                            CreatedOn = DateTime.Now
                        };

                        try
                        {
                            var id = _locationService.AddLocation(location);
                            if (id != -1)
                            {
                                DependencyService.Get<IToastService>().ShortMessage("Location added successfully");
                            }
                            else
                            {
                                DependencyService.Get<IToastService>().LongMessage("Something went wrong. Please try again");
                            }
                        }
                        catch (Exception)
                        {
                            //DB Error
                            DependencyService.Get<IToastService>().LongMessage("Cannot connect to database");
                        }
                    }
                });
            }
        }
    }
}
