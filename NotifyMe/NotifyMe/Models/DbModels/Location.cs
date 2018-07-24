using SQLite;
using System;

namespace NotifyMe.Models.DbModels
{
    [Table("Locations")]
    public class Location
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string User { get; set; }

        public string Name { get; set; }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime DateTime { get; set; }
    }
}
