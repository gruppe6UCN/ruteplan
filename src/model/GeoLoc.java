package model;

import java.awt.geom.Point2D;
import java.awt.geom.Point2D.Double;
import java.util.ArrayList;

public class GeoLoc {
	
	private Point2D.Double location;
	private ArrayList<Road> roadTo;
	private ArrayList<Road> roadFrom;
	private DefaultDeliveryStop deliveryStop;

	public GeoLoc(Double location, ArrayList<Road> roadTo,
			ArrayList<Road> roadFrom, DefaultDeliveryStop deliveryStop) {
		super();
		this.location = location;
		this.roadTo = roadTo;
		this.roadFrom = roadFrom;
		this.deliveryStop = deliveryStop;
	}

	/**
	 * @return the location
	 */
	public Point2D.Double getLocation() {
		return location;
	}

	/**
	 * @param location the location to set
	 */
	public void setLocation(Point2D.Double location) {
		this.location = location;
	}

	/**
	 * @return the roadTo
	 */
	public ArrayList<Road> getRoadTo() {
		return roadTo;
	}

	/**
	 * @param roadTo the roadTo to set
	 */
	public void setRoadTo(ArrayList<Road> roadTo) {
		this.roadTo = roadTo;
	}

	/**
	 * @return the roadFrom
	 */
	public ArrayList<Road> getRoadFrom() {
		return roadFrom;
	}

	/**
	 * @param roadFrom the roadFrom to set
	 */
	public void setRoadFrom(ArrayList<Road> roadFrom) {
		this.roadFrom = roadFrom;
	}

	/**
	 * @return the deliveryStop
	 */
	public DefaultDeliveryStop getDeliveryStop() {
		return deliveryStop;
	}

	/**
	 * @param deliveryStop the deliveryStop to set
	 */
	public void setDeliveryStop(DefaultDeliveryStop deliveryStop) {
		this.deliveryStop = deliveryStop;
	}
	
	
}
