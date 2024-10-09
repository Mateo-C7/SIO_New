using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class VerCartaPv : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {
                int arRol = (int)Session["Rol"];

                string pais = (string)Session["pais"];
                string usuario = (string)Session["Usuario"];
                string rcNombre = (string)Session["Nombre_Usuario"];
                string rcEmail = (string)Session["rcEmail"];
                string area = (string)Session["Area"];
                string idioma = (string)Session["Idioma"];
                string idCliente;
                string rcID = (string)Session["rcID"];
                string Planta = (string)Session["Planta"];


                if (!IsPostBack)
                {
                    if (Planta == "CONSTRUCOMER")
                    {
                        this.CargarReporteContrucomer();
                    }
                    else
                    {
                        this.CargarReporteCarta();
                    }


                    
                }

            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }

        }

        public void CargarReporteCarta()
        {            
            string Rep = (string)Session["Nombre_Usuario"];
            string CorreoRep = (string)Session["rcEmail"];
            string fecha = System.DateTime.Today.ToLongDateString();

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", Session["FUP"].ToString(), true));
            parametro.Add(new ReportParameter("nombrer", Session["Nombre_Usuario"].ToString(), true));
            parametro.Add(new ReportParameter("paisr", Session["PaisNombre"].ToString(), true));
            parametro.Add(new ReportParameter("correor", Session["rcEmail"].ToString(), true));
            parametro.Add(new ReportParameter("fecha", fecha, true));

            this.ReporteCartaPv.KeepSessionAlive = true;
            this.ReporteCartaPv.AsyncRendering = false;
            ReporteCartaPv.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteCartaPv.ServerReport.ReportServerCredentials = irsc;
            ReporteCartaPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteCartaPv.ServerReport.ReportPath = "/Comercial/COM_CartaCotizacionPVNew";
            this.ReporteCartaPv.ServerReport.SetParameters(parametro);
        }

        public void CargarReporteContrucomer()
        {
            string Rep = (string)Session["Nombre_Usuario"];
            string CorreoRep = (string)Session["rcEmail"];
            string fecha = System.DateTime.Today.ToLongDateString();

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", Session["FUP"].ToString(), true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

            ReporteCartaPv.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteCartaPv.ServerReport.ReportServerCredentials = irsc;
            ReporteCartaPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteCartaPv.ServerReport.ReportPath = "/Comercial/COM_CartaCotizacionPVConstr";
            this.ReporteCartaPv.ServerReport.SetParameters(parametro);
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