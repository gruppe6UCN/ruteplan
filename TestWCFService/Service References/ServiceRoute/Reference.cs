﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TestWCFService.ServiceRoute {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Route", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class Route : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.DateTime DateForDepartureField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private TestWCFService.ServiceRoute.DefaultRoute DefaultRouteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.TimeSpan TimeForDepartureField;
        
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
        public System.DateTime DateForDeparture {
            get {
                return this.DateForDepartureField;
            }
            set {
                if ((this.DateForDepartureField.Equals(value) != true)) {
                    this.DateForDepartureField = value;
                    this.RaisePropertyChanged("DateForDeparture");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public TestWCFService.ServiceRoute.DefaultRoute DefaultRoute {
            get {
                return this.DefaultRouteField;
            }
            set {
                if ((object.ReferenceEquals(this.DefaultRouteField, value) != true)) {
                    this.DefaultRouteField = value;
                    this.RaisePropertyChanged("DefaultRoute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.TimeSpan TimeForDeparture {
            get {
                return this.TimeForDepartureField;
            }
            set {
                if ((this.TimeForDepartureField.Equals(value) != true)) {
                    this.TimeForDepartureField = value;
                    this.RaisePropertyChanged("TimeForDeparture");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="DefaultRoute", Namespace="http://schemas.datacontract.org/2004/07/Model")]
    [System.SerializableAttribute()]
    public partial class DefaultRoute : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool ExtraRouteField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private long IDField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private double TrailerTypeField;
        
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
        public bool ExtraRoute {
            get {
                return this.ExtraRouteField;
            }
            set {
                if ((this.ExtraRouteField.Equals(value) != true)) {
                    this.ExtraRouteField = value;
                    this.RaisePropertyChanged("ExtraRoute");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public long ID {
            get {
                return this.IDField;
            }
            set {
                if ((this.IDField.Equals(value) != true)) {
                    this.IDField = value;
                    this.RaisePropertyChanged("ID");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public double TrailerType {
            get {
                return this.TrailerTypeField;
            }
            set {
                if ((this.TrailerTypeField.Equals(value) != true)) {
                    this.TrailerTypeField = value;
                    this.RaisePropertyChanged("TrailerType");
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
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ExceptionNoRoutes", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
    [System.SerializableAttribute()]
    public partial class ExceptionNoRoutes : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ServiceRoute.IServiceRoute")]
    public interface IServiceRoute {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRoute/GetRoutes", ReplyAction="http://tempuri.org/IServiceRoute/GetRoutesResponse")]
        [System.ServiceModel.FaultContractAttribute(typeof(TestWCFService.ServiceRoute.ExceptionNoRoutes), Action="http://tempuri.org/IServiceRoute/GetRoutesExceptionNoRoutesFault", Name="ExceptionNoRoutes", Namespace="http://schemas.datacontract.org/2004/07/WCFService")]
        TestWCFService.ServiceRoute.Route[] GetRoutes();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IServiceRoute/GetRoutes", ReplyAction="http://tempuri.org/IServiceRoute/GetRoutesResponse")]
        System.Threading.Tasks.Task<TestWCFService.ServiceRoute.Route[]> GetRoutesAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IServiceRouteChannel : TestWCFService.ServiceRoute.IServiceRoute, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ServiceRouteClient : System.ServiceModel.ClientBase<TestWCFService.ServiceRoute.IServiceRoute>, TestWCFService.ServiceRoute.IServiceRoute {
        
        public ServiceRouteClient() {
        }
        
        public ServiceRouteClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ServiceRouteClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRouteClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ServiceRouteClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public TestWCFService.ServiceRoute.Route[] GetRoutes() {
            return base.Channel.GetRoutes();
        }
        
        public System.Threading.Tasks.Task<TestWCFService.ServiceRoute.Route[]> GetRoutesAsync() {
            return base.Channel.GetRoutesAsync();
        }
    }
}
