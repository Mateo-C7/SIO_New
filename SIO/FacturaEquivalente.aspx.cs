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
    public partial class FacturaEquivalente1 : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlFacturaProforma contfactura = new ControlFacturaProforma();
        private DataSet dsFacturaEquivalente = new DataSet();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PoblarListaPais();
                //this.Idioma();
                this.Impuesto();
                int bandera = 0;
                Session["Bandera"] = bandera;
            }
        }

        private void Impuesto()
        {
            cboImpuesto.Items.Clear();
            reader = contfactura.ConsultarImpuesto();
            while (reader.Read())
            {
                cboImpuesto.Items.Add(new ListItem(reader.GetInt32(0).ToString(), reader.GetInt32(1).ToString()));
            }
            reader.Close();
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

            dsFacturaEquivalente = contfactura.ConsultarIdiomaFacturaEquivalente();

            foreach (DataRow fila in dsFacturaEquivalente.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    pnlCliente.GroupingText = fila[idiomaId].ToString();
                if (posicion == 2)
                    pnlFacturaEquivalente.GroupingText = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblPais.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblRazonSocial.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    pnlCondiciones.GroupingText = fila[idiomaId].ToString();
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
                    lblSubTotal.Text = fila[idiomaId].ToString();
                if (posicion == 17)
                    lblExpedicion.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    lblRetencion.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    pnlNotas.GroupingText = fila[idiomaId].ToString();
                if (posicion == 20)
                    pnlPrecio.GroupingText = fila[idiomaId].ToString();
                if (posicion == 21)
                    btnGuardar.Text = fila[idiomaId].ToString();
                if (posicion == 22)
                    btnNuevo.Text = fila[idiomaId].ToString();
                if (posicion == 23)
                    btnAdicionar.Text = fila[idiomaId].ToString();
                if (posicion == 24)
                    lblTipoImpuesto.Text = fila[idiomaId].ToString();
            }
            dsFacturaEquivalente.Tables.Remove("Table");
            dsFacturaEquivalente.Dispose();
            dsFacturaEquivalente.Clear();
        }    

        private void PoblarListaPais()
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
                if (cboForsa.SelectedItem.Text == "Arrendadora Bogota")
                {
                    reader = contfactura.ConsultarConsecutivoBogota();
                    reader.Read();
                    consecutivo = reader.GetValue(0).ToString();
                    reader.Close();
                }
                
                if (cboForsa.SelectedItem.Text == "Arrendadora Cali")
                {
                    reader = contfactura.ConsultarConsecutivoCandelaria();
                    reader.Read();
                    consecutivo = reader.GetValue(0).ToString();
                    reader.Close();
                }
                
                if (cboForsa.SelectedItem.Text == "Forsa Caloto")
                {
                    reader = contfactura.ConsultarConsecutivoForsa();
                    reader.Read();
                    consecutivo = reader.GetValue(0).ToString();
                    reader.Close();
                }         

                //int actualizar = contfactura.ActualizarFactura(FacID, txtSubpartida.Text.Replace(",", ""),
                //    txtExp.Text.Replace(",", ""), txtValor.Text.Replace(",", ""), lblTotalFin.Text.Replace(",", ""),
                //    Moneda, txtNotas.Text, txtPrecio.Text, Convert.ToInt32(consecutivo));

                txtConsec.Text = consecutivo;

                int consec = Convert.ToInt32(consecutivo) + 1;

                if (cboForsa.SelectedItem.Text == "Arrendadora Bogota")
                {
                    int actconsec = contfactura.ActualizarConsecutivoBogota(consec);
                }

                if (cboForsa.SelectedItem.Text == "Arrendadora Cali")
                {
                    int actconsec = contfactura.ActualizarConsecutivoCandelaria(consec);
                }

                if (cboForsa.SelectedItem.Text == "Forsa Caloto")
                {
                    int actconsec = contfactura.ActualizarConsecutivoForsa(consec);
                }

                this.MensajeFactura();
            }

            if ((btnGuardar.Text == "Actualizar Factura") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
            {

            }
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

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            string Representante = (string)Session["Nombre_Usuario"];
            string Fecha = System.DateTime.Now.ToString("yyyy/MM/dd");
            int bandera = (int)Session["Bandera"];
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            txtRazonSocial.Text = txtRazonSocial.Text.ToString().ToUpper();
            txtNIT.Text = txtNIT.Text.ToString().ToUpper();
            txtDireccion.Text = txtDireccion.Text.ToString().ToUpper();
            txtTelefono.Text = txtTelefono.Text.ToString().ToUpper();
            txtDescripcion.Text = txtDescripcion.Text.ToString().ToUpper();
            txtNotas.Text = txtNotas.Text.ToString().ToUpper();
            txtPrecio.Text = txtPrecio.Text.ToString().ToUpper();

            if ((btnAdicionar.Text == "Adicionar") || (btnAdicionar.Text == "Add") || (btnAdicionar.Text == "Adicionar"))
            {
                if (cboForsa.SelectedItem.Text == "Elija")
                {
                    if (idioma == "Español")
                    {
                        mensaje = "Empresa no seleccionada.";
                    }

                    if (idioma == "Ingles")
                    {
                        mensaje = "Company unselected.";
                    }

                    if (idioma == "Portugues")
                    {
                        mensaje = "Companhia não selecionado.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
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
                        cboCiudad.SelectedItem.Text == "A Cidade")
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
                            if ((txtCant.Text == "") || (txtDescripcion.Text == "") || (txtPrecioUnitario.Text == "") 
                                || (txtTotalUni.Text == "") || (txtFec.Text == "") || (txtRazonSocial.Text == "")
                                || (txtNIT.Text == "") || (txtDireccion.Text == "") || (txtTelefono.Text == ""))
                            {
                                lblCant.BackColor = System.Drawing.Color.LightYellow;
                                lblDescrip.BackColor = System.Drawing.Color.LightYellow;
                                lblPrecio.BackColor = System.Drawing.Color.LightYellow;
                                lblTotalUnitario.BackColor = System.Drawing.Color.LightYellow;
                                txtFec.BackColor = System.Drawing.Color.LightYellow;
                                txtRazonSocial.BackColor = System.Drawing.Color.LightYellow;
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
                                txtRazonSocial.BackColor = System.Drawing.Color.White;
                                txtNIT.BackColor = System.Drawing.Color.White;
                                txtDireccion.BackColor = System.Drawing.Color.White;
                                txtTelefono.BackColor = System.Drawing.Color.White;

                                if (bandera == 0)
                                { 
                                    int FacturaID = contfactura.FacturaID(cboForsa.SelectedItem.Text, cboPais.SelectedItem.Text, 
                                        cboCiudad.SelectedItem.Text, txtRazonSocial.Text, txtNIT.Text, txtDireccion.Text, 
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

        private void ReporteDetalleFactura()
        {
            int IDFactura = (int)Session["Factura"];
            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("IDFactura", Convert.ToString(IDFactura), true));
            ReportDetalle.Width = 1280;
            ReportDetalle.Height = 1050;
            ReportDetalle.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportDetalle.ServerReport.ReportServerCredentials = irsc;

            ReportDetalle.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportDetalle.ServerReport.ReportPath = "/InformesCRM/COM_DetalleFactura";
            this.ReportDetalle.ServerReport.SetParameters(parametro);
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

        private void SumaFactura()
        {
            int FacID = (int)Session["Factura"];
            reader = contfactura.ConsultarSumaFactura(FacID);
            reader.Read();
            txtSubpartida.Text = reader.GetValue(0).ToString();
            reader.Close();           

            decimal impuesto = (Convert.ToDecimal(txtSubpartida.Text) * (Convert.ToDecimal(cboImpuesto.SelectedItem.Text) / 100));
            if (impuesto == 0)
            {
                txtValor.Text = impuesto.ToString(); ;
            }
            else
            {
                txtValor.Text = impuesto.ToString("#,##.##");
            }

            decimal Total = Convert.ToDecimal(txtSubpartida.Text) + Convert.ToDecimal(txtExp.Text);

            lblTotalFin.Text = Total.ToString("#,##.##");

            decimal subpartida = Convert.ToDecimal(txtSubpartida.Text);
            txtSubpartida.Text = subpartida.ToString("#,##.##");
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

        protected void txtPrecioUnitario_TextChanged(object sender, EventArgs e)
        {
            decimal Total = Convert.ToDecimal(txtPrecioUnitario.Text) * Convert.ToDecimal(txtCant.Text);
            txtTotalUni.Text = Convert.ToString(Total);
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Factura.aspx");
        }
    }
}