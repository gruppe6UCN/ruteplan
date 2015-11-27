using System;
using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Description;
using WCFService;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            WCFServer.Initialize();
            WCFServer.StartServer();
            Console.Read();
            WCFServer.StopServer();
        }
    }
}
