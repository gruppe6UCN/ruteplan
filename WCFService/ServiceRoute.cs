using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Control;
using Model;

namespace WCFService
{
    public class ServiceRoute : IServiceRoute
    {
        public List<Route> GetRoutes()
        {
            List<Route> routes = RouteController.Instance.Routes.ToList();
            if (routes.Count == 0)
            {
                throw new FaultException<ExceptionNoRoutes>(new ExceptionNoRoutes("No routes is imported."));
            }
            return routes;
        }
    }
}
