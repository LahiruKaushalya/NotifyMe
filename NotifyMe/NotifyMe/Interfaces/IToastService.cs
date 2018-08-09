namespace NotifyMe.Interfaces
{
    public interface IToastService
    {
        void ShortMessage (string message);

        void LongMessage(string message);
    }
}
