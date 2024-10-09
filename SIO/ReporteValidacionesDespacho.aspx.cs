using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using dhtmlxConnectors;
using Microsoft.VisualBasic;
namespace SIO
{
    public partial class ReporteValidacionesDespacho : System.Web.UI.Page
    {
        public FUP fupClase = new FUP();
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlPedido contpv = new ControlPedido();

        protected void Page_Load(object sender, EventArgs e)
        {
            

            if (Session["Rol"] != null)
            {

                int arRol = (int)Session["Rol"];

            if (arRol == 36 || arRol == 1)
            {
                btnGuardar.Visible = true;
            }
            else
            {
                btnGuardar.Visible = false;
            }

            if (!IsPostBack)
            {
               poblarPlanta();
               this.CargarReporteValidacionesDespacho();   
            }

            }
                else
                 {
                        string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        Response.Redirect("Inicio.aspx");
                 }
        }

        private void poblarPlanta()
        {
            string usuario = (string)Session["Usuario"];
            int plantaDefault = 0;
            cboPlanta.Items.Add(new ListItem("Seleccione la planta", "0"));
            reader = contubi.poblarPlantaGeneral();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    //plantaDefault = reader.GetInt32(2);
                    cboPlanta.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
                //cboPlanta.SelectedValue = plantaDefault.ToString();
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

        public void CargarReporteValidacionesDespacho()
        {
            List<ReportParameter> parametro = new List<ReportParameter>();
            //parametro.Add(new ReportParameter("operacion", "Acta", true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

            ReportDespValid.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportDespValid.ServerReport.ReportServerCredentials = irsc;

            ReportDespValid.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportDespValid.ServerReport.ReportPath = "/InformesFUP/COM_Acta_ValidacionDespacho";
            //this.ReporteActa.ServerReport.SetParameters(parametro);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Session["Evento"] = 42;
            Session["fechaIni"] = txtFechaIni.Text;
            Session["fechaFin"] = txtFechaFin.Text;
            Session["planta"] = cboPlanta.SelectedItem.ToString();
            fupClase.CorreoGeneral(Convert.ToInt32(Session["Evento"]));
        }

        protected void btnConsultar_Click(object sender, EventArgs e)
        {
            string fechiIni = txtFechaIni.Text;
            string fechaFin = txtFechaFin.Text;

            if (txtFechaIni.Text == "" || txtFechaFin.Text == "")
            {
            }
            else
            {
                List<ReportParameter> parametro = new List<ReportParameter>();
                parametro.Add(new ReportParameter("fechaini", fechiIni, true));
                parametro.Add(new ReportParameter("fechafin", fechaFin, true));
                parametro.Add(new ReportParameter("Planta", cboPlanta.SelectedItem.Value, true));

                //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
                //ReportParameter parametro = new ReportParameter("operacion", "Acta");
                //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
                //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});

                ReportDespValid.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                ReportDespValid.ServerReport.ReportServerCredentials = irsc;

                ReportDespValid.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                ReportDespValid.ServerReport.ReportPath = "/InformesFUP/COM_Acta_ValidacionDespacho";
                this.ReportDespValid.ServerReport.SetParameters(parametro);
            }

        }

       
    }
}