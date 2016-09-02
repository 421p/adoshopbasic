using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backend.Entity
{
    [DbConfigurationType(typeof(NpgsqlConfiguration))]
    internal class ShopContext : DbContext
    {
        public ShopContext(string cs): base(cs) {}

        public DbSet<User.User> Users { get; set; }
    }
}
