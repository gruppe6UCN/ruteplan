﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWCFService.ServiceOptimize {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceOptimize.IServiceOptimize")]
    public interface IServiceOptimize {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/Optimize", ReplyAction="http://tempuri.org/IServiceOptimize/OptimizeResponse")]
        void Optimize();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/Optimize", ReplyAction="http://tempuri.org/IServiceOptimize/OptimizeResponse")]
        System.Threading.Tasks.Task OptimizeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetStatus", ReplyAction="http://tempuri.org/IServiceOptimize/GetStatusResponse")]
        int GetStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetStatus", ReplyAction="http://tempuri.org/IServiceOptimize/GetStatusResponse")]
        System.Threading.Tasks.Task<int> GetStatusAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceOptimizeChannel : TestWCFService.ServiceOptimize.IServiceOptimize, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceOptimizeClient : System.ServiceModel.ClientBase<TestWCFService.ServiceOptimize.IServiceOptimize>, TestWCFService.ServiceOptimize.IServiceOptimize {
        
        public ServiceOptimizeClient() {
        }
        
        public ServiceOptimizeClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceOptimizeClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOptimizeClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceOptimizeClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void Optimize() {
            base.Channel.Optimize();
        }
        
        public System.Threading.Tasks.Task OptimizeAsync() {
            return base.Channel.OptimizeAsync();
        }
        
        public int GetStatus() {
            return base.Channel.GetStatus();
        }
        
        public System.Threading.Tasks.Task<int> GetStatusAsync() {
            return base.Channel.GetStatusAsync();
        }
    }
}