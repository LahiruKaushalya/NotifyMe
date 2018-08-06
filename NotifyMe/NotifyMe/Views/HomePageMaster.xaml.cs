using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePageMaster : ContentPage
    {
        public ListView ListView;

        public HomePageMaster()
        {
            InitializeComponent();
            ListView = MenuItemsListView;
        }

        
    }
}