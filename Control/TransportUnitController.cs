using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Control
{
    public class TransportUnitController
    {
        private DBTransportUnit dbTransportUnit;
        private static TransportUnitController instance;
    
        /// <summary>
        /// Private singleton constructor.
        /// </summary>
        private TransportUnitController() {
            try {
                dbTransportUnit = DBTransportUnit.getInstance();
            } catch (ClassNotFoundException | SQLException e) {
                // TODO Auto-generated catch block
                e.printStackTrace();
            }
        }
    
        /// <summary>
        /// Singleton method. Returns the instance of the class.
        /// </summary>
        /// <returns>Instance of class.</returns>
        public static TransportUnitController getInstance() {
            if (instance == null) {
                instance = new TransportUnitController();            
            }
        
            return instance;
        }
    
        /**
         * Adds a transport unit to the delivery stop.
         * @param deliveryStop DeliveryStop to have transport units added.
         * @param customers ID of customers whose transports units are to be added.
         */
        public void addTransportUnit(DeliveryStop deliveryStop, ArrayList<Customer> customers) {
            ArrayList<Long> IDs = new ArrayList<>();

            //Adds all id form customer to IDs
            customers.forEach((customer -> IDs.add(customer.getID())));

            //Gets a list of all TransportUnits for the customers and place them in deliveryStop
            deliveryStop.setTransportUnits(dbTransportUnit.getTransportUnits(IDs));
        }
        }
    }
}
