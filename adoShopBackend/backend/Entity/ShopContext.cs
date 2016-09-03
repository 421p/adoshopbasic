using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Entity
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    public class ShopContext : DbContext
    {
        public ShopContext(string cs): base(cs) {}

        public DbSet<User.User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Good> Goods { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<GoodsInOrders> GoodsInOrders { get; set; }
    }
}
