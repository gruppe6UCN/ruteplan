using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace WebApp
{
    public class Global : System.Web.HttpApplication
    {
        private WCFClient wcfClient;

        protected void Application_Start(object sender, EventArgs e)
        {
            wcfClient = WCFClient.Instance;

            RouteTable.Routes.MapPageRoute("Show Routes", "", "~/Main.aspx");
            RouteTable.Routes.MapPageRoute("RouteDetil", "{route_id}", "~/RouteDetail.aspx");
        }
    }
}