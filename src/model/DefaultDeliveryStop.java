package model;

import java.util.ArrayList;

public class DefaultDeliveryStop {
    
    private long id;
    private ArrayList<Customer> customers;
    private long geoLocID;
    
    public DefaultDeliveryStop(long id, long geoLocID) {
        this.id = id;
        this.geoLocID = geoLocID;
    }

    /**
     * @return the id
     */
    public long getID() {
        return id;
    }

    /**
     * @return the customers
     */
    public ArrayList<Customer> getCustomers() {
        return customers;
    }

    /**
     * @param customers the customers to set
     */
    public void setCustomers(ArrayList<Customer> customers) {
        this.customers = customers;
    }

    /**
     * @return the geoLoc
     */
    public long getGeoLocID() {
        return this.geoLocID;
    }
}
