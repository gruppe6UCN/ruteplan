package try_stuff;

import model.DefaultDeliveryStop;

import java.awt.geom.Point2D;

public class GeoLoc {
    private Point2D.Double location;
    private DefaultDeliveryStop deliveryStop;

    public GeoLoc(double latitude, double longitude) {
        this.location = new Point2D.Double(latitude, longitude);
    }

    public double getLatitude() {
        return location.getX();
    }

    public double getLongitude() {
        return location.getY();
    }
}
