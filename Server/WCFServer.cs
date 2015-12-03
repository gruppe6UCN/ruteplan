﻿using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using Database;
using WCFService;

namespace Server
{
    public class WCFServer
    {
        private static DBConnection dbInstance;

        private static List<ServiceHost> serviceHosts;

        private static List<Tuple<String, Type, Type>> services = new List<Tuple<String, Type, Type>>()
        {
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/Import/",
                typeof (IServiceImport), typeof (ServiceImport)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/Route/",
                typeof (IServiceRoute), typeof (ServiceRoute)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/ServiceOptimize/",
                typeof (IServiceOptimize), typeof (ServiceOptimize)),
            new Tuple<String, Type, Type>("http://localhost:8733/Design_Time_Addresses/WCFService/ServiceExport/",
                typeof (IServiceExport), typeof (ServiceExport)),
        };

        public static void StartServer()
        {
            if (serviceHosts == null)
            {
                serviceHosts = new List<ServiceHost>();
                BasicHttpBinding binding = new BasicHttpBinding();
                binding.MaxReceivedMessageSize = 2147483647;
                binding.MaxBufferSize = 2147483647;
                binding.MaxBufferPoolSize = 2147483647;

                foreach (Tuple<string, Type, Type> tuple in services)
                {
                    String url = tuple.Item1;
                    Type iType = tuple.Item2;
                    Type type = tuple.Item3;

                    //Instantiate ServiceHost
                    ServiceHost serviceHost = new ServiceHost(type);

                    //Add Endpoint to Host
                    serviceHost.AddServiceEndpoint(iType, binding, url);

                    //Metadata Exchange
                    ServiceMetadataBehavior serviceBehavior = new ServiceMetadataBehavior();
                    serviceHost.Description.Behaviors.Add(serviceBehavior);
                    //serviceBehavior.HttpGetEnabled = true;
                    serviceBehavior.HttpGetUrl = new Uri(url);

                    serviceHosts.Add(serviceHost);

                    //Open
                    serviceHost.Open();
                }

                //InfoFUntime!
                Console.WriteLine("Server Started");
            }
        }

        public static void StopServer()
        {
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

        /// <summary>
        /// Initialize the TestArla database for the the DBConnection.
        /// </summary>
        public static void Initialize()
        {
            dbInstance = DBConnection.Instance;
            dbInstance.Host = "localhost";
            dbInstance.DB = "TestArla";
            dbInstance.User = File.ReadAllText("Config/user.txt");
            dbInstance.Pass = File.ReadAllText("Config/pass.txt");
            dbInstance.Connect();
        }

        /// <summary>
        /// Initialize the TestArlaEmpty database for the the DBConnection.
        /// </summary>
        public static void InitializeEmpty()
        {
            dbInstance = DBConnection.Instance;
            dbInstance.Host = "localhost";
            dbInstance.DB = "TestArlaEmpty";
            dbInstance.User = File.ReadAllText("Config/user.txt");
            dbInstance.Pass = File.ReadAllText("Config/pass.txt");
            dbInstance.Connect();
        }

        public static void Terminate()
        {
            dbInstance.Disconnect();
        }
    }
}
