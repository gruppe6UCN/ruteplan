package model;

import java.awt.geom.Point2D;

public class GeoLoc {
    private long id;
    private long deliveryStopID;
    private Point2D.Double location;

    public GeoLoc(long id, long deliveryStopID, double latitude, double longitude) {
        this.id = id;
        this.deliveryStopID = deliveryStopID;
        this.location = new Point2D.Double(latitude, longitude);
        this.location = new Point2D.Double(latitude, longitude);
    }

    public long getID() {
        return id;
    }

    public double getLatitude() {
        return location.getX();
    }

    public double getLongitude() {
        return location.getY();
    }

    /**
     * @return the deliveryStopID
     */
    public long getDeliveryStopID() {
        return deliveryStopID;
    }
}
