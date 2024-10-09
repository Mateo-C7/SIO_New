using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Threading;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;

namespace SIO
{
    public partial class VerReporteLogItem : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int arRol = (int)Session["Rol"];

            if (!IsPostBack)
            {
                if (Session["Rol"] != null && Session["item_planta_id"] != null )
                {
                    //int.Parse(Session["item_planta_id"].ToString())
                    this.CargarReporte();
                }
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
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("IdItemPlanta",Session["item_planta_id"].ToString(), true));
            
                    

            ReporteInformeOrdenes.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteInformeOrdenes.ServerReport.ReportServerCredentials = irsc;
            ReporteInformeOrdenes.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteInformeOrdenes.ServerReport.ReportPath = "/COMERCIAL/COM_ItemLog";

            this.ReporteInformeOrdenes.ServerReport.SetParameters(parametro);
            ReporteInformeOrdenes.ShowToolBar = true;
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