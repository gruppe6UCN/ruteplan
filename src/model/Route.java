package model;

import java.util.Date;
import java.sql.Time;
import java.util.ArrayList;

public class Route {
	
	private long id;
	private DefaultRoute defaultRoute;
	private ArrayList<DeliveryStop> stops;
	private Time auctualTimeOfDeparture;
	private Date date;
	
	public Route(DefaultRoute defaultRoute, Date date) {
		this.defaultRoute = defaultRoute;
		this.date = date;
		
		//Automatize dem other variables here later...
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
	public long getId() {
		return id;
	}

	/**
	 * @param id the id to set
	 */
	public void setId(long id) {
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
	 * @return the auctualTimeOfDeparture
	 */
	public Time getAuctualTimeOfDeparture() {
		return auctualTimeOfDeparture;
	}

	/**
	 * @param auctualTimeOfDeparture the auctualTimeOfDeparture to set
	 */
	public void setAuctualTimeOfDeparture(Time auctualTimeOfDeparture) {
		this.auctualTimeOfDeparture = auctualTimeOfDeparture;
	}

	/**
	 * @return the date
	 */
	public Date getDate() {
		return date;
	}
}
