using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace qr {

    public static class WebApiConfig {

        public static void Register(HttpConfiguration config) {
            config.Formatters.JsonFormatter.SerializerSettings.ReferenceLoopHandling
                = Newtonsoft.Json.ReferenceLoopHandling.Serialize;
            config.Formatters.JsonFormatter.SerializerSettings.PreserveReferencesHandling
                 = Newtonsoft.Json.PreserveReferencesHandling.Objects;

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
            
        }
    }

}
