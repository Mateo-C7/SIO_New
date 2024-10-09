using System;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;


namespace SIO
{
    public partial class ReporteCartaCotiza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int CartaVersion = 1;
                int SalidaOriginal = 0;
                Boolean haypar = false;
                this.ReporteCartaCotizav.KeepSessionAlive = true;
                this.ReporteCartaCotizav.AsyncRendering = true;
                ReporteCartaCotizav.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                ReporteCartaCotizav.ServerReport.ReportServerCredentials = irsc;

                ReporteCartaCotizav.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");

                List<ReportParameter> parametro = new List<ReportParameter>();
                if (Request.QueryString["IdFUP"] != null)
                {
                    string FUPId = Request.QueryString["IdFUP"];
                    parametro.Add(new ReportParameter("pFupID", FUPId, true));
                    haypar = true;
                }
                if (Request.QueryString["VerFUP"] != null)
                {
                    string varVersion = Request.QueryString["VerFUP"];
                    parametro.Add(new ReportParameter("pVersion", varVersion, true));
                    haypar = true;
                }
                if (Request.QueryString["Detallado"] != null)
                {
                    string varDetallado = Request.QueryString["Detallado"];
                    parametro.Add(new ReportParameter("pDetalle", varDetallado, true));
                    haypar = true;
                }
                string idiomaSeleccionado = "ES";
                if (Request.QueryString["Idioma"] != null)
                {
                    idiomaSeleccionado = Request.QueryString["Idioma"];
                    idiomaSeleccionado = idiomaSeleccionado.ToUpper();
                }
                if (Request.QueryString["CartaVersion"] != null)
                {
                    CartaVersion = Convert.ToInt32(Request.QueryString["CartaVersion"]);
                }
                if (Request.QueryString["SalidaOriginal"] != null)
                {
                    SalidaOriginal = Convert.ToInt32(Request.QueryString["SalidaOriginal"]);
                }
                ReporteCartaCotizav.PromptAreaCollapsed = true;

                if (CartaVersion == 2) {
                    ReporteCartaCotizav.ServerReport.ReportPath = "/Comercial/FUP_CartaCotizacion2024" ;
                    parametro.Add(new ReportParameter("pIdioma", idiomaSeleccionado.ToUpper(), true));
                    parametro.Add(new ReportParameter("pOriginal", SalidaOriginal.ToString(), true));
                    haypar = true;
                }
                else
                {

                    if (idiomaSeleccionado == "ES")
                    {
                        ReporteCartaCotizav.ServerReport.ReportPath = "/Comercial/FUP_CartaCotizacionES";
                    }
                    else
                    {
                        if (idiomaSeleccionado == "EN")
                        {
                            ReporteCartaCotizav.ServerReport.ReportPath = "/Comercial/FUP_CartaCotizacionEN";
                        }
                        else
                        {
                            ReporteCartaCotizav.ServerReport.ReportPath = "/Comercial/FUP_CartaCotizacionBR";
                        }

                    }
                }
                if (haypar)
                {
                    this.ReporteCartaCotizav.ServerReport.SetParameters(parametro);
                } 
                //string OF = (string)Session["IdFUP"];
                           
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
    }
}