using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using CapaControl;

namespace SIO
{
    public partial class FacturaProforma : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlFacturaProforma contfactura = new ControlFacturaProforma();
        public ControlCliente concli = new ControlCliente();
        public ControlCotizacionPreliminar contcp = new ControlCotizacionPreliminar();
        private DataSet dsFacturaProforma = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int rol = (int)Session["Rol"];
                if ((rol == 3) || (rol == 28) || (rol == 29))
                {
                    this.PoblarListaPais();
                }
                else
                {
                    this.PoblarListaPais2();
                }
             
                this.Idioma();
                this.PoblarTDN();

                int year = System.DateTime.Now.Year;
                lblAno.Text = Convert.ToString(year).Substring(2);

                int bandera = 0;
                Session["Bandera"] = bandera;
            }
        }

        private void PoblarListaPais2()
        {           
            cboPais.Items.Clear();

            reader = contubi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {                
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add("Selecione O País");
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
            contubi.cerrarConexion();
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

            dsFacturaProforma = contfactura.ConsultarIdiomaFacturaProforma();

            foreach (DataRow fila in dsFacturaProforma.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    pnlCliente.GroupingText = fila[idiomaId].ToString();
                if (posicion == 2)
                    pnlFacturaProforma.GroupingText = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblPais.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblRazonSocial.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    lblObra.Text = fila[idiomaId].ToString();
                if (posicion == 7)
                    lblNit.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    lblDireccion.Text = fila[idiomaId].ToString();
                if (posicion == 9)
                    lblTelefono.Text = fila[idiomaId].ToString();
                if (posicion == 10)
                    lblFecha.Text = fila[idiomaId].ToString();
                if (posicion == 11)
                    pnlDetalleFactura.GroupingText = fila[idiomaId].ToString();
                if (posicion == 12)
                    lblCant.Text = fila[idiomaId].ToString();
                if (posicion == 13)
                    lblDescrip.Text = fila[idiomaId].ToString();
                if (posicion == 14)
                    lblPrecio.Text = fila[idiomaId].ToString();
                if (posicion == 15)
                    lblTotalUnitario.Text = fila[idiomaId].ToString();              
                if (posicion == 16)
                    pnlCondiciones.GroupingText = fila[idiomaId].ToString();
                if (posicion == 17)
                    lblSubPartida.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    chkDesc.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    lblValorDescuento.Text = fila[idiomaId].ToString();
                if (posicion == 20)
                    pnlNotas.GroupingText = fila[idiomaId].ToString();
                if (posicion == 21)
                    pnlPrecio.GroupingText = fila[idiomaId].ToString();
                if (posicion == 22)
                    btnAdicionar.Text = fila[idiomaId].ToString();
                if (posicion == 23)
                    btnGuardar.Text = fila[idiomaId].ToString();
                if (posicion == 24)
                    btnNuevo.Text = fila[idiomaId].ToString();
                if (posicion == 25)
                    lblPorcDesc.Text = fila[idiomaId].ToString();
            }
            dsFacturaProforma.Tables.Remove("Table");
            dsFacturaProforma.Dispose();
            dsFacturaProforma.Clear();
        }    
                
        private void PoblarListaPais()
        {
            string rcID = (string)Session["rcID"];
            cboPais.Items.Clear();

            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                cboPais.Items.Add("Selecione O País");
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
            contubi.cerrarConexion();
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudad();
        }

        private void PoblarCiudad()
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];

            cboCiudad.Items.Clear();
            if (idioma == "Español")
            {
                cboCiudad.Items.Add("Seleccione La Ciudad");
            }
            if (idioma == "Ingles")
            {
                cboCiudad.Items.Add("Select The City");
            }
            if (idioma == "Portugues")
            {
                cboCiudad.Items.Add("Selecione A Cidade");
            }

            if ((rol == 3) && (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
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
            }
        }

        protected void cboCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.poblarListaCliente();
        }

        //CARGAMOS LOS CLIENTES DE LA CIUDAD SELECCIONADA
        private void poblarListaCliente()
        {
            string idioma = (string)Session["Idioma"];

            cboCliente.Items.Clear();
            reader = concli.ConsultarDatosCliente(Convert.ToInt32(cboCiudad.SelectedItem.Value),0);
            if (idioma == "Español")
            {
                cboCliente.Items.Add("Seleccione La Empresa");
            }
            if (idioma == "Ingles")
            {
                cboCliente.Items.Add("Select Company");
            }
            if (idioma == "Portugues")
            {
                cboCliente.Items.Add("Selecione Companhia");
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
        }

        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarObra();
        }

        private void PoblarObra()
        {
            string idioma = (string)Session["Idioma"];

            if (cboCliente.SelectedItem.Text == "Seleccione La Empresa" || cboCliente.SelectedItem.Text == "Select Company" ||
                cboCliente.SelectedItem.Text == "Selecione Companhia")
            {
                cboObra.Items.Clear();
            }
            else
            {
                cboObra.Items.Clear();
                reader = contcp.ObtenerObra(Convert.ToInt32(cboCliente.SelectedValue));
                
                if (idioma == "Español")
                {
                    cboObra.Items.Add("Seleccione La Obra");
                }
                if (idioma == "Ingles")
                {
                    cboObra.Items.Add("Select The Work");
                }
                if (idioma == "Portugues")
                {
                    cboObra.Items.Add("Selecione A Obra");
                }
                
                while (reader.Read())
                {
                    cboObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));

                }
                reader.Close();
                contcp.cerrarConexion();
            }
        }

        private void PoblarTDN()
        {
            cboTDN.Items.Clear();
            reader = concli.ConsultarTDN();
            cboTDN.Items.Add("Elija El TDN");

            while (reader.Read())
            {
                cboTDN.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            }

            reader.Close();
            concli.CerrarConexion();
        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            string Representante = (string)Session["Nombre_Usuario"];
            string Fecha = System.DateTime.Now.ToString("yyyy/MM/dd");
            int bandera = (int)Session["Bandera"];
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            txtNIT.Text = txtNIT.Text.ToString().ToUpper();
            txtDireccion.Text = txtDireccion.Text.ToString().ToUpper();
            txtTelefono.Text = txtTelefono.Text.ToString().ToUpper();
            txtDescripcion.Text = txtDescripcion.Text.ToString().ToUpper();
            txtNotas.Text = txtNotas.Text.ToString().ToUpper();
            txtPrecio.Text = txtPrecio.Text.ToString().ToUpper();

            if ((btnAdicionar.Text == "Adicionar") || (btnAdicionar.Text == "Add") || (btnAdicionar.Text == "Adicionar"))
            {
                if (cboPais.SelectedItem.Text == "Seleccione El Pais" || cboPais.SelectedItem.Text == "Select The Country" ||
                    cboPais.SelectedItem.Text == "Selecione O País")
                {
                    if (idioma == "Español")
                    {
                        mensaje = "Debe seleccionar el pais.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "You must select the country.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Você deve selecionar o país.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    if (cboCiudad.SelectedItem.Text == "Seleccione La Ciudad" || cboCiudad.SelectedItem.Text == "Select The City" ||
                        cboCiudad.SelectedItem.Text == "Selecione A Cidade")
                    {
                        if (idioma == "Español")
                        {
                            mensaje = "Debe seleccionar la ciudad.";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "You must select the city.";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Você deve selecionar o cidade.";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        if (cboCliente.SelectedItem.Text == "Seleccione La Empresa" || cboCliente.SelectedItem.Text == "Select Company" ||
                            cboCliente.SelectedItem.Text == "Selecione Companhia")
                        {
                            if (idioma == "Español")
                            {
                                mensaje = "Debe seleccionar la empresa.";
                            }
                            if (idioma == "Ingles")
                            {
                                mensaje = "You must select the company.";
                            }
                            if (idioma == "Portugues")
                            {
                                mensaje = "Você deve selecionar o companhia.";
                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                        else
                        {
                            if (cboObra.SelectedItem.Text == "Seleccione La Obra" || cboObra.SelectedItem.Text == "Select The Work" ||
                                cboObra.SelectedItem.Text == "Selecione A Obra")
                            {
                                if (idioma == "Español")
                                {
                                    mensaje = "Debe seleccionar la obra.";
                                }
                                if (idioma == "Ingles")
                                {
                                    mensaje = "You must select the work.";
                                }
                                if (idioma == "Portugues")
                                {
                                    mensaje = "Você deve selecionar a obra.";
                                }
                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            }
                            else
                            {
                                if ((txtCant.Text == "") || (txtDescripcion.Text == "") || (txtPrecioUnitario.Text == "")
                               || (txtTotalUni.Text == "") || (txtFec.Text == "") || (txtNIT.Text == "") || (txtDireccion.Text == "") || (txtTelefono.Text == ""))
                                {
                                    lblCant.BackColor = System.Drawing.Color.LightYellow;
                                    lblDescrip.BackColor = System.Drawing.Color.LightYellow;
                                    lblPrecio.BackColor = System.Drawing.Color.LightYellow;
                                    lblTotalUnitario.BackColor = System.Drawing.Color.LightYellow;
                                    txtFec.BackColor = System.Drawing.Color.LightYellow;
                                    txtNIT.BackColor = System.Drawing.Color.LightYellow;
                                    txtDireccion.BackColor = System.Drawing.Color.LightYellow;
                                    txtTelefono.BackColor = System.Drawing.Color.LightYellow;

                                    if (idioma == "Español")
                                    {
                                        mensaje = "Verifique los datos obligatorios.";
                                    }
                                    if (idioma == "Ingles")
                                    {
                                        mensaje = "Please check mandatory data.";
                                    }
                                    if (idioma == "Portugues")
                                    {
                                        mensaje = "Verifique a obrigatoriedade.";
                                    }
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                }
                                else
                                {
                                    lblCant.BackColor = System.Drawing.Color.White;
                                    lblDescrip.BackColor = System.Drawing.Color.White;
                                    lblPrecio.BackColor = System.Drawing.Color.White;
                                    lblTotalUnitario.BackColor = System.Drawing.Color.White;
                                    txtFec.BackColor = System.Drawing.Color.White;
                                    txtNIT.BackColor = System.Drawing.Color.White;
                                    txtDireccion.BackColor = System.Drawing.Color.White;
                                    txtTelefono.BackColor = System.Drawing.Color.White;

                                    if (bandera == 0)
                                    {
                                        int FacturaID = contfactura.FacturaID(cboPais.SelectedItem.Text, cboCiudad.SelectedItem.Text,
                                           cboCliente.SelectedItem.Text, cboObra.SelectedItem.Text, txtNIT.Text, txtDireccion.Text,
                                            txtTelefono.Text, txtFec.Text, Representante, Fecha);

                                        Session["Factura"] = FacturaID;
                                    }

                                    int FacID = (int)Session["Factura"];

                                    int ingresar = contfactura.DetalleFactura(FacID, Convert.ToInt32(txtCant.Text), txtDescripcion.Text,
                                        txtPrecioUnitario.Text, txtTotalUni.Text, Representante, Fecha, Representante, Fecha);

                                    bandera = 1;
                                    Session["Bandera"] = bandera;

                                    this.SumaFactura();

                                    //CONVERTIR EL VALOR TOTAL EN LETRAS
                                    string ValorLetras = contfactura.NumeroALetras(lblTotalFin.Text);
                                    txtPrecio.Text = ValorLetras;

                                    this.ReporteDetalleFactura();
                                    this.MensajeDetalleFactura();
                                }
                            }

                        }
                    }
                }
            }

            if ((btnAdicionar.Text == "Actualizar") || (btnAdicionar.Text == "Update") || (btnAdicionar.Text == "Atualizar"))
            {

            }
        }

        private void MensajeDetalleFactura()
        {
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            if ((btnAdicionar.Text == "Adicionar") || (btnAdicionar.Text == "Add") || (btnAdicionar.Text == "Adicionar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Detalle factura creada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Invoice detail created successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Detalhe fatura criado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            if ((btnAdicionar.Text == "Actualizar") || (btnAdicionar.Text == "Update") || (btnAdicionar.Text == "Atualizar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Detalle factura actualizada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Invoice detail updated successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Detalhe fatura atualizado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private void ReporteDetalleFactura()
        {
            int IDFactura = (int)Session["Factura"];
            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("IDFactura", Convert.ToString(IDFactura), true));
            RFactura.Width = 1280;
            RFactura.Height = 1050;
            RFactura.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            RFactura.ServerReport.ReportServerCredentials = irsc;

            RFactura.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            RFactura.ServerReport.ReportPath = "/InformesCRM/COM_DetalleFactura";
            this.RFactura.ServerReport.SetParameters(parametro);
        }

        private void VerReporteDetalleFactura(string IDFactura)
        {
            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("IDFactura", IDFactura, true));
            RFactura.Width = 1280;
            RFactura.Height = 1050;
            RFactura.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            RFactura.ServerReport.ReportServerCredentials = irsc;

            RFactura.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            RFactura.ServerReport.ReportPath = "/InformesCRM/COM_DetalleFactura";
            this.RFactura.ServerReport.SetParameters(parametro);
        }        

        private void SumaFactura()
        {
            int FacID = (int)Session["Factura"];
            reader = contfactura.ConsultarSumaFactura(FacID);
            reader.Read();
            txtSubTotal.Text = reader.GetValue(0).ToString();
            reader.Close();

            decimal SubTotal = Convert.ToDecimal(txtSubTotal.Text);
            txtSubTotal.Text = SubTotal.ToString("#,##.##");

            if (cboPais.SelectedItem.Value == "8")
            {
                this.CalcularIVA();
            }

            decimal Total = 0;

            if (txtValor.Text == "0")
            {
                Total = Convert.ToDecimal(txtSubTotal.Text.Replace(",","")) + Convert.ToDecimal(txtIVA.Text.Replace(",",""));
                lblTotalFin.Text = Total.ToString("#,##.##");
            }
            else
            {
                Total = Convert.ToDecimal(txtValor.Text.Replace(",","")) + Convert.ToDecimal(txtIVA.Text.Replace(",",""));
                lblTotalFin.Text = Total.ToString("#,##.##");
            }
        }

        private void CalcularIVA()
        {
            decimal IVA = 0;

            if (txtValor.Text == "0")
            {
                IVA = Convert.ToDecimal(txtSubTotal.Text) * 16/100;
                txtIVA.Text = IVA.ToString("#,##.##");
            }
            else
            {
                IVA = Convert.ToDecimal(txtValor.Text) * 16 / 100;
                txtIVA.Text = IVA.ToString("#,##.##");
            }            
        }

        protected void chkDesc_CheckedChanged(object sender, EventArgs e)
        {
            txtPorc.Enabled = true;
            txtValor.Enabled = true;
        }

        protected void txtValor_TextChanged(object sender, EventArgs e)
        {
            decimal Porc, PorVenta, vrFinal;

            Porc = ((Convert.ToDecimal(txtValor.Text) * 100) / Convert.ToDecimal(txtSubTotal.Text));
            PorVenta = 100 - Convert.ToDecimal(Porc);
            txtPorc.Text = Convert.ToString(PorVenta.ToString("#.##"));
            
            vrFinal = Convert.ToDecimal(txtSubTotal.Text) - PorVenta;
            lblTotalFin.Text = Convert.ToString(vrFinal.ToString("#.##"));

            //CONVERTIR EL VALOR TOTAL EN LETRAS
            string ValorLetras = contfactura.NumeroALetras(lblTotalFin.Text);
            txtPrecio.Text = ValorLetras;
        }

        protected void txtPorc_TextChanged(object sender, EventArgs e)
        {
            decimal total_pagar = 0, vrFinal;
            decimal valor_venta = Convert.ToDecimal(txtSubTotal.Text);
            decimal pr_pag = Convert.ToDecimal(txtPorc.Text) / 100;

            if (txtPorc.Text != "0")
            {
                total_pagar = valor_venta * pr_pag;
                //Math.Round(total_pagar, 2);
                txtValor.Text = Convert.ToString(total_pagar.ToString("#.##"));
            }

            vrFinal = Convert.ToDecimal(txtSubTotal.Text) - total_pagar;
            lblTotalFin.Text = Convert.ToString(vrFinal.ToString("#.##"));

            //CONVERTIR EL VALOR TOTAL EN LETRAS
            string ValorLetras = contfactura.NumeroALetras(lblTotalFin.Text);
            txtPrecio.Text = ValorLetras;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];

            if ((btnGuardar.Text == "Finalizar Factura") || (btnGuardar.Text == "Finish Invoice") ||
                (btnGuardar.Text == "Concluir Fatura"))
            {
                int FacID = (int)Session["Factura"];
                string Moneda = "";

                if (cboPais.SelectedItem.Value == "8")
                {
                    Moneda = "Pesos";
                }
                else
                {
                    Moneda = "Dolar";
                }

                string consecutivo = "";

                reader = contfactura.ConsultarConsecutivoFactura();
                reader.Read();
                consecutivo = reader.GetValue(0).ToString();
                reader.Close();

                txtNotas.Text = txtNotas.Text.ToString().ToUpper();

                int actualizar = contfactura.ActualizarFactura(FacID, txtSubpartida.Text, chkDesc.Checked, txtSubTotal.Text.Replace(",", ""),
                    txtPorc.Text, txtValor.Text.Replace(",", ""), txtIVA.Text.Replace(",", ""), lblTotalFin.Text.Replace(",", ""), Moneda,
                    txtNotas.Text, txtPrecio.Text, Convert.ToInt32(consecutivo), cboTDN.SelectedItem.Text, lblAno.Text);

                txtConsec.Text = consecutivo;
                int consec = Convert.ToInt32(consecutivo) + 1;

                int actconsec = contfactura.ActualizarConsecutivoForsa(consec);
                
                //CONVERTIR EL VALOR TOTAL EN LETRAS
                string ValorLetras = contfactura.NumeroALetras(lblTotalFin.Text);
                txtPrecio.Text = ValorLetras;

                this.MensajeFactura();
            }

            if ((btnGuardar.Text == "Actualizar Factura") || (btnGuardar.Text == "Update Invoice") ||
                (btnGuardar.Text == "Atualizar Fatura"))
            {
                string Moneda = "";

                if (cboPais.SelectedItem.Value == "8")
                {
                    Moneda = "Pesos";
                }
                else
                {
                    Moneda = "Dolar";
                }
                
                int actualizaencabezado = contfactura.ActualizarFacturaProforma(Convert.ToInt32(txtConsec.Text), 
                    cboPais.SelectedItem.Value, cboCiudad.SelectedItem.Value, cboCliente.SelectedItem.Value, 
                    cboObra.SelectedItem.Value, txtNIT.Text, txtDireccion.Text, txtTelefono.Text, txtFec.Text,
                    txtSubpartida.Text, cboTDN.SelectedValue, chkDesc.Checked, txtSubTotal.Text, txtPorc.Text,
                    Moneda, txtValor.Text, txtIVA.Text, lblTotalFin.Text, txtNotas.Text, txtPrecio.Text);

                this.MensajeFactura();
            }
        }

        private void MensajeFactura()
        {
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            if ((btnGuardar.Text == "Finalizar Factura") || (btnGuardar.Text == "Finish Invoice") ||
                (btnGuardar.Text == "Concluir Fatura"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Factura creada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Invoice created successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Faturamento criado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Factura actualizada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Invoice updated successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Faturamento atualizado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        protected void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            decimal Total = Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCant.Text);
            txtTotalUni.Text = Convert.ToString(Total);
        }

        protected void txtConsec_TextChanged(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            int rol = (int)Session["Rol"];
            string mensaje = "";

            reader = contfactura.ConsultarFacturaProforma(Convert.ToInt32(txtConsec.Text));
            if (reader.Read() == false)
            {
                if (idioma == "Español")
                   mensaje = "No existe información para ese número de factura. Verifique.";
                if (idioma == "Ingles")
                    mensaje = "There is no information for that invoice number. Verify.";
                if (idioma == "Portugues")
                    mensaje = "Não há informações de que o número da nota fiscal. verificar.";

                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (idioma == "Español")
                    btnGuardar.Text = "Actualizar Factura";
                if (idioma == "Ingles")
                    btnGuardar.Text = "Update Invoice";
                if (idioma == "Portugues")
                    btnGuardar.Text = "Atualizar Fatura";

                if (idioma == "Español")
                    btnGuardar.Text = "Actualizar";
                if (idioma == "Ingles")
                    btnGuardar.Text = "Update";
                if (idioma == "Portugues")
                    btnGuardar.Text = "Atualizar";

                cboPais.Items.Clear();
                cboPais.Items.Add(new ListItem(reader.GetString(4)));
                cboCiudad.Items.Clear();
                cboCiudad.Items.Add(new ListItem(reader.GetString(5)));
                cboCliente.Items.Clear();
                cboCliente.Items.Add(new ListItem(reader.GetString(0)));
                cboObra.Items.Clear();
                cboObra.Items.Add(new ListItem(reader.GetString(6)));
                txtNIT.Text = reader.GetValue(2).ToString();
                txtDireccion.Text = reader.GetValue(1).ToString();
                txtTelefono.Text = reader.GetValue(3).ToString();
                txtFec.Text = reader.GetDateTime(7).ToShortDateString();
                lblAno.Text = reader.GetValue(20).ToString();
                txtSubpartida.Text = reader.GetValue(15).ToString();
                cboTDN.Items.Clear();
                cboTDN.Items.Add(new ListItem(reader.GetString(16)));
                chkDesc.Checked = reader.GetSqlBoolean(10).Value;
                if (chkDesc.Checked == true)
                {
                    txtPorc.Enabled = true;
                }
                else
                {
                    txtPorc.Enabled = false;
                }
                txtSubTotal.Text = reader.GetValue(8).ToString();
                decimal vrFinal = Convert.ToDecimal(txtSubTotal.Text);
                txtSubTotal.Text = Convert.ToString(vrFinal.ToString("#.##"));
                txtPorc.Text = reader.GetValue(10).ToString();
                txtValor.Text = reader.GetValue(11).ToString();
                decimal vrDesc = Convert.ToDecimal(txtValor.Text);
                txtValor.Text = Convert.ToString(vrDesc.ToString("#.##"));
                txtIVA.Text = reader.GetValue(12).ToString();
                decimal VrIva = Convert.ToDecimal(txtIVA.Text);
                txtIVA.Text = Convert.ToString(VrIva.ToString("#.##"));
                lblTotalFin.Text = reader.GetValue(13).ToString();
                decimal VrTotal = Convert.ToDecimal(lblTotalFin.Text);
                lblTotalFin.Text = Convert.ToString(VrTotal.ToString("#.##"));
                txtNotas.Text = reader.GetValue(18).ToString();
                txtPrecio.Text = reader.GetValue(17).ToString();
                string IDFactura = reader.GetValue(21).ToString();
                reader.Close();

                //CARGAMOS EL PAIS
                if ((rol == 3) || (rol == 28) || (rol == 29))
                {
                    reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
                    if (idioma == "Español")
                    {
                        cboPais.Items.Add("Seleccione El Pais");
                    }
                    if (idioma == "Ingles")
                    {
                        cboPais.Items.Add("Select The Country");
                    }
                    if (idioma == "Portugues")
                    {
                        cboPais.Items.Add("Selecione O País");
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
                }
                else
                {
                    reader = contubi.poblarListaPais();
                    if (idioma == "Español")
                    {
                        cboPais.Items.Add("Seleccione El Pais");
                    }
                    if (idioma == "Ingles")
                    {
                        cboPais.Items.Add("Select The Country");
                    }
                    if (idioma == "Portugues")
                    {
                        cboPais.Items.Add("Selecione O País");
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
                }

                //CARGAMOS EL TDN
                reader = concli.ConsultarTDN();
                cboTDN.Items.Add("Elija El TDN");

                while (reader.Read())
                {
                    cboTDN.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }

                reader.Close();

                //VERIFICAMOS CAMPOS VACIOS
                if (txtIVA.Text == "")
                {
                    txtIVA.Text = "0";
                }

                this.VerReporteDetalleFactura(IDFactura);
            }
        }

        private void ConsultarFactura()
        {

        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("FacturaProforma.aspx");
        }
    }
}