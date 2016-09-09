using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AdoShop.Utils;

namespace AdoShop.Entity.User
{
    [Table("users")]
    public class User
    {
        private string _role;

        public User()
        {
            ApiKey = Guid.NewGuid().ToString("N");
            Orders = new HashSet<Order>();
        }

        [Key]   
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public string Name { get; set; }

        [Column("full_name")]
        [Required]
        public string FullName { get; set; }

        [Column("password")]
        [Required]
        public string Password { get; set; }

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

        public virtual ICollection<Order> Orders { get; set; }

        public bool ValidatePassword(string given)
        {
            return Password == Aes.Encrypt(given, UserSalts.Default);
        }
    }
}
