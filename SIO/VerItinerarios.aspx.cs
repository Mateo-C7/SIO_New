using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class VerItinerarios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int arRol = (int)Session["Rol"];
            if (arRol == 15) 
            {
                Session["modifica"] = 1;
            }
            else
            {
                Session["modifica"] = 0;
            }


            if (!IsPostBack)
            {
                this.CargarReporteItinerarios();
            }

        }

        public void CargarReporteItinerarios()
        {
            string modifica = Session["modifica"].ToString();
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("pais", " Todos", true));
            parametro.Add(new ReportParameter("modifica", modifica, true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

            //ReporteVerItinerarios.Width = 1000;
            //ReporteVerItinerarios.Height = 400;
            ReporteVerItinerarios.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteVerItinerarios.ServerReport.ReportServerCredentials = irsc;
            ReporteVerItinerarios.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteVerItinerarios.ServerReport.ReportPath = "/Logistica/LOG_Itinerarios";
            this.ReporteVerItinerarios.ServerReport.SetParameters(parametro);
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