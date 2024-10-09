
using System;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Collections.Generic;
using CapaControl;
namespace SIO
{
    public partial class EntregaPiezasStock : System.Web.UI.Page
    {
        private Gn fpGn;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                fpGn = new Gn();
                cargarReporte();
            }
           
        }

        private void cargarReporte()
        {

            ViewEntregaPiezas.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ViewEntregaPiezas.ServerReport.ReportServerCredentials = irsc;
            ViewEntregaPiezas.ServerReport.ReportServerUrl = new Uri(CapaControl.Gn.GetConf("UrlReportServer"));
            ViewEntregaPiezas.ServerReport.ReportPath = "/Produccion/PROD_EntrePiezaBodega";
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