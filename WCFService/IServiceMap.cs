using System.Collections.Generic;
using System.ServiceModel;
using GMap.NET;
using Model;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceMap" in both code and config file together.
    [ServiceContract]
    public interface IServiceMap
    {
        [OperationContract]
        //List<MapRoute> GetRoadMap(Route route);
        MapRoute GetRoadMap(Route route);
    }
}
