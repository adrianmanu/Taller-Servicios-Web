using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace ServiceREST
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de Web API

            // Rutas de Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                // Cambiar la ruta predeterminada a api/{controller}/{action}/{id}
                // LA cual es la ruta que se utiliza en el controlador ProductController en el método X
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
