using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Web.UI;

namespace SIO
{
    public partial class VerListaAccesorios : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                // verifico si es un usuario de tipo cliente directo como MRV
                if (Convert.ToUInt32(Session["IdClienteUsuario"]) > 0) Response.Redirect("Home.aspx");

                int arRol = (int)Session["Rol"];

                if (!IsPostBack)
                {
                    this.CargarReporteOrdenes();
                }
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }

        }

        public void CargarReporteOrdenes()
        {
            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});
            
            int usuId = Convert.ToInt32(Session["usuId"]);
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("usuId", usuId.ToString(), true));            

            ReporteListAcc.ProcessingMode = ProcessingMode.Remote;
            //IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteListAcc.ServerReport.ReportServerCredentials = irsc;
            //ReporteListAcc.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteListAcc.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteListAcc.ServerReport.ReportPath = "/Comercial/COM_ListaAccesoriosPvNew";
            this.ReporteListAcc.ServerReport.SetParameters(parametro);
            ReporteListAcc.ShowToolBar = true;
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