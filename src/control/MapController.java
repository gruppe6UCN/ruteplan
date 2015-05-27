package control;

import database.DBGeoLoc;
import database.DBRoad;
import model.DefaultDeliveryStop;
import model.DeliveryStop;
import model.GeoLoc;
import model.Road;
import model.Route;

import org.jgrapht.alg.DijkstraShortestPath;
import org.jgrapht.graph.DefaultWeightedEdge;
import org.jgrapht.graph.DirectedWeightedMultigraph;

import model.*;

import java.sql.SQLException;
import java.util.ArrayList;

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

    private ArrayList<GeoLoc> geoLocs = new ArrayList<>();
    private ArrayList<DefaultWeightedEdge> edges = new ArrayList<>();
    private DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> map
            = new DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge>(DefaultWeightedEdge.class);
	
	/**
	 * Private constructor for singleton.
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
	 * @return instance of class.
	 */
	public static MapController getInstance() {
		if (instance == null) {
			instance = new MapController();			
		}
		
		return instance;
	}	
	
	/**
	 * Loads all map data from the database.
	 */
	public void loadMaps(ArrayList<Route> routes) {
		loadMaps();
//		//Creates an ArrayList for default stops.
//		ArrayList<DefaultDeliveryStop> defaultStops = new ArrayList<>();
//		ArrayList<GeoLoc> geoLocs = new ArrayList<>();
//
//		//Enters a loop for each route.
//        routes.stream().forEach((route) -> {
//
//        	//Enters a loop for each delivery stop.
//        	ArrayList<DeliveryStop> stops = route.getStops();
//        	stops.stream().forEach((stop) -> {
//
//        		//Find the default stop and get its id.
//        		long id = stop.getDefaultStop().getID();
//
//                //Load a GeoLoc from the database.
//                geoLocs.add(dbGeoLoc.getFrom(id));
//
//        	});
//        });

		//Add load road stuff with random libary here later.
	}

	/**
	 * Loads all maps from database, and adds them to map for extended functionality.
	 */
    private void loadMaps() {
        ArrayList<Road> roads = dbRoad.getRoads();

        roads.forEach(road -> {
            GeoLoc geoLocFrom = dbGeoLoc.getGeoLoc(road.getFrom());
            GeoLoc geoLocTo = dbGeoLoc.getGeoLoc(road.getTo());
            if (!geoLocs.contains(geoLocFrom)) {
                geoLocs.add(geoLocFrom);
                map.addVertex(geoLocFrom);
            }
            if (!geoLocs.contains(geoLocTo)) {
                geoLocs.add(geoLocTo);
                map.addVertex(geoLocTo);
            }

            DefaultWeightedEdge edge = map.addEdge(geoLocFrom, geoLocTo);
            map.setEdgeWeight(edge, road.getDistance());
            edges.add(edge);
        });
    }
    
    
    /**
     * Finds the geoLoc for the given deliveryStop.
     * @param stop Delivery stop to find geoLoc for.
     * @return GeoLocation at the given delivery stop.
     */
    public GeoLoc findGeoLoc(DeliveryStop stop) {
    	
    	//Variables to check.
    	double check_id = stop.getID();
    	GeoLoc returnloc = null;
    	
    	//Enters a loop for each geoLoc.
    	for(GeoLoc geoLoc:geoLocs) {
			
    		//Checks to see if id of the geoLoc matches the id of the stop.
    		double geoLoc_id = geoLoc.getDeliveryStopID();
    		if (check_id == geoLoc_id) {
    			
    			//Sets the geoLoc to found. Ends the loop.
    			returnloc = geoLoc;   			
    			break;
    		}
		}
    	
    	//Returns the geoLoc.
    	return returnloc;
    }
    
    
    /**
     * Finds the distance between two points. 
     * @param point1 starting point to search from.
     * @param point2 end point to go to.
     * @return the distance between the two points in double.
     */
    public double pointDistance(GeoLoc point1, GeoLoc point2) {
    	
    	//Uses DijkstraShortestPath algorithm to find the shortest route between the two points.
    	DijkstraShortestPath<GeoLoc, DefaultWeightedEdge> path = new DijkstraShortestPath(map, point1, point2);
    	double length = path.getPathLength();
    	
    	//Returns the distance found.
    	return length;
    }
}
