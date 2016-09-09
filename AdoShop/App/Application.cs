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
        public static ShopContext Context { get; }

        private static PostgresProcess Postgres { get; }

        static Application()
        {
            Context = new ShopContext(
                ConfigurationManager.ConnectionStrings["shop_ado"].ConnectionString
            );

            Postgres = new PostgresProcess();
        }

        public static void Run()
        {
            var instance = new Instance();
            //var injector = new Task(() => Faker.Faker.InjectEntities(Context));

            //Postgres.Load += (sender, args) => injector.Start();

            Postgres.Halted += (sender, args) => Environment.Exit(0);

            Task.Run(() => {
                Postgres.StartPostgres();
            });

            Task.Run(() => instance.Start());

            Console.WriteLine("Loading...");

//            injector.ContinueWith(x => {
//                Console.Clear();
//                Console.WriteLine("Loaded.");
//            });

            Console.ReadKey();
        }

        public static void Halt()
        {
            Postgres.StopPostgres();
        }
    }
}
