using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;

namespace SIO
{
    public partial class PackingList : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        protected void Page_Load(object sender, EventArgs e)
        {
            String orden2 = (string)Session["orden2"];
            String idT = (string)Session["idTrans"];
            int idTrans = int.Parse(idT);
            GridView2.DataSource = CL.cargarGridPacking1(idTrans);
            GridView2.DataBind();
            GridView1.DataSource = CL.cargarGridPacking2(idTrans);
            GridView1.DataBind();
            totalVolumen();
            totalPesoB();
            totalPesoN();
            totalCantidadPall();
            lblOrden.Text = orden2;
            String cliente = validar((string)Session["cliente"], "(cliente) ", lblCliente);
            String direccion = validar((string)Session["direccion"], "(direccion) ", lblTelefono);
            String pais = validar((string)Session["pais"], "(pais) ", lblPais);
            String telefono = validar((string)Session["telefono"], "(telefono) ", lblTelefono);
            String nomUsuCrea = validar((string)Session["nomUsuCrea"], "(Usuario creador del despacho) ", lblUsuarioCreaDes);
            String fecha = validar((string)Session["fecha"], "(fecha) ", lblFecha);
            String puertoEmbarque = validar((string)Session["puertoEmbarque"], "(Puerto de embarque) ", lblPuertoE);
            String puertoDestino = validar((string)Session["puertoDestino"], "(Puerto de destino) ", lblPuertoD);
            String factura = validar((string)Session["factura"], "(factura) ", lblFactura);
            String nit = validar((string)Session["nit"], "(nit) ", lblNit);
            String tdn = validar((string)Session["tdn"], "(tdn) ", lblTdn);
            if (Session["encomendante"].ToString() != null && Session["encomendante"].ToString() != "")
            {
                txtEncomendante.Value = (string)Session["encomendante"];
                Session["1"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["1"] = "(encomendante) ";
            }
            //----------------
            if (Session["1"].ToString() == "" && cliente == "" && direccion == "" && pais == "" && telefono == "" && nomUsuCrea == "" && fecha == "" && puertoEmbarque == "" && puertoDestino == "" && factura == "" && nit == "" && tdn == "")
            {
                form1.Visible = true;
            }
            else
            {
                lblMensaje.Text = "No se puede mostrar hace falta los datos : " + Session["1"].ToString() + cliente + direccion + pais + telefono + nomUsuCrea + fecha + puertoEmbarque + puertoDestino + factura + nit + tdn;
            }
        }

        public String validar(String var, String retorna, Label lbl)
        {
            String men;
            if (var != null && var != "")
            {
                lbl.Text = var;
                men = "";
            }
            else
            {
                form1.Visible = false;
                men = retorna;
            }
            return men;
        }

        public void totalVolumen()
        {
            double suma = GridView2.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDouble(x.Cells[5].Text));
            lblTotalV.Text = Convert.ToString(suma);
        }
        public void totalPesoB()
        {
            double suma = GridView2.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDouble(x.Cells[6].Text));
            lblTotalB.Text = Convert.ToString(suma);
        }
        public void totalPesoN()
        {
            double suma = GridView2.Rows.Cast<GridViewRow>().Sum(x => Convert.ToDouble(x.Cells[7].Text));
            lblTotalN.Text = Convert.ToString(suma);
        }
        public void totalCantidadPall()
        {
            int total = int.Parse(GridView2.Rows.Count.ToString());
            lblCantPallets.Text = total.ToString();
        }
        protected void btnCerrar_Click(object sender, EventArgs e)
        {
            mataSesiones2();
        }
        public void mataSesiones2()
        {
            Session["cliente"] = "";
            Session["direrccion"] = "";
            Session["pais"] = "";
            Session["telefono"] = "";
            Session["nomUsuCrea"] = "";
            Session["fecha"] = "";
            Session["encomendante"] = "";
            Session["puertoEmbarque"] = "";
            Session["puertoDestino"] = "";
            Session["factura"] = "";
            Session["nit"] = "";
            Session["tdn"] = "";
            Session["numContenedor"] = "";
            Session["precinto"] = "";
        }
    }
}