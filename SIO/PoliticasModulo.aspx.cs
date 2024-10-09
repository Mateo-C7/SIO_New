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
    public partial class PoliticasModulo : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                cargarCombo();
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
        private void cargarTabla(int idRol)
        {
            grdModulos.DataSource = CP.cargarModulosFiltro(idRol);
            grdModulos.DataBind();
        }
        protected void cboRoles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboRoles.SelectedItem.ToString() != "Seleccionar")
            {
                grdModulos.Visible = true;
                cargarTabla(int.Parse(cboRoles.SelectedValue.ToString()));
            }
            else
            {
                grdModulos.Visible = false;
            }
        }
        protected void chkActivoMod_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox chk = (CheckBox)sender;
            GridViewRow row = (GridViewRow)chk.Parent.Parent;
            int idMod = int.Parse(row.Cells[0].Text);
            int idRol = int.Parse(cboRoles.SelectedValue.ToString());
            bool valor = chk.Checked;
            String existe = CP.consultarRolMod(idRol, idMod);
            if (existe == "SI")
            {
                CP.cambiarPolRol(idRol, idMod, valor, Session["usuario"].ToString());
            }
            else
            {
                String mensaje = CP.insertarRolMod(idRol, idMod, Session["usuario"].ToString());
                if (mensaje != "OK")
                {
                    mensajeVentana(mensaje);
                }
            }
        }
        //Maneja el mensaje por medio de script
        private void mensajeVentana(String mensaje)
        {
            ClientScript.RegisterClientScriptBlock(GetType(), "alert", "alert('" + mensaje + "');", true);
        }
    }
}