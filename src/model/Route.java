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

}
