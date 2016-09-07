using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AdoShop.Entity
{
    [Table("good_count_pair")]
    public class OrderRelatedGoods
    {
        private int _count;

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("good_id")]
        public int GoodId { get; set; }

        [Column("count")]
        [Required]
        public int Count
        {
            get { return _count; }
            set
            {
                if (value > Good.Count) {
                    throw new Exception("Not enough goods to sell.");
                }
                _count = value;
            }
        }

        [Column("sum")]
        [Required]
        public decimal Sum { get; set; }

        public virtual Good Good { get; set; }

        public virtual Order Order { get; set; }
    }
}
