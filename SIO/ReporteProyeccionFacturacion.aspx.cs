using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class ReporteProyeccionFacturacion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int arRol = (int)Session["Rol"];

            if (!IsPostBack)
            {
                if ((arRol == 9) || (arRol == 30) || (arRol == 2))
                {
                    this.CargarReporteProyeccionFacturacion();
                }

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

        public void CargarReporteProyeccionFacturacion()
        {
            List<ReportParameter> parametro = new List<ReportParameter>();
            //parametro.Add(new ReportParameter("operacion", "Acta", true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

            ReportProyFact.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportProyFact.ServerReport.ReportServerCredentials = irsc;

            ReportProyFact.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportProyFact.ServerReport.ReportPath = "/InformesFUP/COM_Acta_ProyFact";
            //this.ReporteActa.ServerReport.SetParameters(parametro);
        }
    }
}