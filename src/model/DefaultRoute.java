package model;

public class DefaultRoute {
    
    private long id;
    private TrailerType trailerType;
    private boolean extraRoute;

    public DefaultRoute(long id, TrailerType trailerType, boolean extraRoute) {
        this.id = id;
        this.trailerType = trailerType;
        this.extraRoute = extraRoute;
    }

    public DefaultRoute(TrailerType trailerType, boolean extraRoute) {
        this.trailerType = trailerType;
        this.extraRoute = extraRoute;
    }
    
    /**
     * @return the id
     */
    public long getID() {
        return id;
    }

    /**
     * @param id the id to set
     */
    public void setId(long id) {
        this.id = id;
    }

    /**
     * @return the trailerType
     */
    public TrailerType getTrailerType() {
        return trailerType;
    }

    /**
     * @return the extraRoute
     */
    public boolean isExtraRoute() {
        return extraRoute;
    }
    

}
