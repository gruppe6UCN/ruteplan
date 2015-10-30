using Model;
using Server.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap;

namespace Control
{
    public class MapController
    {
        private static MapController instance;

        private DBGeoLoc dbGeoLoc;
        private DBRoad dbRoad;

        private Dictionary<long, GeoLoc> geoLocs = new Dictionary<long, GeoLoc>();
        private List<Road> edges = new List<Road>();

        private GMap.NET.GMaps gMap;
        public GMap.NET.MapProviders.GMapProvider MapProvider { get { return GMap.NET.MapProviders.OpenStreetMapProvider.Instance; } }

        public static MapController Instance {
            get { 
                if (instance == null)
                    instance = new MapController();
                return instance;
            }
        }

        private MapController() {
            dbGeoLoc = DBGeoLoc.Instance;
            dbRoad = DBRoad.Instance;
            gMap = GMap.NET.GMaps.Instance;
        }

        /**
         * Loads map pre generated map if it exist.
         * If not existing created it.
         * This is much faster.
         */
        public void LoadPreGeneratedMap() {
            // remove old data, if there was any
            edges.Clear();
            geoLocs.Clear();
            GenerateMap();
        }


        /**
         * Generate map from the database.
         * This takes time.
         */
        public void GenerateMap() {
            

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
