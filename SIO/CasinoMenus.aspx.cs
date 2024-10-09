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
    public partial class CasinoMenus : System.Web.UI.Page
    {
        public ControlPoliticas CP = new ControlPoliticas();
        private ControlCasino CC = new ControlCasino();
        private InfoCasino InfoCas = new InfoCasino();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDesMenu.Text = "";
                chkEstMenu.Checked = false;
                btnGuardar.Visible = true;
                btnAct.Visible = false;
                cargarCombo(CC.cargarComboTipoSer("AND (castiposerv_actmenu = 1)"), cboSerMenu, 0, 1);//Carga el como cada vez que carga la pagina
            }
            politicas(68, Session["usuario"].ToString());
        }
        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / ,0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            txtDesMenu.Text = "";
            cargarCombo(CC.cargarComboTipoSer("AND (castiposerv_actmenu = 1)"), cboSerMenu, 0, 1);
            chkEstMenu.Checked = false;
            cboSerMenu.Enabled = true;
            Session["CasinoIdMenu"] = "";
            btnGuardar.Visible = true;
            btnAct.Visible = false;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            cargarTabla(grdMenus, null);
            cargarCombo(CC.cargarComboTipoSer("AND (castiposerv_actmenu = 1)"), cboSerBusMenu, 0, 1);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script1", "abrirPopup('PopupBuscaMenu')", true);
        }

        protected void cboSerMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtDesMenu.Text = "";
        }

        protected void btnSelMenu_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            Session["CasinoIdMenu"] = this.grdMenus.DataKeys[row.RowIndex].Value.ToString();
            txtDesMenu.Text = this.grdMenus.Rows[row.RowIndex].Cells[2].Text;
            chkEstMenu.Checked = (this.grdMenus.Rows[row.RowIndex].Cells[3].Text == "SI" ? true : false);
            cboSerMenu.SelectedIndex = cboSerMenu.Items.IndexOf(cboSerMenu.Items.FindByValue(cboSerBusMenu.SelectedValue.ToString()));
            cboSerMenu.Enabled = false;
            btnGuardar.Visible = false;
            btnAct.Visible = true;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script2", "cerrarPopup('PopupBuscaMenu')", true);
        }

        protected void cboSerBusMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarTabla(grdMenus, CC.cargarMenus(cboSerBusMenu.SelectedValue.ToString(), ""));
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboSerMenu.SelectedItem.ToString() != "Seleccionar")
            {
                if (txtDesMenu.Text != "")
                {
                    String idmen = "";
                    idmen = CC.insertarMenu(txtDesMenu.Text, chkEstMenu.Checked.ToString());
                    if (idmen.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        String mensaje = "";
                        mensaje = CC.insertarMenuServ(cboSerMenu.SelectedValue.ToString(), idmen);
                        if (mensaje == "OK")
                        {
                            Session["CasinoIdMenu"] = idmen;
                            cboSerMenu.Enabled = false;
                            mensajeVentana("Se ha ingresado correctamente!!");
                            btnAct.Visible = true;
                            btnGuardar.Visible = false;
                        }
                        else { mensajeVentana(mensaje); }
                    }
                    else { mensajeVentana(idmen); }
                }
                else { mensajeVentana("Por favor llene la descripcion del menu, gracias!"); }
            }
            else { mensajeVentana("Por favor seleccione el tipo de servicio, gracias!"); }
        }

        protected void btnAct_Click(object sender, EventArgs e)
        {
            if (txtDesMenu.Text != "")
            {
                String idmen = "";
                idmen = CC.editarMenu(Session["CasinoIdMenu"].ToString(), txtDesMenu.Text, chkEstMenu.Checked.ToString());
                if (idmen == "OK")
                {
                    cboSerMenu.Enabled = false;
                    mensajeVentana("Se ha actualizado correctamente!!");
                    btnAct.Visible = true;
                    btnGuardar.Visible = false;
                }
                else { mensajeVentana(idmen); }
            }
            else { mensajeVentana("Por favor llene la descripcion del menu, gracias!"); }
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
             {
                 btnGuardar.Visible = true;
                 btnNuevo.Visible = true;
             }
             else
             {
                 btnGuardar.Visible = false;
                 btnNuevo.Visible = false;
             }

             if (editar == true)
             {
                 btnAct.Visible = true;
             }
             else { btnAct.Visible = true; }
         }
    }
}