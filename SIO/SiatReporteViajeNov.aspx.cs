using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class SiatReporteViajeNov : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) 
            {
                rvReporteUno.Visible = true;
                rvReporteUno.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "forsa", "mail.forsa.com.co");
                rvReporteUno.ServerReport.ReportServerCredentials = irsc;
                rvReporteUno.ServerReport.ReportServerUrl = new Uri("http://si.forsa.com.co:81/ReportServer");
                rvReporteUno.ServerReport.ReportPath = "/Postventa/SIAT_Viaje_Novedades";
                rvReporteUno.ShowToolBar = true;
            }
        }
    }
}