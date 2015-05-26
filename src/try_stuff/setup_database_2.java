package try_stuff;

import database.DBConnection;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.sql.Statement;

public class setup_database_2 {

    public static void main(String[] args) throws SQLException, ClassNotFoundException, IOException {
        DBConnection dbConnection = DBConnection.getInstance();
        dbConnection.connect();

        ScriptRunner runner = new ScriptRunner(dbConnection.getConn() , false, true);
        runner.runScript(new BufferedReader(new FileReader("db_scripts/geo_loc.sql")));
    }
}
