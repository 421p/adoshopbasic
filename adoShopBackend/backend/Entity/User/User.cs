using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity.User
{
    [Table("users")]
    public class User
    {
        [Key]   
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

        private string _role;

        [Column("role")]
        [Required]
        public string Role {
            get { return _role; }
            set
            {
                UserRole.AssertUserRole(value);
                _role = value;
            }
        }

        [Column("access_key")]
        [Required]
        public string ApiKey { get; set; }

        public User()
        {
            this.ApiKey = Guid.NewGuid().ToString("N");
        }
    }
}
