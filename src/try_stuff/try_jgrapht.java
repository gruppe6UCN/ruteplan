package try_stuff;

import model.GeoLoc;
import org.jgrapht.alg.DijkstraShortestPath;
import org.jgrapht.graph.DefaultWeightedEdge;
import org.jgrapht.graph.DirectedWeightedMultigraph;

public class try_jgrapht {
    public static void main(String[] args) {
        DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> map
                = new DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge>(DefaultWeightedEdge.class);


        GeoLoc A = new GeoLoc(0,4,0);
        GeoLoc B = new GeoLoc(1,0,3);
        GeoLoc C = new GeoLoc(2,0,0);

        /**
         * C---B
         * |  /
         * | /
         * |/
         * A
         */

        map.addVertex(A);
        map.addVertex(B);
        map.addVertex(C);

        map.setEdgeWeight(map.addEdge(A, B), 8);
        map.setEdgeWeight(map.addEdge(B, A), 8);
        map.setEdgeWeight(map.addEdge(B, C), 3);
        map.setEdgeWeight(map.addEdge(C, B), 3);
        map.setEdgeWeight(map.addEdge(C, A), 4);
        map.setEdgeWeight(map.addEdge(A, C), 4);

        DijkstraShortestPath<GeoLoc, DefaultWeightedEdge> path = new DijkstraShortestPath<GeoLoc, DefaultWeightedEdge>(map, A, B);
        System.out.println(path.getPathLength());
    }
}