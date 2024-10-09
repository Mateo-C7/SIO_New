using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class VerClientesLite : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pais = (string)Session["pais"];
            string usuario = (string)Session["Usuario"];
            string rcNombre = (string)Session["Nombre_Usuario"];
            string rcEmail = (string)Session["rcEmail"];
            string area = (string)Session["Area"];
            string idioma = (string)Session["Idioma"];
            string idCliente;
            string rcID = (string)Session["rcID"];
            int arRol = (int)Session["Rol"];

            if (!IsPostBack)
            {
                this.CargarReporteCliente();
            }

        }

        public void CargarReporteCliente()
        {
            string rcID = (string)Session["rcID"];
            int arRol = (int)Session["Rol"];
            string pais = (string)Session["pais"];
            List<ReportParameter> parametro = new List<ReportParameter>();

            //parametro.Add(new ReportParameter("idrepresentante", rcID, true));
            //parametro.Add(new ReportParameter("rol", arRol.ToString(), true));
            //parametro.Add(new ReportParameter("pais", pais, true));

            //ReporteVerClientes.Width = 1320;
            //ReporteVerClientes.Height = 1000;
            ReporteLite.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteLite.ServerReport.ReportServerCredentials = irsc;

            ReporteLite.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteLite.ServerReport.ReportPath = "/Comercial/COM_ClientesLite";
            //this.ReporteMemos.ServerReport.SetParameters(parametro);
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