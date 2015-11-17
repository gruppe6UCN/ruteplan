//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Model;
//using Server;

//namespace Control
//{
//    public class ExportController
//    {
//        public RouteController RouteCtr { get; private set; }
//        private static ExportController instance;
    
//        /// <summary>
//        /// Private singleton constructor.
//        /// </summary>
//        private ExportController() {
//            RouteCtr = RouteController.Instance;
//        }
    
//        /// <summary>
//        /// Singleton method. Returns the instance of the class.
//        /// </summary>
//        /// <returns>Instance of class.</returns>
//        public static ExportController Instance { 
//                get { 
//                    if (instance == null)
//                        instance = new ExportController();
//                    return instance;
//                }
//            }
    
//        /**
//         * Exports all routes to database.
//         */
//        public void exportDatas(Vector rowData) {
//            RouteCtr.exportData();

//            List<Route> routes = RouteCtr.getRoutes();

//            routes.forEach(route -> {
//                Vector row = new Vector();
//                row.addElement(String.format("%03d", route.getID()));
//                row.addElement(route.getDefaultRoute().isExtraRoute() ? "NONE" : String.format("%03d", route.getDefaultRoute().getID()));
//                row.addElement(route.getStops().size());
//                row.addElement(String.format("%.1f / %.1f", route.getLoadForTrailer(), route.getCapacity()));
//                row.addElement(String.format("%02d:%02d", route.getTimeForDeparture().getHour(), route.getTimeForDeparture().getMinute()));
//                row.addElement(route.getDefaultRoute().isExtraRoute() ? "Yes" : "No");
//                rowData.add(row);
//            });
//    }
//}
