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
using System.Web.UI;
using System.Web.UI.WebControls;
using CapaControl;
using CapaDatos;
using Microsoft.Reporting.WebForms;


namespace SIO
{
    public partial class ItinerariosLogistica : System.Web.UI.Page
    {
        ControlItinerarios ctrlitinerarios = new ControlItinerarios();
        SqlDataReader reader = null;
        protected void Page_Load(object sender, EventArgs e)
        {        
            if (!IsPostBack)
            {
                int rol;             
                string nom = (string)Session["Usuario"];
                reader = ctrlitinerarios.consultarNombre(nom);
                reader.Read();
                txtRepresentante.Text = reader.GetValue(0).ToString();
                rol = reader.GetInt32(4);
                reader.Close();

                if (rol == 15)
                {
                    Session["modifica"] = 1;
                }
                else
                {
                    Session["modifica"] = 0;
                }
                //poblar lista pais
                cboPaisDestino.Items.Clear();
                cboPaisDestino.Items.Add(new ListItem("Seleccione", "0"));
                ctrlitinerarios.Listar_DatosPais(cboPaisDestino);
                //poblar puerto
                cboPuertoZarpe.Items.Clear();
                cboDescargue.Items.Clear();
                cboPuertoZarpe.Items.Add(new ListItem("Seleccione El Puerto", "0"));
                cboDescargue.Items.Add(new ListItem("Seleccione El Puerto", "0"));
                ctrlitinerarios.Listar_Puertos(cboPuertoZarpe);
                ctrlitinerarios.Listar_Puertos(cboDescargue);
                //Poblar naviera
                cboNaviera.Items.Clear();
                cboNaviera.Items.Add(new ListItem("Seleccione La Naviera", "0"));
                ctrlitinerarios.Listar_Naviera(cboNaviera);
                //Poblar reporte
                this.CargarReporteItinerario();

                string estado = "", idItinerario = "";
                if (Request.QueryString["estado"] != null)
                {
                    estado = Request.QueryString["estado"];
                    Session["estado"] = estado;
                    idItinerario = Request.QueryString["id"];
                    Session["codItinerario"] = idItinerario;
                }

                if (estado == "actualizar" || estado == "eliminar")
                {
                    this.CargarItinerarios();
                    btnAdicionar.Visible = true;
                }
                //Inicializa con valor 0
                txtTiempo.Text = "0";

                //Valida decimales en los campos 
                txt20ST.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                txt40HC.Attributes.Add("onKeyPress", " return  validedecimal(event,this);");
                //Valida enteros
                txtTiempo.Attributes.Add("onKeyPress", " return  valideKeyenteros(event,this);");
            }
        }

        private void CargarReporteItinerario()
        {
            string modifica = Session["modifica"].ToString();
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("pais", " Todos", true));
            parametro.Add(new ReportParameter("modifica", modifica, true));

            //// Creo uno o varios parámetros de tipo ReportParameter con sus valores.
            //ReportParameter parametro = new ReportParameter("operacion", "Acta");
            //// Añado uno o varios parámetros(En este caso solo uno al ReportViewer
            //this.ReportViewer1.ServerReport.SetParameters(new ReportParameter [] {parametro});
            this.ReporteVerItinerarios.KeepSessionAlive = true;
            this.ReporteVerItinerarios.AsyncRendering = true;
            ReporteVerItinerarios.Width =975;
            ReporteVerItinerarios.Height = 400;
            ReporteVerItinerarios.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteVerItinerarios.ServerReport.ReportServerCredentials = irsc;
            ReporteVerItinerarios.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReporteVerItinerarios.ServerReport.ReportPath = "/Logistica/LOG_Itinerarios";
            this.ReporteVerItinerarios.ServerReport.SetParameters(parametro);
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


        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            if (cboPaisDestino.Text != "0" && cboPuertoZarpe.Text != "0" &&
                cboDescargue.Text != "0" && cboNaviera.Text != "0")
             {
                if (String.IsNullOrEmpty(txtTiempo.Text))
                {
                    txtTiempo.Text = "0";
                }
                if (!String.IsNullOrEmpty(txtFecCargue.Text))
                {

                    if (btnGuardar.Text == "Guardar")
                    {
                        int guardar = ctrlitinerarios.IngresarItinerario(Convert.ToInt32(cboPaisDestino.SelectedValue), Convert.ToInt32(cboPuertoZarpe.SelectedValue),
                            Convert.ToInt32(cboDescargue.SelectedValue), Convert.ToInt32(txtTiempo.Text), Convert.ToInt32(cboNaviera.SelectedValue),
                            txt20ST.Text, txt40HC.Text, txtBuque.Text, txtFecCargue.Text, txtCierreNav.Text,
                            txtEstZarpe.Text, txtArribo.Text, txtRepresentante.Text);


                        if (guardar == 1)
                        {
                            mensajeVentana("Ingreso satisfactorio del itinerario.");
                        }
                        else
                        {
                            mensajeVentana("No fue posible guardar el itinerario.");
                        }

                        this.CargarReporteItinerario();
                    }

                    if (btnGuardar.Text == "Actualizar")
                    {
                        string codigo = (string)Session["codItinerario"];

                        int actualizar = ctrlitinerarios.ActualizarItinerario(Convert.ToInt32(codigo), Convert.ToInt32(cboPaisDestino.SelectedValue),
                            Convert.ToInt32(cboPuertoZarpe.SelectedValue), Convert.ToInt32(cboDescargue.SelectedValue), Convert.ToInt32(txtTiempo.Text),
                            Convert.ToInt32(cboNaviera.SelectedValue), txt20ST.Text, txt40HC.Text, txtBuque.Text,
                            txtFecCargue.Text, txtCierreNav.Text, txtEstZarpe.Text, txtArribo.Text, txtRepresentante.Text);

                        if (actualizar == 1)
                        {
                            mensajeVentana("Actualización satisfactoria del itinerario.");
                        }
                        else
                        {
                            mensajeVentana("No fue posible actualizar el itinerario.");
                        }

                        this.CargarReporteItinerario();
                    }

                    if (btnGuardar.Text == "Eliminar")
                    {
                        string codigo = (string)Session["codItinerario"];

                        int actualizar = ctrlitinerarios.EliminarItinerario(Convert.ToInt32(codigo));

                        if (actualizar == 1)
                        {
                            mensajeVentana("Eliminación satisfactoria del itinerario.");
                            LimpiarCampos();
                            this.CargarReporteItinerario();
                            btnGuardar.Text = "Guardar";
                            btnAdicionar.Visible = false;
                        }
                        else
                        {
                            mensajeVentana("No fue posible actualizar el itinerario.");
                        }
                    }
                }
                else
                {
                    mensajeVentana("Debe establecer la fecha de cargue del itinerario.");
                }
            }
                else
                {
                    mensajeVentana("Debe establecer el pais, los puertos y la naviera.");
                }
            }
        

        public void LimpiarCampos()
        {
            txtTiempo.Text = "";              
            txt20ST.Text = "";
            txt40HC.Text = "";
            txtBuque.Text = "";
            txtFecCargue.Text = "";      
            txtCierreNav.Text = "";     
            txtEstZarpe.Text = "";           
            txtArribo.Text = "";
            cboPaisDestino.Items.Clear();
            cboDescargue.Items.Clear();
            cboPuertoZarpe.Items.Clear();
            cboNaviera.Items.Clear();
            cboPuertoZarpe.Items.Add(new ListItem("Seleccione El Puerto", "0"));
            cboDescargue.Items.Add(new ListItem("Seleccione El Puerto", "0"));
            cboNaviera.Items.Add(new ListItem("Seleccione La Naviera", "0"));
            cboPaisDestino.Items.Add(new ListItem("Seleccione", "0"));
            ctrlitinerarios.Listar_DatosPais(cboPaisDestino);
            ctrlitinerarios.Listar_Puertos(cboPaisDestino);
            ctrlitinerarios.Listar_Puertos(cboPuertoZarpe);
            ctrlitinerarios.Listar_Naviera(cboNaviera);
        }

        private void CargarItinerarios()
        {
            string codigo = (string)Session["codItinerario"];
            reader = ctrlitinerarios.ConsultarItinerario(Convert.ToInt32(codigo));
            reader.Read();
            cboPaisDestino.Items.Clear();
            cboPaisDestino.Items.Add(new ListItem(reader.GetString(15), reader.GetInt32(1).ToString()));    
            cboPuertoZarpe.Items.Clear();
            cboPuertoZarpe.Items.Add(new ListItem(reader.GetString(13), reader.GetInt32(2).ToString()));       
            cboDescargue.Items.Clear();
            cboDescargue.Items.Add(new ListItem(reader.GetString(14), reader.GetInt32(3).ToString()));    
            txtTiempo.Text = reader.GetValue(4).ToString();
            cboNaviera.Items.Clear();
            cboNaviera.Items.Add(new ListItem(reader.GetString(16), reader.GetInt32(5).ToString()));
            decimal ST = reader.GetDecimal(6);
            txt20ST.Text = Convert.ToString(ST.ToString("#,##.##"));
            decimal HC = reader.GetDecimal(7);
            txt40HC.Text = Convert.ToString(HC.ToString("#,##.##"));  
            txtBuque.Text = reader.GetValue(8).ToString();
            txtFecCargue.Text = reader.GetDateTime(9).ToShortDateString();    
            if (txtFecCargue.Text == "01/01/1900") txtFecCargue.Text = "";
            txtCierreNav.Text = reader.GetDateTime(10).ToShortDateString();
            if (txtCierreNav.Text == "01/01/1900") txtCierreNav.Text = "";
            txtEstZarpe.Text = reader.GetDateTime(11).ToShortDateString();
            if (txtEstZarpe.Text == "01/01/1900") txtEstZarpe.Text = "";
            txtArribo.Text = reader.GetDateTime(12).ToShortDateString();
            if (txtArribo.Text == "01/01/1900") txtArribo.Text = "";
            reader.Close();

            ctrlitinerarios.Listar_DatosPais(cboPaisDestino);
            ctrlitinerarios.Listar_Puertos(cboDescargue);
            ctrlitinerarios.Listar_Puertos(cboPuertoZarpe);
            ctrlitinerarios.Listar_Naviera(cboNaviera);

            string estado = (string)Session["estado"];
            if (estado == "actualizar")
            {
                btnGuardar.Text = "Actualizar";
            }

            if (estado == "eliminar")
            {
                btnGuardar.Text = "Eliminar";
            }
        }

        protected void lkZarpe_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "PUERTO DE ZARPE";
            lblNombre.Text = "Puerto";
            Session["bandera"] = 1;
            if (cboPuertoZarpe.SelectedItem.Text == "Seleccione El Puerto")
            {
                txtDetalle.Text = "";
                btnGuardarMaestro.Text = "Guardar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }
            else
            {
                txtDetalle.Text = cboPuertoZarpe.SelectedItem.Text;
                btnGuardarMaestro.Text = "Actualizar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }

            SetPane("AcorInfoGeneral");
            AcorInfoGeneral.Visible = true;
            panel1.Visible = false; // se debe ocultar el resto de campos
        }

        protected void ImgVolverCiudad_Click(object sender, ImageClickEventArgs e)
        {
            panel1.Visible = true;
            AcorInfoGeneral.Visible = false;
        }

        //Permite establecer que acordion se visualizara
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

        protected void lkDescargue_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "PUERTO DE DESCARGUE";
            lblNombre.Text = "Puerto";
            Session["bandera"] = 3;
            if (cboDescargue.SelectedItem.Text == "Seleccione El Puerto")
            {
                txtDetalle.Text = "";
                btnGuardarMaestro.Text = "Guardar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }
            else
            {
                Session["bandera"] = 3;
                txtDetalle.Text = cboDescargue.SelectedItem.Text;
                btnGuardarMaestro.Text = "Actualizar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }

            SetPane("AcorInfoGeneral");
            AcorInfoGeneral.Visible = true;
            panel1.Visible = false; // se debe ocultar el resto de campos
        }

        protected void lkNaviera_Click(object sender, EventArgs e)
        {
            lblTitulo.Text = "NAVIERA";
            lblNombre.Text = "Naviera";
            Session["bandera"] = 2;
            if (cboNaviera.SelectedItem.Text == "Seleccione La Naviera")
            {
                txtDetalle.Text = "";
                btnGuardarMaestro.Text = "Guardar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }
            else
            {
                txtDetalle.Text = cboNaviera.SelectedItem.Text;
                btnGuardarMaestro.Text = "Actualizar";
                lblMSJMaestro.Text = "";
                lblMSJMaestro.Visible = false;
            }

            SetPane("AcorInfoGeneral");
            AcorInfoGeneral.Visible = true;
            panel1.Visible = false; // se debe ocultar el resto de campos
        }

        protected void btn_Nuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("ItinerariosLogistica.aspx");
        }

        protected void txtEstZarpe_TextChanged(object sender, EventArgs e)
        {
            if(!string.IsNullOrEmpty(txtEstZarpe.Text) && !string.IsNullOrEmpty(txtCierreNav.Text))
            {

                DateTime fechamayor = Convert.ToDateTime(txtEstZarpe.Text);
                DateTime fechaNaviera = Convert.ToDateTime(txtCierreNav.Text);
                DateTime sumaFecha = fechamayor.AddDays(Convert.ToDouble(txtTiempo.Text));

                if (fechamayor < fechaNaviera)
                {
                    mensajeVentana("La fecha de zarpe, no puede ser menor que la fecha de cierre de la naviera.");
                }
                else
                {
                    txtArribo.Text = Convert.ToString(sumaFecha.ToShortDateString());
                }

                //Evalua que el tipo de dato sea una fecha
                string estZarpe;
                estZarpe = txtEstZarpe.Text;
                DateTime validaFecha;

                if (!String.IsNullOrEmpty(txtEstZarpe.Text))
                {
                    if (DateTime.TryParse(estZarpe, out validaFecha))
                    {
                    }
                    else
                    {
                        mensajeVentana("Debe selecionar una fecha desde el calendario");
                        txtEstZarpe.Focus();
                        txtEstZarpe.Text = "";
                    }
                }
            }


        }

        protected void btnAdicionar_Click(object sender, EventArgs e)
        {
            int guardar = ctrlitinerarios.IngresarItinerario(Convert.ToInt32(cboPaisDestino.SelectedValue), Convert.ToInt32(cboPuertoZarpe.SelectedValue),
               Convert.ToInt32(cboDescargue.SelectedValue), Convert.ToInt32(txtTiempo.Text), Convert.ToInt32(cboNaviera.SelectedValue),
               txt20ST.Text, txt40HC.Text, txtBuque.Text, txtFecCargue.Text, txtCierreNav.Text,
               txtEstZarpe.Text, txtArribo.Text, txtRepresentante.Text);

            mensajeVentana("Ingreso satisfactorio del itinerario.");

            this.CargarReporteItinerario();
        }

        protected void btnGuardarMaestro_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtDetalle.Text))
            {
                if (btnGuardarMaestro.Text == "Guardar")
                {
                    int bandera = (int)Session["bandera"];
                    if (bandera == 1 || bandera == 3)
                    {
                        txtDetalle.Text = txtDetalle.Text.ToUpperInvariant();

                        int ing = ctrlitinerarios.IngresarPuerto(txtDetalle.Text, txtRepresentante.Text);
                        lblMSJMaestro.Visible = true;
                        lblMSJMaestro.Text = "PUERTO INGRESADO CORRECTAMENTE.";
                        txtDetalle.Text = "";

                        cboPuertoZarpe.Items.Clear();
                        cboDescargue.Items.Clear();
                        cboPuertoZarpe.Items.Add("Seleccione El Puerto");
                        cboDescargue.Items.Add("Seleccione El Puerto");
                        ctrlitinerarios.Listar_Puertos(cboDescargue);
                        ctrlitinerarios.Listar_Puertos(cboPuertoZarpe);
                    }
                    if (bandera == 2)
                    {
                        txtDetalle.Text = txtDetalle.Text.ToUpperInvariant();

                        int ing = ctrlitinerarios.IngresarNaviera(txtDetalle.Text, txtRepresentante.Text);
                        lblMSJMaestro.Visible = true;
                        lblMSJMaestro.Text = "NAVIERA INGRESADA CORRECTAMENTE.";
                        txtDetalle.Text = "";

                        cboNaviera.Items.Clear();
                        cboNaviera.Items.Add("Seleccione La Naviera");
                        ctrlitinerarios.Listar_Naviera(cboNaviera);

                    }
                    else
                    {
                        lblMSJMaestro.Text = "Debe establecer una descripcion.";
                        txtDetalle.Focus();
                    }
                }

                if (btnGuardarMaestro.Text == "Actualizar")
                {
                    int bandera = (int)Session["bandera"];
                    if (bandera == 1 || bandera == 3)
                    {
                        txtDetalle.Text = txtDetalle.Text.ToUpperInvariant();

                        int ing = ctrlitinerarios.actualizarPuerto(Convert.ToInt32(cboPuertoZarpe.SelectedItem.Value), txtDetalle.Text, txtRepresentante.Text);
                        lblMSJMaestro.Visible = true;
                        lblMSJMaestro.Text = "PUERTO ACTUALIZADO CORRECTAMENTE.";
                        txtDetalle.Text = "";

                        cboPuertoZarpe.Items.Clear();
                        cboDescargue.Items.Clear();
                        cboPuertoZarpe.Items.Add("Seleccione El Puerto");
                        cboDescargue.Items.Add("Seleccione El Puerto");
                        ctrlitinerarios.Listar_Puertos(cboDescargue);
                        ctrlitinerarios.Listar_Puertos(cboPuertoZarpe);

                    }
                    if (bandera == 2)
                    {
                        txtDetalle.Text = txtDetalle.Text.ToUpperInvariant();

                        int ing = ctrlitinerarios.actualizarNaviera(Convert.ToInt32(cboNaviera.SelectedItem.Value), txtDetalle.Text, txtRepresentante.Text);
                        lblMSJMaestro.Visible = true;
                        lblMSJMaestro.Text = "NAVIERA ACTUALIZADA CORRECTAMENTE.";
                        txtDetalle.Text = "";

                        cboNaviera.Items.Clear();
                        cboNaviera.Items.Add("Seleccione La Naviera");
                        ctrlitinerarios.Listar_Naviera(cboNaviera);
                    }
                }
            }
            else
            {
                lblMSJMaestro.Visible = true;
                lblMSJMaestro.Text = "EL CAMPO NO PUEDE ESTAR VACIO";
                txtDetalle.Focus();
            }
         
        }

        protected void txtFecCargue_TextChanged(object sender, EventArgs e)
        {
            string fecCargue;
            fecCargue = txtFecCargue.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txtFecCargue.Text))
            {
                if (DateTime.TryParse(fecCargue, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario");
                   txtFecCargue.Focus();
                   txtFecCargue.Text = "";
                }
            }
        }

        protected void txtArribo_TextChanged(object sender, EventArgs e)
        {
            //Evalua que el tipo de dato sea una fecha
            string estArribo;
            estArribo = txtArribo.Text;
            DateTime validaFecha;

            if (!String.IsNullOrEmpty(txtArribo.Text))
            {
                if (DateTime.TryParse(estArribo, out validaFecha))
                {
                }
                else
                {
                    mensajeVentana("Debe selecionar una fecha desde el calendario");
                    txtArribo.Focus();
                    txtArribo.Text = "";
                }
            }
        }



    }
}