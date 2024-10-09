using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;

namespace SIO
{
    public partial class Roles : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limpiar();
                cargarTabla();
                politicas(41, Session["usuario"].ToString());
            }
        }
        private void cargarTabla()
        {
            grdRoles.DataSource = CP.cargarRoles();
            grdRoles.DataBind();
        }
        protected void chkActivoVice_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int idRol = int.Parse(row.Cells[0].Text);
            bool valor = chk.Checked;
            CP.cambiarEstRol("rap_lvice", idRol, valor);
        }
        protected void chkActivoGen_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int idRol = int.Parse(row.Cells[0].Text);
            bool valor = chk.Checked;
            CP.cambiarEstRol("rap_gerente", idRol, valor);
        }
        protected void chkActivoRep_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int idRol = int.Parse(row.Cells[0].Text);
            bool valor = chk.Checked;
            CP.cambiarEstRol("rap_lrepre", idRol, valor);
        }
        protected void chkActivoVia_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int idRol = int.Parse(row.Cells[0].Text);
            bool valor = chk.Checked;
            CP.cambiarEstRol("rap_lviajes", idRol, valor);
        }
        protected void BtnEliminar_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            GridViewRow row = (GridViewRow)btn.Parent.Parent;
            int idRol = int.Parse(row.Cells[0].Text);
            CP.eliminarRol(idRol, 0);
            cargarTabla();
        }
        protected void btnAgregar_Click(object sender, EventArgs e)
        {
            String mensaje = "";
            String mensaje2 = "";
            if (txtNomRol.Text != "")
            {
                mensaje = CP.insertarRol(txtNomRol.Text, chkVice.Checked, chkGer.Checked, chkRep.Checked, chkVia.Checked);
                mensaje2 = CP.insertarRolRutinas(Session["usuario"].ToString());
                if (mensaje == "OK")
                {
                    if (mensaje2 == "OK")
                    {
                        limpiar();
                        txtNomRol.Focus();
                        mensajeVentana("Se agrego correctamente!");
                        cargarTabla();
                    }
                    else { mensajeVentana(mensaje2); }
                }
                else
                {
                    txtNomRol.Text = "";
                    txtNomRol.Focus();
                    mensajeVentana(mensaje);
                }
                cargarTabla();
            }
            else { mensajeVentana("Por favor escriba el nombre del rol, gracias!"); }
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        private void limpiar()
        {
            txtNomRol.Text = "";
            chkVice.Checked = false;
            chkGer.Checked = false;
            chkRep.Checked = false;
            chkVia.Checked = false;
        }
        //Maneja las politicas dependiendo de la rutina y el rol del usuario
        private void politicas(int rutina, String usuario)
        {
            Boolean agregar = false;
            Boolean eliminar = false;
            Boolean imprimir = false;
            Boolean editar = false;
            DataTable politicas = null;
            politicas = CP.politicasBotones(rutina, usuario);
            foreach (DataRow row in politicas.Rows)
            {
                agregar = Boolean.Parse(row["agregar"].ToString());
                eliminar = Boolean.Parse(row["eliminar"].ToString());
                imprimir = Boolean.Parse(row["imprimir"].ToString());
                editar = Boolean.Parse(row["editar"].ToString());
            }

            if (agregar == true)
            { btnAgregar.Visible = true; }
            else { btnAgregar.Visible = false; }

            if (eliminar == false)
            {
                grdRoles.Columns[6].Visible = false;
            }
        }
    }
}