using System.ServiceModel;
using Model;
using WCFWrapper;

namespace WCFService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceMap" in both code and config file together.
    [ServiceContract]
    public interface IServiceMap
    {
        //List<MapRoute> GetRoadMap(Route route);
        [OperationContract]
        MapRouteWrapper GetRoadMap(Route route);
    }
}
