using System;

namespace NotifyMe.Interfaces
{
    public interface IValidator
    {
        bool ValidateDateTime(DateTime date, TimeSpan time);
    }
}
