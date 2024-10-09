using System;
using System.Net;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class VerSegPvsBrasil : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //ReporteTablero.Width = 1280;
                //reportContactos.Height = 1050;
                Rpt_SegPvsBrasil.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                Rpt_SegPvsBrasil.ServerReport.ReportServerCredentials = irsc;

                Rpt_SegPvsBrasil.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                Rpt_SegPvsBrasil.ServerReport.ReportPath = "/PRODUCCION/PROD_GerboSeguimientoPvs";
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