using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Android;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Widget;
using Android.OS;
using Android.Content;
using Android.Locations;

using Java.Util;
using Xamarin.Forms;

using Rg.Plugins.Popup.Services;

using NotifyMe.Droid;
using NotifyMe.Interfaces;
using NotifyMe.Models;
using NotifyMe.Models.DbModels;
using System.Linq;
using Android.Net;
using Plugin.CurrentActivity;

[assembly: Dependency(typeof(MainActivity))]

namespace NotifyMe.Droid
{
    [Activity(Label = "NotifyMe", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation, ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ILocationListener, INotificationService
    {
        protected async override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            await GetPermissions();
            GetLocationUpdate();

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#02511f"));

            Rg.Plugins.Popup.Popup.Init(this, bundle);
            Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            CrossCurrentActivity.Current.Init(this, bundle);

            LoadApplication(new App());
        }

        protected override void OnResume()
        {
            base.OnResume();
            GetLocationUpdate();
        }

        private void GetLocationUpdate()
        {
            LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);
            string locationProvider = null;
            try
            {
                Criteria criteriaForLocationService = new Criteria
                {
                    HorizontalAccuracy = Accuracy.High
                };
                locationProvider = locationManager.GetBestProvider(criteriaForLocationService, true);
            }
            catch (Exception) { }
            
            if (locationProvider == null)
            {
                return;
            }
            else if (locationProvider.Equals(string.Empty))
            {
                Toast.MakeText(Android.App.Application.Context, "Location provider not available", ToastLength.Long).Show();
            }
            else
            {
                locationManager.RequestLocationUpdates(locationProvider, 100, 1, this);
            }
        }

        #region Location Notification
        
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

        #region Cancel
        public Alert RemoveLocationNotification(Alert alert)
        {
            try
            {
                RemoveProximityAlert(alert.Id);
                return alert;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static void RemoveProximityAlert(int alertID)
        {
            Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));
            PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                    alertID,
                                                                    intent,
                                                                    PendingIntentFlags.UpdateCurrent);
            LocationManager locationManager = (LocationManager)Android.App.Application.Context.GetSystemService(LocationService);
            locationManager.RemoveProximityAlert(broadcast);

        }
        #endregion

        #endregion

        #region Time Notifications

        #region Schedule
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

        #region Cancel
        public Alert RemoveTimeNotification(Alert alert)
        {
            try
            {
                Intent intent = new Intent(Android.App.Application.Context, typeof(NotificationSender));
                PendingIntent broadcast = PendingIntent.GetBroadcast(Android.App.Application.Context,
                                                                     alert.Id,
                                                                     intent,
                                                                     PendingIntentFlags.UpdateCurrent);
                AlarmManager alarmManager = (AlarmManager)Android.App.Application.Context.GetSystemService(AlarmService);
                alarmManager.Cancel(broadcast);
                return alert;
            }
            catch (Exception)
            {
                return null;
            }
        }
        
        #endregion

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
            Manifest.Permission.AccessFineLocation,
            Manifest.Permission.WakeLock,
        };

        private async Task GetPermissionsAsync()
        {
            const string permission1 = Manifest.Permission.AccessFineLocation;
            const string permission2 = Manifest.Permission.WakeLock;

            if (CheckSelfPermission(permission1) == (int)Permission.Granted &&
                CheckSelfPermission(permission2) == (int)Permission.Granted)
            {
                return;
            }

            if (ShouldShowRequestPermissionRationale(permission1) ||
                ShouldShowRequestPermissionRationale(permission2))
            {
                AlertDialog.Builder alert = new AlertDialog.Builder(this);
                alert.SetTitle("Permissions Needed");
                alert.SetMessage("NotifyMe needs permissions to continue");
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

        public override async void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            switch (requestCode)
            {
                case RequestLocationId:
                {
                    if (grantResults[0] == (int)Permission.Granted)
                    {
                        Toast.MakeText(this, "Permissions granted", ToastLength.Short).Show();
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

        #region Location Listner implementation
        public void OnLocationChanged(Android.Locations.Location location)
        {
            
        }

        public void OnProviderDisabled(string provider)
        {
            
        }

        public void OnProviderEnabled(string provider)
        {
            
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            
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

        #region Check Internet Connectivity
        public static bool IsNetworkConnected()
        {
            var connectivityManager = (ConnectivityManager)Android.App.Application.Context.GetSystemService(ConnectivityService);
            return connectivityManager.ActiveNetworkInfo == null ? false : connectivityManager.ActiveNetworkInfo.IsConnected;
        }
        #endregion
    }
}

