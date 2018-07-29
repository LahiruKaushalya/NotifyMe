using System.Collections.Generic;
using System.Threading.Tasks;

using NotifyMe.Models.DbModels;

namespace NotifyMe.ServiceInterfaces
{
    public interface IAlertService
    {
        int AddAlert(Alert alert);

        int DeleteAlertHard(Alert alert);

        int DeleteAlertSoft(Alert alert);

        int DeleteAlertById(int id);

        Alert GetAlertById(int id);

        List<Alert> GetTimeAlerts();

        List<Alert> GetUserTimeAlerts(string userName);

        List<Alert> GetAllUserTimeAlerts(string userName);

        List<Alert> GetLocationAlerts();

        List<Alert> GetUserLocationAlerts(string userName);

        List<Alert> GetAllUserLocationAlerts(string userName);

        int RestoreAlert(Alert alert);
    }
}
