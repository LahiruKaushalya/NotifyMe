using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using UIKit;

using NotifyMe.iOS;
using NotifyMe.Interfaces;

[assembly:Xamarin.Forms.Dependency(typeof(ToastiOS))]
namespace NotifyMe.iOS
{
    public class ToastiOS : IToastService
    {
        const double LongDelay = 3.5;
        const double ShortDelay = 2.0;

        NSTimer alertDelay;
        UIAlertController alert;

        public void ShortMessage(string message)
        {
            ShowToast(message, ShortDelay);
        }

        public void LongMessage(string message)
        {
            ShowToast(message, LongDelay);
        }
        
        void ShowToast(string message, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                DismissToast();
            });
            alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        private void DismissToast()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}