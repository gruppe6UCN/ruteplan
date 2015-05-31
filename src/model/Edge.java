package model;

import org.jgrapht.graph.DefaultWeightedEdge;

/**
 * Created by alt_mulig on 5/31/15.
 */
public class Edge extends DefaultWeightedEdge {
    @Override
    public double getWeight() {
        return super.getWeight();
    }

    @Override
    public Object getSource() {
        return super.getSource();
    }

    @Override
    public Object getTarget() {
        return super.getTarget();
    }

    @Override
    public String toString() {
        return String.valueOf(getWeight());
    }
}
