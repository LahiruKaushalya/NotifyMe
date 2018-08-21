using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPageMaster : ContentPage
    {
        public ListView ListView;

        public IndexPageMaster()
        {
            InitializeComponent();
            ListView = MenuItemsListView;
        }

        
    }
}