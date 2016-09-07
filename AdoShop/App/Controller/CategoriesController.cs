using System.Linq;
using Newtonsoft.Json;
using NHttp;

namespace AdoShop.App.Controller {
    public class CategoriesController : IController {
        public string GetRoute()
        {
            return "/categories";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            response.Headers.Add("Content-Type", "application/json");
            var cats = Application.Context.Categories.ToList();
            return JsonConvert.SerializeObject(cats);
        }
    }
}