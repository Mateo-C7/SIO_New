﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Microsoft.VSDesigner generó automáticamente este código fuente, versión=4.0.30319.42000.
// 
#pragma warning disable 1591

namespace SIO.com.siesacloud.wsforsa {
    using System.Diagnostics;
    using System;
    using System.Xml.Serialization;
    using System.ComponentModel;
    using System.Web.Services.Protocols;
    using System.Web.Services;
    using System.Data;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Web.Services.WebServiceBindingAttribute(Name="WSUNOEESoap", Namespace="http://tempuri.org/")]
    public partial class WSUNOEE : System.Web.Services.Protocols.SoapHttpClientProtocol {
        
        private System.Threading.SendOrPostCallback CrearConexionXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback EjecutarConsultaXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback LeerEsquemaParametrosOperationCompleted;
        
        private System.Threading.SendOrPostCallback SiesaCFDOperationCompleted;
        
        private System.Threading.SendOrPostCallback ImportarXMLOperationCompleted;
        
        private System.Threading.SendOrPostCallback InicializarVariablesImportacionOperationCompleted;
        
        private System.Threading.SendOrPostCallback SiesaWEBContabilizarOperationCompleted;
        
        private bool useDefaultCredentialsSetExplicitly;
        
        /// <remarks/>
        public WSUNOEE() {
            this.Url = global::SIO.Properties.Settings.Default.SIO_com_siesacloud_wsforsa_WSUNOEE;
            if ((this.IsLocalFileSystemWebService(this.Url) == true)) {
                this.UseDefaultCredentials = true;
                this.useDefaultCredentialsSetExplicitly = false;
            }
            else {
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        public new string Url {
            get {
                return base.Url;
            }
            set {
                if ((((this.IsLocalFileSystemWebService(base.Url) == true) 
                            && (this.useDefaultCredentialsSetExplicitly == false)) 
                            && (this.IsLocalFileSystemWebService(value) == false))) {
                    base.UseDefaultCredentials = false;
                }
                base.Url = value;
            }
        }
        
        public new bool UseDefaultCredentials {
            get {
                return base.UseDefaultCredentials;
            }
            set {
                base.UseDefaultCredentials = value;
                this.useDefaultCredentialsSetExplicitly = true;
            }
        }
        
        /// <remarks/>
        public event CrearConexionXMLCompletedEventHandler CrearConexionXMLCompleted;
        
        /// <remarks/>
        public event EjecutarConsultaXMLCompletedEventHandler EjecutarConsultaXMLCompleted;
        
        /// <remarks/>
        public event LeerEsquemaParametrosCompletedEventHandler LeerEsquemaParametrosCompleted;
        
        /// <remarks/>
        public event SiesaCFDCompletedEventHandler SiesaCFDCompleted;
        
        /// <remarks/>
        public event ImportarXMLCompletedEventHandler ImportarXMLCompleted;
        
        /// <remarks/>
        public event InicializarVariablesImportacionCompletedEventHandler InicializarVariablesImportacionCompleted;
        
        /// <remarks/>
        public event SiesaWEBContabilizarCompletedEventHandler SiesaWEBContabilizarCompleted;
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/CrearConexionXML", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public bool CrearConexionXML(string pvstrxmlConexion) {
            object[] results = this.Invoke("CrearConexionXML", new object[] {
                        pvstrxmlConexion});
            return ((bool)(results[0]));
        }
        
        /// <remarks/>
        public void CrearConexionXMLAsync(string pvstrxmlConexion) {
            this.CrearConexionXMLAsync(pvstrxmlConexion, null);
        }
        
        /// <remarks/>
        public void CrearConexionXMLAsync(string pvstrxmlConexion, object userState) {
            if ((this.CrearConexionXMLOperationCompleted == null)) {
                this.CrearConexionXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnCrearConexionXMLOperationCompleted);
            }
            this.InvokeAsync("CrearConexionXML", new object[] {
                        pvstrxmlConexion}, this.CrearConexionXMLOperationCompleted, userState);
        }
        
        private void OnCrearConexionXMLOperationCompleted(object arg) {
            if ((this.CrearConexionXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.CrearConexionXMLCompleted(this, new CrearConexionXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/EjecutarConsultaXML", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet EjecutarConsultaXML(string pvstrxmlParametros) {
            object[] results = this.Invoke("EjecutarConsultaXML", new object[] {
                        pvstrxmlParametros});
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void EjecutarConsultaXMLAsync(string pvstrxmlParametros) {
            this.EjecutarConsultaXMLAsync(pvstrxmlParametros, null);
        }
        
        /// <remarks/>
        public void EjecutarConsultaXMLAsync(string pvstrxmlParametros, object userState) {
            if ((this.EjecutarConsultaXMLOperationCompleted == null)) {
                this.EjecutarConsultaXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnEjecutarConsultaXMLOperationCompleted);
            }
            this.InvokeAsync("EjecutarConsultaXML", new object[] {
                        pvstrxmlParametros}, this.EjecutarConsultaXMLOperationCompleted, userState);
        }
        
        private void OnEjecutarConsultaXMLOperationCompleted(object arg) {
            if ((this.EjecutarConsultaXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.EjecutarConsultaXMLCompleted(this, new EjecutarConsultaXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/LeerEsquemaParametros", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public string LeerEsquemaParametros(string pvstrxmlParametros) {
            object[] results = this.Invoke("LeerEsquemaParametros", new object[] {
                        pvstrxmlParametros});
            return ((string)(results[0]));
        }
        
        /// <remarks/>
        public void LeerEsquemaParametrosAsync(string pvstrxmlParametros) {
            this.LeerEsquemaParametrosAsync(pvstrxmlParametros, null);
        }
        
        /// <remarks/>
        public void LeerEsquemaParametrosAsync(string pvstrxmlParametros, object userState) {
            if ((this.LeerEsquemaParametrosOperationCompleted == null)) {
                this.LeerEsquemaParametrosOperationCompleted = new System.Threading.SendOrPostCallback(this.OnLeerEsquemaParametrosOperationCompleted);
            }
            this.InvokeAsync("LeerEsquemaParametros", new object[] {
                        pvstrxmlParametros}, this.LeerEsquemaParametrosOperationCompleted, userState);
        }
        
        private void OnLeerEsquemaParametrosOperationCompleted(object arg) {
            if ((this.LeerEsquemaParametrosCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.LeerEsquemaParametrosCompleted(this, new LeerEsquemaParametrosCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SiesaCFD", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public object SiesaCFD(short pvintcia, string pvstrCo, string pvstrTipoDocto, long pvintConsecInicial, long pvintConsecFinal, string pvintConexion, long pvintTimeOut) {
            object[] results = this.Invoke("SiesaCFD", new object[] {
                        pvintcia,
                        pvstrCo,
                        pvstrTipoDocto,
                        pvintConsecInicial,
                        pvintConsecFinal,
                        pvintConexion,
                        pvintTimeOut});
            return ((object)(results[0]));
        }
        
        /// <remarks/>
        public void SiesaCFDAsync(short pvintcia, string pvstrCo, string pvstrTipoDocto, long pvintConsecInicial, long pvintConsecFinal, string pvintConexion, long pvintTimeOut) {
            this.SiesaCFDAsync(pvintcia, pvstrCo, pvstrTipoDocto, pvintConsecInicial, pvintConsecFinal, pvintConexion, pvintTimeOut, null);
        }
        
        /// <remarks/>
        public void SiesaCFDAsync(short pvintcia, string pvstrCo, string pvstrTipoDocto, long pvintConsecInicial, long pvintConsecFinal, string pvintConexion, long pvintTimeOut, object userState) {
            if ((this.SiesaCFDOperationCompleted == null)) {
                this.SiesaCFDOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSiesaCFDOperationCompleted);
            }
            this.InvokeAsync("SiesaCFD", new object[] {
                        pvintcia,
                        pvstrCo,
                        pvstrTipoDocto,
                        pvintConsecInicial,
                        pvintConsecFinal,
                        pvintConexion,
                        pvintTimeOut}, this.SiesaCFDOperationCompleted, userState);
        }
        
        private void OnSiesaCFDOperationCompleted(object arg) {
            if ((this.SiesaCFDCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SiesaCFDCompleted(this, new SiesaCFDCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/ImportarXML", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public System.Data.DataSet ImportarXML(string pvstrDatos, ref short printTipoError) {
            object[] results = this.Invoke("ImportarXML", new object[] {
                        pvstrDatos,
                        printTipoError});
            printTipoError = ((short)(results[1]));
            return ((System.Data.DataSet)(results[0]));
        }
        
        /// <remarks/>
        public void ImportarXMLAsync(string pvstrDatos, short printTipoError) {
            this.ImportarXMLAsync(pvstrDatos, printTipoError, null);
        }
        
        /// <remarks/>
        public void ImportarXMLAsync(string pvstrDatos, short printTipoError, object userState) {
            if ((this.ImportarXMLOperationCompleted == null)) {
                this.ImportarXMLOperationCompleted = new System.Threading.SendOrPostCallback(this.OnImportarXMLOperationCompleted);
            }
            this.InvokeAsync("ImportarXML", new object[] {
                        pvstrDatos,
                        printTipoError}, this.ImportarXMLOperationCompleted, userState);
        }
        
        private void OnImportarXMLOperationCompleted(object arg) {
            if ((this.ImportarXMLCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.ImportarXMLCompleted(this, new ImportarXMLCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/InicializarVariablesImportacion", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public void InicializarVariablesImportacion() {
            this.Invoke("InicializarVariablesImportacion", new object[0]);
        }
        
        /// <remarks/>
        public void InicializarVariablesImportacionAsync() {
            this.InicializarVariablesImportacionAsync(null);
        }
        
        /// <remarks/>
        public void InicializarVariablesImportacionAsync(object userState) {
            if ((this.InicializarVariablesImportacionOperationCompleted == null)) {
                this.InicializarVariablesImportacionOperationCompleted = new System.Threading.SendOrPostCallback(this.OnInicializarVariablesImportacionOperationCompleted);
            }
            this.InvokeAsync("InicializarVariablesImportacion", new object[0], this.InicializarVariablesImportacionOperationCompleted, userState);
        }
        
        private void OnInicializarVariablesImportacionOperationCompleted(object arg) {
            if ((this.InicializarVariablesImportacionCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.InicializarVariablesImportacionCompleted(this, new System.ComponentModel.AsyncCompletedEventArgs(invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        [System.Web.Services.Protocols.SoapDocumentMethodAttribute("http://tempuri.org/SiesaWEBContabilizar", RequestNamespace="http://tempuri.org/", ResponseNamespace="http://tempuri.org/", Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=System.Web.Services.Protocols.SoapParameterStyle.Wrapped)]
        public short SiesaWEBContabilizar(string pvstrParametros) {
            object[] results = this.Invoke("SiesaWEBContabilizar", new object[] {
                        pvstrParametros});
            return ((short)(results[0]));
        }
        
        /// <remarks/>
        public void SiesaWEBContabilizarAsync(string pvstrParametros) {
            this.SiesaWEBContabilizarAsync(pvstrParametros, null);
        }
        
        /// <remarks/>
        public void SiesaWEBContabilizarAsync(string pvstrParametros, object userState) {
            if ((this.SiesaWEBContabilizarOperationCompleted == null)) {
                this.SiesaWEBContabilizarOperationCompleted = new System.Threading.SendOrPostCallback(this.OnSiesaWEBContabilizarOperationCompleted);
            }
            this.InvokeAsync("SiesaWEBContabilizar", new object[] {
                        pvstrParametros}, this.SiesaWEBContabilizarOperationCompleted, userState);
        }
        
        private void OnSiesaWEBContabilizarOperationCompleted(object arg) {
            if ((this.SiesaWEBContabilizarCompleted != null)) {
                System.Web.Services.Protocols.InvokeCompletedEventArgs invokeArgs = ((System.Web.Services.Protocols.InvokeCompletedEventArgs)(arg));
                this.SiesaWEBContabilizarCompleted(this, new SiesaWEBContabilizarCompletedEventArgs(invokeArgs.Results, invokeArgs.Error, invokeArgs.Cancelled, invokeArgs.UserState));
            }
        }
        
        /// <remarks/>
        public new void CancelAsync(object userState) {
            base.CancelAsync(userState);
        }
        
        private bool IsLocalFileSystemWebService(string url) {
            if (((url == null) 
                        || (url == string.Empty))) {
                return false;
            }
            System.Uri wsUri = new System.Uri(url);
            if (((wsUri.Port >= 1024) 
                        && (string.Compare(wsUri.Host, "localHost", System.StringComparison.OrdinalIgnoreCase) == 0))) {
                return true;
            }
            return false;
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void CrearConexionXMLCompletedEventHandler(object sender, CrearConexionXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class CrearConexionXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal CrearConexionXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public bool Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((bool)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void EjecutarConsultaXMLCompletedEventHandler(object sender, EjecutarConsultaXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class EjecutarConsultaXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal EjecutarConsultaXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void LeerEsquemaParametrosCompletedEventHandler(object sender, LeerEsquemaParametrosCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class LeerEsquemaParametrosCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal LeerEsquemaParametrosCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public string Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((string)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void SiesaCFDCompletedEventHandler(object sender, SiesaCFDCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SiesaCFDCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SiesaCFDCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public object Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((object)(this.results[0]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void ImportarXMLCompletedEventHandler(object sender, ImportarXMLCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class ImportarXMLCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal ImportarXMLCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public System.Data.DataSet Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((System.Data.DataSet)(this.results[0]));
            }
        }
        
        /// <remarks/>
        public short printTipoError {
            get {
                this.RaiseExceptionIfNecessary();
                return ((short)(this.results[1]));
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void InicializarVariablesImportacionCompletedEventHandler(object sender, System.ComponentModel.AsyncCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    public delegate void SiesaWEBContabilizarCompletedEventHandler(object sender, SiesaWEBContabilizarCompletedEventArgs e);
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Web.Services", "4.8.9032.0")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    public partial class SiesaWEBContabilizarCompletedEventArgs : System.ComponentModel.AsyncCompletedEventArgs {
        
        private object[] results;
        
        internal SiesaWEBContabilizarCompletedEventArgs(object[] results, System.Exception exception, bool cancelled, object userState) : 
                base(exception, cancelled, userState) {
            this.results = results;
        }
        
        /// <remarks/>
        public short Result {
            get {
                this.RaiseExceptionIfNecessary();
                return ((short)(this.results[0]));
            }
        }
    }
}

#pragma warning restore 1591