using System;
using System.Data.Entity;
using System.Runtime.InteropServices;
using AdoShop.App;
using AdoShop.Entity;

namespace AdoShop {
    class Program {
        static void Main()
        {
            ShowWindow(GetConsoleWindow(), 0);

            Database.SetInitializer(new CreateDatabaseIfNotExists<ShopContext>());

            try {
                Application.Run();
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}