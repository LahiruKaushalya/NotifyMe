using SQLite;

namespace NotifyMe.Interfaces
{
    public interface ISqliteConnection
    {
        SQLiteConnection GetConnection();
    }
}
