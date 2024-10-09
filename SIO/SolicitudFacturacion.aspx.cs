using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using Microsoft.Reporting.WebForms;

namespace SIO
{
    public partial class SolicitudFacturacion : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();
        public ControlPedido contpv = new ControlPedido();
        
        public DataSet ds = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblfup.Text = (string)Session["FUP"];
                LVer.Text = (string)Session["VER"];
                lblTipo.Text = (string)Session["TIPO"];
                lblnumeropv.Text = (string)Session["NUMERO"];
                lblClienteprincipal.Text = (string)Session["CLIENTE"];
                lblObraPrincipal.Text = (string)Session["OBRA"];
                lblpais.Text = (string)Session["Pais"];
                            
                this.ConsultarNombrePais(lblpais.Text);

                this.PoblarParte();

                lbllogin.Text = (string)Session["Usuario"];              
                lblCorreousu.Text=(string)Session["rcEmail"];
                lblRolusu.Text=Convert.ToString((int)Session["Rol"]);


                this.consultarPedidoVenta();
                Session["contador"] = 0;

                //activamos los botones
                int arRol = (int)Session["Rol"];
                string bandera = (string)Session["Bandera"];
                
                if ((arRol == 2) || (arRol == 9))
                {
                    if (bandera == "1")
                    {
                        btnconfPV.Visible = false;
                    }
                    else
                    {
                        btnconfPV.Visible = true;
                    }    
                    btnconfsf.Visible = true;
                    chkQuitarConfirsf.Visible = true;
                }
                
                this.consultarConfirmSF();
                this.poblarCuota();
                CargarGrillaCuota();
                this.cargarCuotas();
                //chkQuitarConfirsf.Attributes.Add("language", "javascript");
                //chkQuitarConfirsf.Attributes.Add("OnClick", "return confirm('Desea quitar la confirmacion de la SF?');");                    
            }
        }

        private void PoblarParte()
        {
            //CONSULTAMOS LA VERSION CON EL FUP
            cboParte.Items.Clear();
            reader = controlsf.PoblarParte(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim());
            if (reader.Read() == false)
            {
                cboParte.Items.Add("1");
            }
            else
            {
                reader = controlsf.PoblarParte(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim());
                while (reader.Read())
                {
                    cboParte.Items.Add(new ListItem(reader.GetInt32(1).ToString()));
                }
                reader.Close();
            }
            reader.Close();            
        }

        private void cargarCuotas()
        {
            this.limpiarCuotaLF();
            this.poblarCuota();
            //btngenerarScLF.Text = "Actualizar Cuota";

            ////VISIBILIZAR CUOTA
            //cnlLF.Visible = false;
            //cboCuotaLF.Visible = true;
            //btnAgregarCuotaLF.Visible = true;
            //btnEliminarCuotaLF.Visible = true;
            //btngenerarScLF.Enabled = true;

            string sum;

            reader = controlsf.ObtenerSumaCuota(Convert.ToInt32(lblfup.Text));
            if (reader.Read() == false)
            {
                sum = "0";
            }
            else
            {
                sum = reader.GetValue(0).ToString();
            }

            string moneda, simbolo = "";
            reader = controlsf.ObtnerMonedaFup(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");
            reader.Read();
            moneda = reader.GetValue(1).ToString();
            simbolo = reader.GetValue(2).ToString();
            reader.Close();

            decimal valorSaldo = Convert.ToDecimal(lblValorTotalVenta.Text) - Convert.ToDecimal(sum);
            
            lblsaldocuota.Text = simbolo + Convert.ToString(valorSaldo.ToString("#,##.##")) + " " + moneda;

            ds = controlsf.cuotaAcc(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            GridView1.DataSource = ds.Tables[0];
            GridView1.DataBind();
            Session["Partes"] = ds;
            reader.Close();

            GridView1.Visible = true;

            this.limpiarCuotaLF();
            this.poblarCuota();
            GridView1.DataBind();
        }

        public void ConsultarNombrePais(string pais)
        {
            reader = controlsf.ConsultarNombrePais(Convert.ToInt32(lblpais.Text));

            if (reader.Read())
            {
                Session["paisnombre"] = reader.GetSqlString(0).ToString();
            }
        }

        private void poblarCuota()
        {
            cboCuota.Items.Clear();
            reader = controlsf.obtenerCuotas(Convert.ToInt32(lblfup.Text), LVer.Text);
            if (reader.HasRows)
            {
                cboCuota.Items.Add("---------------");
                int contador = (int)Session["contador"];

                while (reader.Read())
                {
                    cboCuota.Items.Add(new ListItem(reader.GetInt32(0).ToString()));
                }
                reader.Close();
                Session["contador"] = contador;
                txtFechaReal.Text = DateTime.Now.ToShortDateString();
            }
            else
            {
                cboCuota.Items.Add("---------------");
                cboCuota.Items.Add("0");
            }
        }

        private void cargarTDN(string estado)
        {
            reader = controlsf.ConsultarTDN();

            if (estado == "Limpiar")
            {
                cboTDN.Items.Clear();
                cboTDN.Items.Add(new ListItem("Seleccione", "0"));
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboTDN.Items.Add(new ListItem("-----", "0"));
                }
            }
           
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboTDN.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
        }
        

        private void cargarPaisfactura(string estado)
        {
            string rol1 = "";
            string rol2 = "";
            reader = controlsf.ConsultarPaisFactura();

            if (estado == "Limpiar")
            {
                cboPaisFactura.Items.Add(new ListItem("Seleccione", "0"));
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboPaisFactura.Items.Add(new ListItem("-----", "0"));
                }
            }
            
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPaisFactura.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
        }

        private void comboDepartafactura()
        {
            cboDepfact.Items.Clear();
            cboDepfact.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void consultarPedidoVenta()
        {
            int fup = Convert.ToInt32(lblfup.Text);
            

            //cargo los datos de pedido de venta
            reader = controlsf.ConsultarPedidoVenta(fup);

            if (reader.Read() == true)
            {
                string estado = "Adicionar";
                btnGuardarsf.Text = "Actualizar";
                cboCondPago.Items.Add(new ListItem(reader.GetValue(1).ToString(), reader.GetValue(0).ToString()));
                cboCentOpe.Items.Add(new ListItem(reader.GetValue(3).ToString(), reader.GetValue(2).ToString()));
                cboMotivo.Items.Add(new ListItem(reader.GetValue(4).ToString(), reader.GetValue(4).ToString()));
                cboPaisFactura.Items.Add(new ListItem(reader.GetValue(6).ToString(), reader.GetValue(5).ToString()));
                cboDepfact.Items.Add(new ListItem(reader.GetValue(8).ToString(), reader.GetValue(7).ToString()));
                cboCiuFact.Items.Add(new ListItem(reader.GetValue(10).ToString(), reader.GetValue(9).ToString()));
                cboClienteFacturar.Items.Add(new ListItem(reader.GetValue(12).ToString(), reader.GetValue(11).ToString()));
                lblnitfact.Text = reader.GetValue(13).ToString();
                cboPaiDesp.Items.Add(new ListItem(reader.GetValue(15).ToString(), reader.GetValue(14).ToString()));
                cboDepdesp.Items.Add(new ListItem(reader.GetValue(17).ToString(), reader.GetValue(16).ToString()));
                cboCiuDesp.Items.Add(new ListItem(reader.GetValue(19).ToString(), reader.GetValue(18).ToString()));
                cboClienteDespachar.Items.Add(new ListItem(reader.GetValue(21).ToString(), reader.GetValue(20).ToString()));
                txtDias.Text = reader.GetValue(22).ToString();
                txtFechaDes.Text = reader.GetDateTime(23).ToShortDateString();
                cboTipoCliente.Items.Add(new ListItem(reader.GetValue(24).ToString(), reader.GetValue(25).ToString()));

                this.CondicionPago(estado);
                this.Motivo(estado);
                this.CentroOperacion(estado);
                this.TipoCliente(estado);
                this.cargarPaisfactura(estado);
                this.cargarPaisdesp(estado);
                
                lblMensaje.Text = "Estado: SF-Ingresada.";
            }
            else
            {
                string estado = "Limpiar";
                this.CondicionPago(estado);
                this.Motivo(estado);
                this.CentroOperacion(estado);
                this.TipoCliente(estado);
                this.cargarPaisfactura(estado);
                this.cargarPaisdesp(estado);

                this.comboDepartafactura();
                this.comboCiudadfactura();
                this.comboClientefactura();                
                this.comboDepartadesp();
                this.comboCiudaddesp();
                this.comboClientedesp();

                lblMensaje.Text = "Estado: SF-Sin Ingresar.";
            }
            reader.Close();

            //cargo los datos de solicitud de facturacion
            reader = controlsf.ConsultarSolicitudFacturacion(fup, LVer.Text, Convert.ToInt32(cboParte.SelectedItem.Value));
            if (reader.Read() == true)
            {
                string estado = "Adicionar";
                
                lblvlrventa.Text = reader.GetValue(0).ToString();
                txtValorComercial.Text = reader.GetValue(1).ToString();
                txtDscto.Text = reader.GetValue(2).ToString();
                lblValorDscto.Text = reader.GetValue(3).ToString();
                txtRazonDescto.Text = reader.GetValue(4).ToString();
                lblIVA.Text = reader.GetValue(5).ToString();
                txtValorflete.Text = reader.GetValue(6).ToString();
                lblValorTotalVenta.Text =Convert.ToString(reader.GetValue(7).ToString());
                txtComentariosSF.Text = reader.GetValue(8).ToString();
                txtDireccionDesp.Text = reader.GetValue(15).ToString();
                VrAlum.Text = reader.GetValue(16).ToString();
                LPlast.Text = reader.GetValue(17).ToString();
                LAcero.Text = reader.GetValue(18).ToString();

                //LPrecioUni.Text = Convert.ToString(PrecioUni.ToString("#,##.##"));

                cboDirector.Items.Add(new ListItem(reader.GetValue(9).ToString(), reader.GetValue(9).ToString()));
                cboGerente.Items.Add(new ListItem(reader.GetValue(10).ToString(), reader.GetValue(10).ToString()));
                cboInsPago.Items.Add(new ListItem(reader.GetValue(11).ToString(), reader.GetValue(11).ToString()));
                cboFormaPago.Items.Add(new ListItem(reader.GetValue(12).ToString(), reader.GetValue(12).ToString()));
                cboTDN.Items.Add(new ListItem(reader.GetValue(13).ToString(), reader.GetValue(14).ToString()));
                
                this.FormaPago(estado);
                this.cargarTDN(estado);
                this.InstrumentoPago(estado);
                this.DirectorOficina(estado);
                this.GerenteComercial(estado);

                if (txtValorflete.Text.Length == 0) txtValorflete.Text = "0";
                //CONVIERTO A VALORES DECIMALES
                ValoresDecimales();

                //BLINDAMOS LOS VALORES VACIOS EN CERO
                ValoresVacios();
            }
            else 
            {
                string estado = "Limpiar";

                this.FormaPago(estado);
                this.cargarTDN(estado);
                this.InstrumentoPago(estado);
                this.DirectorOficina(estado);
                this.GerenteComercial(estado);
                this.calcularValorVenta();
                this.CalculaIva();
                this.calcularDescuento();
                this.ConsultarValorFlete();
                this.ValorTotal();
            }
            reader.Close();           
        }

        private void ValoresDecimales()
        {
            if (txtValorflete.Text.Length == 0) txtValorflete.Text = "0";
            if (VrAlum.Text.Length == 0) VrAlum.Text = "0";
            if (LPlast.Text.Length == 0) LPlast.Text = "0";
            if (LAcero.Text.Length == 0) LAcero.Text = "0";
            
            //CONVIERTO LOS VALORES A DOS DECIMALES
            decimal vrVenta = Convert.ToDecimal(lblvlrventa.Text);
            lblvlrventa.Text = Convert.ToString(vrVenta.ToString("#,##.##"));
            decimal vrComercial = Convert.ToDecimal(txtValorComercial.Text);
            txtValorComercial.Text = Convert.ToString(vrComercial.ToString("#,##.##"));
            decimal vrDcto = Convert.ToDecimal(lblValorDscto.Text);
            lblValorDscto.Text = Convert.ToString(vrDcto.ToString("#,##.##"));
            decimal vrIVA = Convert.ToDecimal(lblIVA.Text);
            lblIVA.Text = Convert.ToString(vrIVA.ToString("#,##.##"));
            decimal vrFlete = Convert.ToDecimal(txtValorflete.Text);
            txtValorflete.Text = Convert.ToString(vrFlete.ToString("#,##.##"));
            decimal vrTotalVenta = Convert.ToDecimal(lblValorTotalVenta.Text);
            lblValorTotalVenta.Text = Convert.ToString(vrTotalVenta.ToString("#,##.##"));  
            decimal VrAlumin = Convert.ToDecimal(VrAlum.Text);
            VrAlum.Text = Convert.ToString(VrAlumin.ToString("#,##.##"));            
            decimal VrPlast = Convert.ToDecimal(LPlast.Text);
            LPlast.Text = Convert.ToString(VrPlast.ToString("#,##.##"));
            decimal VrAcero = Convert.ToDecimal(LAcero.Text);
            LAcero.Text = Convert.ToString(VrAcero.ToString("#,##.##"));

            ValoresVacios();
        }

        private void ValoresVacios()
        {
            //BLINDAMOS LOS VALORES VACIOS EN CERO
            if (lblvlrventa.Text == "")
            {
                lblvlrventa.Text = "0";
            }
            if (txtValorComercial.Text == "")
            {
                txtValorComercial.Text = "0";
            }
            if (lblValorDscto.Text == "")
            {
                lblValorDscto.Text = "0";
            }
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
            if (VrAlum.Text == "")
            {
                VrAlum.Text = "0";
            }
            if (LPlast.Text == "")
            {
                LPlast.Text = "0";
            }
            if (LAcero.Text == "")
            {
                LAcero.Text = "0";
            }
            if (txtDscto.Text == "")
            {
                txtDscto.Text = "0";
            }
        }

        private void comboCiudadfactura()
        {
            cboCiuFact.Items.Clear();
            cboCiuFact.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void comboClientefactura()
        {
            cboClienteFacturar.Items.Clear();
            cboClienteFacturar.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void cargarPaisdesp(string estado)
        {
            reader = controlsf.ConsultarPaisFactura();

            if (estado == "Limpiar")
            {
                cboPaiDesp.Items.Add(new ListItem("Seleccione", "0"));
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboPaiDesp.Items.Add(new ListItem("-----", "0"));
                }                
            }
            
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboPaiDesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
        }

        private void comboDepartadesp()
        {
            cboDepdesp.Items.Clear();
            cboDepdesp.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void comboCiudaddesp()
        {
            cboCiuDesp.Items.Clear();
            cboCiuDesp.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void comboClientedesp()
        {
            cboClienteDespachar.Items.Clear();
            cboClienteDespachar.Items.Add(new ListItem("Seleccione", "0"));
        }

        private void CondicionPago(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];

            if (estado == "Limpiar")
            {
                cboCondPago.Items.Clear();
                if (idioma == "Español")
                {
                    cboCondPago.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboCondPago.Items.Add(new ListItem("Select", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboCondPago.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else 
            {
                if (estado == "Adicionar")
                {
                    cboCondPago.Items.Add(new ListItem("-----", "0"));
                }
            }
            
            reader = controlsf.ConsultarCondicionPago();

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboCondPago.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();        
        }

        private void Motivo(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string motivo = (string)Session["Motivo"];

            if (estado == "Limpiar")
            {
                cboMotivo.Items.Clear();

                if (idioma == "Español")
                {
                    cboMotivo.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboMotivo.Items.Add(new ListItem("Select", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboMotivo.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else
            {
                if (estado== "Adicionar")
                {
                    cboMotivo.Items.Add(new ListItem("-----", "0"));
                }
            }
           

            if (motivo == "Accesorio")
            {
                motivo = "1";
            }
            else
            {
                motivo = "2";
            }

            reader = controlsf.ConsultarMotivo();

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboMotivo.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
        }

        private void CentroOperacion(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];

            if (estado == "Limpiar")
            {
                cboCentOpe.Items.Clear();
                if (idioma == "Español")
                {
                    cboCentOpe.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboCentOpe.Items.Add(new ListItem("Choose", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboCentOpe.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboCentOpe.Items.Add(new ListItem("-----", "0"));
                }
            }
            
            reader = controlsf.ConsultarCentroOperacion();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboCentOpe.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
        }

        private void TipoCliente(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];

            if (estado == "Limpiar")
            {
                cboTipoCliente.Items.Clear();
                if (idioma == "Español")
                {
                    cboTipoCliente.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboTipoCliente.Items.Add(new ListItem("Choose", "0"));

                }
                if (idioma == "Portugues")
                {
                    cboTipoCliente.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboTipoCliente.Items.Add(new ListItem("-----", "0"));
                }
            }

            reader = controlsf.ConsultarTipoCliente();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboTipoCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
                }
            }
            reader.Close();
        }



        private void DirectorOficina(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            string pais = (string)Session["Pais"];
            string ciudad = (string)Session["Ciudad"];

            if (estado == "Limpiar")
            {
                cboDirector.Items.Clear();

                if (idioma == "Español")
                {
                    cboDirector.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboDirector.Items.Add(new ListItem("Choose", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboDirector.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else
            {
                if (estado == "Adicionar")
                {
                    cboDirector.Items.Add(new ListItem("-----", "0")); 
                }
            }

           
            if (pais == "8")
            {
                reader = controlsf.ConsultarDirectorOficinaColombia(Convert.ToInt32(pais), Convert.ToInt32(ciudad));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        cboDirector.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
            }
            else
            {
                reader = controlsf.ConsultarDirectorOficina(Convert.ToInt32(pais));
                if (reader != null)
                {
                    while (reader.Read())
                    {
                        cboDirector.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
            }
            reader.Close();
        }

        private void GerenteComercial(string estado)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            string pais = (string)Session["Pais"];

            if (estado == "Limpiar")
            {
                cboGerente.Items.Clear();

                if (idioma == "Español")
                {
                    cboGerente.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboGerente.Items.Add(new ListItem("Choose", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboGerente.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else 
            {
                if(estado== "Adicionar")
                {
                    cboGerente.Items.Add(new ListItem("-----", "0"));
                }
            }
            
            reader = controlsf.ConsultarGerenteComercial(Convert.ToInt32(pais));
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboGerente.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
        }

        private void InstrumentoPago(string estado)
        {
            string idioma = (string)Session["Idioma"];

            if (estado == "Limpiar")
            {
                cboInsPago.Items.Clear();

                if (idioma == "Español")
                {
                    cboInsPago.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboInsPago.Items.Add(new ListItem("Choose", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboInsPago.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else 
            {
                cboInsPago.Items.Add(new ListItem("-----", "0"));
            }
            
            reader = controlsf.ConsultarInstrumentoPago();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboInsPago.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
        }

        private void FormaPago(string estado)
        {
            string idioma = (string)Session["Idioma"];
            
            if (estado == "Limpiar")
            {
                cboFormaPago.Items.Clear();
                if (idioma == "Español")
                {
                    cboFormaPago.Items.Add(new ListItem("Seleccione", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboFormaPago.Items.Add(new ListItem("Choose", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboFormaPago.Items.Add(new ListItem("Escolher", "0"));
                }
            }
            else 
            {
                if (estado=="Adicionar")
                {
                    cboFormaPago.Items.Add(new ListItem("-----", "0"));
                }
            }

            reader = controlsf.ConsultarFormaPago();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboFormaPago.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
        }

        

        protected void cboPaisFactura_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
            this.comboDepartafactura();
            this.comboCiudadfactura();
            this.comboClientefactura();
            reader = controlsf.ConsultarDepartamentoFacturar(idpaisfact);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboDepfact.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }

        protected void cboCiuFact_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
            string idpdepfact = Convert.ToString(cboDepfact.SelectedItem.Value);
            string idciufact = Convert.ToString(cboCiuFact.SelectedItem.Value);
            cboClienteFacturar.Items.Clear();
            this.comboClientefactura();
            reader = controlsf.ConsultarClienteFacturar(idciufact, idpaisfact, idpdepfact);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboClienteFacturar.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }

        protected void cboPaiDesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
            this.comboDepartadesp();
            this.comboCiudaddesp();
            this.comboClientedesp();
            reader = controlsf.ConsultarDepartamentoFacturar(idpaisdesp);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboDepdesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }

        protected void cboCiuDesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
            string idpdepdesp = Convert.ToString(cboDepdesp.SelectedItem.Value);
            string idciudesp = Convert.ToString(cboCiuDesp.SelectedItem.Value);
            cboClienteDespachar.Items.Clear();
            this.comboClientedesp();
            reader = controlsf.ConsultarClienteFacturar(idciudesp, idpaisdesp, idpdepdesp);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboClienteDespachar.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }

        //CARGO EL NIT DEL CLIENTE SELECCIONADO
        protected void cboClienteFacturar_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblnitfact.Text = "0";
            lblnitfact.Text = cboClienteFacturar.SelectedItem.Value.ToString();
        }

        //SUMO EL VALOR DE LOS ACCESORIOS PARA EL VALOR DE VENTA
        protected void calcularValorVenta()
        {
            reader = controlsf.ObtnerSuma_accesorios(Convert.ToInt32((string)Session["FUP"]), "n_Rol", "p_Rol");
            if (reader.Read() == true)
            {
                lblvlrventa.Text = reader.GetValue(0).ToString();
                txtValorComercial.Text = reader.GetValue(0).ToString();
            }
            else
            {
                lblvlrventa.Text = "0";
            }
            reader.Close();
        }

        protected void txtValorComercial_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtValorComercial.Text);

            if (txtValorComercial.Text == "" || result == false)
            {
                mensaje = "Digite el valor comercial correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtValorComercial.Text = "0";
            }

            this.calcularDescuento();
            this.CalculaIva();
            this.ValorTotal();
            ValoresDecimales();
        }

        //CALCULO EL IVA SOBRE EL VALOR COMERCIAL
        private void CalculaIva()
        {
            decimal vlrComercial = Convert.ToDecimal(txtValorComercial.Text);
            if (lblpais.Text == "8")
            {
                if (vlrComercial > 0)
                {
                    decimal IVA = controlsf.calculoIVA(Convert.ToDecimal(vlrComercial));
                    lblIVA.Text = Convert.ToString(IVA);
                }
            }
        }

        //CALCULAR DESCUENTO
        private void calcularDescuento()
        {
            if (lblvlrventa.Text != txtValorComercial.Text)
            {
                decimal Porc, PorVenta;

                Porc = ((Convert.ToDecimal(txtValorComercial.Text) * 100) / Convert.ToDecimal(lblvlrventa.Text));
                PorVenta = 100 - Convert.ToDecimal(Porc);
                txtDscto.Text = Convert.ToString(PorVenta.ToString("#.##"));

                ValoresVacios();

                decimal vrFin = Convert.ToDecimal(lblvlrventa.Text) - Convert.ToDecimal(txtValorComercial.Text);
                lblValorDscto.Text = Convert.ToString(vrFin);
            }
            else
            {
                txtDscto.Text = "0";
                lblValorDscto.Text = "0";
            }
        }

        //CONSULTO EL VALOR DEL FLETE DESDE EL FUP
        private void ConsultarValorFlete()
        {
            int fup = Convert.ToInt32(lblfup.Text);
            reader = controlsf.consultarFletepv(fup);

            if (reader.Read() == true)
            {
                txtValorflete.Text = reader.GetDecimal(1).ToString("#.##");
            }
            else
            {
                txtValorflete.Text = "0";
            }
            reader.Close();
        }

        //CONSULTO EL VALOR TOTAL SI NO EXISTE SF
        private void ValorTotal()
        {
            decimal valortotal = 0;
            if (txtValorflete.Text == "")
            {
                txtValorflete.Text = "0";
            }
            valortotal = Convert.ToDecimal(txtValorComercial.Text) + Convert.ToDecimal(txtValorflete.Text) + Convert.ToDecimal(lblIVA.Text);
            lblValorTotalVenta.Text = Convert.ToString(valortotal);
            ValoresDecimales();
        }

        protected void txtValorflete_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtValorflete.Text);

            if (txtValorflete.Text == "" || result == false)
            {                
                mensaje = "Digite el valor del flete correctamente";
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
                    this.calcularDescuento();
                    this.CalculaIva();
                    this.ValorTotal();
                }
            }
            
        }

        public static Boolean IsNumeric(string precio)
        {
            decimal result;
            return decimal.TryParse(precio, out result);
        }     
   
        public static Boolean IsDatet(string fecha)
        {
            DateTime result;
            return DateTime.TryParse(fecha, out result);
        }

        protected void txtDscto_TextChanged(object sender, EventArgs e)
        {
            decimal valordscto = 0;
            decimal valorcomercial = 0;
            string mensaje = "";

            bool result = IsNumeric(txtDscto.Text);

            if (txtDscto.Text == "" || result == false)
            {
                mensaje = "Digite el descuento correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtDscto.Text = "0";
            }
            valordscto = (Convert.ToDecimal(lblvlrventa.Text) * Convert.ToDecimal(txtDscto.Text)) / 100;
            lblValorDscto.Text = Convert.ToString(valordscto);

            valorcomercial = Convert.ToDecimal(lblvlrventa.Text) - valordscto;
            txtValorComercial.Text = Convert.ToString(valorcomercial);

            this.CalculaIva();
            this.ValorTotal();
            ValoresDecimales();
        }

        protected void cboDepfact_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisfact = Convert.ToString(cboPaisFactura.SelectedItem.Value);
            string idDepfact = Convert.ToString(cboDepfact.SelectedItem.Value);
            this.comboCiudadfactura();
            this.comboClientefactura();
            reader = controlsf.ConsultarCiudadFacturar(idDepfact, idpaisfact);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboCiuFact.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }


        protected void cboDepdesp_SelectedIndexChanged(object sender, EventArgs e)
        {
            string idpaisdesp = Convert.ToString(cboPaiDesp.SelectedItem.Value);
            string idDepdesp = Convert.ToString(cboDepdesp.SelectedItem.Value);
            this.comboCiudaddesp();
            this.comboClientedesp();
            reader = controlsf.ConsultarCiudadFacturar(idDepdesp, idpaisdesp);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboCiuDesp.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
        }

        protected void btnGuardarsf_Click(object sender, EventArgs e)
        {
            int validaciondatos=this.validarDatos();

            if (validaciondatos == 1)
            {
                string Nombre = (string)Session["Nombre_Usuario"];
                string prod = (string)Session["PROD"];
                string coutas = "0";
                
                //BUSCAR NUMERO DE CUOTAS
                reader = controlsf.obtenerCantCuotas(Convert.ToInt32(lblfup.Text), LVer.Text, Convert.ToInt32(cboParte.SelectedItem.Value));
                if (reader.Read() == false)
                {
                    coutas = "0";
                }
                else
                {
                    coutas = reader.GetValue(0).ToString();
                }


                if (coutas == "")
                {
                    coutas = "0";
                }

                //CAPTURO DATOS PARA PEDIDO DE VENTA
                reader = controlsf.ConsultarDatosClienteFact(cboClienteFacturar.SelectedItem.Value);

                string CLIENTE_MONEDA = "";
                string VENDEDOR = "";
                string CONTACTO = "";

                if (reader != null)
                {
                    while (reader.Read())
                    {
                        CLIENTE_MONEDA = reader.GetString(1).ToString();
                        VENDEDOR = reader.GetString(2).ToString();
                        CONTACTO = reader.GetDecimal(3).ToString();
                    }
                }
                reader.Close();

                string SOLCLI = (string)Session["CLIENTEID"];
                string CLIENTE_FACTURA = cboClienteFacturar.SelectedItem.Value;
                string FACTURA_SUCURSAL = "001";
                string CLIENTE_DESPACHO = cboClienteDespachar.SelectedItem.Value;
                string DESPACHO_SUCURSAL = "001";
                string CO_FACTURA = "";
                DateTime fechaent = Convert.ToDateTime(txtFechaDes.Text);
                string TIPO_CLIENTE = cboTipoCliente.SelectedItem.Value;
                string FECHA_ENTREGA = Convert.ToString(fechaent.ToShortDateString());
                string CLIENTE_CONDICION_PAGO = cboCondPago.SelectedItem.Value;
                string CLIENTE_NOTAS = txtComentariosSF.Text;
                string DIRECCION = txtDireccionDesp.Text;
                string MOTIVO = cboMotivo.SelectedItem.Text;
                string CENTRO_OPERACION = cboCentOpe.SelectedItem.Value;
                int NUM_DIAS = Convert.ToInt32(txtDias.Text);
                string NUMERO_PV = lblnumeropv.Text;
                string UNIDAD_NEGOCIO = "002";
                string PAIS_CLIENTE_FACTURA = cboPaisFactura.SelectedItem.Value;
                string PAIS_CLIENTE_DESPACHO = cboPaiDesp.SelectedItem.Value;
                string DPTO_CLIENTE_FACTURA = cboDepfact.SelectedItem.Value;
                string DPTO_CLIENTE_DESPACHO = cboDepdesp.SelectedItem.Value;
                string CIUDAD_CLIENTE_FACTURA = cboCiuFact.SelectedItem.Value;
                string CIUDAD_CLIENTE_DESPACHO = cboCiuDesp.SelectedItem.Value;
                string USUARIO = (string)Session["Nombre_Usuario"];
                string FECHA = DateTime.Now.ToString("dd/MM/yyyy");

                ValoresVacios();

                int IngSF = controlsf.IngSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), Convert.ToInt32(cboParte.SelectedItem.Value),
                    Nombre, lblClienteprincipal.Text, cboClienteFacturar.SelectedItem.Text, lblObraPrincipal.Text, cboDirector.SelectedItem.Text,
                    cboGerente.SelectedItem.Text, lblnumeropv.Text, Convert.ToInt32(cboTDN.SelectedItem.Value), cboCondPago.SelectedItem.Text,
                    lblvlrventa.Text.Replace(",", ""), txtValorflete.Text.Replace(",", ""), lblIVA.Text.Replace(",", ""), lblValorTotalVenta.Text.Replace(",", ""), txtValorComercial.Text.Replace(",", ""), cboFormaPago.SelectedItem.Text,
                    cboInsPago.SelectedItem.Text, Convert.ToInt32(coutas), txtComentariosSF.Text, txtDireccionDesp.Text, prod, txtDscto.Text,
                    lblValorDscto.Text.Replace(",", ""), txtRazonDescto.Text, cboClienteDespachar.SelectedItem.Text, VrAlum.Text.Replace(",", ""), LPlast.Text.Replace(",", ""), LAcero.Text.Replace(",", ""), FECHA,
                    Convert.ToInt32(SOLCLI), FACTURA_SUCURSAL, DESPACHO_SUCURSAL, CO_FACTURA, TIPO_CLIENTE, FECHA_ENTREGA, CLIENTE_MONEDA, VENDEDOR,
                    CONTACTO, MOTIVO, CENTRO_OPERACION, Convert.ToInt32(NUM_DIAS), UNIDAD_NEGOCIO, PAIS_CLIENTE_FACTURA, DPTO_CLIENTE_FACTURA, CIUDAD_CLIENTE_FACTURA,
                    PAIS_CLIENTE_DESPACHO, DPTO_CLIENTE_DESPACHO, CIUDAD_CLIENTE_DESPACHO, CLIENTE_CONDICION_PAGO, CLIENTE_FACTURA, CLIENTE_DESPACHO);

                string mensaje = "Solicitud de fabricación ingresada con éxito.";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            consultarConfirmSF();
        }

        protected  int validarDatos()
        {
            int validacion = 0;

            if (cboClienteFacturar.SelectedItem.Value == "0" ||
                        cboClienteDespachar.SelectedItem.Value == "0" ||
                        txtFechaDes.Text == "" ||
                        cboCondPago.SelectedItem.Value == "0" ||
                        txtDireccionDesp.Text == "0" ||
                        cboMotivo.SelectedItem.Text == "0" ||
                        cboCentOpe.SelectedItem.Value == "0" ||
                        txtDias.Text == "0" ||
                        cboPaisFactura.SelectedItem.Value == "0" ||
                        cboPaiDesp.SelectedItem.Value == "0" ||
                        cboDepfact.SelectedItem.Value == "0" ||
                        cboDepdesp.SelectedItem.Value == "0" ||
                        cboCiuFact.SelectedItem.Value == "0" ||
                        cboCiuDesp.SelectedItem.Value == "0")
            {
                string mensaje;
                mensaje = "Seleccione toda la información de datos de venta";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                return validacion;
            }
            else
            {
                if (cboDirector.SelectedItem.Value == "0" ||
                cboGerente.SelectedItem.Value == "0" ||
                cboInsPago.SelectedItem.Value == "0" ||
                cboFormaPago.SelectedItem.Value == "0" ||
                cboTDN.SelectedItem.Value == "0")
                {
                    string mensaje;
                    mensaje = "Seleccione toda la información de datos de facturación";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    return validacion;
                }
                else
                {
                    if (txtValorComercial.Text == "0" ||
                    txtValorComercial.Text == "" ||
                    cboInsPago.SelectedItem.Value == "0" ||
                    txtValorflete.Text == "")
                    {
                        string mensaje;
                        mensaje = "Verifique toda la información de valores de venta";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        return validacion;
                    }
                    else
                    {
                        if (Convert.ToDecimal(txtDscto.Text) > 0 && txtRazonDescto.Text=="")
                        {
                            string mensaje;
                            mensaje = "Digite la razon del descuento";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            return validacion;
                        }
                        else
                        {
                            if (txtDireccionDesp.Text == "")
                            {
                                string mensaje;
                                mensaje = "Digite la dirección de despacho";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                return validacion;
                            }
                            else
                            {
                                validacion = 1;
                                return validacion;
                            }
                        }
                    }                    

                }

            }
        }

        private void guardarActualizarSolicitudFac( string bandera)
        {
            reader = controlsf.ConsultarDatosClienteFact(cboClienteFacturar.SelectedItem.Value);
            
            string CLIENTE_MONEDA = "";
            string VENDEDOR = "";
            string CONTACTO = "";

            if (reader != null)
            {
                while (reader.Read())
                {
                    CLIENTE_MONEDA = reader.GetString(1).ToString();
                    VENDEDOR = reader.GetString(2).ToString();
                    CONTACTO = reader.GetInt32(3).ToString();
                }
            }
            reader.Close();

            int FUP = Convert.ToInt32(lblfup.Text);

            //capturo datos para pedido venta
            string CLIENTE_FACTURA = cboClienteFacturar.SelectedItem.Value;
            string FACTURA_SUCURSAL = "001";
            string CLIENTE_DESPACHO = cboClienteDespachar.SelectedItem.Value;
            string DESPACHO_SUCURSAL = "001";
            string @CO_FACTURA = "";
            DateTime fechaent = Convert.ToDateTime(txtFechaDes.Text);
            string TIPO_CLIENTE = cboTipoCliente.SelectedItem.Value;
            string FECHA_ENTREGA = Convert.ToString(fechaent.ToShortDateString());
            string CLIENTE_CONDICION_PAGO = cboCondPago.SelectedItem.Value;
            string CLIENTE_NOTAS = txtComentariosSF.Text;
            string DIRECCION = txtDireccionDesp.Text;
            string MOTIVO = cboMotivo.SelectedItem.Text;
            string CENTRO_OPERACION = cboCentOpe.SelectedItem.Value;
            int NUM_DIAS = Convert.ToInt32(txtDias.Text);
            string COTIZACION = "";
            string NUMERO_PV = lblnumeropv.Text;
            string UNIDAD_NEGOCIO = "002";
            string PAIS_CLIENTE_FACTURA = cboPaisFactura.SelectedItem.Value;
            string PAIS_CLIENTE_DESPACHO = cboPaiDesp.SelectedItem.Value;
            string DPTO_CLIENTE_FACTURA = cboDepfact.SelectedItem.Value;
            string DPTO_CLIENTE_DESPACHO = cboDepdesp.SelectedItem.Value;
            string CIUDAD_CLIENTE_FACTURA = cboCiuFact.SelectedItem.Value;
            string CIUDAD_CLIENTE_DESPACHO = cboCiuDesp.SelectedItem.Value;
            string USUARIO = (string)Session["Nombre_Usuario"];
            string @FECHA = DateTime.Now.ToString("dd/MM/yyyy");

            //capturo datos para solicitud facturacion
            string sfcliente=cboClienteFacturar.SelectedItem.Text;
            string sfdirector= cboDirector.SelectedItem.Text;
            int sftermineg= Convert.ToInt32(cboTDN.SelectedItem.Value);
            string sfcondpago = cboCondPago.SelectedItem.Text;
            decimal sfvlrventa = Convert.ToDecimal(lblvlrventa.Text);
            decimal sftransporte = Convert.ToDecimal(txtValorflete.Text);
            decimal sfiva = Convert.ToDecimal(lblIVA.Text);
            decimal sftotalventa = Convert.ToDecimal(lblValorTotalVenta.Text);
            decimal sfvalorcomercial = Convert.ToDecimal(txtValorComercial.Text);
            string sfformapago = cboFormaPago.SelectedItem.Text;
            string sfintrumento = cboInsPago.SelectedItem.Text;
            string sfcomentarios = txtComentariosSF.Text;
            string sfdireccion = txtDireccionDesp.Text;
            string sfporcdecto = txtDscto.Text;
            string sfrazondscto = txtRazonDescto.Text;
            string Gerente = cboGerente.SelectedItem.Text;
            decimal sfvalorDscto =Convert.ToDecimal( lblValorDscto.Text);

            //verifico que si se guarda o actuliza
            if (bandera == "Guardar")
            {

                int insertpv = controlsf.InsertarPv(FUP, CLIENTE_FACTURA, FACTURA_SUCURSAL, CLIENTE_DESPACHO,
                 DESPACHO_SUCURSAL, @CO_FACTURA, TIPO_CLIENTE, FECHA_ENTREGA, CLIENTE_MONEDA, CLIENTE_CONDICION_PAGO,
                 CLIENTE_NOTAS, VENDEDOR, CONTACTO, DIRECCION, MOTIVO, CENTRO_OPERACION, NUM_DIAS, COTIZACION,
                 NUMERO_PV, UNIDAD_NEGOCIO, PAIS_CLIENTE_FACTURA, PAIS_CLIENTE_DESPACHO, DPTO_CLIENTE_FACTURA,
                 DPTO_CLIENTE_DESPACHO, CIUDAD_CLIENTE_FACTURA, CIUDAD_CLIENTE_DESPACHO, USUARIO, @FECHA);

                int insertsf = controlsf.InsertarSF(sfcliente, sfcliente, sfdirector, Gerente,
                lblfup.Text, NUMERO_PV, sftermineg, sfcondpago, sfvlrventa, sftransporte, sfiva, sftotalventa, sfvalorcomercial,
                sfformapago, sfintrumento, sfcomentarios, FUP, sfdireccion,
                sfporcdecto, sfrazondscto, USUARIO, @FECHA, sfvalorDscto);

                if (insertpv != -1 && insertsf != -1)
                {
                    string mensaje;
                    string motivo = "Ingreso";
                    mensaje = "Solicicitud de Facturación Ingresada Exitosamente, puede ingresar la cuota";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    btnGuardarsf.Text = "Actualizar";
                    btnGenerarSF.Enabled = true;
                    btnconfsf.Enabled = true;

                    this.EnviarCorreoSF(motivo);

                    if (Convert.ToDecimal(txtDscto.Text) > 10)
                    {
                        this.EnviarCorreoPorcentaje();
                    }
                    string logTipo = "Solicitud De Facturacion";
                    int insertar = controlsf.IngresarDatosLOGpv(Convert.ToInt32(lblfup.Text), 0, 0, " ", " ", logTipo, USUARIO);
                }
                else
                {
                    string mensaje;
                    mensaje = "No fue posible realizar el ingreso";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
            else 
            {
                if (bandera == "Actualizar")
                {
                    int actualizarpv = controlsf.ActualizarPv(FUP, CLIENTE_FACTURA, FACTURA_SUCURSAL, CLIENTE_DESPACHO,
                     DESPACHO_SUCURSAL, @CO_FACTURA, TIPO_CLIENTE, FECHA_ENTREGA, CLIENTE_MONEDA, CLIENTE_CONDICION_PAGO,
                     CLIENTE_NOTAS, VENDEDOR, CONTACTO, DIRECCION, MOTIVO, CENTRO_OPERACION, NUM_DIAS, COTIZACION,
                     NUMERO_PV, UNIDAD_NEGOCIO, PAIS_CLIENTE_FACTURA, PAIS_CLIENTE_DESPACHO, DPTO_CLIENTE_FACTURA,
                     DPTO_CLIENTE_DESPACHO, CIUDAD_CLIENTE_FACTURA, CIUDAD_CLIENTE_DESPACHO, USUARIO, @FECHA);

                    int actualizarsf = controlsf.ActualizarSF(sfcliente, sfcliente, sfdirector, Gerente,
                    lblfup.Text, NUMERO_PV, sftermineg, sfcondpago, sfvlrventa, sftransporte, sfiva, sftotalventa, 
                    sfvalorcomercial,sfformapago, sfintrumento, sfcomentarios, FUP, sfdireccion,
                    sfporcdecto, sfrazondscto, USUARIO, @FECHA, sfvalorDscto);

                    if (actualizarpv != -1 && actualizarsf != -1)
                    {
                        string mensaje;
                        string motivo="Actualizacion";

                        mensaje = "Solicicitud de facturación actualizada Exitosamente, puede ingresar la cuota";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        btnGuardarsf.Text = "Actualizar";
                        btnGenerarSF.Enabled = true;
                        btnconfsf.Enabled = true;

                        this.EnviarCorreoSF(motivo);

                        if (Convert.ToDecimal(txtDscto.Text) > 10)
                        {
                            this.EnviarCorreoPorcentaje();
                        }
                        string logTipo = "Actualizacion SF";
                        int insertar = controlsf.IngresarDatosLOGpv(Convert.ToInt32(lblfup.Text), 0, 0, " ", " ", logTipo, USUARIO);
                    }
                    else
                    {
                        string mensaje;
                        mensaje = "No fue posible realizar la actualizacion";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
 
                }
                    
            }
            
        }
      
        private void EnviarCorreoSF(string motivo)
        {
            int paiscli=Convert.ToInt32(lblpais.Text);
            string mensaje = "";
            string acEmail = "";

            //OBTENGO EL ASESOR COMERCIAL
            reader = controlsf.ObtenerAsesorComercial(paiscli);
            
            
            if (reader.Read() == false)
            {
                mensaje = "el pais seleccionado no tiene agente asociado.";
            }
            else
            {
                reader = controlsf.ObtenerAsesorComercial(paiscli);
                while (reader.Read())
                {
                    if (acEmail == "")
                    {
                        acEmail = reader.GetValue(2).ToString();
                        Session["acEmail"] = acEmail;
                    }
                    else
                    {
                        acEmail = acEmail + "," + reader.GetValue(2).ToString();
                        Session["acEmail"] = acEmail;
                    }
                }

                string sujeto = "", solucion, cuerpo, copia = "";
                string rcEmail = lblCorreousu.Text;
                string EmailAse = (string)Session["acEmail"];
                string USUARIO = (string)Session["Nombre_Usuario"];

                //ENVIO DE CORREO
                //Definimos la clase MailMessage
                MailMessage mail = new MailMessage();
                //Indicamos Email De Origen
                mail.From = new MailAddress(rcEmail);
                //Añadimos la direccion correo del  destinatario
                mail.To.Add(EmailAse);
                //Añadimos la direccion correo de copia del destinatario
                mail.CC.Add(rcEmail);
                //Añadimos la direccion correo de copia al administrador
                mail.Bcc.Add("ivanvidal@forsa.com.co,andressuarez@forsa.com.co,arlexcardona@forsa.com.co");
                //INCLUIMOS EL ASUNTO DEL MENSAJE
                sujeto = "PV - "+motivo +" Solicitud De Facturación. FUP No. " + lblfup.Text + " PV No. "+lblnumeropv.Text;
                mail.Subject = sujeto;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                solucion = "Representante. " + USUARIO + "\n" + "FUP No. " + lblfup.Text + " \n" + "Sitio Web CRM. http://app.forsa.com.co/comercial/Inicio.aspx";
                cuerpo = "Nos complace informarle que el Agente Comercial " + USUARIO + "" +
                " realizo "+motivo+" de Solicitud de Facturación correspondiente al Formato Único de Proyectos(FUP)" + lblfup.Text +
                " información referente al cliente " + lblClienteprincipal.Text + "\n\n" +
                " proyecto " + lblObraPrincipal.Text + " " + "\n\n" + "Señores Gerentes Comerciales recuerden ingresar al CRM a Generar " +
                " la Solicitud de Facturación." + "\n\n\n" +
                "Cordialmente, " + "\n\n" + "Gestión Informática.";
                mail.Body = solucion + "\n\n" + cuerpo;
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = false;
                //DECLARAMOS LA CLASE SMTPCLIENT
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                //smtp.Host = "mail.forsa.com.co";
                ////INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                //smtp.Credentials = new System.Net.NetworkCredential("comercial@forsa.com.co", "c4l0t0.com9364");
                //smtp.Port = 25;
                //smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
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

        protected void txtDias_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool result = IsNumeric(txtDias.Text);

            if (txtDias.Text == "" || result == false)
            {
                mensaje = "Digíte el número de días correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtDias.Text = "0";
            }
            else
            {
                string fecha_acc = DateTime.Now.ToString("dd/MM/yyyy");
                DateTime fechaFinal = Convert.ToDateTime(fecha_acc);
                DateTime sumaFecha = fechaFinal.AddDays(Convert.ToDouble(txtDias.Text));
                txtFechaDes.Text = Convert.ToString(sumaFecha.ToShortDateString());
            }
        }

        protected void txtFechaDes_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            string FechaActual = DateTime.Now.ToString("dd/MM/yyyy");
            TimeSpan FechaFin;
            DateTime FechaIni = Convert.ToDateTime(txtFechaDes.Text);
            
            bool fechacomp = IsDatet(txtFechaDes.Text);

            if (fechacomp != true)
            {
                mensaje = "Digite la fecha correctamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtFechaDes.Text = "";
            }
            else
            {
                FechaFin = FechaIni.Subtract(Convert.ToDateTime(FechaActual));
                int dias = FechaFin.Days;
                txtDias.Text = Convert.ToString(dias);
            }
        }

        private void EnviarCorreoPorcentaje()
        {
            string Email = "";
            string USUARIO = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            string pais = (string)Session["paisnombre"];

            reader = controlsf.ObtenerMailPorcentaje("n_Rol", "p_Rol");
            while (reader.Read())
            {
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

            decimal ValorPorcentaje = Convert.ToDecimal(txtDscto.Text);
            txtDscto.Text = Convert.ToString(ValorPorcentaje.ToString("#,##.##"));

            string sujeto, solucion, cuerpo;
            string rcEmail = lblCorreousu.Text;
            string EmailCita = (string)Session["acEmail"];
            //DEFINIMOS LA CLASE DE MAILMESSAGE
            MailMessage mail = new MailMessage();
            //INDICAMOS EL EMAIL DE ORIGEN
            mail.From = new MailAddress(rcEmail);
            //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
            mail.To.Add(EmailCita);
            //AÑADIMOS COPIA AL REPRESENTANTE
            mail.CC.Add(rcEmail);
            //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
            mail.Bcc.Add("andressuarez@forsa.com.co, ivanvidal@forsa.com.co");
            //INCLUIMOS EL ASUNTO DEL MENSAJE
            sujeto = "PV - Ingreso De Descuento Mayor Al 10%. Proyecto" + lblObraPrincipal.Text + " PV No. " + lblnumeropv.Text + " ";
            mail.Subject = sujeto;
            //AÑADIMOS EL CUERPO DEL MENSAJE
            solucion = "Representante. " + USUARIO + "\n" + "FUP No. " + lblfup.Text + " \n" + "SitioWeb CRM. http://app.forsa.com.co/comercial/Inicio.aspx"
                    + " \n" + "Porcentaje De Descuento Ingresado " + txtDscto.Text + "%";
            cuerpo = "Por medio de la presente se informa que se ha ingresado un descuento superior al 10% permitido correspondiente al Proyecto " + lblObraPrincipal.Text + ", " +
            " Cliente " + lblClienteprincipal.Text + " Pais " + pais + " \n\n\n" +            
            "Cordialmente, " + "\n\n" + "Gestión Informática.";
            mail.Body = solucion + "\n\n" + cuerpo;
            //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            //DEFINIMOS LA PRIORIDAD DEL MENSAJE
            mail.Priority = System.Net.Mail.MailPriority.Normal;
            //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
            mail.IsBodyHtml = false;
            //DECLARAMOS LA CLASE SMTPCLIENT
            SmtpClient smtp = new SmtpClient();
            //DEFINIMOS NUESTRO SERVIDOR SMTP
            //smtp.Host = "mail.forsa.com.co";
            ////INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
            //smtp.Credentials = new System.Net.NetworkCredential("comercial@forsa.com.co", "c4l0t0.com9364");
            //smtp.Port = 25;
            //smtp.EnableSsl = true;

            ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
            {
                return true;
            };
            try
            {
                smtp.Send(mail);
            }
            catch (Exception ex)
            {
                mensaje = "ERROR: " + ex.Message;
            }
        }
        
        //consultar la confirmacion de la sf
        private void consultarConfirmSF()
        {
            string mensaje;
            reader = controlsf.ConsultarConfirmacionSF(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Value));
            if (reader.Read() == false)
            {
                mensaje = "no posee sf.";
            }
            else
            {
                bool confSF = reader.GetSqlBoolean(0).Value;
                reader.Close();

                if (confSF == true)
                {
                    lblMensaje.Text = "Estado: SF-Confirmada.";
                    btnconfsf.Enabled = false;
                    btnGuardarsf.Enabled = false;
                    chkQuitarConfirsf.Enabled = true;
                    chkQuitarConfirsf.Checked = false;
                }
                else
                {
                    chkQuitarConfirsf.Enabled = false;
                    btnconfsf.Enabled = true;
                }

                
                int arRol = (int)Session["Rol"];
                if ((arRol == 15))
                {
                    btnRechazar.Visible = true;
                    pnlRechazoSf.Enabled = true;
                }

                this.consultarRechazoSf();
                Panel1.Enabled = true;
            }
        }

        public void consultarRechazoSf()
        {
            string mensaje="";
             reader = controlsf.ConsultarRechazoSf(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");

            if (reader.Read() == false)
            {
                mensaje = "no posee rechazo.";
            }
            else
            {
                chkRazonsocialRech.Checked= reader.GetSqlBoolean(1).Value;
                chkNitRech.Checked= reader.GetSqlBoolean(2).Value;
                chkDirecFiscalRech.Checked= reader.GetSqlBoolean(3).Value;
                chkTelefRech.Checked= reader.GetSqlBoolean(4).Value;
                chkCondPagoRech.Checked= reader.GetSqlBoolean(5).Value;
                chkTermNegRech.Checked= reader.GetSqlBoolean(6).Value;
                chkValorComercialRech.Checked= reader.GetSqlBoolean(7).Value;
                txtComentRecha.Text=reader.GetSqlString(8).Value;
            }                        
        }

        protected void btnconfsf_Click(object sender, EventArgs e)
        {
            string USUARIO = (string)Session["Nombre_Usuario"];
            int actualizar = controlsf.actualizarConfirmarSFAgente(USUARIO, Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Value));

            if (actualizar != -1)
            {
                string mensaje = "Solicitud de facturación confirmada exitosamente";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                
                this.consultarConfirmSF();
                string logTipo = "Confirma SF";
                //int insertar = controlsf.IngresarDatosLOGpv(Convert.ToInt32(lblfup.Text), 0, 0, " ", " ", logTipo, USUARIO);
                this.EnviarCorreoCierreSF();     
            }
            else
            {
                string mensaje = "No fue posible ingresar la confirmación";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }
        }

        private void EnviarCorreoCierreSF()
        {
            string mensaje;
            string USUARIO = (string)Session["Nombre_Usuario"];
            //OBTENGO EL ASESOR COMERCIAL
            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(lblpais.Text));
            string Email = "";
            if (reader != null)
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
                
                reader.Close();
                
                string sujeto, solucion, cuerpo;
                string rcEmail = lblCorreousu.Text;
                string EmailAse = (string)Session["acEmail"];
                //DEFINIMOS LA CLASE DE MAILMESSAGE
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                mail.To.Add(EmailAse);
                //AÑADIMOS COPIA AL REPRESENTANTE
                mail.CC.Add(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
                mail.Bcc.Add("andressuarez@forsa.com.co, ivanvidal@forsa.com.co");
                //INCLUIMOS EL ASUNTO DEL MENSAJE
                sujeto = "PV - Confirmación Solicitud De Facturación. FUP No. " + lblfup.Text + " Pedido De Venta No. " + lblnumeropv.Text + " ";
                mail.Subject = sujeto;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                solucion = "Representante. " + USUARIO + "\n" + "FUP No. " + lblfup.Text + " \n" + "Sitio Web CRM. http://app.forsa.com.co/comercial/Inicio.aspx";
                cuerpo = "Nos complace informarle que el Agente Comercial " + USUARIO + "" +
                " Confirmo la Solicitud de Facturación correspondiente al Formato Único de Proyectos(FUP)" + lblfup.Text +
                " información referente al cliente " + lblClienteprincipal.Text + "\n\n" +
                " proyecto " + lblObraPrincipal.Text + " " + "\n\n" + "Señores Gerentes Comerciales recuerden ingresar al CRM a Confirmar " +
                " El Pedido De Venta." + "\n\n\n" +
                "Cordialmente, " + "\n\n" + "Gestión Informática.";
                mail.Body = solucion + "\n\n" + cuerpo;
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = false;
                //DECLARAMOS LA CLASE SMTPCLIENT
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                //smtp.Host = "mail.forsa.com.co";
                ////INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                //smtp.Credentials = new System.Net.NetworkCredential("comercial@forsa.com.co", "c4l0t0.com9364");
                //smtp.Port = 25;
                //smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
                try
                {
                    smtp.Send(mail);
                }
                catch (Exception ex)
                {
                    mensaje= "ERROR: " + ex.Message;
                }
            }
        }

        protected void chkQuitarConfirsf_CheckedChanged(object sender, EventArgs e)
        {
            this.quitarconfirmacion();
        }

        private void quitarconfirmacion()
        {
            string USUARIO = (string)Session["Nombre_Usuario"];
            int actualizar = controlsf.desconfirmarSF(USUARIO, Convert.ToInt32(lblfup.Text), LVer.Text.Trim(),
                Convert.ToInt32(cboParte.SelectedItem.Value));

            if (actualizar != -1)
            {              
                this.consultarConfirmSF();
                string logTipo = "Desconfirma SF";
                //int insertar = controlsf.IngresarDatosLOGpv(Convert.ToInt32(lblfup.Text), 0, 0, " ", " ", logTipo, USUARIO);
                chkQuitarConfirsf.Enabled = false;
                btnGuardarsf.Enabled = true;
                lblMensaje.Text = "Estado: SF-Ingresada.";
                string mensaje = "Se ha quitado la confirmación de la SF";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                string mensaje = "No fue posible quitar la confirmación de la SF";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            } 
        }

        protected void btnGuardarCuota_Click(object sender, EventArgs e)
        {
            string mensaje;
            string USUARIO = (string)Session["Nombre_Usuario"];


            if (txtporcpagar.Text == "0" || txtaPagar.Text == "0" || txtporcpagar.Text == "" || txtaPagar.Text == "")
            {
                mensaje = "Digite el valor de la cuota";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

            }
            else
            {
                int contador = (int)Session["contador"];

                contador = contador + 1;

                reader = controlsf.ConsultarSF(Convert.ToInt32(lblfup.Text.Trim()), LVer.Text.Trim());
                reader.Read();
                string numsf = reader.GetValue(0).ToString();
                reader.Close();

                if (btnGuardarCuota.Text == "Guardar")
                {
                    decimal suma = 0;
                    reader = controlsf.ObtenerSumaPorcentaje(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");
                    if (reader.Read() == false)
                    {
                        suma = 0;
                    }
                    else
                    {
                        suma = reader.GetDecimal(0);
                    }
                    suma = suma + Convert.ToDecimal(txtporcpagar.Text);

                    if (suma > 100)
                    {
                    mensaje = "El valor de las cuotas supera el valor de la venta";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        Session["contador"] = contador;
                        int insertado = controlsf.IngresarDatosCuota(Convert.ToInt32(lblfup.Text), contador, txtaPagar.Text, txtpagado.Text, txtporcpagar.Text, txtFechaReal.Text,
                        "Debe", txtComentCuota.Text, numsf, USUARIO, LVer.Text.Trim(), Convert.ToInt32(cboParte.SelectedItem.Value));

                        if (insertado != -1)
                        {
                            mensaje = "Cuota ingresada exitosamente";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                    }
                }
                    else
                    {
                        if (btnGuardarCuota.Text == "Actualizar")
                        {
                            decimal suma = 0;
                            reader = controlsf.ObtenerSumaPorcentaje(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");
                            if (reader.Read() == false)
                            {
                                suma = 0;
                            }
                            else
                            {
                                suma = reader.GetDecimal(0);
                            }
                            reader.Close();

                            decimal sumacuota = 0;
                            reader = controlsf.ObtenerPorcentajecuota(Convert.ToInt32(lblfup.Text),Convert.ToInt32( cboCuota.Text), "n_Rol", "p_Rol");
                            reader.Read();
                            sumacuota = reader.GetDecimal(0);
                            reader.Close();

                            suma = (suma + Convert.ToDecimal(txtporcpagar.Text))-sumacuota;

                            if (suma > 100)
                            {
                                mensaje = "El valor de las cuotas supera el valor de la venta";
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }
                            else 
                            {
                                int actualizar = controlsf.actualizarCuota(Convert.ToInt32(lblfup.Text), Convert.ToInt32(cboCuota.SelectedItem.Value), txtaPagar.Text, txtporcpagar.Text, txtpagado.Text, txtFechaReal.Text,
                                    "Pendiente", txtComentCuota.Text, USUARIO);

                                if (actualizar != -1)
                                {
                                    mensaje = "Cuota actualizada exitosamente";
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                } 
                            }
                        }
                        
                    }


                    string sum;

                    reader = controlsf.ObtenerSumaCuota(Convert.ToInt32(lblfup.Text));
                    if (reader.Read() == false)
                    {
                        sum = "0";
                    }
                    else
                    {
                        sum = reader.GetValue(0).ToString();
                    }

                    string moneda, simbolo = "";
                    reader = controlsf.ObtnerMonedaFup(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");
                    reader.Read();
                    moneda = reader.GetValue(1).ToString();
                    simbolo = reader.GetValue(2).ToString();
                    reader.Close();

                    decimal valorSaldo = Convert.ToDecimal(lblValorTotalVenta.Text) - Convert.ToDecimal(sum);
                                       
                    lblsaldocuota.Text = simbolo + Convert.ToString(valorSaldo) + " " + moneda;
                    
                    CargarGrillaCuota();

                    this.limpiarCuotaLF();
                    this.poblarCuota();
                   
                }
            
        }

        private void CargarGrillaCuota()
        {
            ds.Reset();
            ds = controlsf.cuotaAcc(Convert.ToInt32(lblfup.Text), LVer.Text.Trim());
            if (ds != null)
            {
                GridView1.DataSource = ds.Tables[0];
                GridView1.DataBind();
                GridView1.Visible = true;
            }
            else
            {
                GridView1.Dispose();
                GridView1.Visible = false;
            }
            ds.Reset();
        }

        public void limpiarCuotaLF()
        {
            txtaPagar.Text = "0";
            txtporcpagar.Text = "0";
            txtpagado.Text = "0";
            txtFechaReal.Text = DateTime.Now.ToShortDateString();
            txtComentCuota.Text = "Sin Comentarios";
        }

        protected void cboCuota_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.consultarCuota();
        }

        private void consultarCuota()
        {
            reader = controlsf.ConsultarCuota(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), 
                Convert.ToInt32(cboCuota.SelectedItem.Value));
            reader.Read();

            txtaPagar.Text = reader.GetValue(1).ToString();
            txtporcpagar.Text = reader.GetValue(2).ToString();
            txtpagado.Text = reader.GetValue(3).ToString();
            txtFechaReal.Text = reader.GetDateTime(4).ToShortDateString();
            txtComentCuota.Text = reader.GetValue(6).ToString();
            btnGuardarCuota.Text = "Actualizar";

            reader.Close();
        }

        protected void txtaPagar_TextChanged(object sender, EventArgs e)
        {
            bool result = IsNumeric(txtaPagar.Text);

            string idioma = (string)Session["Idioma"];
            string mensaje = "";
            
            decimal total_hecho = Convert.ToDecimal(lblValorTotalVenta.Text);
            decimal cant_pagar = Convert.ToDecimal(txtaPagar.Text);

            if (txtaPagar.Text == "" || result == false)
            {
                if (idioma == "Español")
                {
                    mensaje = "Digite el valor de la cuota correctamente.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Enter the value of the quota correctly.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Digite o valor da quota corretamente.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                
                txtaPagar.Text = "0";
            }
            else
            {
                if (cant_pagar > total_hecho)
                {
                    if (idioma == "Español")
                    {
                        mensaje = "La cantidad a pagar no puede ser mayor al valor de la venta.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "The amount payable may not exceed the value of the sale.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "O montante a pagar não pode exceder o valor da venda.";
                    }                    
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtaPagar.Text = "0";

                }
                else
                {
                    decimal total_pagar = Convert.ToDecimal(txtaPagar.Text);
                    decimal valor_venta = Convert.ToDecimal(lblValorTotalVenta.Text);
                    decimal pr_pag = 0;

                    if (txtaPagar.Text != "0")
                    {
                        pr_pag = (total_pagar / valor_venta) * 100;
                        txtporcpagar.Text = Convert.ToString(pr_pag.ToString("#,##.##"));
                    }
                   
                }
            }
        }

        protected void txtporcpagar_TextChanged(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            bool result = IsNumeric(txtporcpagar.Text);

            decimal total_pagar = 0;
            decimal valor_venta = Convert.ToDecimal(lblValorTotalVenta.Text);
            decimal pr_pag = Convert.ToDecimal(txtporcpagar.Text) / 100;

            if (txtporcpagar.Text == "" || result == false)
            {
                if (idioma == "Español")
                {
                    mensaje = "Digite el valor del porcentaje correctamente.";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Enter the percentage value correctly.";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Digite o valor percentual corretamente.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtaPagar.Text = "0";
            }
            else 
            {
                if (txtporcpagar.Text != "0")
                {
                    total_pagar = valor_venta * pr_pag;
                    txtaPagar.Text = Convert.ToString(total_pagar.ToString("#,##.##"));
                }
            }
        }

        protected void txtpagado_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnEliminarCuota_Click(object sender, EventArgs e)
        {
            string mensaje="";

            if (cboCuota.Text != "---------------" || cboCuota.Text != "")
            {
                int eliminar = controlsf.EliminarCuota(Convert.ToInt32(lblfup.Text), Convert.ToInt32(cboCuota.SelectedItem.Value),
                    LVer.Text.Trim());

                if (eliminar != -1)
                {
                    mensaje = "Se ha eliminado la cuota";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }

                string sum;

                reader = controlsf.ObtenerSumaCuota(Convert.ToInt32(lblfup.Text));
                if (reader.Read() == false)
                {
                    sum = "0";
                }
                else
                {
                    sum = reader.GetValue(0).ToString();
                }

                string moneda, simbolo = "";
                reader = controlsf.ObtnerMonedaFup(Convert.ToInt32(lblfup.Text), "n_Rol", "p_Rol");
                reader.Read();
                moneda = reader.GetValue(1).ToString();
                simbolo = reader.GetValue(2).ToString();
                reader.Close();

                decimal valorSaldo = Convert.ToDecimal(lblValorTotalVenta.Text) - Convert.ToDecimal(sum);
                
                lblsaldocuota.Text = simbolo + Convert.ToString(valorSaldo.ToString("#,##.##")) + " " + moneda;

                this.limpiarCuotaLF();

                CargarGrillaCuota();

                this.limpiarCuotaLF();
                this.poblarCuota();
                btnGuardarCuota.Text = "Guardar";
            }
        }

        private void CorreoFinalVenta()
        {
            string fup = lblfup.Text;
            string mensaje="";
            string USUARIO = (string)Session["Nombre_Usuario"];
            string pais = (string)Session["paisnombre"];

            string fecha = System.DateTime.Today.ToShortDateString();

            Byte[] correo = new Byte[0];
            WebClient clienteWeb = new WebClient();
            clienteWeb.Dispose();
            clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "forsa", "mail.forsa.com.co");
            correo = clienteWeb.DownloadData(@"http://si.forsa.com.co:81/reportServer?/Comercial/COM_SolicitudFacturacionPrueba&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + fup + "");
            MemoryStream ms = new MemoryStream(correo);

            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(lblpais.Text));
            if (reader.Read() == false)
            {
                mensaje = "El pais no tiene asignado asesor comercial";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                string Email = "";
                reader = controlsf.ObtenerMailPV("n_Rol", "p_Rol");
                while (reader.Read())
                {
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

                string acEmail = "";
                reader = controlsf.ObtenerMailSF("n_Rol", "p_Rol");
                while (reader.Read())
                {
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

                string ano = DateTime.Now.ToString("yy");
                string sujeto, solucion, cuerpo;
                string rcEmail = lblCorreousu.Text;
                string EmailCita = (string)Session["acEmail"];
                string MailSF = (string)Session["Email"];
                string MailFin = EmailCita + "," + MailSF;

                //DEFINIMOS LA CLASE DE MAILMESSAGE
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                
                    mail.To.Add(MailFin);
                
                //AÑADIMOS COPIA AL REPRESENTANTE
                mail.CC.Add(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
                mail.Bcc.Add("andressuarez@forsa.com.co, ivanvidal@forsa.com.co");
                //INCLUIMOS EL ASUNTO DEL MENSAJE
                sujeto = "PV - Confirmación PV Accesorios. FUP No. " + lblfup.Text + " AC-" + ano + " PV No " + lblnumeropv.Text + " ";
                mail.Subject = sujeto;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                solucion = "FUP No. " + lblfup.Text + " AC-" + ano + " \n" + "SitioWeb CRM. http://app.forsa.com.co/comercial/Inicio.aspx";
                cuerpo = "El Pedido De Venta No. " + lblnumeropv.Text + " correspondiente al Proyecto: " + lblObraPrincipal.Text + ", " +
                " Cliente: " + lblClienteprincipal.Text + " Pais: " + pais + 
                " \n\n\n" + "HA SIDO CONFIRMADO. RECUERDE QUE NO PUEDE SER MODIFICADO." + " \n\n\n" +
                "Cordiamente, " + "\n\n" + USUARIO + " ";
                mail.Body = solucion + "\n\n" + cuerpo;
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
                //smtp.Host = "mail.forsa.com.co";
                ////INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                //smtp.Credentials = new System.Net.NetworkCredential("comercial@forsa.com.co", "c4l0t0.com9364");
                //smtp.Port = 25;
                //smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
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

        protected void btnRechazar_Click(object sender, EventArgs e)
        {
            string mensajerechazo = "Por favor revise los siquientes datos en la SF: \n\n ";
            bool razonsocial= chkRazonsocialRech.Checked;
            bool nit = chkNitRech.Checked;
            bool direccion = chkDirecFiscalRech.Checked;
            bool telefono = chkTelefRech.Checked;
            bool condpago = chkCondPagoRech.Checked;
            bool tdn = chkTermNegRech.Checked;
            bool vlrcomercial = chkValorComercialRech.Checked;
            string  observaciones = txtComentRecha.Text;
            string USUARIO = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            string srazonsocial = "", snit = "", sdireccion = "", stelefono = "", scondpago = "", stdn = "", svlrcomercial = "";

            if (razonsocial == false && nit == false && direccion == false && telefono == false && condpago == false &&
                tdn == false && vlrcomercial == false)
            {
                mensaje = "Debe seleccionar un motivo por el cual rechazar la SF ";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (razonsocial == true)
                {
                    mensajerechazo = mensajerechazo + "- Razón Social \n\n";
                }

                if (nit == true)
                {
                    mensajerechazo = mensajerechazo + "- NIT \n\n";
                }
                if (direccion == true)
                {
                    mensajerechazo = mensajerechazo + "- Dirección \n\n";
                }
                if (telefono == true)
                {
                    mensajerechazo = mensajerechazo + "- Teléfono \n\n";
                }

                if (condpago == true)
                {
                    mensajerechazo = mensajerechazo + "- Condición de Pago \n\n";
                }

                if (tdn == true)
                {
                    mensajerechazo = mensajerechazo + "- Termino de Negociación \n\n";
                }
                if (vlrcomercial == true)
                {
                    mensajerechazo = mensajerechazo + "- Valor Comercial \n\n";
                }


                //mensajerechazo = srazonsocial + snit + sdireccion + stelefono + scondpago + stdn + svlrcomercial + "- Observaciones : " + observaciones + " \n\n";

                int insertar = controlsf.IngresarRechazoSf(Convert.ToInt32(lblfup.Text), LVer.Text.Trim(), razonsocial, nit, direccion,
                    telefono, condpago, tdn, vlrcomercial, observaciones, USUARIO);

                if (insertar != -1)
                {
                    this.EnviarCorreRechazoSF(mensajerechazo);
                    mensaje = "Se ha registrado el rechazo de la información de la SF del PV " + lblnumeropv.Text;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    this.quitarconfirmacion();
                    this.btnRechazar.Enabled = false;
                }
            }
        }

        private void EnviarCorreRechazoSF(string mensaje)
        {
            string USUARIO = (string)Session["Nombre_Usuario"];
            //OBTENGO EL ASESOR COMERCIAL
            reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(lblpais.Text));
            string EmailAdmin = "";
            string Email = "";

            if (reader != null)
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

                reader.Close();

                //mail para administradores
                reader = contpv.ObtenerMailAdmin();
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
                reader.Close();

                string sujeto, solucion, cuerpo;
                string rcEmail = lblCorreousu.Text;
                string EmailAse = (string)Session["acEmail"];
                //DEFINIMOS LA CLASE DE MAILMESSAGE
                MailMessage mail = new MailMessage();
                //INDICAMOS EL EMAIL DE ORIGEN
                mail.From = new MailAddress(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
                mail.To.Add(EmailAse);
                //AÑADIMOS COPIA AL REPRESENTANTE
                mail.CC.Add(rcEmail);
                //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
                mail.Bcc.Add(EmailAdmin);
                //INCLUIMOS EL ASUNTO DEL MENSAJE
                sujeto = "PV - Rechazo de SF. FUP No. " + lblfup.Text + " Pedido De Venta No. " + lblnumeropv.Text + " ";
                mail.Subject = sujeto;
                //AÑADIMOS EL CUERPO DEL MENSAJE
                solucion = "El Area de Logistica ha rechazado la información de la solicitud de facturación del FUP No. " + lblfup.Text + " \n" + "SitioWeb CRM. http://app.forsa.com.co/comercial/Inicio.aspx";
                cuerpo = mensaje +
                " ------------------------------------- \n\n" +
                " Información referente al cliente " + lblClienteprincipal.Text + "\n\n" +
                " Proyecto " + lblObraPrincipal.Text + " " + "\n\n" + "Señores Comerciales deben diligenciar correctamente la información y confirmar nuevamente la SF. " +               
                "Cordialmente, " + "\n\n" + "Gestión Informática.";
                mail.Body = solucion + "\n\n" + cuerpo;
                //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
                mail.BodyEncoding = System.Text.Encoding.UTF8;
                //DEFINIMOS LA PRIORIDAD DEL MENSAJE
                mail.Priority = System.Net.Mail.MailPriority.Normal;
                //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
                mail.IsBodyHtml = false;
                //DECLARAMOS LA CLASE SMTPCLIENT
                SmtpClient smtp = new SmtpClient();
                //DEFINIMOS NUESTRO SERVIDOR SMTP
                //smtp.Host = "mail.forsa.com.co";
                ////INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
                //smtp.Credentials = new System.Net.NetworkCredential("comercial@forsa.com.co", "c4l0t0.com9364");
                //smtp.Port = 25;
                //smtp.EnableSsl = true;

                ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
                SslPolicyErrors sslPolicyErrors)
                {
                    return true;
                };
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

        private void CargarCartaSF()
        {
            string Rep = (string)Session["Nombre_Usuario"];
            string CorreoRep = (string)Session["rcEmail"];
            string fecha = System.DateTime.Today.ToLongDateString();

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", lblfup.Text, true));

            ReportViewer4.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "forsa", "mail.forsa.com.co");
            ReportViewer4.ServerReport.ReportServerCredentials = irsc;
            ReportViewer4.ServerReport.ReportServerUrl = new Uri("http://si.forsa.com.co:81/ReportServer");
            string bandera = (string)Session["Bandera"];
            if (bandera == "1")
            {
                parametro.Add(new ReportParameter("version", LVer.Text.Trim(), true));
                parametro.Add(new ReportParameter("parte", cboParte.SelectedItem.Text.Trim(), true));

                ReportViewer4.ServerReport.ReportPath = "/Comercial/COM_SolicitudFacturacionSeguimiento";

            }
            else
            {
                ReportViewer4.ServerReport.ReportPath = "/Comercial/COM_SolicitudFacturacionPrueba";

            }
            this.ReportViewer4.ServerReport.SetParameters(parametro);            
        }
        
        protected void btnGenerarSF_Click(object sender, EventArgs e)
        {
            this.CargarCartaSF();
            Accordion1.Visible = true;
        }

        protected void cboParte_SelectedIndexChanged(object sender, EventArgs e)
        {
            consultarPedidoVenta();
        }        
    }
}
