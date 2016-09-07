using System;
using System.Data.Entity;
using System.Threading.Tasks;
using backend.Entity;
using backend.Server;
using static backend.Faker.Faker;
using static System.Configuration.ConfigurationManager;

namespace backend
{
    class Program
    {
        static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            var instance = new Instance();

            Task.Run(() => instance.Start());

            using (var model = new ShopContext(ConnectionStrings["shop_ado"].ConnectionString)) {
                InjectEntities(model);
            }

            Console.WriteLine("Done");
        }
    }
}
