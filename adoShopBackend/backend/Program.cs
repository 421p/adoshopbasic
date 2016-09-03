using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Entity;
using backend.Entity.User;
using backend.Server;
using LanguageExt;
using static LanguageExt.List;
using static LanguageExt.Prelude;
using static System.Configuration.ConfigurationManager;

namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            Task.Factory.StartNew(LoadData);

            var instance = new Instance();
            instance.Start();

            Console.ReadKey();
        }

        static void LoadData()
        {
            var model = new ShopContext(ConnectionStrings["shop_ado"].ConnectionString);

            model.Users.Add(new User()
            {
                Name = "Alina",
                Password = "alina",
                Role = UserRole.Manager
            });

            model.SaveChanges();

            foreach (var user in model.Users)
            {
                Console.WriteLine(user.Name);
            }
        }
    }
}
