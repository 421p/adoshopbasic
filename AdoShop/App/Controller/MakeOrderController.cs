using System;
using System.Linq;
using NHttp;
using static AdoShop.App.Application;
using static LanguageExt.Prelude;

namespace AdoShop.App.Controller {
    public class MakeOrderController : IController {
        public string GetRoute()
        {
            return "/make/order";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            if (!request.Headers.AllKeys.Contains("Api-Key")) {
                response.StatusCode = 401;
                return "Failed.";
            }

            var key = request.Headers["Api-Key"];

            if (Context.Users.Count(x => x.ApiKey == key) == 0) {
                response.StatusCode = 401;
                Console.WriteLine("no user");
                return "Failed.";
            }

            var user = Context.Users.First(x => x.ApiKey == key);

            var order = request.Params["Order"];

            Console.WriteLine(order);

            return "Success";

        }
    }
}