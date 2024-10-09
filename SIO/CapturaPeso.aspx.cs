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
using Microsoft.Reporting.WebForms;
using System.Net;

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
            txtCedulaUsuario.Focus();   
            Timer1_Tick(sender, e);
            //Timer2_Tick(sender, e);      
            int plantaColombia = (int)Session["plantaColombia"];
            if (plantaColombia != 1) chkManual.Checked = true; else chkManual.Checked = false;
        }

        protected void txtPeso_TextChanged(object sender, EventArgs e)
        {
            String pesoR = "0";
            int tamano = txtPeso.Text.Length;
            if (chkManual.Checked != true)
                pesoR = txtPeso.Text.Substring((tamano - 6), 6);
            else
                pesoR = txtPeso.Text;

            txtPeso.Text = pesoR;
            float vol = float.Parse(txtVolumen.Value);
            DateTime horaInicio = DateTime.Now;
            int Segundos = horaInicio.Second;
            int noimp = int.Parse(txtPeso.Text);
            if (chkDevolver.Checked)
            {
                chbImpEtiqueta.Checked = false;
            }
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
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {alert('El peso no puede ser igual o menor a Cero (0)')}, 1500);  var idapplet = document.getElementById('idapplet'); idapplet.cerrarBat();", true);
                ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()}, 5000);", true);
            }
            else
            {
                guardarDatos();
                Session["PesoB"] = txtPeso.Text;
                limpiarCampos();
            }
        }
        /*----------------------------------------BUSCAR TAMANO -------------------------------------*/
        //Aqui realizamnos la operacion para poder calcular el tamano de la estiba y poder identificar si es accesorios o aluminio
        protected void Button1_Click1(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtCedulaUsuario.Text))
            {
                txtCedulaUsuario_TextChanged(sender, e);
            }
            else
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
                        lblNumP.Text = InfoEst.NumP.ToString();
                        txtPeso.Text = InfoEst.Peso.ToString();
                        txtAncho.Value = InfoEst.Ancho.ToString();
                        txtLargo.Value = InfoEst.Largo.ToString();
                        txtAlto.Value = InfoEst.Alto.ToString();
                        txtVolumen.Value = InfoEst.Volumen.ToString();
                        lblTipoAcAl.Text = log.tabla.ToUpper();
                        Session["nombrecliente"] = InfoEst.Nombrecliente;
                        Session["direccion"] = InfoEst.Direccion;
                        Session["cantidad"] = InfoEst.CantiP.ToString();
                        string a = InfoEst.CantiP.ToString();
                        Session["codigoBarr"] = InfoEst.CodBarra;
                        Session["codigoBarrC"] = InfoEst.CodBarraC;
                        Session["PesoP"] = int.Parse(InfoEst.PesoPallet.ToString());
                        Session["alumacce"] = InfoEst.Material.ToString();
                        Session["idpallet"] = InfoEst.Idpallet;
                        Session["tipoOrden"] = InfoEst.TipoOrden;
                        //txtPeso.Text = "-1";
                        if (chkManual.Checked == true) txtPeso.Text = "0"; else txtPeso.Text = "-1";
                         
                        txtPeso.Focus();
                        ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function () {document.getElementById('ContentPlaceHolder1_txtPeso').focus()}, 1750);", true);
                    }
                    else
                    {
                        limpiarCampos();
                        //txtCodigoEstiba.Focus();
                    }
                }
                else
                {
                    mensajeVentana("El numero de remision " + txtCodigoEstiba.Text + " no es valido");
                    limpiarCampos();                    
                    //txtCodigoEstiba.Focus();
                }
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
            txtCedulaUsuario.Text = "";
            lblNombreUsuario.Text = "";
            lblCodigoUsuario.Text = "";
            chbImpEtiqueta.Checked = true;
            chkDevolver.Checked = false;
            txtCedulaUsuario.Focus();
            lblFechaLog.Value = "";
            lblCorreo.Value = "";
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
                int idPallet = 0;
                if (log.tabla.ToUpper().Trim() == "ALUMINIO")
                {
                   

                    DataTable dt = new DataTable();
                    dt = CL.consultarIdPalletAluminio(idOfpa, int.Parse(lblNumPallet1.Text));
                    idPallet = Convert.ToInt32(dt.Rows[0]["id"]);
                    double pesoAnterior = Convert.ToDouble(dt.Rows[0]["peso"]);

                    int devolver = 0;
                    bool solicitado = false;
                    DataTable dts = new DataTable();
                    string correosolicita = "";
                    string usuariosolicita = "";
                    string areasolicita = "";
                    int area_id = 0;
                    if (chkDevolver.Checked)
                    {
                        solicitado = CL.consultarSolicitudPalletAl(idPallet);
                        if (solicitado)
                        {
                            devolver = 1;
                            txtPeso.Text = "0";
                            dts = CL.getDatosSolicitante(idPallet, Convert.ToInt32(log.tipo));
                            if(dts.Rows.Count > 0)
                            {
                                correosolicita = dts.Rows[0]["correo"].ToString();
                                usuariosolicita = dts.Rows[0]["usuario"].ToString();
                                areasolicita = dts.Rows[0]["area"].ToString();
                                area_id = Convert.ToInt32(dts.Rows[0]["are_id"]);
                            }
                        }
                    }

                    if (devolver == 0 && chkDevolver.Checked)
                    {
                    }
                    else
                    { 
                        CL.actualizarDatosAluminio(idOfpa, int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet, lblCodigoUsuario.Text);
                        //Session["alumacce"] = log.tabla;                    

                        int id = CL.insertarLogPeso(idPallet, Convert.ToInt32(log.tipo), double.Parse(txtPeso.Text), usuConet, Convert.ToInt32(lblCodigoUsuario.Text), devolver, pesoAnterior);
                        string fecha = CL.getFechaLogPeso(id);
                        lblFechaLog.Value = fecha;
                    }
                   
                    mensajeDevolucion(devolver, idPallet, Convert.ToInt32(log.tipo), correosolicita, usuariosolicita, areasolicita, area_id);

                }
                else if (log.tabla.ToUpper() == "ACCESORIOS")
                {
                    int idpalletacc = (int)Session["idpallet"];
                    DataTable dt = new DataTable();
                    dt = CL.consultarIdPalletAccesorio(idpalletacc);
                    idPallet = Convert.ToInt32(dt.Rows[0]["id"]);
                    double pesoAnterior = Convert.ToDouble(dt.Rows[0]["peso"]);

                    int devolver = 0;
                    bool solicitado = false;
                    DataTable dts = new DataTable();
                    string correosolicita = "";
                    string usuariosolicita = "";
                    string areasolicita = "";
                    int area_id = 0;
                    if (chkDevolver.Checked)
                    {
                        solicitado = CL.consultarSolicitudPalletAcc(idPallet);
                        if (solicitado)
                        {
                            devolver = 1;
                            txtPeso.Text = "0";
                            dts = CL.getDatosSolicitante(idPallet, Convert.ToInt32(log.tipo));
                            if (dts.Rows.Count > 0)
                            {
                                correosolicita = dts.Rows[0]["correo"].ToString();
                                usuariosolicita = dts.Rows[0]["usuario"].ToString();
                                areasolicita = dts.Rows[0]["area"].ToString();
                                area_id = Convert.ToInt32(dts.Rows[0]["are_id"]);
                            }
                        }
                    }

                    if (devolver == 0 && chkDevolver.Checked)
                    {
                    }
                    else
                    {
                        CL.actualizarDatosAccesorios(lblNumOrden1.Text.ToString(), int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet, idpalletacc, lblCodigoUsuario.Text);

                        int id = CL.insertarLogPeso(idPallet, Convert.ToInt32(log.tipo), double.Parse(txtPeso.Text), usuConet, Convert.ToInt32(lblCodigoUsuario.Text), devolver, pesoAnterior);
                        string fecha = CL.getFechaLogPeso(id);
                        lblFechaLog.Value = fecha; 
                    }
                   
                    mensajeDevolucion(devolver, idPallet, Convert.ToInt32(log.tipo), correosolicita, usuariosolicita, areasolicita, area_id);
                    Session["alumacce"] = log.tabla;
                }

                cargarReporte(idPallet.ToString(), log.tipo.ToString());
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
                        int idPallet = 0;
                        if (log.tabla.ToUpper().Trim() == "ALUMINIO")
                        {
                            DataTable dt = new DataTable();
                            dt = CL.consultarIdPalletAluminio(idOfpa, int.Parse(lblNumPallet1.Text));
                            idPallet = Convert.ToInt32(dt.Rows[0]["id"]);
                            double pesoAnterior = Convert.ToDouble(dt.Rows[0]["peso"]);

                            int devolver = 0;
                            bool solicitado = false;
                            DataTable dts = new DataTable();
                            string correosolicita = "";
                            string usuariosolicita = "";
                            string areasolicita = "";
                            int area_id = 0;
                            if (chkDevolver.Checked)
                            {
                                solicitado = CL.consultarSolicitudPalletAl(idPallet);
                                if (solicitado)
                                {
                                    devolver = 1;
                                    txtPeso.Text = "0";
                                    dts = CL.getDatosSolicitante(idPallet, Convert.ToInt32(log.tipo));
                                    if (dts.Rows.Count > 0)
                                    {
                                        correosolicita = dts.Rows[0]["correo"].ToString();
                                        usuariosolicita = dts.Rows[0]["usuario"].ToString();
                                        areasolicita = dts.Rows[0]["area"].ToString();
                                        area_id = Convert.ToInt32(dts.Rows[0]["are_id"]);
                                    }
                                }
                            }
                            if (devolver == 0 && chkDevolver.Checked)
                            {
                            }
                            else
                            {
                                CL.actualizarDatosAluminio(idOfpa, int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet, lblCodigoUsuario.Text);
                                //Session["alumacce"] = log.tabla;

                                int id = CL.insertarLogPeso(idPallet, Convert.ToInt32(log.tipo), double.Parse(txtPeso.Text), usuConet, Convert.ToInt32(lblCodigoUsuario.Text), devolver, pesoAnterior);
                                string fecha = CL.getFechaLogPeso(id);
                                lblFechaLog.Value = fecha;
                            }

                            mensajeDevolucion(devolver, idPallet, Convert.ToInt32(log.tipo), correosolicita, usuariosolicita, areasolicita, area_id);
                            //if (chbImpEtiqueta.Checked)
                            //{
                            //    vistaprevia();
                            //    limpiarCampos();
                            //}
                            //else
                            //{
                            //    txtCodigoEstiba.Focus();
                            //    ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
                            //    limpiarCampos();
                            //}
                        }
                        else if (log.tabla.ToUpper() == "ACCESORIOS")
                        {
                            int idpalletacc = (int)Session["idpallet"];
                            DataTable dt = new DataTable();
                            dt = CL.consultarIdPalletAccesorio(idpalletacc);
                            idPallet = Convert.ToInt32(dt.Rows[0]["id"]);
                            double pesoAnterior = Convert.ToDouble(dt.Rows[0]["peso"]);

                            int devolver = 0;
                            bool solicitado = false;
                            DataTable dts = new DataTable();
                            string correosolicita = "";
                            string usuariosolicita = "";
                            string areasolicita = "";
                            int area_id = 0;
                            if (chkDevolver.Checked)
                            {
                                solicitado = CL.consultarSolicitudPalletAcc(idPallet);
                                if (solicitado)
                                {
                                    devolver = 1;
                                    txtPeso.Text = "0";
                                    dts = CL.getDatosSolicitante(idPallet, Convert.ToInt32(log.tipo));
                                    if (dts.Rows.Count > 0)
                                    {
                                        correosolicita = dts.Rows[0]["correo"].ToString();
                                        usuariosolicita = dts.Rows[0]["usuario"].ToString();
                                        areasolicita = dts.Rows[0]["area"].ToString();
                                        area_id = Convert.ToInt32(dts.Rows[0]["are_id"]);
                                    }
                                }
                            }

                            if (devolver == 0 && chkDevolver.Checked)
                            {
                            }
                            else
                            {
                                CL.actualizarDatosAccesorios(lblNumOrden1.Text.ToString(), int.Parse(lblNumPallet1.Text), txtPeso.Text.Replace(",", "."), txtAncho.Value.Replace(",", "."), txtAlto.Value.Replace(",", "."), txtLargo.Value.Replace(",", "."), txtVolumen.Value.Replace(",", "."), 4, usuConet, idpalletacc, lblCodigoUsuario.Text);

                                int id = CL.insertarLogPeso(idPallet, Convert.ToInt32(log.tipo), double.Parse(txtPeso.Text), usuConet, Convert.ToInt32(lblCodigoUsuario.Text), devolver, pesoAnterior);
                                string fecha = CL.getFechaLogPeso(id);
                                lblFechaLog.Value = fecha;
                            }

                            mensajeDevolucion(devolver, idPallet, Convert.ToInt32(log.tipo), correosolicita, usuariosolicita, areasolicita, area_id);
                            Session["alumacce"] = log.tabla;
                        }



                        if (chbImpEtiqueta.Checked)
                        {
                            vistaprevia();
                            limpiarCampos();
                        }
                        else
                        {
                            //txtCodigoEstiba.Focus();
                            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){document.getElementById('ContentPlaceHolder1_txtCodigoEstiba').focus()} , 2000)", true);
                            limpiarCampos();
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
            int idPalletfinal=(int)Session["idpallet"];
            cargarReporte(idPalletfinal.ToString(), log.tipo.ToString());
        }
        /*------------------------------------BOTON GUARDAR--------------------------------*/
        /****---------------------------------------------------VISTA DE LA IMPRESION -------------------------------------------------*****/
        public void vistaprevia()
        {
            int pesoPall = 0;
            int pesoN = 0;
            try
            {
                pesoPall = int.Parse(Session["PesoP"].ToString());
                pesoN = (int.Parse(txtPeso.Text) - pesoPall);
            }
            catch
            {
                pesoN = 0;
            }
           
            Session["ciudad1"] = lblCiudad1.Text;
            Session["pais1"] = lblPais1.Text;
            Session["orden1"] = lblNumOrden1.Text;
            Session["numpallet1"] = lblNumPallet1.Text;
            Session["nump"] = lblNumP.Text;
            Session["volum"] = txtVolumen.Value;
            Session["nombrecliente"] = Session["nombrecliente"];
            Session["direccion"] = Session["direccion"];
            Session["alumacceV"] = Session["alumacce"];
            Session["cantidadV"] = Session["cantidad"];
            string can = (string)Session["cantidad"];
            Session["codigoBarr"] = Session["codigoBarr"];
            Session["PesoN"] = pesoN.ToString();
            Session["codigoBarrC"] = Session["codigoBarrC"];
            Session["codigoUsuarioPeso"] = "Fecha: " + lblFechaLog.Value + " - " + lblCodigoUsuario.Text; 
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

        protected void txtCedulaUsuario_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txtCedulaUsuario.Text))
            {
                DataTable dt = new DataTable();
                dt = CL.consultarUsuario(txtCedulaUsuario.Text);
                if (dt.Rows.Count > 0)
                {
                    lblNombreUsuario.Text = dt.Rows[0]["nombre"].ToString().ToUpper();
                    lblCodigoUsuario.Text = dt.Rows[0]["codigo"].ToString();
                    lblNombreUsuario.ForeColor = System.Drawing.Color.White;
                    lblCorreo.Value = dt.Rows[0]["correo"].ToString();
                    txtCodigoEstiba.Focus();
                }
                else
                {
                    txtCedulaUsuario.Text = "";
                    txtCodigoEstiba.Text = "";
                    lblCodigoUsuario.Text = "";
                    lblNombreUsuario.Text = "USUARIO NO VÁLIDO";
                    lblNombreUsuario.Font.Bold = true;
                    lblNombreUsuario.ForeColor = System.Drawing.Color.Red;
                    lblCorreo.Value = "";
                    txtCedulaUsuario.Focus();
                }
            }
            else
            {
                txtCedulaUsuario.Text = "";
                txtCodigoEstiba.Text = "";
                lblCodigoUsuario.Text = "";
                lblNombreUsuario.Text = "USUARIO NO VÁLIDO";
                lblCorreo.Value = "";
                lblNombreUsuario.Font.Bold = true;
                lblNombreUsuario.ForeColor = System.Drawing.Color.Red;
                txtCedulaUsuario.Focus();
            }
        }
        
        private void mensajeDevolucion(int devolver, int idPallet, int tipo, string correosolicita, string usuariosolicita, string areasolicita, int are_id_solicita)
        {
            if (chkDevolver.Checked)
            {
                string msj = "";
                if(devolver == 1)
                {                              
                    string Nombre = lblNombreUsuario.Text;
                    string CorreoUsuario = lblCorreo.Value;            
                    string correoSistema = (string)Session["CorreoSistema"];
                    int planta = CL.consultarPlantaUsuario((string)Session["Usuario"]);
                    int area = Convert.ToInt32(Session["Area"]);
                    CL.CorreosLogistica(41, idPallet, tipo, area, Nombre, planta, CorreoUsuario, correoSistema, "","",0, "", correosolicita, usuariosolicita, areasolicita, are_id_solicita);
                    msj = "Pallet devuelto con éxito!";
                }
                else
                {
                    msj = "El pallet no está solicitado.";
                }

                mensajeVentana(msj);               
            }
        }

        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        private void cargarReporte(string pallet, string tipo)
        {
            PanelReporte.Visible = true;
            reportePeso.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("pallet", pallet, true));
            parametro.Add(new ReportParameter("tipo", tipo, true));
            this.reportePeso.KeepSessionAlive = true;
            this.reportePeso.AsyncRendering = true;
            reportePeso.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            reportePeso.ServerReport.ReportServerCredentials = irsc;
            reportePeso.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            reportePeso.ServerReport.ReportPath = "/Logistica/logLogisticaPeso";
            this.reportePeso.ServerReport.SetParameters(parametro);
            reportePeso.ShowToolBar = true;
        }

        public class CustomReportCredentials : IReportServerCredentials
        {
            private string _UserName;
            private string _PassWord;
            private string _DomainName;
            public CustomReportCredentials(string UserName, string PassWord, string DomainName)
            {
                _UserName = UserName;
                _PassWord = PassWord;
                _DomainName = DomainName;
            }

            public System.Security.Principal.WindowsIdentity ImpersonationUser
            {
                get { return null; }
            }
            public ICredentials NetworkCredentials
            {
                get { return new NetworkCredential(_UserName, _PassWord, _DomainName); }
            }
            public bool GetFormsCredentials
                (
                out Cookie authCookie,
                out string user,
                out string password,
                out string authority
                )
            { authCookie = null; user = password = authority = null; return false; }
        }

        //protected void chkDevolver_CheckedChanged(object sender, EventArgs e)
        //{
        //    txtCedulaUsuario.Focus();
        //}

        protected void btnReporte_Click(object sender, EventArgs e)
        {            
            Response.Redirect("LogisticaReporteSolicitudDevolucion.aspx");
        }

        protected void btnReporte0_Click(object sender, EventArgs e)
        {
            Response.Redirect("VerPalletPesoLog.aspx");
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            dt = CL.consultarSolicitudes();

            if (dt.Rows.Count > 0)
            {
                imgGif.Visible = true;
            }
            else
            {
                imgGif.Visible = false;
            }
        }

        //protected void Timer2_Tick(object sender, EventArgs e)
        //{
        //    if (String.IsNullOrEmpty(txtCedulaUsuario.Text))
        //    {
        //        txtCedulaUsuario.Focus();
        //    }
        //    else if (!String.IsNullOrEmpty(lblCodigoUsuario.Text) && (String.IsNullOrEmpty(txtPeso.Text) || double.Parse(txtPeso.Text) <= 0))
        //    {
        //        txtCodigoEstiba.Focus();
        //    }
        //    else
        //    {

        //    }
        //}
    }
}