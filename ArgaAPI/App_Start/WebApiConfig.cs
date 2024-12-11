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
            // Rutas específicas primero
            config.Routes.MapHttpRoute(
                name: "GetTiposSocietariosRoute",
                routeTemplate: "api/tiposocietario/GetTiposSocietarios",
                defaults: new { controller = "TipoSocietario", action = "GetTiposSocietarios" }
            );

            config.Routes.MapHttpRoute(
                name: "GetCodigosSinCeroALaIzqRoute",
                routeTemplate: "api/tiposocietario/GetCodigosSinCeroALaIzq",
                defaults: new { controller = "TipoSocietario", action = "GetCodigosSinCeroALaIzq" }
            );
         

            // Ruta específica para GetByRazonSocial
            config.Routes.MapHttpRoute(
                name: "GetByRazonSocialRoute",
                routeTemplate: "api/expediente/GetByRazonSocial",
                defaults: new { controller = "Expediente", action = "GetByRazonSocial" }
            );

            // Ruta específica para GetExpedientes
            config.Routes.MapHttpRoute(
           name: "GetExpedientesRoute",
           routeTemplate: "api/expediente/GetExpedientes",
           defaults: new { controller = "Expediente", action = "GetExpedientes" }
       );

            // Ruta específica para GetByCorrelativo
            config.Routes.MapHttpRoute(
                name: "GetByCorrelativoRoute",
                routeTemplate: "api/expediente/GetByCorrelativo",
                defaults: new { controller = "Expediente", action = "GetByCorrelativo" }
            );

            // Ruta específica para GetByCodigoTipoSocietario
            config.Routes.MapHttpRoute(
                name: "GetByCodigoTipoSocietarioRoute",
                routeTemplate: "api/tiposocietario/GetByCodigoSocietario/{codigo}",
                defaults: new { controller = "TipoSocietario", action = "GetByCodigoTipoSocietario" }
            );

           
           
            // Ruta específica para GetByTipo
            config.Routes.MapHttpRoute(
                name: "GetByTipoRoute",
                routeTemplate: "api/tiposocietario/GetByTipo/{tipo}",
                defaults: new { controller = "TipoSocietario", action = "GetByTipo" }
            );

            // Ruta específica para GetTramites
            config.Routes.MapHttpRoute(
                name: "GetTramitesRoute",
                routeTemplate: "api/tramite/GetTramites",
                defaults: new { controller = "Tramite", action = "GetTramites" }
            );

            // Ruta específica para GetTramiteByCorrelativo
            config.Routes.MapHttpRoute(
                name: "GetTramitesByCorrelativoRoute",
                routeTemplate: "api/tramite/GetTramitesByCorrelativo/{correlativo}",
                defaults: new { controller = "Tramite", action = "GetTramitesByCorrelativo" }
            );

            // Ruta específica para GetAllTipoTramites
            config.Routes.MapHttpRoute(
                name: "GetAllTipoTramitesRoute",
                routeTemplate: "api/tipotramite/GetAllTipoTramites",
                defaults: new { controller = "TipoTramite", action = "GetAllTipoTramites" }
            );

            // Ruta específica para GetTipoTramitebyCodigo
            config.Routes.MapHttpRoute(
                name: "GetTipoTramitebyCodigoRoute",
                routeTemplate: "api/tipotramite/GetTipoTramitebyCodigo/{codigo}",
                defaults: new { controller = "TipoTramite", action = "GetTipoTramitebyCodigo" }
            );

            // Ruta específica para GetDestinos
            config.Routes.MapHttpRoute(
                name: "GetDestinosRoute",
                routeTemplate: "api/destino/GetDestinos",
                defaults: new { controller = "Destino", action = "GetDestinos" }
            );

            // Ruta específica para GetDestinosTramite
            config.Routes.MapHttpRoute(
                 name: "GetDestinosTramiteRoute",
                 routeTemplate: "api/destinoTramite/GetDestinosTramite",
                 defaults: new { controller = "DestinoTramite", action = "GetDestinosTramite" }
            );

            // Ruta específica para GetULtimoDestinoTramite
            config.Routes.MapHttpRoute(
                name: "GetUltimoDestinoTramiteRoute",
                routeTemplate: "api/destinoTramite/GetUltimoDestinoTramite",
                defaults: new { controller = "DestinoTramite", action = "GetUltimoDestinoTramite" }
            );

            // Ruta específica para GetTramiteSinAsignarXDestinoDpto
            config.Routes.MapHttpRoute(
                name: "GetTramiteSinAsignarXDestinoDpto",
                routeTemplate: "api/destinoTramite/GetTramiteSinAsignarXDestinoDpto",
                defaults: new { controller = "DestinoTramite", action = "GetTramiteSinAsignarXDestinoDpto" }
            );

            // Ruta específica para GetTramitesRecibidosXDestinoDepartamento
            config.Routes.MapHttpRoute(
                name: "GetTramitesRecibidosXDestinoDepartamento",
                routeTemplate: "api/destinoTramite/GetTramitesRecibidosXDestinoDepartamento",
                defaults: new { controller = "DestinoTramite", action = "GetTramitesRecibidosXDestinoDepartamento" }
            );

            // Ruta específica para RecibirAsignarSubDestinTramite
            config.Routes.MapHttpRoute(
                name: "RecibirAsignarSubDestinTramite",
                routeTemplate: "api/destinoTramite/RecibirAsignarSubDestinTramite",
                defaults: new { controller = "DestinoTramite", action = "RecibirAsignarSubDestinTramite" }
            );

            // Ruta específica para GetAgentesXSubdestino
            config.Routes.MapHttpRoute(
                name: "GetAgentesXSubdestino",
                routeTemplate: "api/subdestino/GetAgentesXSubdestino",
                defaults: new { controller = "Subdestino", action = "GetAgentesXSubdestino" }
            );

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
