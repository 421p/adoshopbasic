using System;
using System.Net;
using AdoShop.ShopAdo;
using NHttp;

namespace AdoShop.Server
{
    public class Instance : IDisposable
    {
        private readonly HttpServer _server;

        public Instance()
        {
            _server = new HttpServer {EndPoint = new IPEndPoint(IPAddress.Loopback, 8082)};
            _server.RequestReceived += OnRequest;
        }

        private void OnRequest(object sender, HttpRequestEventArgs args)
        {
            Router.HandleRequest(args.Request, args.Response);
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public void Dispose()
        {
            _server.Dispose();
        }
    }
}
