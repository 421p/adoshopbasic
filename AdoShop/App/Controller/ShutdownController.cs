using System.IO;
using AdoShop.Utils;
using static LanguageExt.Prelude;
using NHttp;
using static AdoShop.App.Controller.AdminPageController;

namespace AdoShop.App.Controller {
    public class ShutdownController : IController {
        public string GetRoute()
        {
            return "/shutdown";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            var resp = string.Empty;

            var optional = Optional(Router.InvokeBasicHttpAuth(request));

            var user = optional.Match(x => x, () => new AuthUserData());

            var boolPass = Aes.Encrypt(user.Password, "secret_token") == Aes.Encrypt("admin", "secret_token");

            if (user.Login == "admin" && boolPass) {
                resp = "Server is going to shutdown";
                Application.Halt();
            }
            else {
                Reject(response);
            }

            return resp;
        }
    }
}