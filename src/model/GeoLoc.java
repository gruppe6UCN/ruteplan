package model;

import java.awt.geom.Point2D;

public class GeoLoc {
    private long deliveryStopID;
    private Point2D.Double location;

    public GeoLoc(long deliveryStopID, double latitude, double longitude) {
        this.deliveryStopID = deliveryStopID;
        this.location = new Point2D.Double(latitude, longitude);
        this.location = new Point2D.Double(latitude, longitude);
    }

    public double getLatitude() {
        return location.getX();
    }

    public double getLongitude() {
        return location.getY();
    }
}
