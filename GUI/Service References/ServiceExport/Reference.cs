﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI.ServiceExport {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceExport.IServiceExport")]
    public interface IServiceExport {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceExport/Export", ReplyAction="http://tempuri.org/IServiceExport/ExportResponse")]
        void Export();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceExport/Export", ReplyAction="http://tempuri.org/IServiceExport/ExportResponse")]
        System.Threading.Tasks.Task ExportAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceExportChannel : GUI.ServiceExport.IServiceExport, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceExportClient : System.ServiceModel.ClientBase<GUI.ServiceExport.IServiceExport>, GUI.ServiceExport.IServiceExport {
        
        public ServiceExportClient() {
        }
        
        public ServiceExportClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceExportClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceExportClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceExportClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Export() {
            base.Channel.Export();
        }
        
        public System.Threading.Tasks.Task ExportAsync() {
            return base.Channel.ExportAsync();
        }
    }
}