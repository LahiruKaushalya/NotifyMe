using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotifyMe.ViewModels;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LocationAlertsPage : ContentPage
	{
		public LocationAlertsPage ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as LocationAlertsViewModel;
            var alert = e.Item as Alert;
        }
    }
}