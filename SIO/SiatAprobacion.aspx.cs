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
    public partial class SiatAprobacion : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas(); 
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limCamGen();
                cargarCombo(CS.cargarTecnicos(""), cboTecnico, 0, 1);//Combo tecnico
            }
            politicas(66, Session["usuario"].ToString());
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
                btnApro.Visible = true;
            }
            else
            {
                btnApro.Visible = false;
            }
        }
        //limpia campos generales
        private void limCamGen()
        {
            lblDiasV.Text = "";
            lblPorce.Text = "";
            lblDiasC.Text = "";
        }
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        //calcula los dias compensatorios
        private int diasComp(DayOfWeek dia, DateTime inicio, DateTime fin, int div)
        {
            int dias = 0;
            while (inicio <= fin)
            {
                if (inicio.DayOfWeek == dia)
                {
                    dias = dias + 1;
                }
                inicio = inicio.AddDays(1);
            }
            return (dias / div);
        }
        //carga el combo
        private void cargarCombo(DataTable tabla, DropDownList combo, int value, int texto)
        {
            combo.Items.Clear();
            combo.Items.Add("Seleccionar");
            foreach (DataRow row in tabla.Rows)
            {   //posicion de las colmunas  0 = value / 1 = texto  --- se escoge el numero dependiendo de la columna que tenga en el query //siempre va el valor como id de primero, y despues el texto lo que se va mostrar en el combo / 0,1
                combo.Items.Add(new ListItem(row[texto].ToString(), row[value].ToString()));
            }
        }
        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }
        //boton aprobar compensatorio
        protected void btnApro_Click(object sender, EventArgs e)
        {
            if (lblDiasV.Text != "" && lblPorce.Text != "" && lblDiasC.Text != "")
            {
                String mensaje = CS.editarEstadoV(3, Session["SIATidViaje"].ToString(), ", siat_via_fecha_comp = SYSDATETIME(), siat_via_usu_comp = '" + Session["usuario"].ToString() + "', siat_via_dias_comp = " + lblDiasC.Text + ", siat_via_porce_comp = " + lblPorce.Text.Replace("%", "") + ", siat_via_aproba_comp = 1 ");
                if (mensaje == "OK")
                {
                    mensajeVentana("Se ha aprodado correctamente!!");
                    DataTable datos = CS.datosCompe("AND (siat_viaje.siat_via_tec_id = " + cboTecnico.SelectedValue.ToString() + ")");
                    cargarTabla(grdCompen, datos);
                    Boolean conCorr = CS.correoSiatComp(Session["usuario"].ToString(), cboTecnico.SelectedItem.ToString(), Session["SIATCliente"].ToString(), Session["SIATPais"].ToString(), Session["SIATFIni"].ToString(), Session["SIATFFin"].ToString(), lblDiasV.Text, lblPorce.Text, lblDiasC.Text, Session["SIATidComp"].ToString());
                    if (conCorr == false) { mensajeVentana("Hubo un error al enviar la carta al correo"); }
                    limCamGen();
                }
                else { mensajeVentana(mensaje); }
            }
            else { mensajeVentana("Por favor selecione un compesatorio"); }
        }
        //combo del tecnico
        protected void cboTecnico_SelectedIndexChanged(object sender, EventArgs e)
        {
            limCamGen();
            if (cboTecnico.SelectedItem.ToString() != "Seleccionar")
            {
                DataTable datos = CS.datosCompe("AND (siat_viaje.siat_via_tec_id = " + cboTecnico.SelectedValue.ToString() + ")");
                cargarTabla(grdCompen, datos);
            }
            else
            {
                cargarTabla(grdCompen, null);
            }
        }
        //Identifica que fila ha sido seleccionada
        protected void btnSelComp_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String idViaje = this.grdCompen.DataKeys[row.RowIndex].Value.ToString();
            String ini = this.grdCompen.Rows[row.RowIndex].Cells[2].Text;
            String fin = this.grdCompen.Rows[row.RowIndex].Cells[3].Text;
            String dias = this.grdCompen.Rows[row.RowIndex].Cells[4].Text;
            String cliente = this.grdCompen.Rows[row.RowIndex].Cells[5].Text;
            String pais = this.grdCompen.Rows[row.RowIndex].Cells[6].Text;
            int diasC = diasComp(DayOfWeek.Sunday, DateTime.Parse(ini), DateTime.Parse(fin), 3);
            lblDiasV.Text = dias;
            lblDiasC.Text = diasC.ToString();
            DataTable comp = CS.cargaCompe(dias);
            if (comp.Rows.Count > 0)
            {
                foreach (DataRow row2 in comp.Rows)
                {
                    lblPorce.Text = row2["porce"].ToString() + "%";
                    Session["SIATidComp"] = row2["idComp"].ToString();
                }
            }
            else { lblPorce.Text = "0"; }
            Session["SIATidViaje"] = idViaje;
            Session["SIATCliente"] = cliente;
            Session["SIATPais"] = pais;
            Session["SIATFIni"] = ini;
            Session["SIATFFin"] = fin;
        }
    }
}
