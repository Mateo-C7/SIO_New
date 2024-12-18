﻿using System;
using System.Net;
using Microsoft.Reporting.WebForms;


namespace SIO
{
    public partial class ReportePlaneadorCotizaciones : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.ReportViewer1.KeepSessionAlive = true;
            this.ReportViewer1.AsyncRendering = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;               
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer1.ServerReport.ReportPath = "/InformesFUP/ING_PlaneadorIngenieria";
            ReportViewer1.ServerReport.Refresh();
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