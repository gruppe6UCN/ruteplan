package model;

public enum UnitType {
	EU_PAL              (1),
	HALV_PAL	        (0.5),
	KVART_PAL	        (0.25),
	RULLE_PALLE	        (0.70),
    VARE_CONTAINER      (0.44),
    SMOER_CONTAINER     (0.40),
    MAELKE_CONTAINER    (0.40);


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
