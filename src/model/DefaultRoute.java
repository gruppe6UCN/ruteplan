package model;

import java.sql.Time;
import java.util.ArrayList;

public class DefaultRoute {
    
    private long id;
    private Time timeOfDeparture;
    private TrailerType trailerType;
    private ArrayList<DefaultDeliveryStop> stops;
    private boolean extraRoute;
    
    public DefaultRoute(Time timeOfDeparture,
            TrailerType trailerType, boolean extraRoute) {
        this.timeOfDeparture = timeOfDeparture;
        this.trailerType = trailerType;
        this.extraRoute = extraRoute;
    }
    
    /**
     * @param stop DefaultDeliveryStop to add to ArrayList.
     */
    public void addDefaultDeliveryStop(DefaultDeliveryStop stop) {
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
    public void setId(long id) {
        this.id = id;
    }

    /**
     * @return the timeOfDeparture
     */
    public Time getTimeOfDeparture() {
        return timeOfDeparture;
    }

    /**
     * @param timeOfDeparture the timeOfDeparture to set
     */
    public void setTimeOfDeparture(Time timeOfDeparture) {
        this.timeOfDeparture = timeOfDeparture;
    }

    /**
     * @return the trailerType
     */
    public TrailerType getTrailerType() {
        return trailerType;
    }

    /**
     * @param trailerType the trailerType to set
     */
    public void setTrailerType(TrailerType trailerType) {
        this.trailerType = trailerType;
    }

    /**
     * @return the stops
     */
    public ArrayList<DefaultDeliveryStop> getStops() {
        return stops;
    }

    /**
     * @param stops the stops to set
     */
    public void setStops(ArrayList<DefaultDeliveryStop> stops) {
        this.stops = stops;
    }

    /**
     * @return the extraRoute
     */
    public boolean isExtraRoute() {
        return extraRoute;
    }

    /**
     * @param extraRoute the extraRoute to set
     */
    public void setExtraRoute(boolean extraRoute) {
        this.extraRoute = extraRoute;
    }
    

}
