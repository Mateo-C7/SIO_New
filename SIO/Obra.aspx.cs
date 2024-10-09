using System;
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
using CapaControl;
using CapaDatos;
using System.Drawing;
using AjaxControlToolkit;



namespace SIO
{
    public partial class Obra : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private DataSet dsObra = new DataSet();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlObra contobra = new ControlObra();
        public ControlCliente concli = new ControlCliente();
        public ControlContacto controlCont = new ControlContacto();
    
        
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblComentario.Visible = false;
            //txtComentario.Visible = false;
            //Chk_EstadoObra.Visible = false;
            //LblEstadoObra.Visible = false; 
            //cbo_Origen.Enabled = false;
            //cbo_Fuente.Enabled = false;
            //InhabilitarFuenteObraPorRol();    
            //LblTipoProyecto.Visible = false;
          
           

            if (!IsPostBack)
            {
                if (Session["Rol"] != null)
                {                  
                    // verifico si el usuario puede eliminar                    
                    int idClienteUsuario = Convert.ToInt32(Session["IdClienteUsuario"]);
                    if (idClienteUsuario > 0) btn_AnularObra.Visible = false;

                    cbo_EmpreCompete.Visible = false;
                    lbl_EmpreCompete.Visible = false;
                    txt_Link_Evernot.Visible = false;
                    lbl_Evernot.Visible = false;

                //this.Idioma();
                cargarTipoSeguimiento(0);

                string rcID = (string)Session["rcID"];
                int rol = (int)Session["Rol"];
                string pais = (string)Session["pais"];

                if ((rol == 3) || (rol == 28) || (rol == 29))
                {
                    this.PoblarListaPais();                    
                }
                else
                {
                    this.PoblarListaPais2();
                }

                this.PoblarListaPaisObra();
                //this.PoblarListaTecnicos();
                this.PoblarEstadoSocioEconomico();
                //this.PoblarEstadoObra();
                this.PoblarTipoVivienda();

                //==stiven palacios========================================
                cbo_Fuente.Items.Clear();
                cbo_Origen.Items.Clear();
                contobra.LlenarComboOrigen(cbo_Origen);
                contobra.LlenarComboFuente(cbo_Fuente, Convert.ToInt32(cbo_Origen.SelectedItem.Value));
                ToolTip_Fuente();
                    if (chk_PotenForsa.Checked == true)
                    {
                        LblVenta.Text = "1";
                        LblAlquiler.Text = "0";
                        LblDesarrollo.Text = "0";
                        chk_PotenForsa.Checked = true;
                        chk_PotenArrenda.Checked = false;
                        chk_PotencialNewDesarrollo.Checked = false;
                    }
                    else if (chk_PotenArrenda.Checked == true)
                    {
                        LblVenta.Text = "0";
                        LblAlquiler.Text = "1";
                        LblDesarrollo.Text = "0";
                        chk_PotenForsa.Checked = false;
                        chk_PotenArrenda.Checked = true;
                        chk_PotencialNewDesarrollo.Checked = false;
                    }
                    else if (chk_PotencialNewDesarrollo.Checked == true)
                    {
                        LblVenta.Text = "0";
                        LblAlquiler.Text = "0";
                        LblDesarrollo.Text = "1";
                        chk_PotenForsa.Checked = false;
                        chk_PotenArrenda.Checked = false;
                        chk_PotencialNewDesarrollo.Checked = true;
                    }
                    else
                    {
                        LblVenta.Text = "1";
                        LblAlquiler.Text = "0";
                        LblDesarrollo.Text = "0";
                    }
                    //=========================================================
                    cbo_Segmento.Items.Clear();       
                contobra.LlenarComboSegmento(cbo_Segmento);
                cbo_TipoSegmento.Items.Clear();
                contobra.LlenarComboSegmentoTipo(cbo_TipoSegmento, Convert.ToInt32(cbo_Segmento.SelectedItem.Value));

                if (Request.QueryString["idCliente"] != null)
                {
                    string cliente = Request.QueryString["idCliente"];
                    Session["Cliente"] = cliente;

                    this.cargarDatosCliente();
                    //this.cargarReporteContactoXId();
                }
                else
                {
                    ImageButton1.Visible = true;
                }

                if (Request.QueryString["idObra"] != null)
                {
                    string obra = Request.QueryString["idObra"];
                    Session["Obra"] = obra;
                    this.PoblarObra();
                   // CargarSeguimientoObra();
                    InhabilitarFuenteObraPorRol();
                    //GridView_DetalleSeguimiento.Columns[5].Visible = false;
                    //this.ReporteObra();
                
                }
                else
                {
                    InhabilitarFuenteObraPorRol();
                    //List<ReportParameter> parametro = new List<ReportParameter>();

                    //parametro.Add(new ReportParameter("idrepresentante", rcID, true));
                    //parametro.Add(new ReportParameter("rol", rol.ToString(), true));
                    //parametro.Add(new ReportParameter("pais", pais, true));

                    //reportobra.Width = 1150;
                    //reportobra.Height = 1050;
                    //reportobra.ProcessingMode = ProcessingMode.Remote;
                    //IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
                    //reportobra.ServerReport.ReportServerCredentials = irsc;

                    //reportobra.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
                    //reportobra.ServerReport.ReportPath = "/InformesFUP/COM_ObrasXRol";
                    //this.reportobra.ServerReport.SetParameters(parametro);
                }

                if (Request.QueryString["na"] != null)
                {
                    string na = Request.QueryString["na"];
                    if (na == "1")
                    {
                        cargarTipoSeguimiento(1);                        
                        //cboEstado.SelectedValue = "6";
                        //cboEstado_SelectedIndexChanged( sender,  e);
                        txtObservacion.BackColor = Color.Yellow;
                        txtObservacion.Focus();
                    }
                    else if (na == "2")
                    {
                        cargarTipoSeguimiento(2);
                        //cboEstado.SelectedValue = "6";
                        //cboEstado_SelectedIndexChanged( sender,  e);                  
                        txtObservacion.Focus();
                    }
                }
                }
                else
                    {
                        string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        Response.Redirect("Inicio.aspx");
                    }

                } // termina postb
        }

        private void cargarDatosCliente()
        {
            string cliente = (string)Session["Cliente"];

            reader = controlCont.consultarCliente(Convert.ToInt32(cliente));
            if (reader.HasRows == true)
            {
                reader.Read();
                CboPaisMatriz.Items.Clear();
                CboPaisMatriz.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(1).ToString()));
                cboCiudadMatriz.Items.Clear();
                cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(4), reader.GetInt32(3).ToString()));
                cboClienteMatriz.Items.Clear();
                cboClienteMatriz.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(5).ToString()));
                lblObrap.Text = cboClienteMatriz.SelectedItem.Text;
            }
            reader.Close();
            controlCont.cerrarConexion();
        }

        private void cargarTipoSeguimiento(int na)
        {
            string idioma = (string)Session["Idioma"];
            cboTipoSeg.Items.Clear();

            cboTipoSeg.Items.Add(new ListItem("Seleccione", "0"));

            reader = contobra.PoblarTipoSeguimiento(na);

            if (reader != null)
            {
                while (reader.Read())
                {
                    cboTipoSeg.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }            
            reader.Close();
            contobra.cerrarConexion();
        }

        private void InhabilitarFuenteObraPorRol()
        {
            int rol = (int)Session["Rol"];
            //18-10-2017==============================================
            if (btnGuardar.Text == "Actualizar" && rol != 5 || (rol != 9))
            {
                lblComentario.Visible = false;
                txtComentario.Visible = false;
                Chk_EstadoObra.Visible = false;
                LblEstadoObra.Visible = false;
                cbo_Origen.Enabled = false;
                cbo_Fuente.Enabled = false;
            }
            {
            }
            if (btnGuardar.Text == "Guardar" && rol != 5 || (rol != 9))
            {
                lblComentario.Visible = false;
                txtComentario.Visible = false;
                Chk_EstadoObra.Visible = false;
                LblEstadoObra.Visible = false;
                cbo_Origen.Enabled = false;
                cbo_Fuente.Enabled = false;
            }
            {
            }

            if (btnGuardar.Text == "Actualizar" && rol == 5 || (rol == 9) || (rol == 46))
            {
                Permitir_Visibilidad_Campos();
            }
            {
            }
            if (btnGuardar.Text == "Guardar" && rol == 5 || (rol == 9) || (rol == 46))
            {
                lblComentario.Visible = true;
                txtComentario.Visible = true;
                Chk_EstadoObra.Visible = true;
                LblEstadoObra.Visible = true;
                cbo_Origen.Enabled = true;
                cbo_Fuente.Enabled = true;
            }
            {
            }
        }
        public void Permitir_Visibilidad_Campos()
        {
            int rol = (int)Session["Rol"];
            if (btnGuardar.Text == "Actualizar" && rol == 5 || (rol == 9) || (rol == 46))
            {
                cbo_Origen.Enabled = true;
                cbo_Fuente.Enabled = true;
                lblComentario.Visible = true;
                txtComentario.Visible = true;
                Chk_EstadoObra.Visible = true;
                LblEstadoObra.Visible = true;        
                //CargarSeguimientoObra();
                //GridView_DetalleSeguimiento.Columns[5].Visible = true;
            }
        }
        private void mostrarSeguimiento()
        {
            btnAdicionarSeg.Visible = true;
            lbl_TipoSeg.Visible = true;
            cboTipoSeg.Visible = true;
            lbl_Comentario.Visible = true;
            txtObservacion.Visible = true;
            txt_Link_Evernot.Visible = true;
            lbl_Evernot.Visible = true;
        }

        private void PoblarObra()
        {
            string obra = (string)Session["Obra"];
            this.LimpiarObra();
            string idioma = (string)Session["Idioma"];
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            this.CargarReporteObra("Obra");
            mostrarSeguimiento();
          

            if (idioma == "Español")
                btnGuardar.Text = "Actualizar";
            if (idioma == "Ingles")
                btnGuardar.Text = "Update";
            if (idioma == "Portugues")
                btnGuardar.Text = "Atualizar";

            reader = contobra.ConsultarObra(Convert.ToInt32(obra));
            if (reader.HasRows == true)
            {
                reader.Read();
                cboPais.Items.Clear();
                cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                cboCiudad.Items.Clear();
                cboCiudad.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(2).ToString()));
                obr_nombre.Text = reader.GetValue(4).ToString();
                obr_direccion.Text = reader.GetValue(5).ToString();
                obr_telef.Text = reader.GetValue(6).ToString();
                obr_telef2.Text = reader.GetValue(7).ToString();
                txtprefijo1.Text = reader.GetValue(21).ToString();
                txtprefijo2.Text = reader.GetValue(22).ToString();
                txtFechaInicio.Text = reader.GetSqlDateTime(23).Value.ToString("dd/MM/yyyy");
                if (txtFechaInicio.Text == "01/01/1900") txtFechaInicio.Text = "";
                txtFechafin.Text = reader.GetSqlDateTime(24).Value.ToString("dd/MM/yyyy");
                if (txtFechafin.Text == "01/01/1900") txtFechafin.Text = "";
                // Cbo_Estado.Items.Add(new ListItem(reader.GetString(26), reader.GetInt32(25).ToString()));
                lblEstObra.Text = reader.GetValue(25).ToString();


                //cboEstado.SelectedValue = reader.GetInt32(25).ToString();
                //cboTecnico.Items.Add(new ListItem(reader.GetString(28), reader.GetInt32(27).ToString()));
                //cboTecnico.SelectedValue = reader.GetInt32(27).ToString();
                txtUsuarioAct0.Text = reader.GetValue(29).ToString();
                txtFechaAct0.Text = reader.GetSqlDateTime(30).Value.ToString("dd/MM/yyyy");
                txtComentario.Text = reader.GetValue(31).ToString();
                //==stiven palacios========================================
                cbo_Fuente.Items.Clear();
                cbo_Origen.Items.Clear();
                contobra.LlenarComboOrigen(cbo_Origen);
                cbo_Origen.Text = reader.GetValue(35).ToString();
                contobra.LlenarComboFuente(cbo_Fuente, Convert.ToInt32(cbo_Origen.SelectedItem.Value));
                cbo_Fuente.Text = reader.GetValue(32).ToString();

                cbo_Segmento.Items.Clear();
                contobra.LlenarComboSegmento(cbo_Segmento);
                cbo_Segmento.Text = reader.GetValue(38).ToString();
                cbo_TipoSegmento.Items.Clear();
                contobra.LlenarComboSegmentoTipo(cbo_TipoSegmento, Convert.ToInt32(cbo_Segmento.SelectedItem.Value));
                cbo_TipoSegmento.Text = reader.GetValue(39).ToString();


                if (rol == 1 || rol == 5 || rol == 9)
                {
                    btn_AnularObra.Visible = false;  //06/10/2017  
                }
                {
                }
                //=========================================================         
                lblLink.NavigateUrl = "javascript: window.open('" + reader.GetValue(33).ToString() + "')";
                lblDescripcion.Text = reader.GetValue(34).ToString();
                //if (lblDescripcion.Text != "") lblDescripcion.BackColor = Color.Yellow; else lblDescripcion.BackColor = Color.Transparent;
                if (txtFechaAct0.Text == "01/01/1900") txtFechaAct0.Text = "";
                //Response.Redirect("Cliente.aspx?idCliente=" + reader.GetInt32(15).ToString() + "&Sucursal=1");
                //HyperLink2.NavigateUrl = "javascript: window.open('Cliente.aspx?idCliente=" + reader.GetInt32(15).ToString() + "&Sucursal=1')";
                Session["ClienteObra"] = reader.GetInt32(15).ToString();
                //Response.Redirect("Cliente.aspx?idCliente=" + Session["Cliente"].ToString());
                cboEstrato.Items.Clear();
                HyperLink2.Visible = true;
                if (idioma == "Español")
                {
                    cboEstrato.Items.Add(new ListItem(reader.GetString(9), reader.GetInt32(8).ToString()));
                }
                if (idioma == "Ingles")
                {
                    cboEstrato.Items.Add(new ListItem(reader.GetString(10), reader.GetInt32(8).ToString()));
                }
                if (idioma == "Portugues")
                {
                    cboEstrato.Items.Add(new ListItem(reader.GetString(11), reader.GetInt32(8).ToString()));
                }

                cboVivienda.Items.Clear();
                contobra.LlenarComboTipoVivienda(cboVivienda);
                cboVivienda.Text = reader.GetValue(12).ToString();

                txtM2.Text = reader.GetValue(14).ToString();
                txtUnidades.Text = reader.GetValue(13).ToString();
                obr_direccion.Text = reader.GetValue(5).ToString();
                CboPaisMatriz.Items.Clear();
                CboPaisMatriz.Items.Add(new ListItem(reader.GetString(18), reader.GetInt32(17).ToString()));
                cboCiudadMatriz.Items.Clear();
                cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(20), reader.GetInt32(19).ToString()));
                cboClienteMatriz.Items.Clear();
                cboClienteMatriz.Items.Add(new ListItem(reader.GetString(16), reader.GetInt32(15).ToString()));
                string venta = reader.GetValue(37).ToString();
                string Alquiler = reader.GetValue(36).ToString();
                string n_Desarrollo = reader.GetValue(40).ToString();              
                if (venta == "True")
                {
                    chk_PotenForsa.Checked = true;
                    LblVenta.Text = "1";
                    LblAlquiler.Text = "0";
                    LblDesarrollo.Text = "0";
                }
                else if (Alquiler == "True")
                {
                    chk_PotenArrenda.Checked = true;
                    LblVenta.Text = "0";
                    LblAlquiler.Text = "1";
                    LblDesarrollo.Text = "0";
                }
                else if (n_Desarrollo == "True")
                {
                    chk_PotencialNewDesarrollo.Checked = true;
                    LblVenta.Text = "0";
                    LblAlquiler.Text = "0";
                    LblDesarrollo.Text = "1";
                }
            }

            reader.Close();
            contobra.cerrarConexion();


            //CARGAMOS LOS PAISES
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

            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            contubi.cerrarConexion();

            //CARGAMOS ESTRATO
            reader = contobra.PoblarEstadoSocioEconomico();
            if (reader.HasRows == true)
            {
                if (idioma == "Español")
                {
                    cboEstrato.Items.Add("Seleccione El Estrato");
                    while (reader.Read())
                    {

                        cboEstrato.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                if (idioma == "Ingles")
                {
                    cboEstrato.Items.Add("Select The Status");
                    while (reader.Read())
                    {

                        cboEstrato.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                    }
                }
                if (idioma == "Portugues")
                {
                    cboEstrato.Items.Add("Selecione O Estrato");
                    while (reader.Read())
                    {

                        cboEstrato.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                    }
                }
            }

            reader.Close();
            contobra.cerrarConexion();

            ////CARGAMOS EL TIPO DE VIVIENDA

            //reader = contobra.PoblarTipoViviendaObra();
            //if (reader != null)
            //{
            //    while (reader.Read())
            //    {
            //        cboVivienda.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0)));
            //    }
            //}
            //reader.Close();

            ////if (idioma == "Español")
            ////{
            ////    cboVivienda.Items.Add("Seleccione");
            ////    cboVivienda.Items.Add("Apartamento");
            ////    cboVivienda.Items.Add("Casa");
            ////    cboVivienda.Items.Add("Hotel");
            ////    cboVivienda.Items.Add("Carcel");
            ////    cboVivienda.Items.Add("Otro");
            ////}


            //CARGAMOS EL PAIS DEL CLIENTE
            if ((rol == 3) || (rol == 28) || (rol == 29))
            {
                reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));                
                if (idioma == "Español")
                {
                    CboPaisMatriz.Items.Add("Seleccione El Pais");
                }
                if (idioma == "Ingles")
                {
                    CboPaisMatriz.Items.Add("Select The Country");
                }
                if (idioma == "Portugues")
                {
                    CboPaisMatriz.Items.Add("Selecione O País");
                }

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        CboPaisMatriz.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
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
            else
            {
                reader = contubi.poblarListaPais();
                if (idioma == "Español")
                {
                    CboPaisMatriz.Items.Add("Seleccione El Pais");
                }
                if (idioma == "Ingles")
                {
                    CboPaisMatriz.Items.Add("Select The Country");
                }
                if (idioma == "Portugues")
                {
                    CboPaisMatriz.Items.Add("Selecione O País");
                }

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        CboPaisMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }

                reader.Close();
                contubi.cerrarConexion();
            }
        }

        private void LimpiarObra()
        {

        }

        //private void ReporteObra()
        //{
        //    string rcID = (string)Session["rcID"];
        //    int arRol = (int)Session["Rol"];
        //    string pais = (string)Session["pais"];
        //    List<ReportParameter> parametro = new List<ReportParameter>();

        //    parametro.Add(new ReportParameter("idPais", cboPais.SelectedItem.Value, true));
        //    //if (cboCiudad.SelectedItem.Text == "Seleccione La Ciudad" || cboCiudad.SelectedItem.Text == "Select The City" ||
        //    //        cboCiudad.SelectedItem.Text == "A Cidade")
        //    //{
        //    //    parametro.Add(new ReportParameter("idCiudad", "0", true));
        //    //}
        //    //else
        //    //{
        //    //    parametro.Add(new ReportParameter("idCiudad", cboCiudad.SelectedItem.Value, true));
        //    //}

        //    reportobra.Width = 1150;
        //    reportobra.Height = 1050;
        //    reportobra.ProcessingMode = ProcessingMode.Remote;
        //    IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
        //    reportobra.ServerReport.ReportServerCredentials = irsc;

        //    reportobra.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
        //    reportobra.ServerReport.ReportPath = "/InformesFUP/COM_ObrasXId";
        //    this.reportobra.ServerReport.SetParameters(parametro);
        //}

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

            dsObra = contobra.ConsultarIdiomaObra();

            foreach (DataRow fila in dsObra.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    lblPais.Text = fila[idiomaId].ToString();
                if (posicion == 2)
                    lblCiudad.Text = fila[idiomaId].ToString();
                if (posicion == 3)
                    lblObra.Text = fila[idiomaId].ToString();
                if (posicion == 4)
                    lblDireccion.Text = fila[idiomaId].ToString();
                if (posicion == 5)
                    lblTelefono.Text = fila[idiomaId].ToString();
                if (posicion == 6)
                    lblTeléfono2.Text = fila[idiomaId].ToString();
                if (posicion == 7)
                    lblEstrato.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    lblVivienda.Text = fila[idiomaId].ToString();
                if (posicion == 9)
                    lblUnd.Text = fila[idiomaId].ToString();
                if (posicion == 10)
                    lblM2.Text = fila[idiomaId].ToString();
                               
                if (posicion == 13)
                    lblPaisCliente.Text = fila[idiomaId].ToString();
                if (posicion == 14)
                    lblCiudadObra.Text = fila[idiomaId].ToString();
                if (posicion == 15)
                    lblCliente.Text = fila[idiomaId].ToString();
                if (posicion == 16)
                    btnGuardar.Text = fila[idiomaId].ToString();
                if (posicion == 17)
                    btnNuevo.Text = fila[idiomaId].ToString();               
                
            }
            dsObra.Tables.Remove("Table");
            dsObra.Dispose();
            dsObra.Clear();
        }    

        protected void CboPaisMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarCiudadMatriz();
            //Prefijo();
        }

        private void Prefijo()
        {
            reader = concli.ObtenerPrefijo(Convert.ToInt32(cboPais.SelectedItem.Value));

            if (reader.Read() == true)
            {
                preTel.Text = reader.GetValue(1).ToString();
                preTel2.Text = preTel.Text;
            }
            else
            {
                preTel.Text = "0";
                preTel2.Text = preTel.Text;
            }

            
            reader.Close();
            concli.CerrarConexion();

        }

        private void PoblarCiudadMatriz()
        {
            int rol = (int)Session["Rol"];
            string rcID = (string)Session["rcID"];
            string idioma = (string)Session["Idioma"];

            cboCiudadMatriz.Items.Clear();
            if (idioma == "Español")
            {
                cboCiudadMatriz.Items.Add("Seleccione La Ciudad");
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatriz.Items.Add("Select The City");
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatriz.Items.Add("Selecione A Cidade");
            }

            if ((rol == 3) && (Convert.ToInt32(CboPaisMatriz.SelectedItem.Value) == 8))
            {
                reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                reader.Close();
                contubi.cerrarConexion();
            }
            else
            {
                reader = contubi.poblarListaCiudades(Convert.ToInt32(CboPaisMatriz.SelectedItem.Value));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }

                reader.Close();
                contubi.cerrarConexion();
            }
        }

        protected void cboCiudadMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarClienteMatriz();
        }

        private void PoblarClienteMatriz()
        {
            string idioma = (string)Session["Idioma"];

            cboClienteMatriz.Items.Clear();
            reader = concli.ConsultarDatosCliente(Convert.ToInt32(cboCiudadMatriz.SelectedItem.Value),0);
            if (idioma == "Español")
            {
                cboClienteMatriz.Items.Add("Seleccione La Empresa Matriz");
            }
            if (idioma == "Ingles")
            {
                cboClienteMatriz.Items.Add("Select Company Matrix");
            }
            if (idioma == "Portugues")
            {
                cboClienteMatriz.Items.Add("Selecione Companhia Matriz");
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            string Representante = (string)Session["Nombre_Usuario"];
            string idioma = (string)Session["Idioma"];
            int estaObrActiva = 1;//stivnpalacios 27/09/2017----------
            string PeriodoSim = DateTime.Now.ToString();


            //Colocamos la información de los campos en mayuscula
            obr_nombre.Text = obr_nombre.Text.ToString().ToUpper();
            obr_direccion.Text = obr_direccion.Text.ToString().ToUpper();
            obr_telef.Text = obr_telef.Text.ToString().ToUpper();
            obr_telef2.Text = obr_telef2.Text.ToString().ToUpper();
            //btnGuardar.Enabled = false;

            int vValido = 1;
           

            // VALIDAR CAMPOS REQUERIDOS
            if (cboPais.SelectedIndex == -1) { vValido = 0; }
            else if (cboPais.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cboCiudad.SelectedIndex == -1) { vValido = 0; }
            else if (cboCiudad.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cboEstrato.SelectedIndex == -1) { vValido = 0; }
            else if (cboEstrato.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cboVivienda.SelectedIndex == -1) { vValido = 0; }
            else if (cboVivienda.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (CboPaisMatriz.SelectedIndex == -1) { vValido = 0; }
            else if (CboPaisMatriz.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cboCiudadMatriz.SelectedIndex == -1) { vValido = 0; }
            else if (cboCiudadMatriz.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            //==stiven palacios========================================
            //if (cbo_Origen.SelectedIndex == -1) { vValido = 0; }
            //else if (cbo_Origen.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            if (cbo_Fuente.SelectedIndex == -1) { vValido = 0; }
            else if (cbo_Fuente.SelectedItem.Value.ToString() == "0") { vValido = 0; }
            //=========================================================
            if (cboClienteMatriz.SelectedIndex == -1) { vValido = 0; }
            else if (cboClienteMatriz.SelectedItem.Value.ToString() == "0") { vValido = 0; }
                    
            if (vValido == 0)
            {
                MensajeAlerta();
            }
            else
            {
                if (chk_PotenForsa.Checked == true)
                {
                    LblVenta.Text = "1";
                    LblAlquiler.Text = "0";
                    LblDesarrollo.Text = "0";
                }
                else if (chk_PotenArrenda.Checked == true)
                {
                    LblVenta.Text = "0";
                    LblAlquiler.Text = "1";
                    LblDesarrollo.Text = "0";
                }
                else if (chk_PotencialNewDesarrollo.Checked == true)
                {
                    LblVenta.Text = "0";
                    LblAlquiler.Text = "0";
                    LblDesarrollo.Text = "1";
                }

                if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
                {
                    string id = (string)Session["Obra"];

             
                    //Guarda el Log de actualizacion de la tabla obra
                    string tabla = "Obra";
                    String fecha = DateTime.Now.ToShortDateString();
                    string evento = "Actualizacion";
                    string usuarioMod = (string)Session["Usuario"];
                    //Definimos variables para almacnarlos nombres de los campos
                    string nombre = "", direccion = "", Pais = "", Ciudad = "", prefijo1 = "", telef = "",
                        prefijo2 = "", telef2 = "", M2 = "", Unidades = "", TipoVivi = "", estrato = "",
                        Origen = "", Fuente = "", FechaInicio = "", Fechafin = "", Estado = "", Comentario = "",
                        Tecnico = "";

                    reader = contobra.ConsultarObra(Convert.ToInt32(id));
                    if (reader.HasRows == true)
                    {
                        reader.Read();

                        nombre = reader.GetValue(4).ToString();
                        string nameNomb = "obr_nombre";
                        direccion = reader.GetValue(5).ToString();
                        string nameDire = "obr_direccion";
                        Pais = reader.GetValue(1).ToString();
                        string namePais = "obr_pai_id";
                        Ciudad = reader.GetValue(3).ToString();
                        string nameCiud = "obr_ciu_id";
                        prefijo1 = reader.GetValue(21).ToString();
                        string namePref1 = "obr_prefijo1";
                        telef = reader.GetValue(6).ToString();
                        string nameTele = "obr_telef";
                        prefijo2 = reader.GetValue(22).ToString();
                        string namePref2 = "obr_prefijo2";
                        telef2 = reader.GetValue(7).ToString();
                        string nameTele2 = "obr_telef2";
                        M2 = reader.GetValue(14).ToString();
                        string nameM2 = "obr_m2_vivienda";
                        Unidades = reader.GetValue(13).ToString();
                        string nameUnid = "obr_total_unidades";
                        TipoVivi = reader.GetValue(12).ToString();
                        string nameTipVivi = "obr_tipo_vivienda";
                        estrato = reader.GetValue(9).ToString();
                        string nameEstra = "obr_ese_id";
                        Origen = reader.GetValue(35).ToString();
                        string nameOrig = "lite_tipo_fuente.tifuente_origen_id";
                        Fuente = reader.GetValue(32).ToString();
                        string nameFuente = "obr_fuente_id";
                        FechaInicio = reader.GetSqlDateTime(23).Value.ToString("dd/MM/yyyy");
                        if (FechaInicio == "01/01/1900") FechaInicio = "";
                        string nameFecini = "obr_fecha_ini";
                        Fechafin = reader.GetSqlDateTime(24).Value.ToString("dd/MM/yyyy");
                        if (Fechafin == "01/01/1900") Fechafin = "";
                        string nameFecFin = "obr_fecha_Fin";
                        Estado = reader.GetValue(26).ToString();
                        string nameEsta = "obr_estado";
                        Comentario = reader.GetValue(31).ToString();
                        string nameCome = "obr_comentario";
                        Tecnico = reader.GetValue(27).ToString();
                        string nameTecn = "obr_tecnico";



                        if (nombre != obr_nombre.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameNomb, usuarioMod, fecha, nombre, obr_nombre.Text, evento);
                        }
                        else { }
                        if (direccion != obr_direccion.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameDire, usuarioMod, fecha, direccion, obr_direccion.Text, evento);
                        }
                        else { }
                        if (Pais != cboPais.SelectedItem.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), namePais, usuarioMod, fecha, Pais, cboPais.SelectedItem.ToString(), evento);
                        }
                        else { }
                        if (Ciudad != cboCiudad.SelectedItem.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameCiud, usuarioMod, fecha, Ciudad, cboCiudad.SelectedItem.ToString(), evento);
                        }
                        else { }
                        if (prefijo1 != txtprefijo1.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), namePref1, usuarioMod, fecha, prefijo1, txtprefijo1.Text, evento);
                        }
                        else { }
                        if (telef != obr_telef.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameTele, usuarioMod, fecha, telef, obr_telef.Text, evento);
                        }
                        else { }
                        if (prefijo2 != txtprefijo2.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), namePref2, usuarioMod, fecha, prefijo2, txtprefijo2.Text, evento);
                        }
                        else { }
                        if (telef2 != obr_telef2.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameTele2, usuarioMod, fecha, telef2, obr_telef2.Text, evento);
                        }
                        else { }
                        if (M2 != txtM2.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameM2, usuarioMod, fecha, M2, txtM2.Text, evento);
                        }
                        else { }
                        if (Unidades != txtUnidades.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameUnid, usuarioMod, fecha, Unidades, txtUnidades.Text, evento);
                        }
                        else { }
                        if (estrato != cboEstrato.SelectedItem.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameEstra, usuarioMod, fecha, estrato, cboEstrato.SelectedItem.ToString(), evento);
                        }
                        else { }
                        if (TipoVivi != cboVivienda.SelectedValue.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameTipVivi, usuarioMod, fecha, TipoVivi, cboVivienda.SelectedValue.ToString(), evento);
                        }
                        else { }
                        if (Origen != cbo_Origen.SelectedValue.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameOrig, usuarioMod, fecha, Origen, cbo_Origen.SelectedValue.ToString(), evento);
                        }
                        else { }
                        if (Fuente != cbo_Fuente.SelectedValue.ToString())
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameFuente, usuarioMod, fecha, Fuente, cbo_Fuente.SelectedValue.ToString(), evento);
                        }
                        else { }
                        if (FechaInicio != txtFechaInicio.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameFecini, usuarioMod, fecha, FechaInicio, txtFechaInicio.Text, evento);
                        }
                        else { }
                        if (Fechafin != txtFechafin.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameFecFin, usuarioMod, fecha, Fechafin, txtFechafin.Text, evento);
                        }
                        else { }

                        if (Comentario != txtComentario.Text)
                        {
                            contobra.Met_Insertar_Log_ActualizaObra(tabla, int.Parse(id), nameCome, usuarioMod, fecha, Comentario, txtComentario.Text, evento);
                        }
                        else { }

                        //===============================================

                        if (Chk_EstadoObra.Checked == true)
                        {
                            lblEstObra.Text = "1";
                        }
                        else
                        {
                            lblEstObra.Text = "0";
                        }                       
                        int actualizar = contobra.ActualizarObra(Convert.ToInt32(id), Convert.ToInt32(cboPais.SelectedValue),
                            Convert.ToInt32(cboCiudad.SelectedValue), obr_nombre.Text, obr_direccion.Text, obr_telef.Text, obr_telef2.Text,
                            Convert.ToInt32(cboEstrato.SelectedValue), Convert.ToInt32(cboVivienda.SelectedValue),
                            Convert.ToInt32(txtUnidades.Text), Convert.ToDecimal(txtM2.Text), Convert.ToInt32(cboClienteMatriz.SelectedValue),
                            Representante, txtprefijo1.Text, txtprefijo2.Text, txtFechaInicio.Text, txtFechafin.Text,
                            0, txtComentario.Text, Convert.ToInt32(cbo_Fuente.SelectedItem.Value), Convert.ToInt32(lblEstObra.Text),
                            int.Parse(LblAlquiler.Text), int.Parse(LblVenta.Text), Convert.ToInt32(cbo_TipoSegmento.SelectedItem.Value),
                            int.Parse(LblDesarrollo.Text));

                        this.MensajeObra();
                       
                        btn_AnularObra.Visible = false;
                        mostrarSeguimiento();
                        Permitir_Visibilidad_Campos();
                        //ReporteObra();
                    }
                    reader.Close();
                    contobra.cerrarConexion();
                }
                else
                {
                    if ((btnGuardar.Text == "Guardar") || (btnGuardar.Text == "Save") || (btnGuardar.Text == "Salvar"))
                    {
                        int obra = contobra.Obra(Convert.ToInt32(cboPais.SelectedItem.Value), Convert.ToInt32(cboCiudad.SelectedItem.Value),
                                                 Convert.ToInt32(cboClienteMatriz.SelectedItem.Value), obr_nombre.Text, obr_direccion.Text, 
                                                 obr_telef.Text,obr_telef2.Text, Convert.ToInt32(txtUnidades.Text), 
                                                 Convert.ToInt32(cboEstrato.SelectedItem.Value),Convert.ToDecimal(txtM2.Text),
                                                 Convert.ToInt32( cboVivienda.SelectedItem.Value), Representante, txtprefijo1.Text, txtprefijo2.Text,
                                                 txtFechaInicio.Text, txtFechafin.Text, 1, 0, txtComentario.Text, Convert.ToInt32(cbo_Fuente.SelectedItem.Value),
                                                 estaObrActiva, PeriodoSim,Convert.ToInt32(cbo_TipoSegmento.SelectedItem.Value), int.Parse(LblAlquiler.Text),
                                                 int.Parse(LblVenta.Text), int.Parse(LblDesarrollo.Text));
                        Session["Obra"] = obra.ToString();
                        MensajeObra();
                        btnGuardar.Text = "Actualizar";
                        btn_AnularObra.Visible = false;
                        mostrarSeguimiento();
                    }
                }
            }                       
        }

        private void MensajeAlerta()
        {
            string mensaje = "";
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                mensaje = "Seleccione los datos obligatorios (*)." + (string)Session["MensajeNa"];
            }
            if (idioma == "Ingles")
            {
                mensaje = "Select the required data (*)." + (string)Session["MensajeNa"];
            }
            if (idioma == "Portugues")
            {
                mensaje = "Selecione o obrigatório (*)." + (string)Session["MensajeNa"];
            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        private void MensajeObra()
        {
            string idioma = (string)Session["Idioma"];
            string mensaje = "";

            if ((btnGuardar.Text == "Guardar") || (btnGuardar.Text == "Save") || (btnGuardar.Text == "Salvar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Obra creada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Project created successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Projeto criado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            if ((btnGuardar.Text == "Actualizar") || (btnGuardar.Text == "Update") || (btnGuardar.Text == "Atualizar"))
            {
                if (idioma == "Español")
                {
                    mensaje = "Obra actualizada satisfactoriamente.";
                }

                if (idioma == "Ingles")
                {
                    mensaje = "Project updated successfully.";
                }

                if (idioma == "Portugues")
                {
                    mensaje = "Projeto atualizado com êxito.";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private void PoblarListaPaisObra()
        {
            string rcID = (string)Session["rcID"];

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

        //private void PoblarListaTecnicos()
        //{
        //    string rcID = (string)Session["rcID"];
        //    cboTecnico.Items.Clear();

        //    reader = contubi.poblarListaTecnicos();
        //    string idioma = (string)Session["Idioma"];
        //    cboTecnico.Items.Add(new ListItem("Seleccione el Tecnico","0"));
            
        //    if (reader != null)
        //    {
        //        while (reader.Read())
        //        {
        //            cboTecnico.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //        }
        //    }           
        //    reader.Close();
        //    contubi.cerrarConexion();
        //}

        private void PoblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            CboPaisMatriz.Items.Clear();
            cboPais.Items.Clear();

            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add("Seleccione El Pais");
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add("Select The Country");
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add("Selecione O País");
                cboPais.Items.Add("Selecione O País");
            }

            if (reader != null)
            {
                while (reader.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
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

        private void PoblarListaPais2()
        {
            CboPaisMatriz.Items.Clear();
            cboPais.Items.Clear();

            reader = contubi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add("Seleccione El Pais");
                cboPais.Items.Add("Seleccione El Pais");
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add("Select The Country");
                cboPais.Items.Add("Select The Country");
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add("Selecione O País");
                cboPais.Items.Add("Selecione O País");
            }

            if (reader != null)
            {
                while (reader.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
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

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarListaCiudad();
            this.Prefijo();
        }

        private void PoblarListaCiudad()
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

            //if ((rol == 3) && (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
            //{
            //    reader = contubi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

            //    if (reader.HasRows == true)
            //    {
            //        while (reader.Read())
            //        {
            //            cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
            //        }
            //    }
            //    reader.Close();
            //}
            //else
            //{
            reader = contubi.poblarListaCiudades(Convert.ToInt32(cboPais.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCiudad.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            contubi.cerrarConexion();
            // }
        }

        private void PoblarEstadoSocioEconomico()
        {
            string idioma = (string)Session["Idioma"];

            cboEstrato.Items.Clear();
            reader = contobra.PoblarEstadoSocioEconomico();

            cboVivienda.Items.Clear();
            if (idioma == "Español")
            {
                cboEstrato.Items.Add(new ListItem ("Seleccione El Estrato","0"));
                while (reader.Read())
                {

                    cboEstrato.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            if (idioma == "Ingles")
            {
                cboEstrato.Items.Add("Select The Status");
                while (reader.Read())
                {

                    cboEstrato.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                }
            }
            if (idioma == "Portugues")
            {
                cboEstrato.Items.Add("Selecione O Estrato");
                while (reader.Read())
                {

                    cboEstrato.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            contobra.cerrarConexion();
        }

        //private void PoblarEstadoObra()
        //{
        //    cboEstado.Items.Clear();
        //    reader = contobra.PoblarEstadoObra();

        //    cboEstado.Items.Add(new ListItem("Seleccione", "0"));
        //    while (reader.Read())
        //    {

        //        cboEstado.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
        //    }

        //    reader.Close();
        //    contobra.cerrarConexion();
        //}

      
        private void PoblarTipoVivienda()
        {
            reader = contobra.PoblarTipoViviendaObra();
            if (reader != null)
            {
                while (reader.Read())
                {
                    cboVivienda.Items.Add(new ListItem(reader.GetString(1), reader.GetString(0).ToString()));
                }
            }
            reader.Close();
            contobra.cerrarConexion();

            //string idioma = (string)Session["Idioma"];
            //if (idioma == "Español")
            //{
            //    cboVivienda.Items.Add(new ListItem("Seleccione","0"));
            //    cboVivienda.Items.Add("Apartamento");
            //    cboVivienda.Items.Add("Casa");
            //    cboVivienda.Items.Add("Hotel");
            //    cboVivienda.Items.Add("Carcel");
            //    cboVivienda.Items.Add("Otro");
            //}


        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Obra.aspx?idCliente=" + Session["Cliente"].ToString());
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cliente.aspx?idCliente=" + Session["Cliente"].ToString());
        }

        //protected void cboEstado_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (cboEstado.SelectedItem.Value != "0")
        //    {
        //        txtComentario.Visible = true;
        //        lblComentario.Visible = true;
        //    }
        //    else
        //    {
        //        txtComentario.Visible = false;
        //        lblComentario.Visible = false;
        //    }
        //}

        protected void HyperLink2_Click(object sender, EventArgs e)
        {
            string clienteObra = (string)Session["ClienteObra"];
            Session["Cliente"] = clienteObra;
            string Obra = (string)Session["Obra"];
            Session["Sucursal"] = "1";

            Response.Redirect("Cliente.aspx?idCliente=" + clienteObra + "&Sucursal=1");
        }

        //==stiven palacios========================================
        protected void cbo_Origen_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbo_Fuente.Items.Clear();
            contobra.LlenarComboFuente(cbo_Fuente, int.Parse(cbo_Origen.Text));
            ToolTip_Fuente();
        }
        public void ToolTip_Fuente()
        {
            DataTable dt;
            dt = contobra.ToolTip_Fuente(int.Parse(cbo_Fuente.SelectedItem.Value));
            cbo_Fuente.ToolTip = dt.Rows[0][0].ToString();
        }
        protected void cbo_Fuente_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTip_Fuente();
        }
        //==========================================================

        protected void btnAdicionarSeg_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            string usuconect = (string)Session["Usuario"];
            string idObra = (string)Session["Obra"];

            if (cbo_EmpreCompete.Visible != true)
            {
                if (txtObservacion.Text == "" || cboTipoSeg.SelectedItem.Value == "0")
                {
                    mensaje = "Seleccione el tipo de seguimiento y digite el comentario";
                }
                else
                {
                    // OJO aqui hay un triguer en la tabla obra_seguimiento donde se actualiza el estado en la obra si es no aplica  lo marca como perdido
                    String menId = contobra.guardarSeguimientoObra(Convert.ToInt32(idObra), Convert.ToInt32(cboTipoSeg.SelectedItem.Value), txtObservacion.Text, usuconect,0,txt_Link_Evernot.Text);
                    if (menId.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        mensaje = "Registro exitoso!!";
                        // cargarTipoSeguimiento(0);
                        txtObservacion.Text = "";
                        txt_Link_Evernot.Text = "";
                        Permitir_Visibilidad_Campos();                       
                        //btnGuardar.Text = "Actualizar";
                        //cargargrilla();
                    }
                    else
                    {
                        mensaje = "Hubo un error al guardar";
                    }
                }
            }
            else
            {
                if (txtObservacion.Text == "" || cboTipoSeg.SelectedItem.Value == "0" || cbo_EmpreCompete.SelectedItem.Value == "0")
                {
                    mensaje = " Seleccione la empresa competencia y digite el comentario";
                }
                else
                {
                    // OJO aqui hay un triguer en la tabla obra_seguimiento donde se actualiza el estado en la obra si es no aplica  lo marca como perdido
                    String menId = contobra.guardarSeguimientoObra(Convert.ToInt32(idObra), Convert.ToInt32(cboTipoSeg.SelectedItem.Value), txtObservacion.Text, usuconect, Convert.ToInt32(cbo_EmpreCompete.SelectedItem.Value),txt_Link_Evernot.Text);
                    if (menId.Substring(0, 1) != "E")//E <- de ERROR
                    {
                        mensaje = "Registro exitoso!!";
                        cbo_EmpreCompete.Items.Clear();
                        cbo_EmpreCompete.Items.Add(new ListItem("Seleccione", "0"));
                        contobra.LlenarComboEmpresaCompe(cbo_EmpreCompete);
                        // cargarTipoSeguimiento(0);
                        txtObservacion.Text = "";
                        txt_Link_Evernot.Text = "";
                        Permitir_Visibilidad_Campos();
                        //btnGuardar.Text = "Actualizar";
                        //cargargrilla();
                    }
                    else
                    {
                        mensaje = "Hubo un error al guardar";
                    }
                }

            }
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
             this.CargarReporteObra("Obra");
            Permitir_Visibilidad_Campos();                  
            //CargarSeguimientoObra();
        }

        public void CargarReporteObra(string tipo)
        {
            int idObra = Convert.ToInt32(Session["Obra"]);

            List<ReportParameter> parametro = new List<ReportParameter>();

            parametro.Add(new ReportParameter("obraid", idObra.ToString(), true));

            //ReporteVerClientes.Width = 1320;
            //ReporteVerClientes.Height = 1000;
            this.ReporteVerSegObra.KeepSessionAlive = true;
            this.ReporteVerSegObra.AsyncRendering = true;
            ReporteVerSegObra.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReporteVerSegObra.ServerReport.ReportServerCredentials = irsc;

            ReporteVerSegObra.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            if (tipo == "Obra")
            {
                ReporteVerSegObra.ServerReport.ReportPath = "/Comercial/COM_Obra_Seguimiento";

            }

            this.ReporteVerSegObra.ServerReport.SetParameters(parametro);
        }
        //Permite mostrar mensajes de alerta
        private void mensajeVentana(String mensaje)
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "');", true);
        }

        protected void btn_AnularObra_Click(object sender, EventArgs e)
        {
            string fecha = DateTime.Now.ToShortDateString();
            string tabla = "Obra";
            string evento = "Anulacion";
            string usuarioMod = (string)Session["Usuario"];

            string cliente = Request.QueryString["idCliente"];
            string obra = Request.QueryString["idObra"];
            int anulada = 0;
            //Anula la obra
            contobra.Anular_Obra(int.Parse(obra), anulada);
            //Guarda el log de la tabla obra
            contobra.Met_Insertar_Log_AnulaObra(tabla, int.Parse(obra), usuarioMod, fecha, evento);
            //-------------------------------------------
            mensajeVentana("Obra anulada");
            btn_AnularObra.Visible = false;
        }
        //==========================================================


        protected void chk_PotenArrenda_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PotenArrenda.Checked == true)
            {
                chk_PotenForsa.Checked = false;
                chk_PotencialNewDesarrollo.Checked = false;
                LblVenta.Text = "0";
                LblAlquiler.Text = "1";
                LblDesarrollo.Text = "0";
                //chk_PotencialNewDesarrollo.Visible = true;
                //chk_PotenArrenda.Visible = true;
                //chk_PotenForsa.Visible = true;
                //LblTipoProyecto.Visible = true;
            }
        }
        protected void chk_PotenForsa_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PotenForsa.Checked == true)
            {
                chk_PotenArrenda.Checked = false;
                chk_PotencialNewDesarrollo.Checked = false;
                LblVenta.Text = "1";
                LblAlquiler.Text = "0";
                LblDesarrollo.Text = "0";
                //chk_PotencialNewDesarrollo.Visible = true;
                //chk_PotenArrenda.Visible = true;
                //chk_PotenForsa.Visible = true;
                //LblTipoProyecto.Visible = true;
            }
        }


        protected void chk_PotencialNewDesarrollo_CheckedChanged(object sender, EventArgs e)
        {
            if (chk_PotencialNewDesarrollo.Checked == true)
            {
                chk_PotenArrenda.Checked = false;
                chk_PotenForsa.Checked = false;
                LblVenta.Text = "0";
                LblAlquiler.Text = "0";
                LblDesarrollo.Text = "1";
                //chk_PotenArrenda.Visible = true;
                //chk_PotenForsa.Visible = true;
                //chk_PotencialNewDesarrollo.Visible = true;
                //LblTipoProyecto.Visible = true;
            }

        }

        protected void cboTipoSeg_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboTipoSeg.SelectedValue.ToString() == "3")
            {
                cbo_EmpreCompete.Visible = true;
                lbl_EmpreCompete.Visible = true;
                cbo_EmpreCompete.Items.Clear();            
                cbo_EmpreCompete.Items.Add(new ListItem("Seleccione", "0"));
                contobra.LlenarComboEmpresaCompe(cbo_EmpreCompete);            
            }
            else
            {
                cbo_EmpreCompete.Visible = false;
                lbl_EmpreCompete.Visible = false;              
            }
        }

        protected void cbo_Segmento_TextChanged(object sender, EventArgs e)
        {
            cbo_TipoSegmento.Items.Clear();
            contobra.LlenarComboSegmentoTipo(cbo_TipoSegmento, Convert.ToInt32(cbo_Segmento.SelectedItem.Value));
        }

       


        //protected void GridView_DetalleSeguimiento_RowDeleting(object sender, GridViewDeleteEventArgs e)
        //{
        //    string fecha = DateTime.Now.ToShortDateString();
        //    string tabla = "Obra_Seguimiento";
        //    string evento = "Anulacion";
        //    string usuarioMod = (string)Session["Usuario"];

        //    int respuesta;
        //    int idObra = Convert.ToInt32(Session["Obra"]);
        //    int id_seg = Convert.ToInt32(GridView_DetalleSeguimiento.DataKeys[e.RowIndex].Value);
        //    respuesta = contobra.Anular_SeguientoObra(id_seg, idObra);
        //    if (respuesta == 1)
        //    {
        //        contobra.Met_Insertar_Log_AnulaObra(tabla, id_seg, usuarioMod, fecha, evento);
        //        Permitir_Visibilidad_Campos();
        //        mensajeVentana("Seguimiento anulado correctamente");
        //    }
        //    else
        //    {
        //        mensajeVentana("Solo se pueden anular los seguimientos");
        //    }
        //}

        //public void CargarSeguimientoObra()
        //{
        //    int idObra = Convert.ToInt32(Session["Obra"]);

        //    if (contobra.CargarSeguimientoObra(idObra).Tables[0].Rows.Count != 0)
        //    {
        //        GridView_DetalleSeguimiento.DataSource = contobra.CargarSeguimientoObra(idObra);
        //        GridView_DetalleSeguimiento.DataMember = contobra.CargarSeguimientoObra(idObra).Tables[0].ToString();
        //        GridView_DetalleSeguimiento.DataBind();
        //    }
        //    else
        //    {
        //        GridView_DetalleSeguimiento.DataSource = null;
        //        GridView_DetalleSeguimiento.DataMember = null;
        //        GridView_DetalleSeguimiento.DataBind();
        //    }
        //}

    }

}