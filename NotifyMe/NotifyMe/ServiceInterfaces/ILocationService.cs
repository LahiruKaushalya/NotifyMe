using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface ILocationService
    {
        int AddLocation(Location location);

        int DeleteLocationHard(Location location);

        void DeleteLocationSoft(Location location);

        Location GetLocationById(int id);

        List<Location> GetLocations();

        List<Location> GetLocationsByUser(string userName);

        List<Location> GetAllLocations();

        List<Location> GetAllLocationsByUser(string userName);
        
    }
}
