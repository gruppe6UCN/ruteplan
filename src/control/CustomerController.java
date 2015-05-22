package control;

/**
 * Created by alt_mulig on 5/21/15.
 */

import database.DBCustomer;
import model.Customer;
import model.DefaultDeliveryStop;

import java.sql.SQLException;
import java.util.ArrayList;

/**
 * Customer Controller
 * Handles all functionality related to customer.
 *
 * @author Dennis Værum
 * @version 1.0
 * @since 21-05-15
 */

public class CustomerController {
    private DBCustomer dbCustomer;
    private static CustomerController instance;


    /**
     * Private constructor for singleton.
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private CustomerController() throws SQLException, ClassNotFoundException {
        dbCustomer = DBCustomer.getInstance();
    }


    /**
     * Singleton method for class.
     * @return instance of class.
     */
    public static CustomerController getInstance() throws SQLException, ClassNotFoundException {
        if (instance == null) {
            instance = new CustomerController();
        }

        return instance;
    }

    public void addCustomers(DefaultDeliveryStop defaultStop) {
        defaultStop.setCustomers(
                dbCustomer.getCustomers(
                        defaultStop.getID()
                ));
    }
}
