using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface ILocationService
    {
        int AddLocation(Location location);

        int DeleteLocation(Location location);

        Location GetLocationById(int id);

        List<Location> GetAllLocations();

        List<Location> GetAllLocationsByUser(string userEmail);
        
        List<Location> GetAllLocationsById(int id);
    }
}
