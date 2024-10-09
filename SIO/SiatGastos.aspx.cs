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
    public partial class SiatGastos : System.Web.UI.Page
    { 
        private ControlPoliticas CP = new ControlPoliticas();
        private ControlSIAT CS = new ControlSIAT();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btnGuardarC.Visible = false;
                btnActC.Visible = false;
                trZonaC.Visible = false;
                cargarCombo(CS.cargarZonas(), cboZona, 0, 1);
                limCamCos();
                politicas(60, Session["usuario"].ToString());
            }
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
                btnGuardarC.Visible = true;
            }
            else
            {
                btnGuardarC.Visible = false;
            }

            if (editar == true)
            {
                btnActC.Visible = true;
            }
            else
            {
                btnActC.Visible = false;
            }
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
        //envia los mensajes en los alert
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        //limpia todos los campos de los costos
        private void limCamCos()
        {
            //Costos
            txtPlanHotel.Text = "";
            txtPlanTiq.Text = "";
            txtPlanAli.Text = "";
            txtPlanTranInt.Text = "";
            txtPlanTranAer.Text = "";
            txtPlanLlam.Text = "";
            txtPlanLav.Text = "";
            txtPlanOtros.Text = "";
            txtPlanPen.Text = "";
            //txtPlanObs.Text = "";
        }
        //valida todos los campos de los costos
        private String validaCamposCos()
        {
            String mensaje = "";
            if (txtPlanHotel.Text == "" || txtPlanTiq.Text == "" || txtPlanAli.Text == "" || txtPlanTranInt.Text == "" || txtPlanTranAer.Text == "" || txtPlanLlam.Text == "" || txtPlanLav.Text == "" || txtPlanOtros.Text == "" || txtPlanPen.Text == "")
            {
                mensaje = "Todos los campos deben de tener un valor";
            }
            else { mensaje = "OK"; }
            return mensaje;
        }
        //combo zonas
        protected void cboZona_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnActC.Visible = false;
            btnGuardarC.Visible = false;
            trZonaC.Visible = false;
            limCamCos();
            if (cboZona.SelectedItem.ToString() != "Seleccionar")
            {
                cargarCombo(CS.cargarPais(cboZona.SelectedValue.ToString()), cboPais, 0, 1);
            }
            else { cboPais.Items.Clear(); }
        }
        //combo pais
        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPais.SelectedItem.ToString() != "Seleccionar")
            {
                trZonaC.Visible = false;
                DataTable zonaC = CS.cargarZonasC(cboPais.SelectedValue.ToString());
                if (zonaC.Rows.Count > 0)
                {
                    Session["tipoPaisZona"] = "ZonaC";
                    trZonaC.Visible = true;
                    cargarCombo(zonaC, cboZonaC, 0, 1);
                }
                else
                {
                    Session["tipoPaisZona"] = "Pais";
                    trZonaC.Visible = false;
                    DataTable costos = CS.consultaCosP(" WHERE  (siat_cp_pai_id = " + cboPais.SelectedValue.ToString() + ") ");
                    if (costos.Rows.Count > 0)
                    {
                        btnGuardarC.Visible = false;
                        btnActC.Visible = true;
                        foreach (DataRow row in costos.Rows)
                        {
                            txtPlanHotel.Text = (decimal.Parse(row["hotel"].ToString())).ToString("N0");
                            txtPlanTiq.Text = (decimal.Parse(row["tiq"].ToString())).ToString("N0");
                            txtPlanAli.Text = (decimal.Parse(row["ali"].ToString())).ToString("N0");
                            txtPlanTranInt.Text = (decimal.Parse(row["transInt"].ToString())).ToString("N0");
                            txtPlanTranAer.Text = (decimal.Parse(row["transAereo"].ToString())).ToString("N0");
                            txtPlanLlam.Text = (decimal.Parse(row["llam"].ToString())).ToString("N0");
                            txtPlanLav.Text = (decimal.Parse(row["lav"].ToString())).ToString("N0");
                            txtPlanPen.Text = (decimal.Parse(row["penal"].ToString())).ToString("N0");
                            txtPlanOtros.Text = (decimal.Parse(row["otros"].ToString())).ToString("N0");
                        }
                    }
                    else
                    {
                        btnGuardarC.Visible = true;
                        btnActC.Visible = false;
                        limCamCos();
                    }
                }
            }
            else
            {
                limCamCos();
                btnGuardarC.Visible = false;
                btnActC.Visible = false;
                trZonaC.Visible = false;
            }
        }
        //boton guardar
        protected void btnGuardarC_Click(object sender, EventArgs e)
        {
            String mens = "";
            String filColum = "";
            String filId = "";
            mens = validaCamposCos();
            if (mens == "OK")
            {
                if (Session["tipoPaisZona"].ToString() == "Pais")
                {
                    filColum = "siat_cp_pai_id";
                    filId = cboPais.SelectedValue.ToString();
                }
                else
                {
                    filColum = "siat_cp_zona_id";
                    filId = cboZonaC.SelectedValue.ToString();
                }
                mens = CS.insertarCostosP(filColum, filId, txtPlanHotel.Text, txtPlanTiq.Text, txtPlanAli.Text, txtPlanTranInt.Text, txtPlanTranAer.Text, txtPlanLlam.Text, txtPlanLav.Text, txtPlanOtros.Text, Session["usuario"].ToString(), txtPlanPen.Text);
                if (mens == "OK")
                {
                    mensajeVentana("Se ha ingresado correctamente!");
                    btnActC.Visible = true;
                    btnGuardarC.Visible = false;
                }
                else { mensajeVentana(mens); }
            }
            else { mensajeVentana(mens); }
        }
        //boton actualizar
        protected void btnActC_Click(object sender, EventArgs e)
        {
            String mens = "";
            String filtro = "";
            mens = validaCamposCos();
            if (mens == "OK")
            {
                if (Session["tipoPaisZona"].ToString() == "Pais")
                {
                    filtro = "  WHERE (siat_cp_pai_id = " + cboPais.SelectedValue.ToString() + ")";
                }
                else
                {
                    filtro = "  WHERE (siat_cp_zona_id = " + cboZonaC.SelectedValue.ToString() + ")";
                }

                mens = CS.editarCostosP(filtro, txtPlanHotel.Text, txtPlanTiq.Text, txtPlanAli.Text, txtPlanTranInt.Text, txtPlanTranAer.Text, txtPlanLlam.Text, txtPlanLav.Text, txtPlanOtros.Text, Session["usuario"].ToString(), txtPlanPen.Text);
                if (mens == "OK")
                {
                    mensajeVentana("Se ha actualizado correctamente!");
                }
                else { mensajeVentana(mens); }
            }
            else { mensajeVentana(mens); }
        }
        //combo de las zonas de las ciudades
        protected void cboZonaC_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboZonaC.SelectedItem.ToString() != "Seleccionar")
            {
                DataTable costos = CS.consultaCosP(" WHERE  (siat_cp_zona_id = " + cboZonaC.SelectedValue.ToString() + ") ");
                if (costos.Rows.Count > 0)
                {
                    btnGuardarC.Visible = false;
                    btnActC.Visible = true;
                    foreach (DataRow row in costos.Rows)
                    {
                        txtPlanHotel.Text = (decimal.Parse(row["hotel"].ToString())).ToString("N0");
                        txtPlanTiq.Text = (decimal.Parse(row["tiq"].ToString())).ToString("N0");
                        txtPlanAli.Text = (decimal.Parse(row["ali"].ToString())).ToString("N0");
                        txtPlanTranInt.Text = (decimal.Parse(row["transInt"].ToString())).ToString("N0");
                        txtPlanTranAer.Text = (decimal.Parse(row["transAereo"].ToString())).ToString("N0");
                        txtPlanLlam.Text = (decimal.Parse(row["llam"].ToString())).ToString("N0");
                        txtPlanLav.Text = (decimal.Parse(row["lav"].ToString())).ToString("N0");
                        txtPlanPen.Text = (decimal.Parse(row["penal"].ToString())).ToString("N0");
                        txtPlanOtros.Text = (decimal.Parse(row["otros"].ToString())).ToString("N0");
                    }
                }
                else
                {
                    btnGuardarC.Visible = true;
                    btnActC.Visible = false;
                    limCamCos();
                }
            }
            else
            {
                limCamCos();
                btnGuardarC.Visible = false;
                btnActC.Visible = false;
            }
        }
    }
}