using NHttp;

namespace AdoShop.ShopAdo.Controller
{
    interface IController
    {
        string GetRoute();
        void Proccess(HttpRequest request, HttpResponse response);
    }
}
