using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using CapaControl;

namespace SIO
{
    public partial class ReporteVisTres : System.Web.UI.Page
    {
        private ControlVisitaComercial CVC = new ControlVisitaComercial();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                String rango = CVC.usuarioActual(Session["usuario"].ToString());
               /* if (rango == "VICE" || rango == "GERENTE")
                {*/
                    rvReporteVisitas.Visible = false;
                    rvReporteViajes.Visible = false;
                    String reporte = Request.QueryString["Reporte"];
                    if (reporte == "Visitas")
                    {
                        lblTitulo.Text = "Reporte de " + reporte;
                        cargarReporteVisitas(rango, Session["usuario"].ToString());
                    }
                    else if (reporte == "Viajes")
                    {
                        lblTitulo.Text = "Reporte de " + reporte;
                        cargarReporteViajes(rango, Session["usuario"].ToString());
                    }
               /* }
                else 
                {
                    rvReporteVisitas.Visible = false;
                    rvReporteViajes.Visible = false;
                }*/
            }
        }
        private void cargarReporteVisitas(String rango, String usuario)
        {
            rvReporteVisitas.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("usuario", usuario, true));
            parametro.Add(new ReportParameter("rol", rango, true));
            rvReporteVisitas.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            rvReporteVisitas.ServerReport.ReportServerCredentials = irsc;
            rvReporteVisitas.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            rvReporteVisitas.ServerReport.ReportPath = "/Comercial/COM_Visitas_ReporteVisitas";
            this.rvReporteVisitas.ServerReport.SetParameters(parametro);
            rvReporteVisitas.ShowToolBar = true;
        }
        private void cargarReporteViajes(String rango, String usuario)
        {
            rvReporteViajes.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("usuario", usuario, true));
            parametro.Add(new ReportParameter("rol", rango, true));
            rvReporteViajes.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            rvReporteViajes.ServerReport.ReportServerCredentials = irsc;
            rvReporteViajes.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            rvReporteViajes.ServerReport.ReportPath = "/Comercial/COM_Visitas_ReporteViajes";
            this.rvReporteViajes.ServerReport.SetParameters(parametro);
            rvReporteViajes.ShowToolBar = true;
        }
    }
}