package try_stuff;

import control.RouteController;

/**
 * Created by alt_mulig on 5/30/15.
 */
public class no_gui {
    public static void main(String[] args) {
        RouteController routeController = RouteController.getInstance();
        routeController.importRoutes(new java.util.Date());

        
    }
}
