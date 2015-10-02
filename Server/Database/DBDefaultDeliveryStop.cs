using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server.Database

{
    /// all database functionality for default delivery stops will be handled by this class
    /// 
    /// </summary>
 

    class DBDefaultDeliveryStop
    {
        private DBConnection dbConnection;
        private static DBDefaultDeliveryStop instance;

        /// <summary >
        /// private constructor for singelton
        /// </summary>
       
        private DBDefaultDeliveryStop()
        {
            dbConnection = DBConnection.Instance;
        }

        /// <sunmmary>
        /// singelton get instance method 
        /// returns the instance from DB 
      
        public static DBDefaultDeliveryStop instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DBDefaultDeliveryStop();
                }

                return instance;
            }

        }
    }
}

        


    

