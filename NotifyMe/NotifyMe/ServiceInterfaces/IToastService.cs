namespace NotifyMe.ServiceInterfaces
{
    public interface IToastService
    {
        void ShortMessage (string message);

        void LongMessage(string message);
    }
}
