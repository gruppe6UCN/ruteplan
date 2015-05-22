package model;

public enum Type {
	Standard (1);
	
	private final double size;
	
	Type(double size) {
		this.size = size;
	}

	/**
	 * @return the size
	 */
	public double getSize() {
		return size;
	}	

}
