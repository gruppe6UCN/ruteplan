package database;

import model.DefaultRoute;
import model.TrailerType;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DBDefaultRoute
 * Handles all database functionality for default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBDefaultRoute {

    private DBConnection dbConnection;
    private static DBDefaultRoute instance;

    /**
     * Private constructor for singleton.
     *
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private DBDefaultRoute() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    public static DBDefaultRoute getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBDefaultRoute();
        }

        return instance;
    }

    /**
     * @return list of all default routes
     */

	public ArrayList<DefaultRoute> getDefaultRoutes() {
        ArrayList<DefaultRoute> list;
        String sql = "select * from DefaultRoute";
//        list = (ArrayList<DefaultRoute>) dbConnection.sendSQL(this, sql, "_formatDefaultRoute");
        return null;
    }

    /**
     * @param rs takes the ResultSet from database
     * @return list of all default routes
     */
    public ArrayList<DefaultRoute> _formatDefaultRoute(ResultSet rs) {
        ArrayList<DefaultRoute> tableList = new ArrayList<DefaultRoute>();
        try {
            while (rs.next()) {
                tableList.add(new DefaultRoute(
                        rs.getLong("id"),
                        rs.getTime("time_of_departure"),
                        TrailerType.valueOf(rs.getString("trailer_type")),
                        rs.getBoolean("extra_route")
                ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }

    public void storeDefaultRoute(DefaultRoute defaultRoute) {
        String sql = String.format("INSERT into Route values('%s', '%s', %d);",
                defaultRoute.getTrailerType().toString(),
                defaultRoute.getTimeOfDeparture().toString(),
                // inline if statement: if true return 1 else return 0
                defaultRoute.isExtraRoute() ? 1 : 0);
        long defaultRouteID = dbConnection.sendInsertSQL(sql);
        defaultRoute.setId(defaultRouteID);
    }
}
