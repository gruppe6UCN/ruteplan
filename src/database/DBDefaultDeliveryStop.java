package database;

import model.DefaultDeliveryStop;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DBDefaultDeliveryStop
 * Handles all database functionality for default delivery stops.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBDefaultDeliveryStop {

    private DBConnection dbConnection;
    private static DBDefaultDeliveryStop instance;

    /**
     * Private constructor for singleton.
     *
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private DBDefaultDeliveryStop() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    public static DBDefaultDeliveryStop getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBDefaultDeliveryStop();
        }

        return instance;
    }


    /**
     * @param defaultRouteID
     * @return list of all DefaultDeliveryStop for the given defaultRouteID
     */
    public ArrayList<DefaultDeliveryStop> getDefaultDeliveryStops(long defaultRouteID) {
        ArrayList<DefaultDeliveryStop> list;
        String sql = String.format("select * from DefaultDeliveryStop where default_route_id = '%s';", defaultRouteID);
        list = (ArrayList<DefaultDeliveryStop>) dbConnection.sendSQL(this, sql, "_formatDefaultDeliveryStop");
        return list;
    }


    /**
     * @param rs takes the ResultSet from database
     * @return list of DefaultDeliveryStop
     */
    public ArrayList<DefaultDeliveryStop> _formatDefaultDeliveryStop(ResultSet rs) {
        ArrayList<DefaultDeliveryStop> tableList = new ArrayList<DefaultDeliveryStop>();
        try {
            while (rs.next()) {
                tableList.add(
                        new DefaultDeliveryStop(
                                rs.getLong("id"),
                                rs.getLong("geo_loc_id")
                        ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}
