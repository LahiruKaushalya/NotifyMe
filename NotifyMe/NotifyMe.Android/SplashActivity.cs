using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace NotifyMe.Droid
{
    [Activity(Label = "NotifyMe", Theme = "@style/Theme.Splash", MainLauncher = true, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
        }

        protected override void OnResume()
        {
            base.OnResume();
            Task startupWork = new Task(() => { StartApp(); });
            startupWork.Start();
        }
        
        async void StartApp()
        {
            await Task.Delay(100); 
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
        }
    }
}