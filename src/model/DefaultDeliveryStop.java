package model;

import java.sql.Time;
import java.util.ArrayList;

public class DefaultDeliveryStop {
	
	private Time timeOfDelivery;
	private ArrayList<Customer> customers;
	
	public DefaultDeliveryStop(Time timeOfDelivery,
			ArrayList<Customer> customers) {
		super();
		this.timeOfDelivery = timeOfDelivery;
		this.customers = customers;
	}

	/**
	 * @return the timeOfDelivery
	 */
	public Time getTimeOfDelivery() {
		return timeOfDelivery;
	}

	/**
	 * @param timeOfDelivery the timeOfDelivery to set
	 */
	public void setTimeOfDelivery(Time timeOfDelivery) {
		this.timeOfDelivery = timeOfDelivery;
	}

	/**
	 * @return the customers
	 */
	public ArrayList<Customer> getCustomers() {
		return customers;
	}

	/**
	 * @param customers the customers to set
	 */
	public void setCustomers(ArrayList<Customer> customers) {
		this.customers = customers;
	}
	

}
