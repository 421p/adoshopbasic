using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Entity;
using backend.Entity.User;
using static System.Configuration.ConfigurationManager;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());
           
            var model = new ShopContext(ConnectionStrings["shop_ado"].ConnectionString);

            var user = new User
            {
                Name = "Alina",
                Password = "alina",
                Role = UserRole.Manager
            };

            model.Users.Add(user);

            model.Categories.Add(new Category()
            {
                Name = "Drinks"
            });

            model.Suppliers.Add(new Supplier()
            {
                Name = "Coca-Cola"
            });

            model.SaveChanges();

            var kampot = new Good()
            {
                Name = "Kompot",
                SupplierId = 1,
                CategoryId = 1,
                Price = 10,
                Amount = 1000,
                Supplier = model.Suppliers.First(),
                Category = model.Categories.First()
            };

            model.Goods.Add(kampot);

            model.SaveChanges();

            var order = new Order()
            {
                User = user
            };
            
            order.Goods.Add(new OrderRelatedGoods
            {
                Count = 5,
                Good = kampot
            });

            model.Orders.Add(order);

            model.SaveChanges();

            Console.WriteLine("Done");
            
            Console.ReadKey();
        }
    }
}
