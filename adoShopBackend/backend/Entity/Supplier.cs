using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    [Table("suppliers")]
    public class Supplier
    {
        public Supplier()
        {
            Goods = new HashSet<Good>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(20)]
        public string Name { get; set; }

        public virtual ICollection<Good> Goods { get; set; }
    }
}
