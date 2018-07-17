using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace NotifyMe.Models.DbModels
{
    [Table("Users")]
    public class User
    {
        [PrimaryKey]
        public string Email { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string Telephone { get; set; }

        public bool LoginState { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
