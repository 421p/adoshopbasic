using NHttp;

namespace AdoShop.App.Controller
{
    interface IController
    {
        string GetRoute();
        string Proccess(HttpRequest request, HttpResponse response);
    }
}
