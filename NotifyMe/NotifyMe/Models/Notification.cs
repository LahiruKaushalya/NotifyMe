using System;
using Xamarin.Forms.Maps;

namespace NotifyMe.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }
    }

    public class TimeNotification : Notification
    {
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
    }

    public class LocationNotification : Notification
    {
        public Position Position { get; set; }

        public float Radius { get; set; }
    }
}
