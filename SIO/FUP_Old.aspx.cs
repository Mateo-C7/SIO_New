using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;
using System.Linq;

namespace SIO
{
    public partial class FUP : System.Web.UI.Page
    {
        private DataSet dsFUP = new DataSet();
        public ControlFUP controlfup = new ControlFUP();
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public ControlContacto controlCont = new ControlContacto();
        public SqlDataReader readerCliente = null;
        public ControlObra contobra = new ControlObra();
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();
        //Controles - Metrolink
        private ControlActaEntrega CA = new ControlActaEntrega();
        private ControlEstadoProyecto CEP = new ControlEstadoProyecto();
        // Definicion de mensajes por Idioma
        public readonly string[] msgWar01 = { "German", "Spanish", "Corrects", "Wrongs" };

        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(btnSubirPlano);
            scripManager.RegisterPostBackControl(lkSubirPlanosDoc);

            if (!IsPostBack)
            {
                    Session["Activa"] = null;

                    Idioma();
                    ValidarFUP();
                    ValidacionGeneralFUP();
                    ColorCampoObligatorio();
                    PoblarVersion();
                    PoblarMoneda();
                    PoblarTipoUnion();
                    PoblarFormaConstruccion();
                    PoblarRecotizacion();
                    PoblarTemaRechazo();
                    PoblarTemaRechazoComerc();
                    PoblarResponsable();
                    PoblarEvento();
                    PoblarTipoProyecto();
                    PoblarTipoAnexo();
                    PoblarTipoCotizacion();
                    //PoblarProducidoEn();
                    chkVB2.Enabled = false;
                    FDocument.Visible = false;
                    this.PoblarCombosObra();
                    PoblarClaseCot();

                    string rcID = (string)Session["rcID"];
                    int rol = (int)Session["Rol"];
                    string pais = (string)Session["pais"];

                    //VERIFICO SI VIENE PARA ACTUALIZAR
                    if (Request.QueryString["idCliente"] != null)
                    {
                        string idCliente = Request.QueryString["idCliente"];
                        Session["Cliente"] = idCliente;
                        Session["idCliente"] = idCliente;
                        this.PoblarCliente1(sender, e);
                        lkCliente.Enabled = true;
                        lkContacto.Enabled = true;
                        lkObra.Enabled = true;                        
                    }
                    else
                    {
                        if ((rol == 3) || (rol == 28) || (rol == 29) || (rol == 33) || (rol == 30))
                        {
                            this.PoblarListaPais();
                        }
                        else
                        {
                            this.PoblarListaPais2();
                        }
                    }
            

                    if (Request.QueryString["fup"] != null)
                    {
                        string fup = Request.QueryString["fup"];
                        txtFUP.Text = fup;
                        txtFUP_TextChanged(sender, e);
                    }

                    Session["Evento"] = 0;
                    //boton guardar cambios anulado - JORGE CARDONA - METROLINK
                    btnActualizar.Visible = false;
                    panelActaEntrega.Visible = false;
                    ReportActaEntrega.Visible = false;
                    
                    //activo los botones de acta de entrega equipo
                    this.botonesActaEntrega();
            }
        }

        //protected void Page_Init(object sender, EventArgs e)
        //{
        //    Server.ScriptTimeout = 6000;
        //}

        private void botonesActaEntrega()
        {
            int rol = (int)Session["Rol"];
            if ((rol == 2) || (rol == 9) || (rol == 30))
            {
                btnGuardarActa.Visible = false;
                btnFinalizar.Visible = true;
            }
            else
            {
                btnGuardarActa.Visible = false;
                btnFinalizar.Visible = false;
            }
        }

        private void PoblarCombosObra()
        {
            //CARGAMOS ESTRATO
            reader = contobra.PoblarEstadoSocioEconomico();
            
                cboEstrato.Items.Add(new ListItem("Seleccione El Estrato","0"));
                while (reader.Read())
                {

                    cboEstrato.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }            
            
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            //CARGAMOS EL TIPO DE VIVIENDA
            
                cboVivienda.Items.Add(new ListItem("Seleccione","0"));
                cboVivienda.Items.Add(new ListItem("Apartamento","1"));
                cboVivienda.Items.Add(new ListItem("Casa","2"));
                cboVivienda.Items.Add(new ListItem("Hotel","3"));
                cboVivienda.Items.Add(new ListItem("Carcel","4"));
                cboVivienda.Items.Add(new ListItem("Otro", "5")); 
        }

        private void VisualizarCampos()
        {
            int rol = (int)Session["Rol"];

            //HABILITACIÓN DE LOS ACORDEONES
            AcorInfoGeneral.Visible = true;
            AccorDetEspecif.Visible = true;
            AccorSubirArch.Visible = true;
            AccorSalidaCot.Visible = true;
            AccorRechazo.Visible = true;
            AccorSegPFT.Visible = true;
            AccorCierre.Visible = true;
            AccorOF.Visible = true;
            AccFUP.Visible = true;
            AccRecotiza.Visible = true;
            AccControlCambios.Visible = true;

            //HABILITACION DE LOS PANELES
            pnlMercadeo.Visible = true;
            pnlTitDetalle.Visible = true;
            pnlEspecificaciones.Visible = true;
            pnlEspecTec.Visible = true;
            pnlPlanos.Visible = true;
            pnlPuntoFijo.Visible = true;
            pnlDatosConstructivos.Visible = true;
            pnlDetalles.Visible = true;


            //HABILITACION DE LOS BOTONES 
            btnGuardar.Visible = false;
            btnRechazo.Visible = true;
            btnVB.Visible = true;
            txtModulaciones.Visible = true;
            lblModulaciones.Visible = true;
            btnDetalle.Visible = true;
            btnSubir.Visible = true;
            lkSubirPlanosDoc.Visible = true;
            btnGuardarSalida.Visible = false;            
            btnGuardarRechazo.Visible = true;
            btnNewRechazo.Visible = true;
            btnEnviarCorreoRechazo.Visible = true;
            btnGuardarPlanoForsa.Visible = true;
            btnNuevoTipoForsa.Visible = true;
            btnSubirImagen.Visible = true;
            btnGuardaCierre.Visible = true;
            btnSolicitud.Visible = true;
            btnGenerar.Visible = true;
            btnGuardarRecotiza.Visible = false;
            //btnGuardarCambios.Visible = true;
            //btnNuevoCambio.Visible = true;
            //chkCotRapida.Visible = true;
            chkPlanoForsa.Visible = true;

            if ((rol == 24) || (rol == 26) || (rol == 33) || (rol == 13))
            {
                btnSubirCarta.Visible = true;
            }
            else
            {
                btnSubirCarta.Visible = false;
            }

        }

        private void ColorCampoObligatorio()
        {
            cboFormaAlum.BackColor = Color.Yellow;
        }

        private void ValidacionGeneralFUP()
        {
            VisualizarCampos();
            //opcion ->Acordion, guardar ->botones
            string opcion = "0", guardar = "0";
            int rol = (int)Session["Rol"];
            int posicion = 0;
            bool vis, upd, activa;

            if ((LEstado.Text == "Cotizado") && (rol == 2 || rol == 3 || rol == 9 || rol == 30 || rol == 34 || rol == 40))
            {
                if (chkRechComer.Checked == true) chkRechComer.Enabled = false; else chkRechComer.Enabled = true;              
            }

            

            dsFUP = controlfup.ConsultarOpcion(rol);

            foreach (DataRow fila in dsFUP.Tables[0].Rows)
            {
                posicion = Convert.ToInt32(fila[1]);
                opcion = fila[2].ToString();
                guardar = fila[3].ToString();

                if (opcion == "0") vis = false;
                else vis = true;
                if (guardar == "0") upd = false;
                else upd = true;

                if (posicion == 1)
                {
                    AcorInfoGeneral.Visible = vis;
                    btnGuardar.Visible = upd;
                }
                if (posicion == 2)
                {
                    AccorDetEspecif.Visible = vis;
                    btnDetalle.Visible = upd;

                }
                if (posicion == 3)
                {
                    AccorSubirArch.Visible = vis;
                    lkSubirPlanosDoc.Visible = upd;
                }
                if (posicion == 4)
                {
                    AccorSalidaCot.Visible = vis;
                    if ((LEstado.Text == "Aprobado") || (LEstado.Text == "Cotizado") || (LEstado.Text == "Cierre Comercial"))
                    {
                        btnGuardarSalida.Visible = upd;
                        btnEnviarCotiz.Visible = upd;
                        btnGuardar.Visible = false;
                        btnDetalle.Visible = false;
                        lkSubirPlanosDoc.Visible = false;
                    }
                    else
                    {
                        btnGuardarSalida.Visible = false;
                        btnEnviarCotiz.Visible = false;
                    }
                }
                if (posicion == 5)
                {
                    AccorRechazo.Visible = vis;
                    if (LEstado.Text == "Guardado")
                    {
                        pnlRechazo.Visible = upd;
                        btnRechazo.Visible = upd;
                        btnVB.Visible = upd;
                        txtModulaciones.Enabled = upd;
                        lblModulaciones.Enabled = upd;
                        gvRechazo.Enabled = upd;
                        btnGuardarRechazo.Visible = upd;
                        btnGuardar.Visible = false;
                        btnDetalle.Visible = false;
                        
                    }
                    else
                    {
                        if (LEstado.Text == "Rechazado")
                        {
                            pnlRechazo.Visible = upd;
                            gvRechazo.Enabled = upd;
                            btnVB.Visible = false;
                            txtModulaciones.Enabled = false;
                            lblModulaciones.Enabled = false;
                            chkRecotiza.Visible = false;
                        }
                        else
                        {
                            pnlRechazo.Visible = false;
                            btnRechazo.Visible = false;
                            btnVB.Visible = false;
                            txtModulaciones.Enabled = false;
                            lblModulaciones.Enabled = false;
                            gvRechazo.Enabled = false;
                        }
                    }

                }
                if (posicion == 6)
                {
                    if (chkPlanoForsa.Checked == true)
                    {
                        lblFecSalPlano.Visible = vis;
                        txtFecSalida.Visible = vis;
                        AccorSegPFT.Visible = vis;
                        btnGuardarPlanoForsa.Visible = upd;
                    }
                    else
                    {
                        lblFecSalPlano.Visible = false;
                        txtFecSalida.Visible = false;
                        AccorSegPFT.Visible = false;
                        btnGuardarPlanoForsa.Visible = false;
                    }
                }
                if (posicion == 7)
                {
                    AccorCierre.Visible = vis;

                    if ((LEstado.Text == "Cierre Comercial") ||
                       (LEstado.Text == "Solicitud Facturacion") || (LEstado.Text == "Orden Fabricacion"))
                    {
                        btnGuardaCierre.Visible = upd;
                        btnSolicitud.Visible = upd;
                    }
                    else
                    {
                        btnGuardaCierre.Visible = false;
                        btnSolicitud.Visible = false;

                        //if ((LEstado.Text == "Cotizado") && (cboClaseCot.SelectedItem.Value == "3"))
                        if ((LEstado.Text == "Cotizado") )
                        {
                            btnGuardaCierre.Visible = true;
                            btnSolicitud.Visible = true;
                            lblCierre.Visible = false;
                        }
                        else
                        {
                            lblCierre.Text = "Solo Se Pueden Cerrar Proyectos Con Estado de Fup: Cotizado ";
                            btnGuardaCierre.Visible = false;
                            btnSolicitud.Visible = false;
                            lblCierre.Visible = true;
                        }                            
                      }                                     
                }

                if (posicion == 8)
                {
                    AccorOF.Visible = vis;
                    if ((LEstado.Text == "Cierre Comercial") || (LEstado.Text == "Solicitud Facturacion") || (LEstado.Text == "Orden Fabricacion"))
                    {
                        pnlOF.Visible = upd;
                        btnGenerar.Visible = upd;
                        ObtenerProbabilidad();
                        btnGuardaCierre.Visible = true;
                        btnSolicitud.Visible = true;
                        lblCierre.Visible = false;
                    }
                    else
                    {
                        pnlOF.Visible = false;
                        btnGenerar.Visible = false;
                    }
                }
                if (posicion == 9)
                {
                    AccFUP.Visible = vis;
                }
                if (posicion == 10)
                {
                    AccRecotiza.Visible = vis;
                    btnGuardarRecotiza.Visible = upd;
                }
                if (posicion == 11)
                {
                    AccControlCambios.Visible = vis;
                    //btnGuardarCambios.Visible = upd;
                }
                if (posicion == 12)
                {
                    pnlMercadeo.Visible = vis;
                }
            }

            dsFUP.Tables.Remove("Table");
            dsFUP.Dispose();
            dsFUP.Clear();

            //Recotizacion y Cotizacion Rapida  SOLO AGENTES Y ASISTENTE
            if ((rol == 9) || (rol == 3) || (rol == 2) || (rol == 30) || (rol == 33))
            {
                chkPlanoForsa.Enabled = true;
                if ( (LEstado.Text == "Cotizado") || (LEstado.Text == "Cierre Comercial"))
                {
                    chkRecotiza.Visible = true;
                }
                else
                {
                    chkRecotiza.Visible = false;
                }
                if ((LEstado.Text == "" || LEstado.Text == "Ingresado"))
                {
                    //chkCotRapida.Enabled = true;
                }
                else
                {
                    //chkCotRapida.Enabled = false;
                }

            }
            else
            {
                chkPlanoForsa.Enabled = false;
               // chkCotRapida.Enabled = false;
                chkRecotiza.Visible = false;
            }

            //1 -> EQUIPO NUEVO; 2 -> ADAPTACIÓN; 3 -> LISTADO 
            if (cboTipoCotizacion.SelectedItem.Value == "3") //LISTADO DE PIEZAS
            {
                chkPlanoForsa.Visible = false;
               // chkCotRapida.Visible = false;

                if ((rol == 24) || (rol == 26) || (rol == 33) || (rol == 13))
                {
                    btnSubirListado.Visible = false;
                    btnSubir.Visible = false;
                    pnlTitDetalle.Visible = false;
                    pnlEspecificaciones.Visible = false;
                    pnlEspecTec.Visible = false;
                    pnlPlanos.Visible = false;
                    pnlPuntoFijo.Visible = false;
                    pnlDatosConstructivos.Visible = false;
                    pnlDetalles.Visible = false;
                    btnDetalle.Visible = false;
                    btnSubirCarta.Visible = true;
                }
                else
                {
                    btnSubirListado.Visible = true;
                    AccorDetEspecif.Visible = false;
                    btnSubirCarta.Visible = false;
                }

            }

            if (LEstado.Text == "Ingresado")
            {
                if (chkAluminio.Checked == true) cboTipoAluminio.Enabled = true;
                if (chkAcero.Checked == true) cboTipoAcero.Enabled = true;
                //cboTipoCotizacion.Enabled = true;
            }


            if (LEstado.Text == "Ingresado" || LEstado.Text == "Rechazado" || LEstado.Text == "Guardado"
                || LEstado.Text == "")
            {
                txtAlcance.Enabled = true;
                txtObserva.Enabled = true;
                chkAccesorios.Enabled = true;
                chkRecotiza.Visible = false;
                if (cboTipoCotizacion.SelectedItem.Value == "1")
                    chkAccesorios.Enabled = false;

               

            }
            else
            {
                txtAlcance.Enabled = false;
                txtObserva.Enabled = false;
                chkAccesorios.Enabled = false;
            }

            //VALIDAR CAMPOS DETALLE
            int SeModifica = 0;
            // Validar Detalle Entrada Cotizacion
            if (cboTipoCotizacion.SelectedItem.Value != "3") //NO ES LISTADO DE PIEZAS
            {
                if (LEstado.Text == "Ingresado" || LEstado.Text == "Rechazado" || LEstado.Text == "Guardado")
                {
                    SeModifica = 1;
                }

                //3 ->ACERO; 2 ->PLASTICO; 1 ->ALUMINIO
                if ((chkAcero.Checked == true) && (SeModifica == 1))
                {
                    ActivarCamposDetalle(3, true);
                }
                else
                {
                    ActivarCamposDetalle(3, false);
                    LimpiarCheckBox();
                }

                if ((chkPlastico.Checked == true) && (SeModifica == 1))
                {
                    ActivarCamposDetalle(2, true);
                }
                else
                {
                    ActivarCamposDetalle(2, false);
                    LimpiarCheckBox();
                }

                if ((chkAluminio.Checked == true) && (SeModifica == 1))
                {
                    ActivarCamposDetalle(1, true);
                }
                else
                {
                    ActivarCamposDetalle(1, false);
                    LimpiarCheckBox();
                }
            }
            // Validar Salida Cotizacion
            SeModifica = 0;
            if (LEstado.Text == "Aprobado" || LEstado.Text == "Cotizado"
                || LEstado.Text == "Cierre Comercial" || LEstado.Text == "Solicitud Facturacion")
            {
                SeModifica = 1;
            }

            if ((chkAcero.Checked == true) && (SeModifica == 1))
            {
                ActivarCamposSalidaCot(3, true);
            }
            else
            {
                ActivarCamposSalidaCot(3, false);
            }

            if ((chkPlastico.Checked == true) && (SeModifica == 1))
            {
                ActivarCamposSalidaCot(2, true);
            }
            else
            {
                ActivarCamposSalidaCot(2, false);
            }

            if ((chkAluminio.Checked == true) && (SeModifica == 1))
            {
                ActivarCamposSalidaCot(1, true);
            }
            else
            {
                ActivarCamposSalidaCot(1, false);
            }

            // Cotizacion Rapida
            int vRapida = 0;

            if (cboTipoCotizacion.SelectedItem.Value != "3")
            {
                if (chkCotRapida.Checked == true && cboTipoCotizacion.SelectedItem.Value == "1")
                {
                    vRapida = 1;
                }

                if (vRapida == 1)
                {
                    pnlEspecificaciones.Enabled = false;
                    cboTUAlum.Enabled = false;
                    txtEPAlum.Enabled = false;
                    txtEVAlum.Enabled = false;
                    cboTUPlast.Enabled = false;
                    txtEPPlast.Enabled = false;
                    txtEVPlast.Enabled = false;
                    cboTUAcero.Enabled = false;
                    txtEPAcero.Enabled = false;
                    txtEVAcero.Enabled = false;
                    pnlPlanos.Enabled = false;
                    pnlPuntoFijo.Enabled = false;
                    pnlDatosConstructivos.Enabled = false;
                    pnlDetalles.Enabled = false;

                    pnlEspecificaciones.Visible = false;
                    pnlPlanos.Visible = false;
                    pnlPuntoFijo.Visible = false;
                    pnlDatosConstructivos.Visible = false;
                    pnlDetalles.Visible = false;
                }
                else
                {
                    pnlEspecificaciones.Enabled = true;
                    if (chkAluminio.Checked == true)
                    {
                        cboTUAlum.Enabled = true;
                        txtEPAlum.Enabled = true;
                        txtEVAlum.Enabled = true;
                    }
                    if (chkPlastico.Checked == true)
                    {
                        cboTUPlast.Enabled = true;
                        txtEPPlast.Enabled = true;
                        txtEVPlast.Enabled = true;
                    }
                    if (chkAcero.Checked == true)
                    {
                        cboTUAcero.Enabled = true;
                        txtEPAcero.Enabled = true;
                        txtEVAcero.Enabled = true;
                    }
                    pnlPuntoFijo.Enabled = true;
                    pnlDatosConstructivos.Enabled = true;
                    pnlDetalles.Enabled = true;
                    pnlPlanos.Enabled = true;
                    pnlEspecificaciones.Enabled = true;

                    pnlEspecificaciones.Visible = true;
                    pnlPlanos.Visible = true;
                    pnlPuntoFijo.Visible = true;
                    pnlDatosConstructivos.Visible = true;
                    pnlDetalles.Visible = true;
                }
            }
            if (LEstado.Text == "")
            {
                btnGuardar.Text = "Guardar";
            }
            if (LEstado.Text == "Cotizado" || LEstado.Text == "Aprobado" || LEstado.Text == "Cierre Comercial" 
                ||LEstado.Text == "Solicitud Facturacion" || LEstado.Text == "Orden Fabricacion" )
            {
                btnGuardar.Visible = false;
                btnDetalle.Visible = false;
                lkSubirPlanosDoc.Visible = false;
            }
            if (LEstado.Text == "Recotizado")
            {
                btnGuardarRecotiza.Visible = false;
            }
            if (chkPlanoForsa.Checked == true)
                PoblarTipoAnexo(1);

            if (Session["Activa"] != null)
            {
                activa = (bool)Session["Activa"];
                if (activa == true)
                {
                    btnGuardar.Text = "Actualizar";
                }
                else
                {
                    btnGuardar.Visible = false;
                    btnDetalle.Visible = false;
                    chkPlanoForsa.Enabled = false;
                    
                }
            }


        }

        private void PoblarVersion()
        {
            if (txtFUP.Text == "")
            {
                cboVersion.Items.Clear();
                cboVersion.Items.Add("A");
            }
            else
            {
                //CONSULTAMOS LA VERSION CON EL FUP
                cboVersion.Items.Clear();
                reader = controlfup.PoblarVersion(Convert.ToInt32(txtFUP.Text.Trim()));
                if (reader.Read() == false)
                {
                    cboVersion.Items.Add("A");
                    reader.Close();
                    reader.Dispose();
                    BdDatos.desconectar();
                }
                else
                {
                    reader.Close();
                    reader.Dispose();
                    BdDatos.desconectar();

                    reader = controlfup.PoblarVersion(Convert.ToInt32(txtFUP.Text.Trim()));
                    while (reader.Read())
                    {
                        cboVersion.Items.Add(new ListItem(reader.GetString(1)));
                    }
                    reader.Close();
                    reader.Dispose();
                    BdDatos.desconectar();
                }
            }
        }

        private void ObtenerOrdenFabricacion()
        {
            reader = controlfup.consultarOrdenFabricacionColombiaFUP(Convert.ToInt32(txtFUP.Text),
                cboVersion.SelectedItem.Text.Trim());
            if (reader.Read() == false)
            {
                txtOF.Text = "";
            }
            else
            {
                txtOF.Text = reader.GetValue(0).ToString() + '-' + reader.GetValue(1).ToString();
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void ObtenerProbabilidad()
        {
            reader = controlfup.consultarProbabilidad(Convert.ToInt32(txtFUP.Text),
                cboVersion.SelectedItem.Text.Trim());
            if (reader.Read() == false)
            {
                lblProbabilidad.Text = "";
            }
            else
            {
                lblProbabilidad.Text = reader.GetValue(1).ToString();
                if (reader.GetValue(2).ToString() == "1900-01-01")
                {
                    lblFechaFac.Text = " Sin Fecha a Facturar";
                }
                else
                {
                    lblFechaFac.Text = " Facturar: "+reader.GetValue(2).ToString();
                }
                lblProbabilidad.BackColor = Color.Yellow;
                string a = reader.GetValue(0).ToString();
                string b = reader.GetValue(2).ToString();
                if ((reader.GetValue(0).ToString() == "4" || reader.GetValue(0).ToString() == "5") && (reader.GetValue(2).ToString() != "1900-01-01" ))
                {
                    btnGenerar.Visible = true;
                }
                else
                {
                    if (cboTipoCotizacion.SelectedItem.Value == "5")
                    {
                        btnGenerar.Visible = true;
                    }
                    else
                    {
                        btnGenerar.Visible = false;
                    }                    
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarTipoUnion()
        {
            cboTUAlum.Items.Clear();
            cboTUPlast.Items.Clear();
            cboTUAcero.Items.Clear();

            reader = controlfup.PoblarTipoUnion();
            while (reader.Read())
            {
                cboTUAlum.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                cboTUPlast.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                cboTUAcero.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            } 
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarFormaConstruccion()
        {
            cboFormaAlum.Items.Clear();
            cboFormaPlast.Items.Clear();
            cboFormaAcero.Items.Clear();

            reader = controlfup.PoblarFormaConstruccion();
            while (reader.Read())
            {
                cboFormaAlum.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                cboFormaPlast.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                cboFormaAcero.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            } 
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarRecotizacion()
        {
            cboRecotizacion.Items.Clear();

            reader = controlfup.PoblarRecotizacion();
            while (reader.Read())
            {
                cboRecotizacion.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarTemaRechazo()
        {
            cboTemaRechazo.Items.Clear();
            cboTemaRechazo.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlfup.PoblarTemaRechazo();
            while (reader.Read())
            {
                cboTemaRechazo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarTemaRechazoComerc()
        {
            cboTemaRechCom.Items.Clear();
            cboTemaRechCom.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlfup.PoblarTemaRechazoComerc();
            while (reader.Read())
            {
                cboTemaRechCom.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }


        private void PoblarResponsable()
        {
            cboResponsable.Items.Clear();

            reader = controlfup.PoblarResponsable();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboResponsable.Items.Add(new ListItem("Seleccione", "0"));
            }
            if (idioma == "Ingles")
            {
                cboResponsable.Items.Add(new ListItem("Select", "0"));
            }
            if (idioma == "Portugues")
            {
                cboResponsable.Items.Add(new ListItem("Selecione", "0"));
            }
            while (reader.Read())
            {
                cboResponsable.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarEvento()
        {
            cboEvento.Items.Clear();

            reader = controlfup.PoblarEventos();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboEvento.Items.Add(new ListItem("Seleccione", "0"));
            }
            if (idioma == "Ingles")
            {
                cboEvento.Items.Add(new ListItem("Select", "0"));
            }
            if (idioma == "Portugues")
            {
                cboEvento.Items.Add(new ListItem("Selecione", "0"));
            }
            while (reader.Read())
            {
                cboEvento.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarClaseCot()
        {
            cboClaseCot.Items.Clear();

            reader = controlfup.PoblarClaseCotizacion();
            string idioma = (string)Session["Idioma"];
            
            cboClaseCot.Items.Add(new ListItem("Seleccione", "0"));
            
            while (reader.Read())
            {
                cboClaseCot.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarTipoAnexo(int spf = 0)
        {
            int arRol = (int)Session["Rol"];
            int comercial = 0;
            int cotizador = 0;

            if ((arRol == 24) || (arRol == 26) || (arRol == 33) || (arRol == 13) ) cotizador = 1; else comercial = 1;               


            cboTipoAnexo.Items.Clear();
            cboTipoAnexo.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlfup.PoblarTipoAnexo(comercial,cotizador,spf);
            while (reader.Read())
            {
                cboTipoAnexo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }

            if ( cotizador == 1) cboTipoAnexo.SelectedValue = "6";

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarTipoProyecto()
        {
            cboTipoProy.Items.Clear();

            reader = controlfup.PoblarTipoProyecto();
            while (reader.Read())
            {
                cboTipoProy.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            PoblarTipoVentaProyecto();
        }

        private void PoblarTipoVentaProyecto()
        {
            int proyecto = 0;

            // aluminio
            proyecto = 1;

            cboTipoAluminio.Items.Clear();
            cboTipoAluminio.Items.Add(new ListItem("Tipo Formaleta", "0"));
            reader = controlfup.PoblarTipoVentaProyecto(proyecto);
                while (reader.Read())
                {
                    cboTipoAluminio.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();

            // acero
            proyecto = 3;

            cboTipoAcero.Items.Clear();
            cboTipoAcero.Items.Add(new ListItem("Tipo Formaleta", "0"));
            reader = controlfup.PoblarTipoVentaProyecto(proyecto);
                while (reader.Read())
                {
                    cboTipoAcero.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
        }

        private void PoblarTipoCotizacion()
        {
            cboTipoCotizacion.Items.Clear();
            cboTipoCotizacion.Items.Add(new ListItem("Tipo de Cotizacion", "0"));

            reader = controlfup.PoblarTipoCotizacion();
            while (reader.Read())
            {
                cboTipoCotizacion.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarProducidoEn()
        {
            cboProdOF.Items.Clear();
            cboProdOF.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlfup.PoblarPlantasPv(Convert.ToInt32(txtFUP.Text.Trim()));
            while (reader.Read())
            {
                cboProdOF.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            cboParte.Items.Clear();
            cboParte.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void PoblarPartesOF()
        {
            cboParte.Items.Clear();
            cboParte.Items.Add(new ListItem("Seleccione", "0"));
            int valor;

            reader = controlfup.PoblarPartesOF(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(),Convert.ToInt32(cboProdOF.SelectedItem.Value));
            while (reader.Read())
            {
                valor = reader.GetInt32(0);
                if (valor != -1)
                {
                    cboParte.Items.Add(new ListItem(reader.GetInt32(1).ToString(), reader.GetInt32(0).ToString()));
                    btnGenerar.Enabled = true;
                }
                else
                {
                    cboParte.Items.Add(new ListItem("Seleccione", "0"));
                    btnGenerar.Enabled = false;
                }
            }

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
            ObtenerProbabilidad();
        }

        private void ValidarFUP()
        {
            int arRol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            if (txtFUP.Text == "")
            {
                cboVersion.Items.Clear();
                cboVersion.Items.Add("A");
            }
            else
            {
                reader = controlfup.ConsultarFUP(Convert.ToInt32(txtFUP.Text.Trim()));
                if (reader.Read() == true)
                {
                    bool Accesorio = reader.GetSqlBoolean(2).Value;                    

                    if (Accesorio == false)
                    {
                        if ((arRol == 3) || (arRol == 33) || (arRol == 30))
                        {
                            string pais = reader.GetValue(1).ToString();
                            reader.Close();
                            reader.Dispose();
                            BdDatos.desconectar();

                            reader = controlfup.ValidarPaisRepresentante(Convert.ToInt32(pais), Convert.ToInt32(rcID));
                            if (reader.Read() == true)
                            {
                                PoblarVersion();
                                ObtenerOrdenFabricacion();                                
                                ConsultarEncabezadoEntrada();
                                ConsultarDetalleEntrada();
                                ConsultarSalida();
                                CargarValoresSF();
                                CargarGrillaRechazo();
                                CargarGrillaRechazoCom();
                                ConsultarCierreCom();
                                ParametrosSolicitudFacturacion();
                                CargarGrillaPlanoForsa();
                                CargarGrillaArchivoForsa(); 
                                //PoblarPartesOF();
                                CargarReporteFUP();
                                CargarGrillaRecotizacion();
                                CargarControlCambios();
                                CargarDatosObra();
                                Flete();
                                rutaCartaCotizacion();
                                ObtenerProbabilidad();
                                CargarGrillaOF();
                            }
                            else
                            {
                                if (idioma == "Español")
                                {
                                    mensaje = "No posee permisos sobre el número de fup ingresado.";
                                }
                                if (idioma == "Ingles")
                                {
                                    mensaje = "It does not have permissions on the number fup entered.";
                                }
                                if (idioma == "Portugues")
                                {
                                    mensaje = "Não precisa permissões sobre o número fup informado.";
                                }
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            } 
                        }
                        else
                        {
                            PoblarVersion();
                            ObtenerOrdenFabricacion();                            
                            ConsultarEncabezadoEntrada();
                            ConsultarDetalleEntrada();
                            ConsultarSalida();
                            CargarValoresSF();
                            CargarGrillaRechazo();
                            CargarGrillaRechazoCom();
                            ConsultarCierreCom();
                            ParametrosSolicitudFacturacion();
                            CargarGrillaPlanoForsa();
                            CargarGrillaArchivoForsa(); 
                            //PoblarPartesOF();
                            CargarReporteFUP();
                            CargarGrillaRecotizacion();
                            CargarControlCambios();
                            CargarDatosObra();
                            Flete();
                            rutaCartaCotizacion();
                            ObtenerProbabilidad();
                            CargarGrillaOF();
                        }
                    }
                    else
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "El número de fup ingresado corresponde a pedido de venta.";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "The number entered corresponds to fup sales order.";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "O número digitado corresponde a FUP ordem de venda.";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }

                }
                else
                {
                    reader.Close();
                    reader.Dispose();
                    BdDatos.desconectar();

                    if (idioma == "Español")
                    {
                        mensaje = "El número de fup ingresado no existe. Verifique.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "The number of fup entered does not exist. Verify.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "O número de FUP digitado não existe. Certifique.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        private void ConsultarEncabezadoEntrada()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_entrada_cotizacionNew", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter ID_Cliente = new SqlParameter("@pID_Cliente", SqlDbType.Int);
                    SqlParameter ST_Cliente = new SqlParameter("@pCliente", SqlDbType.VarChar, 50);
                    SqlParameter ID_Moneda = new SqlParameter("@pID_Moneda", SqlDbType.Int);
                    SqlParameter ST_Moneda = new SqlParameter("@pMoneda", SqlDbType.VarChar, 50);
                    SqlParameter ID_Contacto = new SqlParameter("@pID_Contacto", SqlDbType.Int);
                    SqlParameter ST_Contacto = new SqlParameter("@pContacto", SqlDbType.VarChar, 50);
                    SqlParameter ID_Obra = new SqlParameter("@pID_Obra", SqlDbType.Int);
                    SqlParameter ST_Obra = new SqlParameter("@pObra", SqlDbType.VarChar, 50);
                    SqlParameter ID_Pais = new SqlParameter("@pID_Pais", SqlDbType.Int);
                    SqlParameter ST_Pais = new SqlParameter("@pPais", SqlDbType.VarChar, 50);
                    SqlParameter ID_Ciudad = new SqlParameter("@pID_Ciudad", SqlDbType.Int);
                    SqlParameter ST_Ciudad = new SqlParameter("@pCiudad", SqlDbType.VarChar, 50);
                    SqlParameter DATE_FechaCrea = new SqlParameter("@pFecha_crea", SqlDbType.Date);
                    SqlParameter B_Alum = new SqlParameter("@pFAlum", SqlDbType.Bit);
                    SqlParameter B_Plast = new SqlParameter("@pFPlast", SqlDbType.Bit);
                    SqlParameter B_Acero = new SqlParameter("@pFAcero", SqlDbType.Bit);
                    SqlParameter ID_TipoCot = new SqlParameter("@pTipoCotizacion", SqlDbType.Int);
                    SqlParameter B_Accesorios = new SqlParameter("@pAccesorios", SqlDbType.Bit);
                    SqlParameter ID_NumEquip = new SqlParameter("@pNumeroEquipos", SqlDbType.Int);
                    SqlParameter B_TipoForsa = new SqlParameter("@pPlanoTipoForsa", SqlDbType.Bit);
                    SqlParameter B_CotizacionRapida = new SqlParameter("@pCotizacionRapida", SqlDbType.Bit);
                    SqlParameter ST_Alcance = new SqlParameter("@pAlcance", SqlDbType.VarChar, 12500);
                    SqlParameter ST_Descripcion = new SqlParameter("@pDescripcion", SqlDbType.VarChar, 12500);
                    SqlParameter B_Activa = new SqlParameter("@pActiva", SqlDbType.Bit);
                    SqlParameter ST_Email = new SqlParameter("@pContaEmail", SqlDbType.VarChar, 50);
                    SqlParameter ST_Cargo = new SqlParameter("@pContaCargo", SqlDbType.VarChar, 50);
                    SqlParameter ST_Profesion = new SqlParameter("@pContaProfesion", SqlDbType.VarChar, 50);
                    SqlParameter ST_TipoVivienda = new SqlParameter("@pTipoVivienda", SqlDbType.VarChar, 50);
                    SqlParameter ST_Estrato = new SqlParameter("@pEstrato", SqlDbType.VarChar, 50);
                    SqlParameter ID_TotalViviendas = new SqlParameter("@pTotalViviendas", SqlDbType.Int);
                    SqlParameter DC_Area = new SqlParameter("@pTotalArea", SqlDbType.Decimal);
                    SqlParameter ST_Cotizador = new SqlParameter("@pCotizador", SqlDbType.VarChar, 50);
                    SqlParameter ST_CotizacionTipo = new SqlParameter("@pTipoCotizaDesc", SqlDbType.VarChar, 50);
                    SqlParameter B_VistoBueno = new SqlParameter("@pVistoBueno", SqlDbType.Bit);
                    SqlParameter ID_RecotizacionTipo = new SqlParameter("@pTipoRecotizacion", SqlDbType.Int);
                    SqlParameter ST_RecotizacionTipo = new SqlParameter("@pTipoRecotizacionDesc", SqlDbType.VarChar, 50);
                    SqlParameter ST_EstadoProceso = new SqlParameter("@pEstadoProceso", SqlDbType.VarChar, 50);
                    SqlParameter EstadoCliente = new SqlParameter("@pEstadoCli", SqlDbType.VarChar, 50);
                    SqlParameter ID_PaisObra = new SqlParameter("@pIdPaisObra", SqlDbType.Int);
                    SqlParameter ST_PaisObra = new SqlParameter("@pPaisObra", SqlDbType.VarChar, 50);
                    SqlParameter ST_Modulaciones = new SqlParameter("@pModulaciones", SqlDbType.Int);
                    SqlParameter ID_Clase = new SqlParameter("@pClase", SqlDbType.Int);
                    SqlParameter ID_FupAnterior = new SqlParameter("@pFupAnterior", SqlDbType.Int);
                    SqlParameter ID_TipoVentaProyecto = new SqlParameter("@pTipoVentaProyecto", SqlDbType.Int);

                    ID_Cliente.Direction = ParameterDirection.Output;
                    ST_Cliente.Direction = ParameterDirection.Output;
                    ID_Moneda.Direction = ParameterDirection.Output;
                    ST_Moneda.Direction = ParameterDirection.Output;
                    ID_Contacto.Direction = ParameterDirection.Output;
                    ST_Contacto.Direction = ParameterDirection.Output;
                    ID_Obra.Direction = ParameterDirection.Output;
                    ST_Obra.Direction = ParameterDirection.Output;
                    ID_Pais.Direction = ParameterDirection.Output;
                    ST_Pais.Direction = ParameterDirection.Output;
                    ID_Ciudad.Direction = ParameterDirection.Output;
                    ST_Ciudad.Direction = ParameterDirection.Output;
                    DATE_FechaCrea.Direction = ParameterDirection.Output;
                    B_Alum.Direction = ParameterDirection.Output;
                    B_Plast.Direction = ParameterDirection.Output;
                    B_Acero.Direction = ParameterDirection.Output;
                    ID_TipoCot.Direction = ParameterDirection.Output;
                    B_Accesorios.Direction = ParameterDirection.Output;
                    ID_NumEquip.Direction = ParameterDirection.Output;
                    B_TipoForsa.Direction = ParameterDirection.Output;
                    B_CotizacionRapida.Direction = ParameterDirection.Output;
                    ST_Alcance.Direction = ParameterDirection.Output;
                    ST_Descripcion.Direction = ParameterDirection.Output;
                    B_Activa.Direction = ParameterDirection.Output;
                    ST_Email.Direction = ParameterDirection.Output;
                    ST_Cargo.Direction = ParameterDirection.Output;
                    ST_Profesion.Direction = ParameterDirection.Output;
                    ST_TipoVivienda.Direction = ParameterDirection.Output;
                    ST_Estrato.Direction = ParameterDirection.Output;
                    ID_TotalViviendas.Direction = ParameterDirection.Output;
                    DC_Area.Direction = ParameterDirection.Output;
                    ST_Cotizador.Direction = ParameterDirection.Output;
                    ST_CotizacionTipo.Direction = ParameterDirection.Output;
                    B_VistoBueno.Direction = ParameterDirection.Output;
                    ID_RecotizacionTipo.Direction = ParameterDirection.Output;
                    ST_RecotizacionTipo.Direction = ParameterDirection.Output;
                    ST_EstadoProceso.Direction = ParameterDirection.Output;
                    EstadoCliente.Direction = ParameterDirection.Output;
                    ID_PaisObra.Direction = ParameterDirection.Output;
                    ST_PaisObra.Direction = ParameterDirection.Output;
                    ST_Modulaciones.Direction = ParameterDirection.Output;
                    ID_Clase.Direction = ParameterDirection.Output;
                    ID_FupAnterior.Direction = ParameterDirection.Output;
                    ID_TipoVentaProyecto.Direction = ParameterDirection.Output;


                    cmd.Parameters.Add(ID_Cliente);
                    cmd.Parameters.Add(ST_Cliente);
                    cmd.Parameters.Add(ID_Moneda);
                    cmd.Parameters.Add(ST_Moneda);
                    cmd.Parameters.Add(ID_Contacto);
                    cmd.Parameters.Add(ST_Contacto);
                    cmd.Parameters.Add(ID_Obra);
                    cmd.Parameters.Add(ST_Obra);
                    cmd.Parameters.Add(ID_Pais);
                    cmd.Parameters.Add(ST_Pais);
                    cmd.Parameters.Add(ID_Ciudad);
                    cmd.Parameters.Add(ST_Ciudad);
                    cmd.Parameters.Add(DATE_FechaCrea);
                    cmd.Parameters.Add(B_Alum);
                    cmd.Parameters.Add(B_Plast);
                    cmd.Parameters.Add(B_Acero);
                    cmd.Parameters.Add(ID_TipoCot);
                    cmd.Parameters.Add(B_Accesorios);
                    cmd.Parameters.Add(ID_NumEquip);
                    cmd.Parameters.Add(B_TipoForsa);
                    cmd.Parameters.Add(B_CotizacionRapida);
                    cmd.Parameters.Add(ST_Alcance);
                    cmd.Parameters.Add(ST_Descripcion);
                    cmd.Parameters.Add(B_Activa);
                    cmd.Parameters.Add(ST_Email);
                    cmd.Parameters.Add(ST_Cargo);
                    cmd.Parameters.Add(ST_Profesion);
                    cmd.Parameters.Add(ST_TipoVivienda);
                    cmd.Parameters.Add(ST_Estrato);
                    cmd.Parameters.Add(ID_TotalViviendas);
                    cmd.Parameters.Add(DC_Area);
                    cmd.Parameters.Add(ST_Cotizador);
                    cmd.Parameters.Add(ST_CotizacionTipo);
                    cmd.Parameters.Add(B_VistoBueno);
                    cmd.Parameters.Add(ID_RecotizacionTipo);
                    cmd.Parameters.Add(ST_RecotizacionTipo);
                    cmd.Parameters.Add(ST_EstadoProceso);
                    cmd.Parameters.Add(EstadoCliente);
                    cmd.Parameters.Add(ID_PaisObra);
                    cmd.Parameters.Add(ST_PaisObra);
                    cmd.Parameters.Add(ST_Modulaciones);
                    cmd.Parameters.Add(ID_Clase);
                    cmd.Parameters.Add(ID_FupAnterior);
                    cmd.Parameters.Add(ID_TipoVentaProyecto);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        int idCliente = Convert.ToInt32(ID_Cliente.Value);
                        string Cliente = Convert.ToString(ST_Cliente.Value);
                        int idMoneda = Convert.ToInt32(ID_Moneda.Value);
                        string Moneda = Convert.ToString(ST_Moneda.Value);
                        int idContacto = Convert.ToInt32(ID_Contacto.Value);
                        string Contacto = Convert.ToString(ST_Contacto.Value);
                        int idObra = Convert.ToInt32(ID_Obra.Value);
                        string Obra = Convert.ToString(ST_Obra.Value);
                        int idPais = Convert.ToInt32(ID_Pais.Value);
                        string Pais = Convert.ToString(ST_Pais.Value);
                        int idCiudad = Convert.ToInt32(ID_Ciudad.Value);
                        string Ciudad = Convert.ToString(ST_Ciudad.Value);
                        string FechaCrea = Convert.ToString(DATE_FechaCrea.Value);
                        bool Alum = Convert.ToBoolean(B_Alum.Value);
                        bool Plast = Convert.ToBoolean(B_Plast.Value);
                        bool Acero = Convert.ToBoolean(B_Acero.Value);
                        int idTipoCot = Convert.ToInt32(ID_TipoCot.Value);
                        bool Accesorios = Convert.ToBoolean(B_Accesorios.Value);
                        int idNumEquip = Convert.ToInt32(ID_NumEquip.Value);
                        bool TipoForsa = Convert.ToBoolean(B_TipoForsa.Value);
                        bool CotizacionRapida = Convert.ToBoolean(B_CotizacionRapida.Value);
                        string Alcance = Convert.ToString(ST_Alcance.Value);
                        string Descripcion = Convert.ToString(ST_Descripcion.Value);
                        bool Activa = Convert.ToBoolean(B_Activa.Value);
                        string Email = Convert.ToString(ST_Email.Value);
                        string Cargo = Convert.ToString(ST_Cargo.Value);
                        string Profesion = Convert.ToString(ST_Profesion.Value);
                        string TipoVivienda = Convert.ToString(ST_TipoVivienda.Value);
                        string Estrato = Convert.ToString(ST_Estrato.Value);
                        int TotalViviendas = Convert.ToInt32(ID_TotalViviendas.Value);
                        decimal Area = Convert.ToDecimal(DC_Area.Value);
                        string Cotizador = Convert.ToString(ST_Cotizador.Value);
                        string CotizacionTipo = Convert.ToString(ST_CotizacionTipo.Value);
                        bool VB = Convert.ToBoolean(B_VistoBueno.Value);
                        int RecotizacionTipo = Convert.ToInt32(ID_RecotizacionTipo.Value);
                        string RecotizacionTipoDesc = Convert.ToString(ST_RecotizacionTipo.Value);
                        string EstadoProceso = Convert.ToString(ST_EstadoProceso.Value);
                        string Estadocli = Convert.ToString(EstadoCliente.Value);
                        int IdPaisObra = Convert.ToInt32(ID_PaisObra.Value);
                        string PaisObra = Convert.ToString(ST_PaisObra.Value);
                        int Modulaciones = Convert.ToInt32(ST_Modulaciones.Value);
                        int Clase = Convert.ToInt32(ID_Clase.Value);
                        int fupAnterior = Convert.ToInt32(ID_FupAnterior.Value);
                        int tipoVentaProyecto = Convert.ToInt32(ID_TipoVentaProyecto.Value);
                        Session["tipoVentaProyectoSes"] = tipoVentaProyecto;


                        //ASIGNAMOS LOS VALORES A LOS CAMPOS
                        //obtener el id del cliente- Jorge Cardona - Metrolink
                        Session["idClienteActa"] = idCliente;
                        cboPais.Items.Clear();
                        cboPais.Items.Add(new ListItem(Pais, Convert.ToString(idPais)));
                        cboCiudad.Items.Clear();
                        cboCiudad.Items.Add(new ListItem(Ciudad, Convert.ToString(idCiudad)));
                        cboCliente.Items.Clear();
                        cboCliente.Items.Add(new ListItem(Cliente, Convert.ToString(idCliente)));
                        lkCliente.Enabled = true;
                        cboContacto.Items.Clear();
                        cboContacto.Items.Add(new ListItem(Contacto, Convert.ToString(idContacto)));
                        lkContacto.Enabled = true;
                        cboObra.Items.Clear();
                        cboObra.Items.Add(new ListItem(Obra, Convert.ToString(idObra)));
                        lkObra.Enabled = true;
                        lblEmailCont.Text = Email;
                        LCargo.Text = Cargo;
                        LProf.Text = Profesion;
                        LTipoViv.Text = TipoVivienda;
                        LEstrato.Text = Estrato;
                        LNumViv.Text = Convert.ToString(TotalViviendas);
                        LAreaViv.Text = Convert.ToString(Area.ToString("#,##.##"));
                        chkAluminio.Checked = Alum;
                        chkPlastico.Checked = Plast;
                        chkAcero.Checked = Acero;
                        chkAccesorios.Checked = Accesorios;
                        txtNumEquipos.Text = Convert.ToString(idNumEquip);
                        cboMoneda.Items.Clear();
                        cboMoneda.Items.Add(new ListItem(Moneda, Convert.ToString(idMoneda)));
                        lblMonedaFup.Text = Moneda;
                        cboTipoCotizacion.Items.Clear();
                        cboTipoCotizacion.Items.Add(new ListItem(CotizacionTipo, Convert.ToString(idTipoCot)));
                        //cboTipoCotizacion.SelectedValue = Convert.ToString(idTipoCot);
                        LCreoFup.Text = Cotizador;
                        chkPlanoForsa.Checked = TipoForsa;
                        chkCotRapida.Checked = CotizacionRapida;
                        txtAlcance.Text = Alcance;
                        txtObserva.Text = Descripcion;
                        chkVB2.Checked = VB;
                        cboRecotizacion.Items.Clear();
                        cboRecotizacion.Items.Add(new ListItem(RecotizacionTipoDesc, Convert.ToString(RecotizacionTipo)));
                        LEstado.Text = EstadoProceso;
                        LEstadoCli.Text = Estadocli;
                        lblPaisObra.Text = PaisObra;
                        txtModulaciones.Text = Modulaciones.ToString();
                        cboClaseCot.SelectedValue = Clase.ToString();
                        

                        this.marcarClaseCotizacion(); 
                        if (chkAluminio.Checked == true)
                        {
                            cboTipoAluminio.Visible = true;
                            cboTipoAluminio.SelectedValue = tipoVentaProyecto.ToString();
                        }
                        else
                        if (chkAcero.Checked == true)
                        {
                            cboTipoAcero.Visible = true;
                            cboTipoAcero.SelectedValue = tipoVentaProyecto.ToString();
                        }

                        if (IdPaisObra == 8)
                        {
                            lblCont20.Text = "Transporte Sencillo";
                            lblCont40.Text = "Transporte Tractomula";
                        }
                        else
                        {
                            lblCont20.Text = "Contenedores 20 Ton";
                            lblCont40.Text = "Contenedores 40 Ton";
                        }

                        chkAluminio.Enabled = false;
                        chkPlastico.Enabled = false;
                        chkAcero.Enabled = false;

                        cboTipoCotizacion.Enabled = false;
                        cboTipoAcero.Enabled = false;
                        cboTipoAluminio.Enabled = false;

                        int rol = (int)Session["Rol"];
                        if (rol == 26) cboClaseCot.Enabled = true; else cboClaseCot.Enabled = false;

                        txtNumEquipos.Enabled = true;
                        

                        lblEstado.Visible = true;
                        LEstado.Visible = true;

                        lblCreoFup.Visible = true;
                        LCreoFup.Visible = true;

                        Session["Activa"] = false;
                        Session["Activa"] = Activa;

                    }
                    con.Close();                    
                }
            }
        }

        private void PoblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            cboPais.Items.Clear();
            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPais.Items.Add(new ListItem("Seleccione El Pais", "0"));
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add(new ListItem("Select The Country", "0"));
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add(new ListItem("Selecione O País", "0"));
            }

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close(); 
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarMoneda()
        {
            cboMoneda.Items.Clear();

            reader = contubi.poblarMoneda();
            cboMoneda.Items.Add(new ListItem("Seleccione Moneda", "0"));
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboMoneda.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarListaPais2()
        {
            cboPais.Items.Clear();

            reader = contubi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPais.Items.Add(new ListItem("Seleccione El Pais", "0"));
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add(new ListItem("Select The Country", "0"));
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add(new ListItem("Selecione O País", "0"));
            }

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Usted no posee paises asociados.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "You have no partner countries.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Você não tem países parceiros.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            reader.Close(); 
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void Idioma()
        {
            int idiomaId = 0;
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
                idiomaId = 2;
            if (idioma == "Ingles")
                idiomaId = 3;
            if (idioma == "Portugues")
                idiomaId = 4;

            int posicion = 0;

            dsFUP = controlfup.ConsultarIdiomaContacto();

            foreach (DataRow fila in dsFUP.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    lblPais.Text = fila[idiomaId].ToString();
                if (posicion == 2)
                    lblCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblCliente.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblContacto.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblObra.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    //lblEquipo.Text = fila[idiomaId].ToString(); 
                    if (posicion == 7)
                        chkRecotiza.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    //chkAdaptacion.Text = fila[idiomaId].ToString(); 
                    if (posicion == 9)
                        //chkListado.Text = fila[idiomaId].ToString(); 
                        if (posicion == 10)
                            //chkReparacion.Text = fila[idiomaId].ToString(); 
                            if (posicion == 11)
                                lblMoneda.Text = fila[idiomaId].ToString();
                if (posicion == 12)
                    lblNumEquipos.Text = fila[idiomaId].ToString();
                if (posicion == 13)
                    //lblModulacion.Text = fila[idiomaId].ToString(); 
                    if (posicion == 14)
                        //lblAreaUtil.Text = fila[idiomaId].ToString(); 
                        if (posicion == 15)
                            //lblVrAprox.Text = fila[idiomaId].ToString(); 
                            if (posicion == 16)
                                lblProducido.Text = fila[idiomaId].ToString();
                if (posicion == 17)
                    lblVersion.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    chkAccesorios.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    pnlAlcance.GroupingText = fila[idiomaId].ToString();
                if (posicion == 20)
                    pnlObservaciones.GroupingText = fila[idiomaId].ToString();
                if (posicion == 21)
                    pnlEspecificaciones.GroupingText = fila[idiomaId].ToString();
                if (posicion == 22)
                    //lblAltura.Text = fila[idiomaId].ToString(); 
                    if (posicion == 23)
                        //lblEM.Text = fila[idiomaId].ToString();
                        if (posicion == 24)
                            //lblEL.Text = fila[idiomaId].ToString(); 
                            if (posicion == 25)
                                //lblTU.Text = fila[idiomaId].ToString(); 
                                if (posicion == 26)
                                    //lblEP.Text = fila[idiomaId].ToString(); 
                                    if (posicion == 27)
                                        //lblEV.Text = fila[idiomaId].ToString(); 
                                        if (posicion == 28)
                                            pnlPlanos.GroupingText = fila[idiomaId].ToString();
                if (posicion == 29)
                    //chkPlanta.Text = fila[idiomaId].ToString(); 
                    if (posicion == 30)
                        //chkCorteFachada.Text = fila[idiomaId].ToString(); 
                        if (posicion == 31)
                            //chkVaciado.Text = fila[idiomaId].ToString(); 
                            if (posicion == 32)
                                //chkAzotea.Text = fila[idiomaId].ToString(); 
                                if (posicion == 33)
                                    //chkUrba.Text = fila[idiomaId].ToString(); 
                                    if (posicion == 34)
                                        //chkEstructural.Text = fila[idiomaId].ToString(); 
                                        if (posicion == 35)
                                            pnlPuntoFijo.GroupingText = fila[idiomaId].ToString();
                if (posicion == 36)
                    //lblCant.Text = fila[idiomaId].ToString(); 
                    if (posicion == 37)
                        //chklosa.Text = fila[idiomaId].ToString(); 
                        if (posicion == 38)
                            //chkMuro.Text = fila[idiomaId].ToString(); 
                            if (posicion == 39)
                                //chkLosaEscalera.Text = fila[idiomaId].ToString(); 
                                if (posicion == 40)
                                    //chkFosoAscensor.Text = fila[idiomaId].ToString(); 
                                    if (posicion == 41)
                                        //chkFosoEscalera.Text = fila[idiomaId].ToString(); 
                                        if (posicion == 42)
                                            //pnlDatosConstructivos.GroupingText = fila[idiomaId].ToString(); 
                                            if (posicion == 43)
                                                lblForma.Text = fila[idiomaId].ToString();
                if (posicion == 44)
                    //chkJuntaDilata.Text = fila[idiomaId].ToString(); 
                    if (posicion == 45)
                        //chkEJM.Text = fila[idiomaId].ToString(); 
                        if (posicion == 46)
                            //chkDesniveles.Text = fila[idiomaId].ToString(); 
                            if (posicion == 47)
                                //chkRetrocesos.Text = fila[idiomaId].ToString(); 
                                if (posicion == 48)
                                    pnlDetalles.GroupingText = fila[idiomaId].ToString();
            }

            dsFUP.Tables.Remove("Table");
            dsFUP.Dispose();
            dsFUP.Clear();
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudad();
            PoblarMoneda();
        }

        private void PoblarCiudad()
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];

            cboCiudad.Items.Clear();
            if (idioma == "Español")
            {
                cboCiudad.Items.Add(new ListItem("Seleccione La Ciudad", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudad.Items.Add(new ListItem("Select The City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudad.Items.Add(new ListItem("Selecione A Cidade", "0"));
            }

            if (((rol == 3) || (rol == 30)) && (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
            {
                reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
            else
            {
                reader = contubi.poblarListaCiudades(Convert.ToInt32(cboPais.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            int RolId = (int)Session["Rol"];
            int vValido = 1, vTipo = 0;
            int ingreso = -1; int evento = 0; int tipoVentaProyecto = 0;
             
            // VALIDAR CAMPOS REQUERIDOS
            if (cboPais.SelectedIndex == -1 ) { vValido = 0; }
            else if (cboPais.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cboMoneda.SelectedItem.Value == "0") { vValido = 0; }
            else if (cboCiudad.SelectedIndex == -1) { vValido = 0; }
            if (cboCiudad.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            else if (cboObra.SelectedIndex == -1) { vValido = 0; }
            if (cboObra.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            else if (cboCliente.SelectedIndex == -1) { vValido = 0; }
            if (cboCliente.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            else if (cboContacto.SelectedIndex == -1) { vValido = 0; }
            if (cboContacto.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            else if (cboTipoCotizacion.SelectedIndex == -1) { vValido = 0; }
            if (cboTipoCotizacion.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            else if (txtNumEquipos.Text == "0" || txtNumEquipos.Text == "") { vValido = 0; }
            if ((cboEstrato.SelectedItem.Value.ToString() == "0") || (cboVivienda.SelectedItem.Text == "Seleccione") ||
                     (txtM2.Text == "") || (txtUnidades.Text == "") || (txtM2.Text == "0") || (txtUnidades.Text == "0")) { vValido = 0; }
            else if (cboClaseCot.SelectedItem.Value == "0")
            {
                vValido = 0;
            }           

            // Debe de tener Alcance y Descripcion
            if ((txtAlcance.Text.Length == 0) || (txtObserva.Text.Length == 0))
            {
                vValido = 0;
            }
            if (chkRecotiza.Checked == true)
            {
                vValido = 0;
                int ValRec = 0;
                reader = controlfup.ValidarRecotiza(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        ValRec = reader.GetInt32(0);
                    }
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();

                if (ValRec != 0)
                {
                    vValido = 1;
                }
                else vTipo = 1;
            }

            // Debe de tener un tipo de proyecto
            if ((chkAluminio.Checked == false) && (chkAcero.Checked == false) && (chkPlastico.Checked == false))
            {
                vValido = 0;
            }
            else
            {
                if ((chkAluminio.Checked == true) && (cboTipoAluminio.SelectedItem.Value == "0")) vValido = 0;
                if ((chkAcero.Checked == true) && (cboTipoAcero.SelectedItem.Value == "0")) vValido = 0;
            }
             
            if (vValido == 0)
            {
                MensajeAlerta(vTipo);
            }
            else
            {
                if (chkAluminio.Checked == true && cboTipoAluminio.SelectedItem.Value != "0") tipoVentaProyecto = Convert.ToInt32(cboTipoAluminio.SelectedItem.Value);
                else if (chkAcero.Checked == true && cboTipoAcero.SelectedItem.Value != "0") tipoVentaProyecto = Convert.ToInt32(cboTipoAcero.SelectedItem.Value);


                if ((cboTipoCotizacion.SelectedItem.Value == "2" || cboTipoCotizacion.SelectedItem.Value == "3") && (chkAccesorios.Checked == false) )
                {
                    string mensaje = "Alerta!! ha indicado que el fup no contempla Accesorios.. Verifique!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }

                string Nombre = (string)Session["Nombre_Usuario"];

                //COLOCAMOS EL TEXTO EN MAYUSCULAS
                txtAlcance.Text = txtAlcance.Text.ToUpperInvariant();
                txtObserva.Text = txtObserva.Text.ToUpperInvariant();

               
                if (btnGuardar.Text == "Guardar")
                {
                    //INGRESAMOS LOS DATOS
                     ingreso = controlfup.IngFUP(Convert.ToInt32(cboCliente.SelectedItem.Value),
                        Convert.ToInt32(cboContacto.SelectedItem.Value), Convert.ToInt32(cboObra.SelectedItem.Value),
                        Nombre, chkAluminio.Checked, chkPlastico.Checked, chkAcero.Checked,
                        Convert.ToInt32(cboTipoCotizacion.SelectedItem.Value), chkAccesorios.Checked,
                        Convert.ToInt32(txtNumEquipos.Text), chkPlanoForsa.Checked, chkCotRapida.Checked,
                        txtAlcance.Text, txtObserva.Text, Convert.ToInt32(cboMoneda.SelectedItem.Value),Convert.ToInt32(cboClaseCot.SelectedItem.Value), 0, tipoVentaProyecto);

                    string id = cboObra.SelectedItem.Value;
                    int actualizar = contobra.ActualizarObraFup(Convert.ToInt32(id), Convert.ToInt32(cboEstrato.SelectedValue), 
                        cboVivienda.SelectedItem.Text,Convert.ToInt32(txtUnidades.Text), Convert.ToDecimal(txtM2.Text), Nombre);

                    txtFUP.Text = Convert.ToString(ingreso);
                    btnGuardar.Text = "Actualizar";
                    //Session["Evento"] = 1;
                }
                else
                {
                    if (chkRecotiza.Checked == true)
                    {
                        ingreso = controlfup.ActREC(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                        chkAluminio.Checked, chkPlastico.Checked, chkAcero.Checked, chkAccesorios.Checked, Convert.ToInt32(txtNumEquipos.Text),
                        chkPlanoForsa.Checked, chkCotRapida.Checked, txtAlcance.Text, txtObserva.Text, Convert.ToInt32(cboRecotizacion.SelectedItem.Value), Convert.ToInt32(cboClaseCot.SelectedItem.Value), 0, tipoVentaProyecto);

                        chkRecotiza.Checked = false;
                        PoblarVersion();
                        ConsultarEncabezadoEntrada();
                        ConsultarDetalleEntrada();
                        ConsultarSalida();

                        string id = cboObra.SelectedItem.Value;
                        int actualizar = contobra.ActualizarObraFup(Convert.ToInt32(id), Convert.ToInt32(cboEstrato.SelectedValue),
                            cboVivienda.SelectedItem.Text, Convert.ToInt32(txtUnidades.Text), Convert.ToDecimal(txtM2.Text), Nombre);
                    }
                    else
                    {
                        ingreso = controlfup.ActFUP(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                            chkAccesorios.Checked, Convert.ToInt32(txtNumEquipos.Text), chkPlanoForsa.Checked, chkCotRapida.Checked,
                            txtAlcance.Text, txtObserva.Text, chkVB2.Checked, Convert.ToInt32(txtModulaciones.Text), Convert.ToInt32(cboClaseCot.SelectedItem.Value), 0, tipoVentaProyecto, RolId);

                        string id = cboObra.SelectedItem.Value;
                        int actualizar = contobra.ActualizarObraFup(Convert.ToInt32(id), Convert.ToInt32(cboEstrato.SelectedValue),
                            cboVivienda.SelectedItem.Text, Convert.ToInt32(txtUnidades.Text), Convert.ToDecimal(txtM2.Text), Nombre);

                    }
                }
                // Envio de Mensajes diferente a Recotizacion
                if (chkRecotiza.Checked != true)
                {

                    if (Convert.ToInt32(cboTipoCotizacion.SelectedItem.Value) == 3)  //Listado de Piezas
                    {
                        Session["Evento"]  = 2;
                        CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(),2);
                    }
                    if (chkCotRapida.Checked == true)
                    {
                        Session["Evento"] = 6;
                        CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(),6);
                    }
                    if (chkPlanoForsa.Checked == true)
                    {
                        Session["Evento"] = 7;
                        CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), 7);
                    }
                }

                if (ingreso != -1)
                {
                   string mensaje = "Se ha Ingresado el FUP " + txtFUP.Text+ " Exitosamente!";            
                   ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        
                }

                MensajeEncabezado();
                AccorDetEspecif.Enabled = true;

                ValidarFUP();
                ValidacionGeneralFUP();
            }
            //cargaDatosCambios();
        }

        private void MensajeAlerta(int tipo = 0)
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];
            string adicion = "";
            if (tipo == 1) adicion = "* Recotizacion *";
            if (idioma == "Español")
            {
                mensaje = "Seleccione los datos obligatorios (*)." + adicion;
            }
            if (idioma == "Ingles")
            {
                mensaje = "Select the required data (*)." + adicion;
            }
            if (idioma == "Portugues")
            {
                mensaje = "Selecione o obrigatório (*)." + adicion;
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeEncabezado()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (btnGuardar.Text == "Guardar")
            {               
               mensaje = "Informacion en estado 'Ingresado', Recuerde que debe dar clic en 'Enviar' en la pestaña 'Detalles Tecnicos'";               
            }
            else
            {
                mensaje = "Informacion en estado 'Ingresado', Recuerde que debe dar clic en 'Enviar' en la pestaña 'Detalles Tecnicos'";                 
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeDetalleEncabezado()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Datos actualizados correctamente. Mensaje enviado.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Data updated successfully. message sent.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Dados atualizados corretamente. mensagem enviada.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeSalida()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Salida de la cotizacion ingresada con éxito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Exit quotation successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Sair cotação entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void AlertaSalidaCot()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Recuerde digitar los campos necesarios de la solicitud";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Remember to type in the required fields of the application";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Lembre-se de digitar os campos obrigatórios da aplicação";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void cboCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCliente();
        }
        private void PoblarCliente1(object sender, EventArgs e)
        {
            string cliente = (string)Session["Cliente"];
            readerCliente = concli.ConsultarCliente(Convert.ToInt32(cliente));
            readerCliente.Read();

            bool tipo = readerCliente.GetSqlBoolean(0).Value;
           
            cboPais.Items.Clear();
            cboPais.Items.Add(new ListItem(readerCliente.GetString(11), readerCliente.GetInt32(10).ToString()));
            cboCiudad.Items.Clear();
            cboCiudad.Items.Add(new ListItem(readerCliente.GetString(13), readerCliente.GetInt32(12).ToString()));
            cboCliente.Items.Clear();
            cboCliente.Items.Add(new ListItem(readerCliente.GetString(2), cliente));
            cboCliente_SelectedIndexChanged(sender, e);
            ImageButton1.Visible = true;

            readerCliente.Close();
            readerCliente.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarCliente()
        {
            string idioma = (string)Session["Idioma"];

            cboCliente.Items.Clear();
            reader = concli.ConsultarDatosCliente(Convert.ToInt32(cboCiudad.SelectedItem.Value),0);
            if (idioma == "Español")
            {
                cboCliente.Items.Add(new ListItem("Seleccione El Cliente", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCliente.Items.Add(new ListItem("Select Customer", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCliente.Items.Add(new ListItem("Selecione Cliente", "0"));
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                lkCliente.Enabled = true;
            }

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }
       
        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.poblarListaContacto();
            this.PoblarObra();
        }

        private void PoblarObra()
        {
            string idioma = (string)Session["Idioma"];
            if ((cboCliente.SelectedItem.Value.ToString() == "0"))
            {
                cboObra.Items.Clear();
            }
            else
            {
                cboObra.Items.Clear();
                if (idioma == "Español")
                {
                    cboObra.Items.Add(new ListItem("Seleccione La Obra", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboObra.Items.Add(new ListItem("Select The Project", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboObra.Items.Add(new ListItem("Selecione El Projeto", "0"));
                }
                reader = controlCont.ObtnObrasDistPV(Convert.ToInt32(cboCliente.SelectedValue));
                while (reader.Read())
                {
                    cboObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));

                }
                lkObra.Enabled = true;
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
        }

        private void poblarListaContacto()
        {
            if (cboCliente.SelectedItem.Value.ToString() == "0")
            {
                cboContacto.Items.Clear();
            }
            else
            {
                reader = concli.ObtenerContacto(Convert.ToInt32(cboCliente.SelectedValue));
                cboContacto.Items.Clear();
                cboContacto.Items.Add(new ListItem("Seleccione El Contacto", "0"));
                while (reader.Read())
                {
                    string nombre_contacto = reader.GetValue(0).ToString();
                    cboContacto.Items.Add(new ListItem(nombre_contacto, reader.GetInt32(1).ToString()));
                }
                lkContacto.Enabled = true;
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
        }


        protected void txtFUP_TextChanged(object sender, EventArgs e)
        {
            //VALIDO SI ES NUMERICO 
            //txtFUP.Text = IsNumeric(txtFUP.Text);
            //LO CONVIERTO A ENTERO
            txtFUP.Text = txtFUP.Text.Replace(",", "");
            //POBLAMOS LAS VERSIONES
            ValidarFUP();
            ValidacionGeneralFUP();
            //Carga metodos para el acta de entrega - Jorge Cardona - Metrolink
            panelActaEntrega.Visible = false;
            ReportActaEntrega.Visible = false;
            limpiarCamposActa();
            llenarComboOrdenes();
            //Carga el metodo para cambiar el estado del fup
            cargaDatosCambios();
            this.botonesActaEntrega();
            this.PoblarProducidoEn();
            CargarGrillaOF();
        }

        protected void cboVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarEncabezadoEntrada();
            ConsultarDetalleEntrada();
            ConsultarSalida();
            CargarGrillaRechazo();
            CargarGrillaRechazoCom();
            ConsultarCierreCom();
            ParametrosSolicitudFacturacion();
            CargarGrillaPlanoForsa();
            CargarGrillaArchivoForsa();
            
            //PoblarPartesOF();
            CargarReporteFUP();
            CargarGrillaRecotizacion();

            bool Activo = (bool)Session["Activa"];
            if (Activo == false)
            {
                btnGuardaCierre.Visible = false;
                btnSolicitud.Visible = false;
            }
            else
            {
                btnGuardaCierre.Visible = true;
                btnSolicitud.Visible = true;
            }

            ValidacionGeneralFUP();
            CargarGrillaOF();
        }

        protected void btnDetalle_Click(object sender, EventArgs e)
        {
            int arRol = (int)Session["Rol"];

            int valido = 1;
            if (valido == 0)
            {
                MensajeAlerta();
            }
            else
            {
                if (chkAluminio.Checked == true)
                {
                    int IngDET = controlfup.IngDET(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), 1, chkEMuroAlum.Checked, chkELosaAlum.Checked,
                        chkEUMLAlum.Checked, txtALAlum.Text, txtEMAlum.Text, txtELAlum.Text, Convert.ToInt32(cboTUAlum.SelectedItem.Value), txtEPAlum.Text, txtEVAlum.Text,
                        chkPlantaAlum.Checked, chkCorteFachadaAlum.Checked, chkAzotAlum.Checked, chkUrbaAlum.Checked, chkEstructuralAlum.Checked, chklosaAlum.Checked,
                        chkMuroAlum.Checked, chkLosaEscaleraAlum.Checked, chkFosoAscensorAlum.Checked, chkFosoEscaleraAlum.Checked, Convert.ToInt32(cboFormaAlum.SelectedItem.Value),
                        chkJuntaDilataAlum.Checked, txtEspJunAlum.Text, chkDesnAscAlum.Checked, chkDesnDescAlum.Checked, chkCulatsPerimAlum.Checked,
                        chkCulatasInternasAlum.Checked, chkAntepechosAlum.Checked, chkColumnasAlum.Checked, chkEscMonAlum.Checked, chkEscPostAlum.Checked, chkBaseAlum.Checked,
                        chkLosaInclinadaAlum.Checked, chkDomoAlum.Checked, chkMPAlum.Checked, chkNegAceroAlum.Checked, chkPretilesAlum.Checked, chkGargolasAlum.Checked,
                        chkMFTAlum.Checked, chkNegCarriolasAlum.Checked, chkVEAlum.Checked, chkFCMqAlum.Checked, chkVigasAlum.Checked, chkTorreonAlum.Checked, chkRebordesAlum.Checked,
                        chkReservatoriosAlum.Checked, chkDilFacAlum.Checked, chkJCAIAlum.Checked, chkJCAEAlum.Checked, chkCanesAlum.Checked, chkPortAlum.Checked, chkOtrosAlum.Checked, arRol);
                }

                if (chkPlastico.Checked == true)
                {
                    int IngDET = controlfup.IngDET(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), 2, chkEMuroPlast.Checked, chkELosaPlast.Checked,
                        chkEUMLPlast.Checked, txtALPlast.Text, txtEMPlast.Text, txtELPlast.Text, Convert.ToInt32(cboTUPlast.SelectedItem.Value), txtEPPlast.Text, txtEVPlast.Text,
                        chkPlantaPlast.Checked, chkCorteFachadaPlast.Checked, chkAzotPlast.Checked, chkUrbaPlast.Checked, chkEstructuralPlast.Checked, chklosaPlast.Checked, chkMuroPlast.Checked, chkLosaEscaleraPlast.Checked, chkFosoAscensorPlast.Checked, chkFosoEscaleraPlast.Checked,
                        Convert.ToInt32(cboFormaPlast.SelectedItem.Value), chkJuntaDilataPlast.Checked, txtEspJunPlast.Text, chkDesnAscPlast.Checked, chkDesnDescPlast.Checked, chkCulatsPeriPlast.Checked, chkCulatasInternasPlast.Checked, chkAntepechosPlast.Checked, chkColumnasPlast.Checked, chkEscMonPlast.Checked,
                        chkEscPostPlast.Checked, chkBasePlast.Checked, chkLosaInclinadaPlast.Checked, chkDomoPlast.Checked, chkMPPlast.Checked, chkNegAceroPlast.Checked, chkPretilesPlast.Checked,
                        chkGargolasPlast.Checked, chkMFTPlast.Checked, chkNegCarriolasPlast.Checked, chkVEPlast.Checked, chkFCMqPlast.Checked, chkVigasPlast.Checked, chkTorreonPlast.Checked,
                        chkRebordesPlast.Checked, chkReservatoriosPlast.Checked, chkDilFacPlast.Checked, chkJCAIPlast.Checked, chkJCAEPlast.Checked, chkCanesPlast.Checked, chkPortPlast.Checked,
                        chkOtrosPlast.Checked, arRol);
                }

                if (chkAcero.Checked == true)
                {
                    int IngDET = controlfup.IngDET(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), 3,
                        chkEMuroAcero.Checked, chkELosaAcero.Checked, chkEUMLAcero.Checked, txtALAcero.Text, txtEMAcero.Text, txtELAcero.Text, Convert.ToInt32(cboTUAcero.SelectedItem.Value),
                        txtEPAcero.Text, txtEVAcero.Text, chkPlantaAcero.Checked, chkCorteFachadaAcero.Checked, chkAzotAcero.Checked, chkUrbaAcero.Checked, chkEstructuralAcero.Checked,
                        chklosaAcero.Checked, chkMuroAcero.Checked, chkLosaEscaleraAcero.Checked, chkFosoAscensorAcero.Checked, chkFosoEscaleraAcero.Checked,
                        Convert.ToInt32(cboFormaAcero.SelectedItem.Value), chkJuntaDilataAcero.Checked, txtEspJunAcero.Text, chkDesnAscAcero.Checked, chkDesnDescAcero.Checked,
                        chkCulatsPerimAcero.Checked, chkCulatasInternasAcero.Checked, chkAntepechosAcero.Checked, chkColumnasAcero.Checked, chkEscMonAcero.Checked,
                        chkEscPostAcero.Checked, chkBaseAcero.Checked, chkLosaInclinadaAcero.Checked, chkDomoAcero.Checked, chkMPAcero.Checked, chkNegAceroAce.Checked, chkPretilesAcero.Checked,
                        chkGargolasAcero.Checked, chkMFTAcero.Checked, chkNegCarriolasAcero.Checked, chkVEAcero.Checked, chkFCMqAcero.Checked, chkVigasAcero.Checked, chkTorreonAcero.Checked,
                        chkRebordesAcero.Checked, chkReservatoriosAcero.Checked, chkDilFacAcero.Checked, chkJCAIAcero.Checked, chkJCAEcero.Checked, chkCanesAcero.Checked, chkPortAcero.Checked,
                        chkOtrosAcero.Checked, arRol);
                }

                if (LEstado.Text == "" || LEstado.Text == "Ingresado")
                    Session["Evento"] = 2;
                else
                    Session["Evento"] = 11;

                CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(),Convert.ToInt32( Session["Evento"]));
                MensajeDetalleEncabezado();

                ValidarFUP();
                ValidacionGeneralFUP();
            }
        }

        private void ConsultarDetalleEntrada()
        {
            int TipoProyecto = 0;

            if (chkAluminio.Checked == true)
            {
                TipoProyecto = 1;
                Session["Tipo"] = TipoProyecto;
                ObtenerDetalleEntrada();
            }

            if (chkPlastico.Checked == true)
            {
                TipoProyecto = 2;
                Session["Tipo"] = TipoProyecto;
                ObtenerDetalleEntrada();
            }

            if (chkAcero.Checked == true)
            {
                TipoProyecto = 3;
                Session["Tipo"] = TipoProyecto;
                ObtenerDetalleEntrada();
            }
        }

        private void ObtenerDetalleEntrada()
        {
            int TipoProyecto = (int)Session["Tipo"];

            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());
            sqls[2] = new SqlParameter("@tipo_proyecto", TipoProyecto);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_det_entrada_cot", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Espec_Muros = new SqlParameter("@espec_Muros", SqlDbType.Bit);
                    SqlParameter Espec_Losa = new SqlParameter("@espec_Losa", SqlDbType.Bit);
                    SqlParameter Espec_UML = new SqlParameter("@espec_Union_ml", SqlDbType.Bit);
                    SqlParameter Altura_Libre = new SqlParameter("@esptec_Altura_Libre", SqlDbType.VarChar, 50);
                    SqlParameter Espesor_Muro = new SqlParameter("@esptec_Espesor_muro", SqlDbType.VarChar, 50);
                    SqlParameter Espesor_Losa = new SqlParameter("@esptec_Espesor_losa", SqlDbType.VarChar, 50);
                    SqlParameter Tipo_Union = new SqlParameter("@esptec_Tipo_union", SqlDbType.Int);
                    SqlParameter Enrase_Puerta = new SqlParameter("@esptec_Enrase_puerta", SqlDbType.VarChar, 50);
                    SqlParameter Enrase_Ventana = new SqlParameter("@esptec_Enrase_ventana", SqlDbType.VarChar, 50);
                    SqlParameter Plano_Planta = new SqlParameter("@plano_Planta", SqlDbType.Bit);
                    SqlParameter Plano_Cortes_Fachada = new SqlParameter("@plano_Cortes_Fachada", SqlDbType.Bit);
                    SqlParameter Plano_Azotea = new SqlParameter("@plano_Azotea", SqlDbType.Bit);
                    SqlParameter Plano_Urbanistico = new SqlParameter("@plano_Urbanistico", SqlDbType.Bit);
                    SqlParameter Plano_Estructural = new SqlParameter("@plano_estructural", SqlDbType.Bit);
                    SqlParameter PF_Muro = new SqlParameter("@ptofijo_Muro", SqlDbType.Bit);
                    SqlParameter PF_Losa = new SqlParameter("@ptofijo_Losa", SqlDbType.Bit);
                    SqlParameter PF_Azotea = new SqlParameter("@ptofijo_Azotea", SqlDbType.Bit);
                    SqlParameter PF_Ascensor = new SqlParameter("@ptofijo_foso_ascensor", SqlDbType.Bit);
                    SqlParameter PF_Escalera = new SqlParameter("@ptofijo_foso_escalera", SqlDbType.Bit);
                    SqlParameter Forma_Const = new SqlParameter("@dconst_forma", SqlDbType.Int);
                    SqlParameter Dilata_Muro = new SqlParameter("@dconst_dilata_muro", SqlDbType.Bit);
                    SqlParameter Espesor_Juntas = new SqlParameter("@dconst_espesor_juntas", SqlDbType.VarChar, 10);
                    SqlParameter Desnivel_Asc = new SqlParameter("@dconst_desnivel_asc", SqlDbType.Bit);
                    SqlParameter Desnivel_Desc = new SqlParameter("@dconst_desnivel_des", SqlDbType.Bit);
                    SqlParameter Culata_Perim = new SqlParameter("@detal_culata_perim", SqlDbType.Bit);
                    SqlParameter Culata_Interna = new SqlParameter("@detal_culata_interna", SqlDbType.Bit);
                    SqlParameter Antepecho = new SqlParameter("@detal_antepecho", SqlDbType.Bit);
                    SqlParameter Columna = new SqlParameter("@detal_columna", SqlDbType.Bit);
                    SqlParameter Escalera_Monolitica = new SqlParameter("@detal_escalera_monolitica", SqlDbType.Bit);
                    SqlParameter Escalera_Posterior = new SqlParameter("@detal_escalera_posterior", SqlDbType.Bit);
                    SqlParameter Base_Tinaco = new SqlParameter("@detal_base_tinaco", SqlDbType.Bit);
                    SqlParameter Losa_Inclinada = new SqlParameter("@detal_losa_inclinada", SqlDbType.Bit);
                    SqlParameter Domo = new SqlParameter("@detal_domo", SqlDbType.Bit);
                    SqlParameter Muros_Patio = new SqlParameter("@detal_muros_patio", SqlDbType.Bit);
                    SqlParameter Neg_Acero = new SqlParameter("@detal_Neg_acero", SqlDbType.Bit);
                    SqlParameter Pretiles = new SqlParameter("@detal_pretiles", SqlDbType.Bit);
                    SqlParameter Gargolas = new SqlParameter("@detal_gargolas", SqlDbType.Bit);
                    SqlParameter Muro_Formaleta = new SqlParameter("@detal_muro_formaleta", SqlDbType.Bit);
                    SqlParameter Neg_Carriola = new SqlParameter("@detal_neg_carriola", SqlDbType.Bit);
                    SqlParameter Ventana_Especial = new SqlParameter("@detal_ventana_especial", SqlDbType.Bit);
                    SqlParameter Cuarto_Maquinas = new SqlParameter("@detal_cuarto_maquinas", SqlDbType.Bit);
                    SqlParameter Vigas_Descolgadas = new SqlParameter("@detal_vigas_descolgadas", SqlDbType.Bit);
                    SqlParameter Torreon = new SqlParameter("@detal_torreon", SqlDbType.Bit);
                    SqlParameter Rebordes = new SqlParameter("@detal_rebordes", SqlDbType.Bit);
                    SqlParameter Reservatorios = new SqlParameter("@detal_reservatorios", SqlDbType.Bit);
                    SqlParameter Dilatacion_Fachada = new SqlParameter("@detal_dilatacion_fachada", SqlDbType.Bit);
                    SqlParameter Junta_Int_Antep = new SqlParameter("@detal_junta_int_antep", SqlDbType.Bit);
                    SqlParameter Junta_Ext_Antep = new SqlParameter("@detal_junta_ext_antep", SqlDbType.Bit);
                    SqlParameter Canes = new SqlParameter("@detal_canes", SqlDbType.Bit);
                    SqlParameter Porticos = new SqlParameter("@detal_porticos", SqlDbType.Bit);
                    SqlParameter Otros = new SqlParameter("@detal_otros", SqlDbType.Bit);
                    SqlParameter TipoUnionDesc = new SqlParameter("@tipo_union_desc", SqlDbType.VarChar, 50);
                    SqlParameter FormaConstDesc = new SqlParameter("@forma_const_desc", SqlDbType.VarChar, 50);

                    Espec_Muros.Direction = ParameterDirection.Output;
                    Espec_Losa.Direction = ParameterDirection.Output;
                    Espec_UML.Direction = ParameterDirection.Output;
                    Altura_Libre.Direction = ParameterDirection.Output;
                    Espesor_Muro.Direction = ParameterDirection.Output;
                    Espesor_Losa.Direction = ParameterDirection.Output;
                    Tipo_Union.Direction = ParameterDirection.Output;
                    Enrase_Puerta.Direction = ParameterDirection.Output;
                    Enrase_Ventana.Direction = ParameterDirection.Output;
                    Plano_Planta.Direction = ParameterDirection.Output;
                    Plano_Cortes_Fachada.Direction = ParameterDirection.Output;
                    Plano_Azotea.Direction = ParameterDirection.Output;
                    Plano_Urbanistico.Direction = ParameterDirection.Output;
                    Plano_Estructural.Direction = ParameterDirection.Output;
                    PF_Muro.Direction = ParameterDirection.Output;
                    PF_Losa.Direction = ParameterDirection.Output;
                    PF_Azotea.Direction = ParameterDirection.Output;
                    PF_Ascensor.Direction = ParameterDirection.Output;
                    PF_Escalera.Direction = ParameterDirection.Output;
                    Forma_Const.Direction = ParameterDirection.Output;
                    Dilata_Muro.Direction = ParameterDirection.Output;
                    Espesor_Juntas.Direction = ParameterDirection.Output;
                    Desnivel_Asc.Direction = ParameterDirection.Output;
                    Desnivel_Desc.Direction = ParameterDirection.Output;
                    Culata_Perim.Direction = ParameterDirection.Output;
                    Culata_Interna.Direction = ParameterDirection.Output;
                    Antepecho.Direction = ParameterDirection.Output;
                    Columna.Direction = ParameterDirection.Output;
                    Escalera_Monolitica.Direction = ParameterDirection.Output;
                    Escalera_Posterior.Direction = ParameterDirection.Output;
                    Base_Tinaco.Direction = ParameterDirection.Output;
                    Losa_Inclinada.Direction = ParameterDirection.Output;
                    Domo.Direction = ParameterDirection.Output;
                    Muros_Patio.Direction = ParameterDirection.Output;
                    Neg_Acero.Direction = ParameterDirection.Output;
                    Pretiles.Direction = ParameterDirection.Output;
                    Gargolas.Direction = ParameterDirection.Output;
                    Muro_Formaleta.Direction = ParameterDirection.Output;
                    Neg_Carriola.Direction = ParameterDirection.Output;
                    Ventana_Especial.Direction = ParameterDirection.Output;
                    Cuarto_Maquinas.Direction = ParameterDirection.Output;
                    Vigas_Descolgadas.Direction = ParameterDirection.Output;
                    Torreon.Direction = ParameterDirection.Output;
                    Rebordes.Direction = ParameterDirection.Output;
                    Reservatorios.Direction = ParameterDirection.Output;
                    Dilatacion_Fachada.Direction = ParameterDirection.Output;
                    Junta_Int_Antep.Direction = ParameterDirection.Output;
                    Junta_Ext_Antep.Direction = ParameterDirection.Output;
                    Canes.Direction = ParameterDirection.Output;
                    Porticos.Direction = ParameterDirection.Output;
                    Otros.Direction = ParameterDirection.Output;
                    TipoUnionDesc.Direction = ParameterDirection.Output;
                    FormaConstDesc.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Espec_Muros);
                    cmd.Parameters.Add(Espec_Losa);
                    cmd.Parameters.Add(Espec_UML);
                    cmd.Parameters.Add(Altura_Libre);
                    cmd.Parameters.Add(Espesor_Muro);
                    cmd.Parameters.Add(Espesor_Losa);
                    cmd.Parameters.Add(Tipo_Union);
                    cmd.Parameters.Add(Enrase_Puerta);
                    cmd.Parameters.Add(Enrase_Ventana);
                    cmd.Parameters.Add(Plano_Planta);
                    cmd.Parameters.Add(Plano_Cortes_Fachada);
                    cmd.Parameters.Add(Plano_Azotea);
                    cmd.Parameters.Add(Plano_Urbanistico);
                    cmd.Parameters.Add(Plano_Estructural);
                    cmd.Parameters.Add(PF_Muro);
                    cmd.Parameters.Add(PF_Losa);
                    cmd.Parameters.Add(PF_Azotea);
                    cmd.Parameters.Add(PF_Ascensor);
                    cmd.Parameters.Add(PF_Escalera);
                    cmd.Parameters.Add(Forma_Const);
                    cmd.Parameters.Add(Dilata_Muro);
                    cmd.Parameters.Add(Espesor_Juntas);
                    cmd.Parameters.Add(Desnivel_Asc);
                    cmd.Parameters.Add(Desnivel_Desc);
                    cmd.Parameters.Add(Culata_Perim);
                    cmd.Parameters.Add(Culata_Interna);
                    cmd.Parameters.Add(Antepecho);
                    cmd.Parameters.Add(Columna);
                    cmd.Parameters.Add(Escalera_Monolitica);
                    cmd.Parameters.Add(Escalera_Posterior);
                    cmd.Parameters.Add(Base_Tinaco);
                    cmd.Parameters.Add(Losa_Inclinada);
                    cmd.Parameters.Add(Domo);
                    cmd.Parameters.Add(Muros_Patio);
                    cmd.Parameters.Add(Neg_Acero);
                    cmd.Parameters.Add(Pretiles);
                    cmd.Parameters.Add(Gargolas);
                    cmd.Parameters.Add(Muro_Formaleta);
                    cmd.Parameters.Add(Neg_Carriola);
                    cmd.Parameters.Add(Ventana_Especial);
                    cmd.Parameters.Add(Cuarto_Maquinas);
                    cmd.Parameters.Add(Vigas_Descolgadas);
                    cmd.Parameters.Add(Torreon);
                    cmd.Parameters.Add(Rebordes);
                    cmd.Parameters.Add(Reservatorios);
                    cmd.Parameters.Add(Dilatacion_Fachada);
                    cmd.Parameters.Add(Junta_Int_Antep);
                    cmd.Parameters.Add(Junta_Ext_Antep);
                    cmd.Parameters.Add(Canes);
                    cmd.Parameters.Add(Porticos);
                    cmd.Parameters.Add(Otros);
                    cmd.Parameters.Add(TipoUnionDesc);
                    cmd.Parameters.Add(FormaConstDesc);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        bool EspMur = Convert.ToBoolean(Espec_Muros.Value);
                        bool EspLos = Convert.ToBoolean(Espec_Losa.Value);
                        bool EspUML = Convert.ToBoolean(Espec_UML.Value);
                        string AlturaLibre = Convert.ToString(Altura_Libre.Value);
                        string EspesorMuro = Convert.ToString(Espesor_Muro.Value);
                        string EspesorLosa = Convert.ToString(Espesor_Losa.Value);
                        int TipoUnion = Convert.ToInt32(Tipo_Union.Value);
                        string EnrasePuerta = Convert.ToString(Enrase_Puerta.Value);
                        string EnraseVentana = Convert.ToString(Enrase_Ventana.Value);
                        bool PlanoPlanta = Convert.ToBoolean(Plano_Planta.Value);
                        bool PlanoCortesFachada = Convert.ToBoolean(Plano_Cortes_Fachada.Value);
                        bool PlanoAzotea = Convert.ToBoolean(Plano_Azotea.Value);
                        bool PlanoUrbanistico = Convert.ToBoolean(Plano_Urbanistico.Value);
                        bool PlanoEstructural = Convert.ToBoolean(Plano_Estructural.Value);
                        bool PFMuro = Convert.ToBoolean(PF_Muro.Value);
                        bool PFLosa = Convert.ToBoolean(PF_Losa.Value);
                        bool PFAzotea = Convert.ToBoolean(PF_Azotea.Value);
                        bool PFAscensor = Convert.ToBoolean(PF_Ascensor.Value);
                        bool PFEscalera = Convert.ToBoolean(PF_Escalera.Value);
                        int FormaConst = Convert.ToInt32(Forma_Const.Value);
                        bool DilataMuro = Convert.ToBoolean(Dilata_Muro.Value);
                        string EspesorJuntas = Convert.ToString(Espesor_Juntas.Value);
                        bool DesnivelAsc = Convert.ToBoolean(Desnivel_Asc.Value);
                        bool DesnivelDesc = Convert.ToBoolean(Desnivel_Desc.Value);
                        bool CulataPerim = Convert.ToBoolean(Culata_Perim.Value);
                        bool CulataInterna = Convert.ToBoolean(Culata_Interna.Value);
                        bool Antep = Convert.ToBoolean(Antepecho.Value);
                        bool Colum = Convert.ToBoolean(Columna.Value);
                        bool EscaleraMonolitica = Convert.ToBoolean(Escalera_Monolitica.Value);
                        bool EscaleraPosterior = Convert.ToBoolean(Escalera_Posterior.Value);
                        bool BaseTinaco = Convert.ToBoolean(Base_Tinaco.Value);
                        bool LosaInclinada = Convert.ToBoolean(Losa_Inclinada.Value);
                        bool BDomo = Convert.ToBoolean(Domo.Value);
                        bool MurosPatio = Convert.ToBoolean(Muros_Patio.Value);
                        bool NegAcero = Convert.ToBoolean(Neg_Acero.Value);
                        bool BPretiles = Convert.ToBoolean(Pretiles.Value);
                        bool BGargolas = Convert.ToBoolean(Gargolas.Value);
                        bool MuroFormaleta = Convert.ToBoolean(Muro_Formaleta.Value);
                        bool NegCarriola = Convert.ToBoolean(Neg_Carriola.Value);
                        bool VentanaEspecial = Convert.ToBoolean(Ventana_Especial.Value);
                        bool CuartoMaquinas = Convert.ToBoolean(Cuarto_Maquinas.Value);
                        bool VigasDescolgadas = Convert.ToBoolean(Vigas_Descolgadas.Value);
                        bool BTorreon = Convert.ToBoolean(Torreon.Value);
                        bool BRebordes = Convert.ToBoolean(Rebordes.Value);
                        bool BReservatorios = Convert.ToBoolean(Reservatorios.Value);
                        bool DilatacionFachada = Convert.ToBoolean(Dilatacion_Fachada.Value);
                        bool JuntaIntAntep = Convert.ToBoolean(Junta_Int_Antep.Value);
                        bool JuntaExtAntep = Convert.ToBoolean(Junta_Ext_Antep.Value);
                        bool BCanes = Convert.ToBoolean(Canes.Value);
                        bool BPorticos = Convert.ToBoolean(Porticos.Value);
                        bool BOtros = Convert.ToBoolean(Otros.Value);
                        string TUDesc = Convert.ToString(TipoUnionDesc.Value);
                        string FConstDesc = Convert.ToString(FormaConstDesc.Value);

                        //ASIGNAMOS LOS VALORES A LOS CAMPOS
                        if (TipoProyecto == 1)
                        {
                            chkEMuroAlum.Checked = EspMur;
                            chkELosaAlum.Checked = EspLos;
                            chkEUMLAlum.Checked = EspUML;
                            txtALAlum.Text = AlturaLibre;
                            txtEMAlum.Text = EspesorMuro;
                            txtELAlum.Text = EspesorLosa;
                            cboTUAlum.SelectedValue = Convert.ToString(TipoUnion);
                            txtEPAlum.Text = EnrasePuerta;
                            txtEVAlum.Text = EnraseVentana;
                            chkPlantaAlum.Checked = PlanoPlanta;
                            chkCorteFachadaAlum.Checked = PlanoCortesFachada;
                            chkAzotAlum.Checked = PlanoAzotea;
                            chkUrbaAlum.Checked = PlanoUrbanistico;
                            chkEstructuralAlum.Checked = PlanoEstructural;
                            chklosaAlum.Checked = PFLosa;
                            chkMuroAlum.Checked = PFMuro;
                            chkLosaEscaleraAlum.Checked = PFAzotea;
                            chkFosoAscensorAlum.Checked = PFAscensor;
                            chkFosoEscaleraAlum.Checked = PFEscalera;
                            cboFormaAlum.SelectedValue = Convert.ToString(FormaConst);
                            chkJuntaDilataAlum.Checked = DilataMuro;
                            txtEspJunAlum.Text = EspesorJuntas;
                            chkDesnAscAlum.Checked = DesnivelAsc;
                            chkDesnDescAlum.Checked = DesnivelDesc;
                            chkCulatsPerimAlum.Checked = CulataPerim;
                            chkCulatasInternasAlum.Checked = CulataInterna;
                            chkAntepechosAlum.Checked = Antep;
                            chkColumnasAlum.Checked = Colum;
                            chkEscMonAlum.Checked = EscaleraMonolitica;
                            chkEscPostAlum.Checked = EscaleraPosterior;
                            chkBaseAlum.Checked = BaseTinaco;
                            chkLosaInclinadaAlum.Checked = LosaInclinada;
                            chkDomoAlum.Checked = BDomo;
                            chkMPAlum.Checked = MurosPatio;
                            chkNegAceroAlum.Checked = NegAcero;
                            chkPretilesAlum.Checked = BPretiles;
                            chkGargolasAlum.Checked = BGargolas;
                            chkMFTAlum.Checked = MuroFormaleta;
                            chkNegCarriolasAlum.Checked = NegCarriola;
                            chkVEAlum.Checked = VentanaEspecial;
                            chkFCMqAlum.Checked = CuartoMaquinas;
                            chkVigasAlum.Checked = VigasDescolgadas;
                            chkTorreonAlum.Checked = BTorreon;
                            chkRebordesAlum.Checked = BRebordes;
                            chkReservatoriosAlum.Checked = BReservatorios;
                            chkDilFacAlum.Checked = DilatacionFachada;
                            chkJCAIAlum.Checked = JuntaIntAntep;
                            chkJCAEAlum.Checked = JuntaExtAntep;
                            chkCanesAlum.Checked = BCanes;
                            chkPortAlum.Checked = BPorticos;
                            chkOtrosAlum.Checked = BOtros;

                        }

                        if (TipoProyecto == 2)
                        {
                            chkEMuroPlast.Checked = EspMur;
                            chkELosaPlast.Checked = EspLos;
                            chkEUMLPlast.Checked = EspUML;
                            txtALPlast.Text = AlturaLibre;
                            txtEMPlast.Text = EspesorMuro;
                            txtELPlast.Text = EspesorLosa;
                            cboTUPlast.SelectedValue = Convert.ToString(TipoUnion);
                            txtEPPlast.Text = EnrasePuerta;
                            txtEVPlast.Text = EnraseVentana;
                            chkPlantaPlast.Checked = PlanoPlanta;
                            chkCorteFachadaPlast.Checked = PlanoCortesFachada;
                            chkAzotPlast.Checked = PlanoAzotea;
                            chkUrbaPlast.Checked = PlanoUrbanistico;
                            chkEstructuralPlast.Checked = PlanoEstructural;
                            chklosaPlast.Checked = PFLosa;
                            chkMuroPlast.Checked = PFMuro;
                            chkLosaEscaleraPlast.Checked = PFAzotea;
                            chkFosoAscensorPlast.Checked = PFAscensor;
                            chkFosoEscaleraPlast.Checked = PFEscalera;
                            cboFormaPlast.SelectedValue = Convert.ToString(FormaConst);
                            chkJuntaDilataPlast.Checked = DilataMuro;
                            txtEspJunPlast.Text = EspesorJuntas;
                            chkDesnAscPlast.Checked = DesnivelAsc;
                            chkDesnDescPlast.Checked = DesnivelDesc;
                            chkCulatsPeriPlast.Checked = CulataPerim;
                            chkCulatasInternasPlast.Checked = CulataInterna;
                            chkAntepechosPlast.Checked = Antep;
                            chkColumnasPlast.Checked = Colum;
                            chkEscMonPlast.Checked = EscaleraMonolitica;
                            chkEscPostPlast.Checked = EscaleraPosterior;
                            chkBasePlast.Checked = BaseTinaco;
                            chkLosaInclinadaPlast.Checked = LosaInclinada;
                            chkDomoPlast.Checked = BDomo;
                            chkMPPlast.Checked = MurosPatio;
                            chkNegAceroPlast.Checked = NegAcero;
                            chkPretilesPlast.Checked = BPretiles;
                            chkGargolasPlast.Checked = BGargolas;
                            chkMFTPlast.Checked = MuroFormaleta;
                            chkNegCarriolasPlast.Checked = NegCarriola;
                            chkVEPlast.Checked = VentanaEspecial;
                            chkFCMqPlast.Checked = CuartoMaquinas;
                            chkVigasPlast.Checked = VigasDescolgadas;
                            chkTorreonPlast.Checked = BTorreon;
                            chkRebordesPlast.Checked = BRebordes;
                            chkReservatoriosPlast.Checked = BReservatorios;
                            chkDilFacPlast.Checked = DilatacionFachada;
                            chkJCAIPlast.Checked = JuntaIntAntep;
                            chkJCAEPlast.Checked = JuntaExtAntep;
                            chkCanesPlast.Checked = BCanes;
                            chkPortPlast.Checked = BPorticos;
                            chkOtrosPlast.Checked = BOtros;
                        }

                        if (TipoProyecto == 3)
                        {
                            chkEMuroAcero.Checked = EspMur;
                            chkELosaAcero.Checked = EspLos;
                            chkEUMLAcero.Checked = EspUML;
                            txtALAcero.Text = AlturaLibre;
                            txtEMAcero.Text = EspesorMuro;
                            txtELAcero.Text = EspesorLosa;
                            cboTUAcero.SelectedValue = Convert.ToString(TipoUnion);
                            txtEPAcero.Text = EnrasePuerta;
                            txtEVAcero.Text = EnraseVentana;
                            chkPlantaAcero.Checked = PlanoPlanta;
                            chkCorteFachadaAcero.Checked = PlanoCortesFachada;
                            chkAzotAcero.Checked = PlanoAzotea;
                            chkUrbaAcero.Checked = PlanoUrbanistico;
                            chkEstructuralAcero.Checked = PlanoEstructural;
                            chklosaAcero.Checked = PFLosa;
                            chkMuroAcero.Checked = PFMuro;
                            chkLosaEscaleraAcero.Checked = PFAzotea;
                            chkFosoAscensorAcero.Checked = PFAscensor;
                            chkFosoEscaleraAcero.Checked = PFEscalera;
                            cboFormaAcero.SelectedValue = Convert.ToString(FormaConst);
                            chkJuntaDilataAcero.Checked = DilataMuro;
                            txtEspJunAcero.Text = EspesorJuntas;
                            chkDesnAscAcero.Checked = DesnivelAsc;
                            chkDesnDescAcero.Checked = DesnivelDesc;
                            chkCulatsPerimAcero.Checked = CulataPerim;
                            chkCulatasInternasAcero.Checked = CulataInterna;
                            chkAntepechosAcero.Checked = Antep;
                            chkColumnasAcero.Checked = Colum;
                            chkEscMonAcero.Checked = EscaleraMonolitica;
                            chkEscPostAcero.Checked = EscaleraPosterior;
                            chkBaseAcero.Checked = BaseTinaco;
                            chkLosaInclinadaAcero.Checked = LosaInclinada;
                            chkDomoAcero.Checked = BDomo;
                            chkMPAcero.Checked = MurosPatio;
                            chkNegAceroAce.Checked = NegAcero;
                            chkPretilesAcero.Checked = BPretiles;
                            chkGargolasAcero.Checked = BGargolas;
                            chkMFTAcero.Checked = MuroFormaleta;
                            chkNegCarriolasAcero.Checked = NegCarriola;
                            chkVEAcero.Checked = VentanaEspecial;
                            chkFCMqAcero.Checked = CuartoMaquinas;
                            chkVigasAcero.Checked = VigasDescolgadas;
                            chkTorreonAcero.Checked = BTorreon;
                            chkRebordesAcero.Checked = BRebordes;
                            chkReservatoriosAcero.Checked = BReservatorios;
                            chkDilFacAcero.Checked = DilatacionFachada;
                            chkJCAIAcero.Checked = JuntaIntAntep;
                            chkJCAEcero.Checked = JuntaExtAntep;
                            chkCanesAcero.Checked = BCanes;
                            chkPortAcero.Checked = BPorticos;
                            chkOtrosAcero.Checked = BOtros;
                        }
                    }
                    con.Close();
                }
            }
        }

        protected void btnGuardarSalida_Click(object sender, EventArgs e)
        {
            int valido = 1;
            string vTipoProy;
            string Nombre = (string)Session["Nombre_Usuario"];
            //1 -> EQUIPO NUEVO; 2 -> ADAPTACIÓN; 3 -> LISTADO 
            vTipoProy = cboTipoCotizacion.SelectedItem.Value;            

            if (chkAluminio.Checked == true)
            {
                if ((vTipoProy == "1") && ((txtm2Alum.Text.Length == 0) || (txtm2Alum.Text.Trim() == "") || (txtValAlum.Text.Length == 0) || (txtValAlum.Text.Trim() == "") || (txtValAccAlum.Text.Length == 0)))
                    valido = 0;
                if ((vTipoProy == "2") && ((txtm2Alum.Text.Length == 0) || (txtm2Alum.Text.Trim() == "") || (txtValAlum.Text.Length == 0) || (txtValAlum.Text.Trim() == "")))
                    valido = 0;
                if ((vTipoProy == "2") && (chkAccesorios.Checked == true) && ((txtValAccAlum.Text.Length == 0) || (txtValAccAlum.Text == "")))
                    valido = 0;
                //if ((vTipoProy == "3") && (txtValAccAlum.Text.Length == 0))
                //    valido = 0;
            }

            if (chkPlastico.Checked == true)
            {
                if ((vTipoProy == "1") && ((txtm2Plast.Text.Length == 0) || (txtm2Plast.Text.Trim() == "") || (txtValPlast.Text.Length == 0) || (txtValPlast.Text.Trim() == "") || (txtValAccPlast.Text.Length == 0)  ))
                    valido = 0;
                if ((vTipoProy == "2") && ((txtm2Plast.Text.Length == 0) || (txtm2Plast.Text.Trim() == "") || (txtValPlast.Text.Length == 0) || (txtValPlast.Text.Trim() == "")))
                    valido = 0;
                if ((vTipoProy == "2") && (chkAccesorios.Checked == true) && ((txtValAccPlast.Text.Length == 0) || (txtValAccPlast.Text == "")))
                    valido = 0;
                //if ((vTipoProy == "3") && (txtValAccPlast.Text.Length == 0))
                //    valido = 0;
            }

            if (chkAcero.Checked == true)
            {
                if ((vTipoProy == "1") && ((txtm2Acero.Text.Length == 0) || (txtm2Acero.Text.Trim() == "") || (txtValAcero.Text.Length == 0) || (txtValAcero.Text.Trim() == "") || (txtValAccAcero.Text.Length == 0)))
                    valido = 0;
                if ((vTipoProy == "2") && ((txtm2Acero.Text.Length == 0) || (txtm2Acero.Text.Trim() == "") || (txtValAcero.Text.Length == 0) || (txtValAcero.Text.Trim() == "")))
                    valido = 0;
                if ((vTipoProy == "2") && (chkAccesorios.Checked == true) &&  ( (txtValAccAcero.Text.Length == 0) || (txtValAccAcero.Text == "")))
                    valido = 0;
                //if ((vTipoProy == "3") && (txtValAccAcero.Text.Length == 0))
                //    valido = 0;
            }
            if (chkRechComer.Checked == true)
            {
                if ((txtRespRechazo.Text == "") || (txtRespRechazo.Text.Length == 0))
                {
                    valido = 0;
                    string mensaje = "";
                    mensaje = "La cotizacion se encuentra rechazada debe ingresar una respuesta !!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }

            if (valido == 0)
            {
                AlertaSalidaCot();
            }
            else
            {
                int IngSalida = controlfup.IngSalida(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    txtm2Alum.Text.Replace(",", ""), txtValAlum.Text.Replace(",", ""), txtValAccAlum.Text.Replace(",", ""),
                    txtm2Plast.Text.Replace(",", ""), txtValPlast.Text.Replace(",", ""), txtValAccPlast.Text.Replace(",", ""),
                    txtm2Acero.Text.Replace(",", ""), txtValAcero.Text.Replace(",", ""), txtValAccAcero.Text.Replace(",", ""),txtCont20.Text
                    ,txtCont40.Text);

                if (chkRechComer.Checked == true)
                {
                    txtRespRechazo.Text = txtRespRechazo.Text.ToUpperInvariant();
                    int igRespRechazo = controlfup.ActRECHACOM(Convert.ToInt32(cboTemarespRechazo.SelectedItem.Value), Nombre, txtRespRechazo.Text);
                    //CargarComboRechazoResp();
                }

                Session["Evento"] = 5;
                //CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                string mensaje = "Se Registraron los Valores Exitosamente, Debe Subir y Enviar la Cotizacion para Completar el Proceso";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                ValidarFUP();
                ValidacionGeneralFUP();
            }
            cargaDatosCambios();
        }

        private void ConsultarSalida()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_salida_cotizacion", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter M2_Alum = new SqlParameter("@pAlum_m2", SqlDbType.VarChar, 10);
                    SqlParameter Alum_Valor = new SqlParameter("@pAlum_valor", SqlDbType.VarChar, 20);
                    SqlParameter Alum_Valor_Acc = new SqlParameter("@pAlum_vr_acc", SqlDbType.VarChar, 20);
                    SqlParameter M2_Plast = new SqlParameter("@pPlast_m2", SqlDbType.VarChar, 10);
                    SqlParameter Plast_Valor = new SqlParameter("@pPlast_valor", SqlDbType.VarChar, 20);
                    SqlParameter Plast_Valor_Acc = new SqlParameter("@pPlast_vr_acc", SqlDbType.VarChar, 20);
                    SqlParameter M2_Acero = new SqlParameter("@pAcero_m2", SqlDbType.VarChar, 10);
                    SqlParameter Acero_Valor = new SqlParameter("@pAcero_valor", SqlDbType.VarChar, 20);
                    SqlParameter Acero_Valor_Acc = new SqlParameter("@pAcero_vr_acc", SqlDbType.VarChar, 20);
                    SqlParameter M2_Total = new SqlParameter("@pTotal_m2", SqlDbType.VarChar, 10);
                    SqlParameter Valor_Total = new SqlParameter("@pTotal_valor", SqlDbType.VarChar, 20);
                    SqlParameter CotizadoPor = new SqlParameter("@pCotizadopor", SqlDbType.VarChar, 50);
                    SqlParameter conten20 = new SqlParameter("@pconten20", SqlDbType.VarChar, 50);
                    SqlParameter conten40 = new SqlParameter("@pconten40", SqlDbType.VarChar, 50);

                    M2_Alum.Direction = ParameterDirection.Output;
                    Alum_Valor.Direction = ParameterDirection.Output;
                    Alum_Valor_Acc.Direction = ParameterDirection.Output;
                    M2_Plast.Direction = ParameterDirection.Output;
                    Plast_Valor.Direction = ParameterDirection.Output;
                    Plast_Valor_Acc.Direction = ParameterDirection.Output;
                    M2_Acero.Direction = ParameterDirection.Output;
                    Acero_Valor.Direction = ParameterDirection.Output;
                    Acero_Valor_Acc.Direction = ParameterDirection.Output;
                    M2_Total.Direction = ParameterDirection.Output;
                    Valor_Total.Direction = ParameterDirection.Output;
                    CotizadoPor.Direction = ParameterDirection.Output;
                    conten20.Direction = ParameterDirection.Output;
                    conten40.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(M2_Alum);
                    cmd.Parameters.Add(Alum_Valor);
                    cmd.Parameters.Add(Alum_Valor_Acc);
                    cmd.Parameters.Add(M2_Plast);
                    cmd.Parameters.Add(Plast_Valor);
                    cmd.Parameters.Add(Plast_Valor_Acc);
                    cmd.Parameters.Add(M2_Acero);
                    cmd.Parameters.Add(Acero_Valor);
                    cmd.Parameters.Add(Acero_Valor_Acc);
                    cmd.Parameters.Add(M2_Total);
                    cmd.Parameters.Add(Valor_Total);
                    cmd.Parameters.Add(CotizadoPor);
                    cmd.Parameters.Add(conten20);
                    cmd.Parameters.Add(conten40);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string Alum_M2 = Convert.ToString(M2_Alum.Value);
                        string Valor_Alum = Convert.ToString(Alum_Valor.Value);
                        string Valor_Acc_Alum = Convert.ToString(Alum_Valor_Acc.Value);
                        string Plast_M2 = Convert.ToString(M2_Plast.Value);
                        string Valor_Plast = Convert.ToString(Plast_Valor.Value);
                        string Valor_Acc_Plast = Convert.ToString(Plast_Valor_Acc.Value);
                        string Acero_M2 = Convert.ToString(M2_Acero.Value);
                        string Valor_Acero = Convert.ToString(Acero_Valor.Value);
                        string Valor_Acc_Acero = Convert.ToString(Acero_Valor_Acc.Value);
                        string Total_M2 = Convert.ToString(M2_Total.Value);
                        string Total = Convert.ToString(Valor_Total.Value);
                        string CotizadorSalida = Convert.ToString(CotizadoPor.Value);
                        string contenedor20 = Convert.ToString(conten20.Value);
                        string contenedor40 = Convert.ToString(conten40.Value);

                        //ASIGNAMOS LOS VALORES A LOS CAMPOS                        
                        txtm2Alum.Text = IsNumeric(Alum_M2);
                        txtValAlum.Text = IsNumeric(Valor_Alum);
                        txtValAccAlum.Text = IsNumeric(Valor_Acc_Alum);
                        txtm2Plast.Text = IsNumeric(Plast_M2);
                        txtValPlast.Text = IsNumeric(Valor_Plast);
                        txtValAccPlast.Text = IsNumeric(Valor_Acc_Plast);
                        txtm2Acero.Text = IsNumeric(Acero_M2);
                        txtValAcero.Text = IsNumeric(Valor_Acero);
                        txtValAccAcero.Text = IsNumeric(Valor_Acc_Acero);
                        txtTotalm2.Text = IsNumeric(Total_M2);
                        txtTotalValor.Text = IsNumeric(Total);
                        Lcotizadopor.Text = CotizadorSalida;
                        txtCont20.Text = contenedor20;
                        txtCont40.Text = contenedor40;

                        if (Total == "0.00")
                        {
                            AccorCierre.Visible = false;
                        }
                        else
                        {
                            AccorCierre.Visible = true;
                        }

                        rutaCartaCotizacion();
                    }
                    con.Close();
                }
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FUP.aspx");
        }

        protected void btnNewRechazo_Click(object sender, EventArgs e)
        {
            PoblarTemaRechazo();
            txtObservaRechazo.Text = "";
        }

        private void ActivarCamposDetalle(int grupoDet, bool Activar)
        {
            if (grupoDet == 1)
            {
                chkEMuroAlum.Enabled = Activar;
                chkELosaAlum.Enabled = Activar;
                chkEUMLAlum.Enabled = Activar;
                txtALAlum.Enabled = Activar;
                txtEMAlum.Enabled = Activar;
                txtELAlum.Enabled = Activar;
                cboTUAlum.Enabled = Activar;
                txtEPAlum.Enabled = Activar;
                txtEVAlum.Enabled = Activar;
                chkPlantaAlum.Enabled = Activar;
                chkCorteFachadaAlum.Enabled = Activar;
                chkAzotAlum.Enabled = Activar;
                chkUrbaAlum.Enabled = Activar;
                chkEstructuralAlum.Enabled = Activar;
                chklosaAlum.Enabled = Activar;
                chkMuroAlum.Enabled = Activar;
                chkLosaEscaleraAlum.Enabled = Activar;
                chkFosoAscensorAlum.Enabled = Activar;
                chkFosoEscaleraAlum.Enabled = Activar;
                cboFormaAlum.Enabled = Activar;
                cboFormaAlum.Enabled = Activar;
                chkJuntaDilataAlum.Enabled = Activar;
                txtEspJunAlum.Enabled = Activar;
                chkDesnAscAlum.Enabled = Activar;
                chkDesnDescAlum.Enabled = Activar;
                chkCulatsPerimAlum.Enabled = Activar;
                chkCulatasInternasAlum.Enabled = Activar;
                chkAntepechosAlum.Enabled = Activar;
                chkColumnasAlum.Enabled = Activar;
                chkEscMonAlum.Enabled = Activar;
                chkEscPostAlum.Enabled = Activar;
                chkBaseAlum.Enabled = Activar;
                chkLosaInclinadaAlum.Enabled = Activar;
                chkDomoAlum.Enabled = Activar;
                chkMPAlum.Enabled = Activar;
                chkNegAceroAlum.Enabled = Activar;
                chkPretilesAlum.Enabled = Activar;
                chkGargolasAlum.Enabled = Activar;
                chkMFTAlum.Enabled = Activar;
                chkNegCarriolasAlum.Enabled = Activar;
                chkVEAlum.Enabled = Activar;
                chkFCMqAlum.Enabled = Activar;
                chkVigasAlum.Enabled = Activar;
                chkTorreonAlum.Enabled = Activar;
                chkRebordesAlum.Enabled = Activar;
                chkReservatoriosAlum.Enabled = Activar;
                chkDilFacAlum.Enabled = Activar;
                chkJCAIAlum.Enabled = Activar;
                chkJCAEAlum.Enabled = Activar;
                chkCanesAlum.Enabled = Activar;
                chkPortAlum.Enabled = Activar;
                chkOtrosAlum.Enabled = Activar;
                cboTUAlum.Enabled = Activar;
                cboFormaAlum.Enabled = Activar;
            }

            if (grupoDet == 2)
            {
                chkEMuroPlast.Enabled = Activar;
                chkELosaPlast.Enabled = Activar;
                chkEUMLPlast.Enabled = Activar;
                txtALPlast.Enabled = Activar;
                txtEMPlast.Enabled = Activar;
                txtELPlast.Enabled = Activar;
                cboTUPlast.Enabled = Activar;
                txtEPPlast.Enabled = Activar;
                txtEVPlast.Enabled = Activar;
                chkPlantaPlast.Enabled = Activar;
                chkCorteFachadaPlast.Enabled = Activar;
                chkAzotPlast.Enabled = Activar;
                chkUrbaPlast.Enabled = Activar;
                chkEstructuralPlast.Enabled = Activar;
                chklosaPlast.Enabled = Activar;
                chkMuroPlast.Enabled = Activar;
                chkLosaEscaleraPlast.Enabled = Activar;
                chkFosoAscensorPlast.Enabled = Activar;
                chkFosoEscaleraPlast.Enabled = Activar;
                cboFormaPlast.Enabled = false;
                chkJuntaDilataPlast.Enabled = false;
                txtEspJunPlast.Enabled = false;
                chkDesnAscPlast.Enabled = false;
                chkDesnDescPlast.Enabled = false;
                chkCulatsPeriPlast.Enabled = Activar;
                chkCulatasInternasPlast.Enabled = Activar;
                chkAntepechosPlast.Enabled = Activar;
                chkColumnasPlast.Enabled = Activar;
                chkEscMonPlast.Enabled = false;
                chkEscPostPlast.Enabled = false;
                chkBasePlast.Enabled = Activar;
                chkLosaInclinadaPlast.Enabled = false;
                chkDomoPlast.Enabled = false;
                chkMPPlast.Enabled = Activar;
                chkNegAceroPlast.Enabled = Activar;
                chkPretilesPlast.Enabled = Activar;
                chkGargolasPlast.Enabled = false;
                chkMFTPlast.Enabled = false;
                chkNegCarriolasPlast.Enabled = false;
                chkVEPlast.Enabled = false;
                chkFCMqPlast.Enabled = Activar;
                chkVigasPlast.Enabled = Activar;
                chkTorreonPlast.Enabled = false;
                chkRebordesPlast.Enabled = false;
                chkReservatoriosPlast.Enabled = false;
                chkDilFacPlast.Enabled = false;
                chkJCAIPlast.Enabled = false;
                chkJCAEPlast.Enabled = false;
                chkCanesPlast.Enabled = false;
                chkPortPlast.Enabled = false;
                chkOtrosPlast.Enabled = Activar;
                cboTUPlast.Enabled = Activar;
                cboFormaPlast.Enabled = false;
                txtEspJunPlast.Text = "No Aplica";
                chkDesnAscPlast.Checked = false;
                chkDesnDescPlast.Checked = false;
                chkEscMonPlast.Checked = false;
                chkEscPostPlast.Checked = false;
                chkBasePlast.Checked = false;
                chkLosaInclinadaPlast.Checked = false;
                chkDomoPlast.Checked = false;
                chkGargolasPlast.Checked = false;
                chkMFTPlast.Checked = false;
                chkNegCarriolasPlast.Checked = false;
                chkVEPlast.Checked = false;
                chkTorreonPlast.Checked = false;
                chkRebordesPlast.Checked = false;
                chkReservatoriosPlast.Checked = false;
                chkDilFacPlast.Checked = false;
                chkJCAIPlast.Checked = false;
                chkJCAEPlast.Checked = false;
                chkCanesPlast.Checked = false;
                chkPortPlast.Checked = false;
            }

            if (grupoDet == 3)
            {
                chkEMuroAcero.Enabled = Activar;
                chkELosaAcero.Enabled = Activar;
                chkEUMLAcero.Enabled = Activar;
                txtALAcero.Enabled = Activar;
                txtEMAcero.Enabled = Activar;
                txtELAcero.Enabled = Activar;
                cboTUAcero.Enabled = Activar;
                cboTUAcero.Enabled = Activar;
                txtEPAcero.Enabled = Activar;
                txtEVAcero.Enabled = Activar;
                chkPlantaAcero.Enabled = Activar;
                chkCorteFachadaAcero.Enabled = Activar;
                chkAzotAcero.Enabled = Activar;
                chkUrbaAcero.Enabled = Activar;
                chkEstructuralAcero.Enabled = Activar;
                chklosaAcero.Enabled = Activar;
                chkMuroAcero.Enabled = Activar;
                chkLosaEscaleraAcero.Enabled = Activar;
                chkFosoAscensorAcero.Enabled = Activar;
                chkFosoEscaleraAcero.Enabled = Activar;
                cboFormaAcero.Enabled = Activar;
                chkJuntaDilataAcero.Enabled = Activar;
                txtEspJunAcero.Enabled = Activar;
                chkDesnAscAcero.Enabled = Activar;
                chkDesnDescAcero.Enabled = Activar;
                chkCulatsPerimAcero.Enabled = false;
                chkCulatasInternasAcero.Enabled = false;
                chkAntepechosAcero.Enabled = Activar;
                chkColumnasAcero.Enabled = Activar;
                chkEscMonAcero.Enabled = false;
                chkEscPostAcero.Enabled = false;
                chkBaseAcero.Enabled = Activar;
                chkLosaInclinadaAcero.Enabled = false;
                chkDomoAcero.Enabled = false;
                chkMPAcero.Enabled = Activar;
                chkNegAceroAce.Enabled = Activar;
                chkPretilesAcero.Enabled = Activar;
                chkGargolasAcero.Enabled = false;
                chkMFTAcero.Enabled = false;
                chkNegCarriolasAcero.Enabled = false;
                chkVEAcero.Enabled = false;
                chkFCMqAcero.Enabled = Activar;
                chkVigasAcero.Enabled = Activar;
                chkTorreonAcero.Enabled = false;
                chkRebordesAcero.Enabled = false;
                chkReservatoriosAcero.Enabled = false;
                chkDilFacAcero.Enabled = false;
                chkJCAIAcero.Enabled = false;
                chkJCAEcero.Enabled = false;
                chkCanesAcero.Enabled = false;
                chkPortAcero.Enabled = false;
                chkOtrosAcero.Enabled = Activar;
                cboTUAcero.Enabled = Activar;
                cboFormaAcero.Enabled = false;
                chkJuntaDilataAcero.Enabled = false;
                txtEspJunAcero.Text = "No Aplica";
                chkDesnAscAcero.Checked = false;
                chkDesnDescAcero.Checked = false;
                chkEscMonAcero.Checked = false;
                chkEscPostAcero.Checked = false;
                chkBaseAcero.Checked = false;
                chkLosaInclinadaAcero.Checked = false;
                chkDomoAcero.Checked = false;
                chkGargolasAcero.Checked = false;
                chkMFTAcero.Checked = false;
                chkNegCarriolasAcero.Checked = false;
                chkVEAcero.Checked = false;
                chkTorreonAcero.Checked = false;
                chkRebordesAcero.Checked = false;
                chkReservatoriosAcero.Checked = false;
                chkDilFacAcero.Checked = false;
                chkJCAIAcero.Checked = false;
                chkJCAEcero.Checked = false;
                chkCanesAcero.Checked = false;
                chkPortAcero.Checked = false;

            }
        }

        private void ActivarCamposSalidaCot(int grupoDet, bool Activar)
        {
            if (grupoDet == 1)
            {
                txtm2Alum.Enabled = Activar;
                txtValAlum.Enabled = Activar;
                txtValAccAlum.Enabled = Activar;
            }
            if (grupoDet == 2)
            {
                txtm2Plast.Enabled = Activar;
                txtValPlast.Enabled = Activar;
                txtValAccPlast.Enabled = Activar;
            }
            if (grupoDet == 3)
            {
                txtm2Acero.Enabled = Activar;
                txtValAcero.Enabled = Activar;
                txtValAccAcero.Enabled = Activar;
            }
        }

        protected void btnSubir_Click(object sender, EventArgs e)
        {
            AccorSubirArch.Visible = true;
            SetPane("AccorSubirArch");
        }

        protected void btnSubirList_Click(object sender, EventArgs e)
        {
            AccorSubirArch.Visible = true;
            SetPane("AcordSubirDoc");
            MostrarPlanos();
            Session["lugar"] = "Salida";
        }

        protected void btnRechazo_Click(object sender, EventArgs e)
        {
            AccorRechazo.Visible = true;
            SetPane("AccorRechazo");
        }

        protected void cboTipoCotizacion_SelectedIndexChanged(object sender, EventArgs e)
        {
            int rol = (int)Session["Rol"];

            //chkAluminio_CheckedChanged1( sender,  e);
            //chkPlastico_CheckedChanged1(sender, e);
            //chkAcero_CheckedChanged1(sender, e);

            chkAccesorios.Enabled = true;
            if (cboTipoCotizacion.SelectedItem.Value == "1") // Equipo nuevo
            {
                if (txtFUP.Text == "")
                {
                    chkRecotiza.Visible = false;
                    chkAccesorios.Checked = true;
                    chkAccesorios.Enabled = false;
                }
            }

            if (cboTipoCotizacion.SelectedItem.Value == "4" || cboTipoCotizacion.SelectedItem.Value == "5") // Renta o infraestructura
            {
                chkAluminio.Checked = false;
                chkPlastico.Checked = false;
                chkAcero.Checked = true;
                cboTipoAcero.SelectedValue = "3";
                chkAluminio.Enabled = false;
                chkPlastico.Enabled = false;
                chkAcero.Enabled = false;
            }
            else
            {
                chkAluminio.Checked = false;
                chkPlastico.Checked = false;
                chkAcero.Checked = false;
                chkAluminio.Enabled = true;
                chkPlastico.Enabled = true;
                chkAcero.Enabled = true;
            }

            if (cboTipoCotizacion.SelectedItem.Value == "3" || cboTipoCotizacion.SelectedItem.Value == "7") //Listado De Piezas o reparacion
            {
                if ((rol == 24) || (rol == 26) || (rol == 13))
                {
                    btnSubirListado.Visible = false;
                }
                else
                {
                    btnSubirListado.Visible = true;
                }
                AccorDetEspecif.Visible = false;
            }
            else
            {
                btnSubirListado.Visible = false;
                AccorDetEspecif.Visible = true;
            }

        }

        protected void cboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardar.Enabled = true;
            CargarDatosObra();            
        }

        protected void CargarDatosObra()
        {
            reader = contobra.ConsultarObra(Convert.ToInt32(cboObra.SelectedItem.Value));
            reader.Read();
            string PaisID = reader.GetValue(0).ToString();
            string CiudadID = reader.GetValue(2).ToString();
            string CiudadObra = reader.GetValue(3).ToString();
            //cboEstrato.Items.Clear();            
            //cboEstrato.Items.Add(new ListItem(reader.GetString(9), reader.GetInt32(8).ToString())); 
            cboEstrato.SelectedValue = reader.GetInt32(8).ToString();
            //cboVivienda.Items.Clear();
            //cboVivienda.Items.Add(new ListItem(reader.GetString(12), reader.GetString(12)));
            cboVivienda.SelectedItem.Text = reader.GetString(12).ToString();
            txtM2.Text = reader.GetValue(14).ToString();
            txtUnidades.Text = reader.GetValue(13).ToString();

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            Session["PaisIDObra"] = PaisID;
            Session["CiudadIDObra"] = CiudadID;
            Session["CiudadObra"] = CiudadObra;
        }

        protected void btnVB_Click(object sender, EventArgs e)
        {
            int RolId = (int)Session["Rol"];
            string Nombre = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            int tipoVentaProyecto = (int)Session["tipoVentaProyectoSes"];

            int ValRec = 0;
            reader = controlfup.ValidarRechazos(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    ValRec = reader.GetInt32(0);
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            bool numerico = IsInt(txtModulaciones.Text);


            if (txtModulaciones.Text == "" || txtModulaciones.Text == "0" || numerico == false)
            {
                mensaje = "Digite la cantidad de modulaciones!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtModulaciones.Text = "0";
            }
            else
            {
                if (ValRec != 0)
                {
                    MensajeVistoBuenoRec();
                }
                else
                {
                    //COLOCAMOS EL TEXTO EN MAYUSCULAS
                    txtAlcance.Text = txtAlcance.Text.ToUpperInvariant();
                    txtObserva.Text = txtObserva.Text.ToUpperInvariant();

                    int ActFUP = controlfup.ActFUP(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                        chkAccesorios.Checked, Convert.ToInt32(txtNumEquipos.Text), chkPlanoForsa.Checked, chkCotRapida.Checked,
                        txtAlcance.Text, txtObserva.Text, true, Convert.ToInt32(txtModulaciones.Text), Convert.ToInt32(cboClaseCot.SelectedItem.Value), 0, tipoVentaProyecto, RolId);

                    Session["Evento"] = 3;
                    CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                    MensajeVistoBueno();

                    ValidarFUP();
                    ValidacionGeneralFUP();
                }
            }
        }

        private void MensajeVistoBueno()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "VB ingresado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "VB successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "VB entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeVistoBuenoRec()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Existen Rechazos Sin Validar.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Rejects No Validated.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Não Rejeita No Validar.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void chkRecotiza_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRecotiza.Checked == true)
            {
                PoblarRecotizacion();
                pnlMotivo.Visible = true;
                pnlComRecotiza.Visible = true;
                btnGuardarRecotiza.Visible = true;
                btnNewRecot.Visible = true;
                btnRecotizar.Visible = true;
                btnGuardar.Visible = false;
                SetPane("AccRecotiza");
            }
            else
            {
                pnlMotivo.Visible = false;
                pnlComRecotiza.Visible = false;
                btnGuardarRecotiza.Visible = false;
                btnNewRecot.Visible = false;
                btnRecotizar.Visible = false;
                btnGuardar.Visible = true;
                chkAcero.Enabled = false;
                chkAluminio.Enabled = false;
                chkPlastico.Enabled = false;
                txtAlcance.Enabled = false;
                txtObserva.Enabled = false;
                txtNumEquipos.Enabled = false;
            }
        }

        protected void btnGuardarRechazo_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];

            if (cboTemaRechazo.SelectedItem.Value == "0" || txtObservaRechazo.Text == "")
            {
                string mensaje = "Digite o seleccione los datos del rechazo";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                //COLOCAMOS EL TEXTO EN MAYUSCULAS
                txtObservaRechazo.Text = txtObservaRechazo.Text.ToUpperInvariant();

                int ActFUP = controlfup.ActRECHA(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    Convert.ToInt32(cboTemaRechazo.SelectedItem.Value), txtObservaRechazo.Text);

                MensajeRechazo();
                CargarGrillaRechazo();

                ValidarFUP();
                ValidacionGeneralFUP();
                cargaDatosCambios();
            }
        }

        protected void btnGuardarRechCom_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string mensaje = "";

            if (cboTemaRechCom.SelectedItem.Value == "0" || txtObservRecCom.Text == "")
            {
                mensaje = "Digite seleccione los datos del rechazo";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {          
                 //COLOCAMOS EL TEXTO EN MAYUSCULAS
                txtObservRecCom.Text = txtObservRecCom.Text.ToUpperInvariant();

                int ActFUP = controlfup.ActRECHA(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    Convert.ToInt32(cboTemaRechCom.SelectedItem.Value), txtObservRecCom.Text);

                MensajeRechazo();
                CargarGrillaRechazoCom();

                Session["Evento"] = 14;
                CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                
                string idioma = (string)Session["Idioma"];
                mensaje = "Correo enviado con exito.";

                ValidarFUP();
                ValidacionGeneralFUP();
                cargaDatosCambios();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private void MensajeRechazo()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Rechazo ingresado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Rejection successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Rejeição entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarGrillaRechazo()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarRechazoGrilla(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (dsFUP != null)
            {
                gvRechazo.DataSource = dsFUP.Tables[0];
                gvRechazo.DataBind();
                gvRechazo.Visible = true;
            }
            else
            {
                gvRechazo.Dispose();
                gvRechazo.Visible = false;
            }
            dsFUP.Reset();
        }

        private void CargarValoresSF()
        {
            reader = controlfup.ConsultarValoresSf(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
        if (reader.Read() == true)
            {

                string CantSf = reader.GetValue(0).ToString();
                string vlrCotizado = reader.GetValue(1).ToString();
                string porDescto = reader.GetValue(2).ToString();
                string vlrDescto = reader.GetValue(3).ToString();
                string vlrComercial = reader.GetValue(4).ToString();
                string razonDescto = reader.GetValue(5).ToString();
                string usuario = reader.GetValue(6).ToString();

                Label29.Visible = true;
                Label31.Visible = true;
                lblCantSf.Visible = true;
                lblPorcDscto.Visible = true;
                lblValorSf.Visible = true;
                lblValorDscto.Visible = true;
                lblRazonDscto.Visible = true;
                lblUsuarioSf.Visible = true;

                lblCantSf.Text = CantSf + " sf ";
                lblValorSf.Text = colocaMiles(vlrComercial);
                lblPorcDscto.Text = colocaMiles(porDescto) + "  % ";
                lblValorDscto.Text = colocaMiles(vlrDescto);
                if (razonDescto != "") lblRazonDscto.Text = "Razon: " + razonDescto ; else lblRazonDscto.Text = "";
                lblUsuarioSf.Text = usuario;
            }
            else
            {
                Label29.Visible = false;
                Label31.Visible = false;
                lblCantSf.Visible = false;
                lblPorcDscto.Visible = false;
                lblValorSf.Visible = false;
                lblValorDscto.Visible = false;
                lblRazonDscto.Visible = false;
                lblUsuarioSf.Visible = false;
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            
        }



        private void CargarGrillaRechazoCom()
        {
            reader = controlfup.consultarRechazoComercial(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (reader.Read() == true)
            {
                chkRechComer.Checked = reader.GetBoolean(0);
                if (chkRechComer.Checked == true)
                {
                    lblcotizRechazada.Text = "Cotizacion Rechazada";
                    lblcotizRechazada.BackColor = Color.Yellow;
                    lblcotizRechazada.Visible = true;
                    this.MostrarRespuesta();
                    CargarComboRechazoResp();
                }
                else
                {
                    lblcotizRechazada.Visible = false;
                    this.OcultarRespuesta();
                }
                
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
           
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarRechazoGrillaCom(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (dsFUP != null)
            {
                gridRechCom.DataSource = dsFUP.Tables[0];
                gridRechCom.DataBind();
                gridRechCom.Visible = true;
            }
            else
            {
                gridRechCom.Dispose();
                gridRechCom.Visible = false;
            }
            dsFUP.Reset();
        }

        private void CargarComboRechazoResp()
        {
            cboTemarespRechazo.Items.Clear();
            reader = controlfup.poblarTemaRechazoCom(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            cboTemarespRechazo.Items.Add(new ListItem("Seleccione", "0"));
            while (reader.Read())
            {
                cboTemarespRechazo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        protected void gvRechazo_PageIndexChanged(object sender, EventArgs e)
        {
            //gvRechazo.CurrentPageIndex = e.NewPageIndex;
            //ds = contReclamos.consultarPncGuardadoGrilla(Convert.ToInt32(lqnumeroqueja.Text.ToString()), "n_Rol", "p_Rol");
            //gvRechazo.DataSource = ds.Tables[0];
            //gvRechazo.DataBind();
        }
        // Cierre Comercial
        private void ConsultarCierreCom()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_Cierre_Comercial", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter fecfir = new SqlParameter("@pfecha_firma_contrato", SqlDbType.VarChar, 50);
                    SqlParameter fecplano = new SqlParameter("@pfecha_planos_aprobados", SqlDbType.VarChar, 50);
                    SqlParameter plazo = new SqlParameter("@pplazo", SqlDbType.Int);
                    SqlParameter feccontr = new SqlParameter("@pfecha_contractual", SqlDbType.VarChar, 50);
                    SqlParameter m2cierre = new SqlParameter("@pm2_cerrados", SqlDbType.VarChar, 50);
                    SqlParameter tiempoase = new SqlParameter("@ptiempo_asetec", SqlDbType.Int);
                    SqlParameter fecantici = new SqlParameter("@pfecha_anticipado", SqlDbType.VarChar, 50);
                    SqlParameter piezas = new SqlParameter("@ppiezas", SqlDbType.Int);
                    SqlParameter pcomision = new SqlParameter("@pporc_com", SqlDbType.Int);
                    SqlParameter Vlrtotal = new SqlParameter("@pvalor_final", SqlDbType.Money);
                    SqlParameter alcance = new SqlParameter("@pAlcance_Final", SqlDbType.VarChar, -1);
                    SqlParameter CantOF = new SqlParameter("@pCantidadOF", SqlDbType.Int);
                    SqlParameter ValorSf = new SqlParameter("@pValorSf", SqlDbType.Money);

                    
                    fecfir.Direction = ParameterDirection.Output;
                    fecplano.Direction = ParameterDirection.Output;
                    plazo.Direction = ParameterDirection.Output;
                    feccontr.Direction = ParameterDirection.Output;
                    m2cierre.Direction = ParameterDirection.Output;
                    tiempoase.Direction = ParameterDirection.Output;
                    fecantici.Direction = ParameterDirection.Output;
                    piezas.Direction = ParameterDirection.Output;

                    // pcomision son los dias disponibles de servicio tecnico
                    pcomision.Direction = ParameterDirection.Output;
                    Vlrtotal.Direction = ParameterDirection.Output;
                    alcance.Direction = ParameterDirection.Output;
                    CantOF.Direction = ParameterDirection.Output;
                    ValorSf.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(fecfir);
                    cmd.Parameters.Add(fecplano);
                    cmd.Parameters.Add(plazo);
                    cmd.Parameters.Add(feccontr);
                    cmd.Parameters.Add(m2cierre);
                    cmd.Parameters.Add(tiempoase);
                    cmd.Parameters.Add(fecantici);
                    cmd.Parameters.Add(piezas);
                    cmd.Parameters.Add(pcomision);
                    cmd.Parameters.Add(Vlrtotal);
                    cmd.Parameters.Add(alcance);
                    cmd.Parameters.Add(CantOF);
                    cmd.Parameters.Add(ValorSf);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DE CIERRE
                        string porcom = Convert.ToString(pcomision.Value);
                        string fec_firma = Convert.ToString(fecfir.Value);
                        string fec_plano = Convert.ToString(fecplano.Value);
                        string fec_contr = Convert.ToString(feccontr.Value);
                        string fec_antip = Convert.ToString(fecantici.Value);
                        string Valor_cier = Convert.ToString(Vlrtotal.Value);
                        string AlcanceCier = Convert.ToString(alcance.Value);
                        string PiezaCier = Convert.ToString(piezas.Value);
                        string TiempoCier = Convert.ToString(tiempoase.Value);
                        string m2Cier = Convert.ToString(m2cierre.Value);
                        string plazoCier = Convert.ToString(plazo.Value);
                        int QuantOF = Convert.ToInt32(CantOF.Value);
                        string ValorTSf = Convert.ToString(ValorSf.Value);

                        //ASIGNAMOS LOS VALORES A LOS CAMPOS   
                        lblVlrTC.Text = IsNumeric(Valor_cier);
                        LM2Cerrados.Text = IsNumeric(m2Cier);
                        txtTPiezas.Text = PiezaCier;
                        lblDiasDisponibles.Text = TiempoCier;
                        txtFContractual.Text = fec_contr;
                        txtFContrato.Text = fec_firma;
                        txtFPago.Text = fec_antip;
                        txtPAprob.Text = fec_plano;
                        lblDiasConsu.Text = porcom;
                        txtAlcanceCierre.Text = AlcanceCier;
                        txtPlazo.Text = plazoCier;
                        txtCantOF.Text = Convert.ToString(QuantOF);
                        lblSf.Text = colocaMiles(ValorTSf);

                        if (QuantOF == 0)
                        {
                            AccorOF.Visible = false;
                        }
                        else
                        {
                            AccorOF.Visible = true;
                        }
                    }
                    con.Close();
                }
            }
        }

        private void ParametrosSolicitudFacturacion()
        {
            //VARIABLES DE SESION SOLICITUD DE FACTURACIÓN
            string Tipo = "", DescTipo = "";
            if ((chkAluminio.Checked == true) && (chkAcero.Checked == false)
                && (chkPlastico.Checked == false))
            {
                Tipo = "OF";
                DescTipo = "ORDEN DE FABRICACIÓN";
            }

            if ((chkAluminio.Checked == false) && (chkAcero.Checked == true)
                && (chkPlastico.Checked == false))
            {
                Tipo = "FA";
                DescTipo = "FORMALETA ACERO";
            }

            if ((chkAluminio.Checked == false) && (chkAcero.Checked == false)
               && (chkPlastico.Checked == true))
            {
                Tipo = "FP";
                DescTipo = "FORMALETA PLASTICA";
            }

            if (((chkAluminio.Checked == true) && (chkAcero.Checked == true))
                || ((chkAluminio.Checked == true) && (chkPlastico.Checked == true))
                || ((chkPlastico.Checked == true) && (chkAcero.Checked == true)))
            {
                Tipo = "FH";
                DescTipo = "FORMALETA HIBRIDA";
            }

            Session["CLIENTE"] = cboCliente.SelectedItem.Text;
            Session["OBRA"] = cboObra.SelectedItem.Text;
            Session["FUP"] = txtFUP.Text.Trim();
            Session["VER"] = cboVersion.SelectedItem.Text.Trim();
            Session["TIPO"] = Tipo;
            Session["PROD"] = cboProducido.SelectedItem.Text.Trim();
            Session["Pais"] = cboPais.SelectedValue;
            Session["Ciudad"] = cboCiudad.SelectedValue;
            Session["Bandera"] = "1";
            Session["DescTipo"] = DescTipo;

            if (cboPais.SelectedValue == "8")
            {
                Session["MONEDA"] = "COP";
            }
            else
            {
                Session["MONEDA"] = "USD";
            }

        }

        //ACCIONES CIERRE COMERCIAL
        protected void btnGuardaCierre_Click(object sender, EventArgs e)
        {
            int valido = 1;

            int rol = (int)Session["Rol"];

            if ((rol == 24) || (rol == 26) || (rol == 13))
            {
                valido = 1;
            }
            else
            {
                if ((txtFContrato.Text == "") || (txtFPago.Text == "")
                    || (txtPAprob.Text == "") || (txtFContractual.Text == ""))
                {
                    MensajeAlerta();
                    valido = 0;
                }

            }

            if (valido == 1)
            {

                string Nombre = (string)Session["Nombre_Usuario"];
                string Param = "FUP";

                int ActFUP = controlfup.ActCIERRE(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                            txtFContrato.Text, txtPAprob.Text, Convert.ToInt32(txtPlazo.Text.Replace(",", "")),
                            txtFContractual.Text, Convert.ToInt32(lblDiasDisponibles.Text), txtFPago.Text,
                            Convert.ToInt32(txtTPiezas.Text.Replace(",", "")), lblDiasConsu.Text, txtAlcanceCierre.Text,
                            Convert.ToInt32(txtCantOF.Text.Replace(",", "")));

                int IngSF = controlfup.IngSF(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    Param);

                Session["Evento"] = 8;
                CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                MensajeCierreComercial();

                ValidarFUP();
                ValidacionGeneralFUP();
            }
            cargaDatosCambios();
        }

        private void ValidarFechasCierre()
        {
            string msjCierre = "";

            DateTime firmacontrato, planosaprobados, legalizacionpago, fechaIni, fechacontra;
            TimeSpan FechaFin;

            firmacontrato = Convert.ToDateTime(txtFContrato.Text);
            planosaprobados = Convert.ToDateTime(txtPAprob.Text);
            legalizacionpago = Convert.ToDateTime(txtFPago.Text);
            fechacontra = Convert.ToDateTime(txtFContractual.Text);

            if (fechacontra < firmacontrato)
            {
                msjCierre = "La fecha contractual no puede ser menor que la firma del contrato. Verifique.";

            }

            if (fechacontra < planosaprobados)
            {
                msjCierre = "La fecha contractual no puede ser menor que los planos aprobados. Verifique.";
            }

            if (fechacontra < legalizacionpago)
            {
                msjCierre = "La fecha contractual no puede ser menor que la formalizacion del pago. Verifique.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msjCierre + "')", true);


            if (txtPlazo.Text == "0")
            {
                //NUEVO METODO PARA EL CALCULO DE DIAS DE PLAZO
                fechaIni = Convert.ToDateTime(txtFContractual.Text);
                FechaFin = System.DateTime.Now.TimeOfDay;

                if (firmacontrato < planosaprobados && firmacontrato < legalizacionpago)
                {
                    FechaFin = fechaIni.Subtract(firmacontrato);
                }
                else if (planosaprobados < firmacontrato && planosaprobados < legalizacionpago)
                {
                    FechaFin = fechaIni.Subtract(planosaprobados);
                }
                else if (legalizacionpago < firmacontrato && legalizacionpago < planosaprobados)
                {
                    FechaFin = fechaIni.Subtract(legalizacionpago);
                }
                else if (legalizacionpago == firmacontrato && legalizacionpago == planosaprobados && firmacontrato == planosaprobados)
                {
                    FechaFin = fechaIni.Subtract(planosaprobados);
                }
                int dias = FechaFin.Days;
                txtPlazo.Text = Convert.ToString(dias);
            }
            else
            {
                txtPlazo.Text = txtPlazo.Text;
            }
        }

        private void MensajeCierreComercial()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Cierre comercial ingresado con éxito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Close commercial successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Feche o comércio entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }


        protected void btnGuardarPlanoForsa_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];

            //COLOCAMOS EL TEXTO EN MAYUSCULAS
            txtObservaPlano.Text = txtObservaPlano.Text.ToUpperInvariant();

            if ((cboResponsable.SelectedItem.Value == "Seleccione") || (cboResponsable.SelectedItem.Value == "Select")
                || (cboResponsable.SelectedItem.Value == "Selecione") || (cboEvento.SelectedItem.Value == "Seleccione")
                || (cboEvento.SelectedItem.Value == "Select") || (cboEvento.SelectedItem.Value == "Selecione")
                || (txtFecSalida.Text == ""))
            {
                MensajeAlerta();
            }
            else
            {
                int ActFUP = controlfup.ActPLANOFORSA(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    Convert.ToInt64(cboResponsable.SelectedItem.Value), Convert.ToInt32(cboEvento.SelectedItem.Value),
                    txtObservaPlano.Text, chkEnvPlano.Checked, txtFecSalida.Text);
            }
            Session["Evento"] = 7;
            CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
            MensajePlanoForsa();
            CargarGrillaPlanoForsa();

            ValidarFUP();
            ValidacionGeneralFUP();
            cargaDatosCambios();
        }

        private void MensajePlanoForsa()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Plano Forsa ingresado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Plane Forsa successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Avião Forsa entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarGrillaPlanoForsa()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarPlanoForsa(Convert.ToInt32(txtFUP.Text));
            if (dsFUP != null)
            {
                gvTipoForsa.DataSource = dsFUP.Tables[0];
                gvTipoForsa.DataBind();
                gvTipoForsa.Visible = true;
            }
            else
            {
                gvTipoForsa.Dispose();
                gvTipoForsa.Visible = false;
            }
            dsFUP.Reset();
        }

        protected void btnNuevoTipoForsa_Click(object sender, EventArgs e)
        {
            PoblarResponsable();
            PoblarEvento();
            txtObservaPlano.Text = "";
            txtFecSalida.Text = "";
            chkEnvPlano.Checked = false;
        }

        protected void txtOF_TextChanged(object sender, EventArgs e)
        {
            if (cboProducido.SelectedItem.Text == "Colombia")
            {
                reader = controlfup.consultarFUPOrdenFabricacionColombia(txtOF.Text);
                if (reader.Read() == false)
                {
                    string mensaje = "";
                    string idioma = (string)Session["Idioma"];

                    if (idioma == "Español")
                    {
                        mensaje = "El número de orden de fabricación ingresado no existe.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "The production order number entered does not exist.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "O número de ordem de produção entrou não existe.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    txtFUP.Text = reader.GetValue(0).ToString();
                    //POBLAMOS LAS VERSIONES
                    ValidarFUP();
                    ValidacionGeneralFUP();
                    llenarComboOrdenes();
                    PoblarProducidoEn();
                }
                reader.Close();
                reader.Dispose();
                BdDatos.desconectar();
            }
            CargarGrillaOF();
        }

        protected void btnSubirPlano_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string directorio = @"I:\Planos\" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string dirweb = @"~/Planos/" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"/";
            //string directorio = @"C:\Anexos_" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string mensaje = "";

            if (cboTipoAnexo.SelectedItem.Value == "0")
            {
                mensaje = "Seleccione El Tipo de Anexo que Desea Subir!!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (!(Directory.Exists(directorio)))
                {
                    Directory.CreateDirectory(directorio);
                }

                if (cboTipoAnexo.SelectedValue == "5") //Planos Tipo Forsa
                {
                    directorio = directorio + @"PTF\";
                    dirweb = dirweb + @"PTF/";
                    if (!(Directory.Exists(directorio)))
                    {
                        Directory.CreateDirectory(directorio);
                    }
                }
                else
                {
                    if (cboTipoAnexo.SelectedValue == "6") //Carpeta carta cotizacion
                    {
                        directorio = directorio + @"Carta_Cotizacion\";
                        dirweb = dirweb + @"Carta_Cotizacion/";

                        if (!(Directory.Exists(directorio)))
                        {
                            Directory.CreateDirectory(directorio);
                        }
                    }
                }  
              
                FDocument.Enabled = true;
                HttpPostedFile postedFile = FDocument.PostedFile;
                string fileName = System.IO.Path.GetFileName(FDocument.FileName);
                
                //string fileName = Path.GetFileName(postedFile.FileName);

                if (fileName != "")
                {
                    int tamaño = FDocument.PostedFile.ContentLength;

                    if (FDocument.HasFile)
                    {
                        if (FDocument.PostedFile.ContentLength > 100485760)
                        {
                            //Archivo.Text = "Tamaño Maximo del Archivo 10 MB.";
                            mensaje = "El Archivo Pesa: " + tamaño / 1000000 + " Megas, Supera los 100 Megas Permitidos!!";

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                        else
                        {
                            string rutaArchivo = directorio + @"\" + fileName;

                            int counter = 1;
                            string nombreArchivoTemp = "";
                            while (File.Exists(rutaArchivo))
                            {
                                // if a file with this name already exists,
                                // prefix the filename with a number.
                                nombreArchivoTemp = "(" + counter.ToString() + ")" + fileName;
                                rutaArchivo = directorio + nombreArchivoTemp;
                                counter++;
                            }

                            if (counter > 1) fileName = nombreArchivoTemp;

                            postedFile.SaveAs(rutaArchivo);

                            if (File.Exists(rutaArchivo))
                            {
                                int IngDOCPLAN = controlfup.IngDOCPLAN(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                                fileName, dirweb, Convert.ToInt32(cboTipoAnexo.SelectedItem.Value), Convert.ToInt32(cboTipoProy.SelectedItem.Value),
                                txtOFRef.Text);

                                CargarGrillaArchivoForsa();

                                PoblarTipoAnexo();
                                AcorInfoGeneral.Visible = true;
                                AccorDetEspecif.Visible = true;
                                AccorSubirArch.Visible = true;
                                AccorSalidaCot.Visible = true;
                                AccorRechazo.Visible = true;
                                AccorSegPFT.Visible = true;
                                AccorCierre.Visible = true;
                                AccorOF.Visible = true;
                                OcultarPlanos();

                                ValidarFUP();
                                ValidacionGeneralFUP();
                                mensaje = "Se Subio Exitosamente el Archivo: " + fileName;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                                if (Session["Lugar"] == "Salida")
                                {
                                    SetPane("AccorSalidaCot");
                                    rutaCartaCotizacion();
                                }
                            }
                            else
                            {
                                mensaje = "No subio el archivo: " + nombreArchivoTemp + " Verifique!! ";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                    }
                    else
                    {
                        mensaje = "Seleccione el Archivo a Subir!!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                }
                else
                {
                    mensaje = "Archivo no seleccionado.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        private void OcultarPlanos()
        {
            lblTitOFTec.Visible = false;
            txtOFRef.Visible = false;
            lblDetOF1.Visible = false;
            lblDetOF2.Visible = false;
            lblTipoAnexo.Visible = false;
            lblTipoProyecto.Visible = false;
            cboTipoAnexo.Visible = false;
            cboTipoProy.Visible = false;
            FDocument.Visible = false;
            lblArchivo.Visible = false;
            btnSubirPlano.Visible = false;
            btnCancelar.Visible = false;
        }

        private void MostrarPlanos()
        {
            lblTitOFTec.Visible = true;
            txtOFRef.Visible = true;
            lblDetOF1.Visible = true;
            lblDetOF2.Visible = true;
            lblTipoAnexo.Visible = true;
            //lblTipoProyecto.Visible = true;
            cboTipoAnexo.Visible = true;
            //cboTipoProy.Visible = true;
            FDocument.Visible = true;
            lblArchivo.Visible = true;
            btnSubirPlano.Visible = true;
            btnCancelar.Visible = true;
        }

        private void MensajeArchivoForsa()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Archivo ingresado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Archive successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Arquivo entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarGrillaArchivoForsa()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarArchivoForsa(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (dsFUP != null)
            {
                grvArchivo.DataSource = dsFUP.Tables[0];
                grvArchivo.DataBind();
                grvArchivo.Visible = true;
            }
            else
            {
                grvArchivo.Dispose();
                grvArchivo.Visible = false;
            }
            dsFUP.Reset();
        }

        //private void CargarGrillaCarta()
        //{
        //    dsFUP.Reset();
        //    dsFUP = controlfup.ConsultarCartas(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
        //    if (dsFUP != null)
        //    {
        //        gridCartas.DataSource = dsFUP.Tables[0];
        //        gridCartas.DataBind();
        //        gridCartas.Visible = true;
        //    }
        //    else
        //    {
        //        gridCartas.Dispose();
        //        gridCartas.Visible = false;
        //    }
        //    dsFUP.Reset();
        //}

        protected void anexo_OnClick(object sender, EventArgs e)
        {
            LinkButton link = (LinkButton)sender;
            string URL = link.Text;
            URL = Page.ResolveClientUrl(URL);
            link.OnClientClick = "window.open('" + URL + "'); return false;";
            //Response.Redirect(link.Text);
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];
            string mensaje = "";

            if (cboProdOF.SelectedItem.Value == "0" || cboParte.SelectedItem.Value == "0")
            {
                 mensaje = "Debe seleccionar la Planta y la Parte";           
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else 
            {
                int ActOFS = controlfup.ActOFS(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                    cboProdOF.SelectedItem.Text.Trim(), Convert.ToInt32(cboParte.SelectedItem.Text.Trim()),Convert.ToInt32(cboParte.SelectedItem.Value));

                Session["Evento"] = 10;
                Session["Parte"] = cboParte.SelectedItem.Text.Trim();
                CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                MensajeCrearOF();                

                ValidarFUP();
                ValidacionGeneralFUP();
                this.PoblarProducidoEn();
                CargarGrillaOF();
                lblm2Parte.Text = "";
                lblPrecioParte.Text = "";
            }
           
        }
        private void MensajeCrearOF()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Orden Creada Exitosamente";
            }
            if (idioma == "Ingles")
            {
                mensaje = "OF successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "OF entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarGrillaOF()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarOFs(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (dsFUP != null)
            {
                grvOF.DataSource = dsFUP.Tables[0];
                grvOF.DataBind();
                grvOF.Visible = true;
                //chkRecotiza.Visible = false;
            }
            else
            {
                grvOF.Dispose();
                grvOF.Visible = false;
                //chkRecotiza.Visible = true;
            }
            dsFUP.Reset();
        }

        protected void chkPlanoForsa_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlanoForsa.Checked == true)
            {
                AccorSegPFT.Visible = true;
                Session["Evento"] = 7;
                PoblarTipoAnexo(1);
                btnGuardar.Visible = true;
            }
            else
            {
                PoblarTipoAnexo();
                if (LEstado.Text == "Cotizado")
                {
                    btnGuardar.Visible = false;
                    lkSubirPlanosDoc.Visible = false;
                }
            }
        }
        protected void chkCotRapida_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlanoForsa.Checked == true)
            {
                Session["Evento"] = 6;
            }
            ValidacionGeneralFUP();
        }

        protected void lkSubirPlanosDoc_Click(object sender, EventArgs e)
        {
            AcorInfoGeneral.Visible = false;
            AccorDetEspecif.Visible = false;
            AccorSubirArch.Visible = false;
            AccorSalidaCot.Visible = false;
            AccorRechazo.Visible = false;
            AccorSegPFT.Visible = false;
            AccorCierre.Visible = false;
            AccorOF.Visible = false;
            AccFUP.Visible = false;
            AccRecotiza.Visible = false;
            SetPane("AcordSubirDoc");
            MostrarPlanos();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            AcorInfoGeneral.Visible = true;
            AccorDetEspecif.Visible = true;
            AccorSubirArch.Visible = true;
            AccorSalidaCot.Visible = true;
            AccorRechazo.Visible = true;
            AccorSegPFT.Visible = true;
            AccorCierre.Visible = true;
            AccorOF.Visible = true;
            AccFUP.Visible = true;
            AccRecotiza.Visible = true;
            OcultarPlanos();

            ValidacionGeneralFUP();
            if (Session["Lugar"] == "Salida") SetPane("AccorSalidaCot");
        }

        public string IsNumeric(string valor, bool msg = true)
        {

            decimal result;
            bool valido;

            valido = decimal.TryParse(valor, out result);
            if (valor == "" || valido == false)
            {
                if (msg == true)
                { MensajeNumerico(); }
                result = 0;
            }

            return result.ToString("#,###.##");

        }

        public string colocaMiles(string valor, bool msg = true)
        {

            decimal result;
            bool valido;

            valido = decimal.TryParse(valor, out result);
            if (valor == "" || valido == false)
            {
                if (msg == true)
                {
                    valido = false;
                }
                result = 0;
            }

            return result.ToString("#,###.##");

        }

        public void TotalSF(int tipo)
        {
            decimal total;
            string vAlum, vAcero, vPlast, vAlumAcc, vAceroAcc, vPlastAcc;

            total = 0;

            if (tipo == 1)
            {
                vAlum = txtm2Alum.Text.Replace(",", "");
                vAcero = txtm2Acero.Text.Replace(",", "");
                vPlast = txtm2Plast.Text.Replace(",", "");
                vAlumAcc = "0"; vAceroAcc = "0"; vPlastAcc = "0";
            }
            else
            {
                vAlum = txtValAlum.Text.Replace(",", "");
                vAcero = txtValAcero.Text.Replace(",", "");
                vPlast = txtValPlast.Text.Replace(",", "");
                vAlumAcc = txtValAccAlum.Text.Replace(",", "");
                vAceroAcc = txtValAccAcero.Text.Replace(",", "");
                vPlastAcc = txtValAccPlast.Text.Replace(",", "");
            }
            if (vAlum.Trim().Length == 0) vAlum = "0";
            if (vAcero.Trim().Length == 0) vAcero = "0";
            if (vPlast.Trim().Length == 0) vPlast = "0";
            if (vAlumAcc.Trim().Length == 0) vAlumAcc = "0";
            if (vAceroAcc.Trim().Length == 0) vAceroAcc = "0";
            if (vPlastAcc.Trim().Length == 0) vPlastAcc = "0";

            total = Convert.ToDecimal(vAlum) + Convert.ToDecimal(vAcero) + Convert.ToDecimal(vPlast) +
                    Convert.ToDecimal(vAlumAcc) + Convert.ToDecimal(vAceroAcc) + Convert.ToDecimal(vPlastAcc);

            if (tipo == 1)
                txtTotalm2.Text = IsNumeric(Convert.ToString(total), false);
            else
                txtTotalValor.Text = IsNumeric(Convert.ToString(total), false);

            if (txtValAlum.Text == "") txtValAlum.Text = "0";
            if (txtValAcero.Text == "") txtValAcero.Text = "0";
            if (txtValPlast.Text == "") txtValPlast.Text = "0";
            if (txtValAccAlum.Text == "") txtValAccAlum.Text = "0";
            if (txtValAccAcero.Text == "") txtValAccAcero.Text = "0";
            if (txtValAccPlast.Text == "") txtValAccPlast.Text = "0";

            if (txtm2Alum.Text == "") txtm2Alum.Text = "0";
            if (txtm2Acero.Text == "") txtm2Acero.Text = "0";
            if (txtm2Plast.Text == "") txtm2Plast.Text = "0";
            if (txtTotalm2.Text == "") txtTotalm2.Text = "0";
            if (txtTotalValor.Text == "") txtTotalValor.Text = "0";
        }


        protected void txtm2Alum_TextChanged(object sender, EventArgs e)
        {
            txtm2Alum.Text = IsNumeric(txtm2Alum.Text);
            TotalSF(1);
        }

        private void MensajeNumerico()
        {
            string mensaje;
            mensaje = "Digite un valor numerico correctamente.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void txtValAlum_TextChanged(object sender, EventArgs e)
        {
            txtValAlum.Text = IsNumeric(txtValAlum.Text);
            TotalSF(2);
        }

        protected void txtValAccAlum_TextChanged(object sender, EventArgs e)
        {
            txtValAccAlum.Text = IsNumeric(txtValAccAlum.Text);
            TotalSF(2);
        }

        protected void txtm2Plast_TextChanged(object sender, EventArgs e)
        {
            txtm2Plast.Text = IsNumeric(txtm2Plast.Text);
            TotalSF(1);
        }

        protected void txtValPlast_TextChanged(object sender, EventArgs e)
        {
            txtValPlast.Text = IsNumeric(txtValPlast.Text);
            TotalSF(2);
        }

        protected void txtValAccPlast_TextChanged(object sender, EventArgs e)
        {
            txtValAccPlast.Text = IsNumeric(txtValAccPlast.Text);
            TotalSF(2);
        }

        protected void txtm2Acero_TextChanged(object sender, EventArgs e)
        {
            txtm2Acero.Text = IsNumeric(txtm2Acero.Text);
            TotalSF(1);
        }

        protected void txtValAcero_TextChanged(object sender, EventArgs e)
        {
            txtValAcero.Text = IsNumeric(txtValAcero.Text);
            TotalSF(2);
        }

        protected void txtValAccAcero_TextChanged(object sender, EventArgs e)
        {
            txtValAccAcero.Text = IsNumeric(txtValAccAcero.Text);
            TotalSF(2);
        }


        protected void txtCantOF_TextChanged(object sender, EventArgs e)
        {
            txtCantOF.Text = IsNumeric(txtCantOF.Text);
        }

        protected void txtPlazo_TextChanged(object sender, EventArgs e)
        {
            txtPlazo.Text = IsNumeric(txtPlazo.Text);
        }

        protected void txtTPiezas_TextChanged(object sender, EventArgs e)
        {
            txtTPiezas.Text = IsNumeric(txtTPiezas.Text);
        }

        protected void txtTAsesoria_TextChanged(object sender, EventArgs e)
        {
            lblDiasDisponibles.Text = IsNumeric(lblDiasDisponibles.Text);
        }

        private void CargarReporteFUP()
        {
            int rol = (int)Session["Rol"];
            AccFUP.Visible = true;
            ReportViewer1.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", txtFUP.Text.Trim(), true));
            parametro.Add(new ReportParameter("version", cboVersion.SelectedItem.Text.Trim(), true));

            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            if (rol != 4)
                ReportViewer1.ServerReport.ReportPath = "/InformesFUP/COM_InicioDatosFupGeneral";
            else
                ReportViewer1.ServerReport.ReportPath = "/InformesFUP/COM_InicioDatosFupGral_Ing";
            //ReportViewer1.ServerReport.ReportPath = "/InformesFUP_Capacita/COM_InicioDatosFupGeneral";
            this.ReportViewer1.ServerReport.SetParameters(parametro);
            ReportViewer1.ShowToolBar = true;
        }

        protected void txtNumEquipos_TextChanged(object sender, EventArgs e)
        {
            txtNumEquipos.Text = IsNumeric(txtNumEquipos.Text);
        }

        protected void btnSubirListado_Click(object sender, EventArgs e)
        {
            AccorSubirArch.Visible = true;
            SetPane("AccorSubirArch");
        }

        protected void btnGuardarRecotiza_Click(object sender, EventArgs e)
        {
            string Nombre = (string)Session["Nombre_Usuario"];

            //COLOCAMOS EL TEXTO EN MAYUSCULAS
            txtComRec.Text = txtComRec.Text.ToUpperInvariant();

            int ActFUP = controlfup.ActRECOTIZA(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
                Convert.ToInt32(cboRecotizacion.SelectedItem.Value), txtComRec.Text, Convert.ToInt32(cboClaseCot.SelectedItem.Value));

            MensajeRecotizacion();
            CargarGrillaRecotizacion();
            PoblarRecotizacion();
            txtComRec.Text = "";
            cargaDatosCambios();
        }

        protected void btnNewRecot_Click(object sender, EventArgs e)
        {
            txtComRec.Text = "";
            PoblarRecotizacion();
        }

        private void MensajeRecotizacion()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Recotización ingresada con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Requote successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Repactuação entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarGrillaRecotizacion()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarRecotizaGrilla(Convert.ToInt32(txtFUP.Text));
            if (dsFUP != null)
            {
                grvRecotiza.DataSource = dsFUP.Tables[0];
                grvRecotiza.DataBind();
                grvRecotiza.Visible = true;
            }
            else
            {
                grvRecotiza.Dispose();
                grvRecotiza.Visible = false;
            }
            dsFUP.Reset();
        }

        public void CorreoFUP(int fup,string version, int evento)
        {
            string sfId= "0";
            int Evento = (int)Session["Evento"];
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            int parte = 0;

            if (Session["Parte"] == null)
                parte = 0;
            else
                parte = Convert.ToInt32(Session["Parte"]);
            
            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[6];
            sqls[0] = new SqlParameter("@pFupID ", fup);
            sqls[1] = new SqlParameter("@pVersion", version);
            sqls[2] = new SqlParameter("@pEvento", evento);
            sqls[3] = new SqlParameter("@pUsuario", UsuarioAsunto);
            sqls[4] = new SqlParameter("@pRemitente", CorreoUsuario);
            sqls[5] = new SqlParameter("@pParte", parte);

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_notificaciones", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
                    SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
                    SqlParameter LinkAnexo = new SqlParameter("@pLink_anexo", SqlDbType.VarChar, 250);

                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;
                    Anexo.Direction = ParameterDirection.Output;
                    LinkAnexo.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);
                    cmd.Parameters.Add(Anexo);
                    cmd.Parameters.Add(LinkAnexo);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        string MensajeMail = Convert.ToString(Mensaje.Value);
                        bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
                        string EnlaceAnexo = Convert.ToString(LinkAnexo.Value);
                        string tipoAdjunto = "";
                        string enlaceCarta = "";
                        string nombreCarta = "";
                        
                        Byte[] correo = new Byte[0];
                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress(correoSistema);


                        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                        mail.To.Add(DestinatariosMail);
                        //mail.To.Add("ivanvidal@forsa.net.co");


                        //INCLUIMOS EL ASUNTO DEL MENSAJE
                        mail.Subject = AsuntoMail;
                        //AÑADIMOS EL CUERPO DEL MENSAJE
                        mail.Body = MensajeMail;
                        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                        mail.Priority = System.Net.Mail.MailPriority.Normal;
                        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                        mail.IsBodyHtml = true;
                        //ADJUNTAMOS EL ARCHIVO
                        MemoryStream ms = new MemoryStream();
                        if (llevaAnexo == true)
                        {
                            string enlace = "";

                            if (Evento == 13)
                            {
                                tipoAdjunto = "FUP";
                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&idOfa=" + cboComboOrden.SelectedItem.Value.ToString() ;
                            }
                            else
                            {
                                if ((Evento == 2) || (Evento == 4) || (Evento == 5) || (Evento == 7))
                                {
                                    tipoAdjunto = "FUP";
                                    enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + txtFUP.Text.Trim() + "" +
                                            "&version=" + cboVersion.SelectedItem.Text.Trim();
                                }
                                else
                                {
                                    if (Evento == 9 || Evento == 23 || Evento == 24 || Evento == 25)
                                    {
                                        tipoAdjunto = "SF";
                                        parte = Convert.ToInt32(Session["Parte"]);
                                        sfId = Session["SfId"].ToString();

                                        enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
                                         "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                                    }
                                    else
                                    {
                                        if (Evento == 16 || Evento == 15) 
                                        {
                                            tipoAdjunto = "SF";
                                            parte = Convert.ToInt32(Session["Parte"]);
                                            sfId = Session["SfId"].ToString();

                                            enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
                                             "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                                        }
                                    }
                                }
                            }
                            correo = clienteWeb.DownloadData(enlace);
                            ms = new MemoryStream(correo);
                            mail.Attachments.Add(new Attachment(ms, tipoAdjunto+" " + fup.ToString() + version + ".pdf"));
                            
                            // adjunto el archivo de la carta directamente desde la carpeta de planos
                            if (evento == 5)
                            {
                                controlfup.actualizarSalidaCotizacion(fup, version, Nombre);
                                //nombreCarta = Session["nombreCarta"].ToString();
                                //enlaceCarta = Session["rutaCarta"].ToString(); 
                                //tipoAdjunto = "CartaCotizacion";
                                //correo = clienteWeb.DownloadData(enlaceCarta);
                                //ms = new MemoryStream(correo);
                                //mail.Attachments.Add(new Attachment(ms, tipoAdjunto + fup.ToString() + version  +"_" + nombreCarta));
                                
                            }
                        }
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        //smtp.Timeout = 400;

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                            {
                                return true;
                            };
                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        try
                        {
                           // smtp.SendAsync(mail, mail.To);
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = "ERROR: " + ex.Message;
                        }
                        ms.Close();
                    }
                }
            }
        }

        public void CorreoGeneral( int evento)
        {
            string sfId = "0";
            int Evento = (int)Session["Evento"];
            string Nombre = (string)Session["Nombre_Usuario"];
            string CorreoUsuario = (string)Session["rcEmail"];
            int parte = 0;
            string FechaIni = (string)Session["fechaIni"]  ;
            string FechaFin = (string) Session["fechaFin"] ;
            string Planta = (string)Session["planta"];

            string correoSistema = (string)Session["CorreoSistema"];
            string UsuarioAsunto = (string)Session["UsuarioAsunto"];

            if (FechaIni == "" || FechaFin == "")
            {
            }
            else
            {

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3]; 
            sqls[0] = new SqlParameter("@pEvento", evento);
            sqls[1] = new SqlParameter("@pUsuario", UsuarioAsunto);
            sqls[2] = new SqlParameter("@pRemitente", CorreoUsuario); 

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_notificaciones_general", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter Asunto = new SqlParameter("@pAsun_mail", SqlDbType.VarChar, 200);
                    SqlParameter Destinatarios = new SqlParameter("@pLista", SqlDbType.VarChar, 12500);
                    SqlParameter Mensaje = new SqlParameter("@pMsg", SqlDbType.VarChar, 12500);
                    SqlParameter Anexo = new SqlParameter("@pAnexo", SqlDbType.Bit);
                    SqlParameter LinkAnexo = new SqlParameter("@pLink_anexo", SqlDbType.VarChar, 250);

                    Asunto.Direction = ParameterDirection.Output;
                    Destinatarios.Direction = ParameterDirection.Output;
                    Mensaje.Direction = ParameterDirection.Output;
                    Anexo.Direction = ParameterDirection.Output;
                    LinkAnexo.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(Asunto);
                    cmd.Parameters.Add(Destinatarios);
                    cmd.Parameters.Add(Mensaje);
                    cmd.Parameters.Add(Anexo);
                    cmd.Parameters.Add(LinkAnexo);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string AsuntoMail = Convert.ToString(Asunto.Value);
                        string DestinatariosMail = Convert.ToString(Destinatarios.Value);
                        string MensajeMail = Convert.ToString(Mensaje.Value);
                        bool llevaAnexo = Convert.ToBoolean(Anexo.Value);
                        string EnlaceAnexo = Convert.ToString(LinkAnexo.Value);
                        string tipoAdjunto = "";
                        string enlaceCarta = "";
                        string nombreCarta = "";

                        Byte[] correo = new Byte[0];
                        WebClient clienteWeb = new WebClient();
                        clienteWeb.Dispose();
                        clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
                        // Adjunto
                        //DEFINIMOS LA CLASE DE MAILMESSAGE
                        MailMessage mail = new MailMessage();
                        //INDICAMOS EL EMAIL DE ORIGEN
                        mail.From = new MailAddress(correoSistema);


                        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                        mail.To.Add(DestinatariosMail);
                        //mail.To.Add("ivanvidal@forsa.net.co");


                        //INCLUIMOS EL ASUNTO DEL MENSAJE
                        mail.Subject = AsuntoMail;
                        //AÑADIMOS EL CUERPO DEL MENSAJE
                        mail.Body = MensajeMail;
                        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                        mail.BodyEncoding = System.Text.Encoding.UTF8;
                        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                        mail.Priority = System.Net.Mail.MailPriority.Normal;
                        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                        mail.IsBodyHtml = true;
                        //ADJUNTAMOS EL ARCHIVO
                        MemoryStream ms = new MemoryStream();
                        if (llevaAnexo == true)
                        {
                            string enlace = "";

                            if (Evento == 42)
                            {
                                tipoAdjunto = "ValidacionDespacho";
                                enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&fechaini=" + FechaIni.ToString() + "&fechafin=" + FechaFin.ToString() + "&Planta=" + Planta;
                                
                                //enlace = @"http://10.75.131.2:81/ReportServer?" + EnlaceAnexo.Trim() + "&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "" +
                                //                 "&version=" + version + "&parte=" + parte.ToString() + "&sf_id=" + sfId;
                            
                            }


                            correo = clienteWeb.DownloadData(enlace);
                            ms = new MemoryStream(correo);
                            mail.Attachments.Add(new Attachment(ms, tipoAdjunto + " " + ".pdf"));

                        }
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        //DECLARAMOS LA CLASE SMTPCLIENT
                        SmtpClient smtp = new SmtpClient();
                        //DEFINIMOS NUESTRO SERVIDOR SMTP
                        smtp.Host = "smtp.office365.com";
                        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                        smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                        smtp.Port = 25;
                        smtp.EnableSsl = true;
                        //smtp.Timeout =

                        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                            SslPolicyErrors sslPolicyErrors)
                        {
                            return true;
                        };

                        System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        try
                        {
                            // smtp.SendAsync(mail, mail.To);
                            smtp.Send(mail);
                        }
                        catch (Exception ex)
                        {
                            string mensaje = "ERROR: " + ex.Message;
                        }
                        ms.Close();
                    }
                }
              }
            }
        }

      

        protected void btnIngresarTema_Click(object sender, EventArgs e)
        {
            string msj = "";
            if (btnIngresarTema.Text == "Ingresar Tema")
            {
                if (txtTema.Text == "")
                {
                    msj = "Debe ingresar el tema.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
                }
                else
                {
                    string Nombre = (string)Session["Nombre_Usuario"];

                    //COLOCAMOS EL TEXTO EN MAYUSCULAS
                    txtTema.Text = txtTema.Text.ToUpperInvariant();
                    int id = 0, accion = 1;

                    int camb = controlfup.ActCAMBIOS(id, Convert.ToInt32(txtFUP.Text), txtTema.Text, txtFecCierre.Text,
                        Convert.ToInt32(cboAreaResp.SelectedValue), Convert.ToInt32(cboResponTema.SelectedValue), Nombre, accion);

                    msj = "Tema ingresado con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);

                    CargarGrillaIngresoTema();
                }
            }
            else
            {

            }
        }

        protected void btnNuevoTema_Click(object sender, EventArgs e)
        {
            txtTema.Text = "";
            txtFecCierre.Text = "";
            PoblarAreaTema();
            cboResponTema.Items.Clear();
            PoblarEstadoTema();
        }

        protected void btnGuardarComTema_Click(object sender, EventArgs e)
        {
            string idTema = (string)Session["IdComTema"];
            string msj = "";

            if (txtComTema.Text == "")
            {
                msj = "Debe ingresar el comentario del tema.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
            }
            else
            {
                string Nombre = (string)Session["Nombre_Usuario"];

                //COLOCAMOS EL TEXTO EN MAYUSCULAS
                txtComTema.Text = txtComTema.Text.ToUpperInvariant();
                int id = 0, accion = 1;

                int camb = controlfup.ActCOMENCAMBIOS(id, Convert.ToInt32(idTema), txtComTema.Text, Nombre, accion);

                msj = "Comentario ingresado con éxito.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);

                CargarGrillaComentarioTema(idTema);
            }
        }

        private void CargarGrillaComentarioTema(string id)
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarComentariosTema(Convert.ToInt32(id));
            if (dsFUP != null)
            {
                grvComentario.DataSource = dsFUP.Tables[0];
                grvComentario.DataBind();
                grvComentario.Visible = true;
            }
            else
            {
                grvComentario.Dispose();
                grvComentario.Visible = false;
            }
            dsFUP.Reset();
        }

        protected void btnNuevoComTema_Click(object sender, EventArgs e)
        {
            txtComTema.Text = "";
        }


        //protected void btnGuardarCambios_Click(object sender, EventArgs e)
        //{
        //    string Nombre = (string)Session["Nombre_Usuario"];

        //    //COLOCAMOS EL TEXTO EN MAYUSCULAS
        //    txtObservaCambios.Text = txtObservaCambios.Text.ToUpperInvariant();

        //    int ActFUP = controlfup.ActCAMBIOS(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Nombre,
        //        Convert.ToInt64(cboRespCambio.SelectedItem.Value), txtObservaCambios.Text.ToUpperInvariant(),
        //        Convert.ToInt32(cboAreaRespCambios.SelectedItem.Value));

        //    MensajeControlCambios();
        //    CargarControlCambios();

        //    ValidarFUP();
        //    ValidacionGeneralFUP();
        //    cargaDatosCambios();
        //}

        //protected void btnNuevoCambio_Click(object sender, EventArgs e)
        //{
        //    txtObservaCambios.Text = "";
        //    cboRespCambio.SelectedIndex = 0;
        //    cboAreaRespCambios.SelectedIndex = 0;
        //}

        private void MensajeControlCambios()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Control de cambio ingresado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Change control successfully entered.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "Alterar controle entrou com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void CargarControlCambios()
        {
            PoblarAreaTema();
            PoblarEstadoTema();
            CargarGrillaIngresoTema();
        }

        private void CargarGrillaIngresoTema()
        {
            dsFUP.Reset();
            dsFUP = controlfup.ConsultarTema(Convert.ToInt32(txtFUP.Text));
            if (dsFUP != null)
            {
                grvTema.DataSource = dsFUP.Tables[0];
                grvTema.DataBind();
                grvTema.Visible = true;
            }
            else
            {
                grvTema.Dispose();
                grvTema.Visible = false;

            }
            dsFUP.Reset();
        }

        protected void grvTema_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            pnlComen.Visible = false;
            if (e.CommandName == "Estado")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvTema.Rows[index];

                string msj = "";
                string id = Convert.ToString(grvTema.DataKeys[index].Value);
                string usucrea = ((Label)row.FindControl("lblCreaTema")).Text.ToString();
                string Nombre = (string)Session["Nombre_Usuario"];

                if (usucrea.ToString().Trim() == Nombre.ToString().Trim())
                {
                    string estado = ((Label)row.FindControl("lblEstadoTema")).Text.ToString();
                    if (estado == "Cerrado")
                    {
                        msj = "El tema ya se encuentra cerrado.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
                        CargarGrillaIngresoTema();
                    }
                    else
                    {
                        //CAMBIAR ESTADO TEMA
                        reader = controlfup.EstadoTema(Convert.ToInt32(id));
                        msj = "Tema cerrado con éxito.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
                        
                        BdDatos.desconectar();
                    }
                }
                else
                {
                    msj = "No puede cerrar el tema. Solo la persona que lo ingreso lo puede cerrar.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
                }
                CargarGrillaIngresoTema();

            }

            if (e.CommandName == "Comentario")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvTema.Rows[index];

                string id = Convert.ToString(grvTema.DataKeys[index].Value);
                Session["IdComTema"] = id;

                string estado = ((Label)row.FindControl("lblEstadoTema")).Text.ToString();
                if (estado == "Cerrado")
                {
                    string msj = "El tema se encuentra cerrado no puede ingresar más comentarios.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
                    pnlComen.Visible = false;
                }
                else
                {
                    txtComTema.Text = "";
                    pnlComen.Visible = true;
                    CargarGrillaComentarioTema(id);
                }
            }
        }

        private void grvTema_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
        {

        }

        private void PoblarEstadoTema()
        {
            cboEstadoTema.Items.Clear();

            cboEstadoTema.Items.Add(new ListItem("Elija El Estado", "0"));
            cboEstadoTema.Items.Add(new ListItem("Abierto", "1"));
            cboEstadoTema.Items.Add(new ListItem("Cerrado", "2"));
        }


        private void PoblarAreaTema()
        {
            cboAreaResp.Items.Clear();

            reader = controlfup.PoblarAreaResponsableControlCambios();
            cboAreaResp.Items.Add(new ListItem("Elija El Area Responsable", "0"));
            while (reader.Read())
            {
                cboAreaResp.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        protected void cboAreaResp_SelectedIndexChanged(object sender, EventArgs e)
        {
            PoblarResponsableTema();
        }

        private void PoblarResponsableTema()
        {
            cboResponTema.Items.Clear();

            reader = controlfup.PoblarResponsableControlCambios(Convert.ToInt32(cboAreaResp.SelectedValue));
            cboResponTema.Items.Add(new ListItem("Seleccione El Responsable", "0"));
            while (reader.Read())
            {
                cboResponTema.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        protected void btnRecotizar_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = true;
            chkAcero.Enabled = true;
            chkAluminio.Enabled = true;
            chkPlastico.Enabled = true;
            cboTipoAcero.Enabled = true;
            cboTipoAluminio.Enabled = true;

            txtAlcance.Enabled = true;
            txtObserva.Enabled = true;
            txtNumEquipos.Enabled = true;
            cboClaseCot.Enabled = true;
            SetPane("AcorInfoGeneral");
        }

        protected void btnSubirImagen_Click(object sender, EventArgs e)
        {
            AccorSubirArch.Visible = true;
            SetPane("AccorSubirArch");
        }

        protected void btnEnviarCorreoRechazo_Click(object sender, EventArgs e)
        {
            Session["Evento"] = 4;
            CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Correo enviado con exito.";
            }
            if (idioma == "Ingles")
            {
                mensaje = "Mail sent successfully.";
            }
            if (idioma == "Portugues")
            {
                mensaje = "E-mail enviado com sucesso.";
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void btnDeleteFile_Click(object sender, ImageClickEventArgs e)
        {

            MensajeCrearOF();

        }

        protected void grvArchivo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string ruta = "", mensaje = "";
            int index = Convert.ToInt32(e.CommandArgument);
            GridViewRow row = grvArchivo.Rows[index];
            int rol = (int)Session["Rol"];

            string id = Convert.ToString(grvArchivo.DataKeys[index].Value);
            ruta = ((LinkButton)row.FindControl("simpa_anexoEditLink")).Text.ToString().Replace("/","\\") ;
            //ruta = directorio + @"Carta_Cotizacion\"+nombreCarta;
                    //dirweb = dirweb + @"Carta_Cotizacion/"+nombreCarta;   
            

            if (e.CommandName == "Borrar")
            {
                if (rol == 9 && LEstado.Text == "Ingresado")
                {
                    //BORRAR PLANO EN SISTEMA OPERATIVO
                    if (File.Exists(ruta))
                    {
                        File.Delete(ruta);
                    }

                    //BORRAR PLANO EN BASE DE DATOS
                    reader = controlfup.BorrarPlano(Convert.ToInt32(id), (string)Session["Nombre_Usuario"]);

                    CargarGrillaArchivoForsa();
                }
                else
                {
                    mensaje = "No es posible la Eliminacion, solo el perfil de Asistente Comercial puede realizarlo en estado Ingresado.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
           
        }

        protected void grvOF_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "OFA")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = grvOF.Rows[index];

                string idofa = Convert.ToString(grvOF.DataKeys[index].Value);
                string numof = ((Label)row.FindControl("lblNumero")).Text.ToString();
                string anho = ((Label)row.FindControl("lblAno")).Text.ToString();

                Session["OF"] = numof + '-' + anho;
            }
        }

        protected void gvRechazo_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Validar")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvRechazo.Rows[index];

                string id = Convert.ToString(gvRechazo.DataKeys[index].Value);

                //ACTUALIZAR VALIDACIÓN EN BASE DE DATOS
                reader = controlfup.ValidarRechazo(Convert.ToInt32(id));

                CargarGrillaRechazo();
            }

            if (e.CommandName == "EliminarRechazo")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = gvRechazo.Rows[index];

                string id = Convert.ToString(gvRechazo.DataKeys[index].Value);

                //ACTUALIZAR VALIDACIÓN EN BASE DE DATOS
                reader = controlfup.EliminarRechazo(Convert.ToInt32(id));

                CargarGrillaRechazo();
            }
        }

        private void LimpiarCheckBox()
        {
            if (chkAluminio.Checked == false)
            {
                chkEMuroAlum.Checked = false;
                chkELosaAlum.Checked = false;
                chkEUMLAlum.Checked = false;
                txtALAlum.Text = "";
                txtEMAlum.Text = "";
                txtELAlum.Text = "";
                PoblarTipoUnion();
                txtEPAlum.Text = "";
                txtEVAlum.Text = "";
                chkPlantaAlum.Checked = false;
                chkCorteFachadaAlum.Checked = false;
                chkAzotAlum.Checked = false;
                chkUrbaAlum.Checked = false;
                chkEstructuralAlum.Checked = false;
                chklosaAlum.Checked = false;
                chkMuroAlum.Checked = false;
                chkLosaEscaleraAlum.Checked = false;
                chkFosoAscensorAlum.Checked = false;
                chkFosoEscaleraAlum.Checked = false;
                chkJuntaDilataAlum.Checked = false;
                txtEspJunAlum.Text = "";
                chkDesnAscAlum.Checked = false;
                chkDesnDescAlum.Checked = false;
                chkCulatsPerimAlum.Checked = false;
                chkCulatasInternasAlum.Checked = false;
                chkAntepechosAlum.Checked = false;
                chkColumnasAlum.Checked = false;
                chkEscMonAlum.Checked = false;
                chkEscPostAlum.Checked = false;
                chkBaseAlum.Checked = false;
                chkLosaInclinadaAlum.Checked = false;
                chkDomoAlum.Checked = false;
                chkMPAlum.Checked = false;
                chkNegAceroAlum.Checked = false;
                chkPretilesAlum.Checked = false;
                chkGargolasAlum.Checked = false;
                chkMFTAlum.Checked = false;
                chkNegCarriolasAlum.Checked = false;
                chkVEAlum.Checked = false;
                chkFCMqAlum.Checked = false;
                chkVigasAlum.Checked = false;
                chkTorreonAlum.Checked = false;
                chkRebordesAlum.Checked = false;
                chkReservatoriosAlum.Checked = false;
                chkDilFacAlum.Checked = false;
                chkJCAIAlum.Checked = false;
                chkJCAEAlum.Checked = false;
                chkCanesAlum.Checked = false;
                chkPortAlum.Checked = false;
                chkOtrosAlum.Checked = false;
            }

            if (chkPlastico.Checked == false)
            {
                chkEMuroPlast.Checked = false;
                chkELosaPlast.Checked = false;
                chkEUMLPlast.Checked = false;
                txtALPlast.Text = "";
                txtEMPlast.Text = "";
                txtELPlast.Text = "";
                txtEPPlast.Text = "";
                txtEVPlast.Text = "";
                chkPlantaPlast.Checked = false;
                chkCorteFachadaPlast.Checked = false;
                chkAzotPlast.Checked = false;
                chkUrbaPlast.Checked = false;
                chkEstructuralPlast.Checked = false;
                chklosaPlast.Checked = false;
                chkMuroPlast.Checked = false;
                chkLosaEscaleraPlast.Checked = false;
                chkFosoAscensorPlast.Checked = false;
                chkFosoEscaleraPlast.Checked = false;
                txtEspJunPlast.Text = "";
                chkDesnAscPlast.Checked = false;
                chkDesnDescPlast.Checked = false;
                chkCulatsPeriPlast.Checked = false;
                chkCulatasInternasPlast.Checked = false;
                chkAntepechosPlast.Checked = false;
                chkColumnasPlast.Checked = false;
                chkEscMonPlast.Checked = false;
                chkEscPostPlast.Checked = false;
                chkBasePlast.Checked = false;
                chkLosaInclinadaPlast.Checked = false;
                chkDomoPlast.Checked = false;
                chkMPPlast.Checked = false;
                chkNegAceroPlast.Checked = false;
                chkPretilesPlast.Checked = false;
                chkGargolasPlast.Checked = false;
                chkMFTPlast.Checked = false;
                chkNegCarriolasPlast.Checked = false;
                chkVEPlast.Checked = false;
                chkFCMqPlast.Checked = false;
                chkVigasPlast.Checked = false;
                chkTorreonPlast.Checked = false;
                chkRebordesPlast.Checked = false;
                chkReservatoriosPlast.Checked = false;
                chkDilFacPlast.Checked = false;
                chkJCAIPlast.Checked = false;
                chkCanesPlast.Checked = false;
                chkPortPlast.Checked = false;
                chkOtrosPlast.Checked = false;
            }

            if (chkAcero.Checked == false)
            {
                chkEMuroAcero.Checked = false;
                chkELosaAcero.Checked = false;
                chkEUMLAcero.Checked = false;
                txtALAcero.Text = "";
                txtEMAcero.Text = "";
                txtELPlast.Text = "";
                txtEPAcero.Text = "";
                txtEVAcero.Text = "";
                chkPlantaAcero.Checked = false;
                chkCorteFachadaAcero.Checked = false;
                chkAzotAcero.Checked = false;
                chkUrbaAcero.Checked = false;
                chkEstructuralAcero.Checked = false;
                chklosaAcero.Checked = false;
                chkMuroAcero.Checked = false;
                chkLosaEscaleraAcero.Checked = false;
                chkFosoAscensorAcero.Checked = false;
                chkFosoEscaleraAcero.Checked = false;
                txtEspJunAcero.Text = "";
                chkDesnAscAcero.Checked = false;
                chkDesnDescAcero.Checked = false;
                chkCulatsPerimAcero.Checked = false;
                chkCulatasInternasAcero.Checked = false;
                chkAntepechosAcero.Checked = false;
                chkColumnasAcero.Checked = false;
                chkEscMonAcero.Checked = false;
                chkEscPostAcero.Checked = false;
                chkBaseAcero.Checked = false;
                chkLosaInclinadaAcero.Checked = false;
                chkDomoAcero.Checked = false;
                chkMPAcero.Checked = false;
                chkNegAceroAce.Checked = false;
                chkPretilesAcero.Checked = false;
                chkGargolasAcero.Checked = false;
                chkMFTAcero.Checked = false;
                chkNegCarriolasAcero.Checked = false;
                chkVEAcero.Checked = false;
                chkFCMqAcero.Checked = false;
                chkVigasAcero.Checked = false;
                chkTorreonAcero.Checked = false;
                chkRebordesAcero.Checked = false;
                chkReservatoriosAcero.Checked = false;
                chkDilFacAcero.Checked = false;
                chkJCAIAcero.Checked = false;
                chkJCAEcero.Checked = false;
                chkCanesAcero.Checked = false;
                chkPortAcero.Checked = false;
                chkOtrosAcero.Checked = false;
            }
        }

        protected void chkAluminio_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAluminio.Checked == false)
            {
                LimpiarCheckBox();
            }
        }

        protected void chkPlastico_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPlastico.Checked == false)
            {
                LimpiarCheckBox();
            }
        }

        protected void chkAcero_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAcero.Checked == false)
            {
                LimpiarCheckBox();
            }
        }

        protected void SetPane(string PaneID)
        {
            int Index = 0;
            foreach (AjaxControlToolkit.AccordionPane pane in Accordion1.Panes)
            {
                if (pane.Visible == true)
                {
                    if (pane.ID == PaneID)
                    {
                        Accordion1.SelectedIndex = Index;
                        break;
                    }
                    Index++;
                }
            }
        } 

        protected void lkCliente_Click(object sender, EventArgs e)
        {
            if (cboCliente.SelectedItem.Value == null || cboCliente.SelectedItem.Value == "0")
            { 
            }
            else
            {
                string pagina = "http://app.forsa.com.co/SIOMaestros/Cliente.aspx?idCliente=" + cboCliente.SelectedItem.Value.ToString();
                string script = "window.open('" + pagina + "', '_blank');";
                //string script = "window.open('Cliente.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            }

        }

        protected void lkContacto_Click(object sender, EventArgs e)
        {
            if (cboContacto.SelectedItem.Value == null || cboContacto.SelectedItem.Value == "0")
            { 
            }
            else
            {
                string pagina = "http://app.forsa.com.co/SIOMaestros/Contacto.aspx?idContCliente=" + cboContacto.SelectedItem.Value.ToString();
                string script = "window.open('" + pagina + "', '_blank');";
                //string script = "window.open('Cliente.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            }
        }

        protected void lkObra_Click(object sender, EventArgs e)
        {
            if (cboObra.SelectedItem.Value == null || cboObra.SelectedItem.Value == "0")
            { 
            }
            else
            {
                string pagina = "http://app.forsa.com.co/SIOMaestros/Obra.aspx?idObra=" + cboObra.SelectedItem.Value.ToString();
                string script = "window.open('" + pagina + "', '_blank');";
                //string script = "window.open('Cliente.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);
            }
            
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
             string pagina = "http://www.youblisher.com/p/823010-fup/";
                string script = "window.open('" + pagina + "', '_blank');";
                //string script = "window.open('Cliente.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);           
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cliente.aspx?idCliente=" + Session["Cliente"].ToString());
        }
        /***************************************/
        // ACTA DE ENTREGA DE EQUIPO --- JORGE CARDONA --- METROLINK
        //lleno el primer como el cual me llena las ordenes, de hay se toma el idOFa para realizar las siguientes operaciones
        private void llenarComboOrdenes()
        {
            cboComboOrden.Items.Clear();
            DataTable cargaComboOrd = CA.cargarComboOrdenes(int.Parse(Session["FUP"].ToString()), Session["VER"].ToString());
            cboComboOrden.Items.Add("Seleccionar");
            foreach (DataRow row in cargaComboOrd.Rows)
            {
                cboComboOrden.Items.Add(new ListItem(row["orden"].ToString(), row["idofa"].ToString()));
            }
        }
        //se llena la lista con los contactos del cliente
        private void poblarListContantosC()
        {
            DataTable dt = null;
            listaCont.Items.Clear();

            dt = CA.cargarContatosCliente(int.Parse(Session["idClienteActa"].ToString()));            
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    listaCont.Items.Add(new ListItem(row["nombre"].ToString(), row["idCont"].ToString()));
                    listaPerRes.Items.Add(new ListItem(row["nombre"].ToString(), row["idCont"].ToString()));
                }
            }
            else
            { }
        }
        //se llena la lista con los empleados de forsa
        private void poblarListForsa(int idRol)
        {
            listaForsa.Items.Clear();
            DataTable dt = null;
            dt = CA.cargarEmpleadosForsa(idRol);
            if (dt != null)
            {
                foreach (DataRow row in dt.Rows)
                {
                    listaForsa.Items.Add(new ListItem(row["nombre"].ToString(), row["idEmpleado"].ToString()));
                }
            }
            else
            { }
        }
        //se llena el combo de los roles que se manejan para los empleado de forsa, para poder filtrar menos personas
        private void llenarComboRoles()
        {
            cboAreaForsa.Items.Clear();
            DataTable cargaComboSer = CA.cargarComboNomRoles();
            cboAreaForsa.Items.Add("Seleccionar");
            foreach (DataRow row in cargaComboSer.Rows)
            {
                cboAreaForsa.Items.Add(new ListItem(row["nomRol"].ToString(), row["idRol"].ToString()));
            }
        }
        //lipia todas las listas
        private void limpiarListas()
        {
            listaForsa.Items.Clear();
            listaCont.Items.Clear();
            listaContAgg.Items.Clear();
            listaForsaAgg.Items.Clear();
        }
        //carga los datos de la base de datos para editar o mostrar
        private void cargarDatosActa(int idOfa)
        {
            DataTable datos = CA.traerDatosModificarTextos(idOfa);
            foreach (DataRow row in datos.Rows)
            {
                txtOrdenes.Text = row["referencia"].ToString();
                txtTecnicasEq.Text = row["tecEquipos"].ToString();
                txtCronoDespacho.Text = row["cronoDespacho"].ToString();
                txtConEmbalaje.Text = row["condEmbalaje"].ToString();
                txtConFinanciera.Text = row["condFinanciera"].ToString();
                listaPerResAgg.Items.Add(new ListItem(row["nombre"].ToString(), row["idContacto"].ToString()));
            }

            DataTable datoslistaCont = CA.datosContactoCliente(idOfa);
            foreach (DataRow row in datoslistaCont.Rows)
            {
                listaContAgg.Items.Add(new ListItem(row["nombre"].ToString(), row["idContacto"].ToString()));
            }

            DataTable datoslistaEmp = CA.datosEmpleadosForsa(idOfa);
            foreach (DataRow row in datoslistaEmp.Rows)
            {
                listaForsaAgg.Items.Add(new ListItem(row["nombre"].ToString(), row["idEmpForsa"].ToString()));
            }

        }
        //me verifica si el acta existe, si esta finalizada o no
        private void existeActa(int idOfa)
        {
            int existe = CA.existeActaModificar(idOfa);
            int existeAct = CA.existeActa(idOfa);
            if (existe > 0)//si existe el acta y el estado esta en false(acta abierta), que traiga los datos para poderlos modificar
            {
                enableBotones(true);
                btnGuardarActa.Text = "Actualizar";
                btnFinalizar.Text = "Finalizar";
                enableCamposFalse(true);
                ReportActaEntrega.Visible = false;
                Session["Accion"] = "Editar";
                cargarDatosActa(idOfa);
                mensajes("blue", "Guardado");
            }
            else if (existeAct > 0)//si existe el acta y esta cerrada, que anule todos los campos y vizualice el reporte
            {
                enableBotones(false);
                btnFinalizar.Text = "Finalizado";
                enableCamposFalse(false);
                cargarDatosActa(idOfa);
                cargarReporteActaEntrega(Session["idOfaActa"].ToString());
                mensajes("blue", "Finalizada!!");
            }
            else//si ninguna de las condiciones cumple, no existe ninguna acta y pasaria a guardar los datos que ingrese
            {
                btnGuardarActa.Enabled = true;
                btnGuardarActa.Visible = true;
                btnGuardarActa.Text = "Guardar";
                btnFinalizar.Enabled = false;
                enableCamposFalse(true);
                Session["Accion"] = "Guardar";
                ReportActaEntrega.Visible = false;
                mensajes("blue", "Sin ingresar");
                txtOrdenes.Text = cboComboOrden.SelectedItem.Text;
            }
        }
        private void enableBotones(Boolean boo)
        {
            btnGuardarActa.Enabled = boo;
            btnGuardarActa.Visible = boo;
            btnFinalizar.Enabled = boo;
        }
        //los btn son los botones que agregan y eliminan los items de las listas
        protected void btnAgregarCont_Click(object sender, EventArgs e)
        {
            int resp = listaCont.SelectedIndex;
            if (resp >= 0)
            {
                listaContAgg.Items.Add(new ListItem(listaCont.SelectedItem.Text, listaCont.SelectedValue.ToString()));
            }
            else
            { }

            for (int i = 0; i < listaCont.Items.Count; i++)
            {
                if (listaCont.Items[i].Selected)
                {
                    listaCont.Items.Remove(listaCont.Items[i]);
                }
            }

        }
        protected void btnEliminarCont_Click(object sender, EventArgs e)
        {
            int resp = listaContAgg.SelectedIndex;
            if (resp >= 0)
            {
                listaCont.Items.Add(new ListItem(listaContAgg.SelectedItem.Text, listaContAgg.SelectedValue.ToString()));
            }
            else
            { }

            for (int i = 0; i < listaContAgg.Items.Count; i++)
            {
                if (listaContAgg.Items[i].Selected)
                {
                    listaContAgg.Items.Remove(listaContAgg.Items[i]);
                }
            }
        }
        protected void btnElimTodosCont_Click(object sender, EventArgs e)
        {
            listaContAgg.Items.Clear();
        }
        protected void btnAgregarForsa_Click(object sender, EventArgs e)
        {
            int resp = listaForsa.SelectedIndex;
            if (resp >= 0)
            {
                listaForsaAgg.Items.Add(new ListItem(listaForsa.SelectedItem.Text, listaForsa.SelectedValue.ToString()));
            }
            else
            { }

            for (int i = 0; i < listaForsa.Items.Count; i++)
            {
                if (listaForsa.Items[i].Selected)
                {
                    listaForsa.Items.Remove(listaForsa.Items[i]);
                }
            }

        }
        protected void btnEliminarForsa_Click(object sender, EventArgs e)
        {
            int resp = listaForsaAgg.SelectedIndex;
            if (resp >= 0)
            {
                listaForsa.Items.Add(new ListItem(listaForsaAgg.SelectedItem.Text, listaForsaAgg.SelectedValue.ToString()));
            }
            else
            { }
            for (int i = 0; i < listaForsaAgg.Items.Count; i++)
            {
                if (listaForsaAgg.Items[i].Selected)
                {
                    listaForsaAgg.Items.Remove(listaForsaAgg.Items[i]);
                }
            }
        }
        protected void btnElimTodosForsa_Click(object sender, EventArgs e)
        {
            listaForsaAgg.Items.Clear();
        }
        protected void btnAgregarResp_Click(object sender, EventArgs e)
        {
            listaPerResAgg.Items.Clear();
            int resp = listaPerRes.SelectedIndex;
            if (resp >= 0)
            {
                listaPerResAgg.Items.Add(new ListItem(listaPerRes.SelectedItem.Text, listaPerRes.SelectedValue.ToString()));
            }
            else
            { }
        }
        //guarda y modifica los datos
        protected void btnGuardarActa_Click(object sender, EventArgs e)
        {
            if (Session["Accion"].ToString() == "Guardar")//dependiendo de la sesion hace la tarea correspondiente
            {
                if (txtOrdenes.Text == "" || txtTecnicasEq.Text == "" || txtConEmbalaje.Text == "" || txtConFinanciera.Text == "" || txtCronoDespacho.Text == "")
                {
                    mensajeVentana("Por favor llene todos los campos correspondientes, gracias!!");
                }
                else
                {
                    String mensaje = "";
                    int listaConta = listaContAgg.Items.Count;
                    if (listaConta == 0)
                    {
                        mensajeVentana("Por favor selecione los contacto del Cliente, gracias!!");
                    }
                    else
                    {
                        int listaEmp = listaForsaAgg.Items.Count;
                        if (listaEmp == 0)
                        {
                            mensajeVentana("Por favor selecione los empleados de Forsa, gracias!!");
                        }
                        else
                        {
                            int listaPerPresente = listaPerResAgg.Items.Count;
                            if (listaPerPresente == 0)
                            {
                                mensajeVentana("Por favor selecione el contacto responsable del Cliente, gracias!!");
                            }
                            else
                            {
                                String perRespon = "0";
                                foreach (ListItem item in listaPerResAgg.Items)
                                {
                                    perRespon = item.Value.ToString();
                                }
                                mensaje = CA.insertDatos(int.Parse(Session["idOfaActa"].ToString()), txtTecnicasEq.Text, txtCronoDespacho.Text, txtConEmbalaje.Text, txtConFinanciera.Text, int.Parse(perRespon), Session["Usuario"].ToString(),txtOrdenes.Text);
                                if (mensaje == "OK")//verifica si inserto bien los datos generales
                                {
                                    mensaje = "";
                                    foreach (ListItem item in listaContAgg.Items)
                                    {
                                        mensaje = CA.insertListasContaCliente(int.Parse(Session["idOfaActa"].ToString()), int.Parse(item.Value));
                                    }
                                    if (mensaje == "OK")//verifica si inserto bien los contactos
                                    {
                                        mensaje = "";
                                        foreach (ListItem item in listaForsaAgg.Items)
                                        {
                                            mensaje = CA.insertListasEmpForsa(int.Parse(Session["idOfaActa"].ToString()), int.Parse(item.Value));
                                        }
                                        if (mensaje == "OK")//verifica si inserto bien los empleados
                                        {
                                            mensajeVentana("El acta ha sido creada correctamente!!");
                                            btnGuardarActa.Text = "Actualizar";
                                            mensajes("blue", "Guardado");
                                            Session["Accion"] = "Editar";
                                            btnFinalizar.Text = "Finalizar";
                                            btnFinalizar.Enabled = true;
                                        }
                                        else
                                        {
                                            mensajeVentana(mensaje);
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana(mensaje);
                                    }
                                }
                                else
                                {
                                    mensajeVentana(mensaje);
                                }
                            }
                        }
                    }
                }
            }
            else if (Session["Accion"].ToString() == "Editar")//si la sesion es editar, pasa a modificar los datos que esten
            {
                if ( txtOrdenes.Text == "" || txtTecnicasEq.Text == "" || txtConEmbalaje.Text == "" || txtConFinanciera.Text == "" || txtCronoDespacho.Text == "")
                {
                    mensajeVentana("Por favor llene todos los campos correspondientes, gracias!!");
                }
                else
                {
                    String mensaje = "";
                    int listaConta = listaContAgg.Items.Count;
                    if (listaConta == 0)
                    {
                        mensajeVentana("Por favor selecione los contacto del Cliente, gracias!!");
                    }
                    else
                    {
                        int listaEmp = listaForsaAgg.Items.Count;
                        if (listaEmp == 0)
                        {
                            mensajeVentana("Por favor seleccione los empleados de Forsa, gracias!!");
                        }
                        else
                        {
                            int listaPerPresente = listaPerResAgg.Items.Count;
                            if (listaPerPresente == 0)
                            {
                                mensajeVentana("Por favor seleccione el contacto responsable del Cliente, gracias!!");
                            }
                            else
                            {
                                String perRespon = "0";
                                foreach (ListItem item in listaPerResAgg.Items)
                                {
                                    perRespon = item.Value.ToString();
                                }
                                mensaje = CA.actualizarDatos(int.Parse(Session["idOfaActa"].ToString()), txtTecnicasEq.Text, txtCronoDespacho.Text, txtConEmbalaje.Text, txtConFinanciera.Text, int.Parse(perRespon), Session["Usuario"].ToString(),txtOrdenes.Text);
                                if (mensaje == "OK")//verifica si actualizo bien los datos generales
                                {
                                    mensaje = "";
                                    mensaje = CA.actualizarListas(int.Parse(Session["idOfaActa"].ToString()), "aeinvol_emp_forsa_id");//borra los datos dependiendo del campo
                                    if (mensaje == "OK")//si es OK borro bien los datos
                                    {
                                        foreach (ListItem item in listaForsaAgg.Items)
                                        {
                                            mensaje = CA.insertListasEmpForsa(int.Parse(Session["idOfaActa"].ToString()), int.Parse(item.Value));//vuelve a insertar los contactos del cliente
                                        }
                                        if (mensaje == "OK")//si es OK inserto bien los contactos
                                        {
                                            mensaje = "";
                                            mensaje = CA.actualizarListas(int.Parse(Session["idOfaActa"].ToString()), "aeinvol_contacto_id");//borra los datos dependiendo del campo
                                            if (mensaje == "OK")//si es OK borro bien los datos
                                            {
                                                foreach (ListItem item in listaContAgg.Items)
                                                {
                                                    mensaje = CA.insertListasContaCliente(int.Parse(Session["idOfaActa"].ToString()), int.Parse(item.Value));//vuelve a insertar los empleados de forsa
                                                }
                                                if (mensaje == "OK")
                                                {
                                                    mensajeVentana("El acta ha sido actualizada correctamente!!");
                                                    btnGuardarActa.Text = "Actualizar";
                                                    mensajes("blue", "Guardado");
                                                    Session["Accion"] = "Editar";
                                                    btnFinalizar.Text = "Finalizar";
                                                    btnFinalizar.Enabled = true;
                                                }
                                                else
                                                {
                                                    mensajeVentana(mensaje);
                                                }
                                            }
                                            else
                                            {
                                                mensajeVentana(mensaje);
                                            }
                                        }
                                        else
                                        {
                                            mensajeVentana(mensaje);
                                        }
                                    }
                                    else
                                    {
                                        mensajeVentana(mensaje);
                                    }
                                }
                                else
                                {
                                    mensajeVentana(mensaje);
                                }
                            }
                        }
                    }
                }
            }
        }
        //se filtra el rol y se llena el metodo poblar lista de los empleado de forsa
        protected void cboAreaForsa_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblMens.Text = "";
            if (cboAreaForsa.SelectedItem.ToString() == "Seleccionar")
            {
                listaForsa.Items.Clear();
            }
            else
            {
                poblarListForsa(int.Parse(cboAreaForsa.SelectedValue.ToString()));
            }
        }
        //se filtra por la orden y dependiendo que acta tenga cada orden la muestra
        protected void cboComboOrden_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboComboOrden.SelectedItem.ToString() != "Seleccionar")
            {
                limpiarCamposActa();
                panelActaEntrega.Visible = true;
                llenarComboRoles();
                poblarListContantosC();
                Session["idOfaActa"] = cboComboOrden.SelectedValue.ToString();
                existeActa(int.Parse(Session["idOfaActa"].ToString()));
                botonesActaEntrega();
            }
            else
            {
                limpiarCamposActa();
                panelActaEntrega.Visible = false;
            }
        }
        //este metodo maneja los mensajes, desaparece por un determinado tiempo
        private void mensajes(String color, String mensaje)
        {
            lblMens.Text = "";
            lblMens.Text = mensaje;
            lblMens.Visible = true;
            ScriptManager.RegisterStartupScript(Page, GetType(), "Script", "setTimeout(function(){ document.getElementById('ContentPlaceHolder4_lblMens').style.color='" + color + "'; document.getElementById('ContentPlaceHolder4_lblMens').style.display = 'inline';} , 100); ", true);
        }
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }
        //estan todos los campo enable para cuando la acta este finalizada
        private void enableCamposFalse(Boolean boo)
        {
            txtConEmbalaje.Enabled = boo;
            txtConFinanciera.Enabled = boo;
            txtCronoDespacho.Enabled = boo;
            txtTecnicasEq.Enabled = boo;
            listaCont.Enabled = boo;
            listaContAgg.Enabled = boo;
            listaForsa.Enabled = boo;
            listaForsaAgg.Enabled = boo;
            listaPerRes.Enabled = boo;
            listaPerResAgg.Enabled = boo;
            cboAreaForsa.Enabled = boo;
            btnAgregarCont.Enabled = boo;
            btnAgregarForsa.Enabled = boo;
            btnAgregarResp.Enabled = boo;
            btnEliminarCont.Enabled = boo;
            btnEliminarForsa.Enabled = boo;
            btnElimTodosCont.Enabled = boo;
            btnElimTodosForsa.Enabled = boo;
        }
        //cierra el acta para no volverla a modifica y muestra el reporte
        protected void btnFinalizar_Click(object sender, EventArgs e)
        {
            enableCamposFalse(false);
            btnGuardarActa.Enabled = false;
            btnGuardarActa.Visible = false;
            btnFinalizar.Text = "Finalizado";
            btnFinalizar.Enabled = false;
            mensajes("blue", "Finalizada!!");
            CA.cerrarActa(int.Parse(Session["idOfaActa"].ToString()));
            //mensajes("green", "El Acta ha sido finalizada!!");
            mensajeVentana("El Acta ha sido finalizada!!");
            ReportActaEntrega.Visible = true;
            cargarReporteActaEntrega(Session["idOfaActa"].ToString());
            Session["Evento"] = 13;
            this.CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
        }
        //limpiar los campos
        private void limpiarCamposActa()
        {
            lblMens.Text = "";
            txtConEmbalaje.Text = "";
            txtConFinanciera.Text = "";
            txtCronoDespacho.Text = "";
            txtTecnicasEq.Text = "";
            listaCont.Items.Clear();
            listaContAgg.Items.Clear();
            listaForsa.Items.Clear();
            listaForsaAgg.Items.Clear();
            listaPerRes.Items.Clear();
            listaPerResAgg.Items.Clear();
            txtOrdenes.Text = "";
        }
        //metodo para cargar el reporte del Acta de entrega de equipos
        private void cargarReporteActaEntrega(String idOfa)
        {
            ReportActaEntrega.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("idOfa", idOfa, true));
            ReportActaEntrega.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportActaEntrega.ServerReport.ReportServerCredentials = irsc;
            ReportActaEntrega.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportActaEntrega.ServerReport.ReportPath = "/Comercial/COM_ActaEntrega";
            this.ReportActaEntrega.ServerReport.SetParameters(parametro);
            ReportActaEntrega.ShowToolBar = true;
        }
        // ACTA DE ENTREGA DE EQUIPO --- JORGE CARDONA --- METROLINK
        /***************************************/
        /***************************************/
        // CAMBIO DE ESTADO FUP --- JORGE CARDONA --- METROLINK 
        public void cargaDatosCambios()
        {
            DataTable dt = CEP.traerIdEntrega(int.Parse(Session["FUP"].ToString()), Session["VER"].ToString());
            foreach (DataRow row in dt.Rows)
            {
                Session["idEntrada"] = row["idEntrada"].ToString();
                Session["idEstAnt"] = row["idEstAnt"].ToString();
            }
            grdCambiarEstado.DataSource = dt;
            grdCambiarEstado.DataBind();
            grdCambios.DataSource = CEP.cargarTablaCambio(int.Parse(Session["idEntrada"].ToString()));
            grdCambios.DataBind();
            if (Session["Rol"].ToString() != "26")
            {
                btnActualizar.Visible = false;
            }
            else { 
                if (LEstado.Text != "Orden Fabricacion")
                        btnActualizar.Visible = true; 
                else
                    btnActualizar.Visible = false;
            }
        }
        public DataTable estados()
        {
            DataTable dt = CEP.traerEstados(int.Parse(Session["idEstAnt"].ToString()));
            return dt;
        }
        public DataTable motivos()
        {
            DataTable dt = CEP.traerMotivos();
            return dt;
        }
        protected void btnActualizar_Click(object sender, EventArgs e)
        {
            String cboEstado = "";
            String cboMotivo = "";
            String desc = "";
            String mensaje = "";
            foreach (GridViewRow row in grdCambiarEstado.Rows)
            {
                cboEstado = ((DropDownList)row.FindControl("cboEstado")).SelectedItem.Value;
                cboMotivo = ((DropDownList)row.FindControl("cboMotivo")).SelectedItem.Value;
                desc = ((TextBox)row.FindControl("txtDesc")).Text;
            }

            if (desc != "" && desc != null)
            {

                mensaje = CEP.actualizarEntrega(int.Parse(Session["FUP"].ToString()), Session["VER"].ToString(), int.Parse(cboEstado));
                if (mensaje == "OK")
                {
                    mensaje = "";
                    mensaje = CEP.insertEstadoLog(int.Parse(Session["idEntrada"].ToString()), int.Parse(Session["idEstAnt"].ToString()), int.Parse(cboEstado), int.Parse(cboMotivo), desc, Session["Usuario"].ToString());
                    if (mensaje == "OK")
                    {
                        cargaDatosCambios();
                        txtFUP_TextChanged(sender, e);
                        Session["Evento"] = 12;
                        CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                        mensajeVentana("Cambio Realizado!!");
                        ValidarFUP();
                        ValidacionGeneralFUP();
                    }
                    else
                    {
                        mensajeVentana(mensaje);
                    }
                }
                else
                {
                    mensajeVentana(mensaje);
                }
            }
            else {
                mensajeVentana("Por favor llene todos los campos(Descripcion), gracias!!");
            }

        }

        protected void btnReporte_Click(object sender, EventArgs e)
        {
           SetPane("PaneActaEntrega");
           // Accordion1.SelectedIndex = 11;
            cargarReporteActaEntrega(Session["idOfaActa"].ToString());
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
                poblarListContantosC();
        }

        protected void txtCont20_TextChanged(object sender, EventArgs e)
        {
            string resultado = IsNumeric(txtCont20.Text);
            if (resultado == "") txtCont20.Text = "0";
        }

        protected void txtCont40_TextChanged(object sender, EventArgs e)
        {
            string resultado = IsNumeric(txtCont40.Text);
            if (resultado == "") txtCont40.Text = "0";
        }
        
        // CAMBIO DE ESTADO FUP --- JORGE CARDONA --- METROLINK
        /***************************************/

        //CONTROL DE CAMBIOS - GLOBAL BI
        protected void Show_Hide_OrdersGrid(object sender, EventArgs e)
        {
            ImageButton imgShowHide = (sender as ImageButton);
            GridViewRow row = (imgShowHide.NamingContainer as GridViewRow);
            if (imgShowHide.CommandArgument == "Show")
            {
                row.FindControl("pnlOrders").Visible = true;
                imgShowHide.CommandArgument = "Hide";
                imgShowHide.ImageUrl = "~/imagenes/toolkit-arrow.png";
                string customerId = grvTema.DataKeys[row.RowIndex].Value.ToString();
                string tema = ((Label)row.FindControl("lblTema")).Text.ToString();
                GridView grvCom = row.FindControl("grvCom") as GridView;
                BindOrders(customerId, tema, grvCom);

                grvComentario.Visible = false;

            }
            else
            {
                row.FindControl("pnlOrders").Visible = false;
                imgShowHide.CommandArgument = "Show";
                imgShowHide.ImageUrl = "~/imagenes/toolkit-arrow.png";
            }
        }

        private void BindOrders(string customerId, string tema, GridView grvCom)
        {
            grvCom.ToolTip = "Tema: " + tema;
            grvCom.DataSource = controlfup.ConsultarComentariosTema(Convert.ToInt32(customerId));
            grvCom.DataBind();
        }

        //FLETE
        protected void btnCalcularFlete_Click(object sender, EventArgs e)
        {
            CalcularFlete();
        }

        protected void btnGuardarFlete_Click(object sender, EventArgs e)
        {
            string transp = (string)Session["Transp"];
            string agent = (string)Session["Agent"];
            string vrI1 = (string)Session["VrInt1"];
            string vrI2 = (string)Session["VrInt2"];
            string vrD1 = (string)Session["VrDest1"];
            string vrD2 = (string)Session["VrDest2"];
            string Nombre = (string)Session["Nombre_Usuario"];
            string Seguro = (string)Session["Seguro"];

            int IngFlete = controlfup.IngFlete(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(transp),
                Convert.ToInt32(agent), Convert.ToInt32(cboPtoCargue.SelectedValue), Convert.ToInt32(cboPtoDescargue.SelectedValue),
                Convert.ToInt32(cboTDNFlete.SelectedValue), Convert.ToInt32(lblLTF.Text), Convert.ToInt32(txtCant1.Text), Convert.ToInt32(txtCant2.Text),
                Convert.ToInt32(txtCant3.Text), lblVrTipo1.Text, lblVrTipo2.Text, lblVrTipo3.Text, VrGastPtoOrig.Text, VrDespAduana.Text, vrI1, vrI2,
                VrGastosPtoDest.Text, VrDespAduanalDest.Text, vrD1, vrD2, Nombre, Convert.ToInt32(txtCant3.Text), lblVrTipo4.Text, Seguro);

            string msj = "Flete ingresado con éxito.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + msj + "')", true);
        }       

        private void ConsultarFlete()
        {
            reader = controlfup.CargarContenedorSalida(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            if (reader != null)
            {
               
                while (reader.Read())
                {
                    Session["Cont20"] = reader.GetInt32(0).ToString();
                    Session["Cont40"] = reader.GetInt32(1).ToString();
                    Session["PaisDestino"] = reader.GetString(2).ToString();
                }
               
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();


            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_det_fletes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter transpID = new SqlParameter("@transportador_id", SqlDbType.Int);
                    SqlParameter transp = new SqlParameter("@transportador_texto", SqlDbType.VarChar, 12500);
                    SqlParameter agenteID = new SqlParameter("@agente_carga_id", SqlDbType.Int);
                    SqlParameter agente = new SqlParameter("@agente_carga_texto", SqlDbType.VarChar, 12500);
                    SqlParameter origID = new SqlParameter("@puerto_origen_id", SqlDbType.Int);
                    SqlParameter dest = new SqlParameter("@puerto_destino_id", SqlDbType.Int);
                    SqlParameter tdn = new SqlParameter("@termino_negociacion_id", SqlDbType.Int);
                    SqlParameter leadtime = new SqlParameter("@leadTime", SqlDbType.Int);
                    SqlParameter cant1 = new SqlParameter("@cantidad_t1", SqlDbType.Int);
                    SqlParameter cant2 = new SqlParameter("@cantidad_t2", SqlDbType.Int);
                    SqlParameter cant3 = new SqlParameter("@cantidad_t3", SqlDbType.Int);
                    SqlParameter cant4 = new SqlParameter("@cantidad_t4", SqlDbType.Int);
                    SqlParameter vrOrig1 = new SqlParameter("@vr_origen_t1", SqlDbType.Money);
                    SqlParameter vrOrig2 = new SqlParameter("@vr_origen_t2", SqlDbType.Money);
                    SqlParameter vrOrig3 = new SqlParameter("@vr_origen_t3", SqlDbType.Money);
                    SqlParameter vrOrig4 = new SqlParameter("@vr_origen_t4", SqlDbType.Money);
                    SqlParameter vrGastOrig = new SqlParameter("@vr_gastos_origen", SqlDbType.Money);
                    SqlParameter vrAduaOrig = new SqlParameter("@vr_aduana_origen", SqlDbType.Money);
                    SqlParameter vrInt1 = new SqlParameter("@vr_internacional_t1", SqlDbType.Money);
                    SqlParameter vrInt2 = new SqlParameter("@vr_internacional_t2", SqlDbType.Money);
                    SqlParameter vrGastDest = new SqlParameter("@vr_gastos_destino", SqlDbType.Money);
                    SqlParameter vrAduanDest = new SqlParameter("@vr_aduana_destino", SqlDbType.Money);
                    SqlParameter vrDest1 = new SqlParameter("@vr_destino_t1", SqlDbType.Money);
                    SqlParameter vrDest2 = new SqlParameter("@vr_destino_t2", SqlDbType.Money);
                    SqlParameter vrSeguro = new SqlParameter("@vr_seguro", SqlDbType.Money);

                    transpID.Direction = ParameterDirection.Output;
                    transp.Direction = ParameterDirection.Output;
                    agenteID.Direction = ParameterDirection.Output;
                    agente.Direction = ParameterDirection.Output;
                    origID.Direction = ParameterDirection.Output;
                    dest.Direction = ParameterDirection.Output;
                    tdn.Direction = ParameterDirection.Output;
                    leadtime.Direction = ParameterDirection.Output;
                    cant1.Direction = ParameterDirection.Output;
                    cant2.Direction = ParameterDirection.Output;
                    cant3.Direction = ParameterDirection.Output;
                    cant4.Direction = ParameterDirection.Output;
                    vrOrig1.Direction = ParameterDirection.Output;
                    vrOrig2.Direction = ParameterDirection.Output;
                    vrOrig3.Direction = ParameterDirection.Output;
                    vrOrig4.Direction = ParameterDirection.Output;
                    vrGastOrig.Direction = ParameterDirection.Output;
                    vrAduaOrig.Direction = ParameterDirection.Output;
                    vrInt1.Direction = ParameterDirection.Output;
                    vrInt2.Direction = ParameterDirection.Output;
                    vrGastDest.Direction = ParameterDirection.Output;
                    vrAduanDest.Direction = ParameterDirection.Output;
                    vrDest1.Direction = ParameterDirection.Output;
                    vrDest2.Direction = ParameterDirection.Output;
                    vrSeguro.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(transpID);
                    cmd.Parameters.Add(transp);
                    cmd.Parameters.Add(agenteID);
                    cmd.Parameters.Add(agente);
                    cmd.Parameters.Add(origID);
                    cmd.Parameters.Add(dest);
                    cmd.Parameters.Add(tdn);
                    cmd.Parameters.Add(leadtime);
                    cmd.Parameters.Add(cant1);
                    cmd.Parameters.Add(cant2);
                    cmd.Parameters.Add(cant3);
                    cmd.Parameters.Add(cant4);
                    cmd.Parameters.Add(vrOrig1);
                    cmd.Parameters.Add(vrOrig2);
                    cmd.Parameters.Add(vrOrig3);
                    cmd.Parameters.Add(vrOrig4);
                    cmd.Parameters.Add(vrGastOrig);
                    cmd.Parameters.Add(vrAduaOrig);
                    cmd.Parameters.Add(vrInt1);
                    cmd.Parameters.Add(vrInt2);
                    cmd.Parameters.Add(vrGastDest);
                    cmd.Parameters.Add(vrAduanDest);
                    cmd.Parameters.Add(vrDest1);
                    cmd.Parameters.Add(vrDest2);
                    cmd.Parameters.Add(vrSeguro);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {

                        //VALORES DEL ENCABEZADO 
                        string TranspID = Convert.ToString(transpID.SqlValue);
                        lblTransp.Text = Convert.ToString(transp.SqlValue);
                        string AgentID = Convert.ToString(agenteID.SqlValue);
                        lblAgentCarga.Text = Convert.ToString(agente.SqlValue);
                        cboPtoCargue.SelectedValue = Convert.ToString(origID.SqlValue);
                        cboPtoDescargue.SelectedValue = Convert.ToString(dest.SqlValue);
                        cboTDNFlete.SelectedValue = Convert.ToString(tdn.SqlValue);
                        lblLTF.Text = Convert.ToString(leadtime.SqlValue);
                        txtCant1.Text = Convert.ToString(cant1.SqlValue);
                        txtCant2.Text = Convert.ToString(cant2.SqlValue);
                        txtCant3.Text = Convert.ToString(cant3.SqlValue);
                        txtCant4.Text = Convert.ToString(cant4.SqlValue);
                        lblVrTipo1.Text = Convert.ToString(vrOrig1.SqlValue);
                        lblVrTipo2.Text = Convert.ToString(vrOrig2.SqlValue);
                        lblVrTipo3.Text = Convert.ToString(vrOrig3.SqlValue);
                        lblVrTipo4.Text = Convert.ToString(vrOrig4.SqlValue);
                        decimal vrTransInt = Convert.ToDecimal(lblVrTipo1.Text) + Convert.ToDecimal(lblVrTipo2.Text) + Convert.ToDecimal(lblVrTipo3.Text)
                            + Convert.ToDecimal(lblVrTipo4.Text);
                        VrTransInterno.Text = Convert.ToString(vrTransInt);
                        VrGastPtoOrig.Text = Convert.ToString(vrGastOrig.SqlValue);
                        VrDespAduana.Text = Convert.ToString(vrAduaOrig.SqlValue);
                        string vrI1 = Convert.ToString(vrInt1.SqlValue);
                        string vrI2 = Convert.ToString(vrInt2.SqlValue);
                        decimal vrFleteIntern = Convert.ToDecimal(vrI1) + Convert.ToDecimal(vrI2);
                        VrFleteInt.Text = Convert.ToString(vrFleteIntern);
                        VrGastosPtoDest.Text = Convert.ToString(vrGastDest.SqlValue);
                        VrDespAduanalDest.Text = Convert.ToString(vrAduanDest.SqlValue);
                        string vrD1 = Convert.ToString(vrDest1.SqlValue);
                        string vrD2 = Convert.ToString(vrDest2.SqlValue);
                        decimal vrTranspAduanaDest = Convert.ToDecimal(vrD1) + Convert.ToDecimal(vrD2);
                        vrTranspAduaDest.Text = Convert.ToString(vrTranspAduanaDest);
                        vrTipo3.Text = Convert.ToString(vrDest1.SqlValue);
                        VrTipo4.Text = Convert.ToString(vrDest2.SqlValue);
                        string Seguro = Convert.ToString(vrSeguro.SqlValue);

                        if ((txtCant1.Text == "0")|| (txtCant1.Text == "")) txtCant1.Text = (string)Session["Cont20"]; else txtCant1.Text = Convert.ToString(cant1.SqlValue);
                        if ((txtCant2.Text == "0") || (txtCant2.Text == "")) txtCant2.Text = (string)Session["Cont40"]; else txtCant2.Text = Convert.ToString(cant2.SqlValue);

                        if (cboPais.SelectedItem.Value == "8")
                        {
                            LVehic.Text = "VEHICULOS";
                            LVrFlete.Text = VrTransInterno.Text;
                            LTipoTurbo.Visible = true;
                            txtCant3.Visible = true;
                            lblVrTipo3.Visible = true;
                            //CalcularFlete();
                        }
                        else
                        {

                            LVehic.Text = "CONTENEDORES";
                            LTipoTurbo.Visible = false;
                            txtCant3.Visible = false;
                            lblVrTipo3.Visible = false;
                            LTFPD.Visible = true;
                            lblLTF.Visible = true;
                            LMinimula.Visible = false;
                            txtCant4.Visible = false;
                            lblVrTipo4.Visible = false;
                            LVrFlete.Text = VrFleteInt.Text;
                            //CalcularFlete();
                        }

                        decimal vFlete = Convert.ToDecimal(LVrFlete.Text) + Convert.ToDecimal(Seguro);
                        LVrFlete.Text = Convert.ToString(vFlete.ToString("#,##.##"));

                        if (LVrFlete.Text == "")
                        {
                            LVrFlete.Text = "0";
                        }
                        if (txtTotalValor.Text == "") txtTotalValor.Text = "0";
                        decimal vrTotalFlete = Convert.ToDecimal(LVrFlete.Text) + Convert.ToDecimal(txtTotalValor.Text);
                        LVrTotalFlete.Text = Convert.ToString(vrTotalFlete.ToString("#,##.##"));
                    }
                }
            }
        }

        protected void Flete()
        {
            PoblarTDN();
            PoblarOrigen();
            PoblarDestino();

            //Consulta Ciudad De Obra
            string paiID, ciuID, ciuObra;
            paiID = (string)Session["PaisIDObra"];
            ciuID = (string)Session["CiudadIDObra"];
            ciuObra = (string)Session["CiudadObra"];

            //Diseño Panel Fletes
            LVrEXW.Font.Bold = true;
            LVehic.Font.Bold = true;


            lblVrEXW.Text = txtTotalValor.Text;
            LblCiuObraFlete.Text = ciuObra;

            if (paiID == "8")
            {
                pnlExportacion.Visible = false;
                LTipo1.Text = "Sencillo";
                LTipo3.Text = LTipo1.Text;

                LTipo2.Text = "Tractomula";
                LTipo4.Text = LTipo2.Text;
                if (chkAccesorios.Checked == true)
                {
                    txtCant3.Enabled = true;
                }
            }
            else
            {
                pnlExportacion.Visible = true;
                LTipo1.Text = "Contenedor De 20";
                LTipo3.Text = LTipo1.Text;

                LTipo2.Text = "Contenedor De 40";
                LTipo4.Text = LTipo2.Text;
            }

            ConsultarFlete();
        }

        private void PoblarTDN()
        {
            string idioma = (string)Session["Idioma"];

            cboTDNFlete.Items.Clear();
            cboTDNFlete.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlsf.ConsultarTDN();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboTDNFlete.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarOrigen()
        {
            cboPtoCargue.Items.Clear();
            cboPtoCargue.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlfup.CargarPuertoOrigen();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPtoCargue.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void PoblarDestino()
        {
            cboPtoDescargue.Items.Clear();
            cboPtoDescargue.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlfup.CargarPuertoDestino(Convert.ToInt32(cboPais.SelectedValue));
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPtoDescargue.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }       

        private void CalcularFlete()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[9];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(txtFUP.Text.Trim()));
            sqls[1] = new SqlParameter("@pVersion", cboVersion.SelectedItem.Text.Trim());
            sqls[2] = new SqlParameter("@puerto_origen_id", Convert.ToInt32(cboPtoCargue.SelectedValue));
            sqls[3] = new SqlParameter("@puerto_destino_id", Convert.ToInt32(cboPtoDescargue.SelectedValue));
            sqls[4] = new SqlParameter("@termino_negociacion_id", Convert.ToInt32(cboTDNFlete.SelectedValue));
            sqls[5] = new SqlParameter("@cantidad_t1", Convert.ToInt32(txtCant1.Text));
            sqls[6] = new SqlParameter("@cantidad_t2", Convert.ToInt32(txtCant2.Text));
            sqls[7] = new SqlParameter("@cantidad_t3", Convert.ToInt32(txtCant3.Text));
            sqls[8] = new SqlParameter("@cantidad_t4", Convert.ToInt32(txtCant4.Text));

            string conexion = BdDatos.conexionScope();

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_Calcular_fletes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter transpID = new SqlParameter("@transportador_id", SqlDbType.Int);
                    SqlParameter agenteID = new SqlParameter("@agente_carga_id", SqlDbType.Int);
                    SqlParameter leadtime = new SqlParameter("@leadTime", SqlDbType.Int);
                    SqlParameter vrOrig1 = new SqlParameter("@vr_origen_t1", SqlDbType.Money);
                    SqlParameter vrOrig2 = new SqlParameter("@vr_origen_t2", SqlDbType.Money);
                    SqlParameter vrOrig3 = new SqlParameter("@vr_origen_t3", SqlDbType.Money);
                    SqlParameter vrGastOrig = new SqlParameter("@vr_gastos_origen", SqlDbType.Money);
                    SqlParameter vrAduaOrig = new SqlParameter("@vr_aduana_origen", SqlDbType.Money);
                    SqlParameter vrInt1 = new SqlParameter("@vr_internacional_t1", SqlDbType.Money);
                    SqlParameter vrInt2 = new SqlParameter("@vr_internacional_t2", SqlDbType.Money);
                    SqlParameter vrGastDest = new SqlParameter("@vr_gastos_destino", SqlDbType.Money);
                    SqlParameter vrAduanDest = new SqlParameter("@vr_aduana_destino", SqlDbType.Money);
                    SqlParameter vrDest1 = new SqlParameter("@vr_destino_t1", SqlDbType.Money);
                    SqlParameter vrDest2 = new SqlParameter("@vr_destino_t2", SqlDbType.Money);
                    SqlParameter transp = new SqlParameter("@transportador_texto", SqlDbType.VarChar, 12500);
                    SqlParameter agente = new SqlParameter("@agente_carga_texto", SqlDbType.VarChar, 12500);
                    SqlParameter NoValido = new SqlParameter("@NoValido", SqlDbType.Int);
                    SqlParameter MSG_Validacion = new SqlParameter("@MSG_Validacion", SqlDbType.VarChar, 12500);
                    SqlParameter vrOrig4 = new SqlParameter("@vr_origen_t4", SqlDbType.Money);
                    SqlParameter vrSeguro = new SqlParameter("@vr_seguro", SqlDbType.Money);

                    transpID.Direction = ParameterDirection.Output;
                    agenteID.Direction = ParameterDirection.Output;
                    leadtime.Direction = ParameterDirection.Output;
                    vrOrig1.Direction = ParameterDirection.Output;
                    vrOrig2.Direction = ParameterDirection.Output;
                    vrOrig3.Direction = ParameterDirection.Output;
                    vrGastOrig.Direction = ParameterDirection.Output;
                    vrAduaOrig.Direction = ParameterDirection.Output;
                    vrInt1.Direction = ParameterDirection.Output;
                    vrInt2.Direction = ParameterDirection.Output;
                    vrGastDest.Direction = ParameterDirection.Output;
                    vrAduanDest.Direction = ParameterDirection.Output;
                    vrDest1.Direction = ParameterDirection.Output;
                    vrDest2.Direction = ParameterDirection.Output;
                    transp.Direction = ParameterDirection.Output;
                    agente.Direction = ParameterDirection.Output;
                    NoValido.Direction = ParameterDirection.Output;
                    MSG_Validacion.Direction = ParameterDirection.Output;
                    vrOrig4.Direction = ParameterDirection.Output;
                    vrSeguro.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(transpID);
                    cmd.Parameters.Add(agenteID);
                    cmd.Parameters.Add(leadtime);
                    cmd.Parameters.Add(vrOrig1);
                    cmd.Parameters.Add(vrOrig2);
                    cmd.Parameters.Add(vrOrig3);
                    cmd.Parameters.Add(vrGastOrig);
                    cmd.Parameters.Add(vrAduaOrig);
                    cmd.Parameters.Add(vrInt1);
                    cmd.Parameters.Add(vrInt2);
                    cmd.Parameters.Add(vrGastDest);
                    cmd.Parameters.Add(vrAduanDest);
                    cmd.Parameters.Add(vrDest1);
                    cmd.Parameters.Add(vrDest2);
                    cmd.Parameters.Add(transp);
                    cmd.Parameters.Add(agente);
                    cmd.Parameters.Add(NoValido);
                    cmd.Parameters.Add(MSG_Validacion);
                    cmd.Parameters.Add(vrOrig4);
                    cmd.Parameters.Add(vrSeguro);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string TranspID = Convert.ToString(transpID.SqlValue);
                        lblTransp.Text = Convert.ToString(transp.SqlValue);
                        string AgentID = Convert.ToString(agenteID.SqlValue);
                        lblAgentCarga.Text = Convert.ToString(agente.SqlValue);
                        lblLTF.Text = Convert.ToString(leadtime.SqlValue);
                        lblVrTipo1.Text = Convert.ToString(vrOrig1.SqlValue);
                        lblVrTipo2.Text = Convert.ToString(vrOrig2.SqlValue);
                        lblVrTipo3.Text = Convert.ToString(vrOrig3.SqlValue);
                        lblVrTipo4.Text = Convert.ToString(vrOrig4.SqlValue);
                        decimal vrTransInt = Convert.ToDecimal(lblVrTipo1.Text) + Convert.ToDecimal(lblVrTipo2.Text) + Convert.ToDecimal(lblVrTipo3.Text)
                            + Convert.ToDecimal(lblVrTipo4.Text);
                        VrTransInterno.Text = Convert.ToString(vrTransInt);
                        VrGastPtoOrig.Text = Convert.ToString(vrGastOrig.SqlValue);
                        VrDespAduana.Text = Convert.ToString(vrAduaOrig.SqlValue);
                        string vrI1 = Convert.ToString(vrInt1.SqlValue);
                        string vrI2 = Convert.ToString(vrInt2.SqlValue);
                        decimal vrFleteIntern = Convert.ToDecimal(vrI1) + Convert.ToDecimal(vrI2);
                        VrFleteInt.Text = Convert.ToString(vrFleteIntern);
                        VrGastosPtoDest.Text = Convert.ToString(vrGastDest.SqlValue);
                        VrDespAduanalDest.Text = Convert.ToString(vrAduanDest.SqlValue);
                        string vrD1 = Convert.ToString(vrDest1.SqlValue);
                        string vrD2 = Convert.ToString(vrDest2.SqlValue);
                        decimal vrTranspAduanaDest = Convert.ToDecimal(vrD1) + Convert.ToDecimal(vrD2);
                        vrTranspAduaDest.Text = Convert.ToString(vrTranspAduanaDest);
                        vrTipo3.Text = Convert.ToString(vrDest1.SqlValue);
                        VrTipo4.Text = Convert.ToString(vrDest2.SqlValue);
                        string Seguro = Convert.ToString(vrSeguro.SqlValue);
                        string No_Valido = Convert.ToString(NoValido.SqlValue);
                        string MsjVal = Convert.ToString(MSG_Validacion.SqlValue);

                        if (VrTransInterno.Text == "0.00")
                        {
                            LVrFlete.Text = VrFleteInt.Text;
                            LVehic.Text = "CONTENEDORES";
                        }
                        else
                        {
                            LVrFlete.Text = VrTransInterno.Text;
                            LVehic.Text = "VEHICULOS";
                        }
                        decimal vrTotalFlete = 0, vrTotal = 0;

                        decimal vrFlete = Convert.ToDecimal(LVrFlete.Text.Replace(",", ""));

                        decimal vFlete = vrFlete + Convert.ToDecimal(Seguro);

                        if ((cboTDNFlete.SelectedValue == "0") || (cboTDNFlete.SelectedValue == "1"))
                        {
                            vrTotalFlete = vFlete;
                        }

                        if (cboTDNFlete.SelectedValue == "2")
                        {
                            vrTotalFlete = vFlete + Convert.ToDecimal(VrGastPtoOrig.Text) + Convert.ToDecimal(VrDespAduana.Text);
                        }

                        if (cboTDNFlete.SelectedValue == "3")
                        {
                            vrTotalFlete = vFlete + Convert.ToDecimal(VrGastPtoOrig.Text) + Convert.ToDecimal(VrDespAduana.Text)
                                + vrFleteIntern;
                        }

                        if (cboTDNFlete.SelectedValue == "8")
                        {
                            vrTotalFlete = vFlete + Convert.ToDecimal(VrGastPtoOrig.Text) + Convert.ToDecimal(VrDespAduana.Text)
                                + vrFleteIntern + Convert.ToDecimal(VrGastosPtoDest.Text) + Convert.ToDecimal(VrDespAduanalDest.Text)
                                + vrTranspAduanaDest;
                        }

                        vrTotal = vrTotalFlete + Convert.ToDecimal(txtTotalValor.Text);

                        LVrFlete.Text = Convert.ToString(vrTotalFlete.ToString("#,##.##"));
                        LVrTotalFlete.Text = Convert.ToString(vrTotal.ToString("#,##.##"));

                        Session["Transp"] = TranspID;
                        Session["Agent"] = AgentID;
                        Session["VrInt1"] = vrI1;
                        Session["VrInt2"] = vrI2;
                        Session["VrDest1"] = vrD1;
                        Session["VrDest2"] = vrD2;
                        Session["Seguro"] = Seguro;

                        if (MsjVal.ToString() != "")
                        {
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + MsjVal + "')", true);
                        }
                    }
                }
            }
        }

        public static Boolean IsInt(string fup)
        {
            int result;
            return int.TryParse(fup, out result);
        }

        protected void txtModulaciones_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";

            if (txtModulaciones.Text == "")
            {

            }
            else
            {
                if (IsInt(txtModulaciones.Text) == true)
                {

                }
                else
                {
                    mensaje = "Digite la cantidad modulaciones correctamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtModulaciones.Text = "";
                }

            }
        }

       

        protected void chkRechComer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRechComer.Checked == true)
            {
                lblcotizRechazada.Text = "Cotizacion Rechazada";
                lblcotizRechazada.BackColor = Color.Yellow;
                lblcotizRechazada.Visible = true;

                cboTemaRechCom.Enabled = true;
                txtObservRecCom.Enabled = true;
                btnGuardarRechCom.Enabled = true;
                this.PoblarTemaRechazoComerc();
                SetPane("AccorRechazoComerc");
            }
            else
            {
                cboTemaRechCom.Enabled = false;
                txtObservRecCom.Enabled = false;
                btnGuardarRechCom.Enabled = false;
                lblcotizRechazada.Visible = false;
            }
           
        }

        protected void cboTemarespRechazo_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarRechazoComercial();
        }

        private void PoblarRechazoComercial()
        {
            txtRechazo.Text = "";
            reader = controlfup.ConsultarRechazoxId(Convert.ToInt32(cboTemarespRechazo.SelectedItem.Value));

            while (reader.Read())
            {
                txtRechazo.Text = reader.GetString(1);
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        private void MostrarRespuesta ()
        {
            cboTemarespRechazo.Visible= true;
            txtRechazo.Visible= true;
            txtRespRechazo.Visible= true;
            Label23.Visible= true;
            Label22.Visible = true;
        }

        private void OcultarRespuesta()
        {
            cboTemarespRechazo.Visible = false;
            txtRechazo.Visible = false;
            txtRespRechazo.Visible = false;
            Label23.Visible = false;
            Label22.Visible = false;
        }

        protected void cboClaseCot_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.marcarClaseCotizacion();
            //if(cboClaseCot.SelectedItem.Value.ToString() != "1" )
            //{
            //    txtFupAnt.Enabled = true;
            //    lblfupAnterior.Text = "Fup Anterior * ";
            //}
            //else
            //{
            //    txtFupAnt.Enabled = false;
            //    txtFupAnt.Text = "0";
            //    lblfupAnterior.Text = "Fup Anterior ";
            //}
;        }

        protected void marcarClaseCotizacion()
        {
            if (cboClaseCot.SelectedItem.Value != "0")
            {
                if (cboClaseCot.SelectedItem.Value == "1") lblClase.Text = "APROXIMACION";
                if (cboClaseCot.SelectedItem.Value == "2") lblClase.Text = "AJUSTES";
                if (cboClaseCot.SelectedItem.Value == "3") lblClase.Text = "CIERRE";
            }
            else
            {
                lblClase.Text = "";
            }
        }

        protected void lkrapidisimo_Click(object sender, EventArgs e)
        {
            // Limpiamos la salida
            Response.Clear();
            // Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream";
            // esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=Rapidisimo.xlsx");
            // Escribimos el fichero a enviar 
            Response.WriteFile("Rapido/Rapidisimo.xlsx");
            // volcamos el stream 
            Response.Flush();
            // Enviamos todo el encabezado ahora
            Response.End();



            //// Limpiamos la salida
            //Response.Clear();
            //// Con esto le decimos al browser que la salida sera descargable
            //Response.ContentType = "application/octet-stream";
            //// esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            //Response.AddHeader("Content-Disposition", "attachment; filename=Rapido/1.gif");
            //// Escribimos el fichero a enviar C
            //Response.WriteFile("Rapido/1.gif");
            //// volcamos el stream 
            //Response.Flush();
            //// Enviamos todo el encabezado ahora
            //Response.End();
        }

        protected void lkActualizarOrden_Click(object sender, EventArgs e)
        {
            this.PoblarProducidoEn();
            lblm2Parte.Text = "";
            lblPrecioParte.Text = "";
        }

        protected void cboProdOF_SelectedIndexChanged(object sender, EventArgs e)
        {
            PoblarPartesOF();
        }

        protected void cboParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            PoblarDatosParte();
        }

        private void PoblarDatosParte()
        {           
            reader = controlfup.PoblarDatosParte(Convert.ToInt32(cboParte.SelectedItem.Value));
            while (reader.Read())
            {
                lblm2Parte.Text = reader.GetString(0).ToString();
                lblPrecioParte.Text = reader.GetString(1).ToString();
                lblTipo.Text = " Tipo Orden: " + reader.GetString(2).ToString();
            }

            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }
               
        protected void rutaCartaCotizacion()
        {
            string nombreCarta = "";
            reader = controlfup.ConsultarNombreCarta(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());

            while (reader.Read())
            {
                nombreCarta = reader.GetString(0).ToString();
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            string directorio = @"I:\Planos\" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string dirweb = @"~/Planos/" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"/";
            //string directorio = @"C:\Anexos_" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string mensaje = "";

            directorio = directorio + @"Carta_Cotizacion\" + nombreCarta;
            dirweb = dirweb + @"Carta_Cotizacion/" + nombreCarta;

            if (File.Exists(directorio))
            {
                lblRutaCarta.Text = directorio;
            }
            else
            {
                lblRutaCarta.Text = "No se ha Cargado Carta de Cotizacion";
            }
            CargarGrillaCarta();
        }

        protected void cartaEnviada()
        {
            string nombreCarta = "";
            reader = controlfup.ConsultarNombreCarta(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());

            while (reader.Read())
            {
                nombreCarta = reader.GetString(0).ToString();
            }
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();

            string directorio = @"I:\Planos\" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string dirweb = @"~/Planos/" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"/";
            //string directorio = @"C:\Anexos_" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string mensaje = "";

            directorio = directorio + @"Carta_Cotizacion\" + nombreCarta;
            dirweb = dirweb + @"Carta_Cotizacion/" + nombreCarta;

            if (File.Exists(directorio))
            {
                lblRutaCarta.Text = directorio;
            }
            else
            {
                lblRutaCarta.Text = "No se ha Cargado Carta de Cotizacion";
            }
        }

        protected void btnEnviarCotiz_Click(object sender, EventArgs e)
        {
            int valido = 1;
            string vTipoProy;
            string Nombre = (string)Session["Nombre_Usuario"];
            string nombreCarta = "";
            //1 -> EQUIPO NUEVO; 2 -> ADAPTACIÓN; 3 -> LISTADO 
            vTipoProy = cboTipoCotizacion.SelectedItem.Value;

            //reader = controlfup.ConsultarNombreCarta(Convert.ToInt32(txtFUP.Text),cboVersion.SelectedItem.Text.Trim());        
            
            //while (reader.Read())
            //{
            //   nombreCarta =  reader.GetString(0).ToString();
            //}
            //reader.Close();
            //reader.Dispose();
            //BdDatos.desconectar();
            
            //string directorio = @"I:\Planos\" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            //string dirweb = @"~/Planos/" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"/";
            ////string directorio = @"C:\Anexos_" + txtFUP.Text + cboVersion.SelectedItem.Text.Trim() + @"\";
            string mensaje = "";

            //string rutaCarpeta= dirweb+@"Carta_Cotizacion\";
            //directorio = directorio + @"Carta_Cotizacion\"+nombreCarta;
            //dirweb = dirweb + @"Carta_Cotizacion/"+nombreCarta;            

            //if (File.Exists(directorio))
            //{
            //    Session["rutaCarta"] = directorio;
            //    Session["nombreCarta"] = nombreCarta; 
                Session["Evento"] = 5;
                CorreoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim(), Convert.ToInt32(Session["Evento"]));

                controlfup.actualizarEnvioPlano(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
                mensaje = "Cotizacion Enviada con Exito, el Fup se Actualizo a Estado: COTIZADO";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                ValidarFUP();
                ValidacionGeneralFUP();                
                cargaDatosCambios();
                CargarGrillaCarta();
            //}
            //else
            //{
            //    mensaje = "No Existe Carta de Cotizacion Verifique, No es posible Enviar ni Pasar a Estado Cotizado !";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            //}
        }

        protected void descargar(string ruta)
        {
            //System.IO.FileInfo toDownload =
            //           new System.IO.FileInfo(HttpContext.Current.Server.MapPath(ruta));

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("Content-Disposition",
            //           "attachment; filename=" + toDownload.Name);
            //HttpContext.Current.Response.AddHeader("Content-Length",
            //           toDownload.Length.ToString());
            //HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.WriteFile(ruta);

            //System.IO.FileInfo toDownload =
            //     new System.IO.FileInfo(HttpContext.Current.Server.MapPath(ruta));

            //HttpContext.Current.Response.Clear();
            //HttpContext.Current.Response.AddHeader("Content-Disposition",
            //           "attachment; filename=" + toDownload.Name);
            //HttpContext.Current.Response.AddHeader("Content-Length",
            //           toDownload.Length.ToString());
            //HttpContext.Current.Response.ContentType = "application/octet-stream";
            //HttpContext.Current.Response.WriteFile(ruta);
            //HttpContext.Current.Response.End();

            //////HttpContext.Current.Response.End();

            //try
            //{
            //    WebClient webClient = new WebClient();
            //    webClient.DownloadFile(ruta, @"c:/data.zip");
            //    //webClient.DownloadFile("http://example.org/data.zip", @"c:/data.zip");
            //}
            //catch (Exception ex)
            //{
            //    System.Console.WriteLine("Problem: " + ex.Message);
            //}

            //string remoteUri = "http://www.contoso.com/library/homepage/images/";
            //string fileName = "prueba", myStringWebResource = ruta;
            //// Create a new WebClient instance.
            //WebClient myWebClient = new WebClient();
            //// Concatenate the domain with the Web resource filename.
            ////myStringWebResource = remoteUri + fileName;
            //Console.WriteLine("Downloading File \"{0}\" from \"{1}\" .......\n\n", fileName, myStringWebResource);
            //// Download the Web resource and save it into the current filesystem folder.
            //myWebClient.DownloadFile(myStringWebResource, fileName);
            //Console.WriteLine("Successfully Downloaded File \"{0}\" from \"{1}\"", fileName, myStringWebResource);
            ////Console.WriteLine("\nDownloaded file saved in the following file system folder:\n\t" + Application.StartupPath);
        }

        protected void gridCartas_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //string ruta = "";
            //int index = Convert.ToInt32(e.CommandArgument);
            //GridViewRow row = gridCartas.Rows[index];

            //string id = Convert.ToString(gridCartas.DataKeys[index].Value);
            //ruta = ((LinkButton)row.FindControl("simpa_anexoEditLink")).Text.ToString();
            
            //if (e.CommandName == "Borrar")
            //{
            //    //BORRAR PLANO EN SISTEMA OPERATIVO
            //    if (File.Exists(ruta))
            //    {
            //        File.Delete(ruta);
            //    }

            //    //BORRAR PLANO EN BASE DE DATOS
            //    reader = controlfup.BorrarPlano(Convert.ToInt32(id));

            //    CargarGrillaCarta();
            //}
            //else
            //{
            //    if (e.CommandName == "Descargar")
            //    {
            //        this.descargar(ruta);
            //    }
            //}
        }

        protected void GridDetalleCarta_RowCommand(object sender,
           GridViewCommandEventArgs e)
        {
            // descarga carta
            if (e.CommandName == "Download")
            {
                //Limpia la salida
                Response.Clear();
                Response.ContentType = "application/octect-stream";
                //El atributo "Content-Disposition" con valor "attachment" hará más seguro que el navegador 
                //interprete la respuesta como un documento adjunto a guardar.
                Response.AddHeader("Content-Disposition",
                string.Format("attachment; filename = \"{0}\"", e.CommandArgument));
                //Busca en  la ruta y descarga el archivo
                //elegido en la columna File de la grilla    
                Response.WriteFile((LblRutaArchivo.Text)
                + e.CommandArgument);
                //Termina la descarga
                Response.End();
            }
        }


        private void CargarGrillaCarta()
        {
            dsFUP.Reset();
            DataTable dt1, dt2;
            LblRutaArchivo.Text = "";

            dt2 = controlfup.Consultar_CartasCotiza(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
            if (controlfup.ConsultarArchivoForsa2(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim()).Rows.Count != 0)
            {
                dt1 = controlfup.ConsultarArchivoForsa2(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
                LblRutaArchivo.Text = dt1.Rows[0][6].ToString();
            }
            else
            {

            }
            dsFUP = controlfup.ConsultarCartas(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());

            if (dsFUP != null)
            {
                gridCartas.DataSource = dsFUP.Tables[0];
                gridCartas.DataBind();
                gridCartas.Visible = true;
                //Obtiene la ruta seleccionada en la grilla
                string Ruta = LblRutaArchivo.Text;
                //Remplaza caracteres en la ruta obtenida
                Ruta = Ruta.Replace("~/", "I:/");
                //Establece la ruta final, en el label
                LblRutaArchivo.Text = Ruta;
                //Agrega a la grilla una columna con el nombre de "File"
                DataTable dt = new DataTable();
                dt.Columns.Add("NOMBRE_ARCHIVO");
                //try
                //{

                if (Directory.Exists(Ruta))
                {
                    //Obtiene todos los archivos que esten dentro de la ruta espesificada
                    foreach (string strfile in Directory.GetFiles(Ruta))
                    {
                        FileInfo fi = new FileInfo(strfile);
                        dt.Rows.Add(fi.Name);
                    }

                    //Compara los datos internos entre DateTables y muestra los que coincidan
                    DataTable dtRes = new DataTable();
                    dtRes = dt.AsEnumerable()
                        .Where(r =>
                        dt2.AsEnumerable().Any(w =>
                                 w.Field<string>("NOMBRE_ARCHIVO") == r.Field<string>("NOMBRE_ARCHIVO")))
                                .CopyToDataTable<DataRow>();

                    // Establece los archivos encontrados en la grilla
                    GridDetalleCarta.DataSource = dtRes;
                    GridDetalleCarta.DataBind();
                    GridDetalleCarta.Visible = true;
                    gridCartas.DataBind();
                    gridCartas.Visible = true;
                    lblRutaCarta.Text = "";
                }
                else
                {
                    gridCartas.Dispose();
                    gridCartas.Visible = false;
                    GridDetalleCarta.Dispose();
                    GridDetalleCarta.Visible = false;
                }

            }
            else
            {
                gridCartas.Dispose();
                gridCartas.Visible = false;
                GridDetalleCarta.Dispose();
                GridDetalleCarta.Visible = false;
            }
            dsFUP.Reset();
        }

        //private void CargarGrillaCarta()
        //{
        //    dsFUP.Reset();
        //    dsFUP = controlfup.ConsultarCartas(Convert.ToInt32(txtFUP.Text), cboVersion.SelectedItem.Text.Trim());
        //    if (dsFUP != null)
        //    {
        //        gridCartas.DataSource = dsFUP.Tables[0];
        //        gridCartas.DataBind();
        //        gridCartas.Visible = true;
        //    }
        //    else
        //    {
        //        gridCartas.Dispose();
        //        gridCartas.Visible = false;
        //    }
        //    dsFUP.Reset();
        //}

        protected void chkAluminio_CheckedChanged1(object sender, EventArgs e)
        { 
            if (chkAluminio.Checked == true)
            {
                cboTipoAluminio.Visible = true;
                cboTipoAluminio.SelectedValue = "0";

                cboTipoAcero.SelectedValue = "0"; 
                cboTipoAcero.Visible = false;

                chkAcero.Checked = false;
                chkPlastico.Checked = false;
            }
            else
            {
                cboTipoAluminio.Visible = false;
                cboTipoAluminio.SelectedValue = "0";
            }
        }

        protected void chkAcero_CheckedChanged1(object sender, EventArgs e)
        {
            if (chkAcero.Checked == true)
            {
                cboTipoAcero.Visible = true;
                cboTipoAcero.SelectedValue = "0";

                cboTipoAluminio.SelectedValue = "0";
                cboTipoAluminio.Visible = false;

                chkAluminio.Checked = false;
                chkPlastico.Checked = false;
            }
            else
            {
                cboTipoAcero.SelectedValue = "0";
                cboTipoAcero.Visible = false;
            }
        }

        protected void chkPlastico_CheckedChanged1(object sender, EventArgs e)
        {
            if (chkPlastico.Checked == true)
            {
                chkAluminio.Checked = false;
                chkAcero.Checked = false;

                cboTipoAcero.SelectedValue = "0";
                cboTipoAcero.Visible = false;
                cboTipoAluminio.Visible = false;
                cboTipoAluminio.SelectedValue = "0";
            }
                    
        }

    }
}