package database;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import model.*;

/**
 * DBTransportUnit
 * Handles all database functionality for default transport units.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 20-05-15
 */

public class DBTransportUnit {
    
    private DBConnection dbConnection;
    private static DBTransportUnit instance;
    
    /**
     * Private constructor for singleton.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    private DBTransportUnit() throws ClassNotFoundException, SQLException {
        dbConnection = DBConnection.getInstance();        
    }
    
    /**
     * Singleton method for class.
     * @return instance of class.
     * @throws SQLException 
     * @throws ClassNotFoundException 
     */
    public static DBTransportUnit getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBTransportUnit();            
        }
        
        return instance;
    }

    /**
     *
     * @param IDs
     * @return
     */
    public ArrayList<TransportUnit> getTransportUnits(ArrayList<Long> IDs) {
        ArrayList<TransportUnit> list = new ArrayList<>();

        IDs.stream().forEach((ID) -> {
            ArrayList<TransportUnit> tmp_list;
            String sql = String.format("select * from Customer where customer_id = '%s';", ID);
            tmp_list = (ArrayList<TransportUnit>) dbConnection.sendSQL(this, sql, "_formatTransportUnit");

            tmp_list.forEach((tmp) -> list.add(tmp));
        });

        return list;
    }

    /**
     * @param rs takes the ResultSet from database
     * @return list of TransportUnit
     */
    public ArrayList<TransportUnit> _formatTransportUnit(ResultSet rs) {
        ArrayList<TransportUnit> tableList = new ArrayList<>();
        try {
            while (rs.next()) {
                tableList.add(
                        new TransportUnit(
                                rs.getLong("id"),
                                rs.getLong("customer_id"),
                                Type.valueOf(rs.getString("type"))
                        ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}
