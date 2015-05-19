package model;

/**
 * Customer class used for customer information.
 * 
 */

public class Customer {
	
	private long id;
	private String name;
	private String adress;
	
	public Customer(long id, String name, String adress) {
		super();
		this.id = id;
		this.name = name;
		this.adress = adress;
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
	 * @return the name
	 */
	public String getName() {
		return name;
	}

	/**
	 * @param name the name to set
	 */
	public void setName(String name) {
		this.name = name;
	}

	/**
	 * @return the adress
	 */
	public String getAdress() {
		return adress;
	}

	/**
	 * @param adress the adress to set
	 */
	public void setAdress(String adress) {
		this.adress = adress;
	}

}