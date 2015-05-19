package test.database;

import org.junit.BeforeClass;
import org.junit.AfterClass;
import org.junit.Test;
import org.junit.Before;
import org.junit.After;

import database.DBConnection;

import java.io.IOException;
import java.nio.file.NoSuchFileException;
import java.sql.SQLException;

import static org.junit.Assert.*;

/**
 * DBConnection Tester.
 *
 * @author <Authors name>
 * @version 1.0
 * @since <pre>May 19, 2015</pre>
 */
public class DBConnectionTest {
    private static DBConnection instance = null;

    @BeforeClass
    public static void setUpBeforeClass() throws Exception {
        try {
            instance = DBConnection.getInstance();
            instance.connect();
        } catch (ClassNotFoundException | SQLException | NoSuchFileException e) {
        }
    }

    @Before
    public void before() throws Exception {
    }

    @After
    public void after() throws Exception {
    }

    @AfterClass
    public static void tearDownAfterClass() throws Exception {
        try {
            instance.sendUpdateSQL("delete from Customer;");
        } catch (NullPointerException e) {
        }
        instance.disconnect();
    }

    /**
     * Method: getInstance()
     */
    @Test
    public void testGetInstance() throws Exception {
        try {
            DBConnection instance = DBConnection.getInstance();
            assertNotNull(instance);
        } catch (ClassNotFoundException e) {
            // TODO Auto-generated catch block
//			e.printStackTrace();
            fail("Lib: '" + e.getMessage() + "' is missing");
        } catch (SQLException e) {
            // TODO Auto-generated catch block
//			e.printStackTrace();
            fail("kunne ikke forbinde til databasen" + e.getMessage());
        }
    }

    /**
     * Method: connect()
     */
    @Test
    public void testConnect() throws Exception {
        try {
            DBConnection.getInstance().connect();
        } catch (SQLException e) {
            fail(e.getMessage());
        } catch (NoSuchFileException e) {
            fail("The file '" + e.getFile() + "' does not exist at '" + System.getProperty("user.dir") + "'");
        } catch (IOException e) {
            fail(e.getMessage());
        }
    }

    /**
     * Method: getConn()
     */
    @Test
    public void testGetConn() throws Exception {
        //TODO: Test goes here...
    }

    /**
     * Method: disconnect()
     */
    @Test
    public void testDisconnect() throws Exception {
        //TODO: Test goes here...
    }

    /**
     * Method: sendInsertSQL(String sql)
     */
    @Test
    public void testSendInsertSQL() throws Exception {
        int r = instance.sendInsertSQL("INSERT into Customer values('Muahaha','Like','1337','awesome','42884242','private');");
        assertTrue(r > 0);
    }

    /**
     * Method: sendUpdateSQL(String sql)
     */
    @Test
    public void testSendUpdateSQL() throws Exception {
        //TODO: Test goes here...
    }

    /**
     * Method: sendSQL(Object obj, String sql, String method_name)
     */
    @Test
    public void testSendSQL() throws Exception {
        //TODO: Test goes here...
    }
} 
