package control;

import database.DBGeoLoc;
import database.DBRoad;
import model.DeliveryStop;
import model.Edge;
import model.GeoLoc;
import model.Road;
import org.jgrapht.alg.DijkstraShortestPath;
import org.jgrapht.graph.ListenableDirectedWeightedGraph;

import java.io.*;
import java.nio.file.Files;
import java.nio.file.Paths;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;

/**
 * DefaultRouteController
 * Handles all functionality related to default routes.
 *
 * @author Dani Sander
 * @version 1.0
 * @since 22-05-15
 */

public class MapController {

    private static MapController instance;
    private DBGeoLoc dbGeoLoc;
    private DBRoad dbRoad;

    private HashMap<Long, GeoLoc> geoLocs = new HashMap<>();
    private ArrayList<Edge> edges = new ArrayList<>();
    private ListenableDirectedWeightedGraph<GeoLoc, Edge> map;

    /**
     * Private constructor for singleton.
     *
     * @throws SQLException
     * @throws ClassNotFoundException
     */
    private MapController() {
        try {
            dbGeoLoc = DBGeoLoc.getInstance();
        } catch (ClassNotFoundException | SQLException e1) {
            // TODO Auto-generated catch block
            e1.printStackTrace();
        }
        try {
            dbRoad = DBRoad.getInstance();
        } catch (ClassNotFoundException | SQLException e) {
            // TODO Auto-generated catch block
            e.printStackTrace();
        }
    }

    /**
     * Singleton method for class.
     *
     * @return instance of class.
     */
    public static MapController getInstance() {
        if (instance == null) {
            instance = new MapController();
        }

        return instance;
    }

    /**
     * Loads map pre generated map if it exist.
     * If not existing created it.
     * This is much faster.
     */
    public void loadPreGeneratedMap() {
        // TODO: Remove this code
        String file = "map.obj";
        if (new File(file).exists()) {
            ArrayList<Object> backup = null;
            FileInputStream fout = null;
            ObjectInputStream oos = null;
            try {
                fout = new FileInputStream(file);
                oos = new ObjectInputStream(fout);
                backup = (ArrayList<Object>) oos.readObject();
            } catch (java.io.IOException | ClassNotFoundException e) {
                try {
                    Files.deleteIfExists(Paths.get(file));
                } catch (IOException e1) {
                    e1.printStackTrace();
                }
                e.printStackTrace();
            }
            geoLocs = (HashMap<Long, GeoLoc>) backup.get(0);
            map = (ListenableDirectedWeightedGraph<GeoLoc, Edge>) backup.get(1);
            edges = (ArrayList<Edge>) backup.get(2);
        } else {




            generateMap();



            // TODO: Remove this code
            ArrayList<Object> backup = new ArrayList<Object>();
            backup.add(geoLocs);
            backup.add(map);
            backup.add(edges);
            FileOutputStream fout = null;
            ObjectOutputStream oos = null;
            try {
                fout = new FileOutputStream(file);
                oos = new ObjectOutputStream(fout);
                oos.writeObject(backup);
            } catch (java.io.IOException e) {
                e.printStackTrace();
            }
        }
    }


    /**
     * Generate map from the database.
     * This takes time.
     */
    public void generateMap() {
        // remove old data, if there was any
        edges.clear();
        geoLocs.clear();
        map = new ListenableDirectedWeightedGraph<>(Edge.class);

        ArrayList<Road> roads = dbRoad.getRoads();

        roads.forEach(road -> {
            GeoLoc geoLocFrom = dbGeoLoc.getGeoLoc(road.getFrom());
            GeoLoc geoLocTo = dbGeoLoc.getGeoLoc(road.getTo());
            if (!geoLocs.containsKey(geoLocFrom.getID())) {
                geoLocs.put(geoLocFrom.getID(), geoLocFrom);
                map.addVertex(geoLocFrom);
            }
            if (!geoLocs.containsKey(geoLocTo.getID())) {
                geoLocs.put(geoLocTo.getID(), geoLocTo);
                map.addVertex(geoLocTo);
            }

            Edge edge = map.addEdge(
                    geoLocs.get(geoLocFrom.getID()),
                    geoLocs.get(geoLocTo.getID()));
            map.setEdgeWeight(edge, road.getDistance());

            //            DijkstraShortestPath<GeoLoc, DefaultWeightedEdge> path = new DijkstraShortestPath<GeoLoc, DefaultWeightedEdge>(map, geoLocFrom, geoLocTo);
            //            double lenght = path.getPathLength();
            //            System.out.println(lenght);

            edges.add(edge);
        });
    }


    /**
     * Finds the geoLoc for the given deliveryStop.
     *
     * @param stop Delivery stop to find geoLoc for.
     * @return GeoLocation for the given delivery stop.
     */
    public GeoLoc findGeoLoc(DeliveryStop stop) {

        //Variables to check.
        long geoLocID = stop.getDefaultStop().getGeoLocID();

        //Get GeoLoc form HashMap with id for GeoLoc.
        return geoLocs.get(geoLocID);
    }


    /**
     * Finds the distance between two points.
     *
     * @param point1 starting point to search from.
     * @param point2 end point to go to.
     * @return the distance between the two points in double.
     */
    public double getShortestDistance(GeoLoc point1, GeoLoc point2) {

        //Uses DijkstraShortestPath algorithm to find the shortest route between the two points.
        DijkstraShortestPath<GeoLoc, Edge> path = new DijkstraShortestPath<GeoLoc, Edge>(map, point1, point2);
        double length = path.getPathLength();

        //Returns the distance found.
        return length;
    }
}
