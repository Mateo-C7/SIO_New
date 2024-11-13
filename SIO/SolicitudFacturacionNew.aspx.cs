using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Diagnostics;
using System.Data.OracleClient;
using CapaControl;
using CapaDatos;
using CapaControl.Entity;
using Newtonsoft.Json;
using System.Reflection;
using System.EnterpriseServices.Internal;
using System.Globalization;

namespace SIO
{
    public partial class SolicitudFacturacionGBI : System.Web.UI.Page
    {
        ControlMaestroItemPlanta cmIp = new ControlMaestroItemPlanta();
        OracleDataReader Oreader = null;
        public SqlDataReader reader = null;
        public SqlDataReader readerEstadoCli = null;
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();
        public ControlPedido contpv = new ControlPedido();
        public DataSet ds = new DataSet();
        public FUP fup_clase = new FUP();
        Boolean PuedeConfirmar = false;

        protected void Page_Load(object sender, EventArgs e)
        {
//            ScriptManager scripManager = ScriptManager.GetCurrent(this.Page);
//            scripManager.RegisterPostBackControl(btnprocesarSF);
            if (!IsPostBack)
            {
                if (Session["Rol"] != null && Session["FUP"] != null)
                {
                    string origen = Convert.ToString(Session["Bandera"]);

                    //TRAEMOS LOS DATOS GENERALES

                    lblfup.Text = Convert.ToString(Session["FUP"]);
                    LVer.Text = Convert.ToString(Session["VER"].ToString());

                    lblClienteprincipal.Text = Convert.ToString(Session["CLIENTE"]);
                    //lblObraPrincipal.Text = (string)Session["OBRA"];
                    lblnumeropv.Text = Convert.ToString(Session["NUMERO"]);
                    Session["CondPagoText"] = null;
                    Session["CondPagoValue"] = null;
                    Session["partePv"] = 1;
                    //cargo los maestros
                    //COMBOS PARTES DE SOLICITUD
                    //PoblarDirector();
                    //PoblarGerente();
                    InstrumentoPago();
                    FormaPago();
                    PoblarTDN();
                    PoblarTipoFlete();
                    PoblarFacturarFlete();
                    cargarTipoSf();
                    cargarPuerto();

                    // cargo maestro de planta a facturar
                    cargarPlanta();
                    // consulto las partes de pedidos de venta y cargo el combo de partes de pedido venta
                    cargaPartesPv();
                    //valido si hay una parte creada
                    int cantPartesPv = Convert.ToInt32(Session["CantPartesPv"]);

                    if (cantPartesPv > 0)
                    {
                        // cargo los datos de la primera parte del combo partesPv
                        cboPartePv_SelectedIndexChanged(sender, e);

                        // cargo el combo de las partes de solicitud facturacion
                        PoblarParte();
                        this.cboParte_SelectedIndexChanged(sender, e);

                        //CONSULTA PARTES SF
                        ConsultarPartesSF();
                        CargarGrillaPartes();

                        this.cargarConfirmacion();
                        this.cboTipoFlete_SelectedIndexChanged(sender, e);

                    }
                    else
                    {
                        cboPartePv.Items.Add("1");
                        cboParte.Items.Add("1");
                        LimpiarParte();
                    }


                    ConsultarSumaTotales();
                    this.calcularTotal(1);

                    //COMBOS DATOS VENTA
                    //CondicionPago();
                    //CentroOperacion();
                    //TipoCliente();
                    //Motivo();                

                    //cargarFacturarPlanta();


                    ConsultarPedidoVenta(1);

                    //lblfup.Text = Convert.ToString(Session["FUP"]);
                    //LVer.Text = Convert.ToString(Session["VER"].ToString());
                    //lblnumeropv.Text = Convert.ToString(Session["NUMERO"]);
                    //sqls[2] = new SqlParameter("@pparte ", Convert.ToInt32(cboParte.SelectedItem.Text.Trim()));
                    //sqls[3] = new SqlParameter("@pv_id ", Convert.ToInt32(cboPartePv.SelectedItem.Value));
                    PuedeConfirmar = false;
                    if (origen.ToString() == "2")
                    {
                        PuedeConfirmar = true;
                        MostrarTabla(0);
                    }
                    else {
                        reader = controlsf.ConsultarBotonConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                                        Convert.ToInt32(cboParte.SelectedItem.Text)); //, Convert.ToInt32(cboPartePv.SelectedItem.Value));
                        if (reader.HasRows == true)
                        {
                            if (reader.Read() != false)
                            {
                                if (reader[4].ToString() != "0" || reader[6].ToString() != "0")
                                {
                                    PuedeConfirmar = true;
                                }
                                else
                                {
                                    if (reader[0].ToString() != "0")
                                    {
                                        if (reader[2].ToString() != "0")
                                        {
                                            PuedeConfirmar = true;
                                        }
                                        else
                                        {
                                            PuedeConfirmar = false;
                                        }
                                    }
                                }
                            }
                        }
                        MostrarTabla(1);
                    }

                    //VISUALIZAR BOTONES DE CONFIRMACIÓN
                    int arRol = Convert.ToInt32(Session["Rol"]);
                    string bandera = Convert.ToString(Session["Bandera"]);

                    if ((arRol == 2) || (arRol == 9) || (arRol == 30) || (arRol == 1))
                    {
                        if ((arRol == 2) || (arRol == 9))
                        {
                            txtFechaOfac.Enabled = true;
                            cboEstado.Enabled = true;
                            btnOfac.Visible = true;
                            btnGenerarPartePv.Visible = true;
                        }

                        if (bandera == "1")
                        {
                           btnGuardarsf.Visible = true;
                        }
                        else
                        {     //revisar xx
                            ConsultarValorFlete();
                            btnGuardarsf.Visible = true;

                        }
                        if (PuedeConfirmar) btnconfsf.Visible = true;
                        btnQuitarConfirmacion.Visible = false;
                    }
                    else
                    {
                        if (arRol == 3)
                        {
                            //if () modificar aqui ojo
                            btnGuardarsf.Visible = true;
                        }
                    }

                    int pais = -1;

                        reader = controlsf.ConsultarPaisFup(Convert.ToInt32(lblfup.Text) ); //, Convert.ToInt32(cboPartePv.SelectedItem.Value));
                        if (reader.HasRows == true)
                        {
                            if (reader.Read() != false)
                            {
                                pais = reader.GetInt32(0);
                            }
                        }
                                           

                    if ((arRol == 15 || arRol == 1) && (pais == 6) )
                    {
                        Label16.Visible = true;
                        cboPuerto.Visible = true;
                        btnGuardaPuerto.Visible = true;
                    }

                    if (arRol == 10 || arRol == 11 || arRol == 42 || arRol == 49) btnApruebaFinanc.Visible = true;

                    //PoblarCuota();
                    //CONSULTA CUOTAS
                    //ConsultarCuotas();
                    //CargarGrillaCuotas();
                    //ObtenerSumaCuotas();
                    txtFechaReal.Text = System.DateTime.Today.ToShortDateString();

                    //CONSULTAR REPORTE SF
                    //CargarCartaSF();
                    
                    if (pais == 6)
                    {
                        Label16.Visible = true;
                        cboPuerto.Visible = true;
                    }

                }
                else
                {
                    string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    Response.Redirect("Inicio.aspx");
                }
            }
        }

        private void MostrarTabla(int muestra)
        {
            if (muestra == 0)
            {
                pnlDscto.Visible = true;
                //pnlVenta.Visible = false;
            }
            else
            {
                pnlDscto.Visible = false;
                //pnlVenta.Visible = true;
            }
        }



        //CONSULTO EL VALOR DEL FLETE DESDE EL FUP
        private void ConsultarValorFlete()
        {
            int fup = Convert.ToInt32(lblfup.Text);
            reader = controlsf.consultarFletepv(fup);
            if (reader.HasRows == true)
            {
                if (reader.Read() == true)
                {
                    decimal flete = reader.GetDecimal(1);
                    txtValorflete.Text = reader.GetDecimal(1).ToString("#,##.##");
                    if (flete == 0) txtValorflete.Text = "0";
                    else
                        txtValorflete.Text = flete.ToString("#,##.##");
                }
                else
                {
                    txtValorflete.Text = "0";
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void CargarPaisFactura(int cia)
        {
            cboPaisFactura.Items.Clear();
            cboPaiDesp.Items.Clear();

            cboPaisFactura.Items.Add(new ListItem("Seleccione", "0"));
            cboPaiDesp.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarPaisFactura(cia);

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPaisFactura.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                    cboPaiDesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarDirector()
        {
            string pais = Convert.ToString(Session["Pais"]);
            string ciudad = Convert.ToString(Session["Ciudad"]);

            cboDirector.Items.Clear();

            cboDirector.Items.Add(new ListItem("Seleccione", "0"));


            if (pais == "8")
            {
                reader = controlsf.ConsultarDirectorOficinaColombia(Convert.ToInt32(pais), Convert.ToInt32(ciudad));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboDirector.Items.Add(new ListItem(reader.GetString(1)));
                    }
                }
                //ojo revisar
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }
            else
            {
                reader = controlsf.ConsultarDirectorOficina(Convert.ToInt32(pais));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboDirector.Items.Add(new ListItem(reader.GetString(1)));
                    }
                }

                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }
        }

        private void PoblarTipoFlete()
        {

            cboTipoFlete.Items.Clear();
            cboTipoFlete.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarTipoFlete();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoFlete.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarFacturarFlete()
        {
            cboModoFactFlete.Items.Clear();
            cboModoFactFlete.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarFacturarFlete();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboModoFactFlete.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarGerente()
        {
            string pais = Convert.ToString(Session["Pais"]);

            cboGerente.Items.Clear();

            cboGerente.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarGerenteComercial(Convert.ToInt32(pais));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboGerente.Items.Add(new ListItem(reader.GetString(1)));
                }

            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void InstrumentoPago()
        {
            cboInsPago.Items.Clear();

            cboInsPago.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarInstrumentoPago();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboInsPago.Items.Add(new ListItem(reader.GetString(1)));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void FormaPago()
        {
            cboFormaPago.Items.Clear();

            cboFormaPago.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarFormaPago();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboFormaPago.Items.Add(new ListItem(reader.GetString(1)));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarTDN()
        {
            cboTDN.Items.Clear();

            cboTDN.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarTDN();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTDN.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }
        //ojo revisar
        private void TipoCliente(int cia)
        {
            cboTipoCliente.Items.Clear();
            cboTipoCliente.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarTipoCliente(cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void Motivo(int cia)
        {
            cboMotivo.Items.Clear();

            cboMotivo.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarMotivo(cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboMotivo.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void CentroOperacion(int cia)
        {

            cboCentOpe.Items.Clear();

            cboCentOpe.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarCentroOperacion(cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCentOpe.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void cargarPlanta()
        {

            cboPlantaFact.Items.Clear();
            cboPlantaFact.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarPlanta();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPlantaFact.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void cargarPlantaProduccion(int plantaFact)
        {
            cboPlantaProd.Items.Clear();
            cboPlantaProd.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarPlantaProd(plantaFact);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPlantaProd.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

        }

        private void cargarVendedor(int plantaFact)
        {
            cboVendedor.Items.Clear();
            cboVendedor.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarVendedorDw(plantaFact);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboVendedor.Items.Add(new ListItem(reader.GetString(1), reader.GetString(1).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

        }

        private void cargarFacturarPlanta()
        {
            cboFactParte.Items.Clear();
            cboFactParte.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarFacturarPlanta();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboFactParte.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void cargarTipoSf()
        {
            cboTipoSf.Items.Clear();
            cboTipoSf.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarTipoSf();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboTipoSf.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            cboFactParte.Items.Clear();
            cboFactParte.Items.Add(new ListItem("Seleccione", "0"));

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void cargarPuerto()
        {
            cboPuerto.Items.Clear();
            cboPuerto.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarPuerto();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPuerto.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private int cargarPlantaParte()
        {
            int resultado = 0, cant = 0;

            reader = controlsf.ConsultarCantPlantas(Convert.ToInt32(lblfup.Text));
            if (reader.HasRows == true)
            {
                reader.Read();
                cant = reader.GetInt32(0);
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            if (cant > 0)
            {
                resultado = 1;

                reader = controlsf.ConsultarPlantaParte(Convert.ToInt32(lblfup.Text));
                if (reader.HasRows == true)
                {
                    //cboPlantaFact.Items.Clear();
                    //cboPlantaFact.Items.Add(new ListItem("Seleccione", "0"));
                    cboPlantaProd.Items.Clear();
                    cboPlantaProd.Items.Add(new ListItem("Seleccione", "0"));

                    resultado = 1;
                    while (reader.Read())
                    {
                        //cboPlantaFact.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                        cboPlantaProd.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                else
                {
                    btnGenerarPartePv.Enabled = false;
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }

            return resultado;
        }

        private void cargaPartesPv()
        {
            string parte = "";
            parte = Convert.ToString(Session["partePv"]);
            reader = null;
            cboPartePv.Items.Clear();

            reader = controlsf.ConsultarPartesPv(Convert.ToInt32(lblfup.Text));
            if (reader.HasRows == true)
            {
                int cant = 0;
                while (reader.Read())
                {
                    cboPartePv.Items.Add(new ListItem(reader.GetInt32(1).ToString(), reader.GetInt32(0).ToString()));
                    cant = cant + 1;
                }
                Session["CantPartesPv"] = cant;
            }
            else
            {
                Session["CantPartesPv"] = 0;
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            if (parte != "")
            {
                string idPartePv = "0";
                reader = controlsf.ConsultarPartesPvParte(Convert.ToInt32(lblfup.Text), Convert.ToInt32(parte));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        idPartePv = reader.GetInt32(0).ToString();
                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();

                cboPartePv.SelectedValue = idPartePv;
            }
        }

        private void CondicionPago(int cia)
        {
            cboCondPago.Items.Clear();
            cboCondPago.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarCondicionPago(cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCondPago.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).Trim()));
                    string variable = reader.GetString(1);
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void consultarCompaniaPlanta()
        {
            string ruta = "";
            int paisPlanta = 0;
            decimal ivaPlanta = 0;
            string nombrePlanta = "";

            reader = controlsf.ConsultarCompaniaPlanta(Convert.ToInt32(cboPlantaFact.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    Session["Cia"] = reader.GetInt32(0);
                    //lblTipoParte.Text = lblTipoParte.Text ;
                    ruta = reader.GetString(2);
                    paisPlanta = reader.GetInt32(3);
                    ivaPlanta = reader.GetDecimal(4);
                    nombrePlanta = reader.GetString(1);

                    Session["paisPlanta"] = paisPlanta;
                    Session["ivaPlanta"] = ivaPlanta;
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
            //lblNombrePlanta.Text = nombrePlanta;

        }

        private void ConsultarPedidoVenta(int parte)
        {

            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[2];
            sqls[0] = new SqlParameter("@pFupID ", Convert.ToInt32(lblfup.Text.Trim()));
            sqls[1] = new SqlParameter("@parte ", parte);

            string conexion = BdDatos.conexionScope();


            //if (cboPartePv.SelectedItem.Text.Trim() == "1") lblTipoParte.Text = "Principal"; else lblTipoParte.Text = "";

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_Pedido_Venta_V2", con))
                {
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter ST_Tipo = new SqlParameter("@ptipo_solicitud", SqlDbType.VarChar, 50);
                    SqlParameter ST_Cli_Factura = new SqlParameter("@pcli_fact", SqlDbType.VarChar, 100);
                    SqlParameter ST_Cli_Despacho = new SqlParameter("@pcli_despacho", SqlDbType.VarChar, 100);
                    SqlParameter ST_Cli_Tipo = new SqlParameter("@pcli_tipo", SqlDbType.VarChar, 100);
                    SqlParameter ST_Cond_Pago = new SqlParameter("@pcli_cond_pago", SqlDbType.VarChar, 100);
                    SqlParameter ST_Motivo = new SqlParameter("@pmotivo", SqlDbType.VarChar, 100);
                    SqlParameter ST_CentOpe = new SqlParameter("@pcentro_operacion", SqlDbType.VarChar, 100);
                    SqlParameter ID_Dias = new SqlParameter("@pnum_dias", SqlDbType.Int);
                    SqlParameter ST_PaisCliFact = new SqlParameter("@ppais_cli_fact", SqlDbType.VarChar, 100);
                    SqlParameter ST_PaisCliDesp = new SqlParameter("@ppais_cli_desp", SqlDbType.VarChar, 100);
                    SqlParameter ST_DeptoCliFact = new SqlParameter("@pdpto_cli_fact", SqlDbType.VarChar, 100);
                    SqlParameter ST_DeptoCliDesp = new SqlParameter("@pdpto_cli_desp", SqlDbType.VarChar, 100);
                    SqlParameter ST_CiudadCliFact = new SqlParameter("@pciu_cli_fact", SqlDbType.VarChar, 100);
                    SqlParameter ST_CiudadCliDesp = new SqlParameter("@pciu_cli_desp", SqlDbType.VarChar, 100);
                    SqlParameter ST_FechaEntrega = new SqlParameter("@pfecha_entrega", SqlDbType.Date);
                    SqlParameter ST_Cia1E = new SqlParameter("@pcia_1E", SqlDbType.SmallInt);
                    SqlParameter ID_Planta = new SqlParameter("@pplanta", SqlDbType.Int);
                    SqlParameter ST_Ruta = new SqlParameter("@pruta", SqlDbType.VarChar, 200);
                    SqlParameter ST_PlantaFact = new SqlParameter("@planta_id_fact", SqlDbType.SmallInt);

                    SqlParameter ST_ClienteInternoId = new SqlParameter("@ErpCliente_interno_id", SqlDbType.VarChar, 100);
                    SqlParameter ST_ClienteInterno = new SqlParameter("@ErpCliente_interno", SqlDbType.NVarChar, 250);
                    SqlParameter ST_DirEnvio = new SqlParameter("@pdirDespacho", SqlDbType.NVarChar, 250);
                    SqlParameter ST_DirDocumento = new SqlParameter("@pdirDocumentos", SqlDbType.NVarChar, 250);
                    SqlParameter ST_Vendedor = new SqlParameter("@pVendedor", SqlDbType.VarChar, 100);

                    ST_Tipo.Direction = ParameterDirection.Output;
                    ST_Cli_Factura.Direction = ParameterDirection.Output;
                    ST_Cli_Despacho.Direction = ParameterDirection.Output;
                    ST_Cli_Tipo.Direction = ParameterDirection.Output;
                    ST_Cond_Pago.Direction = ParameterDirection.Output;
                    ST_Motivo.Direction = ParameterDirection.Output;
                    ST_CentOpe.Direction = ParameterDirection.Output;
                    ID_Dias.Direction = ParameterDirection.Output;
                    ST_PaisCliFact.Direction = ParameterDirection.Output;
                    ST_PaisCliDesp.Direction = ParameterDirection.Output;
                    ST_DeptoCliFact.Direction = ParameterDirection.Output;
                    ST_DeptoCliDesp.Direction = ParameterDirection.Output;
                    ST_CiudadCliFact.Direction = ParameterDirection.Output;
                    ST_CiudadCliDesp.Direction = ParameterDirection.Output;
                    ST_FechaEntrega.Direction = ParameterDirection.Output;
                    ST_Cia1E.Direction = ParameterDirection.Output;
                    ID_Planta.Direction = ParameterDirection.Output;
                    ST_Ruta.Direction = ParameterDirection.Output;
                    ST_PlantaFact.Direction = ParameterDirection.Output;
                    ST_ClienteInternoId.Direction = ParameterDirection.Output;
                    ST_ClienteInterno.Direction = ParameterDirection.Output;
                    ST_DirEnvio.Direction = ParameterDirection.Output;
                    ST_DirDocumento.Direction = ParameterDirection.Output;
                    ST_Vendedor.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(ST_Tipo);
                    cmd.Parameters.Add(ST_Cli_Factura);
                    cmd.Parameters.Add(ST_Cli_Despacho);
                    cmd.Parameters.Add(ST_Cli_Tipo);
                    cmd.Parameters.Add(ST_Cond_Pago);
                    cmd.Parameters.Add(ST_Motivo);
                    cmd.Parameters.Add(ST_CentOpe);
                    cmd.Parameters.Add(ID_Dias);
                    cmd.Parameters.Add(ST_PaisCliFact);
                    cmd.Parameters.Add(ST_PaisCliDesp);
                    cmd.Parameters.Add(ST_DeptoCliFact);
                    cmd.Parameters.Add(ST_DeptoCliDesp);
                    cmd.Parameters.Add(ST_CiudadCliFact);
                    cmd.Parameters.Add(ST_CiudadCliDesp);
                    cmd.Parameters.Add(ST_FechaEntrega);
                    cmd.Parameters.Add(ST_Cia1E);
                    cmd.Parameters.Add(ID_Planta);
                    cmd.Parameters.Add(ST_Ruta);
                    cmd.Parameters.Add(ST_PlantaFact);
                    cmd.Parameters.Add(ST_ClienteInternoId);
                    cmd.Parameters.Add(ST_ClienteInterno);
                    cmd.Parameters.Add(ST_DirEnvio);
                    cmd.Parameters.Add(ST_DirDocumento);
                    cmd.Parameters.Add(ST_Vendedor);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        string TipoSolicitud = Convert.ToString(ST_Tipo.Value);
                        string ClienteFactura = Convert.ToString(ST_Cli_Factura.Value);
                        string ClienteDespacho = Convert.ToString(ST_Cli_Despacho.Value);
                        string TipoCliente = Convert.ToString(ST_Cli_Tipo.Value);
                        string CondPago = Convert.ToString(ST_Cond_Pago.Value);
                        string Motivo = Convert.ToString(ST_Motivo.Value);
                        string CentOpe = Convert.ToString(ST_CentOpe.Value);
                        int NumDias = Convert.ToInt32(ID_Dias.Value);
                        string PaisCliFact = Convert.ToString(ST_PaisCliFact.Value);
                        string PaisCliDesp = Convert.ToString(ST_PaisCliDesp.Value);
                        string DeptoCliFact = Convert.ToString(ST_DeptoCliFact.Value);
                        string DeptoCliDesp = Convert.ToString(ST_DeptoCliDesp.Value);
                        string CiudadCliFact = Convert.ToString(ST_CiudadCliFact.Value);
                        string CiudadCliDesp = Convert.ToString(ST_CiudadCliDesp.Value);
                        string FecEnt = Convert.ToString(ST_FechaEntrega.Value);
                        //cargo la planta a facturar nuevo campo en pedido venta
                        int idPlantaFact = Convert.ToInt32(ST_PlantaFact.Value);
                        string ClienteInternoId = Convert.ToString(ST_ClienteInternoId.Value);
                        string ClienteInterno = Convert.ToString(ST_ClienteInterno.Value);

                        string DirEnvio = Convert.ToString(ST_DirEnvio.Value);
                        string DirDocumento = Convert.ToString(ST_DirDocumento.Value);
                        string Vendedor = Convert.ToString(ST_Vendedor.Value);

                        txtDireccionDespVentas.Text = DirEnvio;
                        txtDocumentosEnvioVentas.Text = DirDocumento;

                        if (txtDireccionDesp.Text == "")
                            txtDireccionDesp.Text = DirEnvio;

                        if (txtDocumentosEnvio.Text == "")
                            txtDocumentosEnvio.Text = DirDocumento;
                        else


                            lblClienteInterno.Text = ClienteInterno;
                        lblIdClienteInterno.Text = ClienteInternoId;

                        if (lblIdClienteInterno.Text == "0")
                        {
                            lblClienteInterno.Visible = false;
                            lblinterno.Visible = false;
                        }
                        else
                        {
                            lblClienteInterno.Visible = true;
                            lblinterno.Visible = true;
                        }

                        int idCia1E = Convert.ToInt32(ST_Cia1E.Value);
                        int idPlanta = Convert.ToInt32(ID_Planta.Value);

                        if (idCia1E != 0)
                        {
                            // la compañia se convierte en planta
                            idCia1E = idPlantaFact;
                            //imgPlanta.ImageUrl = Convert.ToString( ST_Ruta.Value);
                            //this.consultarCompaniaPlanta();  

                            // cargo el pais a facturar con base en la planta a facturar nuevo campo
                            CargarPaisFactura(idPlantaFact);

                            //VIENEN CARGADOS TODAS LAS CONDICIONES DE PAGO SIN FILTRO AQUI FILTRO SI ES NUEVO SINO CAPTURO EL VIEJO GUARDADO
                            //if (CondPago == "")
                            //{
                            //    this.CondicionPago(idCia1E);
                            //    //ASIGNAMOS LOS VALORES A LOS CAMPOS                        
                            //    cboCondPago.SelectedValue = CondPago.Trim();
                            //}
                            //else
                            //{
                            //    this.CondicionPago(idCia1E);
                            //    cboCondPago.SelectedValue = CondPago.Trim();
                            //    //Session["CondPagoText"] = cboCondPago.SelectedItem.Text;
                            //    //Session["CondPagoValue"] = cboCondPago.SelectedValue;
                            //    ////this.CondicionPago(idCia1E);
                            //    //cboCondPago.Items.Add(new ListItem(Session["CondPagoText"].ToString(), Session["CondPagoValue"].ToString()));
                            //    //cboCondPago.SelectedValue = Session["CondPagoValue"].ToString();
                            //}

                            //CARGAMOS LOS COMBOS DATOS VENTA 
                            Session["Cia"] = idCia1E;
                            this.CondicionPago(idCia1E);
                            this.CentroOperacion(idCia1E);
                            this.Motivo(idCia1E);
                            this.TipoCliente(idCia1E);
                            this.cargarVendedor(idCia1E);

                            //ASIGNAMOS LOS VALORES A LOS CAMPOS 
                            cboCondPago.SelectedValue = CondPago.Trim();
                            cboCentOpe.SelectedValue = CentOpe.Trim();
                            cboMotivo.SelectedValue = Motivo.Trim();
                            cboTipoCliente.SelectedValue = TipoCliente.Trim();
                            cboPaisFactura.SelectedValue = PaisCliFact.Trim();
                            PoblarDepartamentoFactura(idCia1E);
                            cboDepfact.SelectedValue = DeptoCliFact.Trim();
                            PoblarCiudadFactura(idCia1E);
                            cboCiuFact.SelectedValue = CiudadCliFact.Trim();
                            PoblarClienteFactura(idCia1E);
                            string a = ClienteFactura.Trim();
                            cboClienteFacturar.SelectedValue = a;
                            cboClienteFacturar.SelectedValue = ClienteFactura.Trim();
                            cboPaiDesp.SelectedValue = PaisCliDesp.Trim();
                            PoblarDepartamentoDespacho(idCia1E);
                            cboDepdesp.SelectedValue = DeptoCliDesp.Trim();
                            PoblarCiudadDespacho(idCia1E);
                            cboCiuDesp.SelectedValue = CiudadCliDesp.Trim();
                            PoblarClienteDespacho(idCia1E);
                            cboClienteDespachar.SelectedValue = ClienteDespacho.Trim();
                            //PoblarDireccionDespacho();
                            txtDias.Text = "1";
                            txtDias.ReadOnly = true;
                            txtDias.Enabled = false;
                            txtFechaDes.Text = FecEnt;
                            //txtFechaDes.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtFechaDes.Enabled = false;
                            cargarPlanta();
                            cargarPlantaProduccion(idPlantaFact);
                            cboPlantaFact.SelectedValue = idPlantaFact.ToString().Trim();
                            //cboPlantaFact.Enabled = false;

                            //New - Agregamos una funcion para validar la PlantaProd de la SF frente al Pedido de venta
                            AsignarPlantaProdSF(
                                Convert.ToInt32(lblfup.Text.Trim()) //fup
                                , parte //parte SF siempre es 1
                                , idPlanta //planta de la tabla pedido_venta
                            );
                            //cboPlantaProd.SelectedValue = idPlanta.ToString().Trim();
                            cboVendedor.SelectedValue = Vendedor.Trim();

                            PoblarNit();
                            consultarEstadoClienteDespacho();

                        }
                    }
                    con.Close();
                }
            }
        }

        private void AsignarPlantaProdSF(int fup, int parte,int PlantaProdPV) 
        {
            reader = controlsf.ConsultarPlanProdSF(fup, parte);

            //Si no trae nada asigne la de pedido_venta
            if (!reader.HasRows) 
            {
                cboPlantaProd.SelectedValue = PlantaProdPV.ToString().Trim();
                return;
            }

            
            if (reader.Read()) 
            {
                int plantaProdSF = reader.GetInt32(0);

                //Si son diferentes SF y Pedido_Venta asigne la de SF
                if (plantaProdSF != PlantaProdPV)
                {
                    cboPlantaProd.SelectedValue = plantaProdSF.ToString().Trim();
                }
                else 
                {//Si son iguales asigne la del pedido_venta 

                    cboPlantaProd.SelectedValue = PlantaProdPV.ToString().Trim();
                }
            }
        }

        private void PoblarParte()
        {
            bool existe = false;
            //CONSULTAMOS LA VERSION CON EL FUP
            cboParte.Items.Clear();

            reader = controlsf.PoblarParte(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim(), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                existe = reader.Read();
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            if (existe == false)
            {
                cboParte.Items.Add("1");
                LimpiarParte();
            }
            else
            {
                reader = controlsf.PoblarParte(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim(), Convert.ToInt32(cboPartePv.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboParte.Items.Add(new ListItem(reader.GetInt32(1).ToString(), reader.GetInt32(0).ToString()));
                    }
                }

                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }

        }

        private void ConsultarPartesSF()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[4];
            sqls[0] = new SqlParameter("@pfup_id ", Convert.ToInt32(lblfup.Text.Trim()));
            sqls[1] = new SqlParameter("@pversion ", LVer.Text.Trim());
            sqls[2] = new SqlParameter("@pparte ", Convert.ToInt32(cboParte.SelectedItem.Text.Trim()));
            sqls[3] = new SqlParameter("@pv_id ", Convert.ToInt32(cboPartePv.SelectedItem.Value));

            Session["Fup"] = Convert.ToInt32(lblfup.Text.Trim());
            Session["Version"] = LVer.Text.Trim();
            Session["Parte"] = Convert.ToInt32(cboParte.SelectedItem.Text.Trim());
            Session["PvId"] = Convert.ToInt32(cboPartePv.SelectedItem.Value);

            string conexion = BdDatos.conexionScope();


            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_solicitud_facturacion", con))
                {
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter ST_Director = new SqlParameter("@pdirector_oficina", SqlDbType.VarChar, 40);
                    SqlParameter ST_Gerente = new SqlParameter("@pasesor_comercial", SqlDbType.VarChar, 40);
                    SqlParameter ST_TDN = new SqlParameter("@ptermino_negociacion", SqlDbType.Int);
                    SqlParameter ST_Venta = new SqlParameter("@pvlr_venta", SqlDbType.Money);
                    SqlParameter ST_Transporte = new SqlParameter("@ptransporte", SqlDbType.Money);
                    SqlParameter ST_IVA = new SqlParameter("@piva", SqlDbType.Money);
                    SqlParameter ST_Total = new SqlParameter("@ptltiva", SqlDbType.Money);
                    SqlParameter ST_Comercial = new SqlParameter("@pvlr_comercial", SqlDbType.Money);
                    SqlParameter ST_FormaPago = new SqlParameter("@pforma_pago", SqlDbType.VarChar, 40);
                    SqlParameter ST_Instrumento = new SqlParameter("@pinstrumento", SqlDbType.VarChar, 20);
                    SqlParameter ST_Comentarios = new SqlParameter("@pcomentarios", SqlDbType.VarChar, 12600);
                    SqlParameter ST_ObservaFactura = new SqlParameter("@pobservaFactura", SqlDbType.VarChar, 12600);
                    SqlParameter ST_PorcDesc = new SqlParameter("@pporcdesc", SqlDbType.VarChar, 50);
                    SqlParameter ST_VrDesc = new SqlParameter("@pvlr_dscto", SqlDbType.Money);
                    SqlParameter ST_RazonDesc = new SqlParameter("@prazondesc", SqlDbType.VarChar, 12600);
                    SqlParameter ST_M2 = new SqlParameter("@pm2", SqlDbType.Decimal);
                    SqlParameter ST_DirDesp = new SqlParameter("@pdir_obra_desp", SqlDbType.VarChar, 250);
                    SqlParameter ST_DirEnvio = new SqlParameter("@pDir_documentos", SqlDbType.VarChar, 250);
                    SqlParameter ST_CondPago = new SqlParameter("@pcondicion_pago", SqlDbType.VarChar, 40);
                    SqlParameter ST_TipoFlete = new SqlParameter("@ptipoflete", SqlDbType.VarChar, 40);
                    SqlParameter ST_FactFlete = new SqlParameter("@ptipofactflete", SqlDbType.VarChar, 40);
                    SqlParameter ST_Subtotal = new SqlParameter("@psubtotal", SqlDbType.VarChar, 40);
                    SqlParameter ST_TipoSf = new SqlParameter("@ptipo_sf", SqlDbType.VarChar, 40);
                    SqlParameter ST_Compania = new SqlParameter("@pfac_compania", SqlDbType.VarChar, 40);
                    SqlParameter ST_ValorCotizado = new SqlParameter("@pvalor_cotizado", SqlDbType.Money);
                    SqlParameter ST_DirDespVta = new SqlParameter("@pdir_obra_desp2", SqlDbType.VarChar, 100);
                    SqlParameter ST_DirEnvioVta = new SqlParameter("@pdir_documentos2", SqlDbType.VarChar, 100);
                    SqlParameter ID_Planta = new SqlParameter("@pplantaprod", SqlDbType.Int);



                    ST_Director.Direction = ParameterDirection.Output;
                    ST_Gerente.Direction = ParameterDirection.Output;
                    ST_TDN.Direction = ParameterDirection.Output;
                    ST_Venta.Direction = ParameterDirection.Output;
                    ST_Transporte.Direction = ParameterDirection.Output;
                    ST_IVA.Direction = ParameterDirection.Output;
                    ST_Total.Direction = ParameterDirection.Output;
                    ST_Comercial.Direction = ParameterDirection.Output;
                    ST_FormaPago.Direction = ParameterDirection.Output;
                    ST_Instrumento.Direction = ParameterDirection.Output;
                    ST_Comentarios.Direction = ParameterDirection.Output;
                    ST_ObservaFactura.Direction = ParameterDirection.Output;
                    ST_PorcDesc.Direction = ParameterDirection.Output;
                    ST_VrDesc.Direction = ParameterDirection.Output;
                    ST_RazonDesc.Direction = ParameterDirection.Output;
                    ST_M2.Direction = ParameterDirection.Output;
                    ST_M2.Scale = 2;
                    ST_DirDesp.Direction = ParameterDirection.Output;
                    ST_DirEnvio.Direction = ParameterDirection.Output;
                    ST_CondPago.Direction = ParameterDirection.Output;
                    ST_TipoFlete.Direction = ParameterDirection.Output;
                    ST_FactFlete.Direction = ParameterDirection.Output;
                    ST_Subtotal.Direction = ParameterDirection.Output;
                    ST_TipoSf.Direction = ParameterDirection.Output;
                    ST_Compania.Direction = ParameterDirection.Output;
                    ST_ValorCotizado.Direction = ParameterDirection.Output;
                    ST_DirDespVta.Direction = ParameterDirection.Output;
                    ST_DirEnvioVta.Direction = ParameterDirection.Output;
                    ID_Planta.Direction = ParameterDirection.Output;


                    cmd.Parameters.Add(ST_Director);
                    cmd.Parameters.Add(ST_Gerente);
                    cmd.Parameters.Add(ST_TDN);
                    cmd.Parameters.Add(ST_Venta);
                    cmd.Parameters.Add(ST_Transporte);
                    cmd.Parameters.Add(ST_IVA);
                    cmd.Parameters.Add(ST_Total);
                    cmd.Parameters.Add(ST_Comercial);
                    cmd.Parameters.Add(ST_FormaPago);
                    cmd.Parameters.Add(ST_Instrumento);
                    cmd.Parameters.Add(ST_Comentarios);
                    cmd.Parameters.Add(ST_ObservaFactura);
                    cmd.Parameters.Add(ST_PorcDesc);
                    cmd.Parameters.Add(ST_VrDesc);
                    cmd.Parameters.Add(ST_RazonDesc);
                    cmd.Parameters.Add(ST_M2);
                    cmd.Parameters.Add(ST_DirDesp);
                    cmd.Parameters.Add(ST_DirEnvio);
                    cmd.Parameters.Add(ST_CondPago);
                    cmd.Parameters.Add(ST_TipoFlete);
                    cmd.Parameters.Add(ST_FactFlete);
                    cmd.Parameters.Add(ST_Subtotal);
                    cmd.Parameters.Add(ST_TipoSf);
                    cmd.Parameters.Add(ST_Compania);
                    cmd.Parameters.Add(ST_ValorCotizado);
                    cmd.Parameters.Add(ST_DirDespVta);
                    cmd.Parameters.Add(ST_DirEnvioVta);
                    cmd.Parameters.Add(ID_Planta);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //if (dr.HasRows)
                        //{
                            //VALORES DEL ENCABEZADO 
                            string Director = Convert.ToString(ST_Director.Value);
                            string Gerente = Convert.ToString(ST_Gerente.Value);
                            string TDN = Convert.ToString(ST_TDN.Value);
                            decimal VrVenta = Convert.ToDecimal(ST_Venta.Value);
                            decimal Transporte = Convert.ToDecimal(ST_Transporte.Value);
                            decimal IVA = Convert.ToDecimal(ST_IVA.Value);
                            decimal VrTotal = Convert.ToDecimal(ST_Total.Value);
                            decimal VrComercial = Convert.ToDecimal(ST_Comercial.Value);
                            string FormaPago = Convert.ToString(ST_FormaPago.Value);
                            string Instrumento = Convert.ToString(ST_Instrumento.Value);
                            string Comentarios = Convert.ToString(ST_Comentarios.Value);
                            string ObservaFactura = Convert.ToString(ST_ObservaFactura.Value);
                            string PorcDesc = Convert.ToString(ST_PorcDesc.Value);
                            decimal VrDesc = Convert.ToDecimal(ST_VrDesc.Value);
                            string RazonDesc = Convert.ToString(ST_RazonDesc.Value);
                            decimal M2 = Convert.ToDecimal(ST_M2.Value);
                            string DirDesp = Convert.ToString(ST_DirDesp.Value);
                            string DirEnvio = Convert.ToString(ST_DirEnvio.Value);
                            string CondPago = Convert.ToString(ST_CondPago.Value);
                            string tipoflete = Convert.ToString(ST_TipoFlete.Value);
                            string factFlete = Convert.ToString(ST_FactFlete.Value);
                            decimal subtotal = Convert.ToDecimal(ST_Subtotal.Value);
                            string tipoSf = Convert.ToString(ST_TipoSf.Value);
                            string compania = Convert.ToString(ST_Compania.Value);
                            decimal VlrCotizacion = Convert.ToDecimal(ST_ValorCotizado.Value);
                            string DirDespVta = Convert.ToString(ST_DirDespVta.Value);
                            string DirEnvioVta = Convert.ToString(ST_DirEnvioVta.Value);
                            string IdPlantaParte = Convert.ToString(ID_Planta.Value);

                        cboPlantaProd.SelectedValue = IdPlantaParte.ToString().Trim();


                        if (tipoflete.Trim().Length != 0)
                                cboTipoFlete.SelectedValue = tipoflete.Trim();
                            else
                                cboTipoFlete.SelectedIndex = 0;

                            if (factFlete.Trim().Length != 0)
                                cboModoFactFlete.SelectedValue = factFlete.Trim();
                            else
                                cboModoFactFlete.SelectedIndex = 0;

                            lblSubtotal.Text = Convert.ToString(subtotal.ToString("#,##.##"));
                            if (lblSubtotal.Text == "")
                            {
                                lblSubtotal.Text = "0";
                            }

                            //ASIGNAMOS LOS VALORES A LOS CAMPOS
                            //if (Director.Trim().Length != 0)
                            //    cboDirector.SelectedValue = Director.Trim();
                            //else
                            //    cboDirector.SelectedIndex = 0;

                            //if (Gerente.Trim().Length != 0)
                            //    cboGerente.SelectedValue = Gerente.Trim();
                            //else
                            //    cboGerente.SelectedIndex = 0;

                            if (TDN.Trim().Length != 0)
                                cboTDN.SelectedValue = TDN.Trim();
                            else
                                cboTDN.SelectedIndex = 0;

                            if (FormaPago.Trim().Length != 0)
                                cboFormaPago.SelectedValue = FormaPago.Trim().ToUpper();
                            else
                                cboFormaPago.SelectedIndex = 0;

                            if (Instrumento.Trim().Length != 0)
                                cboInsPago.SelectedValue = Instrumento.Trim();
                            else
                                cboInsPago.SelectedIndex = 0;

                            if (tipoSf.Trim().Length != 0)
                                cboTipoSf.SelectedValue = tipoSf.Trim();
                            else
                                cboTipoSf.SelectedIndex = 0;

                            cargarFacturarPlanta();

                            if (compania.Trim().Length != 0)
                                cboFactParte.SelectedValue = compania.Trim();
                            else
                                cboFactParte.SelectedIndex = 0;

                            txtValorflete.Text = Convert.ToString(Transporte.ToString("#,##.##"));
                            if (txtValorflete.Text == "")
                            {
                                txtValorflete.Text = "0";
                            }


                            lblIVA.Text = Convert.ToString(IVA.ToString("#,##"));
                            lblValorTotalVenta.Text = Convert.ToString(VrTotal.ToString("#,##"));
                            Session["VrVenta"] = 0;
                            txtValorComercial.Text = Convert.ToString(VrComercial.ToString("#,##.##"));
                            if (txtValorComercial.Text == "")
                            {
                                txtValorComercial.Text = "0";
                            }
                            txtVlrComer.Text = txtValorComercial.Text;
                            lblvlr_vnta1.Text = Convert.ToString(VrVenta.ToString("#,##.##")); /*txtValorComercial.Text;*/
                            Session["VrVenta"] = txtValorComercial.Text;
                            txtComentariosSF.Text = Comentarios;
                            txtObservaFactura.Text = ObservaFactura;
                            porcDscto.Text = PorcDesc;
                            lblValorDscto.Text = Convert.ToString(VrDesc.ToString("#,##.##"));

                            txtM2.Text = Convert.ToString(M2.ToString("#,##.##"));
                            if (txtM2.Text == "")
                            {
                                txtM2.Text = "0";
                            }
                            Session["VrTotalM2"] = txtM2.Text;

                            if (DirDesp != "")
                                txtDireccionDesp.Text = DirDesp;
                            else
                                txtDireccionDesp.Text = txtDireccionDespVentas.Text;

                            if (DirEnvio != "")
                                txtDocumentosEnvio.Text = DirEnvio;
                            else
                                txtDocumentosEnvio.Text = txtDocumentosEnvioVentas.Text;
                            //if (CondPago.Trim().Length != 0)
                            //    cboCondPago.SelectedValue = CondPago.Trim();
                            //else
                            //    cboCondPago.SelectedIndex = 0;
                        }
                    con.Close();
                }
            }
        }

        private void CargarGrillaPartes()
        {
            ds.Reset();
            ds = controlsf.ConsultarGrillaParte(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            if (ds != null)
            {
                grvPartes.DataSource = ds.Tables[0];
                grvPartes.DataBind();
                grvPartes.Visible = true;
            }
            else
            {
                grvPartes.Dispose();
                grvPartes.Visible = false;
            }

            controlsf.CerrarConexion();
        }

        private void ConsultarSumaTotales()
        {
            string bandera = Convert.ToString(Session["Bandera"]);
            if (bandera == "1")
            {
                reader = controlsf.ConsultarTotales(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            }
            else
            {
                reader = controlsf.ConsultarTotalesPV(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            }

            if (reader.HasRows == true)
            {
                if (reader.Read() == true)
                {
                    decimal M2Proy = Convert.ToDecimal(reader.GetValue(0).ToString());
                    decimal M2Cierre = Convert.ToDecimal(reader.GetValue(1).ToString());
                    decimal VrProy = Convert.ToDecimal(reader.GetValue(2).ToString());
                    decimal VrCierre = Convert.ToDecimal(reader.GetValue(3).ToString());
                    decimal VrVenta = Convert.ToDecimal(reader.GetValue(4).ToString());
                    decimal M2Modulados = Convert.ToDecimal(reader.GetValue(5).ToString());
                    decimal M2Actual = Convert.ToDecimal(reader.GetValue(6).ToString());

                    LVrCierre.Text = Convert.ToString(VrCierre.ToString("#,##.##"));
                    LVrProy.Text = Convert.ToString(VrProy.ToString("#,##.##"));
                    LM2Cierre.Text = Convert.ToString(M2Cierre.ToString("#,##.##"));
                    LM2Proy.Text = Convert.ToString(M2Proy.ToString("#,##.##"));
                    lblM2Modulados.Text = Convert.ToString(M2Modulados.ToString("#,##.##"));
                    lblM2Actual.Text = Convert.ToString(M2Actual.ToString("#,##.##"));
                    Session["VrTotalVenta"] = Convert.ToString(VrVenta.ToString("#,##.##"));


                    if (LM2Proy.Text == "")
                    {
                        LM2Proy.Text = "0";
                    }
                    if (LVrProy.Text == "")
                    {
                        LVrProy.Text = "0";
                    }
                    if (LM2Cierre.Text == "")
                    {
                        LM2Cierre.Text = "0";
                    }
                    if (LVrCierre.Text == "")
                    {
                        LVrCierre.Text = "0";
                    }
                }
                else
                {
                    LVrCierre.Text = "0";
                    LVrProy.Text = "0";
                    LM2Cierre.Text = "0";
                    LM2Proy.Text = "0";
                }
            }
            //revisar
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            ObtenerSumaCuotas();
        }

        protected void txtDias_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";

            bool result = IsNumeric(txtDias.Text);
            if (txtDias.Text == "" || result == false)
            {
                mensaje = "Digíte el número de días correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtDias.Text = "0";
            }
            else
            {
                //string fecha_acc = DateTime.Now.ToString("dd/MM/yyyy");
                //DateTime fechaFinal = Convert.ToDateTime(fecha_acc);
                //DateTime sumaFecha = fechaFinal.AddDays(Convert.ToDouble(txtDias.Text));
                //txtFechaDes.Text = Convert.ToString(sumaFecha.ToShortDateString());
            }
        }

        protected void txtFechaDes_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";

            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            TimeSpan FechaFin;
            //DateTime FechaIni = Convert.ToDateTime(txtFechaDes.Text);

            bool fechacomp = IsDatet(txtFechaDes.Text);

            if (fechacomp != true)
            {
                mensaje = "Digite la fecha correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtFechaDes.Text = "";
            }
            else
            {
                //FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                //int dias = FechaFin.Days;
                //txtDias.Text = Convert.ToString(dias);
            }
        }

        public static Boolean IsDatet(string fecha)
        {
            DateTime result;
            return DateTime.TryParse(fecha, out result);
        }

        protected void cboParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool exist = false;

            reader = controlsf.ConsultarParte(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                exist = reader.Read();
                lblOrden.Text =  reader.GetSqlString(2).Value;                
                cboPuerto.SelectedValue = reader.GetInt32(3).ToString();
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            if (exist == true)
            {
                ConsultarPartesSF();
                CargarGrillaPartes();
            }
            else
            {
                LimpiarParte();
                btnGenerar.Enabled = false;
            }


        }

        public static Boolean IsNumeric(string precio)
        {
            decimal result;
            return decimal.TryParse(precio, out result);
        }

        protected void txtValorComercial_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtValorComercial.Text);

            if (txtValorComercial.Text == "" || result == false)
            //if (txtValorComercial.Text == "")
            {
                mensaje = "Digite el valor comercial correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtValorComercial.Text = "0";
            }
            else
            {
                this.calcularTotal(1);
            }
        }

        //campo: indica si el desto es desde el valor comercial o el porc de dcto
        protected void calcularTotal(int campo)
        {
            //if (Convert.ToInt16(cboParte.SelectedItem.ToString()) > 1)
            //{
            //    txtValorVenta.Text = txtValorComercial.Text;
            //}
            if (pnlDscto.Visible == true)
            {
                CalcularDescuento(campo);
            }

            CalcularSubtotal();
            ValoresDecimales();

        }

        //CALCULAR DESCUENTO
        private void CalcularDescuento(int campo)
        {
            if (campo == 1)
            {
                if (lblvlr_vnta1.Text != txtValorComercial.Text)
                {
                    decimal Porc, PorVenta;
                    ValoresVacios();
                    if (Convert.ToDecimal(lblvlr_vnta1.Text) != 0)
                    {
                        Porc = ((Convert.ToDecimal(txtVlrComer.Text) * 100) / Convert.ToDecimal(lblvlr_vnta1.Text));
                        PorVenta = 100 - Convert.ToDecimal(Porc);
                        porcDscto.Text = Convert.ToString(PorVenta.ToString("#.####"));
                    }
                    else
                    {
                        Porc = 0;
                        PorVenta = 100;
                        porcDscto.Text = Convert.ToString(PorVenta.ToString("#.####"));
                    }

                    decimal vrFin = Convert.ToDecimal(lblvlr_vnta1.Text) - Convert.ToDecimal(txtVlrComer.Text);
                    lblValorDscto.Text = Convert.ToString(vrFin);
                }
                else
                {
                    porcDscto.Text = "0";
                    lblValorDscto.Text = "0";
                }
            }
            else
            {
                if (campo == 2)
                {
                    decimal valordscto = 0, valorcomercial = 0;

                    valordscto = (Convert.ToDecimal(lblvlr_vnta1.Text) * Convert.ToDecimal(porcDscto.Text.Replace(",", ".")) / 100);
                    lblValorDscto.Text = Convert.ToString(valordscto);

                    valorcomercial = Convert.ToDecimal(lblvlr_vnta1.Text) - valordscto;
                    txtVlrComer.Text = Convert.ToString(valorcomercial);
                }
                else
                {
                    porcDscto.Text = "0";
                    lblValorDscto.Text = "0";
                }
            }
        }

        //CALCULO EL IVA SOBRE EL VALOR COMERCIAL
        private void CalcularSubtotal()
        {
            string pais = Convert.ToString(Session["Pais"]);
            int paisCliente = Convert.ToInt32(pais);
            int paisPlanta = Convert.ToInt32(Session["paisPlanta"]);
            decimal ivaPlanta = Convert.ToDecimal(Session["ivaPlanta"]);

            if (txtValorComercial.Text == "") txtValorComercial.Text = "0";

            decimal vlrComercial = Convert.ToDecimal(txtValorComercial.Text);
            if (txtValorflete.Text == "") txtValorflete.Text = "0";
            decimal vlrFlete = Convert.ToDecimal(txtValorflete.Text);
            decimal subtotal = 0;
            decimal total = 0;

            //subtotal = vlrComercial + vlrFlete;
            subtotal = vlrComercial + vlrFlete;
            lblSubtotal.Text = Convert.ToString(subtotal);
            //Asignamos el IVA en 0, en caso de que se quede quemado algun valor que no deberia ser antes de asignarlo, lo limpia
            lblIVA.Text = "0";

            if (vlrComercial > 0)
            {
                if (paisPlanta == paisCliente)
                {
                    //decimal IVA = controlsf.calculoIVA(Convert.ToDecimal(subtotal));
                    decimal IVA = subtotal * ivaPlanta;
                    lblIVA.Text = Convert.ToString(IVA);
                    total = subtotal + IVA;
                    lblValorTotalVenta.Text = Convert.ToString(total);
                }
            }
            else
            {
                lblValorTotalVenta.Text = Convert.ToString(subtotal);
            }
        }

        //CONSULTO EL VALOR TOTAL SI NO EXISTE SF
        private void ValorTotal()
        {
            string mensaje = "";

            string VrTotalVenta = Convert.ToString(Session["VrTotalVenta"]);
            string VrVenta = Convert.ToString(Session["VrVenta"]);
            ValoresVacios();

            if (VrTotalVenta == "")
            {
                VrTotalVenta = "0";
            }

            decimal SumVrTotal = (Convert.ToDecimal(VrTotalVenta.Replace(",", ""))
                                //+ Convert.ToDecimal(txtValorVenta.Text.Replace(",", ""))
                                - Convert.ToDecimal(VrVenta.Replace(",", "")));



        }

        private void ValoresDecimales()
        {
            ValoresVacios();

            //CONVIERTO LOS VALORES A DOS DECIMALES
            //decimal vrVenta = Convert.ToDecimal(txtValorVenta.Text);
            //txtValorVenta.Text = Convert.ToString(vrVenta.ToString("#,##.##"));

            if (txtValorComercial.Text == "") txtValorComercial.Text = "0";

            decimal vrComercial = Convert.ToDecimal(txtValorComercial.Text);
            txtValorComercial.Text = Convert.ToString(vrComercial.ToString("#,##.##"));
            //decimal vrDcto = Convert.ToDecimal(lblValorDscto.Text);
            //lblValorDscto.Text = Convert.ToString(vrDcto.ToString("#,##.##"));
            decimal vrIVA = Convert.ToDecimal(lblIVA.Text);
            lblIVA.Text = Convert.ToString(vrIVA.ToString("#,##"));
            if (txtValorflete.Text == "") txtValorflete.Text = "0";
            decimal vrFlete = Convert.ToDecimal(txtValorflete.Text);
            txtValorflete.Text = Convert.ToString(vrFlete.ToString("#,##.##"));
            decimal vrTotalVenta = vrComercial + vrIVA + vrFlete;
            lblValorTotalVenta.Text = Convert.ToString(vrTotalVenta.ToString("#,##"));

            ValoresVacios();
        }

        private void ValoresVacios()
        {
            //BLINDAMOS LOS VALORES VACIOS EN CERO
            //if (txtValorVenta.Text == "")
            //{
            //    txtValorVenta.Text = "0";
            //}
            //if (txtValorComercial.Text == "")
            //{
            //    txtValorComercial.Text = "0";
            //}
            //if (lblValorDscto.Text == "")
            //{
            //    lblValorDscto.Text = "0";
            //}
            if (lblIVA.Text == "")
            {
                lblIVA.Text = "0";
            }
            if (txtValorflete.Text == "")
            {
                txtValorflete.Text = "0";
            }
            if (lblValorTotalVenta.Text == "")
            {
                lblValorTotalVenta.Text = "0";
            }
            if (lblvlr_vnta1.Text == "")
            {
                lblvlr_vnta1.Text = "0";
            }
            //if (txtDscto.Text == "")
            //{
            //    txtDscto.Text = "0";
            //}
        }

        //protected void txtDscto_TextChanged(object sender, EventArgs e)
        //{
        //    string mensaje = "";
        //    decimal valordscto = 0;
        //    decimal valorcomercial = 0;

        //    bool result = IsNumeric(txtDscto.Text);

        //    if (txtDscto.Text == "" || result == false)
        //    {
        //        mensaje = "Digite el descuento correctamente.";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //        txtDscto.Text = "0";
        //    }
        //    else
        //    {
        //        this.calcularTotal(2);
        //    }
        //}

        protected void txtValorflete_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtValorflete.Text);

            if (txtValorflete.Text == "" || result == false)
            {
                mensaje = "Digite el valor del flete correctamente.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtValorflete.Text = "0";
            }
            else
            {
                decimal flete = Convert.ToDecimal(txtValorflete.Text);
                if (flete < 0)
                {
                    mensaje = "No es posible digitar un valor de flete negativo";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtValorflete.Text = "0";
                }
                else
                {
                    this.calcularTotal(1);
                }
            }
        }

        protected void btnGuardarsf_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(4000);

            if (cboTipoSf.SelectedItem.Value == "1" && (txtM2.Text == "" || txtM2.Text == "0"))
            {
                MensajeValidaSF();
            }
            else
            {
                if (//cboDirector.SelectedItem.Value == "0" || cboGerente.SelectedItem.Value == "0" || 
                cboInsPago.SelectedItem.Value == "0" ||
                cboFormaPago.SelectedItem.Value == "0" || cboTDN.SelectedItem.Value == "0" 
                //||txtValorVenta.Text == "0" || txtValorVenta.Text == "" 
                || txtValorComercial.Text == "0" ||
                txtValorComercial.Text == "" || txtValorflete.Text == "" || txtDireccionDesp.Text == "" ||
                cboTipoFlete.SelectedItem.Value == "0" || cboModoFactFlete.SelectedItem.Value == "0" ||
                cboFactParte.SelectedItem.Value == "0" || cboTipoSf.SelectedItem.Value == "0" ||
                lblSubtotal.Text == "" ||  cboPlantaProd.Text == "" || cboPlantaProd.SelectedItem.Value == "0")
                {
                    MensajeValidaSF();
                }
                else
                {
                    GrabarSF();
                }
            }

        }

        private void GrabarSF()
        {
            string mensaje = "";

                if (cboTipoFlete.SelectedItem.Value == "1" && txtValorflete.Text == "0")
                {
                    mensaje = "Alerta! Debe ingresar el valor del Flete, verifique el Tipo de Flete!";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
                    string obra = Convert.ToString(Session["OBRA"]);
                    var valordctof = "0";
                    var valordctov = "0";
                    if (pnlDscto.Visible == true)
                    {
                        valordctof = porcDscto.Text;
                        valordctov = lblValorDscto.Text;

                    }
                    else
                    {
                        valordctof = "0";
                        valordctov = "0";
                    }

                int IngSF = controlsf.IngSFParte(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                               Convert.ToInt32(cboParte.SelectedItem.Text), Nombre, lblClienteprincipal.Text,
                               cboClienteFacturar.SelectedItem.Text, obra, 
                               "", //cboDirector.SelectedItem.Text,
                               "", //cboGerente.Text, 
                               Convert.ToInt32(cboTDN.SelectedItem.Value), cboFormaPago.SelectedItem.Text,
                               "0", txtValorflete.Text.Replace(",", ""), lblIVA.Text.Replace(",", ""),
                               lblValorTotalVenta.Text.Replace(",", ""), txtValorComercial.Text.Replace(",", ""),
                               cboCondPago.SelectedItem.Text, cboInsPago.SelectedItem.Text, txtComentariosSF.Text.ToUpperInvariant(),
                               txtDireccionDesp.Text, valordctof, valordctov, "",
                               cboClienteDespachar.SelectedItem.Text, Convert.ToDecimal(txtM2.Text.Replace(",", "")), txtDocumentosEnvio.Text
                               , Convert.ToInt32(cboTipoFlete.SelectedItem.Value), Convert.ToInt32(cboModoFactFlete.SelectedItem.Value),
                               lblSubtotal.Text.Replace(",", ""), Convert.ToInt32(cboPartePv.SelectedItem.Value),
                               Convert.ToInt32(cboTipoSf.SelectedItem.Value), Convert.ToInt32(cboFactParte.SelectedItem.Value)
                               , Convert.ToInt32(cboPlantaProd.SelectedItem.Value), txtObservaFactura.Text
                               );

                    if (IngSF > 0)
                    {
                        cboParte.SelectedItem.Value = IngSF.ToString();
                        string idparte = cboParte.SelectedItem.Value;
                    }

                    mensaje = "Solicitud de facturacion ingresada con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                    btnGenerar.Enabled = true;
                    CargarGrillaPartes();
                    ConsultarSumaTotales();
                    Session["Parte"] = cboParte.SelectedItem.Text;
                    Session["SfId"] = cboParte.SelectedItem.Value.ToString();
                    Session["Evento"] = 9;
                    fup_clase.CorreoFUP(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), Convert.ToInt32(Session["Evento"]));
                    //EnviarCorreoConfirmacionParte(" SOLICITUD FACTURACION: ");
                    consultarArrendadora();
                }
            
        }

        private void MensajeSF()
        {
            string mensaje = "";

            mensaje = "Solicitud de facturacion ingresada con éxito.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeValidaSF()
        {
            string mensaje = "";


            if (cboTipoSf.SelectedItem.Value == "1" && (txtM2.Text == "" || txtM2.Text == "0"))
            {
                mensaje = "Verifique los m2 de partes de solicitud.";
            }
            else
            {
                mensaje = "Seleccione toda la información de partes de solicitud.";
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void btnconfsf_Click(object sender, EventArgs e)
        {
            Session["BanderaConfirma"] = "1";
            ValidarConfirmacion();
        }

        protected void cargarConfirmacion()
        {
            int arRol = Convert.ToInt32(Session["Rol"]);
            bool arrendadora = false, confFinanciero = false;
            string fechaFinanciero = "", usuFinanciero = "";

            int PedidoERP = cargarPedidoERP();
            btnprocesarSF.Enabled = false;
            btnprocesarSF.Visible = false;


            reader = controlsf.ConsultarConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() != false)
                {
                    bool confSF = reader.GetSqlBoolean(0).Value;
                    arrendadora = reader.GetSqlBoolean(1).Value;
                    confFinanciero = reader.GetSqlBoolean(2).Value;
                    fechaFinanciero = reader.GetSqlDateTime(3).Value.ToString();
                    usuFinanciero = reader.GetSqlString(4).Value;

                    if (confSF == true)
                    {

                        btnconfsf.Enabled = false;
                        btnconfsf.Visible = false;

                        lblEstadoSf.Text = "SF CONFIRMADA";
                        if (PedidoERP == 0)
                        {
                            btnprocesarSF.Enabled = true;
                            btnprocesarSF.Visible = true;

                            txtDias.ReadOnly = false;
                            txtDias.Enabled = true;
                            txtFechaDes.Enabled = true;
                        }
                    }
                    else
                    {
                        if ((arRol == 2) || (arRol == 9) || (arRol == 30))
                        {
                            btnconfsf.Enabled = true;
                            if (PuedeConfirmar) btnconfsf.Visible = true;
                        }
                        lblEstadoSf.Text = "SF SIN CONFIRMAR";
                    }

                    //verifico si es arrendadora y la aprobacion del financiero
                    if (arrendadora == true)
                    {
                        if (arRol == 10 || arRol == 11 || arRol == 49) btnApruebaFinanc.Visible = true;

                        if (confFinanciero == true)
                        {
                            if (PuedeConfirmar) btnconfsf.Visible = true;
                            lblEstadoSf.Text = "PEDIDO APROBADO POR FINANCIERO";
                            if (PedidoERP == 0)
                            {
                                btnprocesarSF.Enabled = true;
                                btnprocesarSF.Visible = true;

                                txtDias.ReadOnly = false;
                                txtDias.Enabled = true;
                                txtFechaDes.Enabled = true;
                            }
                        }
                        else
                        {
                            btnconfsf.Visible = false;
                            lblEstadoSf.Text = "PEDIDO SIN APROBAR POR FINANCIERO";
                            lblEstadoSf.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        protected int cargarPedidoERP()
        {
            int PedidoERP = -1;
            decimal m2Mod = 0;
            string TipoSf = Convert.ToString(Session["TIPO"]);

            reader = controlsf.ConsultarPedidoERPSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() != false)
                {
                    PedidoERP = reader.GetInt32(0);
                    m2Mod = reader.GetDecimal(1);

                    if (m2Mod == 0 && TipoSf =="OF") { PedidoERP = -1; }

                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
            return PedidoERP;
        }

        protected void consultarArrendadora()
        {
            bool arrendadora = false, confFinanciero = false, notificado = false;
            string fechaFinanciero = "", usuFinanciero = "";

            reader = controlsf.ConsultarConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() != false)
                {
                    bool confSF = reader.GetSqlBoolean(0).Value;
                    arrendadora = reader.GetSqlBoolean(1).Value;
                    confFinanciero = reader.GetSqlBoolean(2).Value;
                    fechaFinanciero = reader.GetSqlDateTime(3).Value.ToString();
                    usuFinanciero = reader.GetSqlString(4).Value;
                    notificado = reader.GetSqlBoolean(5).Value;

                    //verifico si es arrendadora y la aprobacion del financiero
                    if (arrendadora == true)
                    {
                        if (notificado == false)
                        {
                            //btnconfsf.Visible = true;
                            EnviarCorreoFinanciero("SOLICITUD APROBACION PEDIDO:");
                            int IngSF = controlsf.actualizarNotificacionFinanciero(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
                        }
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }


        private void CorreoAlertaOfac(string idCliente, string nombreCliente, int fup, string fechaVerificado, string estado, string pais, string obra)
        {
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string CorreoUsuario = Convert.ToString(Session["rcEmail"]);
            string Fecha = System.DateTime.Today.ToLongDateString();
            string Email = "", EmailGerente = "", EmailAdmin = "", Sujeto = "", Cuerpo = "";
            string Anho = DateTime.Now.ToString("yy");
            string correoSistema = Convert.ToString(Session["CorreoSistema"]);
            string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);


            //OBTENEMOR EL MAIL DE LOS ADMINISTRADORES
            reader = controlsf.ObtenerMailAdmin();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            //OBTENEMOR EL MAIL DE LOS ASOCIADOS A OFAC
            reader = controlsf.ObtenerMailOfac();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (EmailAdmin == "")
                    {
                        EmailAdmin = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            string EmailFinal = Email;

            Sujeto = "SIO - Alerta Cliente Reportado OFAC: " + nombreCliente + " " + Nombre;

            Cuerpo = "Buen día, se ha bloqueado al cliente : " + nombreCliente + " para la generacion de Solicitud de facturacion y ordenes, ya que se encuentra REPORTADO en lista OFAC. \n\n " +
            " Codigo Cliente:  " + idCliente + " \n\n " +
            " Estado: REPORTADO \n\n " +
            " Proyecto: " + obra + " \n\n " +
            " FUP No:  " + fup + " \n\n " +
            " País:  " + pais + " \n\n " +
            " Ultima Fecha Verificacion OFAC:  " + fechaVerificado + " \n\n " +

            " Cordialmente, " + "\n\n SIO-Forsa \n\n" + CorreoUsuario;

            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(EmailFinal);//ojocambiar CorreoUsuario 
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailAdmin + "," + CorreoUsuario);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = Sujeto;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            mail.Body = Cuerpo;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            //ADJUNTAMOS EL ARCHIVO           
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
            }

        }

        private void CorreoAlertaBloqueo(string idCliente, string nombreCliente, int fup, string fechaVerificado, string estado, string pais, string obra)
        {
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string CorreoUsuario = Convert.ToString(Session["rcEmail"]);
            string Fecha = System.DateTime.Today.ToLongDateString();
            string Email = "", EmailGerente = "", EmailAdmin = "", Sujeto = "", Cuerpo = "";
            string Anho = DateTime.Now.ToString("yy");
            string correoSistema = Convert.ToString(Session["CorreoSistema"]);
            string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);


            //OBTENEMOR EL MAIL DE LOS ADMINISTRADORES
            reader = controlsf.ObtenerMailAdmin();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            //OBTENEMOR EL MAIL DE LOS ASOCIADOS A BLOQUEO CLIENTE
            reader = controlsf.ObtenerMailBloqueCli();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (EmailAdmin == "")
                    {
                        EmailAdmin = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            string EmailFinal = Email;

            Sujeto = "SIO - Datos Venta Cliente Bloqueado ERP: " + nombreCliente + " " + UsuarioAsunto;

            Cuerpo = "Buen día, se ha ingresado datos de venta para el cliente : " + nombreCliente + ", el cual se encuentra bloqueado en el ERP, por favor verificar. \n\n " +
            " Codigo Cliente:  " + idCliente + " \n\n " +
            " Estado: " + estado + " \n\n " +
            " Proyecto: " + obra + " \n\n " +
            " FUP No:  " + fup + " \n\n " +
            " País:  " + pais + " \n\n " +

            " Cordialmente, " + "\n\n SIO-Forsa \n\n" + CorreoUsuario;

            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(EmailFinal);//ojocambiar CorreoUsuario 
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailAdmin + "," + CorreoUsuario);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = Sujeto;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            mail.Body = Cuerpo;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            //ADJUNTAMOS EL ARCHIVO           
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                string mensaje = "ERROR: " + ex.Message;
            }

        }


        private void ValidarConfirmacion()
        {
            string mensaje = "";
            string BanderaConfirma = Convert.ToString(Session["BanderaConfirma"]);
            bool confSF = false;

            reader = controlsf.ConsultarConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                bool existe = reader.Read();
                if (existe == true)
                {
                    confSF = reader.GetSqlBoolean(0).Value;
                }

                if (existe == false)
                {

                    mensaje = "La parte seleccionada no ha sido ingresada.";

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    if (BanderaConfirma == "1")
                    {
                        if (confSF == true)
                        {
                            // lblMensaje.Text = "Estado: SF Parte " + cboParte.SelectedItem.Text + "Confirmada.";

                            mensaje = "La parte seleccionada ya ha sido confirmada.";

                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            btnGuardarsf.Enabled = true;
                        }
                        else
                        {
                            string USUARIO = Convert.ToString(Session["Nombre_Usuario"]);
                            int actualizar = controlsf.actualizarConfirmarSFAgente(USUARIO, Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));

                            string banderaPv = Convert.ToString(Session["Bandera"]);

                            Session["Parte"] = cboParte.SelectedItem.Text;
                            Session["SfId"] = cboParte.SelectedItem.Value.ToString();
                            Session["Evento"] = 15;
                            fup_clase.CorreoFUP(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), Convert.ToInt32(Session["Evento"]));

                            mensaje = "Se ha Confirmado la SF Exitosamente.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                            btnGuardarsf.Enabled = false;
                        }
                        // reader.Close();
                    }
                    else
                    {
                        Session["Marca"] = "DESCONFIRMADA.";

                        //lblMensaje.Text = "Estado: SF Parte " + cboParte.SelectedItem.Text + "Desconfirmada.";

                        mensaje = "La parte seleccionada ha sido desconfirmada con éxito.";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                        EnviarCorreoConfirmacionParte(" Desconfirmación SF: ");
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            // revisar 1
            this.cargarConfirmacion();
        }

        private void EnviarCorreoConfirmacionParte(string asunto)
        {
            string tipo = "FUP";
            string bandera1 = Convert.ToString(Session["Bandera"]);
            if (bandera1 != "1") tipo = "PV";
            string mensaje;
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);
            string Email = "";
            string Pais = Convert.ToString(Session["Pais"]);
            string OF = Convert.ToString(Session["OFParte"]);

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");

            string enlace = @"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + "" +
                "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text + "&sf_id=" + cboParte.SelectedItem.Value.ToString();

            //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + ""+
            //    "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text);

            correo = clienteWeb.DownloadData(enlace);

            MemoryStream ms = new MemoryStream(correo);

            string EmailAdmin = "";

            //OBTENEMOR EL MAIL DE LOS ADMINISTRADORES
            reader = contpv.ObtenerMailAdmin();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (EmailAdmin == "")
                    {
                        EmailAdmin = reader.GetValue(0).ToString();
                    }
                    else
                    {
                        EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            //OBTENGO EL ASESOR COMERCIAL
            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(Pais));

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(2).ToString();
                        Session["acEmail"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(2).ToString();
                        Session["acEmail"] = Email;
                    }
                }
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            string rcEmail = Convert.ToString(Session["rcEmail"]);
            string EmailAse = Convert.ToString(Session["acEmail"]);
            string correoSistema = Convert.ToString(Session["CorreoSistema"]);
            string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);
            string obra = Convert.ToString(Session["OBRA"]);

            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(EmailAse);
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailAdmin);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = "SIO - " + tipo + asunto + UsuarioAsunto + " " + tipo + ": " + OF + " " +
            "FUP No. " + lblfup.Text + " Version. " + LVer.Text + " parte " + cboParte.SelectedItem.Text;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            string bandera = Convert.ToString(Session["Marca"]);
            mail.Body = "Se ha ingresado Solicitud de Facturación parte " + cboParte.SelectedItem.Text + " \n\n " +
            " FUP No: " + lblfup.Text + " Version. " + LVer.Text + " " + tipo + ": " + OF + " \n\n " +
            " Cliente: " + lblClienteprincipal.Text + "\n\n" +
            " Proyecto: " + obra + " " + "\n\n" + "\n\n" +
            "Cordialmente, " + "\n\n" + "Gestión Informática." + "\n\n" + rcEmail;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            mail.Attachments.Add(new Attachment(ms, "SF_" + lblfup.Text + ".pdf"));
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                mensaje = "ERROR: " + ex.Message;
            }

        }

        private void EnviarCorreoFinanciero(string asunto)
        {
            string tipo = "FUP";
            string bandera1 = Convert.ToString(Session["Bandera"]);
            if (bandera1 != "1") tipo = "PV";
            string mensaje;
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);
            string Email = "";
            string Pais = Convert.ToString(Session["Pais"]);
            string OF = Convert.ToString(Session["OFParte"]);
            string cuerpo = "";

            if (asunto == "SOLICITUD APROBACION PEDIDO:") cuerpo = "Se ha Ingresado Solicitud de Facturacion que requiere Aprobacion por el proceso financiero para la generacion del pedido. ";
            if (asunto == "APROBACION PEDIDO:") cuerpo = "Se Aprobó el Pedido por parte del area Financiera, se puede proceder a confirmar la Solicitud de Facturacion. ";
            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");

            //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + "" +
            //    "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text);

            string enlace = @"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + "" +
              "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text + "&sf_id=" + cboParte.SelectedItem.Value.ToString();

            //correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFup/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + ""+
            //    "&version=" + LVer.Text.Trim() + "&parte=" + cboParte.SelectedItem.Text);

            correo = clienteWeb.DownloadData(enlace);


            MemoryStream ms = new MemoryStream(correo);

            string EmailAdmin = "";

            //OBTENEMOR EL MAIL DE LOS ADMINISTRADORES
            reader = contpv.ObtenerMailAdmin();
            if (reader.HasRows == true)
            {
                reader.Read();
                if (EmailAdmin == "")
                {
                    EmailAdmin = reader.GetValue(0).ToString();
                }
                else
                {
                    EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            //OBTENGO EL ASESOR COMERCIAL
            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(Pais));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(2).ToString();
                        Session["acEmail"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(2).ToString();
                        Session["acEmail"] = Email;
                    }
                }


            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            //OBTENGO EL USUARIO QUE DEBE APROBAR EL PEDIDO EN FINANCIERO
            reader = controlsf.ObtenerFinancieroAprobador();
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Email == "")
                    {
                        Email = reader.GetValue(0).ToString();
                        Session["mailFinanciero"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(0).ToString();
                        Session["mailFinanciero"] = Email;
                    }
                }

            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();


            string rcEmail = Convert.ToString(Session["rcEmail"]);
            string EmailAse = Convert.ToString(Session["acEmail"]);
            string correoSistema = Convert.ToString(Session["CorreoSistema"]);
            string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);
            string obra = Convert.ToString(Session["OBRA"]);

            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(correoSistema);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(Email);
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(EmailAdmin);
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            mail.Subject = "SIO - " + tipo + " " + asunto + " " + UsuarioAsunto + " " + tipo + ": " + OF + " " +
            "FUP No. " + lblfup.Text + " Version. " + LVer.Text + " parte " + cboParte.SelectedItem.Text;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            string bandera = Convert.ToString(Session["Marca"]);
            mail.Body = "Buen dia, " + cuerpo + "\n\n" +
            " Cliente: " + lblClienteprincipal.Text + "\n\n" +
            " FUP No: " + lblfup.Text + " Version. " + LVer.Text + " " + tipo + ": " + OF + " \n\n " +
            " Proyecto: " + obra + " " + "\n\n" + "\n\n" +
            "Cordialmente, " + "\n\n" + "Gestión Informática." + "\n\n" + rcEmail;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            mail.Attachments.Add(new Attachment(ms, "SF_" + lblfup.Text + ".pdf"));
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            smtp.Host = "smtp.office365.com";
            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
            smtp.Port = 587;
            smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };

            System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                mensaje = "ERROR: " + ex.Message;
            }
        }


        private void CorreoFinalVenta()
        {
            string mensaje = "";
            string usuario = Convert.ToString(Session["Nombre_Usuario"]);
            string pais = Convert.ToString(Session["Pais"]);
            string fecha = System.DateTime.Today.ToShortDateString();

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
            correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFUP/COM_SolicitudFacturacionSeguimientoNew&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + lblfup.Text.Trim() + "&version=A&parte=1");
            MemoryStream ms = new MemoryStream(correo);

            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(pais));
            if (reader.HasRows != true)
            {
                mensaje = "El pais no tiene asignado asesor comercial.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }
            else
            {
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();

                string Email = "";
                reader = controlsf.ObtenerMailPV("n_Rol", "p_Rol");

                if (reader.HasRows == true)
                {
                    reader.Read();
                    if (Email == "")
                    {
                        Email = reader.GetValue(0).ToString();
                        Session["acEmail"] = Email;
                    }
                    else
                    {
                        Email = Email + "," + reader.GetValue(0).ToString();
                        Session["acEmail"] = Email;
                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();

                string acEmail = "";
                reader = controlsf.ObtenerMailSF("n_Rol", "p_Rol");
                if (reader.HasRows == true)
                {
                    reader.Read();
                    if (acEmail == "")
                    {
                        acEmail = reader.GetValue(0).ToString();
                        Session["Email"] = acEmail;
                    }
                    else
                    {
                        acEmail = acEmail + "," + reader.GetValue(0).ToString();
                        Session["Email"] = acEmail;
                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();


                string ano = DateTime.Now.ToString("yy");
                string rcEmail = Convert.ToString(Session["rcEmail"]);
                string EmailCita = Convert.ToString(Session["acEmail"]);
                string MailSF = Convert.ToString(Session["Email"]);
                string MailFin = EmailCita + "," + MailSF;
                string paisprincipal = Convert.ToString(Session["PaisNombre"]);
                string correoSistema = Convert.ToString(Session["CorreoSistema"]);
                string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);
                string obra = Convert.ToString(Session["OBRA"]);

                //DEFINIMOS LA CLASE DE MAILMESSAGE
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(correoSistema);
                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                //mail.To.Add("ivanvidal@forsa.net.co"); 
                mail.To.Add(MailFin); // ojo cambiar
                //AÑADIMOS COPIA AL REPRESENTANTE
                mail.CC.Add(rcEmail); // ojo cambiar
                //mail.CC.Add("ivanvidal@forsa.net.co");
                //INCLUIMOS EL ASUNTO DEL MENSAJE
                mail.Subject = "SIO - PV Confirmación SF: " + UsuarioAsunto + " PV " + lblnumeropv.Text + " FUP. " + lblfup.Text + " ";
                //AÑADIMOS EL CUERPO DEL MENSAJE
                mail.Body = "Pedido De Venta No. " + lblnumeropv.Text + " Se Ha CONFIRMADO. RECUERDE QUE NO PUEDE SER MODIFICADO." + " \n\n\n" +
                "Pais: " + paisprincipal + "\n\n" +
                "Cliente: " + lblClienteprincipal.Text + "\n\n" +
                "Proyecto: " + obra + " \n\n\n" +
                "Cordialmente, " + "\n\n" + usuario + "\n\n" + rcEmail;
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = false;
                mail.Attachments.Add(new Attachment(ms, "SF PV" + lblnumeropv.Text + ".pdf"));
                //DECLARAMOS LA CLASE SMTPCLIENT
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                smtp.Host = "smtp.office365.com";
                //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
                smtp.Port = 587;
                smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };

                System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    mensaje = "ERROR: " + ex.Message;
                }
            }


        }

        protected void btnQuitarConfirmacion_Click(object sender, EventArgs e)
        {
            Session["BanderaConfirma"] = "0";
            ValidarConfirmacion();
        }

        protected void btnGenerarSF_Click(object sender, EventArgs e)
        {
            //rpSF.Visible = true;
            // CargarCartaSF();

            //SetPane("AcorSF");
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

        private void CargarCartaSF()
        {
            string Rep = Convert.ToString(Session["Nombre_Usuario"]);
            string CorreoRep = Convert.ToString(Session["rcEmail"]);
            string fecha = System.DateTime.Today.ToLongDateString();
            int sf_id = Convert.ToInt32(cboParte.SelectedItem.Value);

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", lblfup.Text, true));
            rpSF.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            rpSF.ServerReport.ReportServerCredentials = irsc;
            rpSF.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            this.rpSF.KeepSessionAlive = true;
            this.rpSF.AsyncRendering = true;

            string bandera = Convert.ToString(Session["Bandera"]);
            if (bandera == "1")
            {
                parametro.Add(new ReportParameter("version", LVer.Text.Trim(), true));
                parametro.Add(new ReportParameter("parte", cboParte.SelectedItem.Text.Trim(), true));
                parametro.Add(new ReportParameter("sf_id", sf_id.ToString(), true));

                rpSF.ServerReport.ReportPath = "/InformesFUP/COM_SolicitudFacturacionSeguimientoNew";

            }
            else
            {
                parametro.Add(new ReportParameter("version", "A", true));
                parametro.Add(new ReportParameter("parte", cboParte.SelectedItem.Text.Trim(), true));
                parametro.Add(new ReportParameter("sf_id", sf_id.ToString(), true));

                rpSF.ServerReport.ReportPath = "/InformesFUP/COM_SolicitudFacturacionSeguimientoNew";

            }
            rpSF.ServerReport.SetParameters(parametro);
        }

        protected void txtaPagar_TextChanged(object sender, EventArgs e)
        {
            bool result = IsNumeric(txtaPagar.Text);

            string mensaje = "";

            if (txtaPagar.Text == "" || result == false)
            {

                mensaje = "Digite el valor de la cuota correctamente.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                txtaPagar.Text = "0";
            }
            else
            {
                ValorCuota(1);
            }
        }

        protected void txtporcpagar_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";

            bool result = IsNumeric(txtporcpagar.Text);

            if (txtporcpagar.Text == "" || result == false)
            {

                mensaje = "Digite el valor del porcentaje correctamente.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtaPagar.Text = "0";
            }
            else
            {
                ValorCuota(2);
            }
        }

        private void ValorCuota(int tipo)
        {
            string vrAnterior = Convert.ToString(Session["APagar"]);
            string mensaje = "";
            decimal total_pagar = 0;
            decimal pr_pag = 0;
            decimal valor_venta = 0;
            decimal saldo = 0;

            if (vrAnterior.Length == 0)
                vrAnterior = "0";

            if (LVrProy.Text.Trim().Length != 0)
                valor_venta = Convert.ToDecimal(LVrProy.Text);

            if (lblsaldocuota.Text.Trim().Length != 0)
                saldo = Convert.ToDecimal(lblsaldocuota.Text);

            if (tipo == 1)
            {
                total_pagar = Convert.ToDecimal(txtaPagar.Text);
                pr_pag = (total_pagar / valor_venta) * 100;
            }
            else
            {
                pr_pag = Convert.ToDecimal(txtporcpagar.Text);
                total_pagar = valor_venta * pr_pag / 100;
            }

            if ((total_pagar - Convert.ToDecimal(vrAnterior)) > saldo)
            {

                mensaje = "La cantidad a pagar no puede ser mayor al valor de la venta.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtaPagar.Text = "0";

            }
            else
            {
                txtporcpagar.Text = Convert.ToString(pr_pag.ToString("#,##.##"));
                txtaPagar.Text = Convert.ToString(total_pagar.ToString("#,##.##"));
            }
        }

        protected void btnGuardarCuota_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string Moneda = Convert.ToString(Session["MONEDA"]);

            if (txtporcpagar.Text == "0" || txtaPagar.Text == "0" || txtporcpagar.Text == "" ||
                txtaPagar.Text == "" || txtFechaReal.Text == "")
            {

                mensaje = "Seleccione toda la información de cuota.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

            }
            else
            {
                int IngCuota = controlsf.IngCuota(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), Nombre,
                    Convert.ToInt32(cboCuota.SelectedItem.Text), txtaPagar.Text.Replace(",", ""),
                    txtpagado.Text.Replace(",", ""), txtFechaReal.Text, txtComentCuota.Text.ToUpperInvariant(),
                    lblsaldocuota.Text, txtporcpagar.Text, Moneda);

                //MensajeCuota();
                //CargarGrillaCuotas();
                ////PoblarCuota();
                //ObtenerSumaCuotas();

                btnGenerarCuota.Enabled = true;
            }
        }

        private void MensajeCuota()
        {
            string mensaje = "";


            mensaje = "Cuota ingresada con éxito.";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void btnEliminarCuota_Click(object sender, EventArgs e)
        {
            string mensaje;

            if (cboCuota.Text != "0" || cboCuota.Text != "")
            {
                int eliminar = controlsf.EliminarCuota(Convert.ToInt32(lblfup.Text), Convert.ToInt32(cboCuota.SelectedItem.Value),
                    LVer.Text.Trim());
                mensaje = "Se ha eliminado la cuota.";


                //CargarGrillaCuotas();
                ////PoblarCuota();
                //ObtenerSumaCuotas();
            }
            else
            {

                mensaje = "No se puede eliminar la cuota.";


            }
        }

        protected void cboPaisFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            //PoblarCiudadFactura(PlantaId);
            //PoblarClienteFactura(0);
            PoblarDepartamentoFactura(PlantaId);
            LDirFactura.Text = "";
        }

        protected void cboDepfact_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            PoblarCiudadFactura(PlantaId);
            //PoblarClienteFactura(0);
            LDirFactura.Text = "";
        }

        protected void cboCiuFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            int cia = Convert.ToInt32(Session["Cia"]);
            PoblarClienteFactura(cia);
            LDirFactura.Text = "";
        }

        protected void cboClienteFacturar_SelectedIndexChanged(object sender, EventArgs e)
        {
            PoblarNit();
            //PoblarDireccionFactura();
        }

        protected void cboPaiDesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            //PoblarCiudadDespacho();
            //PoblarClienteDespacho(0); 
            PoblarDepartamentoDespacho(PlantaId);
            LDirDespacho.Text = "";
        }

        protected void cboDepdesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            PoblarCiudadDespacho(PlantaId);
            PoblarClienteDespacho(PlantaId);
            LDirDespacho.Text = "";
        }

        protected void cboCiuDesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            PoblarClienteDespacho(PlantaId);
            LDirDespacho.Text = "";
        }

        protected void btnGuardarVenta_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(4000);
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string DescTipo = Convert.ToString(Session["DescTipo"]);
            string mensaje = "";
            int dias = 0;

            if (cboPlantaFact.SelectedItem.Value == "1")
            {
                string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                TimeSpan FechaFin;
                DateTime FechaIni = Convert.ToDateTime(txtFechaOfac.Text);
                FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                dias = FechaFin.Days;
            }

            if (cboCondPago.SelectedItem.Value == "0" || cboCentOpe.SelectedItem.Value == "0" ||
                cboMotivo.SelectedItem.Text == "0" || cboTipoCliente.SelectedItem.Value == "0" ||
                cboPaisFactura.SelectedItem.Value == "0" || cboDepfact.SelectedItem.Value == "0" ||
                cboCiuFact.SelectedItem.Value == "0" || cboClienteFacturar.SelectedItem.Value == "0" ||
                cboPaiDesp.SelectedItem.Value == "0" || cboDepdesp.SelectedItem.Value == "0" ||
                cboCiuDesp.SelectedItem.Value == "0" || cboClienteDespachar.SelectedItem.Value == "0" ||
                txtDias.Text == "0" || txtDias.Text == "" || txtDireccionDespVentas.Text == ""
                || cboVendedor.SelectedItem.Value == "0")
            //                txtDias.Text == "0" || txtFechaDes.Text == "" || txtDias.Text == "")
            {
                mensaje = "Seleccione toda la información de datos de venta.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                dias = dias * -1;

                if ((dias >= 365 || txtFechaOfac.Text == "" || cboEstado.Text == "-") && cboPlantaFact.SelectedItem.Value == "1")
                {
                    if (dias >= 365) mensaje = mensaje + " La fecha de verificacion de Ofac es superior a 1 año ";
                    if (txtFechaOfac.Text == "") mensaje = mensaje + "No se ha verificado el Cliente en OFac ";
                    if (cboEstado.Text == "-") mensaje = mensaje + "No se ha verificado el estado del Cliente en OFac ";

                    mensaje = "Error! " + mensaje;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    string clienteInterno = "";

                    clienteInterno = lblIdClienteInterno.Text;

                    //Se captura del combo de la SF
                    string plantaprod = Convert.ToInt32(cboPlantaProd.SelectedItem.Value) > 0 ? cboPlantaProd.SelectedItem.Value.ToString(): "0";

                    int IngPV = controlsf.IngPV(Convert.ToInt32(lblfup.Text), Nombre, cboClienteFacturar.SelectedItem.Value,
                        cboClienteDespachar.SelectedItem.Value, cboTipoCliente.SelectedItem.Value, txtFechaDes.Text,
                        cboCondPago.SelectedItem.Value, cboMotivo.SelectedItem.Value, cboCentOpe.SelectedItem.Value,
                        Convert.ToInt32(txtDias.Text), lblnumeropv.Text, DescTipo, cboPaisFactura.SelectedItem.Value,
                        cboPaiDesp.SelectedItem.Value, cboDepfact.SelectedItem.Value, cboDepdesp.SelectedItem.Value,
                        cboCiuFact.SelectedItem.Value, cboCiuDesp.SelectedItem.Value, Convert.ToInt32(cboPlantaFact.SelectedItem.Value)
                        , Convert.ToInt32(cboPartePv.SelectedItem.Text.Trim()), clienteInterno, plantaprod//"0"
                        , txtDireccionDespVentas.Text.Trim(), txtDocumentosEnvioVentas.Text.Trim(), cboVendedor.SelectedItem.Value
                        );

                    int IngOfac = controlsf.InsertarListaOfac(cboClienteFacturar.SelectedItem.Value, txtFechaOfac.Text,
                        Nombre, cboEstado.Text, cboClienteFacturar.SelectedItem.Text);

                    mensaje = "Datos de venta ingresados con éxito.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                    Session["partePv"] = cboPartePv.SelectedItem.Text.Trim();
                    string parte = Convert.ToString(Session["partePv"]);
                    cargaPartesPv();
                    PoblarParte();
                    cboParte_SelectedIndexChanged(sender, e);

                    btnGenerarPartePv.Enabled = true;
                    btnGuardarsf.Visible = true;

                    //if (cboEstado.Text == "Reportado")
                    //    this.CorreoAlertaOfac(cboClienteFacturar.SelectedItem.Value, cboClienteFacturar.SelectedItem.Text
                    //        , Convert.ToInt32(lblfup.Text), txtFechaOfac.Text, "REPORTADO", cboPaisFactura.SelectedItem.Text, lblObraPrincipal.Text);

                    if (chkBloqueado.Checked == true || chkCupo.Checked == true || chkMora.Checked == true)
                    {
                        string bloqueoTotal = "", bloqueoCupo = "", bloqueoMora = "";

                        if (chkBloqueado.Checked == true) bloqueoTotal = "-Bloqueo Total ";
                        if (chkCupo.Checked == true) bloqueoCupo = "-Bloqueado por Cupo ";
                        if (chkMora.Checked == true) bloqueoMora = "-Bloqueado por Mora ";

                        string mensajebloqueo = bloqueoTotal + bloqueoCupo + bloqueoMora;

                        //this.CorreoAlertaBloqueo(cboClienteFacturar.SelectedItem.Value, cboClienteFacturar.SelectedItem.Text
                        //    , Convert.ToInt32(lblfup.Text), txtFechaOfac.Text, mensajebloqueo, cboPaisFactura.SelectedItem.Text, lblObraPrincipal.Text);

                    }
                }
            }            
            if (txtDireccionDesp.Text.Trim().Length == 0){
                txtDireccionDesp.Text = txtDireccionDespVentas.Text.Trim();
            }
            if (txtDocumentosEnvio.Text.Trim().Length == 0)
            {
                txtDocumentosEnvio.Text = txtDocumentosEnvioVentas.Text.Trim();
            }
        }

        private void MensajePedidoVenta()
        {
            string mensaje = "";

            mensaje = "Datos de venta ingresados con éxito.";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void PoblarDepartamentoFactura(int cia)
        {
            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);

            cboDepfact.Items.Clear();
            cboDepfact.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlsf.ConsultarDepartamentoFacturar(cia, idpaisfact);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboDepfact.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarCiudadFactura(int cia)
        {

            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
            string idDepfact = Convert.ToString(cboDepfact.SelectedItem.Value);

            cboCiuFact.Items.Clear();
            cboCiuFact.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarCiudadFacturar(cia, idDepfact, idpaisfact);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCiuFact.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private SFEncabezado BuscarEncabezado(int SFId, ref string compania, ref string listaprecios, string Origen)
        {
            SFEncabezado RegistroSF = controlsf.ConsultarEncabezadoSF(SFId, ref compania, ref listaprecios, Origen);
            return RegistroSF;
        }

        private List<SFDetalle> BuscarDetalle(int SFId, ref string compania, ref string listaprecios, string Origen)
        {
            List<SFDetalle> DetalleSF = controlsf.ConsultarDetallesSF(SFId, ref compania, ref listaprecios, Origen);
            return DetalleSF;
        }

        private void enviarCorreo(int evento, int sfId, string usuario, string remitente, out string mensaje, string emailr)
        {
            controlsf.enviarCorreo(evento, sfId, usuario, remitente, out mensaje, emailr);
            return;
        }

        private string ResultadoSFxWS(int SFId, ref string compania, ref string coOperacion, ref string tiDocumento)
        {
            return controlsf.ResultadoSFxWS(SFId, ref compania, ref coOperacion, ref tiDocumento);            
        }

        private void PoblarClienteFactura(int cia)
        {
            
            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
            string idpdepfact = Convert.ToString(cboDepfact.SelectedItem.Value);
            string idciufact = Convert.ToString(cboCiuFact.SelectedItem.Value);
            
            cboClienteFacturar.Items.Clear();
            cboClienteFacturar.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarClienteFacturar(idciufact, idpaisfact, idpdepfact, cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteFacturar.Items.Add(new ListItem(reader.GetString(2), 
                        reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void AddClienteFacturaCia1(string p_idCliente,int cia)
        {
            reader = controlsf.ConsultarClienteFacCia1(p_idCliente,cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteFacturar.Items.Add(new ListItem(reader.GetString(2),
                        reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarNit()
        {
            lblnitfact.Text = "0";
            lblnitfact.Text = cboClienteFacturar.SelectedItem.Value.ToString();
            string nit = lblnitfact.Text;
            if (cboPlantaFact.SelectedItem.Value == "1") this.consultarFechaOfac(nit);
            this.consultarEstadoCliente(nit);
        }

        private void consultarFechaOfac(string nit)
        {
            string mensaje = "";
            reader = controlsf.ConsultarFechaOfac(nit);
            if (reader.HasRows == true)
            {
                if (reader.Read() == false)
                {
                    if (cboClienteFacturar.SelectedItem.Value != "0")
                    {
                        txtFechaOfac.Text = "";
                        cboEstado.SelectedValue = "-";
                        mensaje = "El Cliente a facturar no tiene verificacion en Lista OFAC, comuniquese con Asistente Comercial";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        this.InactivarBotonesOfac();
                    }
                }
                else
                {
                    DateTime fecha = Convert.ToDateTime(reader.GetValue(1).ToString());
                    txtFechaOfac.Text = fecha.ToString("d");
                    cboEstado.SelectedValue = reader.GetValue(2).ToString();
                    this.InactivarBotonesOfac();
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void consultarEstado(string nit)
        {
            if (cboPaisFactura.SelectedItem.Value != "0" && cboDepfact.SelectedItem.Value != "0" &&
                cboCiuFact.SelectedItem.Value != "0" && cboClienteFacturar.SelectedItem.Value != "0")
            {
                string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
                string idpdepfact = Convert.ToString(cboDepfact.SelectedItem.Value);
                string idciufact = Convert.ToString(cboCiuFact.SelectedItem.Value);
                string mensaje = "";

                lblBloqueo.Text = "";

                reader = controlsf.ConsultarEstadoClienteFact(Convert.ToInt32( cboPlantaFact.SelectedValue), idciufact, idpaisfact, idpdepfact, nit);
                if (reader.HasRows == true)
                {
                    if (reader.Read() != false)
                    {
                        if (reader.GetValue(1).ToString() == "1")
                        {
                            lblBloqueo.Text = "BLOQUEADO";
                            mensaje = "El Cliente a Facturar se encuentra BLOQUEADO";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }
        }

        private void consultarEstadoCliente(string nit)
        {
            int planta_id =Convert.ToInt32( cboPlantaFact.SelectedItem.Value);
            if (cboPaisFactura.SelectedItem.Value != "0" && cboDepfact.SelectedItem.Value != "0" &&
                cboCiuFact.SelectedItem.Value != "0" && cboClienteFacturar.SelectedItem.Value != "0")
            {
                string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
                string idpdepfact = Convert.ToString(cboDepfact.SelectedItem.Value);
                string idciufact = Convert.ToString(cboCiuFact.SelectedItem.Value);
                string mensaje = "";
                int compania=0;
                int rowTercero=0;
                string sucursal="";
                int estado=0, cupo=0,mora=0;

                lblBloqueo.Text = "";

                reader = controlsf.ConsultarEstadoClienteFact(planta_id,idciufact, idpaisfact, idpdepfact, nit);
                if (reader.HasRows == true)
                {
                    if (reader.Read() != false)
                    {
                        estado = Convert.ToInt32(reader.GetValue(0));
                        cupo = Convert.ToInt32(reader.GetValue(1));
                        mora = Convert.ToInt32(reader.GetValue(2));

                        lblnitfact.Text = reader.GetString(3).ToString();
                        LDirFactura.Text = reader.GetString(4).ToString();
                        lblNit2.Text = reader.GetString(5).ToString();
                        lblNit3.Text = reader.GetString(6).ToString();
                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();


                if (estado == 1)
                {
                    chkBloqueado.Checked = true ;
                    chkBloqueado.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    chkBloqueado.Checked = false;
                    chkBloqueado.BackColor = System.Drawing.Color.Transparent;
                }

                if (cupo == 1)
                {
                    chkCupo.Checked = true;
                    chkCupo.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    chkCupo.Checked = false;
                    chkCupo.BackColor = System.Drawing.Color.Transparent;
                }

                if (mora == 1)
                {
                    chkMora.Checked = true;
                    chkMora.BackColor = System.Drawing.Color.Red;
                }
                else
                {
                    chkMora.Checked = false;
                    chkMora.BackColor = System.Drawing.Color.Transparent;
                }
            }            
        }

        private void consultarEstadoClienteDespacho()
        {
            string nit = cboClienteDespachar.SelectedValue;
            int planta_id = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            if (cboPaiDesp.SelectedItem.Value != "0" && cboDepdesp.SelectedItem.Value != "0" &&
                cboCiuDesp.SelectedItem.Value != "0" && cboClienteDespachar.SelectedItem.Value != "0")
            {
                string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
                string idpdepdesp = Convert.ToString(cboDepdesp.SelectedItem.Value);
                string idciudesp = Convert.ToString(cboCiuDesp.SelectedItem.Value);
                string mensaje = "";
                int compania = 0;
                int rowTercero = 0;
                string sucursal = "";
                int estado = 0, cupo = 0, mora = 0;

                lblBloqueo.Text = "";

                reader = controlsf.ConsultarEstadoClienteFact(planta_id, idciudesp, idpaisdesp, idpdepdesp, nit);
                if (reader.HasRows == true)
                {
                    if (reader.Read() != false)
                    {
                        //estado = Convert.ToInt32(reader.GetValue(0));
                        //cupo = Convert.ToInt32(reader.GetValue(1));
                        //mora = Convert.ToInt32(reader.GetValue(2));

                        //lblnitfact.Text = reader.GetString(3).ToString();
                        LDirDespacho.Text = reader.GetString(4).ToString();

                    }
                }
                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();

            }
        }
      
        private void ActivarBotonesOfac()
        {
            string estado = "";
            string mensaje = "";
            Session["EstadoFacturar"] = cboEstado.SelectedValue;
            estado = Convert.ToString(Session["EstadoFacturar"]);

            System.Threading.Thread.Sleep(4000);
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string DescTipo = Convert.ToString(Session["DescTipo"]);

            if (txtFechaOfac.Text != "")
            {
                if (estado != "Reportado")
                {
                    
                    string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                    TimeSpan FechaFin;
                    DateTime FechaIni = Convert.ToDateTime(txtFechaOfac.Text);
                    FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                    int dias = FechaFin.Days;

                    dias = dias * -1;

                    if (dias < 365 && txtFechaOfac.Text != "" && cboEstado.Text != "-")
                    {
                        lblEstadoClifacturar.Text = "";
                        btnGuardarsf.Visible = true;
                        if (PuedeConfirmar) btnconfsf.Visible = true;
                        btnQuitarConfirmacion.Visible = false;
                        btnGenerarSF.Visible = true;
                    }
                }
            }
        }
         

        private void InactivarBotonesOfac()
        {
            string estado = "";
            string mensaje = "";
            Session["EstadoFacturar"] = cboEstado.SelectedValue;
            estado = Convert.ToString(Session["EstadoFacturar"]);

            System.Threading.Thread.Sleep(4000);
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string DescTipo = Convert.ToString(Session["DescTipo"]);

            if (cboClienteFacturar.SelectedItem.Value != "0")
            {

                if (txtFechaOfac.Text == "" && cboPlantaFact.SelectedItem.Value == "1")
                {
                    mensaje = mensaje + "No se ha verificado el Cliente en Lista OFac ";
                    mensaje = "Alerta! " + mensaje;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    btnGuardarsf.Visible = true;
                    btnconfsf.Visible = false;
                    btnQuitarConfirmacion.Visible = false;
                    btnGenerarSF.Visible = true;
                }
                else
                {

                    if (estado == "Reportado")
                    {
                        lblEstadoClifacturar.Text = "Cliente a Facturar REPORTADO en OFAC No es posible Guardar";
                        btnGuardarsf.Visible = true;
                        btnconfsf.Visible = false;
                        btnQuitarConfirmacion.Visible = false;
                        btnGenerarSF.Visible = false;
                        mensaje = "El Cliente a facturar está REPORTADO en Lista OFAC, comuniquese con Asistente Comercial";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                        TimeSpan FechaFin;
                        DateTime FechaIni = Convert.ToDateTime(txtFechaOfac.Text);
                        FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                        int dias = FechaFin.Days;

                        dias = dias * -1;

                        if (dias >= 365 || txtFechaOfac.Text == "" || cboEstado.Text == "-")
                        {
                            if (dias >= 365) mensaje = mensaje + " La fecha de verificacion de Ofac es superior a 1 año ";
                            if (txtFechaOfac.Text == "") mensaje = mensaje + "No se ha verificado el Cliente en OFac ";
                            if (cboEstado.Text == "-") mensaje = mensaje + "No se ha verificado el estado del Cliente en OFac ";

                            mensaje = "Alerta! " + mensaje;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            btnGuardarsf.Visible = true;
                            btnconfsf.Visible = false;
                            btnQuitarConfirmacion.Visible = false;
                            btnGenerarSF.Visible = true;
                        }
                    }
                }
            }
        }
         
        private void PoblarDepartamentoDespacho(int cia)
        {
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);

            cboDepdesp.Items.Clear();
            cboDepdesp.Items.Add(new ListItem("Seleccione", "0"));
            reader = controlsf.ConsultarDepartamentoFacturar(cia, idpaisdesp);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboDepdesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarCiudadDespacho(int PlantaId)
        {
            //int PlantaId = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
            string idDepdesp = Convert.ToString(cboDepdesp.SelectedItem.Value);

            cboCiuDesp.Items.Clear();
            cboCiuDesp.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarCiudadFacturar(PlantaId,idDepdesp, idpaisdesp);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCiuDesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void PoblarClienteDespacho(int cia)
        {
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
            string idpdepdesp = Convert.ToString(cboDepdesp.SelectedItem.Value);
            string idciudesp = Convert.ToString(cboCiuDesp.SelectedItem.Value);

            cboClienteDespachar.Items.Clear();
            cboClienteDespachar.Items.Add(new ListItem("Seleccione", "0"));

            reader = controlsf.ConsultarClienteFacturar(idciudesp, idpaisdesp, idpdepdesp, cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteDespachar.Items.Add(new ListItem(reader.GetString(2), reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        private void AddClienteDespacharCia1(string p_idCliente, int cia)
        {
            reader = controlsf.ConsultarClienteFacCia1(p_idCliente, cia);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteDespachar.Items.Add(new ListItem(reader.GetString(2),
                        reader.GetString(0).ToString().Trim()));
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        protected void btnGenerar_Click(object sender, EventArgs e)
        {
            GenerarParteSF();
        }

        protected void GenerarParteSF()
        {
            reader = controlsf.MaximoParte(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim(), Convert.ToInt32(cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {

                if (reader.Read() == true)
                {
                    int parte = Convert.ToInt32(reader.GetValue(0).ToString());
                    parte = parte + 1;

                    cboParte.Items.Add(new ListItem(Convert.ToString(parte), Convert.ToString(parte)));
                    cboParte.SelectedValue = Convert.ToString(parte);

                    Session["MensajeParte"] = 1;
                    MensajeParte();

                    LimpiarParte();
                    btnGenerar.Enabled = false;
                    //txtValorVenta.Enabled = true;
                }
                else
                {
                    Session["MensajeParte"] = 2;
                    MensajeParte();

                    btnGenerar.Enabled = true;
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }
        
        private void MensajeParte()
        {
            string mensaje = "";
              
            int parte = Convert.ToInt32(Session["MensajeParte"]);

            if (parte == 1)
            {
                 
                    mensaje = "Creación de parte ingresada con éxito.";
                              
            }
            else
            {
                 
                    mensaje = "Debe insertar la parte 1 para poder ingresar una nueva parte.";
                
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        //limpiar las partes
        private void LimpiarParte()
        {
            //PoblarDirector();
            //PoblarGerente();
            InstrumentoPago();
            FormaPago();
            PoblarTDN();
            ConsultarValorFlete();
            cargarTipoSf();
            cargarPuerto();


            Session["VrVenta"] = "0";
            Session["VrTotalM2"] = "0";

            //txtValorVenta.Text = "0";
            txtValorComercial.Text = "0";
            //txtDscto.Text = "0";
            //lblValorDscto.Text = "0";
            //txtRazonDescto.Text = " ";
            txtComentariosSF.Text = " ";
            txtObservaFactura.Text = " ";
            lblIVA.Text = "0";
            txtValorflete.Text = "0";
            lblValorTotalVenta.Text = "0";
            txtDireccionDesp.Text = " ";
            txtM2.Text = "0";
            txtDocumentosEnvio.Text = " ";
        }         

        private void ConsultarCuotas()
        {
            //int EncabezadoFUP = controlfup.EncabezadoFUP(Convert.ToInt32(txtFUP.Text.Trim()), cboVersion.SelectedItem.Text.Trim());
            // Parametros de la BBDD
            SqlParameter[] sqls = new SqlParameter[3];
            sqls[0] = new SqlParameter("@pfup_id ", Convert.ToInt32(lblfup.Text.Trim()));
            sqls[1] = new SqlParameter("@pversion ", LVer.Text.Trim());
            sqls[2] = new SqlParameter("@num ", Convert.ToInt32(cboCuota.SelectedItem.Text));

            string conexion = BdDatos.conexionScope(); 

            // Creamos la conexión
            using (SqlConnection con = new SqlConnection(conexion))
            {
                // Creamos el Comando
                using (SqlCommand cmd = new SqlCommand("USP_fup_SEL_cuotas_sf", con))
                {
                    cmd.CommandTimeout = 3600;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqls);

                    SqlParameter ST_ValorPagar = new SqlParameter("@valor_pagar", SqlDbType.Money);
                    SqlParameter ST_ValorPagado = new SqlParameter("@valor_pagado", SqlDbType.Money);
                    SqlParameter ST_FechaReal = new SqlParameter("@fecha_esperada", SqlDbType.Date);
                    SqlParameter ST_Comentarios = new SqlParameter("@comentarios", SqlDbType.VarChar, 12600);
                    SqlParameter ST_SaldoParcial = new SqlParameter("@saldo_parcial", SqlDbType.Money);
                    SqlParameter ST_Porcentaje = new SqlParameter("@porcentaje", SqlDbType.VarChar, 8);

                    ST_ValorPagar.Direction = ParameterDirection.Output;
                    ST_ValorPagado.Direction = ParameterDirection.Output;
                    ST_FechaReal.Direction = ParameterDirection.Output;                   
                    ST_Comentarios.Direction = ParameterDirection.Output;
                    ST_SaldoParcial.Direction = ParameterDirection.Output;
                    ST_Porcentaje.Direction = ParameterDirection.Output;

                    cmd.Parameters.Add(ST_ValorPagar);
                    cmd.Parameters.Add(ST_ValorPagado);
                    cmd.Parameters.Add(ST_FechaReal);
                    cmd.Parameters.Add(ST_Comentarios);
                    cmd.Parameters.Add(ST_SaldoParcial);
                    cmd.Parameters.Add(ST_Porcentaje);

                    // Abrimos la conexión y ejecutamos el ExecuteReader
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        //VALORES DEL ENCABEZADO 
                        decimal ValorPagar = Convert.ToDecimal(ST_ValorPagar.Value);
                        decimal ValorPagado = Convert.ToDecimal(ST_ValorPagado.Value);
                        string FechaReal = Convert.ToString(ST_FechaReal.Value);
                        string Comentarios = Convert.ToString(ST_Comentarios.Value);
                        decimal SaldoParcial = Convert.ToDecimal(ST_SaldoParcial.Value);
                        string Porcentaje = Convert.ToString(ST_Porcentaje.Value);

                        //ASIGNAMOS LOS VALORES A LOS CAMPOS  
                        txtaPagar.Text = Convert.ToString(ValorPagar.ToString("#,##.##"));
                        Session["APagar"] = txtaPagar.Text;
                        txtpagado.Text = Convert.ToString(ValorPagado.ToString("#,##.##"));
                        txtFechaReal.Text = FechaReal;
                        txtComentCuota.Text = Comentarios;
                        lblsaldocuota.Text = Convert.ToString(SaldoParcial.ToString("#,##.##"));
                        if (lblsaldocuota.Text == "")
                        {
                            lblsaldocuota.Text = "0";
                        }
                        txtporcpagar.Text = Porcentaje;
                    }
                    con.Close();
                }
            }
        }

        private void CargarGrillaCuotas()
        {
            ds.Reset();
            ds = controlsf.cuotaAcc(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            if (ds != null)
            {
                grvCuota.DataSource = ds.Tables[0];
                grvCuota.DataBind();
                grvCuota.Visible = true;
            }
            else
            {
                grvCuota.Dispose();
                grvCuota.Visible = false;
            }
            ds.Reset();
            
            controlsf.CerrarConexion();
        }

        protected void cboCuota_SelectedIndexChanged(object sender, EventArgs e)
        {
            ConsultarCuotas();
            ObtenerSumaCuotas();
        }

        private void ObtenerSumaCuotas()
        {
            string sum = "0";

            reader = controlsf.ObtenerSumaCuota(Convert.ToInt32(lblfup.Text));
            if (reader.HasRows == true)
            {
                if (reader.Read() == false)
                {
                    sum = "0";
                }
                else
                {
                    sum = reader.GetValue(0).ToString();
                }
            }
            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            decimal valorSaldo = Convert.ToDecimal(LVrProy.Text) - Convert.ToDecimal(sum);
            lblsaldocuota.Text = Convert.ToString(valorSaldo.ToString("#,##.##"));
        }

        protected void btnGenerarCuota_Click(object sender, EventArgs e)
        {
            reader = controlsf.MaximoCuota(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim());
            if (reader.HasRows == true)
            {
                if (reader.Read() == true)
                {
                    int cuota = Convert.ToInt32(reader.GetValue(0).ToString());
                    cuota = cuota + 1;

                    cboCuota.Items.Add(new ListItem(Convert.ToString(cuota), Convert.ToString(cuota)));
                    cboCuota.SelectedValue = Convert.ToString(cuota);

                    Session["MensajeCuota"] = 1;
                    MensajeParteCuota();

                    LimpiarCuota();
                    btnGenerarCuota.Enabled = false;
                }
                else
                {
                    Session["MensajeCuota"] = 2;
                    MensajeParteCuota();

                    btnGenerarCuota.Enabled = false;
                }
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

            txtFechaReal.Text = System.DateTime.Today.ToShortDateString();
        }

        private void LimpiarCuota()
        {
            Session["APagar"] = "0";
            txtaPagar.Text = "";
            txtporcpagar.Text = "";
            txtpagado.Text = "";
            txtFechaReal.Text = "";
            txtComentCuota.Text = "SIN COMENTARIOS";
        }

        private void MensajeParteCuota()
        {
            string mensaje = "";
             
            int cuota = Convert.ToInt32(Session["MensajeCuota"]);

            if (cuota == 1)
            {
                 
                    mensaje = "Creación de cuota ingresada con éxito.";
                
            }
            else
            {
                 
                    mensaje = "Debe insertar la cuota 1 para poder ingresar una nueva cuota.";
                 
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void txtM2_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
             
            string M2Venta = Convert.ToString(Session["VrTotalM2"]);

            if (cboTipoSf.SelectedItem.Value == "1")            
            {
                bool result = IsNumeric(txtM2.Text);
                if (cboTipoSf.SelectedItem.Value == "1" && (txtM2.Text == "" || txtM2.Text == "0"))
                {
                //if (txtM2.Text == "" || result == false)
                
                    mensaje = "Digíte los M2 correctamente.";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtM2.Text = "0";
                }
                else
                {

                    decimal SumM2 = (Convert.ToDecimal(txtM2.Text) + Convert.ToDecimal(LM2Proy.Text.Replace(",", "")) -
                        Convert.ToDecimal(M2Venta.Replace(",", "")));
                    //decimal SumM2 = (Convert.ToDecimal(txtM2.Text) + Convert.ToDecimal(lblM2Actual.Text.Replace(",", "")) -
                    //    Convert.ToDecimal(M2Venta.Replace(",", "")));

                    if (SumM2 > Convert.ToDecimal(lblM2Actual.Text.Replace(",", "")))
                    {
                        mensaje = "Los m2 del proyecto no pueden ser mayor a los m2 de cierre.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        txtM2.Text = "0";
                        btnGuardarsf.Enabled = true;
                    }
                    else
                    {
                        btnGuardarsf.Enabled = true;
                    }
                }
            }
            else
            {
 
            }
            
        }

        //protected void txtValorVenta_TextChanged(object sender, EventArgs e)
        //{
        //    string mensaje = "";
            
        //    bool result = IsNumeric(txtValorVenta.Text);
        //    decimal valordscto = 0;
        //    decimal valorcomercial = 0;            

        //    this.calcularTotal(1);
        //}

        protected void cboClienteDespachar_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarEstadoClienteDespacho();
            //PoblarDireccionDespacho();
        }

        protected void txtFechaOfac_TextChanged(object sender, EventArgs e)
        {
            string mensaje = ""; 
            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            TimeSpan FechaFin;
            DateTime FechaIni = Convert.ToDateTime(txtFechaOfac.Text);

            bool fechacomp = IsDatet(txtFechaOfac.Text);

            if (fechacomp != true)
            {
                mensaje = "Digite la fecha correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtFechaOfac.Text = "";
            }
            else
            {
                FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                int dias = FechaFin.Days;
                Session["diasOfac"] = dias;

                if (dias > 0)
                {
                    mensaje = "Alerta! No puede seleccionar una fecha mayor a hoy";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtFechaOfac.Text = "";
                }
                else
                {
                    dias = dias * -1;
                    if (dias >= 365)
                    {
                        dias = dias - 1;
                        mensaje = "Alerta! La Fecha ingresada debe ser inferior a un año, Verifique! " + dias + " dias.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        txtFechaOfac.Text = "";
                    }
                    else
                    {
                        mensaje = "Se verificó el cliente en Ofac hace: " + dias + " dias.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                }

            }

        }

        protected void btnOfac_Click(object sender, EventArgs e)
        {
            System.Threading.Thread.Sleep(4000);
            string Nombre = Convert.ToString(Session["Nombre_Usuario"]);
            string DescTipo = Convert.ToString(Session["DescTipo"]);
            string mensaje = "";

            if (txtFechaOfac.Text == "" || cboEstado.Text == "-")
            {
                mensaje = "Error! Seleccione los datos de OFAC";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
               
            }
            else
            {

                string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
                TimeSpan FechaFin;
                DateTime FechaIni = Convert.ToDateTime(txtFechaOfac.Text);
                FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                int dias = FechaFin.Days;

                dias = dias * -1;

                if (dias >= 365 || txtFechaOfac.Text == "" || cboEstado.Text == "-")
                {
                    if (dias >= 365) mensaje = mensaje + " La fecha de verificacion de Ofac es superior a 1 año ";
                    if (txtFechaOfac.Text == "") mensaje = mensaje + "No se ha ingresado la fecha de verificacion del Cliente en OFac ";
                    if (cboEstado.Text == "-") mensaje = mensaje + "No se ha seleccionado el estado del Cliente en OFac ";

                    mensaje = "Error! " + mensaje;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    string obra = Convert.ToString(Session["OBRA"]);
                    int IngOfac = controlsf.InsertarListaOfac(cboClienteFacturar.SelectedItem.Value, txtFechaOfac.Text,
                        Nombre, cboEstado.Text, cboClienteFacturar.SelectedItem.Text);
                    this.ActivarBotonesOfac();
                    this.InactivarBotonesOfac();

                    mensaje = "Se Actualizó Exitosamente el Cliente en Lista OFAC";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                    if (cboEstado.Text == "Reportado")
                        this.CorreoAlertaOfac(cboClienteFacturar.SelectedItem.Value, cboClienteFacturar.SelectedItem.Text
                                 , Convert.ToInt32(lblfup.Text), txtFechaOfac.Text, "REPORTADO", cboPaisFactura.SelectedItem.Text, obra);
                }
            }
        }

        protected void cboTipoFlete_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoFlete.SelectedItem.Value == "1")
            {
                txtValorflete.Enabled = true;
            }
            else 
            {
                txtValorflete.Enabled = false;
                txtValorflete.Text = "0";
                txtValorflete_TextChanged(sender, e);

                if (cboTipoFlete.SelectedItem.Value == "4")
                {
                    cboModoFactFlete.Enabled = false;
                    cboModoFactFlete.SelectedValue = "3";
                }
                else
                {                    
                    cboModoFactFlete.Enabled = true;
                }
            }            
        }

        protected void cboPlantaFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarCompaniaPlanta();
            // cia se convierte en planta
            int cia = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
           
            CondicionPago(cia);
            CentroOperacion(cia);
            Motivo(cia);
            CargarPaisFactura(cia);
            cboDepfact.Items.Clear();
            cboCiuFact.Items.Clear();
            cboClienteFacturar.Items.Clear();
            cboDepdesp.Items.Clear();
            cboCiuDesp.Items.Clear();
            cboClienteDespachar.Items.Clear();

            TipoCliente(cia);

            cargarPlantaProduccion(cia);
            cargarVendedor(cia);

            LDirFactura.Text = "";
            LDirDespacho.Text = "";
            lblnitfact.Text = "";
            lblNit2.Text = "";
            lblNit3.Text = "";

            if (cia == 3)
            {
                lblNIT.Text = "CNPJ .";
                lNit2.Text = "Ins Esta.";
                lnit3.Text = "Ins Muni.";
            }
            else 
            {
                lblNIT.Text = "NIT .";
                lNit2.Text = "Nit2 .";
                lnit3.Text = "Nit3 .";
            }
            //rpSF.Visible = false;
        }

        protected void cboPartePv_SelectedIndexChanged(object sender, EventArgs e)
        {
            //rpSF.Visible = false;
            //CONSULTA DATOS DE VENTA
            ConsultarPedidoVenta(Convert.ToInt32(cboPartePv.SelectedItem.Text.Trim()));
            consultarCompaniaPlanta();
            PoblarParte();            
            this.cboParte_SelectedIndexChanged(sender,e);            
            if (cboPlantaFact.SelectedItem.Value == "1") this.InactivarBotonesOfac();
            
        }

        protected void btnGenerarPartePv_Click(object sender, EventArgs e)
        {
            cboPlantaFact.Enabled = true;
            int resultado = cargarPlantaParte();

            if (resultado == 0)
            {
                string mensaje = "No Se Puede Generar Pedido a Otra Planta";               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                btnGenerarPartePv.Enabled = false;
            }
            else
            {
                consultarCompaniaPlanta();
                reader = controlsf.MaximoPartePv(Convert.ToInt32(lblfup.Text.Trim()));
                if (reader.HasRows == true)
                {
                    if (reader.Read() == true)
                    {
                        int parte = Convert.ToInt32(reader.GetValue(0).ToString());
                        parte = parte + 1;

                        cboPartePv.Items.Add(new ListItem(Convert.ToString(parte), Convert.ToString(parte)));
                        cboPartePv.SelectedValue = Convert.ToString(parte);

                        this.cargarPlantaParte();
                        this.cboPlantaFact_SelectedIndexChanged(sender, e);
                        //this.cboPlantaFact.SelectedValue = "0";

                        //if (cboPartePv.SelectedItem.Text.Trim() == "1") lblTipoParte.Text = "Principal"; else lblTipoParte.Text = "";
                        btnGenerarPartePv.Enabled = false;
                    }
                    else
                    {
                        Session["MensajeParte"] = 2;
                        MensajeParte();

                        btnGenerarPartePv.Enabled = false;
                    }
                }

                reader.Close();
                reader.Dispose();
                controlsf.CerrarConexion();
            }
        }

        protected void btnApruebaFinanc_Click(object sender, EventArgs e)
        {
            string mensaje = ""; 
            string BanderaConfirma = Convert.ToString(Session["BanderaConfirma"]);
            bool arrendadora = false, confFinanciero = false;
            string fechaFinanciero = "", usuFinanciero = ""; 

            reader = controlsf.ConsultarConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text),Convert.ToInt32( cboPartePv.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() != false)
                {
                    bool confSF = reader.GetSqlBoolean(0).Value;
                    arrendadora = reader.GetSqlBoolean(1).Value;
                    confFinanciero = reader.GetSqlBoolean(2).Value;
                    fechaFinanciero = reader.GetSqlDateTime(3).Value.ToString();
                    usuFinanciero = reader.GetSqlString(4).Value;


                    if (confSF == true)
                    {
                        mensaje = "El Pedido ya fue confirmado por el Comercial.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        btnconfsf.Enabled = false;
                        lblEstadoSf.Text = "SF CONFIRMADA";
                    }
                    else
                    {
                        if (confFinanciero == true)
                        {
                            mensaje = "El Pedido ya fue confirmado por el Financiero.";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                        else
                        {

                            string USUARIO = Convert.ToString(Session["Nombre_Usuario"]);
                            int actualizar = controlsf.actualizarConfirmarFinanciero(USUARIO, Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));

                            mensaje = "Se ha Aprobado el Pedido Exitosamente!";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            EnviarCorreoFinanciero("APROBACION PEDIDO:");
                        }
                    }

                }
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();

        }

        protected void cboTipoSf_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoSf.SelectedItem.Value == "1")
            {
                cboFactParte.Items.Clear();
                cboFactParte.Items.Add(new ListItem(cboPlantaFact.SelectedItem.ToString(), cboPlantaFact.SelectedItem.Value.ToString()));
                cboFactParte.SelectedValue = cboPlantaFact.SelectedItem.Value;
                PoblarTipoFlete();
                
            }
            else
            {
                cargarFacturarPlanta();
                cboTipoFlete.SelectedValue = "4";
                cboTipoFlete_SelectedIndexChanged(sender, e);
                txtM2.Text = "0";
            }
        }

        protected void cboPlantaProd_SelectedIndexChanged(object sender, EventArgs e)
        {

            reader = controlsf.ConsultarClienteFacInterno(Convert.ToInt32(cboPlantaFact.SelectedItem.Value), Convert.ToInt32(cboPlantaProd.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    lblIdClienteInterno.Text = reader.GetString(0).ToString();
                    lblClienteInterno.Text = reader.GetString(1).ToString();
                }

                if (lblIdClienteInterno.Text == "0")
                {
                    lblClienteInterno.Visible = false;
                    lblinterno.Visible = false;
                }
                else
                {
                    lblClienteInterno.Visible = true;
                    lblinterno.Visible = true;
                }
            }

            reader.Close();
            reader.Dispose();
            controlsf.CerrarConexion();
        }

        protected void btnprocesarSF_Click(object sender, EventArgs e)
        {
            btnprocesarSF.Enabled = false;
            Page.Validate("grupIp");
            System.Threading.Thread.Sleep(1000);
            string ti = "0";

            if (Request.QueryString["id_planta"] == null) {
                    if (Session["item_planta_id"] == null) {
                        ti = "0";
                    }
                    else {
                        ti = Session["item_planta_id"].ToString();
                    }               
                }

            ActualizarEstado(ti, e);
            btnprocesarSF.Enabled = false;
        }

        private string validarCamposObligatorios()
        {

            return "";
        }

        private void ActualizarEstado(String iplanta, EventArgs e)
        {

            EnviarWebService(iplanta, "", "", e);
        }
        private void EnviarWebService(string item_planta, string descripcion, string descripcion_corta, EventArgs e)
        {
            //string Mensaje = ""; string Nombre = (string)Session["Nombre_Usuario"];
            string Linea = "", mensaje = "";
            //Se genera la fecha en formato de 8 char
            DateTime thisDate1 = DateTime.Now;
            string fecha = thisDate1.ToString("yyyyMMdd");
            // Se debe buscar la cia que esta relacionada a la planta seleccionada
            int xgr = Convert.ToInt32(cboPlantaFact.SelectedItem.Value);
            string cia = "";
            string Origen = Convert.ToString(Session["Bandera"]);

            //List<SFEncabezado> RegistroSF = new List<SFEncabezado>();
            SFEncabezado RegistroSF = new SFEncabezado();
            List<SFDetalle> DetalleSFLista;
            SFDetalle DetalleSF = new SFDetalle();
            string LineasDetalle;
            string LDetalle;
            // para consulta es Session["SfId"]
            //int sfId = Convert.ToInt32(Session["SfId"]);
            int sfId = Convert.ToInt32(cboParte.SelectedItem.Value);
            //int sfId = Convert.ToInt32(lblfup.Text);
            string listaprecios = "000";
            RegistroSF = BuscarEncabezado(sfId, ref cia, ref listaprecios, Origen);
            DetalleSFLista = BuscarDetalle(sfId, ref cia, ref listaprecios, Origen);

            string ciacompleto = cia.PadLeft(3, '0');
            ciacompleto = LimitLength(ciacompleto, 3);

            string coOperacion = RegistroSF.CentroOperacionDocumento;
            string tiDocumento = RegistroSF.TipoDocumento;

            //Se une toda información necesaria para importar el item
            Type t = RegistroSF.GetType();
            Console.WriteLine("Type is: {0}", t.Name);
            PropertyInfo[] props = t.GetProperties();
            int puestoac = 0;
            char pad = '0';
            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                {
                    puestoac = puestoac + 1;
                    Linea = Linea + prop.GetValue(RegistroSF);
                    if (puestoac == 6)
                    {
                        cia = Convert.ToString(prop.GetValue(RegistroSF));
                        cia = cia.Trim();
                        ciacompleto = cia.PadLeft(3, pad);
                        ciacompleto = LimitLength(ciacompleto, 3);
                    }
                }
            Linea = "<Linea>" + Linea + "</Linea>";
            foreach (var prop in props)
                if (prop.GetIndexParameters().Length == 0)
                {
                    puestoac = puestoac + 1;
                    Linea = Linea + prop.GetValue(RegistroSF);
                    if (puestoac == 6)
                    {
                        cia = Convert.ToString(prop.GetValue(RegistroSF));
                        cia = cia.Trim();
                        ciacompleto = cia.PadLeft(3, pad);
                        ciacompleto = LimitLength(ciacompleto, 3);
                    }
                }
            Linea = "<Linea>" + Linea + "</Linea>";
            LineasDetalle = "";

            foreach (var DetSF in DetalleSFLista)
            {
                t = DetSF.GetType();
                props = t.GetProperties();
                LDetalle = "";
                foreach (var prop in props)
                    if (prop.GetIndexParameters().Length == 0)
                    {
                        LDetalle = LDetalle + prop.GetValue(DetSF);
                    }
                LineasDetalle = LineasDetalle + "<Linea>" + LDetalle + "</Linea>";
            }


            //Linea = ciacompleto + "00000000" + referencia + descripcion + descripcion_corta + grupoimpo + tipoinv + complt1 + Convert.ToInt16(chkListusoIp.Items[0].Selected) + Convert.ToInt16(chkListusoIp.Items[1].Selected) + Convert.ToInt16(chkListusoIp.Items[2].Selected) + complt2 + UndPcpal + "000000.0000000000.0000" + UndAdc + factAdc + PesoAdc + VolAdc + UndOrden + factOrden + PesoOrden + VolOrden + complt3 + fecha + complt4;

            //////Se crea el archivo xml que sera enviado para ser importado
            // esta es la conexion pruebas
            //string importar = "<?xml version='1.0' encoding='utf-8'?>"
            //        + "<Importar>"
            //        + "<NombreConexion>erppru</NombreConexion>"
            //        + "<IdCia>" + ciacompleto + "</IdCia>"
            //        + "<Usuario>samuelleon</Usuario>"
            //        + "<Clave>LOS2070915</Clave>"
            //        + "<Datos>"
            //        + "<Linea>000000100000001" + ciacompleto + "</Linea>"
            //        //+ "<Linea>" + Linea + "</Linea>"
            //        + Linea
            //        + "<Linea>000000499990001" + ciacompleto + "</Linea>"
            //        + "</Datos>"
            //        + "</Importar>";
            // esta es la conexion real
            string importar = "<?xml version='1.0' encoding='utf-8'?>"
                   + "<Importar>"
                   + "<NombreConexion>FORSA_PRUEBAS</NombreConexion>"
                   + "<IdCia>" + cia + "</IdCia>"
                   + "<Usuario>siif</Usuario>"
                   + "<Clave>SiifErp</Clave>"
                   + "<Datos>"
                   + "<Linea>000000100000001" + ciacompleto + "</Linea>"
                   + Linea
                   + LineasDetalle
                   + "</Datos>"
                   + "</Importar>";
            string stMsgErp = "";
            string stMsg = "";
            short x = (short)0;
            //WsReal.WSUNOEE WSDL = new WsReal.WSUNOEE();
            //com.siesacloud.wsforsa.WSUNOEE WSDL = new com.siesacloud.wsforsa.WSUNOEE(); ERP REAL
            com.siesacloud.wsforsapru.WSUNOEE WSDL = new com.siesacloud.wsforsapru.WSUNOEE();//ERP PRUEBAS
            System.Data.DataSet nodes = null;
            nodes = WSDL.ImportarXML(importar, ref x);
            //"Resultado del envio al web service  de item es:" + x;
            if (x == 0)
            {
                //Encontrar el último consecutivo generado
                string ConsecutivoSalida = "";
                ConsecutivoSalida = ResultadoSFxWS(sfId, ref cia, ref coOperacion, ref tiDocumento);
                mensaje = "Envío WebService realizado correctamente. Ultimo Consecutivo " + ConsecutivoSalida;
                int exito = controlsf.actualizarPedidoERP(Convert.ToInt32(ConsecutivoSalida), Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                    Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value));

                ConsecutivoSalida = "FUP_" + lblfup.Text + LVer.Text.Trim() + "_SF_" + cboParte.SelectedItem.Value.ToString() + "_PEDERP_" + ConsecutivoSalida + ".txt";
                CreateTextDelimiterFile(ConsecutivoSalida, importar);
                // Notificacion 
                string usuario = (string)Session["Usuario"];
                string CorreoUsuario = (string)Session["rcEmail"];
                string Nombre = (string)Session["Nombre_Usuario"];
                string correoSistema = (string)Session["CorreoSistema"];
                string UsuarioAsunto = Convert.ToString(Session["UsuarioAsunto"]);
                String cuerpoCorreo = mensaje + Linea;

                enviarCorreo(55, Convert.ToInt32(sfId), usuario, CorreoUsuario, out cuerpoCorreo, CorreoUsuario);
                btnprocesarSF.Enabled = false;
                btnprocesarSF.Visible = false;

            }
            else
            {
                //Recuperar Mensaje del WS
                if (nodes != null)
                {
                    foreach (DataRow fila in nodes.Tables[0].Rows)
                    {
                        stMsgErp = stMsgErp + " " + fila[6].ToString(); // + Environment.NewLine;
                    }
                }
                //mensaje = Environment.NewLine
                //            + stMsg + Environment.NewLine
                //            + stMsgErp + " Código = " + x;
                mensaje = stMsg + " " + stMsgErp;
            }
            string mensaje2 = mensaje;
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje2 + "')", true);

        }

        private string LimitLength(string source, int maxLength)
        {
            if (source.Length <= maxLength)
            {
                return source;
            }

            return source.Substring(0, maxLength);
        }


        // '' <summary>
        // '' Crea un archivo de texto delimitado con el contenido de
        // '' un objeto DataTable.
        // '' </summary>
        // '' <param name="fileName">Ruta y nombre del archivo de texto.</param>
        // '' <param name="dt">Un objeto DataTable v�lido.</param>
        // '' <param name="separatorChar">El car�cter delimitador de los campos.</param>
        // '' <param name="hdr">Indica si la primera fila contiene el nombre de los campos.</param>
        // '' <param name="textDelimiter">Indica si los campos alfanum�ricos deben aparecer
        // '' entre comillas dobles.</param>
        // '' <returns></returns>
        // '' <remarks></remarks>
        public bool CreateTextDelimiterFile(string fileName, string LineaSalida)
        {
            //  Si no se ha especificado un nombre de archivo,
            //  o el objeto DataTable no es v�lido, provocamos
            //  una excepci�n de argumentos no v�lidos.
            // 
            if (fileName == String.Empty)
            {
                throw new System.ArgumentException("Argumentos no v�lidos.");
            }
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            //OJO, Cambiar la G por I al colocar en Producción
            docPath = "I:\\PlanosERP";
            using (StreamWriter outputFile = new StreamWriter(Path.Combine(docPath, fileName)))
            {
                    outputFile.WriteLine(LineaSalida);
            }
            return true;
        }

        protected void txtVlrComer_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtVlrComer.Text);

            if (txtVlrComer.Text == "" || result == false)
            {
                mensaje = "Digite el valor comercial correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtVlrComer.Text = "0";
            }
            else
            {
                if (IsNumeric(txtVlrComer.Text))
                {
                    txtValorComercial.Text = txtVlrComer.Text;
                }
                this.calcularTotal(1);
                txtValorComercial_TextChanged (sender, e);
            }
        }

        protected void porcDscto_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            //decimal valordscto = 0;
            //decimal valorcomercial = 0;

            bool result = IsNumeric(porcDscto.Text);

            if (porcDscto.Text == "" || result == false)
            {
                mensaje = "Digite el descuento correctamente.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                porcDscto.Text = "0";
            }
            else
            {
                if (IsNumeric(txtVlrComer.Text))
                {
                    txtValorComercial.Text = txtVlrComer.Text;
                }
                this.calcularTotal(2);
            }
            txtValorComercial_TextChanged(sender, e);
        }

        protected override void InitializeCulture()
        {
            //public static readonly string fechaFormato = "yyyy-MM-dd";
            //public static readonly string tiempoFormato = "HH:mm";
            //public static readonly string culture = "es-CO";
            CultureInfo CI = new CultureInfo("es-CO");
            CI.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";
            CI.DateTimeFormat.ShortTimePattern = "HH:mm";
            CI.NumberFormat.CurrencyDecimalSeparator = ".";
            CI.NumberFormat.CurrencyGroupSeparator = ",";
            CI.NumberFormat.NumberDecimalSeparator = ".";
            CI.NumberFormat.NumberGroupSeparator = ",";
            CI.NumberFormat.PercentDecimalSeparator = ".";
            CI.NumberFormat.PercentGroupSeparator = ",";
            CI.DateTimeFormat.PMDesignator = "PM";
            CI.DateTimeFormat.AMDesignator = "AM";
            //Thread.CurrentThread.CurrentCulture = CI;
            //Thread.CurrentThread.CurrentUICulture = CI;
            base.InitializeCulture();
        }

        protected void btnGuardaPuerto_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string USUARIO = Convert.ToString(Session["Nombre_Usuario"]);
            int actualizar = controlsf.guardarPuerto(USUARIO, Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Text), Convert.ToInt32(cboPartePv.SelectedItem.Value),
                Convert.ToInt32(cboPuerto.SelectedItem.Value));

            string banderaPv = Convert.ToString(Session["Bandera"]);

            if (actualizar > 0)
            {
                //Session["Parte"] = cboParte.SelectedItem.Text;
                //Session["SfId"] = cboParte.SelectedItem.Value.ToString();
                //Session["Evento"] = 15;
                //fup_clase.CorreoFUP(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), Convert.ToInt32(Session["Evento"]));

                mensaje = "Se ha Guardado el Puerto.";

                // btnGuardarsf.Enabled = false;
            }
            else
            {
                mensaje = "Error No Guardo.";
            }


            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

        }

        //private void EnviarEncuestas(int FupIdE, int ClaseEncuesta) {
        //    reader = controlsf.ConsultarFUPSparaEncuesta();
        //    if (reader.HasRows == true)
        //    {
        //        while (reader.Read())
        //        {
        //            //VALORES DEL Mensaje                    
        //            string DestinatariosMail = reader.GetString(3);
        //            string MensajeMail = "";
        //            string AsuntoMail = "";
        //            //ClaseEncuesta = 1 = PROCESO COMERCIAL -- ClaseEncuesta = 2 = LLEGADA DE EQUIPO A OBRA
        //            if (ClaseEncuesta == 1)
        //            {
        //                AsuntoMail = "Encuesta sobre PROCESO COMERCIAL";
        //                MensajeMail = "Favor diligenciar encuesta siguiendo el enlace a continuación: {0} \n\r https://forms.office.com/Pages/ResponsePage.aspx?id=T1xBod5B6EexY-bZTE3sxubtefLd329FiR_ipbGrvzBUNlRDU044WTg3SDlRVEZENUJSNkRQNVU3Mi4u";
        //            }
        //            else {
        //                AsuntoMail = "Encuesta sobre LLEGADA DE EQUIPO A OBRA";
        //                MensajeMail = "Favor diligenciar encuesta siguiendo el enlace a continuación: {0} \n\r https://forms.office.com/Pages/ResponsePage.aspx?id=T1xBod5B6EexY-bZTE3sxubtefLd329FiR_ipbGrvzBUNE9RM1lOSlFHNTk4OFFISkpYQkM4Mk05OC4u";
        //            }
        //            string remitente = "";
        //            string mensaje = "";

        //            WebClient clienteWeb = new WebClient();
        //            clienteWeb.Dispose();
        //            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "FORSA");
        //            // Adjunto
        //            //DEFINIMOS LA CLASE DE MAILMESSAGE
        //            MailMessage mail = new MailMessage();
        //            //INDICAMOS EL EMAIL DE ORIGEN
        //            MailAddress emailremite = new MailAddress(remitente);
        //            mail.From = emailremite;
        //            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
        //            mail.To.Add(DestinatariosMail);
        //            //INCLUIMOS EL ASUNTO DEL MENSAJE
        //            mail.Subject = AsuntoMail;
        //            //AÑADIMOS EL CUERPO DEL MENSAJE
        //            mail.Body = MensajeMail;
        //            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
        //            mail.BodyEncoding = System.Text.Encoding.UTF8;
        //            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
        //            mail.Priority = System.Net.Mail.MailPriority.Normal;
        //            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
        //            mail.IsBodyHtml = true;
        //            //List<Byte[]> listCorreo = new List<Byte[]>();
        //            MemoryStream ms = new MemoryStream();
        //            SmtpClient smtp = new SmtpClient();
        //            //DEFINIMOS NUESTRO SERVIDOR SMTP
        //            smtp.Host = "smtp.office365.com";
        //            //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
        //            smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
        //            smtp.Port = 25;
        //            smtp.EnableSsl = true;

        //            ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain,
        //                SslPolicyErrors sslPolicyErrors)
        //            {
        //                return true;
        //            };
        //            try
        //            {
        //                smtp.Send(mail);
        //                mensaje = "";
        //                //listCorreo.Clear();
        //            }
        //            catch (Exception ex)
        //            {
        //                mensaje = "ERROR: " + ex.Message;
        //            }
        //            ms.Close();
        //            DateTime thisDate1 = DateTime.Now;
        //            controlsf.GuardasFUPSEncuestaEnviada(FupIdE, reader.GetString(3), thisDate1.ToString("yyyyMMdd"), ClaseEncuesta);
        //        }
        //    }
        //    reader.Close();
        //    reader.Dispose();
        //    controlsf.CerrarConexion();
        //}
    }
}
