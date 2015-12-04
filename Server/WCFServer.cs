using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using Database;
using WCFService;

namespace Server
{
    public class WCFServer
    {
        private static List<ServiceHost> serviceHosts;

        // List containing all needed info for start our WCF services
        private static List<Tuple<String, Type, Type>> services = new List<Tuple<String, Type, Type>>()
        {
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/Route/",
                typeof (IServiceRoute), typeof (ServiceRoute)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/ServiceOptimize/",
                typeof (IServiceOptimize), typeof (ServiceOptimize)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/ServiceMap/",
                typeof (IServiceMap), typeof (ServiceMap)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/ServiceExport/",
                typeof (IServiceExport), typeof (ServiceExport)),
        };

        public static void StartServer()
        {
            // if true then the server is not running
            if (serviceHosts == null)
            {
                serviceHosts = new List<ServiceHost>();
                BasicHttpBinding binding = new BasicHttpBinding();

                foreach (Tuple<string, Type, Type> tuple in services)
                {
                    String url = tuple.Item1;
                    Type serviceInterface = tuple.Item2;
                    Type serviceClass = tuple.Item3;

                    //Instantiate ServiceHost
                    ServiceHost serviceHost = new ServiceHost(serviceClass);

                    //Add Endpoint to Host
                    serviceHost.AddServiceEndpoint(serviceInterface, binding, url);

                    //Metadata Exchange
                    ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                    serviceHost.Description.Behaviors.Add(serviceBehavior);
                    //serviceBehavior.HttpGetEnabled = true;
                    serviceBehavior.HttpGetUrl = new Uri(url);

                    serviceHosts.Add(serviceHost);

                    //Open
                    serviceHost.Open();
                }
            }
        }

        public static void StopServer()
        {
            // true then the server is running
            if (serviceHosts != null)
            {
                foreach (ServiceHost serviceHost in serviceHosts)
                {
                    //Close
                    serviceHost.Close();
                }

                serviceHosts = null;
            }
        }
    }
}
