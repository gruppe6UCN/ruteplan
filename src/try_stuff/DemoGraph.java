package try_stuff;

import com.mxgraph.layout.mxCircleLayout;
import com.mxgraph.layout.mxIGraphLayout;
import com.mxgraph.swing.mxGraphComponent;
import control.MapController;
import model.Edge;
import model.GeoLoc;
import org.jgrapht.ext.JGraphXAdapter;
import org.jgrapht.graph.DefaultWeightedEdge;
import org.jgrapht.graph.DirectedWeightedMultigraph;
import org.jgrapht.graph.ListenableDirectedWeightedGraph;

import javax.swing.*;

class DemoGraph {

    private static void createAndShowGui() {
        JFrame frame = new JFrame("DemoGraph");
        frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);

//        ListenableGraph<String, MyEdge> g = buildGraph();
//        DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> g  = buildGraph();
        MapController.getInstance().loadMap(null);
        ListenableDirectedWeightedGraph<GeoLoc, Edge> g = MapController.getInstance().getMap();
        JGraphXAdapter<GeoLoc, Edge> graphAdapter =
                new JGraphXAdapter<GeoLoc, Edge>(g);

        mxIGraphLayout layout = new mxCircleLayout(graphAdapter);
        layout.execute(graphAdapter.getDefaultParent());

        frame.add(new mxGraphComponent(graphAdapter));

        frame.pack();
        frame.setLocationByPlatform(true);
        frame.setVisible(true);
    }

    public static void main(String[] args) {
        SwingUtilities.invokeLater(new Runnable() {
            public void run() {
                createAndShowGui();
            }
        });
    }

    public static class MyEdge extends DefaultWeightedEdge {
        @Override
        public String toString() {
            return String.valueOf(getWeight());
        }
    }

    public static DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> buildGraph() {
        DirectedWeightedMultigraph<GeoLoc, DefaultWeightedEdge> g
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

        g.addVertex(A);
        g.addVertex(B);
        g.addVertex(C);

        g.setEdgeWeight(g.addEdge(A, B), 8);
        g.setEdgeWeight(g.addEdge(B, A), 8);
        g.setEdgeWeight(g.addEdge(B, C), 3);
        g.setEdgeWeight(g.addEdge(C, B), 3);
        g.setEdgeWeight(g.addEdge(C, A), 4);
        g.setEdgeWeight(g.addEdge(A, C), 4);

        return g;
    }
}