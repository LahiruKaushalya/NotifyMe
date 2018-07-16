using System;

namespace NotifyMe.Models
{
    public class HomePageMenuItem
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Icon { get; set; }

        public Type TargetType { get; set; }
    }
}