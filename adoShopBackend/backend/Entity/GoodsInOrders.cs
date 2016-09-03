using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backend.Entity
{
    [Table("goodsInOrders")]
    public class GoodsInOrders
    {
        private decimal _amount;

        [Key]
        [Column("good_id", Order = 0)]
        public int GoodId { get; set; }

        [Key]
        [Column("order_id", Order = 1)]
        public int OrderId { get; set; }

        //should be checked (less than amount in goods table)
        [Column("amount", TypeName = "numeric")]
        [Required]
        public decimal Amount
        {
            get { return _amount; }
            set
            {
                _amount = value;
            }
        }

        //make it calculated field (not sure whether it's needed)
        [Column("sum", TypeName = "money")]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal Sum { get; private set; }

        public virtual Good Good { get; set; }

        public virtual Order Order { get; set; }
    }
}
