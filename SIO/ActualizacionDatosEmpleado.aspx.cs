using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CapaControl;
using System.Net;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Diagnostics;

namespace SIO
{
    public partial class ActualizacionDatosEmpleado : System.Web.UI.Page
    {
        CapaControl.ControlActualizaDatosEmpleado ctrlactuemple = new ControlActualizaDatosEmpleado();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ctrlactuemple.ListarTipoEstudio(txt_Titulo);
                BloquearIntros();
                ctrlactuemple.ListarEmpresaContra(cbo_EmpresaContra);
                ctrlactuemple.ListarCargoActual(cbo_CargoActual);
                //boton de prueba que esta al lado derecho del boton cancelar busqueda
                // Button1.Visible = false;
                lblUsuario.Text = "1";
                txt_Identificacion.Enabled = true;
                //Evita un tipo de dato incorrecto
                txt_Celular.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                //Evita un tipo de dato incorrecto
                txt_Estracto.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                //Evitar un tipo de dato incorrecto en el modelo del vehiculo //28/08/2019/cristian sanchez
                txtModelMoto1.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtModelMoto2.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtModelo1.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                txtModelo2.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                //txt_Celular.Attributes.Add("Type", "number");
                Txt_TelefonoEmr.Attributes.Add("onKeyPress", " return valideKeyenteros(event,this);");
                //Txt_TelefonoEmr.Attributes.Add("Type", "number");
                //txt_FechNaci.Attributes.Add("Type", "date");
                //txt_Correo.Attributes.Add("Type", "email");
                //----------------------------------------------------------------------------------        
                pnlContent.Visible = false;
                txtPlacaMoto.Enabled = false;
                txtPlacaCarro.Enabled = false;
                txtPlacaCarro2.Enabled = false;
                txtPlacaMoto2.Enabled = false;
                txtMarca1.Enabled = false;
                txtMarca2.Enabled = false;
                txtModelo1.Enabled = false;
                txtModelo2.Enabled = false;
                txtModelMoto1.Enabled = false;
                txtModelMoto2.Enabled = false;
                txtMarMoto1.Enabled = false;
                txtMarMoto2.Enabled = false;
                //Ocultar label´s--------------
                lblTieneMoto.Visible = false;
                lbl_TieneCarro.Visible = false;
                lblTipoVivienda.Visible = false;
                LblCarnet.Visible = false;
                lblUsuario.Visible = false;
                lblnombreUsu.Visible = false;
                lblcargoemc_id.Visible = false;
                lblobtenercargo.Visible = false;
                //-----------------------------
                //Deshabilita los campos para ingresar una nueva persona que vive con el empleado
                Habilitar_PersConvive(false);
                Habilitar_HijosNoConvi(false);
                Habilitar_PersEmergen(false);
                Habilitar_Estudios(false);
                txt_AñoGradua.Enabled = false;
                //ToolTip
                txt_EdadConv.ToolTip = "Para especificar la edad en meses, seguir este ejemplo:(0.1 a 0.11) meses";
                txt_EdadHiNoConvi.ToolTip = "Para especificar la edad en meses, seguir este ejemplo:(0.1 a 0.11) meses";
            }
        }
        //establece un cargo por defecto cuando el empleado no cuenta con uno 
        public void Consultar_Cargo_Actual()
        {
            DataTable dt;
            if (ctrlactuemple.Consultar_Cargo_Actual(int.Parse(txt_Identificacion.Text)).Rows.Count != 0)
            {
                dt = ctrlactuemple.Consultar_Cargo_Actual(int.Parse(txt_Identificacion.Text));
                cbo_CargoActual.Text = dt.Rows[0]["car_id"].ToString();
                // ToolTip mensaje
                cbo_CargoActual.ToolTip = dt.Rows[0]["car_nombre"].ToString();
                //lblobtenercargo.Text = dt.Rows[0]["car_id"].ToString();            
            }
            else
            {
                cbo_CargoActual.Text = "SIN CARGO ACTUAL";//(SIN CARGO)        
            }
        }
        public void ToolTip_Cargo()
        {
            DataTable dt;
            dt = ctrlactuemple.Consultar_Cargo_Actual(int.Parse(txt_Identificacion.Text));
            //  cbo_CargoActual1.Text = dt.Rows[0]["car_id"].ToString();
            // ToolTip mensaje
            cbo_CargoActual.ToolTip = dt.Rows[0]["car_nombre"].ToString();
        }
        //establece una empresa por defecto cuando el empleado no cuenta con una
        public void Obtener_EmpresaContratante_Empleado()
        {
            DataTable dt;
            if (ctrlactuemple.Obtener_EmpresaContratante_Empleado(int.Parse(txt_Identificacion.Text)).Rows.Count != 0)
            {
                dt = ctrlactuemple.Obtener_EmpresaContratante_Empleado(int.Parse(txt_Identificacion.Text));
                cbo_EmpresaContra.Text = dt.Rows[0]["con_epr_id"].ToString();
            }
            else
            {
                cbo_EmpresaContra.Text = "SIN EMPRESA";// codigo 13
            }
        }
        //Busca el empleado por medio de su tipo de documento y su identificacion
        protected void btn_BuscarEmple_Click1(object sender, EventArgs e)
        {
            Buscar_EmpleadoXCedula();
        }
        public void BloquearIntros()
        {
            pnlVehiculo.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            pnlEstudios.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            pnlEmergencia.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            pnlHijoNoConvi.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            pnlPersConviven.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            PnlDatosGeneral.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            GridEstudios.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            GridConvivePers.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            GridEmergencia.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
            GridNoConvivePers.Attributes.Add("onkeypress", "javascript:if(event.keyCode==13){return false;}");
        }
        public void Buscar_EmpleadoXCedula()
        {
            DataTable dt;
            dt = null;
            string cadenaidenti = txt_Identificacion.Text;
            int resultado;
            if (txt_Identificacion.Text != "")
            {
                if (int.TryParse(cadenaidenti, out resultado))
                {
                    if (ctrlactuemple.Consultar_DatosGeneralEmpleado(int.Parse(txt_Identificacion.Text), cboCedula.Text).Rows.Count != 0)
                    {
                        lbl_MsgExisteEmple.Text = "";
                        pnlContent.Visible = true;
                        Consultar_PersConvivenEmpleado();
                        Consultar_Hijos_NoConvivenEmpleado();
                        Consultar_Estudios_Empleado();
                        Consultar_PersonaEmergencia_Empleado();
                        FotoEmple.Visible = true;
                        dt = ctrlactuemple.Consultar_DatosGeneralEmpleado(int.Parse(txt_Identificacion.Text), cboCedula.Text);
                        txt_Identificacion.Enabled = false;
                        cboCedula.Enabled = false;
                        //Limpia los combos
                        cboCiudadVereda.Items.Clear();
                        //Llena los combos
                        ctrlactuemple.ListarCiudadResideEmple(cboCiudadVereda);
                        //Llena los campos con los datos devueltos por la consulta                                                              
                        FotoEmple.ImageUrl = dt.Rows[0]["nombfoto"].ToString();
                        txt_Nombres.Text = dt.Rows[0]["emp_nombre"].ToString();
                        txt_Apellidos.Text = dt.Rows[0]["emp_apellidos"].ToString();
                        txt_DireccResidencia.Text = dt.Rows[0]["emp_direccion_residencia"].ToString();
                        txt_DireccResidencia.ToolTip = dt.Rows[0]["emp_direccion_residencia"].ToString();
                        txt_Barrio.Text = dt.Rows[0]["emp_barrio_residencia"].ToString();
                        cboCiudadVereda.Text = dt.Rows[0]["emp_ciu_id_residencia"].ToString();
                        txt_TeleFijo.Text = dt.Rows[0]["emp_telefono_residencia"].ToString();
                        txt_Celular.Text = dt.Rows[0]["emp_telefono_movil"].ToString();
                        txt_Estracto.Text = dt.Rows[0]["emp_estracto"].ToString();
                        cbo_TipoSangre.Text = dt.Rows[0]["emp_grupo_sanguineo"].ToString();
                        string licencia = dt.Rows[0]["emp_licencia_veh"].ToString();
                        if(licencia == "True")
                        {
                            ChklicenciaSi.Checked = true;
                        }
                        else if (licencia == "")
                        {
                            ChklicenciaNo.Checked = false;
                            ChklicenciaSi.Checked = false;
                        }
                        else
                        {
                            ChklicenciaNo.Checked = true;
                        }
                        //===============================
                        string RedSociales = dt.Rows[0]["emp_redes_sociales"].ToString();

                        if (RedSociales == "True")
                        {
                            chkRdesocialeSi.Checked = true;
                        }
                        else if (RedSociales == "")
                        {
                            chkRdesocialeSi.Checked = false;
                            chRdesocialesNo.Checked = false;
                        }
                        else
                        {
                            chRdesocialesNo.Checked = true;
                        }
                        //===============================
                        string publiFostos = dt.Rows[0]["emp_publica_fotos"].ToString();
                        if (publiFostos == "True")
                        {
                            chkpublicaFotoSi.Checked = true;
                        }
                        else if (publiFostos == "")
                        {
                            chkpublicaFotoSi.Checked = false;
                            chkpublicaFotoNo.Checked = false;
                        }
                        else
                        {
                            chkpublicaFotoNo.Checked = true;
                        }
                        //===============================
                        string RecDotacion = dt.Rows[0]["emp_recibe_dotacion"].ToString();
                        if (RecDotacion == "True")
                        {
                            chkrecibeDotacSi.Checked = true;
                            chkConReglaDotaSi.Enabled = true;
                            chkConReglaDotaNo.Enabled = true;

                            string ConRegDotacion = dt.Rows[0]["emp_con_regls_dota"].ToString();
                            if (ConRegDotacion == "True")
                            {
                                chkConReglaDotaSi.Checked = true;
                            }
                            else 
                            {                          
                                chkConReglaDotaNo.Checked = false;
                            }                       
                        }
                        else if (RecDotacion == "False")
                        {
                            chkrecibeDotacNo.Checked = true;
                            chkConReglaDotaNo.Enabled = false;
                            chkConReglaDotaSi.Enabled = false;
                        }
                        else
                        {
                            chkConReglaDotaNo.Enabled = false;
                            chkConReglaDotaSi.Enabled = false;
                        }
                       
                      
                        //===============================
                     
                        //===============================
                        string carro = dt.Rows[0]["emp_tiene_Carro"].ToString();
                        if (carro == "True")
                        {
                            chkSiPlacaCarro.Checked = true;
                            txtPlacaCarro.Enabled = true;
                            txtPlacaCarro2.Enabled = true;
                            txtModelo1.Enabled = true;
                            txtModelo2.Enabled = true;
                            txtMarca1.Enabled = true;
                            txtMarca2.Enabled = true;
                            


                        }
                        else
                        {
                            chkNoPlacaCarro.Checked = true;
                            txtPlacaCarro.Enabled = false;
                            txtPlacaCarro2.Enabled = false;
                            txtModelo1.Enabled = false;
                            txtModelo2.Enabled = false;
                            txtMarca1.Enabled = false;
                            txtMarca2.Enabled = false;
                           
                        }
                        string moto = dt.Rows[0]["emp_tiene_Moto"].ToString();
                        if (moto == "True")
                        {
                            chkSiPlacaMoto.Checked = true;
                            txtPlacaMoto.Enabled = true;
                            txtPlacaMoto2.Enabled = true;
                            txtModelMoto1.Enabled = true;
                            txtModelMoto2.Enabled = true;
                            txtMarMoto1.Enabled = true;
                            txtMarMoto2.Enabled = true;
                            
                        }
                        else
                        {
                            chkNoPlacaMoto.Checked = true;
                            txtPlacaMoto.Enabled = false;
                            txtPlacaMoto2.Enabled = false;
                            txtModelMoto1.Enabled = false;
                            txtModelMoto2.Enabled = false;
                            txtMarMoto1.Enabled = false;
                            txtMarMoto2.Enabled = false;
                        }
                        string vivienda = dt.Rows[0]["emp_vive_En_Casa"].ToString();
                        if (vivienda == "Alquilada")
                        {
                            chk_Alquilada.Checked = true;
                        }
                        else if (vivienda == "Propia")
                        {
                            chk_Propia.Checked = true;
                        }
                        else if (vivienda == "Otra")
                        {
                            chk_Otro.Checked = true;
                        }
                        else if (vivienda == "Familiar")
                        {
                            chk_Familiar.Checked = true;
                        }
                        else if (vivienda == "")
                        {

                        }
                        string carnet = dt.Rows[0]["emp_tiene_Carnet"].ToString();
                        if (carnet == "True")
                        {
                            chkCarnetSi.Checked = true;
                        }
                        else if (carnet == "False")
                        {
                            chkCarnetNo.Checked = true;
                        }
                        //====================================================
                        txt_FechNaci.Text = dt.Rows[0]["emp_fecha_nacimiento"].ToString();
                        DateTime FechNaci;
                        FechNaci = DateTime.Parse(txt_FechNaci.Text);
                        txt_FechNaci.Text = FechNaci.ToString("dd/MM/yyyy");
                        // ====================================================
                        cbo_EstadoCivil.Text = dt.Rows[0]["emp_estado_civil"].ToString();
                        txt_Correo.Text = dt.Rows[0]["emp_correo_electronico"].ToString();
                        Consultar_Cargo_Actual();
                        Obtener_EmpresaContratante_Empleado();
                        Obtener_Placas();
                        //consulta_carnet();
                    }
                    else
                    {
                        pnlContent.Visible = false;
                        txt_Identificacion.Enabled = true;
                        cboCedula.Enabled = true;
                        mensajeVentana("No se encontraron registros correspondiente a la identificacion" + " " + txt_Identificacion.Text);
                        //lbl_MsgExisteEmple.Text = "No se encontraron registros correspondiente a la identificacion" + " " + txt_Identificacion.Text;
                    }
                }
                else
                {
                    mensajeVentana("El campo identificacion contiene un tipo de dato incorrecto");
                    //lbl_MsgExisteEmple.Text = "El campo identificacion contiene un tipo de dato incorrecto";
                    txt_Identificacion.Focus();
                }
            }
            else
            {
                mensajeVentana("Ingrese el numero de identificacion del empleado");
                //lbl_MsgExisteEmple.Text = "Ingrese el numero de identificacion del empleado";
                pnlContent.Visible = false;
            }
        }
        public void Obtener_IdUsuario()
        {
            DataTable dt;
            dt = ctrlactuemple.Recuperar_ID_Usuario(lblnombreUsu.Text);
            lblUsuario.Text = dt.Rows[0][0].ToString();
        }
        //Boton para actualizar los datos del empleado
        protected void btn_ActualizarRegistro_Click(object sender, EventArgs e)
        {
            int respuesta;
            string cadenafecha = txt_FechNaci.Text;
            DateTime resultado;
            int sio_actualiza = 1;
            string fechaModi = DateTime.Now.ToString("dd/MM/yyyy");

            if (DateTime.TryParse(cadenafecha, out resultado))    //evalua que el tipo de dato en el campo sea correcto         
            {
                if (chkCarnetSi.Checked == true || chkCarnetNo.Checked == true)
                {
                    if (!String.IsNullOrEmpty(txt_Estracto.Text))
                    {
                        if (chkRdesocialeSi.Checked == true || chRdesocialesNo.Checked == true)
                        {
                            if (chkpublicaFotoSi.Checked == true || chkpublicaFotoNo.Checked == true)
                            {
                                if (chkrecibeDotacSi.Checked == true || chkrecibeDotacNo.Checked == true)
                                {
                                    if ((chkrecibeDotacSi.Checked == true && chkConReglaDotaSi.Checked == true || chkConReglaDotaNo.Checked == true)|| chkrecibeDotacSi.Checked == false)
                                    {                                    
                                        if (ChklicenciaSi.Checked == true || ChklicenciaNo.Checked == true)
                                        {
                                            if (chk_Familiar.Checked == true || chk_Alquilada.Checked == true || chk_Otro.Checked == true || chk_Propia.Checked == true)
                                            {
                                                if (chkSiPlacaCarro.Checked == true || chkNoPlacaCarro.Checked == true)
                                                {
                                                    if (chkSiPlacaMoto.Checked == true || chkNoPlacaMoto.Checked == true)
                                                    {

                                                        if (txt_Nombres.Text != "" && txt_Apellidos.Text != "" && txt_DireccResidencia.Text != "" && txt_Barrio.Text != "")
                                                        {
                                                            //Verifica que el numero de contactos de emergencia de un empleado sea igual a dos registro
                                                            if (ctrlactuemple.Consultar_Numero_Personas_Emergencia(int.Parse(txt_Identificacion.Text)).Rows.Count == 2)
                                                            {
                                                                if (chk_Alquilada.Checked == true)
                                                                {
                                                                    lblTipoVivienda.Text = "Alquilada";
                                                                }
                                                                else if (chk_Propia.Checked == true)
                                                                {
                                                                    lblTipoVivienda.Text = "Propia";
                                                                }
                                                                else if (chk_Otro.Checked == true)
                                                                {
                                                                    lblTipoVivienda.Text = "Otra";
                                                                }
                                                                else if (chk_Familiar.Checked == true)
                                                                {
                                                                    lblTipoVivienda.Text = "Familiar";
                                                                }
                                                                if (chkCarnetSi.Checked == true)
                                                                {
                                                                    LblCarnet.Text = "1";
                                                                }
                                                                else if (chkCarnetNo.Checked == true)
                                                                {
                                                                    LblCarnet.Text = "0";
                                                                }
                                                                //====informacion seguridad CheckBox=====
                                                                if (chkRdesocialeSi.Checked == true)
                                                                {
                                                                    lblRdesociales.Text = "1";
                                                                }
                                                                else if (chRdesocialesNo.Checked == true)
                                                                {
                                                                    lblRdesociales.Text = "0";
                                                                }
                                                                //
                                                                if (chkpublicaFotoSi.Checked == true)
                                                                {
                                                                    lblpublicaFotos.Text = "1";
                                                                }
                                                                else if (chkpublicaFotoNo.Checked == true)
                                                                {
                                                                    lblpublicaFotos.Text = "0";
                                                                }
                                                                //
                                                                if (chkrecibeDotacSi.Checked == true)
                                                                {
                                                                    lblrecibeDotac.Text = "1";
                                                                }
                                                                else if (chkrecibeDotacNo.Checked == true)
                                                                {
                                                                    lblrecibeDotac.Text = "0";
                                                                }
                                                                //
                                                                if (chkConReglaDotaSi.Checked == true)
                                                                {
                                                                    lblConReglaDota.Text = "1";
                                                                }
                                                                else if (chkConReglaDotaNo.Checked == true)
                                                                {
                                                                    lblConReglaDota.Text = "0";
                                                                }
                                                                else
                                                                {
                                                                    lblConReglaDota.Text = "-1";
                                                                }
                                                                //=========================0=====

                                                                if (ChklicenciaSi.Checked == true)
                                                                {
                                                                    lbllicencia.Text = "1";
                                                                }
                                                                else if (ChklicenciaNo.Checked == true)
                                                                {
                                                                    lbllicencia.Text = "0";
                                                                }
                                                                if (chkSiPlacaCarro.Checked == true)
                                                                {
                                                                    lbl_TieneCarro.Text = "1";
                                                                }

                                                                else if (chkNoPlacaCarro.Checked == true)
                                                                {
                                                                    lbl_TieneCarro.Text = "0";
                                                                }
                                                                if (chkSiPlacaMoto.Checked == true)
                                                                {
                                                                    lblTieneMoto.Text = "1";

                                                                }
                                                                else if (chkNoPlacaMoto.Checked == true)
                                                                {
                                                                    lblTieneMoto.Text = "0";
                                                                }

                                                                if (ctrlactuemple.Verificar_Existencia_Placas(int.Parse(txt_Identificacion.Text)).Rows.Count == 0)
                                                                {
                                                                    Agregar_Placa_Vehiculo();
                                                                }
                                                                else
                                                                {
                                                                    Actualizar_Placa_Vehiculo();
                                                                }
                                                                respuesta = ctrlactuemple.Actualizar_Empleado(txt_Nombres.Text, txt_Apellidos.Text, txt_DireccResidencia.Text,
                                                                                                              txt_Barrio.Text, int.Parse(cboCiudadVereda.Text), txt_TeleFijo.Text,
                                                                                                              txt_Celular.Text, cbo_TipoSangre.Text, txt_FechNaci.Text,
                                                                                                              cbo_EstadoCivil.Text, txt_Correo.Text, LblCarnet.Text,
                                                                                                              lblTipoVivienda.Text, lblTieneMoto.Text, int.Parse(txt_Estracto.Text),
                                                                                                              int.Parse(lbllicencia.Text), lbl_TieneCarro.Text, fechaModi, sio_actualiza,
                                                                                                              int.Parse(lblRdesociales.Text), int.Parse(lblpublicaFotos.Text), int.Parse(lblrecibeDotac.Text),
                                                                                                              int.Parse(lblConReglaDota.Text), int.Parse(txt_Identificacion.Text), cboCedula.Text);
                                                                Recuperar_EmpresaAnterior();
                                                                Recuperar_Empresa_CargoAnterior();

                                                                if (respuesta == 1)
                                                                {
                                                                    mensajeVentana("Registro actualizado");
                                                                    Limpiar_Formiulario();
                                                                    Limpiar_Labels_Mensages();
                                                                    Abrir_FormatoImpreso();
                                                                    txt_Identificacion.Text = "";
                                                                    txt_Identificacion.Focus();
                                                                }
                                                                else
                                                                {
                                                                    mensajeVentana("Registro fallido");
                                                                }
                                                            }
                                                            else
                                                            {
                                                                mensajeVentana("Debe registrar al menos dos contactos de emergencia");
                                                                btn_HabiliAgregarPersEmergen.Focus();
                                                            }
                                                        }
                                                        else
                                                        {
                                                            mensajeVentana("Los campos (Nombres,Apellidos y Direccion De Residencia son obligatorios");
                                                        }
                                                    }
                                                    else
                                                    {
                                                        mensajeVentana("Debe elejir si posee moto");
                                                        chkSiPlacaMoto.Focus();
                                                    }
                                                }
                                                else
                                                {
                                                    mensajeVentana("Debe elejir si posee carro");
                                                    chkSiPlacaCarro.Focus();
                                                }
                                            }
                                            else
                                            {
                                                mensajeVentana("Debe elejir un tipo de vivienda");
                                                chk_Familiar.Focus();
                                            }
                                        }
                                        else
                                        {
                                            mensajeVentana("Porfavor seleccione si tiene licencia de conducción Vigente");
                                            ChklicenciaSi.Focus();
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana("Selcione Una De las dos opciones Si conoce las reglas de dotacion.");
                                        chkConReglaDotaSi.Focus();
                                    }
                                }
                                    else
                                    {
                                        mensajeVentana("Selcione Una De las dos opciones Si recibe dotacion.");
                                        chkrecibeDotacSi.Focus();
                                    }
                                }
                                else
                                {
                                    mensajeVentana("Selcione Una De las dos opciones Hacerca de la publicacion de fotos En Redes sociales.");
                                    chkpublicaFotoSi.Focus();
                                }
                            }
                            else
                            {
                                mensajeVentana("Selcione Una De las dos opciones Sobre las Redes Rosiales.");
                                chkRdesocialeSi.Focus();
                            }
                        }
                        else
                        {
                            mensajeVentana("Debe ingresar el numero de estrato social");
                            txt_Estracto.Focus();
                        }
                    }
                    else
                    {
                        mensajeVentana("Debe elegir si tiene carnet");
                        chkCarnetSi.Focus();
                    }
                }
                else
                {
                    mensajeVentana("El campo fecha de nacimiento contiene un tipo de dato incorrecto");
                    txt_FechNaci.Focus();
                }
            }
        
        //Establece una nueva busqueda para llenar nuevos datos del empleados
        protected void btn_CancelarBuscar_Click(object sender, EventArgs e)
        {
            Limpiar_Formiulario();
            Limpiar_Labels_Mensages();
            txt_Identificacion.Text = "";
            txt_Identificacion.Enabled = true;
            txt_Identificacion.Focus();
        }
        
                      
           
        //Limpiar formulario 
        public void Limpiar_Formiulario()
        {
            // txt_Identificacion.Text = "";
            txt_Identificacion.Enabled = false;
            cboCedula.Enabled = true;
            pnlContent.Visible = false;
            FotoEmple.Visible = false;
            txtPlacaCarro.Text = "";
            txtPlacaMoto.Text = "";
            txtPlacaCarro2.Text = "";
            txtPlacaMoto2.Text = "";
            chkCarnetNo.Checked = false;
            chkCarnetSi.Checked = false;
            //
            chkRdesocialeSi.Checked = false;
            chRdesocialesNo.Checked = false;
            chkpublicaFotoSi.Checked = false;
            chkpublicaFotoNo.Checked = false;
            chkrecibeDotacSi.Checked = false;
            chkrecibeDotacNo.Checked = false;
            chkConReglaDotaSi.Checked = false;
            chkConReglaDotaNo.Checked = false;
            ChklicenciaSi.Checked = false;
            ChklicenciaNo.Checked = false;
            chkSiPlacaCarro.Checked = false;
            chkNoPlacaCarro.Checked = false;
            chkNoPlacaMoto.Checked = false;
            chkSiPlacaMoto.Checked = false;
            chk_Familiar.Checked = false;
            chk_Otro.Checked = false;
            chk_Propia.Checked = false;
            chk_Alquilada.Checked = false;
            txt_Estracto.Text = "";
            txtMarca1.Text = "";
            txtMarca2.Text = "";
            txtModelo1.Text = "";
            txtModelo2.Text = "";
            txtModelMoto1.Text = "";
            txtModelMoto2.Text = "";
            txtMarMoto1.Text = "";
            txtMarMoto2.Text = "";
        }
        public void Limpiar_Labels_Mensages()
        {
            lblEmergencia.Text = "";
            lblMsgEstudio.Text = "";
            lblMsgHijosNoConvi.Text = "";
            lblMsgPersCOnvive.Text = "";
        }

        //Habilita o deshabilita el campo semestre actual 

        //========================CheckBox Tipo Vehiculo======================================
        protected void chkCarnetSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCarnetSi.Checked == true)
            {
                LblCarnet.Text = "1";
                chkCarnetNo.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        protected void chkCarnetNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCarnetNo.Checked == true)
            {
                LblCarnet.Text = "0";
                chkCarnetSi.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        //===================================================================================
        //========================CheckBox Tipo Vehiculo=====================================
        protected void chkSiPlacaCarro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSiPlacaCarro.Checked == true)
            {
                chkNoPlacaCarro.Checked = false;
                txtPlacaCarro.Enabled = true;
                txtPlacaCarro2.Enabled = true;
                txtMarca1.Enabled = true;
                txtMarca2.Enabled = true;
                txtModelo1.Enabled = true;
                txtModelo2.Enabled = true;
                lbl_TieneCarro.Text = "1";
                lblMsgVehiculo.Text = "";
               
            }
        }
        protected void chkNoPlacaCarro_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoPlacaCarro.Checked == true)
            {
                chkSiPlacaCarro.Checked = false;
                txtPlacaCarro.Enabled = false;
                txtPlacaCarro2.Enabled = false;
                txtMarca1.Enabled = false;
                txtMarca2.Enabled = false;
                txtModelo1.Enabled = false;
                txtModelo2.Enabled = false;
                txtMarca1.Text = "";
                txtMarca2.Text = "";
                txtModelo1.Text = "";
                txtModelo2.Text = "";
                txtPlacaCarro.Text = "";
                txtPlacaCarro2.Text = "";
                lbl_TieneCarro.Text = "0";
                lblMsgVehiculo.Text = "";
            }
        }
        protected void chkSiPlacaMoto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSiPlacaMoto.Checked == true)
            {
                chkNoPlacaMoto.Checked = false;
                txtPlacaMoto.Enabled = true;
                txtPlacaMoto2.Enabled = true;
                txtModelMoto1.Enabled = true;
                txtModelMoto2.Enabled = true;
                txtMarMoto1.Enabled = true;
                txtMarMoto2.Enabled = true;
                lblTieneMoto.Text = "1";
                lblMsgVehiculo.Text = "";               
            }
        }
        protected void chkNoPlacaMoto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkNoPlacaMoto.Checked == true)
            {
                chkSiPlacaMoto.Checked = false;
                txtPlacaMoto.Enabled = false;
                txtPlacaMoto2.Enabled = false;
                txtModelMoto1.Enabled = false;
                txtModelMoto2.Enabled = false;
                txtMarMoto1.Enabled = false;
                txtMarMoto2.Enabled = false;
                txtModelMoto1.Text = "";
                txtModelMoto2.Text = "";
                txtMarMoto1.Text = "";
                txtMarMoto2.Text = "";
                txtPlacaMoto2.Text = "";
                txtPlacaMoto.Text = "";
                lblTieneMoto.Text = "0";
                lblMsgVehiculo.Text = "";
            }
        }
        //====================================================================================
        //========================CheckBox Tipo Vivienda======================================
        protected void chk_Familiar_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Familiar.Checked == true)
            {
                lblTipoVivienda.Text = "Familiar";
                chk_Alquilada.Checked = false;
                chk_Propia.Checked = false;
                chk_Otro.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        protected void chk_Propia_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Propia.Checked == true)
            {
                lblTipoVivienda.Text = "Propia";
                chk_Alquilada.Checked = false;
                chk_Familiar.Checked = false;
                chk_Otro.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        protected void chk_Alquilada_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Alquilada.Checked == true)
            {
                lblTipoVivienda.Text = "Alquilada";
                chk_Propia.Checked = false;
                chk_Familiar.Checked = false;
                chk_Otro.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        protected void chk_Otro_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_Otro.Checked == true)
            {
                lblTipoVivienda.Text = "Otro";
                chk_Alquilada.Checked = false;
                chk_Familiar.Checked = false;
                chk_Propia.Checked = false;
                lbl_MsgExisteEmple.Text = "";
            }
        }
        //====================================================================================
        //Actualizado 27/08/2019 
        //Cristian Sanchez Alias = "Menor"
        public void Obtener_Placas()
        {
            DataTable dt;
            if (ctrlactuemple.Consultar_Placas(int.Parse(txt_Identificacion.Text)).Rows.Count != 0)
            {
                dt = ctrlactuemple.Consultar_Placas(int.Parse(txt_Identificacion.Text));
                txtPlacaCarro.Text = dt.Rows[0][0].ToString();
                txtPlacaCarro2.Text = dt.Rows[0][2].ToString();
                txtMarca1.Text = dt.Rows[0]["veh_marca_carro1"].ToString();
                txtMarca2.Text = dt.Rows[0]["veh_marca_carro2"].ToString();               
                txtPlacaMoto.Text = dt.Rows[0][1].ToString();
                txtPlacaMoto2.Text = dt.Rows[0][3].ToString();
                txtModelo1.Text = dt.Rows[0]["veh_modelo_carro1"].ToString();
                if (txtModelo1.Text == "0") { txtModelo1.Text = ""; }
                txtModelo2.Text = dt.Rows[0]["veh_modelo_carro2"].ToString();
                if (txtModelo2.Text == "0") { txtModelo2.Text = ""; }
                txtModelMoto1.Text = dt.Rows[0]["veh_modelo_moto1"].ToString();
                if (txtModelMoto1.Text == "0") { txtModelMoto1.Text = ""; }
                txtModelMoto2.Text = dt.Rows[0]["veh_modelo_moto2"].ToString();
                if (txtModelMoto2.Text == "0") { txtModelMoto2.Text = ""; }
                txtMarMoto1.Text = dt.Rows[0]["veh_marca_moto1"].ToString();
                txtMarMoto2.Text = dt.Rows[0]["veh_marca_moto2"].ToString();
            }
            else
            {
            }
        }
        //Limpiar campos pnlPersConviven
        public void Limpiar_Campos_pnlPersConviven()
        {
            txt_NombApellConv.Text = "";
            txt_ParentescoConv.Text = "";
            txt_OcupacionConv.Text = "";
            txt_EdadConv.Text = "";
            lblMsgPersCOnvive.Text = "";
        }
        //Limpiar campos pnlHijoNoConvi
        public void Limpiar_Campos_pnlHijoNoConvi()
        {
            txt_NombApellHiNoConvi.Text = "";
            txt_EdadHiNoConvi.Text = "";
            lblMsgHijosNoConvi.Text = "";
        }
        //Limpiar campos pnlEstudios
        public void Limpiar_Campos_pnlEstudios()
        {
            txt_Programa.Text = "";
            txt_Entidad.Text = "";
            txt_AñoGradua.Text = "";
            txt_Semestre.Text = "";
            lblMsgEstudio.Text = "";
            txt_Lugar.Text = "";
            txt_FechaInicio.Text = "";
        }
        //Limpiar campos pnlEmergencia
        public void Limpiar_Campos_pnlEmergencia()
        {
            Txt_NombApelEmr.Text = "";
            Txt_UbicacionEmr.Text = "";
            Txt_ParentescoEmr.Text = "";
            Txt_TelefonoEmr.Text = "";
        }
        //Consultar personas que coviven con un determinado empleado
        public void Consultar_PersConvivenEmpleado()
        {
            GridConvivePers.DataSource = ctrlactuemple.Consultar_PersConvivenEmpleado(int.Parse(txt_Identificacion.Text));
            GridConvivePers.DataMember = ctrlactuemple.Consultar_PersConvivenEmpleado(int.Parse(txt_Identificacion.Text)).Tables[0].ToString();
            GridConvivePers.DataBind();
        }
        //Consultar hijos que no coviven con un determinado empleado
        public void Consultar_Hijos_NoConvivenEmpleado()
        {
            GridNoConvivePers.DataSource = ctrlactuemple.Consultar_Hijos_NoConvivenEmpleado(int.Parse(txt_Identificacion.Text));
            GridNoConvivePers.DataMember = ctrlactuemple.Consultar_Hijos_NoConvivenEmpleado(int.Parse(txt_Identificacion.Text)).Tables[0].ToString();
            GridNoConvivePers.DataBind();
        }
        // Consultar estudios de un empleado
        public void Consultar_Estudios_Empleado()
        {
            GridEstudios.DataSource = ctrlactuemple.Consultar_Estudios_Empleado(int.Parse(txt_Identificacion.Text));
            GridEstudios.DataMember = ctrlactuemple.Consultar_Estudios_Empleado(int.Parse(txt_Identificacion.Text)).Tables[0].ToString();
            GridEstudios.DataBind();
        }
        public void Consultar_Estudios_Empleado2()
        {
            GridEstudios.DataSource = ctrlactuemple.Consultar_Estudios_Empleado2(int.Parse(txt_Identificacion.Text));
            GridEstudios.DataMember = ctrlactuemple.Consultar_Estudios_Empleado2(int.Parse(txt_Identificacion.Text)).ToString();
            GridEstudios.DataBind();
        }
        //==================METODOS CRUD PERSONAS CONVIVEN CON EMPLEADO=========================================
        // Habilita la edicion de campos del GridConvivePers
        protected void GridConvivePers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridConvivePers.EditIndex = e.NewEditIndex;
            Consultar_PersConvivenEmpleado();
            GridConvivePers.Columns[7].Visible = false;
        }
        //Cancela la edicion de campos del GridConvivePers
        protected void GridConvivePers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridConvivePers.EditIndex = -1;
            Consultar_PersConvivenEmpleado();
            GridConvivePers.Columns[7].Visible = true;
            lblMsgPersCOnvive.Text = "";
        }
        //Actualiza campos del GridConvivePers
        protected void GridConvivePers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string nombrApell, parent, edad, nivelEduc, ocupa;
            float resultado;
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();
            txt = (TextBox)GridConvivePers.Rows[e.RowIndex].FindControl("txtNombApel");
            nombrApell = txt.Text;
            txt = (TextBox)GridConvivePers.Rows[e.RowIndex].FindControl("txtParentesco");
            parent = txt.Text;
            txt = (TextBox)GridConvivePers.Rows[e.RowIndex].FindControl("txtEdad");
            edad = txt.Text;
            cbo = (DropDownList)GridConvivePers.Rows[e.RowIndex].FindControl("cboNivelEduca");
            nivelEduc = cbo.Text;
            txt = (TextBox)GridConvivePers.Rows[e.RowIndex].FindControl("txtOcupacio");
            ocupa = txt.Text;
            int covempid = Convert.ToInt32(GridConvivePers.DataKeys[e.RowIndex].Value);
            if (float.TryParse(edad, out resultado))//evalua si el valor en el campo es del tipo correcto
            {
                if (nivelEduc != "ELEGIR UNA OPCION")
                {
                    ctrlactuemple.Actualizar_PersonasConviven_Empleado(nombrApell, parent, float.Parse(edad), nivelEduc, ocupa, covempid);
                    GridConvivePers.EditIndex = -1;
                    GridConvivePers.Columns[7].Visible = true;
                    Consultar_PersConvivenEmpleado();
                }
                else
                {
                    mensajeVentana("Debe elegir un nivel educativo");
                    GridConvivePers.Rows[e.RowIndex].FindControl("cboNivelEduca").Focus();
                }
            }
            else
            {
                mensajeVentana("El campo edad contiene un valor incorrecto");
            }
        }
        //Elimina  personas que conviven con el empleado
        protected void GridConvivePers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int covempid = Convert.ToInt32(GridConvivePers.DataKeys[e.RowIndex].Value);   // recupera la llave del GridView  
            ctrlactuemple.Eliminar_PersonasConviven_Empleado(covempid);
            Consultar_PersConvivenEmpleado();
            GridConvivePers.EditIndex = -1;
            mensajeVentana("Registro Eliminado Correctamente");
        }
        //Agregar personas que conviven con el empleado
        protected void btn_AgregarPersConvi_Click(object sender, EventArgs e)
        {//======Variables para verificar el tipo de dato========
            string cadenaedad = txt_EdadConv.Text;
            float resultado;
            //===================================================
            int respuesta;
            if (txt_NombApellConv.Text != "" && txt_ParentescoConv.Text != "" && txt_EdadConv.Text != "" && cbo_NivenEducaConv.Text != "" && txt_OcupacionConv.Text != "")
            {
                if (float.TryParse(cadenaedad, out resultado))// Evalua si el tipo de dato es correcto
                {
                    if (cbo_NivenEducaConv.Text != "ELEGIR UNA OPCION")
                    {
                        respuesta = ctrlactuemple.Agregar_Pers_ConvivenEmpleado(int.Parse(txt_Identificacion.Text), txt_NombApellConv.Text, txt_ParentescoConv.Text, float.Parse(txt_EdadConv.Text), cbo_NivenEducaConv.Text, txt_OcupacionConv.Text);
                        Consultar_PersConvivenEmpleado();
                        Limpiar_Campos_pnlPersConviven();
                        Habilitar_PersConvive(false);
                        GridConvivePers.Visible = true;
                        txt_NombApellConv.Focus();
                    }
                    else
                    {
                        mensajeVentana("Debe elegir un nivel educativo");
                        cbo_NivenEducaConv.Focus();
                    }
                }
                else
                {
                    mensajeVentana("El contenido en el campo edad es incorrecto");
                    txt_EdadConv.Focus();
                }
            }
            else
            {
                mensajeVentana("Debe llenar todos los campos");
            }
        }
        //=================================================================================================
        //==================METODOS CRUD HIJOS NO CONVIVEN CON EMPLEADO====================================
        //Habilita la edicion de campos del GridNoConvivePers
        protected void GridNoConvivePers_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridNoConvivePers.EditIndex = e.NewEditIndex;
            Consultar_Hijos_NoConvivenEmpleado();
            GridNoConvivePers.Columns[5].Visible = false;
        }
        //Cancela la edicion de campos del GridNoConvivePers
        protected void GridNoConvivePers_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridNoConvivePers.EditIndex = -1;
            Consultar_Hijos_NoConvivenEmpleado();
            GridNoConvivePers.Columns[5].Visible = true;
            lblMsgHijosNoConvi.Text = "";
        }
        //Actualizar hijos que no conviven con el empleado
        protected void GridNoConvivePers_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string nombrApell, edad, nivelEduc;
            float resultado;
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();
            txt = (TextBox)GridNoConvivePers.Rows[e.RowIndex].FindControl("txtNombApelNoConvi");
            nombrApell = txt.Text;
            txt = (TextBox)GridNoConvivePers.Rows[e.RowIndex].FindControl("txtEdadNoConvi");
            edad = txt.Text;
            cbo = (DropDownList)GridNoConvivePers.Rows[e.RowIndex].FindControl("cboNIvelEducaNoConvi");
            nivelEduc = cbo.Text;
            int id = Convert.ToInt32(GridNoConvivePers.DataKeys[e.RowIndex].Value);
            if (float.TryParse(edad, out resultado))//evalua si el valor en el campo es del tipo correcto
            {
                if (nivelEduc != "ELEGIR UNA OPCION")
                {
                    ctrlactuemple.Actualizar_HijosNoConviven_Empleado(nombrApell, float.Parse(edad), nivelEduc, id);
                    GridNoConvivePers.EditIndex = -1;
                    GridNoConvivePers.Columns[5].Visible = true;
                    Consultar_Hijos_NoConvivenEmpleado();
                }
                else
                {
                    mensajeVentana("Debe elegir un nivel educativo");
                    GridNoConvivePers.Rows[e.RowIndex].FindControl("cboNIvelEducaNoConvi").Focus();
                }
            }
            else
            {
                mensajeVentana("El campo edad contiene un valor incorrecto");
            }
        }
        //Agregar hijos que no conviven con el empleado
        protected void btn_AgregarHijosNoConvi_Click(object sender, EventArgs e)
        {
            //======Variables para verificar el tipo de dato========
            string cadenaedad = txt_EdadHiNoConvi.Text;
            float resultado;
            //===================================================
            int respuesta;
            if (txt_NombApellHiNoConvi.Text != "" && cbo_NivelEducHiNoConvi.Text != "" && txt_EdadHiNoConvi.Text != "")
            {
                if (float.TryParse(cadenaedad, out resultado))// Evalua si el tipo de dato es correcto
                {
                    if (cbo_NivelEducHiNoConvi.Text != "ELEGIR UNA OPCION")
                    {
                        respuesta = ctrlactuemple.Agregar_Hijos_NoConvivenEmpleado(int.Parse(txt_Identificacion.Text), txt_NombApellHiNoConvi.Text, float.Parse(txt_EdadHiNoConvi.Text), cbo_NivelEducHiNoConvi.Text);
                        Consultar_Hijos_NoConvivenEmpleado();
                        Limpiar_Campos_pnlHijoNoConvi();
                        GridNoConvivePers.Visible = true;
                        Habilitar_HijosNoConvi(false);
                        txt_NombApellHiNoConvi.Focus();
                    }
                    else
                    {
                        mensajeVentana("Debe elegir un nivel educativo");
                        cbo_NivelEducHiNoConvi.Focus();
                    }
                }
                else
                {
                    mensajeVentana("El campo edad contiene un valor incorrecto");
                    txt_EdadHiNoConvi.Focus();
                }
            }
            else
            {
                mensajeVentana("Debe llenar todos los campos ");
            }
        }
        //Elimina  hijos que no conviven con el empleado
        protected void GridNoConvivePers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int id = Convert.ToInt32(GridNoConvivePers.DataKeys[e.RowIndex].Value);
            ctrlactuemple.Eliminar_HijosNoConviven_Empleado(id);
            Consultar_Hijos_NoConvivenEmpleado();
            GridNoConvivePers.EditIndex = -1;
            mensajeVentana("Registro Eliminado Correctamente");
        }
        //========================================================================================================
        //==================METODOS CRUD ESTUDIOS EMPLEADO========================================================
        //Habilita la edicion de campos del GridEstudios
        protected void GridEstudios_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridEstudios.Columns[9].Visible = false;
            GridEstudios.EditIndex = e.NewEditIndex;
            Consultar_Estudios_Empleado2();
            //ctrlactuemple.tipoestudio();
        }
        //Cancela la edicion de campos del GridEstudios
        protected void GridEstudios_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridEstudios.EditIndex = -1;
            Consultar_Estudios_Empleado();
            GridEstudios.Columns[9].Visible = true;
            lblMsgEstudio.Text = "";
        }
        //Actualizar estudios del empleado
        protected void GridEstudios_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string titulo, programa, entieduca, fechgrad, comple, semestre, cursano;
            int resultado;
            DateTime resultado2;
            TextBox txt = new TextBox();
            DropDownList cbo = new DropDownList();
            cbo = (DropDownList)GridEstudios.Rows[e.RowIndex].FindControl("txt_titulo");
            titulo = cbo.Text;
            txt = (TextBox)GridEstudios.Rows[e.RowIndex].FindControl("txt_Programa");
            programa = txt.Text;
            txt = (TextBox)GridEstudios.Rows[e.RowIndex].FindControl("txt_EntidadEduca");
            entieduca = txt.Text;
            txt = (TextBox)GridEstudios.Rows[e.RowIndex].FindControl("txt_Año_Gradua");
            fechgrad = txt.Text;
            cbo = (DropDownList)GridEstudios.Rows[e.RowIndex].FindControl("cbo_completa");
            comple = cbo.Text;
            txt = (TextBox)GridEstudios.Rows[e.RowIndex].FindControl("txt_semestre");
            semestre = txt.Text;
            cbo = (DropDownList)GridEstudios.Rows[e.RowIndex].FindControl("cbo_cursando");
            cursano = cbo.Text;
            //Asigna un valor numerico a el combo, para poder tener el tipo de dato valido para el campo
            if (comple == "SI")
            {
                comple = "1";
            }
            else
            {
                comple = "0";
            }
            if (cursano == "SI")
            {
                cursano = "1";
            }
            else
            {
                cursano = "0";
            }
            //-----------------------------------------------------------------------------------------
            int estid = Convert.ToInt32(GridEstudios.DataKeys[e.RowIndex].Value);
            if (int.TryParse(semestre, out resultado))//evalua si el valor en el campo es del tipo correcto
            {
                //if (int.Parse(semestre) >= 0 && int.Parse(semestre) <= 12)
                //{
                if (comple == "1" && cursano == "0")
                {
                    if (fechgrad != "")
                    {
                        if (semestre == "0")
                        {
                            if (DateTime.TryParse(fechgrad, out resultado2))//evalua si el valor en el campo es del tipo correcto
                            {
                                ctrlactuemple.Actualizar_Estudios_Empleado(titulo, programa, entieduca, fechgrad.ToString(), int.Parse(comple), int.Parse(semestre), int.Parse(cursano), estid);
                                GridEstudios.EditIndex = -1;
                                GridEstudios.Columns[9].Visible = true;
                                Consultar_Estudios_Empleado();
                                //mensajeVentana("Registro Actualizado Correctamente"); 
                            }
                            else
                            {
                                mensajeVentana("El campo fecha contiene un valor incorrecto");
                            }
                        }
                        else
                        {
                            mensajeVentana("El valor del semestre debe ser '0' si ya completo el estudio");
                            // mensajeVentana("El valor del semestre debe ser '0' si ya completo el estudio");
                        }
                    }
                    else
                    {
                        mensajeVentana("Debe especificar el año en que se graduo");
                    }
                }
                else if (comple == "0" && cursano == "1")
                {
                    if (int.Parse(semestre) >= 1 && int.Parse(semestre) <= 12)
                    {
                        ctrlactuemple.Actualizar_Estudios_Empleado_SinFecGradua(titulo, programa, entieduca, int.Parse(comple), int.Parse(semestre), int.Parse(cursano), estid);
                        GridEstudios.EditIndex = -1;
                        GridEstudios.Columns[9].Visible = true;
                        Consultar_Estudios_Empleado();
                    }
                    else
                    {
                        mensajeVentana("Debe especificar un semestre entre 1 y 12");
                    }
                }
                //}
                //else
                //{
                //    mensajeVentana("El campo semestre contiene un valor incorrecto, establecer el valor (0) si el empleado no esta cursando el estudio");
                //}
            }
        }
        //Elimina estudios del empleado
        protected void GridEstudios_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int estid = Convert.ToInt32(GridEstudios.DataKeys[e.RowIndex].Value);
            ctrlactuemple.Eliminar_Estudio_Empleado(estid);
            Consultar_Estudios_Empleado();
            GridEstudios.EditIndex = -1;
            mensajeVentana("Registro Eliminado Correctamente");
        }
        // Agregar un nuevo estudio cursado o cursando por el empleado
        protected void btn_AgregarEstudio_Click1(object sender, EventArgs e)
        {
            int respuesta;
            //string fecha = DateTime.Now.ToString();
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            int activo = 1;
            //======Variables para verificar el tipo de dato========
            string cadenasemestre, cadenafecha, cadenafechaini;
            cadenafecha = txt_AñoGradua.Text;
            cadenafechaini = txt_FechaInicio.Text;
            cadenasemestre = txt_Semestre.Text;
            int resultado;
            DateTime resultado2;
            //======================================================
            if (txt_Titulo.Text != "" && txt_Programa.Text != "" && txt_Entidad.Text != "")
            {
                if (DateTime.TryParse(cadenafechaini, out resultado2))
                {
                    if (int.TryParse(cadenasemestre, out resultado))
                    {
                        if (cbo_Cursando.Text == "0" && cbo_Completado.Text == "1")
                        {
                            if (txt_AñoGradua.Text != "")
                            {
                                if (DateTime.TryParse(cadenafecha, out resultado2))
                                {
                                    respuesta = ctrlactuemple.Agregar_Estudio(txt_Titulo.Text, txt_Programa.Text, txt_Entidad.Text, txt_AñoGradua.Text, int.Parse(cbo_Completado.Text), int.Parse(txt_Semestre.Text), int.Parse(cbo_Cursando.Text), int.Parse(txt_Identificacion.Text), txt_Lugar.Text, txt_FechaInicio.Text.ToString(), int.Parse(cbo_Estu_Externo.Text), int.Parse(cbo_Costeado.Text), fecha, int.Parse(lblUsuario.Text), activo);
                                    Limpiar_Campos_pnlEstudios();
                                    Consultar_Estudios_Empleado();
                                    GridEstudios.Visible = true;
                                    Habilitar_Estudios(false);
                                    // mensajeVentana("Registro Agregado Correctamente");
                                    txt_Titulo.Focus();
                                }
                                else
                                {
                                    mensajeVentana("El campo fecha terminacion contiene un valor incorrecto");
                                    txt_AñoGradua.Focus();
                                }
                            }
                            else
                            {
                                mensajeVentana("Debe especificar la fecha en que termino  el estudio");
                                txt_AñoGradua.Focus();
                            }
                        }
                        else if (cbo_Cursando.Text == "1" && cbo_Completado.Text == "0")
                        {
                            //if (int.Parse(txt_Semestre.Text) >= 1 && int.Parse(txt_Semestre.Text) <= 12)
                            //{
                            //if (txt_AñoGradua.Text == "")
                            //    {
                            respuesta = ctrlactuemple.Agregar_EstudioSinFechaFinal(txt_Titulo.SelectedItem.ToString(), txt_Programa.Text, txt_Entidad.Text, int.Parse(cbo_Completado.Text), int.Parse(txt_Semestre.Text), int.Parse(cbo_Cursando.Text), int.Parse(txt_Identificacion.Text), txt_Lugar.Text, txt_FechaInicio.Text.ToString(), int.Parse(cbo_Estu_Externo.Text), int.Parse(cbo_Costeado.Text), fecha, int.Parse(lblUsuario.Text), activo);
                            Limpiar_Campos_pnlEstudios();
                            Consultar_Estudios_Empleado();
                            GridEstudios.Visible = true;
                            Habilitar_Estudios(false);
                            // mensajeVentana("Registro Agregado Correctamente");
                            txt_Titulo.Focus();
                            //}
                            //else
                            //{
                            //    mensajeVentana("Debe especificar el año en que se graduo");
                            //    txt_AñoGradua.Focus();
                            //}
                            //}
                            //else
                            //{
                            //    mensajeVentana("Debe especificar un semestre entre 1 y 12");
                            //}
                        }
                    }
                    else
                    {
                        mensajeVentana("El campo semestre contiene un valor incorrecto");
                        txt_Semestre.Focus();
                    }
                }
                else
                {
                    mensajeVentana("El campo fecha de inicio contiene un valor incorrecto");
                    txt_FechaInicio.Focus();
                }
            }
            else
            {
                mensajeVentana("Debe diligenciar todos los campos");
            }
        }
        //Permite la paginacion del gridview
        protected void GridEstudios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridEstudios.EditIndex = -1;
            Consultar_Estudios_Empleado();
            GridEstudios.PageIndex = e.NewPageIndex;
            this.GridEstudios.DataBind();
        }
        //===============================================================================================
        //====================PERSONA EN CASO DE EMERGENCIA EMPLEADO=====================================
        protected void btn_AgregarPersEmergen_Click(object sender, EventArgs e)
        {
            int respuesta;
            if (Txt_NombApelEmr.Text != "" && Txt_UbicacionEmr.Text != "" && Txt_TelefonoEmr.Text != "" && Txt_ParentescoEmr.Text != "")
            {
                if (ctrlactuemple.Consultar_Numero_Personas_Emergencia(int.Parse(txt_Identificacion.Text)).Rows.Count >= 0 && ctrlactuemple.Consultar_Numero_Personas_Emergencia(int.Parse(txt_Identificacion.Text)).Rows.Count <= 1)
                {
                    respuesta = ctrlactuemple.Agregar_Persona_Emergencia_Empleado(Txt_NombApelEmr.Text, Txt_ParentescoEmr.Text, Txt_UbicacionEmr.Text, Txt_TelefonoEmr.Text, int.Parse(txt_Identificacion.Text));

                    Consultar_PersonaEmergencia_Empleado();
                    Limpiar_Campos_pnlEmergencia();
                    GridEmergencia.Visible = true;
                    Habilitar_PersEmergen(false);
                    Txt_NombApelEmr.Focus();
                }
                else
                {
                    mensajeVentana("Solo se permiten agregar dos contactos de emergencia");
                    Habilitar_PersEmergen(false);
                    Limpiar_Campos_pnlEmergencia();
                    Consultar_PersonaEmergencia_Empleado();
                    GridEmergencia.Visible = true;
                }
            }
            else
            {
                mensajeVentana("Dede llenar todos los campos  del grupo (En caso de emergencia)");
            }
        }
        //Consultar persona de emergencia empleado
        public void Consultar_PersonaEmergencia_Empleado()
        {
            GridEmergencia.DataSource = ctrlactuemple.Consultar_PersonaEmergencia_Empleado(int.Parse(txt_Identificacion.Text));
            GridEmergencia.DataMember = ctrlactuemple.Consultar_PersonaEmergencia_Empleado(int.Parse(txt_Identificacion.Text)).Tables[0].ToString();
            GridEmergencia.DataBind();
        }
        //Habilitar la edicion persona de emergencia empleado
        protected void GridEmergencia_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridEmergencia.Columns[6].Visible = false;
            GridEmergencia.EditIndex = e.NewEditIndex;
            Consultar_PersonaEmergencia_Empleado();
        }
        //Cancelar la edicion persona de emergencia empleado
        protected void GridEmergencia_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
        {
            GridEmergencia.EditIndex = -1;
            Consultar_PersonaEmergencia_Empleado();
            GridEmergencia.Columns[6].Visible = true;
            lblEmergencia.Text = "";
        }
        //Actualizar persona de emergencia empleado
        protected void GridEmergencia_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            string nombrapel, parent, direcci, telefo;
            TextBox txt = new TextBox();
            txt = (TextBox)GridEmergencia.Rows[e.RowIndex].FindControl("txtNombApelEmer");
            nombrapel = txt.Text;
            txt = (TextBox)GridEmergencia.Rows[e.RowIndex].FindControl("txtParentescoEmer");
            parent = txt.Text;
            txt = (TextBox)GridEmergencia.Rows[e.RowIndex].FindControl("txt_DireccionEmer");
            direcci = txt.Text;
            txt = (TextBox)GridEmergencia.Rows[e.RowIndex].FindControl("txtTelefonoEmer");
            telefo = txt.Text;
            int peremrid = Convert.ToInt32(GridEmergencia.DataKeys[e.RowIndex].Value);
            if (nombrapel != "" && parent != "" && direcci != "" && telefo != "")
            {
                ctrlactuemple.Actualizar_Persona_Emergencia_Empleado(nombrapel, parent, direcci, telefo, peremrid);
                GridEmergencia.EditIndex = -1;
                GridEmergencia.Columns[6].Visible = true;
                Consultar_PersonaEmergencia_Empleado();
                //mensajeVentana("Registro Actualizado Correctamente");
            }
            else
            {
                mensajeVentana("Dede llenar todos los campos  del grupo (En caso de emergencia)");
            }
        }
        //Elimina estudios del empleado
        protected void GridEmergencia_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            int peremrid = Convert.ToInt32(GridEmergencia.DataKeys[e.RowIndex].Value);
            ctrlactuemple.Eliminar_Persona_Emergencia_Empleado(peremrid);
            Consultar_PersonaEmergencia_Empleado();
            GridEmergencia.EditIndex = -1;
            mensajeVentana("Registro Eliminado Correctamente");
        }
        //======================================================
        public void Habilitar_PersConvive(bool Habilitar)
        {
            if (Habilitar == true)
            {
                txt_NombApellConv.Visible = true;
                txt_ParentescoConv.Visible = true;
                txt_EdadConv.Visible = true;
                cbo_NivenEducaConv.Visible = true;
                txt_OcupacionConv.Visible = true;
                btn_AgregarPersConvi.Visible = true;
                btn_HabiliNuevaPersConvi.Visible = false;
                btn_CancelaNuevaPersConvi.Visible = true;
                lblMsgPersCOnvive.Text = "";
                lblnomape_persconvi.Visible = true;
                lblparents_persconvi.Visible = true;
                lbledad_persconvi.Visible = true;
                lblocupa_persconvi.Visible = true;
                lblniveleduca_persconvi.Visible = true;
            }
            else
            {
                txt_NombApellConv.Visible = false;
                txt_ParentescoConv.Visible = false;
                txt_EdadConv.Visible = false;
                cbo_NivenEducaConv.Visible = false;
                txt_OcupacionConv.Visible = false;
                btn_AgregarPersConvi.Visible = false;
                btn_HabiliNuevaPersConvi.Visible = true;
                btn_CancelaNuevaPersConvi.Visible = false;
                lblMsgPersCOnvive.Text = "";
                lblnomape_persconvi.Visible = false;
                lblparents_persconvi.Visible = false;
                lbledad_persconvi.Visible = false;
                lblocupa_persconvi.Visible = false;
                lblniveleduca_persconvi.Visible = false;
            }
        }
        public void Habilitar_HijosNoConvi(bool Habilitar)
        {
            if (Habilitar == true)
            {
                lblnomape_HiNoConvi.Visible = true;
                lblniveleduca_HiNoConvi.Visible = true;
                lblEdad_HiNoConvi.Visible = true;
                txt_NombApellHiNoConvi.Visible = true;
                txt_NombApellHiNoConvi.Visible = true;
                txt_EdadHiNoConvi.Visible = true;
                cbo_NivelEducHiNoConvi.Visible = true;
                lblMsgHijosNoConvi.Text = "";
                btn_AgregarHijosNoConvi.Visible = true;
                btn_HabiliAgregarHijosNoConvi.Visible = false;
                btn_CancelarAgregarHijosNoConvi.Visible = true;
            }
            else
            {
                lblnomape_HiNoConvi.Visible = false;
                lblniveleduca_HiNoConvi.Visible = false;
                lblEdad_HiNoConvi.Visible = false;
                txt_NombApellHiNoConvi.Visible = false;
                txt_NombApellHiNoConvi.Visible = false;
                txt_EdadHiNoConvi.Visible = false;
                cbo_NivelEducHiNoConvi.Visible = false;
                lblMsgHijosNoConvi.Text = "";
                btn_AgregarHijosNoConvi.Visible = false;
                btn_HabiliAgregarHijosNoConvi.Visible = true;
                btn_CancelarAgregarHijosNoConvi.Visible = false;
            }
        }
        public void Habilitar_PersEmergen(bool Habilitar)
        {
            if (Habilitar == true)
            {
                lblnomape_Emergencia.Visible = true;
                lblparent_Emergencia.Visible = true;
                lbldirecci_Emergencia.Visible = true;
                lbltelefo_Emergencia.Visible = true;
                Txt_NombApelEmr.Visible = true;
                Txt_ParentescoEmr.Visible = true;
                Txt_UbicacionEmr.Visible = true;
                Txt_TelefonoEmr.Visible = true;
                lblEmergencia.Text = "";
                btn_AgregarPersEmergen.Visible = true;
                btn_HabiliAgregarPersEmergen.Visible = false;
                btn_CancelarAgregarPersEmergen.Visible = true;
            }
            else
            {
                lblnomape_Emergencia.Visible = false;
                lblparent_Emergencia.Visible = false;
                lbldirecci_Emergencia.Visible = false;
                lbltelefo_Emergencia.Visible = false;
                Txt_NombApelEmr.Visible = false;
                Txt_ParentescoEmr.Visible = false;
                Txt_UbicacionEmr.Visible = false;
                Txt_TelefonoEmr.Visible = false;
                lblEmergencia.Text = "";
                btn_AgregarPersEmergen.Visible = false;
                btn_HabiliAgregarPersEmergen.Visible = true;
                btn_CancelarAgregarPersEmergen.Visible = false;
            }
        }
        public void Habilitar_Estudios(bool Habilitar)
        {
            if (Habilitar == true)
            {
                lbltitulo_Estudios.Visible = true;
                lblprograma_Estudios.Visible = true;
                lblinstitu_Estudios.Visible = true;
                lblcursa_Estudios.Visible = true;
                lblfechini_Estudios.Visible = true;
                lblsemetre_Estudios.Visible = true;
                lbllugar_Estudios.Visible = true;
                lblcompleta_Estudios.Visible = true;
                lblañogradu_Estudios.Visible = true;
                lblestuexter_Estudios.Visible = true;
                lblcostea_Estudios.Visible = true;
                txt_Titulo.Visible = true;
                txt_Programa.Visible = true;
                txt_Entidad.Visible = true;
                cbo_Cursando.Visible = true;
                txt_FechaInicio.Visible = true;
                txt_Semestre.Visible = true;
                txt_Lugar.Visible = true;
                cbo_Completado.Visible = true;
                txt_AñoGradua.Visible = true;
                cbo_Estu_Externo.Visible = true;
                cbo_Costeado.Visible = true;
                btn_AgregarEstudio.Visible = true;
                btn_Habili_AgregarEstudio.Visible = false;
                btn_Cancelar_AgregarEstudio.Visible = true;
            }
            else
            {
                lbltitulo_Estudios.Visible = false;
                lblprograma_Estudios.Visible = false;
                lblinstitu_Estudios.Visible = false;
                lblcursa_Estudios.Visible = false;
                lblfechini_Estudios.Visible = false;
                lblsemetre_Estudios.Visible = false;
                lbllugar_Estudios.Visible = false;
                lblcompleta_Estudios.Visible = false;
                lblañogradu_Estudios.Visible = false;
                lblestuexter_Estudios.Visible = false;
                lblcostea_Estudios.Visible = false;
                txt_Titulo.Visible = false;
                txt_Programa.Visible = false;
                txt_Entidad.Visible = false;
                cbo_Cursando.Visible = false;
                txt_FechaInicio.Visible = false;
                txt_Semestre.Visible = false;
                txt_Lugar.Visible = false;
                cbo_Completado.Visible = false;
                txt_AñoGradua.Visible = false;
                cbo_Estu_Externo.Visible = false;
                cbo_Costeado.Visible = false;
                btn_AgregarEstudio.Visible = false;
                btn_Habili_AgregarEstudio.Visible = true;
                btn_Cancelar_AgregarEstudio.Visible = false;
            }
        }
        protected void btn_HabiliNuevaPersConvi_Click(object sender, EventArgs e)
        {
            Habilitar_PersConvive(true);
            txt_NombApellConv.Focus();
            GridConvivePers.Visible = false;
        }
        protected void btn_HabiliAgregarHijosNoConvi_Click(object sender, EventArgs e)
        {
            Habilitar_HijosNoConvi(true);
            txt_NombApellHiNoConvi.Focus();
            GridNoConvivePers.Visible = false;
        }
        protected void btn_HabiliAgregarPersEmergen_Click(object sender, EventArgs e)
        {
            Habilitar_PersEmergen(true);
            Txt_NombApelEmr.Focus();
            GridEmergencia.Visible = false;
        }
        protected void btn_Habili_AgregarEstudio_Click(object sender, EventArgs e)
        {
            Habilitar_Estudios(true);
            txt_Titulo.Focus();
            GridEstudios.Visible = false;
            if (cbo_Cursando.SelectedValue == ("1"))
            {
                txt_Semestre.Enabled = true;
                txt_Semestre.Text = "";
                cbo_Completado.Text = "0";
                txt_AñoGradua.Enabled = false;
            }
            else if (cbo_Cursando.SelectedValue == ("0"))
            {
                txt_Semestre.Enabled = false;
                txt_Semestre.Text = "0";
                cbo_Completado.Text = "1";
                txt_AñoGradua.Enabled = true;
            }
        }
        protected void btn_CancelaNuevaPersConvi_Click(object sender, EventArgs e)
        {
            Habilitar_PersConvive(false);
            GridConvivePers.Visible = true;
        }
        protected void btn_CancelarAgregarHijosNoConvi_Click(object sender, EventArgs e)
        {
            Habilitar_HijosNoConvi(false);
            GridNoConvivePers.Visible = true;
        }
        protected void btn_CancelarAgregarPersEmergen_Click(object sender, EventArgs e)
        {
            Habilitar_PersEmergen(false);
            GridEmergencia.Visible = true;
        }
        protected void btn_Cancelar_AgregarEstudio_Click(object sender, EventArgs e)
        {
            Habilitar_Estudios(false);
            GridEstudios.Visible = true;
        }
        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }
        public void Abrir_FormatoImpreso()
        {
            //Variable para almacenar el numero de identificacion del cliente
            String Cedula = txt_Identificacion.Text;
            Response.Redirect("FormatoDatosActualizaEmpleado.aspx?cedula=" + Cedula.Trim() + "");
            // try
            // {
            //    //Descarga el reporte convertido en pdf  y lo muestra en pantalla---------------------------------------------
            //    Byte[] correo = new Byte[0];
            //    WebClient clienteWeb = new WebClient();
            //    clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            //    clienteWeb.UseDefaultCredentials = true;
            //    Uri enlace = new Uri("http://10.75.131.2:81/ReportServer/Pages/ReportViewer.aspx?%2fRep_ActualizacionDatosEmpleado%2fRpt_ActuaDatosEmple&rs:Command=Render&rs:format=PDF&rs:ClearSession=true&cedula=" + Cedula.Trim() + "");
            //    //Ruta de almacenamiento del archivo, nombre y extencion con la cual se guarda
            //    //string path= @"//172.21.0.101/I$/hd/Formatos/" + Cedula + ".pdf";
            //    //string path = @"//172.21.0.101/I:/Planos/" + Cedula + ".pdf";
            //    string directorio = @"I:\hd\"  + Cedula.Trim() + ".pdf" + @"\";
            //    string dirweb = @"~/hd/" + Cedula.Trim() + ".pdf" + @"\";
            //    if (!(Directory.Exists(directorio)))
            //    {
            //        Directory.CreateDirectory(directorio);
            //    }
            //    FDocument.Enabled = true;
            //    HttpPostedFile postedFile = FDocument.PostedFile;
            //    string fileName = System.IO.Path.GetFileName(FDocument.FileName);
            //    string rutaArchivo = directorio + @"\" + fileName;
            //    int counter = 1;
            //    string nombreArchivoTemp = "";
            //    while (File.Exists(rutaArchivo))
            //    {
            //        // if a file with this name already exists,
            //        // prefix the filename with a number.
            //        nombreArchivoTemp = "(" + counter.ToString() + ")" + fileName;
            //        rutaArchivo = directorio + nombreArchivoTemp;
            //        counter++;
            //    }
            //    //string path4= HttpContext.Current.Server.MapPath(Cedula +".pdf");    
            //    //string path = @"http:\\172.21.0.101\i$\HD\Formatos\" + Cedula + ".pdf";//no
            //    //string path = @"\\172.21.0.101\i$\HD\Formatos\" + Cedula + ".pdf";//bueno
            //    //  clienteWeb.DownloadFile(enlace.ToString(), path);         
            //    //clienteWeb.DownloadFile(enlace.ToString(), Cedula + ".pdf");         
            //    //   Process.Start(path);
            //    clienteWeb.Dispose();
            //    txt_Identificacion.Text = "";
            //    txt_Identificacion.Focus();
            //    Response.Redirect("ActualizacionDatosEmpleado.aspx");
            //    mensajeVentana("Por su seguridad cierre el archivo pdf generado. Gracias");               
            //    //------------------------------------------------------------------------------------------------------------
            //}
            //catch (Exception er)
            //{
            //    string error = er.Message.ToString();
            //    mensajeVentana("Cierre el visor de PDF e intente nuevamente.");
            //}
        }
        //07/09//2017 stiven palacios
        public void Agregar_Placa_Vehiculo()
        {
            if (String.IsNullOrEmpty(txtModelo1.Text))
            {
                txtModelo1.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelo2.Text))
            {
                txtModelo2.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelMoto1.Text))
            {
                txtModelMoto1.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelMoto2.Text))
            {
                txtModelMoto2.Text = "0";
            }

            ctrlactuemple.Agregar_Placa_Vehiculo(int.Parse(txt_Identificacion.Text), txtPlacaCarro.Text,
                                                txtPlacaMoto.Text, txtPlacaCarro2.Text, txtPlacaMoto2.Text,
                                                txtMarca1.Text, txtMarca2.Text, int.Parse(txtModelo1.Text), int.Parse(txtModelo2.Text),
                                                int.Parse(txtModelMoto1.Text), int.Parse(txtModelMoto2.Text), txtMarMoto1.Text, txtMarMoto2.Text);
        }
        public void Actualizar_Placa_Vehiculo()
        {
            if (String.IsNullOrEmpty(txtModelo1.Text))
            {
                txtModelo1.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelo2.Text))
            {
                txtModelo2.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelMoto1.Text))
            {
                txtModelMoto1.Text = "0";
            }
            if (String.IsNullOrEmpty(txtModelMoto2.Text))
            {
                txtModelMoto2.Text = "0";
            }

            ctrlactuemple.Actualizar_Placa_Vehiculo(txtPlacaCarro.Text, txtPlacaMoto.Text, txtPlacaCarro2.Text,
                                                    txtPlacaMoto2.Text, int.Parse(txt_Identificacion.Text),
                                                    txtMarca1.Text, txtMarca2.Text, int.Parse(txtModelo1.Text), int.Parse(txtModelo2.Text),
                                                    int.Parse(txtModelMoto1.Text), int.Parse(txtModelMoto2.Text), txtMarMoto1.Text, txtMarMoto2.Text);
        }
        public void consulta_carnet()
        {
            DataTable dt;
            dt = ctrlactuemple.consulta_carnet(int.Parse(txt_Identificacion.Text));
            string carnet = dt.Rows[0][0].ToString();
            if (carnet == "1")
            {
                chkCarnetSi.Checked = true;
            }
            else if (carnet == "0")
            {
                chkCarnetSi.Checked = true;
            }
        }
        protected void txt_Identificacion_TextChanged(object sender, EventArgs e)
        {
            Buscar_EmpleadoXCedula();
            btn_BuscarEmple.Focus();
        }
        protected void txtPlacaCarro_TextChanged(object sender, EventArgs e)
        {
            txtPlacaCarro.Text.ToString().Replace(' ', ' ');
        }
        protected void txtPlacaMoto_TextChanged(object sender, EventArgs e)
        {
            txtPlacaMoto.Text.ToString().Replace(' ', ' ');
        }
        protected void txtPlacaCarro2_TextChanged(object sender, EventArgs e)
        {
            txtPlacaCarro2.Text.ToString().Replace(' ', ' ');
        }
        protected void txtPlacaMoto2_TextChanged(object sender, EventArgs e)
        {
            txtPlacaMoto2.Text.ToString().Replace(' ', ' ');
        }
        protected void cbo_Cursando_TextChanged(object sender, EventArgs e)
        {
            if (cbo_Cursando.SelectedValue == ("1"))
            {
                txt_Semestre.Enabled = true;
                txt_Semestre.Text = "";
                cbo_Completado.Text = "0";
                txt_AñoGradua.Enabled = false;
                txt_AñoGradua.Text = "";
            }
            else if (cbo_Cursando.SelectedValue == ("0"))
            {
                txt_Semestre.Enabled = false;
                txt_Semestre.Text = "0";
                cbo_Completado.Text = "1";
                txt_AñoGradua.Enabled = true;
            }
            if (cbo_Cursando.SelectedValue == "1" && txt_Titulo.Text == "DIPLOMADO" | txt_Titulo.Text == "SEMINARIO")
            {
                txt_Semestre.Text = "0";
                txt_Semestre.Enabled = false;
            }
        }
        protected void cbo_Completado_TextChanged(object sender, EventArgs e)
        {
            if (cbo_Completado.SelectedValue == ("1"))
            {
                txt_Semestre.Enabled = false;
                txt_Semestre.Text = "0";
                cbo_Cursando.Text = "0";
                txt_AñoGradua.Enabled = true;
            }
            else if (cbo_Completado.SelectedValue == ("0"))
            {
                txt_Semestre.Enabled = true;
                txt_Semestre.Text = "";
                cbo_Cursando.Text = "1";
                txt_AñoGradua.Enabled = false;
                txt_AñoGradua.Text = "";
            }
        }
        protected void cbo_cursando_TextChanged1(object sender, EventArgs e)
        {
            ////GridEstudios.DataSource = ctrlactuemple.Consultar_Estudios_Empleado(int.Parse(txt_Identificacion.Text));
            ////GridEstudios.DataMember = ctrlactuemple.Consultar_Estudios_Empleado(int.Parse(txt_Identificacion.Text)).Tables[0].ToString();
            ////GridEstudios.DataBind();
            //if (GridEstudios.Columns[6].ToString()=="SI")  
            // {
            //           GridEstudios.
            //    //     GridEstudios.SelectedRow.Cells[4].Text = "0";
            //     GridEstudios.SelectedRow.Cells[3].Enabled = false;
            // //else if (GridEstudios.SelectedRow.Cells[6].Text == "NO")
            ////{
            //     GridEstudios.SelectedRow.Cells[5].Enabled = false;
            //     GridEstudios.SelectedRow.Cells[5].Text = "0";
            //     GridEstudios.SelectedRow.Cells[4].Text = "1";
            //     GridEstudios.SelectedRow.Cells[3].Enabled = true;
            //}
        }
        protected void GridConvivePers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridConvivePers.EditIndex = -1;
            Consultar_PersConvivenEmpleado();
            GridConvivePers.PageIndex = e.NewPageIndex;
            this.GridConvivePers.DataBind();
        }
        public void Recuperar_Empresa_CargoAnterior()
        {
            DataTable dt;
            int usumod = 1, activo = 1;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            string emc_id = "", emc_ubf_id = "1", emc_emp_usu_num_id = "", car_id = "",
                   emc_emp_usu_num_id_jefe = "1", emc_fecha_inicio = "", emc_fecha_modificacion = "",
                   emc_usuario_modificador = "", emc_activo = "";
            if (ctrlactuemple.Consultar_CargoAnterior(int.Parse(txt_Identificacion.Text)).Rows.Count != 0)
            {
                dt = ctrlactuemple.Consultar_CargoAnterior(int.Parse(txt_Identificacion.Text));
                car_id = dt.Rows[0]["car_id"].ToString();
                if (cbo_CargoActual.Text != car_id)
                {
                    emc_id = dt.Rows[0]["emc_id"].ToString();
                    emc_ubf_id = dt.Rows[0]["emc_ubf_id"].ToString();
                    emc_emp_usu_num_id = dt.Rows[0]["emc_emp_usu_num_id"].ToString();
                    emc_emp_usu_num_id_jefe = dt.Rows[0]["emc_emp_usu_num_id_jefe"].ToString();
                    emc_fecha_inicio = dt.Rows[0]["emc_fecha_inicio"].ToString();
                    emc_fecha_modificacion = dt.Rows[0]["emc_fecha_modificacion"].ToString();
                    emc_usuario_modificador = dt.Rows[0]["emc_usuario_modificador"].ToString();
                    emc_activo = dt.Rows[0]["emc_activo"].ToString();
                    //Crea un nuevo registro con el cargo actual
                    ctrlactuemple.Asignar_NuevoCargo(int.Parse(emc_ubf_id), int.Parse(emc_emp_usu_num_id), int.Parse(cbo_CargoActual.SelectedValue), int.Parse(emc_emp_usu_num_id_jefe),
                                                     fecha, fecha, usumod, activo);
                    //Deshabilita el cargo anterior
                    ctrlactuemple.Deshabilitar_CargoAnterior(int.Parse(txt_Identificacion.Text), int.Parse(emc_id), fecha);
                }
                else
                {
                }
            }
            else
            {
                car_id = "563";//(SIN CARGO)  
                               //Crea un nuevo registro con el cargo actual
                ctrlactuemple.Asignar_NuevoCargo(int.Parse(emc_ubf_id), int.Parse(txt_Identificacion.Text), int.Parse(cbo_CargoActual.SelectedValue), int.Parse(emc_emp_usu_num_id_jefe),
                                                 fecha, fecha, usumod, activo);
            }
        }
        public void Recuperar_EmpresaAnterior()
        {
            DataTable dt;
            string fecha = DateTime.Now.ToString("dd/MM/yyyy");
            int activo = 1, usuario = 1;
            string con_num = "", con_emp_usu_num_id = "", con_epr_id = "", con_tipo = "", con_fecha_inicio = "", con_fecha_fin = "",
                    con_clausulas = "", con_sal_bas = "0", con_sal_int = "0", con_sal_int_mixto = "0", con_porc_comisiones = "0",
                    con_sal_bas_mas_comisiones = "0", con_sal_bas_mas_comp = "0", con_sal_int_mas_comp = "0", con_promedio_sal = "0",
                    con_fecha_venc_pas_judicial = "", con_fecha_venc_pasaporte = "0", con_dias_vacaciones = "0", con_es_de_planta = "0",
                    con_observaciones = "", con_fecha_modificacion = "", con_usuario_modificador = "1", con_activo = "1";

            if (ctrlactuemple.Recuperar_EmpresaAnterior(int.Parse(txt_Identificacion.Text)).Rows.Count != 0)
            {
                dt = ctrlactuemple.Recuperar_EmpresaAnterior(int.Parse(txt_Identificacion.Text));
                con_epr_id = dt.Rows[0]["con_epr_id"].ToString();
                if (cbo_EmpresaContra.Text != con_epr_id)
                {
                    con_num = dt.Rows[0]["con_num"].ToString();
                    con_emp_usu_num_id = dt.Rows[0]["con_emp_usu_num_id"].ToString();
                    con_tipo = dt.Rows[0]["con_tipo"].ToString();
                    con_fecha_inicio = dt.Rows[0]["con_fecha_inicio"].ToString();
                    con_fecha_fin = dt.Rows[0]["con_fecha_fin"].ToString();
                    con_fecha_fin = "";
                    con_clausulas = dt.Rows[0]["con_clausulas"].ToString();
                    con_sal_bas = dt.Rows[0]["con_sal_bas"].ToString();
                    con_sal_int = dt.Rows[0]["con_sal_int"].ToString();
                    con_sal_int_mixto = dt.Rows[0]["con_sal_int_mixto"].ToString();
                    con_porc_comisiones = dt.Rows[0]["con_porc_comisiones"].ToString();
                    con_sal_bas_mas_comisiones = dt.Rows[0]["con_sal_bas_mas_comisiones"].ToString();
                    con_sal_bas_mas_comp = dt.Rows[0]["con_sal_bas_mas_comp"].ToString();
                    con_sal_int_mas_comp = dt.Rows[0]["con_sal_int_mas_comp"].ToString();
                    con_promedio_sal = dt.Rows[0]["con_promedio_sal"].ToString();
                    con_fecha_venc_pas_judicial = dt.Rows[0]["con_fecha_venc_pas_judicial"].ToString();
                    con_fecha_venc_pasaporte = dt.Rows[0]["con_fecha_venc_pasaporte"].ToString();
                    DateTime fechavencpasjudicial, fechavencpasaporte;
                    fechavencpasjudicial = DateTime.Parse(con_fecha_venc_pas_judicial);
                    fechavencpasaporte = DateTime.Parse(con_fecha_venc_pasaporte);
                    con_fecha_venc_pas_judicial = fechavencpasjudicial.ToString("dd/MM/yyyy");
                    con_fecha_venc_pasaporte = fechavencpasaporte.ToString("dd/MM/yyyy");
                    con_dias_vacaciones = dt.Rows[0]["con_dias_vacaciones"].ToString();
                    con_es_de_planta = dt.Rows[0]["con_es_de_planta"].ToString();
                    con_es_de_planta = "1";
                    con_observaciones = dt.Rows[0]["con_observaciones"].ToString();
                    con_fecha_modificacion = dt.Rows[0]["con_fecha_modificacion"].ToString();
                    con_usuario_modificador = dt.Rows[0]["con_usuario_modificador"].ToString();
                    con_activo = dt.Rows[0]["con_activo"].ToString();

                    ctrlactuemple.Asignar_NuevaEmpresaContratante(int.Parse(con_emp_usu_num_id), int.Parse(cbo_EmpresaContra.SelectedItem.Value), con_tipo, fecha, con_fecha_fin, con_clausulas, decimal.Parse(con_sal_bas),
                                                                  decimal.Parse(con_sal_bas_mas_comp), decimal.Parse(con_sal_int_mixto), decimal.Parse(con_porc_comisiones), decimal.Parse(con_sal_bas_mas_comisiones),
                                                                  decimal.Parse(con_sal_bas_mas_comp), decimal.Parse(con_sal_int_mas_comp), decimal.Parse(con_promedio_sal), con_fecha_venc_pas_judicial,
                                                                  con_fecha_venc_pasaporte, int.Parse(con_dias_vacaciones), int.Parse(con_es_de_planta), con_observaciones, fecha, usuario, activo);

                    ctrlactuemple.Deshabilitar_EmpresaAnterior(fecha, int.Parse(txt_Identificacion.Text), int.Parse(con_num));
                }
                else
                {

                }
            }
            else
            {
                con_epr_id = "13";// codigo 13
                ctrlactuemple.Asignar_NuevaEmpresaContratante(int.Parse(txt_Identificacion.Text), int.Parse(cbo_EmpresaContra.SelectedItem.Value), con_tipo, fecha, con_fecha_fin, con_clausulas, decimal.Parse(con_sal_bas),
                                                                decimal.Parse(con_sal_bas_mas_comp), decimal.Parse(con_sal_int_mixto), decimal.Parse(con_porc_comisiones), decimal.Parse(con_sal_bas_mas_comisiones),
                                                                decimal.Parse(con_sal_bas_mas_comp), decimal.Parse(con_sal_int_mas_comp), decimal.Parse(con_promedio_sal), con_fecha_venc_pas_judicial,
                                                                con_fecha_venc_pasaporte, int.Parse(con_dias_vacaciones), int.Parse(con_es_de_planta), con_observaciones, fecha, usuario, activo);
            }
        }
        protected void Txt_CargoActual1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTip_Cargo();
        }
        public object Obtener_TipEstudio()
        {
            DataSet ds1;
            ds1 = ctrlactuemple.Met_Obtener_TipEstudio();
            return ds1;
        }
        protected void cbo_Cursando_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void txt_Titulo_TextChanged(object sender, EventArgs e)
        {
            if (txt_Titulo.Text == "DIPLOMADO" | txt_Titulo.Text == "SEMINARIO")
            {
                txt_Semestre.Text = "0";
                txt_Semestre.Enabled = false;
            }
            else if (cbo_Cursando.SelectedValue == "0" && txt_Titulo.Text != "DIPLOMADO" && txt_Titulo.Text != "SEMINARIO")
            {
                txt_Semestre.Text = "0";
                txt_Semestre.Enabled = false;
            }
            else
            {
                txt_Semestre.Text = "";
                txt_Semestre.Enabled = true;
            }
        }
        protected void txt_Programa_TextChanged(object sender, EventArgs e)
        {
        }
        //Campos agregados
        //27/08/2019 Cristian Sanchez Alias = "Menor"
        protected void txtModelo1_Textchanged(object sender, EventArgs e)
        {
            txtModelo1.Text.ToString().Replace(' ', ' ');
        }
        protected void txtMarca1_Textchanged(object sender, EventArgs e)
        {
            txtMarca1.Text.ToString().Replace(' ', ' ');
        }
        protected void txtMarca2_Textchanged(object sender, EventArgs e)
        {
            txtMarca2.Text.ToString().Replace(' ', ' ');
        }
        protected void txtModelo2_Textchanged(object sender, EventArgs e)
        {
            txtModelo2.Text.ToString().Replace(' ', ' ');
        }
        protected void txtMarMoto2_Textchanged(object sender, EventArgs e)
        {
            txtMarMoto2.Text.ToString().Replace(' ', ' ');
        }
        protected void txtMarMoto1_Textchanged(object sender, EventArgs e)
        {
            txtMarMoto1.Text.ToString().Replace(' ', ' ');
        }
        protected void txtModelMoto2_Textchanged(object sender, EventArgs e)
        {
            txtModelMoto2.Text.ToString().Replace(' ', ' ');
        }
        protected void txtModelMoto1_Textchanged(object sender, EventArgs e)
        {
            txtModelMoto1.Text.ToString().Replace(' ', ' ');
        }
        //========================CheckBox Licencia vehicular=================================
        protected void ChklicenciaSi_CheckedChanged(object sender, EventArgs e)
        {    
            if (ChklicenciaSi.Checked == true)
            {
                ChklicenciaNo.Checked = false;
                ChklicenciaSi.Checked = true;
            }
            else
            {
                ChklicenciaNo.Checked = true;
                ChklicenciaSi.Checked = false;
            }
        }
        protected void ChklicenciaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (ChklicenciaNo.Checked == true)
            {
                ChklicenciaSi.Checked = false;
            }
        }
        //=========Checkedbox conoce Reglas Dotacion==================================
        protected void chkConReglaDotaSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConReglaDotaSi.Checked == true)
            {
                chkConReglaDotaNo.Checked = false;
                chkConReglaDotaSi.Checked = true;
            }
            else
            {
                chkConReglaDotaNo.Checked = true;
                chkConReglaDotaSi.Checked = false;
            }
        }

        protected void chkConReglaDotaNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkConReglaDotaNo.Checked == true)
            {
                chkConReglaDotaSi.Checked = false;
                chkConReglaDotaNo.Checked = true;
            }
            else
            {
                chkConReglaDotaSi.Checked = true;
                chkConReglaDotaNo.Checked = false;
            }
        }

        //=========Checkedbox  Recibe Dotacion==================================
        protected void chkrecibeDotacSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkrecibeDotacSi.Checked == true)
            {
                chkrecibeDotacNo.Checked = false;
                chkrecibeDotacSi.Checked = true;

                chkConReglaDotaNo.Enabled = true;
                chkConReglaDotaSi.Enabled = true;
            }
            else
            {
                chkrecibeDotacNo.Checked = true;
                chkrecibeDotacSi.Checked = false;
                chkConReglaDotaNo.Enabled = false;
                chkConReglaDotaSi.Enabled = false;
            }
        }

        protected void chkrecibeDotacNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkrecibeDotacNo.Checked == true)
            {
                chkrecibeDotacSi.Checked = false;
                chkrecibeDotacNo.Checked = true;

                chkConReglaDotaSi.Checked = false;
                chkConReglaDotaNo.Checked = false;
                chkConReglaDotaNo.Enabled = false;
                chkConReglaDotaSi.Enabled = false;
            }
            else
            {
                chkrecibeDotacSi.Checked = true;
                chkrecibeDotacNo.Checked = false;
                chkConReglaDotaSi.Checked = true;
                chkConReglaDotaNo.Checked = true;
                chkConReglaDotaNo.Enabled = true;
                chkConReglaDotaSi.Enabled = true;
            }
        }
        //=========Checkedbox  Sube Fotos==================================
        protected void chkpublicaFotoSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpublicaFotoSi.Checked == true)
            {
                chkpublicaFotoNo.Checked = false;
                chkpublicaFotoSi.Checked = true;
            }
            else if (chkpublicaFotoSi.Checked == false)
            {
                chkpublicaFotoNo.Checked = true;
                chkpublicaFotoSi.Checked = false;
            }
        }

        protected void chkpublicaFotoNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chkpublicaFotoNo.Checked == true)
            {
                chkpublicaFotoSi.Checked = false;
                chkpublicaFotoNo.Checked = true;
            }
            else if (chkpublicaFotoNo.Checked == false)
            {
                chkpublicaFotoSi.Checked = true;
                chkpublicaFotoNo.Checked = false;
            }
        }
        //=========Checkedbox  Tiene REdes Sociales==================================
        protected void chkRdesocialeSi_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRdesocialeSi.Checked == true)
            {
                chkRdesocialeSi.Checked = true;
                chRdesocialesNo.Checked = false;
            }
            else if (chkRdesocialeSi.Checked == false)
            {
                chkRdesocialeSi.Checked = false;
                chRdesocialesNo.Checked = true;
            }
        }

        protected void chRdesocialesNo_CheckedChanged(object sender, EventArgs e)
        {
            if (chRdesocialesNo.Checked == true)
            {
                chkRdesocialeSi.Checked = false;
                chRdesocialesNo.Checked = true;
            }
            else if (chRdesocialesNo.Checked == false)
            {
                chkRdesocialeSi.Checked = true;
                chRdesocialesNo.Checked = false;
            }
        }
        //====================================================================================

    }
}



