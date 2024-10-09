using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;

namespace SIO
{
    public partial class VerSolFacturacion : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();

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


                if (!IsPostBack)
                {
                    this.CargarReporteCarta();
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
            string fup = Session["FUP"].ToString();

            
            string version = Session["Version"].ToString(); 
            string parte = Session["Parte"].ToString() ;
            string pvId = Session["PvId"].ToString();
            int  SfId = 0;

            reader = controlsf.ObtenerSfId(Convert.ToInt32(fup), version, Convert.ToInt32(parte), Convert.ToInt32(pvId));

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                   SfId = Convert.ToInt32(reader.GetValue(0).ToString());                     
                }
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
            

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", fup, true));
            ReporteSolFacturacion.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteSolFacturacion.ServerReport.ReportServerCredentials = irsc;
            ReporteSolFacturacion.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            this.ReporteSolFacturacion.KeepSessionAlive = true;
            this.ReporteSolFacturacion.AsyncRendering = true;

            string bandera = (string)Session["Bandera"];
            if (bandera == "1")
            {
                parametro.Add(new ReportParameter("version", version, true));
                parametro.Add(new ReportParameter("parte", parte, true));
                parametro.Add(new ReportParameter("sf_id", SfId.ToString(), true));

                ReporteSolFacturacion.ServerReport.ReportPath = "/InformesFUP/COM_SolicitudFacturacionSeguimientoNew";

            }
            else
            {
                parametro.Add(new ReportParameter("version", "A", true));
                parametro.Add(new ReportParameter("parte", parte, true));
                parametro.Add(new ReportParameter("sf_id", SfId.ToString(), true));

                ReporteSolFacturacion.ServerReport.ReportPath = "/InformesFUP/COM_SolicitudFacturacionSeguimientoNew";

            }
            ReporteSolFacturacion.ServerReport.SetParameters(parametro);

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