using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Entity
{
    [Table("good_count_pair")]
    public class OrderRelatedGoods
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("count")]
        public int Count { get; set; }

        public virtual Good Good { get; set; }

        public virtual Order Order { get; set; }
    }
}
