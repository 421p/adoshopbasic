using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using NHttp;

namespace backend.ShopAdo.Controller
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
