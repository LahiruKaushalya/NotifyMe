using System;
using System.IO;
using SQLite;

using NotifyMe.iOS;
using NotifyMe.ServiceInterfaces;


[assembly: Xamarin.Forms.Dependency(typeof(SqliteDbConection_iOS))]

namespace NotifyMe.iOS
{
    public class SqliteDbConection_iOS : ISqliteConnection
    {
        public SQLiteConnection GetConnection()
        {
            var dbName = "NotifyMe.db3";

            string personalFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libraryFolder = Path.Combine(personalFolder, "..", "Library");

            var path = Path.Combine(libraryFolder, dbName);
            return new SQLiteConnection(path);
        }
    }
}