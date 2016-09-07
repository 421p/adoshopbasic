using System;
using System.Data.Entity;
using AdoShop.App;
using AdoShop.Entity;

namespace AdoShop {
    class Program {
        static void Main()
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            try {
                Application.Run();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
    }
}