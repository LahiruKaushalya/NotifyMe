using NotifyMe.Models;
using NotifyMe.Models.DbModels;

namespace NotifyMe.Interfaces
{
    public interface INotificationService
    {
        TimeNotification ScheduleTimeNotification(TimeNotification timeNotification);

        LocationNotification ScheduleLocationNotification(LocationNotification locationNotification);

        Alert RemoveLocationNotification(Alert alert);

        Alert RemoveTimeNotification(Alert alert);
    }
}
