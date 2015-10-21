using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    class DefaultRouteController
    {
        private DBDefaultRoute dbDefaultRoute;
        private static DefaultRouteController instance;

        /**
         * Private constructor for singleton.
         * @throws SQLException 
         * @throws ClassNotFoundException
         */
        private DefaultRouteController() {
        try {
            dbDefaultRoute = DBDefaultRoute.getInstance();
        } catch (ClassNotFoundException | SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

        /**
         * Singleton method for class.
         * @return instance of class.
         */
        public static DefaultRouteController getInstance()
        {
            if (instance == null)
            {
                instance = new DefaultRouteController();
            }

            return instance;
        }

        /**
         * Gets an ArrayList of all defaultRoutes from the database.
         * @return List of all defaultRoutes.
         */
        public ArrayList<DefaultRoute> getDefaultRoutes()
        {

            //Gets a list of all defaultRoutes.
            ArrayList<DefaultRoute> list = dbDefaultRoute.getDefaultRoutes();

            //Returns the list.
            return list;
        }


        public void store(DefaultRoute defaultRoute)
        {
            dbDefaultRoute.store(defaultRoute);
        }
    }
}
