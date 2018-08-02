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

        public bool Type { get; set; } //True means Time Alert, False means Location alert. Only two states. Therefore no Enum used
        
        public DateTime DisplayDateTime { get; set; }

        public DateTime Date { get; set; }

        public TimeSpan Time { get; set; }

        public int LocationID { get; set; }

        public string LocationName { get; set; }

        public DateTime CreatedOn { get; set; }

        public bool IsDisabled { get; set; }

        public bool IsDeleted { get; set; }
    }
}
