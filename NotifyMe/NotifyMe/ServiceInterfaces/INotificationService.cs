using NotifyMe.Models;

namespace NotifyMe.ServiceInterfaces
{
    public interface INotificationService
    {
        TimeNotification ScheduleTimeNotification(TimeNotification timeNotification);

        LocationNotification ScheduleLocationNotification(LocationNotification locationNotification);
    }
}
