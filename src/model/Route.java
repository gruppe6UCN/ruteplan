package model;

import java.time.LocalDate;
import java.time.LocalTime;
import java.util.ArrayList;

public class Route {

    private long id;
    private DefaultRoute defaultRoute;
    private ArrayList<DeliveryStop> stops;
    private LocalTime timeForDeparture;
    private LocalDate dateForDeparture;

    public Route(DefaultRoute defaultRoute, LocalDate date) {
        this.defaultRoute = defaultRoute;
        this.dateForDeparture = date;
        this.stops = new ArrayList<>();

        //Automatize dem other variables here later...
    }

    /**
     * @return the load of transport units in the trailer
     */
    public double getLoadForTrailer() {
        double load = 0.0;

        //Enters a loop for each delivery stop.
        for (DeliveryStop stop : stops) {
            load += stop.getSizeOfTransportUnits();
        }

        return load;
    }

    /**
     * @return the capacity found under default route
     */
    public double getCapacity() {
        return getDefaultRoute().getTrailerType().getCapacity();
    }

    /**
     * @param stop deliveryStop to add to ArrayList.
     */
    public void addDeliveryStop(DeliveryStop stop) {
        stops.add(stop);
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
     * @return the defaultRoute
     */
    public DefaultRoute getDefaultRoute() {
        return defaultRoute;
    }

    /**
     * @return the stops
     */
    public ArrayList<DeliveryStop> getStops() {
        return stops;
    }

    /**
     * @param stops the stops to set
     */
    public void setStops(ArrayList<DeliveryStop> stops) {
        this.stops = stops;
    }

    /**
     * @return the timeForDeparture
     */
    public LocalTime getTimeForDeparture() {
        return timeForDeparture;
    }

    /**
     * @param timeForDeparture the timeForDeparture to set
     */
    public void setTimeForDeparture(LocalTime timeForDeparture) {
        this.timeForDeparture = timeForDeparture;
    }

    /**
     * @return the dateForDeparture
     */
    public LocalDate getDateForDeparture() {
        return dateForDeparture;
    }

    /**
     * Checks to see if route is under loaded.
     * @return True if it is and false vice versa
     */
    public boolean isUnderloaded() {
        return this.getLoadForTrailer() < this.getCapacity() * 0.8;
    }
}
