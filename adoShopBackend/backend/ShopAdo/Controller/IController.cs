using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using NHttp;

namespace backend.ShopAdo.Controller
{
    interface IController
    {
        string GetRoute();
        void Proccess(HttpRequest request, HttpResponse response);
    }
}
