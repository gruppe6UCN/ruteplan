package model;

import java.sql.Date;
import java.sql.Time;
import java.util.ArrayList;

public class Route {
	
	private long id;
	private DefaultDeliveryRoute defaultRoute;
	private ArrayList<DeliveryStop> stops;
	private TrailerType trailerType;
	private Time auctualTimeOfDeparture;
	private Date date;
	
	public Route(long id, DefaultDeliveryRoute defaultRoute,
			ArrayList<DeliveryStop> stops, TrailerType trailerType,
			Time auctualTimeOfDeparture, Date date) {
		super();
		this.id = id;
		this.defaultRoute = defaultRoute;
		this.stops = stops;
		this.trailerType = trailerType;
		this.auctualTimeOfDeparture = auctualTimeOfDeparture;
		this.date = date;
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
	public DefaultDeliveryRoute getDefaultRoute() {
		return defaultRoute;
	}

	/**
	 * @param defaultRoute the defaultRoute to set
	 */
	public void setDefaultRoute(DefaultDeliveryRoute defaultRoute) {
		this.defaultRoute = defaultRoute;
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

	/**
	 * @param date the date to set
	 */
	public void setDate(Date date) {
		this.date = date;
	}

}
