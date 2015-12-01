using Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WCFService
{
    public class ServiceOptimize : IServiceOptimize
    {
        public void Optimize()
        {
            OptimizeController.Instance.Optimize();
        }
    }
}
