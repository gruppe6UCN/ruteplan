package model;

/**
 * Customer class used for customer information.
 * 
 */

public class Customer {
	private long id;
	private String streetName;
	private String streetNo;
	private int zipcode;
	private String city;

	public Customer(long id, String streetName, String streetNo, int zipcode, String city) {
		this.id = id;
		this.streetName = streetName;
		this.streetNo = streetNo;
		this.zipcode = zipcode;
		this.city = city;
	}

	public long getID() {
		return id;
	}
}