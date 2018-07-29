using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Locations;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;

namespace NotifyMe.Droid
{
    [BroadcastReceiver]
    public class NotificationSender : BroadcastReceiver
    {
        private int _id;
        private string _title;
        private string _body;

        public override void OnReceive(Context context, Intent intent)
        {
            _id = intent.GetIntExtra("ID", 0);
            _title = intent.GetStringExtra("Title");
            _body = intent.GetStringExtra("Body");

            var Type = intent.GetStringExtra("Type");

            if (Type.Equals("TimeNotification"))
            {
                SendNotification();
            }
            else if (Type.Equals("LocationNotification"))
            {
                bool isEntering = intent.GetBooleanExtra(LocationManager.KeyProximityEntering, false);

                if (isEntering)
                {
                    SendNotification();
                }
            }
        }

        private void SendNotification()
        {
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(Android.App.Application.Context);

            notificationBuilder.SetContentTitle(_title)
                               .SetContentText(_body)
                               .SetSmallIcon(Resource.Drawable.abc_ic_menu_paste_mtrl_am_alpha)
                               .SetDefaults(NotificationCompat.DefaultSound |
                                            NotificationCompat.DefaultVibrate |
                                            NotificationCompat.DefaultLights)
                               .SetPriority(NotificationCompat.PriorityMax)
                               .SetCategory(NotificationCompat.CategoryReminder);

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Android.App.Application.Context);
            notificationManager.Notify(_id, notificationBuilder.Build());
        }

    }
}