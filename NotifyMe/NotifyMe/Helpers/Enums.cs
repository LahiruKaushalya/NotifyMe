
namespace NotifyMe.Helpers
{
    public class Enums
    {
        public enum AlertType
        {
            Location,
            Time
        };

        public enum AlertState
        {
            Active,
            Disabled,
            Pending,
            Sent,
            Deleted,
            Failed
        }
    }
}
