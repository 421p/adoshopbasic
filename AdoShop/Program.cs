using System;
using System.Configuration;
using System.Data.Entity;
using System.Threading.Tasks;
using AdoShop.Entity;
using AdoShop.Server;

namespace AdoShop
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            var instance = new Instance();

            Task.Run(() => instance.Start());

            using (var model = new ShopContext(ConfigurationManager.ConnectionStrings["shop_ado"].ConnectionString)) {
                Faker.Faker.InjectEntities(model);
            }

            Console.WriteLine("Done");
        }
    }
}
