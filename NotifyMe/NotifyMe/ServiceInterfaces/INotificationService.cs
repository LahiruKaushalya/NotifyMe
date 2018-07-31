using NotifyMe.Models;
using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface INotificationService
    {
        TimeNotification ScheduleTimeNotification(TimeNotification timeNotification);

        LocationNotification ScheduleLocationNotification(LocationNotification locationNotification);

        Alert RemoveLocationNotification(Alert alert);
    }
}
