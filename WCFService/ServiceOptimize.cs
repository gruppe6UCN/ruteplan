using Control;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading;

namespace WCFService
{
    public class ServiceOptimize : IServiceOptimize
    {
        private Thread threadOptimize = new Thread(OptimizeController.Instance.Optimize);
        public void Optimize()
        {
            if (threadOptimize.IsAlive == false)
            {

                threadOptimize = new Thread(OptimizeController.Instance.Optimize);
                threadOptimize.Start();
            }
            else
            {
                throw new FaultException<ExceptionOptimizeInProgress>(new ExceptionOptimizeInProgress("Optimize is already in progress. Wait for it to finish."));
            }
        }

        public int GetProgress()
        {
            int status = OptimizeController.Instance.GetStatus();
            return status;
        }

        public string GetStatus()
        {
            string status = LogController.Instance.GetLatest();
            return status;
        }
    }
}
