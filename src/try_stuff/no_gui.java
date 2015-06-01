package try_stuff;

import control.ExportController;
import control.OptimizeController;
import control.RouteController;

import java.io.IOException;
import java.sql.SQLException;
import java.time.LocalDate;

/**
 * Created by alt_mulig on 5/30/15.
 */
public class no_gui {
    public static void main(String[] args) throws SQLException, IOException, ClassNotFoundException {
        findAndMergeCycles.writeToDB("db_scripts/create_arlas_db_light.sql");

        RouteController routeController = RouteController.getInstance();
        OptimizeController optimizeController = OptimizeController.getInstance();
        ExportController exportController = ExportController.getInstance();

        routeController.importRoutes(LocalDate.now());
        optimizeController.optimize();
        exportController.exportDatas();


        System.out.println("Jobs Down");
    }
}
