using System;
using System.Collections.Generic;
using System.Net;
using Microsoft.Reporting.WebForms;


namespace SIO
{
    public partial class VisorReporteSio : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            int idPos = 0;
            //VisorReport.aspx? /RepIng/rptEntregaListadoPiezas&Orden=2010-20-2 
            string prParametros = Request.RawUrl.ToString();
            if (!this.IsPostBack)
            {
                //Tomar solo la cadena de parametros
                idPos = prParametros.IndexOf("?");
                if (idPos > 0)
                {
                    prParametros = prParametros.Substring(idPos + 1);
                    //Abrir el reporte
                    CargarReporte(prParametros);
                }
            }
        }
        //-------------------------------
        //Cargar el reporte
        //-------------------------------
        protected void CargarReporte(string prParametros)
        {
            try
            {
                string UrlReportServer = "http://10.75.131.2:81/ReportServer";
                string stReporte; //Nombre del Reporte
                string parametro;
                string parNombre;
                string parValor;
                string FormatoSalida;
                int idPos = 0;
                int pos;
                int pi, pf; //Posicion inicial y final de la subadaena
                pi = 0;
                FormatoSalida = "";
                //Inicializar el reporte
                RViewer.Visible = true;
                RViewer.ShowToolBar = true;
                RViewer.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                RViewer.ServerReport.ReportServerCredentials = irsc;
                RViewer.ServerReport.ReportServerUrl = new Uri(UrlReportServer);

                List<ReportParameter> ListParametro = new List<ReportParameter>();
                //Recuperar el Nombre del Reporte                
                idPos = prParametros.IndexOf("&");
                //si tiene parametros
                if (idPos > -1)
                {
                    stReporte = prParametros.Substring(0, idPos);
                    //RViewer.ServerReport.ReportPath = stReporte;
                    //Tomar solo listado de parametros
                    prParametros = prParametros.Substring(idPos + 1);
                    while (prParametros.Length > 0)
                    {
                        pf = prParametros.IndexOf("&");
                        if (pf < 0) //No encontro el separador P4=V4
                        {
                            parametro = prParametros;
                            prParametros = "";
                        }
                        else
                        {
                            parametro = prParametros.Substring(pi, pf);
                            prParametros = prParametros.Substring(pf + 1);
                        }
                        //-------
                        pos = parametro.IndexOf("=");
                        if (pos < 0)
                            continue;
                        //-----------
                        parNombre = parametro.Substring(0, pos);
                        parValor = parametro.Substring(pos + 1);
                        //------ Agregar Parametro al reporte
                        if (parNombre.Contains("Render"))
                        { FormatoSalida = parValor; }
                        else
                        {
                            ListParametro.Add(new ReportParameter(parNombre, parValor, true));
                        }
                    }
                }
                else
                {
                    //El nombre del reporte es toda la cadena
                    stReporte = prParametros;
                }
                //-------
                RViewer.ServerReport.ReportPath = stReporte;
                if (ListParametro.Count > 0)
                {
                    this.RViewer.ServerReport.SetParameters(ListParametro);
                }
                RViewer.ShowToolBar = true;
                if (FormatoSalida.Length > 1)
                {
                    Warning[] warnings;
                    string[] streamids;
                    string mimeType;
                    string encoding;
                    string extension;
                    string filename;

                    string Formato = "PDF"; // Por defecto pdf solo permite PDF, Excel y word
                    if (FormatoSalida.Contains("xls"))
                    {
                        Formato = "EXCELOPENXML";
                    }
                    if (FormatoSalida.Contains("doc"))
                    {
                        Formato = "WORDOPENXML";
                    }

                    byte[] data = RViewer.ServerReport.Render(
                               Formato, null, out mimeType, out encoding,
                                out extension,
                               out streamids, out warnings);

                    filename = string.Format("{0}.{1}", stReporte, extension);
                    Response.Clear();
                    Response.ContentType = mimeType;
                    Response.AddHeader("Content-Disposition", "inline;filename=" + filename);
                    Response.BinaryWrite(data);
                }
            }
            catch (Exception ex)
            {//Manejo de excepcion
            }
        }
        //-------------------------------
        //Definir Privilegios
        //-------------------------------
        class CustomReportCredentials : IReportServerCredentials
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