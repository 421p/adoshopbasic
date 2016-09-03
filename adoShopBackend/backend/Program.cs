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
         
            model.Users.Add(new User()
            {
                Name = "Alina",
                Password = "alina",
                Role = UserRole.Manager
            });
            model.SaveChanges();

            model.Categories.Add(new Category()
            {
                Name = "Drinks"
            });
            model.SaveChanges();

            model.Suppliers.Add(new Supplier()
            {
                Name = "Coca-Cola"
            });
            model.SaveChanges();

            model.Goods.Add(new Good()
            {
                Name = "Kompot",
                SupplierId = 1,
                CategoryId = 1,
                Price = 10,
                Amount = 1000
            });
            model.SaveChanges();

            /*
            model.Orders.Add(new Order()
            {

            });
            */

            foreach (var user in model.Users)
            {
                Console.WriteLine(user.Name);
            }
            
            Console.ReadKey();
        }
    }
}
