using System;
using System.Collections.Generic;
using Model;
using WebApp.ServiceRoute;

namespace WebApp
{
    public class WCFClient
    {
        private static WCFClient _instance;
        public Dictionary<long, Route> Routes { get; private set; }
        private ServiceRouteClient serviceRoute;

        public static WCFClient Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new WCFClient();
                return _instance;
            }
        }
        private WCFClient()
        {
            Routes = new Dictionary<long, Route>();
        }

        public void UpdateRoutes()
        {
            try
            {
                serviceRoute = new ServiceRouteClient();
                Route[] routes = serviceRoute.GetRoutes();
                Dictionary<long, Route> tmpRoutes = new Dictionary<long, Route>();
                foreach (Route route in routes)
                {
                    tmpRoutes[route.ID != 0 ? route.ID : route.DefaultRoute.ID] = route;
                }
                Routes = tmpRoutes;
            }
            catch (Exception)
            {
            }
        }
    }
}