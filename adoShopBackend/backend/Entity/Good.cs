using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    [Table("goods")]
    public class Good
    {
        public Good()
        {
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Column("supplier_id")]
        public int? SupplierId { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [Column("price")]
        public decimal Price { get; set; }

        [Column("amount")]
        public decimal Amount { get; set; }

        public virtual Category Category { get; set; }

        public virtual Supplier Supplier { get; set; }
    }
}
