using System.IO;
using System.Threading.Tasks;
using NHttp;

namespace AdoShop.App.Controller {
    public class ShutdownController : IController {
        public string GetRoute()
        {
            return "/shutdown";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            Task.Run(() => Application.Halt());

            return "Server is going to shutdown.";
        }
    }
}