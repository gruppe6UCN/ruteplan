using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.ServiceModel;

namespace Service
{
    [ServiceContract]
    public interface IImportService
    {
        [OperationContract]
        string Import(); //TODO: Everything... Also not use a string.
    }
}
