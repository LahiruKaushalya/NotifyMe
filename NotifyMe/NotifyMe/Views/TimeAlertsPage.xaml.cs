using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotifyMe.ViewModels;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TimeAlertsPage : ContentPage
	{
		public TimeAlertsPage ()
		{
			InitializeComponent ();
		}

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            var vm = BindingContext as TimeAlertsViewModel;
            var alert = e.Item as Alert;
        }

        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {

        }
    }
}