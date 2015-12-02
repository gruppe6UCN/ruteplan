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
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExceptionOptimizeInProgress", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
    [System.SerializableAttribute()]
    public partial class ExceptionOptimizeInProgress : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceOptimize.IServiceOptimize")]
    public interface IServiceOptimize {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/Optimize", ReplyAction="http://tempuri.org/IServiceOptimize/OptimizeResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestWCFService.ServiceOptimize.ExceptionOptimizeInProgress), Action="http://tempuri.org/IServiceOptimize/OptimizeExceptionOptimizeInProgressFault", Name="ExceptionOptimizeInProgress", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
        void Optimize();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/Optimize", ReplyAction="http://tempuri.org/IServiceOptimize/OptimizeResponse")]
        System.Threading.Tasks.Task OptimizeAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetProgress", ReplyAction="http://tempuri.org/IServiceOptimize/GetProgressResponse")]
        int GetProgress();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetProgress", ReplyAction="http://tempuri.org/IServiceOptimize/GetProgressResponse")]
        System.Threading.Tasks.Task<int> GetProgressAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetStatus", ReplyAction="http://tempuri.org/IServiceOptimize/GetStatusResponse")]
        string GetStatus();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceOptimize/GetStatus", ReplyAction="http://tempuri.org/IServiceOptimize/GetStatusResponse")]
        System.Threading.Tasks.Task<string> GetStatusAsync();
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
        
        public int GetProgress() {
            return base.Channel.GetProgress();
        }
        
        public System.Threading.Tasks.Task<int> GetProgressAsync() {
            return base.Channel.GetProgressAsync();
        }
        
        public string GetStatus() {
            return base.Channel.GetStatus();
        }
        
        public System.Threading.Tasks.Task<string> GetStatusAsync() {
            return base.Channel.GetStatusAsync();
        }
    }
}
