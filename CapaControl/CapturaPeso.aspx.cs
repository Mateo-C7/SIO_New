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

namespace SIO
{
    public partial class CapturaPeso : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlLogistica CL = new ControlLogistica();
        public ELogcappeso log = null;
        public static String usuConet;
        protected void Page_Load(object sender, EventArgs e)
        {
            this.txtAncho.Attributes.Add("onkeypress", "KeyPressAncho()");
            this.txtLargo.Attributes.Add("onkeypress", "KeyPressLargo()");
            this.txtAlto.Attributes.Add("onkeypress", "KeyPressAlto()");
            this.btnGuardar.Attributes.Add("onkeypress", "KeyPressGuardar()");
            usuConet = (string)Session["Usuario"];
            txtCodigoEstiba.Focus();
        }
        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            int tamano = txtPeso.Text.Length;
            String pesoR = txtPeso.Text.Substring((tamano - 6), 6);
            txtPeso.Text = pesoR;
            float vol = float.Parse(txtVolumen.Value);
            DateTime horaInicio = DateTime.Now;
            int Segundos = horaInicio.Second;
            int noimp = int.Parse(txtPeso.Text);
            if (vol > 0 && chbImpEtiqueta.Checked && noimp > 0)
            {
                guardarDatos();
                do
                {
                    Session["PesoB"] = txtPeso.Text;
                    vistaprevia();
                    limpiarCampos();
                } while (Segundos < 5);
            }
            else if (noimp <= 0)
            {
                limpiarCampos();
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {alert('El peso no puede ser igual o menor a Cero (0)')}, 1500); ", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()}, 5000);", true);
            }
        }
        /*----------------------------------------BUSCAR TAMANO -------------------------------------*/
        //Aqui realizamnos la operacion para poder calcular el tamano de la estiba y poder identificar si es accesorios o aluminio
        protected void Button1_Click1(object sender, EventArgs e)
        {
            String CodEstiba = txtCodigoEstiba.Text;
            log = CL.buscar_Tamaño("log_cap_tamano", CodEstiba.ToString().Length.ToString());
            if (log != null)
            {
                int idOfpa = int.Parse(CodEstiba.ToString().Substring(0, log.tipo));
                int numPallet = int.Parse(CodEstiba.ToString().Substring(log.tipo, log.llave).ToString());
                InfoEstiba InfoEst = null;
                if (log.validar.Trim() != "")
                {
                    if (log.validar == idOfpa.ToString())
                    {// CASO ACCESORIOS -----------------------*
                        InfoEst = CL.buscarAccesorios(numPallet);
                    }
                }
                else
                {// CASO ALUMINIO -----------------------*
                    InfoEst = CL.buscarAluminio(idOfpa, numPallet);
                }
                if (InfoEst != null)
                {
                    lblNumOrden1.Text = InfoEst.Orden.ToString();
                    lblPais1.Text = InfoEst.Pais;
                    lblCiudad1.Text = InfoEst.Ciudad;
                    lblNumPallet1.Text = InfoEst.Numpallet.ToString();
                    txtPeso.Text = InfoEst.Peso.ToString();
                    txtAncho.Value = InfoEst.Ancho.ToString();
                    txtLargo.Value = InfoEst.Largo.ToString();
                    txtAlto.Value = InfoEst.Alto.ToString();
                    txtVolumen.Value = InfoEst.Volumen.ToString();
                    lblTipoAcAl.Text = log.tabla.ToUpper();
                    Session["nombrecliente"] = InfoEst.Nombrecliente;
                    Session["direccion"] = InfoEst.Direccion;
                    Session["cantidad"] = InfoEst.CantiP.ToString();
                    Session["codigoBarr"] = InfoEst.CodBarra;
                    Session["codigoBarrC"] = InfoEst.CodBarraC;
                    Session["PesoP"] = int.Parse(InfoEst.PesoPallet.ToString());
                    Session["alumacce"] = log.tabla;
                    txtPeso.Text = "-1";
                    txtPeso.Focus();
                    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtPeso').focus()}, 1750);", true);
                }
                else
                {
                    limpiarCampos();
                    txtCodigoEstiba.Focus();
                }
            }
            else
            {
                limpiarCampos();
                txtCodigoEstiba.Focus();
            }
        }
        /*----------------------------------------BUSCAR TAMANO -------------------------------------*/
        /*-----------------------------------------LIMPIAR CAMPOS--------------------------------------*/
        public void limpiarCampos()
        {
            lblTipoAcAl.Text = "";
            txtCodigoEstiba.Text = "";
            lblNumOrden1.Text = "";
            lblNumPallet1.Text = "";
            lblPais1.Text = "";
            lblCiudad1.Text = "";
            txtPeso.Text = "";
            txtAlto.Value = "";
            txtAncho.Value = "";
            txtLargo.Value = "";
            txtVolumen.Value = "";
            chbImpEtiqueta.Checked = true;
        }
        /*------------------------------------LIMPIAR CAMPOS------------------------------*/
        /*------------------------------------GUARDAR DATOS------------------------------*/
        public void guardarDatos()
        {
            String CodEstiba = txtCodigoEstiba.Text;
            log = CL.buscar_Tamaño("log_cap_tamano", CodEstiba.ToString().Length.ToString());
            if (log != null)
            {
                int idOfpa = int.Parse(CodEstiba.ToString().Substring(0, log.tipo));

                if (log.tabla.ToUpper().Trim() == "ALUMINIO")
                {
                    CL.actualizarDatosAluminio(idOfpa, int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet);
                    Session["alumacce"] = log.tabla;
                }
                else if (log.tabla.ToUpper() == "ACCESORIOS")
                {
                    CL.actualizarDatosAccesorios(lblNumOrden1.Text.ToString(), int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet);
                    Session["alumacce"] = log.tabla;
                }
            }
        }
        /*------------------------------------BOTON GUARDAR--------------------------------*/
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (txtPeso.Text != "")
            {
                if (float.Parse(txtPeso.Text) > 0 && float.Parse(txtVolumen.Value) > 0)
                {
                    String CodEstiba = txtCodigoEstiba.Text;
                    log = CL.buscar_Tamaño("log_cap_tamano", CodEstiba.ToString().Length.ToString());
                    if (log != null)
                    {
                        int idOfpa = int.Parse(CodEstiba.ToString().Substring(0, log.tipo));
                        if (log.tabla.ToUpper().Trim() == "ALUMINIO")
                        {
                            CL.actualizarDatosAluminio(idOfpa, int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet);
                            Session["alumacce"] = log.tabla;
                            if (chbImpEtiqueta.Checked)
                            {
                                vistaprevia();
                                limpiarCampos();
                            }
                            else
                            {
                                txtCodigoEstiba.Focus();
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
                                limpiarCampos();
                            }
                        }
                        else if (log.tabla.ToUpper() == "ACCESORIOS")
                        {
                            CL.actualizarDatosAccesorios(lblNumOrden1.Text.ToString(), int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet);
                            Session["alumacce"] = log.tabla;
                            if (chbImpEtiqueta.Checked)
                            {
                                vistaprevia();
                                limpiarCampos();
                            }
                            else
                            {
                                txtCodigoEstiba.Focus();
                                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
                                limpiarCampos();
                            }
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Verifique sus Datos (Peso, Volumen)')", true);
                }
            }
            else
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('Verifique sus Datos (Peso)')", true);
            }
        }
        /*------------------------------------BOTON GUARDAR--------------------------------*/
        /****---------------------------------------------------VISTA DE LA IMPRESION -------------------------------------------------*****/
        public void vistaprevia()
        {
            int pesoPall = int.Parse(Session["PesoP"].ToString());
            int pesoN = (int.Parse(txtPeso.Text) - pesoPall);
            Session["ciudad1"] = lblCiudad1.Text;
            Session["pais1"] = lblPais1.Text;
            Session["orden1"] = lblNumOrden1.Text;
            Session["numpallet1"] = lblNumPallet1.Text;
            Session["volum"] = txtVolumen.Value;
            Session["nombrecliente"] = Session["nombrecliente"];
            Session["direccion"] = Session["direccion"];
            Session["alumacceV"] = Session["alumacce"];
            Session["cantidad"] = Session["cantidad"];
            Session["codigoBarr"] = Session["codigoBarr"];
            Session["PesoN"] = pesoN.ToString();
            Session["codigoBarrC"] = Session["codigoBarrC"];
            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "imprimir(); ", true);
        }
        /****---------------------------------------------------VISTA DE LA IMPRESION -------------------------------------------------*****/
        /*----------------------------OPERACION DEL CAMPO VOLUMEN-----------------------------*/
        //Aqui se realiza la opearacion del ancho * alto * largo para el resultado del campo volumen
        public void operacionVolumen()
        {
            try
            {
                float alto = float.Parse(txtAlto.Value.Replace(",", "."));
                float ancho = float.Parse(txtAncho.Value.Replace(",", "."));
                float largo = float.Parse(txtLargo.Value.Replace(",", "."));
                txtVolumen.Value = (largo * ancho * alto).ToString();
            }
            catch (Exception)
            {
                txtVolumen.Value = "0";
            }
        }
        /*------------------------------OPERACION DEL CAMPO VOLUMEN----------------------------*/
        /*----------------------------------------------------------EVENTOS DE LOS BOTONES-----------------------------------------------------------*/
        //Aqui se realiza los eventos que realiza cada boton para poder hacer el salto al otro campo con el Focus 
        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                float peso = float.Parse(txtPeso.Text);
                txtAncho.Focus();
            }
            catch (Exception)
            {
                txtPeso.Text = "";
                txtPeso.Focus();
            }
            operacionVolumen();
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                float ancho = float.Parse(txtAncho.Value.Replace(".", ","));
                txtLargo.Focus();
            }
            catch (Exception)
            {
                txtAncho.Value = "0";
                txtAncho.Focus();
            }
            operacionVolumen();
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            try
            {
                float largo = float.Parse(txtLargo.Value.Replace(".", ","));
                txtAlto.Focus();
            }
            catch (Exception)
            {
                txtLargo.Value = "0";
                txtLargo.Focus();
            }
            operacionVolumen();
        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            try
            {
                float alto = float.Parse(txtAlto.Value.Replace(".", ","));
                txtVolumen.Focus();
            }
            catch (Exception)
            {
                txtAlto.Value = "0";
                txtAlto.Focus();
            }
            operacionVolumen();
        }

    }

}