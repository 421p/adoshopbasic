using System;
using System.Configuration;
using System.Data.Entity;
using System.Threading.Tasks;
using AdoShop.Entity;
using AdoShop.Server;
using PostgresRunner;

namespace AdoShop {
    class Program {
        static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            var postgres = new PostgresProcess();
            var instance = new Instance();

            postgres.Load += (sender, args) => {
                var model = new ShopContext(
                    ConfigurationManager.ConnectionStrings["shop_ado"].ConnectionString
                );
                Faker.Faker.InjectEntities(model);

                Console.WriteLine("Done");
                postgres.StopPostgres();
            };

            postgres.Halted += (sender, args) => Environment.Exit(0);

            Task.Run(() => {
                postgres.StartPostgres();
            });

            Task.Run(() => instance.Start());

            Console.ReadKey();
        }
    }
}