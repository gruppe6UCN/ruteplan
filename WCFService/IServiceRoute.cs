using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace WCFService
{
    [ServiceContract()]
    public interface IServiceRoute
    {
        [OperationContract()]
        [FaultContract(typeof(ExceptionNoRoutes))]
        Route GetRoutes();
    }

    [DataContract()]
    public class ExceptionNoRoutes
    {
        public ExceptionNoRoutes(String reason)
        {
            this.Message = reason;
        }

        [DataMember]
        public string Message { get; set; }
    }
}
