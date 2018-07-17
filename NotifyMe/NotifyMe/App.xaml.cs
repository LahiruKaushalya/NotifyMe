using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CommonServiceLocator;

using NotifyMe.Views;
using NotifyMe.ServiceInterfaces;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NotifyMe
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            new DependencyInjector();
		}

		protected override void OnStart ()
		{
            var userService = ServiceLocator.Current.GetInstance<IUserService>();
            var currentUser = userService.GetCurrentUserFromDb();

            if (currentUser == null)
            {
                MainPage = new NavigationPage(new SignupPage());
            }
            else
            {
                userService.SetCurrentUser(currentUser);
                MainPage = new NavigationPage(new HomePage());
            }

		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
