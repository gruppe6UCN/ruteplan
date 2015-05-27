package model;

import java.sql.Time;

public class Road {
    
    private long from;
    private long to;
    private double distance;
    private Time time;
    
    public Road(long from_ID, long to_ID, double distance, Time time) {
        this.from = from_ID;
        this.to = to_ID;
        this.distance = distance;
        this.time = time;
    }

    public long getFrom() {
        return from;
    }

    public long getTo() {
        return to;
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
