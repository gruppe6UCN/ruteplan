package model;

import java.awt.geom.Point2D;
import java.io.Serializable;

public class GeoLoc implements Serializable {
    private long id;
    private Point2D.Double location;

    public GeoLoc(long id, double latitude, double longitude) {
        this.id = id;
        this.location = new Point2D.Double(latitude, longitude);
    }

    public long getID() {
        return id;
    }

    public Point2D getLocation(){
        return this.location;
    }
}
