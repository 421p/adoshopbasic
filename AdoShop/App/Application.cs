using System;
using System.Configuration;
using System.Threading.Tasks;
using AdoShop.Entity;
using AdoShop.Server;
using PostgresRunner;

namespace AdoShop.App
{
    public static class Application
    {
        public static ShopContext Context { get; set; }

        static Application()
        {
            Context = new ShopContext(
                ConfigurationManager.ConnectionStrings["shop_ado"].ConnectionString
            );
        }

        public static void Run()
        {
            var postgres = new PostgresProcess();
            var instance = new Instance();
            var injector = new Task(() => Faker.Faker.InjectEntities(Context));

            postgres.Load += (sender, args) => injector.Start();

            postgres.Halted += (sender, args) => Environment.Exit(0);

            Task.Run(() => {
                postgres.StartPostgres();
            });

            Task.Run(() => instance.Start());

            Console.WriteLine("Loading...");

            injector.ContinueWith(x => {
                Console.Clear();
                Console.WriteLine("Loaded.");
            });

            while (true) {
                var input = Console.ReadLine();

                switch (input) {
                    case "exit":
                        postgres.StopPostgres();
                        break;
                    default:
                        continue;
                }
            }
        }
    }
}
