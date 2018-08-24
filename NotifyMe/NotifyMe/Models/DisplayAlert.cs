using System;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Models
{
    public class DisplayAlert : Alert
    {  
        public string DisplayDateTime { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsSent { get; set; }

        public bool IsFailed { get; set; }
    }
}
