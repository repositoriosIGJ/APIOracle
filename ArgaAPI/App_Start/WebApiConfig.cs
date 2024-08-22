using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

using ArgaAPI.Services;

namespace ArgaAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // Añadir el CorsHandler al pipeline de mensajes
            config.MessageHandlers.Add(new CorsHandler());

            // Otras configuraciones de Web API...
        }

    
    }
}
