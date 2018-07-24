using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using Xamarin.Forms;

namespace NotifyMe.ViewModels
{
    public class LocationsViewModel :  INotifyPropertyChanged
    {
        private IUserService _userService;
        private ILocationService _locationService;

        public LocationsViewModel(IUserService userService, ILocationService locationService)
        {
            _userService = userService;
            _locationService = locationService;

            _userName = _userService.GetCurrentUser().UserName;
            //
            Locations = new ObservableCollection<Location>(_locationService.GetLocationsByUser(_userName));
        }


        private string _userName;

        private bool _isRefreshing = false;

        private bool _showDeleted = false;

        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set
            {
                _isRefreshing = value;
                OnPropertyChanged(nameof(IsRefreshing));
            }
        }

        public bool ShowDeleted
        {
            get { return _showDeleted; }
            set
            {
                _showDeleted = value;
                OnPropertyChanged(nameof(ShowDeleted));
            }
        }

        public ObservableCollection<Location> Locations { get; set; }
        
        public ICommand Refresh
        {
            get
            {
                return new Command(() => {
                    IsRefreshing = true;
                    Locations.Clear();

                    List<Location> list;

                    if (ShowDeleted)
                    {
                        list = _locationService.GetAllLocationsByUser(_userName);
                    }
                    else
                    {
                        list = _locationService.GetLocationsByUser(_userName);
                    }
                    
                    foreach (Location location in list)
                    {
                        Locations.Add(location);
                    } 
                    IsRefreshing = false;
                });
            }
        }

        public void UpdateLocation(Location location, bool delete)
        {
            if (delete)
            {
                _locationService.DeleteLocationSoft(location);
            }
            else
            {
                _locationService.RestoreLocation(location);
            }
        }

        public void AddOrHideAlert(Location location)
        {

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
