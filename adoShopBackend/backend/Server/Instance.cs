using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using backend.ShopAdo;
using NHttp;

namespace backend.Server
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
