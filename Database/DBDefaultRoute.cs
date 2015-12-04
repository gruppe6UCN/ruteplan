using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data;

namespace Database
{
    public class DBDefaultRoute
    {
        public DBConnection DbConnection { get; private set; }
        private static DBDefaultRoute instance;

        /// <summary>
        /// Private singleton constructor.
        /// </summary>  
        private DBDefaultRoute()
        {
            DbConnection = DBConnection.Instance;
        }

        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static DBDefaultRoute Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBDefaultRoute();
                }

                return instance;
            }

        }



        /// Returns a list of all default routes.
        public List<DefaultRoute> DefaultRoutes()
        {
            List<DefaultRoute> list;
            String sql = "select * from DefaultRoute where extra_route=0";
            list = DbConnection.SendSQL<DefaultRoute>(sql, ConvertToDefaultRoute);
            return list;
        }

        /// Takes the ReulstSet from database and returns a list of all default routes.
        public List<DefaultRoute> ConvertToDefaultRoute(IDataReader dataSet)
        {
            List<DefaultRoute> tableList = new List<DefaultRoute>();
            while (dataSet.Read())
            {
                tableList.Add(new DefaultRoute(
                    dataSet.GetInt64(0),
                    dataSet.GetDouble(1),
                    dataSet.GetBoolean(2)

                ));
            }
            return tableList;
        }
    
        /// <summary>
        /// Stores all defaultRoutes in the database.
        /// </summary>
        /// <param name="defaultRoute"></param>
        public void store(DefaultRoute defaultRoute)
        {
            String sql = String.Format("INSERT into DefaultRoute values('{0}', {1});",
                    defaultRoute.TrailerType,
                    // inline if statement: if true return 1 else return 0
                    defaultRoute.ExtraRoute ? 1 : 0);
            long defaultRouteID = DbConnection.SendInsertSQL(sql);
            defaultRoute.ID = defaultRouteID;
        }
    }
}