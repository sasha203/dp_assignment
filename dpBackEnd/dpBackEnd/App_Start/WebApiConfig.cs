using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace dpBackEnd
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {

            //config.EnableCors();  //can comment this line due to CORS from startup.cs
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // config.Formatters.JsonFormatter.SupportedMediaTypes
            // .Add(new MediaTypeHeaderValue("text/html"));

        }
    }
}
