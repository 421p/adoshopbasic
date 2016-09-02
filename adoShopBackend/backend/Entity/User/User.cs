using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity.User
{
    [Table("users")]
    class User
    {
        [Key]   
        [Column("id")]
        public int Id { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("password")]
        public string Password { get; set; }
        [Column("role")]
        public string Role { get; set; }
        [Column("access_key")]
        public string ApiKey { get; set; }

        public User()
        {
            this.ApiKey = Guid.NewGuid().ToString("N");
        }
    }
}
