using System;

namespace NotifyMe.ServiceInterfaces
{
    public interface IValidatorService
    {
        bool ValidateDateTime(DateTime date, TimeSpan time);
    }
}
