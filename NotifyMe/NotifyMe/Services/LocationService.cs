using System.Collections.Generic;
using SQLite;

using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

namespace NotifyMe.Services
{
    public class LocationService : ILocationService
    {
        private SQLiteConnection _dbContext;

        public LocationService()
        {
            _dbContext = DependencyService.Get<ISqliteConnection>().GetConnection();
            _dbContext.CreateTable<Location>();
        }

        public int AddLocation(Location location)
        {  
            return _dbContext.Insert(location);
        }

        public int DeleteLocationHard(Location location)
        {
            return _dbContext.Delete(location);
        }

        public void DeleteLocationSoft(Location location)
        {
            location.IsDeleted = true;
            _dbContext.Update(location);
        }

        public List<Location> GetLocations()
        {
            var locations = _dbContext.Table<Location>()
                            .Where(l => l.IsDeleted == false)
                            .OrderByDescending(l => l.Id)
                            .ToList();
            return locations;
        }

        public List<Location> GetLocationsByUser(string userName)
        {
            var locations = _dbContext.Table<Location>()
                            .Where(l => l.User == userName && l.IsDeleted == false)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return locations;
        }

        public List<Location> GetAllLocations()
        {
            var locations = _dbContext.Table<Location>()
                            .OrderByDescending(l => l.Id)
                            .ToList();
            return locations;
        }
        
        public List<Location> GetAllLocationsByUser(string userName)
        {
            var locations = _dbContext.Table<Location>()
                            .Where(l => l.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return locations;
        }

        public Location GetLocationById(int id)
        {
            var location = _dbContext.Table<Location>()
                            .Where(l => l.Id == id && l.IsDeleted == false)
                            .FirstOrDefault();
            return location;
        }

        public void RestoreLocation(Location location)
        {
            location.IsDeleted = false;
            _dbContext.Update(location);
        }
    }
}
