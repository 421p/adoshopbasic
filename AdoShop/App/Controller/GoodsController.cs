using System.Linq;
using Newtonsoft.Json;
using NHttp;

namespace AdoShop.App.Controller {
    public class GoodsController : IController {
        public string GetRoute()
        {
            return "/goods";
        }

        public string Proccess(HttpRequest request, HttpResponse response)
        {
            response.Headers.Add("Content-Type", "application/json");
            var goods = Application.Context.Goods.ToList();
            return JsonConvert.SerializeObject(goods);
        }
    }
}