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
    public partial class SeguimientoIngenieriaProyectos : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();

        protected void Page_Load(object sender, EventArgs e)
        {
            // string Msj; string Nombre = (string)Session["Nombre_Usuario"];
            //string CorreoUsuario = (string)Session["rcEmail"]; string correoSistema = (string)Session["CorreoSistema"];
            //System.Threading.Thread.Sleep(1000);


            //string orden = "FP-2578-3";
            ////Se envia el correo 
            //cmIp.enviarCorreoPrueba(1, orden, out Msj); // item fue aprobado
            //if (string.IsNullOrEmpty(Msj))
            //{
            //    Debug.WriteLine("Correo Fue enviado");
            //}
            //else
            //{
            //    Debug.WriteLine("Correo no fue enviado");
            //}

            //try
            //{
            //    string nombre = Session["Nombre_Usuario"].ToString();
            //    string usuario = Session["Usuario"].ToString();
            //}
            //catch
            //{
            //    mensajeVentana("La sesion ha Expirado, por favor inicie sesión nuevamente. Gracias!");
            //    cerrarSesion();
            //}

            if (!IsPostBack)
            {
                int rol = (int)Session["Rol"];
                Rol.Value = rol.ToString();

                if ((rol == 25)  || (rol == 26))
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
            Response.Redirect("SeguimientoIngenieriaProyectos.aspx");

        }


    }
}