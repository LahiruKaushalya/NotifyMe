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
            return _dbContext.Insert(alert);
        }

        public int DeleteAlert(Alert alert)
        {
            return _dbContext.Delete(alert);
        }

        public int DeleteAlertById(int id)
        {
            return _dbContext.Table<Alert>().Delete(a => a.Id == id);
        }

        public List<Alert> GetActiveUserLocationAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == false && a.User == userName && a.IsActive == true)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetActiveUserTimeAlerts(string userName)
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == true && a.User == userName && a.IsActive == true)
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
                            .Where(a => a.Type == false)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
        }

        public List<Alert> GetAllTimeAlerts()
        {
            var alerts = _dbContext.Table<Alert>()
                            .Where(a => a.Type == true)
                            .OrderByDescending(a => a.Id)
                            .ToList();
            return alerts;
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
    }
}
