using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Control;

namespace WCFService
{
    public class ServiceImport : IServiceImport
    {
        public void Import()
        {
            ImportController.Instance.ImportRoutes();
        }
    }
}
