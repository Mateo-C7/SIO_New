using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net;
using Microsoft.Reporting.WebForms;
using CapaControl;

namespace SIO
{
    public partial class Contacto : System.Web.UI.Page
    {
        public SqlDataReader reader = null;
        private DataSet dsContacto = new DataSet();
        public ControlContacto controlCont = new ControlContacto();
        ControlUbicacion controlUbi = new ControlUbicacion();
        public ControlCliente concli = new ControlCliente();
        public SqlDataReader readerCargo = null;
        public SqlDataReader readerFeria = null;
        public SqlDataReader readerPais = null;
        public SqlDataReader readerCiudad = null;
        public SqlDataReader readerCliente = null;
        public SqlDataReader readerObra = null;
        public SqlDataReader readerContacto = null;
        public SqlDataReader readerProfesion = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Rol"] != null)
            {

                int arRol = (int)Session["Rol"];

            string pais = (string)Session["pais"];
            string usuario = (string)Session["Usuario"];
            string rcNombre = (string)Session["Nombre_Usuario"];
            string rcEmail = (string)Session["rcEmail"];
            string area = (string)Session["Area"];
            string idioma = (string)Session["Idioma"];
            string idContCliente;
            string rcID = (string)Session["rcID"];
           

            if (!IsPostBack)
            {
                    // verifico si el usuario puede eliminar                    
                    int idClienteUsuario = Convert.ToInt32(Session["IdClienteUsuario"]);
                    if (idClienteUsuario > 0) btnEliminarCont.Visible = false;

                    //this.Idioma();
                    //VERIFICO SI VIENE PARA ACTUALIZAR
                    if (Request.QueryString["idContCliente"] != null)
                {
                    idContCliente = Request.QueryString["idContCliente"];
                    this.cargarContCliente(idContCliente);
                    btnGuardarCont.Text = "Actualizar";
                    btnEliminarCont.Visible = true;
                    //this.cargarReporteContactoXId();                 
                }
                else
                {
                    //this.cargarReporteContacto();
                    this.cargarCombosIdiomas();
                    this.CargarCargo();
                    this.CargarFeria();
                    this.cargarTipoContacto();
                    this.cargarPaisesRol();
                    this.CargarProfesion();
                }

                if (Session["ClientePlaneacion"] != null)
                {
                    Session["Cliente"] = Session["ClientePlaneacion"];
                    this.tipoEmpresa(Convert.ToInt32(Session["Cliente"].ToString()));
                    this.cargarDatosCliente();
                        ImageButton1.Visible = true;
                    }
                else
                {
                    ImageButton1.Visible = false;
                }

                if (Request.QueryString["idCliente"] != null)
                {
                    Session["ClientePlaneacion"] = null;
                    string cliente = Request.QueryString["idCliente"];
                    Session["Cliente"] = cliente;
                    this.tipoEmpresa(Convert.ToInt32(cliente));
                    this.cargarDatosCliente();
                        //this.cargarReporteContactoXId();
                        ImageButton1.Visible = true;
                    }
                else
                {
                    ImageButton1.Visible = false;
                }
                
                Session["contador"] = 0;
            }
            }
            else
            {
                string mensaje = "Sesion Expiro. Por favor inicie sesión nuevamente. Gracias!";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);
                Response.Redirect("Inicio.aspx");
            }
        }

        private void tipoEmpresa(int idcliente)
        {
            reader = controlCont.consultarEmpresa(Convert.ToInt32(idcliente));
            if (reader.HasRows == true)
            {
                reader.Read();
                if (reader.GetBoolean(0) == true)
                {
                    cboObra.Visible = false;
                    lblObra.Visible = false;
                    lblContactoTitulo.Text = "CONTACTO GRUPO APOYO-";
                    cboFeria.Visible = false;
                    lblFeria.Visible = false;
                }
            }
            reader.Close();
            controlCont.cerrarConexion();
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
            }

            this.Prefijo();
            reader.Close();
            controlCont.cerrarConexion();
            lblClienteP.Text = cboClienteMatriz.SelectedItem.Text;
            lblIdContacto.Text = cliente;
        }

        //CARGAMOS LOS PAISES DEACUERDO AL ROL DEL USUARIO
        private void cargarPaisesRol()
        {
            int arRol = (int)Session["Rol"];
            if ((arRol == 3) || (arRol == 28) || (arRol == 29))
                {
                    this.poblarListaPais();
                }
                else
                {
                    this.poblarListaPais2();
                }
        }
        
        ////CARGAMOS EL INFORME DE LOS CONTACTOS CREADOS
        //public void cargarReporteContacto()
        //{
        //    string rcID = (string)Session["rcID"];
        //    int arRol = (int)Session["Rol"];
        //    string pais = (string)Session["pais"];
        //    List<ReportParameter> parametro = new List<ReportParameter>();

        //    parametro.Add(new ReportParameter("idrepresentante", rcID, true));
        //    parametro.Add(new ReportParameter("rol", arRol.ToString(), true));
        //    parametro.Add(new ReportParameter("pais", pais, true));

        //    reportContactos.Width = 1280;
        //    //reportContactos.Height = 1050;
        //    reportContactos.ProcessingMode = ProcessingMode.Remote;
        //    IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
        //    reportContactos.ServerReport.ReportServerCredentials = irsc;

        //    reportContactos.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
        //    reportContactos.ServerReport.ReportPath = "/InformesFUP/COM_ContactosXRol";
        //    this.reportContactos.ServerReport.SetParameters(parametro);
        //}

        //public void cargarReporteContactoXId()
        //{
        //    string rcID = (string)Session["rcID"];
        //    int arRol = (int)Session["Rol"];
        //    string pais = (string)Session["pais"];
        //    List<ReportParameter> parametro = new List<ReportParameter>();

        //    parametro.Add(new ReportParameter("idPais", CboPaisMatriz.SelectedItem.Value, true));
        //    parametro.Add(new ReportParameter("idCiudad", cboCiudadMatriz.SelectedItem.Value, true));
        //    parametro.Add(new ReportParameter("idCliente",cboClienteMatriz.SelectedItem.Value, true));

        //    reportContactos.Width = 1280;
        //    //reportContactos.Height = 1050;
        //    reportContactos.ProcessingMode = ProcessingMode.Remote;
        //    IReportServerCredentials irsc = new CustomReportCredentials("forsa", "F0rUs3s4*", "FORSA");
        //    reportContactos.ServerReport.ReportServerCredentials = irsc;

        //    reportContactos.ServerReport.ReportServerUrl = new Uri("http://10.75.131.2:81/ReportServer");
        //    reportContactos.ServerReport.ReportPath = "/InformesFUP/COM_ContactosXId";
        //    this.reportContactos.ServerReport.SetParameters(parametro);
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

        //CARGAMOS LAS ETIQUETAS CON EL IDIOMA SELECCIONADO
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

            dsContacto = controlCont.ConsultarIdiomaContacto();

            foreach (DataRow fila in dsContacto.Tables[0].Rows)
            {
                posicion = posicion + 1;
                if (posicion == 1)
                    lblTipoContacto.Text = fila[idiomaId].ToString();
                if (posicion == 2)
                    lblNombre1.Text = fila[idiomaId].ToString();               
                if (posicion == 4)
                    lblApellido1.Text = fila[idiomaId].ToString();               
                if (posicion == 6)
                    lblCargo.Text = fila[idiomaId].ToString();
                if (posicion == 7)
                    lblTelefono1.Text = fila[idiomaId].ToString();
                if (posicion == 8)
                    lblTelefono2.Text = fila[idiomaId].ToString();
                if (posicion == 9)
                    lblTelefono3.Text = fila[idiomaId].ToString();
                if (posicion == 10)
                    lblCorreo1.Text = fila[idiomaId].ToString();
                if (posicion == 11)
                    lblProfesion.Text = fila[idiomaId].ToString();
                if (posicion == 12)
                    lblFeria.Text = fila[idiomaId].ToString();
                if (posicion == 13)
                    lblPaiCliMat.Text = fila[idiomaId].ToString();
                if (posicion == 14)
                    lblCiuCliMat.Text = fila[idiomaId].ToString();
                if (posicion == 15)
                    lblClienteMat.Text = fila[idiomaId].ToString();
                if (posicion == 16)
                    lblObra.Text = fila[idiomaId].ToString();
                if (posicion == 17)
                    lblCumple.Text = fila[idiomaId].ToString();
                if (posicion == 18)
                    lblHobby.Text = fila[idiomaId].ToString();
                if (posicion == 19)
                    lblComoContacto.Text = fila[idiomaId].ToString();
                if (posicion == 20)
                    chkTelefono.Text = fila[idiomaId].ToString();
                if (posicion == 21)
                    chkTrabCampo.Text = fila[idiomaId].ToString();
                if (posicion == 22)
                    chkPersonal.Text = fila[idiomaId].ToString();
                if (posicion == 23)
                    chkEmail.Text = fila[idiomaId].ToString();
                if (posicion == 24)
                    chkVisita.Text = fila[idiomaId].ToString();
                if (posicion == 25)
                    chkPublicImpresa.Text = fila[idiomaId].ToString();
                if (posicion == 26)
                    chkSeminarios.Text = fila[idiomaId].ToString();
                if (posicion == 27)
                    chkConferencias.Text = fila[idiomaId].ToString();
                if (posicion == 28)
                    chkCharlas.Text = fila[idiomaId].ToString();
                if (posicion == 29)
                    chkWeb.Text = fila[idiomaId].ToString();
                if (posicion == 30)
                    chkMedComunicacion.Text = fila[idiomaId].ToString();
                if (posicion == 31)
                    chkReferencia.Text = fila[idiomaId].ToString();
                if (posicion == 32)
                    lblComentario.Text = fila[idiomaId].ToString();                             
               
                if (posicion == 37)
                    btnGuardarCont.Text = fila[idiomaId].ToString();
                if (posicion == 38)
                    bntNuevo.Text = fila[idiomaId].ToString();
                
            }
            dsContacto.Tables.Remove("Table");
            dsContacto.Dispose();
            dsContacto.Clear();
        }

        //CARGAMOS EL COMBO DE TIPO DE CONTACTO
        private void cargarTipoContacto()
        {
            string idioma = (string)Session["Idioma"];

            cboTipoContacto.Items.Clear();
            reader = controlCont.ConsultarTipoContacto();

            if (reader.HasRows == true)
            {
                if (idioma == "Español")
                {
                    cboTipoContacto.Items.Add("Seleccione El Tipo De Contacto");
                    while (reader.Read())
                    {
                        cboTipoContacto.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                    }
                }
                if (idioma == "Ingles")
                {
                    cboTipoContacto.Items.Add("Select Type Contact");
                    while (reader.Read())
                    {

                        cboTipoContacto.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                    }
                }
                if (idioma == "Portugues")
                {
                    cboTipoContacto.Items.Add("Selecione O Tipo De Contato");
                    while (reader.Read())
                    {

                        cboTipoContacto.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                    }
                }
            }
            reader.Close();
            controlCont.cerrarConexion();
        }

        //CARGAMOS EL COMBO DE CARGOS
        private void CargarCargo()
        {
            //cboCargo.Items.Clear();
            readerCargo = controlCont.obtenerCargo();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboCargo.Items.Add(new ListItem("Seleccione el Cargo", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCargo.Items.Add(new ListItem("Select the Charge", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCargo.Items.Add(new ListItem("Selecione a Carga", "0"));
            }
            while (readerCargo.Read())
            {
                cboCargo.Items.Add(new ListItem(readerCargo.GetString(1), readerCargo.GetInt32(0).ToString()));
            }
            readerCargo.Close();
            controlCont.cerrarConexion();
        }

        //CARGAMOS EL COMBO DE CARGOS
        private void CargarCargoId(int idTipo)
        {
            cboCargo.Items.Clear();
            readerCargo = controlCont.obtenerCargoId(idTipo);
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboCargo.Items.Add(new ListItem("Seleccione el Cargo", "0"));
            }
            if (idioma == "Ingles")
            {
                cboCargo.Items.Add(new ListItem("Select the Charge", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCargo.Items.Add(new ListItem("Selecione a Carga", "0"));
            }
            while (readerCargo.Read())
            {
                cboCargo.Items.Add(new ListItem(readerCargo.GetString(1), readerCargo.GetInt32(0).ToString()));
            }
            readerCargo.Close();
            controlCont.cerrarConexion();
        }


        //CARGAMOS EL COMBO DE PROFESION
        private void CargarProfesion()
        {
            //cboCargo.Items.Clear();
            readerProfesion = controlCont.obtenerProfesion();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                cboTipoProfesion.Items.Add(new ListItem("Seleccione la Profesión", "0"));
            }
            if (idioma == "Ingles")
            {
                cboTipoProfesion.Items.Add(new ListItem("Select the Profession", "0"));
            }
            if (idioma == "Portugues")
            {
                cboTipoProfesion.Items.Add(new ListItem("Selecione a Profesion", "0"));
            }
            while (readerProfesion.Read())
            {
                cboTipoProfesion.Items.Add(new ListItem(readerProfesion.GetString(1), readerProfesion.GetInt32(0).ToString()));
            }
            readerProfesion.Close();
            controlCont.cerrarConexion();
        }

        //CARGAMOS EL COMBO DE FERIAS
        protected void CargarFeria()
        {
            //cboFeria.Items.Clear();
            readerFeria = controlCont.consultarFeria();
            while (readerFeria.Read())
            {
                cboFeria.Items.Add(new ListItem(readerFeria.GetString(1), readerFeria.GetInt32(0).ToString()));
            }
            readerFeria.Close();
        }

        //CARGAMOS TODOS LOS PAISES
        private void poblarListaPais2()
        {
            //CboPaisMatriz.Items.Clear();
            readerPais = controlUbi.poblarListaPais();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add(new ListItem("Seleccione el Pais","0"));
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add(new ListItem ("Select the Country","0"));
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add(new ListItem ("Selecione o País","0"));
            }

            if (readerPais != null)
            {
                while (readerPais.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(readerPais.GetString(1), readerPais.GetInt32(0).ToString()));
                }
            }
            else
            {
                string mensaje = ""; if (idioma == "Español")
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

            readerPais.Close();
            controlUbi.cerrarConexion();
        }

        //CARGAMOS LOS PAISES DE ACUERDO AL ID DEL REPRESENTANTE
        private void poblarListaPais()
        {
            string rcID = (string)Session["rcID"];

            //CboPaisMatriz.Items.Clear();
            //CboPaisMatriz.Items.Clear();
            readerPais = controlUbi.poblarListaPaisRepresentante(Convert.ToInt32(rcID));
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")
            {
                CboPaisMatriz.Items.Add(new ListItem("Seleccione el Pais","0"));
            }
            if (idioma == "Ingles")
            {
                CboPaisMatriz.Items.Add(new ListItem("Select the Country","0"));
            }
            if (idioma == "Portugues")
            {
                CboPaisMatriz.Items.Add(new ListItem ("Selecione o País","0"));
            }

            if (readerPais != null)
            {
                while (readerPais.Read())
                {
                    CboPaisMatriz.Items.Add(new ListItem(readerPais.GetString(0), readerPais.GetInt32(1).ToString()));
                }
            }
            else
            {
                string mensaje = ""; if (idioma == "Español")
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
            readerPais.Close();
            controlUbi.cerrarConexion();
        }

        //CARGAMOS LOS COMBOS CON EL IDIOMA INDICADO
        public void cargarCombosIdiomas()
        {
            string idioma = (string)Session["Idioma"];
            this.cargarComboObraIdiomas();
            this.cargarComboClienteIdiomas();
            this.cargarComboCiudadIdiomas();
            this.cargarComboFeriaIdiomas();
        }

        //CARGAMOS EL COMBO DE FERIAS INICIALMENTE POR IDIOMAS
        public void cargarComboFeriaIdiomas()
        {
            cboFeria.Items.Clear();
            string idioma = (string)Session["Idioma"];
           
            if (idioma == "Español")
            {
                cboFeria.Items.Add(new ListItem("Seleccione la Feria", "0"));
            }
            if (idioma == "Ingles")
            {
                cboFeria.Items.Add(new ListItem("Select the show", "0"));
            }
            if (idioma == "Portugues")
            {
                cboFeria.Items.Add(new ListItem("Selecione o show", "0"));
            }
        }

        //CARGAMOS EL COMBO DE CIUDAD INICIALMENTE POR IDIOMAS
        public void cargarComboCiudadIdiomas()
        {
            cboCiudadMatriz.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {
                cboCiudadMatriz.Items.Add(new ListItem ("Seleccione la Ciudad","0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatriz.Items.Add(new ListItem("Select the City","0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatriz.Items.Add(new ListItem("Selecione a Cidade","0"));
            }
        }

        //CARGAMOS EL COMBO DE CLIENTE INICIALMENTE POR IDIOMAS
        public void cargarComboClienteIdiomas()
        {
            cboClienteMatriz.Items.Clear();
            string idioma = (string)Session["Idioma"];

            if (idioma == "Español")
            {                
                cboClienteMatriz.Items.Add(new ListItem("Seleccione La Empresa", "0"));                
            }
            if (idioma == "Ingles")
            {                
                cboClienteMatriz.Items.Add(new ListItem("Select Company", "0"));
            }
            if (idioma == "Portugues")
            {
                cboClienteMatriz.Items.Add(new ListItem("Selecione Companhia", "0"));
            }
        }

        //CARGAMOS EL COMBO DE OBRA INICIALMENTE POR IDIOMAS
        public void cargarComboObraIdiomas()
        {
            cboObra.Items.Clear();
            string idioma = (string)Session["Idioma"];
            if (idioma == "Español")         
            {               
                cboObra.Items.Add(new ListItem("Seleccione la Obra", "0"));                
            }
            if (idioma == "Ingles")
            {               
                cboObra.Items.Add(new ListItem("Select the Project", "0"));               
            }
            if (idioma == "Portugues")
            {              
                cboObra.Items.Add(new ListItem("Selecione el Projeto", "0"));                
            }
        }

        //CARGAMOS LA ZONA DEL REPRESENTAMTE
        private void PoblarZona()
        {
            cboCiudadMatriz.Items.Clear();
            readerCiudad = controlCont.poblarZona("n_rol", "p_rol");
            cboCiudadMatriz.Items.Add(new ListItem("Seleccione la Zona", "0"));
            while (readerCiudad.Read())
            {
                cboCiudadMatriz.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
            }

            readerCiudad.Close();
            controlCont.cerrarConexion();
        }

        protected void CboPaisMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboClienteMatriz.Items.Clear();
            this.cargarComboClienteIdiomas();
            cboObra.Items.Clear();
            this.cargarComboObraIdiomas();
            if (CboPaisMatriz.SelectedItem.Text == "Seleccione el Pais")
            {
            }
            else
            {               
                this.poblarListaCiudad(Convert.ToInt32(CboPaisMatriz.SelectedValue));
            }
            //this.cargarReporteContactoXId();
            Prefijo();
        }

        //CARGAMOS LAS CIUDADES DEL PAIS SELECCIONADO
        private void poblarListaCiudad(int pais_id)
        {
            if (Convert.ToString(Session["bandera"]) != "Actualizando")
            cboCiudadMatriz.Items.Clear();

            string idioma = (string)Session["Idioma"];
            string rcID = (string)Session["rcID"];
            int arRol = (int)Session["Rol"];
            int seleccionado, rp_id;

            if (idioma == "Español")
            {
                cboCiudadMatriz.Items.Add(new ListItem("Seleccione la Ciudad","0"));
            }
            if (idioma == "Ingles")
            {
                cboCiudadMatriz.Items.Add(new ListItem("Select the City", "0"));
            }
            if (idioma == "Portugues")
            {
                cboCiudadMatriz.Items.Add(new ListItem("Selecione a Cidade","0"));
            }

            if ((arRol == 3) && (Convert.ToInt32(CboPaisMatriz.SelectedItem.Value) == 8))
            {
                readerCiudad = controlUbi.poblarCiudadesRepresentantesColombia(Convert.ToInt32(rcID));

                if (readerCiudad.HasRows == true)
                {
                    while (readerCiudad.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                    }
                }
                readerCiudad.Close();
            }
            else
            {
                readerCiudad = controlUbi.poblarListaCiudades(Convert.ToInt32(CboPaisMatriz.SelectedItem.Value));
                if (readerCiudad.HasRows == true)
                {
                    while (readerCiudad.Read())
                    {
                        cboCiudadMatriz.Items.Add(new ListItem(readerCiudad.GetString(1), readerCiudad.GetInt32(0).ToString()));
                    }
                }
            }    
        }

       
        protected void cboCiudadMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboObra.Items.Clear();
            this.cargarComboObraIdiomas();
            if (cboCiudadMatriz.SelectedItem.Text == "Seleccione la Ciudad")
            {
            }
            else
            {
                bool respuesta = this.poblarListaCliente();
                if (respuesta == true)
                {
                    
                }
                else
                {
                    string mensaje = "";
                    string idioma = (string)Session["Idioma"];
                    if (idioma == "Español")
                    {
                        mensaje = "Usted no posee clientes asociados.";
                    }
                    if (idioma == "Ingles")
                    {
                        mensaje = "You have no partner customers.";
                    }
                    if (idioma == "Portugues")
                    {
                        mensaje = "Você não tem clientes parceiros.";
                    }
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);    
                }
            }
            //this.cargarReporteContactoXId();
        }

        //CARGAMOS LOS CLIENTES DE LA CIUDAD SELECCIONADA
        public bool poblarListaCliente()
        {
            int seleccionado;
            if (Convert.ToString(Session["bandera"]) != "Actualizando")
            cboClienteMatriz.Items.Clear();

            seleccionado = Convert.ToInt32(cboCiudadMatriz.SelectedItem.Value);
            reader = controlCont.consultarDatosClientexCiudad(seleccionado);
            string idioma = (string)Session["Idioma"];
            bool respuesta= false;

            if (idioma == "Español")
            {
                cboClienteMatriz.Items.Add(new ListItem("Seleccione La Empresa", "0"));
            }
            if (idioma == "Ingles")
            {
                cboClienteMatriz.Items.Add(new ListItem("Select Company", "0"));
            }
            if (idioma == "Portugues")
            {
                cboClienteMatriz.Items.Add(new ListItem("Selecione Companhia", "0"));
            }
            if (reader.HasRows == true)
            {
                while (reader.Read())
                {
                    cboClienteMatriz.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                respuesta = true;
            }
            else
            {
                respuesta = false;
                
            }

            reader.Close();
            controlCont.cerrarConexion();

            return respuesta;
        }


        protected void cboClienteMatriz_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboObra.Items.Clear();
            this.poblarListaObra();
        }

        //CARGAMOS LAS OBRAS DEL CLIENTE SELECCIONADO
        private void poblarListaObra()
        {
            if (Convert.ToString(Session["bandera"]) != "Actualizando")
            cboObra.Items.Clear();

            string idioma = (string)Session["Idioma"];
            if ((cboClienteMatriz.SelectedItem.Text == "Seleccione La Empresa")||(cboClienteMatriz.SelectedItem.Text == "Select Company")||
                (cboClienteMatriz.SelectedItem.Text == "Selecione Companhia"))
            {
                //this.cargarReporteContacto();
                cboObra.Items.Clear();
            }
            else
            {
                readerObra = controlCont.ObtnObrasDistPV(Convert.ToInt32(cboClienteMatriz.SelectedValue));               
                
                if (idioma == "Español")
                {
                    cboObra.Items.Add(new ListItem("Seleccione la Obra", "0"));
                }
                if (idioma == "Ingles")
                {
                    cboObra.Items.Add(new ListItem("Select the Project", "0"));
                }
                if (idioma == "Portugues")
                {
                    cboObra.Items.Add(new ListItem("Selecione el Projeto", "0"));
                }
                while (readerObra.Read())
                {
                    cboObra.Items.Add(new ListItem(readerObra.GetString(1), readerObra.GetInt32(0).ToString()));

                }
                readerObra.Close();
                controlCont.cerrarConexion();
                //this.cargarReporteContactoXId();
            }
        }

        //CARGAMOS LOS DATOS DEL CONTACTO CLIENTE
        private void cargarContCliente(string idContCliente)
        {
            string idioma = (string)Session["Idioma"];

            readerContacto = controlCont.consultarContactoCliente(Convert.ToInt32(idContCliente));
            readerContacto.Read();
            cboTipoContacto.Items.Clear();
            cboCargo.Items.Clear();

            string tipo = readerContacto.GetValue(46).ToString();
            string tipoProfesion = readerContacto.GetValue(48).ToString();
            bool cCliente = readerContacto.GetSqlBoolean(13).Value;
            bool cObra = readerContacto.GetSqlBoolean(14).Value;
            bool cTecnico = readerContacto.GetSqlBoolean(15).Value;
            Session["bandera"] = "Actualizando";
            int rol = (int) Session["Rol"];
            
            if (idioma == "Español")
            {
                reader = controlCont.ConsultarTipoContactoID(Convert.ToInt32(tipo));
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarTipoContacto();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarProfesion();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(1), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();
            }
            if (idioma == "Ingles")
            {
                reader = controlCont.ConsultarTipoContactoID(Convert.ToInt32(tipo));
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarTipoContacto();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarProfesion();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(2), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();
            }
            if (idioma == "Portugues")
            {
                reader = controlCont.ConsultarTipoContactoID(Convert.ToInt32(tipo));
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarTipoContacto();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();

                reader = controlCont.ConsultarProfesion();
                while (reader.Read())
                {
                    cboTipoContacto.Items.Add(new ListItem(reader.GetString(3), reader.GetInt32(0).ToString()));
                }
                reader.Close();
                controlCont.cerrarConexion();
            }

            txtNombre1.Text = readerContacto.GetSqlString(4).Value;            
            txtApellido1.Text = readerContacto.GetSqlString(6).Value;

            cboCargo.Items.Add(new ListItem(readerContacto.GetSqlString(3).Value.ToString(), readerContacto.GetSqlInt32(2).Value.ToString()));
            cboCargo.Items.Add(new ListItem("-------", "0"));

            cboTipoProfesion.Items.Add(new ListItem(readerContacto.GetSqlString(48).Value.ToString(), readerContacto.GetSqlInt32(47).Value.ToString()));
            cboTipoProfesion.Items.Add(new ListItem("-------", "0"));
           
            txtTelefono1.Text = readerContacto.GetSqlString(8).Value;
            txtTelefono2.Text = readerContacto.GetSqlString(9).Value;
            txtTelefMovil.Text = readerContacto.GetSqlString(12).Value;
            txtCorreo1.Text = readerContacto.GetSqlString(10).Value; 
            txtCorreoPersonal.Text = readerContacto.GetSqlString(11).Value;

            cboFeria.Items.Add(new ListItem(readerContacto.GetSqlString(19).Value.ToString(), readerContacto.GetSqlInt32(18).Value.ToString()));
            cboFeria.Items.Add(new ListItem("-------", "0"));

            CboPaisMatriz.Items.Add(new ListItem(readerContacto.GetSqlString(39).Value.ToString(), readerContacto.GetSqlInt32(38).Value.ToString()));
            CboPaisMatriz.Items.Add(new ListItem("-------", "0"));

            cboCiudadMatriz.Items.Add(new ListItem(readerContacto.GetSqlString(41).Value.ToString(), readerContacto.GetSqlInt32(40).Value.ToString()));
            cboCiudadMatriz.Items.Add(new ListItem("-------", "0"));
            poblarListaCiudad(readerContacto.GetSqlInt32(38).Value);

            cboClienteMatriz.Items.Add(new ListItem(readerContacto.GetSqlString(43).Value.ToString(), readerContacto.GetSqlInt32(42).Value.ToString()));
            cboClienteMatriz.Items.Add(new ListItem("-------", "0"));
            poblarListaCliente();

            lblClienteP.Text = cboClienteMatriz.SelectedItem.Text;

            cboObra.Items.Add(new ListItem(readerContacto.GetSqlString(45).Value.ToString(), readerContacto.GetSqlInt32(44).Value.ToString()));
            cboObra.Items.Add(new ListItem("-------", "0"));
            poblarListaObra();

            txtFechaCumple.Text = readerContacto.GetSqlDateTime(17).Value.ToString("dd/MM/yyyy");
            if (txtFechaCumple.Text == "01/01/1900") txtFechaCumple.Text = "";
            txtHobby.Text = readerContacto.GetSqlString(16).Value;
            chkTelefono.Checked = readerContacto.GetSqlBoolean(24).Value;
            chkEmail.Checked = readerContacto.GetSqlBoolean(25).Value;
            chkReferencia.Checked = readerContacto.GetSqlBoolean(26).Value;
            chkTrabCampo.Checked = readerContacto.GetSqlBoolean(28).Value;
            chkVisita.Checked = readerContacto.GetSqlBoolean(29).Value;
            chkMedComunicacion.Checked = readerContacto.GetSqlBoolean(30).Value;
            chkCharlas.Checked = readerContacto.GetSqlBoolean(31).Value;
            chkConferencias.Checked = readerContacto.GetSqlBoolean(32).Value;
            chkWeb.Checked = readerContacto.GetSqlBoolean(33).Value;
            chkSeminarios.Checked = readerContacto.GetSqlBoolean(34).Value;
            chkPublicImpresa.Checked = readerContacto.GetSqlBoolean(35).Value;
            chkPersonal.Checked = readerContacto.GetSqlBoolean(36).Value;
            txtComentarios.Text = readerContacto.GetSqlString(37).Value;
            txtUsuActualiza.Text = readerContacto.GetSqlString(49).Value;
            txtFechaActualizacion.Text = readerContacto.GetSqlDateTime(50).Value.ToString("dd/MM/yyyy");
            if (txtFechaActualizacion.Text == "01/01/1900") txtFechaActualizacion.Text = "";
            txtNombreCargo.Text = readerContacto.GetSqlString(51).Value;
            txtprefijo1.Text = readerContacto.GetSqlString(52).Value;
            txtprefijo2.Text = readerContacto.GetSqlString(53).Value;
            txtprefijo3.Text = readerContacto.GetSqlString(54).Value;
            chkNotificacion.Checked = readerContacto.GetSqlBoolean(55).Value;
            txtLinkedIn.Text= readerContacto.GetSqlString(56).Value;

            readerCargo = controlCont.obtenerCargoId(Convert.ToInt32(cboTipoContacto.SelectedItem.Value));
            while (readerCargo.Read())
            {
                cboCargo.Items.Add(new ListItem(readerCargo.GetString(1), readerCargo.GetInt32(0).ToString()));
            }
            readerCargo.Close();

            readerProfesion = controlCont.obtenerProfesion();
            while (readerProfesion.Read())
            {
                cboTipoProfesion.Items.Add(new ListItem(readerProfesion.GetString(1), readerProfesion.GetInt32(0).ToString()));
            }

            this.CargarFeria();
            readerProfesion.Close();
            //Session["bandera"] = "Iniciando";

        }

        //---GUARDAMOS O ACTUALIZAMOS EL CONTACTO
        protected void btnGuardarCont_Click(object sender, EventArgs e)
        {
            string idioma = (string)Session["Idioma"];
            bool ctelefono = false, trabcampo = false, personal = false, email = false, visita = false, publicidad = false, referencia = false,
                seminarios = false, medicomunic = false, conferencia = false, charlas = false, web = false, contCliente = false, contTecnico = false,
                contObra = false, feria = false, noNotificar = false;

            ctelefono = chkTelefono.Checked; 
            trabcampo = chkTrabCampo.Checked;
            personal = chkPersonal.Checked;
            email = chkEmail.Checked;
            visita = chkVisita.Checked;
            publicidad = chkPublicImpresa.Checked;
            referencia = chkReferencia.Checked;
            seminarios = chkSeminarios.Checked;
            medicomunic = chkMedComunicacion.Checked;
            conferencia = chkConferencias.Checked;
            charlas = chkCharlas.Checked;
            web = chkWeb.Checked;
            noNotificar = chkNotificacion.Checked;
          
            if (txtNombre1.Text == "" || txtApellido1.Text == "" || txtTelefono1.Text=="" || txtCorreo1.Text == ""
                || txtCorreo1.Text == "correo@com.co" || txtCorreoPersonal.Text == ""
                || txtCorreoPersonal.Text == "correoPersonal@com.co")
            {
                if (txtNombre1.Text=="")
                    txtNombre1.BackColor = System.Drawing.Color.Yellow;
                if (txtApellido1.Text == "")
                    txtApellido1.BackColor = System.Drawing.Color.Yellow;
                if (txtTelefono1.Text == "")
                    txtTelefono1.BackColor = System.Drawing.Color.Yellow;
                if ((txtCorreo1.Text == "")||(txtCorreo1.Text == "correo@com.co"))
                    txtCorreo1.BackColor = System.Drawing.Color.Yellow;
                if ((txtCorreoPersonal.Text == "") || (txtCorreoPersonal.Text == "correoPersonal@com.co"))
                    txtCorreoPersonal.BackColor = System.Drawing.Color.Yellow;

                string mensaje = "";
                if (idioma == "Español")
                {
                    mensaje = "Verifique los campos obligatorios (*)";
                }
                if (idioma == "Ingles")
                {
                    mensaje = "Please check the required fields (*)";
                }
                if (idioma == "Portugues")
                {
                    mensaje = "Por favor, verifique os campos obrigatórios (*)";
                }
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }
             else
            {
                if (cboTipoContacto.SelectedItem.Text == "Obra" && cboObra.SelectedItem.Value=="0")
                {
                    
                }
                else
                {
                    if (cboTipoContacto.SelectedItem.Text == "Seleccione el Tipo" || cboTipoContacto.SelectedItem.Text == "Select the Type" || cboTipoContacto.SelectedItem.Text == "Selecione o tipo de"
                        || cboCargo.SelectedItem.Text == "Seleccione el Cargo" || cboCargo.SelectedItem.Text == "Select the Charge" || cboCargo.SelectedItem.Text == "Selecione a Carga"
                        || cboClienteMatriz.SelectedItem.Text == "Seleccione La Empresa" || cboClienteMatriz.SelectedItem.Text == "Select Company" || cboClienteMatriz.SelectedItem.Text == "Selecione Companhia" || cboTipoProfesion.SelectedItem.Value == "0")
                    {
                        string mensaje = "";
                        if (idioma == "Español")
                        {
                            mensaje = "Verifique los campos obligatorios (*)";
                        }
                        if (idioma == "Ingles")
                        {
                            mensaje = "Please check the required fields (*)";
                        }
                        if (idioma == "Portugues")
                        {
                            mensaje = "Por favor, verifique os campos obrigatórios (*)";
                        }
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                    }
                    else
                    {
                        if (ctelefono == false && trabcampo == false && personal == false && email == false && visita == false && publicidad == false
                            && referencia == false && seminarios == false && medicomunic == false && conferencia == false && charlas == false && web == false)
                        {
                            string mensaje = "";
                            if (idioma == "Español")
                            {
                                mensaje = "Debe indicar el como se contacto";
                            }
                            if (idioma == "Ingles")
                            {
                                mensaje = "Please specify as contact";
                            }
                            if (idioma == "Portugues")
                            {
                                mensaje = "Por favor, especifique como contato";
                            }
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                        }
                        else
                        {
                            if (cboFeria.SelectedItem.Text != "Seleccione la Feria" || cboFeria.SelectedItem.Text != "Select the show" || cboFeria.SelectedItem.Text != "Selecione o show" || cboFeria.SelectedItem.Text != "N/A")
                            {
                                feria = true;
                            }
                            txtNombre1.Text = txtNombre1.Text.ToString().ToUpperInvariant();                           
                            txtApellido1.Text = txtApellido1.Text.ToString().ToUpperInvariant();                            
                            txtTelefono1.Text = txtTelefono1.Text.ToString().ToUpperInvariant();
                            txtTelefono2.Text = txtTelefono2.Text.ToString().ToUpperInvariant();
                            txtTelefMovil.Text = txtTelefMovil.Text.ToString().ToUpperInvariant();
                            txtCorreo1.Text = txtCorreo1.Text.ToString().ToLowerInvariant();
                            txtCorreoPersonal.Text = txtCorreoPersonal.Text.ToString().ToLowerInvariant();
                            txtHobby.Text = txtHobby.Text.ToString().ToUpperInvariant();

                            if (cboTipoContacto.SelectedItem.Text == "Cliente")
                                contCliente = true;
                            if (cboTipoContacto.SelectedItem.Text == "Tecnico")
                                contTecnico = true;
                            if (cboTipoContacto.SelectedItem.Text == "Obra")
                                contObra = true;

                            int clienteId = 0, obraId = 0, tipoCargoId = 0, feriaId = 0, profesionId=0;
                            string nombre1, nombre2, apellido1, apellido2, telefono1, telefono2,
                                email1, email2, movil, hobby, fechacumple, usucrea, fechacrea;

                            clienteId = Convert.ToInt32(cboClienteMatriz.SelectedItem.Value);
                            obraId = Convert.ToInt32(cboObra.SelectedItem.Value);
                            tipoCargoId = Convert.ToInt32(cboCargo.SelectedItem.Value);
                            feriaId = Convert.ToInt32(cboFeria.SelectedItem.Value);
                            profesionId = Convert.ToInt32(cboTipoProfesion.SelectedItem.Value);
                            nombre1 = txtNombre1.Text;
                            apellido1 = txtApellido1.Text;                            
                            telefono1 = txtTelefono1.Text;
                            telefono2 = txtTelefono2.Text;
                            email1 = txtCorreo1.Text;
                            email2 = txtCorreoPersonal.Text;
                            movil = txtTelefMovil.Text;
                            hobby = txtHobby.Text;
                            fechacumple = txtFechaCumple.Text;
                            usucrea = (string)Session["Nombre_Usuario"];
                            fechacrea = DateTime.Now.ToString("dd/MM/yyyy");
                            int repId = Convert.ToInt16(Session["rcID"]);
                            string mensaje = "";
                            string mensajenotificacion = "";

                            if (chkNotificacion.Checked == true)
                            {
                                mensajenotificacion = " - No se Enviaran Notificaciones a este Contacto";
                            }

                            //VERIFICANDO SI LA OPCION ES GUARDAR
                            if (btnGuardarCont.Text == "Guardar" || btnGuardarCont.Text == "Save" || btnGuardarCont.Text == "Salvar")
                            {
                                int insertadoCont = controlCont.guardarDatosContactoCliente(clienteId, obraId, tipoCargoId, nombre1, "", apellido1, "",
                                    telefono1, telefono2, email1, email2, movil, contCliente, contObra, contTecnico, hobby, fechacumple, feriaId, usucrea, fechacrea,
                                    ctelefono, email, referencia, feria, trabcampo, visita, medicomunic, charlas, conferencia, web, seminarios, publicidad, personal,
                                    txtComentarios.Text, profesionId,txtNombreCargo.Text, txtprefijo1.Text,txtprefijo2.Text,txtprefijo3.Text, noNotificar,txtLinkedIn.Text);

                                if (insertadoCont != -1)
                                {
                                    //this.cargarReporteContactoXId(); 
                                    if (idioma == "Español")
                                    {
                                        mensaje = "Contacto creado exitosamente";
                                    }
                                    if (idioma == "Ingles")
                                    {
                                        mensaje = "Contact servant satisfactorily";
                                    }
                                    if (idioma == "Portugues")
                                    {
                                        mensaje = "Contacto Creado satisfactoriamente";
                                    } 
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + mensajenotificacion+"')", true);

                                    //verifico si es un contanto SIAT para actualizar el combo del modulo SIAT
                                    /*SIAT - Metrolink*/ 
                                    if (Request.QueryString["Modulo"] == "SIAT")
                                    {
                                        SiatPlanVisita SPV = new SiatPlanVisita();
                                        String combo = SPV.recargarCont(Session["Cliente"].ToString());
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "script40", combo, true);
                                    }
                                }
                                else
                                { 
                                    if (idioma == "Español")
                                    {
                                        mensaje = "No fue posible crear el contacto";
                                    }
                                    if (idioma == "Ingles")
                                    {
                                        mensaje = "Unable to create the contact";
                                    }
                                    if (idioma == "Portugues")
                                    {
                                        mensaje = "Incapaz de criar o contato";
                                    }
                                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                                }
                                btnGuardarCont.Text = "Actualizar";
                            }
                            else
                            {
                                //VERIFICANDO SI LA OPCION ES ACTUALIZAR
                                if (btnGuardarCont.Text == "Actualizar" || btnGuardarCont.Text == "Update" || btnGuardarCont.Text == "Atualizar")
                                {
                                    string idContCliente = Request.QueryString["idContCliente"];
                                    int actualizarCont = controlCont.actualizarDatosContactoCliente(clienteId, obraId, tipoCargoId, nombre1, "", apellido1, "",
                                        telefono1, telefono2, email1, email2, movil, contCliente, contObra, contTecnico, hobby, fechacumple, feriaId, usucrea, fechacrea,
                                        ctelefono, email, referencia, feria, trabcampo, visita, medicomunic, charlas, conferencia, web, seminarios, publicidad, personal,
                                        txtComentarios.Text, Convert.ToInt32(idContCliente), profesionId,txtNombreCargo.Text, txtprefijo1.Text,txtprefijo2.Text,txtprefijo3.Text,noNotificar,txtLinkedIn.Text);

                                    if (actualizarCont != -1)
                                    {
                                        //this.cargarReporteContactoXId();
                                        
                                        if (idioma == "Español")
                                        {
                                            mensaje = "Contacto actualizado exitosamente";
                                        }
                                        if (idioma == "Ingles")
                                        {
                                            mensaje = "Contact updated satisfactorily";
                                        }
                                        if (idioma == "Portugues")
                                        {
                                            mensaje = "contato atualizado satisfatoriamente";
                                        }
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + mensajenotificacion+"')", true);                
                                        btnGuardarCont.Text = "Actualizar";
                                    }
                                    else
                                    { 
                                        if (idioma == "Español")
                                        {
                                            mensaje = "No fue posible crear el contacto";
                                        }
                                        if (idioma == "Ingles")
                                        {
                                            mensaje = "Unable to create the contact";
                                        }
                                        if (idioma == "Portugues")
                                        {
                                            mensaje = "Incapaz de criar o contato";
                                        }
                                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                                    }
                                }                                
                            }                           
                        }
                    }
                }                
            }
        }

        private void Prefijo()
        {
            reader = concli.ObtenerPrefijo(Convert.ToInt32(CboPaisMatriz.SelectedItem.Value));
            if (reader.HasRows == true)
            {
                if (reader.Read() == true)
                {
                    indicativo1.Text = reader.GetValue(1).ToString();
                    indicativo2.Text = indicativo1.Text;
                    indicativo3.Text = indicativo1.Text;
                }
                else
                {
                    indicativo1.Text = "0";
                    indicativo2.Text = indicativo1.Text;
                    indicativo3.Text = indicativo1.Text;
                }
            }

            reader.Close();
            concli.CerrarConexion();
        }

        //CARGAMOS LA PAGINA PARA UN NUEVO CONTACTO
        protected void bntNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("Contacto.aspx?idCliente="+Session["Cliente"].ToString());
        }

        protected void txtNombre1_TextChanged(object sender, EventArgs e)
        {
            if (txtNombre1.BackColor == System.Drawing.Color.Yellow)
                txtNombre1.BackColor = System.Drawing.Color.White;
        }

        protected void txtApellido1_TextChanged(object sender, EventArgs e)
        {
            if (txtApellido1.BackColor == System.Drawing.Color.Yellow)
                txtApellido1.BackColor = System.Drawing.Color.White;
        }

        protected void txtTelefono1_TextChanged(object sender, EventArgs e)
        {
            if (txtTelefono1.BackColor == System.Drawing.Color.Yellow)
                txtTelefono1.BackColor = System.Drawing.Color.White;
        }

        protected void txtCorreo1_TextChanged(object sender, EventArgs e)
        {
            if (txtCorreo1.BackColor == System.Drawing.Color.Yellow)
                txtCorreo1.BackColor = System.Drawing.Color.White;
        }

        //EVENTO PARA ELIMINAR EL CONTACTO
        protected void btnEliminarCont_Click(object sender, EventArgs e)
        {
            string idContCliente = Request.QueryString["idContCliente"];
            string idioma = Request.QueryString["Idioma"];
            string mensaje = "";
            int eliminaContacto = controlCont.eliminarContactoCliente(Convert.ToInt32(idContCliente));

            if (eliminaContacto != -1)
            {               
                mensaje = "Contacto eliminado exitosamente";
               
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
                btnGuardarCont.Enabled = false;
            }
            else
            {
                mensaje = "No fue posible eliminar el contacto";
                
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alert", "alert('" + mensaje + "')", true);                
            }
            
        }

        protected void cboTipoContacto_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.CargarCargoId(Convert.ToInt32(cboTipoContacto.SelectedItem.Value));
        }

        protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("Cliente.aspx?idCliente=" + Session["Cliente"].ToString());
        }

        protected void txtCorreoPersonal_TextChanged(object sender, EventArgs e)
        {
            if (txtCorreoPersonal.BackColor == System.Drawing.Color.Yellow)
                txtCorreoPersonal.BackColor = System.Drawing.Color.White;
        }
    }
}