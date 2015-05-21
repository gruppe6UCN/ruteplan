package model;

import java.util.ArrayList;

public class DeliveryStop {
	
	private long id;
	private DefaultDeliveryStop defaultStop;
	private ArrayList<TransportUnit> transportUnits;
	
	public DeliveryStop(DefaultDeliveryStop defaultStop) {
		
		this.defaultStop = defaultStop;
		
		//Automatize some variables such as id later m8.
	}

	/**
	 * @param transportUnit transportUnit to add to ArrayList.
	 */
	public void addTransportUnit(TransportUnit transportUnit) {
		transportUnits.add(transportUnit);
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
	 * @return the defaultStop
	 */
	public DefaultDeliveryStop getDefaultStop() {
		return defaultStop;
	}

	/**
	 * @return the transportUnits
	 */
	public ArrayList<TransportUnit> getTransportUnits() {
		return transportUnits;
	}

	/**
	 * @param transportUnits the transportUnits to set
	 */
	public void setTransportUnits(ArrayList<TransportUnit> transportUnits) {
		this.transportUnits = transportUnits;
	}
	

}
