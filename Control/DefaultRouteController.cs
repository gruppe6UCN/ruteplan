using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Server.Database;
using System.Collections.Concurrent;
using Model;

namespace Control
{
    public class DefaultRouteController
    {
        public DBDefaultRoute DbDefaultRoute { get; private set; }
        private static DefaultRouteController instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private DefaultRouteController() 
        {
            DbDefaultRoute = DBDefaultRoute.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DefaultRouteController Instance
        {
            get
            {
                if (instance == null)
                    instance = new DefaultRouteController();
                return instance;
            }
        }

        /// <summary>
        /// Gets a list of all default routes from the database.
        /// </summary>
        /// <returns>List of default routes.</returns>
        public List<DefaultRoute> GetDefaultRoutes()
        {
            //Gets a list of all defaultRoutes.
            List<DefaultRoute> list = DbDefaultRoute.DefaultRoutes();

            //Returns the list.
            return list;
        }

        /// <summary>
        /// Stores given default route to the database.
        /// </summary>
        /// <param name="defaultRoute">DefaultRoute to be stored.</param>
        public void store(DefaultRoute defaultRoute)
        {
            DbDefaultRoute.store(defaultRoute);
        }
    }
}
