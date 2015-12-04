using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Control;

namespace WCFService
{
    public class ServiceExport : IServiceExport
    {
        public void Export()
        {
            ExportController.Instance.ExportData();
        }
    }
}
