using System.IO;
using System.Linq;
using AdoShop.Entity.User;
using AdoShop.Utils;
using NHttp;
using static LanguageExt.Prelude;
using static AdoShop.App.Application;

namespace AdoShop.App.Controller
{
    class MainPageController : IController
    {
        public string GetRoute()
        {
            return "/";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            var optional = Router.InvokeBasicHttpAuth(request);

            var userData = optional.Match(x => x, () => new AuthUserData());

            if (userData.Login.Length == 0 || Context.Users.Count(x => x.Name == userData.Login) == 0) {
                return Reject(response);
            }

            var user = Context.Users.First(x => x.Name == userData.Login);

            if (user.ValidatePassword(userData.Password) || user.Role != UserRole.Operator) {
                Router.InvokeFileOperationSafe(() => {
                    var fs = new FileStream(Router.Webroot.FullName + "/index.html", FileMode.Open);
                    fs.CopyTo(response.OutputStream);
                    fs.Close();
                });

                response.Headers.Add("Api-Key", user.ApiKey);
            }
            else {
                Reject(response);
            }

            return string.Empty;
        }

        public static string Reject(HttpResponse response)
        {
            response.StatusCode = 401;
            response.Headers.Add("WWW-Authenticate", "Basic realm=\"CashierPanel\"");

            return string.Empty;
        }
    }
}
