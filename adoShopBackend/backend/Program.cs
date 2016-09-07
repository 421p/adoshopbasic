using System;
using System.Collections.Generic;
using System.Data.Entity;
using backend.Entity;
using backend.Entity.User;
using backend.Server;
using LanguageExt;
using static LanguageExt.List;
using static LanguageExt.Prelude;
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
