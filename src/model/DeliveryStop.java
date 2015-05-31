package model;

import java.util.ArrayList;

public class DeliveryStop {
    
    private long id;
    private DefaultDeliveryStop defaultStop;
    private ArrayList<TransportUnit> transportUnits;
    
    public DeliveryStop(DefaultDeliveryStop defaultStop) {
        this.defaultStop = defaultStop;
        //Automatize some variables such as id later m8.
    }

    /**
     * @return the id
     */
    public long getID() {
        return id;
    }

    /**
     * @param id the id to set
     */
    public void setID(long id) {
        this.id = id;
    }

    /**
     * @return the defaultStop
     */
    public DefaultDeliveryStop getDefaultStop() {
        return defaultStop;
    }

    /**
     * @return the transportUnits
     */
    public ArrayList<TransportUnit> getTransportUnits() {
        return transportUnits;
    }

    /**
     * @param transportUnits the transportUnits to set
     */
    public void setTransportUnits(ArrayList<TransportUnit> transportUnits) {
        this.transportUnits = transportUnits;
    }
    

}
