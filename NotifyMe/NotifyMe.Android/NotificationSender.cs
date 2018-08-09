
using Android.App;
using Android.Content;
using Android.Locations;
using Android.Support.V4.App;
using NotifyMe.Models.DbModels;
using SQLite;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.Droid
{
    [BroadcastReceiver]
    public class NotificationSender : BroadcastReceiver
    {
        private int _id;
        private string _title;
        private string _body;

        private SQLiteConnection _dbContext;

        public override void OnReceive(Context context, Intent intent)
        {
            _id = intent.GetIntExtra("ID", 0);
            _title = intent.GetStringExtra("Title");
            _body = intent.GetStringExtra("Body");

            _dbContext = new SqliteDbConnection_Android().GetConnection();

            var Type = intent.GetStringExtra("Type");

            if (Type.Equals("TimeNotification"))
            {
                SendNotification();
                UpdateAlert(_id);
            }
            else if (Type.Equals("LocationNotification"))
            {
                bool isEntering = intent.GetBooleanExtra(LocationManager.KeyProximityEntering, false);

                if (isEntering)
                {
                    SendNotification();
                    UpdateAlert(_id);
                    MainActivity.RemoveProximityAlert(_id);
                }
            }
        }

        private void SendNotification()
        {
            NotificationCompat.Builder notificationBuilder = new NotificationCompat.Builder(Application.Context);

            notificationBuilder.SetContentTitle(_title)
                               .SetContentText(_body)
                               .SetSmallIcon(Resource.Drawable.abc_ic_menu_paste_mtrl_am_alpha)
                               .SetDefaults(NotificationCompat.DefaultSound |
                                            NotificationCompat.DefaultVibrate |
                                            NotificationCompat.DefaultLights)
                               .SetPriority(NotificationCompat.PriorityMax)
                               .SetCategory(NotificationCompat.CategoryAlarm);

            NotificationManagerCompat notificationManager = NotificationManagerCompat.From(Application.Context);
            notificationManager.Notify(_id, notificationBuilder.Build());
        }

        private void UpdateAlert(int id)
        {
            var alert =  _dbContext.Table<Alert>().Where(a => a.Id == id).FirstOrDefault();
            alert.State = AlertState.Sent;
           _dbContext.Update(alert);
        }
    }
}