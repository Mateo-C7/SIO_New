using System;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Collections.Generic;


namespace SIO
{
    public partial class ReporteListaCT : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string Tipo = "1";
                Boolean haypar = false;
                this.ReporteFUPv.KeepSessionAlive = true;
                this.ReporteFUPv.AsyncRendering = true;
                ReporteFUPv.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                ReporteFUPv.ServerReport.ReportServerCredentials = irsc;

                ReporteFUPv.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");

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

                ReporteFUPv.PromptAreaCollapsed = true;
                ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacion";

                if (Request.QueryString["Tipo"] != null)
                {
                    Tipo = Request.QueryString["Tipo"];
                    switch (Tipo) {
                        case "1":
                            ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacion";
                            parametro.Add(new ReportParameter("pTipoOF", "CT", true));
                            haypar = true;
                            break;
                        case "2":
                            ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacionUsa";
                            parametro.Add(new ReportParameter("pTipoOF", "CT", true));
                            haypar = true;
                            break;
                        case "3":
                            ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacion";
                            parametro.Add(new ReportParameter("pTipoOF", "CI", true));
                            haypar = true;
                            break;
                        case "4":
                            ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacionUsa";
                            parametro.Add(new ReportParameter("pTipoOF", "CI", true));
                            haypar = true;
                            break;
                        default:
                            ReporteFUPv.ServerReport.ReportPath = "/Comercial/SIMUL_DetalleOrdenCotizacion";
                            break;
                    }
                }


                if (haypar)
                {
                    this.ReporteFUPv.ServerReport.SetParameters(parametro);
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