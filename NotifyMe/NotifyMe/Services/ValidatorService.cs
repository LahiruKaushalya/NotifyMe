using System;
using NotifyMe.ServiceInterfaces;

namespace NotifyMe.Services
{
    public class ValidatorService : IValidatorService
    {
        public bool ValidateDate(DateTime date)
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
            else if (date.Day >= DateTime.Now.Day)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool ValidateTime(TimeSpan time)
        {
            if (time.Hours < DateTime.Now.Hour)
            {
                return false;
            }
            else if (time.Hours > DateTime.Now.Hour)
            {
                return true;
            }
            else if (time.Minutes < DateTime.Now.Minute)
            {
                return false;
            }
            else if (time.Minutes > DateTime.Now.Minute)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
