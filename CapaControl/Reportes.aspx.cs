using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using CapaControl;
using System.Data;
using CapaDatos;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Web.SessionState;
using System.Diagnostics;
using System.Threading;

namespace SIO
{
    public partial class CargaEstibas : System.Web.UI.Page
    {
        public ControlLogistica CL = new ControlLogistica();
        static List<InfoOrden> linfoOrd = new List<InfoOrden>();
        static List<InfoContenedor> linfCont = new List<InfoContenedor>();
        static InfoContenedor infco = new InfoContenedor();
        static InfoEstiba infoE = new InfoEstiba();
        static List<InfoActaTraza> linfTraza = new List<InfoActaTraza>();
        static InfoActaTraza infoT = new InfoActaTraza();
        static InfoPacking infoP = new InfoPacking();
        protected void Page_Load(object sender, EventArgs e)
        {
            ActaTraza.Visible = false;
            Packing.Visible = false;
        }
        protected void btnActaTraz_Click(object sender, EventArgs e)
        {
            lblMensaje.Text = "";
            ActaTraza.Visible = true;
            Packing.Visible = false;
            txtOrden2.Text = "";
            txtOrden.Focus();
            String fecha = System.DateTime.Now.ToString(("dd/MM/yyyy"));
            Session["fechaImp"] = fecha;
            mataSesiones();
        }
        protected void btnPacking_Click(object sender, EventArgs e)
        {
            lblMensaje2.Text = "";
            ActaTraza.Visible = false;
            Packing.Visible = true;
            txtOrden.Text = "";
            txtOrden2.Text = "";
            txtOrden2.Focus();
            cboPlaca.Items.Add("");
            cboPlaca.Items.Clear();
            String fecha = System.DateTime.Now.ToString(("dd/MM/yyyy"));
            Session["fechaImp"] = fecha;
            mataSesiones();
        }
        protected void cboPlaca_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ("Placa" != cboPlaca.SelectedValue)
            {
                int idTransporte = int.Parse(cboPlaca.SelectedValue);
                linfTraza = CL.buscarDatosTraza(idTransporte);
                if (linfTraza.Count > 0)
                {
                    foreach (InfoActaTraza inft in linfTraza)
                    {
                        Session["placa"] = inft.Placa;
                        Session["numConte"] = inft.NumContenedor.ToString();
                        Session["idTrans"] = inft.IdDespaTrans;
                        Session["inicioCargue"] = inft.InicioCargue.ToString();
                        Session["finCargue"] = inft.FinCargue.ToString();
                        Session["llegadaC"] = inft.LlegadaCamion.ToString();
                        Session["salidaC"] = inft.SalidaCamion.ToString();
                        Session["tipoExpo"] = inft.TipoExpo.ToString();
                        Session["tipoTrans"] = inft.TipoTrans.ToString();
                        Session["numTrailer"] = inft.NumTrailer.ToString();
                        Session["nombreLlegada"] = inft.NombreLlegada.ToString();
                        Session["nombreSalida"] = inft.NombreSalida.ToString();
                        Session["precintoLlegada"] = inft.PrecintoLlegada.ToString();
                        Session["precintoSalida"] = inft.PrecintoSalida.ToString();
                    }
                    infoT = CL.buscarDatosTraza2(int.Parse(Session["idTrans"].ToString()));
                    Session["pais"] = infoT.Pais.ToString();
                    sumaTotalPeso();
                    sumaTotalPesoNeto();
                    conteo();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "window.open('ActaTrazabilidad.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1200, height=500')", true);
                    ActaTraza.Visible = true;
                }
                else
                {
                    mataSesiones();
                    cboPlaca.Items.Add("");
                    cboPlaca.Items.Clear();
                    lblMensaje.Text = "Las relaciones estan mal asociadas!!";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';}, 3800) } , 100); ", true);
                    txtOrden.Text = "";
                    txtOrden.Focus();
                }
            }
            else
            {
                mataSesiones();
                txtOrden.Text = "";
                txtOrden.Focus();
                cboPlaca.Items.Add("");
                cboPlaca.Items.Clear();
            }
        }
        public void mataSesiones()
        {
            Session["placa"] = "";
            Session["numConte"] = "";
            Session["idTrans"] = "";
            Session["inicioCargue"] = "";
            Session["finCargue"] = "";
            Session["llegadaC"] = "";
            Session["salidaC"] = "";
            Session["pais"] = "";
            Session["orden2"] = "";
            Session["tipoExpo"] = "";
            Session["tipoTrans"] = "";
            Session["numTrailer"] = "";
            Session["nombreLlegada"] = "";
            Session["nombreSalida"] = "";
            Session["precintoLlegada"] = "";
            Session["precintoSalida"] = "";
        }
        protected void txtOrden_TextChanged(object sender, EventArgs e)
        {
            cboPlaca.Items.Clear();
            lblMensaje.Text = "";
            if (txtOrden.Text != "")
            {
                if (txtOrden.Text.Length < 8)
                {
                    String orden = txtOrden.Text;
                    linfCont = CL.buscarPlacaIdTrasn(orden);
                    cboPlaca.Items.Add("Placa");
                    if (linfCont.Count > 0)
                    {
                        foreach (InfoContenedor infd in linfCont)
                        {
                            cboPlaca.Items.Add(new ListItem(infd.TransPlaca.ToString(), infd.Desp_Trans_id.ToString()));
                        }
                    }
                    else
                    {
                        mataSesiones();
                        cboPlaca.Items.Add("");
                        cboPlaca.Items.Clear();
                        lblMensaje.Text = "No hay placas asociadas a esta orden!!";
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';}, 3800) } , 100); ", true);
                        txtOrden.Text = "";
                        txtOrden.Focus();
                    }
                }
                else
                {
                    cboPlaca.Items.Add("");
                    cboPlaca.Items.Clear();
                    lblMensaje.Text = ":: Longitud invalida!! ::";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';}, 3800) } , 100); ", true);
                    txtOrden.Text = "";
                    txtOrden.Focus();
                }
            }
            else
            {
                cboPlaca.Items.Add("");
                cboPlaca.Items.Clear();
                lblMensaje.Text = ":: No exite la orden actual!! ::";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje').style.display = 'none';}, 3800) } , 100); ", true);
                txtOrden.Text = "";
                txtOrden.Focus();
            }

        }
        protected void txtOrden2_TextChanged(object sender, EventArgs e)
        {
            lblMensaje2.Text = "";
            if (txtOrden2.Text != "")
            {
                if (txtOrden2.Text.Length < 8)
                {
                    String orden = txtOrden2.Text;
                    Session["orden2"] = orden;
                    infoP = CL.buscarDatosPacking(orden);
                    if (infoP.ToString() != "")
                    {
                        Session["cliente"] = infoP.Cliente.ToString();
                        Session["direccion"] = infoP.Direccion.ToString();
                        Session["pais"] = infoP.Pais.ToString();
                        Session["telefono"] = infoP.Telefono.ToString();
                        Session["fecha"] = infoP.Fecha.ToString();
                        Session["nomUsuCrea"] = infoP.UsuarioCreaDesp.ToString();
                        Session["encomendante"] = infoP.Encomendante.ToString();
                        Session["puertoEmbarque"] = infoP.PuertoEmbarque.ToString();
                        Session["puertoDestino"] = infoP.PuertoDestino.ToString();
                        Session["factura"] = infoP.Factura.ToString();
                        Session["nit"] = infoP.Nit.ToString();
                        Session["tdn"] = infoP.Tdn.ToString();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "window.open('PackingList.aspx', this.target, 'top=50, left=30, toolbar=no, location=no, status=no, menubar=no, scrollbars=yes, resizable=no, width=1200, height=500')", true);
                        Packing.Visible = true;
                    }
                    else
                    {
                        mataSesiones2();
                        lblMensaje2.Text = "Las relaciones estan mal asociadas!!";
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje2').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'none';}, 3800) } , 100); ", true);
                        txtOrden.Text = "";
                        txtOrden.Focus();
                    }
                }
                else
                {
                    lblMensaje2.Text = ":: Longitud invalida!! ::";
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje2').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'none';}, 3800) } , 100); ", true);
                    txtOrden.Text = "";
                    txtOrden.Focus();
                }
            }
            else
            {
                lblMensaje2.Text = ":: Por favor ingrese la orden!! ::";
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder1_lblMensaje2').style.color='red'; document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'inline'; setTimeout(function(){   document.getElementById('ContentPlaceHolder1_lblMensaje2').style.display = 'none';}, 3800) } , 100); ", true);
                txtOrden.Text = "";
                txtOrden.Focus();
            }
        }
        public void mataSesiones2()
        {
            Session["cliente"] = "";
            Session["direccion"] = "";
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
        public void sumaTotalPeso()
        {
            String conte = Session["idTrans"].ToString();
            int con = int.Parse(conte);
            DataTable tabla = CL.sumaPesoAlYACC(con);
            int suma = 0;
            foreach (DataRow dr in tabla.Rows)
            {
                suma += Convert.ToInt32(dr[0]);
                Session["pesoB"] = suma.ToString();
            }
        }
        public void sumaTotalPesoNeto()
        {
            String conte = Session["idTrans"].ToString();
            int con = int.Parse(conte);
            DataTable tabla = CL.sumaPesoParaNeto(con);
            int suma = 0;
            foreach (DataRow dr in tabla.Rows)
            {
                suma += Convert.ToInt32(dr[0]);
                Session["pesoP"] = suma.ToString();
            }
        }
        public void conteo()
        {
            int idTrans = int.Parse(Session["idTrans"].ToString());
            //Pallet total cargados en la orden
            String sql = sql = " SELECT     'PalletsAluminio' AS tipo, COUNT(pallet_aluminio.pallet_al_id) AS CANT "
                             + " FROM         pallet_aluminio INNER JOIN "
                             + " Orden AS Orden_1 INNER JOIN "
                             + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                             + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                             + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId ON pallet_aluminio.pallet_al_Id_ofa = Orden_1.Id_Ofa "
                             + " WHERE     (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (pallet_aluminio.pallet_al_cant > 0) AND  "
                             + " (Desp_Transporte_1.Desp_Trans_Id = " + idTrans + ") "
                             + " GROUP BY pallet_aluminio.Pallet_Trans_Id "
                             + " UNION ALL "
                             + " SELECT     'PalletsAcc' AS tipo, COUNT(pallet_acc.pallet_acc_id) AS CANT "
                             + " FROM         Orden AS Orden_1 INNER JOIN "
                             + " Despa_Cliente AS Despa_Cliente_1 LEFT OUTER JOIN "
                             + " Desp_Transporte AS Desp_Transporte_1 ON Despa_Cliente_1.DesC_Grp_Id = Desp_Transporte_1.Desp_Grp_Id ON  "
                             + " Orden_1.Id_Ofa = Despa_Cliente_1.DesC_OfPId INNER JOIN "
                             + " pallet_acc ON Orden_1.Id_Ofa = pallet_acc.pallet_acc_id_of_p "
                             + " WHERE     (Orden_1.Despachada = 0) AND (Orden_1.Fec_Desp IS NULL) AND (Orden_1.Anulada = 0) AND (Desp_Transporte_1.Desp_Trans_Id = " + idTrans + ") AND  "
                             + " (pallet_acc.pallet_acc_cant > 0) "
                             + " GROUP BY pallet_acc.pallet_acc_id ";
            int cantidadByP = CL.conteo(sql);
            Session["cantidadByP"] = cantidadByP.ToString();
        }

    }

}