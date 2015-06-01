package model;

import java.time.LocalTime;

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
    private LocalTime timeOfDelivery;

    public Customer(long id, String streetName, String streetNo, int zipcode, String city, LocalTime timeOfDelivery) {
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


    public LocalTime getTimeOfDelivery() {
        return timeOfDelivery;
    }

    /**
     * @param timeOfDelivery the timeOfDelivery to set
     */
    public void setTimeOfDelivery(LocalTime timeOfDelivery) {
        this.timeOfDelivery = timeOfDelivery;
    }
}