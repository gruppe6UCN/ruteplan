package try_stuff;

import database.DBConnection;

import java.sql.ResultSet;
import java.sql.Statement;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.io.PrintWriter;
import java.sql.SQLException;
import java.util.ArrayList;

public class setup_database_1 {

    public static void main(String[] args) throws SQLException, ClassNotFoundException, IOException {
        DBConnection dbConnection = DBConnection.getInstance();
        dbConnection.connect();

        ScriptRunner runner = new ScriptRunner(dbConnection.getConn() , false, true);
        //runner.runScript(new BufferedReader(new FileReader("db_scripts/create_referances.sql")));
        //runner.runScript(new BufferedReader(new FileReader("db_scripts/default.sql")));

        Statement stat = dbConnection.getConn().createStatement();
        ResultSet rs = stat.executeQuery("select * from Customer");

        PrintWriter writer = new PrintWriter("customer.csv", "UTF-8");
        writer.println(String.format("id,default_delivery_stop_id,street_name,street_no,zip_code,city"));

        while (rs.next()) {
            writer.println(
                    String.format("%d,%d,%s,%d,%d,%s",
                            rs.getLong("id"),
                            rs.getLong("default_delivery_stop_id"),
                            rs.getString("street_name"),
                            rs.getInt("street_no"),
                            rs.getInt("zip_code"),
                            rs.getString("city")
                    )
            );
        }

        writer.close();

    }
}
