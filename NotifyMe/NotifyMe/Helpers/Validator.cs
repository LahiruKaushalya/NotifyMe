using System;
using NotifyMe.Interfaces;

namespace NotifyMe.Helpers
{
    public class Validator : IValidator
    {
        public bool ValidateDateTime(DateTime date, TimeSpan time)
        {
            if (date.Year > DateTime.Now.Year)
            {
                return true;
            }
            else if (date.Year < DateTime.Now.Year)
            {
                return false;
            }
            else if (date.Month > DateTime.Now.Month)
            {
                return true;
            }
            else if (date.Month < DateTime.Now.Month)
            {
                return false;
            }
            else if (date.Day > DateTime.Now.Day)
            {
                return true;
            }
            else if (date.Day < DateTime.Now.Day)
            {
                return false;
            }
            else if (time.Hours > DateTime.Now.Hour)
            {
                return true;
            }
            else if (time.Hours < DateTime.Now.Hour)
            {
                return false;
            }
            else if (time.Minutes > DateTime.Now.Minute)
            {
                return true;
            }
            else if (time.Minutes < DateTime.Now.Minute)
            {
                return false;
            }
            else
            {
                return false;
            }
        }
    }
}
