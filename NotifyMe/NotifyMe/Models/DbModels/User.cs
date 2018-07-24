using SQLite;
using System;

namespace NotifyMe.Models.DbModels
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey]
        public string UserName { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }

        public string Address { get; set; }

        public bool LoginState { get; set; }

        public DateTime CreatedOn { get; set; }
        
    }
}
