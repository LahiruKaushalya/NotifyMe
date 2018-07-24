using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using NotifyMe.Models.DbModels;
using NotifyMe.ViewModels;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AddLocationAlertPage : ContentPage
	{
		public AddLocationAlertPage ()
		{
			InitializeComponent ();
		}

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var vm = BindingContext as AddLocationAlertViewModel;
            var picker = (Picker)sender;
            var location = picker.SelectedItem as Location;
            vm.Location = location;
        }
    }
}