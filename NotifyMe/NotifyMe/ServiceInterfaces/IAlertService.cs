using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IAlertService
    {
        int AddAlert(Alert alert);

        int DeleteAlert(Alert alert);

        int DisableAlert(Alert alert);

        int DeleteAlertById(int id);

        Alert GetAlertById(int id);

        List<Alert> GetAllTimeAlerts();

        List<Alert> GetActiveUserTimeAlerts(string userName);

        List<Alert> GetAllUserTimeAlerts(string userName);

        List<Alert> GetAllLocationAlerts();

        List<Alert> GetActiveUserLocationAlerts(string userName);

        List<Alert> GetAllUserLocationAlerts(string userName);

        int ActivateAlert(Alert alert);
    }
}
