using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Control
{
    public class ExportController
    {
        public RouteController RouteCtr { get; private set; }
        private static ExportController instance;
    
        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private ExportController() {
            RouteCtr = RouteController.Instance;
        }
    
        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static ExportController Instance { 
                get { 
                    if (instance == null)
                        instance = new ExportController();
                    return instance;
                }
            }
    
        /// <summary>
        /// Exports all routes to database.
        /// </summary>
        public void ExportData()
        {
            RouteCtr.ExportData();
        }
          
    }
}
