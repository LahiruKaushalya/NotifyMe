using SQLite;
using System;
using static NotifyMe.Helpers.Enums;

namespace NotifyMe.Models.DbModels
{
    [Table("Alerts")]
    public class Alert 
    {
        #region Common
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string User { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public AlertType Type { get; set; }

        public AlertState State { get; set; }
        #endregion

        #region Time
        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }
        #endregion

        #region Location
        public int LocationID { get; set; }

        public string LocationName { get; set; }
        #endregion
    }
}
