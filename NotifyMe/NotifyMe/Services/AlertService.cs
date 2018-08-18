using System.Linq;
using System.Collections.Generic;
using SQLite;
using Xamarin.Forms;
using NotifyMe.Models.DbModels;
using NotifyMe.Interfaces;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.Services
{
    public class AlertService : IAlertService
    {
        private SQLiteConnection _dbContext;

        public AlertService()
        {
            _dbContext = DependencyService.Get<ISqliteConnection>().GetConnection();
            _dbContext.CreateTable<Alert>();
        }

        #region Common
        public int AddAlert(Alert alert)
        {
            _dbContext.Insert(alert);
            var addedAlert = _dbContext.Table<Alert>()
                                .OrderByDescending(a => a.Id)
                                .ToList()[0];
            if (addedAlert != null)
            {
                return addedAlert.Id;
            }
            return -1;
        }

        public int DeleteAlert(Alert alert)
        {
            return _dbContext.Delete(alert);
        }

        public int DisableAlert(Alert alert)
        {
            alert.State = AlertState.Disabled;
            return _dbContext.Update(alert);
        }

        public int DeleteAlertById(int id)
        {
            return _dbContext.Table<Alert>().Delete(a => a.Id == id);
        }

        public Alert GetAlertById(int id)
        {
            var alert = _dbContext.Table<Alert>().Where(a => a.Id == id).FirstOrDefault();
            return alert;
        }

        public int ActivateAlert(Alert alert)
        {
            alert.State = AlertState.Active;
            return _dbContext.Update(alert);
        }
        #endregion

        #region Time Alerts
        public List<Alert> GetAllTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetSentTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time &&  a.User == userName && a.State == AlertState.Sent)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetActiveTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time && a.User == userName && a.State == AlertState.Active)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetDisabledTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time && a.User == userName && a.State == AlertState.Disabled)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }
        #endregion

        #region Location Alerts
        public List<Alert> GetAllLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == (int)AlertType.Location && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetSentLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Location)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetActiveLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == (int)AlertType.Location && a.User == userName && a.State == AlertState.Active)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetDisabledLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Location && a.User == userName && a.State == AlertState.Disabled)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }
        #endregion


    }
}
