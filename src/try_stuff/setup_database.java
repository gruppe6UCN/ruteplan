package try_stuff;

import database.DBConnection;

import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLException;


public class setup_database {
    public static void main(String[] args) throws SQLException, ClassNotFoundException, IOException {
        DBConnection dbConnection = DBConnection.getInstance();
        dbConnection.connect();

        ScriptRunner runner = new ScriptRunner(dbConnection.getConn() , false, true);
        runner.runScript(new BufferedReader(new FileReader("db_scripts/create_referances.sql")));
//        runner.runScript(new BufferedReader(new FileReader("db_scripts/default.sql")));
    }
}
