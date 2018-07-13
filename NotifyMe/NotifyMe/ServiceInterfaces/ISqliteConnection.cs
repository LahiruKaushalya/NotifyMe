using SQLite;

namespace NotifyMe.ServiceInterfaces
{
    public interface ISqliteConnection
    {
        SQLiteConnection GetConnection();
    }
}
