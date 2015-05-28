package model;

import java.sql.Time;
import java.util.ArrayList;

public class DefaultRoute {
    
    private long id;
    private Time timeOfDeparture;
    private TrailerType trailerType;
    private ArrayList<DefaultDeliveryStop> stops;
    private boolean extraRoute;

    public DefaultRoute(long id, Time timeOfDeparture,
                        TrailerType trailerType, boolean extraRoute) {
        this.id = id;
        this.timeOfDeparture = timeOfDeparture;
        this.trailerType = trailerType;
        this.extraRoute = extraRoute;
    }

    public DefaultRoute(Time timeOfDeparture,
                        TrailerType trailerType, boolean extraRoute) {
        this.timeOfDeparture = timeOfDeparture;
        this.trailerType = trailerType;
        this.extraRoute = extraRoute;
        this.stops = new ArrayList<>();
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
     * @return the trailerType
     */
    public TrailerType getTrailerType() {
        return trailerType;
    }

    /**
     * @return the extraRoute
     */
    public boolean isExtraRoute() {
        return extraRoute;
    }
    

}
