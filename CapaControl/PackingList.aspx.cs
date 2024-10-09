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
            GridView2.DataSource = CL.cargarGridPacking1(orden2);
            GridView2.DataBind();
            GridView1.DataSource = CL.cargarGridPacking2(orden2);
            GridView1.DataBind();
            totalVolumen();
            totalPesoB();
            totalPesoN();
            totalCantidadPall();
            if (Session["cliente"].ToString() != null && Session["cliente"].ToString() != "")
            {
                lblCliente.Text = (string)Session["cliente"];
                Session["1"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["1"] = "(cliente) ";
            }
            //
            if (Session["direccion"].ToString() != null && Session["direccion"].ToString() != "")
            {
                lblDireccion.Text = (string)Session["direccion"];
                Session["2"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["2"] = "(direccion) ";
            }
            //
            if (Session["pais"].ToString() != null && Session["pais"].ToString() != "")
            {
                lblPais.Text = (string)Session["pais"];
                Session["3"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["3"] = "(pais) ";
            }
            //
            if (Session["telefono"].ToString() != null && Session["telefono"].ToString() != "")
            {
                lblTelefono.Text = (string)Session["telefono"];
                Session["4"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["4"] = "(telefono) ";
            }
            //
            if (Session["nomUsuCrea"].ToString() != null && Session["nomUsuCrea"].ToString() != "")
            {
                lblUsuarioCreaDes.Text = (string)Session["nomUsuCrea"];
                Session["5"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["5"] = "(usuario creador del despacho) ";
            }
            //
            if (Session["fecha"].ToString() != null && Session["fecha"].ToString() != "")
            {
                lblFecha.Text = (string)Session["fecha"];
                Session["6"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["6"] = "(fecha) ";
            }
            //
            if (Session["encomendante"].ToString() != null && Session["encomendante"].ToString() != "")
            {
                txtEncomendante.Value = (string)Session["encomendante"];
                Session["7"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["7"] = "(encomendante) ";
            }
            //
            if (Session["puertoEmbarque"].ToString() != null && Session["puertoEmbarque"].ToString() != "")
            {
                lblPuertoE.Text = (string)Session["puertoEmbarque"];
                Session["8"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["8"] = "(Puerto de embarque) ";
            }
            //
            if (Session["puertoDestino"].ToString() != null && Session["puertoDestino"].ToString() != "")
            {
                lblPuertoD.Text = (string)Session["puertoDestino"];
                Session["9"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["9"] = "(Puerto de destino) ";
            }
            //
            if (Session["factura"].ToString() != null && Session["factura"].ToString() != "")
            {
                lblFactura.Text = (string)Session["factura"];
                Session["10"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["10"] = "(factura) ";
            }
            //
            if (Session["nit"].ToString() != null && Session["nit"].ToString() != "")
            {
                lblNit.Text = (string)Session["nit"];
                Session["11"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["11"] = "(nit) ";
            }
            //
            if (Session["tdn"].ToString() != null && Session["tdn"].ToString() != "")
            {
                lblTdn.Text = (string)Session["tdn"];
                Session["12"] = "";
            }
            else
            {
                form1.Visible = false;
                Session["12"] = "(tdn) ";
            }
            //
            if (Session["1"].ToString() == "" && Session["2"].ToString() == "" && Session["3"].ToString() == "" && Session["4"].ToString() == "" && Session["5"].ToString() == "" && Session["6"].ToString() == "" && Session["7"].ToString() == "" && Session["8"].ToString() == "" && Session["9"].ToString() == "" && Session["10"].ToString() == "" && Session["11"].ToString() == "" && Session["12"].ToString() == "")
            {
                form1.Visible = true;
            }
            else
            {
                lblMensaje.Text = "No se puede mostrar hace falta los datos : " + Session["1"].ToString() + Session["2"].ToString() + Session["3"].ToString() + Session["4"].ToString() + Session["5"].ToString() + Session["6"].ToString() + Session["7"].ToString() + Session["8"].ToString() + Session["9"].ToString() + Session["10"].ToString() + Session["11"].ToString() + Session["12"].ToString();
            }
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
            String orden2 = (string)Session["orden2"];
            String sql = "SELECT 'PalletsAccesorios' AS tipo, COUNT(pallet_acc_id_of_p) AS CANT "
                 + " FROM   Orden INNER JOIN "
                 + " pallet_acc ON Orden.Id_Ofa = pallet_acc.pallet_acc_id_of_p "
                 + " WHERE (Orden.Numero + '-' + Orden.ano = '" + orden2 + "') AND (pallet_acc.pallet_acc_peso > '0') "
                 + " UNION ALL "
                 + " SELECT 'PalletsAluminio' AS tipo, COUNT(pallet_al_id) AS CANT "
                 + " FROM Orden INNER JOIN "
                 + " pallet_aluminio ON Orden.Id_Ofa = pallet_aluminio.pallet_al_Id_ofa "
                 + " WHERE (Orden.Numero + '-' + Orden.ano = '" + orden2 + "') AND (pallet_aluminio.pallet_al_peso > '0') ";
            lblCantPallets.Text = CL.conteo(sql).ToString();
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