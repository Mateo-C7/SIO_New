using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;
using System.Linq;

namespace SIO
{
    public partial class OrdenDetalle : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlObra contobra = new ControlObra();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                try
                {
                    string nombre = Session["Nombre_Usuario"].ToString();
                    string usuario = Session["Usuario"].ToString();
                }
                catch
                {
                    mensajeVentana("La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!");
                    cerrarSesion();
                }


                if (Request.QueryString["idOfp"] != null || Request.QueryString["idOfp"].ToString() == "0")
                {
                    string idOfp = Request.QueryString["idOfp"];
                    lblIdOfp.Text = idOfp;
                    PoblarDatosGenerales(Convert.ToInt32(idOfp));
                }
                else
                {
                    btnGuardar.Visible = false;
                    lblPais.Visible = false;
                    lblCliente.Visible = false;
                    lblObra.Visible = false;
                    lblTipoCotizacion.Visible = false;
                    lblFormaleta.Visible = false;
                    txtFecDespachoIni.Visible = false;
                    lblOrden.Visible = false;
                    lblPlanta.Visible = false;
                    lblParte.Visible = false;
                    lblFup.Visible = false;                    
                }               
                
            }
        }

        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void cerrarSesion()
        {            
            Response.Redirect("Inicio.aspx");
        }

        private void PoblarDatosGenerales(int IdOfp)
        {
            int rol = (int)Session["Rol"];
            reader = contobra.PoblarDatosGeneralesOrden(IdOfp);
            string fechaIni = "";

            while (reader.Read())
            {
                lblPais.Text = reader.GetString(0).ToString() +" / "+ reader.GetString(1).ToString();
                lblCliente.Text = reader.GetString(2).ToString();
                lblObra.Text = reader.GetString(3).ToString();
                lblTipoCotizacion.Text = reader.GetString(4).ToString();
                lblOrden.Text = reader.GetString(5).ToString();
                lblFormaleta.Text = reader.GetString(6).ToString();
                lblPlanta.Text = reader.GetString(7).ToString();
                lblParte.Text = reader.GetInt32(8).ToString();
                lblFup.Text = reader.GetString(9).ToString();
                lblIdOfp.Text = reader.GetInt32(11).ToString();
                fechaIni = reader.GetString(12).ToString();
                if (fechaIni == "1900-01-01" && (rol == 36 || rol == 1) )
                {
                    fechaIni = "";
                    txtFecDespachoIni.Enabled = true;
                    btnGuardar.Visible = true;  
                }
                else
                {
                    txtFecDespachoIni.Enabled = false;
                    btnGuardar.Visible = false;
                }
                     
                //if (reader.GetInt32(12).ToString() != null) fechaIni = reader.GetInt32(12).ToString();
                txtFecDespachoIni.Text = fechaIni;
                Session["idActa"] = reader.GetInt32(10).ToString();
                string A = reader.GetInt32(10).ToString();
            }

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {            
            string usuarioMod = (string)Session["Usuario"];
            int idActaSeg = Convert.ToInt32( Session["idActa"]);
            string mensaje = "";

            if (txtFecDespachoIni.Text != "")
            {
                //Actualizar la fecha despacho inicial
                contobra.ActualizarFechaIniDesp(idActaSeg, txtFecDespachoIni.Text);
                //Guarda el log de la tabla 
                contobra.InsertarLogFecIniDesp(idActaSeg, usuarioMod, txtFecDespachoIni.Text);
                //-------------------------------------------
                mensaje = "Fecha Actualizada";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
            }
            else
            {
                mensaje = "ERROR!! No fue posible actualizar";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
            }
        }
    }
}