package model;

import java.sql.Time;

/**
 * Customer class used for customer information.
 * 
 */

public class Customer {
    private long id;
    private String streetName;
    private String streetNo;
    private int zipcode;
    private String city;
    private Time timeOfDelivery;

    public Customer(long id, String streetName, String streetNo, int zipcode, String city, Time timeOfDelivery) {
        this.id = id;
        this.streetName = streetName;
        this.streetNo = streetNo;
        this.zipcode = zipcode;
        this.city = city;
        this.timeOfDelivery = timeOfDelivery;
    }

    public long getID() {
        return id;
    }


    public Time getTimeOfDelivery() {
        return timeOfDelivery;
    }

    /**
     * @param timeOfDelivery the timeOfDelivery to set
     */
    public void setTimeOfDelivery(Time timeOfDelivery) {
        this.timeOfDelivery = timeOfDelivery;
    }
}