using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IAlertService
    {
        int AddAlert(Alert alert);

        Alert GetAlertById(int id);

        List<Alert> GetAllTimeAlerts();

        List<Alert> GetAllUserTimeAlerts(string userEmail);

        List<Alert> GetAllLocationAlerts();

        List<Alert> GetAllUserLocationAlerts(string userEmail);
    }
}
