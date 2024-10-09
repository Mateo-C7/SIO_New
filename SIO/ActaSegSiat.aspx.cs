using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;

namespace SIO
{
    public partial class ActaSegSiat : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                string nombre = Session["Nombre_Usuario"].ToString();
                string usuario = Session["Usuario"].ToString();
                string idClienteUsuario = Session["IdClienteUsuario"].ToString();
                Session["Pagina"] = "Acta Asistencia Tecnica";
            }
            catch
            {
                mensajeVentana("La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!");
                cerrarSesion();
            }

            if (!IsPostBack)
            {
                // verifico si es un usuario de tipo cliente directo como MRV
                if (Convert.ToUInt32(Session["IdClienteUsuario"]) > 0) Response.Redirect("Home.aspx");

                int rol = (int)Session["Rol"];
                Rol.Value = rol.ToString();
                int idClienteUsuario = Convert.ToInt32(Session["IdClienteUsuario"]);

                if ((rol == 1) || (rol == 36))
                {
                    add.Visible = true;
                }
                else
                {
                    add.Visible = false;
                }

                if (idClienteUsuario != 0) add.Visible = false;

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

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            string pagina = "http://app.forsa.com.co/siomaestros/ReporteValidacionesDespacho.aspx";
            string script = "window.open('" + pagina + "', '_blank');";
            //string script = "window.open('Cliente.aspx', '_blank');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
        }

        protected void lkActualizar_Click(object sender, EventArgs e)
        {
            Response.Redirect("actasegsiat.aspx");

        }

    }
}