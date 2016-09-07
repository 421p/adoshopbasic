using System.Data.Entity;
namespace AdoShop.Entity
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
        public DbSet<OrderRelatedGoods> GoodCollections { get; set; }
    }
}
