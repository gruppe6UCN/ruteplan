using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApp
{
    public partial class Main : System.Web.UI.Page
    {
        protected WCFClient wcfClient = WCFClient.Instance;
        protected void Page_Load(object sender, EventArgs e)
        {
            wcfClient.UpdateRoutes();
        }

        protected String GetListOfRoutes()
        {
            String links = "";
            List<long> keys = wcfClient.Routes.Keys.ToList();
            keys.Sort((k1,k2) => k1 > k2 ? 1 : -1);
            foreach (long route_id in keys)
            {
                links += String.Format(@"<a id={0} href='{0}'>Route {0}</a></br>", route_id);
            }
            return links;
        }
    }
}