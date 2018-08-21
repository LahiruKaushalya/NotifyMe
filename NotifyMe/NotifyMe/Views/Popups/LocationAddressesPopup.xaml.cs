using NotifyMe.Interfaces;
using NotifyMe.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyMe.Views.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationAddressesPopup
    {
        public ObservableCollection<Address> Locations { get; set; }

        private AddLocationViewModel vm;

        private Position _tapPosition;

        public LocationAddressesPopup (Position tapPosition)
		{
            InitializeComponent ();
            _tapPosition = tapPosition;
            vm = BindingContext as AddLocationViewModel;
            vm.TapPosition = _tapPosition;
        }

        protected override async void OnAppearing()
        {
            try
            {
                ActivityIndicator.IsRunning = true;
                var addresses = await CrossGeolocator.Current.GetAddressesForPositionAsync(_tapPosition);
                if (addresses == null)
                {
                    CreateAddressListView();
                    DependencyService.Get<IToastService>().LongMessage("No address found for this location");
                }
                else
                {
                   CreateAddressListView(addresses);
                }
                ActivityIndicator.IsRunning = false;
            }
            catch (Exception)
            {
                CreateAddressListView();
                DependencyService.Get<IToastService>().LongMessage("Unable to find adresses. Check internet connection.");
            }
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var address = e.Item as Address;
            LocationNameEntry.Text = address.FeatureName;
        }

        private void CreateAddressListView(IEnumerable<Address> addresses)
        {
            Locations = new ObservableCollection<Address>();
            foreach (Address address in addresses)
            {
                Locations.Add(address);
            }
            ListView.ItemsSource = Locations;
            LocationNameEntry.Text = string.Empty;
        }

        private void CreateAddressListView()
        {
            LocationNameEntry.Text = string.Empty;
        }
    }
}