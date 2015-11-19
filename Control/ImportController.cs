using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Control
{
    public class ImportController
    {
        public RouteController RouteCtr { get; private set; }
        private static ImportController instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private ImportController() 
        {
            RouteCtr = RouteController.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static ImportController Instance { 
                get { 
                    if (instance == null)
                        instance = new ImportController();
                    return instance;
                }
            }
    
        /// <summary>
        /// Imports all routes from database.
        /// </summary>
        public void ImportRoutes()
        {
            RouteCtr.ImportRoutes(DateTime.Now);
        }

        /// <summary>
        /// Imports all default routes from a .csv file.
        /// </summary>
        /// <param name="pathRoutes">Path of .csv file to be imported.</param>
        /// <param name="pathStops">Path of .csv file to be imported.</param>
        /// <param name="pathCustomers">Path of .csv file to be imported.</param>
        public void ImportFromFile(string pathRoutes, string pathStops, string pathCustomers)
        {
            RouteCtr.ImportRoutesFromFile(DateTime.Now, pathRoutes, pathStops, pathCustomers);
        }
    }
}
