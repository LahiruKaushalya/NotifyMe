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

        public AlertService()//False => Location Alert
        {
            _dbContext = DependencyService.Get<ISqliteConnection>().GetConnection();
            _dbContext.CreateTable<Alert>();
        }

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

        public List<Alert> GetAllUserLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == (int)AlertType.Location && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetAllUserTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public Alert GetAlertById(int id)
        {
            var alert = _dbContext.Table<Alert>().Where(a => a.Id == id).FirstOrDefault();
            return alert;
        }

        public List<Alert> GetAllLocationAlerts()
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Location)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetAllTimeAlerts()
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetActiveUserLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == (int)AlertType.Location && a.User == userName && a.State == AlertState.Active)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetActiveUserTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == AlertType.Time && a.User == userName && a.State == AlertState.Active)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public int ActivateAlert(Alert alert)
        {
            alert.State = AlertState.Active;
            return _dbContext.Update(alert);
        }
    }
}
