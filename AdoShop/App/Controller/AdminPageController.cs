using System;
using System.IO;
using AdoShop.Utils;
using LanguageExt;
using LanguageExt.SomeHelp;
using static LanguageExt.Prelude;
using NHttp;

namespace AdoShop.App.Controller {
    class AdminPageController : IController {
        public string GetRoute()
        {
            return "/admin";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            var optional = Optional(Router.InvokeBasicHttpAuth(request));

            var user = optional.Match(x => x, () => new AuthUserData());

            var boolPass = Aes.Encrypt(user.Password, "secret_token") == Aes.Encrypt("admin", "secret_token");

            if (user.Login == "admin" && boolPass) {
                Router.InvokeFileOperationSafe(() => {
                    var fs = new FileStream(Router.Webroot.FullName + "/admin.html", FileMode.Open);
                    fs.CopyTo(response.OutputStream);
                    fs.Close();
                });
            } else {
                Reject(response);
            }

            return string.Empty;
        }

        public static string Reject(HttpResponse response)
        {
            response.StatusCode = 401;
            response.Headers.Add("WWW-Authenticate", "Basic realm=\"OdmenPanel\"");

            return string.Empty;
        }
    }
}