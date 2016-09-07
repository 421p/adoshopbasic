using NHttp;

namespace AdoShop.App.Controller
{
    interface IController
    {
        string GetRoute();
        void Proccess(HttpRequest request, HttpResponse response);
    }
}
