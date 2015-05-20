package database;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.ArrayList;

import model.*;

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
	 * @throws SQLException 
	 * @throws ClassNotFoundException 
	 */
	private DBDefaultDeliveryStop() throws ClassNotFoundException, SQLException {
		dbConnection = DBConnection.getInstance();		
	}
	
	/**
	 * Singleton method for class.
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

	public ArrayList<DefaultDeliveryStop> getDefaultRoutes(long defaultRouteID) {
		ArrayList<DefaultDeliveryStop> list;
		String sql = String.format("select * from Customer where id = '%s';", defaultRouteID);
		list = (ArrayList<DefaultDeliveryStop>) dbConnection.sendSQL(this , sql, "_formatDefaultDeliveryStop");
		return list;
	}

	public ArrayList<DefaultDeliveryStop> _formatDefaultDeliveryStop(ResultSet rs) {
		ArrayList<DefaultDeliveryStop> tableList = new ArrayList<DefaultDeliveryStop>();
		try {
			while (rs.next()) {
				tableList.add(new DefaultDeliveryStop(
						rs.getLong("id"),
						rs.getTime("time_of_delivery")));
			}
		} catch (SQLException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return tableList;
	}
}
