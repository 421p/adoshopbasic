using System;
using System.Data.Entity;
using backend.Entity;
using static backend.Faker.Faker;
using static System.Configuration.ConfigurationManager;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            using (var model = new ShopContext(ConnectionStrings["shop_ado"].ConnectionString)) {
                InjectEntities(model);
            }

            Console.WriteLine("Done");
        }
    }
}
