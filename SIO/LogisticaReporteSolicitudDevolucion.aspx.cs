﻿using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SIO
{
    public partial class LogisticaReporteSolicitudDevolucion : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.reporteSolicitud.KeepSessionAlive = true;
                this.reporteSolicitud.AsyncRendering = true;
                reporteSolicitud.Visible = true;
                reporteSolicitud.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                reporteSolicitud.ServerReport.ReportServerCredentials = irsc;
                reporteSolicitud.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                reporteSolicitud.ServerReport.ReportPath = "/Logistica/SolicitudLogisticaGeneral";
                reporteSolicitud.ShowToolBar = true;
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

        protected void btnCapturaPeso_Click(object sender, EventArgs e)
        {
            Response.Redirect("CapturaPeso.aspx");
        }
    }
}