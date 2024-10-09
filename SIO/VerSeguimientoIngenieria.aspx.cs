﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace SIO
{
    public partial class VerSeguimientoIngenieria : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {              
                if (Session["Rol"] != null)
                {
                    string idUsuariop = Session["idUsuario"].ToString();

                    ReporteSeguimientoIngenieria.Visible = true;
                    List<ReportParameter> parametro = new List<ReportParameter>();
                    parametro.Add(new ReportParameter("idUsuario", idUsuariop, true));


                    //ReporteTablero.Width = 1280;
                    //reportContactos.Height = 1050;
                    ReporteSeguimientoIngenieria.ProcessingMode = ProcessingMode.Remote;
                    IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                    ReporteSeguimientoIngenieria.ServerReport.ReportServerCredentials = irsc;

                    ReporteSeguimientoIngenieria.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                    ReporteSeguimientoIngenieria.ServerReport.ReportPath = "/Logistica/LOG_SeguimientoProyectos";
                    this.ReporteSeguimientoIngenieria.ServerReport.SetParameters(parametro);
                }

                else
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }
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