using System.Collections.Generic;
using SQLite;

using NotifyMe.Models.DbModels;
using NotifyMe.ServiceInterfaces;
using Xamarin.Forms;
using System.Linq;
using System.Threading.Tasks;

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

        public int DeleteAlertHard(Alert alert)
        {
            return _dbContext.Delete(alert);
        }
        
        public int DeleteAlertSoft(Alert alert)
        {
            alert.IsDeleted = true;
            return _dbContext.Update(alert);
        }

        public int DeleteAlertById(int id)
        {
            return _dbContext.Table<Alert>().Delete(a => a.Id == id);
        }

        public List<Alert> GetAllUserLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == false && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetAllUserTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == true && a.User == userName)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public Alert GetAlertById(int id)
        {
            var alert = _dbContext.Table<Alert>().Where(a => a.Id == id).FirstOrDefault();
            return alert;
        }

        public List<Alert> GetLocationAlerts()
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == false)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetTimeAlerts()
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == true)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetUserLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == false && a.User == userName && a.IsDeleted == false)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetUserTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == true && a.User == userName && a.IsDeleted == false)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public int RestoreAlert(Alert alert)
        {
            alert.IsDeleted = false;
            return _dbContext.Update(alert);
        }
    }
}
