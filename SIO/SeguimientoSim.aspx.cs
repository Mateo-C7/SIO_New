using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net.Mime;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;

namespace SIO
{
    public partial class SeguimientoSim : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private DataSet ds = new DataSet();
        public ControlFlete controlFlete = new ControlFlete();
        public SqlDataReader readerCiudad = null;
        public ControlContacto controlCont = new ControlContacto();
        ControlUbicacion controlUbi = new ControlUbicacion();  

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["Rol"] != null)
                {
                    int arRol = (int)Session["Rol"];
                this.poblarListaZona();
                //this.cargarMinimos();
                //this.CargarReporteFletes();

                //if (arRol == 15)
                //{
                //    btnActFletMin.Visible = true;
                //    btnActValorCiuda.Visible = true;
                //    btnActValorCiuda0.Visible = true;
                //}
            }
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        private void poblarListaZona()
        {
            cbozona1.Items.Clear();

            cbozona1.Items.Add(new ListItem("Seleccione", "0"));

            readerCiudad = controlUbi.poblarListaZonas();

            if (readerCiudad.HasRows == true)
            {
                while (readerCiudad.Read())
                {
                    cbozona1.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                }
            }
            readerCiudad.Close();
        }
    }
}