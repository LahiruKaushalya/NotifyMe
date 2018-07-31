using System;
using System.Collections.Generic;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Support.V4.Content;

using Java.Util;
using Xamarin.Forms;

using Rg.Plugins.Popup.Services;

using NotifyMe.Droid;
using NotifyMe.ServiceInterfaces;
using System.Threading.Tasks;
using NotifyMe.Models;
using Android.Locations;
using NotifyMe.Models.DbModels;

[assembly: Dependency(typeof(MainActivity))]

namespace NotifyMe.Droid
{
    [Activity(Label = "NotifyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity, INotificationService
    {
        private List<string> _permissions = new List<string>();

        protected async override void OnCreate(Bundle bundle)
        {
            await GetPermissions();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            
            base.OnCreate(bundle);

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);

            LoadApplication(new App());
        }

        #region Location Notification

        #region Delete
        public Alert RemoveLocationNotification(Alert alert)
        {
            try
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));
                PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                     alert.Id,
                                                                     intent,
                                                                     PendingIntentFlags.UpdateCurrent);
                LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);
                locationManager.RemoveProximityAlert(broadcast);
                return alert;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region Schedule
        public LocationNotification ScheduleLocationNotification(LocationNotification locationNotification)
        {
            try
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));

                intent.PutExtra("ID", locationNotification.Id);
                intent.PutExtra("Title", locationNotification.Title);
                intent.PutExtra("Body", locationNotification.Body);
                intent.PutExtra("Type", "LocationNotification");

                var latitude = locationNotification.Position.Latitude;
                var longitude = locationNotification.Position.Longitude;
                var radius = locationNotification.Radius;

                PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                     locationNotification.Id,
                                                                     intent,
                                                                     PendingIntentFlags.UpdateCurrent);

                LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);

                locationManager.AddProximityAlert(latitude, longitude, radius, -1, broadcast);

                return locationNotification;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #endregion

        #region Time Notifications
        public Models.TimeNotification ScheduleTimeNotification(TimeNotification timeNotification)
        {
            try
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));

                intent.PutExtra("ID", timeNotification.Id);
                intent.PutExtra("Title", timeNotification.Title);
                intent.PutExtra("Body", timeNotification.Body);
                intent.PutExtra("Type", "TimeNotification");

                PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                     timeNotification.Id,
                                                                     intent,
                                                                     PendingIntentFlags.UpdateCurrent);

                AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(AlarmService);


                var date = timeNotification.Date;
                var time = timeNotification.Time;

                Calendar calendar = Calendar.Instance;
                calendar.TimeInMillis = Java.Lang.JavaSystem.CurrentTimeMillis();
                calendar.Set(CalendarField.Year, date.Year);
                calendar.Set(CalendarField.Month, date.Month - 1);
                calendar.Set(CalendarField.DayOfMonth, date.Day);
                calendar.Set(CalendarField.HourOfDay, time.Hours);
                calendar.Set(CalendarField.Minute, time.Minutes);
                calendar.Set(CalendarField.Second, 0);

                alarmManager.SetExact(AlarmType.RtcWakeup, calendar.TimeInMillis, broadcast);

                return timeNotification;
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        #region RuntimePermissions

        async Task GetPermissions()
        {
            if ((int)Build.VERSION.SdkInt >= 23)
            {
                await GetPermissionsAsync();
                return;
            }
        }

        const int RequestLocationId = 0;

        readonly string[] PermissionsGroupLocation =
        {
            Manifest.Permission.AccessCoarseLocation,
            Manifest.Permission.AccessFineLocation,
        };

        async Task GetPermissionsAsync()
        {
            const string permission = Manifest.Permission.AccessFineLocation;

            if (CheckSelfPermission(permission) == (int)Permission.Granted)
            {
                Toast.MakeText(this, "Access permissions granted", ToastLength.Short).Show();
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission))
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("NotifyMe needs special permissions to continue");
                alert.SetPositiveButton("Grant Permissions", (senderAlert, args) =>
                {
                    RequestPermissions(PermissionsGroupLocation, RequestLocationId);
                });
                Dialog dialog = alert.Create();
                dialog.Show();
                return;
            }
            RequestPermissions(PermissionsGroupLocation, RequestLocationId);
        }

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                {
                    if (grantResults[0] == (int)Permission.Granted)
                    {
                        Toast.MakeText(this, "Special permissions granted", ToastLength.Short).Show();
                    }
                    else
                    {
                        await GetPermissionsAsync();
                    }
                }
                break;
            }
        }
        #endregion

        #region Popup
        public override void OnBackPressed()
        {
            if (Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed))
            {
                PopupNavigation.Instance.PopAsync(true);
            }
            else
            {
                // Do something if there are not any pages in the `PopupStack`
            }
        }
        
        #endregion
    }
}

