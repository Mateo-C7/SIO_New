using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Web.UI;
using System.Threading;
using System.Globalization;

namespace SIO
{
    public partial class SeguimientoPv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                int arRol = (int)Session["Rol"];

                if (!IsPostBack)
                {
                    this.CargarReportePv();
                }
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }

        }

        protected void Reporte_PreRender(object sender, EventArgs e)
        {
            CultureInfo ci = new CultureInfo("es-CO");
            Thread.CurrentThread.CurrentCulture = ci;
            Thread.CurrentThread.CurrentUICulture = ci;
        }

        public void CargarReportePv()
        {
            //List<ReportParameter> parametro = new List<ReportParameter>();
            //parametro.Add(new ReportParameter("tipo", "PV", true));
            //parametro.Add(new ReportParameter("pais", " Todos", true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

            ReporteSeguimietoPv.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteSeguimietoPv.ServerReport.ReportServerCredentials = irsc;
            ReporteSeguimietoPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteSeguimietoPv.ServerReport.ReportPath = "/Produccion/PROD_ConsultaGeneralPeriodoNew";
            //this.ReporteSeguimietoPv.ServerReport.SetParameters(parametro);
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