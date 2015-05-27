package model;

public enum UnitType {
	Standard (1);
	
	private final double size;
	
	UnitType(double size) {
		this.size = size;
	}

	/**
	 * @return the size
	 */
	public double getSize() {
		return size;
	}	

}
