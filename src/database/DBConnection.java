package database;

import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.lang.reflect.Method;
import java.nio.file.Files;
import java.nio.file.NoSuchFileException;
import java.nio.file.Path;
import java.nio.file.Paths;
import java.sql.*;

public class DBConnection {

    // Database credentials
    private String DATABASE_NAME = "dmab0914_2Sem_6";

    // JDBC driver name and database URL
    private static String JDBC_DRIVER = "java.sql.Driver";
    private String DB_URL = "jdbc:sqlserver://192.168.1.34;databaseName="
//    private String DB_URL = "jdbc:sqlserver://kraka.ucn.dk;databaseName="
            + DATABASE_NAME;

    private static DBConnection instance = null;
    private static Connection conn = null;

    private DBConnection() throws ClassNotFoundException, SQLException {
//        try {
        // Register JDBC driver
        Class.forName(JDBC_DRIVER);
        try {
            connect();
        } catch (IOException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
//        } catch (ClassNotFoundException e) {
//            e.printStackTrace();
//        } catch (SQLException e) {
//            e.printStackTrace();
//        }
    }

    public static DBConnection getInstance() throws ClassNotFoundException, SQLException {
        if (instance == null) {
            instance = new DBConnection();
        }
        return instance;
    }

    public void connect() throws SQLException, IOException, NoSuchFileException {
        // Open a connection
        Path file_user = Paths.get("username.txt");
        Path file_pass = Paths.get("password.txt");

        String user = new String(Files.readAllBytes(file_user));
        String pass = new String(Files.readAllBytes(file_pass));

        conn = DriverManager.getConnection(DB_URL, user, pass);
    }

    public Connection getConn() throws IOException {
        try {
            if (conn.isClosed()) {
                connect();
            }
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        return conn;
    }

    public void disconnect() {
        try {
            conn.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    public int sendInsertSQL(String sql) {
        ResultSet rs;
        Statement stmt;
        int r = 0;

        try {
            stmt = conn.createStatement();
            r = stmt.executeUpdate(sql);
            rs = stmt.executeQuery("SELECT SCOPE_IDENTITY();");
            rs.next();
            r = rs.getInt(1);
            rs.close();
            stmt.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        // Returns the ID for the new Row
        return r;
    }

    public int sendUpdateSQL(String sql) {
        Statement stmt;
        int r = 0;

        try {
            stmt = conn.createStatement();
            r = stmt.executeUpdate(sql);
            stmt.close();
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }

        // Returns a number equal to the amount of rows that has been change
        return r;
    }

    public Object sendSQL(Object obj, String sql, String method_name) {
        ResultSet rs = null;
        Method method = null;
        Object r = null;
        Statement stmt;

        try {

            try {
                method = obj.getClass().getMethod(method_name, ResultSet.class);
            } catch (NoSuchMethodException e) {
                e.printStackTrace();
            }

            stmt = conn.createStatement();
            try {
                rs = stmt.executeQuery(sql);
                r = method.invoke(obj, rs);
            } catch (IllegalAccessException e) {
                e.printStackTrace();
            } catch (InvocationTargetException e) {
                e.printStackTrace();
            }
            stmt.close();

            return r;
        } catch (SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
        return rs;
    }

}
