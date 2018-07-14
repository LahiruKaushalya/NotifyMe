using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using NotifyMe.Views;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace NotifyMe
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();
            new DependencyInjector();

			MainPage = new NavigationPage(new LoginPage());
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
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
