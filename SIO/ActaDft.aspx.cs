using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using CapaControl;
using System.Diagnostics;
namespace SIO
{
    public partial class ActaDft : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string nombre = Session["Nombre_Usuario"].ToString();
                string usuario = Session["Usuario"].ToString();
                string idClienteUsuario = Session["IdClienteUsuario"].ToString();
                Session["Pagina"] = "Planeador Definicion Tecnica";
            }
            catch
            {
                mensajeVentana("La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!");
                cerrarSesion();
            }

            if (!IsPostBack)
            {
                int rol = (int)Session["Rol"];
                Rol.Value = rol.ToString();

                if ( (rol == 26) || (rol == 1))
                {
                    add.Visible = true;
                }
                else
                {
                    add.Visible = false;
                }

                string usu = (string)Session["Nombre_Usuario"];
                Usu.Value = usu.ToString();
            }
        }

        private void mensajeVentana(string mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void cerrarSesion()
        {
            Session["NombreUsuario"] = null;
            Session["UsuarioId"] = null;
            Session["Tipo"] = null;
            Response.Redirect("Inicio.aspx");
        }

        protected void txtFecIni_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtFecFin_TextChanged(object sender, EventArgs e)
        {

        }

        //protected void LinkButton1_Click(object sender, EventArgs e)
        //{
        //    string pagina = "http://app.forsa.com.co/siomaestros/ReporteValidacionesDespacho.aspx";
        //    string script = "window.open('" + pagina + "', '_blank');";
        //    //string script = "window.open('Cliente.aspx', '_blank');";
        //    ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
        //}

        protected void lkActualizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActaDft.aspx");

        }

    }
}