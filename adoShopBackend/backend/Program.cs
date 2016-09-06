using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using backend.Entity;
using backend.Entity.User;


namespace backend
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer(new DropCreateDatabaseAlways<ShopContext>());

            Entities.Create();

            Console.WriteLine("Done");
            
            Console.ReadKey();
        }
    }
}
