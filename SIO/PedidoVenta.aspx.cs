using System;
using System.Collections.Generic;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Security;
using System.Net.Mail;
using System.Net;
using Microsoft.Reporting.WebForms;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography.X509Certificates;
using CapaControl;
using System.Web.UI.HtmlControls;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Drawing;
using dhtmlxConnectors;
using Microsoft.VisualBasic;

namespace SIO
{
    public partial class PedidoVenta : System.Web.UI.Page
    {
        public ControlSolicitudFacturacion controlsf = new ControlSolicitudFacturacion();
        public SqlDataReader reader = null;
        private DataSet dsPedido = new DataSet();
        public ControlPedido contpv = new ControlPedido();
        public ControlUbicacion contubi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public ControlContacto controlCont = new ControlContacto();
        public ControlFUP controlfup = new ControlFUP();
        public SqlDataReader readerCliente = null;
        private System.ComponentModel.IContainer components = null;
        private int tipoClienteid;
        private Int64 itemid, item_planta_id;
        public FUP fup_clase = new FUP();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                // verifico si es un usuario de tipo cliente directo como MRV
                if (Convert.ToUInt32(Session["IdClienteUsuario"]) > 0) Response.Redirect("Home.aspx");

                if (Session["Rol"] != null)
                {
                    Session["ConfirmaAsistente"] = "0";
                    Session["ConfirmaIngenieria"] = "0";
                    Session["ConfirmaComercial"] = "0";
                    String UpPath;
                    UpPath = "C:\\UploadedUserFiles";

                    if (!Directory.Exists(UpPath))
                    {
                        Directory.CreateDirectory("C:\\UploadedUserFiles\\");
                    }

                    //if (Session["SesionActiva"] == null)
                    //    Response.Redirect("index.aspx"); 

                    //this.Idioma();
                    Session["Motivo"] = "Accesorio";
                    //this.cargarGrupoAcc();

                    //VERIFICO SI VIENE PARA CARGAR EL CLIENTE DESDE EL MAESTRO
                    if (Request.QueryString["idCliente"] != null)
                    {
                        poblarPlanta();
                        string idCliente = Request.QueryString["idCliente"];
                        Session["Cliente"] = idCliente;
                        Session["idCliente"] = idCliente;
                        this.PoblarCliente1(sender,e);
                        //lkCliente.Enabled = true;
                        //lkContacto.Enabled = true;
                        //lkObra.Enabled = true;
                    }
                    else
                    {

                    //CARGA LAS VARIABLES DE SESION PARA UTILIZARLAS EN TODAS LAS PAGINAS
                    if (Request.QueryString["Usuario"] != null && Request.QueryString["Rol"] != null && Request.QueryString["estado"] == null)
                    {
                        Session["Usuario"] = Request.QueryString["Usuario"]; 
                        Session["Nombre_Usuario"] = Request.QueryString["Nombre_Usuario"]; 
                        Session["rcID"] = Request.QueryString["rcID"]; 
                        Session["Pais"] = Request.QueryString["Pais"];
                        Session["rcEmail"] = Request.QueryString["rcEmail"];
                        Session["Rol"] = Request.QueryString["Rol"];
                        Session["Area"] = Request.QueryString["Area"];
                        Session["Zona"] = Request.QueryString["Zona"];
                        Session["IdiomaGeneral"] = "1";
                        Session["TipoClienteID"] = Session["TipoClienteID"];
                        Session["Item"] = Session["Item"];
                        Session["Item_Planta"] = Session["Item_Planta"];
                    
                    }                                      
               
                    string rcID = Convert.ToString(Session["rcID"]);
                    int rol = Convert.ToInt32(Session["Rol"]);
                    string pais = Convert.ToString(Session["Pais"]);           

                    //POBLAMOS LOS COMBOS
                    PoblarCombos();
                    this.cargarPaisesRol();
                    //TRAEMOS LOS DATOS DEL REPORTE
                    string estado = "", fup = "", idAcc = "";
                    if (Request.QueryString["estado"] != null)
                    {
                        estado = Request.QueryString["estado"];
                        Session["estado"] = estado;
                        fup = Request.QueryString["numfup"];
                        Session["general"] = fup;
                        idAcc = Request.QueryString["codaccesorio"];
                        Session["IdAcc"] = idAcc;
                    }

                    if (Request.QueryString["numfup"] != null)
                    {
                        fup = Request.QueryString["numfup"];
                        Session["FUP"] = fup;
                        txtFUP.Text = fup;
                        //VOLVER A CARGAR NUMERO PV
                        NumeroPV();
                        pnlCotAcc.Enabled = true;
                        pnlCotAcc.Visible = true;
                        this.cargarGrupoAcc();

                        PoblarPedidoVenta();
                    }

                    if (btnGuardar.ToolTip == "Guardar" && estado != "actualizar" && estado != "eliminar")
                    {
                        btnGuardar.Attributes.Add("language", "javascript");
                        btnGuardar.Attributes.Add("OnClick", "return confirm('Esta seguro de guardar el accesorio?');");
                    }
                    else
                    {
                        if (estado == "actualizar")
                        {
                            this.cboAccesorio.Items.Clear();
                            btnGuardar.Attributes.Add("language", "javascript");
                            btnGuardar.Attributes.Add("OnClick", "return confirm('Esta seguro de actualizar el accesorio?');");

                            btnGuardar.ToolTip = "Actualizar";
                            btnGuardar.BorderColor = System.Drawing.Color.Black;
                            btnGuardar.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                            btnGuardar.BorderWidth = 2;

                            //VOLVER A CARGAR FUP
                            string FUP = Convert.ToString(Session["general"]);
                            txtFUP.Text = FUP;
                            //VOLVER A CARGAR NUMERO PV
                            NumeroPV();
                            pnlCotAcc.Enabled = true;
                            pnlCotAcc.Visible = true;
                            this.cargarGrupoAcc();

                            PoblarPedidoVenta();
                            ConsultarAccesorio();
                            //ConsultarPrecioAccesorio();
                            tipoClienteid = Convert.ToInt32(Session["TipoClienteID"]);
                            itemid = Convert.ToInt64(Session["Item"]);
                            item_planta_id = Convert.ToInt64(Session["Item_Planta"]);
                            calcularPrecioItem(item_planta_id, Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue), tipoClienteid);
                        }
                        else
                        {
                            if (estado == "eliminar")
                            {                            
                                this.cboAccesorio.Items.Clear();
                                btnGuardar.ToolTip = "Eliminar";
                                btnGuardar.BorderColor = System.Drawing.Color.Maroon;
                                btnGuardar.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                                btnGuardar.BorderWidth = 2;

                                //VOLVER A CARGAR FUP
                                string FUP = Convert.ToString(Session["general"]);
                                txtFUP.Text = FUP;
                                //VOLVER A CARGAR NUMERO PV
                                NumeroPV();
                                pnlCotAcc.Enabled = true;
                                pnlCotAcc.Visible = true;
                                this.cargarGrupoAcc();

                                PoblarPedidoVenta();
                                ConsultarAccesorio();
                                btnGuardar.Attributes.Add("language", "javascript");
                                btnGuardar.Attributes.Add("OnClick", "return confirm('Esta seguro de " + btnGuardar.ToolTip.ToString() + " el accesorio?');");
                                Session["estado1"] = "guardar";
                            }
                        }
                    }
                        HabilitarSolicitudFacturacion();
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

        private void PoblarCliente1(object sender, EventArgs e)
        {
            string cliente = Convert.ToString(Session["Cliente"]);
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
            contpv.CerrarConexion();
        }

        public static bool IsSessionTimedOut()
        {
            HttpContext ctx = HttpContext.Current;
            if (ctx == null)
                throw new Exception("Este método sólo se puede usar en una aplicación Web");

            //Comprobamos que haya sesión en primer lugar 
            //(por ejemplo si por ejemplo EnableSessionState=false)
            if (ctx.Session == null)
                return false;   //Si no hay sesión, no puede caducar
            //Se comprueba si se ha generado una nueva sesión en esta petición
            if (!ctx.Session.IsNewSession)
                return false;   //Si no es una nueva sesión es que no ha caducado

            HttpCookie objCookie = ctx.Request.Cookies["ASP.NET_SessionId"];
            //Esto en teoría es imposible que pase porque si hay una 
            //nueva sesión debería existir la cookie, pero lo compruebo porque
            //IsNewSession puede dar True sin ser cierto (más en el post)
            if (objCookie == null)
                return false;

            //Si hay un valor en la cookie es que hay un valor de sesión previo, pero como la sesión 
            //es nueva no debería estar, por lo que deducimos que la sesión anterior ha caducado
            if (!string.IsNullOrEmpty(objCookie.Value))
                return true;
            else
                return false;
        }

        private void habilitarAlmaceLogistica()
        {
            int rol = Convert.ToInt32(Session["Rol"]);

            if (rol == 15 && !String.IsNullOrEmpty(txtFUP.Text))
            {
                reader = contpv.ConsultarContenidoPvFUP(Convert.ToInt32(txtFUP.Text));
                if (reader.HasRows)
                {
                    Panel5.Visible = true;
                    reader.Read();
                    int contenido = Convert.ToInt32(reader.GetValue(0));
                    int chkAlmacen = Convert.ToInt32(reader.GetValue(1));
                    int chkAcccesorio = Convert.ToInt32(reader.GetValue(2));

                    if (contenido == 1)
                    {
                        chkLogisticaAlmacen.Visible = true;
                        chkLogisticaAccesorios.Visible = false;
                        chkLogisticaAccesorios.Checked = false;
                        chkLogisticaAccesorios.Enabled = false;

                        if (chkAlmacen == 1)
                        {
                            chkLogisticaAlmacen.Checked = true;
                            chkLogisticaAlmacen.Enabled = false;
                        }
                        else 
                        {
                            chkLogisticaAlmacen.Checked = false;
                            chkLogisticaAlmacen.Enabled = true;                        
                        } 
                    }
                    
                    else if (contenido == 2)
                    {
                        chkLogisticaAlmacen.Visible = false;
                        chkLogisticaAccesorios.Visible = true;
                        chkLogisticaAlmacen.Checked = false;
                        chkLogisticaAlmacen.Enabled = false;

                        if (chkAcccesorio == 1)
                        {
                            chkLogisticaAccesorios.Checked = true;
                            chkLogisticaAccesorios.Enabled = false;
                        }
                        else 
                        {
                            chkLogisticaAccesorios.Checked = false;
                            chkLogisticaAccesorios.Enabled = true;
                        }
                    }

                    else if (contenido == 3)
                    {
                        chkLogisticaAlmacen.Visible = true;
                        chkLogisticaAccesorios.Visible = true;

                        if (chkAcccesorio == 1)
                        {
                            chkLogisticaAccesorios.Checked = true;
                            chkLogisticaAccesorios.Enabled = false;
                        }
                        else
                        {
                            chkLogisticaAccesorios.Checked = false;
                            chkLogisticaAccesorios.Enabled = true;
                        }

                        if (chkAlmacen == 1)
                        {
                            chkLogisticaAlmacen.Checked = true;
                            chkLogisticaAlmacen.Enabled = false;
                        }
                        else
                        {
                            chkLogisticaAlmacen.Checked = false;
                            chkLogisticaAlmacen.Enabled = true;
                        }                         
                    }
                    else
                    {
                        chkLogisticaAlmacen.Visible = true;
                        chkLogisticaAccesorios.Visible = true;
                        chkLogisticaAccesorios.Checked = false;
                        chkLogisticaAccesorios.Enabled = true;
                        chkLogisticaAlmacen.Checked = false;
                        chkLogisticaAlmacen.Enabled = true;                    
                    }
                }

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
            else
            {
                chkLogisticaAlmacen.Visible = false;
                chkLogisticaAccesorios.Visible = false;
            }
        }

        private void HabilitarSolicitudFacturacion()
        {
            int rol = Convert.ToInt32(Session["Rol"]); 
            if(!String.IsNullOrEmpty(txtFUP.Text) && (rol == 3 || rol == 9 || rol == 30 ))
            {
                btnSolicFatura.Enabled = true;
            }
            else
            {
                btnSolicFatura.Enabled = true;
            }
        }

        private void ConsultarAccesorio()
        {
            string IdAcc = Convert.ToString(Session["IdAcc"]);

            reader = contpv.ConsultarAccesorio(Convert.ToInt32(IdAcc));
            if (reader.HasRows == true)
            {
                reader.Read();
                txtCantidad.Text = Convert.ToInt32(reader.GetValue(0)).ToString("N0", new CultureInfo("en-US"));

                txtCodigo.Text = reader.GetValue(6).ToString();

                decimal PrecioOrigen = Convert.ToDecimal(reader.GetValue(1).ToString());
                txtPrecio.Text = Convert.ToString(PrecioOrigen.ToString("N", new CultureInfo("en-US")));

                txtObservaciones.Text = reader.GetValue(4).ToString();
                cboAccesorio.Items.Add(new ListItem(reader.GetString(7).ToString(), reader.GetInt32(6).ToString()));
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();         
        }       

        private void PoblarCombos()
        {
             
                cboCiudad.Items.Add(new ListItem("Seleccione la ciudad", "0"));
                cboCiudad.SelectedIndex = 0;
                cboCliente.Items.Add(new ListItem("Seleccione la empresa", "0"));
                cboCliente.SelectedIndex = 0;
                cboContacto.Items.Add(new ListItem("Seleccione el contacto", "0"));
                cboContacto.SelectedIndex = 0;
                cboObra.Items.Add(new ListItem("Seleccione la obra", "0"));
                cboObra.SelectedIndex = 0;
                //cboGrupo.Items.Add(new ListItem("Seleccione el grupo", "0"));
                //cboAccesorio.Items.Add(new ListItem("Seleccione el accesorio", "0"));
                poblarPlanta();
                  
        }

         

        private void PoblarListaPais()
        {
            
            string rcID = Convert.ToString(Session["rcID"]);
            cboPais.Items.Clear();

              cboPais.Items.Add(new ListItem("Seleccione el pais", "0"));
                cboPais.SelectedIndex = 0;
            

            reader = contubi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                
                    mensaje = "Usted no posee paises asociados.";
                 
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        //CARGAMOS LOS PAISES DEACUERDO AL ROL DEL USUARIO
        private void cargarPaisesRol()
        {
            int arRol = Convert.ToInt32(Session["Rol"]);
            if ((arRol == 3) || (arRol == 28) || (arRol == 29) || (arRol == 30))
            {
                this.PoblarListaPais();
            }
            else
            {
                this.PoblarListaPais2();
            }
        }

        private void PoblarListaPais2()
        {
             
            cboPais.Items.Clear();
             
                cboPais.Items.Add(new ListItem("Seleccione el pais", "0"));
                cboPais.SelectedIndex = 0;
            

            reader = contubi.poblarListaPais();
           
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    cboPais.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = "";
                 
                    mensaje = "Usted no posee paises asociados.";
                 
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
        }

        protected void cboPais_SelectedIndexChanged(object sender, EventArgs e)
        {            
            this.PoblarCiudad();
        }

        private void PoblarCiudad()
        {
            int rol = Convert.ToInt32(Session["Rol"]);
            string rcID = Convert.ToString(Session["rcID"]);
             

            cboCiudad.Items.Clear();
             
                cboCiudad.Items.Add(new ListItem("Seleccione la ciudad", "0"));
                cboCiudad.SelectedIndex = 0;
            
            
            if ((rol == 3 || rol == 30)&& (Convert.ToInt32(cboPais.SelectedItem.Value) == 8))
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
                contpv.CerrarConexion();
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
                contpv.CerrarConexion();
            }
            Session["Ciudad"] = cboCiudad.SelectedValue;
        }
              
        protected void cboCiudad_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarEmpresa();
        }

        private void PoblarEmpresa()
        {
             

            cboCliente.Items.Clear();
            
                cboCliente.Items.Add(new ListItem("Seleccione la empresa", "0"));
                cboCliente.SelectedIndex = 0;

            int idClienteUsuario = Convert.ToInt32(Session["IdClienteUsuario"]);

            reader = concli.ConsultarDatosCliente(Convert.ToInt32(cboCiudad.SelectedItem.Value), idClienteUsuario);          
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboCliente.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
            }

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }
        
        protected void cboCliente_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.PoblarContacto();
            this.PoblarObra();
            this.TipoCliente();
        }

        private void PoblarContacto()
        {
            
            cboContacto.Items.Clear();
             
                cboContacto.Items.Add(new ListItem("Seleccione el contacto", "0"));
                cboContacto.SelectedIndex = 0;
             
            reader = concli.ObtenerContacto(Convert.ToInt32(cboCliente.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboContacto.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        private void PoblarObra()
        {
            
            if ((cboCliente.SelectedItem.Text == "Seleccione la empresa") || (cboCliente.SelectedItem.Text == "Select The Company") ||
                (cboCliente.SelectedItem.Text == "Selecione Companhia"))
            {
                cboObra.Items.Clear();
            }
            else
            {
                cboObra.Items.Clear();
                 
                    cboObra.Items.Add(new ListItem("Seleccione la obra", "0"));
                    cboObra.SelectedIndex = 0;
                
                reader = controlCont.ObtnObrasDistPV(Convert.ToInt32(cboCliente.SelectedValue));
                if (reader.HasRows == true)
                {
                    while (reader.Read())
                    {
                        cboObra.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));

                    }
                }
                reader.Close();
                reader.Dispose();
                controlCont.cerrarConexion();
            }
        }

        protected void txtPV_TextChanged(object sender, EventArgs e)
        {
            ValidarNumeroPedidoVenta();
            Accordion1.SelectedIndex = 0;
        }

        private void ValidarNumeroPedidoVenta()
        {
            int idofa = 0;

            if (txtPV.Text == "")
            {
                chkLogisticaAlmacen.Enabled = false;
            }
            else
            {
                reader = contpv.ConsultarNumeroPedidoVenta(txtPV.Text);
                bool existePv = reader.HasRows;

                if (!existePv)
                {
                    string mensaje = "";
                     
                        mensaje = "Digíte correctamente el número de pedido de venta, el digitado no existe. Verifíquelo e inténtelo nuevamente.";
                     

                    txtPV.Text = "";
                    txtFUP.Text = "";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    grdDetalle.Visible = false;

                    reader.Close();
                    reader.Dispose();
                    contpv.CerrarConexion();
                }
                else
                {
                    reader.Read();
                    btnSolicFatura.Enabled = true;
                    bool valida1 = reader.GetSqlBoolean(1).Value;
                    txtFUP.Text = reader.GetValue(0).ToString();
                    valida1 = reader.GetSqlBoolean(1).Value;
                    lblidofa.Text = reader.GetValue(2).ToString();
                    btnSeguimiento.Visible = true;

                    reader.Close();
                    reader.Dispose();
                    contpv.CerrarConexion();

                    if (valida1 == false)
                    {
                        //txtFUP.Text = reader.GetValue(0).ToString();
                        //ojo revisar
                        this.ValidarPedidoVenta();
                        chkLogisticaAlmacen.Enabled = true;
                        configurarControles();                        

                        if (!String.IsNullOrEmpty(txtFUP.Text))
                        {
                            SqlDataReader readerTipoItem = contpv.consultarTipoItem(Convert.ToInt32(txtFUP.Text));
                            if (readerTipoItem.HasRows)
                            {
                                List<int> especial = new List<int>();
                                List<int> contenido = new List<int>();
                                while (readerTipoItem.Read())
                                {
                                    especial.Add(Convert.ToInt32(readerTipoItem.GetValue(0)));
                                    contenido.Add(Convert.ToInt32(readerTipoItem.GetValue(1)));
                                }

                                readerTipoItem.Close();
                                readerTipoItem.Dispose();
                                contpv.CerrarConexion();

                                int tipoPedido = validarItems(especial);
                                int tipoContenido = validarItemsContenido(contenido);

                                establecerTipoConetenido(tipoPedido, tipoContenido);
                            }
                            else
                            {
                                readerTipoItem.Close();
                                readerTipoItem.Dispose();
                                contpv.CerrarConexion();
                            }
                        }                                              
                    }
                    else
                    {
                        this.MensajeAnuladoPV();
                    }
                }

                
            }
        }

        private void ValidarPedidoVenta()
        {
            if (Session["Rol"] != null)
            {
                int arRol = (int)Session["Rol"];
                string Pais = (string)Session["Pais"];
                string Zona = (string)Session["Zona"];
                string Representante = (string)Session["Nombre_Usuario"];
                string rcID = (string)Session["rcID"];

               
                string mensaje = "";
                if (!String.IsNullOrEmpty(txtFUP.Text))
                {
                    reader = contpv.ConsultarFUP(Convert.ToInt32(txtFUP.Text));
                    if (reader.HasRows)
                    {
                        reader.Read();
                        bool tipo = reader.GetSqlBoolean(10).Value;
                        string paisReader = reader.GetValue(7).ToString();
                        string zonaReader = reader.GetValue(9).ToString();
                        string representanteReader = reader.GetValue(4).ToString();

                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        if (tipo == true)
                        {
                            if (arRol == 3 || arRol == 30)
                            {
                                if (paisReader == Pais)
                                { //ojo revisar
                                    if (arRol == 30)
                                    {
                                        this.PoblarPedidoVenta();
                                    }
                                    else
                                    {
                                        if (zonaReader == Zona || representanteReader == Representante)
                                        {
                                            this.PoblarPedidoVenta();
                                        }
                                        else
                                        {
                                            this.MensajeValidacionPV();
                                        }
                                    }
                                }
                                else
                                {
                                    string pais = paisReader;
                                    reader = contpv.ValidarPaisRepresentante(Convert.ToInt32(pais), Convert.ToInt32(rcID));
                                    bool validaReader = reader.HasRows;

                                    reader.Close();
                                    reader.Dispose();
                                    contpv.CerrarConexion();

                                    if (validaReader)
                                    {
                                        this.PoblarPedidoVenta();
                                    }
                                    else
                                    {
                                        this.MensajeValidacionPV();
                                    }

                                }
                            }
                            else
                            {
                                this.PoblarPedidoVenta();
                            }
                            configurarControles();
                        }
                        else
                        {
                            grdDetalle.Visible = false;
                            
                              mensaje = "El número de fup ingresado no es un pedido de venta. Verifique.";
                            
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                    }
                    else
                    {
                        grdDetalle.Visible = false;
                        txtFUP.Text = "";
                        
                        mensaje = "El número de fup ingresado no existe. Verifique.";
                         
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }

                }
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }

        private void MensajeValidacionPV()
        {
            
            string mensaje = "";

               mensaje = "No posee permisos sobre el número de pv ingresado.";
             
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true); 
        }

        private void MensajeAnuladoPV()
        {
             
            string mensaje = "";
 
                mensaje = "El número de pv ingresado se encuentra anulado.";
            
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }        

        private void PoblarPedidoVenta()
        {
            //CONSULTA DATOS GENERALES
            reader = contpv.ConsultarFUP(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows == true)
            {
                reader.Read();
                cboPlanta.Items.Clear();
                cboPlanta.Items.Add(new ListItem(reader.GetString(15), reader.GetInt32(14).ToString()));
                cboPais.Items.Clear();
                cboPais.Items.Add(new ListItem(reader.GetString(8), reader.GetInt32(7).ToString()));
                cboCiudad.Items.Clear();
                cboCiudad.Items.Add(new ListItem(reader.GetString(11), reader.GetInt32(12).ToString()));
                cboCliente.Items.Clear();
                cboCliente.Items.Add(new ListItem(reader.GetString(5), reader.GetInt32(1).ToString()));
                cboContacto.Items.Clear();
                cboContacto.Items.Add(new ListItem(reader.GetString(13)));
                cboObra.Items.Clear();
                cboObra.Items.Add(new ListItem(reader.GetString(6), reader.GetInt32(3).ToString()));

                txtIdRecompra.Text = reader.GetString(16).ToString();
                lblMensajeRecompra.Text = reader.GetString(17).ToString();
                lblMensajeRecompra.Visible = true;

            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
            // ojo revisar
            grdDetalle.Visible = true;
            this.cargarGrupoAcc();

            //REPORTE DETALLE DE ACCESORIOS
            this.CargarReporteDetalle();

            //REPORTE CARTA ACCESORIOS
            //this.CargarCarta();

            //REPORTE ESTADO
            //this.CargarReporteEstado();

            //DEFINIR TIPO
            this.TipoCliente();

            //DEFINIR CIUDAD DE LA OBRA
            CiudadObra();

            //verificar el estado de los items para activar la SF
            verificarEstadoItems();

            // TRAEMOS EL VALOR DEL FLETE DEL PV
            reader = contpv.ConsultarFleteTotal(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows)
            {
                reader.Read();
                decimal flete = reader.GetDecimal(0);
                Session["FLETE"] = flete.ToString();
            }

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
                
            //SETEAMOS LAS VARIABLES DE SESION PARA SOLICITUD DE FACTURACIÓN
            Session["Pais"] = cboPais.SelectedValue;
            Session["PaisNombre"] = cboPais.SelectedItem.Text;
            Session["Ciudad"] = cboCiudad.SelectedValue;
            Session["FUP"] = txtFUP.Text;
            Session["Planta"] = cboPlanta.SelectedItem.Text;
            Session["TIPO"] = "PV";
            Session["DescTipo"] = "PEDIDO DE VENTA";
            Session["Bandera"] = "2";
            Session["VER"] = "A";
            Session["NUMERO"] = txtPV.Text;
            Session["CLIENTE"] = cboCliente.SelectedItem.Text;
            Session["OBRA"] = cboObra.SelectedItem.Text;

            if (cboPais.SelectedValue == "8")
            {
                Session["MONEDA"] = "COP";
            }
            else
            {
                Session["MONEDA"] = "USD";
            }

            btnCarta.Visible = true;
            btnEstado.Visible = true;
            btnSolicFatura.Visible = true;

            txtIdRecompra.Visible = true;
            lblIdRecompra.Visible = true;

            Accordion1.SelectedIndex = 0;
        }

        private void verificarEstadoItems() 
        {
            bool confirmarVenta = true;
            reader = contpv.consultarEstadoItem(Convert.ToInt32(txtFUP.Text));
            btnSolicFatura.Visible = true;
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (Convert.ToInt32(reader.GetValue(0)) == 0)
                    {
                        btnSolicFatura.Visible = false;
                        break;
                    }

                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

        }

        private void CargarReporteDetalle()
        {
            //string Estado = (string)Session["EstadoCom"];

            //ReportViewer1.Visible = true;            
            //List<ReportParameter> parametro = new List<ReportParameter>();
            //parametro.Add(new ReportParameter("numfup", txtFUP.Text, true));
            //parametro.Add(new ReportParameter("modifica", Estado, true));
           
            //ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            //IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            //ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            //ReportViewer1.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            //ReportViewer1.ServerReport.ReportPath = "/Comercial/COM_DetalleCotizacionAcc";
            //this.ReportViewer1.ServerReport.SetParameters(parametro);
            //ReportViewer1.ShowToolBar = false;
            double total = 0;
            DataTable dt = new DataTable();
            dt.Columns.Add("cot_acc_id");
            dt.Columns.Add("cot_item");
            dt.Columns.Add("N°");
            dt.Columns.Add("cot_acc_id_acc");
            dt.Columns.Add("cot_cantidad");
            dt.Columns.Add("cot_acc_precio_unitario");
            dt.Columns.Add("cot_acc_precio_total");
            dt.Columns.Add("cot_observacion");
            dt.Columns.Add("especial");
            dt.Columns.Add("descripcion");
            
            reader = contpv.ConsultarDetalleFUP(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
            if (reader.HasRows)
            {
                while (reader.Read())
                {      
                    string especial = "", estadoEspecial = "";
                    total += Convert.ToDouble(reader.GetValue(4).ToString());
                    DataRow row = dt.NewRow();
                    row["cot_acc_id"] = reader.GetValue(0).ToString();
                    row["cot_item"] = reader.GetValue(6).ToString(); 
                    row["cot_acc_id_acc"] = reader.GetValue(1).ToString();
                    row["cot_cantidad"] = Convert.ToInt32(reader.GetValue(2)).ToString("N0", new CultureInfo("en-US"));
                    row["cot_acc_precio_unitario"] = Convert.ToDouble(reader.GetValue(3)).ToString("N", new CultureInfo("en-US"));
                    row["cot_acc_precio_total"] = Convert.ToDouble(reader.GetValue(4)).ToString("N", new CultureInfo("en-US"));
                    row["cot_observacion"] = reader.GetValue(5).ToString();
                    if(Convert.ToBoolean(reader.GetValue(9)))
                        especial = "E";
                    if (Convert.ToInt32(reader.GetValue(10)) == 1)
                        especial = especial + "-Ok";
                    else if (Convert.ToInt32(reader.GetValue(10)) == 0)
                        especial = especial + "-Falta"; 
                    row["especial"] = especial;
                    row["descripcion"] = reader.GetValue(7).ToString();
                    dt.Rows.Add(row);
                }
            }
            ventaTotal.Text = total.ToString("N",new CultureInfo("en-US"));
            grdDetalle.Dispose();
            grdDetalle.DataSource = dt;
            grdDetalle.DataBind();            
            Session["DT"] = dt;
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }
       
        private void Reload_tbDetalle()
        {
            grdDetalle.DataSource = Session["DT"] as DataTable;
            grdDetalle.DataBind();
            PanelEspecificaciones.Visible = false;
            imgItem.Visible = false;
        }  

        private void CargarReporteEstado()
        {
            ReportViewer3.Visible = true;
            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("fup", txtFUP.Text, true));

            this.ReportViewer3.KeepSessionAlive = true;
            this.ReportViewer3.AsyncRendering = false;
            ReportViewer3.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer3.ServerReport.ReportServerCredentials = irsc;
            ReportViewer3.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer3.ServerReport.ReportPath = "/Comercial/COM_DetalleCotizacionAccEstadoNew";
            this.ReportViewer3.ServerReport.SetParameters(parametro);
            ReportViewer3.ShowToolBar = false;
        }
     
        private void CargarCarta()
        {
            Accordion1.Panes["AccordionPane1"].Visible = true;
            ReportViewer2.Visible = true;
            string Rep = (string)Session["Nombre_Usuario"];
            string CorreoRep = (string)Session["rcEmail"];
            string fecha = System.DateTime.Today.ToLongDateString();

            List<ReportParameter> parametro = new List<ReportParameter>();
            parametro.Add(new ReportParameter("numfup", txtFUP.Text, true));
            parametro.Add(new ReportParameter("nombrer", Rep, true));
            parametro.Add(new ReportParameter("paisr", cboPais.SelectedItem.Text, true));
            parametro.Add(new ReportParameter("correor", CorreoRep, true));
            parametro.Add(new ReportParameter("fecha", fecha, true));

            this.ReportViewer2.KeepSessionAlive = true;
            this.ReportViewer2.AsyncRendering = false;
            ReportViewer2.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
            ReportViewer2.ServerReport.ReportServerCredentials = irsc;
            ReportViewer2.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
            ReportViewer2.ServerReport.ReportPath = "/Comercial/COM_CartaCotizacionPVNew";
            this.ReportViewer2.ServerReport.SetParameters(parametro); 
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

        protected void txtFUP_TextChanged(object sender, EventArgs e)
       {
            string mensaje = "";
            if (txtFUP.Text == "")
            {
            }
            
            else
                {
                    if (IsInt(txtFUP.Text)){                    
                        Session["FUP"] = txtFUP.Text;
                        NumeroPV();                                
                    }
                    else
                    {
                        txtFUP.Text = "";  
                        mensaje = "Digite el fup correctamente";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                          
                    }                
                }
            Accordion1.SelectedIndex = 0;
        }

        private void NumeroPV()
        { // ojo revisar
            string numFup = "";
            bool existe;

            if (Session["FUP"].ToString() == null) numFup = txtFUP.Text; else numFup = Session["FUP"].ToString();
            
            txtFUP.Text = numFup;
            reader = contpv.ConsultarNumeroPedidoVentaConFUP(Convert.ToInt32(numFup));
            existe = reader.HasRows;
            if (existe)
            {// ojo revisar consulta carga pv
                reader.Read();
                txtPV.Text = reader.GetValue(0).ToString();
                this.ValidarNumeroPedidoVenta();
                this.ValidarPedidoVenta();

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
                cargarTipoPedido();
                txtIdRecompra.Visible = true;
                lblIdRecompra.Visible = true;


            }
            else
            {
                //string mensaje = "";
                //mensaje = "NO Existe";
                //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                this.ValidarPedidoVenta();
            }

            Accordion1.SelectedIndex = 0;

        }

        protected void btnCarta_Click(object sender, EventArgs e)
        {
            string mensaje="", ruta ="";
            int index = Accordion1.SelectedIndex;

            if (btnCarta.Text == "Generar Carta") ruta = "Carta"; else ruta = "Detalle";

            if (txtPV.Text == "" && txtFUP.Text == "")
            {
                mensaje = "Digite el número de FUP o PV";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                if (ruta == "Carta")
                {
                    Accordion1.SelectedIndex = 2;
                    btnCarta.Text = "Detalle PV";
                    this.CargarCarta();                    
                }
                else
                {
                    if (ruta == "Detalle")
                    {
                        Accordion1.SelectedIndex = 0;
                        btnCarta.Text = "Generar Carta";
                        this.CargarReporteDetalle();
                        
                    }
                }
            }
        }

        protected void txtNomAcc_TextChanged(object sender, EventArgs e)
        {
            this.LimpiarControles();
            btnGuardar.ToolTip = "Guardar";
            if (txtNomAcc.Text != "")
            {
                txtCodigo.Text = "";
                string var = txtNomAcc.Text.ToString().ToUpperInvariant();
                int idioma = 1;
                this.cargarGrupoAcc();
                reader = contpv.ConsultarAccesorioNomEsp(var, Convert.ToInt32(cboPlanta.SelectedValue), idioma);
                if (!reader.HasRows)
                {
                    this.MensajeAccesorio();
                }
                else
                {
                    cboAccesorio.Items.Clear();
                    cboAccesorio.Items.Add(new ListItem("Seleccione el accesorio", "0"));                    
                    while (reader.Read())
                    {
                        cboAccesorio.Items.Add(new ListItem(reader.GetString(1).ToString(), reader.GetInt32(0).ToString()));
                    }                    
                }

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
                cboAccesorio.SelectedValue = "0";
            }
            else
            {
                this.cargarGrupoAcc();
                txtCodigo.Text = "";
            }
        }

        private void MensajeAccesorio()
        {
             
            string mensaje = "";

            mensaje = "No existe accesorio con la descripción ingresada.";
             
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        }

        protected void txtCodigo_TextChanged(object sender, EventArgs e)
        {
            btnGuardar.ToolTip = "Guardar";
            try 
            {
                string mensaje = "";
                if (txtCodigo.Text != "")
                {
                    if (!IsInt(txtCodigo.Text))
                    {
                        PanelEspecificaciones.Visible = false;
                        imgItem.Visible = false;
                        mensaje = "Digite Correctamente el codigo";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        cargarGrupoAcc();
                        txtCodigo.Text = "";
                        txtPrecio.Text = "";
                    }
                    else
                    {
                        txtNomAcc.Text = "";
                         
                        cargarGrupoAcc();
                        reader = contpv.ConsultarAccesorioCodigo(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedItem.Value));

                        if (reader.HasRows)
                        {
                            reader.Read();
                            cboAccesorio.Items.Clear();
                            cboGrupo.SelectedValue = reader.GetValue(6).ToString();
                            cboAccesorio.Items.Add(new ListItem(reader.GetValue(4).ToString(), reader.GetValue(0).ToString()));
                            reader.Close();
                            reader.Dispose();

                            txtCodigo.Text = cboAccesorio.SelectedItem.Value;
                            reader = contpv.ConsultarValorAccesorio(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedValue));
                            if (reader.HasRows == true)
                            {
                                reader.Read();
                                PanelEspecificaciones.Visible = true;
                                Session["Item"] = reader.GetValue(5).ToString();
                                itemid = Convert.ToInt64(Session["Item"]);

                                Session["Item_Planta"] = reader.GetValue(1).ToString();
                                item_planta_id = Convert.ToInt64(Session["Item_Planta"]);

                                string lblPesoUniItem = "";
                                string LEmpaqueItem = "";
                                string LPesoEmpItem = "";

                                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))))
                                    lblPesoUniItem = Convert.ToString(reader.GetValue(2));
                                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(3))))
                                    LEmpaqueItem = Convert.ToString(reader.GetValue(3));
                                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(4))))
                                    LPesoEmpItem = Convert.ToString(reader.GetValue(4));

                                if (!String.IsNullOrEmpty(reader.GetValue(6).ToString()))
                                {
                                    LblFabricado.Visible = true;
                                    if (Convert.ToInt32(reader.GetValue(6)) == 0)
                                        LblFabricado.Text = "Almacén";
                                    else
                                        LblFabricado.Text = "Fabricado";
                                }


                                poblarTipoItem(item_planta_id);
                                poblarModeloItem(item_planta_id);
                                CargarImagenItem(itemid);
                                MostrarTipoItem(item_planta_id);
                                llenarParametros(item_planta_id);

                                cantidadDisponibleItem(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedValue));
                                tipoClienteid = Convert.ToInt32(Session["TipoClienteID"]);
                                calcularPrecioItem(item_planta_id, Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue), tipoClienteid);
                                requerirPlanos(item_planta_id);

                                if (!String.IsNullOrEmpty(lblPesoUniItem))
                                {
                                    Label1.Visible = true;
                                    lblPesoUni.Visible = true;
                                    lblPesoUni.Text = lblPesoUniItem;
                                }

                                if (!String.IsNullOrEmpty(LEmpaqueItem))
                                {
                                    Label11.Visible = true;
                                    LEmpaque.Visible = true;
                                    LEmpaque.Text = LEmpaqueItem;
                                }

                                if (!String.IsNullOrEmpty(LPesoEmpItem))
                                {
                                    Label3.Visible = true;
                                    LPesoEmp.Visible = true;
                                    LPesoEmp.Text = LPesoEmpItem;
                                }
                            }

                            txtPrecio.Text = "";
                            txtCantidad.Text = "0";
                            txtObservaciones.Text = "";
                            LPrecioUni.Visible = true;
                            LblPrecioUni.Visible = true;
                            LblMoneda.Visible = true;
                            Label7.Visible = true;
                            txtPrecio.Visible = true;
                            txtCantidad.Visible = true;
                            Label4.Visible = true;
                            LblMoneda.Visible = true;

                            reader.Close();
                            reader.Dispose();
                        }
                        else
                        {
                            PanelEspecificaciones.Visible = false;
                            imgItem.Visible = false;
                            mensaje = "No existe accesorio con la descripción ingresada.";
                            cargarGrupoAcc();
                            txtNomAcc.Text = "";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                            this.LimpiarControles();
                        }

                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();
                    }
                }
                else
                {
                    PanelEspecificaciones.Visible = false;
                    imgItem.Visible = false;
                    cargarGrupoAcc();
                    txtNomAcc.Text = "";
                    this.LimpiarControles();
                }            
            }
            catch(Exception ex)
            {
                string mensaje = ex.ToString();
            }            
        }

        private void LimpiarControles()
        {
            LblFabricado.Text = "";
            lblPesoUni.Text = "0";
            LEmpaque.Text = "0";            
            LblPrecioUni.Text = "0";
            LPesoEmp.Text = "0";
            txtCantidad.Text = "0";
            txtPrecio.Text = "";
            txtObservaciones.Text = "";
            //cboGrupo.Items.Clear();
            //cboGrupo.Items.Add(new ListItem("Seleccione el grupo", "0"));
            //cboAccesorio.Items.Clear();
            //cboAccesorio.Items.Add(new ListItem("Seleccione el accesiorio", "0"));            
            imgItem.Visible = false;
            PanelEspecificaciones.Visible = false;
            txtFechaDes.Text = "";
        }

        private void TipoCliente()
        {
            string TipoCliente = "";
            //CONSULTAR EL TIPO DE CLIENTE
            reader = contpv.ConsultarTipoCliente(Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue));
            
            if (reader.HasRows)
            {
                reader.Read();
                TipoCliente = reader.GetValue(0).ToString();
                Session["TipoClienteID"] = reader.GetValue(1).ToString();
                Session["MonedaID"] = reader.GetValue(3).ToString();
            }

            else TipoCliente = "No existe";
            
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            lblTipo.Visible = true;
            lblTipoCliente.Visible = true;
            lblTipoCliente.Text = TipoCliente;

            if (TipoCliente == "No existe")
            {
                
            }
        }

        private void CiudadObra()
        {
            //CONSULTAR LA CIUDAD DE LA OBRA
            reader = contpv.ConsultarCiudadObra(Convert.ToInt32(cboObra.SelectedValue));
            if (reader.HasRows == true)
            {
                reader.Read();
                LCiudadObra.Text = reader.GetValue(0).ToString();
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void cboAccesorio_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnGuardar.ToolTip = "Guardar";
            txtCodigo.Text = cboAccesorio.SelectedItem.Value;
            reader = contpv.ConsultarValorAccesorio(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedValue));
            
            if (reader.HasRows)
            {
                reader.Read();
                cboGrupo.SelectedValue = reader.GetValue(7).ToString();
                PanelEspecificaciones.Visible = true;
                Session["Item"] = reader.GetValue(5);
                itemid = Convert.ToInt64(Session["Item"]);

                Session["Item_Planta"] = reader.GetValue(1);
                item_planta_id = Convert.ToInt64(Session["Item_Planta"]);

                string lblPesoUniItem = "";
                string LEmpaqueItem = "";
                string LPesoEmpItem = "";

                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(2))))
                    lblPesoUniItem = Convert.ToString(reader.GetValue(2));
                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(3))))
                    LEmpaqueItem = Convert.ToString(reader.GetValue(3));
                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(4))))
                    LPesoEmpItem = Convert.ToString(reader.GetValue(4));

                if (!String.IsNullOrEmpty(reader.GetValue(6).ToString()))
                {
                    LblFabricado.Visible = true;
                    if (Convert.ToInt32(reader.GetValue(6)) == 0)
                        LblFabricado.Text = "Almacén";
                    else
                        LblFabricado.Text = "Fabricado";
                }

                poblarTipoItem(item_planta_id);
                poblarModeloItem(item_planta_id);
                CargarImagenItem(itemid);
                MostrarTipoItem(item_planta_id);
                llenarParametros(item_planta_id);
                cantidadDisponibleItem(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedValue));
                tipoClienteid = Convert.ToInt32(Session["TipoClienteID"]);
                calcularPrecioItem(item_planta_id, Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue), tipoClienteid);
                requerirPlanos(item_planta_id);
                if (!String.IsNullOrEmpty(lblPesoUniItem))
                {
                    Label1.Visible = true;
                    lblPesoUni.Visible = true;
                    lblPesoUni.Text = lblPesoUniItem;
                }

                if (!String.IsNullOrEmpty(LEmpaqueItem))
                {
                    Label11.Visible = true;
                    LEmpaque.Visible = true;
                    LEmpaque.Text = LEmpaqueItem;
                }

                if (!String.IsNullOrEmpty(LPesoEmpItem))
                {
                    Label3.Visible = true;
                    LPesoEmp.Visible = true;
                    LPesoEmp.Text = LPesoEmpItem;
                }

                txtPrecio.Text = "";
                LPrecioUni.Visible = true;
                LblPrecioUni.Visible = true;
                LblMoneda.Visible = true;
                Label7.Visible = true;
                txtPrecio.Visible = true;

               
            }
            else 
            {
                PanelEspecificaciones.Visible = false;
            }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();         
        }
                
        //protected void btnGuardar1_Click(object sender, EventArgs e)
        //{            
        //    int fup = -1;
        //    string idioma = (string)Session["Idioma"];
        //    string mensaje = "";
        //    string msjMail = "";
        //    string Nombre = (string)Session["Nombre_Usuario"];
        //    int Item = 0;
        //    string FechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
        //    string Hora = DateTime.Now.ToShortTimeString();
        //    string Estado = "";
        //    int rol = (int)Session["Rol"];
        //    itemid = Convert.ToInt64(Session["Item"]);

        //            if (btnGuardar1.ToolTip == "Guardar")
        //            {
        //                if (cboAccesorio.SelectedItem.Text == "Seleccione el accesorio" || txtCantidad.Text == "" || LblPrecioUni.Text == "0" || txtPrecio.Text == "0" || txtPrecio.Text == "")
        //                {
        //                    mensaje = "Debe seleccionar el accesorio, tener precio y digitar la cantidad";
        //                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //                }
                        
        //                else
        //                    {
        //                        if (txtCantidad.Text == "0")
        //                        {
        //                            if (idioma == "Español")
        //                            {
        //                                mensaje = "Debe seleccionar la cantidad.";
        //                            }
        //                            if (idioma == "Ingles")
        //                            {
        //                                mensaje = "You must select the quantidade.";
        //                            }
        //                            if (idioma == "Portugues")
        //                            {
        //                                mensaje = "É preciso selecionar o quantidade.";
        //                            }
        //                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //                        }
        //                        else
        //                        { 
        //                            int moneda = 0;
        //                            moneda = Convert.ToInt32(Session["Moneda"]);
                                    
        //                            if (txtFUP.Text == "")
        //                            {
        //                                //CREAMOS EL FUP DEL ACCESORIO
        //                                int scope = contpv.FUP(FechaCrea, Convert.ToInt32(cboCliente.SelectedItem.Value), moneda,
        //                                    Convert.ToInt32(cboContacto.SelectedValue), Convert.ToInt32(cboObra.SelectedValue), "DT",
        //                                    Nombre, Convert.ToInt32(cboPlanta.SelectedItem.Value));

        //                                string OFA = (string)Session["IDOFA"];
        //                                //ACTUALIZAMOS EL ESTADO EN FUP
        //                                int actualizar = contpv.ActualizarEstadoFUPAccesorio(scope, txtOF.Text);
        //                                txtFUP.Text = Convert.ToString(scope);
                                        
        //                                string usuario = (string)Session["Nombre_Usuario"];
        //                                string correoUsuario = (string)Session["rcEmail"];
        //                                string user = Session["Usuario"].ToString().ToUpper();
        //                                string remitente = Session["CorreoSistema"].ToString();
        //                                contpv.enviarCorreo(1, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);

        //                                if (!String.IsNullOrEmpty(msjMail))
        //                                    msjMail = "\\n El correo electrónico NO fue enviado \\n";
        //                            }

        //                            //CONSULTA CONSECUTIVO ITEM ACCESORIO
        //                            reader = contpv.ConsultarItemAccesorio(Convert.ToInt32(txtFUP.Text));
        //                            if (!reader.HasRows)
        //                            {
        //                                Item = 1;
        //                            }
        //                            else
        //                            {
        //                                reader = contpv.ConsultarMaximoItemAccesorio(Convert.ToInt32(txtFUP.Text));
        //                                reader.Read();
        //                                string maximo = reader.GetValue(0).ToString();
        //                                reader.Close();

        //                                Item = Convert.ToInt32(maximo) + 1;
        //                            }
        //                            reader.Close();

        //                            txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();

        //                            int estado_item = 0;                                    
        //                            estado_item = validarCaracteristicasItem();

        //                            //INGRESAMOS LOS DATOS A COTIZAR
        //                            int cotacc = contpv.CotAcc(Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text),
        //                                LblPrecioUni.Text, lblPesoUni.Text, "0", Convert.ToInt32(txtFUP.Text), txtObservaciones.Text,
        //                                "0", Item, Nombre, FechaCrea, Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboModelo.SelectedValue), estado_item);
                                                                        
        //                            bool reqParametro = insertarParametros(cotacc);
        //                            if (reqParametro) 
        //                            {
        //                                mensaje = "Para confirmar el pedido de venta se deben llenar los parámetros requeridos";
        //                                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                                        
        //                            }

        //                            string parametros = "";
        //                            reader = contpv.cargarParametrosItem(cotacc);
        //                            while (reader.Read())
        //                            {
        //                                parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
        //                            }
        //                            reader.Close();

        //                            //Inserto log_pv
        //                            string est = "Cotización Inicial";
        //                            string Usuario = (string)Session["Nombre_Usuario"];
        //                            double total = Convert.ToDouble(LblPrecioUni.Text) * Convert.ToInt32(txtCantidad.Text);
        //                            int tipo_id = 0, modelo_id = 0;
        //                            if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
        //                                tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
        //                            if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
        //                                modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

        //                            controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text), LblPrecioUni.Text, total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);
                                    
        //                            mensaje = MensajeItemAccesorio() + msjMail;
        //                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //                            Session["EstadoCom"] = "1";                                    
        //                            this.CargarReporteDetalle();
        //                            this.LimpiarDetalle();
        //                        }
        //                    }
        //            }
        //            else
        //            {
        //                if (btnGuardar1.ToolTip == "Eliminar")
        //                {
        //                    string IdAccesorio = (string)Session["IdAcc"];
        //                    txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();
        //                    Estado = "Eliminado";

        //                    int estado_item = 0;
        //                    estado_item = validarCaracteristicasItem();

        //                    string parametros = "";
        //                    reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
        //                    while (reader.Read())
        //                    {
        //                        parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
        //                    }
        //                    reader.Close();

        //                    //INGRESAMOS LOS DATOS A ELIMINAR
        //                    int cotacc = contpv.ActAcc(Convert.ToInt32(IdAccesorio), Convert.ToInt32(txtCantidad.Text),
        //                          LblPrecioUni.Text, lblPesoUni.Text, "0", txtObservaciones.Text, "0",
        //                          Nombre, FechaCrea, Convert.ToInt32(txtFUP.Text), Hora, Estado, Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboModelo.SelectedValue), estado_item);

        //                    //Inserto log_pv
        //                    string est = "Eliminado";
        //                    string Usuario = (string)Session["Nombre_Usuario"];
        //                    double total = Convert.ToDouble(LblPrecioUni.Text) * Convert.ToInt32(txtCantidad.Text);
        //                    int tipo_id = 0, modelo_id = 0;
        //                    if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
        //                        tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
        //                    if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
        //                        modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

        //                    controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text), LblPrecioUni.Text, total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);

        //                    this.LimpiarDetalle();
        //                    MensajeItemAccesorio();

        //                    Session["EstadoCom"] = "1";
        //                    this.CargarReporteDetalle();

        //                    btnGuardar1.ToolTip = "Guardar";
        //                    btnGuardar1.BorderColor = System.Drawing.Color.Transparent;
        //                    btnGuardar1.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //                    btnGuardar1.BorderWidth = 2;
        //                }

        //                else
        //                {
        //                    if (btnGuardar1.ToolTip == "Actualizar")
        //                    {
        //                        if ((contpv.poblarTipoItem(itemid).HasRows && cboTipo.SelectedItem.Text == "Seleccione el tipo") || (contpv.poblarModeloItem(itemid).HasRows && cboModelo.SelectedItem.Text == "Seleccione el modelo"))
        //                        {
        //                            mensaje = "Para confirmar el pedido de venta seleccionar el tipo y modelo para este accesorio";
        //                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //                        }

        //                        string IdAccesorio = Session["IdAcc"].ToString();
        //                        txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();
        //                        Estado = "Actualización";
        //                        Session["Estado"] = Estado;
        //                        int tipo = 0, modelo = 0;
        //                        if (!String.IsNullOrEmpty(cboTipo.SelectedValue.ToString()) && cboTipo.SelectedValue.ToString() != "Seleccione el tipo")
        //                            tipo = Convert.ToInt32(cboTipo.SelectedValue);

        //                        if (!String.IsNullOrEmpty(cboModelo.SelectedValue.ToString()) && cboModelo.SelectedValue.ToString() != "Seleccione el modelo")
        //                            modelo = Convert.ToInt32(cboModelo.SelectedValue);

        //                        int estado_item = 0;
        //                        estado_item = validarCaracteristicasItem();

        //                        //INGRESAMOS LOS DATOS A ACTUALIZAR
        //                        int cotacc = contpv.ActAcc(Convert.ToInt32(IdAccesorio), Convert.ToInt32(txtCantidad.Text.Replace(",", "")),
        //                            LblPrecioUni.Text, lblPesoUni.Text, "0", txtObservaciones.Text, "0",
        //                            Nombre, FechaCrea, Convert.ToInt32(txtFUP.Text), Hora, Estado, Convert.ToInt32(txtCodigo.Text), tipo, modelo, estado_item);

        //                        actualizarParametrosAccesorios(Convert.ToInt32(IdAccesorio));

        //                        string parametros = "";
        //                        reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
        //                        while (reader.Read())
        //                        {
        //                            parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
        //                        }
        //                        reader.Close();
        //                        //Inserto log_pv
        //                        string est = "Actualización";
        //                        string Usuario = (string)Session["Nombre_Usuario"];
        //                        double total = Convert.ToDouble(LblPrecioUni.Text) * Convert.ToInt32(txtCantidad.Text);
        //                        int tipo_id = 0, modelo_id = 0;
        //                        if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
        //                            tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
        //                        if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
        //                            modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

        //                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text), LblPrecioUni.Text, total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);

        //                        reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
        //                        while (reader.Read())
        //                        {
        //                            parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
        //                        }
        //                        reader.Close();

        //                        mensaje = "Item Actualizado exitosamente";
        //                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

        //                        txtFUP_TextChanged(sender, e);

        //                        MensajeItemAccesorio();

        //                        Session["EstadoCom"] = "1";
        //                        this.CargarReporteDetalle();
        //                        this.LimpiarDetalle();

        //                        btnGuardar1.ToolTip = "Guardar";
        //                        btnGuardar1.BorderColor = System.Drawing.Color.Transparent;
        //                        btnGuardar1.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
        //                        btnGuardar1.BorderWidth = 2;                                    
        //                    }
        //                }

        //                Session["FUP"] = txtFUP.Text;
        //                Session["TIPO"] = "Pedido De Venta No.";
        //                Session["NUMERO"] = txtPV.Text;
        //                Session["CLIENTE"] = cboCliente.SelectedValue;
        //                Session["OBRA"] = cboObra.SelectedValue;

        //                if (cboPais.SelectedValue == "8")
        //                {
        //                    Session["MONEDA"] = "COP";
        //                }
        //                else
        //                {
        //                    Session["MONEDA"] = "USD";
        //                }
        //            }

        //            btnConfVenta.Enabled = true;
        //            btnSolicFatura.Enabled = true; 
        //}

        protected void LimpiarDetalle()
        {
            txtNomAcc.Text = "";
            txtCodigo.Text = "";
            cboAccesorio.Items.Clear();
            cboAccesorio.Items.Add(new ListItem("Seleccione el accesorio", "0"));
            cboAccesorio.SelectedIndex = 0;
            cboGrupo.Items.Clear();
            cboGrupo.Items.Add(new ListItem("Seleccione el grupo", "0"));
            cboGrupo.SelectedIndex = 0;
            txtCantidad.Text = "0";
            txtPrecio.Text = "0";
            lblPesoUni.Text = "0";
            LEmpaque.Text = "0";
            LblPrecioUni.Text = "0";
            txtObservaciones.Text = "";
            imgItem.Visible = false;
            PanelEspecificaciones.Visible = false;
            this.cargarGrupoAcc();
        }
        
        private string MensajeItemAccesorio()
        {
             
            string mensaje = "";

            if (btnGuardar.ToolTip == "Guardar")
            {
                 
                    mensaje = "Accesorio ingresado con éxito.";
                
            }

            if (btnGuardar.ToolTip == "Actualizar")
            {
                 
                    mensaje = "Accesorio actualizado con éxito.";
                
            }
            
            if (btnGuardar.ToolTip == "Eliminar")
            {
                 
                    mensaje = "Accesorio eliminado con éxito.";
                 
            }

            return mensaje;
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("PedidoVenta.aspx");
        }

        protected void cboObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlCotAcc.Enabled = true;
            pnlCotAcc.Visible = true;
            this.cargarGrupoAcc();
            //DEFINIR CIUDAD DE LA OBRA
            CiudadObra();
        }

        protected void txtPrecio_TextChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            bool numerico = IsNumeric(txtPrecio.Text);

            if (numerico == false)
            {
                mensaje = "Digite el valor en números";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtPrecio.Text = "0";
            }
            else
            {
                CompararPrecio();
            }            
        }

        public static Boolean IsNumeric(string precio)
        {
            decimal result;
            return decimal.TryParse(precio, out result);
        }

        public static Boolean IsInt(string fup)
        {
            int result;
            return int.TryParse(fup, out result);
        }

        private void CompararPrecio()
        {
             string mensaje = "";
             bool result = IsNumeric(txtPrecio.Text);

             if (txtPrecio.Text == "" || result == false)
             {
                 mensaje = "Digite el precio";
                 ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
             }
             else
             {
                 decimal pUnitario = Convert.ToDecimal(LblPrecioUni.Text.Replace(",", ""));
                 decimal pFinal = Convert.ToDecimal(txtPrecio.Text.Replace(",", ""));

                 if (pFinal < pUnitario)
                 {
                      

                      
                         mensaje = "El precio ingresado es menor al precio real del accesorio. Debe ingresar un valor mas alto.";
                      
                     ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                 }
                 else
                 {
                     pFinal = pUnitario;
                 }
             }
        }

        protected void btnConfirmarPV_Click(object sender, EventArgs e)
        {
            string Anho = DateTime.Now.ToString("yy");
             
            string Nombre = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            string msjMail = "";
            int sf = 0;
            string sf_venta = "", sf_total = "";
            int rol = (int)Session["Rol"];
            btnConfVenta.Enabled = true;
            int tipoPedido = 0;

            if (rol == 3 || rol == 2 || rol == 9 || rol == 33 || rol == 30)
            {
                if (txtFUP.Text == "")
                {
                    mensaje = "Digite el PV ó FUP, para confirmar";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    reader = contpv.consultarSolicitudFacturacion(Convert.ToInt32(txtFUP.Text));
                    if (!reader.HasRows)
                    {
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();
                        btnConfVenta.Enabled = true;
                        mensaje = "Debe diligenciar la solicitud de facturación antes de confirmar el pedido de venta.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }

                    else
                    {
                        reader.Read();
                        sf = Convert.ToInt32(reader.GetValue(1));
                        sf_venta = reader.GetValue(2).ToString();
                        sf_total = reader.GetValue(3).ToString();
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();
                        //Consulto los items a Confirmar
                        SqlDataReader readerTipoItem = contpv.consultarTipoItem(Convert.ToInt32(txtFUP.Text));
                        if (!readerTipoItem.HasRows)
                        {
                            mensaje = "No hay elementos cotizados para este FUP";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }

                        else 
                        {
                            //bool confirmarVenta = true;
                            //reader = contpv.consultarEstadoItem(Convert.ToInt32(txtFUP.Text));
                            //while (reader.Read()) 
                            //{
                            //    if (Convert.ToInt32(reader.GetValue(0)) == 0) 
                            //    {
                            //        confirmarVenta = false;
                            //        break;                                    
                            //    }
                            //}

                            mensaje = validacionAccesorios(Convert.ToInt32(txtFUP.Text));

                            if (!String.IsNullOrEmpty(mensaje))
                            {                                
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(\""+mensaje+"\");", true);
                                btnConfVenta.Enabled = true;
                            }

                            else 
                            {
                                //consulto si el pv existe en SeguimientoPV
                                reader = contpv.ConsultarConfirmacionVenta(Convert.ToInt32(txtFUP.Text));                                
                                if (!reader.HasRows)
                                {
                                    List<int> especial = new List<int>();
                                    List<int> contenido = new List<int>();
                                    while (readerTipoItem.Read())
                                    {
                                        especial.Add(Convert.ToInt32(readerTipoItem.GetValue(0)));
                                        contenido.Add(Convert.ToInt32(readerTipoItem.GetValue(1)));
                                    }
                                    readerTipoItem.Close();
                                    readerTipoItem.Dispose();

                                    tipoPedido = validarItems(especial);
                                    int tipoContenido = validarItemsContenido(contenido);

                                    establecerTipoConetenido(tipoPedido, tipoContenido);
                                                                       
                                    //Tipo pedido 1 es Estandar
                                    if (tipoPedido == 1)
                                    {
                                        this.confirmarPV(tipoPedido, sf, Convert.ToInt32(txtFUP.Text), sf_venta, sf_total, out mensaje);
                                        this.cargaPV(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
                                        string usuario = (string)Session["Nombre_Usuario"];
                                        DateTime time = DateTime.Now;
                                        string format = "yyyy-dd-MM HH:MM:ss";

                                        contpv.insertarSeguimientoPV(Convert.ToInt32(txtFUP.Text), 7, 1, time.ToString(format), usuario, tipoContenido);

                                        msjMail = "";
                                        string correoUsuario = (string)Session["rcEmail"];
                                        string user = Session["Usuario"].ToString().ToUpper();
                                        string remitente = Session["CorreoSistema"].ToString();
                                        int idSF = 0;
                                        reader = contpv.consultarSfId(Convert.ToInt32(txtFUP.Text));
                                        if (reader.HasRows)
                                        {
                                            reader.Read();
                                            idSF = Convert.ToInt32(reader.GetValue(0)); 
                                        }
                                        Session["SfId"] = idSF;
                                        reader.Close();
                                        reader.Dispose();
                                        contpv.CerrarConexion();
                                        Session["Parte"] = 1;

                                        string version = "A";
                                        Session["Evento"] = 16;
                                        // envia correo
                                        fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));
                                        
                                        //contpv.enviarCorreo(2, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, idSF);
                                        //if (!String.IsNullOrEmpty(msjMail))
                                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                                        configurarControles();
                                        Reload_tbDetalle();
                                        mensaje += msjMail;
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                        this.CargarReporteDetalle();
                                        //this.CargarReporteEstado();                                        
                                    }
                                    //Tipo pedido 2 y 3 Especial y Mixto respectivamente
                                    else
                                    {
                                        this.confirmarPV(tipoPedido, sf, Convert.ToInt32(txtFUP.Text), sf_venta, sf_total, out mensaje);
                                        string usuario = (string)Session["Nombre_Usuario"];
                                        DateTime time = DateTime.Now;
                                        string format = "yyyy-dd-MM HH:MM:ss";

                                        contpv.insertarSeguimientoPV(Convert.ToInt32(txtFUP.Text), 2, 1, time.ToString(format), usuario, tipoContenido);
                                        
                                        string correoUsuario = (string)Session["rcEmail"];
                                        string user = Session["Usuario"].ToString().ToUpper();
                                        string remitente = Session["CorreoSistema"].ToString();
                                        int idSF = 0;
                                        reader = contpv.consultarSfId(Convert.ToInt32(txtFUP.Text));
                                        if (reader.HasRows)
                                        {
                                            reader.Read();
                                            idSF = Convert.ToInt32(reader.GetValue(0));                                            
                                        }
                                        reader.Close();
                                        reader.Dispose();
                                        contpv.CerrarConexion();

                                        configurarControles();
                                        Reload_tbDetalle();

                                        // envia correo
                                        string version = "A";
                                        Session["Evento"] = 23;
                                        Session["Parte"] = 1;
                                        Session["SfId"] = idSF;
                                        // envia correo
                                        fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                                        //contpv.enviarCorreo(3, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, idSF);
                                        //if (!String.IsNullOrEmpty(msjMail))
                                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";
                                        //mensaje += msjMail;
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                        this.CargarReporteDetalle();
                                        //this.CargarReporteEstado();                                
                                    }
                                }
                                else
                                {
                                    reader.Close();
                                    reader.Dispose();
                                    contpv.CerrarConexion();
                                    reader = contpv.seleccionarConfirmaciones(Convert.ToInt32(txtFUP.Text));
                                    if (reader.HasRows == true)
                                    {
                                        reader.Read();
                                        if (Convert.ToInt32(reader.GetValue(0)) == 0)
                                        {
                                            int conf_comercial = 1;
                                            string usuario = (string)Session["Nombre_Usuario"];

                                            DateTime time = DateTime.Now;
                                            string format = "yyyy-dd-MM HH:MM:ss";

                                            //Inserto en logPV la confirmacion del pv
                                            string Estado = "Confirma Comercial";
                                            controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Nombre, "", 0, 0, "");
                                            btnConfVenta.Enabled = false;

                                            //Actualizar seguimiento PV
                                            contpv.actualizarSeguimientoPVComercial(Convert.ToInt32(txtFUP.Text), conf_comercial, time.ToString(format), usuario, 2);

                                            mensaje = "Confirmación Exitosa PV No. " + txtPV.Text;
                                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                                            configurarControles();
                                            Reload_tbDetalle();                                            
                                        }


                                        else
                                        {
                                            mensaje = "El Pedido de Venta ya ha sido confirmado previamente.";
                                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(\"" + mensaje + "\");", true);
                                            btnConfVenta.Enabled = false;
                                        }
                                    }
                                    reader.Close();
                                    reader.Dispose();
                                    contpv.CerrarConexion();
                                }                                
                            }                     
                        }                        
                    }                     
                } 
            }
            else
            {
                mensaje = "No posee permisos para realizar esta accion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);            
            }
        }

        private void cargaPV(int fup, int planta)
        {
            // inserto en of accesorios
            controlsf.IngresarDatosOrdenAcc(fup);
            
            //Inserto log_pv
            string Estado = "Carga Items";
            string Usuario = (string)Session["Nombre_Usuario"];
            controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Usuario, "", 0 , 0 , "");

            //insertar la cantidad comprometida
            reader = contpv.seleccionarCantidadComprometer(fup, planta);
            if (reader.HasRows) 
            {
                DateTime time = DateTime.Now;
                string format = "yyyy-dd-MM HH:MM:ss";  
                while (reader.Read()) 
                {
                    contpv.insertarCantidadComprometida(Convert.ToInt32(reader.GetValue(2)), Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(3)), time.ToString(format), 1, Convert.ToInt32(reader.GetValue(1)), Convert.ToInt32(reader.GetValue(4)));
                }

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
        }

        //private void CorreoFinalVenta()
        //{
        //    string mensaje = "";
        //    string usuario = (string)Session["Nombre_Usuario"];
        //    string pais = (string)Session["Pais"];
        //    string fecha = System.DateTime.Today.ToShortDateString();

        //    string correoSistema = (string)Session["CorreoSistema"];
        //    string UsuarioAsunto = (string)Session["UsuarioAsunto"];

        //    Byte[] correo = new Byte[0];
        //    WebClient clienteWeb = new WebClient();
        //    clienteWeb.Dispose();
        //    clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
        //    correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/InformesFUP/COM_SolicitudFacturacionSeguimiento&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + txtFUP.Text.Trim() + "&version=A&parte=1");
        //    MemoryStream ms = new MemoryStream(correo);

        //    reader = controlsf.ObtenerAsesorComercial(Convert.ToInt32(pais));
        //    if (!reader.HasRows)
        //    {
        //        mensaje = "El pais no tiene asignado asesor comercial.";
        //        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
        //    }
        //    else
        //    {
        //        string Email = "";
        //        reader = controlsf.ObtenerMailPV("n_Rol", "p_Rol");
        //        while (reader.Read())
        //        {
        //            if (Email == "")
        //            {
        //                Email = reader.GetValue(0).ToString();
        //                Session["acEmail"] = Email;
        //            }
        //            else
        //            {
        //                Email = Email + "," + reader.GetValue(0).ToString();
        //                Session["acEmail"] = Email;
        //            }
        //        }
        //        reader.Close();

        //        string acEmail = "";
        //        reader = controlsf.ObtenerMailSF("n_Rol", "p_Rol");
        //        while (reader.Read())
        //        {
        //            if (acEmail == "")
        //            {
        //                acEmail = reader.GetValue(0).ToString();
        //                Session["Email"] = acEmail;
        //            }
        //            else
        //            {
        //                acEmail = acEmail + "," + reader.GetValue(0).ToString();
        //                Session["Email"] = acEmail;
        //            }
        //        }
        //        reader.Close();

        //        string ano = DateTime.Now.ToString("yy");
        //        string rcEmail = (string)Session["rcEmail"];
        //        string EmailCita = (string)Session["acEmail"];
        //        string MailSF = (string)Session["Email"];
        //        string MailFin = EmailCita + "," + MailSF;
        //        string paisprincipal = (string)Session["Pais"];

        //        //DEFINIMOS LA CLASE DE MAILMESSAGE
        //        MailMessage mail = new MailMessage();
        //        //INDICAMOS EL EMAIL DE ORIGEN
        //        mail.From = new MailAddress(correoSistema);
        //        //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
        //        mail.To.Add("ivanvidal@forsa.net.co");  
        //        //mail.To.Add(MailFin); activar
        //        //AÑADIMOS COPIA AL REPRESENTANTE
        //        //mail.CC.Add(rcEmail); activar
        //        //INCLUIMOS EL ASUNTO DEL MENSAJE
        //        mail.Subject = "SIO - PV Confirmación de Venta: "+ UsuarioAsunto  +" FUP No. " + txtPV.Text + " FUP. " + txtFUP.Text + " ";
        //        //AÑADIMOS EL CUERPO DEL MENSAJE
        //        mail.Body = "Pedido De Venta No. " + txtPV.Text + "ha sido CONFIRMADO. RECUERDE QUE NO PUEDE SER MODIFICADO." + " \n\n\n" +
        //        "Pais: " + paisprincipal + "\n\n" +
        //        "Cliente: " + cboCliente.Text + "\n\n" +
        //        "Proyecto: " + cboObra.Text + " \n\n\n" +
        //        "Cordialmente, " + "\n\n" + usuario + " ";
        //        //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
        //        mail.BodyEncoding = System.Text.Encoding.UTF8;
        //        //DEFINIMOS LA PRIORIDAD DEL MENSAJE
        //        mail.Priority = System.Net.Mail.MailPriority.Normal;
        //        //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
        //        mail.IsBodyHtml = false;
        //        mail.Attachments.Add(new Attachment(ms, "SF PV" + txtPV.Text + ".pdf"));
        //        //DECLARAMOS LA CLASE SMTPCLIENT
        //        SmtpClient smtp = new SmtpClient();
        //        //DEFINIMOS NUESTRO SERVIDOR SMTP
        //        smtp.Host = "smtp.office365.com";
        //        //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
        //        smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
        //        smtp.Port = 25;
        //        smtp.EnableSsl = true;

        //        ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
        //        SslPolicyErrors sslPolicyErrors)
        //        {
        //            return true;
        //        };
        //        try
        //        {
        //            smtp.Send(mail);
        //        }
        //        catch (Exception ex)
        //        {
        //            mensaje = "ERROR: " + ex.Message;
        //        }
        //    }
        //}

        ////private void CorreoVenta()
        //{
        //    string Nombre = (string)Session["Nombre_Usuario"];
        //    string CorreoUsuario = (string)Session["rcEmail"];
        //    string Fecha = System.DateTime.Today.ToLongDateString();
        //    string Email = "", EmailGerente = "", EmailAdmin = "", Sujeto = "", Cuerpo = "";
        //    string Anho = DateTime.Now.ToString("yy");

        //    string correoSistema = (string)Session["CorreoSistema"];
        //    string UsuarioAsunto = (string)Session["UsuarioAsunto"];
            
        //    //OBTENEMOR EL MAIL DE LOS QUE TIENEN PERMISO PV
        //    reader = contpv.ObtenerMailPV();
        //    while (reader.Read())
        //    {
        //        if (Email == "")
        //        {
        //            Email = reader.GetValue(0).ToString();
        //        }
        //        else
        //        {
        //            Email = Email + "," + reader.GetValue(0).ToString();
        //        }
        //    }
        //    reader.Close();

        //    //OBTENEMOR EL MAIL DE LOS ADMINISTRADORES
        //    reader = contpv.ObtenerMailAdmin();
        //    while (reader.Read())
        //    {
        //        if (EmailAdmin == "")
        //        {
        //            EmailAdmin = reader.GetValue(0).ToString();
        //        }
        //        else
        //        {
        //            EmailAdmin = EmailAdmin + "," + reader.GetValue(0).ToString();
        //        }
        //    }
        //    reader.Close();

        //    if (cboCliente.SelectedItem.Value == "3108" || cboCliente.SelectedItem.Value == "3384")
        //    {
        //        //OBTENEMOR EL MAIL DE LOS DE ARRENDADORA
        //        reader = contpv.ObtenerMailArrendadora();
        //        while (reader.Read())
        //        {
        //            if (CorreoUsuario == "")
        //            {
        //                CorreoUsuario = reader.GetValue(0).ToString();
        //            }
        //            else
        //            {
        //                CorreoUsuario = CorreoUsuario + "," + reader.GetValue(0).ToString();
        //            }
        //        }
        //        reader.Close();
        //    }

        //    //OBTENEMOR EL MAIL DE LOS GERENTES
        //    reader = contpv.ObtenerMailGerentes(Convert.ToInt32(cboPais.SelectedValue));
        //    while (reader.Read())
        //    {
        //        if (EmailAdmin == "")
        //        {
        //            EmailGerente = reader.GetValue(0).ToString();
        //        }
        //        else
        //        {
        //            EmailGerente = EmailAdmin + "," + reader.GetValue(0).ToString();
        //        }
        //    }
        //    reader.Close();

        //    string EmailFinal = Email + ',' + EmailGerente;

        //    Byte[] correo = new Byte[0];
        //    WebClient clienteWeb = new WebClient();
        //    clienteWeb.Dispose();
        //    clienteWeb.Credentials = new System.Net.NetworkCredential("forsa", "F0rUs3s4*", "FORSA");
        //    correo = clienteWeb.DownloadData(@"http://10.75.131.2:81/ReportServer?/Comercial/COM_CartaCotizacionPV&rs:format=PDF&rs:command=render&rs:ClearSession=true&numfup=" + txtFUP.Text.Trim() + "" +
        //        "&nombrer=" + Nombre + "&paisr=" + cboPais.SelectedItem.Text + "&correor=" + CorreoUsuario + "&fecha=" + Fecha + "");
        //    MemoryStream ms = new MemoryStream(correo);

        //    Sujeto = "SIO - PV Confirmación de Venta: "+ UsuarioAsunto  +" FUP No. " + txtFUP.Text + " AC-" + Anho + " PV No. " + txtPV.Text.Trim() + " ";

        //    Cuerpo = "Buen día, se ha confirmado la venta del pedido: " + txtPV.Text.Trim() + " RECUERDE QUE NO PUEDE SER MODIFICADO\n\n " +
        //    " FUP No:  " + txtFUP.Text + " AC-" + Anho + " \n\n " +
        //    " Cliente: " + cboCliente.SelectedItem.Text + " \n\n " +
        //    " País:  " + cboPais.SelectedItem.Text + " \n\n " +
        //    " Ciudad:  " + cboCiudad.SelectedItem.Text + " \n\n " +
        //    " Proyecto: " + cboObra.SelectedItem.Text + " \n\n " +
        //    " Contacto: " + cboContacto.SelectedItem.Text + " \n\n\n" +            
        //    " Cordialmente, " + "\n\n" + Nombre + "";

        //    //DEFINIMOS LA CLASE DE MAILMESSAGE
        //    MailMessage mail = new MailMessage();
        //    //INDICAMOS EL EMAIL DE ORIGEN
        //    mail.From = new MailAddress(correoSistema);
        //    //AÑADIMOS LA DIRRECCIÓN DE CORREO DESTINATARIO
        //    //mail.To.Add("ivanvidal@forsa.net.co");// ojo cambiar aquiva EmailFinal
        //    mail.To.Add(EmailFinal);// ojocambiar aquiva EmailFinal
        //    //AÑADIMOS COPIA AL REPRESENTANTE
        //    //mail.CC.Add("ivanvidal@forsa.net.co");// ojo cambiar
        //    mail.CC.Add(CorreoUsuario);// ojo cambiar aqui va CorreoUsuario
        //    //AÑADIMOS LA DIRRECCIÓN DE COPIA AL ADMINISTRADOR DEL APLICATIVO
        //    mail.Bcc.Add(EmailAdmin);
        //    //INCLUIMOS EL ASUNTO DEL MENSAJE
        //    mail.Subject = Sujeto;
        //    //AÑADIMOS EL CUERPO DEL MENSAJE
        //    mail.Body = Cuerpo;
        //    //INDICAMOS EL TIPO DE CODIFICACIÓN DEL MENSAJE
        //    mail.BodyEncoding = System.Text.Encoding.UTF8;
        //    //DEFINIMOS LA PRIORIDAD DEL MENSAJE
        //    mail.Priority = System.Net.Mail.MailPriority.Normal;
        //    //INDICAMOS SI EL CUERPO DEL MENSAJE ES HTML O NO
        //    mail.IsBodyHtml = false;
        //    //ADJUNTAMOS EL ARCHIVO
        //    mail.Attachments.Add(new Attachment(ms, "Carta.pdf"));  
        //    //DEFINIMOS NUESTRO SERVIDOR SMTP
        //    //DECLARAMOS LA CLASE SMTPCLIENT
        //    SmtpClient smtp = new SmtpClient();
        //    //DEFINIMOS NUESTRO SERVIDOR SMTP
        //    smtp.Host = "smtp.office365.com";
        //    //INDICAMOS LA AUTENTICACION DE NUESTRO SERVIDOR SMTP
        //    smtp.Credentials = new System.Net.NetworkCredential("informes@forsa.net.co", "Those7953");
        //    smtp.Port = 25;
        //    smtp.EnableSsl = true;
        //    ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain,
        //    SslPolicyErrors sslPolicyErrors)
        //    {
        //        return true;
        //    };
        //    try
        //    {
        //        smtp.Send(mail);
        //    }
        //    catch (Exception ex)
        //    {
        //        string mensaje = "ERROR: " + ex.Message;
        //    }

        //    ms.Close();
        //}
       
        protected void btnAnular_Click(object sender, EventArgs e)
        {
            
            string mensaje = "";

            int rol = (int)Session["Rol"];
            if (rol == 3 || rol == 2 || rol == 9 || rol == 33  || rol == 24)
            {
                if (txtPV.Text == "")
                {
                    mensaje = "Digite el número de PV";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    reader = contpv.ConsultarNumeroPedidoVenta(txtPV.Text);
                    if (reader.HasRows == true)
                    {
                        reader.Read();
                        if (reader.GetSqlBoolean(1).Value == false)
                        {
                            int anulado = contpv.AnularPV(Convert.ToInt32(txtFUP.Text));

                            string msjMail = "";
                            string usuario = (string)Session["Nombre_Usuario"];
                            string correoUsuario = (string)Session["rcEmail"];
                            string user = Session["Usuario"].ToString().ToUpper();
                            string remitente = Session["CorreoSistema"].ToString();

                            string version = "A";
                            Session["Evento"] = 27;
                            Session["Parte"] = 1;
                            // envia correo
                            fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                            //contpv.enviarCorreo(7, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                            //if (!String.IsNullOrEmpty(msjMail))
                            //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                             
                                mensaje = "El pedido de venta No " + txtPV.Text + " ha sido anulado.";
                             
                            mensaje += msjMail;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }
                        else
                        {
                            this.MensajeAnuladoPV();
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    contpv.CerrarConexion();
                }
            }
            else
            {
                mensaje = "No posee permisos para realizar esta accion";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        protected void chkLogisticaAlmacen_CheckedChanged(object sender, EventArgs e)
        {
            int chkAlmacen = 0;
            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";

            string usuario = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            int rol = (int)Session["Rol"];

            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                if (rol == 15)
                {
                    if (chkLogisticaAlmacen.Checked)
                    {
                        chkAlmacen = 1;
                        contpv.actualizarSeguimientoPVLogisticaAlmacen(chkAlmacen, time.ToString(format), usuario, Convert.ToInt32(txtFUP.Text), 4);

                        //Inserto log_pv
                        string Estado = "Recibido de Almacén";
                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, usuario, "", 0, 0, "");
                        habilitarAlmaceLogistica();
                        chkLogisticaAlmacen.Checked = true;

                        string msjMail = "";
                        string correoUsuario = (string)Session["rcEmail"];
                        string user = Session["Usuario"].ToString().ToUpper();
                        string remitente = Session["CorreoSistema"].ToString();

                        string version = "A";
                        Session["Evento"] = 26;
                        Session["Parte"] = 1;
                        // envia correo
                        fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                        //contpv.enviarCorreo(9, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                        //if (!String.IsNullOrEmpty(msjMail))
                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                        mensaje = "Confirmación realizada con éxito!" + msjMail;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        //this.CargarReporteEstado();
                        configurarControles();
                    }
                    else
                    {
                        chkLogisticaAlmacen.Checked = true;
                    }                    
                }
                else
                {
                    mensaje = "No posee permisos para realizar esta acción";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
        }

        protected void chkLogisticaAccesorios_CheckedChanged(object sender, EventArgs e)
        {
            int chkAccesorios = 0;
            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";

            string usuario = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            int rol = (int)Session["Rol"];

            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                if (rol == 15)
                {
                    if (chkLogisticaAccesorios.Checked)
                    {
                        chkAccesorios = 1;
                        contpv.actualizarSeguimientoPVLogisticaAccesorios(chkAccesorios, time.ToString(format), usuario, Convert.ToInt32(txtFUP.Text), 4);

                        //Inserto log_pv
                        string Estado = "Recibido de Accesorios";
                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, usuario, "", 0, 0, "");
                        habilitarAlmaceLogistica();
                        chkLogisticaAccesorios.Checked = true;

                        string msjMail = "";
                        string correoUsuario = (string)Session["rcEmail"];
                        string user = Session["Usuario"].ToString().ToUpper();
                        string remitente = Session["CorreoSistema"].ToString();

                        string version = "A";
                        Session["Evento"] = 20;
                        Session["Parte"] = 1;
                        // envia correo
                        fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                        //contpv.enviarCorreo(10, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                        //if (!String.IsNullOrEmpty(msjMail))
                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";
                        
                        mensaje = "Confirmación realizada con éxito!" + msjMail;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        //this.CargarReporteEstado();
                        configurarControles();
                    }
                    else 
                    {
                        chkLogisticaAccesorios.Checked = true;
                    }
                }
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }              
        }

        protected void txtOF_TextChanged(object sender, EventArgs e)
        {
            if (txtOF.Text == "0")
            {
            }
            else
            {
                string mensaje = "";
                reader = contpv.ConsultarOrdenAsociada(txtOF.Text);
                if (reader.HasRows)
                {
                    reader.Read();
                    mensaje = "Orden encontrada exitosamente";
                    txtOF.Text = reader.GetValue(0).ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    mensaje = "Digíte correctamente la orden, la orden ingresada no existe";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtOF.Text = "0";
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
        }

        protected void txtCantidad_TextChanged(object sender, EventArgs e)
        {            
            string mensaje = "";

            if (txtCantidad.Text == "")
            {

            }
            else
            {
                if (IsInt(txtCantidad.Text) == true)
                {
                    //string precioUni = LblPrecioUni.Text.Replace(",", "");
                    //double precioUnitario = Convert.ToDouble(precioUni.Replace(".", ","));

                    //double precio = Convert.ToInt32(txtCantidad.Text) * precioUnitario;

                    double precio = Convert.ToInt32(txtCantidad.Text.Replace(",", "")) * Convert.ToDouble(LblPrecioUni.Text.Replace(",", ""));                                                                                                                                            

                    txtPrecio.Text = precio.ToString("N", new CultureInfo("en-US"));
                    
                    if (Convert.ToInt32(txtCantidad.Text.Replace(",", "")) > Convert.ToDouble(txtDisponible.Text.Replace(",","")))
                    {
                        mensaje = "El Accesorio no tiene disponibilidad en el momento";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                    
                    }
                }
                else
                {
                    mensaje = "Digite la cantidad correctamente";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtCantidad.Text = "";
                }
            }

            itemid = Convert.ToInt64(Session["Item"]);
            item_planta_id = Convert.ToInt64(Session["Item_Planta"]);
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cliente.aspx?idCliente=" + Session["Cliente"].ToString());
        }

        protected void cargarGrupoAcc()
        {
            string usuario = (string)Session["Usuario"];
            int idioma = 1;
            cboGrupo.Items.Clear();
            cboGrupo.Items.Add(new ListItem("Seleccione el grupo", "0"));
            cboGrupo.SelectedIndex = 0;
            reader = contpv.ConsultarGrupoAcc(Convert.ToInt32(cboPlanta.SelectedValue), idioma);
            if (reader.HasRows) 
            {
                while (reader.Read())
                {
                    cboGrupo.Items.Add(new ListItem(reader.GetString(3).ToString(), reader.GetInt32(6).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void cboGrupo_SelectedIndexChanged(object sender, EventArgs e)
        {
            int idioma = 1;
            btnGuardar.ToolTip = "Guardar";
            PanelEspecificaciones.Visible = false;
            txtCodigo.Text = "";
            this.LimpiarControles();
            imgItem.Visible = false;
            reader = contpv.ConsultarDetalleAcc(Convert.ToInt32(cboGrupo.SelectedValue), idioma);
            cboAccesorio.Items.Clear();
            cboAccesorio.Items.Add(new ListItem("Seleccione el accesorio", "0"));
            cboAccesorio.SelectedIndex = 0;
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboAccesorio.Items.Add(new ListItem(reader.GetString(1).ToString(), reader.GetInt32(0).ToString()));
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        //Cargar combo planta
        private void poblarPlanta()
        {
            string usuario = (string)Session["Usuario"];
            int plantaDefault = 0;
            cboPlanta.Items.Add(new ListItem("Seleccione la planta", "0"));
            reader = contubi.poblarPlantaUsuario(usuario);
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    plantaDefault = reader.GetInt32(2);
                    cboPlanta.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }

                
            }
            reader.Close();
            reader.Dispose();
            contubi.cerrarConexion();
            //cboPlanta.SelectedValue = plantaDefault.ToString();
        }

        //Cargar combo modelo  
        private void poblarModeloItem(Int64 item) 
        {
            cboModelo.Items.Clear();
            cboModelo.Items.Add(new ListItem("Seleccione el modelo" , "0"));
            cboModelo.SelectedIndex = 0;
            reader = contpv.poblarModeloItem(item);
            if (reader.HasRows)
            {
                Panel1.Visible = true;
                cboModelo.Visible = true;
                lblModeloItem.Visible = true;
                while (reader.Read())
                {
                    cboModelo.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else
            {
                cboModelo.Visible = false;
                lblModeloItem.Visible = false;
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        //cargar combo tipo
        private void poblarTipoItem(Int64 item) 
        {
            cboTipo.Items.Clear();
            cboTipo.Items.Add(new ListItem("Seleccione el tipo", "0"));
            cboTipo.SelectedIndex = 0;
            reader = contpv.poblarTipoItem(item);
            if (reader.HasRows)
            {
                Panel1.Visible = true;
                cboTipo.Visible = true;
                lblTipoItem.Visible = true;
                while (reader.Read())
                {
                    cboTipo.Items.Add(new ListItem(reader.GetString(0), reader.GetInt32(1).ToString()));
                }
            }
            else 
            {
                cboTipo.Visible = false;
                lblTipoItem.Visible = false;
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void CargarImagenItem(Int64 item)
        {
            reader = contpv.CargarImagenItem(item);
           
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    //imgItem.ImageUrl = "~" + reader.GetValue(0).ToString();
                    imgItem.ImageUrl = "~" + "/Fotos_Items/" + reader.GetValue(0).ToString();
                    //imgItem.ImageUrl = @"I:\Fotos_Item\" + reader.GetValue(0).ToString();
                    imgItem.Visible = true;
                }
            }
            else
                imgItem.Visible = false;

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();  

        }

        protected void MostrarTipoItem(Int64 item) 
        {            
            reader = contpv.MostrarTipoItem(item);
            if (reader.HasRows)
            {
                reader.Read();
                lblMostrarTipo.Visible = true;
                lblMostrarTipoItem.Visible = true;
                if (Convert.ToInt32(reader.GetValue(0)) == 1)
                {
                    lblMostrarTipoItem.Text = "Especial";
                    lblMostrarTipoItem.BackColor = Color.Yellow;
                }
                else
                {
                    lblMostrarTipoItem.Text = "Estándar";
                    lblMostrarTipoItem.BackColor = Color.Transparent;
                }        
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        private void llenarParametros(Int64 item)
        {            
            reader = contpv.ConsultarParametrosItem(item);
            if (reader.HasRows)
            {
                Panel2.Visible = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("lblParametroId");
                dt.Columns.Add("lblParametro");
                dt.Columns.Add("txtParametro");

                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["lblParametroId"] = reader.GetValue(1).ToString();
                    row["lblParametro"] = reader.GetValue(0).ToString();
                    dt.Rows.Add(row);
                }

                GridView1.Dispose();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["parametros"] = dt;                
            }
            else
            {
                DataTable dt = new DataTable();
                GridView1.Dispose();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["parametros"] = dt;
                Panel2.Visible = false;
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }
        
        private void calcularPrecioItem(Int64 item, int cliente, int planta, int cliente_tipo)
        {
            int moneda = 0;
            moneda = Convert.ToInt32(Session["MonedaID"].ToString());

            reader = contpv.descripcionMoneda(moneda);
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    LblMoneda.Text = reader.GetValue(0).ToString();                
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            reader = contpv.ConsultarPrecioItem(item, cliente, planta, cliente_tipo, moneda);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    if (!String.IsNullOrEmpty(reader.GetValue(8).ToString()))
                        LblPrecioUni.Text = Convert.ToDouble(reader.GetValue(8)).ToString("N", new CultureInfo("en-US"));
                    if (LblPrecioUni.Text == "0")
                    {
                        txtPrecio.Enabled = false;
                        string mensaje = "El Accesorio no tiene Precio, No se puede cotizar, comuniquese con Costos.";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }

                    else
                    {
                        Label4.Visible = true;
                        txtCantidad.Visible = true;
                        Label5.Visible = true;
                        txtDisponible.Visible = true;
                    }
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();              
        }

        private void cantidadDisponibleItem(int erp, int planta) 
        {
            int cantidadDisponible = 0;
            int cantidadComprometida = 0;
            //reader = contpv.seleccionarCantidadComprometida(erp, planta);
            //if (reader.HasRows) 
            //{
            //    reader.Read();
            //    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
            //        cantidadComprometida = Convert.ToInt32(reader.GetValue(0));
            //}
            //reader.Close();
            //reader.Dispose();
            //contpv.CerrarConexion();
            // consulta la cantidad disponible en el erp de la compañia
            reader = contpv.cantidadDisponibleItem(erp, planta);
            if (reader.HasRows)
            {
                reader.Read();
                Label5.Visible = true;
                txtDisponible.Visible = true;
                cantidadDisponible = Convert.ToInt32(reader.GetValue(2)) - cantidadComprometida;
                if (cantidadDisponible <= 0)
                {
                    cantidadDisponible = 0;
                    string mensaje = "El Accesorio no tiene disponibilidad, Cantidad: " + cantidadDisponible.ToString();
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtDisponible.Text = cantidadDisponible.ToString("N0", new CultureInfo("en-US"));
                }
                else
                {
                    txtDisponible.Text = cantidadDisponible.ToString("N0", new CultureInfo("en-US"));
                }


                if (cantidadDisponible > Convert.ToDouble(txtCantidad.Text))
                {
                    txtDisponible.Text = cantidadDisponible.ToString("N0", new CultureInfo("en-US"));
                }
                else
                {
                    string mensaje = "El Accesorio no tiene disponibilidad en el momento";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    txtDisponible.Text = cantidadDisponible.ToString("N0", new CultureInfo("en-US"));
                }
            }
            else
            {
                cantidadDisponible = 0;
                string mensaje = "El Accesorio no tiene disponibilidad, Cantidad: " + cantidadDisponible.ToString();
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                txtDisponible.Text = cantidadDisponible.ToString("N0", new CultureInfo("en-US"));
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        private void requerirPlanos(Int64 item)
        {
            reader = contpv.requerirPlanos(item);
            if (reader.HasRows)
            {
                if (!String.IsNullOrEmpty(txtFUP.Text))
                {
                    Panel3.Visible = true;
                    btnSubirPlano.Enabled = true;
                    btnSubirPlano.Visible = true;
                    lblPlano.Visible = false;
                }
                else
                {
                    Panel3.Visible = true;
                    lblPlano.Text = "Este Item requiere Plano, Despues de Adicionarlo Suba el Plano ";
                    btnSubirPlano.Visible = false;
                    btnSubirPlano.Enabled = false;
                }
            }
            else
            {
                Panel3.Visible = false;
                btnSubirPlano.Enabled = false;
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void grdDetalle_RowEditing1(object sender, GridViewEditEventArgs e)
        {
            int rol = (int)Session["Rol"];
            grdDetalle.EditIndex = e.NewEditIndex;
            Reload_tbDetalle();
            System.Web.UI.WebControls.TextBox txt_descrip = (System.Web.UI.WebControls.TextBox)grdDetalle.Rows[e.NewEditIndex].FindControl("textObservacion");
            System.Web.UI.WebControls.TextBox txt_cantidadDetalle = (System.Web.UI.WebControls.TextBox)grdDetalle.Rows[e.NewEditIndex].FindControl("textCantidad");
            
            reader = contpv.consultarCirreVenta(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    txt_descrip.Enabled = false;
                    txt_cantidadDetalle.Enabled = false;
                }
            }

            else 
            {
                if (Session["ConfirmaAsistente"].ToString() == "1" && (rol == 9 || rol == 2))
                {
                    txt_descrip.Enabled = false;
                    txt_cantidadDetalle.Enabled = false;
                }
                else if (rol == 4 || rol == 25)
                {
                    txt_descrip.Enabled = false;
                    txt_cantidadDetalle.Enabled = false;
                }
                else if (Session["ConfirmaComercial"].ToString() == "1" && (rol == 3 || rol == 30))
                {
                    txt_descrip.Enabled = false;
                    txt_cantidadDetalle.Enabled = false;
                }
                else if ((Session["ConfirmaAsistente"].ToString() == "0" && (rol == 9 || rol == 2)) || (Session["ConfirmaComercial"].ToString() == "0" && (rol == 3 || rol == 30)))
                {
                    txt_descrip.Enabled = true;
                    txt_cantidadDetalle.Enabled = true;
                    txt_descrip.Focus();
                    txt_cantidadDetalle.Focus();
                }
                else
                {
                    txt_descrip.Enabled = false;
                    txt_cantidadDetalle.Enabled = false;
                }            
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void grdDetalle_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            PanelEspecificaciones.Visible = false;
            double total = 0;

            //int index = Convert.ToInt32(e.RowIndex);
            System.Web.UI.WebControls.Label labelnum = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.RowIndex].FindControl("consecutivo"));
            int num_consecutivo = int.Parse(labelnum.Text) - 1;
            string b = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.RowIndex].FindControl("idItem")).Text;
            int id = Convert.ToInt32(b);
            reader = contpv.CambiarEstadoAccesorio(id);

            string parametros = "";
            reader = contpv.cargarParametrosItem(id);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
             
            var dt = Session["DT"] as DataTable;
            dt.Rows[num_consecutivo].Delete(); 
            Session["DT"] = dt;
            grdDetalle.EditIndex = -1;
            Reload_tbDetalle();
            reader = contpv.ConsultarDetalleFUP(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    total += Convert.ToDouble(reader.GetValue(4).ToString());
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            ventaTotal.Text = total.ToString("N", new CultureInfo("en-US"));

            reader = contpv.cargarCotizacionAccesorio(id);
            if (reader.HasRows)
            {
                reader.Read();
                string Estado = "Eliminado";
                string Nombre = (string)Session["Nombre_Usuario"];
                controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), Estado, Nombre, reader.GetValue(4).ToString(), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)), parametros);
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void grdDetalle_RowCancelingEdit1(object sender, GridViewCancelEditEventArgs e)
        {
            grdDetalle.EditIndex = -1;
            PanelEspecificaciones.Visible = false;
            imgItem.Visible = false;
            Reload_tbDetalle();
        }

        protected void grdDetalle_RowUpdating1(object sender, GridViewUpdateEventArgs e)
        {
            DataTable dt = Session["DT"] as DataTable;
            int a = grdDetalle.PageIndex;
            System.Web.UI.WebControls.Label labelnum = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.RowIndex].FindControl("consecutivo"));
            int num_consecutivo = int.Parse(labelnum.Text) - 1; 
            //int index = Convert.ToInt32(e.RowIndex);
            //int id = Convert.ToInt32(grdDetalle.Rows[index].Cells[0].Text.ToString());
            //int id = Convert.ToInt32(grdDetalle.DataKeys[num_consecutivo].Value);
            string b = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.RowIndex].FindControl("idItem")).Text;
            int id = Convert.ToInt32(b);
            string strObservacion = ((System.Web.UI.WebControls.TextBox)grdDetalle.Rows[e.RowIndex].FindControl("textObservacion")).Text;
            dt.Columns["cot_observacion"].ReadOnly = false;
            dt.Columns["cot_observacion"].MaxLength = 100;
            dt.Rows[num_consecutivo]["cot_observacion"] = strObservacion;

            string strCantidad = ((System.Web.UI.WebControls.TextBox)grdDetalle.Rows[e.RowIndex].FindControl("textCantidad")).Text;
            dt.Columns["cot_cantidad"].ReadOnly = false;
            dt.Columns["cot_cantidad"].MaxLength = 100;
            dt.Rows[num_consecutivo]["cot_cantidad"] = strCantidad;

            string precioUnitario = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.RowIndex].FindControl("cot_acc_precio_unitario")).Text; 
            double precio = Convert.ToDouble(precioUnitario) * Convert.ToDouble(strCantidad);
            dt.Columns["cot_acc_precio_total"].ReadOnly = false;
            dt.Columns["cot_acc_precio_total"].MaxLength = 100;
            dt.Rows[num_consecutivo]["cot_acc_precio_total"] = precio.ToString("N", new CultureInfo("en-US"));

            contpv.ActulizarAccesorioFUP(id,strObservacion, Convert.ToDouble(strCantidad), precio, Convert.ToInt32(txtFUP.Text));
           
            Session["IdAcc"] = id;
            string parametros = "";
            reader = contpv.cargarParametrosItem(id);
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + "/ ";
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            reader = contpv.cargarCotizacionAccesorio(id);
            if(reader.HasRows)
            {
                reader.Read();
                string Estado = "Actualización";
                string Nombre = (string)Session["Nombre_Usuario"];
                controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(reader.GetValue(0)), Convert.ToInt32(reader.GetValue(1)), reader.GetValue(2).ToString(), reader.GetValue(3).ToString(), Estado ,Nombre ,reader.GetValue(4).ToString(), Convert.ToInt32(reader.GetValue(5)), Convert.ToInt32(reader.GetValue(6)), parametros);
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
            
            double total = 0;
            reader = contpv.ConsultarDetalleFUP(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    total += Convert.ToDouble(reader.GetValue(4).ToString());
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            ventaTotal.Text = total.ToString("N", new CultureInfo("en-US"));
            Session["DT"] = dt;
            grdDetalle.EditIndex = -1;
            Reload_tbDetalle();
        }

        private bool insertarParametros(int idAcc) 
        {
            List<string> parametros = new List<string>();
            List<int> idParametros = new List<int>();
            bool reqParametro = false;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
                string parametro = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("textParametro")).Text;
                if (String.IsNullOrEmpty(parametro))
                {
                    reqParametro = true;
                    parametro = "";
                }
                parametros.Add(parametro);
                idParametros.Add(id);
            }
            
            
            if (parametros.Count > 0) 
            {
                contpv.insertarParametros(idAcc, idParametros, parametros);            
            }

            return reqParametro;
        }

        protected void grdDetalle_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            //int index = Convert.ToInt32(e.NewSelectedIndex.ToString());
            //int id = Convert.ToInt32(grdDetalle.Rows[index].Cells[0].Text.ToString());

            System.Web.UI.WebControls.Label labelnum = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.NewSelectedIndex].FindControl("consecutivo"));
            int num_consecutivo = int.Parse(labelnum.Text);
            Session["ConsecutivoItem"] = num_consecutivo;
            
            string b = ((System.Web.UI.WebControls.Label)grdDetalle.Rows[e.NewSelectedIndex].FindControl("idItem")).Text;
            int id = Convert.ToInt32(b);
            Session["IdAcc"] = id;
            reader = contpv.cargarCotizacionAccesorio(id);

            if (reader.HasRows)
            {
                reader.Read();
                PanelEspecificaciones.Visible = true;
                Session["Item"] = reader.GetValue(14).ToString();
                Session["Item_Planta"] = reader.GetValue(12).ToString();
                itemid = Convert.ToInt64(Session["Item"]);
                item_planta_id = Convert.ToInt64(Session["Item_Planta"]);

                txtCodigo.Text = reader.GetValue(0).ToString();
                LblPrecioUni.Text = Convert.ToDouble(reader.GetValue(2)).ToString("N", new CultureInfo("en-US"));
                txtObservaciones.Text = reader.GetValue(4).ToString();
                txtCantidad.Text = Convert.ToInt32(reader.GetValue(1)).ToString("N0", new CultureInfo("en-US"));
                txtPrecio.Text = Convert.ToDouble(reader.GetValue(3)).ToString("N", new CultureInfo("en-US"));
                string lblPesoUniItem = "";
                string LEmpaqueItem = "";
                string LPesoEmpItem = "";

                if (!String.IsNullOrEmpty(reader.GetValue(16).ToString()))
                {
                    LblFabricado.Visible = true;
                    if (Convert.ToInt32(reader.GetValue(16)) == 0)
                        LblFabricado.Text = "Almacén";
                    else
                        LblFabricado.Text = "Fabricado";
                }

                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(13))))
                    lblPesoUniItem = Convert.ToString(reader.GetValue(13));
                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(7))))
                    LEmpaqueItem = Convert.ToString(reader.GetValue(7));
                if (!String.IsNullOrEmpty(Convert.ToString(reader.GetValue(8))))
                    LPesoEmpItem = Convert.ToString(reader.GetValue(8));

                //Tipo
                if (reader.GetInt32(5) != 0)
                {
                    List<string> listTipo = new List<string>();
                    cboTipo.Items.Clear();
                    SqlDataReader readerTipo = null;
                    readerTipo = contpv.consultarTipo(Convert.ToInt32(reader.GetValue(5)));
                    readerTipo.Read();
                    cboTipo.Items.Add(new ListItem(readerTipo.GetString(1), readerTipo.GetInt32(0).ToString()));
                    cboTipo.Items.Add(new ListItem("Seleccione el tipo", "0"));
                    cboTipo.SelectedIndex = 0;
                    listTipo.Add(readerTipo.GetString(1));
                    readerTipo.Close();
                    readerTipo.Dispose();

                    readerTipo = contpv.poblarTipoItem(item_planta_id);
                    if (readerTipo.HasRows)
                    {
                        Panel1.Visible = true;
                        cboTipo.Visible = true;
                        lblTipoItem.Visible = true;
                        while (readerTipo.Read())
                        {
                            if (!listTipo.Contains(readerTipo.GetString(0)))
                                cboTipo.Items.Add(new ListItem(readerTipo.GetString(0), readerTipo.GetInt32(1).ToString()));
                        }

                        readerTipo.Close();
                        readerTipo.Dispose();
                    }

                    else 
                    {
                        cboTipo.Visible = false;
                        lblTipoItem.Visible = false;                    
                    }
                }

                else 
                {
                    cboTipo.Items.Clear();
                    SqlDataReader readerTipo = null;
                    readerTipo = contpv.poblarTipoItem(item_planta_id);
                    if (readerTipo.HasRows)
                    {
                        cboTipo.Items.Add(new ListItem("Seleccione el tipo", "0"));
                        cboTipo.SelectedIndex = 0;
                        Panel1.Visible = true;
                        cboTipo.Visible = true;
                        lblTipoItem.Visible = true;
                        while (readerTipo.Read())
                        {
                            cboTipo.Items.Add(new ListItem(readerTipo.GetString(0), readerTipo.GetInt32(1).ToString()));
                        }
                        readerTipo.Close();
                        readerTipo.Dispose();
                    }
                    else 
                    {
                        cboTipo.Visible = false;
                        lblTipoItem.Visible = false;
                    }
                }

                //Modelo
                if (reader.GetInt32(6) != 0)
                {
                    List<string> listModelo = new List<string>();
                    cboModelo.Items.Clear();
                    SqlDataReader readerModelo = null;
                    readerModelo = contpv.consultarModelo(Convert.ToInt32(item_planta_id));
                    readerModelo.Read();
                    cboModelo.Items.Add(new ListItem(readerModelo.GetString(1), readerModelo.GetInt32(0).ToString()));
                    cboModelo.Items.Add(new ListItem("Seleccione el modelo", "0"));
                    cboModelo.SelectedIndex = 0;
                    listModelo.Add(readerModelo.GetString(1));
                    readerModelo.Close();
                    readerModelo.Dispose();

                    readerModelo = contpv.poblarModeloItem(item_planta_id);
                    if (readerModelo.HasRows)
                    {
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        cboModelo.Visible = true;
                        lblModeloItem.Visible = true;
                        while (readerModelo.Read())
                        {
                            if (!listModelo.Contains(readerModelo.GetString(0)))
                                cboModelo.Items.Add(new ListItem(readerModelo.GetString(0), readerModelo.GetInt32(1).ToString()));
                        }
                        readerModelo.Close();
                        readerModelo.Dispose();
                    }
                    else 
                    {
                        cboModelo.Visible = false;
                        lblModeloItem.Visible = false;                    
                    }
                }

                else 
                {
                    cboModelo.Items.Clear();
                    SqlDataReader readerModelo = null;
                    readerModelo = contpv.poblarModeloItem(item_planta_id);
                    if (readerModelo.HasRows)
                    {
                        cboModelo.Items.Add(new ListItem("Seleccione el modelo", "0"));
                        cboModelo.SelectedIndex = 0;
                        Panel1.Visible = true;
                        Panel2.Visible = true;
                        cboModelo.Visible = true;
                        lblModeloItem.Visible = true;
                        while (readerModelo.Read())
                        {
                            cboModelo.Items.Add(new ListItem(readerModelo.GetString(0), readerModelo.GetInt32(1).ToString()));
                        }
                        readerModelo.Close();
                        readerModelo.Dispose();
                    }
                    else 
                    {
                        cboModelo.Visible = false;
                        lblModeloItem.Visible = false;
                    }
                }

                SqlDataReader readerAccesorio = null;
                readerAccesorio = contpv.ConsultarAccesorioCodigo(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedItem.Value));
                readerAccesorio.Read();

                cargarGrupoAcc();
                cboAccesorio.Items.Clear();  
                cboGrupo.SelectedValue = readerAccesorio.GetValue(6).ToString();
                cboAccesorio.Items.Add(new ListItem(readerAccesorio.GetValue(4).ToString(), readerAccesorio.GetValue(0).ToString()));
                
                readerAccesorio.Close();
                readerAccesorio.Dispose();

                CargarImagenItem(itemid);
                MostrarTipoItem(item_planta_id);
                cargarParametrosItem(id);

                cantidadDisponibleItem(Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboPlanta.SelectedValue));
                tipoClienteid = Convert.ToInt32(Session["TipoClienteID"]);
                calcularPrecioItem(item_planta_id, Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue), tipoClienteid);
                requerirPlanos(item_planta_id);
                
                if (!String.IsNullOrEmpty(lblPesoUniItem))
                {
                    Label1.Visible = true;
                    lblPesoUni.Visible = true;
                    lblPesoUni.Text = lblPesoUniItem;
                }

                if (!String.IsNullOrEmpty(LEmpaqueItem))
                {
                    Label11.Visible = true;
                    LEmpaque.Visible = true;
                    LEmpaque.Text = LEmpaqueItem;
                }

                if (!String.IsNullOrEmpty(LPesoEmpItem))
                {
                    Label3.Visible = true;
                    LPesoEmp.Visible = true;
                    LPesoEmp.Text = LPesoEmpItem;
                }

                LPrecioUni.Visible = true;
                LblPrecioUni.Visible = true;
                LblMoneda.Visible = true;
                Label7.Visible = true;
                txtPrecio.Visible = true;
                txtCantidad.Visible = true;
                Label5.Visible = true;
                txtDisponible.Visible = true;
                
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();

                btnGuardar.ToolTip = "Actualizar";
                btnCargarPrecio.Enabled = true;

            }
            else
            {
                string mensaje = "";
                PanelEspecificaciones.Visible = false;
                imgItem.Visible = false;
                mensaje = "No existe accesorio con la descripción ingresada.";
                cargarGrupoAcc();
                txtNomAcc.Text = "";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                this.LimpiarControles();
            }
            e.NewSelectedIndex = -1;
            grdDetalle.EditIndex = -1;
            grdDetalle.DataSource = Session["DT"] as DataTable;
            grdDetalle.DataBind();            
        }

        private void cargarParametrosItem(int idAcc) 
        {
            reader = contpv.cargarParametrosItem(idAcc);
            if (reader.HasRows)
            {
                Panel2.Visible = true;
                DataTable dt = new DataTable();
                dt.Columns.Add("lblParametroId");
                dt.Columns.Add("lblParametro");
                dt.Columns.Add("txtParametro");

                while (reader.Read())
                {
                    DataRow row = dt.NewRow();
                    row["lblParametroId"] = reader.GetValue(0).ToString();
                    row["lblParametro"] = reader.GetValue(2).ToString();
                    row["txtParametro"] = reader.GetValue(1).ToString();
                    dt.Rows.Add(row);
                }
                GridView1.Dispose();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["parametros"] = dt;                
            }

            else 
            {
                DataTable dt = new DataTable();
                GridView1.Dispose();
                GridView1.DataSource = dt;
                GridView1.DataBind();
                Session["parametros"] = dt;
                Panel2.Visible = false;
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        private void actualizarParametrosAccesorios(int idAccesorio) 
        {
            List<string> descripcion = new List<string>();
            List<int> idParametros = new List<int>();
            bool reqParametro = false;
            string mensaje = "";
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
                string parametro = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("textParametro")).Text;
                if (String.IsNullOrEmpty(parametro))
                {
                    reqParametro = true;
                    parametro = "";
                }
                descripcion.Add(parametro);
                idParametros.Add(id);
            }

            if (reqParametro)
            {
                mensaje = "Para confirmar el pedido de venta se deben llenar los parámetros requeridos";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }

            if (idParametros.Count > 0)
            {
                contpv.actualizarParametrosAccesorios(idAccesorio, idParametros, descripcion);
            }            
        }

        protected void btnSubirPlano_Click(object sender, EventArgs e)
        {
            Session["FUP"] = txtFUP.Text;
            Session["IdAcc"] = Session["IdAcc"];
            Session["Nombre_Usuario"] = Session["Nombre_Usuario"];
            Session["ERP"] = txtCodigo.Text;
            Session["ConsecutivoItem"] = Session["ConsecutivoItem"];
            string script = "window.open('WebForm2.aspx', '_blank','width=500', 'height=70');";
            ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true); 
        }

        protected void btnSolicFatura_Click(object sender, EventArgs e)
        {
            Session["Idioma"] = Session["Idioma"];
            Session["Pais"] = cboPais.SelectedValue;
            Session["Ciudad"] = cboCiudad.SelectedValue;
            Session["FUP"] = txtFUP.Text;
            Session["Planta"] = cboPlanta.SelectedItem.Text;
            Session["rcID"] = Session["rcID"];
            Session["VER"] = "A";
            Session["DescTipo"] = "PEDIDO DE VENTA";
            Session["PaisNombre"] = cboPais.SelectedItem.Text;
            Session["TIPO"] = "PV";
            Session["NUMERO"] = txtPV.Text;
            Session["CLIENTE"] = cboCliente.SelectedItem.Text;
            Session["OBRA"] = cboObra.SelectedItem.Text;

            int rol = (int)Session["Rol"];
            string mensaje = "";

            if (rol != 4) 
            {
                string Param = "PV";
                int IngSF = controlfup.IngSF(Convert.ToInt32(txtFUP.Text), "A", Session["Nombre_Usuario"].ToString(), Param);

                //Inserto en logPV la SF
                string Nombre = Session["Nombre_Usuario"].ToString();
                string Estado = "Solicitud de Facturación";
                controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Nombre, "", 0, 0, "");

                string script = "window.open('SolicitudFacturacionNew.aspx', '_blank');";
                ScriptManager.RegisterStartupScript(this, typeof(Page), "script", script, true);            
            }

            else 
            {
                mensaje = "No tiene los permisos para realizar esta acción";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private int validarItems(List<int> especial)
        {
            int tipoPedido = 1;
            int valor;
            bool especialAcc = false;
            bool estandarAcc = false;

            for (int i = 0; i < especial.Count; i++)
            {
                valor = especial.ElementAt(i);
                if (valor == 0)
                {
                    estandarAcc = true;
                    tipoPedido = 1;
                }
                else if(valor == 1) 
                {
                    especialAcc = true;
                    tipoPedido = 2;                    
                }              
            }

            if (estandarAcc && especialAcc)
                tipoPedido = 3;

            return tipoPedido;
        }

        private int validarItemsContenido(List<int> contenido)
        {
            int tipoContenido = 0;
            int valorContenido;
            bool almacen = false;
            bool accesorio = false;

            for (int i = 0; i < contenido.Count; i++)
            {
                valorContenido = contenido.ElementAt(i);
                if (valorContenido == 0)
                {
                    almacen = true;
                    tipoContenido = 1;
                }
                else if (valorContenido == 1) 
                {
                    accesorio = true;
                    tipoContenido = 2;                    
                } 
            }

            if (almacen && accesorio)
                tipoContenido = 3;

            return tipoContenido;
        }

        private int validarCaracteristicasItem() 
        {
            int estado_item = 0;
            bool parametroListo = true;
            bool reqPlano = false;

            List<string> parametros = new List<string>();
            List<int> idParametros = new List<int>();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
                string parametro = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("textParametro")).Text;
                if (String.IsNullOrEmpty(parametro))
                {
                    parametroListo = false;
                    parametro = "";
                }
            }

            reader = contpv.requierePlanoItem(Convert.ToInt32(cboPlanta.SelectedValue), Convert.ToInt32(txtCodigo.Text));
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString())) 
                {
                    if (Convert.ToInt32(reader.GetValue(0)) == 1)
                        reqPlano = true;

                    else reqPlano = false;
                }                
                else reqPlano = false;                
            }

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            if (!parametroListo ||  reqPlano || (contpv.poblarTipoItem(item_planta_id).HasRows && cboTipo.SelectedItem.Text == "Seleccione el tipo") || (contpv.poblarModeloItem(item_planta_id).HasRows && cboModelo.SelectedItem.Text == "Seleccione el modelo"))
                estado_item = 0;
            else 
                estado_item = 1;

            return estado_item;        
        }

        public int validarCaracteristicasItemActualizacion(string idAcc)
        {
            int estado_item = 0;
            bool parametroListo = true;
            bool reqPlano = false;
            bool planoListo = true;

            List<string> parametros = new List<string>();
            List<int> idParametros = new List<int>();

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                int id = Convert.ToInt32(GridView1.DataKeys[i].Value);
                string parametro = ((System.Web.UI.WebControls.TextBox)GridView1.Rows[i].FindControl("textParametro")).Text;
                if (String.IsNullOrEmpty(parametro))
                {
                    parametroListo = false;
                    parametro = "";
                }
            }

            reader = contpv.requierePlanoItem(Convert.ToInt32(cboPlanta.SelectedValue), Convert.ToInt32(txtCodigo.Text));
            if (reader.HasRows)
            {
                reader.Read();
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    if (Convert.ToInt32(reader.GetValue(0)) == 1)
                    {
                        reqPlano = true;
                        string ruta = "";
                        DataTable dtp = new DataTable();
                        dtp = contpv.consultarRutaPlano(Convert.ToInt32(idAcc));

                        if (dtp.Rows.Count > 0)
                        {
                            ruta = dtp.Rows[0]["planos"].ToString();
                            if (String.IsNullOrEmpty(ruta))
                            {
                                planoListo = false;
                            }
                            else 
                            {
                                if (!(Directory.Exists(ruta)))
                                {
                                    planoListo = false;
                                }
                                else
                                {
                                    string[] smFiles = Directory.GetFiles(@ruta);
                                    if (smFiles.Length == 0)
                                    {
                                        planoListo = false;
                                    }
                                }
                            }
                        }
                    }

                    else reqPlano = false;
                }
                else reqPlano = false;
            }

            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            if (!parametroListo || !planoListo || (contpv.poblarTipoItem(item_planta_id).HasRows && cboTipo.SelectedItem.Text == "Seleccione el tipo") || (contpv.poblarModeloItem(item_planta_id).HasRows && cboModelo.SelectedItem.Text == "Seleccione el modelo"))
                estado_item = 0;
            else
                estado_item = 1;

            return estado_item;
        }


        private string validacionAccesorios(int fup) 
        {
            int itemAux=0, item=0;
            string mensaje = "";
            SqlDataReader readerValidacion = contpv.consultarAccesoriosPorConfirmar(fup);
            while (readerValidacion.Read())             
            {
                int erp = Convert.ToInt32(readerValidacion.GetValue(3));
                int idItemAcc = Convert.ToInt32(readerValidacion.GetValue(4));
                int reqtipo = Convert.ToInt32(readerValidacion.GetValue(1));
                int reqModelo = Convert.ToInt32(readerValidacion.GetValue(2));
                if (Convert.ToInt32(readerValidacion.GetValue(0)) == 0) 
                {
                    item = Convert.ToInt32(readerValidacion.GetValue(5));
                    int a = grdDetalle.PageIndex;
                    for (int b = 0; b < grdDetalle.PageCount; b++)
                    {
                        grdDetalle.SetPageIndex(b);
                        foreach (GridViewRow row in grdDetalle.Rows)
                        {
                            System.Web.UI.WebControls.Label valor = (System.Web.UI.WebControls.Label)grdDetalle.Rows[row.RowIndex].FindControl("cot_item");
                            int vlr = Convert.ToInt32(valor.Text);
                            if (vlr == item)
                            {
                                System.Web.UI.WebControls.Label consec = (System.Web.UI.WebControls.Label)grdDetalle.Rows[row.RowIndex].FindControl("consecutivo");
                                itemAux = Convert.ToInt32(consec.Text);
                                break;
                            }
                        }
                    }
                    grdDetalle.SetPageIndex(a);

                    SqlDataReader readerCaracteristicas = null;
                    readerCaracteristicas = contpv.consultarRequiereTipoModelo(erp, Convert.ToInt32(cboPlanta.SelectedValue));
                    if (readerCaracteristicas.HasRows) 
                    {
                        readerCaracteristicas.Read();
                        if (Convert.ToInt32(readerCaracteristicas.GetValue(0)) == 1 && Convert.ToInt32(readerCaracteristicas.GetValue(3)) == erp && reqtipo == 0)
                        {
                            mensaje += "El item " + itemAux + " le falta definir el: " + " Tipo \\n\\n";
                        }

                        if (Convert.ToInt32(readerCaracteristicas.GetValue(1)) == 1 && Convert.ToInt32(readerCaracteristicas.GetValue(3)) == erp && reqModelo == 0)
                        {
                            mensaje += "El item " + itemAux + " le falta definir el: " + " Modelo \\n\\n";
                        }

                        if (Convert.ToInt32(readerCaracteristicas.GetValue(4)) == 1 && (Convert.ToInt32(readerCaracteristicas.GetValue(3)) == erp))
                        {
                            string directorio = "";
                            string ruta = "";
                            DataTable dtp = new DataTable();
                            dtp = contpv.consultarRutaPlano(Convert.ToInt32(idItemAcc));

                            if (dtp.Rows.Count > 0)
                            {
                                ruta = dtp.Rows[0]["planos"].ToString();
                                if (!String.IsNullOrEmpty(ruta))
                                {
                                    directorio = @ruta;
                                }
                                else { directorio = @"I:\PV_Planos\" + fup + @"\" + idItemAcc + @"\" + erp + @"\"; }
                            }
                            else { directorio = @"I:\PV_Planos\" + fup + @"\" + idItemAcc + @"\" + erp + @"\"; }
                
                            //string directorio = @"I:\PV_Planos\" + fup + @"\" + itemAux + '-' + erp + @"\";

                            //string directorio = @"I:\PV_Planos\" + fup + @"\" + idItem + @"\";
                            //string dirweb = @"~/PV/" + txtFUP.Text + @"/";

                            if (!(Directory.Exists(directorio)))
                            {
                                mensaje += "El item " + itemAux + " le falta cargar los planos\\n\\n";
                            }
                            else
                            {
                                string[] smFiles = Directory.GetFiles(@directorio);
                                if (smFiles.Length == 0)
                                {
                                    mensaje += "El item " + itemAux + " le falta cargar los planos\\n\\n";
                                }
                            }
                        }

                        readerCaracteristicas.Close();
                        readerCaracteristicas.Dispose();
                    }                   
                }                
            }
            readerValidacion.Close();
            readerValidacion.Dispose();
            readerValidacion = contpv.consultarParametrosPorConfirmar(fup);
            while (readerValidacion.Read())
            {
                if (Convert.ToInt32(readerValidacion.GetValue(0)) == 0)
                {
                    item = Convert.ToInt32(readerValidacion.GetValue(5));

                    if (readerValidacion.GetValue(1).ToString() == "")
                    {
                        string descripcion = readerValidacion.GetValue(2).ToString();
                         int a = grdDetalle.PageIndex;
                         for (int b = 0; b < grdDetalle.PageCount; b++)
                         {
                             grdDetalle.SetPageIndex(b);
                             foreach (GridViewRow row in grdDetalle.Rows)
                             {
                                 System.Web.UI.WebControls.Label valor = (System.Web.UI.WebControls.Label)grdDetalle.Rows[row.RowIndex].FindControl("cot_item");
                                 int vlr = Convert.ToInt32(valor.Text);
                                 if (vlr == item)
                                 {
                                     System.Web.UI.WebControls.Label consec = (System.Web.UI.WebControls.Label)grdDetalle.Rows[row.RowIndex].FindControl("consecutivo");
                                     itemAux = Convert.ToInt32(consec.Text);
                                     break;
                                 }
                             }
                         }
                         grdDetalle.SetPageIndex(a);
                        mensaje += "El item " + itemAux + " le falta definir el parámetro: " + descripcion + "\\n\\n";
                    }
                }
            }
            readerValidacion.Close();
            readerValidacion.Dispose();
            contpv.CerrarConexion();
            return mensaje;
        }

        private void confirmarPV(int tipoPedido, int sf, int fup, string sf_venta, string sf_total, out string mensaje) 
        {
            string Anho = DateTime.Now.ToString("yy");
            string Tipo = "PV", Solic = "PEDIDO DE VENTA", Ver = "A";
            string Nombre = (string)Session["Nombre_Usuario"];
            mensaje = "";

            //Validamos de que en caso de que se ejecute varias veces el proceso, si ya hay un registro no cree mas PVs
            //Aqui uso la misma consulta que se hace despues del Insert
            reader = contpv.ConsultarNumeroPedidoVentaConFUP(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows == true)
            {
                reader.Read();
                string PV = reader.GetValue(0).ToString();
                mensaje = "El FUP: " + fup + " Ya cuenta con un PV confirmado: " + PV + " Porfavor consulte nuevamente.";
            }
            else //Si el reader no trae nada Inserte el PV
            {
                int insertar = contpv.IngPV(Anho, Tipo, fup, Solic, Ver, tipoPedido, sf, Convert.ToInt32(cboPlanta.SelectedValue));
            }

            //Cierre el reader y libere los recursos
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
            
            //Consulte el PV recien ingresado
            reader = contpv.ConsultarNumeroPedidoVentaConFUP(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows == true)
            {
                reader.Read();
                txtPV.Text = reader.GetValue(0).ToString();
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            //Inserto en logPV la confirmacion del pv
            string Estado = "Confirma Comercial";
            controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Nombre, "", 0, 0 , "");
            // activando confirmacion venta
            btnConfVenta.Enabled = true;            

            int moneda = 0;
            reader = contpv.consultarMonedaPais(Convert.ToInt32(cboPais.SelectedItem.Value));
            if (reader.HasRows)
            {
                reader.Read();                
                if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                {
                    Session["MonedaID"] = reader.GetValue(0).ToString();
                    moneda = Convert.ToInt32(Session["MonedaID"]);
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            int Orden = 0;

            string Pais = (string)Session["Pais"];

            reader = controlsf.consultarOrden(fup, "A", 1);
            if (reader.HasRows == true)
            {
                reader.Read();
                Orden = Convert.ToInt32(reader.GetValue(1).ToString());
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();

            int insertado = controlsf.IngresarDatosCierre(fup, moneda, sf_venta, sf_total, Convert.ToInt32(Pais), Orden, 0, Nombre, 1);

            mensaje = "Confirmación Exitosa PV No. " + txtPV.Text;
        }

        public static Boolean IsDatet(string fecha)
        {
            DateTime result;
            return DateTime.TryParse(fecha, out result);
        }

        protected void btnGuardarFechaDespacho_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Nombre_Usuario"];
            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";
            string mensaje = "";

            int rol = (int)Session["Rol"];

            if (!String.IsNullOrEmpty(txtFUP.Text)) 
            {
                if (rol == 36 || rol ==  20)
                {
                    if (txtFechaDes.Text != "")
                    {
                        reader = contpv.actualizarFechaSeguimientoPV(time.ToString(format), txtFechaDes.Text, Convert.ToInt32(txtFUP.Text), usuario);
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();
                        mensaje = "Fecha de despacho actualizada!";

                        string msjMail = "";
                        string correoUsuario = (string)Session["rcEmail"];
                        string user = Session["Usuario"].ToString().ToUpper();
                        string remitente = Session["CorreoSistema"].ToString();

                         string version = "A";
                        Session["Evento"] = 28;
                        Session["Parte"] = 1;
                        // envia correo
                        fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));
                        
                        //contpv.enviarCorreo(8, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                        //if (!String.IsNullOrEmpty(msjMail))
                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                        //Inserto log_pv
                        string Estado = "Fecha Despacho";
                        string Usuario = (string)Session["Nombre_Usuario"];
                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Usuario, "", 0, 0, "");

                        mensaje += msjMail;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        configurarControles();                        
                    }
                    else
                    {
                        mensaje = "Debe ingresar una fecha de despacho para ser actualizada";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }                    
                }
                else
                {
                    mensaje = "No posee permisos para realizar esta acción";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                } 
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }            
        }

        protected void chkConfAsistente_CheckedChanged(object sender, EventArgs e)
        {
            int conf_asistente = 0, rechaza_asistente = 0;
            string usuario = (string)Session["Nombre_Usuario"];

            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";    
            
            string mensaje = "";
            int rol = (int)Session["Rol"];

            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                if (rol == 9 || rol == 4 || rol == 2 || rol == 25)
                {
                    //bool confirmarVenta = true;
                    //reader = contpv.consultarEstadoItem(Convert.ToInt32(txtFUP.Text));
                    //while (reader.Read())
                    //{
                    //    if (Convert.ToInt32(reader.GetValue(0)) == 0)
                    //    {
                    //        confirmarVenta = false;
                    //        break;
                    //    }
                    //}

                    //if (!confirmarVenta)
                    //{
                        mensaje = validacionAccesorios(Convert.ToInt32(txtFUP.Text));
                        if (!String.IsNullOrEmpty(mensaje)) 
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(\"" + mensaje + "\");", true);
                            chkConfAsistente.Checked = false;                        
                        }

                    //}
                    else
                    {
                        if (chkConfAsistente.Checked)
                        {
                            conf_asistente = 1;
                            rechaza_asistente = 0;
                            contpv.actualizarSeguimientoPVAsistente(Convert.ToInt32(txtFUP.Text), conf_asistente, time.ToString(format), usuario, rechaza_asistente, 6);

                            //Inserto log_pv
                            string Estado = "Confirma Asistente";
                            string Usuario = (string)Session["Nombre_Usuario"];
                            controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Usuario, "", 0, 0, "");
                            configurarControles();
                            Reload_tbDetalle();
                            //this.CargarReporteEstado();
                            chkConfAsistente.Enabled = false;
                            chkConfAsistente.Checked = true;
                            ChkConfComercial.Enabled = false;

                            string msjMail = "";
                            string correoUsuario = (string)Session["rcEmail"];
                            string user = Session["Usuario"].ToString().ToUpper();
                            string remitente = Session["CorreoSistema"].ToString();

                            string version = "A";
                            Session["Evento"] = 24;
                            int idSF = 0;
                            reader = contpv.consultarSfId(Convert.ToInt32(txtFUP.Text));
                            if (reader.HasRows)
                            {
                                reader.Read();
                                idSF = Convert.ToInt32(reader.GetValue(0));
                            }
                            Session["SfId"] = idSF;
                            Session["Parte"] = 1;
                            // envia correo
                            fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                            //contpv.enviarCorreo(5, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                            //if (!String.IsNullOrEmpty(msjMail))
                            //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                            mensaje = "Confirmación realizada con éxito!" + msjMail;
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                            textRechazo.Visible = false;
                            btnRechazarComercialPV.Visible = false;
                            btnRechzarAsistentePV.Visible = false;
                                    
                        }
                        else
                        {
                            textRechazo.Visible = true;
                            btnRechzarAsistentePV.Visible = true;
                            btnRechazarComercialPV.Visible = false;
                        }
                    }
                }
                else
                {
                    mensaje = "No posee permisos para realizar esta acción";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    textRechazo.Visible = false;
                    btnRechazarComercialPV.Visible = false;
                    btnRechzarAsistentePV.Visible = false;
                }                
            }
            else 
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                textRechazo.Visible = false;
                btnRechazarComercialPV.Visible = false;
                btnRechzarAsistentePV.Visible = false;
            }
        }
       
        protected void ChkConfComercial_CheckedChanged(object sender, EventArgs e)
        {
            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                if (ChkConfComercial.Checked)
                {
                    ChkConfComercial.Checked = true;
                    textRechazo.Visible = false;
                    btnRechazarComercialPV.Visible = false;
                    btnRechzarAsistentePV.Visible = false;
                }
                else 
                {
                    textRechazo.Visible = true;
                    btnRechazarComercialPV.Visible = true;
                    btnRechzarAsistentePV.Visible = false;
                }
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

        private void habilitarFechaDespacho()
        {
            int rol = (int)Session["Rol"];

            if (rol == 36 || rol == 20)
            {
                txtFechaDes.Enabled = true;
                btnGuardarFechaDespacho.Enabled = true;
                btnGuardarFechaDespacho.Visible = true;
            }
            else
                btnGuardarFechaDespacho.Visible = false;


            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                reader = contpv.seleccionarFechaPlaneadaDespacho(Convert.ToInt32(txtFUP.Text));
                if (reader.HasRows)
                {
                    reader.Read();
                    
                    //lblOF.Text = reader.GetValue(0).ToString();
                    //1900 - 01 - 01 00:00:00.000   antes: "01/01/1900 12:00:00 a.m."         

                    if (String.IsNullOrEmpty(reader.GetValue(0).ToString()) ||
                        (reader.GetValue(0).ToString() == "01/01/1900 12:00:00 a.m.") ||
                        (reader.GetValue(0).ToString() == "1900-01-01 00:00:00.000"))
                    {
                        txtFechaDes.Text = "";
                    }
                    else
                    {  
                        txtFechaDes.Text = reader.GetValue(0).ToString();
                    }        
                   
                }
                else
                {
                    txtFechaDes.Text = "";
                }

                reader.Close();
                reader.Dispose(); 
                contpv.CerrarConexion();
            }
            else
            {
                txtFechaDes.Visible = false;
                txtFechaDes.Enabled = false;
                btnGuardarFechaDespacho.Visible = false;
                btnGuardarFechaDespacho.Enabled = false;
            }
        }

        private void configurarControles() 
        {
            if (!String.IsNullOrEmpty(txtFUP.Text)) 
            {
                Session["ConfirmaAsistente"] = "0";
                Session["ConfirmaIngenieria"] = "0";
                Session["ConfirmaComercial"] = "0";

                habilitarAlmaceLogistica();
                habilitarFechaDespacho();
                HabilitarSolicitudFacturacion();

                SqlDataReader readerEstado = contpv.consultarEstadoPV(Convert.ToInt32(txtFUP.Text));
                if (readerEstado.HasRows)
                {
                    readerEstado.Read();
                    Label6.Text = readerEstado.GetValue(1).ToString();                    
                }
                else
                    Label6.Text = "Cotizado";

                readerEstado.Close();
                readerEstado.Dispose(); 
                contpv.CerrarConexion();

                int rol = (int)Session["Rol"];
                if (rol == 1) btnCarga.Visible = true;
                reader = contpv.seleccionarConfirmaciones(Convert.ToInt32(txtFUP.Text));


                if (reader.HasRows)
                {
                    reader.Read();
                    int conf_comercial = Convert.ToInt32(reader.GetValue(0));
                    int conf_asistente = Convert.ToInt32(reader.GetValue(1));
                    int conf_ingenieria = Convert.ToInt32(reader.GetValue(2));
                    int rechazo_asistente = Convert.ToInt32(reader.GetValue(3));
                    int rechazo_ingenieria = Convert.ToInt32(reader.GetValue(4));

                    if (rechazo_asistente == 1 && rechazo_ingenieria == 1)
                    {
                        lblRechazado.Text = "Asistente/Ingenieria";
                        lblRechazado.Visible = true;
                        Label13.Visible = true;
                    }

                    else if (rechazo_asistente == 1)
                    {
                        lblRechazado.Text = "Asistente";
                        lblRechazado.Visible = true;
                        Label13.Visible = true;
                    }
                    else if (rechazo_ingenieria == 1)
                    {
                        lblRechazado.Text = "Ingenieria";
                        lblRechazado.Visible = true;
                        Label13.Visible = true;
                    }

                    else
                    {
                        lblRechazado.Visible = false;
                        Label13.Visible = false;
                    }

                    Panel5.Visible = true;
                    ChkConfComercial.Visible = true;
                    chkConfAsistente.Visible = true;
                    btnConfIngenieria.Visible = false;
                    chkConfAsistente.Enabled = false;
                    ChkConfComercial.Enabled = false;
                    btnConfIngenieria.Enabled = false;

                    if (conf_asistente == 1)
                    {
                        Session["ConfirmaAsistente"] = "1";
                        chkConfAsistente.Checked = true;
                    }
                    else
                        chkConfAsistente.Checked = false;

                    if (conf_ingenieria == 1)
                    {
                        Session["ConfirmaIngenieria"] = "1";
                    }


                    if (conf_comercial == 1)
                    {
                        Session["ConfirmaComercial"] = "1";
                        ChkConfComercial.Checked = true;
                        //Asistente
                        if ((rol == 9 || rol == 2) && conf_asistente == 1)
                        {
                            chkConfAsistente.Checked = true;
                            chkConfAsistente.Enabled = false;
                            ChkConfComercial.Enabled = false;
                            btnConfIngenieria.Enabled = false;

                        }
                        else if ((rol == 9 || rol == 2) && conf_asistente == 0)
                        {
                            chkConfAsistente.Checked = false;
                            chkConfAsistente.Enabled = true;
                            ChkConfComercial.Enabled = true;
                            btnConfIngenieria.Enabled = false;
                        }
                        //Ingenieria
                        else if ((rol == 4 || rol == 25 || rol == 24) && conf_asistente == 1 && conf_ingenieria == 1)
                        {
                            btnConfIngenieria.Enabled = false;
                            ChkConfComercial.Enabled = false;
                            chkConfAsistente.Enabled = false;

                        }
                        else if ((rol == 4 || rol == 25 || rol == 24) && conf_asistente == 1 && conf_ingenieria == 0)
                        {
                            btnConfIngenieria.Enabled = true;
                            ChkConfComercial.Enabled = false;
                            chkConfAsistente.Enabled = true;
                        }
                    }

                    else
                    {
                        ChkConfComercial.Checked = false;
                    }



                    if ((rol == 4 || rol == 25 || rol == 24) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 1)
                    {
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        Panel4.Enabled = false;
                        //Panel4.Visible = false;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        btnConfIngenieria.Visible = true;
                        chkConfAsistente.Checked = true;
                        ChkConfComercial.Checked = true;
                    }

                    //Si es Asistente,  Comercial y está confirmado por comercial y está confirmado por asistente y esta confirmado por ingenieria
                    else if ((rol == 9 || rol == 3 || rol == 30 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 1)
                    {
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        Panel4.Enabled = false;
                        //Panel4.Visible = false;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        btnConfIngenieria.Visible = false;
                        chkConfAsistente.Checked = true;
                        ChkConfComercial.Checked = true;
                    }

                    //Si es Asistente y está confirmado por comercial y no está confirmado por asistente y no esta confirmado por ingenieria
                    else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 0 && Convert.ToInt32(reader.GetValue(2)) == 0)
                    {
                        btnConfVenta.Enabled = true;
                        pnlBusAcc.Enabled = true;
                        Panel4.Enabled = true;
                        pnlCotAcc.Enabled = true;
                        chkConfAsistente.Enabled = true;
                        ChkConfComercial.Enabled = true;
                        btnConfIngenieria.Enabled = false;
                    }

                    //Si es Asistente y está confirmado por comercial y está confirmado por asistente y no esta confirmado por ingenieria
                    else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 0)
                    {
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        pnlCotAcc.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                    }

                    //Si es Asistente y no está confirmado por comercial y no está confirmado por asistente y no esta confirmado por ingenieria
                    else if ((rol == 9 || rol == 2) && Convert.ToInt32(reader.GetValue(0)) == 0 && Convert.ToInt32(reader.GetValue(1)) == 0 && Convert.ToInt32(reader.GetValue(2)) == 0)
                    {
                        btnConfVenta.Enabled = true;
                        pnlBusAcc.Enabled = true;
                        Panel4.Enabled = true;
                        pnlCotAcc.Enabled = true;
                        pnlCotAcc.Visible = true;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                    }

                    //Si es Ingenieria y está confirmado por Asistente y está confirmado por Comercial y NO está confirmado por Ingenieria
                    else if ((rol == 4 || rol == 25 || rol == 24) && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 0)
                    {
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                        Panel4.Enabled = false; 
                        pnlCotAcc.Visible = false;
                        chkConfAsistente.Enabled = true;
                        btnConfIngenieria.Enabled = true;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Visible = true;
                    }

                    //Si es Ingenieria y NO está confirmado por Asistente 
                    else if ((rol == 4 || rol == 25 || rol == 24) && Convert.ToInt32(reader.GetValue(1)) == 0)
                    {
                        pnlBusAcc.Enabled = false;
                        btnConfVenta.Enabled = false;
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        pnlCotAcc.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Visible = true;
                    }

                    //Si es ingeniería y está confirmado por comercial y está confirmado asistente y está confirmado por ingeniería
                    else if ((rol == 4 || rol == 25 || rol == 24) && Convert.ToInt32(reader.GetValue(0)) == 1 && Convert.ToInt32(reader.GetValue(1)) == 1 && Convert.ToInt32(reader.GetValue(2)) == 1)
                    {
                        pnlBusAcc.Enabled = false;
                        btnConfVenta.Enabled = false;
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        pnlCotAcc.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Visible = true;
                    }
                    // Si es Comercial y NO está confirmado por Comercial
                    else if ((rol == 3 || rol == 30 || rol == 33) && Convert.ToInt32(reader.GetValue(0)) == 0)
                    {
                        btnConfVenta.Enabled = true;
                        pnlBusAcc.Enabled = true;
                        Panel4.Enabled = true;
                        pnlCotAcc.Visible = true;
                        pnlCotAcc.Enabled = true;
                        ChkConfComercial.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                    }
                    // Si es Comercial y está confirmado por Comercial
                    else if ((rol == 3 || rol == 30) && Convert.ToInt32(reader.GetValue(0)) == 1)
                    {
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                    }
                    else
                    {
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        btnConfVenta.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                    }
                }

                else
                {
                    if (rol == 3 || rol == 30 || rol == 9 || rol == 2)
                    {
                        btnConfVenta.Enabled = true;
                        Panel4.Enabled = true;
                        pnlBusAcc.Enabled = true;
                        pnlCotAcc.Enabled = true;
                        pnlCotAcc.Visible = true;
                    }
                    else
                    {
                        btnConfVenta.Enabled = false;
                        pnlCotAcc.Visible = false;
                        Panel4.Enabled = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                    }
                }

                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();

                reader = contpv.consultarCirreVenta(Convert.ToInt32(txtFUP.Text));
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        btnConfVenta.Enabled = false;
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        //chkLogisticaAlmacen.Enabled = false;
                        //chkLogisticaAccesorios.Enabled = false;
                    }
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }            
        }

        protected void grdDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            int rol = (int)Session["Rol"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                reader = contpv.consultarCirreVenta(Convert.ToInt32(txtFUP.Text));
                if (reader.HasRows)
                {
                    reader.Read();
                    if (!String.IsNullOrEmpty(reader.GetValue(0).ToString()))
                    {
                        btnConfVenta.Enabled = false;
                        Panel4.Enabled = false;
                        pnlCotAcc.Visible = false;
                        pnlBusAcc.Enabled = false;
                        pnlCotAcc.Enabled = false;
                        chkConfAsistente.Enabled = false;
                        ChkConfComercial.Enabled = false;
                        btnConfIngenieria.Enabled = false;
                        //chkLogisticaAlmacen.Enabled = false;
                        //chkLogisticaAccesorios.Enabled = false;

                        try
                        {
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textObservacion")).Enabled = false;
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textCantidad")).Enabled = false;
                            ((LinkButton)e.Row.FindControl("LB2")).Visible = false;
                            ((LinkButton)e.Row.FindControl("LkEliminar1")).Visible = false;
                            txtCantidad.Enabled = false;
                            txtObservaciones.Enabled = false;
                            cboTipo.Enabled = false;
                            cboModelo.Enabled = false;
                            GridView1.Enabled = false;
                            btnGuardar.Enabled = false;
                        }
                        catch { }
                    }
                }

                else 
                {
                    if ((Session["ConfirmaAsistente"].ToString() == "1" && rol == 9) || (Session["ConfirmaComercial"].ToString() == "1" && rol == 3) || (rol == 4 || rol == 25))
                    {
                        try
                        {
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textObservacion")).Enabled = false;
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textCantidad")).Enabled = false;
                            ((LinkButton)e.Row.FindControl("LB2")).Visible = false;
                            ((LinkButton)e.Row.FindControl("LkEliminar1")).Visible = false;
                            txtCantidad.Enabled = false;
                            txtObservaciones.Enabled = false;
                            cboTipo.Enabled = false;
                            cboModelo.Enabled = false;
                            GridView1.Enabled = false;
                            btnGuardar.Enabled = false;
                        }
                        catch { }
                    }
                    else if ((Session["ConfirmaAsistente"].ToString() == "0" && rol == 9) || (Session["ConfirmaComercial"].ToString() == "0" && rol == 3) || (Session["ConfirmaComercial"].ToString() == "0" && rol == 30))
                    {
                        try
                        {
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textObservacion")).Enabled = true;
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textCantidad")).Enabled = true;
                            ((LinkButton)e.Row.FindControl("LB2")).Visible = true;
                            ((LinkButton)e.Row.FindControl("LkEliminar1")).Visible = true;
                            txtCantidad.Enabled = true;
                            txtObservaciones.Enabled = true;
                            cboTipo.Enabled = true;
                            cboModelo.Enabled = true;
                            GridView1.Enabled = true;
                            btnGuardar.Enabled = true;
                        }
                        catch { }
                    }
                    else
                    {
                        try
                        {
                            ((LinkButton)e.Row.FindControl("LB2")).Visible = false;
                            ((LinkButton)e.Row.FindControl("LkEliminar1")).Visible = false;
                            txtCantidad.Enabled = false;
                            txtObservaciones.Enabled = false;
                            cboTipo.Enabled = false;
                            cboModelo.Enabled = false;
                            GridView1.Enabled = false;
                            btnGuardar.Enabled = false;
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textObservacion")).Enabled = false;
                            ((System.Web.UI.WebControls.TextBox)e.Row.FindControl("textCantidad")).Enabled = false;
                        }
                        catch { }
                    }                
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
        }

        private void establecerTipoConetenido(int tipoPedido, int tipoContenido) 
        {

            SqlDataReader readerTipoItem = contpv.consultarTipoPV(tipoPedido);
            if (readerTipoItem.HasRows)
            {
                readerTipoItem.Read();
                lblTipoPedido.Text = readerTipoItem.GetValue(1).ToString();                
                lblTipoPedido.Visible = true;
                if(lblTipoPedido.Text == "ESPECIAL") lblTipoPedido.BackColor = Color.Yellow;
                Label10.Visible = true;
            }
            else
            {
                lblTipoPedido.Visible = false;
                Label10.Visible = false;
            }

            readerTipoItem.Close();
            readerTipoItem.Dispose();
            contpv.CerrarConexion();

            readerTipoItem = contpv.consultarContenidoPV(tipoContenido);
            if (readerTipoItem.HasRows)
            {
                readerTipoItem.Read();
                lblContenido.Text = readerTipoItem.GetValue(1).ToString();                
                lblContenido.Visible = true;
                Label12.Visible = true;
            }
            else
            {
                lblContenido.Visible = false;
                Label12.Visible = false;
            }
            readerTipoItem.Close();
            readerTipoItem.Dispose();
            contpv.CerrarConexion();
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            Reload_parametros();
        }

        private void Reload_parametros() 
        {
            DataTable dt = Session["parametros"] as DataTable;
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        protected void grdDetalle_PageIndexChanging1(object sender, GridViewPageEventArgs e)
        {
            grdDetalle.PageIndex = e.NewPageIndex;
            Reload_tbDetalle();
        }

        protected void btnRechazarComercialPV_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            int rechaza_asistente = 0;
            int conf_comercial = 0;
            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";
            string usuario = (string)Session["Nombre_Usuario"];

            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                mensaje = textRechazo.Text;
                if (!String.IsNullOrEmpty(mensaje))
                {
                    PanelEspecificaciones.Visible = false;
                    imgItem.Visible = false;
                    Reload_tbDetalle();
                    rechaza_asistente = 1;
                    conf_comercial = 0;
                    contpv.actualizarSeguimientoPVRechazoAsistente(Convert.ToInt32(txtFUP.Text), rechaza_asistente, time.ToString(format), usuario, mensaje, conf_comercial, 2);

                    //Inserto log_pv
                    string Estado = "Rechaza Asistente";
                    string Usuario = (string)Session["Nombre_Usuario"];
                    controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, Usuario, mensaje, 0, 0, "");
                    configurarControles();
                    Reload_tbDetalle();
                    ChkConfComercial.Checked = false;
                    ChkConfComercial.Enabled = false;
                    chkConfAsistente.Enabled = false;

                    string msjMail = "";
                    string correoUsuario = (string)Session["rcEmail"];
                    string user = Session["Usuario"].ToString().ToUpper();
                    string remitente = Session["CorreoSistema"].ToString();                    

                    string version = "A";
                    Session["Evento"] = 29;
                    Session["Parte"] = 1;
                    // envia correo
                    fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));
                    
                    //contpv.enviarCorreo(4, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                    //if (!String.IsNullOrEmpty(msjMail))
                    //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                    mensaje = "El rechazo fue registrado con éxito." + msjMail;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                    textRechazo.Visible = false;
                    btnRechazarComercialPV.Visible = false;
                    btnRechzarAsistentePV.Visible = false;
                }
                else
                {
                    mensaje = "El rechazo no fue registrado, por favor ingrese el motivo";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    ChkConfComercial.Checked = true;
                    textRechazo.Visible = false;
                    btnRechazarComercialPV.Visible = false;
                    btnRechzarAsistentePV.Visible = false;
                }
                configurarControles();
                Reload_tbDetalle();
                //this.CargarReporteEstado(); 
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                textRechazo.Visible = false;
                btnRechazarComercialPV.Visible = false;
                btnRechzarAsistentePV.Visible = false;
            }

        }

        protected void btnRechzarAsistentePV_Click(object sender, EventArgs e)
        {
            int conf_asistente = 0, rechaza_ingenieria = 0;
            string usuario = (string)Session["Nombre_Usuario"];
            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";
            string mensaje = "";
            
            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                mensaje = textRechazo.Text;
                if (!String.IsNullOrEmpty(mensaje))
                {
                conf_asistente = 0;
                rechaza_ingenieria = 1;
                contpv.actualizarSeguimientoPVRechazoIngenieria(Convert.ToInt32(txtFUP.Text), time.ToString(format), usuario, mensaje, conf_asistente, rechaza_ingenieria, 2);

                //Inserto log_pv
                string Estado = "Rechaza Ingeniería";
                controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, usuario, mensaje, 0, 0, "");
                configurarControles();
                Reload_tbDetalle();
                chkConfAsistente.Checked = false;
                chkConfAsistente.Enabled = false;
                ChkConfComercial.Enabled = false;

                string msjMail = "";
                string correoUsuario = (string)Session["rcEmail"];
                string user = Session["Usuario"].ToString().ToUpper();
                string remitente = Session["CorreoSistema"].ToString();
                
                string version = "A";
                Session["Evento"] = 30;
                Session["Parte"] = 1;
                // envia correo
                fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));
                
                //contpv.enviarCorreo(6, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                //if (!String.IsNullOrEmpty(msjMail))
                //    msjMail = "\\n El correo electrónico NO fue enviado \\n";

                mensaje = "El rechazo fue registrado con éxito" + msjMail;
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                textRechazo.Visible = false;
                btnRechazarComercialPV.Visible = false;
                btnRechzarAsistentePV.Visible = false;

            }
            else
            {
                mensaje = "El rechazo no fue registrado, por favor ingrese el motivo";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                chkConfAsistente.Checked = true;
                textRechazo.Visible = false;
                btnRechazarComercialPV.Visible = false;
                btnRechzarAsistentePV.Visible = false;

            }
                configurarControles();
                Reload_tbDetalle();
                //this.CargarReporteEstado(); 
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                textRechazo.Visible = false;
                btnRechazarComercialPV.Visible = false;
                btnRechzarAsistentePV.Visible = false;
            }
        }

        protected void btnConfIngenieria_Click(object sender, EventArgs e)
        {
            int conf_ingenieria = 0, rechaza_ingenieria = 0;

            DateTime time = DateTime.Now;
            string format = "yyyy-dd-MM HH:MM:ss";

            string usuario = (string)Session["Nombre_Usuario"];
            string mensaje = "";
            string msjMail = "";
            int rol = (int)Session["Rol"];

            if (!String.IsNullOrEmpty(txtFUP.Text))
            {
                if (rol == 4 || rol == 25)
                {
                    //bool confirmarVenta = true;
                    //reader = contpv.consultarEstadoItem(Convert.ToInt32(txtFUP.Text));
                    //while (reader.Read())
                    //{
                    //    if (Convert.ToInt32(reader.GetValue(0)) == 0)
                    //    {
                    //        confirmarVenta = false;
                    //        break;
                    //    }
                    //}

                    //if (!confirmarVenta)
                    //{
                    //    mensaje = validacionAccesorios(Convert.ToInt32(txtFUP.Text));
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(\"" + mensaje + "\");", true);
                    //    chkConfAsistente.Checked = false;
                    //}
                    mensaje = validacionAccesorios(Convert.ToInt32(txtFUP.Text));
                    if (!String.IsNullOrEmpty(mensaje))
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(\"" + mensaje + "\");", true);
                        chkConfAsistente.Checked = false;
                    }
                    else
                    {
                        PanelEspecificaciones.Visible = false;
                        imgItem.Visible = false;
                        conf_ingenieria = 1;
                        rechaza_ingenieria = 0;
                        reader = contpv.actualizarSeguimientoPVIngenieria(Convert.ToInt32(txtFUP.Text), conf_ingenieria, time.ToString(format), usuario, rechaza_ingenieria, 7);

                        //Inserto log_pv
                        string Estado = "Confirma Ingeniería";
                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), 0, 0, "", "", Estado, usuario, "", 0, 0, "");

                        this.cargaPV(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));

                        msjMail = "";
                        string correoUsuario = (string)Session["rcEmail"];
                        string user = Session["Usuario"].ToString().ToUpper();
                        string remitente = Session["CorreoSistema"].ToString();
                        int idSF = 0;
                        reader = contpv.consultarSfId(Convert.ToInt32(txtFUP.Text));
                        if (reader.HasRows)
                        {
                            reader.Read();
                            idSF = Convert.ToInt32(reader.GetValue(0));                            
                        }
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        configurarControles();
                        Reload_tbDetalle();
                        //this.CargarReporteEstado();
                        chkConfAsistente.Enabled = false;

                        string version = "A";
                            Session["Evento"] = 25;                           
                            Session["SfId"] = idSF;
                            Session["Parte"] = 1;
                            // envia correo
                            fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));
                        
                        //contpv.enviarCorreo(, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, idSF);

                        //if (!String.IsNullOrEmpty(msjMail))
                        //{
                        //    msjMail = "\\n El correo electrónico NO fue enviado \\n";
                        //}
                        mensaje = "Confirmación realizada con éxito!" + msjMail;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        textRechazo.Visible = false;
                        btnRechazarComercialPV.Visible = false;
                        btnRechzarAsistentePV.Visible = false;
                        btnConfIngenieria.Enabled = false;
                    }
                }
                else
                {
                    mensaje = "No posee permisos para realizar esta acción";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }          
        }

        protected void btnGuardar_Click(object sender, ImageClickEventArgs e)         
        {
            int fup = -1;

            string mensaje = "";
            string msjMail = "";
            string Nombre = (string)Session["Nombre_Usuario"];
            int Item = 0;
            string FechaCrea = DateTime.Now.ToString("dd/MM/yyyy");
            string Hora = DateTime.Now.ToShortTimeString();
            string Estado = "";
            int rol = (int)Session["Rol"];
            itemid = Convert.ToInt64(Session["Item"]);

            if (btnGuardar.ToolTip == "Guardar")
            {
                if (cboAccesorio.SelectedItem.Text == "Seleccione el accesorio" || txtCantidad.Text.Trim() == "" || txtCantidad.Text == "0" || LblPrecioUni.Text == "0" || txtPrecio.Text == "0" || LblPrecioUni.Text == "0.00" || txtPrecio.Text == "0.00" || txtPrecio.Text.Trim() == "")
                {
                    mensaje = "Debe seleccionar el accesorio, tener precio y digitar la cantidad";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }

                else
                {
                    if (txtCantidad.Text == "0")
                    {

                        mensaje = "Debe seleccionar la cantidad.";

                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    }
                    else
                    {
                        int moneda = 0;
                        moneda = Convert.ToInt32(Session["MonedaID"]);

                        if (txtFUP.Text == "")
                        {
                            //CREAMOS EL FUP DEL ACCESORIO
                            int scope = contpv.FUP(FechaCrea, Convert.ToInt32(cboCliente.SelectedItem.Value), moneda,
                                Convert.ToInt32(cboContacto.SelectedValue), Convert.ToInt32(cboObra.SelectedValue), "DT",
                                Nombre, Convert.ToInt32(cboPlanta.SelectedItem.Value));

                            txtFUP.Text = Convert.ToString(scope);

                            txtIdRecompra.Visible = true;
                            lblIdRecompra.Visible = true;

                            string OFA = (string)Session["IDOFA"];
                            //ACTUALIZAMOS EL ESTADO EN FUP
                            int actualizar = contpv.ActualizarEstadoFUPAccesorio(scope, txtOF.Text);

                            string usuario = (string)Session["Nombre_Usuario"];
                            string correoUsuario = (string)Session["rcEmail"];
                            string user = Session["Usuario"].ToString().ToUpper();
                            string remitente = Session["CorreoSistema"].ToString();

                            string version = "A";
                            Session["Evento"] = 31;
                            this.cargarSesiones();
                            // envia correo
                            //fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                            //contpv.enviarCorreo(1, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                            //if (!String.IsNullOrEmpty(msjMail))
                            //    msjMail = "\\n El correo electrónico NO fue enviado \\n";
                        }

                        //CONSULTA CONSECUTIVO ITEM ACCESORIO
                        reader = contpv.ConsultarItemAccesorio(Convert.ToInt32(txtFUP.Text));
                        if (!reader.HasRows)
                        {
                            Item = 1;
                        }
                        else
                        {
                            reader = contpv.ConsultarMaximoItemAccesorio(Convert.ToInt32(txtFUP.Text));
                            if (reader.HasRows == true)
                            {
                                reader.Read();
                                string maximo = reader.GetValue(0).ToString();
                                Item = Convert.ToInt32(maximo) + 1;
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();

                        int estado_item = 0;
                        estado_item = validarCaracteristicasItem();

                        //INGRESAMOS LOS DATOS A COTIZAR
                        int cotacc = contpv.CotAcc(Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text.Replace(",", "")),
                            LblPrecioUni.Text, lblPesoUni.Text, "0", Convert.ToInt32(txtFUP.Text), txtObservaciones.Text,
                            "0", Item, Nombre, FechaCrea, Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboModelo.SelectedValue), estado_item);

                        bool reqParametro = insertarParametros(cotacc);
                        if (reqParametro)
                        {
                            mensaje = "Para confirmar el pedido de venta se deben llenar los parámetros requeridos";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }

                        string parametros = "";
                        reader = contpv.cargarParametrosItem(cotacc);
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        //Inserto log_pv
                        string est = "Cotización Inicial";
                        string Usuario = (string)Session["Nombre_Usuario"];
                        double total = Convert.ToDouble(LblPrecioUni.Text.Replace(",", "")) * Convert.ToInt32(txtCantidad.Text.Replace(",", ""));
                        int tipo_id = 0, modelo_id = 0;
                        if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
                            tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
                        if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
                            modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text.Replace(",", "")), LblPrecioUni.Text.Replace(",", ""), total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);

                        mensaje = MensajeItemAccesorio() + msjMail;
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        Session["EstadoCom"] = "1";
                        this.CargarReporteDetalle();
                        this.LimpiarDetalle();
                    }
                }
            }
            else
            {
                if (btnGuardar.ToolTip == "Eliminar")
                {
                    string IdAccesorio = (string)Session["IdAcc"];
                    txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();
                    Estado = "Eliminado";

                    int estado_item = 0;
                    estado_item = validarCaracteristicasItem();

                    string parametros = "";
                    reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
                    if (reader.HasRows == true)
                    {
                        while (reader.Read())
                        {
                            parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
                        }
                    }
                    reader.Close();
                    reader.Dispose();
                    contpv.CerrarConexion();
                    //INGRESAMOS LOS DATOS A ELIMINAR
                    int cotacc = contpv.ActAcc(Convert.ToInt32(IdAccesorio), Convert.ToInt32(txtCantidad.Text.Replace(",", "")),
                          LblPrecioUni.Text, lblPesoUni.Text, "0", txtObservaciones.Text, "0",
                          Nombre, FechaCrea, Convert.ToInt32(txtFUP.Text), Hora, Estado, Convert.ToInt32(txtCodigo.Text), Convert.ToInt32(cboTipo.SelectedValue), Convert.ToInt32(cboModelo.SelectedValue), estado_item);

                    //Inserto log_pv
                    string est = "Eliminado";
                    string Usuario = (string)Session["Nombre_Usuario"];
                    double total = Convert.ToDouble(LblPrecioUni.Text.Replace(",", "")) * Convert.ToInt32(txtCantidad.Text.Replace(",", ""));
                    int tipo_id = 0, modelo_id = 0;
                    if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
                        tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
                    if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
                        modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

                    controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text.Replace(",", "")), LblPrecioUni.Text.Replace(",", ""), total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);

                    this.LimpiarDetalle();
                    MensajeItemAccesorio();

                    Session["EstadoCom"] = "1";
                    this.CargarReporteDetalle();

                    btnGuardar.ToolTip = "Guardar";
                    btnGuardar.BorderColor = System.Drawing.Color.Transparent;
                    btnGuardar.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                    btnGuardar.BorderWidth = 2;
                }

                else
                {
                    if (btnGuardar.ToolTip == "Actualizar")
                    {
                        if ((contpv.poblarTipoItem(item_planta_id).HasRows && cboTipo.SelectedItem.Text == "Seleccione el tipo") || (contpv.poblarModeloItem(item_planta_id).HasRows && cboModelo.SelectedItem.Text == "Seleccione el modelo"))
                        {
                            mensaje = "Para confirmar el pedido de venta seleccionar el tipo y modelo para este accesorio";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                        }

                        string IdAccesorio = Session["IdAcc"].ToString();
                        txtObservaciones.Text = txtObservaciones.Text.ToUpperInvariant();
                        Estado = "Actualización";
                        Session["Estado"] = Estado;
                        int tipo = 0, modelo = 0;
                        if (!String.IsNullOrEmpty(cboTipo.SelectedValue.ToString()) && cboTipo.SelectedValue.ToString() != "Seleccione el tipo")
                            tipo = Convert.ToInt32(cboTipo.SelectedValue);

                        if (!String.IsNullOrEmpty(cboModelo.SelectedValue.ToString()) && cboModelo.SelectedValue.ToString() != "Seleccione el modelo")
                            modelo = Convert.ToInt32(cboModelo.SelectedValue);

                        int estado_item = 0;
                        estado_item = validarCaracteristicasItemActualizacion(IdAccesorio);

                        //INGRESAMOS LOS DATOS A ACTUALIZAR
                        int cotacc = contpv.ActAcc(Convert.ToInt32(IdAccesorio), Convert.ToInt32(txtCantidad.Text.Replace(",", "")),
                            LblPrecioUni.Text, lblPesoUni.Text, "0", txtObservaciones.Text, "0",
                            Nombre, FechaCrea, Convert.ToInt32(txtFUP.Text), Hora, Estado, Convert.ToInt32(txtCodigo.Text), tipo, modelo, estado_item);

                        actualizarParametrosAccesorios(Convert.ToInt32(IdAccesorio));

                        string parametros = "";
                        reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        //Inserto log_pv
                        string est = "Actualización";
                        string Usuario = (string)Session["Nombre_Usuario"];
                        double total = Convert.ToDouble(LblPrecioUni.Text.Replace(",", "")) * Convert.ToInt32(txtCantidad.Text.Replace(",", ""));
                        int tipo_id = 0, modelo_id = 0;
                        if (!String.IsNullOrEmpty(cboTipo.SelectedValue))
                            tipo_id = Convert.ToInt32(cboTipo.SelectedValue);
                        if (!String.IsNullOrEmpty(cboModelo.SelectedValue))
                            modelo_id = Convert.ToInt32(cboModelo.SelectedValue);

                        controlsf.IngresarDatosLOGpv(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboAccesorio.SelectedValue), Convert.ToInt32(txtCantidad.Text.Replace(",", "")), LblPrecioUni.Text, total.ToString(), est, Usuario, txtObservaciones.Text, tipo_id, modelo_id, parametros);

                        reader = contpv.cargarParametrosItem(Convert.ToInt32(IdAccesorio));
                        if (reader.HasRows == true)
                        {
                            while (reader.Read())
                            {
                                parametros += reader.GetValue(2).ToString() + ": " + reader.GetValue(1).ToString() + " / ";
                            }
                        }
                        reader.Close();
                        reader.Dispose();
                        contpv.CerrarConexion();

                        mensaje = "Item Actualizado exitosamente";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);

                        txtFUP_TextChanged(sender, e);

                        MensajeItemAccesorio();

                        Session["EstadoCom"] = "1";
                        this.CargarReporteDetalle();
                        this.LimpiarDetalle();

                        btnGuardar.ToolTip = "Guardar";
                        btnGuardar.BorderColor = System.Drawing.Color.Transparent;
                        btnGuardar.BorderStyle = System.Web.UI.WebControls.BorderStyle.Solid;
                        btnGuardar.BorderWidth = 2;
                    }
                }

                if (cboPais.SelectedValue == "8")
                {
                    Session["MONEDA"] = "COP";
                }
                else
                {
                    Session["MONEDA"] = "USD";
                }
            }

            btnConfVenta.Enabled = true;
            btnSolicFatura.Enabled = true;
            cargarTipoPedido();
            verificarEstadoItems();
        }

        protected void cargarTipoPedido()
        {
             
            reader = contpv.ConsultarPvEspecial(Convert.ToInt32(txtFUP.Text));
            if (reader.HasRows)
            {
                reader.Read();
                lblTipoPedido.Visible = true;
                lblTipoPedido.Text = reader.GetValue(0).ToString();
                Label10.Visible = true;
                if (lblTipoPedido.Text == "ESPECIAL") lblTipoPedido.BackColor = Color.Yellow;

                int pendientes = Convert.ToInt32(reader.GetValue(5));

                if (pendientes > 0)
                {
                    lblmensajePedido.Visible = true;
                    lblmensajePedido.Text = reader.GetValue(4).ToString();
                    lblmensajePedido.BackColor = Color.Yellow;
                }
                else
                {
                    lblmensajePedido.Visible = false;
                }
            }
            reader.Close();
            reader.Dispose();
            contpv.CerrarConexion();
        }

        protected void cargarSesiones()
        {
            //SETEAMOS LAS VARIABLES DE SESION PARA SOLICITUD DE FACTURACIÓN
            Session["Pais"] = cboPais.SelectedValue;
            Session["PaisNombre"] = cboPais.SelectedItem.Text;
            Session["Ciudad"] = cboCiudad.SelectedValue;
            Session["Planta"] = cboPlanta.SelectedItem.Text;
            Session["FUP"] = txtFUP.Text;
            Session["Planta"] = cboPlanta.SelectedItem.Text;
            Session["TIPO"] = "PV";
            Session["DescTipo"] = "PEDIDO DE VENTA";
            Session["Bandera"] = "2";
            Session["VER"] = "A";
            Session["NUMERO"] = txtPV.Text;
            Session["CLIENTE"] = cboCliente.SelectedItem.Text;
            Session["OBRA"] = cboObra.SelectedItem.Text;
        }

        protected void btnSeguimiento_Click(object sender, EventArgs e)
        {
            Response.Redirect("SeguimientoDespachos.aspx?idofa="+ lblidofa.Text);
        }

        protected void btnCarga_Click(object sender, EventArgs e)
        {
                //this.confirmarPV(tipoPedido, sf, Convert.ToInt32(txtFUP.Text), sf_venta, sf_total, out mensaje);
                this.cargaPV(Convert.ToInt32(txtFUP.Text), Convert.ToInt32(cboPlanta.SelectedValue));
        }

        
        protected void txtIdRecompra_TextChanged(object sender, EventArgs e)
        {
            if (txtIdRecompra.Text == "0" || txtIdRecompra.Text == "")
            {
            }
            else
            {
                string mensaje = "";
                reader = contpv.ConsultarIdRecompra(txtIdRecompra.Text);
                if (reader.HasRows)
                {
                    reader.Read();
                    mensaje = "Id Recompra de Hoja Vida Proyecto encontrada exitosamente";
                    btnGuardarIdRecompra.Visible = true;
                    lblMensajeRecompra.Text = reader.GetValue(1).ToString();
                    lblMensajeRecompra.Visible = true;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                else
                {
                    mensaje = "No Existe Id Recompra digitado";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    lblMensajeRecompra.Text = "";
                }
                reader.Close();
                reader.Dispose();
                contpv.CerrarConexion();
            }
        }

        protected void btnGuardarIdRecompra_Click(object sender, EventArgs e)
        {
            string usuario = (string)Session["Nombre_Usuario"];

            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFUP.Text))
            {                
                if (lblMensajeRecompra.Text != "")
                {
                    reader = contpv.actualizarIdRecompra(Convert.ToInt32(txtFUP.Text),Convert.ToInt32(txtIdRecompra.Text), usuario);                    
                    contpv.CerrarConexion();
                    mensaje = "Id Recompra Guardada con Exito!";

                    string msjMail = "";
                    string correoUsuario = (string)Session["rcEmail"];
                    string user = Session["Usuario"].ToString().ToUpper();
                    string remitente = Session["CorreoSistema"].ToString();

                    string version = "A";
                    Session["Evento"] = 28;
                    Session["Parte"] = 1;
                    // envia correo
                    //fup_clase.CorreoFUP(Convert.ToInt32(txtFUP.Text), version, Convert.ToInt32(Session["Evento"]));

                    //contpv.enviarCorreo(8, Convert.ToInt32(txtFUP.Text), user, remitente, out msjMail, usuario, correoUsuario, cboPais.SelectedItem.Text, 0);
                    //if (!String.IsNullOrEmpty(msjMail))
                    //    msjMail = "\\n El correo electrónico NO fue enviado \\n";                  

                    mensaje += msjMail;
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                    
                }
                else
                {
                    mensaje = "Debe ingresar una fecha de despacho para ser actualizada";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                }
                 
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }

       

        protected void btnCargarPrecio_Click(object sender, EventArgs e)
        {
            string mensaje = "";
            if (!String.IsNullOrEmpty(txtFUP.Text) && cboAccesorio.SelectedValue != "0")
            {
                 
                btnGuardar.ToolTip = "Actualizar";
                btnGuardar.Enabled = true;
                tipoClienteid = Convert.ToInt32(Session["TipoClienteID"]);
                item_planta_id = Convert.ToInt64(Session["Item_Planta"]);
                calcularPrecioItem(item_planta_id, Convert.ToInt32(cboCliente.SelectedValue), Convert.ToInt32(cboPlanta.SelectedValue), tipoClienteid);
                double precio = Convert.ToInt32(txtCantidad.Text.Replace(",", "")) * Convert.ToDouble(LblPrecioUni.Text);
                txtPrecio.Text = precio.ToString("N", new CultureInfo("en-US"));
                mensaje = "Precio actualizado correctamente!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
            else
            {
                mensaje = "Verifique que el campo FUP no esté vacío y sea válido o que el ítem este seleccionado";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
            }
        }
    }    
}