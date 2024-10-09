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
    public partial class PoliticasRoles : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCombo();
            }
        }
        protected void cboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRoles.SelectedItem.ToString() != "Seleccionar")
            {
                cboModulos.Items.Clear();
                DataTable cargaModulos = CP.cargarModulosXRolFiltro(int.Parse(cboRoles.SelectedValue.ToString()));
                cboModulos.Items.Add("Seleccionar");
                foreach (DataRow row in cargaModulos.Rows)
                {
                    cboModulos.Items.Add(new ListItem(row["nomMod"].ToString(), row["idMod"].ToString()));
                }
            }
            else
            {
                cboModulos.Items.Clear();
                cboModulos.Items.Add("Seleccionar");
            }
            grdRutinas.Visible = false;
        }
        protected void cboModulos_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboModulos.SelectedItem.ToString() != "Seleccionar")
            {
                CP.insertarRolRutinasNew(Session["usuario"].ToString(), cboRoles.SelectedValue.ToString(), cboModulos.SelectedValue.ToString());
                grdRutinas.Visible = true;
                grdRutinas.DataSource = CP.cargarRutinasFiltro(int.Parse(cboRoles.SelectedValue.ToString()), int.Parse(cboModulos.SelectedValue.ToString()));
                grdRutinas.DataBind();
            }
            else
            {
                grdRutinas.Visible = false;
            }
        }
        private void cargarCombo()
        {
            cboRoles.Items.Clear();
            DataTable cargaRoles = CP.cargarRoles();
            cboRoles.Items.Add("Seleccionar");
            foreach (DataRow row in cargaRoles.Rows)
            {
                cboRoles.Items.Add(new ListItem(row["nomRol"].ToString(), row["idRol"].ToString()));
            }
        }
        protected void chkActivoAgr_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            Label lblIdRut = (Label)row.FindControl("idRut");
            int idRut = int.Parse(lblIdRut.Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            validaRutina("poli_rol_rut.rr_agregar", idRol, idRut, valor);
        }
        protected void chkActivoEli_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            Label lblIdRut = (Label)row.FindControl("idRut");
            int idRut = int.Parse(lblIdRut.Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            validaRutina("poli_rol_rut.rr_eliminar", idRol, idRut, valor);
        }
        protected void chkActivoImp_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            Label lblIdRut = (Label)row.FindControl("idRut");
            int idRut = int.Parse(lblIdRut.Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            validaRutina("poli_rol_rut.rr_imprimir", idRol, idRut, valor);
        }
        protected void chkActivoEdi_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            Label lblIdRut = (Label)row.FindControl("idRut");
            int idRut = int.Parse(lblIdRut.Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            validaRutina("poli_rol_rut.rr_editar", idRol, idRut, valor);
        }
        protected void chkActivoMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            Label lblIdRut = (Label)row.FindControl("idRut");
            int idRut = int.Parse(lblIdRut.Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            validaRutina("poli_rol_rut.rr_act", idRol, idRut, valor);
        }
        private void validaRutina(String filtro, int idRol, int idRut, bool valor)
        {
            CP.cambiarPolRut(filtro, idRol, idRut, valor, Session["usuario"].ToString());
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('" + mensaje + "');", true);
        }
    }
}