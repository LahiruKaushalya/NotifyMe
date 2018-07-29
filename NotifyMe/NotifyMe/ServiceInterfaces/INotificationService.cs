using NotifyMe.Models;

namespace NotifyMe.ServiceInterfaces
{
    public interface INotificationService
    {
        Notification ScheduleNotification(Notification notification);
    }
}
