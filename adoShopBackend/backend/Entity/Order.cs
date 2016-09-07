using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LanguageExt;

namespace backend.Entity
{
    [Table("orders")]
    public class Order
    {
        public Order()
        {
            Date = DateTime.Now; 
            Goods = new HashSet<OrderRelatedGoods>();
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

        [NotMapped]
        public decimal Sum => Goods.Fold(decimal.Zero, (seed, current) => current.Sum);

        public virtual User.User User { get; set; }

        public virtual ICollection<OrderRelatedGoods> Goods { get; set; }
    }
}
