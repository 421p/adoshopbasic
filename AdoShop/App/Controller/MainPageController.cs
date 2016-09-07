using System.IO;
using NHttp;

namespace AdoShop.App.Controller
{
    class MainPageController : IController
    {
        public string GetRoute()
        {
            return "/";
        }

        public void Proccess(HttpRequest request, HttpResponse response)
        {
            Router.InvokeFileOperationSafe(() =>
            {
                var fs = new FileStream(Router.Webroot.FullName + "/index.html", FileMode.Open);
                fs.CopyTo(response.OutputStream);
                fs.Close();
            });
        }
    }
}
