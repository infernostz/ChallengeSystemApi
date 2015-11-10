namespace CS.API
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Http;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.EnableCors();

            config.Routes.MapHttpRoute(
                name: "GetNextAvailableChallengeDate",
                routeTemplate: "api/challenges/next",
                defaults: new { controller = "Challenges", action = "GetNextAvailableChallengeDate" }
            );

            config.Routes.MapHttpRoute(
                name: "CompleteChallenge",
                routeTemplate: "api/challenges/{id}",
                defaults: new { controller = "Challenges" }
            );

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}