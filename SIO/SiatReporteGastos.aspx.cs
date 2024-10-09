using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Drawing;
namespace SIO
{
    public partial class SiatReporteGastos : System.Web.UI.Page
    {
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                limCamCos();
            }
        }
        //limpia todos los campos de los costos
        private void limCamCos()
        {   //Costos
            lblNomPais.Text = "";
            lblPlanHotel.Text = "";
            lblPlanTiq.Text = "";
            lblPlanAli.Text = "";
            lblPlanTran.Text = "";
            lblPlanLLam.Text = "";
            lblPlanLav.Text = "";
            lblPlanPenal.Text = "";
            lblPlanOtros.Text = "";
            lblPlanAereo.Text = "";
            lblRealHotel.Text = "0";
            lblRealTiq.Text = "0";
            lblRealAli.Text = "0";
            lblRealTran.Text = "0";
            lblRealLlam.Text = "0";
            lblRealLav.Text = "0";
            lblRealPenal.Text = "0";
            lblRealOtros.Text = "0";
            lblRealTrm.Text = "0";
            lblRealAereo.Text = "0";
            txtConsecutivo.Text = "";
            lblIdPais.Value = "";
            txtOF.Text = "";
        }

        private void cargarCostos()
        {
            //DataTable costosR = CS.consultaCosR(Session["SIATidViaje"].ToString());
            if (!String.IsNullOrEmpty(txtConsecutivo.Text))
            {
                int pais = 0;
                int dias = 0;
                DataTable dt = new DataTable();
                dt = CS.consultarPaisId(int.Parse(txtConsecutivo.Text));
                if (dt.Rows.Count > 0)
                {
                    pais = Convert.ToInt32(dt.Rows[0]["pais"]);
                    dias = Convert.ToInt32(dt.Rows[0]["dias"]);
                }
                if (pais != 0)
                {
                    lblIdPais.Value = pais.ToString();
                    PanelCostos.GroupingText = "COSTOS CP-" + txtConsecutivo.Text;
                    DataTable costosR = CS.buscarCostoRealErp(int.Parse(txtConsecutivo.Text), Convert.ToInt32(lblIdPais.Value));
                    if (costosR.Rows.Count > 0)
                    {
                        Session["SIATCostosReal"] = "SI";

                        foreach (DataRow row in costosR.Rows)
                        {
                            if (row["grupo"].ToString() == "1")
                            {
                                lblRealHotel.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }

                            else if (row["grupo"].ToString() == "2")
                            {
                                lblRealAli.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }

                            else if (row["grupo"].ToString() == "3")
                            {
                                lblRealTiq.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }

                            else if (row["grupo"].ToString() == "4")
                            {
                                lblRealPenal.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }

                            else if (row["grupo"].ToString() == "5")
                            {
                                lblRealTran.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }

                            else if (row["grupo"].ToString() == "6")
                            {
                                lblRealLlam.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }
                            else if (row["grupo"].ToString() == "7")
                            {
                                lblRealLav.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }
                            else if (row["grupo"].ToString() == "8")
                            {
                                lblRealOtros.Text = (decimal.Parse(row["valor"].ToString())).ToString("N0");
                            }
                        }
                    }
                    else { Session["SIATCostosReal"] = "NO"; }

                    decimal trm = CS.consultarTRM(int.Parse(txtConsecutivo.Text));
                    lblRealTrm.Text = trm.ToString("N0");
                    DataTable costosP = new DataTable();
                    costosP = CS.consultaCosP(" WHERE  (siat_cp_pai_id = " + int.Parse(lblIdPais.Value) + ")");

                    lblNomPais.Text = CS.consultarPais(int.Parse(lblIdPais.Value)).ToString().ToUpper();

                    foreach (DataRow row1 in costosP.Rows)
                    {
                        lblPlanHotel.Text = ((decimal.Parse(row1["hotel"].ToString()) * trm) * dias).ToString("N0");
                        lblPlanTiq.Text = (decimal.Parse(row1["tiq"].ToString()) * trm).ToString("N0");
                        lblPlanAli.Text = ((decimal.Parse(row1["ali"].ToString()) * trm) * dias).ToString("N0");
                        decimal transporteInterno = ((decimal.Parse(row1["transInt"].ToString())) * trm) * dias;
                        lblPlanTran.Text = transporteInterno.ToString("N0");
                        //lblPlanTranAer.Text = (decimal.Parse(row1["transAereo"].ToString())).ToString("N0");
                        lblPlanLLam.Text = ((decimal.Parse(row1["llam"].ToString()) * trm) * dias).ToString("N0");
                        lblPlanLav.Text = ((decimal.Parse(row1["lav"].ToString()) * trm) * dias).ToString("N0");
                        lblPlanPenal.Text = (decimal.Parse(row1["penal"].ToString()) * trm).ToString("N0");
                        lblPlanOtros.Text = (decimal.Parse(row1["otros"].ToString()) * trm).ToString("N0");
                        lblPlanAereo.Text = ((decimal.Parse(row1["transAereo"].ToString()) * trm)).ToString("N0");
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupCos')", true);
                }
                else
                {
                    mensajeVentana("Por favor cargue nuevamente el viaje. Gracias");
                }
            }

            else
            {
                mensajeVentana("Por favor cargue nuevamente el viaje. Gracias");
            }
        }

        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void txtConsecutivo_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtConsecutivo.Text))
            {
                cargarCostos();
            }
            else
            {
                limCamCos();
                mensajeVentana("Por favor cargue nuevamente el viaje. Gracias");
            }
        }

        protected void txtOF_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtOF.Text))
            {
                consultarViajeOf();
            }
            else
            {
                limCamCos();
                mensajeVentana("Por favor cargue nuevamente el viaje. Gracias");
            }
        }

        private void consultarViajeOf()
        {
            DataTable dt1 = new DataTable();
            if (!String.IsNullOrEmpty(txtOF.Text))
            {
                dt1 = CS.consultarViajeOf(txtOF.Text);
                if (dt1.Rows.Count > 0)
                {
                    cargarViajes();
                    btnBuscarOF();
                }
            }
        }

        private void cargarViajes()
        {
            cargarTabla(grdViajes, null);
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "abrirPopup('PopupBuscaVis')", true);
        }

        //Carga los grids
        private void cargarTabla(GridView grid, DataTable tabla)
        {
            grid.DataSource = tabla;
            grid.DataBind();
        }

        //Filtras las ofs para la busqueda
        protected void btnBuscarOF()
        {
            DataTable OFs = null;
            //String filtroBase = " (siat_viaje.siat_via_cargo = 'Tecnico')  AND (siat_of_viaje.siat_of_viaje_activo = 1) AND  (Orden.Numero + '-' + Orden.ano LIKE '%" + txtFiltroOF.Text + "%') ";
            String filtroBase = " (siat_viaje.siat_via_cargo = 'Tecnico')  AND (siat_of_viaje.siat_of_viaje_activo = 1) AND  (Orden.Numero + '-' + Orden.ano LIKE '%" + txtOF.Text + "%') ";

            OFs = CS.filtrarOF(" WHERE " + filtroBase);
            cargarTabla(grdViajes, OFs);
        }

        protected void btnSelOrden_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btn = sender as ImageButton;
            GridViewRow row = (GridViewRow)btn.NamingContainer;
            String cotizacion = this.grdViajes.DataKeys[row.RowIndex].Values["siat_cotizacion_id"].ToString();
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script41", "cerrarPopup('PopupBuscaVis')", true);
            txtConsecutivo.Text = cotizacion;
            cargarCostos();
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            Response.Redirect("SiatReporteGastos.aspx");
        }
    }
}