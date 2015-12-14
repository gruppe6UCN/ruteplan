﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GUI.ServiceMap {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceMap.IServiceMap")]
    public interface IServiceMap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMap/GetRoadMap", ReplyAction="http://tempuri.org/IServiceMap/GetRoadMapResponse")]
        WCFWrapper.MapRouteWrapper GetRoadMap(Model.Route route);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceMap/GetRoadMap", ReplyAction="http://tempuri.org/IServiceMap/GetRoadMapResponse")]
        System.Threading.Tasks.Task<WCFWrapper.MapRouteWrapper> GetRoadMapAsync(Model.Route route);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceMapChannel : GUI.ServiceMap.IServiceMap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceMapClient : System.ServiceModel.ClientBase<GUI.ServiceMap.IServiceMap>, GUI.ServiceMap.IServiceMap {
        
        public ServiceMapClient() {
        }
        
        public ServiceMapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceMapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceMapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public WCFWrapper.MapRouteWrapper GetRoadMap(Model.Route route) {
            return base.Channel.GetRoadMap(route);
        }
        
        public System.Threading.Tasks.Task<WCFWrapper.MapRouteWrapper> GetRoadMapAsync(Model.Route route) {
            return base.Channel.GetRoadMapAsync(route);
        }
    }
}
