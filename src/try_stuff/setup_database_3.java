package try_stuff;

import database.DBConnection;
import database.DBGeoLoc;
import database.DBRoad;
import model.Edge;
import model.GeoLoc;
import model.Road;
import org.jgrapht.alg.CycleDetector;
import org.jgrapht.graph.ListenableDirectedWeightedGraph;

import java.awt.geom.Point2D;
import java.io.BufferedReader;
import java.io.FileReader;
import java.io.IOException;
import java.sql.SQLException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Set;

public class setup_database_3 {

    public static void main(String[] args) throws SQLException, ClassNotFoundException, IOException {
        DBConnection dbConnection = DBConnection.getInstance();
        DBRoad dbRoad = DBRoad.getInstance();
        DBGeoLoc dbGeoLoc = DBGeoLoc.getInstance();

        ListenableDirectedWeightedGraph<GeoLoc, Edge> map
                = new ListenableDirectedWeightedGraph<>(Edge.class);

        ScriptRunner runner = new ScriptRunner(dbConnection.getConn(), false, true);
        runner.runScript(new BufferedReader(new FileReader("db_scripts/create_arlas_db.sql")));


        HashMap<Long, GeoLoc> geoLocs = new HashMap<>();
        ArrayList<Edge> edges = new ArrayList<>();
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

        ArrayList<Set<GeoLoc>> cycleOfVertex = new ArrayList<Set<GeoLoc>>();
        geoLocs.forEach((aLong, geoLoc) -> {
            boolean geoLocIsInCycle = cycleOfVertex.stream().anyMatch(geoLocs1 -> geoLocs1.contains(geoLoc));
            ArrayList<Set<GeoLoc>> t = cycleOfVertex;
            if (!geoLocIsInCycle) {
                cycleOfVertex.add(new CycleDetector<GeoLoc, Edge>(map).findCyclesContainingVertex(geoLoc));
            }
        });

        System.out.println(String.format("There is %d cycles", cycleOfVertex.size()));

        if (cycleOfVertex.size() > 1) {
            HashMap<Double, Road> _roads = new HashMap<>();

            cycleOfVertex.stream().forEach(cycleA -> {
                cycleOfVertex.stream().forEach((cycleB) -> {
                    if (cycleA != cycleB) {
                        cycleA.stream().forEach(geoLocA -> {
                            cycleB.stream().forEach(geoLocB -> {
                                Double d = distFrom(geoLocA.getLocation(), geoLocB.getLocation());
                                if (d >= 0.1) {
                                    _roads.put(d, new Road(geoLocA.getID(), geoLocB.getID(), d, null));
                                }
                            });
                        });
                    }
                });
            });

            Object[] distances = _roads.keySet().stream().sorted().toArray();
            for (int i = 0; i < 3; i++) {
                String sql = "INSERT into Road values(%d, %d, %.3f, '%02d:%02d:%02d')";
                Road road = _roads.get((Double) distances[i]);

                long time_in_sec = Math.round(road.getDistance() / (80.0 / 2.0) * 3600.0);
                long hours = time_in_sec / 3600;
                long minutes = (time_in_sec - hours * 3600) / 60;
                long secunds = (time_in_sec - hours * 3600 - minutes * 60);
                dbConnection.sendInsertSQL(
                        String.format(sql, road.getFrom(), road.getTo(), road.getDistance(), hours, minutes, secunds));
                dbConnection.sendInsertSQL(
                        String.format(sql, road.getTo(), road.getFrom(), road.getDistance(), hours, minutes, secunds));

            }
        }

        System.out.println("Jobs Done");
    }

    public static double distFrom(Point2D p1, Point2D p2) {

        double earthRadius = 6371; //kilometers
        double dLat = Math.toRadians(p2.getX() - p1.getX());
        double dLng = Math.toRadians(p2.getY() - p1.getY());
        double a = Math.sin(dLat / 2) * Math.sin(dLat / 2) +
                Math.cos(Math.toRadians(p1.getX())) * Math.cos(Math.toRadians(p2.getX())) *
                        Math.sin(dLng / 2) * Math.sin(dLng / 2);
        double c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
        double dist = (double) (earthRadius * c);

        return dist;
    }
}
