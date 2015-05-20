package control;

/**
 * OptimizeController
 * Handles all functionality for the use-case optimize.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class OptimizeController {
	
	private static OptimizeController instance;
	
	/**
	 * Private constructor for singleton.
	 */
	private OptimizeController() {

	}
	
	/**
	 * Singleton method for class.
	 * @return instance of class.
	 */
	public static OptimizeController getInstance() {
		if (instance == null) {
			instance = new OptimizeController();			
		}
		
		return instance;
	}
}