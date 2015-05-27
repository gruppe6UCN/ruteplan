package try_stuff;

import org.jgrapht.graph.DefaultWeightedEdge;
import org.jgrapht.graph.DirectedWeightedMultigraph;

public class try_jgrapht {
    public static void main(String[] args) {
        DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> map
                = new DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge>(DefaultWeightedEdge.class);

        GeoLoc A = new GeoLoc(4,0);
        GeoLoc B = new GeoLoc(0,3);
        GeoLoc C = new GeoLoc(0,0);

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

        map.setEdgeWeight(map.addEdge(A, B), 5);
        map.setEdgeWeight(map.addEdge(B, C), 3);
        map.setEdgeWeight(map.addEdge(C, A), 4);

        DefaultWeightedEdge[] t = (DefaultWeightedEdge[])map.getAllEdges(A, B).toArray();
        for (int i = 0; i < t.length; i++) {
            System.out.println();

        }
    }
}