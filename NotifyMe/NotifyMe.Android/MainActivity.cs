using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;

using Java.Util;
using Xamarin.Forms;

using Rg.Plugins.Popup.Services;

using NotifyMe.Droid;
using NotifyMe.ServiceInterfaces;
using NotifyMe.Models;

[assembly: Dependency(typeof(MainActivity))]

namespace NotifyMe.Droid
{
    [Activity(Label = "NotifyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity, INotificationSender
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());

        }
        
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                PopupNavigation.PopAsync(true);
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }

        public Models.Notification ScheduleNotification(Models.Notification notification)
        {
            try
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));

                intent.PutExtra("ID", notification.Id);
                intent.PutExtra("Title", notification.Title);
                intent.PutExtra("Body", notification.Body);

                PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                     notification.Id,
                                                                     intent,
                                                                     PendingIntentFlags.UpdateCurrent);

                AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(AlarmService);

                
                var date = notification.Date;
                var time = notification.Time;
                
                Calendar calendar = Calendar.Instance;
                calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                calendar.Set(CalendarField.Year, date.Year);
                calendar.Set(CalendarField.Month, date.Month - 1);
                calendar.Set(CalendarField.DayOfMonth, date.Day);
                calendar.Set(CalendarField.HourOfDay, time.Hours);
                calendar.Set(CalendarField.Minute, time.Minutes);
                calendar.Set(CalendarField.Second, 0);

                alarmManager.Set(AlarmType.RtcWakeup, calendar.TimeInMillis, broadcast);

                return notification;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
    }
}

