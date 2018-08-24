using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CommonServiceLocator;

using NotifyMe.Models;
using NotifyMe.Interfaces;
using System.Threading.Tasks;

namespace NotifyMe.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class IndexPage : MasterDetailPage
    {
        private IUserService _userService;
        
        public IndexPage()
        {
            InitializeComponent();
            MasterPage.ListView.ItemSelected += ListView_ItemSelectedAsync;
            NavigationPage.SetHasNavigationBar(this, false);

            _userService = ServiceLocator.Current.GetInstance<IUserService>();
        }

        private async void ListView_ItemSelectedAsync(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as HomePageMenuItem;
            if (item == null)
                return;

            if (item.TargetType == typeof(SignupPage))
            {
                MasterPage.ListView.SelectedItem = null;
                var responce = await Application.Current.MainPage.DisplayAlert("Logout", "Are you sure?", "Yes", "No");
                if (responce)
                {
                    Application.Current.MainPage = new NavigationPage(new SignupPage());
                    var user = _userService.GetCurrentUser();
                    user.LoginState = false;
                    _userService.UpdateUser(user);
                }
                else
                {
                    return;
                }
            }
            else
            {
                var page = (Page)Activator.CreateInstance(item.TargetType);
                page.Title = item.Title;
                Detail = new NavigationPage(page);
                MasterPage.ListView.SelectedItem = null;

                await Task.Delay(5);
                IsPresented = false;
            } 
        }

        protected override bool OnBackButtonPressed()
        {
            return true;
        }
    }
}