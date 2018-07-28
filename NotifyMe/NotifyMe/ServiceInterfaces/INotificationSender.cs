using NotifyMe.Models;

namespace NotifyMe.ServiceInterfaces
{
    public interface INotificationSender
    {
        Notification ScheduleNotification(Notification notification);
    }
}
