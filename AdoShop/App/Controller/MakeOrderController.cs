using System;
using System.Linq;
using System.Windows.Forms;
using AdoShop.Entity;
using AdoShop.Entity.User;
using AdoShop.Factory;
using AdoShop.Utils;
using LanguageExt;
using Newtonsoft.Json;
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
                return "Failed.";
            }

            var key = request.Headers["Api-Key"];

            if (Context.Users.Count(x => x.ApiKey == key) == 0) {
                Console.WriteLine("no user");
                return "Failed. No user.";
            }

            var user = Context.Users.First(x => x.ApiKey == key);

            if (user.Role != UserRole.Operator) {
                return "Only operator can create orders.";
            }

            var json = request.Params["Order"];

            var data = JsonConvert.DeserializeObject<ClientGoodsData[]>(json);

            try {
                var goods = data.Map(x => new OrderRelatedGoods {
                    Good = Context.Goods.First(good => good.Id == x.Id),
                    Count = x.Count
                });

                var order = OrderFactory.CreateOrder(user, goods);
                Context.Orders.Add(order);
                Context.SaveChanges();

                var resp = "ТОВ \"БІЛОЦЕРКІВСЬКИЙ ХАВЧИК\"\n\n" +
                           $"ФІКСАЛЬНИЙ ЧЕК НОМЕР {order.Id}\n" +
                           $"КАССИР: {order.User.FullName}\n\n";

                resp = order.Goods.Fold(resp, (current, pair)
                    => current + $"{pair.Good.Name} x{pair.Count} : {pair.Sum} грн.\n");

                resp += $"\n\nЗАГАЛЬНА СУММА: {order.Sum} грн.\n" +
                        "ДЯКУЄМО ЗА ПОКУПКУ";

                return resp;
            } catch (Exception e) {
                return e.Message;
            }
        }
    }
}