package model;

public enum TrailerType {
	STOR (33);
	
	private final double capacity;
	
	TrailerType(double capacity) {
		this.capacity = capacity;
	}

	/**
	 * @return the capacity
	 */
	public double getCapacity() {
		return capacity;
	}	

}
