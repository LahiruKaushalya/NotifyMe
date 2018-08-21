using NotifyMe.ViewModels;
using Plugin.Geolocator;
using Plugin.Geolocator.Abstractions;
using System;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddLocationPage : ContentPage
	{
        private MapCreationViewModel vm;

		public AddLocationPage ()
		{
			InitializeComponent ();
            vm = BindingContext as MapCreationViewModel;
        }

        protected override async void OnAppearing()
        { 
            vm.IsLoading = true; // Set up activity indicator
            await vm.GetCurrentLocation();
            vm.IsLoading = false; // Remove activity indicator and set up real views
        }
        
    }
}