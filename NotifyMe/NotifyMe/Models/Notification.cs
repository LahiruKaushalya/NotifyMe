using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyMe.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
    }
}
