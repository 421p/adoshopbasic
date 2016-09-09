using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using AdoShop.App.Controller;
using LanguageExt;
using static LanguageExt.Prelude;
using NHttp;

namespace AdoShop.App
{
    public class Router
    {
        private static readonly IEnumerable<IController> _controllers;
        public static DirectoryInfo Webroot { get; }

        static Router()
        {
            _controllers = LoadControllers().Map(Activator.CreateInstance).Cast<IController>();
            Webroot = new DirectoryInfo("..\\..\\www");
        }

        public static void HandleRequest(HttpRequest request, HttpResponse response)
        {
            var availableRoutes = _controllers.Map(x => x.GetRoute()).ToArray();

            if (availableRoutes.Contains(request.Path))
            {
                using (var writer = new StreamWriter(response.OutputStream)) {
                    var responseString = _controllers.First(x => x.GetRoute() == request.Path)
                        .Proccess(request, response);
                    writer.Write(responseString);
                }
            }
            else if (Webroot.GetFiles().Map(x => x.Name).Contains(request.Path.Substring(1)))
            {
                InvokeFileOperationSafe(() =>
                {
                    var file = new FileStream(Webroot.FullName + request.Path, FileMode.Open);
                    file.CopyTo(response.OutputStream);
                    file.Close();
                });
            }
            else
            {
                using (var writer = new StreamWriter(response.OutputStream))
                {
                    writer.Write("404");
                }
            }
        }

        public static void InvokeFileOperationSafe(Action closure)
        {
            var waitHandle = new EventWaitHandle(true, EventResetMode.AutoReset, "SHARED_BY_ALL_PROCESSES");
            waitHandle.WaitOne();
            closure.Invoke();
            waitHandle.Set();
        }

        public static Option<AuthUserData> InvokeBasicHttpAuth(HttpRequest request)
        {
            if (!request.Headers.Keys.Cast<string>().Contains("Authorization")) {
                return None;
            }

            var base64 = request.Headers["Authorization"].Substring("Basic ".Length);
            var authData = Encoding.UTF8.GetString(Convert.FromBase64String(base64)).Split(':');

            return Some(new AuthUserData{Login = authData[0], Password = authData[1]});
        }

        private static IEnumerable<Type> LoadControllers()
        {
            return typeof (Router).GetTypeInfo().Assembly.DefinedTypes
                .Filter(type => type.ImplementedInterfaces.Any(x => x == typeof(IController)));
        } 
    }
}
