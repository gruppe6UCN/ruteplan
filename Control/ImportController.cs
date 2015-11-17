using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using FileHelpers;

namespace Control
{
    public class ImportController
    {
        public RouteController RouteCtr { get; private set; }
        private static ImportController instance;

        //Mapping Class for File Import.
        [DelimitedRecord(",")]
        public class MappingDefaultRoute
        {
            public long ID;

            public double TrailerType;

            public bool ExtraRoute;
        }

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
        /// Imports all routes from a .csv file.
        /// </summary>
        /// <param name="path">Path of .csv file to be imported.</param>
        public void ImportFromFile(string path)
        {
            //Reads the file and maps it to mapping class.
            var engine = new FileHelperEngine<MappingDefaultRoute>();
            var records = engine.ReadFile(path);

            //Creates list of default routes.
            List<DefaultRoute> defaultRoutes = new List<DefaultRoute>();

            //Converts mapping class to routes.
            foreach (var record in records)
            {
                DefaultRoute defaultRoute = new DefaultRoute(record.ID, record.TrailerType, record.ExtraRoute);
            }

            //Creates routes from list.
            RouteCtr.ImportRoutes(defaultRoutes, DateTime.Now);
        }


        /**
         * Imports all routes from database.
         * @param rowData
         *
        public void importRoutes(Vector<Vector> rowData) {
            routeController.importRoutes(LocalDate.now());

            List<Route> routes = routeController.getRoutes();

            routes.forEach(route -> {
                Vector row = new Vector();
                row.addElement(String.format("%03d", route.getDefaultRoute().getID()));
                row.addElement(route.getStops().size());
                row.addElement(String.format("%.1f / %.1f", route.getLoadForTrailer(), route.getCapacity()));
                rowData.add(row);
            });
         * 
         * */
    }
}
