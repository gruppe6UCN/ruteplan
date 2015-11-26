using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFService;

namespace Server
{
    public class Program
    {
        private static ServiceHost serviceRoute;
        static public void StartServer()
        {
            if (serviceRoute == null || serviceRoute.State == CommunicationState.Opened)
            {
                //Base Address for StudentService
                Uri httpBaseAddress = new Uri("http://localhost:8733/Design_Time_Addresses/WCFService/Route/");
                
                //Instantiate ServiceHost
                serviceRoute = new ServiceHost(typeof(ServiceRoute),
                    httpBaseAddress);
 
                //Add Endpoint to Host
                serviceRoute.AddServiceEndpoint(typeof(IServiceRoute), new BasicHttpBinding(), "");            
 
                //Metadata Exchange
                ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                serviceBehavior.HttpGetEnabled = true;
                serviceRoute.Description.Behaviors.Add(serviceBehavior);

                //Open
                serviceRoute.Open();
            }
        }
        static public void StopServer()
        {
            if (serviceRoute != null && serviceRoute.State != CommunicationState.Opened)
            {
                //Close
                serviceRoute.Close();
            }
        }

        static void Main(string[] args)
        {
            StartServer();
            Console.Read();
            StopServer();
        }
    }
}
