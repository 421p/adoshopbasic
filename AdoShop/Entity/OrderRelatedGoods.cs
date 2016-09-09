using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace AdoShop.Entity
{
    [Table("good_count_pair")]
    public class OrderRelatedGoods
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("order_id")]
        public int OrderId { get; set; }

        [Column("good_id")]
        public int GoodId { get; set; }

        [Column("count")]
        [Required]
        public int Count { get; set; }

        [Column("sum")]
        [Required]
        public decimal Sum { get; set; }

        [JsonIgnore]
        public virtual Good Good { get; set; }

        [JsonIgnore]
        public virtual Order Order { get; set; }
    }
}
