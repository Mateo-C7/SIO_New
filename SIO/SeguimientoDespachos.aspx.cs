using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using CapaDatos;

namespace SIO
{
    public partial class SeguimientoDespachos : System.Web.UI.Page
    {
        BdDatos BdDatos = new BdDatos();
        ControlSeguimientoDespachos ctrlseguidespachos = new ControlSeguimientoDespachos();
        SqlDataReader reader = null;
        private DataTable dsDocDespa = new DataTable();

        //Variable global
        DateTime fecEntregaCliente;
        string tipoOrden="";


        protected void Page_Load(object sender, EventArgs e)
        {
            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
            scripManager.RegisterPostBackControl(btnSubirArchivos);
            AccEvento.Visible = false;

            //Oculta campos
            txt_FechaRealLlegaObra.Visible = false;
            Label29.Visible = false;
            txt_FechaConfirmaArribo.Visible = false;
            Label28.Visible = false;
          
            if (!IsPostBack)
            {
                txt_DiasCumpleCliente.Visible = false;
                lbl_diasCumple.Visible = false;
                lbl_PlanEntregaCliente.Visible = false;
                lbl_FecEntreCliente.Visible = false;
           


                if (Session["Rol"] != null)
                {
                    int rol = Convert.ToInt32(Session["Rol"]);
                    int DesC_Id = 0;

                if (Request.QueryString["idDespacho"] != null)
                {
                    DesC_Id = Convert.ToInt32( Request.QueryString["idDespacho"]);
                    Session["idDespacho"] = DesC_Id;
                    lbl_Desc_Id.Text= DesC_Id.ToString();
                    Limpiar_Campos_Despacho();
                    GridDatosOrden.EditIndex = -1;
                    Poblar_Datos_Despacho(DesC_Id);
                    //
                    btn_Filtrar.Enabled = false;
                    Acc_GastosOperaciones.Visible = false;
                    Acc_GastosDestinoBrasil.Visible = false;
                    Acc_DetalleDespaNacional.Visible = false;                        
                    Acc_DetalleDespa.Visible = false;
                    Acc_Observacion.Visible = true;
                    Acc_Observacion.Enabled = false;
                    grid_DtsOrden.Columns[0].Visible = false;
                    GridDatosOrden.Visible = false;
                    grid_DtsOrden.Visible = false;
                    GridTransporte.Visible = false;
                    AcorInfoGeneral.Visible = false;
                   
                        Tbl_Filtros.Visible = false;
                    Tbl_datosp.Visible = false;
                    SetPane("Acc_Datos_Generales");
                }else if 
                        (Request.QueryString["idofa"] != null)
                    {
                        rol = Convert.ToInt32(Session["Rol"]);
                        int idofa = Convert.ToInt32(Request.QueryString["idofa"]);
                  
                        DataTable dt = null;                        

                        dt = ctrlseguidespachos.Obtener_IdDespacho_X_Orden(idofa);

                        //Si el rol es diferente a logistica, oculte los botones y el panel Gastos de Operacion
                        if (rol != 15)
                        {                                                           
                            if (dt.Rows.Count != 0)
                            {
                                DesC_Id = Convert.ToInt32(dt.Rows[0][0].ToString());
                                Session["idDespacho"] = DesC_Id;
                                lbl_Desc_Id.Text = DesC_Id.ToString();
                                GridDatosOrden.EditIndex = -1;
                                Poblar_Datos_Despacho_X_Orden(DesC_Id);

                                //Paneles----------------------------------
                                Acc_GastosOperaciones.Visible = false;
                                Acc_GastosDestinoBrasil.Visible = false;
                                AccEvento.Visible = false;                             
                                Tbl_Filtros.Visible = false;
                                //-----------------------------------------
                                //Botones----------------------------------
                                btn_Filtrar.Enabled = false;
                                btnGuardarDetDesp.Visible = false;
                                btn_GuardarGast.Visible = false;
                                btnSubirArchivos.Visible = false;
                                btn_GuardarDetalleDespaNal.Visible = false;
                                btn_Guardarobserva.Visible = false;
                                btnCancelar.Visible = false;
                                btn_DtsGeneralAct.Visible = false;
                                //-----------------------------------------
                                //Grillas----------------------------------
                        
                                GridDatosOrden.Visible = false;
                                //grid_DtsOrden.Visible = true;
                                //GridTransporte.Visible = true;
                                GridDatosOrden.EditIndex = -1;
                                Tbl_datosp.Visible = false;
                                Tbl_Filtros.Visible = false;
                                //-----------------------------------------
                            }
                            else
                            {
                                //Si la orden no cuenta con despacho, oculta todo el contenido del formulario y muestra un mensaje                              
                                Limpiar_Campos_Despacho();
                                     
                                //Paneles----------------------------------
                                Acc_GastosOperaciones.Visible = false;
                                Acc_DetalleDespaNacional.Visible = false;
                                Acc_GastosDestinoBrasil.Visible = false;
                                Acc_DetalleDespa.Visible = false;
                                Acc_Observacion.Visible = false;
                                Acc_Observacion.Enabled = false;
                                AccEvento.Visible = false;
                                Acc_Datos_Generales.Visible = false;
                                AcorInfoGeneral.Visible = false;
                                                   
                                Tbl_Filtros.Visible = false;                            
                                //-----------------------------------------
                                //Botones----------------------------------
                                btn_Filtrar.Enabled = false;
                                //-----------------------------------------
                                //Grillas----------------------------------
                                grid_DtsOrden.Columns[0].Visible = false;
                                GridDatosOrden.Visible = false;
                                grid_DtsOrden.Visible = false;
                                GridTransporte.Visible = false;
                                GridDatosOrden.EditIndex = -1;
                                Grid_DocAnexo.Visible = true;
                                //-----------------------------------------
                                mensajeVentana("Esta orden no cuenta con un despacho.");
                            }
                            }
                        else
                        {

                        }
                    }

                        PoblarTipoAnexo();
                // Limpiar_Campos_Despacho();
                // Carga las plantas asociadas a el usuario
                    CargarPlanta();
                    //-----------------------------------------
                    cbo_FiltMes.Items.Add(new ListItem("Seleccione", "0"));
                    ctrlseguidespachos.Listar_Mes(cbo_FiltMes);
                    cbo_FiltroMesCrea.Items.Add(new ListItem("Seleccione", "0"));
                    ctrlseguidespachos.Listar_Mes(cbo_FiltroMesCrea);
                    ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
                    ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
                    cbo_FiltEstatusCarga.Items.Add(new ListItem("Seleccione", "0"));
                    ctrlseguidespachos.Listar_EstatusCarga(cbo_FiltEstatusCarga);
                    ctrlseguidespachos.Listar_EstatusCarga(cbo_EstatusCargaDet);
                    lbl_tdn_1.Visible = false;
                    cbo_Tdn.Visible = false;
                    // Listar años
                    cbo_FiltAño.Items.Add(new ListItem("Seleccione", "0"));
                    cbo_FiltAño.Items.Add(new ListItem("2018", "1"));
                    cbo_FiltAño.Items.Add(new ListItem("2019", "2"));
                    cbo_FiltroAñoCrea.Items.Add(new ListItem("Seleccione", "0"));
                    cbo_FiltroAñoCrea.Items.Add(new ListItem("2018", "1"));
                    cbo_FiltroAñoCrea.Items.Add(new ListItem("2019", "2"));

                    // Listar tipo despacho
                    cbo_TipoDespacho.Items.Add(new ListItem("Seleccione", "0"));
                    cbo_TipoDespacho.Items.Add(new ListItem("Nacional", "True"));
                    cbo_TipoDespacho.Items.Add(new ListItem("Exportacion", "False"));

                    //Listar Tipo Canal
                    cbo_Canal.Items.Add(new ListItem("Seleccione", "0"));
                    cbo_Canal.Items.Add(new ListItem("Verde", "1"));
                    cbo_Canal.Items.Add(new ListItem("Amarillo", "2"));
                    cbo_Canal.Items.Add(new ListItem("Rojo", "3"));
                    cbo_Canal.Items.Add(new ListItem("Pendiente", "4"));
                    cbo_Canal.Items.Add(new ListItem("No Aplica", "5"));

                    //Valida decimales en los campos, gastos de operación
                    txt_ValorFob.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                    txt_ValorExw.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteNalProvi.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteInterReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_AgradeAduanProvi.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                    txt_AgradeAduanReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_GastPuertoProvision.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_GastPuertoReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteInterProvision.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteInterReal.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                    txt_GastDestinoProvision.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_GastDestinoReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_SelloSateliProvi.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_SelloSateliReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_TotalProvi.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_TotalGastFletreal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_StandBy.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_TrmFechafact.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_BodegajeReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_PesoFactura.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteRealNal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_StanByNal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ValorExwNal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ValorTolFactura.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_FleteCotiNal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_Seguro.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_inspAntiNarcot.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ManejoFlete.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_TranspDestino.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_AduanaDestino.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_DemoraDestino.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_RollOver.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_BodegajeDestino.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ImpuestoDestino.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    //Valida decimales en los campos, gastos en destino brasil
                    txt_trm.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_liberaHblProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_liberaHblReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_droppOffProvi.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_droppOffReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_taxaAdminProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_taxaAdminReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ispsProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_ispsReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_otrosGastosProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_1erPeriodoProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_1erPeriodoReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_2doPeriodoProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_2doPeriodoReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_3erPeriodoProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_3erPeriodoReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_escanContenProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_escanContenReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_insoMapaProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_insoMapaReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_corretagenProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_corretagenReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_demurrageProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_demurrageReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_despaHonoraProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_despaHonoraReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_margenTimboProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_margenTimboReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_fleteTerresProv.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");
                    txt_fleteTerresReal.Attributes.Add("onKeyPress", " return   validedecimal(event,this);");

                    // Valida enteros en el campo            
                    txt_DexFmm.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
                    txt_NoGuiaDtos.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
                    txt_TiempoEnvioDtos.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
                    txt_Indicador.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
                    txt_Dias_libres.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");

                    Cargar_GridView_Despachos(int.Parse(cbo_Planta.SelectedValue));

                    Inhabilitar_Campos(false);
                    Inhabilitar_Campos_Tramites(true);
                    Campos_Solo_Lectura();
                    chk_CumpleEntregaNal.Enabled = false;

            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }

         }      
        


public void Campos_Solo_Lectura()
        {
            ////Panel Datos Generales
            txt_MesDespacho.ReadOnly = true;
            txt_Zona.ReadOnly = true;
            txt_PaisDestino.ReadOnly = true;
            txt_Ciudad.ReadOnly = true;
            txt_ValorTolFactura.ReadOnly = true;
            txt_M2.ReadOnly = true;
            txt_Indicador.ReadOnly = true;
            //-------------------------------------
           
            //Panel Detalle Despacho
            txt_TtInternacional.ReadOnly = true;
            txt_PuertoDestCliente.ReadOnly = true;         
            txt_EfectividadEnvioDtos.ReadOnly = true;
            txt_LeadTimeEspera.ReadOnly = true;
            txt_EfectiviEntrega.ReadOnly = true;
            txt_TtPlantaCliente.ReadOnly = true;           
            //-------------------------------------

            //Panel Gastos Operacionales
            txt_TotalProvi.ReadOnly = true;
            txt_TotalGastFletreal.ReadOnly = true;
            txt_Difprovi.ReadOnly = true;
            txt_PorcentajeAhorro.ReadOnly = true;
            txt_GastFletFacturados.Enabled = false;  
            txt_Concatenar.ReadOnly = true;
            //-------------------------------------

            //Panel Detalle Despacho Nacional
            txt_OrdenNal.ReadOnly = true;
            txt_ClienteNal.ReadOnly = true;
            txt_CiudadDestinoNal.ReadOnly = true;         
            txt_ValorExwNal.ReadOnly = true;
            txt_FleteCotiNal.ReadOnly = true;
            txt_RelFleTValorNal.ReadOnly = true;
            txt_PesoNal.ReadOnly = true;
      
        }


        public void Cargar_DtsOrden_(int desc_Id)
        {
           DataSet dt = ctrlseguidespachos.Cargar_DtsOrden_(desc_Id);

            grid_DtsOrden.DataSource = ctrlseguidespachos.Cargar_DtsOrden_(desc_Id);
            grid_DtsOrden.DataMember = ctrlseguidespachos.Cargar_DtsOrden_(desc_Id).Tables[0].ToString();
            grid_DtsOrden.DataBind();

            bool diferente = false;    

            for (int i = 0; i < dt.Tables[0].Rows.Count - 1; i++)
            {
                if (dt.Tables[0].Rows[i]["Tdn"].ToString() == dt.Tables[0].Rows[i + 1]["Tdn"].ToString())
                {
                    // acciones.
                }
                else
                {
                    if (dt.Tables[0].Rows[i]["idTdn"].ToString() == "0" || dt.Tables[0].Rows[i + 1]["idTdn"].ToString() == "0")
                    {

                    }
                    else
                    {
                        diferente = true;
                    }
                }
            }

            if (diferente == true)
            {
                btn_DtsGeneralAct.Visible = false;
                lbl_idTdn.Text = "0";
                lbl_tdn.Text = "";
                lbl_MsjValidaTdn.Text = "Alerta - El Tdn de las órdenes asociadas es diferente, comuníquese con comercial.";
            }
            else
            {
                btn_DtsGeneralAct.Visible = true;
                lbl_MsjValidaTdn.Text = "";
                lbl_tdn.Text = dt.Tables[0].Rows[0][5].ToString().Trim();
                lbl_idTdn.Text = dt.Tables[0].Rows[0][6].ToString().Trim();           
            }
            grid_DtsOrden.DataBind();          
            ctrlseguidespachos.CerrarConexion();
            Calcular_Concatenar();
        }


        /*Este metodo fue reemplazado por  public void Cargar_DtsOrden_(int desc_Id), ya que
         con este no se podia formatear el valor comercial a tipo moneda, pero funciona correctamente*/
        private void Cargar_DtsOrden(int desc_Id)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Orden");
            dt.Columns.Add("Obra");
            dt.Columns.Add("M2");
            dt.Columns.Add("Valor_Comercial");
            dt.Columns.Add("Tdn");
            dt.Columns.Add("idTdn");
            dt.Columns.Add("Destino");
            dt.Columns.Add("Flete_Cotizado");
            dt.Columns.Add("Peso");
            dt.Columns.Add("ciu_id");
            dt.Columns.Add("cli_id");
       

            reader = ctrlseguidespachos.Cargar_DtsOrden(desc_Id);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Cliente"] = reader.GetValue(0).ToString();
                    row["Orden"] = reader.GetValue(1).ToString();
                    row["Obra"] = reader.GetValue(2).ToString();
                    row["M2"] = reader.GetValue(3).ToString();
                    row["Valor_Comercial"] = reader.GetValue(4).ToString();
                    row["Tdn"] = reader.GetValue(5).ToString();
                    row["idTdn"] = reader.GetValue(6).ToString();
                    row["Destino"] = reader.GetValue(7).ToString();
                    row["Flete_Cotizado"] = reader.GetValue(8).ToString();
                    row["Peso"] = reader.GetValue(9).ToString();
                    row["ciu_id"] = reader.GetValue(10).ToString();
                    row["cli_id"] = reader.GetValue(11).ToString();                  

                    dt.Rows.Add(row);                            
                }           
            }
           
            grid_DtsOrden.Dispose();
            grid_DtsOrden.DataSource = dt;

            bool diferente = false;
            string tdn1="";

            for (int i = 0; i < dt.Rows.Count - 1; i++)
            {
                if (dt.Rows[i]["Tdn"].ToString() == dt.Rows[i + 1]["Tdn"].ToString())
                {
                    // acciones.
                
                }
                else
                {
                    if(dt.Rows[i]["idTdn"].ToString() =="0" || dt.Rows[i + 1]["idTdn"].ToString() == "0")
                    {
                  
                    }
                    else
                    {
                        diferente = true;
                    }                         
                }
            }             
       
            if (diferente == true)
            {
                btn_DtsGeneralAct.Visible = false;
                lbl_idTdn.Text = "0";
                lbl_tdn.Text = "";
                lbl_MsjValidaTdn.Text= "Alerta - El Tdn de las órdenes asociadas es diferente, comuníquese con comercial.";
            }
            else
            {
                btn_DtsGeneralAct.Visible = true;
                lbl_MsjValidaTdn.Text = "";
                lbl_tdn.Text = dt.Rows[0][5].ToString().Trim();
                lbl_idTdn.Text = dt.Rows[0][6].ToString().Trim();
                //if (lbl_idTdn.Text=="0")
                //{
                //    lbl_MsjValidaTdn.Text = "Alerta - Establezca el Tdn para este despacho";
                //    cbo_Tdn.Items.Clear();                  
                //    ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
                //    cbo_Tdn.Items.RemoveAt(0);
                //    lbl_tdn_1.Visible = true;
                //    cbo_Tdn.Visible = true;
                //}
                //else
                //{
                //    lbl_MsjValidaTdn.Text = "";
                //    lbl_tdn_1.Visible =false;
                //    cbo_Tdn.Visible = false;
                //}
            }
            grid_DtsOrden.DataBind();
            reader.Close();
            reader.Dispose();
            ctrlseguidespachos.CerrarConexion();
            Calcular_Concatenar();
        }

        public void Calcular_Concatenar()
        {
            if (lbl_tdn.Text != "N/A" && !String.IsNullOrEmpty(lbl_tdn.Text))
            {
                txt_Concatenar.Text = (txt_PaisDestino.Text.ToUpper() + "" + lbl_tdn.Text + "" + cbo_MedioTrasnporte.SelectedItem.ToString().ToUpper());
                txt_Concatenar.Text.ToUpper();
            }
            else
            {
                txt_Concatenar.Text = "";
            }
        }

        private void Cargar_DtsTrasnporte(int desc_Id)
        {
            GridTransporte.DataSource = ctrlseguidespachos.Cargar_DtsTrasnporte(desc_Id);
            GridTransporte.DataMember = ctrlseguidespachos.Cargar_DtsTrasnporte(desc_Id).Tables[0].ToString();
            GridTransporte.DataBind();
        }

        public void Poblar_observaciones_Despa(int desc_id)
        {
            DataSet ds;

            ds = ctrlseguidespachos.Poblar_observaciones_Despa(desc_id, txt_OrdenNal.Text);

            if (ds.Tables[0].Rows.Count != 0)
            {
                grid_Observaciones.DataSource =ctrlseguidespachos.Poblar_observaciones_Despa(desc_id,txt_OrdenNal.Text);
                grid_Observaciones.DataMember = ctrlseguidespachos.Poblar_observaciones_Despa(desc_id, txt_OrdenNal.Text).Tables[0].ToString();
                grid_Observaciones.DataBind();

                int rol = Convert.ToInt32(Session["Rol"]);

                if (rol != 15)
                {
                    grid_Observaciones.Columns[6].Visible = false;
                }
                else
                {
                    grid_Observaciones.Columns[6].Visible = true;
                }

            }
            else
            {
                grid_Observaciones.DataSource = null;
                grid_Observaciones.DataMember = null;
                grid_Observaciones.DataBind();
            }             
        }


        public void Poblar_GastosOperacionales(int desc_id)
        {
            DataTable dt;
            String cerrado;
            int rol = Convert.ToInt32(Session["Rol"]);


            reader = ctrlseguidespachos.Poblar_GastosOperacionales(desc_id);
            reader.Read();

            if (reader.HasRows == true)
            {
               decimal FleteNalProvi = reader.GetDecimal(1);
                txt_FleteNalProvi.Text = Convert.ToString(FleteNalProvi.ToString("#,##.##"));
                decimal FletNalReal= reader.GetDecimal(2);
                txt_FletNalReal.Text = Convert.ToString(FletNalReal.ToString("#,##.##"));
                decimal AgradeAduanProvi= reader.GetDecimal(3);
                txt_AgradeAduanProvi.Text = Convert.ToString(AgradeAduanProvi.ToString("#,##.##"));
                decimal AgradeAduanReal= reader.GetDecimal(4);
                txt_AgradeAduanReal.Text = Convert.ToString(AgradeAduanReal.ToString("#,##.##"));
                decimal GastPuertoProvision = reader.GetDecimal(5);
                txt_GastPuertoProvision.Text = Convert.ToString(GastPuertoProvision.ToString("#,##.##"));
                decimal GastPuertoReal= reader.GetDecimal(6);
                txt_GastPuertoReal.Text = Convert.ToString(GastPuertoReal.ToString("#,##.##"));
                decimal FleteInterProvision = reader.GetDecimal(7);
                txt_FleteInterProvision.Text = Convert.ToString(FleteInterProvision.ToString("#,##.##"));
                decimal FleteInterReal= reader.GetDecimal(8);
                txt_FleteInterReal.Text = Convert.ToString(FleteInterReal.ToString("#,##.##"));
                decimal GastDestinoProvision = reader.GetDecimal(9);
                txt_GastDestinoProvision.Text = Convert.ToString(GastDestinoProvision.ToString("#,##.##"));
                decimal GastDestinoReal= reader.GetDecimal(10);
                txt_GastDestinoReal.Text = Convert.ToString(GastDestinoReal.ToString("#,##.##"));
                decimal SelloSateliProvi= reader.GetDecimal(11);
                txt_SelloSateliProvi.Text = Convert.ToString(SelloSateliProvi.ToString("#,##.##"));
                decimal SelloSateliReal = reader.GetDecimal(12);
                txt_SelloSateliReal.Text = Convert.ToString(SelloSateliReal.ToString("#,##.##"));
                decimal TotalProvi = reader.GetDecimal(13);
                txt_TotalProvi.Text =  Convert.ToString(TotalProvi.ToString("#,##.##"));
                decimal TotalGastFletreal = reader.GetDecimal(14);
                txt_TotalGastFletreal.Text = Convert.ToString(TotalGastFletreal.ToString("#,##.##"));
                decimal Difprovi = reader.GetDecimal(15);
                txt_Difprovi.Text = Convert.ToString(Difprovi.ToString("#,##.##"));
                txt_PorcentajeAhorro.Text = reader.GetValue(16).ToString();
                decimal StandBy = reader.GetDecimal(17);
                txt_StandBy.Text = Convert.ToString(StandBy.ToString("#,##.##"));
                decimal TrmFechafact = reader.GetDecimal(18);
                txt_TrmFechafact.Text = Convert.ToString(TrmFechafact.ToString("#,##.##"));
                decimal BodegajeReal = reader.GetDecimal(19);
                txt_BodegajeReal.Text = Convert.ToString(BodegajeReal.ToString("#,##.##"));
                decimal PesoFactura = reader.GetDecimal(20);
                txt_PesoFactura.Text = Convert.ToString(PesoFactura.ToString("#,##.##"));
                txt_EnvioCtrlEmpaque.Text = reader.GetSqlDateTime(21).Value.ToString("dd/MM/yyyy");
                if (txt_EnvioCtrlEmpaque.Text == "01/01/1900") txt_EnvioCtrlEmpaque.Text = "";
                txt_Concatenar.Text = reader.GetValue(22).ToString();
                decimal GastFleteFact = reader.GetDecimal(24);
                txt_GastFletFacturados.Text = Convert.ToString(GastFleteFact.ToString("#,##.##"));
                decimal Seguro = reader.GetDecimal(25);
                txt_Seguro.Text = Convert.ToString(Seguro.ToString("#,##.##"));
                decimal InspAntiNar = reader.GetDecimal(26);
                txt_inspAntiNarcot.Text = Convert.ToString(InspAntiNar.ToString("#,##.##"));
                decimal RollOver = reader.GetDecimal(27);
                txt_RollOver.Text = Convert.ToString(RollOver.ToString("#,##.##"));
                decimal ManejoFlete = reader.GetDecimal(28);
                txt_ManejoFlete.Text = Convert.ToString(ManejoFlete.ToString("#,##.##"));
                decimal AduanaDest = reader.GetDecimal(29);
                txt_AduanaDestino.Text = Convert.ToString(AduanaDest.ToString("#,##.##"));
                decimal BodegDest = reader.GetDecimal(30);
                txt_BodegajeDestino.Text = Convert.ToString(BodegDest.ToString("#,##.##"));
                decimal TransDest = reader.GetDecimal(31);
                txt_TranspDestino.Text = Convert.ToString(TransDest.ToString("#,##.##"));
                decimal DemoraDest = reader.GetDecimal(32);
                txt_DemoraDestino.Text = Convert.ToString(DemoraDest.ToString("#,##.##"));
                decimal ImpuestDest = reader.GetDecimal(33);
                txt_ImpuestoDestino.Text = Convert.ToString(ImpuestDest.ToString("#,##.##"));

                if (rol != 53)
                {
                    cerrado = reader.GetValue(23).ToString();
                    if (cerrado == "True")
                    {
                        chk_Cerrado.Checked = true;
                        Inhabilitar_Campos_Dtos_GastosOperacional(true);
                    }
                    else
                    {
                        chk_Cerrado.Checked = false;
                        Inhabilitar_Campos_Dtos_GastosOperacional(false);
                    }
                }                             
                btn_GuardarGast.Text = "Actualizar";
                Validar_Campos_Dtos_Gastos();
            }                    
            else
            {
               btn_GuardarGast.Text = "Guardar";
            }
            reader.Close();
            ctrlseguidespachos.CerrarConexion();
            Calcular_Gastos_Fletes_Facturados();
        }
          
        public void Inhabilitar_Campos_Dtos_GastosOperacional(bool IN)
        {
            if (IN == true)
            {
                //Datos Gastos Operacionales
                txt_FleteNalProvi.Enabled = false;
                txt_FletNalReal.Enabled = false;
                txt_AgradeAduanProvi.Enabled = false;
                txt_AgradeAduanReal.Enabled = false;
                txt_GastPuertoProvision.Enabled = false;
                txt_GastPuertoReal.Enabled = false;
                txt_FleteInterProvision.Enabled = false;
                txt_FleteInterReal.Enabled = false;
                txt_GastDestinoProvision.Enabled = false;
                txt_GastDestinoReal.Enabled = false;
                txt_SelloSateliProvi.Enabled = false;
                txt_SelloSateliReal.Enabled = false;
                txt_TotalProvi.Enabled = false;
                txt_TotalGastFletreal.Enabled = false;
                txt_StandBy.Enabled = false;
                txt_TrmFechafact.Enabled = false;
                txt_BodegajeReal.Enabled = false;
                txt_PesoFactura.Enabled = false;
                txt_Difprovi.Enabled = false;
                txt_PorcentajeAhorro.Enabled = false;
                txt_Concatenar.Enabled = false;
                txt_EnvioCtrlEmpaque.Enabled = false;
                chk_Cerrado.Enabled = false;
                btn_GuardarGast.Enabled = false;
                txt_Seguro.Enabled = false;
                txt_inspAntiNarcot.Enabled = false;
                txt_RollOver.Enabled = false;
                txt_ManejoFlete.Enabled = false;
                txt_AduanaDestino.Enabled = false;
                txt_BodegajeDestino.Enabled = false;
                txt_TranspDestino.Enabled = false;
                txt_DemoraDestino.Enabled = false;
                txt_ImpuestoDestino.Enabled = false;
            }
            else
            {
                //Datos Gastos Operacionales
                txt_FleteNalProvi.Enabled = true;
                txt_FletNalReal.Enabled = true;
                txt_AgradeAduanProvi.Enabled = true;
                txt_AgradeAduanReal.Enabled = true;
                txt_GastPuertoProvision.Enabled = true;
                txt_GastPuertoReal.Enabled = true;
                txt_FleteInterProvision.Enabled = true;
                txt_FleteInterReal.Enabled = true;
                txt_GastDestinoProvision.Enabled = true;
                txt_GastDestinoReal.Enabled = true;
                txt_SelloSateliProvi.Enabled = true;
                txt_SelloSateliReal.Enabled = true;
                txt_TotalProvi.Enabled = true;
                txt_TotalGastFletreal.Enabled = true;
                txt_StandBy.Enabled = true;
                txt_TrmFechafact.Enabled = true;
                txt_BodegajeReal.Enabled = true;
                txt_PesoFactura.Enabled = true;
                txt_Difprovi.Enabled = true;
                txt_PorcentajeAhorro.Enabled = true;
                txt_Concatenar.Enabled = true;
                txt_EnvioCtrlEmpaque.Enabled = true;
                chk_Cerrado.Enabled = true;
                btn_GuardarGast.Enabled = true;
                txt_Seguro.Enabled = true;
                txt_inspAntiNarcot.Enabled = true;
                txt_RollOver.Enabled = true;
                txt_ManejoFlete.Enabled = true;
                txt_AduanaDestino.Enabled = true;
                txt_BodegajeDestino.Enabled = true;
                txt_TranspDestino.Enabled = true;
                txt_DemoraDestino.Enabled = true;
                txt_ImpuestoDestino.Enabled = true;
            }
        }

        public void Inhabilitar_CamposGastos_Destino_Brasil(bool IN)
        {
            if (IN == true)
            {
                txt_noProceso.Enabled = false;
                txt_fechPlanilla.Enabled = false;
                txt_trm.Enabled = false;
                txt_liberaHblProv.Enabled = false;
                txt_liberaHblReal.Enabled = false;
                txt_droppOffProvi.Enabled = false;
                txt_droppOffReal.Enabled = false;
                txt_taxaAdminProv.Enabled = false;
                txt_taxaAdminReal.Enabled = false;
                txt_ispsProv.Enabled = false;
                txt_ispsReal.Enabled = false;
                txt_otrosGastosProv.Enabled = false;
                txt_otrosGastosReal.Enabled = false;
                txt_1erPeriodoProv.Enabled = false;
                txt_1erPeriodoReal.Enabled = false;
                txt_2doPeriodoProv.Enabled = false;
                txt_2doPeriodoReal.Enabled = false;
                txt_3erPeriodoProv.Enabled = false;
                txt_3erPeriodoReal.Enabled = false;
                txt_escanContenProv.Enabled = false;
                txt_escanContenReal.Enabled = false;
                txt_insoMapaProv.Enabled = false;
                txt_insoMapaReal.Enabled = false;
                txt_corretagenProv.Enabled = false;
                txt_corretagenReal.Enabled = false;
                txt_demurrageProv.Enabled = false;
                txt_demurrageReal.Enabled = false;
                txt_despaHonoraProv.Enabled = false;
                txt_despaHonoraReal.Enabled = false;
                txt_margenTimboProv.Enabled = false;
                txt_margenTimboReal.Enabled = false;
                txt_fleteTerresProv.Enabled = false;
                txt_fleteTerresReal.Enabled = false;
                btn_GuardarGastDespDestBrasil.Enabled = false;
                chk_Cerrado_DespaDestBrasil.Enabled = false;
            }
            else
            {
                txt_noProceso.Enabled = true;
                txt_fechPlanilla.Enabled = true;
                txt_trm.Enabled = true;
                txt_liberaHblProv.Enabled = true;
                txt_liberaHblReal.Enabled = true;
                txt_droppOffProvi.Enabled = true;
                txt_droppOffReal.Enabled = true;
                txt_taxaAdminProv.Enabled = true;
                txt_taxaAdminReal.Enabled = true;
                txt_ispsProv.Enabled = true;
                txt_ispsReal.Enabled = true;
                txt_otrosGastosProv.Enabled = true;
                txt_otrosGastosReal.Enabled = true;
                txt_1erPeriodoProv.Enabled = true;
                txt_1erPeriodoReal.Enabled = true;
                txt_2doPeriodoProv.Enabled = true;
                txt_2doPeriodoReal.Enabled = true;
                txt_3erPeriodoProv.Enabled = true;
                txt_3erPeriodoReal.Enabled = true;
                txt_escanContenProv.Enabled = true;
                txt_escanContenReal.Enabled = true;
                txt_insoMapaProv.Enabled = true;
                txt_insoMapaReal.Enabled = true;
                txt_corretagenProv.Enabled = true;
                txt_corretagenReal.Enabled = true;
                txt_demurrageProv.Enabled = true;
                txt_demurrageReal.Enabled = true;
                txt_despaHonoraProv.Enabled = true;
                txt_despaHonoraReal.Enabled = true;
                txt_margenTimboProv.Enabled = true;
                txt_margenTimboReal.Enabled = true;
                txt_fleteTerresProv.Enabled = true;
                txt_fleteTerresReal.Enabled = true;
                btn_GuardarGastDespDestBrasil.Enabled = true;
                chk_Cerrado_DespaDestBrasil.Enabled =  true;
            }
        }



        private void Cargar_Dts_DetalleDespacho(int desc_id)
        {
            DataTable dt;
            int rol = Convert.ToInt32(Session["Rol"]);
            String inpeccion, EfectiviEntrega,detalleCerrado, detTramiteCerrado;

            reader = ctrlseguidespachos.Poblar_Detalle_Despacho(desc_id);
            
         
            if (reader.HasRows==true)
            {
                reader.Read();
                txt_DocumentTrasnGuia.Text = reader.GetValue(0).ToString();
                txt_DexFmm.Text = reader.GetValue(1).ToString();
                txt_NoGuiaDtos.Text = reader.GetValue(2).ToString();
                cbo_EstatusCargaDet.Items.Clear();
                ctrlseguidespachos.Listar_EstatusCarga(cbo_EstatusCargaDet);
                cbo_EstatusCargaDet.SelectedValue = reader.GetValue(3).ToString();            
                txt_Embarcador.Text = reader.GetValue(4).ToString();             
                txtFechaEnvioDtos.Text = reader.GetSqlDateTime(5).Value.ToString("dd/MM/yyyy");
                if (txtFechaEnvioDtos.Text == "01/01/1900") txtFechaEnvioDtos.Text = "";
                txt_TiempoEnvioDtos.Text = reader.GetValue(6).ToString();
                txt_EfectividadEnvioDtos.Text = reader.GetValue(7).ToString();
                if (txt_EfectividadEnvioDtos.Text == "255")
                {
                    txt_EfectividadEnvioDtos.Text = "";
                }
                else if (txt_EfectividadEnvioDtos.Text == "1")
                {
                    txt_EfectividadEnvioDtos.Text = "SI";
                }
                else if (txt_EfectividadEnvioDtos.Text == "0")
                {
                    txt_EfectividadEnvioDtos.Text = "NO";
                }

                inpeccion = reader.GetValue(8).ToString();
                if (inpeccion == "True")
                {
                    chk_InspeccionSi.Checked = true;
                }
                else
                {
                    chk_InspeccionSi.Checked = false;
                }


                if (rol != 53)
                {
                    detalleCerrado = reader.GetValue(23).ToString();
                    if (detalleCerrado == "True")
                    {
                        chk_DetalleCerrado.Checked = true;
                        Inhabilitar_Campos_Detalle_Despa(true);
                        btnGuardarDetDesp.Enabled = true;
                    }
                    else
                    {
                        chk_DetalleCerrado.Checked = false;
                        Inhabilitar_Campos_Detalle_Despa(false);
                    }
                }
                else
                {

                }

                detTramiteCerrado = reader.GetValue(39).ToString();
                if (detTramiteCerrado == "True")
                {
                    chk_Tramite_Cerrado.Checked = true;
                    Inhabilitar_Campos_Tramites(true);
                }
                else
                {
                    chk_Tramite_Cerrado.Checked = false;
                    Inhabilitar_Campos_Tramites(false);
                }

                txt_FechaEstimaDespa.Text = reader.GetSqlDateTime(9).Value.ToString("dd/MM/yyyy");
                if (txt_FechaEstimaDespa.Text == "01/01/1900") txt_FechaEstimaDespa.Text = "";
                txt_FechaRealDespa.Text = reader.GetSqlDateTime(10).Value.ToString("dd/MM/yyyy");
                if (txt_FechaRealDespa.Text == "01/01/1900") txt_FechaRealDespa.Text = "";
                txt_FechaEstimaZarpe.Text = reader.GetSqlDateTime(11).Value.ToString("dd/MM/yyyy");
                if (txt_FechaEstimaZarpe.Text == "01/01/1900") txt_FechaEstimaZarpe.Text = "";
                txt_FechaRealZarpe.Text = reader.GetSqlDateTime(12).Value.ToString("dd/MM/yyyy");
                if (txt_FechaRealZarpe.Text == "01/01/1900") txt_FechaRealZarpe.Text = "";
                txt_EstimadaArribo.Text = reader.GetSqlDateTime(13).Value.ToString("dd/MM/yyyy");
                if (txt_EstimadaArribo.Text == "01/01/1900") txt_EstimadaArribo.Text = "";
                txt_FechaConfirmaArribo.Text = reader.GetSqlDateTime(14).Value.ToString("dd/MM/yyyy");
                if (txt_FechaConfirmaArribo.Text == "01/01/1900") txt_FechaConfirmaArribo.Text = "";
                txt_FechaEstLlegaObra.Text = reader.GetSqlDateTime(15).Value.ToString("dd/MM/yyyy");
                if (txt_FechaEstLlegaObra.Text == "01/01/1900") txt_FechaEstLlegaObra.Text = "";
                txt_FechaRealLlegaObra.Text = reader.GetSqlDateTime(16).Value.ToString("dd/MM/yyyy");
                if (txt_FechaRealLlegaObra.Text == "01/01/1900") txt_FechaRealLlegaObra.Text = "";
                txt_TtInternacional.Text = reader.GetValue(17).ToString();
                txt_PuertoDestCliente.Text = reader.GetValue(18).ToString();
                txt_TtPlantaCliente.Text = reader.GetValue(19).ToString();
                EfectiviEntrega = reader.GetValue(20).ToString();
                txt_fechaDocumentado.Text = reader.GetSqlDateTime(40).Value.ToString("dd/MM/yyyy");
                if (txt_fechaDocumentado.Text == "01/01/1900") txt_fechaDocumentado.Text = "";
                txt_ModTransDespa.Text = reader.GetValue(41).ToString();
                txt_EstimadaArriboMod.Text = reader.GetSqlDateTime(42).Value.ToString("dd/MM/yyyy");
                if (txt_EstimadaArriboMod.Text == "01/01/1900") txt_EstimadaArriboMod.Text = "";
                txt_FechaEstLlegaObraMod.Text = reader.GetSqlDateTime(43).Value.ToString("dd/MM/yyyy");
                if (txt_FechaEstLlegaObraMod.Text == "01/01/1900") txt_FechaEstLlegaObraMod.Text = "";

                //Campos Tramites
                txt_Finalizacion.Text = reader.GetSqlDateTime(24).Value.ToString("dd/MM/yyyy");
                if (txt_Finalizacion.Text == "01/01/1900") txt_Finalizacion.Text = "";           
                txt_Dias_libres.Text = reader.GetValue(25).ToString();
                if (txt_Dias_libres.Text == "255")
                {
                    txt_Dias_libres.Text = "";
                }
                txt_Inspeccion.Text = reader.GetSqlDateTime(26).Value.ToString("dd/MM/yyyy");
                if (txt_Inspeccion.Text == "01/01/1900") txt_Inspeccion.Text = "";               
                cbo_Canal.Items.Clear();
                //Listar Tipo Canal
                cbo_Canal.Items.Add(new ListItem("Seleccione", "0"));
                cbo_Canal.Items.Add(new ListItem("Verde", "1"));
                cbo_Canal.Items.Add(new ListItem("Amarillo", "2"));
                cbo_Canal.Items.Add(new ListItem("Rojo", "3"));
                cbo_Canal.Items.Add(new ListItem("Pendiente", "4"));
                cbo_Canal.Items.Add(new ListItem("No Aplica", "5"));
                string canal = reader.GetValue(27).ToString();
                if (String.IsNullOrEmpty(canal))
                {
                    cbo_Canal.SelectedValue = "0";
                }
                else
                {
                    cbo_Canal.SelectedValue = reader.GetValue(27).ToString();
                }
                txt_Nacionalizacion.Text = reader.GetSqlDateTime(28).Value.ToString("dd/MM/yyyy");
                if (txt_Nacionalizacion.Text == "01/01/1900") txt_Nacionalizacion.Text = "";
                txt_FacturacionProveed.Text = reader.GetSqlDateTime(29).Value.ToString("dd/MM/yyyy");
                if (txt_FacturacionProveed.Text == "01/01/1900") txt_FacturacionProveed.Text = "";
                txt_CI_impuestos.Text = reader.GetSqlDateTime(30).Value.ToString("dd/MM/yyyy");
                if (txt_CI_impuestos.Text == "01/01/1900") txt_CI_impuestos.Text = "";
                txt_Retiro_Conten.Text = reader.GetSqlDateTime(31).Value.ToString("dd/MM/yyyy");
                if (txt_Retiro_Conten.Text == "01/01/1900") txt_Retiro_Conten.Text = "";
                txt_Desove.Text = reader.GetSqlDateTime(32).Value.ToString("dd/MM/yyyy");
                if (txt_Desove.Text == "01/01/1900") txt_Desove.Text = "";
                txt_Almacenamiento.Text = reader.GetSqlDateTime(33).Value.ToString("dd/MM/yyyy");
                if (txt_Almacenamiento.Text == "01/01/1900") txt_Almacenamiento.Text = "";
                txt_Notifica_Cliente.Text = reader.GetSqlDateTime(34).Value.ToString("dd/MM/yyyy");
                if (txt_Notifica_Cliente.Text == "01/01/1900") txt_Notifica_Cliente.Text = "";
                txt_Devolucion_Conten.Text = reader.GetSqlDateTime(35).Value.ToString("dd/MM/yyyy");
                if (txt_Devolucion_Conten.Text == "01/01/1900") txt_Devolucion_Conten.Text = "";
                txt_Facturacion_ForsaCliente.Text = reader.GetSqlDateTime(36).Value.ToString("dd/MM/yyyy");
                if (txt_Facturacion_ForsaCliente.Text == "01/01/1900") txt_Facturacion_ForsaCliente.Text = "";
                txt_Fecha_Carga_Cliente.Text = reader.GetSqlDateTime(37).Value.ToString("dd/MM/yyyy");
                if (txt_Fecha_Carga_Cliente.Text == "01/01/1900") txt_Fecha_Carga_Cliente.Text = "";
                txt_Entrega_Obra.Text = reader.GetSqlDateTime(38).Value.ToString("dd/MM/yyyy");
                if (txt_Entrega_Obra.Text == "01/01/1900") txt_Entrega_Obra.Text = "";
                //---------------

                if (String.IsNullOrEmpty(EfectiviEntrega) || EfectiviEntrega == "255")
                {
                    txt_EfectiviEntrega.Text = "";
                }                
                else  if (EfectiviEntrega == "1")
                {
                    txt_EfectiviEntrega.Text = "SI";
                }
                else
                {
                    txt_EfectiviEntrega.Text = "NO";
                }
            
                if (txt_NoGuiaDtos.Text == "-1")
                {
                    txt_NoGuiaDtos.Text = "";
                }             
                if (txt_DexFmm.Text == "-1")
                {
                    txt_DexFmm.Text = "";
                }
                                           
                btnGuardarDetDesp.Text = "Actualizar";
            }
            else
            {
                btnGuardarDetDesp.Text = "Guardar";
            }
            Valida_Despacho_CerradoCompleto();
            reader.Close();
            ctrlseguidespachos.CerrarConexion();

        }

        public void Inhabilitar_Campos_Detalle_Despa(bool IN)
        {

            if (IN == true)
            {
                //Datos Detalle Despacho
                txt_DocumentTrasnGuia.Enabled = false;
                txt_DexFmm.Enabled = false;
                txt_NoGuiaDtos.Enabled = false;        
                txt_Embarcador.Enabled = false;
                txtFechaEnvioDtos.Enabled = false;
                txt_TiempoEnvioDtos.Enabled = false;
                txt_EfectividadEnvioDtos.Enabled = false;
                chk_InspeccionSi.Enabled = false;
                txt_FechaEstimaDespa.Enabled = false;
                txt_FechaEstimaZarpe.Enabled = false;
                txt_EstimadaArribo.Enabled = false;
                txt_FechaEstLlegaObra.Enabled = false;
                txt_TtInternacional.Enabled = false;
                txt_FechaRealDespa.Enabled = false;
                txt_FechaRealZarpe.Enabled = false;
                txt_FechaConfirmaArribo.Enabled = false;
                txt_FechaRealLlegaObra.Enabled = false;
                txt_PuertoDestCliente.Enabled = false;
                txt_TtPlantaCliente.Enabled = false;
                txt_EfectiviEntrega.Enabled = false;
                txt_LeadTimeEspera.Enabled = false;              
                chk_DetalleCerrado.Enabled = false;
                btnGuardarDetDesp.Enabled = false;   
                cbo_Tdn.Enabled = false;
                txt_DiasCumpleCliente.Enabled = false;
                txt_fechaDocumentado.Enabled = false;
                txt_ModTransDespa.Enabled = false;
            }
            else
            {                
                txt_DocumentTrasnGuia.Enabled = true;
                txt_DexFmm.Enabled = true;
                txt_NoGuiaDtos.Enabled = true;      
                txt_Embarcador.Enabled = true;
                txtFechaEnvioDtos.Enabled = true;
                txt_TiempoEnvioDtos.Enabled = true;
                txt_EfectividadEnvioDtos.Enabled = true;
                chk_InspeccionSi.Enabled = true;
                txt_FechaEstimaDespa.Enabled = true;
                txt_FechaEstimaZarpe.Enabled = true;
                txt_EstimadaArribo.Enabled = true;
                txt_FechaEstLlegaObra.Enabled = true;
                txt_TtInternacional.Enabled = true;
                txt_FechaRealDespa.Enabled = true;
                txt_FechaRealZarpe.Enabled = true;
                txt_FechaConfirmaArribo.Enabled = true;
                txt_FechaRealLlegaObra.Enabled = true;
                txt_PuertoDestCliente.Enabled = true;
                txt_TtPlantaCliente.Enabled = true;
                txt_EfectiviEntrega.Enabled = true;
                txt_LeadTimeEspera.Enabled = true;
                chk_DetalleCerrado.Enabled = true;
                btnGuardarDetDesp.Enabled = true;
                cbo_EstatusCargaDet.Enabled = true;
                cbo_Tdn.Enabled = true;
                txt_DiasCumpleCliente.Enabled = true;
                txt_fechaDocumentado.Enabled = true;
                txt_ModTransDespa.Enabled = true;
            }
       
        }

        private void Cargar_GridView_Despachos(int plantaDespacho)
        {
            string fechaDespacho, fechaCreaDespacho;

            DataTable dt = new DataTable();
            dt.Columns.Add("Año_Despacho");
            dt.Columns.Add("Mes_Despacho");
            dt.Columns.Add("DesC_Id");
            dt.Columns.Add("NumDespacho");
            dt.Columns.Add("FechaCreaDespacho");
            dt.Columns.Add("FechaDespacho");
            dt.Columns.Add("Cliente");
            dt.Columns.Add("Orden");
            dt.Columns.Add("Ordenes");
            dt.Columns.Add("Facturas");
            dt.Columns.Add("ZonaCliente");
            dt.Columns.Add("PaisCliente");
            dt.Columns.Add("CiudadCliente");     
            dt.Columns.Add("DesC_Nal");
            dt.Columns.Add("DesDt_Estatus_Carga");
            dt.Columns.Add("planta_id");
            dt.Columns.Add("año_Crea");
            dt.Columns.Add("mes_Crea");


            reader = ctrlseguidespachos.Cargar_GridView(plantaDespacho);

            if (reader.HasRows)
            {               
                    while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["Año_Despacho"] = reader.GetValue(0).ToString();
                    row["Mes_Despacho"] = reader.GetValue(1).ToString();
                    row["DesC_Id"] = reader.GetValue(2).ToString();
                    row["NumDespacho"] = reader.GetValue(3).ToString();
                    fechaCreaDespacho = reader.GetValue(4).ToString();
                    fechaDespacho = reader.GetValue(5).ToString();
                    lbl_FechaDespacho.Text = reader.GetValue(5).ToString();
                 

                    if (String.IsNullOrEmpty(fechaCreaDespacho))
                    {
                        row["FechaCreaDespacho"] = reader.GetValue(4).ToString();
                    }
                    else
                    {
                        row["FechaCreaDespacho"] = Convert.ToDateTime(reader.GetValue(4)).ToString("dd/MM/yyyy");
                    }
                    if (String.IsNullOrEmpty(fechaDespacho))
                    {
                        row["FechaDespacho"] = reader.GetValue(5).ToString();
                    }
                    else
                    {
                        row["FechaDespacho"] = Convert.ToDateTime(reader.GetValue(5)).ToString("dd/MM/yyyy");
                    }
                    row["Cliente"] = reader.GetValue(6).ToString();
                    row["Orden"] = reader.GetValue(7).ToString();
                    row["Ordenes"] = reader.GetValue(8).ToString();
                    row["Facturas"] = reader.GetValue(9).ToString();
                    row["ZonaCliente"] = reader.GetValue(10).ToString();
                    row["PaisCliente"] = reader.GetValue(11).ToString();
                    row["CiudadCliente"] = reader.GetValue(12).ToString();       
                    row["DesC_Nal"] = reader.GetValue(13).ToString();
                    row["DesDt_Estatus_Carga"] = reader.GetValue(14).ToString();
                    row["planta_id"] = reader.GetValue(15).ToString();
                    row["año_Crea"] = reader.GetValue(16).ToString();
                    row["mes_Crea"] = reader.GetValue(17).ToString();
                    dt.Rows.Add(row);
                }
            }
            GridDatosOrden.Dispose();
            GridDatosOrden.DataSource = dt;
            GridDatosOrden.DataBind();
            Session["TbReporte"] = dt;
            reader.Close();
            reader.Dispose();
            ctrlseguidespachos.CerrarConexion();
        }

        protected void btn_Filtrar_Click(object sender, EventArgs e)
        {
            Cargar_GridView_Despachos(int.Parse(cbo_Planta.SelectedValue));
            GridDatosOrden.EditIndex = -1;
            Limpiar_Campos_Despacho();
            txt_ValorExw.ReadOnly = true;
            cbo_TipoVehiculo.Visible = true;
            lbl_Tipo_Vehiculo.Visible = true;
            Inhabilitar_Campos(false);
            lbl_Acc_Observacion.Text = "Observaciones/Despacho";
            // Traemos la tabla y su encabezado
            DataTable dtreporte = Session["TbReporte"] as DataTable;
            if (dtreporte != null)
            {
                DataView dv = new DataView(dtreporte);
                String cadena = "";

                if (txt_FiltFactura.Text.Equals("") && txt_FiltOrden.Text.Equals("") && cbo_FiltMes.SelectedValue.Equals(" ")
                    && cbo_FiltAño.SelectedValue.Equals(" ") && cbo_FiltEstatusCarga.SelectedValue.Equals(" ") && cbo_TipoDespacho.SelectedValue.Equals(" "))
                {
                    cadena = "";
                }
                if (!txt_FiltOrden.Text.Equals(""))
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  Ordenes   LIKE '%" + txt_FiltOrden.Text.Trim().ToUpper() + "%'";
                    }
                    else
                    {
                        cadena = "Ordenes  LIKE '%" + txt_FiltOrden.Text.Trim().ToUpper() + "%'";
                    }
                }
                if (!txt_FiltFactura.Text.Equals(""))
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND Facturas   LIKE '%" + txt_FiltFactura.Text.Trim().ToUpper() + "%'";
                    }
                    else
                    {
                        cadena = "Facturas  LIKE '%" + txt_FiltFactura.Text.Trim().ToUpper() + "%'";
                    }
                }
                if (!cbo_FiltAño.SelectedItem.Text.Equals(" ") && cbo_FiltAño.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  Año_Despacho = '" + cbo_FiltAño.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " Año_Despacho = '" + cbo_FiltAño.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                }
                if (!cbo_FiltMes.SelectedItem.Text.Equals(" ") && cbo_FiltMes.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  Mes_Despacho = '" + cbo_FiltMes.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " Mes_Despacho = '" + cbo_FiltMes.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                }

                if (!cbo_FiltroAñoCrea.SelectedItem.Text.Equals(" ") && cbo_FiltroAñoCrea.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  año_Crea = '" + cbo_FiltroAñoCrea.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " año_Crea = '" + cbo_FiltroAñoCrea.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                }
                if (!cbo_FiltroMesCrea.SelectedItem.Text.Equals(" ") && cbo_FiltroMesCrea.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  mes_Crea = '" + cbo_FiltroMesCrea.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " mes_Crea = '" + cbo_FiltroMesCrea.SelectedItem.Text.Trim().ToUpper() + "'";
                    }
                }                
                if (!cbo_TipoDespacho.SelectedItem.Text.Equals(" ") && cbo_TipoDespacho.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  DesC_Nal = '" + cbo_TipoDespacho.SelectedValue.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " DesC_Nal = '" + cbo_TipoDespacho.SelectedValue.Trim().ToUpper() + "'";
                    }
                }
                if (!cbo_FiltEstatusCarga.SelectedItem.Text.Equals(" ") && cbo_FiltEstatusCarga.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  DesDt_Estatus_Carga = '" + cbo_FiltEstatusCarga.SelectedValue.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " DesDt_Estatus_Carga = '" + cbo_FiltEstatusCarga.SelectedValue.Trim().ToUpper() + "'";
                    }
                }
                if (chk_SinDespacho.Checked==true)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  FechaDespacho = ''";
                    }
                    else
                    {
                        cadena = " FechaDespacho=''";
                    }
                }
                if (!cbo_Planta.SelectedItem.Text.Equals(" ") && cbo_Planta.SelectedIndex != 0)
                {
                    if (!cadena.Equals(""))
                    {
                        cadena += " AND  planta_id = '" + cbo_Planta.SelectedValue.Trim().ToUpper() + "'";
                    }
                    else
                    {
                        cadena = " planta_id = '" + cbo_Planta.SelectedValue.Trim().ToUpper() + "'";
                    }
                }
                if (!cadena.Equals(""))
                {
                    dv.RowFilter = cadena;
                    Session.Add("TbReporte", dv.ToTable());
                }
                SetPane("Acc_Datos_Generales");
                Reload_Reporte();
            }
        }
        private void Reload_Reporte()
        {
            DataTable dt = Session["TbReporte"] as DataTable;
            GridDatosOrden.DataSource = dt;
            GridDatosOrden.DataBind();
            GridDatosOrden.EditIndex = -1;
        }

        protected void btn_CancelaBusqueda_Click(object sender, EventArgs e)
        {
            txt_FiltFactura.Text = "";
            txt_FiltOrden.Text = "";
            cbo_FiltMes.SelectedIndex = 0;
            cbo_FiltAño.SelectedIndex = 0;
            cbo_FiltroMesCrea.SelectedIndex = 0;
            cbo_FiltroAñoCrea.SelectedIndex = 0;
            cbo_TipoDespacho.SelectedIndex = 0;
            cbo_FiltEstatusCarga.SelectedIndex = 0;
            chk_SinDespacho.Checked = false;
        }

        protected void GridDatosOrden_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridDatosOrden.EditIndex = -1;
            Reload_Reporte();
            GridDatosOrden.PageIndex = e.NewPageIndex;
            this.GridDatosOrden.DataBind();
        }

        protected void GridDatosOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable dt;
            DataTable factura = new DataTable();
            DataTable ordenObra = new DataTable();
            lbl_Acc_Observacion.Text = "Observaciones/Despacho";

            Limpiar_Campos_Despacho();

            GridDatosOrden.EditIndex = -1;

            if (e.CommandName == "Select")
            {
                // Se obtiene indice de la row seleccionada

                int index = Convert.ToInt32(e.CommandArgument);
                // Obtengo el id de la entidad que se esta en seleccion
                

                int DesC_Id = Convert.ToInt32(GridDatosOrden.DataKeys[index].Value);
          
                //pasamos el id a el label para utilizarlo como variable global
                lbl_Desc_Id.Text = DesC_Id.ToString();

                Poblar_Datos_Despacho(DesC_Id);
            }
        }

        public void Poblar_Datos_Despacho_X_Orden(int DesC_Id)
        {
            reader = ctrlseguidespachos.PoblarDatosGenerales(Convert.ToInt32(DesC_Id));
            if (reader.HasRows == true)
            {
                reader.Read();
                txt_TiempoEnvioDtos.ReadOnly = true;
                string Exw = "0.0000", Fob = "0.0000", TotFac = "0.0000";
                decimal ValorExw = 0, ValorFob = 0, ValorTotFac = 0;


                lbl_IdDespacho.Text = reader.GetValue(0).ToString();
                txt_MesDespacho.Text = reader.GetValue(1).ToString();
                if (txt_MesDespacho.Text == "0")
                {
                    txt_MesDespacho.Text = "";
                }
                txt_Zona.Text = reader.GetValue(2).ToString();
                txt_PaisDestino.Text = reader.GetValue(3).ToString();
                txt_Ciudad.Text = reader.GetValue(4).ToString();
                lst_Facturas.Items.Add(new ListItem(reader.GetValue(6).ToString()));
                txt_M2.Text = reader.GetValue(13).ToString();

                if (Exw != reader.GetValue(5).ToString())
                {
                    ValorExw = reader.GetDecimal(5);
                    txt_ValorExw.Text = Convert.ToString(ValorExw.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorExw.Text = "0";
                }
                if (Fob != reader.GetValue(10).ToString())
                {
                    ValorFob = reader.GetDecimal(10);
                    txt_ValorFob.Text = Convert.ToString(ValorFob.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorFob.Text = "0";
                }
                if (TotFac != reader.GetValue(16).ToString())
                {
                    ValorTotFac = reader.GetDecimal(16);
                    txt_ValorTolFactura.Text = Convert.ToString(ValorTotFac.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorTolFactura.Text = "0";
                }

                lbl_IdPais.Text = reader.GetValue(14).ToString();
                lbl_FechaDespacho.Text = reader.GetValue(18).ToString();

                //deshabilita la escritura del campo txt_ValorExw.
             
                    txt_ValorExw.ReadOnly = false;
                    txt_ValorTolFactura.ReadOnly = false;
            
                int id_tdn = int.Parse(reader.GetValue(15).ToString());
                cbo_Tdn.Items.Clear();
                ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
                cbo_Tdn.SelectedValue = reader.GetValue(15).ToString();

                string mediotransp = reader.GetValue(12).ToString();
                mediotransp = mediotransp.ToUpper();

                //True Nacional, False Exportacion
                string tipoDespa = reader.GetValue(17).ToString();
                SetPane("AcorInfoGeneral");

                if (tipoDespa == "True")
                {
                    Acc_DetalleDespa.Visible = false;
                    Acc_GastosOperaciones.Visible = false;
                    Acc_DetalleDespaNacional.Visible = true;
                    cbo_MedioTrasnporte.Enabled = false;
                    cbo_TipoVehiculo.Enabled = false;
                    cbo_TipoVehiculo.Visible = true;
                    lbl_Tipo_Vehiculo.Visible = true;
                }
                else
                {
                    Acc_DetalleDespaNacional.Visible = false;
                    Acc_DetalleDespa.Visible = true;
                    Acc_GastosOperaciones.Visible = false;
                    cbo_MedioTrasnporte.Enabled = true;
                    cbo_TipoVehiculo.Visible = false;
                    lbl_Tipo_Vehiculo.Visible = false;
                    cbo_MedioTrasnporte.SelectedIndex = 2;
                }

                lbl_IdMedTrasnp.Text = reader.GetValue(7).ToString();
                if (lbl_IdMedTrasnp.Text != "0")
                {
                    //btnGuardarDetDesp.Visible = true;
                    //btn_GuardarGast.Visible = true;
                    //btnSubirArchivos.Visible = true;
                    //btn_GuardarDetalleDespaNal.Visible = true;
                    //btn_Guardarobserva.Visible = true;
                    //btnCancelar.Visible = true;

                    cbo_MedioTrasnporte.Items.Clear();
                    ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
                    cbo_MedioTrasnporte.SelectedValue = reader.GetValue(7).ToString();
                    cbo_TipoVehiculo.Items.Clear();
                    ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
                    cbo_TipoVehiculo.SelectedValue = reader.GetValue(8).ToString();
                }
                else
                {
                   
                }

               cbo_TipoVehiculo.Items.Clear();
                ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(lbl_IdMedTrasnp.Text));

                Inhabilitar_Campos(false);
               
                if (reader.HasRows == true)
                {
              
                    Cargar_DtsOrden_(DesC_Id);
                   
                    if (tipoDespa == "True")
                    {
                        grid_DtsOrden.Columns[0].Visible = true;
                        cbo_MedioTrasnporte.Enabled = false;
                        cbo_TipoVehiculo.Enabled = false;
                        cbo_TipoVehiculo.Visible = true;
                        lbl_Tipo_Vehiculo.Visible = true;
                    }
                    else
                    {
                        grid_DtsOrden.Columns[0].Visible = false;
                        cbo_MedioTrasnporte.Enabled = true;
                        cbo_TipoVehiculo.Enabled = true;
                        cbo_TipoVehiculo.Visible = false;
                        lbl_Tipo_Vehiculo.Visible = false;
                    }
                    Cargar_DtsTrasnporte(DesC_Id);
                    Cargar_Dts_DetalleDespacho(DesC_Id);
                    //Poblar_GastosOperacionales(DesC_Id);
                    Consultar_Doc_Despacho(DesC_Id);
                    Calcular_Gastos_Fletes_Facturados();
                    Calcular_DiffProvisiones();
                    Calcular_Ahorro();
                    Poblar_observaciones_Despa(DesC_Id);
                    Obtener_LeadTime();
                    SetPane("AcorInfoGeneral");
                }
            }
            #region No borrar este metodo, en caso de revertir se utilizara, el el mismo de arriba con algunas modificaciones
            //    reader = ctrlseguidespachos.PoblarDatosGenerales(Convert.ToInt32(DesC_Id));
            //    reader.Read();
            //    txt_TiempoEnvioDtos.ReadOnly = true;

            //    lbl_IdDespacho.Text = reader.GetValue(0).ToString();
            //    txt_MesDespacho.Text = reader.GetValue(1).ToString();
            //    if (txt_MesDespacho.Text == "0")
            //    {
            //        txt_MesDespacho.Text = "";
            //    }
            //    txt_Zona.Text = reader.GetValue(2).ToString();
            //    txt_PaisDestino.Text = reader.GetValue(3).ToString();
            //    txt_Ciudad.Text = reader.GetValue(4).ToString();
            //    lst_Facturas.Items.Add(new ListItem(reader.GetValue(6).ToString()));
            //    txt_M2.Text = reader.GetValue(13).ToString();
            //    decimal ValorExw = reader.GetDecimal(5);
            //    decimal ValorFob = reader.GetDecimal(10);
            //    decimal ValorTotFac = reader.GetDecimal(16);
            //    txt_ValorExw.Text = Convert.ToString(ValorExw.ToString("#,##.##"));
            //    txt_ValorFob.Text = Convert.ToString(ValorFob.ToString("#,##.##"));
            //    txt_ValorTolFactura.Text = Convert.ToString(ValorTotFac.ToString("#,##.##"));
            //    lbl_IdPais.Text = reader.GetValue(14).ToString();
            //    lbl_FechaDespacho.Text = reader.GetValue(18).ToString();
            //    if (!String.IsNullOrEmpty(lbl_FechaDespacho.Text)) { txt_FechaEntregaNal.Enabled = true; }
            //    else { txt_FechaEntregaNal.Enabled = false; }

            //    //habilita la escritura del campo txt_ValorExw, si el pais es  brasil y mexico
            //    if (lbl_IdPais.Text == "6" || lbl_IdPais.Text == "21")
            //    {
            //        txt_ValorExw.ReadOnly = false;
            //        txt_ValorTolFactura.ReadOnly = false;
            //    }
            //    else
            //    {
            //        txt_ValorExw.ReadOnly = true;
            //        txt_ValorTolFactura.ReadOnly = true;
            //    }
            //    int id_tdn = int.Parse(reader.GetValue(15).ToString());
            //    cbo_Tdn.Items.Clear();
            //    ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
            //    cbo_Tdn.SelectedValue = reader.GetValue(15).ToString();

            //    string mediotransp = reader.GetValue(12).ToString();
            //    mediotransp = mediotransp.ToUpper();

            //    //True Nacional, False Exportacion
            //    string tipoDespa = reader.GetValue(17).ToString();
            //    SetPane("AcorInfoGeneral");

            //    if (tipoDespa == "True")
            //    {
            //        Acc_DetalleDespa.Visible = false;
            //        Acc_GastosOperaciones.Visible = false;
            //        Acc_DetalleDespaNacional.Visible = true;
            //        cbo_MedioTrasnporte.Enabled = false;
            //        cbo_TipoVehiculo.Enabled = false;
            //        cbo_TipoVehiculo.Visible = true;
            //        lbl_Tipo_Vehiculo.Visible = true;
            //    }
            //    else
            //    {
            //        Acc_DetalleDespaNacional.Visible = false;
            //        Acc_DetalleDespa.Visible = true;
            //        Acc_GastosOperaciones.Visible = true;
            //        cbo_MedioTrasnporte.Enabled = true;
            //        cbo_TipoVehiculo.Visible = false;
            //        lbl_Tipo_Vehiculo.Visible = false;
            //        cbo_MedioTrasnporte.SelectedIndex = 2;
            //    }

            //    lbl_IdMedTrasnp.Text = reader.GetValue(7).ToString();
            //    if (lbl_IdMedTrasnp.Text != "0")
            //    {
            //        cbo_MedioTrasnporte.Items.Clear();
            //        ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
            //        cbo_MedioTrasnporte.SelectedValue = reader.GetValue(7).ToString();
            //        cbo_TipoVehiculo.Items.Clear();
            //        ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
            //        cbo_TipoVehiculo.SelectedValue = reader.GetValue(8).ToString();
            //    }
            //    cbo_TipoVehiculo.Items.Clear();
            //    ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));

            //    Inhabilitar_Campos(true);
            //    Acc_DetalleDespaNacional.Enabled = false;

            //    if (reader.HasRows == true)
            //    {
            //        //Cargar_DtsOrden(DesC_Id);
            //        Cargar_DtsOrden_(DesC_Id);

            //        if (tipoDespa == "True")
            //        {
            //            grid_DtsOrden.Columns[0].Visible = true;
            //            cbo_MedioTrasnporte.Enabled = false;
            //            cbo_TipoVehiculo.Enabled = false;
            //            cbo_TipoVehiculo.Visible = true;
            //            lbl_Tipo_Vehiculo.Visible = true;
            //        }
            //        else
            //        {
            //            grid_DtsOrden.Columns[0].Visible = false;
            //            cbo_MedioTrasnporte.Enabled = true;
            //            cbo_TipoVehiculo.Enabled = true;
            //            cbo_TipoVehiculo.Visible = false;
            //            lbl_Tipo_Vehiculo.Visible = false;
            //        }
            //        Cargar_DtsTrasnporte(DesC_Id);
            //        Cargar_Dts_DetalleDespacho(DesC_Id);
            //        Poblar_GastosOperacionales(DesC_Id);
            //        Consultar_Doc_Despacho(DesC_Id);
            //       // Calcular_Gastos_Fletes_Facturados();
            //        Calcular_DiffProvisiones();
            //        Calcular_Ahorro();
            //        Poblar_observaciones_Despa(DesC_Id);
            //        Obtener_LeadTime();
            //        SetPane("AcorInfoGeneral");
            //    }
            //}
            #endregion
            reader.Close();
            reader.Dispose();
            ctrlseguidespachos.CerrarConexion();
        }



        public void Poblar_Datos_Despacho(int DesC_Id)
        {
            int rol = Convert.ToInt32(Session["Rol"]);
            reader = ctrlseguidespachos.PoblarDatosGenerales(Convert.ToInt32(DesC_Id));
            if (reader.HasRows == true)
            {
                reader.Read();
                txt_TiempoEnvioDtos.ReadOnly = true;
                string Exw = "0.0000", Fob = "0.0000", TotFac = "0.0000";
                decimal ValorExw = 0, ValorFob = 0, ValorTotFac = 0;          
                DateTime fechaActualSistema;

                tipoOrden= reader.GetValue(21).ToString();

                lbl_IdDespacho.Text = reader.GetValue(0).ToString();
      
                txt_MesDespacho.Text = reader.GetValue(1).ToString();
                if (txt_MesDespacho.Text == "0")
                {
                    txt_MesDespacho.Text = "";
                }
                txt_Zona.Text = reader.GetValue(2).ToString();
                txt_PaisDestino.Text = reader.GetValue(3).ToString();
                txt_Ciudad.Text = reader.GetValue(4).ToString();
                lst_Facturas.Items.Add(new ListItem(reader.GetValue(6).ToString()));
                txt_M2.Text = reader.GetValue(13).ToString();
                txt_Puerto_Origen.Text= reader.GetValue(22).ToString();
                txt_Puerto_Destino.Text= reader.GetValue(23).ToString();

                if (Exw != reader.GetValue(9).ToString())
                {
                    ValorExw = reader.GetDecimal(9);
                    txt_ValorExw.Text = Convert.ToString(ValorExw.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorExw.Text = "0";
                }
                if (Fob != reader.GetValue(10).ToString())
                {
                    ValorFob = reader.GetDecimal(10);
                    txt_ValorFob.Text = Convert.ToString(ValorFob.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorFob.Text = "0";
                }
                if (TotFac != reader.GetValue(19).ToString())
                {
                    ValorTotFac = reader.GetDecimal(19);
                    txt_ValorTolFactura.Text = Convert.ToString(ValorTotFac.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorTolFactura.Text = "0";
                }

                lbl_IdPais.Text = reader.GetValue(14).ToString();
                lbl_FechaDespacho.Text = reader.GetValue(18).ToString();

                //Asignamos valor a variable global para utilizar en el metodo (Obtener_Dias_Cumplemiento_Cliente)
                fecEntregaCliente = Convert.ToDateTime(reader.GetValue(20).ToString());

                //habilita la escritura del campo txt_ValorExw, si el pais es  brasil y mexico              
                if (lbl_IdPais.Text == "6" || lbl_IdPais.Text == "21")
                {
                    txt_ValorExw.ReadOnly = false;
                    txt_ValorTolFactura.ReadOnly = false;
                    txt_DiasCumpleCliente.Visible = true;
                    lbl_diasCumple.Visible = true;
                    lbl_PlanEntregaCliente.Visible = true;
                    lbl_FecEntreCliente.Visible = true;
                }
                else
                {

                    if(tipoOrden=="OM" || tipoOrden == "OG")
                    {
                        txt_ValorExw.ReadOnly = false;
                        txt_ValorTolFactura.ReadOnly = false;
                    }
                    else
                    {
                        txt_ValorExw.ReadOnly = true;
                        txt_ValorTolFactura.ReadOnly = true;
                    }
                   
                    txt_DiasCumpleCliente.Visible = false;
                    lbl_diasCumple.Visible = false;
                    lbl_PlanEntregaCliente.Visible = false;
                    lbl_FecEntreCliente.Visible = false;
                }            

                    int id_tdn = int.Parse(reader.GetValue(15).ToString());
                cbo_Tdn.Items.Clear();
                ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
                cbo_Tdn.SelectedValue = reader.GetValue(15).ToString();

                string mediotransp = reader.GetValue(12).ToString();
                mediotransp = mediotransp.ToUpper();

                //True Nacional, False Exportacion
                string tipoDespa = reader.GetValue(17).ToString();
                SetPane("AcorInfoGeneral");

                if (tipoDespa == "True")
                {
                    Acc_DetalleDespa.Visible = false;
                    Acc_GastosOperaciones.Visible = false;
                    Acc_DetalleDespaNacional.Visible = true;
                    cbo_MedioTrasnporte.Enabled = false;
                    cbo_TipoVehiculo.Enabled = false;
                    cbo_TipoVehiculo.Visible = true;
                    lbl_Tipo_Vehiculo.Visible = true;
                }
                else
                {
                    Acc_DetalleDespaNacional.Visible = false;
                    Acc_DetalleDespa.Visible = true;
                    Acc_GastosOperaciones.Visible = true;
                    cbo_MedioTrasnporte.Enabled = true;
                    cbo_TipoVehiculo.Visible = false;
                    lbl_Tipo_Vehiculo.Visible = false;
                    cbo_MedioTrasnporte.SelectedIndex = 2;
                }

                lbl_IdMedTrasnp.Text = reader.GetValue(7).ToString();
                if (lbl_IdMedTrasnp.Text != "0")
                {                   
                    btnGuardarDetDesp.Visible = true;
                    btn_GuardarGast.Visible = true;
                    btnSubirArchivos.Visible = true;
                    btn_GuardarDetalleDespaNal.Visible = true;
                    btn_Guardarobserva.Visible = true;
                    btnCancelar.Visible = true;

                    cbo_MedioTrasnporte.Items.Clear();
                    ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
                    cbo_MedioTrasnporte.SelectedValue = reader.GetValue(7).ToString();
                    cbo_TipoVehiculo.Items.Clear();
                    ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
                    cbo_TipoVehiculo.SelectedValue = reader.GetValue(8).ToString();
                }else
                {
                    if (tipoDespa == "False")
                    {
                        btnGuardarDetDesp.Visible = false;
                        btn_GuardarGast.Visible = false;
                        btnSubirArchivos.Visible = false;
                        btn_GuardarDetalleDespaNal.Visible = false;
                        btn_Guardarobserva.Visible = false;
                        btnCancelar.Visible = false;
                        mensajeVentana("Debe dar clic en el boton Actualizar, de la pestaña 'Datos Generales', para que se habiliten los demas botones  de Guardar y Actualizar");
                    }                   
                }

                cbo_TipoVehiculo.Items.Clear();
                ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));

                //Rol usuario logistica exterior
                if (rol != 53)
                {
                    Inhabilitar_Campos(true);
                    //Inhabilitar_Campos_Detalle_Despa(false);
                    Inhabilitar_Campos_Tramites(false);
                    //Datos Generales
                    txt_M2.Enabled = true;
                    txt_MesDespacho.Enabled = true;
                    txt_Zona.Enabled = true;
                    txt_PaisDestino.Enabled = true;
                    txt_Ciudad.Enabled = true;
                    cbo_MedioTrasnporte.Enabled = true;
                    txt_ValorTolFactura.Enabled = true;
                    txt_ValorExw.Enabled = true;
                    txt_ValorFob.Enabled = true;
                    lst_Facturas.Enabled = true;
                    btn_DtsGeneralAct.Enabled = true;
                }
                else
                {
                    Inhabilitar_Campos(false);
                    //Inhabilitar_Campos_Detalle_Despa(true);
                    Inhabilitar_Campos_Tramites(false);
                    //Datos Generales
                    txt_M2.Enabled = false;
                    txt_MesDespacho.Enabled = false;
                    txt_Zona.Enabled = false;
                    txt_PaisDestino.Enabled = false;
                    txt_Ciudad.Enabled = false;
                    cbo_MedioTrasnporte.Enabled = false;
                    txt_ValorTolFactura.Enabled = false;
                    txt_ValorExw.Enabled = false;
                    txt_ValorFob.Enabled = false;
                    lst_Facturas.Enabled = false;
                    btn_DtsGeneralAct.Enabled = false;
                }
                Acc_DetalleDespaNacional.Enabled = false;      
                if (reader.HasRows == true)
                {
                    //Cargar_DtsOrden(DesC_Id);
                    Cargar_DtsOrden_(DesC_Id);

                    if (tipoDespa == "True")
                    {
                        grid_DtsOrden.Columns[0].Visible = true;
                        cbo_MedioTrasnporte.Enabled = false;
                        cbo_TipoVehiculo.Enabled = false;
                        cbo_TipoVehiculo.Visible = true;
                        lbl_Tipo_Vehiculo.Visible = true;
                    }
                    else
                    {
                        grid_DtsOrden.Columns[0].Visible = false;
                        cbo_MedioTrasnporte.Enabled = true;
                        cbo_TipoVehiculo.Enabled = true;
                        cbo_TipoVehiculo.Visible = false;
                        lbl_Tipo_Vehiculo.Visible = false;
                    }

                    Cargar_DtsTrasnporte(DesC_Id);
                    Cargar_Dts_DetalleDespacho(DesC_Id);
                    Obtener_Dias_Cumplimiento_Cliente();
                    Poblar_GastosOperacionales(DesC_Id);
                    //¿Si el pais destino es brasil?, entonces deja visible el acordeón (GASTOS EN DESTINO BRASIL)
                    if (lbl_IdPais.Text == "6")
                    {
                        Acc_GastosDestinoBrasil.Visible = true;
                        Poblar_Gastos_Destino_Brasil(Convert.ToInt32(DesC_Id));
                    }
                    else
                    {
                        Acc_GastosDestinoBrasil.Visible = false;
                    }
                    Consultar_Doc_Despacho(DesC_Id);
                    // Calcular_Gastos_Fletes_Facturados();
                    Calcular_DiffProvisiones();
                    Calcular_Ahorro();
                    Poblar_observaciones_Despa(DesC_Id);
                    Obtener_LeadTime();             
                    SetPane("AcorInfoGeneral");
                }
            }
            #region No borrar este metodo, en caso de revertir se utilizara, el el mismo de arriba con algunas modificaciones
            //    reader = ctrlseguidespachos.PoblarDatosGenerales(Convert.ToInt32(DesC_Id));
            //    reader.Read();
            //    txt_TiempoEnvioDtos.ReadOnly = true;

            //    lbl_IdDespacho.Text = reader.GetValue(0).ToString();
            //    txt_MesDespacho.Text = reader.GetValue(1).ToString();
            //    if (txt_MesDespacho.Text == "0")
            //    {
            //        txt_MesDespacho.Text = "";
            //    }
            //    txt_Zona.Text = reader.GetValue(2).ToString();
            //    txt_PaisDestino.Text = reader.GetValue(3).ToString();
            //    txt_Ciudad.Text = reader.GetValue(4).ToString();
            //    lst_Facturas.Items.Add(new ListItem(reader.GetValue(6).ToString()));
            //    txt_M2.Text = reader.GetValue(13).ToString();
            //    decimal ValorExw = reader.GetDecimal(5);
            //    decimal ValorFob = reader.GetDecimal(10);
            //    decimal ValorTotFac = reader.GetDecimal(16);
            //    txt_ValorExw.Text = Convert.ToString(ValorExw.ToString("#,##.##"));
            //    txt_ValorFob.Text = Convert.ToString(ValorFob.ToString("#,##.##"));
            //    txt_ValorTolFactura.Text = Convert.ToString(ValorTotFac.ToString("#,##.##"));
            //    lbl_IdPais.Text = reader.GetValue(14).ToString();
            //    lbl_FechaDespacho.Text = reader.GetValue(18).ToString();
            //    if (!String.IsNullOrEmpty(lbl_FechaDespacho.Text)) { txt_FechaEntregaNal.Enabled = true; }
            //    else { txt_FechaEntregaNal.Enabled = false; }

            //    //habilita la escritura del campo txt_ValorExw, si el pais es  brasil y mexico
            //    if (lbl_IdPais.Text == "6" || lbl_IdPais.Text == "21")
            //    {
            //        txt_ValorExw.ReadOnly = false;
            //        txt_ValorTolFactura.ReadOnly = false;
            //    }
            //    else
            //    {
            //        txt_ValorExw.ReadOnly = true;
            //        txt_ValorTolFactura.ReadOnly = true;
            //    }
            //    int id_tdn = int.Parse(reader.GetValue(15).ToString());
            //    cbo_Tdn.Items.Clear();
            //    ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
            //    cbo_Tdn.SelectedValue = reader.GetValue(15).ToString();

            //    string mediotransp = reader.GetValue(12).ToString();
            //    mediotransp = mediotransp.ToUpper();

            //    //True Nacional, False Exportacion
            //    string tipoDespa = reader.GetValue(17).ToString();
            //    SetPane("AcorInfoGeneral");

            //    if (tipoDespa == "True")
            //    {
            //        Acc_DetalleDespa.Visible = false;
            //        Acc_GastosOperaciones.Visible = false;
            //        Acc_DetalleDespaNacional.Visible = true;
            //        cbo_MedioTrasnporte.Enabled = false;
            //        cbo_TipoVehiculo.Enabled = false;
            //        cbo_TipoVehiculo.Visible = true;
            //        lbl_Tipo_Vehiculo.Visible = true;
            //    }
            //    else
            //    {
            //        Acc_DetalleDespaNacional.Visible = false;
            //        Acc_DetalleDespa.Visible = true;
            //        Acc_GastosOperaciones.Visible = true;
            //        cbo_MedioTrasnporte.Enabled = true;
            //        cbo_TipoVehiculo.Visible = false;
            //        lbl_Tipo_Vehiculo.Visible = false;
            //        cbo_MedioTrasnporte.SelectedIndex = 2;
            //    }

            //    lbl_IdMedTrasnp.Text = reader.GetValue(7).ToString();
            //    if (lbl_IdMedTrasnp.Text != "0")
            //    {
            //        cbo_MedioTrasnporte.Items.Clear();
            //        ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
            //        cbo_MedioTrasnporte.SelectedValue = reader.GetValue(7).ToString();
            //        cbo_TipoVehiculo.Items.Clear();
            //        ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
            //        cbo_TipoVehiculo.SelectedValue = reader.GetValue(8).ToString();
            //    }
            //    cbo_TipoVehiculo.Items.Clear();
            //    ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));

            //    Inhabilitar_Campos(true);
            //    Acc_DetalleDespaNacional.Enabled = false;

            //    if (reader.HasRows == true)
            //    {
            //        //Cargar_DtsOrden(DesC_Id);
            //        Cargar_DtsOrden_(DesC_Id);

            //        if (tipoDespa == "True")
            //        {
            //            grid_DtsOrden.Columns[0].Visible = true;
            //            cbo_MedioTrasnporte.Enabled = false;
            //            cbo_TipoVehiculo.Enabled = false;
            //            cbo_TipoVehiculo.Visible = true;
            //            lbl_Tipo_Vehiculo.Visible = true;
            //        }
            //        else
            //        {
            //            grid_DtsOrden.Columns[0].Visible = false;
            //            cbo_MedioTrasnporte.Enabled = true;
            //            cbo_TipoVehiculo.Enabled = true;
            //            cbo_TipoVehiculo.Visible = false;
            //            lbl_Tipo_Vehiculo.Visible = false;
            //        }
            //        Cargar_DtsTrasnporte(DesC_Id);
            //        Cargar_Dts_DetalleDespacho(DesC_Id);
            //        Poblar_GastosOperacionales(DesC_Id);
            //        Consultar_Doc_Despacho(DesC_Id);
            //       // Calcular_Gastos_Fletes_Facturados();
            //        Calcular_DiffProvisiones();
            //        Calcular_Ahorro();
            //        Poblar_observaciones_Despa(DesC_Id);
            //        Obtener_LeadTime();
            //        SetPane("AcorInfoGeneral");
            //    }
            //}
            #endregion
            reader.Close();
            reader.Dispose();
            ctrlseguidespachos.CerrarConexion();
        }
       
        public void Obtener_Dias_Cumplimiento_Cliente()
        {
            DateTime fechaActualSistema;

            //Proceso que calcula la diferencia de dias entre fechas---------------
            if (String.IsNullOrEmpty(txt_Notifica_Cliente.Text))
            {

                lbl_FecEntreCliente.Text = Convert.ToDateTime(fecEntregaCliente).ToString("dd/MM/yyyy");
                fechaActualSistema = Convert.ToDateTime(DateTime.Now.ToString("G"));

                TimeSpan ts = fecEntregaCliente - fechaActualSistema;

                int differenceInDays = ts.Days;

                txt_DiasCumpleCliente.Text = differenceInDays.ToString();

                /*Si tenemos un incumplimiento en dias, a la fecha de entrega al cliente,
                  el resultado se visualizara de color rojo*/
                if (differenceInDays < 0)
                {
                    txt_DiasCumpleCliente.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txt_DiasCumpleCliente.ForeColor = System.Drawing.Color.Black;
                }
            }
            else
            {
                txt_DiasCumpleCliente.Text = "";
                txt_DiasCumpleCliente.ForeColor = System.Drawing.Color.Black;
            }
            //-----------------------------------------------------------------------
        }



        public void Obtener_LeadTime()
        {
            int idPais = 0, idTdn = 0, idMediot = 0;

            if (String.IsNullOrEmpty(lbl_IdPais.Text) || lbl_IdPais.Text=="0" || String.IsNullOrEmpty(lbl_idTdn.Text) ||
                lbl_idTdn.Text == "0" || String.IsNullOrEmpty(lbl_IdMedTrasnp.Text) || lbl_IdMedTrasnp.Text == "0")
            {

            }
            else
            { 

            idPais = int.Parse(lbl_IdPais.Text);
            idTdn = int.Parse(lbl_idTdn.Text);
            idMediot = int.Parse(lbl_IdMedTrasnp.Text);

            reader = ctrlseguidespachos.Consultar_LeadTime(idPais, idTdn, idMediot);
           
            if (reader.HasRows)
            {
                reader.Read();
                txt_LeadTimeEspera.Text = reader.GetValue(0).ToString();
                Calcular_Tt_PlantaCliente();
            }
            else
            {
                txt_LeadTimeEspera.Text = "";
                txt_EfectiviEntrega.Text = "";
            }
                reader.Close();
                ctrlseguidespachos.CerrarConexion();
            }
        }



        public void Limpiar_Campos_Gastos_Dest_Brasil()
        {
            //Gastos en destino brasil
            txt_liberaHblProv.Text = "";
            txt_liberaHblReal.Text = "";
            txt_droppOffProvi.Text = "";
            txt_droppOffReal.Text = "";
            txt_taxaAdminProv.Text = "";
            txt_taxaAdminReal.Text = "";
            txt_ispsProv.Text = "";
            txt_ispsReal.Text = "";
            txt_otrosGastosProv.Text = "";
            txt_otrosGastosReal.Text = "";
            txt_1erPeriodoProv.Text = "";
            txt_1erPeriodoReal.Text = "";
            txt_2doPeriodoProv.Text = "";
            txt_2doPeriodoReal.Text = "";
            txt_3erPeriodoProv.Text = "";
            txt_3erPeriodoReal.Text = "";
            txt_escanContenProv.Text = "";
            txt_escanContenReal.Text = "";
            txt_insoMapaProv.Text = "";
            txt_insoMapaReal.Text = "";
            txt_corretagenProv.Text = "";
            txt_corretagenReal.Text = "";
            txt_demurrageProv.Text = "";
            txt_demurrageReal.Text = "";
            txt_despaHonoraProv.Text = "";
            txt_despaHonoraReal.Text = "";
            txt_fleteTerresProv.Text = "";
            txt_fleteTerresReal.Text = "";
            txt_noProceso.Text = "";
            txt_fechPlanilla.Text = "";
            txt_margenTimboProv.Text = "";
            txt_margenTimboReal.Text = "";
            txt_trm.Text = "";
            chk_Cerrado_DespaDestBrasil.Checked = false;
        }





        public void Limpiar_Campos_Despacho()
        {
            //Dts_Generales
            lbl_IdDespacho.Text = "";
            txt_M2.Text = "";
            //lbl_Desc_Id.Text = "";
            txt_MesDespacho.Text = "";
            txt_Zona.Text = "";
            txt_PaisDestino.Text = "";
            txt_Ciudad.Text = "";
            txt_ValorTolFactura.Text = "";
            txt_ValorExw.Text = "";
            txt_Puerto_Origen.Text = "";
            txt_Puerto_Destino.Text = "";
            cbo_MedioTrasnporte.Items.Clear();
            cbo_Tdn.Items.Clear();
            cbo_Tdn.Visible = false;
            lbl_tdn_1.Visible = false;
            cbo_Tdn.Items.Add(new ListItem("Seleccione", "0"));
            ctrlseguidespachos.Listar_Tdn(cbo_Tdn);
            ctrlseguidespachos.Listar_MedioTrasnporte(cbo_MedioTrasnporte);
            cbo_TipoVehiculo.Items.Clear();
            ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
            txt_ValorExw.Text = "";
            txt_ValorFob.Text = "";
            txt_DiasCumpleCliente.Text = "";
            lbl_FecEntreCliente.Text = "";
            txt_ValorTolFactura.Text = "";
            lst_Facturas.Items.Clear();
            grid_DtsOrden.DataSource = null;
            grid_DtsOrden.DataBind();
            GridTransporte.DataSource = null;
            GridTransporte.DataBind();
            lbl_MsjValidaTdn.Text = "";
            btn_DtsGeneralAct.Visible = true;
            //-----------------------------------------
            //Dts_Detalle
            txt_DocumentTrasnGuia.Text = "";
            txt_DexFmm.Text = "";
            txt_NoGuiaDtos.Text = "";
            cbo_EstatusCargaDet.Items.Clear();
            ctrlseguidespachos.Listar_EstatusCarga(cbo_EstatusCargaDet);
            txt_Embarcador.Text = "";
            txtFechaEnvioDtos.Text = "";
            txt_TiempoEnvioDtos.Text = "";
            txt_EfectividadEnvioDtos.Text = "";
            chk_InspeccionSi.Checked = false;
            txt_FechaEstimaDespa.Text = "";
            txt_FechaRealDespa.Text = "";
            txt_TtPlantaCliente.Text = "";
            txt_FechaEstimaZarpe.Text = "";
            txt_FechaRealZarpe.Text = "";
            txt_EfectiviEntrega.Text = "";
            txt_EstimadaArribo.Text = "";
            txt_FechaConfirmaArribo.Text = "";
            txt_LeadTimeEspera.Text = "";
            txt_FechaEstLlegaObra.Text = "";
            txt_FechaRealLlegaObra.Text = "";
            txt_TtInternacional.Text = "";
            txt_fechaDocumentado.Text = "";
            txt_PuertoDestCliente.Text = "";
            txt_ModTransDespa.Text = "";
            chk_DetalleCerrado.Checked = false;
            //Datos Gastos Operacionales
            txt_FleteNalProvi.Text = "";
            txt_FletNalReal.Text = "";
            txt_AgradeAduanProvi.Text = "";
            txt_AgradeAduanReal.Text = "";
            txt_GastPuertoProvision.Text = "";
            txt_GastPuertoReal.Text = "";
            txt_FleteInterProvision.Text = "";
            txt_FleteInterReal.Text = "";
            txt_GastDestinoProvision.Text = "";
            txt_GastDestinoReal.Text = "";
            txt_SelloSateliProvi.Text = "";
            txt_SelloSateliReal.Text = "";
            txt_TotalProvi.Text = "";
            txt_TotalGastFletreal.Text = "";
            txt_StandBy.Text = "";
            txt_TrmFechafact.Text = "";
            txt_BodegajeReal.Text = "";
            txt_PesoFactura.Text = "";
            txt_Difprovi.Text = "";
            txt_PorcentajeAhorro.Text = "";
            txt_Concatenar.Text = "";
            txt_EnvioCtrlEmpaque.Text = "";
            txt_GastFletFacturados.Text = "";
            chk_Cerrado.Checked = false;
            txt_Seguro.Text = "";
            txt_inspAntiNarcot.Text = "";
            txt_ManejoFlete.Text = "";
            txt_TranspDestino.Text = "";
            txt_AduanaDestino.Text = "";
            txt_DemoraDestino.Text = "";
            txt_RollOver.Text = "";
            txt_BodegajeDestino.Text = "";
            txt_ImpuestoDestino.Text = "";
            //Gastos en destino brasil
            txt_liberaHblProv.Text = "";
            txt_liberaHblReal.Text = "";
            txt_droppOffProvi.Text = "";
            txt_droppOffReal.Text = "";
            txt_taxaAdminProv.Text = "";
            txt_taxaAdminReal.Text = "";
            txt_ispsProv.Text = "";
            txt_ispsReal.Text = "";
            txt_otrosGastosProv.Text = "";
            txt_otrosGastosReal.Text = "";
            txt_1erPeriodoProv.Text = "";
            txt_1erPeriodoReal.Text = "";
            txt_2doPeriodoProv.Text = "";
            txt_2doPeriodoReal.Text = "";
            txt_3erPeriodoProv.Text = "";
            txt_3erPeriodoReal.Text = "";
            txt_escanContenProv.Text = "";
            txt_escanContenReal.Text = "";
            txt_insoMapaProv.Text = "";
            txt_insoMapaReal.Text = "";
            txt_corretagenProv.Text = "";
            txt_corretagenReal.Text = "";
            txt_demurrageProv.Text = "";
            txt_demurrageReal.Text = "";
            txt_despaHonoraProv.Text = "";
            txt_despaHonoraReal.Text = "";
            txt_fleteTerresProv.Text = "";
            txt_fleteTerresReal.Text = "";
            txt_noProceso.Text = "";
            txt_fechPlanilla.Text = "";
            txt_margenTimboProv.Text = "";
            txt_margenTimboReal.Text = "";
            txt_trm.Text = "";
            chk_Cerrado_DespaDestBrasil.Checked = false;
            //
            txt_Finalizacion.Text = "";
            txt_Dias_libres.Text = "";
            txt_Inspeccion.Text = "";           
            //cbo_Canal.SelectedItem.Value = "0";
            cbo_Canal.SelectedIndex = 0;
            txt_Nacionalizacion.Text = "";
            txt_FacturacionProveed.Text = "";
            txt_CI_impuestos.Text = "";
            txt_Retiro_Conten.Text = "";
            txt_Desove.Text = "";
            txt_Almacenamiento.Text = "";
            txt_Notifica_Cliente.Text = "";
            txt_Devolucion_Conten.Text = "";
            txt_Facturacion_ForsaCliente.Text = "";
            txt_Fecha_Carga_Cliente.Text = "";
            txt_Entrega_Obra.Text = "";
            chk_Tramite_Cerrado.Checked = false;
            //Datos Despacho Nacional
            txt_OrdenNal.Text = "";
            txt_facturaNal.Text = "";
            txt_ClienteNal.Text = "";
            txt_CiudadDestinoNal.Text = "";
            txt_EmpresaTrasnpNal.Text = "";
            txt_FechaEntregaNal.Text = "";
            txt_NoGuiaNal.Text = "";
            txt_RespTranspNal.Text = "";
            txt_ValorExwNal.Text = "";
            txt_FleteCotiNal.Text = "";
            txt_FleteRealNal.Text = "";
            txt_RelFleTValorNal.Text = "";
            txt_PesoNal.Text = "";
            txt_StanByNal.Text = "";
            txt_Indicador.Text = "";
            chk_CumpleEntregaNal.Checked = false;
            //Grilla observaciones
            grid_Observaciones.DataBind();
            grid_Observaciones.DataSource = null;
            Limpiar_Campos_DetDespachoNacional();
        }
       
        public void Calcular_ValorTotalFactura()
        {
            //decimal valExw = 0, valFob = 0, result;

            //if (!String.IsNullOrEmpty(txt_ValorFob.Text) && !String.IsNullOrEmpty(txt_ValorExw.Text))
            //{
            //    valFob = decimal.Parse(txt_ValorFob.Text);
            //    valExw = decimal.Parse(txt_ValorExw.Text);
            //    result = valExw + valFob;

            //    txt_ValorTolFactura.Text = Convert.ToString(result.ToString("#,##.##"));
            //}
            //else if (String.IsNullOrEmpty(txt_ValorFob.Text))
            //{
            //    valFob = 0;
            //    if (String.IsNullOrEmpty(txt_ValorExw.Text))
            //    {
            //        valExw = 0;
            //    }
            //    else
            //    {
            //        valExw = decimal.Parse(txt_ValorExw.Text);
            //    }                  
            //    result = valExw + valFob;

            //    txt_ValorTolFactura.Text = Convert.ToString(result.ToString("#,##.##"));
            //}
            //else if (String.IsNullOrEmpty(txt_ValorExw.Text))
            //{
            //    valExw = 0;
            //    if (String.IsNullOrEmpty(txt_ValorFob.Text))
            //    {
            //        valFob = 0;
            //    }
            //    else
            //    {
            //        valFob = decimal.Parse(txt_ValorFob.Text);
            //    }               
            //    result = valExw + valFob;

            //    txt_ValorTolFactura.Text = Convert.ToString(result.ToString("#,##.##"));
            //}
        }

  

        public void Inhabilitar_Campos(bool respuesta)
        {
            int rol = Convert.ToInt32(Session["Rol"]);

            if (respuesta == false)
            {
                //Datos Generales
                txt_M2.Enabled = false;
                txt_MesDespacho.Enabled = false;
                txt_Zona.Enabled = false;
                txt_PaisDestino.Enabled = false;
                txt_Ciudad.Enabled = false;
                cbo_MedioTrasnporte.Enabled = false;
                cbo_TipoVehiculo.Enabled = false;
                txt_ValorTolFactura.Enabled = false;
                txt_ValorExw.Enabled = false;
                txt_ValorFob.Enabled = false;
                lst_Facturas.Enabled = false;
                txt_DiasCumpleCliente.Enabled = false;
                btn_DtsGeneralAct.Enabled = false;
                //Datos Detalle Despacho
                txt_DocumentTrasnGuia.Enabled = false;
                txt_DexFmm.Enabled = false;
                txt_NoGuiaDtos.Enabled = false;
                cbo_EstatusCargaDet.Enabled = false;
                txt_Embarcador.Enabled = false;
                txtFechaEnvioDtos.Enabled = false;
                txt_TiempoEnvioDtos.Enabled = false;
                txt_EfectividadEnvioDtos.Enabled = false;
                chk_InspeccionSi.Enabled = false;
                txt_FechaEstimaDespa.Enabled = false;
                txt_FechaEstimaZarpe.Enabled = false;
                txt_EstimadaArribo.Enabled = false;
                txt_FechaEstLlegaObra.Enabled = false;
                txt_TtInternacional.Enabled = false;
                txt_FechaRealDespa.Enabled = false;
                txt_FechaRealZarpe.Enabled = false;
                txt_FechaConfirmaArribo.Enabled = false;
                txt_FechaRealLlegaObra.Enabled = false;
                txt_PuertoDestCliente.Enabled = false;
                txt_TtPlantaCliente.Enabled = false;
                txt_EfectiviEntrega.Enabled = false;
                txt_fechaDocumentado.Enabled = false;
                txt_LeadTimeEspera.Enabled = false;
                btnGuardarDetDesp.Enabled = false;
                txt_ModTransDespa.Enabled = false;
                chk_DetalleCerrado.Enabled = false;
                Panel_Tramites.Enabled = false;
                //Datos Gastos Operacionales
                txt_FleteNalProvi.Enabled = false;
                txt_FletNalReal.Enabled = false;
                txt_AgradeAduanProvi.Enabled = false;
                txt_AgradeAduanReal.Enabled = false;
                txt_GastPuertoProvision.Enabled = false;
                txt_GastPuertoReal.Enabled = false;
                txt_FleteInterProvision.Enabled = false;
                txt_FleteInterReal.Enabled = false;
                txt_GastDestinoProvision.Enabled = false;
                txt_GastDestinoReal.Enabled = false;
                txt_SelloSateliProvi.Enabled = false;
                txt_SelloSateliReal.Enabled = false;
                txt_TotalProvi.Enabled = false;
                txt_TotalGastFletreal.Enabled = false;
                txt_StandBy.Enabled = false;
                txt_TrmFechafact.Enabled = false;
                txt_BodegajeReal.Enabled = false;
                txt_PesoFactura.Enabled = false;
                txt_Difprovi.Enabled = false;
                txt_PorcentajeAhorro.Enabled = false;
                txt_Concatenar.Enabled = false;
                txt_EnvioCtrlEmpaque.Enabled = false;
                chk_Cerrado.Enabled = false;
                btn_GuardarGast.Enabled = false;
                txt_Seguro.Enabled = false;
                txt_inspAntiNarcot.Enabled = false;
                txt_ManejoFlete.Enabled = false;
                txt_TranspDestino.Enabled = false;
                txt_AduanaDestino.Enabled = false;
                txt_DemoraDestino.Enabled = false;
                txt_RollOver.Enabled = false;
                txt_BodegajeDestino.Enabled = false;
                txt_ImpuestoDestino.Enabled = false;
                //Gastos en destino brasil
                txt_noProceso.Enabled = false;
                txt_fechPlanilla.Enabled = false;
                txt_trm.Enabled = false;
                txt_liberaHblProv.Enabled = false;
                txt_liberaHblReal.Enabled = false;
                txt_droppOffProvi.Enabled = false;
                txt_droppOffReal.Enabled = false;
                txt_taxaAdminProv.Enabled = false;
                txt_taxaAdminReal.Enabled = false;
                txt_ispsProv.Enabled = false;
                txt_ispsReal.Enabled = false;
                txt_otrosGastosProv.Enabled = false;
                txt_otrosGastosReal.Enabled = false;
                txt_1erPeriodoProv.Enabled = false;
                txt_1erPeriodoReal.Enabled = false;
                txt_2doPeriodoProv.Enabled = false;
                txt_2doPeriodoReal.Enabled = false;
                txt_3erPeriodoProv.Enabled = false;
                txt_3erPeriodoReal.Enabled = false;
                txt_escanContenProv.Enabled = false;
                txt_escanContenReal.Enabled = false;
                txt_insoMapaProv.Enabled = false;
                txt_insoMapaReal.Enabled = false;
                txt_corretagenProv.Enabled = false;
                txt_corretagenReal.Enabled = false;
                txt_demurrageProv.Enabled = false;
                txt_demurrageReal.Enabled = false;
                txt_despaHonoraProv.Enabled = false;
                txt_despaHonoraReal.Enabled = false;
                txt_margenTimboProv.Enabled = false;
                txt_margenTimboReal.Enabled = false;
                txt_fleteTerresProv.Enabled = false;
                txt_fleteTerresReal.Enabled = false;
                btn_GuardarGastDespDestBrasil.Enabled = false;
                chk_Cerrado_DespaDestBrasil.Enabled = false;
                //Observaciones
                txt_Observacion.Enabled = false;
                btn_Guardarobserva.Enabled = false;
                //Datos adjuntos
                btnSubirArchivos.Enabled = false;
                btnCancelar.Enabled = false;
                FDocument.Enabled = false;
                Acc_DetalleDespaNacional.Enabled = false;
                cboTipoAnexo.Enabled = false;
                lblTipoAnexo.Enabled = false;            
            }
            else
            {
                txt_M2.Enabled = true;
                txt_MesDespacho.Enabled = true;
                txt_Zona.Enabled = true;
                txt_PaisDestino.Enabled = true;
                txt_Ciudad.Enabled = true;
                cbo_MedioTrasnporte.Enabled = true;
                cbo_TipoVehiculo.Enabled = true;
                txt_ValorTolFactura.Enabled = true;
                txt_ValorExw.Enabled = true;
                txt_ValorFob.Enabled = true;
                lst_Facturas.Enabled = true;
                txt_DiasCumpleCliente.Enabled = true;
                btn_DtsGeneralAct.Enabled = true;
                //Datos Detalle Despacho
                txt_DocumentTrasnGuia.Enabled = true;
                txt_DexFmm.Enabled = true;
                txt_NoGuiaDtos.Enabled = true;
                cbo_EstatusCargaDet.Enabled = true;
                txt_Embarcador.Enabled = true;
                txtFechaEnvioDtos.Enabled = true;
                txt_TiempoEnvioDtos.Enabled = true;
                txt_EfectividadEnvioDtos.Enabled = true;
                chk_InspeccionSi.Enabled = true;
                txt_FechaEstimaDespa.Enabled = true;
                txt_FechaEstimaZarpe.Enabled = true;
                txt_EstimadaArribo.Enabled = true;
                txt_FechaEstLlegaObra.Enabled = true;
                txt_TtInternacional.Enabled = true;
                txt_FechaRealDespa.Enabled = true;
                txt_FechaRealZarpe.Enabled = true;
                txt_FechaConfirmaArribo.Enabled = true;
                txt_FechaRealLlegaObra.Enabled = true;
                txt_PuertoDestCliente.Enabled = true;
                txt_TtPlantaCliente.Enabled = true;
                txt_EfectiviEntrega.Enabled = true;
                txt_LeadTimeEspera.Enabled = true;
                btnGuardarDetDesp.Enabled = true;
                chk_DetalleCerrado.Enabled = true;
                Panel_Tramites.Enabled = true;
                txt_fechaDocumentado.Enabled = true;
                txt_ModTransDespa.Enabled = true;
                //Datos Gastos Operacionales
                txt_FleteNalProvi.Enabled = true;
                txt_FletNalReal.Enabled = true;
                txt_AgradeAduanProvi.Enabled = true;
                txt_AgradeAduanReal.Enabled = true;
                txt_GastPuertoProvision.Enabled = true;
                txt_GastPuertoReal.Enabled = true;
                txt_FleteInterProvision.Enabled = true;
                txt_FleteInterReal.Enabled = true;
                txt_GastDestinoProvision.Enabled = true;
                txt_GastDestinoReal.Enabled = true;
                txt_SelloSateliProvi.Enabled = true;
                txt_SelloSateliReal.Enabled = true;
                txt_TotalProvi.Enabled = true;
                txt_TotalGastFletreal.Enabled = true;
                txt_StandBy.Enabled = true;
                txt_TrmFechafact.Enabled = true;
                txt_BodegajeReal.Enabled = true;
                txt_PesoFactura.Enabled = true;
                txt_Difprovi.Enabled = true;
                txt_PorcentajeAhorro.Enabled = true;
                txt_Concatenar.Enabled = true;
                txt_EnvioCtrlEmpaque.Enabled = true;
                chk_Cerrado.Enabled = true;
                btn_GuardarGast.Enabled = true;
                txt_Seguro.Enabled = true;
                txt_inspAntiNarcot.Enabled = true;
                txt_ManejoFlete.Enabled = true;
                txt_TranspDestino.Enabled = true;
                txt_AduanaDestino.Enabled = true;
                txt_DemoraDestino.Enabled = true;
                txt_RollOver.Enabled = true;
                txt_BodegajeDestino.Enabled = true;
                txt_ImpuestoDestino.Enabled = true;
                //Gastos en destino brasil
                txt_noProceso.Enabled = false;
                txt_fechPlanilla.Enabled = false;
                txt_trm.Enabled = false;
                txt_liberaHblProv.Enabled = false;
                txt_liberaHblReal.Enabled = false;
                txt_droppOffProvi.Enabled = false;
                txt_droppOffReal.Enabled = false;
                txt_taxaAdminProv.Enabled = false;
                txt_taxaAdminReal.Enabled = false;
                txt_ispsProv.Enabled = false;
                txt_ispsReal.Enabled = false;
                txt_otrosGastosProv.Enabled = false;
                txt_otrosGastosReal.Enabled = false;
                txt_1erPeriodoProv.Enabled = false;
                txt_1erPeriodoReal.Enabled = false;
                txt_2doPeriodoProv.Enabled = false;
                txt_2doPeriodoReal.Enabled = false;
                txt_3erPeriodoProv.Enabled = false;
                txt_3erPeriodoReal.Enabled = false;
                txt_escanContenProv.Enabled = false;
                txt_escanContenReal.Enabled = false;
                txt_insoMapaProv.Enabled = false;
                txt_insoMapaReal.Enabled = false;
                txt_corretagenProv.Enabled = false;
                txt_corretagenReal.Enabled = false;
                txt_demurrageProv.Enabled = false;
                txt_demurrageReal.Enabled = false;
                txt_despaHonoraProv.Enabled = false;
                txt_despaHonoraReal.Enabled = false;
                txt_margenTimboProv.Enabled = false;
                txt_margenTimboReal.Enabled = false;
                txt_fleteTerresProv.Enabled = false;
                txt_fleteTerresReal.Enabled = false;
                chk_Cerrado_DespaDestBrasil.Enabled = false;
                btn_GuardarGastDespDestBrasil.Enabled = false;
                //Observaciones
                txt_Observacion.Enabled = true;
                btn_Guardarobserva.Enabled = true;
                //Datos adjuntos
                btnSubirArchivos.Enabled = true;
                btnCancelar.Enabled = true;
                FDocument.Enabled = true;
                cboTipoAnexo.Enabled = true;
                lblTipoAnexo.Enabled = true;              
            }
        }

        protected void btn_DtsGeneralAct_Click(object sender, EventArgs e)
        {
            int respuesta;

            if (!String.IsNullOrEmpty(txt_ValorExw.Text))
            {
                if (!String.IsNullOrEmpty(txt_ValorFob.Text))
                {
                    if (!String.IsNullOrEmpty(txt_ValorTolFactura.Text))
                    {
                        respuesta = ctrlseguidespachos.Actualizar_Datos_Generales(int.Parse(cbo_MedioTrasnporte.SelectedValue), int.Parse(cbo_TipoVehiculo.SelectedValue),
                                                                                decimal.Parse(txt_ValorExw.Text), decimal.Parse(txt_ValorFob.Text),int.Parse(cbo_Tdn.SelectedValue),
                                                                                int.Parse(lbl_Desc_Id.Text),decimal.Parse(txt_ValorTolFactura.Text));
                Calcular_Gastos_Fletes_Facturados();
                Calcular_DiffProvisiones();
                Calcular_Ahorro();
                ctrlseguidespachos.Actualizar_Campo_GastFletesFactu(decimal.Parse(txt_GastFletFacturados.Text), int.Parse(lbl_Desc_Id.Text));
                ctrlseguidespachos.Actualizar_Campo_Concatenar(txt_Concatenar.Text, int.Parse(lbl_Desc_Id.Text));                    
                lbl_IdMedTrasnp.Text = cbo_MedioTrasnporte.SelectedValue;
                Calcular_Concatenar();
              
                Obtener_LeadTime();
                if (String.IsNullOrEmpty(txt_EfectiviEntrega.Text))
                {
                    /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                    de lo contrario muestra el valor que se calcula*/
                    txt_EfectiviEntrega.Text = "255";
                }
                else if (txt_EfectiviEntrega.Text == "SI")
                {
                    txt_EfectiviEntrega.Text = "1";
                }
                else if (txt_EfectiviEntrega.Text == "NO")
                {
                    txt_EfectiviEntrega.Text = "0";
                }
                ctrlseguidespachos.Actualizar_Campo_EfectiEntrega(int.Parse(txt_EfectiviEntrega.Text), int.Parse(lbl_Desc_Id.Text));
                if (txt_EfectiviEntrega.Text == "255")
                {                             
                    txt_EfectiviEntrega.Text = "";
                }
                else if (txt_EfectiviEntrega.Text == "1")
                {
                    txt_EfectiviEntrega.Text = "SI";
                }
                else if (txt_EfectiviEntrega.Text == "0")
                {
                    txt_EfectiviEntrega.Text = "NO";
                }
           
                // Cargar_DtsOrden(int.Parse(lbl_Desc_Id.Text));
                Cargar_DtsOrden_(int.Parse(lbl_Desc_Id.Text));

                if (respuesta == 1)
                {
                    mensajeVentana("Registro actualizado correctamente");
                }
                else
                {
                    mensajeVentana("El registro no se puedo actualizar, verifique los datos");
                }
                    }
                    else
                    {
                        mensajeVentana("Debe digitar el total de la factura");
                    }
                }
                else
                {
                    mensajeVentana("Debe digitar el valor FOB");
                }
            }
            else
            {
                mensajeVentana("Debe digitar el valor EXW");
            }     
        }



        public void Valida_Campos_DtsDetalle()
        {
            if (String.IsNullOrEmpty(txt_TiempoEnvioDtos.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_TiempoEnvioDtos.Text = "255";
            }
            else if (txt_TiempoEnvioDtos.Text == "255")
            {
                txt_TiempoEnvioDtos.Text = "";
            }
            if (String.IsNullOrEmpty(txt_EfectividadEnvioDtos.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_EfectividadEnvioDtos.Text = "255";
            }
            else if (txt_EfectividadEnvioDtos.Text == "255")
            {
                txt_EfectividadEnvioDtos.Text = "";
            }
            else if (txt_EfectividadEnvioDtos.Text == "1")
            {
                txt_EfectividadEnvioDtos.Text = "SI";
            }
            else if (txt_EfectividadEnvioDtos.Text == "0")
            {
                txt_EfectividadEnvioDtos.Text = "NO";
            }
            else if (txt_EfectividadEnvioDtos.Text == "SI")
            {
                txt_EfectividadEnvioDtos.Text = "1";
            }
            else if (txt_EfectividadEnvioDtos.Text == "NO")
            {
                txt_EfectividadEnvioDtos.Text = "0";
            }

                if (String.IsNullOrEmpty(txt_TtInternacional.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_TtInternacional.Text = "255";
            }
            else if (txt_TtInternacional.Text == "255")
            {
                txt_TtInternacional.Text = "";
            }
            if (String.IsNullOrEmpty(txt_PuertoDestCliente.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_PuertoDestCliente.Text = "255";
            }
            else if (txt_PuertoDestCliente.Text == "255")
            {
                txt_PuertoDestCliente.Text = "";
            }
            if (String.IsNullOrEmpty(txt_TtPlantaCliente.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_TtPlantaCliente.Text = "255";
            }
            else if (txt_TtPlantaCliente.Text == "255")
            {
                txt_TtPlantaCliente.Text = "";
            }
            if (String.IsNullOrEmpty(txt_EfectiviEntrega.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 1000 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_EfectiviEntrega.Text = "255";
            }
            else if (txt_EfectiviEntrega.Text == "255")
            {
                txt_EfectiviEntrega.Text = "";
            }
            else if (txt_EfectiviEntrega.Text == "1")
            {
                txt_EfectiviEntrega.Text = "SI";
            }
            else if (txt_EfectiviEntrega.Text == "0")
            {
                txt_EfectiviEntrega.Text = "NO";
            }

            else if (txt_EfectiviEntrega.Text == "SI")
            {
                txt_EfectiviEntrega.Text = "1";
            }
            else
            {
                txt_EfectiviEntrega.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_LeadTimeEspera.Text))
            {
                /*Valor 1000 se utiliza para validar si es = a 255 no muestra nada en el campo
                de lo contrario muestra el valor que se calcula*/
                txt_LeadTimeEspera.Text = "255";
            }
            else if (txt_LeadTimeEspera.Text == "255")
            {
                txt_LeadTimeEspera.Text = "";
            }
           
        }

        public void Limpiar_Campos_Calculados()
        {
            txt_TiempoEnvioDtos.Text = "";
            txt_EfectividadEnvioDtos.Text = "";
            txt_TtInternacional.Text = "";
            txt_PuertoDestCliente.Text = "";
            txt_TtPlantaCliente.Text = "";
            txt_EfectiviEntrega.Text = "";
            txt_LeadTimeEspera.Text = "";
        }

        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void cbo_MedioTrasnporte_TextChanged(object sender, EventArgs e)
        {
            cbo_TipoVehiculo.Items.Clear();
            ctrlseguidespachos.Listar_TipoVehiculo(cbo_TipoVehiculo, int.Parse(cbo_MedioTrasnporte.SelectedValue));
            lbl_IdMedTrasnp.Text = cbo_MedioTrasnporte.SelectedValue.ToString();
            Calcular_Concatenar();
        }

        protected void btn_GuardarDetalleDespaNal_Click(object sender, EventArgs e)
        {
            int respuesta;
            bool cumpleEntrega;
            string usuconect = Convert.ToString(Session["Usuario"]);
            string fechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);
            string idOfa= Convert.ToString(Session["idOfa"]);

            txt_NoGuiaNal.Text = txt_NoGuiaNal.Text.ToUpper();
            txt_NoGuiaNal.Text = txt_NoGuiaNal.Text.Trim();

            if (chk_CumpleEntregaNal.Checked == true)
            {
                cumpleEntrega = true;
            }
            else
            {
                cumpleEntrega = false;
            }

            ValidarCampos_DetalleDespaNal();

            if (btn_GuardarDetalleDespaNal.Text == "Guardar")
            {
                respuesta = ctrlseguidespachos.Registrar_Detalle_Despacho_Nacional(int.Parse(lbl_Desc_Id.Text), txt_OrdenNal.Text, txt_facturaNal.Text, int.Parse(lbl_cli_id.Text),int.Parse(lbl_ciu_id.Text),
                                                                                   txt_EmpresaTrasnpNal.Text, txt_FechaEntregaNal.Text, txt_NoGuiaNal.Text, txt_RespTranspNal.Text, decimal.Parse(txt_ValorExwNal.Text),
                                                                                   decimal.Parse(txt_FleteCotiNal.Text), decimal.Parse(txt_FleteRealNal.Text), txt_RelFleTValorNal.Text,
                                                                                   decimal.Parse(txt_PesoNal.Text), decimal.Parse(txt_StanByNal.Text), int.Parse(txt_Indicador.Text), cumpleEntrega,
                                                                                   usuconect, fechaCrea,txt_fechaDespacho.Text);

                            ctrlseguidespachos.Actualizar_NoFactura_DespachoNal(int.Parse(idOfa), txt_facturaNal.Text);
                            ctrlseguidespachos.Actualizar_NoGuia_DespachoNal(int.Parse(idOfa), txt_NoGuiaNal.Text);
             


                if (respuesta == 1)
                {
                    mensajeVentana("Registro guardado correctamente");

                    if (String.IsNullOrEmpty(txt_Indicador.Text))
                    {
                        txt_Indicador.Text = "-1";
                    }
                    else if (txt_Indicador.Text == "-1")
                    {
                        txt_Indicador.Text = "";
                    }
                    if (txt_RelFleTValorNal.Text == "0")
                    {
                        txt_RelFleTValorNal.Text = "";
                    }else if (txt_RelFleTValorNal.Text == "-1")
                    {
                        txt_RelFleTValorNal.Text = "";
                    }
                    if (cumpleEntrega == true)
                    {
                        chk_CumpleEntregaNal.Checked = true;
                    }
                    else
                    {
                        chk_CumpleEntregaNal.Checked = false;
                    }
                    Poblar_DetalleDespaNacional();
                    btn_GuardarDetalleDespaNal.Text = "Actualizar";
                }
                else
                {
                    mensajeVentana("El registro no se puedo guardar correctamente,verifique los datos");
                }
            }
            else if (btn_GuardarDetalleDespaNal.Text == "Actualizar")
            {
                if (chk_CumpleEntregaNal.Checked == true)
                {
                    cumpleEntrega = true;
                }
                else
                {
                    cumpleEntrega = false;
                }

                if (String.IsNullOrEmpty(txt_Indicador.Text))
                {
                    txt_Indicador.Text = "-1";
                }
               
                respuesta = ctrlseguidespachos.Actualizar_Detalle_Despacho_Nacional(txt_OrdenNal.Text, txt_facturaNal.Text,int.Parse(lbl_cli_id.Text), int.Parse(lbl_ciu_id.Text), txt_EmpresaTrasnpNal.Text,
                                                                                    txt_FechaEntregaNal.Text.ToString(), txt_NoGuiaNal.Text, txt_RespTranspNal.Text, decimal.Parse(txt_ValorExwNal.Text),
                                                                                    decimal.Parse(txt_FleteCotiNal.Text), decimal.Parse(txt_FleteRealNal.Text),txt_RelFleTValorNal.Text, 
                                                                                    decimal.Parse(txt_PesoNal.Text), decimal.Parse(txt_StanByNal.Text), int.Parse(txt_Indicador.Text), 
                                                                                    cumpleEntrega, txt_fechaDespacho.Text, int.Parse(lbl_Desc_Id.Text));

                            ctrlseguidespachos.Actualizar_NoFactura_DespachoNal(int.Parse(idOfa), txt_facturaNal.Text);
                            ctrlseguidespachos.Actualizar_NoGuia_DespachoNal(int.Parse(idOfa), txt_NoGuiaNal.Text);
                if (respuesta == 1)
                {
                    mensajeVentana("Registro actualizado correctamente");
                    if (String.IsNullOrEmpty(txt_Indicador.Text))
                    {
                        txt_Indicador.Text = "-1";
                    }
                    else if (txt_Indicador.Text == "-1")
                    {
                        txt_Indicador.Text = "";
                    }
                    if (txt_RelFleTValorNal.Text == "0")
                    {
                        txt_RelFleTValorNal.Text = "";
                    }
                    Poblar_DetalleDespaNacional();
                }
            }
        }

        protected void btnGuardarDetDesp_Click(object sender, EventArgs e)
        {
            int respuesta;
            string usuconect = Convert.ToString(Session["Usuario"]);
            string fechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);

            bool Inspeccion, detalleCerrado;


            if (chk_InspeccionSi.Checked == true)
            {
                Inspeccion = true;
            }
            else
            {
                Inspeccion = false;
            }

            if (chk_DetalleCerrado.Checked == true)
            {
                detalleCerrado = true;
                Inhabilitar_Campos_Detalle_Despa(true);
                btnGuardarDetDesp.Enabled = true;

            }
            else
            {
                detalleCerrado = false;
                Inhabilitar_Campos_Detalle_Despa(false);
            }

            if (String.IsNullOrEmpty(txt_NoGuiaDtos.Text))
            {
                txt_NoGuiaDtos.Text = "-1";

            }
            else if (txt_NoGuiaDtos.Text == "-1")
            {
                txt_NoGuiaDtos.Text = "";
            }
            if (String.IsNullOrEmpty(txt_DexFmm.Text))
            {
                txt_DexFmm.Text = "-1";

            }
            else if (txt_DexFmm.Text == "-1")
            {
                txt_DexFmm.Text = "";
            }

            txt_DocumentTrasnGuia.Text = txt_DocumentTrasnGuia.Text.ToUpper();
            txt_Embarcador.Text = txt_Embarcador.Text.ToUpper();

            txt_DexFmm.Text=txt_DexFmm.Text.Replace(" ", "");
            txt_NoGuiaDtos.Text= txt_NoGuiaDtos.Text.Replace(" ", "");

            Valida_Campos_DtsDetalle();
        

           if (btnGuardarDetDesp.Text == "Guardar")
           {
                respuesta = ctrlseguidespachos.Registrar_Detalle_Despacho(int.Parse(lbl_Desc_Id.Text), txt_DocumentTrasnGuia.Text.ToString(),
                                                                          txt_DexFmm.Text, txt_NoGuiaDtos.Text,
                                                                          int.Parse(cbo_EstatusCargaDet.SelectedValue.ToString()),
                                                                          txt_Embarcador.Text.ToString(), txtFechaEnvioDtos.Text.ToString(),
                                                                          int.Parse(txt_TiempoEnvioDtos.Text), int.Parse(txt_EfectividadEnvioDtos.Text),
                                                                          Inspeccion, txt_FechaEstimaDespa.Text.ToString(), txt_FechaRealDespa.Text.ToString(),
                                                                          txt_FechaEstimaZarpe.Text.ToString(), txt_FechaRealZarpe.Text.ToString(),
                                                                          txt_EstimadaArribo.Text.ToString(), txt_FechaConfirmaArribo.Text.ToString(),
                                                                          txt_FechaEstLlegaObra.Text.ToString(), txt_FechaRealLlegaObra.Text.ToString(),
                                                                          int.Parse(txt_TtInternacional.Text), int.Parse(txt_PuertoDestCliente.Text),
                                                                          int.Parse(txt_TtPlantaCliente.Text), int.Parse(txt_EfectiviEntrega.Text),
                                                                          usuconect, fechaCrea, detalleCerrado,txt_fechaDocumentado.Text, txt_ModTransDespa.Text,
                                                                          txt_EstimadaArriboMod.Text.ToString(), txt_FechaEstLlegaObraMod.Text.ToString());
                if (respuesta == 1)
                {
                    mensajeVentana("Registro guardado correctamente");
                    Valida_Campos_DtsDetalle();
                    if (String.IsNullOrEmpty(txt_NoGuiaDtos.Text))
                    {
                        txt_NoGuiaDtos.Text = "-1";
                    }
                    else if (txt_NoGuiaDtos.Text == "-1")
                    {
                        txt_NoGuiaDtos.Text = "";
                    }
                    if (String.IsNullOrEmpty(txt_DexFmm.Text))
                    {
                        txt_DexFmm.Text = "-1";
                    }
                    else if (txt_DexFmm.Text == "-1")
                    {
                        txt_DexFmm.Text = "";
                    }
                    if (chk_DetalleCerrado.Checked == true)
                    {
                        detalleCerrado = true;
                        Inhabilitar_Campos_Detalle_Despa(true);
                        btnGuardarDetDesp.Enabled = true;
                    }
                    else
                    {
                        detalleCerrado = false;
                        Inhabilitar_Campos_Detalle_Despa(false);
                    }
                    btnGuardarDetDesp.Text = "Actualizar";
                }
                else
                {
                    mensajeVentana("El registro no se puedo guardar correctamente,verifique los datos");
                }
            }
            else if (btnGuardarDetDesp.Text == "Actualizar")
            {
             
                respuesta = ctrlseguidespachos.Actualizar_Detalle_Despacho(txt_DocumentTrasnGuia.Text.ToString(), txt_DexFmm.Text, txt_NoGuiaDtos.Text,
                                                               int.Parse(cbo_EstatusCargaDet.SelectedValue.ToString()), txt_Embarcador.Text.ToString(), txtFechaEnvioDtos.Text.ToString(),
                                                               int.Parse(txt_TiempoEnvioDtos.Text), int.Parse(txt_EfectividadEnvioDtos.Text), Inspeccion, txt_FechaEstimaDespa.Text.ToString(),
                                                               txt_FechaRealDespa.Text.ToString(), txt_FechaEstimaZarpe.Text.ToString(), txt_FechaRealZarpe.Text.ToString(),
                                                               txt_EstimadaArribo.Text.ToString(), txt_FechaConfirmaArribo.Text.ToString(), txt_FechaEstLlegaObra.Text.ToString(),
                                                               txt_FechaRealLlegaObra.Text.ToString(), int.Parse(txt_TtInternacional.Text), int.Parse(txt_PuertoDestCliente.Text),
                                                               int.Parse(txt_TtPlantaCliente.Text), int.Parse(txt_EfectiviEntrega.Text), detalleCerrado,txt_fechaDocumentado.Text, txt_ModTransDespa.Text,
                                                               int.Parse(lbl_Desc_Id.Text), txt_EstimadaArriboMod.Text.ToString(), txt_FechaEstLlegaObraMod.Text.ToString());
               

                if (respuesta == 1)
                {
                    mensajeVentana("Registro actualizado correctamente");
                    Valida_Campos_DtsDetalle();
                    Valida_Despacho_CerradoCompleto();
                    if (String.IsNullOrEmpty(txt_NoGuiaDtos.Text))
                    {
                        txt_NoGuiaDtos.Text = "-1";

                    }
                    else if (txt_NoGuiaDtos.Text == "-1")
                    {
                        txt_NoGuiaDtos.Text = "";
                    }
                    if (String.IsNullOrEmpty(txt_DexFmm.Text))
                    {
                        txt_DexFmm.Text = "-1";

                    }
                    else if (txt_DexFmm.Text == "-1")
                    {
                        txt_DexFmm.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("El registro no se puedo actualizar, verifique los datos");
                }
            }
        }


        public void Valida_Despacho_CerradoCompleto()
        {            
            if (chk_Tramite_Cerrado.Checked == true && chk_DetalleCerrado.Checked == true)
            {
                cbo_EstatusCargaDet.Enabled = false;
                btnGuardarDetDesp.Enabled = false;            
            }             
        }



        protected void btn_Guardar_Tramite_Click(object sender, EventArgs e)
        {
            bool detTramiteCerrado;
            int respuesta;

            if (chk_Tramite_Cerrado.Checked == true)
            {
                detTramiteCerrado = true;
                Inhabilitar_Campos_Tramites(true);
            }
            else
            {
                 detTramiteCerrado = false;
                 Inhabilitar_Campos_Tramites(false);
            }

            //Valida si existe un registro en Despa_Detalle, para poder guardar los datos de tramites
            if (ctrlseguidespachos.Valida_Existencia_DetaDespacho(Convert.ToInt32(lbl_Desc_Id.Text)).Rows.Count != 0)
            {
            if (cbo_Canal.SelectedIndex != 0)
            {
                if (String.IsNullOrEmpty(txt_Dias_libres.Text))
                {
                    txt_Dias_libres.Text = "99";
                }          

                respuesta = ctrlseguidespachos.Actualizar_Tramites_Despacho (txt_Finalizacion.Text.ToString(), Convert.ToInt32(txt_Dias_libres.Text),
                                                                         txt_Inspeccion.Text.ToString(), Convert.ToInt32(cbo_Canal.SelectedValue.ToString()),
                                                                         txt_Nacionalizacion.Text.ToString(), txt_FacturacionProveed.Text.ToString(), txt_CI_impuestos.Text.ToString(),
                                                                         txt_Retiro_Conten.Text.ToString(), txt_Desove.Text.ToString(), txt_Almacenamiento.Text.ToString(),
                                                                         txt_Notifica_Cliente.Text.ToString(), txt_Devolucion_Conten.Text.ToString(), txt_Facturacion_ForsaCliente.Text.ToString(),
                                                                         txt_Fecha_Carga_Cliente.Text.ToString(), txt_Entrega_Obra.Text.ToString(), detTramiteCerrado, Convert.ToInt32(lbl_Desc_Id.Text));
                if (respuesta == 1)
                {
                    if (txt_Dias_libres.Text == "99")
                    {
                        txt_Dias_libres.Text = "";
                    }
                    if (chk_Tramite_Cerrado.Checked == true)
                    {                      
                        Inhabilitar_Campos_Tramites(true);
                    }
                    else
                    {                       
                        Inhabilitar_Campos_Tramites(false);
                    }
                        Valida_Despacho_CerradoCompleto();
                        mensajeVentana("Registro guardado correctamente");
                }
                else
                {
                    mensajeVentana("El registro no se puedo guardar, verifique los datos");
                    if (txt_Dias_libres.Text == "99")
                    {
                        txt_Dias_libres.Text = "";
                    }
                }
            }
            else
            {
                mensajeVentana("Debe elegir un canal");
                cbo_Canal.Focus();
            }
            }
            else
            {
                mensajeVentana("Debe guardar primero el detalle maestro");
                btnGuardarDetDesp.Focus();
            }
        }


        public void Inhabilitar_Campos_Tramites(bool IN)
        {
            if (IN == true)
            {
                txt_Finalizacion.Enabled = false;
                txt_Dias_libres.Enabled = false;
                txt_Inspeccion.Enabled = false;
                cbo_Canal.Enabled = false;
                txt_Nacionalizacion.Enabled = false;
                txt_FacturacionProveed.Enabled = false;
                txt_CI_impuestos.Enabled = false;
                txt_Retiro_Conten.Enabled = false;
                txt_Desove.Enabled = false;
                txt_Almacenamiento.Enabled = false;
                txt_Notifica_Cliente.Enabled = false;
                txt_Devolucion_Conten.Enabled = false;
                txt_Facturacion_ForsaCliente.Enabled = false;
                txt_Fecha_Carga_Cliente.Enabled = false;
                txt_Entrega_Obra.Enabled = false;
                chk_Tramite_Cerrado.Enabled = false;
                btn_Guardar_Tramite.Enabled = false;
            }
            else
            {
                txt_Finalizacion.Enabled = true;
                txt_Dias_libres.Enabled = true;
                txt_Inspeccion.Enabled = true;
                cbo_Canal.Enabled = true;
                txt_Nacionalizacion.Enabled = true;
                txt_FacturacionProveed.Enabled = true;
                txt_CI_impuestos.Enabled = true;
                txt_Retiro_Conten.Enabled = true;
                txt_Desove.Enabled = true;
                txt_Almacenamiento.Enabled = true;
                txt_Notifica_Cliente.Enabled = true;
                txt_Devolucion_Conten.Enabled = true;
                txt_Facturacion_ForsaCliente.Enabled = true;
                txt_Fecha_Carga_Cliente.Enabled = true;
                txt_Entrega_Obra.Enabled = true;
                chk_Tramite_Cerrado.Enabled = true;
                btn_Guardar_Tramite.Enabled = true;
            }
        }


        protected void btn_GuardarGast_Click(object sender, EventArgs e)
        {
            string usuconect = Convert.ToString(Session["Usuario"]);
            string fechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
            bool cerrado;

              if (chk_Cerrado.Checked == true)
                {
                    cerrado = true;
                }
                else
                {
                    cerrado = false;
                }

            int respuesta;

           Validar_Campos_Dtos_Gastos();


            if (btn_GuardarGast.Text == "Guardar")
            {                          
                respuesta = ctrlseguidespachos.Guardar_Dtos_GastosOperacion(int.Parse(lbl_Desc_Id.Text), decimal.Parse(txt_FleteNalProvi.Text), decimal.Parse(txt_FletNalReal.Text),
                                                                            decimal.Parse(txt_AgradeAduanProvi.Text), decimal.Parse(txt_AgradeAduanReal.Text), decimal.Parse(txt_GastPuertoProvision.Text),
                                                                            decimal.Parse(txt_GastPuertoReal.Text), decimal.Parse(txt_FleteInterProvision.Text), decimal.Parse(txt_FleteInterReal.Text),
                                                                            decimal.Parse(txt_GastDestinoProvision.Text), decimal.Parse(txt_GastDestinoReal.Text), decimal.Parse(txt_SelloSateliProvi.Text),
                                                                            decimal.Parse(txt_SelloSateliReal.Text), decimal.Parse(txt_TotalProvi.Text), decimal.Parse(txt_TotalGastFletreal.Text),
                                                                            decimal.Parse(txt_Difprovi.Text), txt_PorcentajeAhorro.Text, decimal.Parse(txt_StandBy.Text),
                                                                            decimal.Parse(txt_TrmFechafact.Text), decimal.Parse(txt_BodegajeReal.Text), decimal.Parse(txt_PesoFactura.Text),
                                                                            txt_EnvioCtrlEmpaque.Text, txt_Concatenar.Text, cerrado,usuconect, fechaCrea,decimal.Parse(txt_GastFletFacturados.Text),
                                                                            decimal.Parse(txt_Seguro.Text), decimal.Parse(txt_inspAntiNarcot.Text), decimal.Parse(txt_RollOver.Text),
                                                                            decimal.Parse(txt_ManejoFlete.Text), decimal.Parse(txt_AduanaDestino.Text), decimal.Parse(txt_BodegajeDestino.Text),
                                                                             decimal.Parse(txt_TranspDestino.Text), decimal.Parse(txt_DemoraDestino.Text), decimal.Parse(txt_ImpuestoDestino.Text));
                if (respuesta == 1)
                {
                    mensajeVentana("Registro guardado correctamente");
                    if (chk_Cerrado.Checked == true)
                    {
                        cerrado = true;
                    }
                    else
                    {
                        cerrado = false;
                    }
                    btn_GuardarGast.Text = "Actualizar";
                    Poblar_GastosOperacionales(int.Parse(lbl_Desc_Id.Text));
                }
                else
                {
                    mensajeVentana("El registro no se puedo guardar correctamente,verifique los datos");
                }
            }
            else if (btn_GuardarGast.Text == "Actualizar")
            {
         
                respuesta = ctrlseguidespachos.Actualizar_Dtos_GastosOperacion(decimal.Parse(txt_FleteNalProvi.Text), decimal.Parse(txt_FletNalReal.Text),
                                                                            decimal.Parse(txt_AgradeAduanProvi.Text), decimal.Parse(txt_AgradeAduanReal.Text), decimal.Parse(txt_GastPuertoProvision.Text),
                                                                            decimal.Parse(txt_GastPuertoReal.Text), decimal.Parse(txt_FleteInterProvision.Text), decimal.Parse(txt_FleteInterReal.Text),
                                                                            decimal.Parse(txt_GastDestinoProvision.Text), decimal.Parse(txt_GastDestinoReal.Text), decimal.Parse(txt_SelloSateliProvi.Text),
                                                                            decimal.Parse(txt_SelloSateliReal.Text), decimal.Parse(txt_TotalProvi.Text), decimal.Parse(txt_TotalGastFletreal.Text),
                                                                            decimal.Parse(txt_Difprovi.Text), txt_PorcentajeAhorro.Text, decimal.Parse(txt_StandBy.Text),
                                                                            decimal.Parse(txt_TrmFechafact.Text), decimal.Parse(txt_BodegajeReal.Text), decimal.Parse(txt_PesoFactura.Text),
                                                                            txt_EnvioCtrlEmpaque.Text, txt_Concatenar.Text, cerrado,decimal.Parse(txt_GastFletFacturados.Text),
                                                                            decimal.Parse(txt_Seguro.Text), decimal.Parse(txt_inspAntiNarcot.Text), decimal.Parse(txt_RollOver.Text),
                                                                            decimal.Parse(txt_ManejoFlete.Text), decimal.Parse(txt_AduanaDestino.Text), decimal.Parse(txt_BodegajeDestino.Text),
                                                                            decimal.Parse(txt_TranspDestino.Text), decimal.Parse(txt_DemoraDestino.Text), decimal.Parse(txt_ImpuestoDestino.Text),
                                                                            (int.Parse(lbl_Desc_Id.Text)));

                  if (respuesta == 1)
                {
                    mensajeVentana("Registro  actualizado correctamente");                             
                    btn_GuardarGast.Text = "Actualizar";
                    Poblar_GastosOperacionales(int.Parse(lbl_Desc_Id.Text));
                }
                else
                {
                    mensajeVentana("El registro no se puedo actualizar correctamente, verifique los datos");
                }
            }
        }

        public void ValidarCampos_DetalleDespaNal()
        {
            if (String.IsNullOrEmpty(txt_FleteRealNal.Text))
            {
                txt_FleteRealNal.Text = "-1";
            }
            else if (txt_FleteRealNal.Text == "-1")
            {
                txt_FleteRealNal.Text = "";
            }

            if (String.IsNullOrEmpty(txt_RelFleTValorNal.Text))
            {
                txt_RelFleTValorNal.Text = "-1";
            }
            else if (txt_RelFleTValorNal.Text == "-1")
            {
                txt_RelFleTValorNal.Text = "";
            }


            if (String.IsNullOrEmpty(txt_StanByNal.Text))
            {
                txt_StanByNal.Text = "-1";
            }
            else if (txt_StanByNal.Text == "-1")
            {
                txt_StanByNal.Text = "";
            }


            if (String.IsNullOrEmpty(txt_ValorExwNal.Text))
            {
                txt_ValorExwNal.Text = "0";
            }
            if (String.IsNullOrEmpty(txt_FleteCotiNal.Text))
            {
                txt_FleteCotiNal.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_Indicador.Text))
            {
                txt_Indicador.Text = "-1";
            }else if (txt_Indicador.Text == "-1")
            {
                txt_Indicador.Text = "";
            }
        }

        public void Validar_Campos_Dtos_Gastos()
        {
            if (String.IsNullOrEmpty(txt_FleteNalProvi.Text))
            {
                txt_FleteNalProvi.Text = "0";
            }
            if (String.IsNullOrEmpty(txt_FletNalReal.Text))
            {
                txt_FletNalReal.Text = "0";
             }

            if (String.IsNullOrEmpty(txt_AgradeAduanProvi.Text))
            {
                txt_AgradeAduanProvi.Text = "0";
            }
                
            if (String.IsNullOrEmpty(txt_AgradeAduanReal.Text))
            {
                txt_AgradeAduanReal.Text = "0";
            }
            
            if (String.IsNullOrEmpty(txt_GastPuertoProvision.Text))
            {
                txt_GastPuertoProvision.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_GastPuertoReal.Text))
            {
                txt_GastPuertoReal.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_FleteInterProvision.Text))
            {
                txt_FleteInterProvision.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_FleteInterReal.Text))
            {
                txt_FleteInterReal.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_GastDestinoProvision.Text))
            {
                txt_GastDestinoProvision.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_GastDestinoReal.Text))
            {
                txt_GastDestinoReal.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_SelloSateliProvi.Text))
            {
                txt_SelloSateliProvi.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_SelloSateliReal.Text))
            {
                txt_SelloSateliReal.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_TotalProvi.Text))
            {
                txt_TotalProvi.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_TotalGastFletreal.Text))
            {
                txt_TotalGastFletreal.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_GastFletFacturados.Text))
            {
                txt_GastFletFacturados.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_StandBy.Text))
            {
                txt_StandBy.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_TrmFechafact.Text))
            {
                txt_TrmFechafact.Text = "0";
            }
        
            if (String.IsNullOrEmpty(txt_BodegajeReal.Text))
            {
                txt_BodegajeReal.Text = "0";
            }
          
            if (String.IsNullOrEmpty(txt_PesoFactura.Text))
            {
                txt_PesoFactura.Text = "0";
            }
         
            if (String.IsNullOrEmpty(txt_Difprovi.Text))
            {
                txt_Difprovi.Text = "0";
            }
           
            if (String.IsNullOrEmpty(txt_PorcentajeAhorro.Text))
            {
                txt_PorcentajeAhorro.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_Seguro.Text))
            {
                txt_Seguro.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_inspAntiNarcot.Text))
            {
                txt_inspAntiNarcot.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_RollOver.Text))
            {
                txt_RollOver.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_ManejoFlete.Text))
            {
                txt_ManejoFlete.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_AduanaDestino.Text))
            {
                txt_AduanaDestino.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_BodegajeDestino.Text))
            {
                txt_BodegajeDestino.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_TranspDestino.Text))
            {
                txt_TranspDestino.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_DemoraDestino.Text))
            {
                txt_DemoraDestino.Text = "0";
            }

            if (String.IsNullOrEmpty(txt_ImpuestoDestino.Text))
            {
                txt_ImpuestoDestino.Text = "0";
            }
        }


        public void Validar_Campos_Gastos_DestinoBrasil()
        {
            if (String.IsNullOrEmpty(txt_trm.Text)){txt_trm.Text = "0";}
            if (String.IsNullOrEmpty(txt_liberaHblProv.Text)) { txt_liberaHblProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_liberaHblReal.Text)) { txt_liberaHblReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_droppOffProvi.Text)) { txt_droppOffProvi.Text = "0"; }
            if (String.IsNullOrEmpty(txt_droppOffReal.Text)) { txt_droppOffReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_taxaAdminProv.Text)) { txt_taxaAdminProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_taxaAdminReal.Text)) { txt_taxaAdminReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_ispsProv.Text)) { txt_ispsProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_ispsReal.Text)) { txt_ispsReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_otrosGastosProv.Text)) { txt_otrosGastosProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_otrosGastosReal.Text)) { txt_otrosGastosReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_1erPeriodoProv.Text)) { txt_1erPeriodoProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_1erPeriodoReal.Text)) { txt_1erPeriodoReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_2doPeriodoProv.Text)) { txt_2doPeriodoProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_2doPeriodoReal.Text)) { txt_2doPeriodoReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_3erPeriodoProv.Text)) { txt_3erPeriodoProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_3erPeriodoReal.Text)) { txt_3erPeriodoReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_escanContenProv.Text)) { txt_escanContenProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_escanContenReal.Text)) { txt_escanContenReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_insoMapaProv.Text)) { txt_insoMapaProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_insoMapaReal.Text)) { txt_insoMapaReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_corretagenProv.Text)) { txt_corretagenProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_corretagenReal.Text)) { txt_corretagenReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_demurrageProv.Text)) { txt_demurrageProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_demurrageReal.Text)) { txt_demurrageReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_despaHonoraProv.Text)) { txt_despaHonoraProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_despaHonoraReal.Text)) { txt_despaHonoraReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_margenTimboProv.Text)) { txt_margenTimboProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_margenTimboReal.Text)) { txt_margenTimboReal.Text = "0"; }
            if (String.IsNullOrEmpty(txt_fleteTerresProv.Text)) { txt_fleteTerresProv.Text = "0"; }
            if (String.IsNullOrEmpty(txt_fleteTerresReal.Text)) { txt_fleteTerresReal.Text = "0"; }
        }

        //REGION
        #region CALCULOS TOTAL DE LOS GASTOS REALES
        public void Calcular_Total_Gastos_Reales()
        {
            decimal fletNal = 0, aduanero = 0, puerto = 0,
                    fleteInt = 0, destino = 0, satelital = 0,
                    total = 0, bodegaje=0, trmvalor=0, stanBy=0,
                    impdest=0, maneflete=0, rollover = 0, inspantinar = 0,
                    aduadest = 0, bodegadest = 0,transdest = 0, demordest = 0;
          
            if (!String.IsNullOrEmpty(txt_FletNalReal.Text))
            {
                fletNal = decimal.Parse(txt_FletNalReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_AgradeAduanReal.Text))
            {
                aduanero = decimal.Parse(txt_AgradeAduanReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_GastPuertoReal.Text))
            {
                puerto = decimal.Parse(txt_GastPuertoReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_FleteInterReal.Text))
            {
                fleteInt = decimal.Parse(txt_FleteInterReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_GastDestinoReal.Text))
            {
                destino = decimal.Parse(txt_GastDestinoReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_SelloSateliReal.Text))
            {
                satelital = decimal.Parse(txt_SelloSateliReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_TrmFechafact.Text))
            {
               trmvalor = decimal.Parse(txt_TrmFechafact.Text);
            }
            if (!String.IsNullOrEmpty(txt_BodegajeReal.Text))
            {
                bodegaje = decimal.Parse(txt_BodegajeReal.Text);
            }
            if (!String.IsNullOrEmpty(txt_StandBy.Text))
            {
                stanBy = decimal.Parse(txt_StandBy.Text);
            }
            if (!String.IsNullOrEmpty(txt_ImpuestoDestino.Text))
            {
                impdest = decimal.Parse(txt_ImpuestoDestino.Text);
            }
            if (!String.IsNullOrEmpty(txt_ManejoFlete.Text))
            {
                maneflete = decimal.Parse(txt_ManejoFlete.Text);
            }
            if (!String.IsNullOrEmpty(txt_RollOver.Text))
            {
                rollover = decimal.Parse(txt_RollOver.Text);
            }
            if (!String.IsNullOrEmpty(txt_inspAntiNarcot.Text))
            {
                inspantinar = decimal.Parse(txt_inspAntiNarcot.Text);
            }
            if (!String.IsNullOrEmpty(txt_AduanaDestino.Text))
            {
                aduadest = decimal.Parse(txt_AduanaDestino.Text);
            }
            if (!String.IsNullOrEmpty(txt_BodegajeDestino.Text))
            {
                bodegadest = decimal.Parse(txt_BodegajeDestino.Text);
            }
            if (!String.IsNullOrEmpty(txt_TranspDestino.Text))
            {
                transdest = decimal.Parse(txt_TranspDestino.Text);
            }
            if (!String.IsNullOrEmpty(txt_DemoraDestino.Text))
            {
                demordest = decimal.Parse(txt_DemoraDestino.Text);
            }
            total = fletNal + aduanero + puerto + fleteInt + destino + satelital + bodegaje + stanBy + impdest +
                    maneflete + rollover + inspantinar + aduadest + bodegadest + transdest + demordest;

            txt_TotalGastFletreal.Text = Convert.ToString(total.ToString("#,##.##"));
        }

        protected void txt_FletNalReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();         
        }
        protected void txt_AgradeAduanReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();            
        }

        protected void txt_GastPuertoReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();         
        }

        protected void txt_FleteInterReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();        
        }

        protected void txt_GastDestinoReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();           
        }

        protected void txt_SelloSateliReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }
        protected void txt_BodegajeReal_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }
        protected void txt_StandBy_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_AduanaDestino_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_DemoraDestino_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_RollOver_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_inspAntiNarcot_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_BodegajeDestino_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_ManejoFlete_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_ImpuestoDestino_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }

        protected void txt_TranspDestino_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Reales();
        }
        #endregion

        //REGION
        #region CALCULOS TOTAL DE LOS GASTOS PROVISIONALES

        public void Calcular_Total_Gastos_Provisionales()
        {
            decimal fletNal = 0, aduanero = 0, puerto = 0,
                    fleteInt = 0, destino = 0, satelital = 0,
                    total = 0, seguro=0;
            if (!String.IsNullOrEmpty(txt_FleteNalProvi.Text))
            {
                fletNal = decimal.Parse(txt_FleteNalProvi.Text);
            }
            if (!String.IsNullOrEmpty(txt_AgradeAduanProvi.Text))
            {
                aduanero = decimal.Parse(txt_AgradeAduanProvi.Text);
            }
            if (!String.IsNullOrEmpty(txt_GastPuertoProvision.Text))
            {
                puerto = decimal.Parse(txt_GastPuertoProvision.Text);
            }
            if (!String.IsNullOrEmpty(txt_FleteInterProvision.Text))
            {
                fleteInt = decimal.Parse(txt_FleteInterProvision.Text);
            }
            if (!String.IsNullOrEmpty(txt_GastDestinoProvision.Text))
            {
                destino = decimal.Parse(txt_GastDestinoProvision.Text);
            }
            if (!String.IsNullOrEmpty(txt_SelloSateliProvi.Text))
            {
                satelital = decimal.Parse(txt_SelloSateliProvi.Text);
            }
            if (!String.IsNullOrEmpty(txt_Seguro.Text))
            {
                seguro = decimal.Parse(txt_Seguro.Text);
            }

            total = fletNal + aduanero + puerto + fleteInt + destino + satelital + seguro;

            txt_TotalProvi.Text = Convert.ToString(total.ToString("#,##.##"));
        }
        protected void txt_FleteNalProvi_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();      
            Calcular_Ahorro();
        }

        protected void txt_AgradeAduanProvi_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();          
            Calcular_Ahorro();
        }

       
        protected void txt_GastPuertoProvision_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();    
            Calcular_Ahorro();
        }

        protected void txt_FleteInterProvision_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();       
            Calcular_Ahorro();
        }

        protected void txt_GastDestinoProvision_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();      
            Calcular_Ahorro();
        }

        protected void txt_SelloSateliProvi_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();     
            Calcular_Ahorro();
        }


        protected void txt_Seguro_TextChanged(object sender, EventArgs e)
        {
            Calcular_Total_Gastos_Provisionales();
            Calcular_DiffProvisiones();
            Calcular_Ahorro();
        }
        #endregion   

        protected void txt_TiempoEnvioDtos_TextChanged(object sender, EventArgs e)
        {           
            if ((String.IsNullOrEmpty(txt_TiempoEnvioDtos.Text) || String.IsNullOrEmpty(txt_FechaRealDespa.Text)))
            {
                txt_TiempoEnvioDtos.Text = "";
            }
            else if (!String.IsNullOrEmpty(txt_TiempoEnvioDtos.Text))
                {
                    int TiempoEnvioDtos = int.Parse(txt_TiempoEnvioDtos.Text);

                    if (TiempoEnvioDtos < 4)
                    {
                        txt_EfectividadEnvioDtos.Text = "SI";
                    }
                    else
                    {
                        txt_EfectividadEnvioDtos.Text = "NO";
                    }
                }
                else
                {
                    txt_EfectividadEnvioDtos.Text = "";
                }
            }
        
     //Campos calculados
        public void Calcular_TtInternacional()
        {
            string ReaArribo, ReaZarpe;
            DateTime resultado3;
            ReaArribo = txt_FechaConfirmaArribo.Text;
            ReaZarpe =txt_FechaRealZarpe.Text;

            if ((!String.IsNullOrEmpty(txt_FechaConfirmaArribo.Text) && !String.IsNullOrEmpty(txt_FechaRealZarpe.Text)))
            {
                if (DateTime.TryParse(ReaArribo, out resultado3))
                {
                    if (DateTime.TryParse(ReaZarpe, out resultado3))
                    {
                        DateTime ConfirmaArribo = DateTime.Parse(txt_FechaConfirmaArribo.Text);
                        DateTime FechaRealZarpe = DateTime.Parse(txt_FechaRealZarpe.Text);

                        TimeSpan ts = ConfirmaArribo - FechaRealZarpe;

                        int differenceInDays = ts.Days;

                        txt_TtInternacional.Text = differenceInDays.ToString();
                    }
                    else
                    {
                        mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Real Zarpe");
                        txt_FechaRealZarpe.Focus();
                        txt_FechaRealZarpe.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Confirmada Arribo");
                    txt_FechaConfirmaArribo.Focus();
                    txt_FechaConfirmaArribo.Text = "";
                }              
            }
            else
            {
                txt_TtInternacional.Text = "";
            }
        }

        public void Calcular_Tt_PuertoDestinoPlanta()
        {
            string ReaArribo, ReaObra;
            DateTime resultado3;
            ReaArribo = txt_FechaConfirmaArribo.Text;
            ReaObra = txt_FechaRealLlegaObra.Text;

            if ((!String.IsNullOrEmpty(txt_FechaConfirmaArribo.Text) && !String.IsNullOrEmpty(txt_FechaRealLlegaObra.Text)))
            {
                if (DateTime.TryParse(ReaArribo, out resultado3))
                {
                    if (DateTime.TryParse(ReaObra, out resultado3))
                    {

                        DateTime ConfirmaArribo = DateTime.Parse(txt_FechaConfirmaArribo.Text);
                        DateTime FechaRealLlegaObra = DateTime.Parse(txt_FechaRealLlegaObra.Text);

                        TimeSpan ts = FechaRealLlegaObra - ConfirmaArribo;

                        int differenceInDays = ts.Days;

                        txt_PuertoDestCliente.Text = differenceInDays.ToString();

                    }
                    else
                    {
                        mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Real Llegada Obra");
                        txt_FechaRealLlegaObra.Focus();
                        txt_FechaRealLlegaObra.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Confirmada Arribo");
                    txt_FechaConfirmaArribo.Focus();
                    txt_FechaConfirmaArribo.Text = "";
                }
            }
            else
            {
                txt_PuertoDestCliente.Text = "";
            }
        }

        public void Calcular_TiempoEnvioDtos()
        {
            DateTime d2 = DateTime.Now;
            string  ReaDespa,envioDoc;
            DateTime resultado3;
            ReaDespa = txt_FechaRealDespa.Text;
            envioDoc = txtFechaEnvioDtos.Text;

            if ((!String.IsNullOrEmpty(txtFechaEnvioDtos.Text) && !String.IsNullOrEmpty(txt_FechaRealDespa.Text)))
            {
                if (DateTime.TryParse(ReaDespa, out resultado3))
                {
                    if (DateTime.TryParse(envioDoc, out resultado3))
                    {
                        DateTime FechaEnvioDtos = DateTime.Parse(txtFechaEnvioDtos.Text);
                        DateTime FechaRealDespa = DateTime.Parse(txt_FechaRealDespa.Text);

                        TimeSpan ts = FechaEnvioDtos - FechaRealDespa;

                        int differenceInDays = ts.Days;

                        differenceInDays = differenceInDays - 2;

                        txt_TiempoEnvioDtos.Text = differenceInDays.ToString();

                        if (differenceInDays < 4)
                        {
                            txt_EfectividadEnvioDtos.Text = "SI";
                        }
                        else
                        {
                            txt_EfectividadEnvioDtos.Text = "NO";
                        }
                    }
                    else
                    {
                        mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Fecha Envio Dtos");
                        txtFechaEnvioDtos.Focus();
                        txtFechaEnvioDtos.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Real Despacho");
                    txt_FechaRealDespa.Focus();
                    txt_FechaRealDespa.Text = "";
                }
            }
                    else
                    {
                        txt_TiempoEnvioDtos.Text = "";
                    }                         
        }

        public void Calcular_Tt_PlantaCliente()
        {
            string ReaDespa, ReaObra;
            DateTime resultado3;
            ReaDespa = txt_FechaRealDespa.Text;
            ReaObra = txt_FechaRealLlegaObra.Text;

            if ((!String.IsNullOrEmpty(txt_FechaRealDespa.Text) && !String.IsNullOrEmpty(txt_FechaRealLlegaObra.Text)))
            {
                if (DateTime.TryParse(ReaDespa, out resultado3))
                {
                    if (DateTime.TryParse(ReaObra, out resultado3))
                    {
                        DateTime FechaRealDespa = DateTime.Parse(txt_FechaRealDespa.Text);
                        DateTime FechaRealLlegaObra = DateTime.Parse(txt_FechaRealLlegaObra.Text);

                        TimeSpan ts = FechaRealLlegaObra - FechaRealDespa;

                        int differenceInDays = ts.Days;

                        txt_TtPlantaCliente.Text = differenceInDays.ToString();

                        //Calcula la efectividad de la entrega

                        if (!String.IsNullOrEmpty(txt_LeadTimeEspera.Text))
                        {
                            int leadTime = int.Parse(txt_LeadTimeEspera.Text);

                            if (differenceInDays <= leadTime)
                            {
                                txt_EfectiviEntrega.Text = "SI";
                            }
                            else
                            {
                                txt_EfectiviEntrega.Text = "NO";
                            }
                        }
                        else
                        {

                        }
                    }
                    else
                    {
                        mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Real Llegada Obra");
                        txt_FechaRealLlegaObra.Focus();
                        txt_FechaRealLlegaObra.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Real Despacho");
                    txt_FechaRealDespa.Focus();
                    txt_FechaRealDespa.Text = "";
                }            
            }
            else
            {
                txt_TtPlantaCliente.Text = "";
            }
        }

        public void Calcular_Gastos_Fletes_Facturados()
        {
            decimal valorExw = 0, valorTolFacturado = 0,
                   total = 0;
            if (!String.IsNullOrEmpty(txt_ValorExw.Text))
            {
                valorExw = decimal.Parse(txt_ValorExw.Text);
            }
            if (!String.IsNullOrEmpty(txt_ValorTolFactura.Text))
             {
                valorTolFacturado = decimal.Parse(txt_ValorTolFactura.Text);
            }
            total =  valorTolFacturado-valorExw;

            if (total==0)
            {
                txt_GastFletFacturados.Text = "0";
            }
            else
            {
                txt_GastFletFacturados.Text = Convert.ToString(total.ToString("#,##.##"));
            }        
        }

        public void Calcular_DiffProvisiones()
        {
            decimal gastFletFact = 0, totalProvis = 0,
                  total = 0;
            if (!String.IsNullOrEmpty(txt_GastFletFacturados.Text))
            {
                gastFletFact = decimal.Parse(txt_GastFletFacturados.Text);
            }
            if (!String.IsNullOrEmpty(txt_TotalProvi.Text))
            {
                totalProvis = decimal.Parse(txt_TotalProvi.Text);
            }
            total = gastFletFact - totalProvis;

           txt_Difprovi.Text = Convert.ToString(total.ToString("#,##.##"));
        }

        public void Calcular_Ahorro()
        {
            decimal gastFletFact = 0, totalProvis = 0,
                total = 0;

            if ((!String.IsNullOrEmpty(txt_GastFletFacturados.Text) && !String.IsNullOrEmpty(txt_TotalProvi.Text)))
            {
                if (txt_GastFletFacturados.Text == "0")
                {

                }
                else
                {
                    gastFletFact = decimal.Parse(txt_GastFletFacturados.Text);
                    totalProvis = decimal.Parse(txt_TotalProvi.Text);
                    total = totalProvis / gastFletFact;

                    txt_PorcentajeAhorro.Text = Convert.ToString(total.ToString("P"));
                }              
            }
            else
            {
                txt_PorcentajeAhorro.Text = "";
            }       
        }
        //=============================================================       


        //Agrupa todos los TextChanged
        #region Text_Changes              
        protected void txt_FechaEstimaDespa_TextChanged(object sender, EventArgs e)
        {
            string estDespa;
            estDespa = txt_FechaEstimaDespa.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_FechaEstimaDespa.Text))
            {
                if (DateTime.TryParse(estDespa, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Estimada Despacho");
                    txt_FechaEstimaDespa.Focus();
                    txt_FechaEstimaDespa.Text = "";
                }
            }
        }

        protected void txtFechaEnvioDtos_TextChanged(object sender, EventArgs e)
        {
            string envioDoc;
            envioDoc = txtFechaEnvioDtos.Text;
            DateTime validaFecha;


            Calcular_TiempoEnvioDtos();

            if (!String.IsNullOrEmpty(txtFechaEnvioDtos.Text) && (!String.IsNullOrEmpty(txt_FechaRealDespa.Text)))
            {
                txt_TiempoEnvioDtos.ReadOnly = false;
            }
            else
            {
                txt_TiempoEnvioDtos.ReadOnly = true;
            }

            if (String.IsNullOrEmpty(txtFechaEnvioDtos.Text))
            {
                txt_EfectividadEnvioDtos.Text = "";
            }
            else
            {              
                if (DateTime.TryParse(envioDoc, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Fecha Envio Dtos");
                    txtFechaEnvioDtos.Focus();
                    txtFechaEnvioDtos.Text = "";
                }
            }
        }
        
        protected void txt_FechaRealDespa_TextChanged(object sender, EventArgs e)
        {           
            string reaDespa;
            reaDespa = txt_FechaRealDespa.Text;
            DateTime validaFecha;

            Calcular_TiempoEnvioDtos();
            Calcular_Tt_PlantaCliente();

            if (!String.IsNullOrEmpty(txtFechaEnvioDtos.Text) && (!String.IsNullOrEmpty(txt_FechaRealDespa.Text)))
            {
                txt_TiempoEnvioDtos.ReadOnly = false;
            }
            else
            {
                txt_TiempoEnvioDtos.ReadOnly = true;
            }
            if (String.IsNullOrEmpty(txt_FechaRealDespa.Text))
            {
                txt_EfectividadEnvioDtos.Text = "";
            }
            else
            {               
                if (DateTime.TryParse(reaDespa, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Real Despacho");
                    txt_FechaRealDespa.Focus();
                    txt_FechaRealDespa.Text = "";
                }
            }
        }

        protected void txt_FechaRealZarpe_TextChanged(object sender, EventArgs e)
        {           
            string reaZarpe;
            reaZarpe = txt_FechaRealZarpe.Text;
            DateTime validaFecha;

            Calcular_TtInternacional();

            if (String.IsNullOrEmpty(txt_FechaRealZarpe.Text))
            {              
            }
            else
            {
                if (DateTime.TryParse(reaZarpe, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Real Zarpe");
                    txt_FechaRealZarpe.Focus();
                    txt_FechaRealZarpe.Text = "";
                }
            }
        }
    
        protected void txt_FechaConfirmaArribo_TextChanged(object sender, EventArgs e)
        {            
            string reaArribo;
            reaArribo = txt_FechaConfirmaArribo.Text;
            DateTime validaFecha;

            Calcular_TtInternacional();
            Calcular_Tt_PuertoDestinoPlanta();

            if (String.IsNullOrEmpty(txt_FechaConfirmaArribo.Text))
            {
            }
            else
            {
                if (DateTime.TryParse(reaArribo, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Confirmada Arribo");
                    txt_FechaConfirmaArribo.Focus();
                    txt_FechaConfirmaArribo.Text = "";
                }
            }
        }

        protected void txt_FechaRealLlegaObra_TextChanged(object sender, EventArgs e)
        {           
            string reaObra;
            reaObra = txt_FechaRealLlegaObra.Text;
            DateTime validaFecha;

            Calcular_Tt_PuertoDestinoPlanta();
            Obtener_LeadTime();
            Calcular_Tt_PlantaCliente();

            if (String.IsNullOrEmpty(txt_FechaRealLlegaObra.Text))
            {
                txt_EfectiviEntrega.Text = "";
                txt_LeadTimeEspera.Text = "";
            }
            else
            {
                if (DateTime.TryParse(reaObra, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Real Llegada Obra");
                    txt_FechaRealLlegaObra.Focus();
                    txt_FechaRealLlegaObra.Text = "";
                }
            }
        }

        protected void txt_FechaEstimaZarpe_TextChanged(object sender, EventArgs e)
        {
            string estZarpe;
            estZarpe = txt_FechaEstimaZarpe.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_FechaEstimaZarpe.Text))
            {
                if (DateTime.TryParse(estZarpe, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Estimada Zarpe");
                    txt_FechaEstimaZarpe.Focus();
                    txt_FechaEstimaZarpe.Text = "";
                }
            }
        }

        protected void txt_EstimadaArribo_TextChanged(object sender, EventArgs e)
        {
            string estArribo;
            estArribo = txt_EstimadaArribo.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_EstimadaArribo.Text))
            {
                if (DateTime.TryParse(estArribo, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Estimada Arribo");
                    txt_EstimadaArribo.Focus();
                    txt_EstimadaArribo.Text = "";
                }
            }
        }

        protected void txt_FechaEstLlegaObra_TextChanged(object sender, EventArgs e)
        {
            string estObra;
            estObra = txt_FechaEstLlegaObra.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_FechaEstLlegaObra.Text))
            {
                if (DateTime.TryParse(estObra, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo Estimada Llegada Obra");
                    txt_FechaEstLlegaObra.Focus();
                    txt_FechaEstLlegaObra.Text = "";
                }
            }
        }
        protected void txt_TtPlantaCliente_TextChanged(object sender, EventArgs e)
        {
            Calcular_Tt_PlantaCliente();
        }

        protected void txt_TotalProvi_TextChanged(object sender, EventArgs e)
        {
            Calcular_DiffProvisiones();
        }
     
        protected void txt_ValorFob_TextChanged(object sender, EventArgs e)
        {   
            //Calcular_Gastos_Fletes_Facturados();
            Calcular_DiffProvisiones();
            Calcular_Ahorro();
            decimal valorFob = 0;

            if (!String.IsNullOrEmpty(txt_ValorFob.Text))
            {
                if (txt_ValorFob.Text != "0")
                {
                    valorFob = decimal.Parse(txt_ValorFob.Text);

                    txt_ValorFob.Text = Convert.ToString(valorFob.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorFob.Text = "0";
                }
            }
        }
        protected void txt_ValorExw_TextChanged(object sender, EventArgs e)
        {        
           // Calcular_Gastos_Fletes_Facturados();
            Calcular_DiffProvisiones();
            Calcular_Ahorro();
            decimal valorEwx = 0;

            if (!String.IsNullOrEmpty(txt_ValorExw.Text))
            {
                if (txt_ValorExw.Text != "0")
                {
                    valorEwx = decimal.Parse(txt_ValorExw.Text);

                    txt_ValorExw.Text = Convert.ToString(valorEwx.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorExw.Text = "0";
                }
            }

        }

        protected void txt_ValorTolFactura_TextChanged(object sender, EventArgs e)
        {
            decimal valorTotal = 0;

            if (!String.IsNullOrEmpty(txt_ValorTolFactura.Text))
            {
                if (txt_ValorTolFactura.Text != "0")
                {
                    valorTotal = decimal.Parse(txt_ValorTolFactura.Text);

                    txt_ValorTolFactura.Text = Convert.ToString(valorTotal.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorTolFactura.Text = "0";
                }
            }
        }
        #endregion

        protected void btn_Guardarobserva_Click(object sender, EventArgs e)
        {
            string usuconect = Convert.ToString(Session["Usuario"]);
            string fechaCrea = DateTime.Now.ToString("dd/MM/yyyy");

            if (String.IsNullOrEmpty(txt_Observacion.Text))
            {
                mensajeVentana("Debe digitar la observacion");
                txt_Observacion.Focus();
            }
            else
            {
                ctrlseguidespachos.Guardar_Observacion_Despa(int.Parse(lbl_Desc_Id.Text), txt_Observacion.Text, usuconect, fechaCrea,txt_OrdenNal.Text,Convert.ToInt32(cbo_EstatusCargaDet.SelectedValue));
                txt_Observacion.Text = "";
                Poblar_observaciones_Despa(int.Parse(lbl_Desc_Id.Text));
            }
        }

        protected void btnSubirArchivos_Click(object sender, EventArgs e)
        {
           // string usuario = (string)Session["Nombre_Usuario"];
            string usuario = Convert.ToString(Session["Usuario"]);
            string directorio = @"I:\Doc_Despachos\" + lbl_Desc_Id.Text + @"\";
            string dirweb = @"~/Doc_Despachos/" + lbl_Desc_Id.Text + @"/";
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

                FDocument.Enabled = true;
                HttpPostedFile postedFile = FDocument.PostedFile;
                string fileName = System.IO.Path.GetFileName(FDocument.FileName);

                if (fileName != "")
                {
                    int tamaño = FDocument.PostedFile.ContentLength;

                    if (FDocument.HasFile)
                    {
                        if (FDocument.PostedFile.ContentLength > 50485760)
                        {
                            //Archivo.Text = "Tamaño Maximo del Archivo 10 MB.";
                            mensaje = "El Archivo Pesa: " + tamaño / 1000000 + " Megas, Supera los 50 Megas Permitidos!!";

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
                                ctrlseguidespachos.Subir_Doc_Despacho(fileName, int.Parse(lbl_Desc_Id.Text), dirweb, usuario, Convert.ToInt32(cboTipoAnexo.SelectedItem.Value));

                                Consultar_Doc_Despacho(int.Parse(lbl_Desc_Id.Text));
                                PoblarTipoAnexo();
                                mensaje = "Se subio exitosamente el archivo: " + fileName;
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }
                            else
                            {
                                mensaje = "No fue posible subir el archivo: " + nombreArchivoTemp + " Verifique!! ";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }                  
                }
                }
                else
                {
                    mensaje = "Seleccione el archivo a subir!!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }

          
        }


        public void Consultar_Doc_Despacho(int desc_id)
        {
            int rol = Convert.ToInt32(Session["Rol"]);
            //Resetea el ds por si hay algo cargado de otra consulta
            dsDocDespa.Reset();                                             
            dsDocDespa = ctrlseguidespachos.Consultar_Doc_Despacho(desc_id, rol);
            lbl_rutaArchivo.Text = "";

            if (dsDocDespa.Rows.Count != 0)
            {
                lbl_rutaArchivo.Text = dsDocDespa.Rows[0][3].ToString();

                //Obtiene la ruta seleccionada en la grilla
                string Ruta = lbl_rutaArchivo.Text;
                //Remplaza caracteres en la ruta obtenida
                Ruta = Ruta.Replace("~/", "I:/");
                //Establece la ruta final, en el label
                lbl_rutaArchivo.Text = Ruta;
                //Agrega  columnas a la tabla 
                DataTable dt = new DataTable();
                dt.Columns.Add("Nombre_Archivo");
                   
                if (Directory.Exists(Ruta))
                {
                    //Obtiene todos los archivos que esten dentro de la ruta espesificada
                    foreach (string strfile in Directory.GetFiles(Ruta))
                    {
                        FileInfo fi = new FileInfo(strfile);
                        dt.Rows.Add(fi.Name);
                    }
                    // Establece los archivos encontrados en la grilla
                    grid_Document_Despacho.DataSource =  ctrlseguidespachos.Consultar_Doc_Despacho(int.Parse(lbl_Desc_Id.Text), rol);
                    grid_Document_Despacho.DataMember = ctrlseguidespachos.Consultar_Doc_Despacho(int.Parse(lbl_Desc_Id.Text),rol).ToString();
                    grid_Document_Despacho.DataBind();
                    grid_Document_Despacho.Visible = true;                                       
                    Grid_DocAnexo.DataSource = ctrlseguidespachos.Consultar_Doc_Despacho(int.Parse(lbl_Desc_Id.Text),rol);
                    Grid_DocAnexo.DataMember = ctrlseguidespachos.Consultar_Doc_Despacho(int.Parse(lbl_Desc_Id.Text),rol).ToString();                    
                    Grid_DocAnexo.DataBind();
                    Grid_DocAnexo.Visible = true;                  
                }
                else
                {
                    grid_Document_Despacho.Dispose();
                    grid_Document_Despacho.Visible = false;
                    Grid_DocAnexo.Dispose();
                    Grid_DocAnexo.Visible = false;
                }
            }
            else
            {
                grid_Document_Despacho.Dispose();
                grid_Document_Despacho.Visible = false;
                Grid_DocAnexo.Dispose();
                Grid_DocAnexo.Visible = false;
            }
            dsDocDespa.Reset();
        }

        protected void grid_Document_Despacho_RowCommand(object sender, GridViewCommandEventArgs e)
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
                Response.WriteFile((lbl_rutaArchivo.Text)
                + e.CommandArgument);
                //Termina la descarga
                Response.End();
            }
       }
        private void CargarPlanta()
        {
            cbo_Planta.Items.Clear();
            reader = ctrlseguidespachos.PoblarPlanta(Session["Usuario"].ToString());
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cbo_Planta.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                cbo_Planta.SelectedIndex = 0;
            }
            reader.Close();
            reader.Dispose();
            ctrlseguidespachos.CerrarConexion();
        }

        protected void chk_SinDespacho_CheckedChanged(object sender, EventArgs e)
        {
            cbo_FiltMes.Items.Clear();
            cbo_FiltMes.Items.Add(new ListItem("Seleccione", "0"));            ctrlseguidespachos.Listar_Mes(cbo_FiltMes);
            // Listar años
            cbo_FiltAño.Items.Clear();
            cbo_FiltAño.Items.Add(new ListItem("Seleccione", "0"));       
            cbo_FiltAño.Items.Add(new ListItem("2018", "1"));
            cbo_FiltAño.Items.Add(new ListItem("2019", "2"));                   
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
                 
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


        public void Limpiar_Campos_DetDespachoNacional()
        {
           
            txt_OrdenNal.Text = "";            
            txt_facturaNal.Text =  "";
            txt_ClienteNal.Text =  "";
            txt_CiudadDestinoNal.Text = "";
            txt_EmpresaTrasnpNal.Text = "";
            txt_FechaEntregaNal.Text = "";         
            txt_NoGuiaNal.Text = "";
            txt_RespTranspNal.Text = ""; 
            txt_ValorExwNal.Text = "";
            txt_FleteCotiNal.Text = "";         
            txt_FleteRealNal.Text = "";
            txt_RelFleTValorNal.Text = "";
            txt_PesoNal.Text = "";        
            txt_StanByNal.Text = "";
            txt_Indicador.Text = "";
            chk_CumpleEntregaNal.Checked = false;
            txt_fechaDespacho.Text = "";
            //lbl_cli_id.Text = "";
            //lbl_ciu_id.Text ="";
        }

        public void Poblar_DetalleDespaNacional()
        {
            int  index = Convert.ToInt32(Session["index"]);
            DataTable dt,dtalu,dtacc;
            decimal pesoacc = 0, pesoalu = 0, PesoBruto=0;

            dt = ctrlseguidespachos.Cargar_DtsOrden_DespaNal(int.Parse(lbl_Desc_Id.Text));
            txt_OrdenNal.Text = dt.Rows[index][1].ToString();
            lbl_ciu_id.Text = dt.Rows[index][10].ToString();
            lbl_cli_id.Text = dt.Rows[index][11].ToString();
            txt_fechaDespacho.Text= dt.Rows[index][15].ToString();
            DateTime fechaDespacho = Convert.ToDateTime(txt_fechaDespacho.Text.ToString());
            txt_fechaDespacho.Text = fechaDespacho.ToShortDateString();
           Session["idOfa"] = dt.Rows[index][12].ToString();
            int idOfa = int.Parse(dt.Rows[index][12].ToString());
            string TipoCliente= dt.Rows[index][13].ToString();

            if (TipoCliente == "Filial 2")
            {
                txt_ValorExwNal.ReadOnly = false;
                txt_FleteCotiNal.ReadOnly = false;
            }
            else
            {
                txt_ValorExwNal.ReadOnly = true;
                txt_FleteCotiNal.ReadOnly = true;
            }

            dtacc = ctrlseguidespachos.Recuperar_PesoPallet_Acce(idOfa);
           dtalu =ctrlseguidespachos.Recuperar_PesoPallet_Alum(idOfa);
           pesoacc = decimal.Parse(dtacc.Rows[0][0].ToString());
           pesoalu = decimal.Parse(dtalu.Rows[0][0].ToString());
           PesoBruto = pesoacc + pesoalu;

            int rol = Convert.ToInt32(Session["Rol"]);

            if (rol != 53)
            {
                Acc_DetalleDespaNacional.Enabled = true;
            }
            else
            {
                Acc_DetalleDespaNacional.Enabled = false;
            }
                

            reader = ctrlseguidespachos.Poblar_DetalleDespaNacional(int.Parse(lbl_Desc_Id.Text), txt_OrdenNal.Text);
            

            Limpiar_Campos_DetDespachoNacional();

            if (reader.HasRows == true)
            {
                reader.Read();
                txt_ClienteNal.Text = dt.Rows[index][0].ToString();
                txt_CiudadDestinoNal.Text = dt.Rows[index][7].ToString();
                txt_OrdenNal.Text = reader.GetValue(0).ToString();
                txt_facturaNal.Text = reader.GetValue(1).ToString();
                txt_EmpresaTrasnpNal.Text = reader.GetValue(4).ToString();
                txt_FechaEntregaNal.Text = reader.GetSqlDateTime(5).Value.ToString("dd/MM/yyyy");
                if (txt_FechaEntregaNal.Text == "01/01/1900") txt_FechaEntregaNal.Text = "";
                txt_fechaDespacho.Text = reader.GetSqlDateTime(18).Value.ToString("dd/MM/yyyy");
                if (txt_fechaDespacho.Text == "01/01/1900") txt_fechaDespacho.Text = "";
                if (!String.IsNullOrEmpty(txt_fechaDespacho.Text) || txt_fechaDespacho.Text == "01/01/1900") { txt_FechaEntregaNal.Enabled = true; }
                else { txt_FechaEntregaNal.Enabled = false; }

                if (String.IsNullOrEmpty(txt_FechaEntregaNal.Text))
                {
                    txt_Indicador.Enabled = false;
                }
                else
                {
                    txt_Indicador.Enabled = true;
                    txt_Indicador.ReadOnly = false;
                }
                txt_NoGuiaNal.Text = reader.GetValue(6).ToString();
                txt_RespTranspNal.Text = reader.GetValue(7).ToString();
                decimal ValorExwNal = reader.GetDecimal(8);
                if (ValorExwNal != 0)
                {
                    txt_ValorExwNal.Text = Convert.ToString(ValorExwNal.ToString("#,##.##"));
                }
                else
                {
                    txt_ValorExwNal.Text = "0";
                }             
                decimal FleteCotiNal = reader.GetDecimal(9);
                if (FleteCotiNal != 0)
                {
                    txt_FleteCotiNal.Text = Convert.ToString(FleteCotiNal.ToString("#,##.##"));
                }
                else
                {
                    txt_FleteCotiNal.Text = "0";
                }                           
                decimal FleteRealNal = reader.GetDecimal(10);

                if (FleteRealNal == -1)
                {
                    txt_FleteRealNal.Text = "";
                }
                else  if (FleteRealNal == 0)
                {
                    txt_FleteRealNal.Text = "0";                    
                }
                else
                {
                    txt_FleteRealNal.Text = Convert.ToString(FleteRealNal.ToString("#,##.##"));
                }

                decimal StanByNal = reader.GetDecimal(13);
                if (StanByNal == -1)
                {
                    txt_StanByNal.Text = "";                    
                }
                else if (StanByNal == 0)
                {
                    txt_StanByNal.Text = "0";
                }
                else
                {
                    txt_StanByNal.Text = Convert.ToString(StanByNal.ToString("#,##.##"));
                }
                txt_RelFleTValorNal.Text = reader.GetValue(11).ToString();
                if (txt_RelFleTValorNal.Text == "-1")
                {
                    txt_RelFleTValorNal.Text = "";
                }                
                decimal peso  = reader.GetDecimal(12);
                if (peso != 0)
                {
                 txt_PesoNal.Text = Convert.ToString(peso.ToString("#,##.##"));
                }
                else
                {
                txt_PesoNal.Text = "0";
                }

                       
                txt_Indicador.Text = reader.GetValue(14).ToString();
                string cumpleEntrega = reader.GetValue(15).ToString();
                if (cumpleEntrega == "True")
                {
                    chk_CumpleEntregaNal.Checked = true;
                }
                else
                {
                    chk_CumpleEntregaNal.Checked = false;
                }
                if (String.IsNullOrEmpty(txt_Indicador.Text))
                {
                    txt_Indicador.Text = "-1";
                }
                else if (txt_Indicador.Text == "-1")
                {
                    txt_Indicador.Text = "";
                }
                
                SetPane("Acc_DetalleDespaNacional");
                btn_GuardarDetalleDespaNal.Text = "Actualizar";
            }
            else
            {
                decimal ValorExwNal = decimal.Parse(dt.Rows[index][4].ToString());
                txt_ValorExwNal.Text = Convert.ToString(ValorExwNal.ToString("#,##.##"));

                //string FleteCotiNal1= dt.Rows[index][8].ToString();
                //if ( !String.IsNullOrEmpty(FleteCotiNal1))
                //{
                //}

                decimal FleteCotiNal = decimal.Parse(dt.Rows[index][8].ToString());
                txt_FleteCotiNal.Text = Convert.ToString(FleteCotiNal.ToString("#,##.##"));
                if (String.IsNullOrEmpty(txt_FleteCotiNal.Text))
                {
                    txt_FleteCotiNal.Text = "0";
                }
                if (String.IsNullOrEmpty(txt_ValorExwNal.Text))
                {
                    txt_ValorExwNal.Text = "0";
                }                   

                txt_ClienteNal.Text = dt.Rows[index][0].ToString();
                txt_OrdenNal.Text = dt.Rows[index][1].ToString();
                txt_CiudadDestinoNal.Text = dt.Rows[index][7].ToString();
                txt_PesoNal.Text = PesoBruto.ToString("#,##.##");
                txt_facturaNal.Text = dt.Rows[index][9].ToString();
                txt_NoGuiaNal.Text = dt.Rows[index][14].ToString();         
                txt_fechaDespacho.Text = dt.Rows[index][15].ToString();
                if (txt_fechaDespacho.Text == "1/01/1900 12:00:00 a.m.") { txt_fechaDespacho.Text=""; }
                else {
                    txt_fechaDespacho.Text = dt.Rows[index][15].ToString();
                    fechaDespacho = Convert.ToDateTime(txt_fechaDespacho.Text.ToString());
                    txt_fechaDespacho.Text = fechaDespacho.ToShortDateString();
                }
                if (!String.IsNullOrEmpty(txt_fechaDespacho.Text)) { txt_FechaEntregaNal.Enabled = true; }
                else { txt_FechaEntregaNal.Enabled = false; }
                SetPane("Acc_DetalleDespaNacional");
                btn_GuardarDetalleDespaNal.Text = "Guardar";
                //declare variabe de seccion  para que guarde el valor de la celda 
                // de la fila seleccionada y utilizarla en otro metodo
            }

            reader.Close();
            ctrlseguidespachos.CerrarConexion();
        }

        protected void grid_DtsOrden_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Select")
            {
                lbl_Acc_Observacion.Text = "Observaciones/Orden";
                // int index = Convert.ToInt32(e.CommandArgument);
                Session["index"] = Convert.ToInt32(e.CommandArgument);

                Poblar_DetalleDespaNacional();
                Poblar_observaciones_Despa(int.Parse(lbl_Desc_Id.Text));

            }              
         }

        protected void txt_FechaEntregaNal_TextChanged(object sender, EventArgs e)
           {
            string fechaEntrega;
            DateTime resultado3;         
            fechaEntrega = txt_FechaEntregaNal.Text;
         
            if (!String.IsNullOrEmpty(txt_FechaEntregaNal.Text) && !String.IsNullOrEmpty(txt_fechaDespacho.Text))
            {                                              
                txt_Indicador.Enabled = true;
                txt_Indicador.ReadOnly = false;

                if (DateTime.TryParse(fechaEntrega, out resultado3))
                {                                
                    DateTime FechaEntrega = DateTime.Parse(txt_FechaEntregaNal.Text);
                    DateTime FechaDespacho = DateTime.Parse(txt_fechaDespacho.Text);

                    if(FechaEntrega>= FechaDespacho)
                    {
                                          
                    TimeSpan ts = FechaEntrega - FechaDespacho;

                    int differenceInDays = ts.Days;

                    txt_Indicador.Text = differenceInDays.ToString();

                   reader=ctrlseguidespachos.Consultar_LeadTime_Nal(int.Parse(lbl_ciu_id.Text));

                    reader.Read();
                    if (reader.HasRows)
                    {
                        int leadtime = reader.GetInt32(0);
                        if (differenceInDays <= leadtime)
                        {
                            chk_CumpleEntregaNal.Checked = true;
                        }
                        else
                        {
                            chk_CumpleEntregaNal.Checked = false;
                        }
                    }
                    else
                    {
                        chk_CumpleEntregaNal.Checked = false;
                    }
                    reader.Close();
                    ctrlseguidespachos.CerrarConexion();
                    }
                    else
                    {
                        mensajeVentana("La fecha de entrega debe ser mayor a la fecha de despacho");
                        txt_FechaEntregaNal.Focus();
                        txt_FechaEntregaNal.Text = "";
                        txt_Indicador.ReadOnly = true;
                        txt_Indicador.Text = "";
                    }
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, en el campo Fecha Entrega");
                    txt_FechaEntregaNal.Focus();
                    txt_FechaEntregaNal.Text = "";
                    txt_Indicador.ReadOnly = true;
                    txt_Indicador.Text = "";
                }            
            }
            else
            {
                txt_Indicador.Enabled = false;
                txt_Indicador.ReadOnly = true;
                txt_Indicador.Text = "";
            }        
        }

        protected void txt_FleteRealNal_TextChanged(object sender, EventArgs e)
        {
            decimal fletereal = 0, valorExw = 0,
                total = 0;

            if ((!String.IsNullOrEmpty(txt_ValorExwNal.Text) && !String.IsNullOrEmpty(txt_FleteRealNal.Text)))
            {               
                    txt_Indicador.ReadOnly = false;
                    if (txt_FleteRealNal.Text != "0" && txt_ValorExwNal.Text != "0")
                    {
                        valorExw = decimal.Parse(txt_ValorExwNal.Text);
                        fletereal = decimal.Parse(txt_FleteRealNal.Text);
                        total = fletereal / valorExw;

                        txt_RelFleTValorNal.Text = Convert.ToString(total.ToString("P"));
                    }
                    else
                    {
                    txt_RelFleTValorNal.Text = "0.00%";
                }         
            }
            else
            {
                txt_RelFleTValorNal.Text = " ";
                txt_Indicador.ReadOnly = false;
            }
        }

        protected void txt_Concatenar_TextChanged(object sender, EventArgs e)
        {
            Obtener_LeadTime();
            Calcular_Tt_PlantaCliente();
        }

        public void grid_Observaciones_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            // Se deja vacio para que pueda funcionar el anular del  (e.CommandName == "Delete")
            //validar y buscar solucion
        }

        protected void grid_Observaciones_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                // int index = Convert.ToInt32(e.CommandArgument);
             
               int filaSelect= Convert.ToInt32(e.CommandArgument);
                int DesObs_Id = Convert.ToInt32(grid_Observaciones.DataKeys[filaSelect].Value);
                ctrlseguidespachos.Anular_Observacion_Despacho(DesObs_Id);
                Poblar_observaciones_Despa(int.Parse(lbl_Desc_Id.Text));
                grid_Observaciones.EditIndex = -1;
                mensajeVentana("Observación anulada correctamente");
            }
        }

        protected void txt_Indicador_TextChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(txt_Indicador.Text))
            {
            reader = ctrlseguidespachos.Consultar_LeadTime_Nal(int.Parse(lbl_ciu_id.Text));
            int indicadorDias = int.Parse(txt_Indicador.Text);

                    reader.Read();
                    if (reader.HasRows)
                    {
                        int leadtime = reader.GetInt32(0);
                        if (indicadorDias <= leadtime)
                        {
                            chk_CumpleEntregaNal.Checked = true;
                        }
                        else
                        {
                            chk_CumpleEntregaNal.Checked = false;
                        }
                    }
                    else
                    {
                        chk_CumpleEntregaNal.Checked = false;
                    }
                reader.Close();
                reader.Dispose();
                ctrlseguidespachos.CerrarConexion();
            }
            else
            {
                txt_FechaEntregaNal.Text = "";
                txt_Indicador.Enabled = false;

            }
        }

        private void PoblarTipoAnexo()
        {               
            cboTipoAnexo.Items.Clear();
            cboTipoAnexo.Items.Add(new ListItem("Seleccione", "0"));

            reader = ctrlseguidespachos.PoblarTipoAnexo();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoAnexo.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }                        
            reader.Close();
            reader.Dispose();
            BdDatos.desconectar();
        }

        protected void txt_Finalizacion_TextChanged(object sender, EventArgs e)
        {
            string finalizacion;
            finalizacion = txt_Finalizacion.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Finalizacion.Text))
            {
                if (DateTime.TryParse(finalizacion, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F.Finalización");
                    txt_Finalizacion.Focus();
                    txt_Finalizacion.Text = "";
                }
            }
        }
       
        protected void txt_Inspeccion_TextChanged(object sender, EventArgs e)
        {
            string inspeccion;
            inspeccion = txt_Inspeccion.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Inspeccion.Text))
            {
                if (DateTime.TryParse(inspeccion, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Inspeccion");
                    txt_Inspeccion.Focus();
                    txt_Inspeccion.Text = "";
                }
            }
        }

        protected void txt_Nacionalizacion_TextChanged(object sender, EventArgs e)
        {
            string nacionalizacion;
            nacionalizacion = txt_Nacionalizacion.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Nacionalizacion.Text))
            {
                if (DateTime.TryParse(nacionalizacion, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F de CI Nacionalizacion");
                    txt_Nacionalizacion.Focus();
                    txt_Nacionalizacion.Text = "";
                }
            }
        }

        protected void txt_FacturacionProveed_TextChanged(object sender, EventArgs e)
        {
            string facturacionProvee;
            facturacionProvee = txt_FacturacionProveed.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_FacturacionProveed.Text))
            {
                if (DateTime.TryParse(facturacionProvee, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F facturacion proveedor a forsa");
                    txt_FacturacionProveed.Focus();
                    txt_FacturacionProveed.Text = "";
                }
            }
        }

        protected void txt_CI_impuestos_TextChanged(object sender, EventArgs e)
        {
            string ciImpuestos;
            ciImpuestos = txt_CI_impuestos.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_CI_impuestos.Text))
            {
                if (DateTime.TryParse(ciImpuestos, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Pago de CI Impuestos");
                    txt_CI_impuestos.Focus();
                    txt_CI_impuestos.Text = "";
                }
            }
        }

        protected void txt_Retiro_Conten_TextChanged(object sender, EventArgs e)
        {
            string retiroConten;
            retiroConten = txt_Retiro_Conten.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Retiro_Conten.Text))
            {
                if (DateTime.TryParse(retiroConten, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Retiro Contenedores");
                    txt_Retiro_Conten.Focus();
                    txt_Retiro_Conten.Text = "";
                }
            }
        }

        protected void txt_Desove_TextChanged(object sender, EventArgs e)
        {
            string desove;
            desove = txt_Desove.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Desove.Text))
            {
                if (DateTime.TryParse(desove, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Vaciado Contenedores (Desove)");
                    txt_Desove.Focus();
                    txt_Desove.Text = "";
                }
            }
        }

        protected void txt_Almacenamiento_TextChanged(object sender, EventArgs e)
        {
            string almacenamiento;
            almacenamiento = txt_Almacenamiento.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Almacenamiento.Text))
            {
                if (DateTime.TryParse(almacenamiento, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F de almacenamiento (Cargo)");
                    txt_Almacenamiento.Focus();
                    txt_Almacenamiento.Text = "";
                }
            }
        }

        protected void txt_Notifica_Cliente_TextChanged(object sender, EventArgs e)
        {
            string notificaCliente;
            notificaCliente = txt_Notifica_Cliente.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Notifica_Cliente.Text))
            {
                if (DateTime.TryParse(notificaCliente, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Notificación a Cliente");
                    txt_Notifica_Cliente.Focus();
                    txt_Notifica_Cliente.Text = "";
                }
            }
        }

        protected void txt_Devolucion_Conten_TextChanged(object sender, EventArgs e)
        {
            string devolucionConten;
            devolucionConten = txt_Devolucion_Conten.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Devolucion_Conten.Text))
            {
                if (DateTime.TryParse(devolucionConten, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Devolucion contenedores");
                    txt_Devolucion_Conten.Focus();
                    txt_Devolucion_Conten.Text = "";
                }
            }
        }

        protected void txt_Facturacion_ForsaCliente_TextChanged(object sender, EventArgs e)
        {
             string facturacionForsaCliente;
            facturacionForsaCliente = txt_Facturacion_ForsaCliente.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Facturacion_ForsaCliente.Text))
            {
                if (DateTime.TryParse(facturacionForsaCliente, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Facturacion Forsa a Cliente");
                    txt_Facturacion_ForsaCliente.Focus();
                    txt_Facturacion_ForsaCliente.Text = "";
                }
            }
        }

        protected void txt_Fecha_Carga_Cliente_TextChanged(object sender, EventArgs e)
        {
            string fechaCargaCliente;
            fechaCargaCliente = txt_Fecha_Carga_Cliente.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Fecha_Carga_Cliente.Text))
            {
                if (DateTime.TryParse(fechaCargaCliente, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F de carga cliente");
                    txt_Fecha_Carga_Cliente.Focus();
                    txt_Fecha_Carga_Cliente.Text = "";
                }
            }
        }

        protected void txt_Entrega_Obra_TextChanged(object sender, EventArgs e)
        {
            string entregaObra;
            entregaObra = txt_Entrega_Obra.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_Entrega_Obra.Text))
            {
                if (DateTime.TryParse(entregaObra, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario, para el campo F Entrega en obra");
                    txt_Entrega_Obra.Focus();
                    txt_Entrega_Obra.Text = "";
                }
            }
        }

      

        protected void btn_GuardarGastDespDestBrasil_Click(object sender, EventArgs e)
        {
            string usuconect = Convert.ToString(Session["Usuario"]);
            string fechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
            bool cerrado;
            int respuesta;

            if (!String.IsNullOrEmpty(txt_noProceso.Text) && !String.IsNullOrEmpty(txt_fechPlanilla.Text))
            {
                                            
            if (chk_Cerrado_DespaDestBrasil.Checked == true)
            {
                cerrado = true;
            }
            else
            {
                cerrado = false;
            }

            Validar_Campos_Gastos_DestinoBrasil();

            if (btn_GuardarGastDespDestBrasil.Text == "Guardar")
            {               
                  respuesta = ctrlseguidespachos.Guardar_Gastos_Destino_Brasil(Convert.ToInt32(lbl_IdDespacho.Text), txt_noProceso.Text, txt_fechPlanilla.Text, Convert.ToDecimal(txt_trm.Text),
                                                                             Convert.ToDecimal(txt_liberaHblProv.Text), Convert.ToDecimal(txt_liberaHblReal.Text), Convert.ToDecimal(txt_droppOffProvi.Text),
                                                                             Convert.ToDecimal(txt_droppOffReal.Text), Convert.ToDecimal(txt_taxaAdminProv.Text), Convert.ToDecimal(txt_taxaAdminReal.Text),
                                                                             Convert.ToDecimal(txt_ispsProv.Text), Convert.ToDecimal(txt_ispsReal.Text), Convert.ToDecimal(txt_otrosGastosProv.Text),
                                                                             Convert.ToDecimal(txt_otrosGastosReal.Text), Convert.ToDecimal(txt_1erPeriodoProv.Text), Convert.ToDecimal(txt_1erPeriodoReal.Text),
                                                                             Convert.ToDecimal(txt_2doPeriodoProv.Text), Convert.ToDecimal(txt_2doPeriodoReal.Text), Convert.ToDecimal(txt_3erPeriodoProv.Text),
                                                                             Convert.ToDecimal(txt_3erPeriodoReal.Text), Convert.ToDecimal(txt_escanContenProv.Text), Convert.ToDecimal(txt_escanContenReal.Text),
                                                                             Convert.ToDecimal(txt_insoMapaProv.Text), Convert.ToDecimal(txt_insoMapaReal.Text), Convert.ToDecimal(txt_corretagenProv.Text),
                                                                             Convert.ToDecimal(txt_corretagenReal.Text), Convert.ToDecimal(txt_demurrageProv.Text), Convert.ToDecimal(txt_demurrageReal.Text),
                                                                             Convert.ToDecimal(txt_despaHonoraProv.Text), Convert.ToDecimal(txt_despaHonoraReal.Text), Convert.ToDecimal(txt_margenTimboProv.Text),
                                                                             Convert.ToDecimal(txt_margenTimboReal.Text), Convert.ToDecimal(txt_fleteTerresProv.Text), Convert.ToDecimal(txt_fleteTerresReal.Text),
                                                                             cerrado, usuconect, fechaCrea);
                if (respuesta == 1)
                {
                    mensajeVentana("Registro guardado correctamente");
                    if (chk_Cerrado_DespaDestBrasil.Checked == true)
                    {
                        cerrado = true;
                    }
                    else
                    {
                        cerrado = false;
                    }
                    btn_GuardarGastDespDestBrasil.Text = "Actualizar";
                }
                else
                {
                    mensajeVentana("El registro no se puedo guardar correctamente,verifique los datos");
                }
            }
            else if (btn_GuardarGast.Text == "Actualizar")
            {
                respuesta = ctrlseguidespachos.Actualizar_Gastos_Destino_Brasil(txt_noProceso.Text,txt_fechPlanilla.Text,Convert.ToDecimal(txt_trm.Text), Convert.ToDecimal(txt_liberaHblProv.Text),
                                                                                Convert.ToDecimal(txt_liberaHblReal.Text), Convert.ToDecimal(txt_droppOffProvi.Text), Convert.ToDecimal(txt_droppOffReal.Text),
                                                                                Convert.ToDecimal(txt_taxaAdminProv.Text), Convert.ToDecimal(txt_taxaAdminReal.Text), Convert.ToDecimal(txt_ispsProv.Text),
                                                                                Convert.ToDecimal(txt_ispsReal.Text), Convert.ToDecimal(txt_otrosGastosProv.Text), Convert.ToDecimal(txt_otrosGastosReal.Text),
                                                                                Convert.ToDecimal(txt_1erPeriodoProv.Text), Convert.ToDecimal(txt_1erPeriodoReal.Text), Convert.ToDecimal(txt_2doPeriodoProv.Text),
                                                                                Convert.ToDecimal(txt_2doPeriodoReal.Text), Convert.ToDecimal(txt_3erPeriodoProv.Text), Convert.ToDecimal(txt_3erPeriodoReal.Text),
                                                                                Convert.ToDecimal(txt_escanContenProv.Text), Convert.ToDecimal(txt_escanContenReal.Text), Convert.ToDecimal(txt_insoMapaProv.Text),
                                                                                Convert.ToDecimal(txt_insoMapaReal.Text), Convert.ToDecimal(txt_corretagenProv.Text), Convert.ToDecimal(txt_corretagenReal.Text),
                                                                                Convert.ToDecimal(txt_demurrageProv.Text), Convert.ToDecimal(txt_demurrageReal.Text), Convert.ToDecimal(txt_despaHonoraProv.Text),
                                                                                Convert.ToDecimal(txt_despaHonoraReal.Text), Convert.ToDecimal(txt_margenTimboProv.Text), Convert.ToDecimal(txt_margenTimboReal.Text),
                                                                                Convert.ToDecimal(txt_fleteTerresProv.Text), Convert.ToDecimal(txt_fleteTerresReal.Text), cerrado, (int.Parse(lbl_Desc_Id.Text)));

                if (respuesta == 1)
                {
                    mensajeVentana("Registro  actualizado correctamente");
                    btn_GuardarGastDespDestBrasil.Text = "Actualizar";
                    if (chk_Cerrado_DespaDestBrasil.Checked == true)
                    {
                        Inhabilitar_CamposGastos_Destino_Brasil(true);
                    }                 
                }
                else
                {
                    mensajeVentana("El registro no se puedo actualizar correctamente, verifique los datos");
                }
            }
            }
            else
            {
                mensajeVentana("Debe diligenciar los campos obligatorios");
            }


        }

        public void Poblar_Gastos_Destino_Brasil(int desc_id)
        {
            DataTable dt;
            String cerrado;
            int rol = Convert.ToInt32(Session["Rol"]);


            reader= ctrlseguidespachos.Poblar_Gastos_Destino_Brasil(desc_id);
            reader.Read();

            if (reader.HasRows == true)
            {
                string numProceso = reader.GetString(0);
                txt_noProceso.Text = numProceso;              
                txt_fechPlanilla.Text = reader.GetSqlDateTime(1).Value.ToString("dd/MM/yyyy");
                if (txt_fechPlanilla.Text == "01/01/1900") txt_fechPlanilla.Text = "";
                decimal trm = reader.GetDecimal(2);
                txt_trm.Text = Convert.ToString(trm.ToString("#,##.##"));
                decimal liberaHblProv = reader.GetDecimal(3);
                txt_liberaHblProv.Text = Convert.ToString(liberaHblProv.ToString("#,##.##"));
                decimal liberaHblReal = reader.GetDecimal(4);
                txt_liberaHblReal.Text = Convert.ToString(liberaHblReal.ToString("#,##.##"));
                decimal droppOffProv = reader.GetDecimal(5);
                txt_droppOffProvi.Text = Convert.ToString(droppOffProv.ToString("#,##.##"));
                decimal droppOffReal = reader.GetDecimal(6);
                txt_droppOffReal.Text = Convert.ToString(droppOffReal.ToString("#,##.##"));
                decimal taxaAdminProv = reader.GetDecimal(7);
                txt_taxaAdminProv.Text = Convert.ToString(taxaAdminProv.ToString("#,##.##"));
                decimal taxaAdminReal = reader.GetDecimal(8);
                txt_taxaAdminReal.Text = Convert.ToString(taxaAdminReal.ToString("#,##.##"));
                decimal ispsProv = reader.GetDecimal(9);
                txt_ispsProv.Text = Convert.ToString(ispsProv.ToString("#,##.##"));
                decimal ispsReal = reader.GetDecimal(10);
                txt_ispsReal.Text = Convert.ToString(ispsReal.ToString("#,##.##"));
                decimal otrosGastosProv = reader.GetDecimal(11);
                txt_otrosGastosProv.Text = Convert.ToString(otrosGastosProv.ToString("#,##.##"));
                decimal otrosGastosReal = reader.GetDecimal(12);
                txt_otrosGastosReal.Text = Convert.ToString(otrosGastosReal.ToString("#,##.##"));
                decimal primPeriodoProv = reader.GetDecimal(13);
                txt_1erPeriodoProv.Text = Convert.ToString(primPeriodoProv.ToString("#,##.##"));
                decimal primPeriodoReal = reader.GetDecimal(14);
                txt_1erPeriodoReal.Text = Convert.ToString(primPeriodoReal.ToString("#,##.##"));               
                decimal segunPeriodpProv = reader.GetDecimal(15);
                txt_2doPeriodoProv.Text = Convert.ToString(segunPeriodpProv.ToString("#,##.##"));
                decimal segunPeriodpReal = reader.GetDecimal(16);
                txt_2doPeriodoReal.Text = Convert.ToString(segunPeriodpReal.ToString("#,##.##"));
                decimal tercPeriodoProv = reader.GetDecimal(17);
                txt_3erPeriodoProv.Text = Convert.ToString(tercPeriodoProv.ToString("#,##.##"));
                decimal tercPeriodoReal = reader.GetDecimal(18);
                txt_3erPeriodoReal.Text = Convert.ToString(tercPeriodoReal.ToString("#,##.##"));                                                
                decimal escaContProv = reader.GetDecimal(19);
                txt_escanContenProv.Text = Convert.ToString(escaContProv.ToString("#,##.##"));
                decimal escaContReal = reader.GetDecimal(20);
                txt_escanContenReal.Text = Convert.ToString(escaContReal.ToString("#,##.##"));
                decimal insopeProv = reader.GetDecimal(21);
                txt_insoMapaProv.Text = Convert.ToString(insopeProv.ToString("#,##.##"));
                decimal insopeReal = reader.GetDecimal(22);
                txt_insoMapaReal.Text = Convert.ToString(insopeReal.ToString("#,##.##"));
                decimal corretagenProv = reader.GetDecimal(23);
                txt_corretagenProv.Text = Convert.ToString(corretagenProv.ToString("#,##.##"));
                decimal corretagenReal = reader.GetDecimal(24);
                txt_corretagenReal.Text = Convert.ToString(corretagenReal.ToString("#,##.##"));
                decimal demurrageProv = reader.GetDecimal(25);
                txt_demurrageProv.Text = Convert.ToString(demurrageProv.ToString("#,##.##"));
                decimal demurrageReal = reader.GetDecimal(26);
                txt_demurrageReal.Text = Convert.ToString(demurrageReal.ToString("#,##.##"));
                decimal despaHonoProv = reader.GetDecimal(27);
                txt_despaHonoraProv.Text = Convert.ToString(despaHonoProv.ToString("#,##.##"));
                decimal despaHonoReal = reader.GetDecimal(28);
                txt_despaHonoraReal.Text = Convert.ToString(despaHonoReal.ToString("#,##.##"));
                decimal margenTimboProv = reader.GetDecimal(29);
                txt_margenTimboProv.Text = Convert.ToString(margenTimboProv.ToString("#,##.##"));
                decimal margenTimboReal = reader.GetDecimal(30);
                txt_margenTimboReal.Text = Convert.ToString(margenTimboReal.ToString("#,##.##"));
                decimal fleteTerreProv = reader.GetDecimal(31);
                txt_fleteTerresProv.Text = Convert.ToString(fleteTerreProv.ToString("#,##.##"));
                decimal fleteTerreReal = reader.GetDecimal(32);
                txt_fleteTerresReal.Text = Convert.ToString(fleteTerreReal.ToString("#,##.##")); 
                             
                    cerrado = reader.GetValue(33).ToString();
                    if (cerrado == "True")
                    {
                        chk_Cerrado_DespaDestBrasil.Checked = true;
                        Inhabilitar_CamposGastos_Destino_Brasil(true);
                    }
                    else
                    {
                        chk_Cerrado_DespaDestBrasil.Checked = false;
                        Inhabilitar_CamposGastos_Destino_Brasil(false);
                    }               
                btn_GuardarGastDespDestBrasil.Text = "Actualizar";
                Validar_Campos_Gastos_DestinoBrasil();
            }
            else
            {
                btn_GuardarGastDespDestBrasil.Text = "Guardar";
                Limpiar_Campos_Gastos_Dest_Brasil();
                Inhabilitar_CamposGastos_Destino_Brasil(false);
            }
            reader.Close();
            ctrlseguidespachos.CerrarConexion();         
        }

        protected void txt_fechPlanilla_TextChanged(object sender, EventArgs e)
        {
            string fechPlanilla;
            fechPlanilla = txt_fechPlanilla.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txt_fechPlanilla.Text))
            {
                if (DateTime.TryParse(fechPlanilla, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario");       
                    txt_fechPlanilla.Focus();                  
                }
            }
        }

    

    }
}