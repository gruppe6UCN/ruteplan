using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    [ServiceContract]
    public interface IServiceOptimize
    {
        [OperationContract]
        void Optimize();
        [OperationContract]
        int GetStatus();
    }

    [DataContract()]
    public class ExceptionOptimizeInProgress
    {
        private String _message;
        public ExceptionOptimizeInProgress(String reason)
        {
            this.Message = reason;
        }

        [DataMember]
        public string Message { get { return _message; } set { _message = value; } }
    }
}
