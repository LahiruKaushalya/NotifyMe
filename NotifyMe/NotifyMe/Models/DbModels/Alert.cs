using SQLite;
using System;

namespace NotifyMe.Models.DbModels
{
    [Table("Alerts")]
    public class Alert
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        
        public string User { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool Type { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateTime { get; set; }

        public int LocationID { get; set; }
    }
}
