package database;

import model.Road;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

/**
 * DBRoad
 * Handles all database functionality for roads.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBRoad {
    
    private DBConnection dbConnection;
    private static DBRoad instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBRoad() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBRoad getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBRoad();            
        }

        return instance;
    }

    public ArrayList<Road> getRoads() {
        ArrayList<Road> list;
        String sql = "select * from Road";
        list = (ArrayList<Road>) dbConnection.sendSQL(this, sql, "_formatRoad");
        return list;
    }

    public ArrayList<Road> _formatDefaultRoute(ResultSet rs) {
        ArrayList<Road> tableList = new ArrayList<>();
        try {
            while (rs.next()) {
                tableList.add(new Road(
                        rs.getLong("from"),
                        rs.getLong("to"),
                        rs.getDouble("distance"),
                        rs.getTime("time")
                ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}