package database;

import model.GeoLoc;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DBGeoLoc
 * Handles all database functionality for GeoLocs.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBGeoLoc {

    private DBConnection dbConnection;
    private static DBGeoLoc instance;

    /**
     * Private constructor for singleton.
     *
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private DBGeoLoc() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    public static DBGeoLoc getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBGeoLoc();
        }

        return instance;
    }

    public GeoLoc getGeoLoc(long defaultDeliveryStopID) {
        ArrayList<GeoLoc> list;
        String sql = String.format("select * from GeoLoc where id = %d",
                defaultDeliveryStopID);
        list = (ArrayList<GeoLoc>) dbConnection.sendSQL(this, sql, "_formatGeoLoc");
        return list.get(0);
    }

    public ArrayList<GeoLoc> _formatGeoLoc(ResultSet rs) {
        ArrayList<GeoLoc> tableList = new ArrayList<>();
        try {
            while (rs.next()) {
                long tmp = rs.getLong("id");
                tableList.add(new GeoLoc(
                        rs.getLong("id"),
                        rs.getDouble("x"),
                        rs.getDouble("y")
                ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}