using System.Linq;
using Newtonsoft.Json;
using NHttp;

namespace AdoShop.App.Controller {
    public class SuppliersController : IController {
        public string GetRoute()
        {
            return "/suppliers";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            var context = Application.CreateContext();
            response.Headers.Add("Content-Type", "application/json");
            var suppliers = context.Suppliers.ToList();
            return JsonConvert.SerializeObject(suppliers);
        }
    }
}