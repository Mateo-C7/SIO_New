using System;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;

namespace SIO
{
    public partial class VerReporteListaEmpaqueHv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Boolean haypar = false;
                this.ReporteFUPv.KeepSessionAlive = true;
                this.ReporteFUPv.AsyncRendering = true;
                ReporteFUPv.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                ReporteFUPv.ServerReport.ReportServerCredentials = irsc;

                ReporteFUPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");

                List<ReportParameter> parametro = new List<ReportParameter>();
                if (Request.QueryString["IdOrden"] != null && Request.QueryString["Tipo"] != null)
                {
                    string OrdenId = Request.QueryString["IdOrden"];
                    string Tipo = Request.QueryString["Tipo"];
                    
                    haypar = true;

                    if ( Tipo == "consolida")
                    { 
                        parametro.Add(new ReportParameter("IdOrden", OrdenId, true));
                        ReporteFUPv.ServerReport.ReportPath = "/Logistica/LOG_ListaEmpaqueConsolidada";
                    }
                    else
                    {
                        parametro.Add(new ReportParameter("idofa", OrdenId, true));
                        ReporteFUPv.ServerReport.ReportPath = "/Logistica/LOG_ListaEmpaquePallet";
                    }
                        

                }                                

                if (haypar)
                {
                    ReporteFUPv.PromptAreaCollapsed = true;
                    this.ReporteFUPv.ServerReport.SetParameters(parametro);
                }
                //string OF = (string)Session["IdFUP"];
                
            }
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }
    }
}