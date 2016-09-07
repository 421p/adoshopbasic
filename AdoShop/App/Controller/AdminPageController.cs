using System.IO;
using NHttp;

namespace AdoShop.App.Controller {
    class AdminPageController : IController {
        public string GetRoute()
        {
            return "/admin";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            Router.InvokeFileOperationSafe(() => {
                var fs = new FileStream(Router.Webroot.FullName + "/admin.html", FileMode.Open);
                fs.CopyTo(response.OutputStream);
                fs.Close();
            });

            return string.Empty;
        }
    }
}