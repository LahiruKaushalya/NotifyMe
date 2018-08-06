using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAlertPage : Xamarin.Forms.TabbedPage
    {
        public AddAlertPage ()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.Android>().SetToolbarPlacement(ToolbarPlacement.Bottom);
            
        }
    }
}