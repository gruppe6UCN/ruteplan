using System.Collections.Generic;
using Control;
using GMap.NET;
using Model;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceMap" in both code and config file together.
    public class ServiceMap : IServiceMap
    {
        //public List<MapRoute> GetRoadMap(Route route)
        public MapRoute GetRoadMap(Route route)
        {
            return MapController.Instance.GetCalcRoad(route)[0];
        }
    }
}
