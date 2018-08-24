using System.Collections.Generic;

using NotifyMe.Models.DbModels;

namespace NotifyMe.Interfaces
{
    public interface IAlertService
    {
        int AddAlert(Alert alert);

        int DeleteAlert(Alert alert);

        int DisableAlert(Alert alert);

        int DeleteAlertById(int id);

        Alert GetAlertById(int id);

        List<Alert> GetAllTimeAlerts(string userName);

        List<Alert> GetSentTimeAlerts(string userName);

        List<Alert> GetActiveTimeAlerts(string userName);

        List<Alert> GetDisabledTimeAlerts(string userName);

        List<Alert> GetSentLocationAlerts(string userName);

        List<Alert> GetActiveLocationAlerts(string userName);

        List<Alert> GetAllLocationAlerts(string userName);

        List<Alert> GetDisabledLocationAlerts(string userName);

        int ActivateAlert(Alert alert);

        int UpdateAlert(Alert alert);
    }
}
