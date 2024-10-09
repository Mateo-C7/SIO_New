
using System;
using Microsoft.Reporting.WebForms;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using System.Collections.Generic;
using CapaControl;
using System.Data;


namespace SIO
{
    public partial class CalidadReporte : System.Web.UI.Page
    {
        private Gn fpGn;
        public ControlReporte ctrlreporte = new ControlReporte();
        protected void Page_Load(object sender, EventArgs e)
        {
            fpGn = new Gn();

            if (!IsPostBack)
            {
                String grupo = Request.QueryString["Grupo"];
                DataTable nombreGrupo;
                nombreGrupo = ctrlreporte.Obtener_Nombre_Grupo(int.Parse(grupo));
                ctrlreporte.InitListaReportes(DdlReporte, grupo);
                lbTitulo.Text = nombreGrupo.Rows[0][0].ToString();
                DdlReporte.Items.Insert(0, "Seleccione un Reporte");
                DdlReporte.SelectedIndex = 0;             
            }
        }

        //verifica que en el combo este seleccionado algo y vuelve a cargar los reportes
        protected void cmdSelecRepo(object sender, EventArgs e)
        {   //Redefinir reporte
            if (!DdlReporte.SelectedValue.ToString().Equals("Seleccione un Reporte"))
            {                            
                cargarReporte(1);
            }
            else
            {
                mensajeVentana("No se ha seleccionado ningun reporte");
                cargarReporte(0);
            }     
        }
       
        private void cargarReporte(int cargar)
        {
            if (cargar == 1)
            {
                string stReporte = DdlReporte.SelectedValue.ToString();// linea modificada
                visorReporte.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                visorReporte.ServerReport.ReportServerCredentials = irsc;
                visorReporte.ServerReport.ReportServerUrl = new Uri(CapaControl.Gn.GetConf("UrlReportServer"));
                visorReporte.ServerReport.ReportPath = stReporte; // linea modificada     
            }
            else
            {
                //No muestre nada en el contenedor, o limpie el contenedor
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
        /**************METODO PARA MOSTRAR LOS ALERT QUE ARROJA LA PAGINA*************/
        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
    }
}






