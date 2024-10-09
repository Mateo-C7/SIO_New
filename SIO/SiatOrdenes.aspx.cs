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
    public partial class SiatOrdenes : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }
        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }
        //Seleccionar la fila del grid
        protected void btnSelOrden_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String idOfa = this.grdOrdenes.DataKeys[row.RowIndex].Values["idOf"].ToString();
            String idCliente = this.grdOrdenes.DataKeys[row.RowIndex].Values["idCliente"].ToString();
            String idObra = this.grdOrdenes.DataKeys[row.RowIndex].Values["idObra"].ToString();
            String fechaObra = this.grdOrdenes.Rows[row.RowIndex].Cells[9].Text;
            String txtCliente = Server.HtmlDecode(this.grdOrdenes.Rows[row.RowIndex].Cells[10].Text);
            String moneda = this.grdOrdenes.DataKeys[row.RowIndex].Values["moneda"].ToString();
            String valor = this.grdOrdenes.DataKeys[row.RowIndex].Values["valor"].ToString();
            Session["SIATOrdenes"] = idOfa + "|" + idCliente + "|" + idObra + "|" + ((fechaObra == "&nbsp;" || fechaObra == null) ? "" : fechaObra) + "|" + txtCliente + "|" + moneda + "|" + valor;
            Response.Redirect("SiatViajeTec.aspx");
        }
        //Boton de buscar
        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string year = "";
            string filtro = "";
            if (!String.IsNullOrEmpty(txtYear.Text))
            {
                year = " AND (YEAR(Orden_Seg.fecha_crea) = " + txtYear.Text + ") ";
            }

            if (!String.IsNullOrEmpty(txtOrden.Text))
            {
                filtro = " AND (Orden.Numero + '-' + Orden.ano LIKE '%" + txtOrden.Text + "%') ";
            }

            cargarTabla(grdOrdenes, CS.cargarOrdenesPlan(year, filtro));
        }
    }
}