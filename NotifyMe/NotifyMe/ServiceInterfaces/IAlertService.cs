using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IAlertService
    {
        int AddAlert(Alert alert);

        int DeleteAlert(Alert alert);

        int DeleteAlertById(int id);

        Alert GetAlertById(int id);

        List<Alert> GetAllTimeAlerts();

        List<Alert> GetAllUserTimeAlerts(string userName);

        List<Alert> GetActiveUserTimeAlerts(string userName);

        List<Alert> GetAllLocationAlerts();

        List<Alert> GetAllUserLocationAlerts(string userName);

        List<Alert> GetActiveUserLocationAlerts(string userName);
    }
}
