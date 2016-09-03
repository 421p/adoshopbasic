using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    [Table("orders")]
    public class Order
    {
        public Order()
        {
            GoodsInOrders = new HashSet<GoodsInOrders>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("date")]
        [Required]
        public DateTime Date { get; set; }

        [Column("user_id")]
        [Required]
        public int UserId { get; set; }

        //calculate on db level?
        [Column("sum", TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public int Sum { get; private set; }

        public virtual ICollection<GoodsInOrders> GoodsInOrders { get; set; }
    }
}
