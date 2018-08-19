using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AlertsPage : Xamarin.Forms.TabbedPage
    {
        public AlertsPage ()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetBarSelectedItemColor((Color)Xamarin.Forms.Application.Current.Resources["BaseColor"]);
        }

    }
}