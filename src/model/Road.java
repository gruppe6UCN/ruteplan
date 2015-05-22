package model;

import java.sql.Time;

public class Road {
    
    private GeoLoc geoLoc;
    private double distance;
    private Time time;
    
    public Road(GeoLoc geoLoc, double distance, Time time) {
        super();
        this.geoLoc = geoLoc;
        this.distance = distance;
        this.time = time;
    }

    /**
     * @return the geoLoc
     */
    public GeoLoc getGeoLoc() {
        return geoLoc;
    }

    /**
     * @param geoLoc the geoLoc to set
     */
    public void setGeoLoc(GeoLoc geoLoc) {
        this.geoLoc = geoLoc;
    }

    /**
     * @return the distance
     */
    public double getDistance() {
        return distance;
    }

    /**
     * @param distance the distance to set
     */
    public void setDistance(double distance) {
        this.distance = distance;
    }

    /**
     * @return the time
     */
    public Time getTime() {
        return time;
    }

    /**
     * @param time the time to set
     */
    public void setTime(Time time) {
        this.time = time;
    }

}
