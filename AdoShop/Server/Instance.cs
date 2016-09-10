using System;
using System.Net;
using AdoShop.App;
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
            _server.UnhandledException += (sender, args) => {
                Console.WriteLine(args.Exception.Message);
                Console.WriteLine(args.Exception.StackTrace);
            };
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
