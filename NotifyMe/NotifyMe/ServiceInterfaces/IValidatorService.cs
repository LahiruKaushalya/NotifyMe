using System;

namespace NotifyMe.ServiceInterfaces
{
    public interface IValidatorService
    {
        bool ValidateDate(DateTime date);

        bool ValidateTime(TimeSpan time);
    }
}
