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
    public partial class FormatoDatosActualizaEmpleado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string cedu = Request.QueryString["Cedula"];    
                cargarReporte(cedu);
            }
        }
        private void cargarReporte(string cedu)
        {
            //lista de parametros para el reporte donde "cedula" es el parametro del reporte
            // y cedu es la variable donde recuperamos la cedula
            List<ReportParameter> parametro = new List<ReportParameter>();
            //agregamos parametros donde "cedula" 
            parametro.Add(new ReportParameter("cedula", cedu, true));
            reporteActualizaDatosEmpleado.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            reporteActualizaDatosEmpleado.ServerReport.ReportServerCredentials = irsc;
            reporteActualizaDatosEmpleado.ServerReport.ReportServerUrl = new Uri(CapaControl.Gn.GetConf("UrlReportServer"));
            reporteActualizaDatosEmpleado.ServerReport.ReportPath = "/Rep_ActualizacionDatosEmpleado/Rpt_ActuaDatosEmple";
            //Se pasa el parametro al reporte
            this.reporteActualizaDatosEmpleado.ServerReport.SetParameters(parametro);      
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

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActualizacionDatosEmpleado.aspx");
        }
    }
}





