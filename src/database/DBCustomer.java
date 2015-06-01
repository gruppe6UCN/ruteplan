package database;

import model.Customer;

import java.sql.ResultSet;
import java.sql.SQLException;
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
    private DBConnection dbConnection;
    private static DBCustomer instance;

    /**
     * Private constructor for singleton
     *
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private DBCustomer() throws SQLException, ClassNotFoundException {
        dbConnection = DBConnection.getInstance();
    }

    /**
     * Singleton method for class
     *
     * @return the instance of DBCustomer
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    public static DBCustomer getInstance() throws SQLException, ClassNotFoundException {
        if (instance == null) {
            instance = new DBCustomer();
        }

        return instance;
    }

    /**
     * @param id from a DefaultDeliveryStop
     * @return list of all Customer for the given id
     */
    public ArrayList<Customer> getCustomers(long id) {
        ArrayList<Customer> list;
        String sql = String.format("select * from Customer where default_delivery_stop_id = '%s';", id);
        list = (ArrayList<Customer>) dbConnection.sendSQL(this, sql, "_formatCustomer");
        return list;
    }

    /**
     * @param rs takes the ResultSet from database
     * @return list of Customer
     */
    public ArrayList<Customer> _formatCustomer(ResultSet rs) {
        ArrayList<Customer> tableList = new ArrayList<>();
        try {
            while (rs.next()) {
                tableList.add(
                        new Customer(
                                rs.getLong("id"),
                                rs.getString("street_name"),
                                rs.getString("street_no"),
                                rs.getInt("zip_code"),
                                rs.getString("city"),
                                rs.getTime("time_of_delivery").toLocalTime()
                        ));
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return tableList;
    }
}
