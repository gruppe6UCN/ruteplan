package database;

import model.Customer;
import model.DefaultDeliveryStop;

import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.AbstractList;
import java.util.ArrayList;

/**
 * DBDefaultDeliveryStop
 * Handles all database functionality for default delivery stops.
 *
 * @author Dennis Værum
 * @version 1.0
 * @since 21-05-15
 */

public class DBCustomer {
    private static DBCustomer instance;
    private static DBConnection dbConnection;

    private DBCustomer() throws SQLException, ClassNotFoundException {
        dbConnection = DBConnection.getInstance();
    }

    public static DBCustomer getInstance() throws SQLException, ClassNotFoundException {
        if (instance == null) {
            instance = new DBCustomer();
        }

        return instance;
    }

    /**
     * @param defaultDeliveryStopID
     * @return list of all Customer for the given defaultDeliveryStopID
     */
    public AbstractList<Customer> getCustomers(long defaultDeliveryStopID) {
        ArrayList<Customer> list;
        String sql = String.format("select * from Customer where default_delivery_stop_id = '%s';", defaultDeliveryStopID);
        list = (ArrayList<Customer>) dbConnection.sendSQL(this, sql, "_formatDefaultDeliveryStop");
        return list;
    }

    /**
     * @param rs takes the ResultSet from database
     * @return list of Customer
     */
    public ArrayList<Customer> _formatDefaultDeliveryStop(ResultSet rs) {
        ArrayList<Customer> tableList = new ArrayList<Customer>();
        try {
            while (rs.next()) {
                tableList.add(
                        new Customer(
                                rs.getLong("id"),
                                rs.getString("street_name"),
                                rs.getString("street_no"),
                                rs.getInt("zip_code"),
                                rs.getString("city")
                        ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}
