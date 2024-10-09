using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Threading;
using System.Globalization;

namespace SIO
{
    public partial class VerResumenM2Ingenieria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int arRol = (int)Session["Rol"];

            if (!IsPostBack)
            {
                this.CargarReporte();                
            }

        }

        protected void Reporte_PreRender(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public void CargarReporte()
        {
            string orden = "";

            if (Request.QueryString["orden"] != null)
            {
                orden = Request.QueryString["orden"];
            }

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("Orden", orden, true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("fup", fup);
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReporteOrden.ServerReport.SetParameters(new ReportParameter [] {parametro});

            //string idfp = cboOfs.SelectedItem.Value.ToString();
            this.ReporteOrden.KeepSessionAlive = true;
            this.ReporteOrden.AsyncRendering = true;
            ReporteOrden.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteOrden.ServerReport.ReportServerCredentials = irsc;
            ReporteOrden.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteOrden.ServerReport.ReportPath = "/Ingenieria/ING_Resumen_M2";

            this.ReporteOrden.ServerReport.SetParameters(parametro);
            ReporteOrden.ShowToolBar = true;
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