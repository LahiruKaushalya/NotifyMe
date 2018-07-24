using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;

namespace NotifyMe.ViewModels
{
    public class LocationsViewModel : INotifyPropertyChanged
    {
        private IUserService _userService;
        private ILocationService _locationService;

        private string _userName;

        private ObservableCollection<Location> _locations;
        
        public ObservableCollection<Location> Locations
        {
            get { return _locations; }
            set
            {
                _locations = value;
                OnPropertyChanged();
            }
        }

        public LocationsViewModel(IUserService userService, ILocationService locationService)
        {
            _userService = userService;
            _locationService = locationService;

            _userName = _userService.GetCurrentUser().UserName;

            _locations = new ObservableCollection<Location>(_locationService.GetLocationsByUser(_userName));
        }

        public void AddOrHideAlert(Location location)
        {

        }

        public void DeleteLocation(Location location)
        {
            _locationService.DeleteLocationSoft(location);
            _locations = new ObservableCollection<Location>(_locationService.GetAllLocationsByUser(_userName));
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
