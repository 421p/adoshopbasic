using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using AdoShop.App.Controller;
using LanguageExt;
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
                _controllers.First(x => x.GetRoute() == request.Path).Proccess(request, response);
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

        private static IEnumerable<Type> LoadControllers()
        {
            return typeof (Router).GetTypeInfo().Assembly.DefinedTypes
                .Filter(type => type.ImplementedInterfaces.Any(x => x == typeof(IController)));
        } 
    }
}
